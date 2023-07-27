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
    public partial class StockTransferGrid : System.Web.UI.Page
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
                DataSet ds = objBs.GetGridStockTransfer();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVItem.DataSource = ds;
                    GVItem.DataBind();
                }

                else
                {
                    GVItem.DataSource = null;
                    GVItem.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("StockTransfer.aspx");
        }

        protected void GVItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("StockTransfer.aspx?StockTransferId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    DataSet dsup = objBs.DeleteIPOEntryCheck(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (dsup.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot be Delete,Please Check ItemProcess Order Challan.');", true);
                        return;
                    }
                    else
                    {
                        int iSucess = objBs.DeleteIPOEntry(Convert.ToInt32(e.CommandArgument.ToString()));
                        Response.Redirect("ItemProcessOrderEntryGrid.aspx");
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


