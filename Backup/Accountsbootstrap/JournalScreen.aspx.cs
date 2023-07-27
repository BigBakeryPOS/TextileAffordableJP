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

namespace Billing.Accountsbootstrap
{
    public partial class JournalScreen : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        string sTableName = "";

        int EmpId = 0;
        string EmployeeName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            //EmpId = Convert.ToInt32(Session["EmpId"].ToString());
            //EmployeeName = Session["EmployeeName"].ToString();

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                FirstGridViewRow1();

                string ijourID = Request.QueryString.Get("TransNo");
                if (ijourID != null)
                {
                    btnadd.Text = "Update";
                    DataSet DR = objbs.GetJournalNo(Convert.ToInt32(ijourID), "tblJournal_" + sTableName, "tblDayBook_" + sTableName);
                    if (DR.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtt;
                        DataRow drNew;
                        DataColumn dct;
                        DataSet dstd = new DataSet();
                        dtt = new DataTable();

                        dct = new DataColumn("JVNo");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("date");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("DebtorId");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("CreditorId");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("Narration");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("Amount");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("PayMode");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("BankName");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("ChequeNO");
                        dtt.Columns.Add(dct);

                        dct = new DataColumn("AganistBillNo");
                        dtt.Columns.Add(dct);

                        dstd.Tables.Add(dtt);
                        foreach (DataRow dr in DR.Tables[0].Rows)
                        {
                            drNew = dtt.NewRow();
                            drNew["JVNo"] = dr["JV_NO"];
                            drNew["date"] = dr["TransDate"];
                            drNew["DebtorId"] = dr["DebtorID"];
                            drNew["CreditorId"] = dr["CreditorID"];
                            drNew["Narration"] = dr["Narration"];
                            drNew["Amount"] = dr["Amount"];
                            drNew["PayMode"] = dr["PayMode"];
                            //string pay = dr["Paymode"].ToString();
                            //if (pay == "Cheque" || pay == "DD")
                            //{

                            //}
                            drNew["BankName"] = dr["BankID"];
                            drNew["ChequeNO"] = dr["ChequeNo"];
                            drNew["AganistBillNo"] = dr["AganistBillNo"];
                            dstd.Tables[0].Rows.Add(drNew);
                        }

                        ViewState["CurrentTable1"] = dstd;

                        GridView2.DataSource = dstd;
                        GridView2.DataBind();

                        for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                        {
                            TextBox txJvno = (TextBox)GridView2.Rows[vLoop].FindControl("txtJvno");
                            TextBox txDate = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                            DropDownList txDebetor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlDebtor");
                            DropDownList txCreditor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlCreditor");
                            TextBox txNarration = (TextBox)GridView2.Rows[vLoop].FindControl("txtNarration");
                            TextBox txAmount = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                            DropDownList txPaymode = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlpaymentmode");
                            DropDownList txbank = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlBankname");
                            TextBox txChequeno = (TextBox)GridView2.Rows[vLoop].FindControl("txtChequeno");
                            TextBox txAgbillno = (TextBox)GridView2.Rows[vLoop].FindControl("txtAganistBno");

                            txJvno.Text = dstd.Tables[0].Rows[vLoop]["JVNo"].ToString();
                            txDate.Text = Convert.ToDateTime(dstd.Tables[0].Rows[vLoop]["date"]).ToString("dd/MM/yyyy");
                            txDebetor.SelectedValue = dstd.Tables[0].Rows[vLoop]["DebtorId"].ToString();
                            txCreditor.SelectedValue = dstd.Tables[0].Rows[vLoop]["CreditorId"].ToString();
                            txNarration.Text = dstd.Tables[0].Rows[vLoop]["Narration"].ToString();
                            txAmount.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Amount"]).ToString("0.000");
                            txPaymode.SelectedValue = dstd.Tables[0].Rows[vLoop]["PayMode"].ToString();
                            if (txPaymode.SelectedValue == "Cheque" || txPaymode.SelectedValue == "DD")
                            {
                                txbank.Enabled = true;
                                txChequeno.Enabled = true;
                                txbank.SelectedValue = dstd.Tables[0].Rows[vLoop]["BankName"].ToString();
                                txChequeno.Text = dstd.Tables[0].Rows[vLoop]["ChequeNO"].ToString();
                            }
                            else
                            {
                                txbank.Enabled = false;
                                txChequeno.Enabled = false;
                                txbank.SelectedValue = dstd.Tables[0].Rows[vLoop]["BankName"].ToString();
                                txChequeno.Text = dstd.Tables[0].Rows[vLoop]["ChequeNO"].ToString();
                            }
                           
                            txAgbillno.Text = dstd.Tables[0].Rows[vLoop]["AganistBillNo"].ToString();
                        }



                    }
                }
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
             DateTime recdate;
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                TextBox txJvno = (TextBox)GridView2.Rows[vLoop].FindControl("txtJvno");
                TextBox txDate = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                DropDownList txDebetor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlDebtor");
                DropDownList  txCreditor= (DropDownList)GridView2.Rows[vLoop].FindControl("ddlCreditor");
                TextBox txNarration = (TextBox)GridView2.Rows[vLoop].FindControl("txtNarration");
                TextBox txAmount = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                DropDownList txPaymode = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlpaymentmode");
                DropDownList txbank = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlBankname");
                TextBox txChequeno = (TextBox)GridView2.Rows[vLoop].FindControl("txtChequeno");
                TextBox txAgbillno = (TextBox)GridView2.Rows[vLoop].FindControl("txtAganistBno");


