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
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class PaymentGridView : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string Sort_Direction = "LedgerName ASC";
        string Sort_Direction1 = "Narration ASC";
        string sTableName = "";

        int TotalQuantity = 0; double TotalAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");




       

            if (!IsPostBack)
            {
                string super = Session["IsSuperAdmin"].ToString();


                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "ALL");
                    }
                }
                else
                {

                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }

                DataSet dst = objBs.Alljobworkmasterpayment();
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddljobworker.DataSource = dst.Tables[0];
                        ddljobworker.DataTextField = "LedgerName";
                        ddljobworker.DataValueField = "LedgerID";
                        ddljobworker.DataBind();
                        ddljobworker.Items.Insert(0, "ALL");
                    }
                }

                DataSet drpProcess = objBs.SelectAllProcessTypeLotProcess();
                if (drpProcess.Tables[0].Rows.Count > 0)
                {
                    DpProcess.DataSource = drpProcess;
                    DpProcess.DataTextField = "ProcessType";
                    DpProcess.DataValueField = "ProcessMasterID";
                    DpProcess.DataBind();
                    DpProcess.Items.Insert(0, "ALL");
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                ViewState["SortExpr"] = Sort_Direction;
                ViewState["SortExpr"] = Sort_Direction1;
                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();

                DataSet ds = objBs.getjppaymentsval(drpbranch.SelectedValue);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        PaymentGrid.DataSource = ds;
                        PaymentGrid.DataBind();
                    }
                    else
                    {
                        PaymentGrid.DataSource = null;
                        PaymentGrid.DataBind();
                    }
                }
                else
                {
                    PaymentGrid.DataSource = null;
                    PaymentGrid.DataBind();
                }
            }
        }

        protected void drpbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objBs.getjppaymentsval(drpbranch.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    PaymentGrid.DataSource = ds;
                    PaymentGrid.DataBind();
                }
                else
                {
                    PaymentGrid.DataSource = null;
                    PaymentGrid.DataBind();
                }
            }
            else
            {
                PaymentGrid.DataSource = null;
                PaymentGrid.DataBind();
            }

        }
        protected void searchchanged(object sender, EventArgs e)
        {
            //if (ddlfilter.SelectedValue == "4")
            //{
            //    normaltext.Visible = false;
            //    dateserach.Visible = true;
            //}
            //else
            //{
            //    dateserach.Visible = false;
            //    normaltext.Visible = true;
            //}
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {


            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet ds = objBs.SearchPaymentDetails(fromdate, todate,DpProcess.SelectedValue,drpbranch.SelectedValue,ddljobworker.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    PaymentGrid.DataSource = ds;
                    PaymentGrid.DataBind();
                }
                else
                {
                    PaymentGrid.DataSource = null;
                    PaymentGrid.DataBind();
                }
            }
            else
            {
                PaymentGrid.DataSource = null;
                PaymentGrid.DataBind();
            }
            //DataSet ds = new DataSet();
            //if (ddlfilter.SelectedValue == "4")
            //{
            //    ds = objBs.searchPaymentmaster(txtdate.Text, Convert.ToInt32(ddlfilter.SelectedValue), "tblDayBook", "Payment", "tblPayment");
            //}
            //else
            //{
            //    ds = objBs.searchPaymentmaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), "tblDayBook", "Payment", "tblPayment");
            //}
            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        PaymentGrid.DataSource = ds;
            //        PaymentGrid.DataBind();
            //    }
            //    else
            //    {
            //        PaymentGrid.DataSource = null;
            //        PaymentGrid.DataBind();
            //    }
            //}
            //else
            //{
            //    PaymentGrid.DataSource = null;
            //    PaymentGrid.DataBind();
            //}
        }
        protected void GVDespatchstock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalQuantity = TotalQuantity + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                TotalAmount = TotalAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = TotalQuantity.ToString(); 
                e.Row.Cells[4].Text = TotalAmount.ToString(); 
            }
            #endregion
        }
        protected void btnrefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentGrit.aspx");
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Payment.aspx");
        }

      

        protected void PaymentGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Paymentscreen.aspx?TransNo=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Print")
            {
                Response.Redirect("PaymentPrint.aspx?PaymentID=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.paymentmaster(e.CommandArgument.ToString(), "tblDayBook", "tblPayment", "tblTransPayment", "tblAuditMaster", lblUser.Text);
                Response.Redirect("PaymentGrit.aspx");
            }
        }



      
    }
}