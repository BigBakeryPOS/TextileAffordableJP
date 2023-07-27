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

namespace Billing.Accountsbootstrap
{
    public partial class PaymentGrit : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string Sort_Direction = "LedgerName ASC";
        string Sort_Direction1 = "Narration ASC";
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");



           
            //sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                ViewState["SortExpr"] = Sort_Direction;
                ViewState["SortExpr"] = Sort_Direction1;
                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();

                DataSet ds = objBs.selectpaymentDaybook("tblDayBook"  , "tblPayment"  );
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
        protected void searchchanged(object sender, EventArgs e)
        {
            if (ddlfilter.SelectedValue == "4")
            {
                normaltext.Visible = false;
                dateserach.Visible = true;
            }
            else
            {
                dateserach.Visible = false;
                normaltext.Visible = true;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "4")
            {
                ds = objBs.searchPaymentmaster(txtdate.Text, Convert.ToInt32(ddlfilter.SelectedValue), "tblDayBook"  , "Payment", "tblPayment"  );
            }
            else
            {
                ds = objBs.searchPaymentmaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), "tblDayBook"  , "Payment", "tblPayment" );
            }
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

        protected void btnrefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentGrit.aspx");
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Paymentscreen.aspx");
        }

        protected void PaymentGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectpaymentDaybook("tblDayBook" , "tblPayment");
            }
            else if (ddlfilter.SelectedValue == "4")
            {
                ds = objBs.searchPaymentmaster(txtdate.Text, Convert.ToInt32(ddlfilter.SelectedValue), "tblDayBook" , "Payment", "tblPayment" );
            }
            else
            {
                ds = objBs.searchPaymentmaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), "tblDayBook" , "Payment", "tblPayment"  );
            }


            if (Session["SortedView"] != null)
            {
                PaymentGrid.DataSource = Session["SortedView"];
                PaymentGrid.DataBind();
            }
            else
            {
                PaymentGrid.DataSource = ds;
                PaymentGrid.PageIndex = e.NewPageIndex;
                // gvsales.DataBind();
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    PaymentGrid.DataSource = ds;
                    PaymentGrid.PageIndex = e.NewPageIndex;
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

        protected void PaymentGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Paymentscreen.aspx?TransNo=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.paymentmaster(e.CommandArgument.ToString(), "tblDayBook"  , "tblPayment"  , "tblTransPayment"  , "tblAuditMaster"  , lblUser.Text);
                Response.Redirect("PaymentGrit.aspx");
            }
        }



        protected void gridview_Sorting(object sender, GridViewSortEventArgs e)
        {
            string[] SortOrder = ViewState["SortExpr"].ToString().Split(' ');
            if (SortOrder[0] == e.SortExpression)
            {
                if (SortOrder[1] == "ASC")
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "DESC";
                }
                else
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
                }
            }
            else
            {
                ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
            }
            DataSet ds = objBs.selectpaymentDaybook("tblDayBook"  , "tblPayment"   );
            DataView dvEmp = ds.Tables[0].DefaultView;
            dvEmp.Sort = ViewState["SortExpr"].ToString();
            Session["SortedView"] = dvEmp;
            PaymentGrid.DataSource = dvEmp;
            PaymentGrid.DataBind();

        }
    }
}