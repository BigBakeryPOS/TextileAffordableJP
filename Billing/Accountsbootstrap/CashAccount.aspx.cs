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
    public partial class CashAccount : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        public Double damt = 0.0;
        public Double camt = 0.0;
        public Double dDiffamt = 0.0;
        public Double cDiffamt = 0.0;
        string sTableName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
      
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            if (!IsPostBack)
            {
                string Branch = ddloutlet.SelectedValue;
                txtfrmdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //DateTime frmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //txtfrmdate.Text = frmdate.ToString("dd/MM/yyyy");
                //txttodate.Text = todate.ToString("dd/MM/yyyy");
                string super = Session["IsSuperAdmin"].ToString();
                 sTableName = Session["User"].ToString();

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
            lblMessage.Text = "Cash Account Report From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            string Branch = ddloutlet.SelectedValue;
            //DateTime frmdate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime todate = Convert.ToDateTime(txttodate.Text);

            DateTime frmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dst = objBs.generateReportCash(frmdate, todate, Branch);

            gvCash.DataSource = dst;
            gvCash.DataBind();
            
            idt.Visible = true;
            Calculate();
            //gvCash.DataBind();
        }

        protected void gvCash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double debit = 0;
            double credit = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                debit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                credit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                damt = damt + debit;
                camt = camt + credit;

                lblDebitSum.Text = damt.ToString("f2"); 
                lblCreditSum.Text = camt.ToString("f2");
              //  e.Row.Cells[1].Text = "Grand Total:";
                e.Row.Cells[5].Text = debit.ToString("f2");
                e.Row.Cells[6].Text = credit.ToString("f2");
            }
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
                        tablecell.Text = "Balance:";
                        tablecell.ForeColor = System.Drawing.Color.Red;
                    }

                    if (damt > amnt)
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
           
        }

        protected void Calculate()
        {
            string LedgerID = string.Empty;
            int iLedgerID = 1;
            
            double opCr = 0.0;
            double opDr = 0.0;
            double netOp = 0.0;
            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string Branch = ddloutlet.SelectedValue;

            opCr = objBs.getOpeningBalancecashaccount(0, 0, Convert.ToInt32(0), "credit", startDate, Branch);
            opDr = objBs.getOpeningBalancecashaccount(0, 0, Convert.ToInt32(0), "debit", startDate, Branch);

            if (opDr > opCr)
            {
                netOp = opDr - opCr;
                lblOBDR.Text = netOp.ToString("f2");
                lblOBCR.Text = "0.000";
                if (damt > camt)
                {
                    dDiffamt = netOp + (damt - camt);
                    cDiffamt = 0;
                }
                else
                {
                    if (((camt - damt) - netOp) > 0)
                    {
                        cDiffamt = (camt - damt) - netOp;
                        dDiffamt = 0;
                    }
                    else
                    {
                        dDiffamt = Math.Abs((camt - damt) - netOp);
                        cDiffamt = 0;
                    }
                }

                lblOBCR.Text = "0.000";
                lblOBDR.Text = netOp.ToString("f2");

            }
            else
            {
                netOp = opCr - opDr;
                if (damt > camt)
                {
                    if (((damt - camt) - netOp) > 0)
                    {
                        dDiffamt = (damt - camt) - netOp;
                        cDiffamt = 0;
                    }
                    else
                    {
                        cDiffamt = Math.Abs((damt - camt) - netOp);
                        dDiffamt = 0;
                    }

                }
                else
                {
                    cDiffamt = netOp + (camt - damt);
                    dDiffamt = 0;
                }
                lblOBDR.Text = "0.000";
                lblOBCR.Text = netOp.ToString("f2");

            }

            if (dDiffamt > 0)
            {
                lblDebitDiff.Text = dDiffamt.ToString("f2");
                lblCreditDiff.Text = "0.000";
            }
            if (cDiffamt > 0)
            {
                lblDebitDiff.Text = "0.000";
                lblCreditDiff.Text = cDiffamt.ToString("f2");

            }
            

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
            sb.Append("Cash Account Report </br>");
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