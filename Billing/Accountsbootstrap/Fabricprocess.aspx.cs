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
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class Fabricprocess : System.Web.UI.Page
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
            if (!IsPostBack)
            {

                imgDemo1.ImageUrl = "";
                divcode.Visible = false;

                DataSet dst = objBs.hrmgridview();
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddlEmployee.DataSource = dst.Tables[0];
                        ddlEmployee.DataTextField = "Name";
                        ddlEmployee.DataValueField = "Employee_Id";
                        ddlEmployee.DataBind();
                        ddlEmployee.Items.Insert(0, "Select Employee Name");
                    }
                }

                DataSet dsSupplier = objBs.getnewsupplierforfab();
                if (dsSupplier != null)
                {
                    if (dsSupplier.Tables[0].Rows.Count > 0)
                    {
                        ddlSupplier.DataSource = dsSupplier.Tables[0];
                        ddlSupplier.DataTextField = "LedgerName";
                        ddlSupplier.DataValueField = "LedgerID";
                        ddlSupplier.DataBind();
                        ddlSupplier.Items.Insert(0, "Select Supplier Name");

                    }
                }

                DataSet dswidth = objBs.GetWidth();
                if (dswidth != null)
                {
                    if (dswidth.Tables[0].Rows.Count > 0)
                    {
                        ddlWidth.DataSource = dswidth.Tables[0];
                        ddlWidth.DataTextField = "Width";
                        ddlWidth.DataValueField = "WidthID";
                        ddlWidth.DataBind();
                        ddlWidth.Items.Insert(0, "Select Width");
                    }
                }

                string iid = Request.QueryString.Get("iid");
                if (iid != null)
                {
                    DataSet ds1 = objBs.getFabricProcess(Convert.ToInt32(iid));
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Text = "Update";
                            txtID.Text = ds1.Tables[0].Rows[0]["fabid"].ToString();
                            //TextBox3.Text = ds1.Tables[0].Rows[0]["fabid"].ToString();

                            ddlWidth.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["Width"]).ToString();
                            txtDNo.Text = ds1.Tables[0].Rows[0]["DesignNo"].ToString();
                            txtcolour.Text = ds1.Tables[0].Rows[0]["Color"].ToString();
                            txtpiece.Text = ds1.Tables[0].Rows[0]["Piece"].ToString();
                            txtMeter.Text = ds1.Tables[0].Rows[0]["Meter"].ToString();
                            txtAvailableMeter.Text = ds1.Tables[0].Rows[0]["AvaliableMeter"].ToString();
                            txtRate.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Rate"]).ToString("0.00");
                            txtinvoiceNo.Text = ds1.Tables[0].Rows[0]["Refno"].ToString();
                            ddlEmployee.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["Checkedsign"]).ToString();
                            txtdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["InvDate"]).ToString("dd/MM/yyyy");
                            ddlSupplier.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["Supplierid"]).ToString();//Supplier name
                        }
                    }
                }
                else
                {
                    Response.Redirect("../Accountsbootstrap/Fabric_Grid.aspx");                  
                }

                # region block

                //DataSet dst = objBs.hrmgridview();
                //if (dst != null)
                //{
                //    if (dst.Tables[0].Rows.Count > 0)
                //    {
                //        ddlSupplier.DataSource = dst.Tables[0];
                //        ddlSupplier.DataTextField = "Name";
                //        ddlSupplier.DataValueField = "Employee_Id";
                //        ddlSupplier.DataBind();
                //        ddlSupplier.Items.Insert(0, "Select Employee Name");
                //    }
                //}
                //DataSet dsSupplier = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 2);
                //if (dsSupplier != null)
                //{
                //    if (dsSupplier.Tables[0].Rows.Count > 0)
                //    {
                //        ddlVendor.DataSource = dsSupplier.Tables[0];
                //        ddlVendor.DataTextField = "LedgerName";
                //        ddlVendor.DataValueField = "LedgerID";
                //        ddlVendor.DataBind();
                //        ddlVendor.Items.Insert(0, "Select Supplier Name");

                //    }
                //}
                //DataSet dswidth = objBs.GetWidth();
                //if (dswidth != null)
                //{
                //    if (dswidth.Tables[0].Rows.Count > 0)
                //    {
                //        ddlWidth.DataSource = dswidth.Tables[0];
                //        ddlWidth.DataTextField = "Width";
                //        ddlWidth.DataValueField = "WidthID";
                //        ddlWidth.DataBind();
                //        ddlWidth.Items.Insert(0, "Select Width");
                //    }
                //}

                //string date = DateTime.Now.ToString("dd/MM/yyyy");

                //txtdate.Text = date;

                //lblUser.Text = Session["UserName"].ToString();
                //lblUserID.Text = Session["UserID"].ToString();

                //string iid = Request.QueryString.Get("iid");
                //if (iid != null)
                //{
                //    DataSet ds1 = objBs.getFabricProcess(Convert.ToInt32(iid));
                //    if (ds1 != null)
                //    {
                //        if (ds1.Tables[0].Rows.Count > 0)
                //        {
                //            btnadd.Text = "Update";
                //            txtID.Text = ds1.Tables[0].Rows[0]["fabid"].ToString();
                //            TextBox3.Text = ds1.Tables[0].Rows[0]["fabid"].ToString();
                            
                //            ddlWidth.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["Width"]).ToString();
                //            txtDNo.Text = ds1.Tables[0].Rows[0]["Refno"].ToString();
                //            txtMeter.Text = ds1.Tables[0].Rows[0]["TotalMeter"].ToString();
                //            //txtRate.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Rate"]).ToString("0.00");
                //            //txtRMCode.Text = ds1.Tables[0].Rows[0]["RMCode"].ToString();
                //            ddlSupplier.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["SupplierId"]).ToString();
                //            //imgDemo1.ImageUrl = ds1.Tables[0].Rows[0]["ClothImage"].ToString();
                //            txtChallenNo.Text = ds1.Tables[0].Rows[0]["Delivery_Challan"].ToString();
                //            txtdate.Text = ds1.Tables[0].Rows[0]["InvDate"].ToString();
                //            ddlVendor.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["SupplierId"]).ToString();
                //        }
                //    }
                //}
                //else
                //{

                //    DataSet ds = objBs.processno("tblFabricprocess");
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        if (ds.Tables[0].Rows[0]["ProcessID"].ToString() == "")
                //            TextBox3.Text = "1";
                //        else
                //            TextBox3.Text = ds.Tables[0].Rows[0]["ProcessID"].ToString();

                //        btnadd.Text = "Save";
                //    }

                //}

                #endregion
            }
        }

        protected void txtDNo_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = objBs.Checkdesignno(txtDNo.Text);
            if ((ds.Tables[0].Rows.Count) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This Design No Already exist!');", true);
                btnadd.Visible = false;
                return;
            }
            else
            {
                lblerror.Text = "";
                btnadd.Visible = true;
                txtChallenNo.Focus();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            FileUpload2.SaveAs(MapPath("~/Files/" + FileUpload2.FileName));
            imgDemo1.ImageUrl = imgDemo1.ImageUrl = "~/Files/" + FileUpload2.FileName;
            //lbl1.Text = FileUpload2.FileName;
            lbl1.Text = imgDemo1.ImageUrl;
            UpdatePanel2.Update();
        }

        protected void del1(object sender, EventArgs e)
        {
            imgDemo1.ImageUrl = "";
            lbl1.Text = "";
            txt1.Text = "";
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");
            string iid = Request.QueryString.Get("iid");

            int iStatus = objBs.UpdateTransFabricprocess(Convert.ToInt32(iid), txtDNo.Text, txtcolour.Text, txtpiece.Text, txtMeter.Text, txtRate.Text,
               Convert.ToInt32(ddlWidth.SelectedValue), txtAvailableMeter.Text);
            Response.Redirect("../Accountsbootstrap/Fabric_Grid.aspx");

            System.Threading.Thread.Sleep(3000);
            
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewprocess.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewprocess.aspx");
        }
    }
}