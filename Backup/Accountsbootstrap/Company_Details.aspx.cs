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


namespace Billing.Accountsbootstrap
{
    public partial class Pranav_Details : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = string.Empty;
        string comppany = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            if (!IsPostBack)
            {

                DataSet dstate = objBs.GetAllCountry();
                if (dstate.Tables[0].Rows.Count > 0)
                {
                    ddlCountry.DataSource = dstate.Tables[0];
                    ddlCountry.DataTextField = "Name";
                    ddlCountry.DataValueField = "ID";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, "Select Country");
                }

                ddlState.Items.Insert(0, "Select State");
                ddlCity.Items.Insert(0, "Select City");


                string comppany = Request.QueryString.Get("iCusID");

                if (comppany != null)
                {

                    DataSet dstate1 = objBs.GetAllCountry();
                    if (dstate1.Tables[0].Rows.Count > 0)
                    {
                        ddlCountry.DataSource = dstate1.Tables[0];
                        ddlCountry.DataTextField = "Name";
                        ddlCountry.DataValueField = "ID";
                        ddlCountry.DataBind();
                        ddlCountry.Items.Insert(0, "Select Country");
                    }




                    DataSet ds = objBs.GetupdateCompanyDetails(comppany);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        btnadd.Text = "Update";
                        txtcompanyID.Text = ds.Tables[0].Rows[0]["ComapanyID"].ToString();
                        txtcompanycode.Text = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                        txtcompanyname.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                        txtmobileno.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        txtphoneno.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        txtarea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                        txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                      //  txtcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                        txtpincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString();
                        txtemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                        txttin.Text = ds.Tables[0].Rows[0]["Tin"].ToString();
                        txtcst.Text = ds.Tables[0].Rows[0]["cst"].ToString();
                        txtpan.Text = ds.Tables[0].Rows[0]["pan"].ToString();
                        ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["country"].ToString();
                        DataSet dsDistrict = objBs.GetAllCountryState(Convert.ToInt32(ddlCountry.SelectedValue));
                        if (dsDistrict.Tables[0].Rows.Count > 0)
                        {

                            ddlState.DataSource = dsDistrict.Tables[0];
                            ddlState.DataTextField = "State";
                            ddlState.DataValueField = "ID";
                            ddlState.DataBind();
                            ddlState.Items.Insert(0, "Select State");
                            ddlState.SelectedValue = ds.Tables[0].Rows[0]["state"].ToString();
                        }
                        else
                        {
                            ddlState.Items.Clear();
                            ddlState.Items.Insert(0, "Select State");
                            ddlCity.Items.Clear();
                            ddlCity.Items.Insert(0, "Select City");
                        }


                        DataSet dsCity = objBs.GetAllStateCity(Convert.ToInt32(ddlState.SelectedValue));
                        if (dsCity.Tables[0].Rows.Count > 0)
                        {
                            ddlCity.DataSource = dsCity.Tables[0];
                            ddlCity.DataTextField = "City";
                            ddlCity.DataValueField = "ID";
                            ddlCity.DataBind();
                            ddlCity.Items.Insert(0, "Select City");
                            ddlCity.SelectedValue = ds.Tables[0].Rows[0]["City"].ToString();
                        }
                        else
                        {
                            ddlCity.Items.Clear();
                            ddlCity.Items.Insert(0, "Select City");
                        }

                    }


                }
                //DataSet ds = objBs.GetCompanyDetails();
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    txtcompanyID.Text = ds.Tables[0].Rows[0]["ComapanyID"].ToString();
                //    txtcustomername.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                //    txtmobileno.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                //    txtphoneno.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                //    txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                //    txtarea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                //    txtpincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString();
                //    txtemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                //    txtcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                //    txttin.Text = ds.Tables[0].Rows[0]["tin"].ToString();
                //    txtcompanyID.Enabled = false;
                //    txtcustomername.Enabled = false;
                //    txtmobileno.Enabled = false;
                //    txtphoneno.Enabled = false;
                //    txtaddress.Enabled = false;
                //    txtarea.Enabled = false;
                //    txtpincode.Enabled = false;
                //    txtemail.Enabled = false;
                //    txtcity.Enabled = false;
                //    txttin.Enabled = false;
                //}

              
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }
        protected void ddlCountry_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, "Select State");
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, "Select City");

            if (ddlCountry.SelectedValue == "" || ddlCountry.SelectedValue == "0" || ddlCountry.SelectedValue == "Select Country")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Country.')", true);
                ddlCountry.Focus();

                ddlState.Items.Clear();
                ddlState.Items.Insert(0, "Select State");
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, "Select City");

                return;
            }

            DataSet dsDistrict = objBs.GetAllCountryState(Convert.ToInt32(ddlCountry.SelectedValue));
            if (dsDistrict.Tables[0].Rows.Count > 0)
            {

                ddlState.DataSource = dsDistrict.Tables[0];
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "ID";
                ddlState.DataBind();
                ddlState.Items.Insert(0, "Select State");
            }
            else
            {
                ddlState.Items.Clear();
                ddlState.Items.Insert(0, "Select State");
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, "Select City");
            }

        }

        protected void ddlState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue == "" || ddlState.SelectedValue == "0" || ddlState.SelectedValue == "Select State")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select State.')", true);
                ddlState.Focus();

                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, "Select City");

                return;
            }


            DataSet dsCity = objBs.GetAllStateCity(Convert.ToInt32(ddlState.SelectedValue));
            if (dsCity.Tables[0].Rows.Count > 0)
            {
                ddlCity.DataSource = dsCity.Tables[0];
                ddlCity.DataTextField = "City";
                ddlCity.DataValueField = "ID";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, "Select City");
            }
            else
            {
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, "Select City");
            }
        }


        protected void Add_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");

            if (btnadd.Text == "Save")
            {
                if (txtcompanycode.Text != "")
                {
                    DataSet ds = objBs.EmailID_CompanyDetails(txtcompanycode.Text);
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //Response.Write("email id already exists");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Company Code Already Exists.Thank You!!!');", true);
                        return;
                    }


                    else
                    {
                        int iStatus = objBs.CompanyDetails(txtcompanyname.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, ddlCity.SelectedValue, txtpincode.Text, txtemail.Text, txttin.Text,txtcst.Text,txtpan.Text,txtcompanycode.Text,ddlCountry.SelectedValue,ddlState.SelectedValue);
                    }

                }
            }

            else
            {
                int iStatus = objBs.EditCompanyDetails(txtcompanyname.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, ddlCity.SelectedValue, txtpincode.Text, txtemail.Text, Convert.ToInt32(txtcompanyID.Text), txttin.Text, txtcst.Text, txtpan.Text, txtcompanycode.Text, ddlCountry.SelectedValue, ddlState.SelectedValue);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Sucessfully Updated ');", true);
            }

            Response.Redirect("CompanyGrid.aspx");
            }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            btnadd.Text = "Update";
            txtcompanyID.Enabled = true;
           // txtcustomername.Enabled = true;
            txtmobileno.Enabled = true;
            txtphoneno.Enabled = true;
            txtaddress.Enabled = true;
            txtarea.Enabled = true;
            txtpincode.Enabled = true;
            txtemail.Enabled = true;
           // txtcity.Enabled = true;
            txttin.Enabled = true;
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyGrid.aspx");
        }
           
    }
}