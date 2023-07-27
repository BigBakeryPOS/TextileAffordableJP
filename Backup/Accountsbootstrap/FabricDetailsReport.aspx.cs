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
using System.Drawing;
namespace Billing.Accountsbootstrap
{
    public partial class FabricDetailsReport : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();

        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        
          double Meter = 0;
          double AvaliableMeter= 0;
          double BillMeter = 0;
        
          double Amount = 0;
          double TaxAmount = 0;
          double NetAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                DataSet dss = objBs.GetSupplierLedgername();
                if (dss.Tables[0].Rows.Count > 0)
                {
                    ddlsupplier.DataSource = dss.Tables[0];
                    ddlsupplier.DataTextField = "LedgerName";
                    ddlsupplier.DataValueField = "LedgerID";
                    ddlsupplier.DataBind();
                    ddlsupplier.Items.Insert(0, "ALL");
                }



                DataSet dscompany = objBs.Getcompanyyname();
                if (dscompany.Tables[0].Rows.Count > 0)
                {
                    ddlcompany.DataSource = dscompany.Tables[0];
                    ddlcompany.DataTextField = "CompanyName";
                    ddlcompany.DataValueField = "ComapanyID";
                    ddlcompany.DataBind();
                    ddlcompany.Items.Insert(0, "ALL");
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");



            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }
        protected void Print(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            gridcatqty.UseAccessibleHeader = true;
            gridcatqty.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridcatqty.FooterRow.TableSection = TableRowSection.TableFooter;
            gridcatqty.Attributes["style"] = "border-collapse:separate";
            foreach (GridViewRow row in gridcatqty.Rows)
            {
                if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
                {
                    row.Attributes["style"] = "page-break-after:always;";
                }
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridcatqty.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
            sb.Append(style + gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();");
            sb.Append("};");
            sb.Append("</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "sb.ToString());", true);

            gridcatqty.DataBind();
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {
            string check = "";
            if (rdbfinished.Checked == true)
            {
                check = "Finished";
            }
            else if (rdbunfinished.Checked == true)
            {
                check = "UnFinish";
            }
            else
            {
                check = "Both";
            }

            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (ddltype.SelectedValue == "1")
            {
                ds = objBs.FabfullDetailssummary(ddlcompany.SelectedValue, fromdate, todate, check, ddlsupplier.SelectedValue, ddlfabmode.SelectedValue);
            }
            else
            {
                ds = objBs.FabfullDetailsdetailed(ddlcompany.SelectedValue, fromdate, todate, check, ddlsupplier.SelectedValue, ddlfabmode.SelectedValue);
            }

            gridcatqty.Caption = " COMPANY :- " + ddlcompany.SelectedItem.Text + " , " + " SUPPLIER :- " + ddlsupplier.SelectedItem.Text + " , " + " METER TYPE :- " + check + " <br /> " + " Fabric Detail Report From " + txtFromDate.Text + " To " + txtToDate.Text + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gridcatqty.DataSource = ds;
                gridcatqty.DataBind();
            }
            else
            {
                gridcatqty.DataSource = null;
                gridcatqty.DataBind();
            }
        }


        protected void gridcatqty_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Meter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter").ToString());
                AvaliableMeter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvaliableMeter").ToString());
                BillMeter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "BillMeter").ToString());

                Amount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount").ToString());
                TaxAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TaxAmount").ToString());
                NetAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetAmount").ToString());

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = "Total :";
                e.Row.Cells[6].Text = Meter.ToString("f2");
                e.Row.Cells[7].Text = AvaliableMeter.ToString("f2");
                e.Row.Cells[8].Text = BillMeter.ToString("f2");
                e.Row.Cells[10].Text = Amount.ToString("f2");
                e.Row.Cells[12].Text = TaxAmount.ToString("f2");
                e.Row.Cells[13].Text = NetAmount.ToString("f2");
            }
        }

        protected void btnexcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= FabricDetailsReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div2.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

    }
}