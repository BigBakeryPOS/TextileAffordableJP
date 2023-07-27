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
using System.IO;


namespace Billing.Accountsbootstrap
{
    public partial class MaterialsIssueGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                lblUser.Text = Session["UserName"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objBs.GetMAterialIssueLoadGrid();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBuyerOrderCutting.DataSource = ds;
                    gvBuyerOrderCutting.DataBind();
                }

                else
                {
                    gvBuyerOrderCutting.DataSource = null;
                    gvBuyerOrderCutting.DataBind();
                }
            }
        }
        protected void ddlfilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet ds = objBs.GetBuyerOrderCuttingData(ddlfilter.SelectedValue);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    gvBuyerOrderCutting.DataSource = ds;
            //    gvBuyerOrderCutting.DataBind();
            //}

            //else
            //{
            //    gvBuyerOrderCutting.DataSource = null;
            //    gvBuyerOrderCutting.DataBind();
            //}
        }

        protected void Add_Click(object sender, EventArgs e)
        {
              Response.Redirect("MaterialsIssue.aspx");
        }

        protected void gvBuyerOrderCutting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("MaterialsIssue.aspx?MaterialissueId=" + e.CommandArgument.ToString());
                }
            }
            if (e.CommandName == "IssuePrint")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("MaterialProcessEntryPrint.aspx?MaterialissueId=" + e.CommandArgument.ToString());
                }
            }
        }

    }
}


