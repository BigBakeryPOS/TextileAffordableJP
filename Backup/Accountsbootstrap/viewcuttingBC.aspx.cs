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
    public partial class viewcuttingBC : System.Web.UI.Page
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
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {
                DataSet ds = objBs.selectCutprocessbc(drpbranch.SelectedValue);
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
        }
        protected void RatioShirtProcess_OnDataBound(object sender, GridViewRowEventArgs e)
        {
            string cMeter = "";
            string cActualMeter = "";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                cMeter = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter")).ToString("f2");
                cActualMeter = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualMeter")).ToString("f2");

                ttlissuemtr = ttlissuemtr + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter"));
                ttlActualMeter = ttlActualMeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualMeter"));

             
                if (Convert.ToDouble(cMeter) == Convert.ToDouble(cActualMeter))
                {
                    ((System.Web.UI.WebControls.Image)e.Row.FindControl("Image1")).Visible = true;
                    ((ImageButton)e.Row.FindControl("imgdisabledel")).Visible = false;
                }
                else
                {
                    ((System.Web.UI.WebControls.Image)e.Row.FindControl("Image1")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisabledel")).Visible = true;
                }
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

            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {
                DataSet ds = objBs.selectCutprocessbcrequired(drpbranch.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue);
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

        }
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {
                DataSet ds = objBs.selectCutprocessbcrequired(drpbranch.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue);
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
        }
        protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {
                DataSet ds = objBs.selectCutprocessbcrequired(drpbranch.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue);
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
        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchviewprocess(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
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
        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("NewPrecutprocessBC.aspx?BCID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "Delete")
            {

                int upBCCutFab = objBs.updateBCCutFab(Convert.ToInt32(e.CommandArgument.ToString()));
                Response.Redirect("viewcuttingBC.aspx");
            }
            else if (e.CommandName == "print")
            {
                Response.Redirect("BCPreviewLot.aspx?iCutID=" + e.CommandArgument.ToString());
            }

            else if (e.CommandName == "custprint")
            {
                Response.Redirect("PrintCuttingnew.aspx?iCutID=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "labelprint")
            {
                Response.Redirect("Customerlabelprint.aspx?iCutID=" + e.CommandArgument.ToString());
            }
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
    }
}