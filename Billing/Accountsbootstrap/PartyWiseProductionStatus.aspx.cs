using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class PartyWiseProductionStatus : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsProcess = objBs.gridProcess();
                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    ddlProcess.DataSource = dsProcess.Tables[0];
                    ddlProcess.DataTextField = "Process";
                    ddlProcess.DataValueField = "ProcessId";
                    ddlProcess.DataBind();
                    ddlProcess.Items.Insert(0, "All");
                }

                DataSet dsProcessLedger = objBs.GetApprovedProcessLedger();
                if (dsProcessLedger.Tables[0].Rows.Count > 0)
                {
                    ddlProcessLedger.DataSource = dsProcessLedger.Tables[0];
                    ddlProcessLedger.DataTextField = "LedgerName";
                    ddlProcessLedger.DataValueField = "LedgerID";
                    ddlProcessLedger.DataBind();
                    ddlProcessLedger.Items.Insert(0, "All");
                }
                else
                {
                    ddlProcessLedger.Items.Insert(0, "All");
                }

                DataSet dsset = objBs.getLedger(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlBuyerCode.DataSource = dsset.Tables[0];
                    ddlBuyerCode.DataTextField = "CompanyCode";
                    ddlBuyerCode.DataValueField = "LedgerID";
                    ddlBuyerCode.DataBind();
                    ddlBuyerCode.Items.Insert(0, "All");
                }

                DataSet dsExcNo = objBs.getAllExcNo_IsShipped(ddlBuyerCode.SelectedValue, "False");
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExcNo.Tables[0];
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderId";
                    chkExcNo.DataBind();
                }

            }
        }

        protected void buyer_order(object sender, EventArgs e)
        {
            DataSet dsExcNo = objBs.getAllExcNo_IsShipped(ddlBuyerCode.SelectedValue, "False");
            if (dsExcNo.Tables[0].Rows.Count > 0)
            {
                chkExcNo.DataSource = dsExcNo.Tables[0];
                chkExcNo.DataTextField = "ExcNo";
                chkExcNo.DataValueField = "BuyerOrderId";
                chkExcNo.DataBind();
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            gvPartyWiseProductionStatus.DataSource = null;
            gvPartyWiseProductionStatus.DataBind();

            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string BuyerOrderId = "";
            string IsFirst = "Yes";

            foreach (ListItem listItem in chkExcNo.Items)
            {
                #region
                if (chkExcNo.SelectedIndex < 0)
                {
                    BuyerOrderId = "All";
                }
                else
                {
                    if (listItem.Selected)
                    {
                        if (IsFirst == "Yes")
                        {
                            BuyerOrderId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                        }
                    }
                }

                #endregion
            }

            DataSet ds1 = objBs.PartywiseProductionStatus1(ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue, BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                DataSet ds2 = objBs.PartywiseProductionStatus2(ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue, BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsStyle = objBs.PartywiseProductionStatus22(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue, "False");
                DataSet dsStyleQtyDate = objBs.PartywiseProductionStatus5(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsProcessQty = objBs.PartywiseProductionStatus4(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue, "False");
                DataSet dsProcessQtyDate = objBs.ProductionStatus6();

                #region

                DataTable DT = new DataTable();
                DT.Columns.Add(new DataColumn("ExcNo"));
                DT.Columns.Add(new DataColumn("Shipment"));
                DT.Columns.Add(new DataColumn("StyleNo"));
                DT.Columns.Add(new DataColumn("OrderQty"));
                DT.Columns.Add(new DataColumn("IssQty"));
                DT.Columns.Add(new DataColumn("RecQty"));
                DT.Columns.Add(new DataColumn("DmgQty"));
                DT.Columns.Add(new DataColumn("TtlQty"));
                DT.Columns.Add(new DataColumn("RecBalQty"));
                DT.Columns.Add(new DataColumn("IssBalQty"));

                DT.Columns.Add(new DataColumn("QtyDate"));

                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    string IsHeader = "No";

                    int OrdQty = 0; int IssQty = 0; int RecQty = 0; int DmgQty = 0; int TtlQty = 0; int IssBalQty = 0; int BalQty = 0;
                    DataRow[] RowStylesC = dsStyle.Tables[0].Select("LedgerID='" + dr["LedgerID"] + "' and ProcessId=5 ");
                    for (int j = 0; j < RowStylesC.Length; j++)
                    {
                        if (IsHeader == "No")
                        {
                            DataRow DRM = DT.NewRow();
                            DRM["ExcNo"] = dr["LedgerName"];
                            DRM["StyleNo"] = "- Cutting";
                            DT.Rows.Add(DRM);
                            IsHeader = "Yes";
                        }

                        DataRow DRM1 = DT.NewRow();
                        DRM1["ExcNo"] = RowStylesC[j]["ExcNo"];
                        DRM1["Shipment"] = Convert.ToDateTime(RowStylesC[j]["ShipmentDate"]).ToString("dd/MM/yyyy");
                        DRM1["StyleNo"] = RowStylesC[j]["StyleNo"];
                        DRM1["OrderQty"] = Convert.ToInt32(RowStylesC[j]["OQty"]);
                        DRM1["IssQty"] = "";
                        DRM1["RecQty"] = Convert.ToInt32(RowStylesC[j]["RecQty"]);
                        DRM1["DmgQty"] = Convert.ToInt32(RowStylesC[j]["DmgQty"]);
                        DRM1["TtlQty"] = Convert.ToInt32(RowStylesC[j]["TotalQty"]);
                        DRM1["RecBalQty"] = Convert.ToInt32(RowStylesC[j]["BalQty"]);
                        DRM1["IssBalQty"] = Convert.ToInt32(RowStylesC[j]["BalQty"]);

                        string QtyDate = "";
                        string First = "Yes";

                        DataRow[] RowsQtyDate = dsStyleQtyDate.Tables[0].Select("BuyerOrderId='" + RowStylesC[j]["BuyerOrderId"] + "' and StyleNoId='" + RowStylesC[j]["StyleNoId"] + "' ");
                        for (int k = 0; k < RowsQtyDate.Length; k++)
                        {
                            if (First == "Yes")
                            {
                                QtyDate = " " + Convert.ToDateTime(RowsQtyDate[k]["DefaultDate"]).ToString("dd/MMM") + "(" + Convert.ToInt32(RowsQtyDate[k]["TtlQty"]) + ")";
                                First = "No";
                            }
                            else
                            {
                                QtyDate = QtyDate + ", " + Convert.ToDateTime(RowsQtyDate[k]["DefaultDate"]).ToString("dd/MMM") + "(" + Convert.ToInt32(RowsQtyDate[k]["TtlQty"]) + ")";
                            }
                        }

                        DRM1["QtyDate"] = "   " + QtyDate;

                        OrdQty += Convert.ToInt32(RowStylesC[j]["OQty"]);
                        RecQty += Convert.ToInt32(RowStylesC[j]["RecQty"]);
                        DmgQty += Convert.ToInt32(RowStylesC[j]["DmgQty"]);
                        TtlQty += Convert.ToInt32(RowStylesC[j]["TotalQty"]);
                        IssBalQty += Convert.ToInt32(RowStylesC[j]["BalQty"]);
                        BalQty += Convert.ToInt32(RowStylesC[j]["BalQty"]);

                        DT.Rows.Add(DRM1);

                    }
                    if (OrdQty != 0)
                    {
                        DataRow DRM2 = DT.NewRow();
                        DRM2["StyleNo"] = "";
                        DRM2["OrderQty"] = OrdQty;
                        DRM2["RecQty"] = RecQty;
                        DRM2["DmgQty"] = DmgQty;
                        DRM2["TtlQty"] = TtlQty;
                        DRM2["RecBalQty"] = BalQty;
                        DRM2["IssBalQty"] = IssBalQty;
                        DT.Rows.Add(DRM2);

                        OrdQty = 0; IssQty = 0; RecQty = 0; DmgQty = 0; TtlQty = 0; IssBalQty = 0; BalQty = 0;
                    }

                    DataRow[] RowPartyProcess = ds2.Tables[0].Select("LedgerID='" + dr["LedgerID"] + "'  ");
                    for (int i = 0; i < RowPartyProcess.Length; i++)
                    {
                        if (RowPartyProcess[i]["ProcessId"].ToString() != "5")
                        {
                            DataRow[] RowStyles = dsProcessQty.Tables[0].Select("LedgerID='" + dr["LedgerID"] + "' and Process='" + RowPartyProcess[i]["ProcessId"] + "' ");
                            for (int j = 0; j < RowStyles.Length; j++)
                            {
                                if (IsHeader == "No")
                                {
                                    DataRow DRM = DT.NewRow();
                                    DRM["ExcNo"] = dr["LedgerName"];
                                    DRM["StyleNo"] = "- " + RowPartyProcess[i]["Process"];
                                    DT.Rows.Add(DRM);

                                    IsHeader = "Yes";
                                }

                                DataRow DRM1 = DT.NewRow();
                                DRM1["ExcNo"] = RowStyles[j]["ExcNo"];
                                DRM1["Shipment"] = Convert.ToDateTime(RowStyles[j]["ShipmentDate"]).ToString("dd/MM/yyyy");
                                DRM1["StyleNo"] = RowStyles[j]["StyleNo"];

                                DataRow[] ROrderQty = dsStyle.Tables[0].Select("BuyerOrderMasterCuttingId='" + RowStyles[j]["BuyerOrderMasterCuttingId"] + "' and BuyerOrderId='" + RowStyles[j]["BuyerOrderId"] + "' and StyleNoId='" + RowStyles[j]["StyleNoId"] + "'  ");
                                DRM1["OrderQty"] = ROrderQty[0]["CQty"];


                                DRM1["IssQty"] = Convert.ToInt32(RowStyles[j]["Issued"]);
                                DRM1["RecQty"] = Convert.ToInt32(RowStyles[j]["Received"]);
                                DRM1["DmgQty"] = Convert.ToInt32(RowStyles[j]["Damaged"]);
                                DRM1["TtlQty"] = Convert.ToInt32(RowStyles[j]["Total"]);
                                DRM1["RecBalQty"] = Convert.ToInt32(RowStyles[j]["IssBal"]);
                                DRM1["IssBalQty"] = Convert.ToInt32(ROrderQty[0]["CQty"]) - Convert.ToInt32(RowStyles[j]["Issued"]);



                                string QtyDate = "";
                                string First = "Yes";
                                DataRow[] RowsQtyDate = dsProcessQtyDate.Tables[0].Select("BuyerOrderMasterCuttingId='" + RowStyles[j]["BuyerOrderMasterCuttingId"] + "' and StyleNoId='" + RowStyles[j]["StyleNoId"] + "' and Process='" + RowStyles[j]["Process"] + "' ");
                                for (int k = 0; k < RowsQtyDate.Length; k++)
                                {
                                    if (First == "Yes")
                                    {
                                        QtyDate = " " + Convert.ToDateTime(RowsQtyDate[k]["DefaultDate"]).ToString("dd/MMM") + "(" + Convert.ToInt32(RowsQtyDate[k]["Issued"]) + ")";
                                        First = "No";
                                    }
                                    else
                                    {
                                        QtyDate = QtyDate + ", " + Convert.ToDateTime(RowsQtyDate[k]["DefaultDate"]).ToString("dd/MMM") + "(" + Convert.ToInt32(RowsQtyDate[k]["Issued"]) + ")";
                                    }
                                }

                                DRM1["QtyDate"] = "   " + QtyDate;

                                OrdQty += Convert.ToInt32(ROrderQty[0]["CQty"]);
                                IssQty += Convert.ToInt32(RowStyles[j]["Issued"]);
                                RecQty += Convert.ToInt32(RowStyles[j]["Received"]);
                                DmgQty += Convert.ToInt32(RowStyles[j]["Damaged"]);
                                TtlQty += Convert.ToInt32(RowStyles[j]["Total"]);
                                IssBalQty += Convert.ToInt32(RowStyles[j]["IssBal"]);
                                BalQty += Convert.ToInt32(ROrderQty[0]["CQty"]) - Convert.ToInt32(RowStyles[j]["Issued"]);


                                DT.Rows.Add(DRM1);

                            }
                        }
                        if (OrdQty != 0)
                        {
                            DataRow DRM22 = DT.NewRow();
                            DRM22["StyleNo"] = "";
                            DRM22["OrderQty"] = OrdQty;
                            DRM22["IssQty"] = IssQty;
                            DRM22["RecQty"] = RecQty;
                            DRM22["DmgQty"] = DmgQty;
                            DRM22["TtlQty"] = TtlQty;
                            DRM22["RecBalQty"] = IssBalQty;
                            DRM22["IssBalQty"] = BalQty;
                            DT.Rows.Add(DRM22);

                            OrdQty = 0; IssQty = 0; RecQty = 0; DmgQty = 0; TtlQty = 0; IssBalQty = 0; BalQty = 0;
                            IsHeader = "No";

                        }

                    }

                    DataRow DRM24 = DT.NewRow();
                    DRM24["ExcNo"] = "";
                    DT.Rows.Add(DRM24);
                }

                #endregion

                if (DT.Rows.Count > 0)
                {
                    gvPartyWiseProductionStatus.DataSource = DT;
                    gvPartyWiseProductionStatus.DataBind();
                }
                else
                {
                    gvPartyWiseProductionStatus.DataSource = null;
                    gvPartyWiseProductionStatus.DataBind();
                }
            }

        }
        protected void gvPartyWiseProductionStatus_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.TableSection = TableRowSection.TableHeader;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= PartyWiseProductionStatus.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Excel.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


    }
}


