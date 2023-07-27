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
    public partial class ItemProcessOrderEntryReport : System.Web.UI.Page
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

                //DataSet dsset = objBs.getLedger(lblContactTypeId.Text);
                DataSet dsset = objBs.getLedger_New(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlPartyCode.DataSource = dsset.Tables[0];
                    ddlPartyCode.DataTextField = "LedgerName";
                    ddlPartyCode.DataValueField = "LedgerID";
                    ddlPartyCode.DataBind();
                    ddlPartyCode.Items.Insert(0, "All");

                    ddlPartyName.DataSource = dsset.Tables[0];
                    ddlPartyName.DataTextField = "LedgerName";
                    ddlPartyName.DataValueField = "LedgerID";
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, "All");
                }

                DataSet dsPONo = objBs.getItemProcessOrderEntryPONo();
                if (dsPONo.Tables[0].Rows.Count > 0)
                {
                    chkPONo.DataSource = dsPONo.Tables[0];
                    chkPONo.DataTextField = "FullPONo";
                    chkPONo.DataValueField = "ItemEntryId";
                    chkPONo.DataBind();

                }

            }
        }

        protected void ddlReportType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "1" || ddlReportType.SelectedValue == "5" || ddlReportType.SelectedValue == "6")
            {
                #region
                AccountingYear.Visible = false;

                BuyerCode.Visible = true;
                ExcNo.Visible = true;

                Date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblDate.Text = "Order Date";
                lblFrom.Text = "From";
                lblTo.Text = "To";
                #endregion
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                #region
                AccountingYear.Visible = true;

                BuyerCode.Visible = false;
                ExcNo.Visible = false;

                Date.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;
                #endregion
            }
            else if (ddlReportType.SelectedValue == "3")
            {
                #region
                AccountingYear.Visible = false;

                BuyerCode.Visible = true;
                ExcNo.Visible = false;

                Date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblDate.Text = "Shipment Date";
                lblFrom.Text = "From";
                lblTo.Text = "To";

                #endregion
            }
            else if (ddlReportType.SelectedValue == "4")//Shipment Wise Details
            {
                #region
                AccountingYear.Visible = false;

                BuyerCode.Visible = true;
                ExcNo.Visible = false;

                Date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblDate.Text = "Shipment Date";
                lblFrom.Text = "From";
                lblTo.Text = "To";

                #endregion
            }

            gvOrderReport.DataSource = null;
            gvOrderReport.DataBind();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {


            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string ItemPOId = "";
            string IsFirst = "Yes";
            foreach (ListItem listItem in chkPONo.Items)
            {
                #region
                if (chkPONo.SelectedIndex < 0)
                {
                    if (IsFirst == "Yes")
                    {
                        ItemPOId = listItem.Value;
                        IsFirst = "No";
                    }
                    else
                    {
                        ItemPOId = ItemPOId + "," + listItem.Value;
                    }
                }
                else
                {
                    if (listItem.Selected)
                    {
                        if (IsFirst == "Yes")
                        {
                            ItemPOId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            ItemPOId = ItemPOId + "," + listItem.Value;
                        }
                    }
                }
                #endregion
            }

            DataSet ds = objBs.getTransItemProcessOrderEntry_Report(ItemPOId, chkUseDate.Checked, From, To, ddlPartyCode.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet ds1 = objBs.getTransItemProcessOrderEntryRec_Report(ItemPOId, chkUseDate.Checked, From, To, ddlPartyCode.SelectedValue);

                #region

                DataTable DTS = new DataTable();
                DTS.Columns.Add(new DataColumn("FullPONo"));
                DTS.Columns.Add(new DataColumn("OrderDate"));
                DTS.Columns.Add(new DataColumn("Party"));
                DTS.Columns.Add(new DataColumn("category"));
                DTS.Columns.Add(new DataColumn("PurchaseFor"));
                DTS.Columns.Add(new DataColumn("purchasefortype"));
                DTS.Columns.Add(new DataColumn("IssueItem"));
                DTS.Columns.Add(new DataColumn("IssueColor"));
                DTS.Columns.Add(new DataColumn("Process"));
                DTS.Columns.Add(new DataColumn("Qty"));
                DTS.Columns.Add(new DataColumn("Shrink"));
                DTS.Columns.Add(new DataColumn("TotalQty"));
                DTS.Columns.Add(new DataColumn("Rate"));
                DTS.Columns.Add(new DataColumn("Amount"));
                DTS.Columns.Add(new DataColumn("RecQty"));
                DTS.Columns.Add(new DataColumn("ReceiveQty"));

                DTS.Columns.Add(new DataColumn("ShrinkQty"));
                DTS.Columns.Add(new DataColumn("Total"));
                DTS.Columns.Add(new DataColumn("Balance")); 

                double TtlQty = 0; double TtlTotalQty = 0; double TtlAmount = 0; double TtlRecQty = 0; double TtlReceiveQty = 0;
                double TtlShrink = 0; double TtlTotal = 0; double TtlBalance = 0;

                foreach (DataRow DRDS in ds.Tables[0].Rows)
                {
                    DataRow DR = DTS.NewRow();
                    DR["FullPONo"] = DRDS["FullPONo"];
                    DR["OrderDate"] =Convert.ToDateTime(DRDS["OrderDate"]).ToString("dd/MM/yyyy");
                    DR["Party"] = DRDS["Party"];
                    DR["category"] = DRDS["category"];
                    DR["PurchaseFor"] = DRDS["PurchaseFor"];
                    DR["purchasefortype"] = DRDS["purchasefortype"];
                    DR["IssueItem"] = DRDS["IssueItem"];
                    DR["IssueColor"] = DRDS["IssueColor"];
                    DR["Process"] = DRDS["Process"];
                    DR["Qty"] = Convert.ToDouble(DRDS["Qty"]).ToString("f2");
                    DR["Shrink"] = Convert.ToDouble(DRDS["Shrink"]).ToString("f2");
                    DR["TotalQty"] = Convert.ToDouble(DRDS["TotalQty"]).ToString("f2");
                    DR["Rate"] = Convert.ToDouble(DRDS["Rate"]).ToString("f2");
                    DR["Amount"] = Convert.ToDouble(DRDS["Amount"]).ToString("f2");
                    DR["RecQty"] = Convert.ToDouble(DRDS["RecQty"]).ToString("f2");

                    double ReceiveQty = 0;
                    DataRow[] RowsRecQty = ds1.Tables[0].Select("TransId='" + DRDS["TransId"] + "' ");
                    if (RowsRecQty.Length > 0)
                    {
                        for (int i = 0; i < RowsRecQty.Length; i++)
                        {
                            ReceiveQty += Convert.ToDouble(RowsRecQty[i]["ReceiveQty"]);
                        }
                    }

                    DR["ReceiveQty"] = ReceiveQty.ToString("f2");

                    double Shrink = ((ReceiveQty * Convert.ToDouble(DRDS["Shrink"])) / 100);
                    DR["ShrinkQty"] = Shrink.ToString("f2");
                    DR["Total"] = (ReceiveQty+Shrink).ToString("f2");
                    DR["Balance"] = (Convert.ToDouble(DRDS["RecQty"]) - (ReceiveQty + Shrink)).ToString("f2");

                    TtlQty += Convert.ToDouble(DRDS["Qty"]);
                    TtlTotalQty += Convert.ToDouble(DRDS["TotalQty"]);
                    TtlAmount += Convert.ToDouble(DRDS["Amount"]);
                    TtlRecQty += Convert.ToDouble(DRDS["RecQty"]);
                    TtlReceiveQty += ReceiveQty;

                    TtlShrink += Shrink;
                    TtlTotal += (ReceiveQty + Shrink);
                    TtlBalance += (Convert.ToDouble(DRDS["RecQty"]) - (ReceiveQty + Shrink));

                    DTS.Rows.Add(DR);

                }

                DataRow DR1 = DTS.NewRow();
                DR1["IssueColor"] = "Total:";
                DR1["Qty"] = TtlQty.ToString("f2");
                DR1["TotalQty"] = TtlTotalQty.ToString("f2");
                DR1["Amount"] = TtlAmount.ToString("f2");
                DR1["RecQty"] = TtlRecQty.ToString("f2");
                DR1["ReceiveQty"] = TtlReceiveQty.ToString("f2");

                DR1["ShrinkQty"] = TtlShrink.ToString("f2");
                DR1["Total"] = TtlTotal.ToString("f2");
                DR1["Balance"] = TtlBalance.ToString("f2");

                DTS.Rows.Add(DR1);

                #endregion

                gvOrderReport.DataSource = DTS;
                gvOrderReport.DataBind();
            }
            else
            {
                gvOrderReport.DataSource = null;
                gvOrderReport.DataBind();
            }


        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= ItemProcessOrderEntryReport.xls");
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

        public void ExportToExcel(string filename, DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.Caption = "Buyer Order Details";
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.ShowHeader = false;
                dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.HeaderStyle.Font.Bold = true;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }

    }
}


