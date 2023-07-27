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
    public partial class Dillo_Irongrid : System.Web.UI.Page
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
                DataSet ds = objBs.SelectIronDetGrid();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvcust.DataSource = ds;
                        gvcust.DataBind();

                        DataTable dt = ds.Tables[0];

                        //Calculate Sum and display in Footer Row
                        decimal total = dt.AsEnumerable().Sum(row => row.Field<int>("TotalQuantity"));
                        gvcust.FooterRow.Cells[5].Text = "Total";
                        gvcust.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                        gvcust.FooterRow.Cells[6].Text = total.ToString("N2");
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
            if (e.CommandName == "view")
            {
                Response.Redirect("Dillo_IronPacking.aspx?lotid=" + e.CommandArgument.ToString());
            }
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string unit = e.Row.Cells[2].Text;

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