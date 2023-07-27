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
    public partial class Statement : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string username = "";
        string userid = "";

        public Double damt = 0.0;
        public Double camt = 0.0;
        public Double dDiffamt = 0.0;
        public Double cDiffamt = 0.0;
        double OpBalance = 0.0;
        double dLedger = 0;
        double cLedger = 0;

        System.Globalization.CultureInfo Cul = new System.Globalization.CultureInfo("en-GB", true);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            username = Session["UserName"].ToString();
            userid = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                string Branch = ddloutlet.SelectedValue;
                txtfrmdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dst = objBs.GetBankLedgers(Convert.ToInt32(userid), 4, sTableName);
                if (dst.Tables[0].Rows.Count > 0)
                {
                    ddlBank.DataSource = dst.Tables[0];
                    ddlBank.DataTextField = "LedgerName";
                    ddlBank.DataValueField = "LedgerID";
                    ddlBank.DataBind();
                    ddlBank.Items.Insert(0, "Select Bank");
                }
                string super = Session["IsSuperAdmin"].ToString();
                //string sTableName = Session["User"].ToString();

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
                    ddloutlet.Enabled = false;
                }
            }
        }

        protected void btnreport_Click(object sender, EventArgs e)
        {
            
            lblMessage.Text = "Bank Statement Report From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            string Branch = ddloutlet.SelectedValue;
            //DateTime frmdate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime todate = Convert.ToDateTime(txttodate.Text);

            DateTime frmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int iLedgerID = Convert.ToInt32(ddlBank.SelectedValue);
            DataSet dst = objBs.generateRep(iLedgerID, frmdate, todate, Branch);
            DataSet dstt = new DataSet();
            double tot = 0;
            double tot1 = 0;

          
          

            if (dst != null)
            {
                if (dst.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add(new DataColumn("Date"));
                    dt.Columns.Add(new DataColumn("Particulars"));
                    //   dt.Columns.Add(new DataColumn("Commodity Code"));
                    dt.Columns.Add(new DataColumn("Type"));
                    dt.Columns.Add(new DataColumn("Narration"));
                    dt.Columns.Add(new DataColumn("Branch"));
                    dt.Columns.Add(new DataColumn("Debit"));
                    dt.Columns.Add(new DataColumn("Credit"));
                  

                    DataRow dr_export1 = dt.NewRow();
                    dr_export1["Type"] = "OP";
                    double opCr = objBs.getOpeningBalanceforbank(0, 0, iLedgerID, "credit", frmdate, Branch);
                    double opDr = objBs.getOpeningBalanceforbank(0, 0, iLedgerID, "debit", frmdate, Branch);
                    double netOp = 0;


                    if (opDr > opCr)
                    {
                        netOp = opDr - opCr;
                        //lblOBDR.Text = netOp.ToString("f2");
                        // lblOBCR.Text = "0.000";
                    }
                    else
                    {
                        netOp = opCr - opDr;
                        // lblOBDR.Text = "0.000";
                        //lblOBCR.Text = netOp.ToString("f2");
                    }


                    if (opDr > opCr)
                    {

                        dr_export1["Debit"] = netOp;
                        dr_export1["Credit"] = "0";
                    }
                    else
                    {


                        dr_export1["Debit"] = "0";
                        dr_export1["Credit"] = netOp;
                    }


                    
                    
                    dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in dst.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                      //  dr_export["SNo"] = serialno;
                        dr_export["Date"] = dr["Date"];
                        dr_export["Particulars"] = dr["Particulars"];
                        //  dr_export["Commodity Code"] = "";
                        dr_export["Type"] = dr["Type"];
                        dr_export["Branch"] = dr["Branch"];

                        //string aa = dr["BillDate"].ToString().ToUpper().Trim();
                        //string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                        //dr_export["Invoice Date"] = dtaa;

                        tot = tot + Convert.ToDouble(dr["Debit"]);

                        tot1 = tot1 + Convert.ToDouble(dr["Credit"]);

                        dr_export["Narration"] = dr["Narration"];
                        dr_export["Debit"] = Convert.ToDouble(dr["Debit"]).ToString("0.000");
                        dr_export["Credit"] = Convert.ToDouble(dr["Credit"]).ToString("0.000");
                        //   dr_export["Category"] = "";
                        dt.Rows.Add(dr_export);

                      //  serialno = serialno + 1;
                    }
                
                    dstt.Tables.Add(dt);
                    gvCash.DataSource = dstt;
                    gvCash.DataBind();
                    Calculate();
                    idt.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                    idt.Visible = false;
                }


            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvCash.ClientID + "', 100, 150 , 40 ,true); </script>", false);
            }

            //gvCash.DataSource = dst;
            //gvCash.DataBind();
            
            idt.Visible = true;
        }

        protected void gvCash_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    if ((DataBinder.Eval(e.Row.DataItem, "Debit")) == "")
                        debit = 0;

                    if ((DataBinder.Eval(e.Row.DataItem, "Credit")) == "")
                        credit = 0;

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
                        lblCreditDiff.Text = "0.000";
                      
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
                        lblDebitDiff.Text = "0.000";
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
                    int Columnscount = gvCash.Columns.Count;
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
                    this.gvCash.Controls[0].Controls.Add(row);
                    //  e.Row.Cells[2].Text
                }
          
                //double debit = 0;
                //double credit = 0;
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                //   if (e.Row.Cells[1].Text == "Grand Total:")
                //   {
                //       e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                //       e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;
                //       e.Row.Cells[6].ForeColor = System.Drawing.Color.Blue;
                //      // e.Row.BackColor = System.Drawing.Color.Blue;
                   
                //   }
                //   else if (e.Row.Cells[1].Text == "Balance:")
                //   {
                //       e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                //      // e.Row.BackColor = System.Drawing.Color.Red;
                //       e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                //       e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                //   }


                   ////if (e.Row.RowIndex == 0)
                   ////    e.Row.Style.Add("height", "5px");


                //    debit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                //    credit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                //    damt = damt + debit;
                //    camt = camt + credit;

                //    lblDebitSum.Text = damt.ToString("f2");
                //    lblCreditSum.Text = camt.ToString("f2");

                //    dDiffamt = damt - camt;
                //    cDiffamt = camt - damt;

                //    e.Row.Cells[5].Text = debit.ToString("f2");
                //    e.Row.Cells[6].Text = credit.ToString("f2");

                //}
                //else
                //{
                //    if (dDiffamt > 0)
                //    {
                //        lblDebitDiff.Text = dDiffamt.ToString("f2");
                //        lblCreditDiff.Text = "0.000";
                //    }

                //    if (cDiffamt > 0)
                //    {
                //        lblDebitDiff.Text = "0.000";
                //        lblCreditDiff.Text = cDiffamt.ToString("f2");
                //    }

                //    if (cDiffamt == 0 && dDiffamt == 0)
                //    {
                //        lblDebitDiff.Text = "0.000";
                //        lblCreditDiff.Text = "0.000";
                //    }
                
        }

        protected void Calculate()
        {
            string LedgerID = string.Empty;
            int iLedgerID = Convert.ToInt32(ddlBank.SelectedValue);
            double opCr = 0.0;
            double opDr = 0.0;
            double netOp = 0.0;
            double cbDr = 0.000;
            double cbCr = 0.000;
            //double opCr = 0.0;
            //double opDr = 0.0;
            //double netOp = 0.0;
            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            DateTime startDate = DateTime.Parse(txtfrmdate.Text.Trim(), Cul, System.Globalization.DateTimeStyles.NoCurrentDateDefault);// Convert.ToDateTime(txtfrmdate.Text);
            string Branch = ddloutlet.SelectedValue;

            opCr = objBs.getOpeningBalanceforbank(0, 0, iLedgerID, "credit", startDate, Branch);
            opDr = objBs.getOpeningBalanceforbank(0, 0, iLedgerID, "debit", startDate, Branch);

            cbDr = opDr + Convert.ToDouble(lblDebitDiff.Text);
            cbCr = opCr + Convert.ToDouble(lblCreditDiff.Text);

            if (opDr > opCr)
            {
                netOp = opDr - opCr;
                lblOBDR.Text = netOp.ToString("f2");
                lblOBCR.Text = "0.000";
            }
            else
            {
                netOp = opCr - opDr;
                lblOBDR.Text = "0.000";
                lblOBCR.Text = netOp.ToString("f2");
            }
            if (cbDr > cbCr)
            {

                cbDr = cbDr - cbCr;
                lblClosDr.Text = cbDr.ToString("f2");
                lblClosCr.Text = "0.000";
            }
            else
            {
                cbCr = cbCr - cbDr;
                lblClosCr.Text = cbCr.ToString("f2");
                lblClosDr.Text = "0.000";
            }

            //if (opDr > opCr)
            //{
            //    netOp = opDr - opCr;
            //    lblOBDR.Text = netOp.ToString("f2");
            //    lblOBCR.Text = "0.000";
            //    if (damt > camt)
            //    {
            //        dDiffamt = netOp + (damt - camt);
            //        cDiffamt = 0;
            //    }
            //    else
            //    {
            //        if (((camt - damt) - netOp) > 0)
            //        {
            //            cDiffamt = (camt - damt) - netOp;
            //            dDiffamt = 0;
            //        }
            //        else
            //        {
            //            dDiffamt = Math.Abs((camt - damt) - netOp);
            //            cDiffamt = 0;
            //        }
            //    }

            //    lblOBCR.Text = "0.000";
            //    lblOBDR.Text = netOp.ToString("f2");

            //}
            //else
            //{
            //    netOp = opCr - opDr;
            //    if (damt > camt)
            //    {
            //        if (((damt - camt) - netOp) > 0)
            //        {
            //            dDiffamt = (damt - camt) - netOp;
            //            cDiffamt = 0;
            //        }
            //        else
            //        {
            //            cDiffamt = Math.Abs((damt - camt) - netOp);
            //            dDiffamt = 0;
            //        }

            //    }
            //    else
            //    {
            //        cDiffamt = netOp + (camt - damt);
            //        dDiffamt = 0;
            //    }
            //    lblOBDR.Text = "0.000";
            //    lblOBCR.Text = netOp.ToString("f2");

            //}

            //if (dDiffamt > 0)
            //{
            //    lblDebitDiff.Text = dDiffamt.ToString("f2");
            //    lblCreditDiff.Text = "0.000";
            //}
            //if (cDiffamt > 0)
            //{
            //    lblDebitDiff.Text = "0.000";
            //    lblCreditDiff.Text = cDiffamt.ToString("f2");

            //}


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            gvCash.PagerSettings.Visible = false;
            //  GridView1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvCash.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            //  sb.Append("test1");
            sb.Append("printWin.document.write(\"");
            sb.Append("ARK </br>");
            sb.Append("Bank Statement </br>");
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
            gvCash.PagerSettings.Visible = true;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}