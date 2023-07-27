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
    public partial class ItemProcessOrderGrid : System.Web.UI.Page
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
                DataSet ds = objBs.gridItemProcessOrder();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVItemProcessOrder.DataSource = ds;
                    GVItemProcessOrder.DataBind();
                }

                else
                {
                    GVItemProcessOrder.DataSource = null;
                    GVItemProcessOrder.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("ItemProcessOrder.aspx");
        }

        protected void GVItemProcessOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("ItemProcessOrder.aspx?ItemPOId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Print")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("ItemProcessOrderPrint.aspx?ItemPOId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    DataSet dsup = objBs.DeleteIPOrderCheck(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (dsup.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot be Delete,Please Check ItemProcess Order Receive.');", true);
                        return;
                    }
                    else
                    {
                        int iSucess = objBs.DeleteIPOrder(Convert.ToInt32(e.CommandArgument.ToString()));
                        Response.Redirect("ItemProcessOrderGrid.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Admin.');", true);
                    return;
                }
            }
        }
       
    }
}


