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
using Org.BouncyCastle.Ocsp;

namespace Billing.Accountsbootstrap
{
    public partial class DebitNote : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        DataSet ds=new DataSet();
        int EmpId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            //sTableName = Session["User"].ToString();
            EmpId = Convert.ToInt32(Session["EmpId"].ToString());


            if (!IsPostBack)
            {

                DataSet dsNoteno = objBs.NoteNumber("tblCreditDebitNote_" + sTableName);
                if (dsNoteno != null)
                {
                    if (dsNoteno.Tables[0].Rows.Count > 0)
                    {

                        if (dsNoteno.Tables[0].Rows[0]["Note_NO"].ToString() == "")
                            txtxNoteno.Text = "1";
                        else
                            txtxNoteno.Text = dsNoteno.Tables[0].Rows[0]["Note_NO"].ToString();
                    }
                }

                txtDCDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                DataSet dsed = objBs.CheckEditDelete(EmpId);
                if (dsed.Tables[0].Rows[0]["allowdate"].ToString() == "1")
                {
                    txtDCDate.Enabled = true;
                }
                else
                {
                    txtDCDate.Enabled = false;
                }
                
                ds = objBs.Getcustomers4Debit(sTableName);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlLname.DataSource = ds.Tables[0];
                        ddlLname.DataTextField = "LedgerName";
                        ddlLname.DataValueField = "LedgerID";
                        ddlLname.DataBind();
                        ddlLname.Items.Insert(0, "Select Ledger");
                    }
                    else
                    {
                        ddlLname.Items.Insert(0, "Select Ledger");
                    }
                }
                else
                {
                    ddlLname.Items.Insert(0, "Select Ledger");
                }
               
                string DayBookID = Request.QueryString.Get("DayBook_ID");
                if (DayBookID != "" || DayBookID != null)
                {
                    DataSet ds1 = objBs.getCreditDebitmaster("tblCreditDebitNote_" + sTableName, DayBookID);
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Text = "Update";

                            ds = objBs.getLedger(sTableName);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {

                                    ddlLname.DataSource = ds.Tables[0];
                                    ddlLname.DataTextField = "LedgerName";
                                    ddlLname.DataValueField = "LedgerID";
                                    ddlLname.DataBind();
                                    ddlLname.Items.Insert(0, "Select Ledger");
                                }
                                else
                                {
                                    ddlLname.Items.Insert(0, "Select Ledger");
                                }
                            }
                            else
                            {
                                ddlLname.Items.Insert(0, "Select Ledger");
                            }


                            txtxNoteno.Text = ds1.Tables[0].Rows[0]["Note_NO"].ToString();
                            txtDCDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                            RbtnCD.SelectedValue = ds1.Tables[0].Rows[0]["Type"].ToString();
                            ddlLname.SelectedValue = ds1.Tables[0].Rows[0]["Ledger_Name"].ToString();

                            DataSet ledgerid = objBs.getledgerdet(Convert.ToInt32(ddlLname.SelectedValue), sTableName);

                            string Ledger = ledgerid.Tables[0].Rows[0]["GroupID"].ToString();

                           
                              if (Convert.ToInt32(Ledger) == 1)
                              {
                                  TransPaymentGrid.Visible = false;
                                  gvledgrid.Visible = true;
                                 
                                  DataSet dstt = objBs.selecttransReceipt(Convert.ToInt32(DayBookID), "tblTransReceipt_" + sTableName);
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
                              }
                              if (Convert.ToInt32(Ledger) == 2)
                              {
                                  TransPaymentGrid.Visible = true;
                                  gvledgrid.Visible = false;

                                  DataSet dsTransPayment = objBs.getTransPaymentdet("tblTransPayment_" + sTableName, Convert.ToInt32(DayBookID));
                                  if (dsTransPayment != null)
                                  {
                                      if (dsTransPayment.Tables[0].Rows.Count > 0)
                                      {
                                          DataTable dttt;
                                          DataRow drNew;
                                          DataColumn dct;
                                          DataSet dstd = new DataSet();
                                          dttt = new DataTable();

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

                                          dstd.Tables.Add(dttt);

                                          if (dsTransPayment != null)
                                          {
                                              if (dsTransPayment.Tables[0].Rows.Count > 0)
                                              {
                                                  for (int i = 0; i < dsTransPayment.Tables[0].Rows.Count; i++)
                                                  {
                                                      drNew = dttt.NewRow();
                                                      drNew["DC_NO"] = Convert.ToInt32(dsTransPayment.Tables[0].Rows[i]["Invoice_No"]);
                                                      drNew["DC_Date"] = Convert.ToDateTime(dsTransPayment.Tables[0].Rows[i]["BillDate"]).ToString("dd/MM/yyyy");
                                                      drNew["Bill_NO"] = Convert.ToInt32(dsTransPayment.Tables[0].Rows[i]["BillNo"]);
                                                      drNew["BillAmount"] = Convert.ToDouble(dsTransPayment.Tables[0].Rows[i]["BillAmount"]);
                                                      drNew["Balance"] = Convert.ToDouble(dsTransPayment.Tables[0].Rows[i]["Balance"]);
                                                      drNew["Amount"] = Convert.ToDouble(dsTransPayment.Tables[0].Rows[i]["Amount"]); ;
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
                              txtAmount.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Amount"]).ToString("0.00");
                              txtNar.Text = ds1.Tables[0].Rows[0]["Narration"].ToString();
                          }
                      }
                  }
                  ddlLname.Focus();
              }
              ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        
            
        }


        protected void btnadd_Click(object sender, EventArgs e)
        {
            DateTime recdate;
            string DayBookID = Request.QueryString.Get("DayBook_ID");
            if (btnadd.Text == "Save")
            {
                if (ddlLname.SelectedItem.Text != "Select Ledger")
                {
                    DataSet ledgerid = objBs.getledgerdet(Convert.ToInt32(ddlLname.SelectedValue), sTableName);

                    string Ledger = ledgerid.Tables[0].Rows[0]["GroupID"].ToString();

                    if (Convert.ToInt32(Ledger) == 1)
                    {
                        TransPaymentGrid.Visible = false;
                        gvledgrid.Visible = true;


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

                        dct = new DataColumn("BillAmount");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Balance");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        for (int vLoop = 0; vLoop < gvledgrid.Rows.Count; vLoop++)
                        {
                            Label txtd = (Label)gvledgrid.Rows[vLoop].FindControl("txtSalesid");
                            Label txttt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillno");
                            // Label txt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillDate");
                            Label txt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillDate");
                            recdate = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Label txttd = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillAmount");
                            Label txttd123 = (Label)gvledgrid.Rows[vLoop].FindControl("txtBalance");
                            TextBox txttdtt = (TextBox)gvledgrid.Rows[vLoop].FindControl("txtAmount");

                            drNew = dttt.NewRow();
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



                        DateTime txtdate = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                      
                                                if (RbtnCD.SelectedValue == "Credit Note")
                                                {
                                                    int j = objBs.insertDayBook("tblDayBook_" + sTableName, "tblCreditDebitNote_" + sTableName, "tblTransPayment_" + sTableName, "tblTransReceipt_" + sTableName, txtdate, Convert.ToInt32(0), Convert.ToInt32(ddlLname.SelectedValue), txtNar.Text, txtxNoteno.Text, RbtnCD.SelectedValue, Convert.ToDecimal(txtAmount.Text), ddlLname.SelectedItem.Text, "tblAuditMaster_" + sTableName, Convert.ToString(EmpId), sTableName, dstd, Ledger);
                                                    Response.Redirect("DebitNoteGrid.aspx");

                                                }
                                                else
                                                {
                                                    int j = objBs.insertDayBook("tblDayBook_" + sTableName, "tblCreditDebitNote_" + sTableName, "tblTransPayment_" + sTableName, "tblTransReceipt_" + sTableName, txtdate, Convert.ToInt32(ddlLname.SelectedValue), Convert.ToInt32(0), txtNar.Text, txtxNoteno.Text, RbtnCD.SelectedValue, Convert.ToDecimal(txtAmount.Text), ddlLname.SelectedItem.Text, "tblAuditMaster_" + sTableName, Convert.ToString(EmpId), sTableName, dstd, Ledger);
                                                    Response.Redirect("DebitNoteGrid.aspx");
                                                }

                                            }
                                            else if (Convert.ToInt32(Ledger) == 2)
                                            {
                                                gvledgrid.Visible = false;
                                                TransPaymentGrid.Visible = true;


                                                DataTable dttt;
                                                DataRow dr;
                                                DataColumn dct;
                                                DataSet dstd = new DataSet();
                                                dttt = new DataTable();
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
                                                dstd.Tables.Add(dttt);

                                                for (int vLoop = 0; vLoop < TransPaymentGrid.Rows.Count; vLoop++)
                                                {
                                                    Label txttt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtDCNo");
                                                    Label txttt1 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillno");
                                                    // Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                                                    Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                                                    recdate = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    Label txttd = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillAmount");
                                                    Label txttd123 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBalance");
                                                    TextBox txttdtt = (TextBox)TransPaymentGrid.Rows[vLoop].FindControl("txtAmount");

                                                    dr = dttt.NewRow();
                                                    dr["DC_NO"] = txttt.Text;
                                                    dr["Bill_NO"] = txttt1.Text;
                                                    dr["BillAmount"] = txttd.Text;
                                                    dr["Balance"] = txttd123.Text;
                                                    dr["DC_Date"] = recdate;
                                                    dr["Amount"] = Convert.ToDouble(txttdtt.Text);
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


                                                DateTime txtdate = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                                if (RbtnCD.SelectedValue == "Credit Note")
                                                {
                                                    int j = objBs.insertDayBook("tblDayBook_" + sTableName, "tblCreditDebitNote_" + sTableName, "tblTransPayment_" + sTableName, "tblTransReceipt_" + sTableName, txtdate, Convert.ToInt32(0), Convert.ToInt32(ddlLname.SelectedValue), txtNar.Text, txtxNoteno.Text, RbtnCD.SelectedValue, Convert.ToDecimal(txtAmount.Text), ddlLname.SelectedItem.Text, "tblAuditMaster_" + sTableName, Convert.ToString(EmpId), sTableName, dstd, Ledger);
                                                    Response.Redirect("DebitNoteGrid.aspx");

                                                }
                                                else
                                                {
                                                    int j = objBs.insertDayBook("tblDayBook_" + sTableName, "tblCreditDebitNote_" + sTableName, "tblTransPayment_" + sTableName, "tblTransReceipt_" + sTableName, txtdate, Convert.ToInt32(ddlLname.SelectedValue), Convert.ToInt32(0), txtNar.Text, txtxNoteno.Text, RbtnCD.SelectedValue, Convert.ToDecimal(txtAmount.Text), ddlLname.SelectedItem.Text, "tblAuditMaster_" + sTableName, Convert.ToString(EmpId), sTableName, dstd, Ledger);
                                                    Response.Redirect("DebitNoteGrid.aspx");
                                                }
                                            }

                                        }

                                    }
                                    else if (btnadd.Text == "Update")
                                    {
                                        if (ddlLname.SelectedItem.Text != "Select Ledger")
                                        {
                                            DataSet ledgerid = objBs.getledgerdet(Convert.ToInt32(ddlLname.SelectedValue), sTableName);

                                            string Ledger = ledgerid.Tables[0].Rows[0]["GroupID"].ToString();

                                            if (Convert.ToInt32(Ledger) == 1)
                                            {
                                                TransPaymentGrid.Visible = false;
                                                gvledgrid.Visible = true;


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

                                                dct = new DataColumn("BillAmount");
                                                dttt.Columns.Add(dct);

                                                dct = new DataColumn("Balance");
                                                dttt.Columns.Add(dct);

                                                dstd.Tables.Add(dttt);

                                                for (int vLoop = 0; vLoop < gvledgrid.Rows.Count; vLoop++)
                                                {
                                                    Label txtd = (Label)gvledgrid.Rows[vLoop].FindControl("txtSalesid");
                                                    Label txttt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillno");
                                                    //  Label txt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillDate");
                                                    Label txt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillDate");
                                                    recdate = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    Label txttd = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillAmount");
                                                    Label txttd123 = (Label)gvledgrid.Rows[vLoop].FindControl("txtBalance");
                                                    TextBox txttdtt = (TextBox)gvledgrid.Rows[vLoop].FindControl("txtAmount");

                                                    drNew = dttt.NewRow();
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


                                                DateTime txtdate = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                                int j = objBs.CreditDebitupdate("tblDayBook_" + sTableName, "tblCreditDebitNote_" + sTableName, "tblTransPayment_" + sTableName, "tblTransReceipt_" + sTableName, ddlLname.SelectedValue, Convert.ToDecimal(txtAmount.Text), txtdate, RbtnCD.SelectedValue, txtNar.Text, Convert.ToInt32(txtxNoteno.Text), Convert.ToInt32(DayBookID), ddlLname.SelectedItem.Text, "tblAuditMaster_" + sTableName, Convert.ToString(EmpId), dstd, Ledger);
                                                Response.Redirect("DebitNoteGrid.aspx");


                                            }
                                            else if (Convert.ToInt32(Ledger) == 2)
                                            {
                                                gvledgrid.Visible = false;
                                                TransPaymentGrid.Visible = true;


                                                DataTable dttt;
                                                DataRow dr;
                                                DataColumn dct;
                                                DataSet dstd = new DataSet();
                                                dttt = new DataTable();
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
                                                dstd.Tables.Add(dttt);

                                                for (int vLoop = 0; vLoop < TransPaymentGrid.Rows.Count; vLoop++)
                                                {
                                                    Label txttt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtDCNo");
                                                    Label txttt1 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillno");
                                                    // Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                                                    Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                                                    recdate = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    Label txttd = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillAmount");
                                                    Label txttd123 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBalance");
                                                    TextBox txttdtt = (TextBox)TransPaymentGrid.Rows[vLoop].FindControl("txtAmount");

                                                    dr = dttt.NewRow();
                                                    dr["DC_NO"] = txttt.Text;
                                                    dr["Bill_NO"] = txttt1.Text;
                                                    dr["BillAmount"] = txttd.Text;
                                                    dr["Balance"] = txttd123.Text;
                                                    dr["DC_Date"] = recdate;
                                                    dr["Amount"] = Convert.ToDouble(txttdtt.Text);
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

                                                DateTime txtdate = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                                int j = objBs.CreditDebitupdate("tblDayBook_" + sTableName, "tblCreditDebitNote_" + sTableName, "tblTransPayment_" + sTableName, "tblTransReceipt_" + sTableName, ddlLname.SelectedValue, Convert.ToDecimal(txtAmount.Text), txtdate, RbtnCD.SelectedValue, txtNar.Text, Convert.ToInt32(txtxNoteno.Text), Convert.ToInt32(DayBookID), ddlLname.SelectedItem.Text, "tblAuditMaster_" + sTableName, Convert.ToString(EmpId), dstd, Ledger);
                                                Response.Redirect("DebitNoteGrid.aspx");

                                            }

                                        }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("DebitNoteGrid.aspx");
        }

        protected void ddlLname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLname.SelectedItem.Text != "Select Ledger")
            {
                DataSet ledgerid = objBs.getledgerdet(Convert.ToInt32(ddlLname.SelectedValue), sTableName);

                string Ledger = ledgerid.Tables[0].Rows[0]["GroupID"].ToString();

                if (Convert.ToInt32(Ledger) == 1)
                {
                    TransPaymentGrid.Visible = false;
                    gvledgrid.Visible = true;



                    DataSet ds = objBs.GetCreditSales((Convert.ToInt32(ddlLname.SelectedValue)), "tblSales_" + sTableName, "tblDayBook_" + sTableName, sTableName, sTableName);

                    DataSet dsReceiptDet = objBs.GetReceiptDetails("tblDayBook_" + sTableName, "tblTransReceipt_" + sTableName, "tblReceipt_" + sTableName);


                    if (ds != null)
                    {
                        foreach (DataRow dr in dsReceiptDet.Tables[0].Rows)
                        {
                            var billNo = dr["BillNo"].ToString();
                            var billAmount = dr["TotalAmount"].ToString();

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (billNo == ds.Tables[0].Rows[i]["BillNo"].ToString())
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

                                        drNew["BillDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString("dd/MM/yyyy");
                                        drNew["SalesID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["BuyerOrderSalesId"]);
                                        drNew["Billno"] = (ds.Tables[0].Rows[i]["Billno"]);
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
                        }

                    }
                    else
                    {
                        gvledgrid.DataSource = null;
                        gvledgrid.DataBind();
                        gvledgrid.Visible = true;
                    }

                }
                    
                else if (Convert.ToInt32(Ledger) == 2)
                {
                    gvledgrid.Visible = false;
                    TransPaymentGrid.Visible = true;
                    DataSet ds = objBs.GetCreditPurchasenew((Convert.ToInt32(ddlLname.SelectedValue)), "tblPurchase_" + sTableName, "tblDayBook_" + sTableName);

                    DataSet dsReceiptDet = objBs.GetPaymentDetails("tblDayBook_" + sTableName, "tblTransPayment_" + sTableName, "tblPayment_" + sTableName);

                    if (ds != null)
                    {
                        foreach (DataRow dr in dsReceiptDet.Tables[0].Rows)
                        {
                            var billNo = dr["Invoice_No"].ToString();
                            var billAmount = dr["TotalAmount"].ToString();

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (billNo.Trim() == ds.Tables[0].Rows[i]["DC_NO"].ToString())
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

                            dstd.Tables.Add(dttt);

                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        drNew = dttt.NewRow();
                                        drNew["DC_NO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["DC_NO"]);
                                        drNew["DC_Date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["DC_Date"]).ToString("dd/MM/yyyy");
                                        drNew["Bill_NO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["Bill_NO"]);
                                        drNew["BillAmount"] = Convert.ToDouble(ds.Tables[0].Rows[i]["BillAmount"]);
                                        drNew["Balance"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"]);
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
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (ddlLname.SelectedItem.Text != "Select Ledger")
            {
                DataSet ledgerid = objBs.getledgerdet(Convert.ToInt32(ddlLname.SelectedValue), sTableName);

                string Ledger = ledgerid.Tables[0].Rows[0]["GroupID"].ToString();

                if (Convert.ToInt32(Ledger) == 1)
                {
                    TransPaymentGrid.Visible = false;
                    gvledgrid.Visible = true;

                    double adtotal = Convert.ToDouble(txtAmount.Text);

                    if (gvledgrid.Rows.Count > 0)
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

                        dct = new DataColumn("BillAmount");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Balance");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        for (int vLoop = 0; vLoop < gvledgrid.Rows.Count; vLoop++)
                        {
                            Label txtd = (Label)gvledgrid.Rows[vLoop].FindControl("txtSalesid");
                            Label txttt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillno");
                            Label txt = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillDate");
                            Label txttd = (Label)gvledgrid.Rows[vLoop].FindControl("txtBillAmount");
                            Label txttd123 = (Label)gvledgrid.Rows[vLoop].FindControl("txtBalance");
                            TextBox txttdtt = (TextBox)gvledgrid.Rows[vLoop].FindControl("txtAmount");

                            drNew = dttt.NewRow();
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

                }
                else if (Convert.ToInt32(Ledger) == 2)
                {
                    gvledgrid.Visible = false;
                    TransPaymentGrid.Visible = true;

                    double adtotal = Convert.ToDouble(txtAmount.Text);

                    if (TransPaymentGrid.Rows.Count > 0)
                    {
                        DataTable dttt;
                        DataRow drNew;
                        DataColumn dct;
                        DataSet dstd = new DataSet();
                        dttt = new DataTable();

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

                        dct = new DataColumn("Balance");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        for (int vLoop = 0; vLoop < TransPaymentGrid.Rows.Count; vLoop++)
                        {
                            Label txttt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtDCNo");
                            Label txttt1 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillno");
                            Label txt = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillDate");
                            Label txttd = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBillAmount");
                            Label txttd123 = (Label)TransPaymentGrid.Rows[vLoop].FindControl("txtBalance");
                            TextBox txttdtt = (TextBox)TransPaymentGrid.Rows[vLoop].FindControl("txtAmount");

                            drNew = dttt.NewRow();
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

            }

        }
    }

}