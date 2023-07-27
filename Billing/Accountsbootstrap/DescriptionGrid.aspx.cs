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
    public partial class DescriptionGrid : System.Web.UI.Page
    {
        DataSet tbllogin = new DataSet();
        BSClass objBs = new BSClass();
        string sTableName = "";
        OpeningStockEntry objBs1 = new OpeningStockEntry();
        string Sort_Direction = "Description ASC";
        string Sort_Direction1 = "category ASC";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
                ViewState["SortExpr"] = Sort_Direction;
                ViewState["SortExpr"] = Sort_Direction1;
                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();


                string super = Session["IsSuperAdmin"].ToString();


                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "All");
                    }
                }
                else
                {

                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }

                DataSet ds1 = objBs.gridcustomerproduct(sTableName,drpbranch.SelectedValue);
                if (ds1 != null)
                {
                    gridview.DataSource = ds1;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }

        }

        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {
                DataSet ds1 = objBs.gridcustomerproduct(sTableName, drpbranch.SelectedValue);
                if (ds1 != null)
                {
                    gridview.DataSource = ds1;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
        }

        protected void gridview_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objBs.CheckIfproductUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((Image)e.Row.FindControl("Image1")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;
                }

            }
        }

        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("itempage.aspx?cust=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "Del")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    objBs.deletedescgrid(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text,sTableName);
                    Response.Redirect("DescriptionGrid.aspx");
                }
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = btnadd.Text;
            {
                button = btnadd.Text;
                Response.Redirect("itempage.aspx?name=" + button.ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(ddlcategory.SelectedValue) == 0 && txtdescription.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Search By Value and Enter the Searching Data!');", true);
                ddlcategory.Focus();
                return;
            }


            else if (Convert.ToInt32(ddlcategory.SelectedValue) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Search By Value!');", true);
                ddlcategory.Focus();
                return;
            }


            else if (txtdescription.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Searching Data!');", true);
                txtdescription.Focus();
                return;
            }


            DataSet ds = objBs.Itemsearch(txtdescription.Text, Convert.ToInt32(ddlcategory.SelectedValue),sTableName);

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



            //if (ddlcategory.SelectedValue=="1")
            //{
            //    DataSet ds = objBs.categorysrch(txtdescription.Text);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gridview.DataSource = ds;
            //        gridview.DataBind();
            //    }
            //    else
            //    {
            //        gridview.DataSource = null;
            //        gridview.DataBind();

            //        //lblerror.Text = "No Records Found for this Category!";
            //    }
            //}
            //else if (ddlcategory.SelectedValue == "2")
            //{
            //    DataSet ds = objBs.srchbydef(txtdescription.Text);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gridview.DataSource = ds;
            //        gridview.DataBind();
            //    }
            //    else
            //    {
            //        gridview.DataSource = null;
            //        gridview.DataBind();

            //        //lblerror.Text = "No Records Found for this Description!";
            //    }


            //}
            //else if (ddlcategory.SelectedValue == "3")
            //{
            //    DataSet ds = objBs.SearchSerial( txtdescription.Text);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gridview.DataSource = ds;
            //        gridview.DataBind();
            //    }
            //    else
            //    {
            //        gridview.DataSource = null;
            //        gridview.DataBind();

            //        //lblerror.Text = "No Records Found for this Description!";
            //    }

            //else if (ddlcategory.SelectedValue == "4")
            //{
            //    DataSet ds = objBs.SearchSerialNO(txtdescription.Text);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gridview.DataSource = ds;
            //        gridview.DataBind();
            //    }
            //    else
            //    {
            //        gridview.DataSource = null;
            //        gridview.DataBind();

            //        //lblerror.Text = "No Records Found for this Description!";
            //    }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("DescriptionGrid.aspx");
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
            DataSet ds1 = objBs.gridcustomerproduct(sTableName,drpbranch.SelectedValue);
            DataView dvEmp = ds1.Tables[0].DefaultView;
            dvEmp.Sort = ViewState["SortExpr"].ToString();
            Session["SortedView"] = dvEmp;
            gridview.DataSource = dvEmp;
            gridview.DataBind();

        }

        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlcategory.SelectedValue == "0")
            {
                ds = objBs.gridcustomer();
            }
            else
            {

                ds = objBs.Itemsearch(txtdescription.Text, Convert.ToInt32(ddlcategory.SelectedValue), sTableName);
            }

            if (Session["SortedView"] != null)
            {
                gridview.DataSource = Session["SortedView"];
                gridview.DataBind();
            }
            else
            {
                gridview.DataSource = ds;
                gridview.PageIndex = e.NewPageIndex;
                // gvsales.DataBind();
            }
        
            gridview.PageIndex = e.NewPageIndex;
            DataView dvEmployee = ds.Tables[0].DefaultView;
            gridview.DataSource = dvEmployee;
            gridview.DataBind();
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = Button3.Text;
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('If You Need This.Please Contact Administrator.Thank You!!!.');", true);
                return;
                //button = Button3.Text;
                ////Response.Redirect("categorymaster.aspx");
                //Response.Redirect("itempage.aspx?name=" + button.ToString());
            }

        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "Itemmaster_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.gridcustomerproduct(sTableName, drpbranch.SelectedValue);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Category"));
                    dt.Columns.Add(new DataColumn("Itemcode"));
                    dt.Columns.Add(new DataColumn("ItemName"));
                    dt.Columns.Add(new DataColumn("Tax"));             
                    dt.Columns.Add(new DataColumn("IsActive"));


                    //DataRow dr_export1 = dt.NewRow();
                    //dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Category"] = dr["Category"];
                        dr_export["Tax"] = dr["Tax"];
                        dr_export["Itemcode"] = dr["Definition"];
                        dr_export["ItemName"] = dr["Serial_No"];
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
