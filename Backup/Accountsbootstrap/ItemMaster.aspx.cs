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
    public partial class ItemMaster : System.Web.UI.Page
    {
        DataSet tbllogin = new DataSet();
        BSClass objBs = new BSClass();
        string sTableName = "";
        string id = string.Empty;
        string Sort_Direction = "ItemName ASC";
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


                DataSet ds = objBs.griditem();
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
            Response.Redirect("itemmaster.aspx");
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
                    Response.Redirect("itemmaster.aspx?iCat=" + e.CommandArgument.ToString());
                }
            }

            if (e.CommandName == "Del")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    objBs.deleteitemmaster(e.CommandArgument.ToString());
                    Response.Redirect("itemmaster.aspx");
                }
            }

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

            DataSet ds = objBs.griditem();

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
            string filename = "ItemMaster_" + DateTime.Now.ToString() + ".xls";

            DataSet ds = new DataSet();

            ds = objBs.griditem();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Itemcode"));
                    dt.Columns.Add(new DataColumn("ItemName"));
                    dt.Columns.Add(new DataColumn("Avg.Gms"));
                    dt.Columns.Add(new DataColumn("IsActive"));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["ItemCode"] = dr["ItemCode"];
                        dr_export["Itemname"] = dr["Itemname"];
                        dr_export["IsActive"] = dr["IsActive"];
                        dr_export["Avg.Gms"] = Convert.ToDouble(dr["Avggms"]).ToString("0.000");

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
                DataSet dsCategory = objBs.Itemsrchgrid(txtitemcode.Text, txtitemname.Text);
                if (dsCategory != null)
                {
                    if (dsCategory.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Item has already Exists. please enter a new one')", true);
                        return;
                    }
                    else
                    {
                        int iStatus = objBs.InsertitemmasterLabel(txtitemcode.Text, txtitemname.Text, ddlIsActive.SelectedValue, txtavggms.Text,txtrate.Text,txttax.Text,txtnarration.Text);

                        DataSet ds = objBs.griditem();
                        gridview.DataSource = ds;
                        gridview.DataBind();
                        Response.Redirect("../Accountsbootstrap/Itemmaster.aspx");
                    }
                }
                else
                {
                    int iStatus = objBs.InsertitemmasterLabel(txtitemcode.Text, txtitemname.Text, ddlIsActive.SelectedValue, txtavggms.Text, txtrate.Text, txttax.Text, txtnarration.Text);
                    DataSet ds = objBs.griditem();
                    gridview.DataSource = ds;
                    gridview.DataBind();

                    Response.Redirect("../Accountsbootstrap/Itemmaster.aspx");
                }
            }

            else
            {
                DataSet dsCategory = objBs.itemmastersrchgrid(txtitemId.Text, txtitemcode.Text, txtitemname.Text);
                if (dsCategory != null)
                {
                    if (dsCategory.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Item Name has already Exists. please enter a new one')", true);
                        return;
                    }
                    else
                    {

                        objBs.updateItemmaster(txtitemId.Text, txtitemcode.Text, txtitemname.Text, ddlIsActive.SelectedValue, txtavggms.Text, txtrate.Text, txttax.Text, txtnarration.Text);

                        DataSet ds = objBs.griditem();
                        gridview.DataSource = ds;
                        gridview.DataBind();

                        Response.Redirect("Itemmaster.aspx");
                    }
                }
                else
                {
                    objBs.updateItemmaster(txtitemId.Text, txtitemcode.Text, txtitemname.Text, ddlIsActive.SelectedValue, txtavggms.Text, txtrate.Text, txttax.Text, txtnarration.Text);

                    DataSet ds = objBs.griditem();
                    gridview.DataSource = ds;
                    gridview.DataBind();
                    Response.Redirect("Itemmaster.aspx");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtitemId.Text = "";
            txtitemcode.Text = "";
            txtitemname.Text = "";
            txtnarration.Text = "";
            ddlIsActive.ClearSelection();
            Button1.Text = "Save";
            lblName.Text = "Add Item";
        }

        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblName.Text = "Update Item";

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
                id = gridview.SelectedDataKey.Value.ToString();
            {

                DataSet ds = objBs.getitemvalue(id);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    
                    txtitemId.Text = ds.Tables[0].Rows[0]["Itemid"].ToString();
                    txtitemcode.Text = ds.Tables[0].Rows[0]["Itemcode"].ToString();
                    txtitemname.Text = ds.Tables[0].Rows[0]["Itemname"].ToString();
                    ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();
                    txtavggms.Text = ds.Tables[0].Rows[0]["Avggms"].ToString();

                    txtrate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    txttax.Text = ds.Tables[0].Rows[0]["Tax"].ToString();

                    txtnarration.Text = ds.Tables[0].Rows[0]["Narrations"].ToString();

                    Button1.Text = "Update";
                }
            }
        }

    }
}
