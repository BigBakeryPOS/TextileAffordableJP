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

namespace Billing.Accountsbootstrap
{
    public partial class RequirementSheetGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string IsSuperAdmin = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            IsSuperAdmin = Session["IsSuperAdmin"].ToString();

            if (!IsPostBack)
            {

                // Get Year
                DataSet dsyear = objBs.GetYear();
                if (dsyear.Tables[0].Rows.Count > 0)
                {
                    drpyear.DataSource = dsyear.Tables[0];
                    drpyear.DataTextField = "yearname";
                    drpyear.DataValueField = "yearname";
                    drpyear.DataBind();
                    //drpyear.Items.Insert(0, "CompanyName");
                }


                DataSet ds = objBs.GetRequirementSheetGrid(drpyear.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvrequirmentOrder.DataSource = ds;
                    gvrequirmentOrder.DataBind();
                }

                else
                {
                    gvrequirmentOrder.DataSource = null;
                    gvrequirmentOrder.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {

            string button = string.Empty;
            button = btnadd.Text;
            {
                button = btnadd.Text;
                Response.Redirect("RequirementSheet.aspx");
            }
            //  Response.Redirect("../Accountsbootstrap/customermaster.aspx");

        }

        protected void gvBuyerOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("RequirementSheet.aspx?ReqID=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    DataSet dsup = objBs.DeleteRequirementSheetCheck(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (dsup.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot be Delete,Please Check BuyerOrder Cutting.');", true);
                        return;
                    }
                    else
                    {
                        int iSucess = objBs.DeleteRequirementSheet(Convert.ToInt32(e.CommandArgument.ToString()));
                        Response.Redirect("RequirementSheetGrid.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Admin.');", true);
                    return;
                }
            }
            
        }

        protected void Year_selected(object sender, EventArgs e)
        {
            DataSet ds = objBs.GetRequirementSheetGrid(drpyear.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvrequirmentOrder.DataSource = ds;
                gvrequirmentOrder.DataBind();
            }

            else
            {
                gvrequirmentOrder.DataSource = null;
                gvrequirmentOrder.DataBind();
            }
        }
    }
}


