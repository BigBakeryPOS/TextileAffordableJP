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
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class PurchaseOrderGrid : System.Web.UI.Page
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
                txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet dsyear = objBs.GetYear();
                if (dsyear.Tables[0].Rows.Count > 0)
                {
                    drpyear.DataSource = dsyear.Tables[0];
                    drpyear.DataTextField = "yearname";
                    drpyear.DataValueField = "yearname";
                    drpyear.DataBind();
                }

                //DataSet ds = objBs.gridPurchaseOrder();
                DataSet ds = objBs.gridPurchaseOrderEntryGrid(drpyear.SelectedValue, From, To);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVPurchaseOrder.DataSource = ds;
                    GVPurchaseOrder.DataBind();
                }
                else
                {
                    GVPurchaseOrder.DataSource = null;
                    GVPurchaseOrder.DataBind();
                }
            }
        }

        protected void drpyear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet ds = objBs.gridPurchaseOrderEntryGrid(drpyear.SelectedValue, From, To);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVPurchaseOrder.DataSource = ds;
                GVPurchaseOrder.DataBind();
            }

            else
            {
                GVPurchaseOrder.DataSource = null;
                GVPurchaseOrder.DataBind();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseOrder.aspx");

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectcustomerDet(1, 1);
            }
            else
            {
                ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVPurchaseOrder.DataSource = ds;
                    GVPurchaseOrder.PageIndex = e.NewPageIndex;
                    GVPurchaseOrder.DataBind();
                }

                else
                {
                    GVPurchaseOrder.DataSource = null;
                    GVPurchaseOrder.DataBind();
                }
            }
            else
            {
                GVPurchaseOrder.DataSource = null;
                GVPurchaseOrder.DataBind();
            }

        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet ds = objBs.gridPurchaseOrderEntryGrid(drpyear.SelectedValue, From, To);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVPurchaseOrder.DataSource = ds;
                GVPurchaseOrder.DataBind();
            }

            else
            {
                GVPurchaseOrder.DataSource = null;
                GVPurchaseOrder.DataBind();
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/viewcustomer.aspx");

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVPurchaseOrder.DataSource = ds;
                    GVPurchaseOrder.DataBind();
                }
                else
                {
                    GVPurchaseOrder.DataSource = null;
                    GVPurchaseOrder.DataBind();
                }
            }
            else
            {
                GVPurchaseOrder.DataSource = null;
                GVPurchaseOrder.DataBind();
            }
        }
        protected void GVPurchaseOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("PurchaseOrder.aspx?POId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Print")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("PurchaseOrderPrint.aspx?POId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "dele")
            {
                if (e.CommandArgument.ToString() != "")
                {
                   // Response.Redirect("PurchaseOrderPrint.aspx?POId=" + e.CommandArgument.ToString());

                    // check PO ORDER 
                    DataSet dpoordercheck = objBs.CheckPoentry(e.CommandArgument.ToString());
                    if (dpoordercheck.Tables[0].Rows.Count > 0)
                    {
                        string FullGrn = "";

                        for (int i = 0; i < dpoordercheck.Tables[0].Rows.Count; i++)
                        {
                            string grnno = dpoordercheck.Tables[0].Rows[i]["FullRecPONo"].ToString();
                            FullGrn +=  "," + grnno;

                        }

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please check With This GRN Number " + FullGrn + ".Thank You!!!.')", true);
                        return;
                    }
                    else
                    {
                        int idletepo = objBs.IdeletePO(e.CommandArgument.ToString());
                        //Response.Redirect("PurchaseOrderGrid.aspx");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Order Deleted Successfully.Thank You!!!.')", true);
                        DataSet ds = objBs.gridPurchaseOrder();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GVPurchaseOrder.DataSource = ds;
                            GVPurchaseOrder.DataBind();
                        }

                        else
                        {
                            GVPurchaseOrder.DataSource = null;
                            GVPurchaseOrder.DataBind();
                        }
                    }
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
                Response.Redirect("customermaster.aspx?name=" + button.ToString());
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "CustomerMaster_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.selectcustomerDet(1, 1);

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
                    //dt.Columns.Add(new DataColumn("Area"));
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


