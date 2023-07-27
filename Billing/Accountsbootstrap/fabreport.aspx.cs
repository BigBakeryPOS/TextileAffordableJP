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
namespace Billing.Accountsbootstrap
{
    public partial class fabreport : System.Web.UI.Page
    {
        string sTableName = "";
        BSClass objbs = new BSClass();
        double amount1 = 0;
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

                DataSet dss = new DataSet();
                dss = objbs.fabreport();
                gvCustsales.DataSource = dss;
                gvCustsales.DataBind();


            }
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
                GridView gv = e.Row.FindControl("gvLiaLedger") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                    DataSet ds = objbs.getdetailedfabreport(groupID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        //double amount = Convert.ToDouble(ds.Tables[0].Rows[0]["NetAmount"]);
                        //   amount1 = amount1 + amount;
                        gv.DataBind();
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

        public void gvCustt_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv1 = e.Row.FindControl("LiaLedger") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                    DataSet ds = objbs.getdetailedfabcutreport(groupID);
                    if (ds.Tables[0].Rows.Count > 0)
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
    }
}