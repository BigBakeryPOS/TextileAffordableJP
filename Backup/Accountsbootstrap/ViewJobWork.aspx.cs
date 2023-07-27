using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Billing.Accountsbootstrap
{
    public partial class ViewJobWork : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objBs.selectJobWorkDet(10, 2);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvcust.DataSource = ds;
                        gvcust.DataBind();
                    }

                    else
                    {
                        gvcust.DataSource = null;
                        gvcust.DataBind();
                    }
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {

            string button = string.Empty;
            button = btnadd.Text;
            {
                button = btnadd.Text;
                Response.Redirect("JobWorkMaster.aspx?name=" + button.ToString());
            }
            //  Response.Redirect("../Accountsbootstrap/customermaster.aspx");

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectcustomerDet(6, 2);
            }
            else
            {
                ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.PageIndex = e.NewPageIndex;
                    gvcust.DataBind();
                }

                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }

        }

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/viewsupplier.aspx");

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.DataBind();
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("JobWorkMaster.aspx?LedgerID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.deleteJobWorkcustomer(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text);

                Response.Redirect("ViewJobWork.aspx");
            }
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objBs.CeckIfVendorUsed("tblDayBook_" + sTableName, int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    //((Image)e.Row.FindControl("img")).Visible = false;
                    //((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;


                    ((Image)e.Row.FindControl("dlt")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = true;
                }

            }
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = Button3.Text;
            {
                button = Button3.Text;
                //Response.Redirect("categorymaster.aspx");
                Response.Redirect("Suppliermaster.aspx?name=" + button.ToString());
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "SupplierMaster_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.selectcustomerDet(6, 2);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("CustomerName"));
                    dt.Columns.Add(new DataColumn("MobileNo"));
                    dt.Columns.Add(new DataColumn("Email"));
                    dt.Columns.Add(new DataColumn("Type"));
                    dt.Columns.Add(new DataColumn("Address"));
                    // dt.Columns.Add(new DataColumn("Area"));
                    //dt.Columns.Add(new DataColumn("City"));
                    dt.Columns.Add(new DataColumn("IsActive"));
                    dt.Columns.Add(new DataColumn("Open-Credit"));
                    dt.Columns.Add(new DataColumn("Open-Debit"));



                    //DataRow dr_export1 = dt.NewRow();
                    //dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["CustomerName"] = dr["LedgerName"];
                        dr_export["MobileNo"] = dr["MobileNo"];
                        dr_export["Email"] = dr["Email"];
                        dr_export["Type"] = dr["contacttypename"];
                        dr_export["Address"] = dr["Address"];
                        //dr_export["Area"] = dr["Area"];
                        //dr_export["City"] = dr["City"];
                        dr_export["IsActive"] = dr["IsActive"];
                        dr_export["Open-Credit"] = dr["Open_Credit"];
                        dr_export["Open-Debit"] = dr["Open_Depit"];
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
    }
}