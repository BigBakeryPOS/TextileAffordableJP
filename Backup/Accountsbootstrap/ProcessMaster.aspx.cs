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
    public partial class ProcessMaster : System.Web.UI.Page
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
                DataSet dsFit = objbs.ProcessMasterHeadingSelect();//tblProcessMaster
                if (dsFit.Tables[0].Rows.Count > 0)
                {
                    ddlHeading.DataSource = dsFit.Tables[0];
                    ddlHeading.DataTextField = "HeadingName";
                    ddlHeading.DataValueField = "ProcessHeadingID";
                    ddlHeading.DataBind();
                    ddlHeading.Items.Insert(0, "Select Process");
                    txtid.Text = dsFit.Tables[0].Rows[0]["ProcessHeadingID"].ToString();
                }

                //DataSet dsType = objbs.ProcessMasterTypeSelect();//tblProcessMaster
                //if (dsType.Tables[0].Rows.Count > 0)
                //{
                //    ddlHeading.DataSource = dsType.Tables[0];
                //    ddlHeading.DataTextField = "Type";
                //    ddlHeading.DataValueField = "TypeID";
                //    ddlHeading.DataBind();
                //}

                ds = objbs.ProcessMasterSel();
                gv.DataSource = ds;
                gv.DataBind();
            }
        }

        protected void reset(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            ds = objbs.Unit();
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
                ds = objbs.ProcessHeading();
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
                dserch = objbs.Unitsrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);
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
            if (txtProcessType.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Process Type.Thank You!!!');", true);
                return;
            }

            if (txtPrintName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Print Name. Thank You!!!');", true);
            }
            if (btnSubmit.Text == "Save")
            {

                int iStatus = objbs.InsertProcessMaster(Convert.ToInt32(ddlHeading.SelectedValue), txtProcessType.Text, ddlIsActive.SelectedValue,Convert.ToInt32(ddlType.SelectedValue), ddlType.SelectedItem.Text, txtPrintName.Text , "tblAuditMaster_" + sTableName, lblUser.Text);
                Response.Redirect("../Accountsbootstrap/ProcessMaster.aspx");
            }
            else
            {
                int iStatus = objbs.updateProcessMaster(Convert.ToInt32(txtid.Text), Convert.ToInt32(ddlHeading.SelectedValue), txtProcessType.Text, ddlIsActive.SelectedValue, Convert.ToInt32(ddlType.SelectedValue), ddlType.SelectedItem.Text, txtPrintName.Text, "tblAuditMaster_" + sTableName, lblUser.Text);
                Response.Redirect("../Accountsbootstrap/ProcessMaster.aspx");
            }
        }

        private void clearall()
        {
            txtProcessType.Text = "";
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

                    dedit = objbs.editProcessMasterCase(Convert.ToInt32(e.CommandArgument));
                    if (dedit.Tables[0].Rows.Count > 0)
                    {
                        ddlHeading.SelectedValue = dedit.Tables[0].Rows[0]["ProcessHeadingID"].ToString();
                        txtProcessType.Text = dedit.Tables[0].Rows[0]["ProcessType"].ToString();
                        txtPrintName.Text = dedit.Tables[0].Rows[0]["PrintName"].ToString();
                        ddlIsActive.SelectedValue = dedit.Tables[0].Rows[0]["Isactive"].ToString();
                        txtid.Text = dedit.Tables[0].Rows[0]["ProcessMasterID"].ToString();
                        ddlType.Text = dedit.Tables[0].Rows[0]["TypeID"].ToString();
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
                dss = objbs.Unit();
            }
            else
            {
                dss = objbs.Unitsrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);
            }

            gv.PageIndex = e.NewPageIndex;
            DataView dvEmployee = dss.Tables[0].DefaultView;
            gv.DataSource = dvEmployee;
            gv.DataBind();

        }
    }
}