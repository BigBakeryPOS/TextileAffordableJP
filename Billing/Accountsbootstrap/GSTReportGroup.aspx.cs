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
    public partial class GSTReportGroup : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        public Double damt = 0.0;
        public Double camt = 0.0;
        public Double dDiffamt = 0.0;
        public Double cDiffamt = 0.0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sTableName = string.Empty;
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
            DataSet dsFinalnew = new DataSet();

            DataSet dsFinal = new DataSet();
            dsFinal = objBs.gstgroupreport(frmdate, todate, condi);

            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.ColumnName = "SalesID";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "BillNo";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "CustomerName";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "With GST";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Without GST";
            dt.Columns.Add(dc);

            if (dsFinal != null)
            {
                if (dsFinal.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsFinal.Tables[0].Rows.Count; i++)
                    {
                        DataSet dsTrans0 = objBs.gstzerogroupreport(dsFinal.Tables[0].Rows[i]["SalesID"].ToString(), condi);
                        DataSet dsTrans1 = objBs.gstgreaterzerogroupreport(dsFinal.Tables[0].Rows[i]["SalesID"].ToString(), condi);
                        

                        DataRow dr;
                        dr = dt.NewRow();
                        dr["SalesID"] = dsFinal.Tables[0].Rows[i]["SalesID"].ToString();
                        dr["BillNo"] = dsFinal.Tables[0].Rows[i]["fullbillno"].ToString();
                        dr["CustomerName"] = dsFinal.Tables[0].Rows[i]["LedgerName"].ToString();
                        if (dsTrans0.Tables[0].Rows.Count > 0)
                        {
                            dr["Without GST"] = Convert.ToDouble(dsTrans0.Tables[0].Rows[0]["Amt"]).ToString("0.00");
                        }
                        else
                        {
                            dr["Without GST"] = "0";
                        }
                        if (dsTrans1.Tables[0].Rows.Count > 0)
                        {
                            dr["With GST"] = Convert.ToDouble(dsTrans1.Tables[0].Rows[0]["Amt"]).ToString("0.00");
                        }
                        else
                        {
                            dr["With GST"] = "0";
                        }
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    
                }

            }

            dsFinalnew.Tables.Add(dt);
            if (dsFinalnew != null)
            {
                gvCash.DataSource = dsFinalnew;
                gvCash.DataBind();

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                idt.Visible = false;
            }


        }

        protected void gvCash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //double ctotal;
            //double dtotal;

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ctotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
            //    camt = camt + ctotal;

            //    dtotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
            //    damt = damt + dtotal;

            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[2].Text = " Total:";
            //    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            //    e.Row.Cells[3].Text = damt.ToString("f2");

            //    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;

            //    e.Row.Cells[4].Text = camt.ToString("f2");

            //    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            //    double amnt = damt - camt;

            //    int RowIndex = e.Row.RowIndex;
            //    int DataItemIndex = e.Row.DataItemIndex;
            //    int Columnscount = gvCash.Columns.Count;
            //    GridViewRow row = new GridViewRow(RowIndex, DataItemIndex, DataControlRowType.Footer, DataControlRowState.Normal);
            //    for (int i = 0; i < 6; i++)
            //    {
            //        TableCell tablecell = new TableCell();
            //        //   tablecell.Text = "dynamic footer" + i;
            //        if (i == 2)
            //        {
            //            tablecell.Text = "Difference:";
            //            tablecell.ForeColor = System.Drawing.Color.Red;
            //        }

            //        if (damt > camt)
            //        {
            //            if (i == 3)
            //            {

            //                tablecell.Text = amnt.ToString("0.00");
            //                tablecell.ForeColor = System.Drawing.Color.Red;
            //                tablecell.HorizontalAlign = HorizontalAlign.Right;
            //            }
            //        }
            //        else
            //        {
            //            if (i == 4)
            //            {

            //                tablecell.Text = (-amnt).ToString("0.00");
            //                tablecell.ForeColor = System.Drawing.Color.Red;
            //                tablecell.HorizontalAlign = HorizontalAlign.Right;
            //            }

            //        }
            //        row.Cells.Add(tablecell);
            //    }
            //    this.gvCash.Controls[0].Controls.Add(row);
            //}
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
