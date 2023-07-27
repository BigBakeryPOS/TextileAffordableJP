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
    public partial class GodownMaster : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        int EmpId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            EmpId = Convert.ToInt32(Session["EmpId"].ToString());

            if (!IsPostBack)
            {
                DataSet dscountry = objBs.GetCompanyDet();
                if (dscountry.Tables[0].Rows.Count > 0)
                {
                    ddlunit.DataSource = dscountry.Tables[0];
                    ddlunit.DataTextField = "companyname";
                    ddlunit.DataValueField = "comapanyID";
                    ddlunit.DataBind();
                    ddlunit.Items.Insert(0, "Select Company");
                }

                txtGodowncode.Enabled = true;
                DataSet ds = objBs.GetGodown();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["Godown"] = ds.Tables[0];
                    ViewState["GodownSort"] = "Asc";

                    GVGodown.DataSource = ds;
                    GVGodown.DataBind();
                }
            }
        }

        //Save Click
        protected void btnsave_OnClick(object sender, EventArgs e)
        {
            #region Validations

            if (txtGodowncode.Text == "" || txtGodowncode.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Godown Code.');", true);
                txtGodowncode.Focus();
                return;
            }
            if (txtGodownname.Text == "" || txtGodownname.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Godown Name.');", true);
                txtGodownname.Focus();
                return;
            }
            if (ddlunit.SelectedValue == "" || ddlunit.SelectedValue == "0" || ddlunit.SelectedValue == "Select Unit")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Unit.');", true);
                ddlunit.Focus();
                return;
            }

            #endregion

            txtGodowncode.Text = txtGodowncode.Text.Substring(0, 1).ToUpper() + txtGodowncode.Text.Substring(1);
            txtGodownname.Text = txtGodownname.Text.Substring(0, 1).ToUpper() + txtGodownname.Text.Substring(1);

            if (btnsave.Text == "Save")
            {
                #region

                DataSet ds = objBs.CheckGodownsCode(txtGodowncode.Text, "0");
                DataSet ds1 = objBs.CheckGodowns(txtGodownname.Text, "0");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Godown Code was already Exists.Plz Enter New One.');", true);
                    txtGodowncode.Focus();
                    return;
                }
                else if (ds1.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Godown Name was already Exists.Plz Enter New One.');", true);
                    txtGodownname.Focus();
                    return;
                }
                else
                {
                    int GodownId = objBs.InsertGodown(txtGodowncode.Text, txtGodownname.Text, Convert.ToInt32(ddlunit.SelectedValue), ddlactive.Text, EmpId);

                }

                #endregion
            }
            else
            {
                #region
                DataSet ds = objBs.CheckGodownsCode(txtGodowncode.Text, hdGodownId.Value);
                DataSet ds1 = objBs.CheckGodowns(txtGodownname.Text, hdGodownId.Value);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Godown Code  was already Exists.Plz Enter New One.');", true);
                    txtGodowncode.Focus();
                    return;
                }
                else if (ds1.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Godown Name  was already Exists.Plz Enter New One.');", true);
                    txtGodownname.Focus();
                    return;
                }
                else
                {
                    int Insert = objBs.updateGodown(txtGodowncode.Text, txtGodownname.Text, Convert.ToInt32(ddlunit.SelectedValue), ddlactive.Text, Convert.ToInt32(hdGodownId.Value), EmpId);
                }

                #endregion
            }

            Response.Redirect("GodownMaster.aspx");

        }
        //Cancel Click
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            txtGodowncode.Enabled = true;
            txtGodowncode.Text = "";
            txtGodownname.Text = "";
            hdGodownId.Value = "";
            ddlunit.SelectedIndex = 0;
            ddlactive.SelectedValue = "Yes";
            lblName.Text = "Add Godown Details";
            btnsave.Text = "Save";


        }

        //Search Click
        protected void btnsearch_OnClick(object sender, EventArgs e)
        {
            DataSet ds = objBs.SearchGodown(ddlactiveselect.SelectedValue, ddltype.SelectedValue, txtsearch.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Godown"] = ds.Tables[0];
                ViewState["GodownSort"] = "Desc";

                GVGodown.DataSource = ds;
                GVGodown.DataBind();
            }
            else
            {
                GVGodown.DataSource = null;
                GVGodown.DataBind();
            }
        }
        //Reset Click
        protected void btnreset_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("GodownMaster.aspx");
        }
        //Excel to Excel Click
        protected void btnexcel_OnClick(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "GodownMaster_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = objBs.SearchGodown(ddlactiveselect.SelectedValue, ddltype.SelectedValue, txtsearch.Text);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Godown Code"));
                    dt.Columns.Add(new DataColumn("Godown Name"));
                    dt.Columns.Add(new DataColumn("Unit Name"));
                    dt.Columns.Add(new DataColumn("IsActive"));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Godown Code"] = dr["GodownCode"];
                        dr_export["Godown Name"] = dr["GodownName"];
                        dr_export["Unit Name"] = dr["UnitName"];
                        dr_export["IsActive"] = dr["IsActive"];
                        dt.Rows.Add(dr_export);
                    }

                    #endregion

                    ExportToExcel(filename, dt);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
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

        //Grid SelectedIndexChanged
        protected void GVGodown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GVGodown.SelectedDataKey.Value != null && GVGodown.SelectedDataKey.Value.ToString() != "")
            {
                DataSet ds = objBs.EditGodown(GVGodown.SelectedDataKey.Value.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtGodowncode.Enabled = false;
                    txtGodowncode.Text = ds.Tables[0].Rows[0]["Godowncode"].ToString();
                    txtGodownname.Text = ds.Tables[0].Rows[0]["GodownName"].ToString();
                    ddlunit.SelectedValue = ds.Tables[0].Rows[0]["unitId"].ToString();
                    hdGodownId.Value = ds.Tables[0].Rows[0]["Godownid"].ToString();
                    ddlactive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();
                    lblName.Text = "Update Godown Details";
                    btnsave.Text = "Update";

                }
            }
        }
        //OnSorting
        protected void GVGodown_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable DT = (DataTable)ViewState["Godown"];
            if (DT.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["GodownSort"]) == "Asc")
                {
                    DT.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["GodownSort"] = "Desc";
                }
                else
                {
                    DT.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["GodownSort"] = "Asc";
                }

                GVGodown.DataSource = DT;
                GVGodown.DataBind();
            }

        }
        //Grid PageIndexChanging
        protected void GVGodown_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = objBs.SearchGodown(ddlactiveselect.SelectedValue, ddltype.SelectedValue, txtsearch.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Godown"] = ds.Tables[0];
                ViewState["GodownSort"] = "Desc";

                GVGodown.PageIndex = e.NewPageIndex;
                DataView DV = ds.Tables[0].DefaultView;
                GVGodown.DataSource = DV;
                GVGodown.DataBind();

            }
            else
            {
                GVGodown.DataSource = null;
                GVGodown.DataBind();
            }
        }

    }
}
