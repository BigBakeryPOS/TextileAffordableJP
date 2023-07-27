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
    public partial class viewreceipts : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        string sTableName = "";
        string Sort_Direction = "Creditor ASC";
        string Sort_Direction1 = "Narration ASC";
        int EmpId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            //EmpId = Convert.ToInt32(Session["EmpId"].ToString());
         
            //sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                ViewState["SortExpr"] = Sort_Direction;
                ViewState["SortExpr"] = Sort_Direction1;
                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();

                DataSet dLedger = objbs.LedgerReceipt("tblReceipt_" + sTableName, "tblDaybook_" + sTableName,"N");
                if (dLedger != null)
                {
                    if (dLedger.Tables[0].Rows.Count > 0)
                    {

                        gvledgrid.DataSource = dLedger;
                        gvledgrid.DataBind();
                    }
                    else
                    {
                        gvledgrid.DataSource = null;
                        gvledgrid.DataBind();
                    }
                }
                else
                {
                    gvledgrid.DataSource = null;
                    gvledgrid.DataBind();
                }

                //DataSet dacess = new DataSet();
                //dacess = objbs.getuseraccessscreen(Session["EmpId"].ToString(), "receipt");
                //if (dacess.Tables[0].Rows.Count > 0)
                //{
                //    //if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowadd"]) == true)
                //    //{
                //        btnnew.Visible = true;
                //    //}
                //    //else
                //    //{
                //    //    btnnew.Visible = false;
                //    //}

                //    //if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowedit"]) == true)
                //    //{
                //        gvledgrid.Columns[9].Visible = true;
                //    //}
                //    //else
                //    //{
                //    //    gvledgrid.Columns[9].Visible = false;
                //    //}

                //    //if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowdelete"]) == true)
                //    //{
                //        gvledgrid.Columns[10].Visible = true;
                //    //}
                //    //else
                //    //{
                //    //    gvledgrid.Columns[10].Visible = false;
                //    //}
                //}

                //DataSet dsed = objbs.CheckEditDelete(EmpId);
                //if (dsed.Tables[0].Rows.Count > 0)
                //{
                //    //if (dsed.Tables[0].Rows[0]["allowedit"].ToString() == "1")
                //    //{
                //    //    //btnadd.Visible = true;
                //    //    gvledgrid.Columns[9].Visible = true;
                //    //}
                //    //else
                //    //{
                //    //    gvledgrid.Columns[9].Visible = false;
                //    //}

                //    //if (dsed.Tables[0].Rows[0]["allowdelete"].ToString() == "1")
                //    //{
                //    //    gvledgrid.Columns[10].Visible = true;
                //    //}
                //    //else
                //    //{
                //    //    gvledgrid.Columns[10].Visible = false;
                //    //}

                //    if (dsed.Tables[0].Rows[0]["allowview"].ToString() == "1")
                //    {
                //        gvledgrid.Columns[7].Visible = true;
                //        gvledgrid.Columns[8].Visible = true;
                //    }
                //    else
                //    {
                //        gvledgrid.Columns[7].Visible = false;
                //        gvledgrid.Columns[8].Visible = false;
                //    }
                //}
            }
        }

        protected void searchchange(object sender, EventArgs e)
        {
            if (ddlfilter.SelectedValue == "4")
            {
                normal.Visible = false;
                dateserach.Visible = true;
            }
            else
            {
                normal.Visible = true;
                dateserach.Visible = false;
            }
        }
        protected void btnnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Receipt.aspx");
        }

        protected void gvledgrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DataSet dsPresent = objbs.GetAutoGenerateReceipt(e.CommandArgument.ToString(), sTableName);
                    if (dsPresent.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not able to Edit, Auto Generate - Receipt Against Sales')", true);                       
                        return;
                    }
                    else
                    {
                        Response.Redirect("Receipt.aspx?TransNo=" + e.CommandArgument.ToString());
                    }
                    //if (e.CommandArgument.ToString() != "")
                    //{
                    //    Response.Redirect("Receipt.aspx?TransNo=" + e.CommandArgument.ToString());
                    //}
                }
            }
            else if (e.CommandName == "Print")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("ReceiptPrintNew.aspx?iReceiptNo=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "PrintNew")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("ReceiptPrintNew1.aspx?iReceiptNo=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "Del")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DataSet dsPresent = objbs.GetAutoGenerateReceipt(e.CommandArgument.ToString(), sTableName);
                    if (dsPresent.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not able to Delete, Auto Generate - Receipt Against Sales')", true);
                        return;
                    }
                    else
                    {
                        objbs.DeleteReceipt(e.CommandArgument.ToString(), "tblReceipt_" + sTableName, "tblDaybook_" + sTableName, "tblTransReceipt_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, sTableName, EmpId);
                        Response.Redirect("viewreceipts.aspx");
                    }

                    //if (e.CommandArgument.ToString() != "")
                    //{
                    //    objbs.DeleteReceipt(e.CommandArgument.ToString(), "tblReceipt_" + sTableName, "tblDaybook_" + sTableName, "tblTransReceipt_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, sTableName, EmpId);
                    //    Response.Redirect("viewreceipts.aspx");
                    //}
                }
            }
        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();

            string searchoption = string.Empty;

            if (ddlfilter.SelectedValue == "Search By")
            {

            }
            else
            {
                if (ddlfilter.SelectedValue == "1")
                {
                    searchoption = "r.receiptno like '%" + txtsearch1.Text + "%'";
                }
                else if (ddlfilter.SelectedValue == "2")
                {
                    searchoption = " convert(date,r.receiptDate)='" + txtsearch1.Text + "'";
                }
                else if (ddlfilter.SelectedValue == "3")
                {
                    searchoption = "l.LedgerName like '%" + txtsearch1.Text + "%'";
                }

            }

            ds = objbs.searchReceiptNew(searchoption, sTableName, "Receipt");



            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    gvledgrid.DataSource = ds;
                    gvledgrid.PageIndex = e.NewPageIndex;
                 //   DataView dvEmployee = dLedger.Tables[0].DefaultView;

                    gvledgrid.DataBind();
                }
                else
                {
                    gvledgrid.DataSource = null;
                    gvledgrid.DataBind();
                }
            }
            else
            {
                gvledgrid.DataSource = null;
                gvledgrid.DataBind();
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            string searchoption = string.Empty;

            if (ddlfilter.SelectedValue == "Search By")
            {

            }
            else
            {
                if (ddlfilter.SelectedValue == "1")
                {
                    searchoption = "r.receiptno like '%" + txtsearch1.Text + "%'";
                }
                else if (ddlfilter.SelectedValue == "2")
                {
                    searchoption = " convert(date,r.receiptDate)='" + txtsearch1.Text + "'";
                }
                else if (ddlfilter.SelectedValue == "3")
                {
                    searchoption = "l.LedgerName like '%" + txtsearch1.Text + "%'";
                }

            }

            ds = objbs.searchReceiptNew(searchoption, sTableName,"Receipt");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvledgrid.DataSource = ds;
                    gvledgrid.DataBind();
                }
                else
                {
                    gvledgrid.DataSource = null;
                    gvledgrid.DataBind();
                }
            }
            else
            {
                gvledgrid.DataSource = null;
                gvledgrid.DataBind();
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/viewreceipts.aspx");

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
            DataSet dLedger = objbs.LedgerReceipt("tblReceipt_" + sTableName, "tblDaybook_" + sTableName,"N");
            DataView dvEmp = dLedger.Tables[0].DefaultView;
            dvEmp.Sort = ViewState["SortExpr"].ToString();
            Session["SortedView"] = dvEmp;
            gvledgrid.DataSource = dvEmp;
            gvledgrid.DataBind();

        }
    }
}