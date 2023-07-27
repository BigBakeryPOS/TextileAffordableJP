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
    public partial class PurchaseGRNReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double TtlQty = 0;

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

                DataSet dsItem = objBs.getAlliItems();
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataSource = dsItem.Tables[0];
                    ddlItem.DataTextField = "Description";
                    ddlItem.DataValueField = "ItemMasterId";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "All");
                }
                DataSet dsset = objBs.getLedger_New(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlPartyCode.DataSource = dsset.Tables[0];
                    ddlPartyCode.DataTextField = "CompanyCode";
                    ddlPartyCode.DataValueField = "LedgerID";
                    ddlPartyCode.DataBind();
                    ddlPartyCode.Items.Insert(0, "All");

                    ddlPartyName.DataSource = dsset.Tables[0];
                    ddlPartyName.DataTextField = "LedgerName";
                    ddlPartyName.DataValueField = "LedgerID";
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, "All");
                }

                DataSet dsRecPONo = objBs.getPurchaseGRNReportPONo();
                if (dsRecPONo.Tables[0].Rows.Count > 0)
                {
                    chkRecPONo.DataSource = dsRecPONo.Tables[0];
                    chkRecPONo.DataTextField = "FullRecPONo";
                    chkRecPONo.DataValueField = "POGRNId";
                    chkRecPONo.DataBind();

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
            #region

            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string ItemPOId = "";
            string IsFirst = "Yes";
            foreach (ListItem listItem in chkRecPONo.Items)
            {
                #region
                if (chkRecPONo.SelectedIndex < 0)
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

            DataSet dsStyles = objBs.getTransPurchaseGRNReport_Report(ddlItem.SelectedValue,ItemPOId, chkUseDate.Checked, From, To, ddlPartyCode.SelectedValue);
            if (dsStyles.Tables[0].Rows.Count > 0)
            {
                gvOrderReport.DataSource = dsStyles;
                gvOrderReport.DataBind();
            }
            else
            {
                gvOrderReport.DataSource = null;
                gvOrderReport.DataBind();
            }

            #endregion
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= PurchaseGRNReport.xls");
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

        protected void gvOrderReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TtlQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RecQty"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[12].Text = "Total";
                e.Row.Cells[13].Text = TtlQty.ToString("f2");
            }
        }
    }
}


