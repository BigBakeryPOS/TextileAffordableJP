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
    public partial class PartyType : System.Web.UI.Page
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

                DataSet dsset = objBs.getitemhead();
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    chkitemlist.DataSource = dsset.Tables[0];
                    chkitemlist.DataTextField = "Category";
                    chkitemlist.DataValueField = "CategoryID";
                    chkitemlist.DataBind();
                }

                DataSet dsGroup = objBs.getselectHeadingall();
                if (dsGroup.Tables[0].Rows.Count > 0)
                {
                    ddlGroup.DataSource = dsGroup.Tables[0];
                    ddlGroup.DataTextField = "GroupName";
                    ddlGroup.DataValueField = "GroupID";
                    ddlGroup.DataBind();
                }


                DataSet ds = objBs.gridPartyTypeWithGroup();
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
            lblName.Text = "Update PartyType";
            chkitemlist.ClearSelection();

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
            {
                string id = gridview.SelectedDataKey.Value.ToString();

                DataSet ds = objBs.getiPartyTypevalues(id);
                if (ds.Tables[0].Rows.Count > 0)

                    txtPartyTypeID.Text = ds.Tables[0].Rows[0]["PartyTypeID"].ToString();

                txtPartyType.Text = ds.Tables[0].Rows[0]["PartyType"].ToString();

                ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();
                ddlGroup.SelectedValue = ds.Tables[0].Rows[0]["GroupID"].ToString();
                if (ds.Tables[0].Rows[0]["ManagementApproval"].ToString() == "Y")
                {
                    chkmanagementApproval.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["ProcessEntryApproval"].ToString() == "Y")
                {
                    chkProcessEntryApproval.Checked = true;
                }

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[j]["ItemHeadId"].ToString() != "")
                    {
                        chkitemlist.Items.FindByValue(ds.Tables[0].Rows[j]["ItemHeadId"].ToString()).Selected = true;
                    }
                }

                Button1.Text = "Update";
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            string ManagementApproval = "N"; string ProcessEntryApproval = "N";

            if (chkmanagementApproval.Checked == true)
            {
                ManagementApproval = "Y";
            }
            if (chkProcessEntryApproval.Checked == true)
            {
                ProcessEntryApproval = "Y";
            }


            if (Button1.Text == "Save")
            {
                DataSet dsPartyType = objBs.PartyTypesrchgrid(txtPartyType.Text, 0);
                if (dsPartyType.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This PartyType was already Exists.')", true);
                    txtPartyType.Focus();
                    return;
                }
                else
                {
                    int ipartyid = objBs.InsertPartyType(txtPartyType.Text, ddlIsActive.SelectedValue, ManagementApproval, ProcessEntryApproval,Convert.ToInt32(ddlGroup.SelectedValue));

                    foreach (ListItem listItem in chkitemlist.Items)
                    {
                        if (listItem.Selected)
                        {
                            int iSiz = objBs.InsertTranspartytype(ipartyid, Convert.ToInt32(listItem.Value));
                        }
                    }


                    Response.Redirect("PartyType.aspx");
                }
            }
            else
            {
                DataSet dsPartyType = objBs.PartyTypesrchgrid(txtPartyType.Text, Convert.ToInt32(txtPartyTypeID.Text));
                if (dsPartyType.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This PartyType was already Exists.')", true);
                    txtPartyType.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.updatePartyTypeMaster(txtPartyType.Text, ddlIsActive.SelectedValue, Convert.ToInt32(txtPartyTypeID.Text), ManagementApproval, ProcessEntryApproval, Convert.ToInt32(ddlGroup.SelectedValue));

                    foreach (ListItem listItem in chkitemlist.Items)
                    {
                        if (listItem.Selected)
                        {
                            int iSiz = objBs.InsertTranspartytype(Convert.ToInt32(txtPartyTypeID.Text), Convert.ToInt32(listItem.Value));
                        }
                    }


                    Response.Redirect("PartyType.aspx");
                }

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtPartyTypeID.Text = "";

            txtPartyType.Text = "";
            ddlIsActive.ClearSelection();
            chkmanagementApproval.Checked = false;
            chkProcessEntryApproval.Checked = false;
            Button1.Text = "Save";
            lblName.Text = "Add PartyType";
        }
        protected void btnresret_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PartyType.aspx");
        }

    }
}
