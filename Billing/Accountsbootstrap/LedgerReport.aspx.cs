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
    public partial class LedgerReport : System.Web.UI.Page
    {
        decimal totalDebit = 0;
        decimal totalCredit = 0;
        int totalItems = 0;
        BSClass objBs = new BSClass();
        string sTableName = "";
        public Double damt = 0.0;
        public Double camt = 0.0;
        public Double dDiffamt = 0.0;
        public Double cDiffamt = 0.0;
        double OpBalance = 0.0;
        double dLedger = 0;
        double cLedger = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();

          

            if (!IsPostBack)
            {
                DataSet dLed = objBs.GetLedgers11(sTableName);
                DataView dds = dLed.Tables[0].DefaultView;
                dds.Sort = "ledgername asc";

                ddLedger.DataSource = dds;
                ddLedger.DataValueField = "LedgerId";
                ddLedger.DataTextField = "ledgername";
                ddLedger.DataBind();
           
                //DataSet ds = objBs.selectGroups();
                //ddlGroup.DataSource = ds;
                //ddlGroup.DataValueField = "GroupId";
                //ddlGroup.DataTextField = "GroupName";
                //ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, "Select");

                DataSet dst = objBs.GetHeading();
                DropHeading.DataSource = dst;
                DropHeading.DataValueField = "HeadingId";
                DropHeading.DataTextField = "HeadingName";
                DropHeading.DataBind();
                DropHeading.Items.Insert(0, "Select");

                //DataSet dLed = objBs.ledgeridretrive();
                //ddLedger.DataSource = dLed.Tables[0];
                //ddLedger.DataValueField = "LedgerId";
                //ddLedger.DataTextField = "LedgerName";
                //ddLedger.DataBind();
                ddLedger.Items.Insert(0, "Select");

                txtfrmdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");


                string super = Session["IsSuperAdmin"].ToString();
                // string sTableName = Session["User"].ToString();

                if (super == "1")
                {
                    ddloutlet.Enabled = true;
                    DataSet dsbranchto = objBs.Branchto();
                    ddloutlet.DataSource = dsbranchto.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    ddloutlet.Items.Insert(0, "All");
                }
                else
                {
                    DataSet dsbranch = new DataSet();
                    dsbranch = objBs.Branchfrom(sTableName);
                    ddloutlet.DataSource = dsbranch.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    ddloutlet.Enabled = true;
                }

                //DataSet dLedger = objBs.getAllLedger();
                //gvdaybook.DataSource = dLedger;
                //gvdaybook.DataBind();
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void btnfind_Click(object sender, EventArgs e)
        {
        
        }

        protected void gvdaybook_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
                double debit = 0;
                double credit = 0;

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    int iLedgerID = 0;
                    int iGroupID = 0;
                    int iAccHeadingID = 0;

                    string sLedgerName = string.Empty;
                    string sType = string.Empty;

                    double opSalesReturn = 0;
                    double opPurchaseReturn = 0;
                    int ledgerID = 0;
                    double opCr = 0.0;
                    double opDr = 0.0;
                    //DateTime startDate;

                    //startDate = Convert.ToDateTime(txtfrmdate.Text);

                 //   DateTime stdt = Convert.ToDateTime(txtfrmdate.Text);
                 //   DateTime etdt = Convert.ToDateTime(txttodate.Text);
                    //iGroupID = Convert.ToInt32(ddlGroup.SelectedValue);
                    //ledgerID = Convert.ToInt32(ddLedger.SelectedValue);
                    //string Branch = ddloutlet.SelectedValue;
                    //iAccHeadingID = Convert.ToInt32(DropHeading.SelectedValue);

                    //DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    //opCr = objBs.getOpeningBalance(iAccHeadingID, iGroupID, ledgerID, "credit", startDate, Branch);
                    //opDr = objBs.getOpeningBalance(iAccHeadingID, iGroupID, ledgerID, "debit", startDate, Branch);
                    
                    //OpBalance = opDr - opCr;
                    //if (OpBalance >= 0)
                    //{
                    //    dLedger = dLedger + OpBalance;
                    //}
                    //else
                    //{
                    //    cLedger = cLedger + OpBalance;
                    //}

                }
               
                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    debit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                    credit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                    damt = damt + debit;
                    camt = camt + credit;

                    lblDebitSum.Text = damt.ToString("f2");  
                    lblCreditSum.Text = camt.ToString("f2");

                    dDiffamt = damt - camt; 
                    cDiffamt = camt - damt; 
               
                    e.Row.Cells[5].Text = debit.ToString("f2");
                    e.Row.Cells[6].Text = credit.ToString("f2");
               
                    Label lblBal = (Label)e.Row.FindControl("lblBalance");
               
                    if (dDiffamt >= 0)
                    {
                        lblDebitDiff.Text = dDiffamt.ToString("f2"); 
                        lblCreditDiff.Text = "0.00";
                      
                        dDiffamt = dDiffamt + OpBalance;
                        if (dDiffamt >= 0)
                        {
                            lblBal.Text = dDiffamt.ToString("f2") + " Dr";
                            lblBal.ForeColor = System.Drawing.Color.Blue;
                        }
                        else
                        {
                            lblBal.Text = Math.Abs(dDiffamt).ToString("f2") + " Cr";
                            lblBal.ForeColor = System.Drawing.Color.Blue;
                        }
                  

                    }
                    if (cDiffamt > 0)
                    {
                        lblDebitDiff.Text = "0.00";
                        lblCreditDiff.Text = cDiffamt.ToString("f2"); 
                    
                        cDiffamt = cDiffamt - OpBalance;
                        if (cDiffamt > 0)
                        {
                            lblBal.Text = cDiffamt.ToString("f2") + " Cr";
                            lblBal.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lblBal.Text = Math.Abs(cDiffamt).ToString("f2") + " Dr";
                            lblBal.ForeColor = System.Drawing.Color.Blue;
                        }
                       
                    }

                }
                ////else if (e.Row.RowType == DataControlRowType.Footer)
                ////{
                ////    e.Row.Cells[5].Text = damt.ToString("f2");
                ////    e.Row.Cells[6].Text = camt.ToString("f2");
                ////}
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[3].Text = "Grand Total:";
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[5].Text = damt.ToString("f2");
                    e.Row.Cells[6].Text = camt.ToString("f2");
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Blue;
                    double amnt = damt - camt;

                    int RowIndex = e.Row.RowIndex;
                    int DataItemIndex = e.Row.DataItemIndex;
                    int Columnscount = gvdaybook.Columns.Count;
                    GridViewRow row = new GridViewRow(RowIndex, DataItemIndex, DataControlRowType.Footer, DataControlRowState.Normal);
                    for (int i = 0; i < 6; i++)
                    {
                        TableCell tablecell = new TableCell();
                        //   tablecell.Text = "dynamic footer" + i;
                        if (i == 3)
                        {
                            tablecell.Text = "Current Balance:";
                            tablecell.ForeColor = System.Drawing.Color.Red;
                        }

                        if (damt > camt)
                        {
                            if (i == 4)
                            {

                                tablecell.Text = lblDebitDiff.Text;
                                tablecell.ForeColor = System.Drawing.Color.Red;
                                tablecell.HorizontalAlign = HorizontalAlign.Right;
                            }
                        }
                        else
                        {
                            if (i == 5)
                            {

                                tablecell.Text = lblCreditDiff.Text;
                                tablecell.ForeColor = System.Drawing.Color.Red;
                            }

                        }




                        row.Cells.Add(tablecell);
                    }
                    this.gvdaybook.Controls[0].Controls.Add(row);
                    //  e.Row.Cells[2].Text
                }
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Ledger Report From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            DataSet dSeaarch = new DataSet();
            string Branch = ddloutlet.SelectedValue;
            int GroupID = Convert.ToInt32(ddlGroup.SelectedValue);

            int LedgerID = Convert.ToInt32(ddLedger.SelectedValue);

            //DateTime frmdate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime todate = Convert.ToDateTime(txttodate.Text);

            DateTime frmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int Heading = Convert.ToInt32(DropHeading.SelectedValue);

            dSeaarch = objBs.ReportLedger(Heading, GroupID, LedgerID, frmdate, todate, Branch);

            DataView dds = dSeaarch.Tables[0].DefaultView;
            dds.Sort = "Date asc";

            gvdaybook.DataSource = dds;
            gvdaybook.DataBind();
            dd.Visible = true;
            Calculate();
        }

        protected void DropHeading_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet ds = objBs.getselectHeading( Convert.ToInt32(DropHeading.SelectedValue));
            //ddlGroup.DataSource = ds;
            //ddlGroup.DataValueField = "GroupId";
            //ddlGroup.DataTextField = "GroupName";
            //ddlGroup.DataBind();
            //ddlGroup.Items.Insert(0, "Select");

            //DataSet dLed = objBs.GetLedgers(0, Convert.ToInt32(ddlGroup.SelectedValue));
            //ddLedger.DataSource = dLed.Tables[0];
            //ddLedger.DataValueField = "LedgerId";
            //ddLedger.DataTextField = "LedgerName";
            //ddLedger.DataBind();
            //ddLedger.Items.Insert(0, "Select");
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet dLed = objBs.GetLedgers1(0, Convert.ToInt32(ddlGroup.SelectedValue), sTableName);
            //ddLedger.DataSource = dLed.Tables[0];
            //ddLedger.DataValueField = "LedgerId";
            //ddLedger.DataTextField = "LedgerName";
            //ddLedger.DataBind();
            //ddLedger.Items.Insert(0, "Select");
            
        }


        protected void Calculate()
        {
            //DateTime startDate;
            double opCr = 0.0;
            double opDr = 0.0;
            double netOp = 0.0;
            double cbDr = 0.00;
            double cbCr = 0.00;
            int ledgerID = 0;
            int GroupID = 0;
            int HeadingID = 0;

            //startDate = Convert.ToDateTime(txtfrmdate.Text);

          //  DateTime stdt = Convert.ToDateTime(txtfrmdate.Text);
          //  DateTime etdt = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            GroupID = Convert.ToInt32(ddlGroup.SelectedValue);
            ledgerID = Convert.ToInt32(ddLedger.SelectedValue);
            string Branch = ddloutlet.SelectedValue;

            HeadingID = Convert.ToInt32(DropHeading.SelectedValue);
            string sLedgerName = string.Empty;

            opCr = objBs.getOpeningBalance(HeadingID, GroupID, ledgerID, "credit", startDate, Branch);
            opDr = objBs.getOpeningBalance(HeadingID, GroupID, ledgerID, "debit", startDate, Branch);
           
            cbDr = opDr + Convert.ToDouble(lblDebitDiff.Text);
            cbCr = opCr + Convert.ToDouble(lblCreditDiff.Text);

            if (opDr > opCr)
            {
                netOp = opDr - opCr;
                lblOBDR.Text = netOp.ToString("f2");
                lblOBCR.Text = "0.00";
            }
            else
            {
                netOp = opCr - opDr;
                lblOBDR.Text = "0.00";
                lblOBCR.Text = netOp.ToString("f2");
            }
            if (cbDr > cbCr)
            {

                cbDr = cbDr - cbCr;
                lblClosDr.Text = cbDr.ToString("f2");
                lblClosCr.Text = "0.00";
            }
            else
            {
                cbCr = cbCr - cbDr;
                lblClosCr.Text = cbCr.ToString("f2");
                lblClosDr.Text = "0.00";
            }
        }

        protected void rbBranch1_CheckedChanged(object sender, EventArgs e)
        {
      
        }

        protected void rbBranch2_CheckedChanged(object sender, EventArgs e)
        {
      
        }

        protected void rbBranch3_CheckedChanged(object sender, EventArgs e)
        {
       
        }

        protected void rbBranch4_CheckedChanged(object sender, EventArgs e)
        {
     
        }

        protected void ddLedger_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet get_ledger = objBs.get_ledgerdetails(ddLedger.SelectedValue);
            if (get_ledger.Tables[0].Rows.Count > 0)
            {
                DataSet dst = objBs.getselectHeadingall();
                ddlGroup.DataSource = dst;
                ddlGroup.DataValueField = "GroupId";
                ddlGroup.DataTextField = "GroupName";
                ddlGroup.DataBind();
                ddlGroup.SelectedValue = get_ledger.Tables[0].Rows[0]["GroupID"].ToString();


                DataSet dst1 = objBs.getselectHeadingallbyheading(ddlGroup.SelectedValue);
                DropHeading.DataSource = dst1;
                DropHeading.DataValueField = "HeadingId";
                DropHeading.DataTextField = "HeadingId";
                DropHeading.DataBind();
                DropHeading.SelectedValue = dst1.Tables[0].Rows[0]["HeadingId"].ToString();


               

            }

           
           

           

           
          


           


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
                gvdaybook.PagerSettings.Visible = false;
            //  GridView1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvdaybook.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            //  sb.Append("test1");
            sb.Append("printWin.document.write(\"");
            sb.Append("ARK  </br>");
            sb.Append("Ledger Report </br>");
            sb.Append(" </br></br>");
            sb.Append("</br>");

            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvdaybook.PagerSettings.Visible = true;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        }
    }
