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
    public partial class Cheque : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
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

                //sTableName = Session["User"].ToString();

                divcode.Visible = false;
                DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 4, sTableName);
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddlBank.DataSource = dst.Tables[0];
                        ddlBank.DataTextField = "LedgerName";
                        ddlBank.DataValueField = "LedgerID";
                        ddlBank.DataBind();
                        ddlBank.Items.Insert(0, "Select Bank");
                    }
                }

                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();

                string iid = Request.QueryString.Get("iid");
                if (iid != null)
                {
                    DataSet ds1 = objBs.getselectCheque(Convert.ToInt32(iid));
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Text = "Update";
                            txtID.Text = ds1.Tables[0].Rows[0]["id"].ToString();
                            TextBox3.Text = ds1.Tables[0].Rows[0]["id"].ToString();
                            TextBox1.Text = ds1.Tables[0].Rows[0]["fromcheque"].ToString();
                            TextBox2.Text = ds1.Tables[0].Rows[0]["tocheque"].ToString();

                            ddlBank.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["bankid"]).ToString();
                        }


                        
                    }

                }
                else
                {

                    DataSet ds = objBs.Chequeno("tblCheque");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["ID"].ToString() == "")
                            TextBox3.Text = "1";
                        else
                            TextBox3.Text = ds.Tables[0].Rows[0]["ID"].ToString();

                        btnadd.Text = "Save";
                    }

                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");

            if (TextBox1.Text == TextBox2.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('From Cheque No and To Cheque No not be same.!!! ')", true);
                return;
            }

            if (Convert.ToInt32(TextBox1.Text) > Convert.ToInt32(TextBox2.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                return;
            }

            if (objBs.ChequeLeafUsed( Convert.ToInt32(TextBox1.Text),Convert.ToInt32(TextBox2.Text),ddlBank.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given Cheque No already entered for this bank');", true);
                return;
            }
            if (btnadd.Text == "Save")
            {

                int iStatus = objBs.insertCheque(Convert.ToInt32(lblUserID.Text), TextBox3.Text, Convert.ToInt32(TextBox1.Text), Convert.ToInt32(TextBox2.Text), Convert.ToInt32(ddlBank.SelectedValue), ddlBank.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text);
                Response.Redirect("../Accountsbootstrap/viewcheques.aspx");
              

            }
            else
            {
                int iStatus = objBs.updatecheque(Convert.ToInt32(txtID.Text), TextBox3.Text, Convert.ToInt32(TextBox1.Text), Convert.ToInt32(TextBox2.Text), Convert.ToInt32(ddlBank.SelectedValue), ddlBank.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text);
                Response.Redirect("../Accountsbootstrap/viewcheques.aspx");
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewcheques.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewcheques.aspx");
        }
    }
}