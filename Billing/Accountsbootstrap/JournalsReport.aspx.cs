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

namespace Billing.Accountsbootstrap
{
    public partial class JournalsReport : System.Web.UI.Page
    {
        journalsReport objBs = new journalsReport();
        BSClass objbs = new BSClass();
        string sTableName = "";
        double amt;
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
                sTableName = Session["User"].ToString();
                //txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtToDate.Text = DateTime.Now.ToShortDateString();
               

                //DataSet ds = objBs.getJournalsreport(sTableName, txtFromDate.Text, txtToDate.Text);
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
                    DataSet dsbranchto = objbs.Branchto();
                    ddloutlet.DataSource = dsbranchto.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    //ddloutlet.Items.Insert(0, "All");
                }
                else
                {
                    DataSet dsbranch = new DataSet();
                    dsbranch = objbs.Branchfrom(sTableName);
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
            lblMessage.Text = "Journals Report From  '" + txtFromDate.Text + "'  To  '" + txtToDate.Text + "'  for  " + ddloutlet.SelectedItem.Text;

            sTableName = Session["User"].ToString();
            DataSet ds = objBs.getJournalsreport(ddloutlet.SelectedValue, txtFromDate.Text, txtToDate.Text);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
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

        protected void btnExport_Click(object sender, EventArgs e)
        {

            sTableName = Session["User"].ToString();
            GridView gvsales = new GridView();
            gvsales.DataSource = objBs.getJournalsreport(ddloutlet.SelectedValue, txtFromDate.Text, txtToDate.Text);
            gvsales.AutoGenerateColumns = false;

            BoundField test = new BoundField();

            test.DataField = "TransNo";
            test.HeaderText = "Trans No";
            gvsales.Columns.Add(test);
            BoundField test1 = new BoundField();

            test1.DataField = "TransDate";
            test1.HeaderText = "Date";
            gvsales.Columns.Add(test1);
            BoundField test2 = new BoundField();

            test2.DataField = "Debtor";
            test2.HeaderText = "Debtor";
            gvsales.Columns.Add(test2);
            BoundField test3 = new BoundField();

            test3.DataField = "Creditor";
            test3.HeaderText = "Creditor";
            gvsales.Columns.Add(test3);
            BoundField test4 = new BoundField();

            test4.DataField = "Amount";
            test4.HeaderText = "Amount";
            gvsales.Columns.Add(test4);

            BoundField test71 = new BoundField();

            test71.DataField = "Narration";
            test71.HeaderText = "Narration";
            gvsales.Columns.Add(test71);
            




            gvsales.HeaderStyle.BackColor = System.Drawing.Color.BurlyWood;
            gvsales.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition",
                "attachment;filename=JournalsReport" + "-" + DateTime.Now.ToString("MM/dd/yyyy") + ".xls");
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                amt = amt + total;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[3].Text = "Grand Total:";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[4].Text = amt.ToString("f2");

                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;


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
            DataSet ds = objBs.getJournalsreport(ddloutlet.SelectedValue, txtFromDate.Text, txtToDate.Text);
            if (ds != null)
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
                    gridPurchase.DataBind();

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
            sb.Append("Journal Report </br>");
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