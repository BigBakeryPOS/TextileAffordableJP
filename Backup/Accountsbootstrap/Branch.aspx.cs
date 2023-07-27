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

namespace Billing.Accountsbootstrap
{
    public partial class Branch : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divcode.Visible = false;

                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();

                int iCusID = Convert.ToInt32(Request.QueryString.Get("iCusID"));
                if (Convert.ToString(iCusID) != "" || iCusID != null)
                {
                    DataSet ds1 = objBs.getselectBranch(iCusID);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        btnadd.Text = "Update";
                        txtBranchid.Text = ds1.Tables[0].Rows[0]["Branchid"].ToString();
                        txtBranchCode.Enabled = false;
                        txtBranchname.Text = ds1.Tables[0].Rows[0]["BranchName"].ToString();
                        txtBranchCode.Text = ds1.Tables[0].Rows[0]["BranchCode"].ToString();
                        txtBranchaddress.Text = ds1.Tables[0].Rows[0]["address"].ToString();
                        txtphonenumber.Text = ds1.Tables[0].Rows[0]["phone"].ToString();
                        string active = ds1.Tables[0].Rows[0]["isactive"].ToString();
                        if (active == "Yes")

                            ddlIsActive.SelectedValue = "1";
                        else
                            ddlIsActive.SelectedValue = "2";
                        
                    }

                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");

            if (btnadd.Text == "Save")
            {
                //DataSet ds = objBs.selectemailid(txtemail.Text, txtmobileno.Text);
                //if (ds.Tables[0].Rows.Count != 0)
                //{
                //    //Response.Write("email id already exists");
                //    lblerror.Text = "Email or Mobile Number id already exists";

                //}
                //else
                //{
                DataSet dsCategory = objBs.Branchsrchgrid(txtBranchname.Text, txtBranchCode.Text, Convert.ToInt32(0), "Save");
                if (dsCategory.Tables[0].Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Branch Name or Code already Exists. please enter a new one')", true);
                    return;
                }
                else
                {
                    int iStatus = objBs.insertBranch(Convert.ToInt32(lblUserID.Text), txtBranchname.Text, txtBranchCode.Text,txtBranchaddress.Text, txtphonenumber.Text, ddlIsActive.SelectedItem.Text);

                    //CreateTableforaudit(sender, e);
                  //  CreateTableforsales(sender, e);
                    int isucess = 0;
                    isucess = objBs.newtablecreation(txtBranchCode.Text, txtBranchname.Text, lblUserID.Text);
                    Response.Redirect("../Accountsbootstrap/viewbranch.aspx");
                }

            }
            else
            {
                DataSet dsCategory = objBs.Branchsrchgrid(txtBranchname.Text, txtBranchCode.Text, Convert.ToInt32(txtBranchid.Text), "Upd");
                if (dsCategory.Tables[0].Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Branch Name or Code already Exists. please enter a new one')", true);
                    return;
                }
                else
                {
                    int iStatus = objBs.updateBranch(Convert.ToInt32(txtBranchid.Text), txtBranchname.Text, txtBranchCode.Text,txtBranchaddress.Text,txtphonenumber.Text,ddlIsActive.SelectedItem.Text);
                    Response.Redirect("../Accountsbootstrap/viewbranch.aspx");
                }
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewbranch.aspx");
        }

        //protected void Update_Click(object sender, EventArgs e)
        //{

        //   int iStatus = objBs.updatecustomer(txtcuscode.Text, txtcustomername.Text,txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text);
        //   Response.Redirect("../Accountsbootstrap/customermaster.aspx");
        //}

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewbranch.aspx");
        }



        //protected void CreateTableforaudit(object sender, EventArgs e)
        //{
        //    string query = "IF OBJECT_ID('dbo.tblAuditMaster_" +txtBranchCode.Text+ "', 'U') IS NULL ";
        //    query += "BEGIN ";
        //    query += "CREATE TABLE [dbo].[tblAuditMaster_" + txtBranchCode.Text + "](";
        //    query += "[AuditId] [int] IDENTITY(1,1) NOT NULL  CONSTRAINT [PK_tblAuditMaster_"+txtBranchCode.Text+"] PRIMARY KEY CLUSTERED ,";
        //    query += "[Type] [nvarchar](50) NULL,";
        //    query += "[Screen] [nvarchar](50) NULL,";
        //    query += "[BillNo] [int] NULL,";
        //    query += "[Amount] [money] NULL,";
        //    query += "[Bill_Date] [date] NULL,";
        //    query += "[Audit_Date] [datetime] NULL,";
        //    query += "[UserId] [nvarchar](50) NULL,";
        //    query += "[Ledgername] [nvarchar](50) NULL,[Description] [nvarchar](50) NULL ";
        //    query += ")";
        //    query += " END";
        //    string constr = ConfigurationManager.ConnectionStrings["Accounts"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //}
    }
}