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
    public partial class OrderGrid : System.Web.UI.Page
    {
        string sTableName = "";
        BSClass objbs = new BSClass();
        double amount1 = 0;
        double grandtot = 0.00;
        double grandremtot = 0.00;
        double grandremtoto = 0.00;
        string brach = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExport);

            if (!IsPostBack)
            {
                //DateTime today = DateTime.Today;
                //int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);


                // Supplier List
                DataSet dsup = objbs.getnewsupplierforfab();
                if (dsup.Tables[0].Rows.Count > 0)
                {

                    ddlsupplier.DataSource = dsup.Tables[0];
                    ddlsupplier.DataTextField = "LEdgerName";
                    ddlsupplier.DataValueField = "LedgerID";
                    ddlsupplier.DataBind();
                    ddlsupplier.Items.Insert(0, "All");
                }

                DataSet dss = new DataSet();
                dss = objbs.orderreport();
                gvCustsales.DataSource = dss;
                gvCustsales.DataBind();


                txtfromdate.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }
        }

        protected void refeshClcik(object sender, EventArgs e)
        {
            Response.Redirect("OrderGrid.aspx");
        }

        protected void Serachclick(object sender, EventArgs e)
        {
            if (txtfromdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select From-Date.Thank You!!!');", true);
                return;
            }
            if (txttodate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select To-Date.Thank You!!!');", true);
                return;
            }

            DateTime fromdate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet dchk = objbs.orderreport(fromdate, todate);
            if (dchk.Tables[0].Rows.Count > 0)
            {
                gvCustsales.DataSource = dchk;
                gvCustsales.DataBind();
            }
            else
            {
                gvCustsales.DataSource = null;
                gvCustsales.DataBind();
            }

        }
        protected void ddlsupplier_SelectedIndexChanged(object sender,EventArgs e)
        {
            DataSet dchk = objbs.orderreport_BySupplier(ddlsupplier.SelectedValue);
            if (dchk.Tables[0].Rows.Count > 0)
            {
                gvCustsales.DataSource = dchk;
                gvCustsales.DataBind();
            }
            else
            {
                gvCustsales.DataSource = null;
                gvCustsales.DataBind();
            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/Orderprocess.aspx");

        }


        protected void btnall_Click(object sender, EventArgs e)
        {


        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            DataSet dCustReport = objbs.CustomerSalesAdmin();
            gvCustsales.DataSource = dCustReport.Tables[0];
            gvCustsales.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //DataSet dt = new DataSet();
            //GridView gridview = new GridView();
            //int sbranch = Convert.ToInt32(ddlBranch.SelectedValue);
            //DataSet dsbranch1 = objbs.selectbranchmaster(sbranch);
            //string name = string.Empty;



            //if (dsbranch1.Tables[0].Rows.Count > 0)
            //{
            //    string sales = dsbranch1.Tables[0].Rows[0]["sales"].ToString();
            //    string[] wordArray = sales.Split('_');

            //     brach = wordArray[1];


            //    if (brach == "CO1")
            //    {
            //        Label123.Text = "KKNagar";
            //        name = "KKNAGAR";

            //    }
            //    else if (brach == "CO2")
            //    {
            //        Label123.Text = "BYEPASS";
            //        name = "BYEPASS";
            //    }
            //    else if (brach == "CO3")
            //    {
            //        Label123.Text = "BBKULAM";
            //        name = "BBKULAM";
            //    }
            //    else if (brach == "CO4")
            //    {
            //        Label123.Text = "NARANAYAPURAM";
            //        name = "NARANAYAPURAM";
            //    }
            //}
            //if (sTableName == "admin")
            //{
            //    gridview.DataSource = objbs.CustomerSalesAdmin1();
            //    gridview.DataBind();
            //}

            //else
            //{
            //    dt = objbs.CustomerSalesBranchreport(sTableName, txtfromdate.Text, txttodate.Text, name);
            //    gridview.DataSource = objbs.CustomerSalesBranchreport(sTableName, txtfromdate.Text, txttodate.Text, name);
            //    gridview.DataBind();
            //}
            ////Response.ClearContent();
            ////Response.AddHeader("content-disposition",
            ////    "attachment;filename=CustomerSalesReport.xls");
            ////Response.ContentType = "applicatio/excel";
            ////StringWriter sw = new StringWriter(); ;
            ////HtmlTextWriter htm = new HtmlTextWriter(sw);
            ////gridview.AllowPaging = false;
            ////gridview.RenderControl(htm);
            ////Response.Write(sw.ToString());
            ////Response.End();
            ////gridview.AllowPaging = true;
            //string filename = "salesreport.xls";
            //System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //DataGrid dgGrid = new DataGrid();
            //dgGrid.DataSource = dt;
            //dgGrid.DataBind();
            //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            //dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            //dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            //dgGrid.HeaderStyle.Font.Bold = true;
            ////Get the HTML for the control.
            //dgGrid.RenderControl(hw);
            ////Write the HTML back to the browser.
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //this.EnableViewState = false;
            //Response.Write(tw.ToString());
            //Response.End();
        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            //DateTime sFrom = Convert.ToDateTime(txtfromdate.Text);

            //DateTime sTo = Convert.ToDateTime(txttodate.Text);
            //DataSet ds = objbs.CustomerSalesBranch(sTableName, sFrom, sTo);
            //gvCustsales.PageIndex = e.NewPageIndex;
            //gvCustsales.DataSource = ds.Tables[0];
            //gvCustsales.DataBind();


        }

        public void gvCustsales_RowDataBound(object sender, GridViewRowEventArgs e)
        {

           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }



            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                double toto = Convert.ToDouble(e.Row.Cells[5].Text);
                grandtot = grandtot + toto;


                double totoo = Convert.ToDouble(e.Row.Cells[6].Text);
                grandremtot = grandremtot + totoo;

                double totooo = Convert.ToDouble(e.Row.Cells[7].Text);
                grandremtoto = grandremtoto + totooo;
                //GridView gv = e.Row.FindControl("gvLiaLedger") as GridView;
                //GridView gvGroup = (GridView)sender;
                //if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                //{
                //    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                //    DataSet ds = objbs.getdetailedfabreport(groupID);
                //    if (gvGroup.DataKeys[e.Row.RowIndex].Value.ToString() == "edit")
                //    {
                //        if (gvGroup.DataKeys[e.Row.RowIndex].Value.ToString() != "")
                //        {
                //            Response.Redirect("Fabricprocess.aspx?iid=" + gvGroup.DataKeys[e.Row.RowIndex].Value.ToString());
                //        }
                //    }

                //    else if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        gv.DataSource = ds;
                //        //double amount = Convert.ToDouble(ds.Tables[0].Rows[0]["NetAmount"]);
                //        //   amount1 = amount1 + amount;
                //        gv.DataBind();
                //    }
                //}

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total";
                e.Row.Cells[5].Text = grandtot.ToString("#.##");
                e.Row.Cells[6].Text = grandremtot.ToString("#.##");
                e.Row.Cells[7].Text = grandremtoto.ToString("#.##");
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;

                //e.Row.Cells[0].Text = "Total";
                ////e.Row.Cells[1].Text = HorizontalAlign.Right;
                //e.Row.Cells[7].Text = amount1.ToString("N2");


            }

        }

        public void gvCustsales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Orderprocess.aspx?iid=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Print")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Dillo_OrderprocessPrint.aspx?printid=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Printnew")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Dillo_OrderprocessPrintnew.aspx?printid=" + e.CommandArgument.ToString());
                }
            }
        }

        
        public void gvCustt_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv1 = e.Row.FindControl("gvLiaLedger") as GridView;
                GridView gvGroup = (GridView)sender;
                int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                DataSet ds = objbs.getdetailedfabcutreport(groupID);
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {

                    if (gvGroup.DataKeys[e.Row.RowIndex].Value.ToString() == "edit")
                    {
                        if (gvGroup.DataKeys[e.Row.RowIndex].Value.ToString() != "")
                        {
                            Response.Redirect("Fabricprocess.aspx?iid=" + gvGroup.DataKeys[e.Row.RowIndex].Value.ToString());
                        }
                    }

                    else if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv1.DataSource = ds;
                        //double amount = Convert.ToDouble(ds.Tables[0].Rows[0]["NetAmount"]);
                        //   amount1 = amount1 + amount;
                        gv1.DataBind();
                    }
                }

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;

                //e.Row.Cells[0].Text = "Total";
                ////e.Row.Cells[1].Text = HorizontalAlign.Right;
                //e.Row.Cells[7].Text = amount1.ToString("N2");


            }

        }

        public void gvlia_comm(object sender, GridViewCommandEventArgs e)
        {
            //string tranid = e.CommandArgument.ToString();
            //Response.Redirect("Fabricprocess.aspx?iid=" + tranid.ToString());
        }
    }
}