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
    public partial class ItemType : System.Web.UI.Page
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
                DataSet dsParkingType = objBs.selectcategorylist();
                if (dsParkingType.Tables[0].Rows.Count > 0)
                {
                    ddlItemHead.DataSource = dsParkingType.Tables[0];
                    ddlItemHead.DataTextField = "category";
                    ddlItemHead.DataValueField = "categoryid";
                    ddlItemHead.DataBind();
                    ddlItemHead.Items.Insert(0, "Select ItemHead");
                }
                ddlItemGroup.Items.Insert(0, "Select ItemGroup");

                DataSet ds = objBs.gridItemType();
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

        protected void ddlItemHead_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlItemHead.SelectedValue == "" || ddlItemHead.SelectedValue == "0" || ddlItemHead.SelectedValue == "Select ItemHead")
            {
                ddlItemGroup.Items.Clear();
                ddlItemGroup.Items.Insert(0, "Select ItemGroup");
            }
            else
            {
                DataSet dsItemGroup = objBs.GetHeadItemGroup(Convert.ToInt32(ddlItemHead.SelectedValue));
                if (dsItemGroup.Tables[0].Rows.Count > 0)
                {
                    ddlItemGroup.DataSource = dsItemGroup.Tables[0];
                    ddlItemGroup.DataTextField = "Itemgroupname";
                    ddlItemGroup.DataValueField = "ItemgroupId";
                    ddlItemGroup.DataBind();
                    ddlItemGroup.Items.Insert(0, "Select ItemGroup");
                }
                else
                {
                    ddlItemGroup.Items.Clear();
                    ddlItemGroup.Items.Insert(0, "Select ItemGroup");
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
            lblName.Text = "Update ItemType";

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
            {
                string id = gridview.SelectedDataKey.Value.ToString();

                DataSet ds = objBs.getiItemTypevalues(id);
                if (ds.Tables[0].Rows.Count > 0)

                    txtItemId.Text = ds.Tables[0].Rows[0]["ItemId"].ToString();

                ddlItemHead.SelectedValue = ds.Tables[0].Rows[0]["HeadId"].ToString();
                ddlItemHead.Enabled = false;

                DataSet dsItemGroup = objBs.GetHeadItemGroup(Convert.ToInt32(ddlItemHead.SelectedValue));
                if (dsItemGroup.Tables[0].Rows.Count > 0)
                {
                    ddlItemGroup.DataSource = dsItemGroup.Tables[0];
                    ddlItemGroup.DataTextField = "Itemgroupname";
                    ddlItemGroup.DataValueField = "ItemgroupId";
                    ddlItemGroup.DataBind();
                    ddlItemGroup.Items.Insert(0, "Select ItemGroup");
                }
                else
                {
                    ddlItemGroup.Items.Clear();
                    ddlItemGroup.Items.Insert(0, "Select ItemGroup");
                }
                ddlItemGroup.SelectedValue = ds.Tables[0].Rows[0]["GroupId"].ToString();
                ddlItemGroup.Enabled = false;

                txtItemCode.Text = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                txtItemDescription.Text = ds.Tables[0].Rows[0]["ItemDescription"].ToString();

                ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                Button1.Text = "Update";
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            if (ddlItemHead.SelectedValue == "Select ItemHead")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Item Head.Thank you.')", true);
                ddlItemHead.Focus();
                return;
            }
            if (ddlItemGroup.SelectedValue == "Select ItemGroup")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Item Group.Thank you.')", true);
                ddlItemGroup.Focus();
                return;
            }



            if (Button1.Text == "Save")
            {
                DataSet dsItemCode = objBs.ItemTypesrchgrid("ItemCode", txtItemCode.Text, 0);
                DataSet dsItemDescription = objBs.ItemTypesrchgrid("ItemDescription", txtItemDescription.Text, 0);
                if (dsItemCode.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This ItemCode was already Exists.')", true);
                    txtItemCode.Focus();
                    return;
                }
                else if (dsItemDescription.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Description was already Exists.')", true);
                    txtItemDescription.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.InsertItemType(Convert.ToInt32(ddlItemHead.SelectedValue), Convert.ToInt32(ddlItemGroup.SelectedValue), txtItemCode.Text, txtItemDescription.Text, ddlIsActive.SelectedValue);
                    Response.Redirect("ItemType.aspx");
                }
            }
            else
            {
                DataSet dsItemCode = objBs.ItemTypesrchgrid("ItemCode", txtItemCode.Text, Convert.ToInt32(txtItemId.Text));
                DataSet dsItemDescription = objBs.ItemTypesrchgrid("ItemDescription", txtItemDescription.Text, Convert.ToInt32(txtItemId.Text));
                if (dsItemCode.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This ItemCode was already Exists.')", true);
                    txtItemCode.Focus();
                    return;
                }
                else if (dsItemDescription.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Description was already Exists.')", true);
                    txtItemDescription.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.updateItemTypeMaster(Convert.ToInt32(ddlItemHead.SelectedValue), Convert.ToInt32(ddlItemGroup.SelectedValue), txtItemCode.Text, txtItemDescription.Text, ddlIsActive.SelectedValue, Convert.ToInt32(txtItemId.Text));
                    Response.Redirect("ItemType.aspx");
                }

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtItemId.Text = "";

            ddlItemHead.ClearSelection();
            ddlItemGroup.ClearSelection();

            txtItemCode.Text = "";
            txtItemDescription.Text = "";

            ddlIsActive.ClearSelection();

            Button1.Text = "Save";
            lblName.Text = "Add ItemType";
        }
        protected void btnresret_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ItemType.aspx");
        }

    }
}
