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
    public partial class PurchaseOrderPendingReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double TtlQty = 0; double TtlRecQty = 0; double TtlBalQty = 0;

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

                DataSet dsPONo = objBs.getPurchaseOrderReportPONo();
                if (dsPONo.Tables[0].Rows.Count > 0)
                {
                    chkPONo.DataSource = dsPONo.Tables[0];
                    chkPONo.DataTextField = "FullPONo";
                    chkPONo.DataValueField = "POId";
                    chkPONo.DataBind();

                }

            }
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

            string ReportType = "";
            if (ddlReportType.SelectedValue == "1")
            {
                ReportType = "All";
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                ReportType = ">";
            }
            else if (ddlReportType.SelectedValue == "3")
            {
                ReportType = "<=";
            }
            DataSet dsStyles = objBs.getTransPurchaseOrderPending_Report(ddlItem.SelectedValue, ReportType, ItemPOId, chkUseDate.Checked, From, To, ddlPartyCode.SelectedValue);
            if (dsStyles.Tables[0].Rows.Count > 0)
            {
                gvPOPending.DataSource = dsStyles;
                gvPOPending.DataBind();
            }
            else
            {
                gvPOPending.DataSource = null;
                gvPOPending.DataBind();
            }

            #endregion
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= PurchaseOrderPendingReport.xls");
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
       
        protected void gvOrderReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TtlQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                TtlRecQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RecQty"));
                TtlBalQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "BalQty"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total";
                e.Row.Cells[6].Text = TtlQty.ToString("f2");
                e.Row.Cells[7].Text = TtlRecQty.ToString("f2");
                e.Row.Cells[8].Text = TtlBalQty.ToString("f2");
            }
        }
    }
}


