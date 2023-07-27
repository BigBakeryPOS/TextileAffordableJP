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
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class Cuttingprocess : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            iid = Request.QueryString.Get("CuttingID");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                if (radbtn.SelectedValue == "1")
                {
                    multiple.Visible = false;
                    single.Visible = true;
                }
                else
                {
                    multiple.Visible = true;
                    single.Visible = false;
                }

                //sTableName = Session["User"].ToString();
               
                divcode.Visible = false;
                DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddlSupplier.DataSource = dst.Tables[0];
                        ddlSupplier.DataTextField = "LedgerName";
                        ddlSupplier.DataValueField = "LedgerID";
                        ddlSupplier.DataBind();
                        ddlSupplier.Items.Insert(0, "Select Party Name");

                        chkSupplier.DataSource = dst.Tables[0];
                        chkSupplier.DataTextField = "LedgerName";
                        chkSupplier.DataValueField = "LedgerID";
                        chkSupplier.DataBind();
                       // ddlSupplier.Items.Insert(0, "Select Party Name");
                    }
                }

                DataSet dsDNo = objBs.GetDNo();
                if (dsDNo != null)
                {
                    if (dsDNo.Tables[0].Rows.Count > 0)
                    {
                        ddlDNo.DataSource = dsDNo.Tables[0];
                        ddlDNo.DataTextField = "Dno";
                        ddlDNo.DataValueField = "ProcessID";
                        ddlDNo.DataBind();
                        ddlDNo.Items.Insert(0, "Select Design No");
                    }
                }

                DataSet dsFit = objBs.GetFit();
                if (dsFit != null)
                {
                    if (dsFit.Tables[0].Rows.Count > 0)
                    {
                        ddlFit.DataSource = dsFit.Tables[0];
                        ddlFit.DataTextField = "Fit";
                        ddlFit.DataValueField = "FitID";
                        ddlFit.DataBind();
                        ddlFit.Items.Insert(0, "Select Fit");
                    }
                }

                string date = DateTime.Now.ToString("dd/MM/yyyy");

               // txtdate.Text = date;

                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();
               
             
                if (iid != null)
                {
                    DataSet ds1 = objBs.getCuttingProcess(Convert.ToInt32(iid));
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            DataSet dsDNo1 = objBs.allGetDNo();
                            if (dsDNo1 != null)
                            {
                                if (dsDNo1.Tables[0].Rows.Count > 0)
                                {
                                    ddlDNo.DataSource = dsDNo1.Tables[0];
                                    ddlDNo.DataTextField = "Dno";
                                    ddlDNo.DataValueField = "ProcessID";
                                    ddlDNo.DataBind();
                                    ddlDNo.Items.Insert(0, "Select Design No");
                                }
                            }

                            btnadd.Text = "Update";
                            double totmeter = Convert.ToDouble(ds1.Tables[0].Rows[0]["Req_Meter"]) + Convert.ToDouble(ds1.Tables[0].Rows[0]["met"]);
                            txtID.Text = ds1.Tables[0].Rows[0]["CuttingID"].ToString();
                            TextBox3.Text = ds1.Tables[0].Rows[0]["CuttingID"].ToString();
                            txtreq_meter.Text = ds1.Tables[0].Rows[0]["Req_Meter"].ToString();
                            ddlDNo.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["DNo"]).ToString();
                            txtLotNo.Text = ds1.Tables[0].Rows[0]["LotNo"].ToString();
                            txtMeter.Text = totmeter.ToString();
                            txtRate.Text = ds1.Tables[0].Rows[0]["Rate"].ToString();
                            txtColor.Text = ds1.Tables[0].Rows[0]["Color"].ToString();
                            radbtn.SelectedValue = ds1.Tables[0].Rows[0]["Type"].ToString();
                            if (radbtn.SelectedValue == "1")
                            {
                                ddlSupplier.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["PartyName"]).ToString();
                                single.Visible = true;
                                multiple.Visible = false;
                            }
                            else
                            {
                                single.Visible = false;
                                multiple.Visible = true;
                                string str = ds1.Tables[0].Rows[0]["PartyName"].ToString();
                                string[] strList = str.Split(',');


                                foreach (string s in strList)
                                {
                                    foreach (ListItem item in chkSupplier.Items)
                                    {
                                        if (item.Value == s)
                                        {
                                            item.Selected = true;
                                            break;
                                        }

                                    }

                                }

                            }
                            txtWidth.Text = ds1.Tables[0].Rows[0]["WidthID"].ToString();
                            ddlFit.SelectedValue = ds1.Tables[0].Rows[0]["Fit"].ToString();
                            txtdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");
                        }
                    }
                }
                else
                {

                    DataSet ds = objBs.CuttingID();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["CuttingID"].ToString() == "")
                            TextBox3.Text = "1";
                        else
                            TextBox3.Text = ds.Tables[0].Rows[0]["CuttingID"].ToString();

                        btnadd.Text = "Save";
                    }

                }
            }
        }

      

        protected void Add_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");

            DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
           
            if (btnadd.Text == "Save")

            {
                int iStatus = 0;
                if (radbtn.SelectedValue == "1")
                {
                    double meter = Convert.ToDouble(txtMeter.Text);
                    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                    double number = meter - reqmeter;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                        return;
                    }

                //    string condno = getCond();
                 //   string condname = getCondname();

                  //  return;


                    iStatus = objBs.insertCuttingprocess(Convert.ToInt32(txtLotNo.Text), ddlSupplier.SelectedValue, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), txtreq_meter.Text, ddlSupplier.SelectedItem.Text, radbtn.SelectedValue, deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
                }
                else
                {

                    
                    double meter = Convert.ToDouble(txtMeter.Text);
                    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                    double number = meter - reqmeter;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                        return;
                    }

                    string condno = getCond();
                    string condname = getCondname();

                  //  return;


                    iStatus = objBs.insertCuttingprocess(Convert.ToInt32(txtLotNo.Text), condno, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), txtreq_meter.Text, condname, radbtn.SelectedValue, deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");

                }
            }
            else
            {
                int iStatus = 0;
                if (radbtn.SelectedValue == "1")
                {
                    double meter = Convert.ToDouble(txtMeter.Text);
                    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                    double number = meter - reqmeter;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                        return;
                    }


                    iStatus = objBs.UpdateCuttingprocess(Convert.ToInt32(txtLotNo.Text), ddlSupplier.SelectedValue, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), Convert.ToInt32(iid), txtreq_meter.Text, ddlSupplier.SelectedItem.Text, radbtn.SelectedValue, deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
                }
                else
                {
                    double meter = Convert.ToDouble(txtMeter.Text);
                    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                    double number = meter - reqmeter;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                        return;
                    }
                    string condno = getCond();
                    string condname = getCondname();

                    iStatus = objBs.UpdateCuttingprocess(Convert.ToInt32(txtLotNo.Text), condno, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), Convert.ToInt32(iid), txtreq_meter.Text,condname,radbtn.SelectedValue,deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
                }
            }
        }

        protected string getCond()
        {
            string cond = "";

            foreach (ListItem listItem in chkSupplier.Items)
            {
                if (listItem.Text != "All")
                {
                    if (listItem.Selected)
                    {
                        cond += listItem.Value + ",";
                    }
                }
            }
            cond = cond.TrimEnd(',');
         //   cond = cond.Replace(",", ",");
            return cond;
        }

        protected string getCondname()
        {
            string cond = "";

            foreach (ListItem listItem in chkSupplier.Items)
            {
                if (listItem.Text != "All")
                {
                    if (listItem.Selected)
                    {
                        cond += listItem.Text + ",";
                    }
                }
            }
            // cond = cond.TrimEnd(',');
            //   cond = cond.Replace(",", ",");
            cond = cond.TrimEnd(',');
            return cond;
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
        }

        protected void ddlDNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_Cutting = objBs.SelectDesign_CuttingProcess(ddlDNo.SelectedValue);
            int Width_Id = Convert.ToInt32(ds_Cutting.Tables[0].Rows[0]["WidthID"].ToString());

            DataSet ds_Width = objBs.editwidth(Width_Id);
            txtWidth.Text = ds_Width.Tables[0].Rows[0]["Width"].ToString();
            txtRate.Text = ds_Cutting.Tables[0].Rows[0]["Rate"].ToString();
            txtMeter.Text = ds_Cutting.Tables[0].Rows[0]["AvaliableMeter"].ToString();

            txtreq_meter.Focus();
        }

        protected void radchecked(object sender, EventArgs e)
        {
            if (radbtn.SelectedValue == "1")
            {
                single.Visible = true;
                multiple.Visible = false;
            }
            else
            {
                multiple.Visible = true;
                single.Visible = false;

            }
        }
    }
}