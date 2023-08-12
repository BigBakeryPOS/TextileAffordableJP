using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Text;
using System.Data;


namespace Billing.Accountsbootstrap
{
    public partial class SizeSettingNew : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string userid = string.Empty;
        int id = 0;
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            lblSuccess.Visible = false;
            lblFailure.Visible = false;
            lblWarning.Visible = false;
            if (!IsPostBack)
            {
                DataSet dsFit = objbs.Fit();//tblFit
                if (dsFit.Tables[0].Rows.Count > 0)
                {
                    ddlFit.DataSource = dsFit.Tables[0];
                    ddlFit.DataTextField = "Fit";
                    ddlFit.DataValueField = "FitID";
                    ddlFit.DataBind();
                    ddlFit.Items.Insert(0, "Select Fit");
                }

                DataSet dsSize = objbs.Width();//tblFit
                if (dsSize.Tables[0].Rows.Count > 0)
                {
                    ddlSize.DataSource = dsSize.Tables[0];
                    ddlSize.DataTextField = "Width";
                    ddlSize.DataValueField = "WidthID";
                    ddlSize.DataBind();
                    ddlSize.Items.Insert(0, "Select Width");
                }

                //clearall();
                ds = objbs.SizeSetting();
                gv.DataSource = ds;
                gv.DataBind();
            }
        }

        protected void reset(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            ds = objbs.SizeSetting();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds;
                gv.DataBind();
            }
        }
        protected void search(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                ds = objbs.SizeSetting();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                else
                {
                    gv.DataSource = null;
                    gv.DataBind();
                }

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Mobile No!!!');", true);
                // return; 
            }
            else
            {
                DataSet dserch = new DataSet();
                dserch = objbs.Fitsrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);
                if (dserch.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = dserch;
                    gv.DataBind();
                }
                else
                {
                    gv.DataSource = null;
                    gv.DataBind();
                }
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            clearall();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtFit.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Width.Thank You!!!');", true);
                return;
            }
            if (btnSubmit.Text == "Save")
            {
                DataSet dsCategory = objbs.Mtrsrchgrid(txtFit.Text,ddlFit.SelectedValue,ddlSize.SelectedValue, 1);
                if (dsCategory != null)
                {
                    if (dsCategory.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Fit has already Exists. please enter a new one')", true);
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    else
                    {
                        int iStatus = objbs.InsertSizeSettingNew( ddlFit.SelectedValue,ddlSize.SelectedValue, txtFit.Text);
                        Response.Redirect("../Accountsbootstrap/SizeSettingnew.aspx");
                    }
                }
                else
                {
                    int iStatus = objbs.InsertSizeSettingNew(ddlFit.SelectedValue, ddlSize.SelectedValue, txtFit.Text);
                    Response.Redirect("../Accountsbootstrap/SizeSettingnew.aspx");
                }
            }
            else
            {
                DataSet dsCategory = objbs.SizeSettingsrchgridforupdate(Convert.ToInt32(txtid.Text), txtFit.Text,ddlFit.SelectedValue,ddlSize.SelectedValue);
                 if (dsCategory != null)
                 {
                     if (dsCategory.Tables[0].Rows.Count > 0)
                     {
                         ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Size Setting has already Exists. please enter a new one')", true);
                         return;
                         // lblerror.Text = "These Category has already Exists. please enter a new one";

                     }
                     else
                     {

                         int ist = objbs.updateSizeSettingNewMaster(Convert.ToInt32(txtid.Text), txtFit.Text,ddlFit.SelectedValue,ddlSize.SelectedValue);
                         Response.Redirect("SizeSettingnew.aspx");
                     }
                 }
                 else
                 {
                    int iStatus = objbs.InsertSizeSettingNew(ddlFit.SelectedValue, ddlSize.SelectedValue, txtFit.Text);
                    Response.Redirect("../Accountsbootstrap/SizeSettingnew.aspx");
                 }                
            }
        }
        private void clearall()
        {
            txtFit.Text = "";    
            ddlIsActive.ClearSelection();
            btnSubmit.Text = "Save";


        }
        protected void gv_selectedindex(object sender, EventArgs e)
        {

            
        }
        protected void edit(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DataSet dedit = new DataSet();

                    dedit = objbs.editSizeSetting(Convert.ToInt32(e.CommandArgument));
                    if (dedit.Tables[0].Rows.Count > 0)
                    {
                        txtFit.Text = dedit.Tables[0].Rows[0]["Mtr"].ToString();
                        ddlFit.SelectedValue = dedit.Tables[0].Rows[0]["FitId"].ToString();
                        ddlSize.SelectedValue = dedit.Tables[0].Rows[0]["SizeId"].ToString();
                        txtid.Text = dedit.Tables[0].Rows[0]["ID"].ToString();
                        btnSubmit.Text = "Update";
                    }

                }
            }
        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet dss = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                dss = objbs.Fit();
            }
            else
            {
                dss = objbs.Fitsrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);    
            }

            gv.PageIndex = e.NewPageIndex;
            DataView dvEmployee = dss.Tables[0].DefaultView;
            gv.DataSource = dvEmployee;
            gv.DataBind();
          
        }

    }
}