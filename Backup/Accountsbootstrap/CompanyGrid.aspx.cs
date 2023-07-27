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
    public partial class CompanyGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string Sort_Direction = "BrandName ASC";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                ViewState["SortExpr"] = Sort_Direction;

                DataSet ds = objBs.GetCompanyDetails();
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

            Response.Redirect("company_Details.aspx");
            //string button = string.Empty;
            //button = btnadd.Text;
            //if (button == "Add New")
            //{
            //    // Response.Redirect("categorymaster.aspx");
            //    Response.Redirect("BrandMaster.aspx?name=" + button.ToString());
            //}
            //else
            //{
            //    button = Button2.Text;
            //    //Response.Redirect("categorymaster.aspx");
            //    Response.Redirect("BrandMaster.aspx?name=" + button.ToString());
            //}
            //Response.Redirect("../Accountsbootstrap/BrandMaster.aspx");

        }

        protected void gridview_Sorting(object sender, GridViewSortEventArgs e)
        {
            //string[] SortOrder = ViewState["SortExpr"].ToString().Split(' ');
            //if (SortOrder[0] == e.SortExpression)
            //{
            //    if (SortOrder[1] == "ASC")
            //    {
            //        ViewState["SortExpr"] = e.SortExpression + " " + "DESC";
            //    }
            //    else
            //    {
            //        ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
            //    }
            //}
            //else
            //{
            //    ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
            //}

            //DataSet ds = objBs.selectBrand();

            //DataView dvEmp = ds.Tables[0].DefaultView;
            //dvEmp.Sort = ViewState["SortExpr"].ToString();
            //gvcust.DataSource = dvEmp;
            //gvcust.DataBind();
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = Button2.Text;
            {
                button = Button2.Text;
                //Response.Redirect("categorymaster.aspx");
                Response.Redirect("BrandMaster.aspx?name=" + button.ToString());
            }

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            //DataSet ds = new DataSet();
            //if (ddlfilter.SelectedValue == "0")
            //{
            //    ds = objBs.selectBrand();
            //}
            //else
            //{
            //    ds = objBs.searchBrand(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue));
            //}
            //if (ds != null)
            //{
            //    gvcust.DataSource = ds;
            //    gvcust.PageIndex = e.NewPageIndex;
            //    gvcust.DataBind();
            //}
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/viewbrands.aspx");

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchBrand(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue));
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
                    Response.Redirect("Company_Details.aspx?iCusID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                //int iSucess = objBs.deleteBrand(Convert.ToInt32(e.CommandArgument.ToString()), "tblAuditMaster_" + sTableName, lblUser.Text);
                //Response.Redirect("viewbrands.aspx");
            }
        }

        protected void gvcust_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (objBs.CheckIfbrandUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
            //    {
            //        ((Image)e.Row.FindControl("dlt")).Visible = false;
            //        ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;
            //    }

            //}
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "Brand_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.selectBrandlist();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("BrandName"));
                    dt.Columns.Add(new DataColumn("BrandCode"));
                    dt.Columns.Add(new DataColumn("IsActive"));


                    //DataRow dr_export1 = dt.NewRow();
                    //dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["BrandName"] = dr["BrandName"];
                        dr_export["BrandCode"] = dr["BrandCode"];
                        dr_export["IsActive"] = dr["IsActive"];

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