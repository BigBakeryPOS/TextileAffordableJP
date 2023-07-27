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
    public partial class BuyerOrderSalesGrid : System.Web.UI.Page
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
                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlcompany.DataSource = dsCompany.Tables[0];
                    ddlcompany.DataTextField = "CompanyName";
                    ddlcompany.DataValueField = "ComapanyID";
                    ddlcompany.DataBind();
                    ddlcompany.Items.Insert(0, "All");
                }
                txtfromDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                //DataSet ds = objBs.GetBuyerOrderSalesInv(ddlcompany.SelectedValue,txtfromDate.Text,txttodate.Text );
                DataSet ds = objBs.GetBuyerOrderSalesInvwithCompany(ddlcompany.SelectedValue,Convert.ToDateTime(txtfromDate.Text), Convert.ToDateTime(txttodate.Text));
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
            Response.Redirect("BuyerOrderSales.aspx");
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.GetBuyerOrderSalesInvwithCompany(ddlcompany.SelectedValue, Convert.ToDateTime(txtfromDate.Text), Convert.ToDateTime(txttodate.Text));
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
            protected void GVItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("BuyerOrderSales.aspx?BuyerOrderSalesId=" + e.CommandArgument.ToString());
                }
            }
          else  if (e.CommandName == "Print")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("BuyerSalesOrderPrintNew.aspx?BuyerOrderSalesId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    int iSucess = objBs.DeleteBuyerOrderSalesInv(Convert.ToInt32(e.CommandArgument.ToString()));
                    Response.Redirect("BuyerOrderSalesGrid.aspx");
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


