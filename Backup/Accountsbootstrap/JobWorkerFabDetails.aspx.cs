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
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class JobWorkerFabDetails : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double ttlissuemtr = 0; double ttlActualMeter = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dst = objBs.Getjobworkmastrr();
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddlsupplier.DataSource = dst.Tables[0];
                        ddlsupplier.DataTextField = "LedgerName";
                        ddlsupplier.DataValueField = "LedgerID";
                        ddlsupplier.DataBind();
                        ddlsupplier.Items.Insert(0, "ALL");
                    }
                }

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
                        drpbranch.Items.Insert(0, "Select Branch");
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
                        company_SelectedIndexChnaged(sender, e);
                    }
                }




            }
        }


        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {
            txtFromDate_TextChanged(sender, e);
        }
        protected void txtsearch_OnTextChanged(object sender, EventArgs e)
        {
            txtFromDate_TextChanged(sender, e);
        }
        protected void RatioShirtProcess_OnDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ttlissuemtr = ttlissuemtr + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter"));
                ttlActualMeter = ttlActualMeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualMeter"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[2].Text = "Total :";
                e.Row.Cells[4].Text = ttlissuemtr.ToString();
                e.Row.Cells[3].Text = ttlActualMeter.ToString();
            }
        }



        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {


            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet ds = objBs.gettailorfab(drpbranch.SelectedValue, fromdate, todate, ddltype.SelectedValue, txtsearch.Text, ddlsupplier.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.DataBind();
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
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            txtFromDate_TextChanged(sender, e);
        }
        protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFromDate_TextChanged(sender, e);
        }
        protected void Add_Click(object sender, EventArgs e)
        {

            //Response.Redirect("../Accountsbootstrap/Cuttingprocess.aspx");
            Response.Redirect("../Accountsbootstrap/NewPrecutprocess.aspx");

        }

        protected void Add1_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/NewPrecutprocess.aspx");

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectCheque();
            }
            else
            {
                ds = objBs.searchfilterCheque(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.PageIndex = e.NewPageIndex;
                    gvcust.DataBind();
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

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");

        }


        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lotno = e.Row.Cells[3].Text;


                if (objBs.CeckIfChequenonew(lotno))
                {
                    ((Image)e.Row.FindControl("img")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;


                    ((Image)e.Row.FindControl("dlt")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = true;
                }

            }
        }

        protected void btnexcel_OnClick(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet ds = objBs.gettailorfab(drpbranch.SelectedValue, fromdate, todate, ddltype.SelectedValue, txtsearch.Text, ddlsupplier.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.DataBind();

                    #region

                    double ttlActualMeter = 0;
                    double ttlRemainMeter = 0;

                    DataSet ndstt = new DataSet();
                    DataTable ndttt = new DataTable();
                    DataColumn ndc = new DataColumn("DCNo");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("DC Date");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("Master Name");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("ActualMeter");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("RemainMeter");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("ItemName");
                    ndttt.Columns.Add(ndc);
                    ndstt.Tables.Add(ndttt);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                      
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["DCNo"] = ds.Tables[0].Rows[i]["LotNo"].ToString();
                        ndrd["DC Date"] =Convert.ToDateTime(ds.Tables[0].Rows[i]["IssueDate"]).ToString("dd/MM/yyyy");
                        ndrd["Master Name"] =ds.Tables[0].Rows[i]["LedgerName"].ToString();
                        ndrd["ActualMeter"] = Convert.ToDouble(ds.Tables[0].Rows[i]["ActualMeter"]).ToString("f2");
                        ttlActualMeter += Convert.ToDouble(ds.Tables[0].Rows[i]["ActualMeter"].ToString());

                        ndrd["RemainMeter"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Meter"].ToString()).ToString("f2");
                        ttlRemainMeter += Convert.ToDouble(ds.Tables[0].Rows[i]["Meter"].ToString());

                        ndrd["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }



                    DataRow ndrd1 = ndstt.Tables[0].NewRow();
                    ndrd1["DCNo"] = "";
                    ndrd1["DC Date"] = "";
                    ndrd1["Master Name"] = "Total";
                    ndrd1["ActualMeter"] = ttlActualMeter.ToString("f2");
                    ndrd1["RemainMeter"] = ttlRemainMeter.ToString("f2");
                    ndrd1["ItemName"] = "";
                    ndstt.Tables[0].Rows.Add(ndrd1);

                    #endregion

                    ExportToExcel(ndstt.Tables[0]);
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
        }
        public void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                //string filename = "Sales Report.xls";
                string filename = "JobWorker FabDetails _" + DateTime.Now.ToString() + ".xls";
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
    }
}