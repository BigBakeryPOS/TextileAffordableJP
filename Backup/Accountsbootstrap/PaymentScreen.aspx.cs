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
    public partial class PaymentScreen : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet drppEmp = objBs.getLedger1();
                if (drppEmp.Tables[0].Rows.Count > 0)
                {
                    ddlEName.DataSource = drppEmp;
                    ddlEName.DataTextField = "LedgerName";
                    ddlEName.DataValueField = "LedgerId";
                    ddlEName.DataBind();
                    ddlEName.Items.Insert(0, "Select Name");
                }

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtpdate.Text = dtaa;

                //txtFromDate.Text = dtaa;
                //txtToDate.Text = dtaa;

                DataSet ds12 = objBs.paymentNo("tblPayment");
                if (ds12.Tables[0].Rows.Count > 0)
                {
                    if (ds12.Tables[0].Rows[0]["Paymentno"].ToString() == "")
                    {
                        txtPaymentNo.Text = "1";
                    }
                    else
                    {
                        txtPaymentNo.Text = ds12.Tables[0].Rows[0]["Paymentno"].ToString();
                        save.Text = "Save";
                    }
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

                string ID = Request.QueryString.Get("ID");
                string name = Request.QueryString.Get("name");
                if (ID != "" || ID != null)
                {
                    DataSet dspay = objBs.getQuery(ID, name);
                     if (dspay.Tables[0].Rows.Count > 0)
                     {
                         ddlEName.SelectedValue = dspay.Tables[0].Rows[0]["workerId"].ToString();
                         DpProcess.SelectedValue = dspay.Tables[0].Rows[0]["processid"].ToString();
                         txtLotDetailId.Text = dspay.Tables[0].Rows[0]["LotDetailId"].ToString();
                         txtLotNo.Text = dspay.Tables[0].Rows[0]["LotNo"].ToString();
                         txtTotalAmount.Text = dspay.Tables[0].Rows[0]["TotalAmount"].ToString();
                         txtPaidAmount.Text = dspay.Tables[0].Rows[0]["PaidAmount"].ToString();
                     }

                }


                string DayBookID = Request.QueryString.Get("TransNo");
                if (DayBookID != "" || DayBookID != null)
                {

                    DataSet dspayment = objBs.getPaymentmaster("tblPayment"  , DayBookID);
                    if (dspayment.Tables[0].Rows.Count > 0)
                    {
                        save.Text = "Update";

                        TextBox1.Text = DayBookID;
                        txtPaymentNo.Text = dspayment.Tables[0].Rows[0]["PaymentNo"].ToString();
                        txtpdate.Text = Convert.ToDateTime(dspayment.Tables[0].Rows[0]["PaymentDate"]).ToString("dd/MM/yyyy");
                        ddlPayment.SelectedValue = dspayment.Tables[0].Rows[0]["PayModeID"].ToString();
                        txtAmount.Text = Convert.ToDouble(dspayment.Tables[0].Rows[0]["Amount"]).ToString("0.00");
                        txtNarration.Text = dspayment.Tables[0].Rows[0]["Narration"].ToString();
                        ddlEName.SelectedValue = dspayment.Tables[0].Rows[0]["ledgerid"].ToString();
                        //txtFromDate.Text = Convert.ToDateTime(dspayment.Tables[0].Rows[0]["from_Date"]).ToString("dd/MM/yyyy");
                        //txtToDate.Text = Convert.ToDateTime(dspayment.Tables[0].Rows[0]["to_Date"]).ToString("dd/MM/yyyy");
                        DpProcess.SelectedValue = dspayment.Tables[0].Rows[0]["processid"].ToString();
                        txtbank.Text = dspayment.Tables[0].Rows[0]["BankName"].ToString();
                        txtcheque.Text = dspayment.Tables[0].Rows[0]["chequeno"].ToString();
                        txtLotDetailId.Text = dspayment.Tables[0].Rows[0]["LotDetailId"].ToString();
                    }
                }

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

            if (ddlEName.SelectedValue == "Select Employee Name")
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

        protected void Add_Click(object sender, EventArgs e)
        {
            string chequeno = string.Empty;
            string bank = string.Empty;
            if (save.Text == "Save")
            {
                if (ddlPayment.SelectedValue == "0")
                {
                    chequeno = "0";
                    bank = "0";
                }
                else
                {
                    chequeno = txtcheque.Text;
                    bank = txtbank.Text;
                }
                DateTime pdate = DateTime.ParseExact(txtpdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime fdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime tdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string ID = Request.QueryString.Get("ID");

                string name = Request.QueryString.Get("name");

                int j = objBs.paymentinsert("tblDayBook", "tblPayment", "tblTransPayment", Convert.ToInt32(ddlEName.SelectedValue), Convert.ToInt32(txtPaymentNo.Text), pdate, Convert.ToInt32(ddlPayment.SelectedValue), txtNarration.Text, bank, chequeno, Convert.ToDecimal(txtAmount.Text), txtLotDetailId.Text, DpProcess.SelectedValue, name, ID);
                Response.Redirect("Paymentgrit.aspx");
            }
            else
            {
                //string DayBookID = Request.QueryString.Get("TransNo");

                if (ddlPayment.SelectedValue == "0")
                {
                    chequeno = "0";
                    bank = "0";
                }
                else
                {
                    chequeno = txtcheque.Text;
                    bank = txtbank.Text;
                }

                DateTime pdate = DateTime.ParseExact(txtpdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime fdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime tdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string ID = Request.QueryString.Get("ID");
                string name = Request.QueryString.Get("name");

                //////int j = objBs.Paymentupdate("tblDayBook", "tblPayment", "tblTransPayment", Convert.ToInt32(ddlEName.SelectedValue), Convert.ToInt32(txtPaymentNo.Text), pdate, Convert.ToInt32(ddlPayment.SelectedValue), txtNarration.Text, fdate, tdate, bank, chequeno, Convert.ToDouble(txtAmount.Text), TextBox1.Text, TextBox1.Text);
                Response.Redirect("Paymentgrit.aspx");
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Paymentgrit.aspx");
        }
    }
}
