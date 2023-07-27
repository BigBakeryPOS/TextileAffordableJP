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
    public partial class Cutprocess : System.Web.UI.Page
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

                
                //sTableName = Session["User"].ToString();

                divcode.Visible = false;
                DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                if (dst != null)
                {
                    //if (dst.Tables[0].Rows.Count > 0)
                    //{
                    //    ddlSupplier.DataSource = dst.Tables[0];
                    //    ddlSupplier.DataTextField = "LedgerName";
                    //    ddlSupplier.DataValueField = "LedgerID";
                    //    ddlSupplier.DataBind();
                    //    ddlSupplier.Items.Insert(0, "Select Party Name");

                    //    //chkSupplier.DataSource = dst.Tables[0];
                    //    //chkSupplier.DataTextField = "LedgerName";
                    //    //chkSupplier.DataValueField = "LedgerID";
                    //    //chkSupplier.DataBind();
                    //    // ddlSupplier.Items.Insert(0, "Select Party Name");
                    //}
                }

                //DataSet dsDNo = objBs.GetDNo();
                //if (dsDNo != null)
                //{
                //    if (dsDNo.Tables[0].Rows.Count > 0)
                //    {
                //        ddlDNo.DataSource = dsDNo.Tables[0];
                //        ddlDNo.DataTextField = "Dno";
                //        ddlDNo.DataValueField = "ProcessID";
                //        ddlDNo.DataBind();
                //        ddlDNo.Items.Insert(0, "Select Design");
                //    }
                //}

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

                DataSet dsize = objBs.Getsizetype();
                if (dsize != null)
                {
                    if (dsize.Tables[0].Rows.Count > 0)
                    {
                        chkSizes.DataSource = dsize.Tables[0];
                        chkSizes.DataTextField = "Size";
                        chkSizes.DataValueField = "Sizeid";
                        chkSizes.DataBind();
                        //  chkSizes.Items.Insert(0, "Select Customer");
                        // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();
                    }
                }

                DataSet dswidth = objBs.GetWidth();
                if (dswidth != null)
                {
                    if (dswidth.Tables[0].Rows.Count > 0)
                    {
                        drpwidth.DataSource = dswidth.Tables[0];
                        drpwidth.DataTextField = "Width";
                        drpwidth.DataValueField = "WidthID";
                        drpwidth.DataBind();
                       // drpwidth.Items.Insert(0, "Select Width");
                    }
                }


                DataSet dsrefno = objBs.getnewsupplierforcut(drpwidth.SelectedValue);
                if (dsrefno != null)
                {
                    if (dsrefno.Tables[0].Rows.Count > 0)
                    {
                        chkinvno.DataSource = dsrefno.Tables[0];
                        chkinvno.DataTextField = "refno";
                        chkinvno.DataValueField = "transid";
                        chkinvno.DataBind();
                      //  drpwidth.Items.Insert(0, "Select Width");
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
                                    ddlDNo.Items.Insert(0, "Select Design");
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
                               // single.Visible = true;
                               // multiple.Visible = false;
                            }
                            else
                            {
                              //  single.Visible = false;
                              //  multiple.Visible = true;
                                string str = ds1.Tables[0].Rows[0]["PartyName"].ToString();
                                string[] strList = str.Split(',');


                                //foreach (string s in strList)
                                //{
                                //    foreach (ListItem item in chkSupplier.Items)
                                //    {
                                //        if (item.Value == s)
                                //        {
                                //            item.Selected = true;
                                //            break;
                                //        }

                                //    }

                                //}

                            }
                            txtWidth.Text = ds1.Tables[0].Rows[0]["WidthID"].ToString();
                            ddlFit.SelectedValue = ds1.Tables[0].Rows[0]["Fit"].ToString();
                            txtdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");
                        }
                    }
                }
                else
                {
                    txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //DataSet ds = objBs.CuttingID();
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    if (ds.Tables[0].Rows[0]["CuttingID"].ToString() == "")
                    //        TextBox3.Text = "1";
                    //    else
                    //        TextBox3.Text = ds.Tables[0].Rows[0]["CuttingID"].ToString();
                    DataSet dss = objBs.getmaaxBillnoforcut("");
                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        txtLotNo.Text = dss.Tables[0].Rows[0]["billId"].ToString();
                    }
                       btnadd.Text = "Save";
                       btnadd.Enabled = false;
                    //  //  FirstGridViewRow();
                    //}

                }
            }
        }
        protected void ckhsize_index(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            if (chkSizes.SelectedIndex >= 0)
            {
                gvcustomerorder.Columns[7].Visible = false;
                gvcustomerorder.Columns[8].Visible = false;

                gvcustomerorder.Columns[9].Visible = false;
                gvcustomerorder.Columns[10].Visible = false;

                gvcustomerorder.Columns[11].Visible = false; gvcustomerorder.Columns[12].Visible = false;

                gvcustomerorder.Columns[13].Visible = false; gvcustomerorder.Columns[14].Visible = false;

                gvcustomerorder.Columns[15].Visible = false; gvcustomerorder.Columns[16].Visible = false;

                gvcustomerorder.Columns[17].Visible = false; gvcustomerorder.Columns[18].Visible = false;


                gvcustomerorder.Columns[19].Visible = false;
                gvcustomerorder.Columns[20].Visible = false;

                gvcustomerorder.Columns[21].Visible = false;
                gvcustomerorder.Columns[22].Visible = false;

                gvcustomerorder.Columns[23].Visible = false; gvcustomerorder.Columns[24].Visible = false;

                gvcustomerorder.Columns[25].Visible = false; gvcustomerorder.Columns[26].Visible = false;

                gvcustomerorder.Columns[27].Visible = false; gvcustomerorder.Columns[28].Visible = false;

                gvcustomerorder.Columns[29].Visible = false; gvcustomerorder.Columns[30].Visible = false;

                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (ListItem item in chkSizes.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {

                        {
                            if (item.Value == "1")
                            {
                                gvcustomerorder.Columns[7].Visible = true;
                            }
                            if (item.Value == "2")
                            {
                                gvcustomerorder.Columns[8].Visible = true;
                            }
                            if (item.Value == "3")
                            {
                                gvcustomerorder.Columns[9].Visible = true;
                            }
                            if (item.Value == "4")
                            {
                                gvcustomerorder.Columns[10].Visible = true;
                            }
                            if (item.Value == "5")
                            {
                                gvcustomerorder.Columns[11].Visible = true;
                            }
                            if (item.Value == "6")
                            {
                                gvcustomerorder.Columns[12].Visible = true;
                            }
                            if (item.Value == "7")
                            {
                                gvcustomerorder.Columns[13].Visible = true;
                            }
                            if (item.Value == "8")
                            {
                                gvcustomerorder.Columns[14].Visible = true;
                            }
                            if (item.Value == "9")
                            {
                                gvcustomerorder.Columns[15].Visible = true;
                            }
                            if (item.Value == "10")
                            {
                                gvcustomerorder.Columns[16].Visible = true;
                            }
                            if (item.Value == "11")
                            {
                                gvcustomerorder.Columns[17].Visible = true;
                            }
                            if (item.Value == "12")
                            {
                                gvcustomerorder.Columns[18].Visible = true;
                            }


                            lop++;

                        }
                    }
                }
                //gvcustomerorder.DataSource = dssmer;
                //gvcustomerorder.DataBind();
            }
            else
            {
                gvcustomerorder.Columns[7].Visible = false;
                gvcustomerorder.Columns[8].Visible = false;

                gvcustomerorder.Columns[9].Visible = false;
                gvcustomerorder.Columns[10].Visible = false;

                gvcustomerorder.Columns[11].Visible = false; gvcustomerorder.Columns[12].Visible = false;

                gvcustomerorder.Columns[13].Visible = false; gvcustomerorder.Columns[14].Visible = false;

                gvcustomerorder.Columns[15].Visible = false; gvcustomerorder.Columns[16].Visible = false;

                gvcustomerorder.Columns[17].Visible = false; gvcustomerorder.Columns[18].Visible = false;
            }
        }

        protected void call_Click(object sender, EventArgs e)
        {
            DataSet dcalculate = new DataSet();
           
            btnadd.Enabled = false;
            string width = string.Empty;
            if (btnadd.Text == "Save")
            {
                if (ddlFit.SelectedValue == "Select fit")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit. Thank you');", true);
                    return;
                }
                if (CheckBoxList2.SelectedIndex >= 0)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Design Number. Thank you');", true);
                    return;

                }

                if (chkinvno.SelectedIndex >= 0)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Invoice Number. Thank you');", true);
                    return;

                }




                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    double totgnd = 0;
                    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                    Label lblid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblid");
                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
                    DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpparty");
                    TextBox meter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");
                    TextBox reqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                 
                 

                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");

                    if (drpwidth.SelectedValue == "1")
                    {
                        width = "36";
                    }
                    else if (drpwidth.SelectedValue == "2")
                    {
                        width = "48";
                    }
                    else
                    {
                        width = "54";
                    }



                    dcalculate = objBs.getsizeforcutt(ddlFit.SelectedValue,width);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt36fs.Text) * wid;
                          //  grandfs = grandfs + Convert.ToDouble(txt36fs.Text);
                        }
                    }


                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");

                  //  dcalculate = objBs.getsizeforworkorder("36HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt36hs.Text) * wid;
                          //  grandhs = grandhs + Convert.ToDouble(txt36hs.Text);
                        }
                    }

                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                  //  dcalculate = objBs.getsizeforworkorder("38FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt38fs.Text) * wid;
                          //  grandfs = grandfs + Convert.ToDouble(txt38fs.Text);
                        }
                    }

                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                 //   dcalculate = objBs.getsizeforworkorder("38HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt38hs.Text) * wid;
                          //  grandhs = grandhs + Convert.ToDouble(txt38hs.Text);
                        }
                    }

                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                  //  dcalculate = objBs.getsizeforworkorder("39FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt39fs.Text) * wid;
                          //  grandfs = grandfs + Convert.ToDouble(txt39fs.Text);
                        }
                    }

                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                 //   dcalculate = objBs.getsizeforworkorder("39HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt39hs.Text) * wid;
                          //  grandhs = grandhs + Convert.ToDouble(txt39hs.Text);
                        }
                    }

                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                   // dcalculate = objBs.getsizeforworkorder("40FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt40fs.Text) * wid;
                         //   grandfs = grandfs + Convert.ToDouble(txt40fs.Text);
                        }
                    }

                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                 //   dcalculate = objBs.getsizeforworkorder("40HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt40hs.Text) * wid;
                          //  grandhs = grandhs + Convert.ToDouble(txt40hs.Text);
                        }
                    }

                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                  //  dcalculate = objBs.getsizeforworkorder("42FS", str);

                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt42fs.Text) * wid;
                          //  grandfs = grandfs + Convert.ToDouble(txt42fs.Text);
                        }
                    }

                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                  //  dcalculate = objBs.getsizeforworkorder("42HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt42hs.Text) * wid;
                          //  grandhs = grandhs + Convert.ToDouble(txt42hs.Text);
                        }
                    }

                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                  //  dcalculate = objBs.getsizeforworkorder("44FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt44fs.Text) * wid;
                           // grandfs = grandfs + Convert.ToDouble(txt44fs.Text);
                        }
                    }

                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");
                 //   dcalculate = objBs.getsizeforworkorder("44HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt44hs.Text) * wid;
                          //  grandhs = grandhs + Convert.ToDouble(txt44hs.Text);
                        }
                    }

                    reqmeter.Text = totgnd.ToString();





                    int col = vLoop + 1;

                    double meter1 = Convert.ToDouble(meter.Text);
                    double reqmeter1 = Convert.ToDouble(totgnd);


                    if (drpparty.SelectedValue == "Select Party Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name in Row " + col + ". Thank you');", true);
                        btnadd.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnadd.Enabled = true;
                    }



                    double number = meter1 - reqmeter1;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter in Row " + col + ". Thank you');", true);
                        btnadd.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnadd.Enabled = true;
                    }

                  



                }
            }
        }



        protected void Add_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");
            DataSet dcalculate = new DataSet();
           
            btnadd.Enabled = false;
            string width = string.Empty;

            DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Save")
            {
                if (ddlFit.SelectedValue == "Select fit")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit. Thank you');", true);
                    return;
                }
                if (CheckBoxList2.SelectedIndex >= 0)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Design Number. Thank you');", true);
                    return;

                }

                if (chkinvno.SelectedIndex >= 0)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Invoice Number. Thank you');", true);
                    return;

                }

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    double totgnd = 0;
                   
                    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                    Label lblid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblid");
                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
                    DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpparty");
                    TextBox meter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");
                    TextBox reqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");

                    if (drpwidth.SelectedValue == "1")
                    {
                        width = "36";
                    }
                    else if (drpwidth.SelectedValue == "2")
                    {
                        width = "48";
                    }
                    else
                    {
                        width = "54";
                    }



                    dcalculate = objBs.getsizeforcutt(ddlFit.SelectedValue, width);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt36fs.Text) * wid;
                            //  grandfs = grandfs + Convert.ToDouble(txt36fs.Text);
                        }
                    }


                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");

                    //  dcalculate = objBs.getsizeforworkorder("36HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt36hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt36hs.Text);
                        }
                    }

                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    //  dcalculate = objBs.getsizeforworkorder("38FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt38fs.Text) * wid;
                            //  grandfs = grandfs + Convert.ToDouble(txt38fs.Text);
                        }
                    }

                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    //   dcalculate = objBs.getsizeforworkorder("38HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt38hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt38hs.Text);
                        }
                    }

                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    //  dcalculate = objBs.getsizeforworkorder("39FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt39fs.Text) * wid;
                            //  grandfs = grandfs + Convert.ToDouble(txt39fs.Text);
                        }
                    }

                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    //   dcalculate = objBs.getsizeforworkorder("39HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt39hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt39hs.Text);
                        }
                    }

                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    // dcalculate = objBs.getsizeforworkorder("40FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt40fs.Text) * wid;
                            //   grandfs = grandfs + Convert.ToDouble(txt40fs.Text);
                        }
                    }

                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    //   dcalculate = objBs.getsizeforworkorder("40HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt40hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt40hs.Text);
                        }
                    }

                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    //  dcalculate = objBs.getsizeforworkorder("42FS", str);

                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt42fs.Text) * wid;
                            //  grandfs = grandfs + Convert.ToDouble(txt42fs.Text);
                        }
                    }

                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    //  dcalculate = objBs.getsizeforworkorder("42HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt42hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt42hs.Text);
                        }
                    }

                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    //  dcalculate = objBs.getsizeforworkorder("44FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt44fs.Text) * wid;
                            // grandfs = grandfs + Convert.ToDouble(txt44fs.Text);
                        }
                    }

                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");
                    //   dcalculate = objBs.getsizeforworkorder("44HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgnd = totgnd + Convert.ToDouble(txt44hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt44hs.Text);
                        }
                    }

                    reqmeter.Text = totgnd.ToString();





                    int col = vLoop + 1;

                    double meter1 = Convert.ToDouble(meter.Text);
                    double reqmeter1 = Convert.ToDouble(totgnd);

                    if (drpparty.SelectedValue == "Select Party Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name in Row " + col + ". Thank you');", true);
                        btnadd.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnadd.Enabled = true;
                    }

                    double number = meter1 - reqmeter1;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter in Row " + col + ". Thank you');", true);
                        btnadd.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnadd.Enabled = true;
                    }

                }


                //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                //{
                //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                //    Label lblid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblid");
                //    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
                //    DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpparty");
                //    TextBox meter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");
                //    TextBox reqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                //    int col = vLoop + 1;

                //    double meter1 = Convert.ToDouble(meter.Text);
                //    double reqmeter1 = Convert.ToDouble(reqmeter.Text);

                //    double number = meter1 - reqmeter1;
                //    if (number < 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter in Row "+ col +". Thank you');", true);
                //        return;
                //    }
                  
                //}

                int iStatus = 0;

                iStatus = objBs.insertcut(txtLotNo.Text, deliverydate, drpwidth.SelectedValue, ddlFit.SelectedValue);

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    double totgndfin = 0;
                    TextBox orderno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                    Label lblid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblid");
                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
                    DropDownList dparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpparty");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");
                    TextBox txtreq = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");

                    if (drpwidth.SelectedValue == "1")
                    {
                        width = "36";
                    }
                    else if (drpwidth.SelectedValue == "2")
                    {
                        width = "48";
                    }
                    else
                    {
                        width = "54";
                    }



                    dcalculate = objBs.getsizeforcutt(ddlFit.SelectedValue, width);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt36fs.Text) * wid;
                            //  grandfs = grandfs + Convert.ToDouble(txt36fs.Text);
                        }
                    }


                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");

                    //  dcalculate = objBs.getsizeforworkorder("36HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt36hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt36hs.Text);
                        }
                    }

                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    //  dcalculate = objBs.getsizeforworkorder("38FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt38fs.Text) * wid;
                            //  grandfs = grandfs + Convert.ToDouble(txt38fs.Text);
                        }
                    }

                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    //   dcalculate = objBs.getsizeforworkorder("38HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt38hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt38hs.Text);
                        }
                    }

                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    //  dcalculate = objBs.getsizeforworkorder("39FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt39fs.Text) * wid;
                            //  grandfs = grandfs + Convert.ToDouble(txt39fs.Text);
                        }
                    }

                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    //   dcalculate = objBs.getsizeforworkorder("39HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt39hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt39hs.Text);
                        }
                    }

                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    // dcalculate = objBs.getsizeforworkorder("40FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt40fs.Text) * wid;
                            //   grandfs = grandfs + Convert.ToDouble(txt40fs.Text);
                        }
                    }

                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    //   dcalculate = objBs.getsizeforworkorder("40HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt40hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt40hs.Text);
                        }
                    }

                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    //  dcalculate = objBs.getsizeforworkorder("42FS", str);

                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt42fs.Text) * wid;
                            //  grandfs = grandfs + Convert.ToDouble(txt42fs.Text);
                        }
                    }

                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    //  dcalculate = objBs.getsizeforworkorder("42HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt42hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt42hs.Text);
                        }
                    }

                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    //  dcalculate = objBs.getsizeforworkorder("44FS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt44fs.Text) * wid;
                            // grandfs = grandfs + Convert.ToDouble(txt44fs.Text);
                        }
                    }

                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");
                    //   dcalculate = objBs.getsizeforworkorder("44HS", str);
                    if (dcalculate != null)
                    {
                        if (dcalculate.Tables[0].Rows.Count > 0)
                        {
                            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            totgndfin = totgndfin + Convert.ToDouble(txt44hs.Text) * wid;
                            //  grandhs = grandhs + Convert.ToDouble(txt44hs.Text);
                        }
                    }

                    txtreq.Text = totgndfin.ToString();





                    int col = vLoop + 1;

                    double meter1 = Convert.ToDouble(txtmeter.Text);
                    double reqmeter1 = Convert.ToDouble(totgndfin);

                    if (dparty.SelectedValue == "Select Party Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name in Row " + col + ". Thank you');", true);
                        btnadd.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnadd.Enabled = true;
                    }

                    double number = meter1 - reqmeter1;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter in Row " + col + ". Thank you');", true);
                        btnadd.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnadd.Enabled = true;
                    }
                    int iStatus2 = objBs.insertTranscut(txtLotNo.Text, orderno.Text, lblid.Text, txtdesign.Text, dparty.SelectedValue, txtmeter.Text, txtreq.Text, txtrate.Text, txt36fs.Text, txt36hs.Text, txt38fs.Text, txt38hs.Text, txt39fs.Text, txt39hs.Text, txt40fs.Text, txt40hs.Text, txt42fs.Text, txt42hs.Text, txt44fs.Text, txt44hs.Text);

                }

                //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                //{

                //    TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                //    Label lblid = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("lblid");

                //    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesigno");


                //    DropDownList dparty = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpparty");


                //    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtmet");


                //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                //    TextBox txtreq = (TextBox)gvcustomerorder.Rows[0].FindControl("txtrmeter");






                //    int iStatus2 = objBs.insertTranscut(txtLotNo.Text,orderno.Text,lblid.Text,txtdesign.Text,dparty.SelectedValue,txtmeter.Text,txtreq.Text,txtrate.Text);

                //}
              
                 

                  

                    //    string condno = getCond();
                    //   string condname = getCondname();

                    //  return;


                 ///  iStatus = objBs.insertCuttingprocess(Convert.ToInt32(txtLotNo.Text), ddlSupplier.SelectedValue, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), txtreq_meter.Text, ddlSupplier.SelectedItem.Text, radbtn.SelectedValue, deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
            
            }
            else
            {
                //int iStatus = 0;
                //if (radbtn.SelectedValue == "1")
                //{
                //    double meter = Convert.ToDouble(txtMeter.Text);
                //    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                //    double number = meter - reqmeter;
                //    if (number < 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                //        return;
                //    }


                //    iStatus = objBs.UpdateCuttingprocess(Convert.ToInt32(txtLotNo.Text), ddlSupplier.SelectedValue, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), Convert.ToInt32(iid), txtreq_meter.Text, ddlSupplier.SelectedItem.Text, radbtn.SelectedValue, deliverydate);
                //    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
                //}
                //else
                //{
                //    double meter = Convert.ToDouble(txtMeter.Text);
                //    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                //    double number = meter - reqmeter;
                //    if (number < 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                //        return;
                //    }
                //    string condno = getCond();
                //    string condname = getCondname();

                //    iStatus = objBs.UpdateCuttingprocess(Convert.ToInt32(txtLotNo.Text), condno, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), Convert.ToInt32(iid), txtreq_meter.Text, condname, radbtn.SelectedValue, deliverydate);
                //    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
                //}
            }

            System.Threading.Thread.Sleep(3000);
        }

        protected string getCond()
        {
            string cond = "";

            //foreach (ListItem listItem in chkSupplier.Items)
            //{
            //    if (listItem.Text != "All")
            //    {
            //        if (listItem.Selected)
            //        {
            //            cond += listItem.Value + ",";
            //        }
            //    }
            //}
            //cond = cond.TrimEnd(',');
            ////   cond = cond.Replace(",", ",");
            return cond;
        }

        protected string getCondname()
        {
            string cond = "";

            //foreach (ListItem listItem in chkSupplier.Items)
            //{
            //    if (listItem.Text != "All")
            //    {
            //        if (listItem.Selected)
            //        {
            //            cond += listItem.Text + ",";
            //        }
            //    }
            //}
            //// cond = cond.TrimEnd(',');
            ////   cond = cond.Replace(",", ",");
            //cond = cond.TrimEnd(',');
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

        protected void drpwidthChange(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();

            DataSet dsrefno = objBs.getnewsupplierforcut(drpwidth.SelectedValue);
            if (dsrefno != null)
            {
                if (dsrefno.Tables[0].Rows.Count > 0)
                {
                    chkinvno.DataSource = dsrefno.Tables[0];
                    chkinvno.DataTextField = "refno";
                    chkinvno.DataValueField = "transid";
                    chkinvno.DataBind();
                    //  drpwidth.Items.Insert(0, "Select Width");
                }
            }

            if (chkinvno.SelectedIndex >= 0)
            {
                // CheckBoxList2.Enabled = true;
                //Loop through each item of checkboxlist
                foreach (ListItem item in chkinvno.Items)
                {
                    //check if item selected
                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        if (!IsParticipantExists(item.Value))
                        {

                        }
                        else
                        {
                            dteo = objBs.getcutlistdesign(item.Value, drpwidth.SelectedValue);
                            if (dteo != null)
                            {
                                if (dteo.Tables[0].Rows.Count > 0)
                                {
                                    dssmer.Merge(dteo);
                                }
                            }
                        }
                    }
                }
                if (dssmer != null)
                {
                    if (dssmer.Tables[0].Rows.Count > 0)
                    {
                        CheckBoxList2.DataSource = dssmer;
                        CheckBoxList2.DataTextField = "Design";
                        CheckBoxList2.DataValueField = "id";
                        CheckBoxList2.DataBind();
                    }
                }
                //Uncheck all selected items
                //  cbParticipants.ClearSelection();
            }
            else
            {

                CheckBoxList2.Items.Clear();
                // chkinvno.Enabled = false;

            }


            DataSet dssmer1 = new DataSet();
            DataSet dteo1 = new DataSet();
            if (CheckBoxList2.SelectedIndex >= 0)
            {

                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (ListItem item in CheckBoxList2.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        //if (!IsParticipantExists(item.Value))
                        //{

                        //}
                        //if (lop == 1)
                        //{
                        //    ButtonAdd1_Click(sender, e);

                        //}
                        // else
                        {
                            dteo1 = objBs.getcutlistdesignfortrans(item.Value);
                            if (dteo1 != null)
                            {
                                if (dteo1.Tables[0].Rows.Count > 0)
                                {
                                    dssmer1.Merge(dteo1);
                                }
                                lop++;
                            }
                        }
                    }
                }
                gvcustomerorder.DataSource = dssmer1;
                gvcustomerorder.DataBind();
            }
            else
            {
                //CheckBoxList2.Enabled = true;
                //chkinvno.Enabled = true;

                gvcustomerorder.DataSource = null;
                gvcustomerorder.DataBind();
            }
            //if (drpwidth.SelectedValue == "Select Width")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Width');", true);
            //    return;

            //}
            //else
            //{
            //    DataSet ds = objBs.getnewsupplierforcut(drpwidth.SelectedValue);
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            chkinvno.DataSource = ds.Tables[0];
            //            chkinvno.DataTextField = "refno";
            //            chkinvno.DataValueField = "fabid";
            //            chkinvno.DataBind();

            //        }
            //        else
            //        {
            //            chkinvno.DataSource = null;
            //            chkinvno.DataBind();
            //            chkinvno.ClearSelection();
            //            chkinvno.Items.Clear();

            //        }
            //    }
            //    else
            //    {
            //        chkinvno.DataSource = null;
            //        chkinvno.DataBind();
            //        chkinvno.ClearSelection();
            //        chkinvno.Items.Clear();
            //    }

            //}


        }
        protected void chkinvnochanged(object sender, EventArgs e)
        {

            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            //  dteo = objBs.getjobcardlistdesign(CheckBoxList1.SelectedValue);



            if (chkinvno.SelectedIndex >= 0)
            {
               // CheckBoxList2.Enabled = true;
                //Loop through each item of checkboxlist
                foreach (ListItem item in chkinvno.Items)
                {
                    //check if item selected
                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        if (!IsParticipantExists(item.Value))
                        {

                        }
                        else
                        {
                            dteo = objBs.getcutlistdesign(item.Value,drpwidth.SelectedValue);
                            if (dteo != null)
                            {
                                if (dteo.Tables[0].Rows.Count > 0)
                                {
                                    dssmer.Merge(dteo);
                                }
                            }
                        }
                    }
                }
                if (dssmer != null)
                {
                    if (dssmer.Tables[0].Rows.Count > 0)
                    {
                        CheckBoxList2.DataSource = dssmer;
                        CheckBoxList2.DataTextField = "Design";
                        CheckBoxList2.DataValueField = "id";
                        CheckBoxList2.DataBind();
                    }
                }
                //Uncheck all selected items
                //  cbParticipants.ClearSelection();
            }
            else
            {

                CheckBoxList2.Items.Clear();
               // chkinvno.Enabled = false;
               
            }


        }

        private bool IsParticipantExists(string val)
        {
            bool exists = false;
            //Loop through each item in selected participant checkboxlist
            foreach (ListItem item in chkinvno.Items)
            {
                //Check if item selected already exists in the selected participant checboxlist or not
                if (item.Value == val)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }
        protected void check2_changed(object sender, EventArgs e)
        {

            gvcustomerorder.Columns[7].Visible = false;
            gvcustomerorder.Columns[8].Visible = false;

            gvcustomerorder.Columns[9].Visible = false;
            gvcustomerorder.Columns[10].Visible = false;

            gvcustomerorder.Columns[11].Visible = false; gvcustomerorder.Columns[12].Visible = false;

            gvcustomerorder.Columns[13].Visible = false; gvcustomerorder.Columns[14].Visible = false;

            gvcustomerorder.Columns[15].Visible = false; gvcustomerorder.Columns[16].Visible = false;

            gvcustomerorder.Columns[17].Visible = false; gvcustomerorder.Columns[18].Visible = false;


            gvcustomerorder.Columns[19].Visible = false;
            gvcustomerorder.Columns[20].Visible = false;

            gvcustomerorder.Columns[21].Visible = false;
            gvcustomerorder.Columns[22].Visible = false;

            gvcustomerorder.Columns[23].Visible = false; gvcustomerorder.Columns[24].Visible = false;

            gvcustomerorder.Columns[25].Visible = false; gvcustomerorder.Columns[26].Visible = false;

            gvcustomerorder.Columns[27].Visible = false; gvcustomerorder.Columns[28].Visible = false;

            gvcustomerorder.Columns[29].Visible = false; gvcustomerorder.Columns[30].Visible = false;




            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            if (CheckBoxList2.SelectedIndex >= 0)
            {
             
                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (ListItem item in CheckBoxList2.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        //if (!IsParticipantExists(item.Value))
                        //{

                        //}
                        //if (lop == 1)
                        //{
                        //    ButtonAdd1_Click(sender, e);

                        //}
                        // else
                        {
                            dteo = objBs.getcutlistdesignfortrans(item.Value);
                            if (dteo != null)
                            {
                                if (dteo.Tables[0].Rows.Count > 0)
                                {
                                    dssmer.Merge(dteo);
                                }
                                lop++;
                            }
                        }
                    }
                }
                gvcustomerorder.DataSource = dssmer;
                gvcustomerorder.DataBind();
            }
            else
            {
                //CheckBoxList2.Enabled = true;
                //chkinvno.Enabled = true;
             
                gvcustomerorder.DataSource = null;
                gvcustomerorder.DataBind();
            }
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
            //if (radbtn.SelectedValue == "1")
            //{
            //    single.Visible = true;
            //    multiple.Visible = false;
            //}
            //else
            //{
            //    multiple.Visible = true;
            //    single.Visible = false;

            //}
        }

        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Designno", typeof(string)));
            dtt.Columns.Add(new DataColumn("Ameter", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rmeter", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("Color", typeof(string)));
            dtt.Columns.Add(new DataColumn("Width", typeof(string)));


            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["Designno"] = string.Empty;
            dr["Ameter"] = string.Empty;
            dr["Rmeter"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["Color"] = string.Empty;
            dr["Width"] = string.Empty;


            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Designno");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Ameter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rmeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Width");
            dttt.Columns.Add(dct);





            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();

            drNew["OrderNo"] = 0;
            drNew["Designno"] = "";
            drNew["Ameter"] = 0;
            drNew["Rmeter"] = 0;
            drNew["Rate"] = 0;
            drNew["Color"] = 0;
            drNew["Width"] = "";



            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();


            //TextBox txn = (TextBox)grvStudentDetails.Rows[0].Cells[1].FindControl("txtName");
            //txn.Focus();
            //Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
            //Page.Form.DefaultFocus = btnAdd.ClientID;

        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsDNo = objBs.getnewpartyforcut();

          //  DataSet dsFit = objBs.GetFit();



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox txtno = (TextBox)e.Row.FindControl("txtno");

                //TextBox txtttk = (TextBox)e.Row.FindControl("txtqty");
                //TextBox txttk = (TextBox)e.Row.FindControl("txtRate");
                //TextBox txtkt = (TextBox)e.Row.FindControl("txtTax");
                //TextBox txtkttt = (TextBox)e.Row.FindControl("txtAmount");
                //TextBox txtktt = (TextBox)e.Row.FindControl("txtStock");
                //TextBox txtktttt = (TextBox)e.Row.FindControl("txtDiscount");

                //txtno.Text = "1";
                //txtttk.Text = "0";
                //txttk.Text = "0";
                //txtkt.Text = "0";
                //txtkttt.Text = "0";
                //txtktt.Text = "0";
                //txtktttt.Text = "0";
                // txtno.Text = "1";


                var ddl = (DropDownList)e.Row.FindControl("drpparty");
                ddl.DataSource = dsDNo;
                ddl.DataTextField = "LedgerName";
                ddl.DataValueField = "Ledgerid";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Party Name");



                //var ddlt = (DropDownList)e.Row.FindControl("drpfit");
                //ddlt.DataSource = dsFit;
                //ddlt.DataTextField = "Fit";
                //ddlt.DataValueField = "fitid";
                //ddlt.DataBind();
                //ddlt.Items.Insert(0, "Select Fit");




            }

        }

        protected void gvcustomerorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsCategory = new DataSet();

            DataSet dscat = new DataSet();

            string OrderNo = Request.QueryString.Get("OrderNo");
            //if (OrderNo != "")
            //{
            //    /// dsCategory = objBs.GetCAT_OrderForm();
            //}
            //else
            //    dsCategory = objBs.selectcategorybrandcat(sTableName);

            DataSet dsDNo = objBs.GetDNo();

            DataSet dsFit = objBs.GetFit();



            //else
            //    dsCategory = objBs.selectcategorymaster_Dealer("tblStock_" + sTableName);


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlCategory1 = (DropDownList)(e.Row.FindControl("drpItem") as DropDownList);
                ddlCategory1.Focus();
                ddlCategory1.Enabled = true;
                ddlCategory1.DataSource = dsDNo.Tables[0];
                ddlCategory1.DataTextField = "Dno";
                ddlCategory1.DataValueField = "ProcessID";
                ddlCategory1.DataBind();
                ddlCategory1.Items.Insert(0, "Select Design");

                //DropDownList ddlDef1 = (DropDownList)(e.Row.FindControl("drpfit") as DropDownList);
                //ddlDef1.Focus();
                //ddlDef1.Enabled = true;
                //ddlDef1.DataSource = dsFit.Tables[0];
                //ddlDef1.DataTextField = "Fit";
                //ddlDef1.DataValueField = "fitid";
                //ddlDef1.DataBind();
                //ddlDef1.Items.Insert(0, "Select Fit");



            }
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        TextBox txtameter =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");


                        DropDownList drpItem =
                          (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

                        TextBox txtrmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");




                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
                        TextBox txtcolor =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
                        TextBox txtwidth =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");

                        TextBox txttno =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");






                        drpItem.Items.Clear();

                        DataSet dst = objBs.GetDNo();
                        drpItem.Items.Add(new ListItem("Select Design", "0"));
                        drpItem.DataSource = dst;
                        drpItem.DataBind();
                        drpItem.DataTextField = "Dno";
                        drpItem.DataValueField = "Processid";


                        txtameter.Text = dt.Rows[i]["ameter"].ToString();
                        txtrmeter.Text = dt.Rows[i]["rmeter"].ToString();

                        drpItem.SelectedValue = dt.Rows[i]["Designno"].ToString();
                        txttno.Text = dt.Rows[i]["OrderNo"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtcolor.Text = dt.Rows[i]["color"].ToString();
                        txtwidth.Text = dt.Rows[i]["width"].ToString();
                        txtwidth.Text = dt.Rows[i]["width"].ToString();


                        rowIndex++;

                    }
                }
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {


                        TextBox txtameter =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");


                        DropDownList drpItem =
                          (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

                        TextBox txtrmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");




                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
                        TextBox txtcolor =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
                        TextBox txtwidth =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Designno"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Ameter"] = txtameter.Text;
                        dtCurrentTable.Rows[i - 1]["Rmeter"] = txtrmeter.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["Width"] = txtwidth.Text;



                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList drpCategory = (DropDownList)row.FindControl("drpItem");


            //DropDownList Defitem = (DropDownList)row.FindControl("drpItem");

            //DropDownList procode = (DropDownList)row.FindControl("ProductCode");


            DataSet ds_Cutting = objBs.SelectDesign_CuttingProcess(drpCategory.SelectedValue);
            int Width_Id = Convert.ToInt32(ds_Cutting.Tables[0].Rows[0]["WidthID"].ToString());

            DataSet ds_Width = objBs.editwidth(Width_Id);




            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

                    TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
                    TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                    TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
                    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");


                    txtwidth.Text = ds_Width.Tables[0].Rows[0]["Width"].ToString();
                    txtrate.Text = ds_Cutting.Tables[0].Rows[0]["Rate"].ToString();
                    txtameter.Text = ds_Cutting.Tables[0].Rows[0]["AvaliableMeter"].ToString();


                }
            }
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

                TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
                TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");


                if (ProductCode.SelectedItem.Text == "Select Design")
                {
                    No = 0;
                    break;
                }
                else
                {
                    No = 1;
                }
            }

            if (No == 1)
            {

                AddNewRow();
            }
            else
            {

            }
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                //  TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                txtno.Focus();
            }



        }

        private void AddNewRow()
        {
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

                TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
                TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                // Label txtno = (Label)gvcustomerorder.Rows[vLoop].FindControl("txtno");


                int col = vLoop + 1;


                txtno.Focus();
            }

            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        TextBox txtameter =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");


                        DropDownList drpItem =
                          (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

                        TextBox txtrmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");




                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
                        TextBox txtcolor =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
                        TextBox txtwidth =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Designno"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Ameter"] = txtameter.Text;
                        dtCurrentTable.Rows[i - 1]["Rmeter"] = txtrmeter.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["Width"] = txtwidth.Text;



                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void reqmeter(object sender, EventArgs e)
        {
            ButtonAdd1_Click(sender, e);

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
                    if (oldtxttk.Text == "0.00")
                    {
                        oldtxttk.Text = ".00";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            }
        }
    }
}