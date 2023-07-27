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
    public partial class FabricType : System.Web.UI.Page
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
            //  Label myLabel = this.FindControl("lblscreenname") as Label;
            if (!IsPostBack)
            {
                ViewState["SortExpr"] = Sort_Direction;


                DataSet ds = objBs.gridFabricType();
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


                //DataSet ds1 = objBs.MinStock();
                //GVStockAlert.DataSource = ds1;
                //GVStockAlert.DataBind();

            }
        }

        protected void Sorting(object sender, GridViewSortEventArgs e)
        {

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = btnadd.Text;
            if (button == "Add New")
            {
                // Response.Redirect("categorymaster.aspx");
                Response.Redirect("categorymaster.aspx?name=" + button.ToString());
            }
            else
            {
                button = Button2.Text;
                //Response.Redirect("categorymaster.aspx");
                Response.Redirect("categorymaster.aspx?name=" + button.ToString());
            }


        }

        protected void Btn_Reset(object sender, EventArgs e)
        {
            Response.Redirect("FabricType.aspx");
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





            //if (ddlfilter.SelectedValue == "1")
            //{
            //    DataSet ds = objBs.categorysrchgrid(txtsearch.Text);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {

            //        gridview.DataSource = ds;
            //        gridview.DataBind();

            //    }
            //    else
            //    {
            //        gridview.DataSource = null;
            //        gridview.DataBind();
            //    }
            //}

            //else
            //    if (ddlfilter.SelectedValue == "0")
            //    {
            //        gridview.DataSource = null;
            //        gridview.DataBind();
            //    }
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = Button2.Text;
            {
                button = Button2.Text;
                //Response.Redirect("categorymaster.aspx");
                Response.Redirect("categorymaster.aspx?name=" + button.ToString());
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

        protected void gvcat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("categorymaster.aspx?iCat=" + e.CommandArgument.ToString());
                }
            }

            if (e.CommandName == "Del")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    objBs.deletecategorymaster(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text, sTableName);
                    Response.Redirect("FabricType.aspx");
                }
            }

        }


        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.gridFabricType();
            }
            else
            {
                ds = objBs.categorysrchgrid(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), sTableName);
            }
            gridview.PageIndex = e.NewPageIndex;
            DataView dvEmployee = ds.Tables[0].DefaultView;
            gridview.DataSource = dvEmployee;
            gridview.DataBind();



        }

       

        protected void gridview_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objBs.CheckIfCategoryUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((Image)e.Row.FindControl("Image1")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;
                }

            }
        }

        protected void GVStockAlert_RowDataBound()
        {

        }
        public SortDirection dir
        {

            get
            {

                if (ViewState["dirState"] == null)
                {

                    ViewState["dirState"] = SortDirection.Ascending;

                }

                return (SortDirection)ViewState["dirState"];

            }

            set
            {

                ViewState["dirState"] = value;

            }

        }
        protected void gridview_Sorting(object sender, GridViewSortEventArgs e)
        {
            string[] SortOrder = ViewState["SortExpr"].ToString().Split(' ');
            if (SortOrder[0] == e.SortExpression)
            {
                if (SortOrder[1] == "ASC")
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "DESC";
                }
                else
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
                }
            }
            else
            {
                ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
            }

            DataSet ds = objBs.gridFabricType();

            DataView dvEmp = ds.Tables[0].DefaultView;
            dvEmp.Sort = ViewState["SortExpr"].ToString();
            gridview.DataSource = dvEmp;
            gridview.DataBind();
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "FabricType_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.gridFabricType();

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
                DataSet dsFabricType = objBs.getFabricType(txtFabricType.Text);
                if (dsFabricType != null)
                {
                    if (dsFabricType.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This FabricType has already Exists. please enter a new one')", true);
                        return;
                    }
                    else
                    {
                        int iStatus = objBs.InsertFabricType(txtFabricType.Text,Convert.ToInt32(ddlIsActive.SelectedValue));

                        DataSet ds = objBs.gridFabricType();
                        gridview.DataSource = ds;
                        gridview.DataBind();
                       
                    }
                }
                else
                {
                    int iStatus = objBs.InsertFabricType(txtFabricType.Text, Convert.ToInt32(ddlIsActive.SelectedValue));
                    DataSet ds = objBs.gridFabricType();

                    gridview.DataSource = ds;
                    gridview.DataBind();
                }

                Response.Redirect("Fabrictype.aspx");
            }
            else
            {
                DataSet dsFabricType = objBs.getFabricTypeup(txtFabricType.Text, Convert.ToInt32(txtFabricTypeId.Text));
                if (dsFabricType != null)
                {
                    if (dsFabricType.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This FabricType has already Exists. please enter a new one')", true);
                        return;
                    }
                    else
                    {

                        int iStatus = objBs.InsertFabricTypeupdate(txtFabricType.Text, Convert.ToInt32(ddlIsActive.SelectedValue), Convert.ToInt32(txtFabricTypeId.Text));
                        DataSet ds = objBs.gridFabricType();
                        gridview.DataSource = ds;
                        gridview.DataBind();
                    }
                }
                else
                {
                    int iStatus = objBs.InsertFabricTypeupdate(txtFabricType.Text, Convert.ToInt32(ddlIsActive.SelectedValue), Convert.ToInt32(txtFabricTypeId.Text));
                    DataSet ds = objBs.gridFabricType();
                    gridview.DataSource = ds;
                    gridview.DataBind();
                   
                }
                Response.Redirect("Fabrictype.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtFabricTypeId.Text = "";
            txtFabricType.Text = "";
            ddlIsActive.ClearSelection();
            Button1.Text = "Save";
            lblName.Text = "Add Fabrictype";
        }

        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblName.Text = "Update Fabrictype";

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
                id = gridview.SelectedDataKey.Value.ToString();
            {

                DataSet ds = objBs.getFabricTypeid(id);
                if (ds.Tables[0].Rows.Count > 0)
                {

                   // listcategory.Enabled = false;
                    txtFabricTypeId.Text = ds.Tables[0].Rows[0]["FabricTypeID"].ToString();
                    txtFabricType.Text = ds.Tables[0].Rows[0]["FabricType"].ToString();
                    ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                    Button1.Text = "Update";
                }
            }
        }





    }
}
