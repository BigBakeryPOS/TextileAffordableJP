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
    public partial class Uom : System.Web.UI.Page
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
                ds = objbs.UNITS();
                gv.DataSource = ds;
                gv.DataBind();

            }




        }
        protected void reset(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            ds = objbs.UNITS();
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
                ds = objbs.UNITS();
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
                dserch = objbs.Uomsrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);
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
            if (txtuom.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Unit Of Measure.Thank You!!!');", true);
                return;
            }
            if (btnSubmit.Text == "Save")
            {
                DataSet dsCategory = objbs.Uomsrchgrid(txtuom.Text, 1);
                if (dsCategory != null)
                {
                    if (dsCategory.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These UOM has already Exists. please enter a new one')", true);
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    else
                    {
                        int iStatus = objbs.InsertUOM(txtuom.Text, ddlIsActive.SelectedValue, "tblAuditMaster_" + sTableName, lblUser.Text);
                        Response.Redirect("../Accountsbootstrap/Uom.aspx");
                    }
                }
                else
                {
                    int iStatus = objbs.InsertUOM(txtuom.Text, ddlIsActive.SelectedValue, "tblAuditMaster_" + sTableName, lblUser.Text);
                    Response.Redirect("../Accountsbootstrap/Uom.aspx");
                }
            }
            else
            {
                DataSet dsCategory = objbs.UOMsrchgridforupdate(Convert.ToInt32(txtid.Text), txtuom.Text);
                 if (dsCategory != null)
                 {
                     if (dsCategory.Tables[0].Rows.Count > 0)
                     {
                         ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These UOM has already Exists. please enter a new one')", true);
                         return;
                         // lblerror.Text = "These Category has already Exists. please enter a new one";

                     }
                     else
                     {

                         objbs.updateUOMMaster(Convert.ToInt32(txtid.Text), txtuom.Text, ddlIsActive.SelectedValue, "tblAuditMaster_" + sTableName, lblUser.Text);
                         Response.Redirect("Uom.aspx");
                     }
                 }
                 else
                 {
                     int iStatus = objbs.InsertUOM(txtuom.Text, ddlIsActive.SelectedValue, "tblAuditMaster_" + sTableName, lblUser.Text);
                     Response.Redirect("../Accountsbootstrap/Uom.aspx");
                 }
                
            }

         


           
        }
        private void clearall()
        {
            txtuom.Text = "";    
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

                    dedit = objbs.editumo( Convert.ToInt32(e.CommandArgument));
                    if (dedit.Tables[0].Rows.Count > 0)
                    {
                        txtuom.Text = dedit.Tables[0].Rows[0]["Units"].ToString();
                        ddlIsActive.SelectedValue = dedit.Tables[0].Rows[0]["Isactive"].ToString();
                        txtid.Text = dedit.Tables[0].Rows[0]["Uomid"].ToString();
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
                dss = objbs.UNITS();
            }
            else
            {
                dss = objbs.Uomsrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);    
            }

            gv.PageIndex = e.NewPageIndex;
            DataView dvEmployee = dss.Tables[0].DefaultView;
            gv.DataSource = dvEmployee;
            gv.DataBind();
          
        }

    }
}