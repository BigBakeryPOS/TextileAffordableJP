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
    public partial class MasterCuttingDetailReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        int RecQty = 0; int DmgQty = 0; 

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

        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Cuttingdetailsnew.aspx");
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

            //string ProductionQty = ""; string ReportType = "";
            //if (ddlReportType.SelectedValue == "1")
            //{
            //    ProductionQty = "Sum(Issued) as ProductionQty";
            //    ReportType = "Issued";
            //}
            //else if (ddlReportType.SelectedValue == "2")
            //{
            //    ProductionQty = "Sum(Received) as ProductionQty";
            //    ReportType = "Received";
            //}
            //else if (ddlReportType.SelectedValue == "3")
            //{
            //    ProductionQty = "Sum(Damaged) as ProductionQty";
            //    ReportType = "Received";
            //}

            DataSet ds = objBs.MasterCuttingDetailReport1(BuyerOrderId, chkUseDate.Checked, From, To);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //    gvProductionReport.ShowHeader = true;
                //    gvProductionReport.Caption = "Production " + ddlReportType.SelectedItem.Text + " Qty Report";
                gvProductionReport.DataSource = ds;
                gvProductionReport.DataBind();

            }
            else
            {
                gvProductionReport.DataSource = null;
                gvProductionReport.DataBind();
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= MasterCuttingDetailReport.xls");
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

        protected void gvProductionReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RecQty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecQty"));
                DmgQty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DmgQty"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "Total:";
                e.Row.Cells[7].Text = RecQty.ToString();
                e.Row.Cells[8].Text = DmgQty.ToString();
            }
        }
    }
}


