using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Globalization;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class Receipt : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string sFrom = "";
        string sBranch = "";


        int EmpId = 0;
        string EmployeeName = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            aganistbillno.Visible = false;

            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");


            EmpId = Convert.ToInt32(Session["EmpId"].ToString());
           // EmployeeName = Session["EmployeeName"].ToString();


            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            //sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                lbltype.Text = "Customer Receipt";
                rblist.SelectedValue = "1";
                rblist.Enabled = true;

                ddlType.Enabled = true;
                lblcustomer.Visible = true;
                ddlcustomerID.Visible = true;
                ddlBank.Enabled = false;
                txtChequeno.Enabled = false;

                txtAgainst.Visible = true;
                //lblAganist.Visible = false;
                gvledgrid.DataSource = null;
                gvledgrid.DataBind();
                gvledgrid.Visible = true;

                DataSet dagent = objBs.getagentlist(sTableName);
                if (dagent.Tables[0].Rows.Count > 0)
                {

                    ddlType.DataSource = dagent.Tables[0];
                    ddlType.DataTextField = "LEdgerName";
                    ddlType.DataValueField = "LedgerID";
                    ddlType.DataBind();
                    //ddlType.Items.Insert(0, "Select Agent");
                }

                DataSet dstax = objBs.SelectTax();
                if (dstax.Tables[0].Rows.Count > 0)
                {
                    ddltax.DataSource = dstax.Tables[0];
                    ddltax.DataTextField = "tax";
                    ddltax.DataValueField = "taxid";
                    ddltax.DataBind();
                    ddltax.Items.Insert(0, "Select Tax");
                    //ddlcategory.Items.Insert(0, "Select Category");
                }

               
                DataSet ds = objBs.Receiptno("tblReceipt_" + sTableName);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                    if (ds.Tables[0].Rows[0]["ReceiptNo"].ToString() == "")
                        txtreceiptno.Text = "1";
                    else
                        txtreceiptno.Text = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();

                    txtreceiptdate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
                txtNarration.Text = "Receipt No: " + txtreceiptno.Text;

                //DataSet dsed = objBs.CheckEditDelete(EmpId);
                //if (dsed.Tables[0].Rows[0]["allowdate"].ToString() == "1")
                //{
                //    txtreceiptdate.Enabled = true;
                //}
                //else
                //{
                //    txtreceiptdate.Enabled = false;
                //}


                if (!IsPostBack)
                {
                    lblUserID.Text = Session["UserID"].ToString();
                    txtreceiptdate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    //DataSet dsCustomer = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                    //if (dsCustomer != null)
                    //{
                    //    if (dsCustomer.Tables[0].Rows.Count > 0)
                    //    {
                    //        ddlcustomerID.DataSource = dsCustomer.Tables[0];
                    //        ddlcustomerID.DataTextField = "LedgerName";
                    //        ddlcustomerID.DataValueField = "LedgerID";
                    //        ddlcustomerID.DataBind();
                    //        ddlcustomerID.Items.Insert(0, "Select Ledger");
                    //    }
                    //    else
                    //    {
                    //        ddlcustomerID.Items.Insert(0, "Select Ledger");
                    //    }
                    //}
                    //else
                    //{
                    //    ddlcustomerID.Items.Insert(0, "Select Ledger");
                    //}


                    FirstGridViewRow();

                    DataSet dst1 = objBs.Getbanknamebranch(4, sTableName);

                    if (dst1 != null)
                    {
                        if (dst1.Tables[0].Rows.Count > 0)
                        {
                            ddlBank.DataSource = dst1.Tables[0];
                            ddlBank.DataTextField = "LedgerName";
                            ddlBank.DataValueField = "LedgerID";
                            ddlBank.DataBind();
                            ddlBank.Items.Insert(0, "Select Bank Name");


                            //ddlbankID.DataSource = dst1.Tables[0];
                            //ddlbankID.DataTextField = "LedgerName";
                            //ddlbankID.DataValueField = "LedgerID";
                            //ddlbankID.DataBind();
                            //ddlbankID.Items.Insert(0, "Select Bank Name");
                        }
                        else
                        {
                            ddlBank.Items.Insert(0, "Select Bank Name");
                            //   ddlbankID.Items.Insert(0, "Select Bank Name");
                        }
                    }
                    else
                    {
                        ddlBank.Items.Insert(0, "Select Bank Name");
                        //   ddlbankID.Items.Insert(0, "Select Bank Name");
                    }



                    DataSet d1 = objBs.Getpaymentmode();

                    if (d1 != null)
                    {
                        if (d1.Tables[0].Rows.Count > 0)
                        {
                            ddmodeofpayment.DataSource = d1.Tables[0];
                            ddmodeofpayment.DataTextField = "Payment_Mode";
                            ddmodeofpayment.DataValueField = "Payment_ID";
                            ddmodeofpayment.DataBind();
                            ddmodeofpayment.Items.Insert(0, "Select Payment Mode");
                        }
                        else
                        {
                            ddmodeofpayment.Items.Insert(0, "Select Payment Mode");
                        }
                    }
                    else
                    {
                        ddmodeofpayment.Items.Insert(0, "Select Payment Mode");
                    }



                    DataSet dstd = objBs.GetLedgerType();
                    //DataSet dstd = objBs.Getagent(sTableName);
                    //if (dstd != null)
                    //{

                    //    if (dstd.Tables[0].Rows.Count > 0)
                    //    {
                    //        ddlcustomerID.DataSource = dstd.Tables[0];
                    //        ddlcustomerID.DataTextField = "Ledgername";
                    //        ddlcustomerID.DataValueField = "LedgerID";
                    //        ddlcustomerID.DataBind();
                    //        ddlcustomerID.Items.Insert(0, "Select Customer");
                    //    }
                    //    else
                    //    {
                    //        ddlType.Items.Insert(0, "Select Customer");
                    //    }
                    //}
                    //else
                    //{
                    //    ddlType.Items.Insert(0, "Select Customer");
                    //}

                    rblist_selectedIndexChanged(sender, e);
                    
                    ddlType.SelectedValue = Convert.ToString(1);
                    narra.Visible = false;
                    string TransNo = Request.QueryString.Get("TransNo");
                    if (TransNo != null)
                    {
                        btnadd.Text = "Update";
                        hidfnwe.Value = "Edit";
                        narra.Visible = true;
                        DataSet DR = objBs.GetReceiptNo(Convert.ToInt32(TransNo), "tblReceipt_" + sTableName, "tblDayBook_" + sTableName);
                        if (DR.Tables[0].Rows.Count > 0)
                        {
                            rblist.SelectedValue = DR.Tables[0].Rows[0]["rbList"].ToString();
                            rblist_selectedIndexChanged(sender, e);
                            txtreceiptid.Text = DR.Tables[0].Rows[0]["ReceiptID"].ToString();
                            txtreceiptno.Text = DR.Tables[0].Rows[0]["ReceiptNo"].ToString();
                            //txtAgainst.Text = DR.Tables[0].Rows[0]["Against"].ToString();
                            txtreceiptdate.Text = Convert.ToDateTime(DR.Tables[0].Rows[0]["ReceiptDate"]).ToString("dd/MM/yyyy");




                            DataSet dst = new DataSet();

                            //if (DR.Tables[0].Rows[0]["LedgerType"].ToString() == "Customer")
                            //{
                            //    lbltype.Text = "Customer Receipt";
                            //    ddlType.SelectedValue = "1";
                            //    txtAgainst.Text = "";
                            //    txtAgainst.Visible = false;
                            //    gvledgrid.DataSource = null;
                            //    gvledgrid.DataBind();
                            //    gvledgrid.Visible = true;

                            //    //lblAganist.Visible = false;
                            //    dst = objBs.GetLedgersname(Convert.ToInt32(lblUserID.Text), 1);
                            //}
                            //else if (DR.Tables[0].Rows[0]["LedgerType"].ToString() == "Vendor")
                            //{
                            //    lbltype.Text = "Vendor Receipt";
                            //    ddlType.SelectedValue = "2";
                            //    txtAgainst.Visible = true;
                            //    gvledgrid.DataSource = null;
                            //    gvledgrid.DataBind();
                            //    gvledgrid.Visible = false;
                            //    //lblAganist.Visible = true;
                            //    dst = objBs.GetLedgersname(Convert.ToInt32(lblUserID.Text), 2);
                            //}
                            //else if (DR.Tables[0].Rows[0]["LedgerType"].ToString() == "Expense")
                            //{
                            //    lbltype.Text = "Expense Receipt";
                            //    ddlType.SelectedValue = "3";
                            //    txtAgainst.Visible = true;
                            //    gvledgrid.DataSource = null;
                            //    gvledgrid.DataBind();
                            //    gvledgrid.Visible = false;
                            //    //lblAganist.Visible = true;
                            //    dst = objBs.GetLedgersname(Convert.ToInt32(lblUserID.Text), 3);
                            //}
                            //else if (DR.Tables[0].Rows[0]["LedgerType"].ToString() == "Bank")
                            //{
                            //    lbltype.Text = "Bank Receipt";
                            //    ddlType.SelectedValue = "4";
                            //    txtAgainst.Visible = true;
                            //    gvledgrid.DataSource = null;
                            //    gvledgrid.DataBind();
                            //    gvledgrid.Visible = false;
                            //   // lblAganist.Visible = true;
                            //    dst = objBs.GetLedgersname(Convert.ToInt32(lblUserID.Text), 4);
                            //}



                            DataSet dsbankname = objBs.Getbanknamebranch(4, sTableName);
                            if (dsbankname != null)
                            {
                                if (dsbankname.Tables[0].Rows.Count > 0)
                                {

                                    ddlBank.DataSource = dsbankname.Tables[0];
                                    ddlBank.DataTextField = "LedgerName";
                                    ddlBank.DataValueField = "LedgerID";
                                    ddlBank.DataBind();
                                    ddlBank.Items.Insert(0, "Select Bank Name");
                                }
                                else
                                {
                                    ddlBank.Items.Insert(0, "Select Bank Name");
                                }
                            }
                            else
                            {
                                ddlBank.Items.Insert(0, "Select Bank Name");
                            }
                            ddlcustomerID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();
                            ddlbankID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();
                            ddmodeofpayment.SelectedValue = DR.Tables[0].Rows[0]["PayModeID"].ToString();
                           // drpEmployee.SelectedValue = DR.Tables[0].Rows[0]["EmployeeID"].ToString();
                            // ddlType.SelectedItem.Text = DR.Tables[0].Rows[0]["AgentID"].ToString();

                            //dagent = objBs.getagentlist(sTableName);
                            //if (dagent.Tables[0].Rows.Count > 0)
                            //{
                            //    ddlType.ClearSelection();
                            //    ddlType.DataSource = dagent.Tables[0];
                            //    ddlType.DataTextField = "LedgerName";
                            //    ddlType.DataValueField = "LedgerID";
                            //    ddlType.DataBind();
                            //    ddlType.Items.Insert(0, "Select Agent");
                            //}

                            drpGsttype.SelectedValue = DR.Tables[0].Rows[0]["GSTType"].ToString();
                            ddltax.SelectedValue = DR.Tables[0].Rows[0]["GSTTax"].ToString();
                            ddlProvince.SelectedValue = DR.Tables[0].Rows[0]["Province"].ToString();
                            txtgstAmount.Text = Convert.ToDouble(DR.Tables[0].Rows[0]["Total"]).ToString("0.00");
                            txtCGST.Text = Convert.ToDouble(DR.Tables[0].Rows[0]["CGST"]).ToString("0.00");
                            txtSGST.Text = Convert.ToDouble(DR.Tables[0].Rows[0]["SGST"]).ToString("0.00");
                            txtIGST.Text = Convert.ToDouble(DR.Tables[0].Rows[0]["IGST"]).ToString("0.00");

                            lblFile_Path.Text = DR.Tables[0].Rows[0]["Imgpath"].ToString();// "~/Files/" + fp_Upload.PostedFile.FileName;
                            img_Photo.ImageUrl = DR.Tables[0].Rows[0]["Imgpath"].ToString(); //"~/Files/" + fp_Upload.PostedFile.FileName;

                            if (rblist.SelectedValue == "1")
                            {
                                rblist.SelectedValue = "1";
                                rblist.Enabled = false;

                                lblcustomer.Visible = true;
                                ddlcustomerID.Visible = true;
                                lblbank.Visible = false;
                                ddlbankID.Visible = false;


                                ddlType.SelectedValue = DR.Tables[0].Rows[0]["AgentID"].ToString();
                                ddlcustomerID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();


                                dst = objBs.getLedger_New(lblContactTypeId.Text);
                                if (dstd != null)
                                {
                                    if (dst.Tables[0].Rows.Count > 0)
                                    {
                                        ddlcustomerID.DataSource = dst.Tables[0];
                                        ddlcustomerID.DataTextField = "LedgerName";
                                        ddlcustomerID.DataValueField = "LedgerID";
                                        ddlcustomerID.DataBind();
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                    else
                                    {
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                }
                                else
                                {
                                    ddlcustomerID.Items.Insert(0, "Select customer");
                                }

                                //DataSet dsCustDet = objBs.GetCustomerDetails(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                                //if (dsCustDet.Tables[0].Rows.Count > 0)
                                //{
                                //    txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                                //    txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                                //    txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                                //    txtarea.Text = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                                //}
                            }
                            else if (rblist.SelectedValue == "5")
                            {
                                rblist.SelectedValue = "5";
                                rblist.Enabled = false;

                                lblcustomer.Visible = true;
                                ddlcustomerID.Visible = true;
                                lblbank.Visible = false;
                                ddlbankID.Visible = false;


                                ddlType.SelectedValue = DR.Tables[0].Rows[0]["AgentID"].ToString();
                                ddlcustomerID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();


                                dst = objBs.getLedger_New(lblContactTypeId.Text);
                                if (dstd != null)
                                {
                                    if (dst.Tables[0].Rows.Count > 0)
                                    {
                                        ddlcustomerID.DataSource = dst.Tables[0];
                                        ddlcustomerID.DataTextField = "LedgerName";
                                        ddlcustomerID.DataValueField = "LedgerID";
                                        ddlcustomerID.DataBind();
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                    else
                                    {
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                }
                                else
                                {
                                    ddlcustomerID.Items.Insert(0, "Select customer");
                                }

                                //DataSet dsCustDet = objBs.GetCustomerDetails(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                                //if (dsCustDet.Tables[0].Rows.Count > 0)
                                //{
                                //    txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                                //    txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                                //    txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                                //    txtarea.Text = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                                //}
                            }
                            else if (rblist.SelectedValue == "6")
                            {
                                rblist.SelectedValue = "6";
                                rblist.Enabled = false;

                                lblcustomer.Visible = true;
                                ddlcustomerID.Visible = true;
                                lblbank.Visible = false;
                                ddlbankID.Visible = false;


                                ddlType.SelectedValue = DR.Tables[0].Rows[0]["AgentID"].ToString();
                                ddlcustomerID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();


                                dst = objBs.getLedger_New(lblContactTypeId.Text);
                                if (dstd != null)
                                {
                                    if (dst.Tables[0].Rows.Count > 0)
                                    {
                                        ddlcustomerID.DataSource = dst.Tables[0];
                                        ddlcustomerID.DataTextField = "LedgerName";
                                        ddlcustomerID.DataValueField = "LedgerID";
                                        ddlcustomerID.DataBind();
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                    else
                                    {
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                }
                                else
                                {
                                    ddlcustomerID.Items.Insert(0, "Select customer");
                                }

                                //DataSet dsCustDet = objBs.GetCustomerDetails(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                                //if (dsCustDet.Tables[0].Rows.Count > 0)
                                //{
                                //    txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                                //    txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                                //    txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                                //    txtarea.Text = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                                //}
                            }
                            else if (rblist.SelectedValue == "7")
                            {
                                rblist.SelectedValue = "7";
                                rblist.Enabled = false;

                                lblcustomer.Visible = true;
                                ddlcustomerID.Visible = true;
                                lblbank.Visible = false;
                                ddlbankID.Visible = false;


                                ddlType.SelectedValue = DR.Tables[0].Rows[0]["AgentID"].ToString();
                                ddlcustomerID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();


                                dst = objBs.getLedger_New(lblContactTypeId.Text);
                                if (dstd != null)
                                {
                                    if (dst.Tables[0].Rows.Count > 0)
                                    {
                                        ddlcustomerID.DataSource = dst.Tables[0];
                                        ddlcustomerID.DataTextField = "LedgerName";
                                        ddlcustomerID.DataValueField = "LedgerID";
                                        ddlcustomerID.DataBind();
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                    else
                                    {
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                }
                                else
                                {
                                    ddlcustomerID.Items.Insert(0, "Select customer");
                                }

                                //DataSet dsCustDet = objBs.GetCustomerDetails(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                                //if (dsCustDet.Tables[0].Rows.Count > 0)
                                //{
                                //    txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                                //    txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                                //    txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                                //    txtarea.Text = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                                //}
                            }
                            else if (rblist.SelectedValue == "4")
                            {
                               
                            }
                            else if (rblist.SelectedValue == "2")
                            {
                             
                            }
                            else
                            {
                              
                            }
                            //DataSet  dsCustomer = objBs.getledgeragentname(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                            //    if (dsCustomer.Tables[0].Rows.Count > 0)
                            //    {
                            //        ddlType.DataSource = dsCustomer.Tables[0];
                            //        ddlType.DataTextField = "LedgerName";
                            //        ddlType.DataValueField = "LedgerID";
                            //        ddlType.DataBind();

                            //    }



                            if (Convert.ToInt32(DR.Tables[0].Rows[0]["PayModeID"]) == 1)
                            {
                                ddlBank.Enabled = false;
                                txtChequeno.Enabled = false;
                            }
                            else if (Convert.ToInt32(DR.Tables[0].Rows[0]["PayModeID"]) == 6)
                            {
                                ddlBank.Enabled = true;

                            }

                            else
                            {
                                ddlBank.Enabled = true;
                                txtChequeno.Enabled = true;
                            }



                            DataSet dstt = objBs.GetTransReceipt(Convert.ToInt32(TransNo), "tblReceipt_" + sTableName, "tblTransReceipt_" + sTableName);
                            if (dstt.Tables[0].Rows.Count > 0)
                            {
                                gvledgrid.DataSource = dstt;
                                gvledgrid.DataBind();
                            }
                            else
                            {
                                gvledgrid.DataSource = null;
                                gvledgrid.DataBind();
                            }

                            //DataSet ds2 = objBs.GetTransVehicleReceipt(Convert.ToInt32(TransNo), "tblReceipt_" + sTableName, "tblTransvehicleReceipt_" + sTableName);
                            //{
                            //    if (ds2.Tables[0].Rows.Count > 0)
                            //    {
                            //        int Tpo = ds2.Tables[0].Rows.Count;

                            //        DataTable dttt;
                            //        DataRow drNew;
                            //        DataColumn dct;
                            //        DataSet dstd1 = new DataSet();
                            //        dttt = new DataTable();

                            //        dct = new DataColumn("OrderNo");
                            //        dttt.Columns.Add(dct);

                            //        dct = new DataColumn("LoginID");
                            //        dttt.Columns.Add(dct);

                            //        dct = new DataColumn("SIMNo");
                            //        dttt.Columns.Add(dct);

                            //        dct = new DataColumn("IMEINo");
                            //        dttt.Columns.Add(dct);

                            //        dct = new DataColumn("PlateNo");
                            //        dttt.Columns.Add(dct);

                            //        dstd1.Tables.Add(dttt);

                            //        int snocount = 1;
                            //        foreach (DataRow dr in ds2.Tables[0].Rows)
                            //        {
                            //            drNew = dttt.NewRow();
                            //            drNew["OrderNo"] = snocount;
                            //            drNew["LoginID"] = dr["LoginID"];
                            //            drNew["SIMNo"] = dr["SIMNo"];
                            //            drNew["IMEINo"] = dr["IMEINo"];
                            //            drNew["PlateNo"] = dr["PlateNo"];
                            //            dstd1.Tables[0].Rows.Add(drNew);
                            //            snocount = snocount + 1;
                            //        }

                            //        ViewState["CurrentTable1"] = dttt;

                            //        grdVehno.DataSource = dstd1;
                            //        grdVehno.DataBind();

                            //        for (int vLoop = 0; vLoop < grdVehno.Rows.Count; vLoop++)
                            //        {
                            //            TextBox txtno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtno");
                            //            TextBox txtLoginID = (TextBox)grdVehno.Rows[vLoop].FindControl("txtLoginID");
                            //            TextBox txtSIMno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtSIMno");
                            //            TextBox txtIMEIno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtIMEIno");
                            //            TextBox txtPlateno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtPlateno");

                            //            txtno.Text = dstd1.Tables[0].Rows[vLoop]["OrderNo"].ToString();
                            //            txtLoginID.Text = dstd1.Tables[0].Rows[vLoop]["LoginID"].ToString();
                            //            txtSIMno.Text = dstd1.Tables[0].Rows[vLoop]["SIMNo"].ToString();
                            //            txtIMEIno.Text = dstd1.Tables[0].Rows[vLoop]["IMEINo"].ToString();
                            //            txtPlateno.Text = dstd1.Tables[0].Rows[vLoop]["PlateNo"].ToString();

                            //            txtno.Focus();
                            //        }
                            //    }
                            //}

                            txtNarration.Text = DR.Tables[0].Rows[0]["Narration"].ToString();
                            txtAmount.Text = Convert.ToDouble(DR.Tables[0].Rows[0]["Amount"]).ToString("f2");

                            txtChequeno.Text = DR.Tables[0].Rows[0]["ChequeNo"].ToString();
                            ddlBank.SelectedValue = DR.Tables[0].Rows[0]["BankName"].ToString();

                            txtTransNo.Text = DR.Tables[0].Rows[0]["TransNo"].ToString();
                        }
                    }

                    string iVoucherNo = Request.QueryString.Get("iVoucherNo");
                    if (iVoucherNo != null)
                    {
                        btnadd.Text = "Save";
                        hidfnwe.Value = "New";
                        DataSet DR = null;// objBs.getReceiptvouchermaster("tblReceiptVoucher_" + sTableName, Convert.ToString(iVoucherNo));
                        if (DR.Tables[0].Rows.Count > 0)
                        {
                            rblist.SelectedValue = DR.Tables[0].Rows[0]["rbList"].ToString();
                            //txtreceiptid.Text = DR.Tables[0].Rows[0]["VoucherID"].ToString();
                            //txtreceiptno.Text = DR.Tables[0].Rows[0]["VoucherNo"].ToString();
                            //txtAgainst.Text = DR.Tables[0].Rows[0]["Against"].ToString();

                            DataSet ds12 = objBs.Receiptno("tblReceipt_" + sTableName);
                            if (ds12.Tables[0].Rows.Count > 0)
                            {
                                // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                                if (ds12.Tables[0].Rows[0]["ReceiptNo"].ToString() == "")
                                    txtreceiptno.Text = "1";
                                else
                                    txtreceiptno.Text = ds12.Tables[0].Rows[0]["ReceiptNo"].ToString();

                                txtreceiptdate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                            }
                            txtNarration.Text = "Receipt No: " + txtreceiptno.Text;

                           // txtreceiptdate.Text = Convert.ToDateTime(DR.Tables[0].Rows[0]["VoucherDate"]).ToString("dd/MM/yyyy");

                            DataSet dst = new DataSet();
                            
                            ddlcustomerID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();
                           
                            ddlbankID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();

                            //drpEmployee.SelectedValue = DR.Tables[0].Rows[0]["EmployeeID"].ToString();

                            drpGsttype.SelectedValue = DR.Tables[0].Rows[0]["GSTType"].ToString();
                            ddltax.SelectedValue = DR.Tables[0].Rows[0]["GSTTax"].ToString();
                            ddlProvince.SelectedValue = DR.Tables[0].Rows[0]["Province"].ToString();
                            txtgstAmount.Text = DR.Tables[0].Rows[0]["Total"].ToString();
                            //txtCGST.Text = DR.Tables[0].Rows[0]["CGST"].ToString();
                           // txtSGST.Text = DR.Tables[0].Rows[0]["SGST"].ToString();
                            txtIGST.Text = DR.Tables[0].Rows[0]["IGST"].ToString();
                            txtgstAmount_TextChanged(sender, e);

                            if (rblist.SelectedValue == "1")
                            {
                                rblist.SelectedValue = "1";
                                rblist.Enabled = false;

                                lblcustomer.Visible = true;
                                ddlcustomerID.Visible = true;
                                lblbank.Visible = false;
                                ddlbankID.Visible = false;


                                ddlType.SelectedValue = DR.Tables[0].Rows[0]["AgentID"].ToString();
                                ddlcustomerID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();


                                dst = objBs.getLedger_New(lblContactTypeId.Text);
                                if (dstd != null)
                                {
                                    if (dst.Tables[0].Rows.Count > 0)
                                    {
                                        ddlcustomerID.DataSource = dst.Tables[0];
                                        ddlcustomerID.DataTextField = "LedgerName";
                                        ddlcustomerID.DataValueField = "LedgerID";
                                        ddlcustomerID.DataBind();
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                    else
                                    {
                                        ddlcustomerID.Items.Insert(0, "Select customer");
                                    }
                                }
                                else
                                {
                                    ddlcustomerID.Items.Insert(0, "Select customer");
                                }

                                //DataSet dsCustDet = objBs.GetCustomerDetails(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                                //if (dsCustDet.Tables[0].Rows.Count > 0)
                                //{
                                //    txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                                //    txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                                //    txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                                //    txtarea.Text = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                                //}
                            }
                            else
                            {
                                //rblist.SelectedValue = "3";
                                //rblist.Enabled = false;

                                //lblcustomer.Visible = false;
                                //ddlcustomerID.Visible = false;
                                //lblbank.Visible = true;
                                //ddlbankID.Visible = true;
                                //ddlType.Enabled = false;

                                //dst = objBs.selectVendor(2, sTableName);
                                //if (dst.Tables[0].Rows.Count > 0)
                                //{
                                //    ddlbankID.DataSource = dst.Tables[0];
                                //    ddlbankID.DataTextField = "LedgerName";
                                //    ddlbankID.DataValueField = "LedgerID";
                                //    ddlbankID.DataBind();
                                //    ddlbankID.Items.Insert(0, "Select Supplier Name");
                                //}
                                //else
                                //{
                                //    ddlbankID.Items.Insert(0, "Select Supplier Name");
                                //}

                                //ddlbankID.SelectedValue = DR.Tables[0].Rows[0]["LedgerID"].ToString();
                                //string agent_ID = "0";
                            }
                            ddlcustomerID_SelectedIndexChanged(sender, e);
                            //txtNarration.Text = DR.Tables[0].Rows[0]["Narration"].ToString();
                            txtAmount.Text = Convert.ToDouble(DR.Tables[0].Rows[0]["Amount"]).ToString("f2");
                            txtgstAmount_TextChanged(sender, e);

                            //  txtTransNo.Text = DR.Tables[0].Rows[0]["TransNo"].ToString();
                        }
                    }
                }

                txtreceiptdate.Focus();
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void billno(object sender, EventArgs e)
        {

            if (txtAgainst.Text == "" || txtAgainst.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Valid BillNo!!.Thank You');", true);
                return;
            }
            else
            {


                DataSet ds = objBs.GetCreditSalesbybillno((Convert.ToInt32(ddlcustomerID.SelectedValue)), "tblSales_" + sTableName, "tblDayBook_" + sTableName, txtAgainst.Text);

                DataSet dsReceiptDet = objBs.GetReceiptDetails("tblDayBook_" + sTableName, "tblTransReceipt_" + sTableName, "tblReceipt_" + sTableName);


                if (ds != null)
                {
                    foreach (DataRow dr in dsReceiptDet.Tables[0].Rows)
                    {
                        var billNo = dr["BillNo"].ToString();
                        var billAmount = dr["TotalAmount"].ToString();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (billNo.Trim() == ds.Tables[0].Rows[i]["BillNo"].ToString())
                            {
                                ds.Tables[0].Rows[i].BeginEdit();
                                double val = (double.Parse(ds.Tables[0].Rows[i]["Balance"].ToString()) - double.Parse(billAmount));
                                ds.Tables[0].Rows[i]["Balance"] = val;
                                ds.Tables[0].Rows[i].EndEdit();

                                if (val == 0.0)
                                    ds.Tables[0].Rows[i].Delete();
                            }
                        }
                        ds.Tables[0].AcceptChanges();
                    }
                }

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dttt;
                        DataRow drNew;
                        DataColumn dct;
                        DataSet dstd = new DataSet();
                        dttt = new DataTable();

                        dct = new DataColumn("SalesID");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Billno");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Amount");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("BillDate");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Balance");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("BillAmount");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    drNew = dttt.NewRow();
                                    drNew["BillDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["BillDate"]).ToString("dd/MM/yyyy");
                                    drNew["Billno"] = Convert.ToInt32(ds.Tables[0].Rows[i]["Billno"]);
                                    drNew["SalesID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["SalesID"]);
                                    drNew["BillAmount"] = Convert.ToDouble(ds.Tables[0].Rows[i]["BillAmount"]);
                                    drNew["Balance"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"]);
                                    drNew["Amount"] = 0;
                                    dstd.Tables[0].Rows.Add(drNew);
                                }
                                gvledgrid.DataSource = dstd.Tables[0];
                                gvledgrid.DataBind();
                                gvledgrid.Visible = true;
                            }
                        }
                        else
                        {
                            gvledgrid.DataSource = null;
                            gvledgrid.DataBind();
                            gvledgrid.Visible = true;
                        }

                    }
                    else
                    {
                        gvledgrid.DataSource = null;
                        gvledgrid.DataBind();
                        gvledgrid.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Valid BillNo!!.For This Customer.Thank You');", true);
                        return;
                    }

                }
                else
                {
                    gvledgrid.DataSource = null;
                    gvledgrid.DataBind();
                    gvledgrid.Visible = true;
                }
            }

        }

        protected void txtreceiptnochanged(object sender, EventArgs e)
        {
            if (btnadd.Text == "Save")
            {
                if (txtreceiptno.Text == "")
                {
                    txtreceiptno.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Receipt No!!!');", true);
                    return;
                }
                else
                {
                    //DataSet drec = objBs.duplicatenoforreciept(txtreceiptno.Text, sTableName, "Save", "0");
                    //if (drec.Tables[0].Rows.Count > 0)
                    //{
                    //    txtreceiptno.Focus();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Receipt No Already Exists.Thank You!!!');", true);
                    //    return;
                    //}
                    //else
                    //{
                    //    txtreceiptdate.Focus();
                    //}
                }
            }
            else
            {
                if (txtreceiptno.Text == "")
                {
                    txtreceiptno.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Receipt No!!!');", true);
                    return;
                }
                else
                {
                    //DataSet drec = objBs.duplicatenoforreciept(txtreceiptno.Text, sTableName, "Update", txtreceiptid.Text);
                    //if (drec.Tables[0].Rows.Count > 0)
                    //{
                    //    txtreceiptno.Focus();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Receipt No Already Exists.Thank You!!!');", true);
                    //    return;
                    //}
                    //else
                    //{
                    //    txtreceiptdate.Focus();
                    //}
                }
            }
        }

        protected void drpGsttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtgstAmount_TextChanged(sender, e);
        }

        protected void ddlcustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcustomerID.SelectedValue == "Select customer")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Customer Name');", true);
                return;
            }
            else
            {
                txtAgainst.Text = "";
                //DataSet dsCustDet = objBs.LedgerDetailsList(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                //if (dsCustDet.Tables[0].Rows.Count > 0)
                //{
                //    txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                //    txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                //    txtcity.Text = dsCustDet.Tables[0].Rows[0]["City1"].ToString();
                //    txtarea.Text = dsCustDet.Tables[0].Rows[0]["Area1"].ToString();
                //    //txtpincode.Text = dsCustDet.Tables[0].Rows[0]["Pincode"].ToString();
                //    //txtcuscode.Text = dsCustDet.Tables[0].Rows[0]["CustomerID"].ToString();
                //}
                //if (ddlType.SelectedValue == "Select Agent Name")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Agent Name');", true);
                //    return;
                //}

                if ((ddlType.SelectedValue != "Select Agent Name"))
                {

                    // DataSet ds = new DataSet();

                    //DataSet dsdd = objBs.selectBranch();
                    //if (dsdd != null)
                    //{
                    //    foreach (DataRow drd in dsdd.Tables[0].Rows)
                    //    {
                    //DataSet ds = objBs.GetCreditSales((Convert.ToInt32(ddlcustomerID.SelectedValue)), "tblSales_" + drd["Branchcode"], "tblDayBook_" + drd["Branchcode"], drd["Branchcode"].ToString(), drd["city"].ToString());

                    //DataSet dsReceiptDet = objBs.GetReceiptDetails("tblDayBook_" + drd["Branchcode"], "tblTransReceipt_" + drd["Branchcode"], "tblReceipt_" + drd["Branchcode"]);
                    DataSet ds = objBs.GetCreditSales((Convert.ToInt32(ddlcustomerID.SelectedValue)), "tblBuyerOrderSales", "tblDayBook_" + sTableName, sTableName.ToString(), sTableName);

                    DataSet dsReceiptDet = objBs.GetReceiptDetails("tblDayBook_" + sTableName, "tblTransReceipt_" + sTableName, "tblReceipt_" + sTableName);


                    if (ds != null)
                    {
                        foreach (DataRow dr in dsReceiptDet.Tables[0].Rows)
                        {
                            var billNo = dr["BillNo"].ToString();
                            var billAmount = dr["TotalAmount"].ToString();

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (billNo.ToString() == ds.Tables[0].Rows[i]["BillNo"].ToString())
                                {
                                    ds.Tables[0].Rows[i].BeginEdit();
                                    double val = (double.Parse(ds.Tables[0].Rows[i]["Balance"].ToString()) - double.Parse(billAmount));
                                    ds.Tables[0].Rows[i]["Balance"] = val;
                                    ds.Tables[0].Rows[i].EndEdit();

                                    if (val == 0.0)
                                        ds.Tables[0].Rows[i].Delete();
                                }
                            }
                            ds.Tables[0].AcceptChanges();
                        }
                    }

                    //        ds.Merge(dsd);
                    //    }
                    //}

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dttt;
                            DataRow drNew;
                            DataColumn dct;
                            DataSet dstd = new DataSet();
                            dttt = new DataTable();

                            //dct = new DataColumn("Branch");
                            //dttt.Columns.Add(dct);

                            dct = new DataColumn("Branchcode");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("SalesID");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Billno");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Amount");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("BillDate");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Balance");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("BillAmount");
                            dttt.Columns.Add(dct);

                            dstd.Tables.Add(dttt);

                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        drNew = dttt.NewRow();

                                        drNew["BillDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString("dd/MM/yyyy");
                                        drNew["SalesID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["Buyerordersalesid"]);
                                        drNew["Billno"] = Convert.ToString(ds.Tables[0].Rows[i]["Billno"]);
                                        drNew["BillAmount"] = Convert.ToDouble(ds.Tables[0].Rows[i]["BillAmount"]).ToString("0.00");
                                        drNew["Balance"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"]).ToString("0.00");
                                        drNew["Branchcode"] = Convert.ToString(ds.Tables[0].Rows[i]["Branchcode"]);
                                        //drNew["Branch"] = Convert.ToString(ds.Tables[0].Rows[i]["Branch"]);
                                        drNew["Amount"] = 0;
                                        dstd.Tables[0].Rows.Add(drNew);
                                    }
                                    gvledgrid.DataSource = dstd.Tables[0];
                                    gvledgrid.DataBind();
                                    gvledgrid.Visible = true;
                                }
                            }
                            else
                            {
                                gvledgrid.DataSource = null;
                                gvledgrid.DataBind();
                                gvledgrid.Visible = true;
                            }

                        }
                        else
                        {
                            gvledgrid.DataSource = null;
                            gvledgrid.DataBind();
                            gvledgrid.Visible = true;
                        }

                    }
                    else
                    {
                        gvledgrid.DataSource = null;
                        gvledgrid.DataBind();
                        gvledgrid.Visible = true;
                    }
                }
                else
                {
                    gvledgrid.Visible = false;

                }
            }
            txtAmount.Focus();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsCustomer = new DataSet();
            if (ddlType.SelectedValue == "Select Agent")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Agent Name');", true);
                return;
            }
            else
            // if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                lbltype.Text = "Customer Receipt";
               
                txtAgainst.Text = "";
                txtAgainst.Visible = true;
                // lblAganist.Visible = false;
                gvledgrid.DataSource = null;
                gvledgrid.DataBind();
                gvledgrid.Visible = true;
            }
            //else if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            //{
            //    lbltype.Text = "Vendor Receipt";
            //    dsCustomer = objBs.GetLedgernames(2,hidfnwe.Value);
            //    txtAgainst.Visible = true;
            //   // lblAganist.Visible = true;
            //    gvledgrid.DataSource = null;
            //    gvledgrid.DataBind();
            //    gvledgrid.Visible = false;
            //}
            //else if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            //{
            //    lbltype.Text = "Expense Receipt";
            //    dsCustomer = objBs.GetLedgernames(3,hidfnwe.Value);
            //    txtAgainst.Visible = true;
            //   // lblAganist.Visible = true;
            //    gvledgrid.DataSource = null;
            //    gvledgrid.DataBind();
            //    gvledgrid.Visible = false;
            //}
            //else if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            //{
            //    lbltype.Text = "Bank Receipt";
            //    dsCustomer = objBs.GetLedgernames(4,hidfnwe.Value);
            //    txtAgainst.Visible = true;
            //   // lblAganist.Visible = true;
            //    gvledgrid.DataSource = null;
            //    gvledgrid.DataBind();
            //   gvledgrid.Visible = false;
            //}

            if (rblist.SelectedValue == "1")
            {
                dsCustomer = objBs.getLedger_New(lblContactTypeId.Text);
                if (dsCustomer.Tables[0].Rows.Count > 0)
                {
                    ddlcustomerID.DataSource = dsCustomer.Tables[0];
                    ddlcustomerID.DataTextField = "LedgerName";
                    ddlcustomerID.DataValueField = "LedgerID";
                    ddlcustomerID.DataBind();
                    ddlcustomerID.Items.Insert(0, "Select customer");
                }
            }
            else if (rblist.SelectedValue == "5")
            {
                //dsCustomer = objBs.GetLedgernamesnew1(1, hidfnwe.Value, ddlType.SelectedValue, sTableName, rblist.SelectedValue);
                //if (dsCustomer.Tables[0].Rows.Count > 0)
                //{
                //    ddlcustomerID.DataSource = dsCustomer.Tables[0];
                //    ddlcustomerID.DataTextField = "LedgerName";
                //    ddlcustomerID.DataValueField = "LedgerID";
                //    ddlcustomerID.DataBind();
                //    ddlcustomerID.Items.Insert(0, "Select customer");
                //}
            }
            else if (rblist.SelectedValue == "65")
            {
                //dsCustomer = objBs.GetLedgernamesnew1(1, hidfnwe.Value, ddlType.SelectedValue, sTableName, rblist.SelectedValue);
                //if (dsCustomer.Tables[0].Rows.Count > 0)
                //{
                //    ddlcustomerID.DataSource = dsCustomer.Tables[0];
                //    ddlcustomerID.DataTextField = "LedgerName";
                //    ddlcustomerID.DataValueField = "LedgerID";
                //    ddlcustomerID.DataBind();
                //    ddlcustomerID.Items.Insert(0, "Select customer");
                //}
            }
            else if (rblist.SelectedValue == "6")
            {
                //dsCustomer = objBs.GetLedgernamesnew1(1, hidfnwe.Value, "0", sTableName, "3");
                //if (dsCustomer.Tables[0].Rows.Count > 0)
                //{
                //    ddlcustomerID.DataSource = dsCustomer.Tables[0];
                //    ddlcustomerID.DataTextField = "LedgerName";
                //    ddlcustomerID.DataValueField = "LedgerID";
                //    ddlcustomerID.DataBind();
                //    ddlcustomerID.Items.Insert(0, "Select customer");
                //}
            }
            else if (rblist.SelectedValue == "7")
            {
                //dsCustomer = objBs.GetLedgernamesnew1(1, hidfnwe.Value, "0", sTableName, "7");
                //if (dsCustomer.Tables[0].Rows.Count > 0)
                //{
                //    ddlcustomerID.DataSource = dsCustomer.Tables[0];
                //    ddlcustomerID.DataTextField = "LedgerName";
                //    ddlcustomerID.DataValueField = "LedgerID";
                //    ddlcustomerID.DataBind();
                //    ddlcustomerID.Items.Insert(0, "Select customer");
                //}
            }
            ddlcustomerID.Focus();
        }

        protected void oldddlcustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcustomerID.SelectedValue == "Select Customer")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Customer Name');", true);
                return;
            }
            else
            {
                txtAgainst.Text = "";
                DataSet dsCustDet = objBs.LedgerDetailsList(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                if (dsCustDet.Tables[0].Rows.Count > 0)
                {
                    txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                    txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                    txtcity.Text = dsCustDet.Tables[0].Rows[0]["City1"].ToString();
                    txtarea.Text = dsCustDet.Tables[0].Rows[0]["Area1"].ToString();
                    //txtpincode.Text = dsCustDet.Tables[0].Rows[0]["Pincode"].ToString();
                    //txtcuscode.Text = dsCustDet.Tables[0].Rows[0]["CustomerID"].ToString();
                }




                DataSet dsCustomer = new DataSet();
                if (ddlcustomerID.SelectedValue == "Select Customer")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Customer Name');", true);
                    return;
                }
                else
                // if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    lbltype.Text = "Customer Receipt";
                   // dsCustomer = objBs.getledgeragentname(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                    txtAgainst.Text = "";
                    txtAgainst.Visible = true;
                    // lblAganist.Visible = false;
                    gvledgrid.DataSource = null;
                    gvledgrid.DataBind();
                    gvledgrid.Visible = true;
                }
                //else if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                //{
                //    lbltype.Text = "Vendor Receipt";
                //    dsCustomer = objBs.GetLedgernames(2,hidfnwe.Value);
                //    txtAgainst.Visible = true;
                //   // lblAganist.Visible = true;
                //    gvledgrid.DataSource = null;
                //    gvledgrid.DataBind();
                //    gvledgrid.Visible = false;
                //}
                //else if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                //{
                //    lbltype.Text = "Expense Receipt";
                //    dsCustomer = objBs.GetLedgernames(3,hidfnwe.Value);
                //    txtAgainst.Visible = true;
                //   // lblAganist.Visible = true;
                //    gvledgrid.DataSource = null;
                //    gvledgrid.DataBind();
                //    gvledgrid.Visible = false;
                //}
                //else if (Convert.ToInt32(ddlType.SelectedValue) == 4)
                //{
                //    lbltype.Text = "Bank Receipt";
                //    dsCustomer = objBs.GetLedgernames(4,hidfnwe.Value);
                //    txtAgainst.Visible = true;
                //   // lblAganist.Visible = true;
                //    gvledgrid.DataSource = null;
                //    gvledgrid.DataBind();
                //   gvledgrid.Visible = false;
                //}

                if (dsCustomer.Tables[0].Rows.Count > 0)
                {
                    ddlType.DataSource = dsCustomer.Tables[0];
                    ddlType.DataTextField = "LedgerName";
                    ddlType.DataValueField = "LedgerID";
                    ddlType.DataBind();
                    ddlType.Items.Insert(0, "Select Agent Name");
                }


                //if (ddlType.SelectedValue == "Select Agent Name")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Agent Name');", true);
                //    return;
                //}

                //if ((ddlType.SelectedValue != "Select Agent Name"))
                //{


                //    DataSet ds = objBs.GetCreditSales((Convert.ToInt32(ddlcustomerID.SelectedValue)), "tblSales_" + sTableName, "tblDayBook_" + sTableName);

                //    DataSet dsReceiptDet = objBs.GetReceiptDetails("tblDayBook_" + sTableName, "tblTransReceipt_" + sTableName, "tblReceipt_" + sTableName);


                //    if (ds != null)
                //    {
                //        foreach (DataRow dr in dsReceiptDet.Tables[0].Rows)
                //        {
                //            var billNo = dr["BillNo"].ToString();
                //            var billAmount = dr["TotalAmount"].ToString();

                //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //            {
                //                if (billNo.Trim() == ds.Tables[0].Rows[i]["BillNo"].ToString())
                //                {
                //                    ds.Tables[0].Rows[i].BeginEdit();
                //                    double val = (double.Parse(ds.Tables[0].Rows[i]["Balance"].ToString()) - double.Parse(billAmount));
                //                    ds.Tables[0].Rows[i]["Balance"] = val;
                //                    ds.Tables[0].Rows[i].EndEdit();

                //                    if (val == 0.0)
                //                        ds.Tables[0].Rows[i].Delete();
                //                }
                //            }
                //            ds.Tables[0].AcceptChanges();
                //        }
                //    }

                //    if (ds != null)
                //    {
                //        if (ds.Tables[0].Rows.Count > 0)
                //        {
                //            DataTable dttt;
                //            DataRow drNew;
                //            DataColumn dct;
                //            DataSet dstd = new DataSet();
                //            dttt = new DataTable();

                //            dct = new DataColumn("SalesID");
                //            dttt.Columns.Add(dct);

                //            dct = new DataColumn("Billno");
                //            dttt.Columns.Add(dct);

                //            dct = new DataColumn("Amount");
                //            dttt.Columns.Add(dct);

                //            dct = new DataColumn("BillDate");
                //            dttt.Columns.Add(dct);

                //            dct = new DataColumn("Balance");
                //            dttt.Columns.Add(dct);

                //            dct = new DataColumn("BillAmount");
                //            dttt.Columns.Add(dct);

                //            dstd.Tables.Add(dttt);

                //            if (ds != null)
                //            {
                //                if (ds.Tables[0].Rows.Count > 0)
                //                {
                //                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //                    {
                //                        drNew = dttt.NewRow();

                //                        drNew["BillDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["BillDate"]).ToString("dd/MM/yyyy");
                //                        drNew["SalesID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["SalesID"]);
                //                        drNew["Billno"] = Convert.ToInt32(ds.Tables[0].Rows[i]["Billno"]);
                //                        drNew["BillAmount"] = Convert.ToDouble(ds.Tables[0].Rows[i]["BillAmount"]);
                //                        drNew["Balance"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"]);
                //                        drNew["Amount"] = 0;
                //                        dstd.Tables[0].Rows.Add(drNew);
                //                    }
                //                    gvledgrid.DataSource = dstd.Tables[0];
                //                    gvledgrid.DataBind();
                //                    gvledgrid.Visible = true;
                //                }
                //            }
                //            else
                //            {
                //                gvledgrid.DataSource = null;
                //                gvledgrid.DataBind();
                //                gvledgrid.Visible = true;
                //            }

                //        }
                //        else
                //        {
                //            gvledgrid.DataSource = null;
                //            gvledgrid.DataBind();
                //            gvledgrid.Visible = true;
                //        }

                //    }
                //    else
                //    {
                //        gvledgrid.DataSource = null;
                //        gvledgrid.DataBind();
                //        gvledgrid.Visible = true;
                //    }
                //}
                //else
                //{
                //    gvledgrid.Visible = false;

                //}
            }
        }

        
        protected void btnUpload_Clickimg(object sender, EventArgs e)
        {
            if (fp_Upload.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
                fp_Upload.PostedFile.SaveAs(Server.MapPath("~/Files/") + fileName);
                lblFile_Path.Text = "~/Files/" + fp_Upload.PostedFile.FileName;
                img_Photo.ImageUrl = "~/Files/" + fp_Upload.PostedFile.FileName;
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("LoginID", typeof(string)));
            dtt.Columns.Add(new DataColumn("SIMNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("IMEINo", typeof(string)));
            dtt.Columns.Add(new DataColumn("PlateNo", typeof(string)));


            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["LoginID"] = string.Empty;
            dr["SIMNo"] = string.Empty;
            dr["IMEINo"] = string.Empty;
            dr["PlateNo"] = string.Empty;

            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            grdVehno.DataSource = dtt;
            grdVehno.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("LoginID");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SIMNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("IMEINo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("PlateNo");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();

            drNew["OrderNo"] = "1";
            drNew["LoginID"] = "";
            drNew["SIMNo"] = "";
            drNew["IMEINo"] = "";
            drNew["PlateNo"] = "0";

            dstd.Tables[0].Rows.Add(drNew);

            grdVehno.DataSource = dstd;
            grdVehno.DataBind();
        }

        protected void grdVehno_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();

            if (ViewState["CurrentTable1"] != null)
            {
                DataSet ds = new DataSet();
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    ds.Merge(dt);

                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();

                    ViewState["CurrentTable1"] = dt;
                    grdVehno.DataSource = dt;
                    grdVehno.DataBind();

                    SetPreviousData();

                    for (int i = 0; i < grdVehno.Rows.Count; i++)
                    {
                        TextBox txtno = (TextBox)grdVehno.Rows[i].FindControl("txtno");
                        txtno.Text = Convert.ToString(i + 1);
                    }

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    grdVehno.DataSource = dt;
                    grdVehno.DataBind();

                    SetPreviousData();
                    FirstGridViewRow();
                }
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            ButtonAdd1_Click(sender, e);
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;
            int tottqty = 0;
            double mettt = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < grdVehno.Rows.Count; vLoop++)
            {
                TextBox txtLoginID = (TextBox)grdVehno.Rows[vLoop].FindControl("txtLoginID");
                TextBox txtIMEIno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtIMEIno");
                TextBox txtPlateno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtPlateno");

                if (txtLoginID.Text == "" || txtIMEIno.Text == "" || txtPlateno.Text == "")
                {

                }
                else
                {
                }

            }

            for (int vLoop = 0; vLoop < grdVehno.Rows.Count; vLoop++)
            {
                int cnt = grdVehno.Rows.Count;
                TextBox txtLoginID = (TextBox)grdVehno.Rows[vLoop].FindControl("txtLoginID");
                TextBox txtIMEIno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtIMEIno");
                if (vLoop >= 1)
                {
                    TextBox oldtxtPlateno = (TextBox)grdVehno.Rows[vLoop - 1].FindControl("txtLoginID");

                    oldtxtPlateno.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxtPlateno = (TextBox)grdVehno.Rows[vLoop - 1].FindControl("txtLoginID");
                    oldtxtPlateno.Focus();

                }
            }


            for (int i = 0; i < grdVehno.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)grdVehno.Rows[i].FindControl("txtno");
                TextBox txtLoginID = (TextBox)grdVehno.Rows[i].FindControl("txtLoginID");
                txtno.Text = Convert.ToString(i + 1);
                txtLoginID.Focus();
            }
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < grdVehno.Rows.Count; vLoop++)
            {

                TextBox txtLoginID = (TextBox)grdVehno.Rows[vLoop].FindControl("txtLoginID");
                TextBox txtIMEIno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtIMEIno");

                if (txtLoginID.Text == "" || txtLoginID.Text == "")
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
            for (int vLoop = 0; vLoop < grdVehno.Rows.Count; vLoop++)
            {
                TextBox txtLoginID = (TextBox)grdVehno.Rows[vLoop].FindControl("txtLoginID");
                TextBox txtIMEIno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtIMEIno");
                txtLoginID.Focus();
            }
        }

        private void AddNewRow()
        {
            for (int vLoop = 0; vLoop < grdVehno.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtno");
                TextBox txtLoginID = (TextBox)grdVehno.Rows[vLoop].FindControl("txtLoginID");
                TextBox txtSIMno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtSIMno");
                TextBox txtIMEIno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtIMEIno");
                TextBox txtPlateno = (TextBox)grdVehno.Rows[vLoop].FindControl("txtPlateno");
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
                        TextBox txtno =
                          (TextBox)grdVehno.Rows[rowIndex].Cells[0].FindControl("txtno");
                        TextBox txtLoginID =
                          (TextBox)grdVehno.Rows[rowIndex].Cells[1].FindControl("txtLoginID");
                        TextBox txtSIMno =
                         (TextBox)grdVehno.Rows[rowIndex].Cells[1].FindControl("txtSIMno");
                        TextBox txtIMEIno =
                          (TextBox)grdVehno.Rows[rowIndex].Cells[2].FindControl("txtIMEIno");
                        TextBox txtPlateno =
                          (TextBox)grdVehno.Rows[rowIndex].Cells[3].FindControl("txtPlateno");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["OrderNo"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["LoginID"] = txtLoginID.Text;
                        dtCurrentTable.Rows[i - 1]["SIMNo"] = txtSIMno.Text;
                        dtCurrentTable.Rows[i - 1]["IMEINo"] = txtIMEIno.Text;
                        dtCurrentTable.Rows[i - 1]["PlateNo"] = txtPlateno.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    grdVehno.DataSource = dtCurrentTable;
                    grdVehno.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
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
                        TextBox txtno =
                          (TextBox)grdVehno.Rows[rowIndex].Cells[0].FindControl("txtno");
                        TextBox txtLoginID =
                          (TextBox)grdVehno.Rows[rowIndex].Cells[1].FindControl("txtLoginID");
                        TextBox txtSIMno =
                        (TextBox)grdVehno.Rows[rowIndex].Cells[1].FindControl("txtSIMno");
                        TextBox txtIMEIno =
                         (TextBox)grdVehno.Rows[rowIndex].Cells[2].FindControl("txtIMEIno");
                        TextBox txtPlateno =
                        (TextBox)grdVehno.Rows[rowIndex].Cells[3].FindControl("txtPlateno");


                        txtno.Text = dt.Rows[i]["OrderNo"].ToString();
                        txtLoginID.Text = dt.Rows[i]["LoginID"].ToString();
                        txtSIMno.Text = dt.Rows[i]["SIMNo"].ToString();
                        txtIMEIno.Text = dt.Rows[i]["IMEINo"].ToString();
                        txtPlateno.Text = dt.Rows[i]["PlateNo"].ToString();

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
                        TextBox txtno =
                          (TextBox)grdVehno.Rows[rowIndex].Cells[0].FindControl("txtno");
                        TextBox txtLoginID =
                          (TextBox)grdVehno.Rows[rowIndex].Cells[1].FindControl("txtLoginID");
                        TextBox txtSIMno =
                         (TextBox)grdVehno.Rows[rowIndex].Cells[1].FindControl("txtSIMno");
                        TextBox txtIMEIno =
                         (TextBox)grdVehno.Rows[rowIndex].Cells[2].FindControl("txtIMEIno");
                        TextBox txtPlateno =
                         (TextBox)grdVehno.Rows[rowIndex].Cells[3].FindControl("txtPlateno");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["LoginID"] = txtLoginID.Text;
                        dtCurrentTable.Rows[i - 1]["SIMNo"] = txtSIMno.Text;
                        dtCurrentTable.Rows[i - 1]["IMEINo"] = txtIMEIno.Text;
                        dtCurrentTable.Rows[i - 1]["PlateNo"] = txtPlateno.Text;

                        rowIndex++;

                    }
                    ViewState["CurrentTable1"] = dtCurrentTable;
                    grdVehno.DataSource = dtCurrentTable;
                    grdVehno.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void Add_Click(object sender, EventArgs e)
        {

            double TAXVALUE = 0;

            if (ddltax.SelectedItem.Text == "Select Tax")
            {
                TAXVALUE = 0;
            }
            else
            {
                TAXVALUE = Convert.ToDouble(ddltax.SelectedValue);
            }

            string agent = "";
            string bank = "";
            string bankID = "";
            string agenttext = "";
            DateTime recdate;
            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("Branchcode");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SalesID");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Billno");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Amount");
            dttt.Columns.Add(dct);

            dct = new DataColumn("BillDate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("BillAmount");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Balance");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            for (int vLoop = 0; vLoop < gvledgrid.Rows.Count; vLoop++)
            {
                Label txtdd = (Label)gvledgrid.Rows[vLoop].FindControl("txtBranchcode");
                Label txtd = (Label)gvledgrid.Rows[vLoop].FindControl("txtSalesid");
                Label txttt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillno");
                if (btnadd.Text == "Update")
                {
                    Label txt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillDate");
                    string dat = Convert.ToDateTime(txt.Text).ToString("dd/MM/yyyy");
                    recdate = DateTime.ParseExact(dat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    Label txt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillDate");
                    recdate = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                Label txttd = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillAmount");
                Label txttd123 = (Label)gvledgrid.Rows[vLoop].FindControl("txtBalance");
                TextBox txttdtt = (TextBox)gvledgrid.Rows[vLoop].FindControl("txtAmount");

                drNew = dttt.NewRow();
                drNew["Branchcode"] = txtdd.Text;
                drNew["SalesID"] = txtd.Text;
                drNew["Billno"] = txttt.Text;
                drNew["BillAmount"] = txttd.Text;
                drNew["Balance"] = txttd123.Text;
                drNew["BillDate"] = recdate;
                drNew["Amount"] = Convert.ToDouble(txttdtt.Text);
                if (Convert.ToDouble(txttdtt.Text) > 0)
                {
                    dstd.Tables[0].Rows.Add(drNew);
                }
                decimal amount;
                decimal balance;
                decimal dTotal = 0;
                decimal Dtotal = 0;

                amount = Convert.ToDecimal(txttdtt.Text);
                balance = Convert.ToDecimal(txttd123.Text);

                if (amount > balance)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Amount is  greater-than  Balance!');", true);

                    return;
                }

                if (dstd.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dstd.Tables[0].Rows.Count; i++)
                    {
                        dTotal += Convert.ToDecimal(dstd.Tables[0].Rows[i]["Amount"].ToString());
                    }
                    Dtotal = dTotal;
                    if (Dtotal > Convert.ToDecimal(txtAmount.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Totalamount is  greater-than  Amount!');", true);
                        return;
                    }
                }


            }

           

            DateTime date = DateTime.ParseExact(txtreceiptdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtreceiptnochanged(sender, e);
            if (btnadd.Text == "Save")
            {
                if (ddmodeofpayment.SelectedItem.Text == "DD" || ddmodeofpayment.SelectedItem.Text == "Cheque" || ddmodeofpayment.SelectedValue == "4")
                {
                    if (ddlBank.SelectedValue == "Select Bank Name" && txtChequeno.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No/UTR!');", true);

                        txtChequeno.Focus();
                        ddlBank.Focus();
                        return;

                    }
                    else if (ddlBank.SelectedValue == "Select Bank Name")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name!');", true);
                        ddlBank.Focus();
                        return;

                    }
                    else if (txtChequeno.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Cheque No/UTR!');", true);
                        txtChequeno.Focus();
                        return;

                    }
                }
                if (ddmodeofpayment.SelectedValue == "5")
                {
                    if (ddlBank.SelectedValue == "Select Bank Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name !');", true);


                        ddlBank.Focus();
                        return;

                    }
                }
                if (rblist.SelectedValue == "1")
                {
                    if (ddlType.SelectedValue == "Select Agent")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Agent !');", true);


                        ddlBank.Focus();
                        return;

                    }
                }

               

                int bankid = 0;

                if (Convert.ToInt32(ddmodeofpayment.SelectedValue) == 1)
                {
                    bankid = 0;
                }
                else if (Convert.ToInt32(ddmodeofpayment.SelectedValue) == 5)
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                }
                else
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                }

                double dBalance = 0, dTotal = 0;

                if (rblist.SelectedValue == "1")
                {
                    agent = ddlType.SelectedValue;
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = ddlType.SelectedItem.Text;
                }
                else if (rblist.SelectedValue == "5")
                {
                    agent = ddlType.SelectedValue;
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = ddlType.SelectedItem.Text;
                }
                else if (rblist.SelectedValue == "6")
                {
                    agent = ddlType.SelectedValue;
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = ddlType.SelectedItem.Text;
                }
                else if (rblist.SelectedValue == "7")
                {
                    agent = ddlType.SelectedValue;
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = ddlType.SelectedItem.Text;
                }
                else if (rblist.SelectedValue == "4")
                {
                    agent = "0";
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = null;
                }
                else
                {
                    agent = "0";
                    bank = ddlbankID.SelectedItem.Text;
                    bankID = ddlbankID.SelectedValue;
                    agenttext = null;
                }

                //  DateTime EntryDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                //  DateTime EditDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                string narration = string.Empty;
                narration = txtNarration.Text;

                for (int i = 0; i < gvledgrid.Rows.Count; i++)
                {
                    Label txtBillNo = (Label)gvledgrid.Rows[i].FindControl("txtBillNo");
                    Label txtBillDate = (Label)gvledgrid.Rows[i].FindControl("txtBillDate");
                    TextBox txtAmount1 = (TextBox)gvledgrid.Rows[i].FindControl("txtAmount");

                    if ((txtAmount1.Text == "") || (txtAmount1.Text == "0"))
                    {

                    }
                    else
                    {
                        narration = narration + "(InvoiceNo: " + txtBillNo.Text.ToString();
                        narration = narration + "-InvoiceDate: " + txtBillDate.Text.ToString();
                        narration = narration + "-Amount: " + txtAmount1.Text + ")";
                    }
                }


                for (int i = 0; i < grdVehno.Rows.Count; i++)
                {
                    TextBox txtLoginID = (TextBox)grdVehno.Rows[i].FindControl("txtLoginID");
                    TextBox txtIMEIno = (TextBox)grdVehno.Rows[i].FindControl("txtIMEIno");
                    TextBox txtPlateno = (TextBox)grdVehno.Rows[i].FindControl("txtPlateno");
                    TextBox txtSIMno = (TextBox)grdVehno.Rows[i].FindControl("txtSIMno");

                    if (txtLoginID.Text == "")
                    {

                    }
                    else
                    {                       
                        narration = narration + "(LoginID: " + txtLoginID.Text.ToString();
                        narration = narration + "-SIMno: " + txtSIMno.Text.ToString();
                        narration = narration + "-IMEIno: " + txtIMEIno.Text.ToString();
                        narration = narration + "-Plateno: " + txtPlateno.Text + ")";
                    }
                }

                //int iStatus = objBs.insertReceipts(sTableName, "tblReceipt_" + sTableName, "tblDaybook_" + sTableName, Convert.ToInt32(lblUserID.Text), txtreceiptno.Text, date, Convert.ToInt32(bankID), Convert.ToInt32(ddmodeofpayment.SelectedValue), bankid, txtChequeno.Text, agenttext, Convert.ToDouble(txtAmount.Text), txtNarration.Text, txtAgainst.Text, dstd, "tblTransReceipt_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, bank, ddmodeofpayment.SelectedItem.Text, Convert.ToInt32(agent), rblist.SelectedValue, EmpId, Convert.ToInt32(drpEmployee.SelectedValue), Convert.ToInt32(drpGsttype.SelectedValue), Convert.ToInt32(ddltax.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtCGST.Text), Convert.ToDouble(txtSGST.Text), Convert.ToDouble(txtIGST.Text), Convert.ToDouble(txtgstAmount.Text), lblFile_Path.Text);
                int iStatus = objBs.insertReceipts(sTableName, "tblReceipt_" + sTableName, "tblDaybook_" + sTableName, Convert.ToInt32(lblUserID.Text), txtreceiptno.Text, date, Convert.ToInt32(bankID), Convert.ToInt32(ddmodeofpayment.SelectedValue), bankid, txtChequeno.Text, agenttext, Convert.ToDouble(txtAmount.Text), narration, txtAgainst.Text, dstd, "tblTransReceipt_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, bank, ddmodeofpayment.SelectedItem.Text, Convert.ToInt32(agent), rblist.SelectedValue, EmpId, Convert.ToInt32(0), Convert.ToInt32(drpGsttype.SelectedValue), Convert.ToInt32(TAXVALUE), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtCGST.Text), Convert.ToDouble(txtSGST.Text), Convert.ToDouble(txtIGST.Text), Convert.ToDouble(txtgstAmount.Text), lblFile_Path.Text, Convert.ToDouble(Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtCGST.Text)- Convert.ToDouble(txtSGST.Text)- Convert.ToDouble(txtIGST.Text)));
                
                for (int i = 0; i < grdVehno.Rows.Count; i++)
                {
                    TextBox txtno = (TextBox)grdVehno.Rows[i].FindControl("txtno");
                    TextBox txtLoginID = (TextBox)grdVehno.Rows[i].FindControl("txtLoginID");
                    TextBox txtSIMno = (TextBox)grdVehno.Rows[i].FindControl("txtSIMno");
                    TextBox txtIMEIno = (TextBox)grdVehno.Rows[i].FindControl("txtIMEIno");
                    TextBox txtPlateno = (TextBox)grdVehno.Rows[i].FindControl("txtPlateno");

                    if (txtLoginID.Text == "")
                    {

                    }
                    else
                    {
                       // int iStatus1 = objBs.insertVehicleReceipts(sTableName, txtLoginID.Text, txtIMEIno.Text, txtPlateno.Text, txtSIMno.Text);
                    }
                }

                string iVoucherNo = Request.QueryString.Get("iVoucherNo");
                if (iVoucherNo != "" || iVoucherNo != null)
                {
                  //  int g = objBs.updateReceiptVoucherStatus(iVoucherNo, sTableName);
                }

                Response.Redirect("viewreceipts.aspx");
            }
            else if (btnadd.Text == "Update")
            {

                if (txteditnarration.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Mention Reason For Edit.Thank You!');", true);
                    txteditnarration.Focus();
                    return;
                }

                if (ddmodeofpayment.SelectedItem.Text == "DD" || ddmodeofpayment.SelectedItem.Text == "Cheque" || ddmodeofpayment.SelectedValue == "4")
                {
                    if (ddlBank.SelectedValue == "Select Bank Name" && txtChequeno.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No/UTR!');", true);

                        txtChequeno.Focus();
                        ddlBank.Focus();
                        return;

                    }
                    else if (ddlBank.SelectedValue == "Select Bank Name")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name!');", true);
                        ddlBank.Focus();
                        return;

                    }
                    else if (txtChequeno.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Cheque No/UTR!');", true);
                        txtChequeno.Focus();
                        return;

                    }
                }
                if (ddmodeofpayment.SelectedValue == "5")
                {
                    if (ddlBank.SelectedValue == "Select Bank Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name !');", true);


                        ddlBank.Focus();
                        return;

                    }
                }

                
                //int bankid = 0;

                //if (Convert.ToInt32(ddmodeofpayment.SelectedValue) == 1)
                //{
                //    bankid = 0;
                //}
                //else
                //{
                //    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                //}

                int bankid = 0;

                if (Convert.ToInt32(ddmodeofpayment.SelectedValue) == 1)
                {
                    bankid = 0;
                }
                else if (Convert.ToInt32(ddmodeofpayment.SelectedValue) == 5)
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                }
                else
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                }


                if (rblist.SelectedValue == "1")
                {
                    agent = ddlType.SelectedValue;
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = ddlType.SelectedItem.Text;
                }
                else if (rblist.SelectedValue == "5")
                {
                    agent = ddlType.SelectedValue;
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = ddlType.SelectedItem.Text;
                }
                else if (rblist.SelectedValue == "6")
                {
                    agent = ddlType.SelectedValue;
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = ddlType.SelectedItem.Text;
                }
                else if (rblist.SelectedValue == "7")
                {
                    agent = ddlType.SelectedValue;
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = ddlType.SelectedItem.Text;
                }
                else if (rblist.SelectedValue == "4")
                {
                    agent = "0";
                    bank = ddlcustomerID.SelectedItem.Text;
                    bankID = ddlcustomerID.SelectedValue;
                    agenttext = null;
                }
                else
                {
                    agent = "0";
                    bank = ddlbankID.SelectedItem.Text;
                    bankID = ddlbankID.SelectedValue;
                    agenttext = null;
                }

                string[] splitString = txtNarration.Text.Split('(');
                
                string narration = string.Empty;
                narration = splitString[0].Trim(); //txtNarration.Text;

                for (int i = 0; i < gvledgrid.Rows.Count; i++)
                {
                    Label txtBillNo = (Label)gvledgrid.Rows[i].FindControl("txtBillNo");
                    Label txtBillDate = (Label)gvledgrid.Rows[i].FindControl("txtBillDate");
                    TextBox txtAmount1 = (TextBox)gvledgrid.Rows[i].FindControl("txtAmount");

                    if ((txtAmount1.Text == "") || (txtAmount1.Text == "0"))
                    {

                    }
                    else
                    {
                        narration = narration + "(InvoiceNo: " + txtBillNo.Text.ToString();
                        narration = narration + "-InvoiceDate: " + txtBillDate.Text.ToString();
                        narration = narration + "-Amount: " + txtAmount1.Text + ")";
                    }
                }


                for (int i = 0; i < grdVehno.Rows.Count; i++)
                {
                    TextBox txtLoginID = (TextBox)grdVehno.Rows[i].FindControl("txtLoginID");
                    TextBox txtIMEIno = (TextBox)grdVehno.Rows[i].FindControl("txtIMEIno");
                    TextBox txtPlateno = (TextBox)grdVehno.Rows[i].FindControl("txtPlateno");
                    TextBox txtSIMno = (TextBox)grdVehno.Rows[i].FindControl("txtSIMno");

                    if (txtLoginID.Text == "")
                    {

                    }
                    else
                    {
                        narration = narration + "(LoginID: " + txtLoginID.Text.ToString();
                        narration = narration + "-SIMno: " + txtSIMno.Text.ToString();
                        narration = narration + "-IMEIno: " + txtIMEIno.Text.ToString();
                        narration = narration + "-Plateno: " + txtPlateno.Text + ")";
                    }
                }


                // DateTime EditDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                //int iStatus = objBs.UpdateReceipts(sTableName, "tblReceipt_" + sTableName, "tblDaybook_" + sTableName, Convert.ToInt32(lblUserID.Text), txtreceiptno.Text, date, Convert.ToInt32(bankID), Convert.ToInt32(ddmodeofpayment.SelectedValue), bankid, txtChequeno.Text, agenttext, Convert.ToDouble(txtAmount.Text), narration, Convert.ToInt32(txtTransNo.Text), txtAgainst.Text, dstd, "tblTransReceipt_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, bank, ddmodeofpayment.SelectedItem.Text, Convert.ToInt32(agent), rblist.SelectedValue, EmpId, Convert.ToInt32(0), Convert.ToInt32(drpGsttype.SelectedValue), Convert.ToInt32(TAXVALUE), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtCGST.Text), Convert.ToDouble(txtSGST.Text), Convert.ToDouble(txtIGST.Text), Convert.ToDouble(txtgstAmount.Text), lblFile_Path.Text, Convert.ToDouble(Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtCGST.Text) - Convert.ToDouble(txtSGST.Text) - Convert.ToDouble(txtIGST.Text)), txteditnarration.Text);

                //int iStatus2 = objBs.DeleteVehicleReceipts(sTableName, txtTransNo.Text);


                //for (int i = 0; i < grdVehno.Rows.Count; i++)
                //{
                //    TextBox txtno = (TextBox)grdVehno.Rows[i].Cells[0].FindControl("txtno");
                //    TextBox txtLoginID = (TextBox)grdVehno.Rows[i].Cells[0].FindControl("txtLoginID");
                //    TextBox txtSIMno = (TextBox)grdVehno.Rows[i].Cells[0].FindControl("txtSIMno");
                //    TextBox txtIMEIno = (TextBox)grdVehno.Rows[i].Cells[0].FindControl("txtIMEIno");
                //    TextBox txtPlateno = (TextBox)grdVehno.Rows[i].Cells[0].FindControl("txtPlateno");

                //    if (txtLoginID.Text == "")
                //    {

                //    }
                //    else
                //    {
                //        int iStatus1 = objBs.UpdateVehicleReceipts(sTableName, txtLoginID.Text, txtIMEIno.Text, txtPlateno.Text, txtTransNo.Text, txtSIMno.Text);
                //    }
                //}

                Response.Redirect("viewreceipts.aspx");
            }
            else
            {
                if (Session["Mode"] == "Switch")
                {
                    Session["Mode"] = "";
                    Session["UserID"] = "";
                    Session["UserName"] = "";
                    Response.Redirect("../Accountsbootstrap/login.aspx");
                }
                else
                    Response.Redirect("../Accountsbootstrap/viewreceipts.aspx");
            }

        }
        protected void rblist_selectedIndexChanged(object sender, EventArgs e)
        {
            if (rblist.SelectedValue == "1")
            {
                lblcustomer.Visible = true;
                ddlcustomerID.Visible = true;
                lblbank.Visible = false;
                ddlbankID.Visible = false;
                ddlType.Enabled = true;
                lbltype.Text = "Customer Receipt";
                ddlcustomerID.Items.Clear();

                DataSet dagent = objBs.getagentlist(sTableName);
                if (dagent.Tables[0].Rows.Count > 0)
                {

                    ddlType.DataSource = dagent.Tables[0];
                    ddlType.DataTextField = "LEdgerName";
                    ddlType.DataValueField = "LedgerID";
                    ddlType.DataBind();
                   // ddlType.Items.Insert(0, "Select Agent");
                }
            }
            else if (rblist.SelectedValue == "5")
            {
                lblcustomer.Visible = true;
                ddlcustomerID.Visible = true;
                lblbank.Visible = false;
                ddlbankID.Visible = false;
                ddlType.Enabled = true;
                lbltype.Text = "Dealer Receipt";
                ddlcustomerID.Items.Clear();

                DataSet dagent = objBs.getagentlist(sTableName);
                if (dagent.Tables[0].Rows.Count > 0)
                {

                    ddlType.DataSource = dagent.Tables[0];
                    ddlType.DataTextField = "LEdgerName";
                    ddlType.DataValueField = "LedgerID";
                    ddlType.DataBind();
                    //ddlType.Items.Insert(0, "Select Agent");
                }
            }
         
            else if (rblist.SelectedValue == "6")
            {
                lblcustomer.Visible = true;
                ddlcustomerID.Visible = true;
                lblbank.Visible = false;
                ddlbankID.Visible = false;
                ddlType.Enabled = true;
                lbltype.Text = "Distributor Receipt";
                ddlcustomerID.Items.Clear();

                DataSet dagent = objBs.getagentlist(sTableName);
                if (dagent.Tables[0].Rows.Count > 0)
                {

                    ddlType.DataSource = dagent.Tables[0];
                    ddlType.DataTextField = "LEdgerName";
                    ddlType.DataValueField = "LedgerID";
                    ddlType.DataBind();
                 //   ddlType.Items.Insert(0, "Select Agent");
                }
            }
            else if (rblist.SelectedValue == "4")
            {
                lblcustomer.Visible = true;
                ddlcustomerID.Visible = true;
                lblbank.Visible = false;
                ddlbankID.Visible = false;
                ddlType.Enabled = false;
                lbltype.Text = "Employee Receipt";

                //DataSet dsCustomer = objBs.selectVendor_Employee(2, sTableName);
                //if (dsCustomer.Tables[0].Rows.Count > 0)
                //{
                //    ddlcustomerID.DataSource = dsCustomer.Tables[0];
                //    ddlcustomerID.DataTextField = "LedgerName";
                //    ddlcustomerID.DataValueField = "LedgerID";
                //    ddlcustomerID.DataBind();
                //    ddlcustomerID.Items.Insert(0, "Select Employee Name");
                //}
                //else
                //{
                //    ddlcustomerID.Items.Insert(0, "Select Employee Name");
                //}
            }
            else if (rblist.SelectedValue == "7")
            {
                lblcustomer.Visible = true;
                ddlcustomerID.Visible = true;
                lblbank.Visible = false;
                ddlbankID.Visible = false;
                ddlType.Enabled = false;
                lbltype.Text = "Sub Dealer Receipt";

                //DataSet dsCustomer = objBs.selectVendor_SubDealer(6, sTableName);
                //if (dsCustomer.Tables[0].Rows.Count > 0)
                //{
                //    ddlcustomerID.DataSource = dsCustomer.Tables[0];
                //    ddlcustomerID.DataTextField = "LedgerName";
                //    ddlcustomerID.DataValueField = "LedgerID";
                //    ddlcustomerID.DataBind();
                //    ddlcustomerID.Items.Insert(0, "Select Employee Name");
                //}
                //else
                //{
                //    ddlcustomerID.Items.Insert(0, "Select Employee Name");
                //}
            }
            else if (rblist.SelectedValue == "6")
            {
                lblcustomer.Visible = true;
                ddlcustomerID.Visible = true;
                lblbank.Visible = false;
                ddlbankID.Visible = false;
                ddlType.Enabled = true;
                lbltype.Text = "Agent Receipt";
                ddlcustomerID.Items.Clear();

                DataSet dagent = objBs.getagentlist(sTableName);
                if (dagent.Tables[0].Rows.Count > 0)
                {

                    ddlType.DataSource = dagent.Tables[0];
                    ddlType.DataTextField = "LEdgerName";
                    ddlType.DataValueField = "LedgerID";
                    ddlType.DataBind();
                    //   ddlType.Items.Insert(0, "Select Agent");
                }
            }
            else if (rblist.SelectedValue == "2")
            {
                DataSet dst1 = objBs.Getbanknamebranch(4, sTableName);

                if (dst1 != null)
                {
                    if (dst1.Tables[0].Rows.Count > 0)
                    {

                        ddlbankID.DataSource = dst1.Tables[0];
                        ddlbankID.DataTextField = "LedgerName";
                        ddlbankID.DataValueField = "LedgerID";
                        ddlbankID.DataBind();
                        ddlbankID.Items.Insert(0, "Select Bank Name");
                    }
                    else
                    {

                        ddlbankID.Items.Insert(0, "Select Bank Name");
                    }
                }
                else
                {

                    ddlbankID.Items.Insert(0, "Select Bank Name");
                }

                lblcustomer.Visible = false;
                ddlcustomerID.Visible = false;
                lblbank.Visible = true;
                ddlbankID.Visible = true;
                ddlType.Enabled = false;
                txtaddress.Text = "";
                txtarea.Text = "";
                txtcity.Text = "";
                gvledgrid.DataSource = null;
                gvledgrid.DataBind();
                lbltype.Text = "Bank Withdrawal";
            }
            else
            {
                DataSet dsCustomer = objBs.Getbanknamebranch(4, sTableName);
                if (dsCustomer.Tables[0].Rows.Count > 0)
                {
                    ddlbankID.DataSource = dsCustomer.Tables[0];
                    ddlbankID.DataTextField = "LedgerName";
                    ddlbankID.DataValueField = "LedgerID";
                    ddlbankID.DataBind();
                    ddlbankID.Items.Insert(0, "Select Supplier Name");
                }
                else
                {
                    ddlbankID.Items.Insert(0, "Select Supplier Name");
                }

                lblcustomer.Visible = false;
                ddlcustomerID.Visible = false;
                lblbank.Visible = true;
                ddlbankID.Visible = true;
                ddlType.Enabled = false;
                txtaddress.Text = "";
                txtarea.Text = "";
                txtcity.Text = "";
                gvledgrid.DataSource = null;
                gvledgrid.DataBind();
                lbltype.Text = "Supplier Receipt";
            }
            ddlType_SelectedIndexChanged(sender, e);
        }
        protected void txtbalance1_TextChanged(object sender, EventArgs e)
        {
            //    Decimal iamount = (Convert.ToDecimal(txtbillamount1.Text)) - (Convert.ToDecimal(txtbalance1.Text));
            //    txtamount1.Text = Decimal.Round(iamount, 2).ToString("f2");
            //    Decimal iGross1 = 0;
            //    if (txtamount1.Text != "")
            //        iGross1 = Convert.ToDecimal(txtamount1.Text);

            //    Decimal iTotalAmount = iGross1;
            // txttotal.Text = Convert.ToString(iTotalAmount);
        }

        protected void ddbillno1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Decimal IbillAmt1 = 0, IBAL1 = 0;
            //DataSet dContactType = objBs.GetCustomerDetails(Convert.ToInt32(ddlcustomerID.SelectedValue));
            //if (dContactType.Tables[0].Rows[0]["ContactTypeID"].ToString() == "1")
            //{
            //    DataSet dsCustsalesID = objBs.getCustomerReceipt(Convert.ToInt32(ddbillno1.SelectedValue), "tblSales_" + sTableName);
            //    if (dsCustsalesID.Tables[0].Rows.Count > 0)
            //    {
            //        // ddlbillno.Text = dssalesID.Tables[0].Rows[0]["BillNo"].ToString();
            //        txtbilldate1.Text = dsCustsalesID.Tables[0].Rows[0]["BillDate"].ToString();


            //        IbillAmt1 = Convert.ToDecimal(dsCustsalesID.Tables[0].Rows[0]["BillAmount"].ToString());
            //        txtbillamount1.Text = Decimal.Round(IbillAmt1, 2).ToString("f2");
            //        txtbillamount1.Enabled = false;
            //        //   txtbalance1.Text = dssalesID.Tables[0].Rows[0]["NetAmount"].ToString();
            //        DataSet dsRep = objBs.GetReceiptAmountDet(Convert.ToInt32((ddbillno1.SelectedValue)), "tblTransReceipt_" + sTableName);
            //        Decimal iBal = 0, iAmount = 0;
            //        if (dsRep.Tables[0].Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dsRep.Tables[0].Rows.Count; i++)
            //            {
            //                iBal += Convert.ToDecimal(dsRep.Tables[0].Rows[i]["Amount"].ToString());
            //            }
            //            iAmount = Convert.ToDecimal(dsRep.Tables[0].Rows[0]["BillAmount"].ToString());

            //            txtbalance1.Text = Decimal.Round((IbillAmt1 - iBal), 2).ToString("f2");
            //            txtbalance1.Enabled = false;
            //        }
            //        else
            //        {
            //            IBAL1 = Convert.ToDecimal(dsCustsalesID.Tables[0].Rows[0]["NetAmount"].ToString());
            //            txtbalance1.Text = Decimal.Round(IBAL1, 2).ToString("f2");

            //        }
            //    }
            //}
            //else
            //{
            //    DataSet dssalesID = objBs.GetSalesIDReceipt(Convert.ToInt32(ddbillno1.SelectedValue), "tblSales_" + sTableName);
            //    if (dssalesID.Tables[0].Rows.Count > 0)
            //    {
            //        // ddlbillno.Text = dssalesID.Tables[0].Rows[0]["BillNo"].ToString();
            //        txtbilldate1.Text = dssalesID.Tables[0].Rows[0]["BillDate"].ToString();


            //        IbillAmt1 = Convert.ToDecimal(dssalesID.Tables[0].Rows[0]["NetAmount"].ToString());
            //        txtbillamount1.Text = Decimal.Round(IbillAmt1, 2).ToString("f2");
            //        txtbillamount1.Enabled = false;
            //        //   txtbalance1.Text = dssalesID.Tables[0].Rows[0]["NetAmount"].ToString();
            //        DataSet dsRep = objBs.GetReceiptAmountDet(Convert.ToInt32((ddbillno1.SelectedValue)), "tblTransReceipt_" + sTableName);
            //        Decimal iBal = 0, iAmount = 0;
            //        if (dsRep.Tables[0].Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dsRep.Tables[0].Rows.Count; i++)
            //            {
            //                iBal += Convert.ToDecimal(dsRep.Tables[0].Rows[i]["Amount"].ToString());
            //            }
            //            iAmount = Convert.ToDecimal(dsRep.Tables[0].Rows[0]["BillAmount"].ToString());

            //            txtbalance1.Text = Decimal.Round((IbillAmt1 - iBal), 2).ToString("f2");
            //            txtbalance1.Enabled = false;
            //        }
            //        else
            //        {
            //            IBAL1 = Convert.ToDecimal(dssalesID.Tables[0].Rows[0]["NetAmount"].ToString());
            //            txtbalance1.Text = Decimal.Round(IBAL1, 2).ToString("f2");

            //        }


            //    }

            //}

        }


        protected void ddmodeofpayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddmodeofpayment.SelectedValue == "1")
            {
                ddlBank.Enabled = false;
                txtChequeno.Enabled = false;

                ddlBank.SelectedIndex = 0;
                txtChequeno.Text = "";

            }
            else if (ddmodeofpayment.SelectedValue == "5")
            {
                ddlBank.Enabled = true;
                txtChequeno.Enabled = false;

                //  ddlBank.SelectedIndex = 0;
                txtChequeno.Text = "0";

            }

            else
            {
                ddlBank.Enabled = true;
                txtChequeno.Enabled = true;
                ddlBank.Focus();
            }
        }

        protected void btnop_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/OutstandingPayment.aspx");
        }


        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewreceipts.aspx");
        }

        protected void txtgstAmount_TextChanged(object sender, EventArgs e)
        {

            double TAX = 0;

            if (ddltax.SelectedItem.Text == "Select Tax")
            {
                TAX = 0;
            }
            else
            {
                TAX = Convert.ToDouble(ddltax.SelectedItem.Text);
            }


            if (drpGsttype.SelectedValue == "1")//GST Inclusive
            {
                if (txtgstAmount.Text != "")
                {
                    double gst = Convert.ToDouble(txtgstAmount.Text) * Convert.ToDouble(TAX) / (100 + Convert.ToDouble(TAX));

                    if (ddlProvince.SelectedValue == "1")
                    {
                        txtCGST.Text = (Convert.ToDouble(gst) / 2).ToString("f2");
                        txtSGST.Text = (Convert.ToDouble(gst) / 2).ToString("f2");
                        txtIGST.Text = "0";
                    }
                    else
                    {
                        txtCGST.Text = "0";
                        txtSGST.Text = "0";
                        txtIGST.Text = Convert.ToDouble(gst).ToString("f2");
                    }

                    txtAmount.Text = Convert.ToDouble(txtgstAmount.Text).ToString("f2");// (Convert.ToDouble(txtgstAmount.Text) - gst).ToString("f2");
                }
                txtAmount_TextChanged(sender, e);
            }
            if (drpGsttype.SelectedValue == "2")//GST Exclusive
            {
                if (txtgstAmount.Text != "")
                {
                    double gst = Convert.ToDouble(txtgstAmount.Text) * Convert.ToDouble(TAX) / (100);

                    if (ddlProvince.SelectedValue == "1")
                    {
                        txtCGST.Text = (Convert.ToDouble(gst) / 2).ToString("f2");
                        txtSGST.Text = (Convert.ToDouble(gst) / 2).ToString("f2");
                        txtIGST.Text = "0";
                    }
                    else
                    {
                        txtCGST.Text = "0";
                        txtSGST.Text = "0";
                        txtIGST.Text = Convert.ToDouble(gst).ToString("f2");
                    }

                    txtAmount.Text = (Convert.ToDouble(txtgstAmount.Text) + gst).ToString("f2");
                }
                txtAmount_TextChanged(sender, e);
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            double adtotal = Convert.ToDouble(txtAmount.Text);

            if (gvledgrid.Rows.Count > 0)
            {
                DataTable dttt;
                DataRow drNew;
                DataColumn dct;
                DataSet dstd = new DataSet();
                dttt = new DataTable();

                //dct = new DataColumn("Branch");
                //dttt.Columns.Add(dct);

                dct = new DataColumn("Branchcode");
                dttt.Columns.Add(dct);

                dct = new DataColumn("SalesID");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Billno");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Amount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("BillDate");
                dttt.Columns.Add(dct);

                dct = new DataColumn("BillAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Balance");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                for (int vLoop = 0; vLoop < gvledgrid.Rows.Count; vLoop++)
                {
                    Label Branchcode = (Label)gvledgrid.Rows[vLoop].FindControl("txtBranchcode");
                    // Label Branch = (Label)gvledgrid.Rows[vLoop].FindControl("txtBranch");
                    Label txtd = (Label)gvledgrid.Rows[vLoop].FindControl("txtSalesid");
                    Label txttt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillno");
                    Label txt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillDate");
                    Label txttd = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillAmount");
                    Label txttd123 = (Label)gvledgrid.Rows[vLoop].FindControl("txtBalance");
                    TextBox txttdtt = (TextBox)gvledgrid.Rows[vLoop].FindControl("txtAmount");

                    drNew = dttt.NewRow();
                    drNew["Branchcode"] = Branchcode.Text;
                    //  drNew["Branch"] = Branch.Text;
                    drNew["SalesID"] = txtd.Text;
                    drNew["Billno"] = txttt.Text;
                    drNew["BillAmount"] = txttd.Text;
                    drNew["Balance"] = txttd123.Text;

                    drNew["BillDate"] = txt.Text;
                    if (adtotal > Convert.ToDouble(txttd123.Text))
                    {
                        drNew["Amount"] = Convert.ToDouble(txttd123.Text);
                        adtotal = adtotal - Convert.ToDouble(txttd123.Text);


                    }
                    else if (adtotal < Convert.ToDouble(txttd123.Text))
                    {
                        drNew["Amount"] = adtotal;
                        adtotal = 0;
                    }
                    else if (adtotal == Convert.ToDouble(txttd123.Text))
                    {
                        drNew["Amount"] = adtotal;
                        adtotal = 0;
                    }

                    dstd.Tables[0].Rows.Add(drNew);
                }
                gvledgrid.DataSource = dstd;
                gvledgrid.DataBind();
            }
            else
            {
                gvledgrid.DataSource = null;
                gvledgrid.DataBind();
            }
            txtNarration.Focus();
        }

        protected void gridbutton_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewreceipts.aspx");
        }

    }

}
