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

namespace Billing.Accountsbootstrap
{
    public partial class Style : System.Web.UI.Page
    {
        DataSet tbllogin = new DataSet();
        BSClass objBs = new BSClass();
        string sTableName = "";
        string id = string.Empty;
        string Sort_Direction = "Category ASC";
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
                ViewState["SortExpr"] = Sort_Direction;

                DataSet dsset = objBs.gettblFabricType();
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlfabrictype.DataSource = dsset.Tables[0];
                    ddlfabrictype.DataTextField = "FabricType";
                    ddlfabrictype.DataValueField = "FabricTypeID";
                    ddlfabrictype.DataBind();
                    //ddlBottilot.Items.Insert(0, "Select LotNo");
                }

                DataSet ds = objBs.gridStyle();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridview.DataSource = ds;
                        gridview.DataBind();
                    }
                    else
                    {
                        gridview.DataSource = null;
                        gridview.DataBind();
                    }

                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }



            }
        }



        protected void Btn_Reset(object sender, EventArgs e)
        {
            Response.Redirect("Style.aspx");
        }
        protected void Btn_Search(object sender, EventArgs e)
        {
            DataSet ds = objBs.categorysrchgrid(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), sTableName);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridview.DataSource = ds;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }

            }
            else
            {
                gridview.DataSource = null;
                gridview.DataBind();
            }



        }



        public void bindData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Category"));

            DataRow dr_final12 = dt.NewRow();
            dr_final12["Category"] = "";
            dt.Rows.Add(dr_final12);

            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                //string filename = "Sales Report.xls";
                string filename = "NewCategory _" + DateTime.Now.ToString() + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                //dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                //dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
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








        protected void gridview_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objBs.CheckIfCategoryUsedStyleID(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((Image)e.Row.FindControl("Image1")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;
                }

            }
        }



        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "Style_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.gridStyle();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Category"));
                    dt.Columns.Add(new DataColumn("IsActive"));


                    //DataRow dr_export1 = dt.NewRow();
                    //dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Category"] = dr["Category"];
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Button1.Text == "Save")
            {
                DataSet dsStyle = objBs.getStyle(txtStyle.Text, Convert.ToInt32(ddlfabrictype.SelectedValue));
                if (dsStyle != null)
                {
                    if (dsStyle.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Style has already Exists. please enter a new one')", true);
                        return;
                    }
                    else
                    {
                        int iStatus = objBs.InsertStyle(Convert.ToInt32(ddlfabrictype.SelectedValue), txtStyle.Text, Convert.ToInt32(ddlIsActive.SelectedValue));

                        DataSet ds = objBs.gridStyle();
                        gridview.DataSource = ds;
                        gridview.DataBind();

                    }
                }
                else
                {
                    int iStatus = objBs.InsertStyle(Convert.ToInt32(ddlfabrictype.SelectedValue), txtStyle.Text, Convert.ToInt32(ddlIsActive.SelectedValue));
                    DataSet ds = objBs.gridStyle();

                    gridview.DataSource = ds;
                    gridview.DataBind();
                }
                Response.Redirect("Style.aspx");
            }
            else
            {
                DataSet dsStyle = objBs.getStyleup(txtStyle.Text, Convert.ToInt32(txtstyleId.Text), Convert.ToInt32(ddlfabrictype.SelectedValue));
                if (dsStyle != null)
                {
                    if (dsStyle.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Style has already Exists. please enter a new one')", true);
                        return;
                    }
                    else
                    {

                        int iStatus = objBs.InsertStyleupdate(Convert.ToInt32(ddlfabrictype.SelectedValue), txtStyle.Text, Convert.ToInt32(ddlIsActive.SelectedValue), Convert.ToInt32(txtstyleId.Text));
                        DataSet ds = objBs.gridStyle();
                        gridview.DataSource = ds;
                        gridview.DataBind();
                    }
                }
                else
                {
                    int iStatus = objBs.InsertStyleupdate(Convert.ToInt32(ddlfabrictype.SelectedValue), txtStyle.Text, Convert.ToInt32(ddlIsActive.SelectedValue), Convert.ToInt32(txtstyleId.Text));
                    DataSet ds = objBs.gridStyle();
                    gridview.DataSource = ds;
                    gridview.DataBind();

                }
                Response.Redirect("Style.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtstyleId.Text = "";
            txtStyle.Text = "";
            ddlIsActive.ClearSelection();
            Button1.Text = "Save";
            lblName.Text = "Add Style";
        }

        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblName.Text = "Update Style";

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
                id = gridview.SelectedDataKey.Value.ToString();
            {

                DataSet ds = objBs.getStyleid(id);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    // listcategory.Enabled = false;
                    txtstyleId.Text = ds.Tables[0].Rows[0]["StyleID"].ToString();
                    ddlfabrictype.Text = ds.Tables[0].Rows[0]["FabricTypeID"].ToString();
                    txtStyle.Text = ds.Tables[0].Rows[0]["Style"].ToString();
                    ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                    Button1.Text = "Update";
                }
            }
        }
    }
}