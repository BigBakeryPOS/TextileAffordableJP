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
    public partial class viewjournals : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        string sTableName = "";
        int EmpId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            EmpId = Convert.ToInt32(Session["EmpId"].ToString());
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

           // sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                DataSet dLedger = objbs.LedgerJournal("tblJournal_" + sTableName, "tblDaybook_" + sTableName, sTableName);
                //if (dLedger.Tables[0].Rows.Count == 0)
                //{
                //    gvledgrid.DataSource = null;
                //    gvledgrid.DataBind();
                //}
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

                DataSet dacess = new DataSet();
                dacess = objbs.getuseraccessscreen(Session["EmpId"].ToString(), "journal");
                if (dacess.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowadd"]) == true)
                    {
                    btnnew.Visible = true;
                    }
                    else
                    {
                        btnnew.Visible = false;
                    }

                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowedit"]) == true)
                    {
                        gvledgrid.Columns[7].Visible = true;
                    }
                    else
                    {
                        gvledgrid.Columns[7].Visible = false;
                    }

                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowdelete"]) == true)
                    {
                        gvledgrid.Columns[8].Visible = true;
                    }
                    else
                    {
                        gvledgrid.Columns[8].Visible = false;
                    }
                }

                DataSet dsed = objbs.CheckEditDelete(EmpId);
                if (dsed.Tables[0].Rows.Count > 0)
                {
                    //if (dsed.Tables[0].Rows[0]["allowedit"].ToString() == "1")
                    //{
                    //    //btnadd.Visible = true;
                    //    gvledgrid.Columns[7].Visible = true;
                    //}
                    //else
                    //{
                    //    gvledgrid.Columns[7].Visible = false;
                    //}

                    //if (dsed.Tables[0].Rows[0]["allowdelete"].ToString() == "1")
                    //{
                    //    gvledgrid.Columns[8].Visible = true;
                    //}
                    //else
                    //{
                    //    gvledgrid.Columns[8].Visible = false;
                    //}
                }
            }
        }

        protected void btnnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("JournalScreen.aspx");
        }

        protected void gvledgrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("JournalScreen.aspx?TransNo=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "Del")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    objbs.DeleteJournal(Convert.ToInt32(e.CommandArgument.ToString()), "tblJournal_" + sTableName, "tblDaybook_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, EmpId);
                    Response.Redirect("viewjournals.aspx");
                }
            }
        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet dLedger = objbs.LedgerJournal("tblJournal_" + sTableName, "tblDaybook_" + sTableName, sTableName);
            if (dLedger != null)
            {
                if (dLedger.Tables[0].Rows.Count > 0)
                {
                    gvledgrid.DataSource = dLedger;
                    gvledgrid.PageIndex = e.NewPageIndex;
                  //  bindGridView();
                   // DataView dvEmployee = dLedger.Tables[0].DefaultView;
                  
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
            DataSet ds = objbs.searchJournal(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), "tblJournal_" + sTableName, "tblDaybook_" + sTableName);

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

            Response.Redirect("../Accountsbootstrap/viewjournals.aspx");

        }
    }
}