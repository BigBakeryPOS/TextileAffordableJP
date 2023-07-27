using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using CommonLayer;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;


namespace Billing.Accountsbootstrap
{
    public partial class usercreate : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        int superadmin = 0;
        string empid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string sTableName = string.Empty;
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            empid = Session["Empid"].ToString();

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            if (!IsPostBack)
            {
                #region

                DataSet dsbranchto = objBs.getemployeeforusercreate("U");
                ddlemployee.DataSource = dsbranchto.Tables[0];
                ddlemployee.DataTextField = "Name";
                ddlemployee.DataValueField = "Employee_Id";
                ddlemployee.DataBind();
                ddlemployee.Items.Insert(0, "Select Employee");

                DataSet dbraqnch = objBs.GetCompanyDet();
                drpbranch.DataSource = dbraqnch.Tables[0];
                drpbranch.DataTextField = "CompanyName";
                drpbranch.DataValueField = "Comapanyid";
                drpbranch.DataBind();
                drpbranch.Items.Insert(0, "All");

                DataSet dsmsater = new DataSet();
                dsmsater = objBs.GetMasterRolesWithArea("Master");
                grdmaster.DataSource = dsmsater;
                grdmaster.DataBind();

                DataSet dsinventory = new DataSet();
                dsinventory = objBs.GetMasterRolesWithArea("Process");
                grdinventory.DataSource = dsinventory;
                grdinventory.DataBind();

                DataSet dsreport = new DataSet();
                dsreport = objBs.GetMasterRolesWithArea("Report");
                grdreport.DataSource = dsreport;
                grdreport.DataBind();

                DataSet dsadmin = new DataSet();
                dsadmin = objBs.GetMasterRolesWithArea("admin");
                grdadmin.DataSource = dsadmin;
                grdadmin.DataBind();

                #endregion

                int iCusID = Convert.ToInt32(Request.QueryString.Get("iCusID"));
                if (Convert.ToString(iCusID) != "" || iCusID != null)
                {
                    #region
                    DataSet ds1 = objBs.getselectuser(iCusID);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        btnadd.Text = "Update";
                        DataSet dsbranchto1 = objBs.getemployeeforusercreate("S");
                        ddlemployee.DataSource = dsbranchto1.Tables[0];
                        ddlemployee.DataTextField = "Name";
                        ddlemployee.DataValueField = "Employee_Id";
                        ddlemployee.DataBind();
                        ddlemployee.Items.Insert(0, "Select Employee");

                        ddlemployee.Enabled = false;
                        txtUserid.Text = ds1.Tables[0].Rows[0]["Userid"].ToString();
                        txtusername.Text = ds1.Tables[0].Rows[0]["Username"].ToString();
                        txtpassword.Attributes.Add("value", ds1.Tables[0].Rows[0]["Password"].ToString());
                        txtconfirmpasswprd.Attributes.Add("value", ds1.Tables[0].Rows[0]["Password"].ToString());
                        ddlemployee.SelectedValue = ds1.Tables[0].Rows[0]["Empid"].ToString();
                        drpbranch.SelectedValue = ds1.Tables[0].Rows[0]["Companyid"].ToString();
                        bool suer = Convert.ToBoolean(ds1.Tables[0].Rows[0]["Issuperadmin"]);
                        if (suer == true)
                        {
                            chkRememberMe.Checked = true;
                        }
                        else
                        {
                            chkRememberMe.Checked = false;

                        }
                        //  txtpassword.Text = ds1.Tables[0].Rows[0]["Password"].ToString();
                        //  txtconfirmpasswprd.Text = ds1.Tables[0].Rows[0]["confirmpassword"].ToString();
                        txtEmail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();
                        int idd = Convert.ToInt32(txtUserid.Text);



                        //DataSet dsdashboardup = new DataSet();
                        //dsdashboardup = objBs.Getuseroptionsforid("Dashboard", idd);
                        //Griddashborad.DataSource = dsdashboardup;
                        //Griddashborad.DataBind();

                        DataSet dsmsaterup = new DataSet();
                        //string types ="Master";
                        dsmsaterup = objBs.Getuseroptionsforid("Master", idd);
                        // DataSet dsdashboardup = new DataSet();
                        //   dsdashboardup = objBs.Getuseroptionsforid("Dashboard", idd);
                        for (int vLoop = 0; vLoop < grdmaster.Rows.Count; vLoop++)
                        {
                            Label txtsno = (Label)grdmaster.Rows[vLoop].FindControl("lblDebtorID");
                            for (int j = 0; j < dsmsaterup.Tables[0].Rows.Count; j++)
                            {
                                string roleid = dsmsaterup.Tables[0].Rows[j]["roleid"].ToString();
                                bool Add = Convert.ToBoolean(dsmsaterup.Tables[0].Rows[j]["Visible"]);

                                if (roleid == txtsno.Text)
                                {

                                    CheckBox chkboxAdd = (CheckBox)grdmaster.Rows[vLoop].FindControl("chkboxAdd");

                                    chkboxAdd.Checked = Add;
                                }
                            }

                        }
                        //grdmaster.DataSource = dsmsaterup;
                        //grdmaster.DataBind();

                        DataSet dsinventoryup = new DataSet();
                        dsinventoryup = objBs.Getuseroptionsforid("Process", idd);
                        for (int vLoop = 0; vLoop < grdinventory.Rows.Count; vLoop++)
                        {
                            Label txtsno = (Label)grdinventory.Rows[vLoop].FindControl("lblDebtorID");
                            for (int j = 0; j < dsinventoryup.Tables[0].Rows.Count; j++)
                            {
                                string roleid = dsinventoryup.Tables[0].Rows[j]["roleid"].ToString();
                                bool Add = Convert.ToBoolean(dsinventoryup.Tables[0].Rows[j]["Visible"]);

                                if (roleid == txtsno.Text)
                                {

                                    CheckBox chkboxAdd = (CheckBox)grdinventory.Rows[vLoop].FindControl("chkboxAdd");

                                    chkboxAdd.Checked = Add;
                                }
                            }

                        }
                        // grdinventory.DataSource = dsinventoryup;
                        //grdinventory.DataBind();

                        //DataSet dsaccountup = new DataSet();
                        //dsaccountup = objBs.Getuseroptionsforid("Account", idd);
                        //grdaccount.DataSource = dsaccountup;
                        //grdaccount.DataBind();

                        DataSet dsreportup = new DataSet();
                        dsreportup = objBs.Getuseroptionsforid("Report", idd);
                        //  grdreport.DataSource = dsreportup;
                        //                        grdreport.DataBind();
                        for (int vLoop = 0; vLoop < grdreport.Rows.Count; vLoop++)
                        {
                            Label txtsno = (Label)grdreport.Rows[vLoop].FindControl("lblDebtorID");
                            for (int j = 0; j < dsreportup.Tables[0].Rows.Count; j++)
                            {
                                string roleid = dsreportup.Tables[0].Rows[j]["roleid"].ToString();
                                bool Add = Convert.ToBoolean(dsreportup.Tables[0].Rows[j]["Visible"]);

                                if (roleid == txtsno.Text)
                                {

                                    CheckBox chkboxAdd = (CheckBox)grdreport.Rows[vLoop].FindControl("chkboxAdd");

                                    chkboxAdd.Checked = Add;
                                }
                            }

                        }

                        DataSet dsadminup = new DataSet();
                        dsadminup = objBs.Getuseroptionsforid("admin", idd);
                        // grdadmin.DataSource = dsadminup;
                        // grdadmin.DataBind();
                        for (int vLoop = 0; vLoop < grdadmin.Rows.Count; vLoop++)
                        {
                            Label txtsno = (Label)grdadmin.Rows[vLoop].FindControl("lblDebtorID");
                            for (int j = 0; j < dsadminup.Tables[0].Rows.Count; j++)
                            {
                                string roleid = dsadminup.Tables[0].Rows[j]["roleid"].ToString();
                                bool Add = Convert.ToBoolean(dsadminup.Tables[0].Rows[j]["Visible"]);

                                if (roleid == txtsno.Text)
                                {

                                    CheckBox chkboxAdd = (CheckBox)grdadmin.Rows[vLoop].FindControl("chkboxAdd");

                                    chkboxAdd.Checked = Add;
                                }
                            }

                        }
                    }

                    #endregion
                }

            }
        }

        protected void ddlemployee_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlemployee.SelectedValue == "" || ddlemployee.SelectedValue == "0" || ddlemployee.SelectedValue == "Select Employee")
            {
                txtusername.Text = "";
                txtEmail.Text = "";
                drpbranch.ClearSelection();
            }
            else
            {
                DataSet ds = objBs.searchempid(ddlemployee.SelectedValue);
                txtusername.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();
                //  drpbranch.SelectedValue = ds.Tables[0].Rows[0]["UnitId"].ToString();

            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            #region

            //if (Session["YearCode"] == null || Session["YearCode"] == "" || Session["YearCode"].ToString() == null || Session["YearCode"].ToString() == "")
            if (Request.Cookies["userInfo"]["YearCode"] == null || Request.Cookies["userInfo"]["YearCode"] == "" || Request.Cookies["userInfo"]["YearCode"].ToString() == null || Request.Cookies["userInfo"]["YearCode"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Signout after Create.')", true);
                return;
            }
            if (txtpassword.Text != txtconfirmpasswprd.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Password and confirm password should not match')", true);
                txtconfirmpasswprd.Focus();
                return;
            }

            if (chkRememberMe.Checked == true)
            {
                superadmin = 1;
            }
            else
            {
                superadmin = 0;
            }

            #endregion

            #region

            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataSet dsBranch;
            DataTable dtBranch;
            DataColumn dc;

            ds = new DataSet();

            dt = new DataTable();

            dc = new DataColumn("UserName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Screen");
            dt.Columns.Add(dc);

            dc = new DataColumn("screenid");
            dt.Columns.Add(dc);

            dc = new DataColumn("screencode");
            dt.Columns.Add(dc);

            dc = new DataColumn("Active");
            dt.Columns.Add(dc);


            ds.Tables.Add(dt);
            bool Add = false;
            bool Edit = false;
            bool Delete = false;

            DataSet dsroles = new DataSet();
            DataTable dtt = new DataTable();
            DataRow drNewt;
            DataColumn dcc;
            dcc = new DataColumn("UserName");
            dtt.Columns.Add(dcc);

            dcc = new DataColumn("Screen");
            dtt.Columns.Add(dcc);
            dsroles.Tables.Add(dtt);

            bool Views = false;
            Label lblDebtorID = null;

            for (int vLoop = 0; vLoop < grdmaster.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)grdmaster.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                if ((txt.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtusername.Text;
                    drNewt["Screen"] = grdmaster.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtusername.Text;
                drNew["screencode"] = grdmaster.Rows[vLoop].Cells[3].Text;
                drNew["Screen"] = grdmaster.Rows[vLoop].Cells[1].Text;

                lblDebtorID = (Label)grdmaster.Rows[vLoop].FindControl("lblDebtorID");
                drNew["screenid"] = lblDebtorID.Text;

                drNew["Active"] = Add;

                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < grdinventory.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)grdinventory.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }



                if ((txt.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtusername.Text;
                    drNewt["Screen"] = grdinventory.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtusername.Text;
                drNew["screencode"] = grdinventory.Rows[vLoop].Cells[3].Text;
                drNew["Screen"] = grdinventory.Rows[vLoop].Cells[1].Text;

                lblDebtorID = (Label)grdinventory.Rows[vLoop].FindControl("lblDebtorID");
                drNew["screenid"] = lblDebtorID.Text;

                drNew["Active"] = Add;

                ds.Tables[0].Rows.Add(drNew);
            }
            //for (int vLoop = 0; vLoop < grdaccount.Rows.Count; vLoop++)
            //{
            //    CheckBox txt = (CheckBox)grdaccount.Rows[vLoop].FindControl("chkboxAdd");
            //    if (txt.Checked)
            //    {
            //        Add = txt.Checked;
            //    }
            //    else
            //    {
            //        Add = false;
            //    }



            //    if ((txt.Checked == true))
            //    {
            //        drNewt = dtt.NewRow();
            //        drNewt["UserName"] = txtusername.Text;
            //        drNewt["Screen"] = grdaccount.Rows[vLoop].Cells[3].Text;
            //        dsroles.Tables[0].Rows.Add(drNewt);
            //    }

            //    drNew = dt.NewRow();
            //    drNew["UserName"] = txtusername.Text;
            //    drNew["screencode"] = grdaccount.Rows[vLoop].Cells[3].Text;
            //    drNew["Screen"] = grdaccount.Rows[vLoop].Cells[1].Text;

            //    lblDebtorID = (Label)grdaccount.Rows[vLoop].FindControl("lblDebtorID");
            //    drNew["screenid"] = lblDebtorID.Text;

            //    drNew["Active"] = Add;

            //    ds.Tables[0].Rows.Add(drNew);
            //}
            for (int vLoop = 0; vLoop < grdreport.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)grdreport.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }



                if ((txt.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtusername.Text;
                    drNewt["Screen"] = grdreport.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtusername.Text;
                drNew["screencode"] = grdreport.Rows[vLoop].Cells[3].Text;
                drNew["Screen"] = grdreport.Rows[vLoop].Cells[1].Text;

                lblDebtorID = (Label)grdreport.Rows[vLoop].FindControl("lblDebtorID");
                drNew["screenid"] = lblDebtorID.Text;

                drNew["Active"] = Add;

                ds.Tables[0].Rows.Add(drNew);
            }
            for (int vLoop = 0; vLoop < grdadmin.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)grdadmin.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }



                if ((txt.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtusername.Text;
                    drNewt["Screen"] = grdadmin.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtusername.Text;
                drNew["screencode"] = grdadmin.Rows[vLoop].Cells[3].Text;
                drNew["Screen"] = grdadmin.Rows[vLoop].Cells[1].Text;

                lblDebtorID = (Label)grdadmin.Rows[vLoop].FindControl("lblDebtorID");
                drNew["screenid"] = lblDebtorID.Text;

                drNew["Active"] = Add;

                ds.Tables[0].Rows.Add(drNew);
            }

            #endregion

            if (btnadd.Text == "Save")
            {
                DataSet DS = objBs.usersrchgrid(txtusername.Text, 0);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This UserName was already Exists. Please Enter a new one.')", true);
                    txtusername.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.inseruserwithaccess(Convert.ToInt32(lblUserID.Text), txtusername.Text, txtpassword.Text, txtEmail.Text, ds, ddlemployee.SelectedValue, superadmin, empid, drpbranch.SelectedValue, Request.Cookies["userInfo"]["YearCode"].ToString());
                }
            }
            else
            {
                DataSet DS = objBs.usersrchgrid(txtusername.Text, Convert.ToInt32(txtUserid.Text));
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This UserName was already Exists. Please Enter a new one.')", true);
                    txtusername.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.updateusernew(Convert.ToInt32(txtUserid.Text), txtusername.Text, txtpassword.Text, txtEmail.Text, ds, ddlemployee.SelectedValue, superadmin, empid, drpbranch.SelectedValue, Request.Cookies["userInfo"]["YearCode"].ToString());
                }
            }

            Response.Redirect("../Accountsbootstrap/UserCreateGrid.aspx");

        }
        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/UserCreateGrid.aspx");
        }
    }
}