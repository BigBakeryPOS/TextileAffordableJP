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
    public partial class Dillo_Designation : System.Web.UI.Page
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
                //clearall();
                ds = objbs.Select_department();
                if (ds != null)
                {
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
                }

            }




        }

        protected void reset(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            ds = objbs.Grade();
            if (ds != null)
            {
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
            }
        }

        protected void search(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                ds = objbs.Grade();
                if (ds != null)
                {
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
                }
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Mobile No!!!');", true);
                // return;
            }
            else
            {
                DataSet dserch = new DataSet();
                dserch = objbs.Gradesrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);
                if (dserch != null)
                {
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
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            clearall();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string Followedby = "N";

            if (chkfollowedbyapprovallist.Checked == true)
            {
                Followedby = "Y";
            }


            if (txtdep.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Designation.Thank You!!!');", true);
                return;
            }
            if (btnSubmit.Text == "Save")
            {
                DataSet dsCategory = objbs.Depsrchgrid(txtdep.Text, 1);
                if (dsCategory != null)
                {
                    if (dsCategory.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Designation has already Exists. please enter a new one')", true);
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    else
                    {
                        int iStatus = objbs.InsertDEP(txtdep.Text, "tblAuditMaster_" + sTableName, lblUser.Text,Followedby);
                        Response.Redirect("../Accountsbootstrap/Dillo_Designation.aspx");
                    }
                }
                else
                {
                    int iStatus = objbs.InsertDEP(txtdep.Text, "tblAuditMaster_" + sTableName, lblUser.Text, Followedby);
                    Response.Redirect("../Accountsbootstrap/Dillo_Designation.aspx");
                }
            }
            else
            {
                DataSet dsCategory = objbs.deptsrchgridforupdate(Convert.ToInt32(txtid.Text), txtdep.Text);
                if (dsCategory != null)
                {
                    if (dsCategory.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Designation has already Exists. please enter a new one')", true);
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    else
                    {

                        objbs.updateDEP(Convert.ToInt32(txtid.Text), txtdep.Text, "tblAuditMaster_" + sTableName, lblUser.Text,Followedby);
                        Response.Redirect("Dillo_Designation.aspx");
                    }
                }
                else
                {
                    //int iStatus = objbs.InsertGrade(txtGrade.Text, txtSMSFrequency.Text, "tblAuditMaster_" + sTableName, lblUser.Text);
                    objbs.updateDEP(Convert.ToInt32(txtid.Text), txtdep.Text, "tblAuditMaster_" + sTableName, lblUser.Text, Followedby);
                    Response.Redirect("../Accountsbootstrap/Dillo_Designation.aspx");
                }

            }





        }

        private void clearall()
        {
            //txtGrade.Text = "";
            //txtSMSFrequency.Text = "";
            txtdep.Text = "";
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

                    dedit = objbs.editDEPT(Convert.ToInt32(e.CommandArgument));
                    if (dedit.Tables[0].Rows.Count > 0)
                    {
                        txtdep.Text = dedit.Tables[0].Rows[0]["DesiginationName"].ToString();

                        txtid.Text = dedit.Tables[0].Rows[0]["Desiginationid"].ToString();


                        string ManagementApproval = dedit.Tables[0].Rows[0]["Chkfollowedby"].ToString();

                        if (ManagementApproval == "Y")
                        {
                            chkfollowedbyapprovallist.Checked = true;
                        }


                        btnSubmit.Text = "Update";
                    }

                }
            }



        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            //DataSet dss = new DataSet();
            //if (ddlfilter.SelectedValue == "0")
            //{
            //    dss = objbs.Grade();
            //}
            //else
            //{
            //    dss = objbs.Gradesrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);
            //}

            //gv.PageIndex = e.NewPageIndex;
            //DataView dvEmployee = dss.Tables[0].DefaultView;
            //gv.DataSource = dvEmployee;
            //gv.DataBind();

        }

    }
}