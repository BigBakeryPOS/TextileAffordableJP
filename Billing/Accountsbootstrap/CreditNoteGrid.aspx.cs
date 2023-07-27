using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using CommonLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.Globalization;
using Org.BouncyCastle.Ocsp;

namespace Billing.Accountsbootstrap
{
    public partial class CreditNoteGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        DataSet ds=new DataSet();
        int EmpId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            EmpId = Convert.ToInt32(Session["EmpId"].ToString());
            if (!IsPostBack)
            {
               
                ds = objBs.selectCreditdebit1("tblCreditDebitNote_" + sTableName, sTableName);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        CreditDebitGrid.DataSource = ds;
                        CreditDebitGrid.DataBind();
                        Table1.Visible = false;
                    }
                    else
                    {
                        CreditDebitGrid.DataSource = null;
                        CreditDebitGrid.DataBind();
                    }
                }
                else
                {
                    CreditDebitGrid.DataSource = null;
                    CreditDebitGrid.DataBind();
                }
                
                DataSet dacess = new DataSet();
                dacess = objBs.getuseraccessscreen(Session["EmpId"].ToString(), "crdrnote");
                if (dacess.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowadd"]) == true)
                    {
                        btnadd.Visible = true;
                    }
                    else
                    {
                        btnadd.Visible = false;
                    }

                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowedit"]) == true)
                    {
                        CreditDebitGrid.Columns[7].Visible = true;
                    }
                    else
                    {
                        CreditDebitGrid.Columns[7].Visible = false;
                    }

                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowdelete"]) == true)
                    {
                        CreditDebitGrid.Columns[8].Visible = true;
                    }
                    else
                    {
                        CreditDebitGrid.Columns[8].Visible = false;
                    }
                }

                DataSet dsed = objBs.CheckEditDelete(EmpId);
                if (dsed.Tables[0].Rows.Count > 0)
                {
                    //if (dsed.Tables[0].Rows[0]["allowedit"].ToString() == "1")
                    //{
                    //    //btnadd.Visible = true;
                    //    CreditDebitGrid.Columns[7].Visible = true;
                    //}
                    //else
                    //{
                    //    CreditDebitGrid.Columns[7].Visible = false;
                    //}

                    //if (dsed.Tables[0].Rows[0]["allowdelete"].ToString() == "1")
                    //{
                    //    CreditDebitGrid.Columns[8].Visible = true;
                    //}
                    //else
                    //{
                    //    CreditDebitGrid.Columns[8].Visible = false;
                    //}
                }
            }
        }

        protected void btnrefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditNoteGrid.aspx");
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            
            DataSet ds = objBs.searchCreditmaster1("tblCreditDebitNote_" + sTableName, txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), sTableName);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CreditDebitGrid.DataSource = ds;
                    CreditDebitGrid.DataBind();
                    Table1.Visible = false;
                }
                else
                {
                    CreditDebitGrid.DataSource = null;
                    CreditDebitGrid.DataBind();

                }
            }
            else
            {
                CreditDebitGrid.DataSource = null;
                CreditDebitGrid.DataBind();
            }

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditNote.aspx");
        }

        protected void CreditDebitGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("CreditNote.aspx?DayBook_ID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.Creditdebitmasternew("tblCreditDebitNote_" + sTableName, e.CommandArgument.ToString(), "tblDaybook_" + sTableName, "tblTransReceipt_" + sTableName, "tblTransPayment_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, sTableName);
                Response.Redirect("CreditNoteGrid.aspx");
            }
        }

        protected void CreditDebitGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            ds = objBs.selectCreditdebitnew("tblCreditDebitNote_" + sTableName, sTableName);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    CreditDebitGrid.DataSource = ds;
                    CreditDebitGrid.PageIndex = e.NewPageIndex;
                    CreditDebitGrid.DataBind();
                }
                else
                {
                    CreditDebitGrid.DataSource = null;
                    CreditDebitGrid.DataBind();
                }
            }
            else
            {
                CreditDebitGrid.DataSource = null;
                CreditDebitGrid.DataBind();
            }
        }
    }
}