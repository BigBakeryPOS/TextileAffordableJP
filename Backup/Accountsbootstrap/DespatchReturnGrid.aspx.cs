using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusinessLayer;
using CommonLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;
using System.Drawing;

namespace Billing.Accountsbootstrap
{
    public partial class DespatchReturnGrid : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();
        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        int TotalQty = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
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
                        drpbranch.Items.Insert(0, "ALL");
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

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dss = objBs.getdespatchdetailsRet(drpbranch.SelectedValue);
                if (dss.Tables[0].Rows.Count > 0)
                {
                    gridcatqty.DataSource = dss;
                    gridcatqty.DataBind();
                }



            }
        }
        protected void drpbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = objBs.getdespatchdetailsRet(drpbranch.SelectedValue);
            if (dss.Tables[0].Rows.Count > 0)
            {
                gridcatqty.DataSource = dss;
                gridcatqty.DataBind();
            }

        }
        public void gvCustsales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
               // Response.Redirect("DespatchStock.aspx?DespatchId=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "print")
            {
                Response.Redirect("DespatchPrint.aspx?DSPRetid=" + e.CommandArgument.ToString());
            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {


            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet dss = objBs.serchdeapatchRet(fromdate, todate,drpbranch.SelectedValue);
            if (dss.Tables[0].Rows.Count > 0)
            {
                string dcno = dss.Tables[0].Rows[0]["DcNo"].ToString();
                string dcdate = Convert.ToDateTime(dss.Tables[0].Rows[0]["DcDate"]).ToString("dd/MM/yyyy");
                string ledgername = dss.Tables[0].Rows[0]["ledgername"].ToString();
                string Narration = dss.Tables[0].Rows[0]["Narration"].ToString();
                string TotalQty = dss.Tables[0].Rows[0]["TotalQty"].ToString();
                string Customer = dss.Tables[0].Rows[0]["CustomerName"].ToString();

            }

            gridcatqty.Caption = " Despatch Detail Report From " + txtFromDate.Text + " To " + txtToDate.Text + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            gridcatqty.DataSource = dss;
            gridcatqty.DataBind();
        }
        protected void gridcatqty_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalQty = TotalQty + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalQty"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total:";
                e.Row.Cells[5].Text = TotalQty.ToString();

            }
            #endregion
        }
    }
}