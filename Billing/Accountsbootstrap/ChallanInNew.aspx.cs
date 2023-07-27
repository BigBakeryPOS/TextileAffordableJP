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
    public partial class ChallanInNew : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        int Issued = 0; int Received = 0; int Damaged = 0;

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
                    ddlProcess.DataValueField = "ProcessID";
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

                DataSet dsExcNo = objBs.getAllExcNo("All");
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExcNo.Tables[0];
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderId";
                    chkExcNo.DataBind();
                }

            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlChallan.SelectedValue == "In")
            {
                gvChallanOut.Visible = true;
                gvChallanOutNew.Visible = false;
                #region ChallanIn
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

                string ProcessType = "(ProcessType ='Received' or ProcessType ='Damaged')";
                DataSet ds = objBs.ChallanIn(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
                DataSet ds1 = objBs.ChallanIn1(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
                DataSet ds2 = objBs.ChallanIn2(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable DT = new DataTable();
                    DT.Columns.Add(new DataColumn("Challan"));
                    DT.Columns.Add(new DataColumn("ChallanNo"));
                    DT.Columns.Add(new DataColumn("Name"));
                    DT.Columns.Add(new DataColumn("Process"));
                    DT.Columns.Add(new DataColumn("ExcNo"));
                    DT.Columns.Add(new DataColumn("StyleNo"));
                    DT.Columns.Add(new DataColumn("StyleDescription"));
                    DT.Columns.Add(new DataColumn("Color"));
                    DT.Columns.Add(new DataColumn("IsuType"));
                    DT.Columns.Add(new DataColumn("Received"));
                    DT.Columns.Add(new DataColumn("Damaged"));
                    DT.Columns.Add(new DataColumn("TtlQty"));

                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        DataRow DRM = DT.NewRow();
                        DRM["Challan"] = Convert.ToDateTime(dr["DefaultDate"]).ToString("dd/MM/yyyy");
                        DT.Rows.Add(DRM);

                        int GrandTtlRecQty = 0; int GrandTtlDmgQty = 0; int GrandTtlQty = 0;

                        #region Cutting

                        int TtlQty = 0; int Received = 0; int Damaged = 0;
                        DataRow[] Rows11 = ds.Tables[0].Select("Date ='" + dr["Date"] + "' and Process ='Cutting' ");
                        for (int i = 0; i < Rows11.Length; i++)
                        {
                            DataRow DRM11 = DT.NewRow();
                            DRM11["Challan"] = Convert.ToDateTime(Rows11[i]["DefaultDate"]).ToString("dd/MM/yyyy");
                            DRM11["ChallanNo"] = Convert.ToDateTime(Rows11[i]["DefaultDate"]).ToString("dd/MM/yyyy");
                            DRM11["Name"] = Rows11[i]["LedgerName"];
                            DRM11["Process"] = Rows11[i]["Process"];
                            DRM11["ExcNo"] = Rows11[i]["ExcNo"];
                            DRM11["StyleNo"] = Rows11[i]["StyleNo"];
                            DRM11["StyleDescription"] = Rows11[i]["Description"];
                            DRM11["Color"] = Rows11[i]["Color"];
                            DRM11["IsuType"] = "";
                            DRM11["Received"] = Rows11[i]["Received"];
                            DRM11["Damaged"] = Rows11[i]["Damaged"];
                            DRM11["TtlQty"] = Convert.ToInt32(Rows11[i]["TtlQty"]);

                            Received += Convert.ToInt32(Rows11[i]["Received"]);
                            Damaged += Convert.ToInt32(Rows11[i]["Damaged"]);
                            TtlQty += Convert.ToInt32(Rows11[i]["TtlQty"]);

                            GrandTtlRecQty += Convert.ToInt32(Rows11[i]["Received"]);
                            GrandTtlDmgQty += Convert.ToInt32(Rows11[i]["Damaged"]);
                            GrandTtlQty += Convert.ToInt32(Rows11[i]["TtlQty"]);

                            DT.Rows.Add(DRM11);
                        }
                        if (TtlQty != 0)
                        {
                            DataRow DRM22 = DT.NewRow();
                            DRM22["Received"] = Received;
                            DRM22["Damaged"] = Damaged;
                            DRM22["TtlQty"] = TtlQty;
                            DT.Rows.Add(DRM22);
                            Received = 0; Damaged = 0; TtlQty = 0;
                        }

                        #endregion

                        DataRow[] Rows1 = ds2.Tables[0].Select("Date ='" + dr["Date"] + "' ");
                        for (int j = 0; j < Rows1.Length; j++)
                        {
                            DataRow[] Rows = ds.Tables[0].Select("Date ='" + dr["Date"] + "' and Process ='" + Rows1[j]["Process"] + "' ");
                            for (int i = 0; i < Rows.Length; i++)
                            {
                                if (Rows1[j]["Process"].ToString() != "Cutting")
                                {
                                    DataRow DRM1 = DT.NewRow();
                                    DRM1["Challan"] = Convert.ToDateTime(Rows[i]["DefaultDate"]).ToString("dd/MM/yyyy");
                                    DRM1["ChallanNo"] = Rows[i]["ChallanNo"];
                                    DRM1["Name"] = Rows[i]["LedgerName"];
                                    DRM1["Process"] = Rows[i]["Process"];
                                    DRM1["ExcNo"] = Rows[i]["ExcNo"];
                                    DRM1["StyleNo"] = Rows[i]["StyleNo"];
                                    DRM1["StyleDescription"] = Rows[i]["Description"];
                                    DRM1["Color"] = Rows[i]["Color"];
                                    DRM1["IsuType"] = "";
                                    DRM1["Received"] = Rows[i]["Received"];
                                    DRM1["Damaged"] = Rows[i]["Damaged"];
                                    DRM1["TtlQty"] = Rows[i]["TtlQty"];

                                    Received += Convert.ToInt32(Rows[i]["Received"]);
                                    Damaged += Convert.ToInt32(Rows[i]["Damaged"]);
                                    TtlQty += Convert.ToInt32(Rows[i]["TtlQty"]);

                                    GrandTtlRecQty += Convert.ToInt32(Rows[i]["Received"]);
                                    GrandTtlDmgQty += Convert.ToInt32(Rows[i]["Damaged"]);
                                    GrandTtlQty += Convert.ToInt32(Rows[i]["TtlQty"]);

                                    DT.Rows.Add(DRM1);
                                }
                            }

                            if (TtlQty != 0)
                            {
                                DataRow DRM2 = DT.NewRow();
                                DRM2["Received"] = Received;
                                DRM2["Damaged"] = Damaged;
                                DRM2["TtlQty"] = TtlQty;
                                DT.Rows.Add(DRM2);
                                Received = 0; Damaged = 0; TtlQty = 0;
                            }
                        }

                        DataRow DRM3 = DT.NewRow();
                        DRM3["Color"] = "Total Pcs. :";
                        DRM3["Received"] = GrandTtlRecQty;
                        DRM3["Damaged"] = GrandTtlDmgQty;
                        DRM3["TtlQty"] = GrandTtlQty;
                        DT.Rows.Add(DRM3);

                    }

                    gvChallanOut.DataSource = DT;
                    gvChallanOut.DataBind();

                }
                else
                {
                    gvChallanOut.DataSource = null;
                    gvChallanOut.DataBind();
                }
                #endregion
            }
            else
            {
                gvChallanOut.Visible = false;
                gvChallanOutNew.Visible = true;
                #region ChallanOut
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

                string ProcessType = "ProcessType='Issued'";
                DataSet dso = objBs.ChallanOut(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
                DataSet ds1o = objBs.ChallanOut1(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
                DataSet ds2o = objBs.ChallanOut2(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
                if (dso.Tables[0].Rows.Count > 0)
                {
                    DataTable DTo = new DataTable();
                    DTo.Columns.Add(new DataColumn("Challan"));
                    DTo.Columns.Add(new DataColumn("ChallanNo"));
                    DTo.Columns.Add(new DataColumn("Name"));
                    DTo.Columns.Add(new DataColumn("Process"));
                    DTo.Columns.Add(new DataColumn("ExcNo"));
                    DTo.Columns.Add(new DataColumn("StyleNo"));
                    DTo.Columns.Add(new DataColumn("StyleDescription"));
                    DTo.Columns.Add(new DataColumn("Color"));
                    DTo.Columns.Add(new DataColumn("IsuType"));
                    DTo.Columns.Add(new DataColumn("TtlQty"));

                    foreach (DataRow dro in ds1o.Tables[0].Rows)
                    {
                        DataRow DRMo= DTo.NewRow();
                        DRMo["Challan"] = Convert.ToDateTime(dro["DefaultDate"]).ToString("dd/MM/yyyy");
                        DTo.Rows.Add(DRMo);

                        int GrandTtlQtyo = 0;
                        DataRow[] Rows1o = ds2o.Tables[0].Select("Date ='" + dro["Date"] + "' ");
                        for (int j = 0; j < Rows1o.Length; j++)
                        {
                            int TtlQtyo = 0;
                            DataRow[] Rowso = dso.Tables[0].Select("Date ='" + dro["Date"] + "' and Process ='" + Rows1o[j]["Process"] + "' ");
                            for (int i = 0; i < Rowso.Length; i++)
                            {
                                DataRow DRM1o = DTo.NewRow();
                                DRM1o["Challan"] = Convert.ToDateTime(Rowso[i]["DefaultDate"]).ToString("dd/MM/yyyy");
                                DRM1o["ChallanNo"] = Rowso[i]["FullEntryNo"];
                                DRM1o["Name"] = Rowso[i]["LedgerName"];
                                DRM1o["Process"] = Rowso[i]["Process"];
                                DRM1o["ExcNo"] = Rowso[i]["ExcNo"];
                                DRM1o["StyleNo"] = Rowso[i]["StyleNo"];
                                DRM1o["StyleDescription"] = Rowso[i]["Description"];
                                DRM1o["Color"] = Rowso[i]["Color"];
                                DRM1o["IsuType"] = "";
                                DRM1o["TtlQty"] = Rowso[i]["Issued"];
                                TtlQtyo += Convert.ToInt32(Rowso[i]["Issued"]);
                                GrandTtlQtyo += Convert.ToInt32(Rowso[i]["Issued"]);
                                DTo.Rows.Add(DRM1o);
                            }

                            DataRow DRM2o = DTo.NewRow();
                            DRM2o["TtlQty"] = TtlQtyo;
                            DTo.Rows.Add(DRM2o);
                        }

                        DataRow DRM3o = DTo.NewRow();
                        DRM3o["Color"] = "Total Pcs. :";
                        DRM3o["TtlQty"] = GrandTtlQtyo;
                        DTo.Rows.Add(DRM3o);

                    }

                    gvChallanOutNew.DataSource = DTo;
                    gvChallanOutNew.DataBind();

                }
                else
                {
                    gvChallanOutNew.DataSource = null;
                    gvChallanOutNew.DataBind();
                }
                #endregion

            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= ChallanIn.xls");
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

        protected void gvChallanOut_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Issued += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Issued"));
                Received += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Received"));
                Damaged += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Damaged"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[9].Text = "Total:";
                e.Row.Cells[10].Text = Issued.ToString();
                e.Row.Cells[11].Text = Received.ToString();
                e.Row.Cells[12].Text = Damaged.ToString();
            }
        }
    }
}


