using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLayer;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
namespace Billing.Accountsbootstrap
{
    public partial class OpeningBalanceReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        public Double dQtyamt = 0.0;
        public Double crateamt = 0.0;
        public Double cratetotamt = 0.0;
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



                DataSet ds = objBs.getopeningbalance(ddloutlet.SelectedValue);
                gridOpening.DataSource = ds;
                gridOpening.DataBind();
            }
              lblMessage.Text = "Opening Balance List";
        }


        protected void gvstock_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            double Qty = 0;
            double RAte = 0;
            double stockRAte = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Qty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Open_Credit"));
                RAte = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Open_Depit"));
              
                dQtyamt = dQtyamt + Qty;
                crateamt = crateamt + RAte;
              
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total:";
                e.Row.Cells[3].Text = dQtyamt.ToString("f2");
                e.Row.Cells[2].Text = crateamt.ToString("f2");
                //e.Row.Cells[6].Text = cratetotamt.ToString("f2");
            }

        }



        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void branch_selectedIndex(object sender, EventArgs e)
        {

            DataSet ds = objBs.getopeningbalance(ddloutlet.SelectedValue);
            gridOpening.DataSource = ds;
            gridOpening.DataBind();
        }
        protected void btnxl_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "OpeningBalanceReport_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.gridopeningbalance();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("LedgerName"));
                    dt.Columns.Add(new DataColumn("Open_Depit"));
                    dt.Columns.Add(new DataColumn("Open_Credit"));

                    //DataRow dr_export1 = dt.NewRow();
                    //dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["LedgerName"] = dr["LedgerName"];
                        dr_export["Open_Depit"] = dr["Open_Depit"];
                        dr_export["Open_Credit"] = dr["Open_Credit"];

                        dt.Rows.Add(dr_export);
                    }

                    ExportToExcel(filename, dt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }
        }


        public void ExportToExcel(string filename, DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.HeaderStyle.Font.Bold = true;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            gridOpening.PagerSettings.Visible = false;
            //  GridView1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridOpening.RenderControl(hw);
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
            sb.Append("OpeningStock Report </br>");
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
            gridOpening.PagerSettings.Visible = true;
        }
    }
}