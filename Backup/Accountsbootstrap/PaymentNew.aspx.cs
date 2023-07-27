using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using CommonLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.Globalization;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class PaymentNew : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        decimal totalamount = 0;

        int EmpId = 0;
        string EmployeeName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
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

                lblTitle.Text = "Payment";
                ds = objBs.getLedger_New(lblContactTypeId.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlLName.DataSource = ds.Tables[0];
                    ddlLName.DataTextField = "LedgerName";
                    ddlLName.DataValueField = "LedgerID";
                    ddlLName.DataBind();
                    ddlLName.Items.Insert(0, "PartyName");
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


                ds = objBs.getPaymentmode();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlPMode.DataSource = ds;
                        ddlPMode.DataTextField = "Payment_Mode";
                        ddlPMode.DataValueField = "Payment_ID";
                        ddlPMode.DataBind();
                        ddlPMode.Items.Insert(0, "Select Paymode Type");
                    }
                    else
                    {
                        ddlPMode.Items.Insert(0, "Select Paymode Type");
                    }
                }
                else
                {
                    ddlPMode.Items.Insert(0, "Select Paymode Type");
                }

                DataSet dsb = objBs.GetLedgers(4, sTableName);
                if (dsb != null)
                {
                    if (dsb.Tables[0].Rows.Count > 0)
                    {
                        ddlBankName.DataSource = dsb;
                        ddlBankName.DataTextField = "LedgerName";
                        ddlBankName.DataValueField = "LedgerID";
                        ddlBankName.DataBind();
                        ddlBankName.Items.Insert(0, "Select Bank Name");
                    }
                }

                ddlChequeNo.Items.Insert(0, "Select Chequeno");

                DataSet ds1 = objBs.paymentNo("tblPayment_" + sTableName);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                    if (ds1.Tables[0].Rows[0]["PaymentNo"].ToString() == "")
                        txtPaymentNo.Text = "1";
                    else
                        txtPaymentNo.Text = ds1.Tables[0].Rows[0]["PaymentNo"].ToString();

                    txtpdate.Text = DateTime.Today.ToString("dd/MM/yyyy");


                }
                txtNarration.Text = "Payment No: " + txtPaymentNo.Text;

                //ldgID.Value = "New";    


                ddlBankName.Enabled = false;
                ddlChequeNo.Enabled = false;
                txtAddress.Enabled = false;
                txtArea.Enabled = false;
                txtCity.Enabled = false;


                narra.Visible = false;
                string DayBookID = Request.QueryString.Get("TransNo");
                if (DayBookID != "" || DayBookID != null)
                {
                    int typeid = 0;

                    DataSet dspayment = objBs.getPaymentmaster("tblPayment_" + sTableName, DayBookID);
                    if (dspayment.Tables[0].Rows.Count > 0)
                    {
                        btnadd.Text = "Update";
                        ldgID.Value = "Edit";
                        narra.Visible = true;
                        // ddlType.SelectedValue = dspayment.Tables[0].Rows[0]["UserId"].ToString();
                        txtpaymentid.Text = dspayment.Tables[0].Rows[0]["PaymentId"].ToString();
                        DataSet dst = new DataSet();

                        if (dspayment.Tables[0].Rows[0]["LedgerType"].ToString() == "Supplier")
                        {
                            lblTitle.Text = "Customer Payment";
                            ddlType.SelectedValue = "2";
                            typeid = 2;

                            txtAgainst.Visible = false;
                            TransPaymentGrid.Visible = true;
                            //lblAganistBno.Visible = true;
                            dst = objBs.getLedger_New(lblContactTypeId.Text);

                        }

                        if (dst != null)
                        {
                            if (dst.Tables[0].Rows.Count > 0)
                            {
                                ddlLName.DataSource = dst.Tables[0];
                                ddlLName.DataTextField = "LedgerName";
                                ddlLName.DataValueField = "LedgerID";
                                ddlLName.DataBind();
                                ddlLName.Items.Insert(0, "Select LedgerName");
                            }
                            else
                            {
                                ddlLName.Items.Insert(0, "Select LedgerName");
                            }
                        }
                        else
                        {
                            ddlLName.Items.Insert(0, "Select LedgerName");
                        }
                        ddlLName.SelectedValue = dspayment.Tables[0].Rows[0]["LedgerID"].ToString();

                        ds = objBs.Getbanknamebranch(4, sTableName);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ddlBankName.DataSource = ds;
                                ddlBankName.DataTextField = "LedgerName";
                                ddlBankName.DataValueField = "LedgerID";
                                ddlBankName.DataBind();
                                ddlBankName.Items.Insert(0, "Select Bank Name");
                            }
                            else
                            {
                                ddlBankName.Items.Insert(0, "Select Bank Name");
                            }
                        }
                        else
                        {
                            ddlBankName.Items.Insert(0, "Select Bank Name");
                        }

                        string ledid = dspayment.Tables[0].Rows[0]["LedgerID"].ToString();
                        ds = objBs.getledgerdet(Convert.ToInt32(ledid), sTableName);

                        txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        txtArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                        txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();

                        txtPaymentNo.Text = dspayment.Tables[0].Rows[0]["PaymentNo"].ToString();
                        txtpdate.Text = Convert.ToDateTime(dspayment.Tables[0].Rows[0]["PaymentDate"]).ToString("dd/MM/yyyy");
                        ddlPMode.SelectedValue = dspayment.Tables[0].Rows[0]["PayModeID"].ToString();
                        txtAmount.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["Amount"]).ToString("0.00");
                        txtNarration.Text = dspayment.Tables[0].Rows[0]["Narration"].ToString();
                        txtAgainst.Text = dspayment.Tables[0].Rows[0]["Against"].ToString();
                        //txtAgainst.Visible = false;
                        // lblAganistBno.Visible = false;


                        drpGsttype.SelectedValue = dspayment.Tables[0].Rows[0]["GSTType"].ToString();
                        ddltax.SelectedValue = dspayment.Tables[0].Rows[0]["GSTTax"].ToString();
                        ddlProvince.SelectedValue = dspayment.Tables[0].Rows[0]["Province"].ToString();
                        txtgstAmount.Text = dspayment.Tables[0].Rows[0]["Total"].ToString();
                        txtCGST.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["CGST"]).ToString("0.00");
                        txtSGST.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["SGST"]).ToString("0.00");
                        txtIGST.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["IGST"]).ToString("0.00");

                        lblFile_Path.Text = dspayment.Tables[0].Rows[0]["Imgpath"].ToString();// "~/Files/" + fp_Upload.PostedFile.FileName;
                        img_Photo.ImageUrl = dspayment.Tables[0].Rows[0]["Imgpath"].ToString(); //"~/Files/" + fp_Upload.PostedFile.FileName;

                        if (Convert.ToInt32(ddlPMode.SelectedValue) == 1)
                        {
                            ddlChequeNo.Enabled = false;
                            ddlBankName.Enabled = false;
                            txtutr.Enabled = false;

                            ddlBankName.SelectedValue = dspayment.Tables[0].Rows[0]["BankName"].ToString();
                            ddlChequeNo.SelectedValue = dspayment.Tables[0].Rows[0]["Chequeno"].ToString();

                            //ddlChequeNo.Items.Insert(0, "Select Chequeno");
                        }
                        else if (Convert.ToInt32(ddlPMode.SelectedValue) == 4)
                        {
                            ddlChequeNo.Enabled = false;
                            ddlBankName.Enabled = true;
                            txtutr.Enabled = true;

                            ddlBankName.SelectedValue = dspayment.Tables[0].Rows[0]["BankName"].ToString();
                            txtutr.Text = dspayment.Tables[0].Rows[0]["Utrno"].ToString();

                            //ddlChequeNo.Items.Insert(0, "Select Chequeno");
                        }
                        else if (Convert.ToInt32(ddlPMode.SelectedValue) == 5)
                        {
                            ddlChequeNo.Enabled = false;
                            ddlBankName.Enabled = true;
                            txtutr.Enabled = false;

                            ddlBankName.SelectedValue = dspayment.Tables[0].Rows[0]["BankName"].ToString();
                            txtutr.Text = dspayment.Tables[0].Rows[0]["Utrno"].ToString();

                            //ddlChequeNo.Items.Insert(0, "Select Chequeno");
                        }
                        else
                        {
                            txtutr.Enabled = false;
                            ddlChequeNo.Enabled = true;
                            ddlBankName.Enabled = true;




                            ddlBankName.SelectedValue = dspayment.Tables[0].Rows[0]["BankName"].ToString();

                            ds = objBs.getBankLedger(4, Convert.ToInt32(ddlBankName.SelectedValue), sTableName);

                            ddlChequeNo.DataSource = ds;
                            ddlChequeNo.DataTextField = "chequeno";
                            ddlChequeNo.DataValueField = "TransChequeId";
                            ddlChequeNo.DataBind();
                            ddlChequeNo.Items.Insert(0, "Select Chequeno");
                            ddlChequeNo.SelectedItem.Text = dspayment.Tables[0].Rows[0]["Chequeno"].ToString();
                        }
                        DataSet dsgetPaymentNO = objBs.getPaymentNo("tblPayment_" + sTableName, DayBookID);
                        string paymentno = dsgetPaymentNO.Tables[0].Rows[0]["PaymentNo"].ToString();
                        //drpEmployee.SelectedValue = dsgetPaymentNO.Tables[0].Rows[0]["EmployeeID"].ToString();

                        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                        {
                            DataSet dsTransPayment = objBs.getTransPaymentNodet("tblTransPayment_" + sTableName, paymentno);

                            if (dsTransPayment != null)
                            {
                                if (dsTransPayment.Tables[0].Rows.Count > 0)
                                {
                                    DataTable dttt;
                                    DataRow drNew;
                                    DataColumn dct;
                                    DataSet dstd = new DataSet();
                                    dttt = new DataTable();

                                    dct = new DataColumn("P_ID");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("DC_NO");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Bill_NO");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Amount");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("DC_Date");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Balance");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("BillAmount");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("PurchaseReturnAmount");
                                    dttt.Columns.Add(dct);

                                    dstd.Tables.Add(dttt);

                                    if (dsTransPayment != null)
                                    {
                                        if (dsTransPayment.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dsTransPayment.Tables[0].Rows.Count; i++)
                                            {
                                                drNew = dttt.NewRow();
                                                drNew["P_ID"] = Convert.ToInt32(dsTransPayment.Tables[0].Rows[i]["Pid"]);
                                                drNew["DC_NO"] = Convert.ToInt32(dsTransPayment.Tables[0].Rows[i]["Invoice_No"]);
                                                drNew["DC_Date"] = Convert.ToDateTime(dsTransPayment.Tables[0].Rows[i]["BillDate"]).ToString("dd/MM/yyyy");
                                                drNew["Bill_NO"] = Convert.ToInt32(dsTransPayment.Tables[0].Rows[i]["BillNo"]);
                                                drNew["BillAmount"] = Convert.ToDouble(dsTransPayment.Tables[0].Rows[i]["BillAmount"]);
                                                drNew["Balance"] = Convert.ToDouble(dsTransPayment.Tables[0].Rows[i]["Balance"]);
                                                drNew["Amount"] = Convert.ToDouble(dsTransPayment.Tables[0].Rows[i]["Amount"]); ;
                                                drNew["PurchaseReturnAmount"] = Convert.ToDouble(dsTransPayment.Tables[0].Rows[i]["PurchaseReturnAmount"]);
                                                dstd.Tables[0].Rows.Add(drNew);
                                            }
                                        }
                                    }

                                    TransPaymentGrid.DataSource = dstd.Tables[0];
                                    TransPaymentGrid.DataBind();
                                }
                                else
                                {
                                    TransPaymentGrid.DataSource = null;
                                    TransPaymentGrid.DataBind();
                                }
                            }
                        }

                    }
                }

                string iVoucherNo = Request.QueryString.Get("iVoucherNo");
                if (iVoucherNo != "" || iVoucherNo != null)
                {
                    int typeid = 0;

                    //DataSet dspayment = objBs.getPaymentvouchermaster("tblPaymentVoucher_" + sTableName, iVoucherNo);
                    //if (dspayment.Tables[0].Rows.Count > 0)
                    //{
                    //    btnadd.Text = "Save";
                    //    ldgID.Value = "New";
                    //    // ddlType.SelectedValue = dspayment.Tables[0].Rows[0]["UserId"].ToString();
                    //    txtpaymentid.Text = dspayment.Tables[0].Rows[0]["VoucherId"].ToString();
                    //    DataSet dst = new DataSet();

                    //    if (dspayment.Tables[0].Rows[0]["LedgerType"].ToString() == "Supplier")
                    //    {
                    //        lblTitle.Text = "Supplier Payment against Voucher";
                    //        ddlType.SelectedValue = "2";
                    //        typeid = 2;

                    //        dst = objBs.getLedger_New(lblContactTypeId.Text);

                    //    }


                    //    if (dst != null)
                    //    {

                    //        if (dst.Tables[0].Rows.Count > 0)
                    //        {
                    //            ddlLName.DataSource = dst.Tables[0];
                    //            ddlLName.DataTextField = "LedgerName";
                    //            ddlLName.DataValueField = "LedgerID";
                    //            ddlLName.DataBind();
                    //            ddlLName.Items.Insert(0, "Select LedgerName");
                    //        }
                    //        else
                    //        {
                    //            ddlLName.Items.Insert(0, "Select LedgerName");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ddlLName.Items.Insert(0, "Select LedgerName");
                    //    }
                    //    ddlLName.SelectedValue = dspayment.Tables[0].Rows[0]["LedgerID"].ToString();
                    //    ddlLName_SelectedIndexChanged(sender, e);

                    //    string ledid = dspayment.Tables[0].Rows[0]["LedgerID"].ToString();
                    //    ds = objBs.getledgerdet(Convert.ToInt32(ledid), sTableName);

                    //    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    //    txtArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                    //    txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();

                    //    DataSet ds12 = objBs.paymentNo("tblPayment_" + sTableName);
                    //    if (ds12.Tables[0].Rows.Count > 0)
                    //    {
                    //        // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                    //        if (ds12.Tables[0].Rows[0]["PaymentNo"].ToString() == "")
                    //            txtPaymentNo.Text = "1";
                    //        else
                    //            txtPaymentNo.Text = ds12.Tables[0].Rows[0]["PaymentNo"].ToString();

                    //        txtpdate.Text = DateTime.Today.ToString("dd/MM/yyyy");


                    //    }
                    //    txtNarration.Text = "Payment No: " + txtPaymentNo.Text + "(" + dspayment.Tables[0].Rows[0]["Narration"].ToString() + ")";

                    //    //txtPaymentNo.Text = dspayment.Tables[0].Rows[0]["VoucherNo"].ToString();
                    //    //txtpdate.Text = Convert.ToDateTime(dspayment.Tables[0].Rows[0]["VoucherDate"]).ToString("dd/MM/yyyy");

                    //    txtAmount.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["Amount"]).ToString("0.00");
                    //    //txtNarration.Text = dspayment.Tables[0].Rows[0]["Narration"].ToString();

                    //    drpGsttype.SelectedValue = dspayment.Tables[0].Rows[0]["GSTType"].ToString();
                    //    ddltax.SelectedValue = dspayment.Tables[0].Rows[0]["GSTTax"].ToString();
                    //    ddlProvince.SelectedValue = dspayment.Tables[0].Rows[0]["Province"].ToString();
                    //    txtAmount.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["Amount"]).ToString("0.00");
                    //    txtgstAmount.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["Amount"]).ToString("0.00");
                    //    // txtCGST.Text = dspayment.Tables[0].Rows[0]["CGST"].ToString();
                    //    // txtSGST.Text = dspayment.Tables[0].Rows[0]["SGST"].ToString();
                    //    //   txtIGST.Text = dspayment.Tables[0].Rows[0]["IGST"].ToString();



                    //    //DataSet dsgetPaymentNO = objBs.getPaymentNo("tblPayment_" + sTableName, DayBookID);
                    //    //string paymentno = dsgetPaymentNO.Tables[0].Rows[0]["PaymentNo"].ToString();

                    //    ///drpEmployee.SelectedValue = dspayment.Tables[0].Rows[0]["EmployeeID"].ToString();
                    //    txtgstAmount_TextChanged(sender, e);

                    //    if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                    //    {

                    //    }

                    //}
                }
                txtpdate.Focus();
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
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

        protected void drpGsttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtgstAmount_TextChanged(sender, e);
        }

     

        protected void ddlLName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlType.SelectedItem.Text != "Select Type")
            //{
                //if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                //{
                    //ds = objBs.GetPurchaseDet("tblPurchase_" + sTableName, Convert.ToInt32(ddlLName.SelectedValue));
                    //TransPaymentGrid.DataSource = ds;
                    //TransPaymentGrid.DataBind();
                    if (ddlLName.SelectedItem.Text != "PartyName")
                    {

                        DataSet ds = objBs.GetCreditPurchase((Convert.ToInt32(ddlLName.SelectedValue)), "tblPurchaseGRN", "tblDayBook_" + sTableName);

                        DataSet dsReceiptDet = objBs.GetPaymentDetails("tblDayBook_" + sTableName, "tblTransPayment_" + sTableName, "tblPayment_" + sTableName);

                       // DataSet dsPurRtnDet = objBs.GetPurRtnDetails("tblDayBook_" + sTableName, "tbltranspurchasereturn_" + sTableName, "tblPurchaseReturn_" + sTableName);

                        if (ds != null)
                        {
                            foreach (DataRow dr in dsReceiptDet.Tables[0].Rows)
                            {
                                var billNo = dr["Invoice_No"].ToString();
                                var billAmount = dr["TotalAmount"].ToString();

                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (billNo.Trim() == ds.Tables[0].Rows[i]["RecPONo"].ToString())
                                    {
                                        ds.Tables[0].Rows[i].BeginEdit();
                                        double val = (double.Parse(ds.Tables[0].Rows[i]["Balance"].ToString()) - double.Parse(billAmount));
                                        ds.Tables[0].Rows[i]["Balance"] = val;
                                       
                                           // ds.Tables[0].Rows[i]["PurchaseReturn"] = "0";
                                       
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

                                dct = new DataColumn("P_ID");
                                dttt.Columns.Add(dct);


                                dct = new DataColumn("DC_NO");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("Bill_NO");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("Amount");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("DC_Date");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("Balance");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("BillAmount");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("PurchaseReturnAmount");
                                dttt.Columns.Add(dct);

                                dstd.Tables.Add(dttt);

                                if (ds != null)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            drNew = dttt.NewRow();
                                            drNew["P_ID"] = (ds.Tables[0].Rows[i]["POGRNId"]);
                                            drNew["DC_NO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RecPONo"]);
                                            drNew["DC_Date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]).ToString("dd/MM/yyyy");
                                            drNew["Bill_NO"] = (ds.Tables[0].Rows[i]["RecPONo"]);
                                            drNew["BillAmount"] = Convert.ToDouble(ds.Tables[0].Rows[i]["BillAmount"]).ToString("0.00");
                                            drNew["Balance"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"]).ToString("0.00");
                                            drNew["PurchaseReturnAmount"] = "0";
                                            drNew["Amount"] = 0;
                                            dstd.Tables[0].Rows.Add(drNew);
                                        }
                                        TransPaymentGrid.DataSource = dstd.Tables[0];
                                        TransPaymentGrid.DataBind();
                                        TransPaymentGrid.Visible = true;
                                    }
                                }
                                else
                                {
                                    TransPaymentGrid.DataSource = null;
                                    TransPaymentGrid.DataBind();
                                    TransPaymentGrid.Visible = true;
                                }

                            }
                            else
                            {
                                TransPaymentGrid.DataSource = null;
                                TransPaymentGrid.DataBind();
                                TransPaymentGrid.Visible = true;
                            }

                        }
                        else
                        {
                            TransPaymentGrid.DataSource = null;
                            TransPaymentGrid.DataBind();
                            TransPaymentGrid.Visible = true;
                        }
                    }
                //}
                //if (ddlLName.SelectedValue == "Select LedgerName")
                //{
                //    //TransPaymentGrid.Visible = false;
                //}
                //else
                //{

                //    ds = objBs.LedgerDetailsList(Convert.ToInt32(ddlLName.SelectedValue), sTableName);
                //    if (ds != null)
                //    {
                //        if (ds.Tables[0].Rows.Count > 0)
                //        {
                //            txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                //            txtArea.Text = ds.Tables[0].Rows[0]["Area1"].ToString();
                //            txtCity.Text = ds.Tables[0].Rows[0]["City1"].ToString();
                //        }
                //    }
                //}
            //}

            txtAmount.Focus();
        }
        protected void btnadd_Click(object sender, EventArgs e)
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



            string utr = string.Empty;
            txtpaymentnochnaged(sender, e);
            string DayBookID = Request.QueryString.Get("TransNo");
            ds = objBs.getCashledgerId("Cash A/C _" + sTableName);
            string ledgerid = ds.Tables[0].Rows[0]["LedgerID"].ToString();

            if (ddlPMode.SelectedItem.Text == "DD" || ddlPMode.SelectedItem.Text == "Cheque")
            {
                if (ddlBankName.SelectedValue == "Select Bank Name" && ddlChequeNo.SelectedValue == "Select Chequeno")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No!');", true);
                    return;

                }
                else if (ddlBankName.SelectedValue == "Select Bank Name")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name!');", true);
                    return;

                }
                else if (ddlChequeNo.SelectedValue == "Select Chequeno")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Cheque No!');", true);
                    return;

                }
            }
            if (ddlPMode.SelectedValue == "4")
            {
                if (ddlBankName.SelectedValue == "Select Bank Name" && txtutr.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Utr No!');", true);
                    return;

                }
                else if (ddlBankName.SelectedValue == "Select Bank Name")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name!');", true);
                    return;

                }
                else if (txtutr.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Utr No!');", true);
                    return;

                }
            }
            if (ddlPMode.SelectedValue == "5")
            {
                if (ddlBankName.SelectedValue == "Select Bank Name")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name !');", true);
                    return;

                }

            }



            DateTime recdate;
            DateTime date = DateTime.ParseExact(txtpdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Save")
            {

                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("DC_NO") });
                //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Bill_NO") });
                //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("DC_Date") });
                //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("TotalAmount") });
                //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Balance") });
                //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Amount1") });

                //string data = "";
                //DataRow dr;
                //foreach (GridViewRow row in GridPurchase.Rows)
                //{
                //    //if (row.RowType == DataControlRowType.DataRow)
                //    //{

                //    for (int i = 1; i < GridPurchase.Rows.Count; i++)
                //    {
                //        string InvoiceNo = row.Cells[0].Text;
                //        string Billno = row.Cells[1].Text;
                //        string BillDate = row.Cells[2].Text;
                //        string BillAmount = row.Cells[3].Text;
                //        double Balance = Convert.ToDouble(row.Cells[4].Text);
                //       TextBox Amount = (TextBox)GridPurchase.Rows[i].Cells[5].FindControl("txtPayAmount");


                //        Balance = Convert.ToDouble(Balance) - Convert.ToDouble(Amount.Text);




                //        data = data + InvoiceNo + " ,  " + Billno + " , " + BillDate + "," + BillAmount + "," + Balance + "," + Amount.Text + "<br>";
                //        dr = dt.NewRow();

                //        dr.Table.Rows.Add(InvoiceNo, Billno, BillDate, BillAmount, Balance, Amount.Text);

                //        DataSet dspayment = new DataSet();
                //    dspayment.Merge(dt);
                //    return dr;




                //    int iStatus = objBs.insertTransPayment("tblTransPayment_" + sTableName, dspayment);




                //    }
                //}



                int bankid;
                int chequeno;



                if (ddlPMode.SelectedValue == "1")
                {
                    bankid = 0;
                    chequeno = 0;
                }
                else if (ddlPMode.SelectedValue == "5")
                {
                    bankid = Convert.ToInt32(ddlBankName.SelectedValue);
                    chequeno = 0;
                    utr = "0";

                }
                else if (ddlPMode.SelectedValue == "4")
                {
                    bankid = Convert.ToInt32(ddlBankName.SelectedValue);
                    utr = txtutr.Text;
                    chequeno = 0;

                }
                else
                {
                    bankid = Convert.ToInt32(ddlBankName.SelectedValue);
                    chequeno = Convert.ToInt32(ddlChequeNo.SelectedItem.Text);
                }

                DataTable dttt;
                DataRow dr;
                DataColumn dct;
                DataSet dstd = new DataSet();
                dttt = new DataTable();
                dct = new DataColumn("P_ID");
                dttt.Columns.Add(dct);

                dct = new DataColumn("DC_NO");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Bill_NO");
                dttt.Columns.Add(dct);

                dct = new DataColumn("DC_Date");
                dttt.Columns.Add(dct);

                dct = new DataColumn("BillAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Balance");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Amount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("PurchaseReturnAmount");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                for (int vLoop = 0; vLoop < TransPaymentGrid.Rows.Count; vLoop++)
                {

                    Label txtPID = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtPID");
                    Label txttt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtDCNo");
                    Label txttt1 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillno");
                    //Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                    Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                    recdate = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Label txttd = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillAmount");
                    Label txttd123 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBalance");
                    TextBox txttdtt = (TextBox)TransPaymentGrid.Rows[vLoop].FindControl("txtAmount");
                    Label txtPurchaseReturn = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtPurchaseReturn");

                    dr = dttt.NewRow();
                    dr["P_ID"] = txtPID.Text;
                    dr["DC_NO"] = txttt.Text;
                    dr["Bill_NO"] = txttt1.Text;
                    dr["BillAmount"] = txttd.Text;
                    dr["Balance"] = txttd123.Text;
                    dr["DC_Date"] = recdate;
                    dr["Amount"] = Convert.ToDouble(txttdtt.Text);
                    dr["PurchaseReturnAmount"] = txtPurchaseReturn.Text;
                    //dr["Balance"] = Convert.ToDouble(txttd123.Text) - Convert.ToDouble(txttdtt.Text);
                    if (Convert.ToDouble(txttdtt.Text) > 0)
                    {
                        dstd.Tables[0].Rows.Add(dr);
                    }

                    //dstd.Tables[0].Rows.Add(dr);

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
                //int iStatus = objBs.TransPaymentInsert("tblTransPayment_" + sTableName, dstd, Convert.ToInt32(txtPaymentNo.Text));

                // DateTime EntryDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                // DateTime EditDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                int j = objBs.paymentinsert("tblDayBook_" + sTableName, "tblPayment_" + sTableName, "tblTransPayment_" + sTableName, dstd, sTableName, Convert.ToInt32(0), Convert.ToInt32(txtPaymentNo.Text), date, Convert.ToInt32(ddlLName.SelectedValue), Convert.ToInt32(ddlPMode.SelectedValue), Convert.ToDecimal(txtAmount.Text), bankid, Convert.ToInt32(chequeno), "Supplier", txtNarration.Text, txtAgainst.Text, "tblAuditMaster_" + sTableName, lblUser.Text, "Supplier", ddlPMode.SelectedItem.Text, utr, EmpId, Convert.ToInt32(0), Convert.ToInt32(drpGsttype.SelectedValue), Convert.ToInt32(TAXVALUE), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtCGST.Text), Convert.ToDouble(txtSGST.Text), Convert.ToDouble(txtIGST.Text), Convert.ToDouble(txtgstAmount.Text), lblFile_Path.Text, Convert.ToDouble(Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtCGST.Text) - Convert.ToDouble(txtSGST.Text) - Convert.ToDouble(txtIGST.Text)));


                string iVoucherNo = Request.QueryString.Get("iVoucherNo");
                if (iVoucherNo != "" || iVoucherNo != null)
                {
                  //  int g = objBs.updatePaymentVoucherStatus(iVoucherNo, sTableName);
                }

                Response.Redirect("PaymentNew.aspx");



                //{
                //  //  int j = objBs.insertpayment("tblDayBook_" + sTableName, "tblPayment_" + sTableName, sTableName, Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(txtPaymentNo.Text), txtpdate.Text, Convert.ToInt32(ddlLName.SelectedValue), Convert.ToInt32(ddlPMode.SelectedValue), Convert.ToDecimal(txtAmount.Text), ddlBankName.SelectedValue, txtChequeNo.Text, "Payment", ddlType.SelectedItem.Text, txtNarration.Text);
                //    Response.Redirect("PaymentGrit.aspx");
                //}

            }
            else
            {

                if (txteditnarration.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Mention Reason For Edit.Thank You!');", true);
                    txteditnarration.Focus();
                    return;
                }

                int bankid;
                int chequeno;



                if (ddlPMode.SelectedValue == "1")
                {
                    bankid = 0;
                    chequeno = 0;
                }
                else if (ddlPMode.SelectedValue == "5")
                {
                    bankid = Convert.ToInt32(ddlBankName.SelectedValue);
                    chequeno = 0;
                    utr = "0";

                }
                else if (ddlPMode.SelectedValue == "4")
                {
                    bankid = Convert.ToInt32(ddlBankName.SelectedValue);
                    utr = txtutr.Text;
                    chequeno = 0;

                }
                else
                {
                    bankid = Convert.ToInt32(ddlBankName.SelectedValue);
                    chequeno = Convert.ToInt32(ddlChequeNo.SelectedItem.Text);
                }

                DataTable dttt;
                DataRow dr;
                DataColumn dct;
                DataSet dstd = new DataSet();
                dttt = new DataTable();
                dct = new DataColumn("P_ID");
                dttt.Columns.Add(dct);

                dct = new DataColumn("DC_NO");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Bill_NO");
                dttt.Columns.Add(dct);

                dct = new DataColumn("DC_Date");
                dttt.Columns.Add(dct);

                dct = new DataColumn("BillAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Balance");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Amount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("PurchaseReturnAmount");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                for (int vLoop = 0; vLoop < TransPaymentGrid.Rows.Count; vLoop++)
                {
                    Label txtPID = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtPID");
                    Label txttt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtDCNo");
                    Label txttt1 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillno");
                    //   Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                    Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                    recdate = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Label txttd = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillAmount");
                    Label txttd123 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBalance");
                    TextBox txttdtt = (TextBox)TransPaymentGrid.Rows[vLoop].FindControl("txtAmount");
                    Label txtPurchaseReturn = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtPurchaseReturn");

                    if (txttdtt.Text != "0")
                    {
                        dr = dttt.NewRow();
                        dr["P_ID"] = txtPID.Text;
                        dr["DC_NO"] = txttt.Text;
                        dr["Bill_NO"] = txttt1.Text;
                        dr["BillAmount"] = txttd.Text;
                        dr["Balance"] = txttd123.Text;
                        dr["DC_Date"] = recdate;
                        dr["Amount"] = Convert.ToDouble(txttdtt.Text);
                        dr["Balance"] = Convert.ToDouble(txttd.Text) - Convert.ToDouble(txttdtt.Text);
                        dr["PurchaseReturnAmount"] = Convert.ToDouble(txtPurchaseReturn.Text);
                        dstd.Tables[0].Rows.Add(dr);
                    }
                }

                // DateTime EditDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                //int iStatus = objBs.TransPaymentInsert("tblTransPayment_" + sTableName, dstd, Convert.ToInt32(txtPaymentNo.Text));
                //int j = objBs.Paymentupdate("tblDayBook_" + sTableName, "tblPayment_" + sTableName, "tblTransPayment_" + sTableName, dstd, sTableName, Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(txtPaymentNo.Text), date, Convert.ToInt32(ddlLName.SelectedValue), Convert.ToInt32(ddlPMode.SelectedValue), Convert.ToDouble(txtAmount.Text), ddlBankName.SelectedValue, ddlChequeNo.SelectedItem.Text, ddlType.SelectedItem.Text, txtNarration.Text, DayBookID, DayBookID, txtAgainst.Text, "tblAuditMaster_" + sTableName, lblUser.Text, ddlLName.SelectedItem.Text, ddlPMode.SelectedItem.Text, txtutr.Text, EmpId, Convert.ToInt32(0), Convert.ToInt32(drpGsttype.SelectedValue), Convert.ToInt32(TAXVALUE), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtCGST.Text), Convert.ToDouble(txtSGST.Text), Convert.ToDouble(txtIGST.Text), Convert.ToDouble(txtgstAmount.Text), Convert.ToDouble(Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtCGST.Text) - Convert.ToDouble(txtSGST.Text) - Convert.ToDouble(txtIGST.Text)), txteditnarration.Text);

                Response.Redirect("PaymentGrit.aspx");

            }

            //{
            //    //int j = objBs.Paymentupdate("tblDayBook_" + sTableName, "tblPayment_" + sTableName, Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(txtPaymentNo.Text), txtpdate.Text, Convert.ToInt32(ddlLName.SelectedValue), Convert.ToInt32(ddlPMode.SelectedValue), Convert.ToDecimal(txtAmount.Text), ddlBankName.SelectedValue, txtChequeNo.Text, "Payment", ddlType.SelectedItem.Text, txtNarration.Text, DayBookID, DayBookID);
            //    Response.Redirect("PaymentGrit.aspx");
            //}



        }

        protected void ddlPMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlPMode.SelectedItem.Text == "Cheque" || ddlPMode.SelectedItem.Text == "DD")
            //{
            //    ddlBankName.Enabled = true;
            //    ddlChequeNo.Enabled = true;
            //    txtAgainst.Visible = false;
            //    ddlBankName.Focus();
            //}
            //else
            //{
            //    ddlBankName.SelectedIndex = 0;
            //    ddlChequeNo.SelectedItem.Text= "Select Chequeno";
            //    ddlBankName.Enabled = false;
            //    ddlChequeNo.Enabled = false;


            //}
            if (ddlPMode.SelectedValue == "1")
            {
                ddlBankName.Enabled = false;
                ddlChequeNo.SelectedItem.Text = "Select Chequeno";
                ddlChequeNo.Enabled = false;

                ddlBankName.SelectedIndex = 0;
                txtutr.Enabled = false;
                txtutr.Text = "0";
                // ddlChequeNo.Text = "";

            }
            else if (ddlPMode.SelectedValue == "5")
            {
                ddlBankName.Enabled = true;
                ddlChequeNo.Enabled = false;
                ddlChequeNo.SelectedItem.Text = "Select Chequeno";
                txtutr.Enabled = false;
                txtutr.Text = "0";
                //  ddlBank.SelectedIndex = 0;
                //ddlChequeNo.Text = "0";

            }
            else if (ddlPMode.SelectedValue == "4")
            {
                ddlBankName.Enabled = true;
                ddlChequeNo.Enabled = false;
                ddlChequeNo.SelectedItem.Text = "Select Chequeno";
                txtutr.Enabled = true;
                txtutr.Text = "0";
                //  ddlBank.SelectedIndex = 0;
                //ddlChequeNo.Text = "0";

            }

            else
            {
                ddlBankName.Enabled = true;
                ddlChequeNo.Enabled = true;
                ddlBankName.Focus();
            }
        }





        protected void btnexit_Click1(object sender, EventArgs e)
        {
            Response.Redirect("PaymentGrit.aspx");
        }

        protected void txtpaymentnochnaged(object sender, EventArgs e)
        {
            if (btnadd.Text == "Save")
            {
                if (txtPaymentNo.Text == "")
                {
                    txtPaymentNo.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please enter Payment Number!!!.');", true);
                    return;
                }
                else
                {
                    //DataSet ds = objBs.duplicateno(txtPaymentNo.Text, sTableName, "Save", "0");
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    txtPaymentNo.Focus();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Payment No Already Exists.Thank You!!!');", true);
                    //    return;
                    //}
                    //else
                    //{
                    //    txtpdate.Focus();
                    //}
                }
            }
            else
            {
                if (txtPaymentNo.Text == "")
                {
                    txtPaymentNo.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please enter Payment Number!!!.');", true);
                    return;
                }
                else
                {
                        //DataSet ds = objBs.duplicateno(txtPaymentNo.Text, sTableName, "Update", txtpaymentid.Text);
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    txtPaymentNo.Focus();
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Payment No Already Exists.Thank You!!!');", true);
                        //    return;
                        //}
                        //else
                        //{
                        //    txtpdate.Focus();
                        //}
                }
            }
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
            
                TransPaymentGrid.Visible = true;


                double adtotal = Convert.ToDouble(txtAmount.Text);

                if (TransPaymentGrid.Rows.Count > 0)
                {
                    DataTable dttt;
                    DataRow drNew;
                    DataColumn dct;
                    DataSet dstd = new DataSet();
                    dttt = new DataTable();

                    dct = new DataColumn("P_ID");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("DC_NO");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Bill_NO");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Amount");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("DC_Date");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("BillAmount");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("PurchaseReturnAmount");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Balance");
                    dttt.Columns.Add(dct);

                    dstd.Tables.Add(dttt);

                    for (int vLoop = 0; vLoop < TransPaymentGrid.Rows.Count; vLoop++)
                    {
                        Label txtPID = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtPID");
                        Label txttt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtDCNo");
                        Label txttt1 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillno");
                        Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                        Label txttd = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillAmount");
                        Label txttd123 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBalance");
                        TextBox txttdtt = (TextBox)TransPaymentGrid.Rows[vLoop].FindControl("txtAmount");
                        Label txtPurchaseReturn = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtPurchaseReturn");



                        drNew = dttt.NewRow();
                        drNew["P_ID"] = txtPID.Text;
                        drNew["DC_NO"] = txttt.Text;
                        drNew["Bill_NO"] = txttt1.Text;
                        drNew["BillAmount"] = txttd.Text;
                        drNew["Balance"] = txttd123.Text;

                        drNew["DC_Date"] = txt.Text;
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

                        drNew["PurchaseReturnAmount"] = Convert.ToDouble(txtPurchaseReturn.Text);
                        dstd.Tables[0].Rows.Add(drNew);
                    }
                    TransPaymentGrid.DataSource = dstd;
                    TransPaymentGrid.DataBind();

                }
                else
                {
                    TransPaymentGrid.DataSource = null;
                    TransPaymentGrid.DataBind();

                }
            
        }

        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBankName.SelectedItem.Text != "Select Bank Name")
            {
                ds = objBs.getBankLedger(4, Convert.ToInt32(ddlBankName.SelectedValue), sTableName);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlChequeNo.DataSource = ds;
                        ddlChequeNo.DataTextField = "chequeno";
                        ddlChequeNo.DataValueField = "TransChequeId";
                        ddlChequeNo.DataBind();
                        ddlChequeNo.Items.Insert(0, "Select Chequeno");

                    }
                    else
                    {
                        ddlChequeNo.Items.Insert(0, "Select Chequeno");
                    }
                }
                else
                {
                    ddlChequeNo.Items.Insert(0, "Select Chequeno");
                }
            }
        }

        protected void gridbutton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentGrit.aspx");
        }




    }
}