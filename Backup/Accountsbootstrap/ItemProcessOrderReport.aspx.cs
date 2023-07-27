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
    public partial class ItemProcessOrderReport : System.Web.UI.Page
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

                DataSet dsPONo = objBs.getItemProcessOrderPONo();
                if (dsPONo.Tables[0].Rows.Count > 0)
                {
                    chkPONo.DataSource = dsPONo.Tables[0];
                    chkPONo.DataTextField = "FullPONo";
                    chkPONo.DataValueField = "ItemPOId";
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
            #region

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

            DataSet dsStyles = objBs.getTransItemProcessOrder_Report(ItemPOId, chkUseDate.Checked, From, To, ddlPartyCode.SelectedValue);
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
            Response.AddHeader("content-disposition", "attachment;filename= ItemProcessOrderReport.xls");
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


