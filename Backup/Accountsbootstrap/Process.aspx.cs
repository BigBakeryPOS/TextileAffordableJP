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
    public partial class Process : System.Web.UI.Page
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
                DataSet dswidth = objBs.getItemCodewithescription();
                if (dswidth.Tables[0].Rows.Count > 0)
                {
                    chkItemTypes.DataSource = dswidth.Tables[0];
                    chkItemTypes.DataTextField = "ItemType";
                    chkItemTypes.DataValueField = "ItemId";
                    chkItemTypes.DataBind();
                }

                DataSet ds = objBs.gridProcess();
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
            lblName.Text = "Update Process";
            chkItemTypes.ClearSelection();
            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
            {
                string id = gridview.SelectedDataKey.Value.ToString();

                DataSet ds = objBs.getiProcessvalues(id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProcessID.Text = ds.Tables[0].Rows[0]["ProcessID"].ToString();

                    txtProcess.Text = ds.Tables[0].Rows[0]["Process"].ToString();
                    ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (ds.Tables[0].Rows[j]["ItemTypeId"].ToString() != "")
                        {
                            chkItemTypes.Items.FindByValue(ds.Tables[0].Rows[j]["ItemTypeId"].ToString()).Selected = true;
                        }
                    }
                }
                Button1.Text = "Update";
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Button1.Text == "Save")
            {
                DataSet dsProcess = objBs.Processsrchgrid(txtProcess.Text, 0);
                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Process was already Exists.')", true);
                    txtProcess.Focus();
                    return;
                }
                else
                {
                    int ProcessId = objBs.InsertProcess(txtProcess.Text, ddlIsActive.SelectedValue);
                    foreach (ListItem listItem in chkItemTypes.Items)
                    {
                        if (listItem.Selected)
                        {
                            int iSiz = objBs.InsertTransProcess(ProcessId, Convert.ToInt32(listItem.Value));
                        }
                    }
                    Response.Redirect("Process.aspx");
                }
            }
            else
            {
                DataSet dsProcess = objBs.Processsrchgrid(txtProcess.Text, Convert.ToInt32(txtProcessID.Text));
                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Process was already Exists.')", true);
                    txtProcess.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.updateProcessMaster(txtProcess.Text, ddlIsActive.SelectedValue, Convert.ToInt32(txtProcessID.Text));
                    foreach (ListItem listItem in chkItemTypes.Items)
                    {
                        if (listItem.Selected)
                        {
                            int iSiz = objBs.InsertTransProcess(Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(listItem.Value));
                        }
                    }
                    Response.Redirect("Process.aspx");
                }

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtProcessID.Text = "";

            txtProcess.Text = "";
            ddlIsActive.ClearSelection();

            Button1.Text = "Save";
            lblName.Text = "Add Process";
        }
        protected void btnresret_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Process.aspx");
        }

    }
}
