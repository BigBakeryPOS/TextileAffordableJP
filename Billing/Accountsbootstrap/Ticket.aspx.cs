using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class Ticket : System.Web.UI.Page
    {

        BSClass objMC = new BSClass();
        DataSet ds = new DataSet();
        string Product_id = "";
        string Issuperadmin = "0";
        int Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //GridBindTicket
            Id = Convert.ToInt32(Session["Empid"]);
            Issuperadmin = Session["IsSuperAdmin"].ToString();
            //if (!IsPostBack)
            {

                ds = objMC.GridBindTicket(Issuperadmin,Id.ToString());

                gv_Product.DataSource = ds;
                gv_Product.DataBind();
            }

            for (int i = 0; i < gv_Product.Rows.Count; i++)
            {
                string status = ds.Tables[0].Rows[i]["Status"].ToString();
                string statusPriority = ds.Tables[0].Rows[i]["PriorityStatus"].ToString();

                if (statusPriority == "1")
                {
                    gv_Product.Rows[i].BackColor = System.Drawing.Color.Red;
                }

                if (status == "4")
                {
                    gv_Product.Rows[i].BackColor = System.Drawing.Color.LightGreen;
                }
            }

            if (Product_id != "" || Product_id != null)
            {
                if (!IsPostBack)
                {

                }
            }
        }
        protected void gv_Product_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("ViewTicket.aspx?TicketId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Delete")
            {

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ticket.aspx");
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtfromdate.Text == "Select From Date" || txttodate.Text == "Select To Date")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Date!');", true);
                return;
            }

            DateTime frmdate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dst = objMC.GridBindTicket_Gen(frmdate.ToString(), todate.ToString(),Issuperadmin,Id.ToString());
            gv_Product.DataSource = dst;
            gv_Product.DataBind();

            for (int i = 0; i < gv_Product.Rows.Count; i++)
            {
                string status = ds.Tables[0].Rows[i]["Status"].ToString();
                string statusPriority = ds.Tables[0].Rows[i]["PriorityStatus"].ToString();

                if (statusPriority == "1")
                {
                    gv_Product.Rows[i].BackColor = System.Drawing.Color.Red;
                }

                if (status == "4")
                {
                    gv_Product.Rows[i].BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "Ticket Issues" + ".xls";
            DataSet ds = new DataSet();
            System.Globalization.CultureInfo cultureinfo =
              new System.Globalization.CultureInfo("nl-NL");
            {
                if (txtfromdate.Text == "Select From Date" || txttodate.Text == "Select To Date")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Date!');", true);
                    return;
                }

                DateTime frmdate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet dst = objMC.GridBindTicket_Gen(frmdate.ToString(), todate.ToString(), Issuperadmin, Id.ToString());
                //gv_Product.DataSource = dst;
                //gv_Product.DataBind();

                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("Date"));
                        dt.Columns.Add(new DataColumn("Name"));
                        dt.Columns.Add(new DataColumn("SName"));
                        dt.Columns.Add(new DataColumn("TicketNo"));
                        dt.Columns.Add(new DataColumn("Comments"));
                        dt.Columns.Add(new DataColumn("Status"));
                        dt.Columns.Add(new DataColumn("AdminComment"));
                        dt.Columns.Add(new DataColumn("PriorityStatus"));
                        dt.Columns.Add(new DataColumn("CompletedDate"));

                        foreach (DataRow dr in dst.Tables[0].Rows)
                        {
                            DataRow dr_export = dt.NewRow();
                            dr_export["Date"] = dr["Date"];
                            dr_export["Raise-Name"] = dr["Name"];
                            dr_export["Service-Name"] = dr["SName"];
                            dr_export["TicketNo"] = dr["TicketNo"];
                            dr_export["Comments"] = dr["Comments"];
                            dr_export["Status"] = dr["Status1"];
                            dr_export["AdminComment"] = dr["AdminComment"];
                            dr_export["PriorityStatus"] = dr["PriorityStatus_"];
                            dr_export["CompletedDate"] = dr["CompletedDate"];
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

    }
}