using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using CommonLayer;
using System.Text;
using System.Data;

namespace Billing.Accountsbootstrap
{
    public partial class Dillo_EmbJobWorkPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
                string iEmbPrintID = Request.QueryString.Get("EmbId");
                sTableName = Session["User"].ToString();
                if (iEmbPrintID != null)
                {

                    gridprint.Visible = true;
                    DataSet ds = objBs.EmbJobWorkPrint(Convert.ToInt32(iEmbPrintID));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblDCNo.Text = ds.Tables[0].Rows[0]["Dcno"].ToString();
                        lblJobWorkerName.Text = ds.Tables[0].Rows[0]["JobWorkerName"].ToString(); //Convert.ToDateTime(ds.Tables[0].Rows[0]["RegDate"].ToString()).ToString("dd/MM/yyyy");
                        //lblLotNo.Text = ds.Tables[0].Rows[0]["Lotno"].ToString();
                        lblSupervisorName.Text = ds.Tables[0].Rows[0]["Supervisor"].ToString();
                        lblTotQty.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalQty"]).ToString();
                        lblDCdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DCDate"].ToString()).ToString("dd/MM/yyyy");
                        lbldesignno.Text = ds.Tables[0].Rows[0]["desgnNo"].ToString();
                        gridprint.DataSource = ds;
                        gridprint.DataBind();

                    }
                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dillo_EmbworkGrid.aspx");
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //    // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            //}
        }
    }
}