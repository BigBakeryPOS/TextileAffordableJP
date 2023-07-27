using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusinessLayer;
using CommonLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class Payment : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rdbprocesstype_OnSelectedIndexChanged(sender, e);

                //DataSet drppEmp = objBs.getLedger1();
                //if (drppEmp.Tables[0].Rows.Count > 0)
                //{
                //    ddlEName.DataSource = drppEmp;
                //    ddlEName.DataTextField = "LedgerName";
                //    ddlEName.DataValueField = "LedgerId";
                //    ddlEName.DataBind();
                //    ddlEName.Items.Insert(0, "Select Name");
                //}

                string super = Session["IsSuperAdmin"].ToString();


                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "ALL");
                    }
                }
                else
                {

                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }


                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                DateTime indianStd2 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa2 = Convert.ToDateTime(indianStd2).ToString("dd/MM/yyyy");

                DateTime indianpdate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string pdate = Convert.ToDateTime(indianpdate).ToString("dd/MM/yyyy");

                txtironrecdate.Text = dtaa;
                txtironrecdate2.Text = dtaa2;
                txtpdate.Text = pdate;


                DataSet ds12 = objBs.getjppayments();
                if (ds12.Tables[0].Rows.Count > 0)
                {

                    txtPaymentNo.Text = ds12.Tables[0].Rows[0]["PaymentNo"].ToString();
                    save.Text = "Save";

                }

                DataSet drpProcess = objBs.SelectAllProcessTypeLotProcess();
                if (drpProcess.Tables[0].Rows.Count > 0)
                {
                    DpProcess.DataSource = drpProcess;
                    DpProcess.DataTextField = "ProcessType";
                    DpProcess.DataValueField = "ProcessMasterID";
                    DpProcess.DataBind();
                    DpProcess.Items.Insert(0, "Select Process Name");
                }

                //string ID = Request.QueryString.Get("ID");
                //string name = Request.QueryString.Get("name");
                //if (ID != "" || ID != null)
                //{
                //    DataSet dspay = objBs.getQuery(ID, name);
                //    if (dspay.Tables[0].Rows.Count > 0)
                //    {
                //        ddlEName.SelectedValue = dspay.Tables[0].Rows[0]["workerId"].ToString();
                //        DpProcess.SelectedValue = dspay.Tables[0].Rows[0]["processid"].ToString();
                //        txtLotDetailId.Text = dspay.Tables[0].Rows[0]["LotDetailId"].ToString();
                //        txtLotNo.Text = dspay.Tables[0].Rows[0]["LotNo"].ToString();
                //        txtTotalAmount.Text = dspay.Tables[0].Rows[0]["TotalAmount"].ToString();
                //        txtPaidAmount.Text = dspay.Tables[0].Rows[0]["PaidAmount"].ToString();
                //    }

                //}


                //string DayBookID = Request.QueryString.Get("TransNo");
                //if (DayBookID != "" || DayBookID != null)
                //{

                //    DataSet dspayment = objBs.getPaymentmaster("tblPayment", DayBookID);
                //    if (dspayment.Tables[0].Rows.Count > 0)
                //    {
                //        save.Text = "Update";

                //        TextBox1.Text = DayBookID;
                //        txtPaymentNo.Text = dspayment.Tables[0].Rows[0]["PaymentNo"].ToString();
                //        txtpdate.Text = Convert.ToDateTime(dspayment.Tables[0].Rows[0]["PaymentDate"]).ToString("dd/MM/yyyy");
                //        ddlPayment.SelectedValue = dspayment.Tables[0].Rows[0]["PayModeID"].ToString();
                //        txtAmount.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["Amount"]).ToString("0.00");
                //        txtNarration.Text = dspayment.Tables[0].Rows[0]["Narration"].ToString();
                //        ddlEName.SelectedValue = dspayment.Tables[0].Rows[0]["ledgerid"].ToString();
                //        //txtFromDate.Text = Convert.ToDateTime(dspayment.Tables[0].Rows[0]["from_Date"]).ToString("dd/MM/yyyy");
                //        //txtToDate.Text = Convert.ToDateTime(dspayment.Tables[0].Rows[0]["to_Date"]).ToString("dd/MM/yyyy");
                //        DpProcess.SelectedValue = dspayment.Tables[0].Rows[0]["processid"].ToString();
                //        txtbank.Text = dspayment.Tables[0].Rows[0]["BankName"].ToString();
                //        txtcheque.Text = dspayment.Tables[0].Rows[0]["chequeno"].ToString();
                //        txtLotDetailId.Text = dspayment.Tables[0].Rows[0]["LotDetailId"].ToString();
                //    }
                //}

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void Process_CIclk(object sender, EventArgs e)
        {
            //if (txtFromDate.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select From Date!!!.Thanks You!!!')", true);
            //    return;
            //}
            //if (txtToDate.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select To Date!!!.Thanks You!!!')", true);
            //    return;
            //}

            if (ddlEName.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name!!!.Thanks You!!!')", true);
                return;
            }

            //DataSet dckhh = objBs.getledgernameforpayment(ddlEName.SelectedValue);
            //if (dckhh.Tables[0].Rows.Count > 0)
            //{
            //    string ledgerid = dckhh.Tables[0].Rows[0]["EmployeeId"].ToString();
            //    DataSet ds = objBs.getdatewisedetailedreportforemployeewise(txtFromDate.Text, txtToDate.Text, ledgerid);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        double tot = 0;

            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            double ratee = Convert.ToDouble(ds.Tables[0].Rows[i]["Ratee"]);
            //            tot = tot + ratee;
            //        }
            //        txtAmount.Text = tot.ToString();
            //    }
            //}
        }


        protected void Calc_Click(object sender, EventArgs e)
        {
            #region Calculations
            double sampleAmt = 0;

            int TtlQty = 0;
            double TotalAmount = 0;
            double DebitAmt = 0;
            double miscellaneous = 0;
            double Advance = 0;

            for (int i = 0; i < gvpayment.Rows.Count; i++)
            {
                #region Checking


                Label lblRate = (Label)gvpayment.Rows[i].FindControl("lblRate");
                TextBox txtPaymentQty = (TextBox)gvpayment.Rows[i].FindControl("txtPaymentQty");
                if (txtPaymentQty.Text == "")
                    txtPaymentQty.Text = "0";

                CheckBox chkAllowit = (CheckBox)gvpayment.Rows[i].FindControl("chkAllowit");

                CheckBox chkitemchecked = (CheckBox)gvpayment.Rows[i].FindControl("chkitemchecked");

                Label lblCompanyLotNo = (Label)gvpayment.Rows[i].FindControl("lblCompanyLotNo");
                Label lblAlreadyPaid = (Label)gvpayment.Rows[i].FindControl("lblAlreadyPaid");

                Label lblTotalrecamt = (Label)gvpayment.Rows[i].FindControl("lblTotalrecamt");

                Label lblAdvancePaid = (Label)gvpayment.Rows[i].FindControl("lblAdvancePaid");

                TextBox lblpayamount = (TextBox)gvpayment.Rows[i].FindControl("lblpayamount");
                if (lblpayamount.Text == "")
                    lblpayamount.Text = "0";

                TextBox lblpayamountNew = (TextBox)gvpayment.Rows[i].FindControl("lblpayamountNew");
                if (lblpayamountNew.Text == "")
                    lblpayamountNew.Text = "0";

                TextBox txtDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtDebitAmt");
                if (txtDebitAmt.Text == "")
                    txtDebitAmt.Text = "0";

                TextBox txtAdvance = (TextBox)gvpayment.Rows[i].FindControl("txtAdvance");
                if (txtAdvance.Text == "")
                    txtAdvance.Text = "0";

                Label lblTotalIssue = (Label)gvpayment.Rows[i].FindControl("lblTotalIssue");

                Label lblTotalDamage = (Label)gvpayment.Rows[i].FindControl("lblTotalDamage");

                TextBox txtTotalPaidAmt = (TextBox)gvpayment.Rows[i].FindControl("txtTotalPaidAmt");
                if (txtTotalPaidAmt.Text == "")
                    txtTotalPaidAmt.Text = "0";

                Label lblBalAmount = (Label)gvpayment.Rows[i].FindControl("lblBalAmount");



                lblpayamountNew.Text = ((Convert.ToDouble(txtPaymentQty.Text) * Convert.ToDouble(lblRate.Text)) - Convert.ToDouble(txtDebitAmt.Text)).ToString("f2");

                //////if (Convert.ToDouble(lblBalAmount.Text) < Convert.ToDouble(lblpayamount.Text))
                //////{
                //////    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Payable Amount in LotNo " + lblCompanyLotNo.Text + "');", true);
                //////    save.Enabled = false;
                //////    return;
                //////}

                //if (lblAlreadyPaid.Text == "0")
                //{

                //    lblpayamount.Text = ((Convert.ToDouble(txtPaymentQty.Text) * Convert.ToDouble(lblRate.Text))).ToString("f2");
                //}
                //else
                //{
                //    double totamnt = Convert.ToDouble(lblTotalIssue.Text) * Convert.ToDouble(lblRate.Text) ;
                //    double dmgamnt = Convert.ToDouble(lblTotalDamage.Text) * Convert.ToDouble(lblRate.Text);

                //    double lotvalue = totamnt - dmgamnt;





                //}



                TextBox txtpreDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtpreDebitAmt");
                if (txtpreDebitAmt.Text == "")
                    txtpreDebitAmt.Text = "0";

                txtAdvance.Text = "0";
                lblpayamount.Text = "0";

                if (Convert.ToDouble(lblTotalrecamt.Text) < (Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblAlreadyPaid.Text) + Convert.ToDouble(lblpayamountNew.Text) + Convert.ToDouble(txtAdvance.Text) + Convert.ToDouble(lblAdvancePaid.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Payable Amount in LotNo " + lblCompanyLotNo.Text + "');", true);
                    save.Enabled = false;
                    return;
                }


                double ttl = Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblpayamountNew.Text);

                double ss = (Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblAlreadyPaid.Text) + Convert.ToDouble(lblpayamountNew.Text) + Convert.ToDouble(txtAdvance.Text) + Convert.ToDouble(lblAdvancePaid.Text));

                if (Convert.ToDouble(lblTotalrecamt.Text) < Convert.ToDouble(ss.ToString("f2")))
                {

                    chkAllowit.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('PayableAmount is Mismatched in LotNo " + lblCompanyLotNo.Text + "');", true);
                    //save.Enabled = false;
                    //return;
                }
                else if (ttl != 0 && Convert.ToDouble(txtAdvance.Text) != 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Either can be paid in LotNo " + lblCompanyLotNo.Text + "');", true);
                    save.Enabled = false;
                    return;
                }
                else if (Convert.ToDouble(lblTotalrecamt.Text) <= (Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblAlreadyPaid.Text) + Convert.ToDouble(lblpayamountNew.Text) + Convert.ToDouble(txtAdvance.Text) + Convert.ToDouble(lblAdvancePaid.Text)))
                {
                    CheckBox item = (CheckBox)gvpayment.Rows[i].FindControl("chkitemchecked");
                    if (item.Checked == false)
                    {
                        //////ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Plz Close the LotNo " + lblCompanyLotNo.Text + "');", true);
                        //////save.Enabled = false;
                        //////return;
                        item.Checked = true;
                    }
                    else
                    {
                        save.Enabled = true;
                        item.Checked = false;
                    }

                }
                else
                {
                    save.Enabled = true;
                    chkitemchecked.Checked = false;
                }



                #endregion
            }

            for (int i = 0; i < gvpayment.Rows.Count; i++)
            {
                #region Calculate

                Label lblCompanyLotNo = (Label)gvpayment.Rows[i].FindControl("lblCompanyLotNo");
                Label lblStichingId = (Label)gvpayment.Rows[i].FindControl("lblStichingId");

                TextBox lblpayamount = (TextBox)gvpayment.Rows[i].FindControl("lblpayamount");
                if (lblpayamount.Text == "")
                    lblpayamount.Text = "0";

                TextBox lblpayamountNew = (TextBox)gvpayment.Rows[i].FindControl("lblpayamountNew");
                if (lblpayamountNew.Text == "")
                    lblpayamountNew.Text = "0";

                TextBox txtPaymentQty = (TextBox)gvpayment.Rows[i].FindControl("txtPaymentQty");
                if (txtPaymentQty.Text == "")
                    txtPaymentQty.Text = "0";

                TextBox txtDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtDebitAmt");
                if (txtDebitAmt.Text == "")
                    txtDebitAmt.Text = "0";

                TextBox txtmiscellaneous = (TextBox)gvpayment.Rows[i].FindControl("txtmiscellaneous");
                if (txtmiscellaneous.Text == "")
                    txtmiscellaneous.Text = "0";

                TextBox txtAdvance = (TextBox)gvpayment.Rows[i].FindControl("txtAdvance");
                if (txtAdvance.Text == "")
                    txtAdvance.Text = "0";

                DropDownList drppaymenttype = (DropDownList)gvpayment.Rows[i].FindControl("drppaymenttype");


                DebitAmt = DebitAmt + Convert.ToDouble(txtDebitAmt.Text);
                TtlQty = TtlQty + Convert.ToInt32(txtPaymentQty.Text);
                lblpayamount.Text = "0";
                txtAdvance.Text = "0";

                if (drppaymenttype.SelectedValue == "Balance")
                {
                    lblpayamount.Text = Convert.ToDouble(lblpayamountNew.Text).ToString("0.00");
                    TotalAmount = TotalAmount + Convert.ToDouble(lblpayamountNew.Text);
                }
                else
                {
                    ////// Advance = Advance + Convert.ToDouble(txtAdvance.Text);
                    Advance = Advance + Convert.ToDouble(lblpayamountNew.Text);
                    txtAdvance.Text = Convert.ToDouble(lblpayamountNew.Text).ToString("0.00");
                }

                miscellaneous = miscellaneous + Convert.ToDouble(txtmiscellaneous.Text);

                #endregion
            }


            lbllTotalQty.Text = TtlQty.ToString();

            lblbillAmt.Text = TotalAmount.ToString("F2");
            lbladv.Text = Advance.ToString("F2");

            lblMisc.Text = miscellaneous.ToString("F2");

            double lessamt = Convert.ToDouble(txttrimsdebitamt.Text) + DebitAmt;
            lblDebitAmt.Text = DebitAmt.ToString("F2");

            double valamt = TotalAmount - lessamt;
            lbllTotalAmount.Text = (valamt + sampleAmt + Advance + miscellaneous).ToString("F2");

            txtTotalAmount.Text = (valamt + sampleAmt + Advance + miscellaneous).ToString("F2");


            if (Convert.ToDouble(txtTotalAmount.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Check TotalAmount.')", true);
                ddlEName.Focus();
                return;
            }
            #endregion
        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Paymentgrit.aspx");
        }
        protected void Add_Click(object sender, EventArgs e)
        {

            //#region Calculations
            //double sampleAmt = 0;

            //int TtlQty = 0;
            //double TotalAmount = 0;
            //double DebitAmt = 0;
            //double miscellaneous = 0;
            //double Advance = 0;

            //for (int i = 0; i < gvpayment.Rows.Count; i++)
            //{
            //    #region Checking


            //    Label lblRate = (Label)gvpayment.Rows[i].FindControl("lblRate");
            //    TextBox txtPaymentQty = (TextBox)gvpayment.Rows[i].FindControl("txtPaymentQty");
            //    if (txtPaymentQty.Text == "")
            //        txtPaymentQty.Text = "0";

            //    CheckBox chkAllowit = (CheckBox)gvpayment.Rows[i].FindControl("chkAllowit");
            //    Label lblCompanyLotNo = (Label)gvpayment.Rows[i].FindControl("lblCompanyLotNo");
            //    Label lblAlreadyPaid = (Label)gvpayment.Rows[i].FindControl("lblAlreadyPaid");

            //    Label lblTotalrecamt = (Label)gvpayment.Rows[i].FindControl("lblTotalrecamt");

            //    Label lblAdvancePaid = (Label)gvpayment.Rows[i].FindControl("lblAdvancePaid");

            //    TextBox lblpayamount = (TextBox)gvpayment.Rows[i].FindControl("lblpayamount");
            //    if (lblpayamount.Text == "")
            //        lblpayamount.Text = "0";

            //    TextBox txtDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtDebitAmt");
            //    if (txtDebitAmt.Text == "")
            //        txtDebitAmt.Text = "0";

            //    TextBox txtAdvance = (TextBox)gvpayment.Rows[i].FindControl("txtAdvance");
            //    if (txtAdvance.Text == "")
            //        txtAdvance.Text = "0";

            //    Label lblTotalIssue = (Label)gvpayment.Rows[i].FindControl("lblTotalIssue");

            //    Label lblTotalDamage = (Label)gvpayment.Rows[i].FindControl("lblTotalDamage");

            //    TextBox txtTotalPaidAmt = (TextBox)gvpayment.Rows[i].FindControl("txtTotalPaidAmt");
            //    if (txtTotalPaidAmt.Text == "")
            //        txtTotalPaidAmt.Text = "0";

            //    Label lblBalAmount = (Label)gvpayment.Rows[i].FindControl("lblBalAmount");



            //    lblpayamount.Text = ((Convert.ToDouble(txtPaymentQty.Text) * Convert.ToDouble(lblRate.Text)) - Convert.ToDouble(txtDebitAmt.Text)).ToString("f2");

            //    if (Convert.ToDouble(lblBalAmount.Text) < Convert.ToDouble(lblpayamount.Text))
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Payable Amount in LotNo " + lblCompanyLotNo.Text + "');", true);
            //        save.Enabled = false;
            //        return;
            //    }

            //    //if (lblAlreadyPaid.Text == "0")
            //    //{

            //    //    lblpayamount.Text = ((Convert.ToDouble(txtPaymentQty.Text) * Convert.ToDouble(lblRate.Text))).ToString("f2");
            //    //}
            //    //else
            //    //{
            //    //    double totamnt = Convert.ToDouble(lblTotalIssue.Text) * Convert.ToDouble(lblRate.Text) ;
            //    //    double dmgamnt = Convert.ToDouble(lblTotalDamage.Text) * Convert.ToDouble(lblRate.Text);

            //    //    double lotvalue = totamnt - dmgamnt;





            //    //}



            //    TextBox txtpreDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtpreDebitAmt");
            //    if (txtpreDebitAmt.Text == "")
            //        txtpreDebitAmt.Text = "0";




            //    double ttl = Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblpayamount.Text);

            //    double ss = (Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblAlreadyPaid.Text) + Convert.ToDouble(lblpayamount.Text) + Convert.ToDouble(txtAdvance.Text) + Convert.ToDouble(lblAdvancePaid.Text));

            //    if (Convert.ToDouble(lblTotalrecamt.Text) < Convert.ToDouble(ss.ToString("f2")))
            //    {
            //        chkAllowit.Checked = true;
            //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('PayableAmount is Mismatched in LotNo " + lblCompanyLotNo.Text + "');", true);
            //        //save.Enabled = false;
            //        //return;
            //    }
            //    else if (ttl != 0 && Convert.ToDouble(txtAdvance.Text) != 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Either can be paid in LotNo " + lblCompanyLotNo.Text + "');", true);
            //        save.Enabled = false;
            //        return;
            //    }
            //    else if (Convert.ToDouble(lblTotalrecamt.Text) <= (Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblAlreadyPaid.Text) + Convert.ToDouble(lblpayamount.Text) + Convert.ToDouble(txtAdvance.Text) + Convert.ToDouble(lblAdvancePaid.Text)))
            //    {
            //        CheckBox item = (CheckBox)gvpayment.Rows[i].FindControl("chkitemchecked");
            //        if (item.Checked == false)
            //        {
            //            //////ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Plz Close the LotNo " + lblCompanyLotNo.Text + "');", true);
            //            //////save.Enabled = false;
            //            //////return;
            //            item.Checked = true;
            //        }
            //        else
            //        {
            //            save.Enabled = true;
            //        }

            //    }
            //    else
            //    {
            //        save.Enabled = true;
            //    }



            //    #endregion
            //}

            //for (int i = 0; i < gvpayment.Rows.Count; i++)
            //{
            //    #region Calculate

            //    Label lblCompanyLotNo = (Label)gvpayment.Rows[i].FindControl("lblCompanyLotNo");
            //    Label lblStichingId = (Label)gvpayment.Rows[i].FindControl("lblStichingId");

            //    TextBox lblpayamount = (TextBox)gvpayment.Rows[i].FindControl("lblpayamount");
            //    if (lblpayamount.Text == "")
            //        lblpayamount.Text = "0";

            //    TextBox txtPaymentQty = (TextBox)gvpayment.Rows[i].FindControl("txtPaymentQty");
            //    if (txtPaymentQty.Text == "")
            //        txtPaymentQty.Text = "0";

            //    TextBox txtDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtDebitAmt");
            //    if (txtDebitAmt.Text == "")
            //        txtDebitAmt.Text = "0";

            //    TextBox txtmiscellaneous = (TextBox)gvpayment.Rows[i].FindControl("txtmiscellaneous");
            //    if (txtmiscellaneous.Text == "")
            //        txtmiscellaneous.Text = "0";

            //    TextBox txtAdvance = (TextBox)gvpayment.Rows[i].FindControl("txtAdvance");
            //    if (txtAdvance.Text == "")
            //        txtAdvance.Text = "0";

            //    DropDownList drppaymenttype = (DropDownList)gvpayment.Rows[i].FindControl("drppaymenttype");


            //    DebitAmt = DebitAmt + Convert.ToDouble(txtDebitAmt.Text);
            //    TtlQty = TtlQty + Convert.ToInt32(txtPaymentQty.Text);

            //    if (drppaymenttype.SelectedValue == "Balance")
            //    {
            //        TotalAmount = TotalAmount + Convert.ToDouble(lblpayamount.Text);
            //    }
            //    else
            //    {
            //        ////// Advance = Advance + Convert.ToDouble(txtAdvance.Text);
            //        Advance = Advance + Convert.ToDouble(lblpayamount.Text);
            //    }

            //    miscellaneous = miscellaneous + Convert.ToDouble(txtmiscellaneous.Text);

            //    #endregion
            //}


            //lbllTotalQty.Text = TtlQty.ToString();

            //lblbillAmt.Text = TotalAmount.ToString("F2");
            //lbladv.Text = Advance.ToString("F2");

            //lblMisc.Text = miscellaneous.ToString("F2");

            //double lessamt = Convert.ToDouble(txttrimsdebitamt.Text) + DebitAmt;
            //lblDebitAmt.Text = DebitAmt.ToString("F2");

            //double valamt = TotalAmount - lessamt;
            //lbllTotalAmount.Text = (valamt + sampleAmt + Advance + miscellaneous).ToString("F2");

            //txtTotalAmount.Text = (valamt + sampleAmt + Advance + miscellaneous).ToString("F2");


            //if (Convert.ToDouble(txtTotalAmount.Text) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Check TotalAmount.')", true);
            //    ddlEName.Focus();
            //    return;
            //}
            //#endregion



            #region Calculations
            double sampleAmt = 0;

            int TtlQty = 0;
            double TotalAmount = 0;
            double DebitAmt = 0;
            double miscellaneous = 0;
            double Advance = 0;

            for (int i = 0; i < gvpayment.Rows.Count; i++)
            {
                #region Checking


                Label lblRate = (Label)gvpayment.Rows[i].FindControl("lblRate");
                TextBox txtPaymentQty = (TextBox)gvpayment.Rows[i].FindControl("txtPaymentQty");
                if (txtPaymentQty.Text == "")
                    txtPaymentQty.Text = "0";

                CheckBox chkAllowit = (CheckBox)gvpayment.Rows[i].FindControl("chkAllowit");

                CheckBox chkitemchecked = (CheckBox)gvpayment.Rows[i].FindControl("chkitemchecked");

                Label lblCompanyLotNo = (Label)gvpayment.Rows[i].FindControl("lblCompanyLotNo");
                Label lblAlreadyPaid = (Label)gvpayment.Rows[i].FindControl("lblAlreadyPaid");

                Label lblTotalrecamt = (Label)gvpayment.Rows[i].FindControl("lblTotalrecamt");

                Label lblAdvancePaid = (Label)gvpayment.Rows[i].FindControl("lblAdvancePaid");

                TextBox lblpayamount = (TextBox)gvpayment.Rows[i].FindControl("lblpayamount");
                if (lblpayamount.Text == "")
                    lblpayamount.Text = "0";

                TextBox lblpayamountNew = (TextBox)gvpayment.Rows[i].FindControl("lblpayamountNew");
                if (lblpayamountNew.Text == "")
                    lblpayamountNew.Text = "0";

                TextBox txtDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtDebitAmt");
                if (txtDebitAmt.Text == "")
                    txtDebitAmt.Text = "0";

                TextBox txtAdvance = (TextBox)gvpayment.Rows[i].FindControl("txtAdvance");
                if (txtAdvance.Text == "")
                    txtAdvance.Text = "0";

                Label lblTotalIssue = (Label)gvpayment.Rows[i].FindControl("lblTotalIssue");

                Label lblTotalDamage = (Label)gvpayment.Rows[i].FindControl("lblTotalDamage");

                TextBox txtTotalPaidAmt = (TextBox)gvpayment.Rows[i].FindControl("txtTotalPaidAmt");
                if (txtTotalPaidAmt.Text == "")
                    txtTotalPaidAmt.Text = "0";

                Label lblBalAmount = (Label)gvpayment.Rows[i].FindControl("lblBalAmount");



                lblpayamountNew.Text = ((Convert.ToDouble(txtPaymentQty.Text) * Convert.ToDouble(lblRate.Text)) - Convert.ToDouble(txtDebitAmt.Text)).ToString("f2");

                //////if (Convert.ToDouble(lblBalAmount.Text) < Convert.ToDouble(lblpayamount.Text))
                //////{
                //////    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Payable Amount in LotNo " + lblCompanyLotNo.Text + "');", true);
                //////    save.Enabled = false;
                //////    return;
                //////}

                //if (lblAlreadyPaid.Text == "0")
                //{

                //    lblpayamount.Text = ((Convert.ToDouble(txtPaymentQty.Text) * Convert.ToDouble(lblRate.Text))).ToString("f2");
                //}
                //else
                //{
                //    double totamnt = Convert.ToDouble(lblTotalIssue.Text) * Convert.ToDouble(lblRate.Text) ;
                //    double dmgamnt = Convert.ToDouble(lblTotalDamage.Text) * Convert.ToDouble(lblRate.Text);

                //    double lotvalue = totamnt - dmgamnt;





                //}



                TextBox txtpreDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtpreDebitAmt");
                if (txtpreDebitAmt.Text == "")
                    txtpreDebitAmt.Text = "0";

                txtAdvance.Text = "0";
                lblpayamount.Text = "0";

                if (Convert.ToDouble(lblTotalrecamt.Text) < (Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblAlreadyPaid.Text) + Convert.ToDouble(lblpayamountNew.Text) + Convert.ToDouble(txtAdvance.Text) + Convert.ToDouble(lblAdvancePaid.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Payable Amount in LotNo " + lblCompanyLotNo.Text + "');", true);
                    save.Enabled = false;
                    return;
                }


                double ttl = Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblpayamountNew.Text);

                double ss = (Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblAlreadyPaid.Text) + Convert.ToDouble(lblpayamountNew.Text) + Convert.ToDouble(txtAdvance.Text) + Convert.ToDouble(lblAdvancePaid.Text));

                if (Convert.ToDouble(lblTotalrecamt.Text) < Convert.ToDouble(ss.ToString("f2")))
                {

                    chkAllowit.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('PayableAmount is Mismatched in LotNo " + lblCompanyLotNo.Text + "');", true);
                    //save.Enabled = false;
                    //return;
                }
                else if (ttl != 0 && Convert.ToDouble(txtAdvance.Text) != 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Either can be paid in LotNo " + lblCompanyLotNo.Text + "');", true);
                    save.Enabled = false;
                    return;
                }
                else if (Convert.ToDouble(lblTotalrecamt.Text) <= (Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtpreDebitAmt.Text) + Convert.ToDouble(lblAlreadyPaid.Text) + Convert.ToDouble(lblpayamountNew.Text) + Convert.ToDouble(txtAdvance.Text) + Convert.ToDouble(lblAdvancePaid.Text)))
                {
                    CheckBox item = (CheckBox)gvpayment.Rows[i].FindControl("chkitemchecked");
                    if (item.Checked == false)
                    {
                        //////ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Plz Close the LotNo " + lblCompanyLotNo.Text + "');", true);
                        //////save.Enabled = false;
                        //////return;
                        item.Checked = true;
                    }
                    else
                    {
                        save.Enabled = true;
                        item.Checked = false;
                    }

                }
                else
                {
                    save.Enabled = true;
                    chkitemchecked.Checked = false;
                }



                #endregion
            }

            for (int i = 0; i < gvpayment.Rows.Count; i++)
            {
                #region Calculate

                Label lblCompanyLotNo = (Label)gvpayment.Rows[i].FindControl("lblCompanyLotNo");
                Label lblStichingId = (Label)gvpayment.Rows[i].FindControl("lblStichingId");

                TextBox lblpayamount = (TextBox)gvpayment.Rows[i].FindControl("lblpayamount");
                if (lblpayamount.Text == "")
                    lblpayamount.Text = "0";

                TextBox lblpayamountNew = (TextBox)gvpayment.Rows[i].FindControl("lblpayamountNew");
                if (lblpayamountNew.Text == "")
                    lblpayamountNew.Text = "0";

                TextBox txtPaymentQty = (TextBox)gvpayment.Rows[i].FindControl("txtPaymentQty");
                if (txtPaymentQty.Text == "")
                    txtPaymentQty.Text = "0";

                TextBox txtDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtDebitAmt");
                if (txtDebitAmt.Text == "")
                    txtDebitAmt.Text = "0";

                TextBox txtmiscellaneous = (TextBox)gvpayment.Rows[i].FindControl("txtmiscellaneous");
                if (txtmiscellaneous.Text == "")
                    txtmiscellaneous.Text = "0";

                TextBox txtAdvance = (TextBox)gvpayment.Rows[i].FindControl("txtAdvance");
                if (txtAdvance.Text == "")
                    txtAdvance.Text = "0";

                DropDownList drppaymenttype = (DropDownList)gvpayment.Rows[i].FindControl("drppaymenttype");


                DebitAmt = DebitAmt + Convert.ToDouble(txtDebitAmt.Text);
                TtlQty = TtlQty + Convert.ToInt32(txtPaymentQty.Text);
                lblpayamount.Text = "0";
                txtAdvance.Text = "0";

                if (drppaymenttype.SelectedValue == "Balance")
                {
                    lblpayamount.Text = Convert.ToDouble(lblpayamountNew.Text).ToString("0.00");
                    TotalAmount = TotalAmount + Convert.ToDouble(lblpayamountNew.Text);
                }
                else
                {
                    ////// Advance = Advance + Convert.ToDouble(txtAdvance.Text);
                    Advance = Advance + Convert.ToDouble(lblpayamountNew.Text);
                    txtAdvance.Text = Convert.ToDouble(lblpayamountNew.Text).ToString("0.00");
                }

                miscellaneous = miscellaneous + Convert.ToDouble(txtmiscellaneous.Text);

                #endregion
            }


            lbllTotalQty.Text = TtlQty.ToString();

            lblbillAmt.Text = TotalAmount.ToString("F2");
            lbladv.Text = Advance.ToString("F2");

            lblMisc.Text = miscellaneous.ToString("F2");

            double lessamt = Convert.ToDouble(txttrimsdebitamt.Text) + DebitAmt;
            lblDebitAmt.Text = DebitAmt.ToString("F2");

            double valamt = TotalAmount - lessamt;
            lbllTotalAmount.Text = (valamt + sampleAmt + Advance + miscellaneous).ToString("F2");

            txtTotalAmount.Text = (valamt + sampleAmt + Advance + miscellaneous).ToString("F2");


            if (Convert.ToDouble(txtTotalAmount.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Check TotalAmount.')", true);
                ddlEName.Focus();
                return;
            }
            #endregion




            if (ddlEName.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select JobWorker.Thank You!!!.')", true);
                ddlEName.Focus();
                return;
            }

            if (DpProcess.SelectedValue == "Select Process Name")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Process.Thank You!!!.')", true);
                DpProcess.Focus();
                return;
            }

            string bank = string.Empty;
            string chequeno = string.Empty;
            if (ddlPayment.SelectedValue == "1")
            {
                chequeno = "0";
                bank = "0";
            }
            else
            {
                chequeno = txtcheque.Text;
                bank = txtbank.Text;
            }

            #region

            DateTime pdate = DateTime.ParseExact(txtpdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (save.Text == "Save")
            {


                int patinsertid = objBs.jppaymentinsertothers(pdate, ddlEName.SelectedValue, Convert.ToDouble(txtTotalAmount.Text), txtNarration.Text, DpProcess.SelectedValue, ddlPayment.SelectedValue, bank, chequeno, "Stc", Convert.ToInt32(lbllTotalQty.Text), Convert.ToInt32(drpbranch.SelectedValue));


                for (int i = 0; i < gvpayment.Rows.Count; i++)
                {
                    int Iscomplete = 0;
                    Label lblCompanyLotNo = (Label)gvpayment.Rows[i].FindControl("lblCompanyLotNo");
                    Label lblStichingId = (Label)gvpayment.Rows[i].FindControl("lblStichingId");

                    TextBox txtmiscellaneous = (TextBox)gvpayment.Rows[i].FindControl("txtmiscellaneous");

                    Label lblRate = (Label)gvpayment.Rows[i].FindControl("lblRate");

                    TextBox txtDebitAmt = (TextBox)gvpayment.Rows[i].FindControl("txtDebitAmt");
                    TextBox txtPaymentQty = (TextBox)gvpayment.Rows[i].FindControl("txtPaymentQty");
                    TextBox lblpayamount = (TextBox)gvpayment.Rows[i].FindControl("lblpayamount");

                    TextBox lblpayamountNew = (TextBox)gvpayment.Rows[i].FindControl("lblpayamountNew");

                    TextBox txtAdvance = (TextBox)gvpayment.Rows[i].FindControl("txtAdvance");
                    TextBox txtnarration = (TextBox)gvpayment.Rows[i].FindControl("txtnarration");

                    DropDownList drppaymenttype = (DropDownList)gvpayment.Rows[i].FindControl("drppaymenttype");
                    lblpayamount.Text = "0";
                    txtAdvance.Text = "0";

                    if (drppaymenttype.SelectedValue == "Advance")
                    {
                        txtAdvance.Text = lblpayamountNew.Text;
                        
                    }
                    else
                    {

                        lblpayamount.Text = lblpayamountNew.Text;
                    }

                    double All = Convert.ToDouble(txtDebitAmt.Text) + Convert.ToDouble(txtPaymentQty.Text) + Convert.ToDouble(lblpayamount.Text) + Convert.ToDouble(txtAdvance.Text);



                    CheckBox item = (CheckBox)gvpayment.Rows[i].FindControl("chkitemchecked");
                    if (item.Checked == true)
                        Iscomplete = 1;
                    else
                        Iscomplete = 0;

                    if (All > 0)
                    {
                        int j = objBs.jppaymentinserttransothers(patinsertid, lblCompanyLotNo.Text, Convert.ToDouble(lblpayamount.Text), Convert.ToInt32(lblStichingId.Text), "Stc", Convert.ToInt32(DpProcess.SelectedValue), 0, Convert.ToDouble(lblRate.Text), Convert.ToInt32(txtPaymentQty.Text), Convert.ToDouble(txtDebitAmt.Text), Iscomplete, Convert.ToDouble(txtmiscellaneous.Text), Convert.ToDouble(txtAdvance.Text), txtnarration.Text, drppaymenttype.SelectedValue);

                    }

                }



                Response.Redirect("PaymentGridView.aspx");
            }


            #endregion

        }
        protected void bank_select(object sender, EventArgs e)
        {
            if (drpbanklist.SelectedValue == "Select Bank")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name. Thank you!!');", true);
                return;
            }
            else
            {
                DataSet getbankdetailss = objBs.getbankdetailsforparbank(drpbanklist.SelectedValue);
                if (getbankdetailss.Tables[0].Rows.Count > 0)
                {
                    txtbank.Text = getbankdetailss.Tables[0].Rows[0]["Bankname"].ToString();
                    lblbankname.Text = getbankdetailss.Tables[0].Rows[0]["Bankname"].ToString();
                    lblbankaccno.Text = getbankdetailss.Tables[0].Rows[0]["BankAccno"].ToString();
                    lblbankdesc.Text = getbankdetailss.Tables[0].Rows[0]["BankDes"].ToString();
                }
            }
        }
        protected void ddlPayment_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPayment.SelectedValue == "1")
            {
                bankdetails.Visible = false;
                txtbank.Enabled = false;
                txtcheque.Enabled = false;
                drpbanklist.Enabled = false;
                drpbanklist.ClearSelection();
                drpbanklist.DataSource = null;
                drpbanklist.DataBind();
                drpbanklist.Items.Insert(0, "Select Bank");
            }
            else if (ddlPayment.SelectedValue == "2")
            {
                drpbanklist.Enabled = true;
                if (ddlEName.SelectedValue == "ALL")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Any One Jobworker/Employee For Bank Paymode. Thank you!!');", true);
                    return;
                }


                DataSet getbankdetails = objBs.getbankdetailsforpayment(ddlEName.SelectedValue);
                if (getbankdetails.Tables[0].Rows.Count > 0)
                {

                    {
                        drpbanklist.DataSource = getbankdetails.Tables[0];
                        drpbanklist.DataTextField = "bankname";
                        drpbanklist.DataValueField = "JobBankId";
                        drpbanklist.DataBind();
                        drpbanklist.Items.Insert(0, "Select Bank");

                    }
                }
                else
                {
                    drpbanklist.DataSource = null;
                    drpbanklist.DataBind();
                    drpbanklist.Items.Insert(0, "Select Bank");
                }

                txtbank.Enabled = true;
                txtcheque.Enabled = true;
                bankdetails.Visible = true;
            }
        }

        protected void drpbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            gvpayment.DataSource = null;
            gvpayment.DataBind();


        }
        protected void rdbprocesstype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbprocesstype.SelectedValue == "")
            {

            }
            else
            {
                #region
                if (rdbprocesstype.SelectedValue == "IronerIn")
                {
                    gvpayment.DataSource = null;
                    gvpayment.DataBind();

                    txtironrecdate.Enabled = true;
                    txtironrecdate2.Enabled = false;

                    btnsearch.Enabled = true;
                    DpProcess.Enabled = false;

                    idbillAmt.Visible = false;
                    idDebitAmt.Visible = false;
                    idtrimsdebitamt.Visible = false;
                    idsampleAmt.Visible = false;
                    idMisc.Visible = false;
                    idadv.Visible = false;

                    DataSet drppEmp = objBs.Selectname("9");
                    if (drppEmp.Tables[0].Rows.Count > 0)
                    {
                        ddlEName.DataSource = drppEmp;
                        ddlEName.DataTextField = "LedgerName";
                        ddlEName.DataValueField = "LedgerId";
                        ddlEName.DataBind();
                        ddlEName.Items.Insert(0, "ALL");
                    }
                }
                else if (rdbprocesstype.SelectedValue == "IronerOut")
                {
                    txtironrecdate.Enabled = true;
                    txtironrecdate2.Enabled = true;

                    btnsearch.Enabled = true;
                    DpProcess.Enabled = false;


                    idbillAmt.Visible = false;
                    idDebitAmt.Visible = false;
                    idtrimsdebitamt.Visible = false;
                    idsampleAmt.Visible = false;
                    idMisc.Visible = false;
                    idadv.Visible = false;

                    DataSet drppEmp = objBs.Selectname("10");
                    if (drppEmp.Tables[0].Rows.Count > 0)
                    {
                        ddlEName.DataSource = drppEmp;
                        ddlEName.DataTextField = "LedgerName";
                        ddlEName.DataValueField = "LedgerId";
                        ddlEName.DataBind();
                        ddlEName.Items.Insert(0, "ALL");
                    }

                }
                else if (rdbprocesstype.SelectedValue == "Others")
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    txtironrecdate.Enabled = true;
                    txtironrecdate2.Enabled = true;

                    btnsearch.Enabled = true;
                    DpProcess.Enabled = true;

                    idbillAmt.Visible = true;
                    idDebitAmt.Visible = true;
                    idtrimsdebitamt.Visible = false;
                    idsampleAmt.Visible = false;
                    idMisc.Visible = true;
                    idadv.Visible = true;

                    DataSet drppEmp = objBs.Selectname("10");
                    if (drppEmp.Tables[0].Rows.Count > 0)
                    {
                        ddlEName.DataSource = drppEmp;
                        ddlEName.DataTextField = "LedgerName";
                        ddlEName.DataValueField = "LedgerId";
                        ddlEName.DataBind();
                        ddlEName.Items.Insert(0, "ALL");
                    }
                }
                #endregion
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GridView1.DataSource = null;
            GridView1.DataBind();

            if (drpbranch.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch.');", true);
                return;
            }
            if (DpProcess.SelectedValue == "" || DpProcess.SelectedValue == "0" || DpProcess.SelectedValue == "Select Process Name")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Process.')", true);
                return;
            }
            if (ddlEName.SelectedValue == "" || ddlEName.SelectedValue == "0" || ddlEName.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select JobWork.')", true);
                return;
            }

            DateTime irondate = DateTime.ParseExact(txtironrecdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime irondate2 = DateTime.ParseExact(txtironrecdate2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            #region

            DataSet payment = objBs.paypayment(ddlEName.SelectedValue, Convert.ToInt32(DpProcess.SelectedValue), irondate, irondate2, drpbranch.SelectedValue);
            if (payment.Tables[0].Rows.Count > 0)
            {


                #region
                DataTable dttt;
                DataColumn dct;
                DataSet dstd = new DataSet();
                dttt = new DataTable();

                dct = new DataColumn("idd");
                dttt.Columns.Add(dct);

                dct = new DataColumn("CompanyLotNo");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Date");
                dttt.Columns.Add(dct);

                dct = new DataColumn("TotalIssue");
                dttt.Columns.Add(dct);

                dct = new DataColumn("TotalReceive");
                dttt.Columns.Add(dct);

                dct = new DataColumn("TotalDamage");
                dttt.Columns.Add(dct);

                dct = new DataColumn("TotalAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("TotalDamageAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("PaidAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("PayableAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("PaymentQty");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Rate");
                dttt.Columns.Add(dct);

                dct = new DataColumn("PreDebitAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("SampleAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("ValId");
                dttt.Columns.Add(dct);

                dct = new DataColumn("DebitAmount");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Advance");
                dttt.Columns.Add(dct);

                dct = new DataColumn("TotalPaid");
                dttt.Columns.Add(dct);

                dct = new DataColumn("BalAmount");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                for (int vLoop1 = 0; vLoop1 < payment.Tables[0].Rows.Count; vLoop1++)
                {
                    DataSet paymentrate = objBs.paypaymentrate(payment.Tables[0].Rows[vLoop1]["ValId"].ToString(), Convert.ToInt32(DpProcess.SelectedValue));

                    DataRow drd = dstd.Tables[0].NewRow();
                    drd["idd"] = payment.Tables[0].Rows[vLoop1]["idd"].ToString();
                    drd["ValId"] = payment.Tables[0].Rows[vLoop1]["ValId"].ToString();
                    drd["CompanyLotNo"] = payment.Tables[0].Rows[vLoop1]["CompanyLotNo"].ToString();
                    drd["Date"] = Convert.ToDateTime(payment.Tables[0].Rows[vLoop1]["Date"]).ToString("dd/MM/yyyy");
                    drd["TotalIssue"] = payment.Tables[0].Rows[vLoop1]["TotalIssue"].ToString();
                    drd["TotalReceive"] = payment.Tables[0].Rows[vLoop1]["TotalReceive"].ToString();
                    drd["TotalDamage"] = payment.Tables[0].Rows[vLoop1]["TotalDamage"].ToString();
                    drd["TotalAmount"] = Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["TotalAmount"]).ToString("f2");
                    drd["TotalDamageAmount"] = Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["TotalDamageAmount"]).ToString("f2");
                    drd["PaidAmount"] = Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["PaidAmount"]).ToString("f2");
                    drd["Advance"] = Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["Advance"]).ToString("f2");
                    //drd["PayableAmount"] = ((Convert.ToDouble(paymentrate.Tables[0].Rows[0]["Rate"]) * Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["PaymentQty"])) - Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["Advance"])).ToString("f2");   //(Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["TotalAmount"]) - Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["PaidAmount"])).ToString("f2");
                    drd["PayableAmount"] = "0"; //((Convert.ToDouble(paymentrate.Tables[0].Rows[0]["Rate"]) * Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["PaymentQty"]))).ToString("f2");
                    drd["PaymentQty"] = payment.Tables[0].Rows[vLoop1]["PaymentQty"].ToString();
                    drd["PreDebitAmount"] = Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["DebitAmount"]).ToString("f2");
                    drd["Rate"] = Convert.ToDouble(paymentrate.Tables[0].Rows[0]["Rate"]).ToString("f2");
                    drd["DebitAmount"] = 0.ToString("f2");
                    drd["SampleAmount"] = 0.ToString("f2");
                    drd["TotalPaid"] = (Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["PaidAmount"]) + Convert.ToDouble(payment.Tables[0].Rows[vLoop1]["Advance"])).ToString("f2");
                    drd["BalAmount"] = (Convert.ToDouble(drd["TotalAmount"]) - Convert.ToDouble(drd["TotalPaid"]) - Convert.ToDouble(drd["PreDebitAmount"])).ToString("0.00");

                    if (Convert.ToDouble(drd["BalAmount"]) > 0)
                    {
                        dstd.Tables[0].Rows.Add(drd);
                    }
                }

                #endregion

                gvpayment.DataSource = dstd;
                gvpayment.DataBind();
            }
            else
            {
                gvpayment.DataSource = null;
                gvpayment.DataBind();
            }



            #endregion

        }
        protected void GVPaymentGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double PaidAmount = 0;

            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PaidAmount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PaidAmount"));
                if (PaidAmount > 0)
                {
                    //  e.Row.Cells[11].BackColor = System.Drawing.Color.Red;
                    e.Row.BackColor = System.Drawing.Color.Beige;
                    //  e.Row.BackColor = Color.Blue;
                }


            }

            #endregion
        }
        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region

                if (e.Row.Cells[5].Text != "-" && e.Row.Cells[5].Text != "")
                {
                    F30 = F30 + Convert.ToDouble(e.Row.Cells[5].Text);
                }
                if (e.Row.Cells[6].Text != "-" && e.Row.Cells[6].Text != "")
                {
                    F32 = F32 + Convert.ToDouble(e.Row.Cells[6].Text);
                }
                if (e.Row.Cells[7].Text != "-" && e.Row.Cells[7].Text != "")
                {
                    F34 = F34 + Convert.ToDouble(e.Row.Cells[7].Text);
                }
                if (e.Row.Cells[8].Text != "-" && e.Row.Cells[8].Text != "")
                {
                    F36 = F36 + Convert.ToDouble(e.Row.Cells[8].Text);
                }
                if (e.Row.Cells[9].Text != "-" && e.Row.Cells[9].Text != "")
                {
                    FXS = FXS + Convert.ToDouble(e.Row.Cells[9].Text);
                }
                if (e.Row.Cells[10].Text != "-" && e.Row.Cells[10].Text != "")
                {
                    FS = FS + Convert.ToDouble(e.Row.Cells[10].Text);
                }
                if (e.Row.Cells[11].Text != "-" && e.Row.Cells[11].Text != "")
                {
                    FM = FM + Convert.ToDouble(e.Row.Cells[11].Text);
                }
                if (e.Row.Cells[12].Text != "-" && e.Row.Cells[12].Text != "")
                {
                    FL = FL + Convert.ToDouble(e.Row.Cells[12].Text);
                }
                if (e.Row.Cells[13].Text != "-" && e.Row.Cells[13].Text != "")
                {
                    FXL = FXL + Convert.ToDouble(e.Row.Cells[13].Text);
                }
                if (e.Row.Cells[14].Text != "-" && e.Row.Cells[14].Text != "")
                {
                    FXXL = FXXL + Convert.ToDouble(e.Row.Cells[14].Text);
                }
                if (e.Row.Cells[15].Text != "-" && e.Row.Cells[15].Text != "")
                {
                    F3XL = F3XL + Convert.ToDouble(e.Row.Cells[15].Text);
                }
                if (e.Row.Cells[16].Text != "-" && e.Row.Cells[16].Text != "")
                {
                    F4XL = F4XL + Convert.ToDouble(e.Row.Cells[16].Text);
                }
                if (e.Row.Cells[17].Text != "-" && e.Row.Cells[17].Text != "")
                {

                    H30 = H30 + Convert.ToDouble(e.Row.Cells[17].Text);
                }
                if (e.Row.Cells[18].Text != "-" && e.Row.Cells[18].Text != "")
                {
                    H32 = H32 + Convert.ToDouble(e.Row.Cells[18].Text);
                }
                if (e.Row.Cells[19].Text != "-" && e.Row.Cells[19].Text != "")
                {
                    H34 = H34 + Convert.ToDouble(e.Row.Cells[19].Text);
                }
                if (e.Row.Cells[20].Text != "-" && e.Row.Cells[20].Text != "")
                {
                    H36 = H36 + Convert.ToDouble(e.Row.Cells[20].Text);
                }
                if (e.Row.Cells[21].Text != "-" && e.Row.Cells[21].Text != "")
                {
                    HXS = HXS + Convert.ToDouble(e.Row.Cells[21].Text);
                }
                if (e.Row.Cells[22].Text != "-" && e.Row.Cells[22].Text != "")
                {
                    HS = HS + Convert.ToDouble(e.Row.Cells[22].Text);
                }
                if (e.Row.Cells[23].Text != "-" && e.Row.Cells[23].Text != "")
                {
                    HM = HM + Convert.ToDouble(e.Row.Cells[23].Text);
                }
                if (e.Row.Cells[24].Text != "-" && e.Row.Cells[24].Text != "")
                {
                    HL = HL + Convert.ToDouble(e.Row.Cells[24].Text);
                }
                if (e.Row.Cells[25].Text != "-" && e.Row.Cells[25].Text != "")
                {
                    HXL = HXL + Convert.ToDouble(e.Row.Cells[25].Text);
                }
                if (e.Row.Cells[26].Text != "-" && e.Row.Cells[26].Text != "")
                {
                    HXXL = HXXL + Convert.ToDouble(e.Row.Cells[26].Text);
                }
                if (e.Row.Cells[27].Text != "-" && e.Row.Cells[27].Text != "")
                {
                    H3XL = H3XL + Convert.ToDouble(e.Row.Cells[27].Text);
                }
                if (e.Row.Cells[28].Text != "-" && e.Row.Cells[28].Text != "")
                {
                    H4XL = H4XL + Convert.ToDouble(e.Row.Cells[28].Text);
                }

                #endregion

                TOTAL = TOTAL + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TtlQty"));

                #region




                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "-";
                }
                if (e.Row.Cells[6].Text == "0")
                {
                    e.Row.Cells[6].Text = "-";
                }
                if (e.Row.Cells[7].Text == "0")
                {
                    e.Row.Cells[7].Text = "-";
                }
                if (e.Row.Cells[8].Text == "0")
                {
                    e.Row.Cells[8].Text = "-";
                }
                if (e.Row.Cells[9].Text == "0")
                {
                    e.Row.Cells[9].Text = "-";
                }
                if (e.Row.Cells[10].Text == "0")
                {
                    e.Row.Cells[10].Text = "-";
                }
                if (e.Row.Cells[11].Text == "0")
                {
                    e.Row.Cells[11].Text = "-";
                }
                if (e.Row.Cells[12].Text == "0")
                {
                    e.Row.Cells[12].Text = "-";
                }
                if (e.Row.Cells[13].Text == "0")
                {
                    e.Row.Cells[13].Text = "-";
                }
                if (e.Row.Cells[14].Text == "0")
                {
                    e.Row.Cells[14].Text = "-";
                }

                if (e.Row.Cells[15].Text == "0")
                {
                    e.Row.Cells[15].Text = "-";
                }
                if (e.Row.Cells[16].Text == "0")
                {
                    e.Row.Cells[16].Text = "-";
                }
                if (e.Row.Cells[17].Text == "0")
                {
                    e.Row.Cells[17].Text = "-";
                }
                if (e.Row.Cells[18].Text == "0")
                {
                    e.Row.Cells[18].Text = "-";
                }
                if (e.Row.Cells[19].Text == "0")
                {
                    e.Row.Cells[19].Text = "-";
                }
                if (e.Row.Cells[20].Text == "0")
                {
                    e.Row.Cells[20].Text = "-";
                }
                if (e.Row.Cells[21].Text == "0")
                {
                    e.Row.Cells[21].Text = "-";
                }
                if (e.Row.Cells[22].Text == "0")
                {
                    e.Row.Cells[22].Text = "-";
                }
                if (e.Row.Cells[23].Text == "0")
                {
                    e.Row.Cells[23].Text = "-";
                }
                if (e.Row.Cells[24].Text == "0")
                {
                    e.Row.Cells[24].Text = "-";
                }
                if (e.Row.Cells[25].Text == "0")
                {
                    e.Row.Cells[25].Text = "-";
                }
                if (e.Row.Cells[26].Text == "0")
                {
                    e.Row.Cells[26].Text = "-";
                }
                if (e.Row.Cells[27].Text == "0")
                {
                    e.Row.Cells[27].Text = "-";
                }
                if (e.Row.Cells[28].Text == "0")
                {
                    e.Row.Cells[28].Text = "-";
                }
                #endregion

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region
                //e.Row.Cells[3].Text = F30.ToString();
                //e.Row.Cells[4].Text = F32.ToString();
                //e.Row.Cells[5].Text = F34.ToString();
                //e.Row.Cells[6].Text = F36.ToString();
                //e.Row.Cells[7].Text = FXS.ToString();
                //e.Row.Cells[8].Text = FS.ToString();
                //e.Row.Cells[9].Text = FM.ToString();
                //e.Row.Cells[10].Text = FL.ToString();
                //e.Row.Cells[11].Text = FXL.ToString();
                //e.Row.Cells[12].Text = FXXL.ToString();
                //e.Row.Cells[13].Text = F3XL.ToString();
                //e.Row.Cells[14].Text = F4XL.ToString();

                //e.Row.Cells[15].Text = H30.ToString();
                //e.Row.Cells[16].Text = H32.ToString();
                //e.Row.Cells[17].Text = H34.ToString();
                //e.Row.Cells[18].Text = H36.ToString();
                //e.Row.Cells[19].Text = HXS.ToString();
                //e.Row.Cells[20].Text = HS.ToString();
                //e.Row.Cells[21].Text = HM.ToString();
                //e.Row.Cells[22].Text = HL.ToString();
                //e.Row.Cells[23].Text = HXL.ToString();
                //e.Row.Cells[24].Text = HXXL.ToString();
                //e.Row.Cells[25].Text = H3XL.ToString();
                //e.Row.Cells[26].Text = H3XL.ToString();

                //e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;

                //e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[23].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[24].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[25].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[26].HorizontalAlign = HorizontalAlign.Right;


                //e.Row.Cells[3].Font.Bold = true;
                //e.Row.Cells[4].Font.Bold = true;
                //e.Row.Cells[5].Font.Bold = true;
                //e.Row.Cells[6].Font.Bold = true;
                //e.Row.Cells[7].Font.Bold = true;
                //e.Row.Cells[8].Font.Bold = true;
                //e.Row.Cells[9].Font.Bold = true;
                //e.Row.Cells[10].Font.Bold = true;
                //e.Row.Cells[11].Font.Bold = true;
                //e.Row.Cells[12].Font.Bold = true;
                //e.Row.Cells[13].Font.Bold = true;
                //e.Row.Cells[14].Font.Bold = true;

                //e.Row.Cells[15].Font.Bold = true;
                //e.Row.Cells[16].Font.Bold = true;
                //e.Row.Cells[17].Font.Bold = true;
                //e.Row.Cells[18].Font.Bold = true;
                //e.Row.Cells[19].Font.Bold = true;
                //e.Row.Cells[20].Font.Bold = true;
                //e.Row.Cells[21].Font.Bold = true;
                //e.Row.Cells[22].Font.Bold = true;
                //e.Row.Cells[23].Font.Bold = true;
                //e.Row.Cells[24].Font.Bold = true;
                //e.Row.Cells[25].Font.Bold = true;
                //e.Row.Cells[26].Font.Bold = true;

                //e.Row.Cells[4].Text = "TOTAL :";
                //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[4].Font.Bold = true;

                e.Row.Cells[29].Text = TOTAL.ToString();
                e.Row.Cells[29].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[29].Font.Bold = true;


                #endregion
            }
        }

        //protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        //{


        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Quantity1 = Quantity1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity1"));
        //        PieceRate1 = PieceRate1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PieceRate1"));
        //        Amount1 = Amount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount1"));

        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.Cells[1].Text = "Total:- ";
        //        e.Row.Cells[2].Text = Quantity1.ToString();
        //        e.Row.Cells[3].Text = PieceRate1.ToString();
        //        e.Row.Cells[4].Text = Amount1.ToString();

        //        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        //        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
        //        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
        //        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;

        //        e.Row.Cells[1].Font.Bold = true;
        //        e.Row.Cells[2].Font.Bold = true;
        //        e.Row.Cells[3].Font.Bold = true;
        //        e.Row.Cells[4].Font.Bold = true;
        //    }
        //}
        protected void ddlEName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEName.SelectedValue == "Select Name")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Name!!!.Thanks You!!!')", true);
                return;
            }
            else if (DpProcess.SelectedValue == "Select Process Name")
            {
                if (rdbprocesstype.SelectedValue == "Others")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Process!!!.Thanks You!!!')", true);
                    return;

                }

            }
            else
            {
                if (DpProcess.SelectedValue == "5")
                {

                    //DataSet payment = objBs.paypaymentforiron(Convert.ToInt32(ddlEName.SelectedValue), Convert.ToInt32(DpProcess.SelectedValue));
                    //if (payment.Tables[0].Rows.Count > 0)
                    //{
                    //    gvpayment.DataSource = payment;
                    //    gvpayment.DataBind();
                    //}
                    //else
                    //{
                    //    gvpayment.DataSource = null;
                    //    gvpayment.DataBind();
                    //}
                }
                else
                {
                    //////DataSet payment = objBs.paypayment(ddlEName.SelectedValue, Convert.ToInt32(DpProcess.SelectedValue));
                    //////if (payment.Tables[0].Rows.Count > 0)
                    //////{
                    //////    gvpayment.DataSource = payment;
                    //////    gvpayment.DataBind();
                    //////}
                    //////else
                    //////{
                    //////    gvpayment.DataSource = null;
                    //////    gvpayment.DataBind();
                    //////}
                }
            }
        }
    }
}