                int col = vLoop + 1;
                txJvno.Text = col.ToString();
                if ((txJvno.Text == "") || (Convert.ToInt32(txJvno.Text) == 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter JV NO in row " + col + " ')", true);
                    txJvno.Focus();
                    return;
                }

                if (txDate.Text == "") 
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date in row " + col + " ')", true);
                    txDate.Focus();
                    return;
                }

                if (txDebetor.SelectedItem.Text == "Select Ledger")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Debetor in row " + col + " ')", true);
                    txDebetor.Focus();
                    return;
                }

                if (txCreditor.SelectedItem.Text == "Select Ledger")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Creditor in row " + col + " ')", true);
                    txCreditor.Focus();
                    return;
                }

                if (txNarration.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Narration in row " + col + " ')", true);
                    txNarration.Focus();
                    return;
                }

                if ((txAmount.Text == "") || (Convert.ToDouble(txAmount.Text) == 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Amount in row " + col + " ')", true);
                    txAmount.Focus();
                    return;
                }
                if (txPaymode.SelectedItem.Text == "Select")
                 {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Payment Mode! " + col + "');", true);
                 txPaymode.Focus();
                        return;
                 }

                if (txPaymode.SelectedItem.Text == "DD" || txPaymode.SelectedItem.Text == "Cheque")
                {
                    if (txbank.SelectedValue == "Select Bank" && txChequeno.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No! " + col + "');", true);
                        txbank.Focus();
                        return;

                    }
                    else if (txbank.SelectedValue == "Select Bank")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name! " + col + " ');", true);
                        txbank.Focus();
                        return;

                    }
                    else if (txChequeno.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Cheque No! " + col + " ');", true);
                        txChequeno.Focus();
                        return;

                    }
                }


                //if (txPaymode.SelectedValue == "0")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Payment Mode in row " + col + " ')", true);
                //    txPaymode.Focus();
                //    return;
                //}

                //if (txbank.SelectedValue == "0")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Bank Name in row " + col + " ')", true);
                //    txbank.Focus();
                //    return;
                //}

                //if (txChequeno.Text == "") 
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Cheque no in row " + col + " ')", true);
                //    txChequeno.Focus();
                //    return;
                //}
               
            }

            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txJvno = (TextBox)GridView2.Rows[vLoop].FindControl("txtJvno");
                    TextBox txDate = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                    DropDownList txDebetor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlDebtor");
                    DropDownList txCreditor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlCreditor");
                    TextBox txNarration = (TextBox)GridView2.Rows[vLoop].FindControl("txtNarration");
                    TextBox txAmount = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                    DropDownList txPaymode = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlpaymentmode");
                    DropDownList txbank = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlBankname");
                    TextBox txChequeno = (TextBox)GridView2.Rows[vLoop].FindControl("txtChequeno");
                    TextBox txAgbillno = (TextBox)GridView2.Rows[vLoop].FindControl("txtAganistBno");


                    itemc = txDebetor.SelectedItem.Text;
                    itemcd = txCreditor.SelectedItem.Text;

                    if ((itemc == null) || (itemc == ""))
                    {
                    }
                    else
                    {
                        if (itemc == itemcd)
                        {

                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already exists in the Debtor.');", true);
                            txCreditor.Focus();
                            return;

                        }
                    

                        //for (int vLoop1 = 0; vLoop1 < GridView2.Rows.Count; vLoop1++)
                        //{
                        //    DropDownList txtDebetor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlDebtor");
                        //    DropDownList txtCreditor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlCreditor");

                        //    if (ii == iq)
                        //    {
                        //    }
                        //    else
                        //    {
                        //        if (itemc == itemcd)
                        //        {

                        //           // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' '" + itemcd + " Already exists in the Debtor.');", true);
                        //            txCreditor.Focus();
                        //            return;

                        //        }
                        //    }
                        //    ii = ii + 1;
                        //}
                    }
                    iq = iq + 1;
                    ii = 1;
                }
          // }
    


            //DateTime date = DateTime.ParseExact(txDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            if (btnadd.Text == "Save")
            {
                int iCustid = 0;
                int Id = 0;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                DataRow dr;

                int iStatus = 0;

                dt.Columns.Add(new DataColumn("JVNo", typeof(string)));
                dt.Columns.Add(new DataColumn("date", typeof(string)));
                dt.Columns.Add(new DataColumn("DebtorId", typeof(int)));
                dt.Columns.Add(new DataColumn("CreditorId", typeof(int)));
                dt.Columns.Add(new DataColumn("Narration", typeof(string)));
                dt.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt.Columns.Add(new DataColumn("PayMode", typeof(string)));
                dt.Columns.Add(new DataColumn("BankName", typeof(int)));
                dt.Columns.Add(new DataColumn("ChequeNO", typeof(string)));
                dt.Columns.Add(new DataColumn("AganistBillNo", typeof(string)));
                dt.Columns.Add(new DataColumn("DebtorName", typeof(string)));
                dt.Columns.Add(new DataColumn("CreditorName", typeof(string)));


                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txJvno = (TextBox)GridView2.Rows[vLoop].FindControl("txtJvno");
                    //TextBox txDate = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");

                    TextBox txDate = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                    recdate = DateTime.ParseExact(txDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    
                    DropDownList txDebetor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlDebtor");
                    DropDownList txCreditor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlCreditor");
                    TextBox txNarration = (TextBox)GridView2.Rows[vLoop].FindControl("txtNarration");
                    TextBox txAmount = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                    DropDownList txPaymode = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlpaymentmode");
                    DropDownList txbank = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlBankname");
                    TextBox txChequeno = (TextBox)GridView2.Rows[vLoop].FindControl("txtChequeno");
                    TextBox txAgbillno = (TextBox)GridView2.Rows[vLoop].FindControl("txtAganistBno");


                    dr = dt.NewRow();
                    dr["JVNo"] = Convert.ToInt32(txJvno.Text);
                    dr["date"] = recdate;
                    dr["DebtorId"] = Convert.ToInt32(txDebetor.SelectedValue);
                    dr["CreditorId"] = Convert.ToInt32(txCreditor.SelectedValue);
                    dr["Narration"] = txNarration.Text;
                    dr["Amount"] = txAmount.Text;
                    dr["PayMode"] = txPaymode.SelectedValue;
                    dr["DebtorName"] = txDebetor.SelectedItem.Text;
                    dr["CreditorName"] = txCreditor.SelectedItem.Text;

                    if (txPaymode.SelectedValue == "Cash")
                    {
                        dr["BankName"] = "0";
                        dr["ChequeNO"] = txChequeno.Text;
                    }
                    else if (txPaymode.SelectedValue == "Online")
                    {
                        dr["BankName"] = txbank.SelectedValue;
                        dr["ChequeNO"] = txChequeno.Text;
                    }
                    else if (txPaymode.SelectedValue == "ATM")
                    {
                        dr["BankName"] = txbank.SelectedValue;
                        dr["ChequeNO"] = txChequeno.Text;
                    }
                    else
                    {
                        dr["BankName"] = txbank.SelectedValue;
                        dr["ChequeNO"] = txChequeno.Text;
                    }

                    dr["AganistBillNo"] = txAgbillno.Text;

                    dt.Rows.Add(dr);

                }

                ds.Merge(dt);

            //    DateTime EntryDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            //    DateTime EditDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                int iRtn = objbs.InsertJournal(ds, "tblJournal_" + sTableName, "tblDaybook_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, EmpId);
             Response.Redirect("viewjournals.aspx");
            }
            else
            {

                int iCustid = 0;
                int Id = 0;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                DataRow dr;

                int iStatus = 0;
               // dt.Columns.Add(new DataColumn("JVID", typeof(string)));
                dt.Columns.Add(new DataColumn("JVNo", typeof(string)));
                dt.Columns.Add(new DataColumn("date", typeof(string)));
                dt.Columns.Add(new DataColumn("DebtorId", typeof(int)));
                dt.Columns.Add(new DataColumn("CreditorId", typeof(int)));
                dt.Columns.Add(new DataColumn("Narration", typeof(string)));
                dt.Columns.Add(new DataColumn("Amount", typeof(string)));

                dt.Columns.Add(new DataColumn("PayMode", typeof(string)));
                dt.Columns.Add(new DataColumn("BankName", typeof(int)));
                dt.Columns.Add(new DataColumn("ChequeNO", typeof(string)));
                dt.Columns.Add(new DataColumn("AganistBillNo", typeof(string)));
                dt.Columns.Add(new DataColumn("DebtorName", typeof(string)));
                dt.Columns.Add(new DataColumn("CreditorName", typeof(string)));


                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txJvno = (TextBox)GridView2.Rows[vLoop].FindControl("txtJvno");
                    TextBox txDate = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                    recdate = DateTime.ParseExact(txDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DropDownList txDebetor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlDebtor");
                    DropDownList txCreditor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlCreditor");
                    TextBox txNarration = (TextBox)GridView2.Rows[vLoop].FindControl("txtNarration");
                    TextBox txAmount = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                    DropDownList txPaymode = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlpaymentmode");
                    DropDownList txbank = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlBankname");
                    TextBox txChequeno = (TextBox)GridView2.Rows[vLoop].FindControl("txtChequeno");
                    TextBox txAgbillno = (TextBox)GridView2.Rows[vLoop].FindControl("txtAganistBno");


                    dr = dt.NewRow();
                    dr["JVNo"] = Convert.ToInt32(txJvno.Text);
                    dr["date"] = recdate;
                    dr["DebtorId"] = Convert.ToInt32(txDebetor.SelectedValue);
                    dr["CreditorId"] = Convert.ToInt32(txCreditor.SelectedValue);
                    dr["Narration"] = txNarration.Text;
                    dr["Amount"] = txAmount.Text;
                    dr["PayMode"] = txPaymode.SelectedValue;
                    dr["DebtorName"] = txbank.SelectedItem.Text;
                    dr["CreditorName"] = txCreditor.SelectedItem.Text;

                    if (txPaymode.SelectedValue == "Cash" || txPaymode.SelectedValue == "Credit")
                    {
                        dr["BankName"] = "0";
                        dr["ChequeNO"] = txChequeno.Text;
                    }
                    else
                    {
                        dr["BankName"] = txbank.SelectedValue;
                        dr["ChequeNO"] = txChequeno.Text;
                    }

                    dr["AganistBillNo"] = txAgbillno.Text;

                    dt.Rows.Add(dr);

                }

                ds.Merge(dt);
                string ijourID = Request.QueryString.Get("TransNo");

               // DateTime EditDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                string pay = ds.Tables[0].Rows[0]["PayMode"].ToString();
                if (pay == "Cash" || pay == "Credit")
                {
                    int iRtn = objbs.updatejournalnewbank(ds, "tblJournal_" + sTableName, "tblDaybook_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, ijourID, EmpId);
                    Response.Redirect("viewjournals.aspx");
                }
                else
                {

                    int iRtn = objbs.updatejournalnew(ds, "tblJournal_" + sTableName, "tblDaybook_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, ijourID, EmpId);
                    Response.Redirect("viewjournals.aspx");
                }
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewjournals.aspx");
        }



        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {

            int No = 0;
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
             
                TextBox txJvno = (TextBox)GridView2.Rows[vLoop].FindControl("txtJvno");
                TextBox txDate = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                DropDownList txDebetor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlDebtor");
                DropDownList txCreditor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlCreditor");
                TextBox txNarration = (TextBox)GridView2.Rows[vLoop].FindControl("txtNarration");
                TextBox txAmount = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                DropDownList txPaymode = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlpaymentmode");
                DropDownList txbank = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlBankname");
                TextBox txChequeno = (TextBox)GridView2.Rows[vLoop].FindControl("txtChequeno");
                TextBox txAgbillno = (TextBox)GridView2.Rows[vLoop].FindControl("txtAganistBno");


                string Agbillnono = txAgbillno.Text;
                string Chequeno = txChequeno.Text;
                string amount = txAmount.Text;

                if (Chequeno != "" || Agbillnono != "" || amount != "")
                {
                    
                }
                else
                {
                    txChequeno.Text = "0";
                    txAgbillno.Text = "0";
                    txAmount.Text = "0";
                }

                if (txDebetor.SelectedItem.Text == "Select Ledger")
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

                AddNewRow1();
            }
            else
            {

            }

              //  AddNewRow1();
               
            
        }

        private void AddNewRow1()
        {
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                TextBox txJvno = (TextBox)GridView2.Rows[vLoop].FindControl("txtJvno");
                TextBox txDate = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                DropDownList txDebetor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlDebtor");
                DropDownList txCreditor = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlCreditor");
                TextBox txNarration = (TextBox)GridView2.Rows[vLoop].FindControl("txtNarration");
                TextBox txAmount = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                DropDownList txPaymode = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlpaymentmode");
                DropDownList txbank = (DropDownList)GridView2.Rows[vLoop].FindControl("ddlBankname");
                TextBox txChequeno = (TextBox)GridView2.Rows[vLoop].FindControl("txtChequeno");
                TextBox txAgbillno = (TextBox)GridView2.Rows[vLoop].FindControl("txtAganistBno");

                 int col = vLoop + 1;
                 txJvno.Text = "1";
                 //if ((txJvno.Text == "") || (Convert.ToInt32(txJvno.Text) == 0))
                 //{
                 //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter JV NO in row " + col + " ')", true);
                 //    txJvno.Focus();
                 //    return;
                 //}

                 //if (txDate.Text == "")
                 //{
                 //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date in row " + col + " ')", true);
                 //    txDate.Focus();
                 //    return;
                 //}

                 //if (txDebetor.SelectedItem.Text == "Select Ledger")
                 //{
                 //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Debetor in row " + col + " ')", true);
                 //    txDebetor.Focus();
                 //    return;
                 //}

                 //if (txCreditor.SelectedItem.Text == "Select Ledger")
                 //{
                 //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Creditor in row " + col + " ')", true);
                 //    txCreditor.Focus();
                 //    return;
                 //}
                 //if (txDebetor.SelectedItem.Text == txCreditor.SelectedItem.Text)
                 //{
                 //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('both debitor and creditor should not be same.Please change it" + col + " ')", true);
                 //    txCreditor.Focus();
                 //    return;
                 //}


                 //if (txNarration.Text == "")
                 //{
                 //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Narration in row " + col + " ')", true);
                 //    txNarration.Focus();
                 //    return;
                 //}

                 //if ((txAmount.Text == "") || (Convert.ToInt32(txAmount.Text) == 0))
                 //{
                 //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Amount in row " + col + " ')", true);
                 //    txAmount.Focus();
                 //    return;
                 //}
                 //if (txPaymode.SelectedItem.Text == "Select")
                 //{
                 //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Payment Mode! " + col + "');", true);
                 //    txPaymode.Focus();
                 //    return;
                 //}

                 //if (txPaymode.SelectedItem.Text == "DD" || txPaymode.SelectedItem.Text == "Cheque")
                 //{
                 //    if (txbank.SelectedValue == "Select Bank" && txChequeno.Text == "0")
                 //    {
                 //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No! " + col + "');", true);
                 //        txbank.Focus();
                 //        return;

                 //    }
                 //    else if (txbank.SelectedValue == "Select Bank")
                 //    {

                 //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name! " + col + " ');", true);
                 //        txbank.Focus();
                 //        return;

                 //    }
                 //    else if (txChequeno.Text == "0")
                 //    {
                 //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Cheque No! " + col + " ');", true);
                 //        txChequeno.Focus();
                 //        return;

                 //    }
                 //}
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
                        TextBox txJvno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtJvno");
                        TextBox txDate = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtDate");
                        DropDownList txDebetor = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlDebtor");
                        DropDownList txCreditor = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlCreditor");
                        TextBox txNarration = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtNarration");
                        TextBox txAmount = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                        DropDownList txPaymode = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlpaymentmode");
                        DropDownList txbank = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlBankname");
                        TextBox txChequeno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtChequeno");
                        TextBox txAgbillno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtAganistBno");
                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["JVNo"] = Convert.ToInt32(txJvno.Text);
                        dtCurrentTable.Rows[i - 1]["date"] = txDate.Text;
                        dtCurrentTable.Rows[i - 1]["DebtorId"] = txDebetor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["CreditorId"] = txCreditor.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Narration"] = txNarration.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = txAmount.Text;

                        dtCurrentTable.Rows[i - 1]["PayMode"] = txPaymode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["BankName"] = txbank.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ChequeNO"] = txChequeno.Text;
                        dtCurrentTable.Rows[i - 1]["AganistBillNo"] = txAgbillno.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    GridView2.DataSource = dtCurrentTable;
                    GridView2.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData1();
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    SetPreviousData1();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();

                    SetPreviousData1();
                   
                    FirstGridViewRow1();
                }
            }
        }

        private void SetRowData1()
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

                        TextBox txJvno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtJvno");
                        TextBox txDate = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtDate");
                        DropDownList txDebetor = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlDebtor");
                        DropDownList txCreditor = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlCreditor");
                        TextBox txNarration = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtNarration");
                        TextBox txAmount = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                        DropDownList txPaymode = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlpaymentmode");
                        DropDownList txbank = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlBankname");
                        TextBox txChequeno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtChequeno");
                        TextBox txAgbillno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtAganistBno");
                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["JVNo"] = Convert.ToInt32(txJvno.Text);
                        dtCurrentTable.Rows[i - 1]["date"] = txDate.Text;
                        dtCurrentTable.Rows[i - 1]["DebtorId"] = txDebetor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["CreditorId"] = txCreditor.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Narration"] = txNarration.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = txAmount.Text;

                        dtCurrentTable.Rows[i - 1]["PayMode"] = txPaymode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["BankName"] = txbank.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ChequeNO"] = txChequeno.Text;
                        dtCurrentTable.Rows[i - 1]["AganistBillNo"] = txAgbillno.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    GridView2.DataSource = dtCurrentTable;
                    GridView2.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
        }

        private void FirstGridViewRow1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("JVNo", typeof(string)));
            dt.Columns.Add(new DataColumn("date", typeof(string)));
            dt.Columns.Add(new DataColumn("DebtorId", typeof(string)));
            dt.Columns.Add(new DataColumn("CreditorId", typeof(string)));
            dt.Columns.Add(new DataColumn("Narration", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("PayMode", typeof(string)));
            dt.Columns.Add(new DataColumn("BankName", typeof(string)));
            dt.Columns.Add(new DataColumn("ChequeNO", typeof(string)));
            dt.Columns.Add(new DataColumn("AganistBillNo", typeof(string)));
        

            dr = dt.NewRow();
            dr["JVNo"] = string.Empty;
            dr["date"] = string.Empty;
            dr["DebtorId"] = string.Empty;
            dr["CreditorId"] = string.Empty;
            dr["Narration"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["PayMode"] = string.Empty;
            dr["BankName"] = string.Empty;
            dr["ChequeNO"] = string.Empty;
            dr["AganistBillNo"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dt;

            GridView2.DataSource = dt;
            GridView2.DataBind();

            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("JVNo");
            dtt.Columns.Add(dct);

            dct = new DataColumn("date");
            dtt.Columns.Add(dct);

            dct = new DataColumn("DebtorId");
            dtt.Columns.Add(dct);

            dct = new DataColumn("CreditorId");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Narration");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Amount");
            dtt.Columns.Add(dct);

            dct = new DataColumn("PayMode");
            dtt.Columns.Add(dct);

            dct = new DataColumn("BankName");
            dtt.Columns.Add(dct);

            dct = new DataColumn("ChequeNO");
            dtt.Columns.Add(dct);

            dct = new DataColumn("AganistBillNo");
            dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);

            drNew = dtt.NewRow();
            drNew["JVNo"] = 0;
            drNew["date"] = "";
            drNew["DebtorId"] = "";
            drNew["CreditorId"] = "";
            drNew["Narration"] = 0;
            drNew["Amount"] = 0;
            drNew["PayMode"] = "";
            drNew["BankName"] = "";
            drNew["ChequeNO"] = 0;
            drNew["AganistBillNo"] = 0;
            dstd.Tables[0].Rows.Add(drNew);

            GridView2.DataSource = dstd;
            GridView2.DataBind();
        }


        private void SetPreviousData1()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txJvno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtJvno");
                        TextBox txDate = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtDate");
                        DropDownList txDebetor = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlDebtor");
                        DropDownList txCreditor = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlCreditor");
                        TextBox txNarration = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtNarration");
                        TextBox txAmount = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                        DropDownList txPaymode = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlpaymentmode");
                        DropDownList txbank = (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ddlBankname");
                        TextBox txChequeno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtChequeno");
                        TextBox txAgbillno = (TextBox)GridView2.Rows[rowIndex].Cells[1].FindControl("txtAganistBno");




                        txJvno.Text = dt.Rows[i]["JVNo"].ToString();
                        txDate.Text = dt.Rows[i]["date"].ToString();
                        txDebetor.SelectedValue = dt.Rows[i]["DebtorId"].ToString();
                        txCreditor.SelectedValue = dt.Rows[i]["CreditorId"].ToString();
                        txNarration.Text = dt.Rows[i]["Narration"].ToString();
                        txAmount.Text = dt.Rows[i]["Amount"].ToString();
                        txPaymode.SelectedValue = dt.Rows[i]["PayMode"].ToString();
                        txbank.SelectedValue = dt.Rows[i]["BankName"].ToString();
                        txChequeno.Text = dt.Rows[i]["ChequeNO"].ToString();
                        txAgbillno.Text = dt.Rows[i]["AganistBillNo"].ToString();
               

                        rowIndex++;

                    }
                }
            }
        }

        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txPaymode = (DropDownList)GridView2.Rows[vLoop].Cells[1].FindControl("ddlpaymentmode");
                DropDownList txbank = (DropDownList)GridView2.Rows[vLoop].Cells[1].FindControl("ddlBankname");
                TextBox txChequeno = (TextBox)GridView2.Rows[vLoop].Cells[1].FindControl("txtChequeno");

                if (txPaymode.SelectedValue == "Cash")
                {
                    txbank.ClearSelection();
                    txChequeno.Text = "";
                    txbank.Enabled = false;
                    txChequeno.Enabled = false;
                }
                else if (txPaymode.SelectedValue == "Online")
                {
                    txbank.Enabled = true;
                    txChequeno.Enabled = true;
                }
                else if (txPaymode.SelectedValue == "Atm")
                {
                    txbank.Enabled = true;
                    txChequeno.Enabled = true;
                }
                else 
                {
                    txbank.Enabled = true;
                    txChequeno.Enabled = true;
                }

            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
   
            DataSet dsBankName = objbs.SelectBankName("4",sTableName);
            DataSet ds = objbs.LedgerGrid(sTableName);
         //   GridView gridView = new GridView();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtJvno = (TextBox)e.Row.FindControl("txtJvno");
                txtJvno.Text = "1";
                TextBox amount= (TextBox)e.Row.FindControl("txtAmount");
                amount.Text = "0";
                TextBox Chequeno = (TextBox)e.Row.FindControl("txtChequeno");
                Chequeno.Text = "0";
                Chequeno.Enabled = false;
                TextBox AganistBno = (TextBox)e.Row.FindControl("txtAganistBno");
                AganistBno.Text = "0";
                TextBox date = (TextBox)e.Row.FindControl("txtDate");
                date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                DropDownList bank = (DropDownList)e.Row.FindControl("ddlBankname");
                bank.Enabled = false;

                //DataSet dsed = objbs.CheckEditDelete(EmpId);
                //if (dsed.Tables[0].Rows[0]["allowdate"].ToString() == "1")
                //{
                //    date.Enabled = true;
                //}
                //else
                //{
                //    date.Enabled = false;
                //}

                if (btnadd.Text == "Update")
                {
                    this.GridView2.Columns[11].Visible = false;
                    this.GridView2.Columns[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                       //LinkButton cmdField = (LinkButton)e.Row.Cells[11].FindControl("Button");
                       // cmdField.Visible = false;
                        Button btnEdit = (Button)e.Row.FindControl("ButtonAdd1");
                        btnEdit.Visible = false;
                    
                }



                var ddl = (DropDownList)e.Row.FindControl("ddlDebtor");
                ddl.DataSource = ds;
                ddl.DataTextField = "ledgername";
                ddl.DataValueField = "LedgerID";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Ledger");

                var ddlt = (DropDownList)e.Row.FindControl("ddlCreditor");
                ddlt.DataSource = ds;
                ddlt.DataTextField = "ledgername";
                ddlt.DataValueField = "LedgerID";
                ddlt.DataBind();
                ddlt.Items.Insert(0, "Select Ledger");

                var ddlb = (DropDownList)e.Row.FindControl("ddlBankname");
                ddlb.DataSource = dsBankName;
                ddlb.DataTextField = "LedgerName";
                ddlb.DataValueField = "LedgerID";
                ddlb.DataBind();
                ddlb.Items.Insert(0, "Select Bank");

            }
        }
    }
}