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
    public partial class CheckingProcessGrid : System.Web.UI.Page
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
                DataSet dsUnitName = objBs.Select_UnitFirst();//tblUnit
                if (dsUnitName.Tables[0].Rows.Count > 0)
                {
                    drpunit.DataSource = dsUnitName.Tables[0];
                    drpunit.DataTextField = "UnitName";
                    drpunit.DataValueField = "UnitID";
                    drpunit.DataBind();
                    drpunit.Items.Insert(0, "All");
                }


                unit_changed(sender, e);

            }
        }


        protected void unit_changed(object sender, EventArgs e)
        {
            DataSet dunit = objBs.Checking_Unit_Pending(drpunit.SelectedValue, drppending.SelectedValue);
            if (dunit.Tables[0].Rows.Count > 0)
            {
                gvcust.DataSource = dunit;
                gvcust.DataBind();
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void pending_changed(object sender, EventArgs e)
        {
            DataSet pending = objBs.Checking_Unit_Pending(drpunit.SelectedValue, drppending.SelectedValue);
            if (pending.Tables[0].Rows.Count > 0)
            {
                gvcust.DataSource = pending;
                gvcust.DataBind();
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataSet ds = new DataSet();
            if (e.CommandName == "view")
            {

                ds = objBs.Check_CheckingProcess_Availability(e.CommandArgument.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {

                    Response.Redirect("CheckingProcess.aspx?checkingid=" + e.CommandArgument.ToString());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot number Because this is used in another Process.Thank You!!!')", true);
                    return;
                }
            }
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string unit = e.Row.Cells[3].Text;

                foreach (TableCell gr in e.Row.Cells)
                {
                    if (unit == "UNIT 2")
                    {
                        gr.BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (unit == "UNIT 1")
                    {
                        gr.BackColor = System.Drawing.Color.Tomato;
                    }
                }
            }
        }
    }
}