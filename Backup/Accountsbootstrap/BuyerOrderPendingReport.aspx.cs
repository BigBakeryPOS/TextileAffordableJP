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
    public partial class BuyerOrderPendingReport : System.Web.UI.Page
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

                    ddlBuyerName.DataSource = dsset.Tables[0];
                    ddlBuyerName.DataTextField = "LedgerName";
                    ddlBuyerName.DataValueField = "LedgerID";
                    ddlBuyerName.DataBind();
                    ddlBuyerName.Items.Insert(0, "All");
                }

                DataSet dsExcNo = objBs.getAllExcNo(ddlBuyerCode.SelectedValue);
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExcNo.Tables[0];
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderId";
                    chkExcNo.DataBind();

                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "All");
                }

            }
        }

        protected void buyer_order(object sender, EventArgs e)
        {
            DataSet dsExcNo = objBs.getAllExcNo(ddlBuyerCode.SelectedValue);
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

            DataSet ds = objBs.BuyerOrderPending1(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBuyerOrderQty.DataSource = ds;
                gvBuyerOrderQty.DataBind();
            }
            else
            {
                gvBuyerOrderQty.DataSource = null;
                gvBuyerOrderQty.DataBind();
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= BuyerOrderPendingReport.xls");
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


