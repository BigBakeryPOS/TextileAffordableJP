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
    public partial class Size : System.Web.UI.Page
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
                //DataSet dsCategory = objbs.selectcategorymaster();
                //if (dsCategory != null)
                //{
                //    if (dsCategory.Tables[0].Rows.Count > 0)
                //    {
                //        drpcategory.DataSource = dsCategory.Tables[0];
                //        drpcategory.DataTextField = "category";
                //        drpcategory.DataValueField = "CategoryID";
                //        drpcategory.DataBind();
                //        drpcategory.Items.Insert(0, "Select Category");
                //        //ddlcategory.Items.Insert(0, "Select Category");

                //    }
                //}

                DataSet dcust = objbs.getsize();
                if (dcust != null)
                {
                    if (dcust.Tables[0].Rows.Count > 0)
                    {
                        //ddlSize.DataSource = dcust.Tables[0];
                        //txtSize.Text = "Sizename";
                        //ddlSize.DataTextField = "Sizename";
                        //ddlSize.DataValueField = "Sizeid";
                        //ddlSize.DataBind();
                        //ddlSize.Items.Insert(0, "Select Size");
                    }
                }

                //DataSet dcustSleeve = objbs.getSleeve();
                //if (dcustSleeve != null)
                //{
                //    if (dcustSleeve.Tables[0].Rows.Count > 0)
                //    {
                //        ddlSleeve.DataSource = dcustSleeve.Tables[0];
                //        ddlSleeve.DataTextField = "Sleeve";
                //        ddlSleeve.DataValueField = "Sleeveid";
                //        ddlSleeve.DataBind();
                //       // ddlSleeve.Items.Insert(0, "Select Sleeve");
                //    }
                //}

                //clearall();
                ds = objbs.Size();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();
                    }
                }
            }




        }
        protected void reset(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            ds = objbs.Size();
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

        protected void search(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                ds = objbs.Size();
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
                dserch = objbs.Sizesrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);
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
            if (txtSize.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Size.Thank You!!!');", true);
                return;
            }
            if (btnSubmit.Text == "Save")
            {
                DataSet dsCategory = objbs.Sizesrchgrid(txtSize.Text, 1);
                if (dsCategory != null)
                {
                    if (dsCategory.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Size has already Exists. please enter a new one')", true);
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    else
                    {
                        int iStatus = objbs.InsertSize(txtSize.Text , txtSize.Text,txtSize.Text, "0", "tblAuditMaster_" + sTableName, lblUser.Text);
                        Response.Redirect("../Accountsbootstrap/Size.aspx");
                    }
                }
                else
                {
                    int iStatus = objbs.InsertSize(txtSize.Text, txtSize.Text, txtSize.Text, ("0"), "tblAuditMaster_" + sTableName, lblUser.Text);
                    Response.Redirect("../Accountsbootstrap/Size.aspx");
                }
            }
            else
            {
                DataSet dsCategory = objbs.Sizesrchgridforupdate(Convert.ToInt32(txtid.Text), txtSize.Text);
                if (dsCategory != null)
                {
                    if (dsCategory.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Size has already Exists. please enter a new one')", true);
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    else
                    {

                        objbs.updateSize(Convert.ToInt32(txtid.Text), txtSize.Text , txtSize.Text, txtSize.Text, Convert.ToInt32(0), "tblAuditMaster_" + sTableName, lblUser.Text);
                        Response.Redirect("Size.aspx");
                    }
                }
                else
                {
                    int iStatus = objbs.InsertSize(txtSize.Text , txtSize.Text, txtSize.Text,"0", "tblAuditMaster_" + sTableName, lblUser.Text);
                    Response.Redirect("../Accountsbootstrap/Size.aspx");
                }

            }





        }
        private void clearall()
        {
            //ddlSize.SelectedIndex = 0;
           // ddlSleeve.SelectedIndex = 0;
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

                    dedit = objbs.editSize(Convert.ToInt32(e.CommandArgument));
                    if (dedit.Tables[0].Rows.Count > 0)
                    {
                        txtSize.Text = dedit.Tables[0].Rows[0]["SizeNo"].ToString();
                      //  ddlSleeve.SelectedValue = dedit.Tables[0].Rows[0]["Sleeveid"].ToString();
                        //drpcategory.SelectedValue = dedit.Tables[0].Rows[0]["Category"].ToString();
                        txtid.Text = dedit.Tables[0].Rows[0]["Sizeid"].ToString();
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
                dss = objbs.Size();
            }
            else
            {
                dss = objbs.Sizesrchgridsearxh(txtsearch.Text, ddlfilter.SelectedValue);
            }

            gv.PageIndex = e.NewPageIndex;
            DataView dvEmployee = dss.Tables[0].DefaultView;
            gv.DataSource = dvEmployee;
            gv.DataBind();

        }

    }
}