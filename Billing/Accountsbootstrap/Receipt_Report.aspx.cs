using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using CommonLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class Receipt_Report : System.Web.UI.Page
    {
        BusinessLayer.ReceiptReport objbs = new BusinessLayer.ReceiptReport();
        BSClass objBs = new BSClass();
        string sTableName = "";
        double amt;
        double cashsalesamt;
        double receiptsalesamt;
        double ddreceiptamt;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
                txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                sTableName = Session["User"].ToString();
                //DataSet ds = objbs.getReceiptbyDateWise(sTableName, txtFromDate.Text, txtToDate.Text);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    gridPurchase.DataSource = ds;
                //    gridPurchase.DataBind();
                //}
                //else
                //{
                //    gridPurchase.DataSource = null;

                //}
                string super = Session["IsSuperAdmin"].ToString();


                if (super == "1")
                {
                    ddloutlet.Enabled = true;
                    DataSet dsbranchto = objBs.Branchto();
                    ddloutlet.DataSource = dsbranchto.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    //ddloutlet.Items.Insert(0, "All");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Receipt Report From  '" + txtFromDate.Text + "'  To  '" + txtToDate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            sTableName = Session["User"].ToString();
            
            DateTime frmdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = objbs.getReceiptbyDateWise(ddloutlet.SelectedValue, frmdate, todate);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ReceiptNo"].ToString() != "")
                    {
                        gridPurchase.DataSource = ds;
                        gridPurchase.DataBind();
                    }
                    else
                    {
                        gridPurchase.DataSource = null;
                        gridPurchase.DataBind();

                    }
                }
                else
                {
                    gridPurchase.DataSource = null;
                    gridPurchase.DataBind();

                }
            }
            else
            {
                gridPurchase.DataSource = null;
                gridPurchase.DataBind();

            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            sTableName = Session["User"].ToString();
            GridView gvsales = new GridView();

            DateTime frmdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            gvsales.DataSource = objbs.getReceiptbyDateWise(ddloutlet.SelectedValue, frmdate, todate);
            gvsales.AutoGenerateColumns = false;

            BoundField test = new BoundField();

            test.DataField = "ReceiptNo";
            test.HeaderText = "Receipt No";
            gvsales.Columns.Add(test);
            BoundField test1 = new BoundField();

            test1.DataField = "DaybookId";
            test1.HeaderText = "Trans No";
            gvsales.Columns.Add(test1);
            BoundField test2 = new BoundField();

            test2.DataField = "LedgerType";
            test2.HeaderText = "Ledger Type";
            gvsales.Columns.Add(test2);
            BoundField testt = new BoundField();

            testt.DataField = "LedgerName";
            testt.HeaderText = "Ledger Name";
            gvsales.Columns.Add(testt);
            BoundField test3 = new BoundField();

            test3.DataField = "ReceiptDate";
            test3.HeaderText = "Date";
            gvsales.Columns.Add(test3);
            BoundField test4 = new BoundField();

            test4.DataField = "Payment_Mode";
            test4.HeaderText = "Payment Mode";
            gvsales.Columns.Add(test4);
            BoundField test7 = new BoundField();

            test7.DataField = "BankName";
            test7.HeaderText = "BankName";
            gvsales.Columns.Add(test7);
            BoundField test71 = new BoundField();

            test71.DataField = "Amount";
            test71.HeaderText = "Amount";
            gvsales.Columns.Add(test71);
            BoundField test5 = new BoundField();

            test5.DataField = "Chequeno";
            test5.HeaderText = "Cheque NO";
            gvsales.Columns.Add(test5);
            BoundField test10 = new BoundField();

            test10.DataField = "Narration";
            test10.HeaderText = "Narration";
            gvsales.Columns.Add(test10);




            gvsales.HeaderStyle.BackColor = System.Drawing.Color.BurlyWood;
            gvsales.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition",
                "attachment;filename=ReceiptReport" + "-" + DateTime.Now.ToString("MM/dd/yyyy") + ".xls");
            Response.ContentType = "applicatio/excel";
            StringWriter sw = new StringWriter(); ;
            HtmlTextWriter htm = new HtmlTextWriter(sw);
            gvsales.RenderControl(htm);
            Response.Write(sw.ToString());
            Response.End();

        }

        protected void gridPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double total;
            double totalcash;
            double totalreceipt=0;
            double totaldd;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((DataBinder.Eval(e.Row.DataItem, "Amount").ToString()) == "")
                {
                }
                else
                {
                    total = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                    amt = amt + total;
                }


                if ((DataBinder.Eval(e.Row.DataItem, "LedgerName").ToString()) == "Direct Sales")
                {
                    totalcash = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                    cashsalesamt = cashsalesamt + totalcash;
                }
                else if (((DataBinder.Eval(e.Row.DataItem, "Payment_Mode").ToString()) == "Cash"))
                {
                    totalreceipt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                    receiptsalesamt = receiptsalesamt + totalreceipt;                    
                }

                if (((DataBinder.Eval(e.Row.DataItem, "Payment_Mode").ToString()) == "Cheque") || ((DataBinder.Eval(e.Row.DataItem, "Payment_Mode").ToString()) == "OnlinePayment") || ((DataBinder.Eval(e.Row.DataItem, "Payment_Mode").ToString()) == "Card"))
                {
                    totaldd = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                    ddreceiptamt = ddreceiptamt + totaldd;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[5].Text = "Grand Total:";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[8].Text = amt.ToString("f2");

                e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;

                lblCashsalestot.Text = cashsalesamt.ToString("f2");
                lblreceipttot.Text = receiptsalesamt.ToString("f2");
                lblreceiptBanktot.Text = ddreceiptamt.ToString("f2"); 

                int RowIndex = e.Row.RowIndex;
                int DataItemIndex = e.Row.DataItemIndex;
                int Columnscount = gridPurchase.Columns.Count;
                GridViewRow row = new GridViewRow(RowIndex, DataItemIndex, DataControlRowType.Footer, DataControlRowState.Normal);
                for (int i = 0; i < 6; i++)
                {
                    TableCell tablecell = new TableCell();
                    //   tablecell.Text = "dynamic footer" + i;
                    if (i == 3)
                    {
                        tablecell.Text = "Total:";
                        tablecell.ForeColor = System.Drawing.Color.Red;
                    }

                    if (i == 4)
                    {

                        tablecell.Text = lblamt.Text;
                        tablecell.ForeColor = System.Drawing.Color.Red;
                        tablecell.HorizontalAlign = HorizontalAlign.Right;
                    }
                }


            }
        }

        protected void gridPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            sTableName = Session["User"].ToString();


            DateTime frmdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = objbs.getReceiptbyDateWise(ddloutlet.SelectedValue, frmdate, todate);
            if(ds!=null)
            {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridPurchase.DataSource = ds;
                gridPurchase.PageIndex = e.NewPageIndex;
                gridPurchase.DataBind();
            }
            else
            {
                gridPurchase.DataSource = null;

            }
        }
        else
            {
                gridPurchase.DataSource = null;
                gridPurchase.DataBind();

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            gridPurchase.PagerSettings.Visible = false;
            //  GridView1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridPurchase.RenderControl(hw);
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
            sb.Append("Receipt Report </br>");
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
            gridPurchase.PagerSettings.Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}