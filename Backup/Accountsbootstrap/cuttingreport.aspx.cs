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
    public partial class cuttingreport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {

                DataSet ds = objBs.selectCuttingprocess();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvcust.DataSource = ds;
                        gvcust.DataBind();
                    }
                    else
                    {
                        gvcust.DataSource = null;
                        gvcust.DataBind();
                    }
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/Cuttingprocess.aspx");

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectCheque();
            }
            else
            {
                ds = objBs.searchfilterCheque(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.PageIndex = e.NewPageIndex;
                    gvcust.DataBind();
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchviewprocess(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.DataBind();
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }
        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Cuttingprocess.aspx?CuttingID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.deleteCheque(Convert.ToInt32(e.CommandArgument.ToString()));
                Response.Redirect("viewcutting.aspx");
            }
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string bankid = e.Row.Cells[1].Text;


                if (objBs.CeckIfChequeno(Convert.ToInt32(bankid), int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((Image)e.Row.FindControl("img")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;


                    ((Image)e.Row.FindControl("dlt")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = true;
                }

            }
        }
    }
}