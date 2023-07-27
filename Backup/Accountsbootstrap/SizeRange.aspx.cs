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
    public partial class SizeRange : System.Web.UI.Page
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

                DataSet dswidth = objBs.selectsize();
                if (dswidth.Tables[0].Rows.Count > 0)
                {
                    chksize.DataSource = dswidth.Tables[0];
                    chksize.DataTextField = "Size";
                    chksize.DataValueField = "SizeID";
                    chksize.DataBind();
                }


                DataSet ds = objBs.gridSizeRange();
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
            lblName.Text = "Update SizeRange";
            chksize.ClearSelection();

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
            {
                string id = gridview.SelectedDataKey.Value.ToString();

                DataSet ds = objBs.getiSizeRangevalues(id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRangeID.Text = ds.Tables[0].Rows[0]["RangeID"].ToString();

                    txtSizeRange.Text = ds.Tables[0].Rows[0]["Range"].ToString();
                    ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (ds.Tables[0].Rows[j]["SizeID"].ToString() != "")
                        {
                            chksize.Items.FindByValue(ds.Tables[0].Rows[j]["SizeID"].ToString()).Selected = true;
                        }
                    }
                }
                Button1.Text = "Update";
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            if (chksize.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Select Size.')", true);
                chksize.Focus();
                return;
            }

            if (Button1.Text == "Save")
            {
                DataSet dsSizeRange = objBs.SizeRangesrchgrid(txtSizeRange.Text, 0);
                if (dsSizeRange.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This SizeRange was already Exists.')", true);
                    txtSizeRange.Focus();
                    return;
                }
                else
                {
                    int RangeId = objBs.InsertSizeRange(txtSizeRange.Text, ddlIsActive.SelectedValue);

                    foreach (ListItem listItem in chksize.Items)
                    {
                        if (listItem.Selected)
                        {
                            int iSiz = objBs.InsertTransSizeRange(RangeId, Convert.ToInt32(listItem.Value));
                        }
                    }

                    Response.Redirect("SizeRange.aspx");
                }
            }
            else
            {
                DataSet dsSizeRange = objBs.SizeRangesrchgrid(txtSizeRange.Text, Convert.ToInt32(txtRangeID.Text));
                if (dsSizeRange.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This SizeRange was already Exists.')", true);
                    txtSizeRange.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.updateSizeRangeMaster(txtSizeRange.Text, ddlIsActive.SelectedValue, Convert.ToInt32(txtRangeID.Text));
                    foreach (ListItem listItem in chksize.Items)
                    {
                        if (listItem.Selected)
                        {
                            int iSiz = objBs.InsertTransSizeRange(Convert.ToInt32(txtRangeID.Text), Convert.ToInt32(listItem.Value));
                        }
                    }
                    Response.Redirect("SizeRange.aspx");
                }

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtRangeID.Text = "";

            txtSizeRange.Text = "";

            chksize.ClearSelection();

            ddlIsActive.ClearSelection();

            Button1.Text = "Save";
            lblName.Text = "Add SizeRange";
        }
        protected void btnresret_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SizeRange.aspx");
        }

    }
}
