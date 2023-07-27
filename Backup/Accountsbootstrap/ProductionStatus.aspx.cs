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
    public partial class ProductionStatus : System.Web.UI.Page
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
            gvCuttingQtyDetails.DataSource = null;
            gvCuttingQtyDetails.DataBind();

            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string BuyerOrderId = "";
            string IsFirst = "Yes";

            foreach (ListItem listItem in chkExcNo.Items)
            {
                #region
                if (chkExcNo.SelectedIndex < 0)
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

            DataSet ds = objBs.ProductionStatus1(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsStyle = objBs.ProductionStatus2(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                DataSet dsProcess = objBs.ProductionStatus3(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                DataSet dsProcessQty = objBs.ProductionStatus4();

                DataSet dsStyleQtyDate = objBs.ProductionStatus5(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                DataSet dsProcessQtyDate = objBs.ProductionStatus6();

                #region

                DataTable DT = new DataTable();
                DT.Columns.Add(new DataColumn("Supplier"));
                DT.Columns.Add(new DataColumn("Process"));
                DT.Columns.Add(new DataColumn("Style"));
                DT.Columns.Add(new DataColumn("OrderQty"));
                DT.Columns.Add(new DataColumn("IssQty"));
                DT.Columns.Add(new DataColumn("RecQty"));
                DT.Columns.Add(new DataColumn("DmgQty"));
                DT.Columns.Add(new DataColumn("TtlQty"));
                DT.Columns.Add(new DataColumn("RecBalQty"));
                DT.Columns.Add(new DataColumn("IssBalQty"));

                DT.Columns.Add(new DataColumn("QtyDate"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow DRM = DT.NewRow();
                    DRM["Supplier"] = dr["ExcNo"];
                    DRM["Process"] = Convert.ToDateTime(dr["OrderDate"]).ToString("dd/MM/yyyy");
                    DT.Rows.Add(DRM);

                    int OrdQty = 0; int RecQty = 0; int DmgQty = 0; int TtlQty = 0; int IssBalQty = 0; int BalQty = 0; string BuyerOrderMasterCuttingId = "";
                    DataRow[] RowsQty = dsStyle.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "'  ");
                    for (int i = 0; i < RowsQty.Length; i++)
                    {
                        BuyerOrderMasterCuttingId = RowsQty[i]["BuyerOrderMasterCuttingId"].ToString();

                        DataRow DRM1 = DT.NewRow();
                        DRM1["Supplier"] = RowsQty[i]["LedgerName"];
                        DRM1["Process"] = "Cutting";
                        DRM1["Style"] = RowsQty[i]["StyleNo"];
                        DRM1["OrderQty"] = Convert.ToInt32(RowsQty[i]["OQty"]);
                        DRM1["IssQty"] = "";
                        DRM1["RecQty"] = Convert.ToInt32(RowsQty[i]["RecQty"]);
                        DRM1["DmgQty"] = Convert.ToInt32(RowsQty[i]["DmgQty"]);
                        DRM1["TtlQty"] = Convert.ToInt32(RowsQty[i]["TotalQty"]);
                        DRM1["RecBalQty"] = Convert.ToInt32(RowsQty[i]["BalQty"]);
                        DRM1["IssBalQty"] = Convert.ToInt32(RowsQty[i]["BalQty"]);

                        string QtyDate = "";
                        string First = "Yes";

                        DataRow[] RowsQtyDate = dsStyleQtyDate.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' and StyleNoId='" + RowsQty[i]["StyleNoId"] + "' ");
                        for (int j = 0; j < RowsQtyDate.Length; j++)
                        {
                            if (First == "Yes")
                            {
                                QtyDate =" "+ Convert.ToDateTime(RowsQtyDate[j]["DefaultDate"]).ToString("dd/MMM") + "(" + Convert.ToInt32(RowsQtyDate[j]["TtlQty"]) + ")";
                                First = "No";
                            }
                            else
                            {
                                QtyDate = QtyDate + ", " + Convert.ToDateTime(RowsQtyDate[j]["DefaultDate"]).ToString("dd/MMM") + "(" + Convert.ToInt32(RowsQtyDate[j]["TtlQty"]) + ")";
                            }
                        }

                        DRM1["QtyDate"] = " " + QtyDate;
                        OrdQty += Convert.ToInt32(RowsQty[i]["OQty"]);
                        RecQty += Convert.ToInt32(RowsQty[i]["RecQty"]);
                        DmgQty += Convert.ToInt32(RowsQty[i]["DmgQty"]);
                        TtlQty += Convert.ToInt32(RowsQty[i]["TotalQty"]);
                        IssBalQty += Convert.ToInt32(RowsQty[i]["BalQty"]);
                        BalQty += Convert.ToInt32(RowsQty[i]["BalQty"]);

                        DT.Rows.Add(DRM1);
                    }

                    DataRow DRM2 = DT.NewRow();
                    DRM2["Style"] = "";
                    DRM2["OrderQty"] = OrdQty;
                    DRM2["RecQty"] = RecQty;
                    DRM2["DmgQty"] = DmgQty;
                    DRM2["TtlQty"] = TtlQty;
                    DRM2["RecBalQty"] = BalQty;
                    DRM2["IssBalQty"] = IssBalQty;
                    DT.Rows.Add(DRM2);

                    DataRow DRM33 = DT.NewRow();
                    DRM33["Style"] = "";
                    DRM33["OrderQty"] = "";
                    DRM33["RecQty"] = "";
                    DRM33["DmgQty"] = "";
                    DRM33["TtlQty"] = "";
                    DRM33["RecBalQty"] = "";
                    DT.Rows.Add(DRM33);

                    DataRow[] RowProcess = dsProcess.Tables[0].Select("BuyerOrderMasterCuttingId='" + BuyerOrderMasterCuttingId + "'  ");
                    for (int i = 0; i < RowProcess.Length; i++)
                    {
                        int POrdQty = 0; int PIssQty = 0; int PRecQty = 0; int PDmgQty = 0; int PTtlQty = 0; int PIssBalQty = 0; int PBalQty = 0;
                        DataRow[] RowProcessQty = dsProcessQty.Tables[0].Select("BuyerOrderMasterCuttingId='" + RowProcess[i]["BuyerOrderMasterCuttingId"] + "' and Process='" + RowProcess[i]["ProcessId"] + "'  ");
                        for (int j = 0; j < RowProcessQty.Length; j++)
                        {
                            DataRow DRM1 = DT.NewRow();
                            DRM1["Supplier"] = RowProcessQty[j]["LedgerName"];
                            DRM1["Process"] = RowProcess[i]["Process"];
                            DRM1["Style"] = RowProcessQty[j]["StyleNo"];

                            DataRow[] ROrderQty = dsStyle.Tables[0].Select("BuyerOrderMasterCuttingId='" + RowProcess[i]["BuyerOrderMasterCuttingId"] + "' and BuyerOrderId='" + RowProcess[i]["BuyerOrderId"] + "' and StyleNoId='" + RowProcessQty[j]["SamplingCostingId"] + "'  ");
                            DRM1["OrderQty"] = ROrderQty[0]["CQty"];
                            POrdQty += Convert.ToInt32(ROrderQty[0]["CQty"]);

                            DRM1["IssQty"] = Convert.ToInt32(RowProcessQty[j]["Issued"]);
                            DRM1["RecQty"] = Convert.ToInt32(RowProcessQty[j]["Received"]);
                            DRM1["DmgQty"] = Convert.ToInt32(RowProcessQty[j]["Damaged"]);
                            DRM1["TtlQty"] = Convert.ToInt32(RowProcessQty[j]["Total"]);
                            DRM1["RecBalQty"] = Convert.ToInt32(RowProcessQty[j]["IssBal"]);
                            DRM1["IssBalQty"] = Convert.ToInt32(ROrderQty[0]["CQty"]) - Convert.ToInt32(RowProcessQty[j]["Issued"]);

                            string QtyDate = "";
                            string First = "Yes";
                            DataRow[] RowsQtyDate = dsProcessQtyDate.Tables[0].Select("BuyerOrderMasterCuttingId='" + RowProcessQty[j]["BuyerOrderMasterCuttingId"] + "' and StyleNoId='" + RowProcessQty[j]["StyleNoId"] + "' and Process='" + RowProcessQty[j]["Process"] + "' ");
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

                            DT.Rows.Add(DRM1);

                            PIssQty += Convert.ToInt32(RowProcessQty[j]["Issued"]);
                            PRecQty += Convert.ToInt32(RowProcessQty[j]["Received"]);
                            PDmgQty += Convert.ToInt32(RowProcessQty[j]["Damaged"]);
                            PTtlQty += Convert.ToInt32(RowProcessQty[j]["Total"]);
                            PIssBalQty += Convert.ToInt32(ROrderQty[0]["CQty"]) - Convert.ToInt32(RowProcessQty[j]["Issued"]);
                            PBalQty += Convert.ToInt32(RowProcessQty[j]["IssBal"]);

                        }
                        if (PIssQty != 0)
                        {
                            DataRow DRM3 = DT.NewRow();
                            DRM3["Style"] = "";
                            DRM3["OrderQty"] = POrdQty;
                            DRM3["IssQty"] = PIssQty;
                            DRM3["RecQty"] = PRecQty;
                            DRM3["DmgQty"] = PDmgQty;
                            DRM3["TtlQty"] = PTtlQty;
                            DRM3["RecBalQty"] = PBalQty;
                            DRM3["IssBalQty"] = PIssBalQty;
                            DT.Rows.Add(DRM3);

                            DataRow DRM244 = DT.NewRow();
                            DRM244["Supplier"] = "";
                            DT.Rows.Add(DRM244);
                        }
                    }


                    DataRow DRM24 = DT.NewRow();
                    DRM24["Supplier"] = "";
                    DT.Rows.Add(DRM24);
                }

                #endregion

                if (DT.Rows.Count > 0)
                {
                    gvCuttingQtyDetails.DataSource = DT;
                    gvCuttingQtyDetails.DataBind();
                }
                else
                {
                    gvCuttingQtyDetails.DataSource = null;
                    gvCuttingQtyDetails.DataBind();
                }
            }

        }
      
        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= ProductionStatus.xls");
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

        protected void gvCuttingQtyDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
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

    }
}


