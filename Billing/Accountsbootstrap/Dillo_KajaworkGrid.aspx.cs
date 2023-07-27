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
    public partial class Dillo_KajaworkGrid : System.Web.UI.Page
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
                DataSet ds = objBs.kajaworkgridshow();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvcust.DataSource = ds;
                        gvcust.DataBind();


                        DataTable dt = ds.Tables[0];

                        //Calculate Sum and display in Footer Row
                        decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQty"));
                        gvcust.FooterRow.Cells[4].Text = "Total";
                        gvcust.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        gvcust.FooterRow.Cells[5].Text = total.ToString("N2");

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

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlstatus.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Status.Thank You!!!.')", true);
                return;
            }
            else
            {
                string status = "";
                if (ddlstatus.SelectedValue == "1")
                {
                    status = "N";
                }
                else if (ddlstatus.SelectedValue == "2")
                {
                    status = "Y";
                }


                DataSet ds = objBs.ProcessDetails_ByStatus("kaja", status);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvcust.DataSource = ds;
                        gvcust.DataBind();


                        DataTable dt = ds.Tables[0];

                        //Calculate Sum and display in Footer Row
                        decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQty"));
                        gvcust.FooterRow.Cells[4].Text = "Total";
                        gvcust.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        gvcust.FooterRow.Cells[5].Text = total.ToString("N2");

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

        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                Response.Redirect("Dillo_KajaJobPrint.aspx?KajaId=" + e.CommandArgument.ToString());
            }

            else if (e.CommandName == "Edit")
            {
                Response.Redirect("Dillo_KajaWork.aspx?lotid=" + e.CommandArgument.ToString());
            }

        }
    }
}