using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class EditFirstGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            string date = DateTime.Now.ToString("dd/MM/yyyy");

          
           


            if (!IsPostBack)
            {
                divcode.Visible = false;
                DataSet dsContact = objBs.getnewsupplierforfab();
                if (dsContact.Tables[0].Rows.Count > 0)
                {
                    drpsupplier.DataSource = dsContact.Tables[0];
                    drpsupplier.DataTextField = "Ledgername";
                    drpsupplier.DataValueField = "LedgerID";
                    drpsupplier.DataBind();
                    drpsupplier.Items.Insert(0, "Select Supplier");
                }

                string iCusID = Request.QueryString.Get("iid");
                    if (iCusID != "" || iCusID != null)
                    {
                        DataSet ds1 = objBs.editfirstcodeprocess(iCusID);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Text = "Update";
                            txttransid.Text = ds1.Tables[0].Rows[0]["FirstStageid"].ToString();
                            drpitem.SelectedValue = ds1.Tables[0].Rows[0]["item"].ToString();
                            drpsupplier.SelectedValue = ds1.Tables[0].Rows[0]["Supplier"].ToString();
                            txtdesign.Text = ds1.Tables[0].Rows[0]["Designno"].ToString();
                            txtcolor.Text = ds1.Tables[0].Rows[0]["Color"].ToString();
                            drporder.SelectedValue = ds1.Tables[0].Rows[0]["Ordered"].ToString();
                            txtmtr.Text = ds1.Tables[0].Rows[0]["MtrRate"].ToString();
                            txtmrp.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["MRP"]).ToString("N");
                            txtwsp.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["WSP"]).ToString("N");
                            sample.Text = ds1.Tables[0].Rows[0]["SampleCode"].ToString();

                            //string[] ret = sample.Text.Split('.');
                            //string samm = ret[0] + txtmrp.Text + ret[2];
                        }

                   }
                

            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            {
                if (drpsupplier.SelectedValue == "Select Supplier")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Supplier.Thank You!!!')", true);
                    return;
                }
                if (txtwsp.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter WSP.Thank You!!!')", true);
                    return;
                }

                if (txtmrp.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter MRP.Thank You!!!')", true);
                    return;
                }

                int d = (int)Math.Round( Convert.ToDouble(txtmrp.Text), 0);

                string[] ret = sample.Text.Split('.');
                string samm = ret[0]+"."+d+ ".00";
                string iCusID = Request.QueryString.Get("iid");
                int isus = objBs.Updatefirststage(drpsupplier.SelectedValue, txtdesign.Text, txtcolor.Text, drporder.SelectedValue, txtmtr.Text, txtwsp.Text, d.ToString(), iCusID, samm);
                Response.Redirect("FirstStageGrid.aspx");
                //if (ddlCDType.SelectedValue == "Credit Note")
                //{
                //    string Credite = txtOBalance.Text;

                //    int iStatus = objBs.updatecustomer(Convert.ToInt32(iCusID), txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Convert.ToInt32("8"), ddlIsActive.SelectedItem.Text, Convert.ToDouble(Credite), Convert.ToDouble("0"), ddlCDType.SelectedValue, Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Agent", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), "0");
                //    Response.Redirect("../Accountsbootstrap/Viewagent.aspx");
                //}
                //else
                //{
                //    string Debit = txtOBalance.Text;
                //    int iStatus = objBs.updatecustomer(Convert.ToInt32(iCusID), txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Convert.ToInt32("8"), ddlIsActive.SelectedItem.Text, Convert.ToDouble("0"), Convert.ToDouble(Debit), ddlCDType.SelectedValue, Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Agent", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), "0");
                //    Response.Redirect("../Accountsbootstrap/Viewagent.aspx");
                //}
            }
        }
     
        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/FirstStageGrid.aspx");
        }

      
  
    }
}