using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Billing.Accountsbootstrap
{
    public partial class Category : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                DataSet ds = objBs.gridCategory();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridview.DataSource = ds;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
        }

        protected void gridview_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (IsSuperAdmin != "1")
                {
                    Button1.Enabled = false;

                    ((Image)e.Row.FindControl("imdedit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imdedit2")).Visible = true;

                    ((Image)e.Row.FindControl("Image1")).Visible = false;
                    ((ImageButton)e.Row.FindControl("Image2")).Visible = true;
                }
            }
        }
        protected void gridview_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            lblName.Text = "Update Category";

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
            {
                string id = gridview.SelectedDataKey.Value.ToString();

                DataSet ds = objBs.getiCategoryvalues(id);
                if (ds.Tables[0].Rows.Count > 0)

                    txtCategoryID.Text = ds.Tables[0].Rows[0]["CategoryID"].ToString();

                txtCategory.Text = ds.Tables[0].Rows[0]["Category"].ToString();

                ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                Button1.Text = "Update";
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Button1.Text == "Save")
            {
                DataSet dsCategory = objBs.Categorysrchgrid(txtCategory.Text, 0);
                if (dsCategory.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Category was already Exists.')", true);
                    txtCategory.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.InsertCategory(txtCategory.Text, ddlIsActive.SelectedValue);
                    Response.Redirect("Category.aspx");
                }
            }
            else
            {
                DataSet dsCategory = objBs.Categorysrchgrid(txtCategory.Text, Convert.ToInt32(txtCategoryID.Text));
                if (dsCategory.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Category was already Exists.')", true);
                    txtCategory.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.updateCategoryMaster(txtCategory.Text, ddlIsActive.SelectedValue, Convert.ToInt32(txtCategoryID.Text));
                    Response.Redirect("Category.aspx");
                }

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCategoryID.Text = "";

            txtCategory.Text = "";
            ddlIsActive.ClearSelection();

            Button1.Text = "Save";
            lblName.Text = "Add Category";
        }
        protected void btnresret_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Category.aspx");
        }

    }
}
