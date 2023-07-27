using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class GSTReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        public Double damt = 0.0;
        public Double camt = 0.0;
        public Double dDiffamt = 0.0;
        public Double cDiffamt = 0.0;
        string sTableName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //string sTableName = string.Empty;
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
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
                //ddloutlet.Enabled = true;
                //DataSet dsbranchto = objBs.Branchto();
                //ddloutlet.DataSource = dsbranchto.Tables[0];
                //ddloutlet.DataTextField = "branchName";
                //ddloutlet.DataValueField = "branchcode";
                //ddloutlet.DataBind();
                //ddloutlet.Items.Insert(0, "All");

                string Branch = ddloutlet.SelectedValue;
                txtfrmdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnreport_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "GST REPORT  From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            string condi = ddloutlet.SelectedValue;
            string condi1 = "";

            DateTime frmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string condi = "";

            //condi = "";

            DataSet ds = new DataSet();
            ds = objBs.gstreport(frmdate, todate, condi);

            Double serialno = 1;
            DataSet dstt = new DataSet();
            double tot = 0;
            double tot1 = 0;
            double tot2 = 0;

            double totSG = 0;
            double totCG = 0;
            double totIG = 0;
            double totgst = 0;
            double ptotgst = 0;

            if (ds != null)
            {
                idt.Visible = true;
                DataView dvEmp = ds.Tables[0].DefaultView;
                dvEmp.Sort = "Billdate ASC";
                // dtt.Sort = "Vendorid ASC";
                gvCash.DataSource = dvEmp;
                gvCash.DataBind();

                // gvCash.DataSource = ds;
                // gvCash.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                idt.Visible = false;
            }


        }

        protected void gvCash_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[1];
            arg = e.CommandArgument.ToString().Split(',');

            if (e.CommandName == "print")
            {
                if (arg[0] == "Sales")
                {
                    //Response.Redirect("InvoicePrint_Auditor.aspx?iSalesID=" + arg[1]);

                    if (sTableName == "CO4")
                    {
                        Response.Redirect("Print_Sales_InvoiceNormalNEW.aspx?iSalesID=" + arg[1]);
                    }
                    else
                    {
                        Response.Redirect("InvoicePrint_Auditor.aspx?iSalesID=" + arg[1]);
                    }
                }

                if (arg[0] == "Sales Quotation")
                {
                    Response.Redirect("SalesQuotationPrint_Auditor.aspx?iSalesID=" + arg[1]);
                }

                if (arg[0] == "Purchase")
                {
                    Response.Redirect("Print_Purchase.aspx?DC_NO=" + arg[1]);
                }
            }

        }

        protected void gvCash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double ctotal;
            double dtotal;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ctotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                camt = camt + ctotal;

                dtotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                damt = damt + dtotal;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = " Total:";
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[6].Text = damt.ToString("f2");

                e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[7].Text = camt.ToString("f2");

                e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                double amnt = damt - camt;

                int RowIndex = e.Row.RowIndex;
                int DataItemIndex = e.Row.DataItemIndex;
                int Columnscount = gvCash.Columns.Count;
                GridViewRow row = new GridViewRow(RowIndex, DataItemIndex, DataControlRowType.Footer, DataControlRowState.Normal);
                for (int i = 0; i < 6; i++)
                {
                    TableCell tablecell = new TableCell();
                    //   tablecell.Text = "dynamic footer" + i;
                    if (i == 2)
                    {
                        tablecell.Text = "Difference:";
                        tablecell.ForeColor = System.Drawing.Color.Red;
                    }

                    if (damt > camt)
                    {
                        if (i == 6)
                        {

                            tablecell.Text = amnt.ToString("0.00");
                            tablecell.ForeColor = System.Drawing.Color.Red;
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                    else
                    {
                        if (i == 4)
                        {

                            tablecell.Text = (-amnt).ToString("0.00");
                            tablecell.ForeColor = System.Drawing.Color.Red;
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                        }

                    }
                    row.Cells.Add(tablecell);
                }
                this.gvCash.Controls[0].Controls.Add(row);
            }
        }

        protected void Calculate()
        {

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
            sb.Append("" + Session["Cname"].ToString() + " </br>");
            sb.Append("Vat Annuxere Report </br>");
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
