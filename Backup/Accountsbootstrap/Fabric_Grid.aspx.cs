using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class Fabric_Grid : System.Web.UI.Page
    {
        string sTableName = "";
        BSClass objbs = new BSClass();
        double amount1 = 0;
        string brach = string.Empty;

        double billMeter = 0; double Meter = 0; double AvaliableMeter = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExport);

            if (!IsPostBack)
            {


                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //DateTime today = DateTime.Today;
                //int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                DataSet dssupplier = objbs.GetSupplierLedgername();
                if (dssupplier.Tables[0].Rows.Count > 0)
                {
                    ddlsupplier.DataSource = dssupplier.Tables[0];
                    ddlsupplier.DataTextField = "LedgerName";
                    ddlsupplier.DataValueField = "LedgerID";
                    ddlsupplier.DataBind();
                    ddlsupplier.Items.Insert(0, "ALL");
                }



                DataSet dss = new DataSet();
                dss = objbs.fabreport();
                gvCustsales.DataSource = dss;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = true;
                gvCustsales.Columns[9].Visible = true;
                gvCustsales.Columns[11].Visible = true;
                gvCustsales.Columns[12].Visible = true;
                gvCustsales.Columns[13].Visible = true;
                gvCustsales.Columns[14].Visible = true;
                gvCustsales.Columns[15].Visible = false;
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/Fabprocess.aspx");
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            DataSet dCustReport = objbs.CustomerSalesAdmin();
            gvCustsales.DataSource = dCustReport.Tables[0];
            gvCustsales.DataBind();
        }

        public void gvCustsales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Fabprocess.aspx?iid=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "print")
            {
                //Response.Redirect("Print_Fabric.aspx?iSalesID=" + e.CommandArgument.ToString());

                string yourUrl = "Print_Fabric.aspx?iSalesID=" + e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);

            }
            else if (e.CommandName == "ReturnPrint")
            {
                //Response.Redirect("Print_Fabric.aspx?iReturnPrint=" + e.CommandArgument.ToString());

                string yourUrl = "Print_Fabric.aspx?iReturnPrint=" + e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);
            }
            else if (e.CommandName == "Status")
            {

                //////DataSet dstat = objbs.getdetailedfabreport(Convert.ToInt32(e.CommandArgument.ToString()));

                DataSet dstat = objbs.Fabricreport(Convert.ToInt32(e.CommandArgument.ToString()));
                if (dstat.Tables[0].Rows.Count > 0)
                {
                    GVFullFabric.DataSource = dstat;
                    GVFullFabric.DataBind();
                }
                else
                {
                    GVFullFabric.DataSource = null;
                    GVFullFabric.DataBind();
                }
                mpenew.Show();
            }

            else if (e.CommandName == "Return")
            {

                DataSet dsReturnId = objbs.GetReturnId();
                txtreturnno.Text = dsReturnId.Tables[0].Rows[0]["ReturnId"].ToString();

                txtreturndate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dst = objbs.hrmgridviewnew();
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddlpreparedby.DataSource = dst.Tables[0];
                        ddlpreparedby.DataTextField = "Name";
                        ddlpreparedby.DataValueField = "Employee_Id";
                        ddlpreparedby.DataBind();
                        ddlpreparedby.Items.Insert(0, "Select Employee Name");
                    }
                }

                DataSet dstat = objbs.getdetailedfabreportret(Convert.ToInt32(e.CommandArgument.ToString()));
                if (dstat.Tables[0].Rows.Count > 0)
                {
                    string caption = "Fabric Return Entry " + "</br>" + "DCNo : " + dstat.Tables[0].Rows[0]["FabNo"].ToString() + " , " + "InvDate : " + Convert.ToDateTime(dstat.Tables[0].Rows[0]["InvDate"].ToString()) + "</br>" + "Supplier : " + dstat.Tables[0].Rows[0]["LedgerName"].ToString() + " , " + "LRNo : " + dstat.Tables[0].Rows[0]["LRNO"].ToString();
                    gvreturn.Caption = caption;
                    gvreturn.DataSource = dstat;
                    gvreturn.DataBind();
                }
                else
                {
                    gvreturn.DataSource = null;
                    gvreturn.DataBind();
                }
                // mpereturn.Show();

                ret.Visible = true;
            }
        }

        protected void btnexit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/Fabric_Grid.aspx");
        }
        protected void lnkDownload_OnClick(object sender, EventArgs e)
        {
            string FilePath = (sender as LinkButton).CommandName;
            if (FilePath == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Data');", true);
                return;

            }
            else
            {
                //   Response.Clear();
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
                Response.WriteFile(FilePath);
                Response.End();
            }
        }
        protected void Btncalc_OnClick(object sender, EventArgs e)
        {
            double Meter = 0;
            for (int vLoop = 0; vLoop < gvreturn.Rows.Count; vLoop++)
            {
                Label lblAvaliableMeter = (Label)gvreturn.Rows[vLoop].FindControl("lblAvaliableMeter");
                TextBox txtreturn = (TextBox)gvreturn.Rows[vLoop].FindControl("txtreturn");
                Label Transid = (Label)gvreturn.Rows[vLoop].FindControl("Transid");

                if (txtreturn.Text == "")
                {
                    txtreturn.Text = "0";
                }
                if (Convert.ToDouble(lblAvaliableMeter.Text) >= Convert.ToDouble(txtreturn.Text))
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check ReturnMeter " + txtreturn.Text + ". Thank you!!');", true);
                    return;
                }

                Meter = Meter + Convert.ToDouble(txtreturn.Text);

            }
            lbllretmeter.Text = Meter.ToString("f2") + " Meters ";
        }


        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                billMeter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "billMeter").ToString());
                Meter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter").ToString());
                AvaliableMeter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvaliableMeter").ToString());


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total";
                e.Row.Cells[6].Text = billMeter.ToString("f2");
                e.Row.Cells[7].Text = Meter.ToString("f2");
                e.Row.Cells[8].Text = AvaliableMeter.ToString("f2");

                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;
            }
        }
        protected void btnreturnsave_OnClick(object sender, EventArgs e)
        {
            if (ddlpreparedby.SelectedValue == "Select Employee Name")
            {
                // mpereturn.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select PrePared By. Thank you!!');", true);
                return;
            }
            for (int vLoop = 0; vLoop < gvreturn.Rows.Count; vLoop++)
            {
                Label lblAvaliableMeter = (Label)gvreturn.Rows[vLoop].FindControl("lblAvaliableMeter");
                TextBox txtreturn = (TextBox)gvreturn.Rows[vLoop].FindControl("txtreturn");
                Label Transid = (Label)gvreturn.Rows[vLoop].FindControl("Transid");

                if (txtreturn.Text == "")
                {
                    txtreturn.Text = "0";
                }
                if (Convert.ToDouble(lblAvaliableMeter.Text) >= Convert.ToDouble(txtreturn.Text))
                {

                }
                else
                {
                    // mpereturn.Show();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check ReturnMeter " + txtreturn.Text + ". Thank you!!');", true);
                    return;
                }
            }

            DateTime returndate = DateTime.ParseExact(txtreturndate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dsReturnId = objbs.GetReturnId();
            int ReturnId = Convert.ToInt32(dsReturnId.Tables[0].Rows[0]["ReturnId"].ToString());

            for (int vLoop = 0; vLoop < gvreturn.Rows.Count; vLoop++)
            {
                TextBox txtreturn = (TextBox)gvreturn.Rows[vLoop].FindControl("txtreturn");
                Label lbllTransid = (Label)gvreturn.Rows[vLoop].FindControl("lbllTransid");
                Label lblFabid = (Label)gvreturn.Rows[vLoop].FindControl("lblFabid");

                Label lbllAvaliableMeter = (Label)gvreturn.Rows[vLoop].FindControl("lbllAvaliableMeter");

                if (txtreturn.Text != "0" && txtreturn.Text != "")
                {
                    int save = objbs.insertreturn(ReturnId, returndate, Convert.ToInt32(ddlpreparedby.SelectedValue), Convert.ToDouble(txtreturn.Text), Convert.ToInt32(lbllTransid.Text), Convert.ToInt32(lblFabid.Text), txttransport.Text, txtnarration.Text);

                }

            }

            Response.Redirect("Fabric_Grid.aspx");
        }
        protected void rbltype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = new DataSet();
            if (rbltype.SelectedValue == "1")
            {
                dss = objbs.fabreport();
                gvCustsales.DataSource = dss;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = true;
                gvCustsales.Columns[9].Visible = true;
                gvCustsales.Columns[10].Visible = true;
                gvCustsales.Columns[11].Visible = true;
                gvCustsales.Columns[12].Visible = true;
                gvCustsales.Columns[13].Visible = true;
                gvCustsales.Columns[14].Visible = true;
                gvCustsales.Columns[15].Visible = false;


            }
            else
            {
                dss = objbs.fabreportreturn();
                gvCustsales.DataSource = dss;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = false;
                gvCustsales.Columns[9].Visible = false;
                gvCustsales.Columns[10].Visible = false;
                gvCustsales.Columns[11].Visible = false;
                gvCustsales.Columns[12].Visible = false;
                gvCustsales.Columns[13].Visible = false;
                gvCustsales.Columns[14].Visible = false;
                gvCustsales.Columns[15].Visible = true;

                ret.Visible = false;
            }


        }
        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = new DataSet();

            if (rbltype.SelectedValue == "1")
            {
                ds = objbs.fabreportdetails(fromdate, todate, ddlsupplier.SelectedValue);
                gvCustsales.DataSource = ds;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = true;
                gvCustsales.Columns[9].Visible = true;
                gvCustsales.Columns[10].Visible = true;
                gvCustsales.Columns[11].Visible = true;
                gvCustsales.Columns[12].Visible = true;
            }
            else
            {
                ds = objbs.fabreportdetailsrett(fromdate, todate, ddlsupplier.SelectedValue);
                gvCustsales.DataSource = ds;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = false;
                gvCustsales.Columns[9].Visible = false;
                gvCustsales.Columns[10].Visible = false;
                gvCustsales.Columns[11].Visible = false;
                gvCustsales.Columns[12].Visible = false;
            }
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = new DataSet();
            if (rbltype.SelectedValue == "1")
            {
                ds = objbs.fabreportdetails(fromdate, todate, ddlsupplier.SelectedValue);
                gvCustsales.DataSource = ds;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = true;
                gvCustsales.Columns[9].Visible = true;
                gvCustsales.Columns[10].Visible = true;
                gvCustsales.Columns[11].Visible = true;
                gvCustsales.Columns[12].Visible = true;
            }
            else
            {
                ds = objbs.fabreportdetailsrett(fromdate, todate, ddlsupplier.SelectedValue);
                gvCustsales.DataSource = ds;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = false;
                gvCustsales.Columns[9].Visible = false;
                gvCustsales.Columns[10].Visible = false;
                gvCustsales.Columns[11].Visible = false;
                gvCustsales.Columns[12].Visible = false;
            }
        }

        protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = new DataSet();
            if (rbltype.SelectedValue == "1")
            {
                ds = objbs.fabreportdetails(fromdate, todate, ddlsupplier.SelectedValue);
                gvCustsales.DataSource = ds;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = true;
                gvCustsales.Columns[9].Visible = true;
                gvCustsales.Columns[10].Visible = true;
                gvCustsales.Columns[11].Visible = true;
                gvCustsales.Columns[12].Visible = true;
            }
            else
            {
                ds = objbs.fabreportdetailsrett(fromdate, todate, ddlsupplier.SelectedValue);
                gvCustsales.DataSource = ds;
                gvCustsales.DataBind();

                gvCustsales.Columns[4].Visible = false;
                gvCustsales.Columns[9].Visible = false;
                gvCustsales.Columns[10].Visible = false;
                gvCustsales.Columns[11].Visible = false;
                gvCustsales.Columns[12].Visible = false;

            }
        }


        public void gvlia_comm(object sender, GridViewCommandEventArgs e)
        {
            string tranid = e.CommandArgument.ToString();
            Response.Redirect("Fabricprocess.aspx?iid=" + tranid.ToString());
        }
    }
}