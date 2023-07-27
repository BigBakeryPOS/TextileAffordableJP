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
    public partial class UnUsedFab : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();

        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        double dblSubTotalUnitPrice = 0;
        double dblSubTotalQuantity = 0;
        double dblSubTotalQuantitymtr = 0;
        double dblSubTotalDiscount = 0;
        double dblSubTotalAmount = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice = 0;
        double dblGrandTotalQuantity = 0;
        double dblGrandTotalQuantitymtr = 0;
        double dblGrandTotalDiscount = 0;
        double dblGrandTotalAmount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                DataSet dss = objBs.GetSupplierLedgername();
                if (dss.Tables[0].Rows.Count > 0)
                {
                    ddlsupplier.DataSource = dss.Tables[0];
                    ddlsupplier.DataTextField = "LedgerName";
                    ddlsupplier.DataValueField = "LedgerID";
                    ddlsupplier.DataBind();
                    ddlsupplier.Items.Insert(0, "ALL");
                }



                DataSet dscompany = objBs.Getcompanyyname();
                if (dscompany.Tables[0].Rows.Count > 0)
                {
                    ddlcompany.DataSource = dscompany.Tables[0];
                    ddlcompany.DataTextField = "CompanyName";
                    ddlcompany.DataValueField = "ComapanyID";
                    ddlcompany.DataBind();
                    ddlcompany.Items.Insert(0, "ALL");
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");



            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void gridPurchase_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void rdbCustomer_CheckedChanged(object sender, EventArgs e)
        {


        }

        protected void rbdPayMode_CheckedChanged(object sender, EventArgs e)
        {


        }



        protected void rbdProduct_CheckedChanged(object sender, EventArgs e)
        {


        }

        protected void rbdBrnd_CheckedChanged(object sender, EventArgs e)
        {


        }

        protected void rbdcatqty_CheckedChanged(object sender, EventArgs e)
        {


        }




        protected void btn_Click(object sender, EventArgs e)
        {


        }
        protected void btnlessmeter_OnClick(object sender, EventArgs e)
        {
            if (ddlmoveto.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Move To. Thank you !!!');", true);
                return;
            }
            else
            {
                for (int vLoop = 0; vLoop < gridcatqty.Rows.Count; vLoop++)
                {
                    CheckBox chkitemchecked = (CheckBox)gridcatqty.Rows[vLoop].FindControl("chkitemchecked");
                    Label Nlblitemname = (Label)gridcatqty.Rows[vLoop].FindControl("Nlblitemname");
                    Label lbllTransid = (Label)gridcatqty.Rows[vLoop].FindControl("lbllTransid");

                    Label lbllAvaliableMeter = (Label)gridcatqty.Rows[vLoop].FindControl("lbllAvaliableMeter");

                    if (chkitemchecked.Checked == true)
                    {
                        if (ddlmoveto.SelectedValue == "3")
                        {
                            int save = objBs.insertlessfab(Convert.ToDouble(lbllAvaliableMeter.Text), Convert.ToInt32(lbllTransid.Text));

                        }
                        else if (ddlmoveto.SelectedValue == "1")
                        {
                            int save = objBs.MovetoBody(Convert.ToInt32(lbllTransid.Text));
                        }
                        else if (ddlmoveto.SelectedValue == "2")
                        {
                            int save = objBs.MovetoContrast(Convert.ToInt32(lbllTransid.Text));
                        }
                    }

                }
            }

            Response.Redirect("UnUsedFab.aspx");

        }
        protected void gridcatqty_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }
        protected void gridcatqty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btn_OnClick(object sender, EventArgs e)
        {

            //  log.Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Denomination", "Denomination();", true);

        }


        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            string check = "";
            if (rdbfinished.Checked == true)
            {
                check = "Finished";
            }
            else if (rdbunfinished.Checked == true)
            {
                check = "UnFinish";
            }
            else
            {
                check = "Both";
            }

            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (sTableName == "admin")
            {
                ds1 = objBs.FabSupplierDetailsless(fromdate, todate, ddlsupplier.SelectedValue, check, ddlcompany.SelectedValue, Convert.ToDouble(txtissuemeter.Text));
            }
            else
            {
                ds1 = objBs.FabSupplierDetailsless(fromdate, todate, ddlsupplier.SelectedValue, check, ddlcompany.SelectedValue, Convert.ToDouble(txtissuemeter.Text));
            }
            gridcatqty.Caption = " COMPANY :- " + ddlcompany.SelectedItem.Text + " , " + " SUPPLIER :- " + ddlsupplier.SelectedItem.Text + " , " + " METER TYPE :- " + check + " <br /> " + " Fabric Detail Report From " + txtFromDate.Text + " To " + txtToDate.Text + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            gridcatqty.DataSource = ds1;
            gridcatqty.DataBind();
        }
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            string check = "";
            if (rdbfinished.Checked == true)
            {
                check = "Finished";
            }
            else if (rdbunfinished.Checked == true)
            {
                check = "UnFinish";
            }
            else
            {
                check = "Both";
            }

            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (sTableName == "admin")
            {
                ds1 = objBs.FabSupplierDetailsless(fromdate, todate, ddlsupplier.SelectedValue, check, ddlcompany.SelectedValue, Convert.ToDouble(txtissuemeter.Text));
            }
            else
            {
                ds1 = objBs.FabSupplierDetailsless(fromdate, todate, ddlsupplier.SelectedValue, check, ddlcompany.SelectedValue, Convert.ToDouble(txtissuemeter.Text));
            }
            gridcatqty.Caption = " COMPANY :- " + ddlcompany.SelectedItem.Text + " , " + " SUPPLIER :- " + ddlsupplier.SelectedItem.Text + " , " + " METER TYPE :- " + check + " <br /> " + " Fabric Detail Report From " + txtFromDate.Text + " To " + txtToDate.Text + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            gridcatqty.DataSource = ds1;
            gridcatqty.DataBind();
        }

        protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string check = "";
            if (rdbfinished.Checked == true)
            {
                check = "Finished";
            }
            else if (rdbunfinished.Checked == true)
            {
                check = "UnFinish";
            }
            else
            {
                check = "Both";
            }

            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (sTableName == "admin")
            {
                ds1 = objBs.FabSupplierDetailsless(fromdate, todate, ddlsupplier.SelectedValue, check, ddlcompany.SelectedValue, Convert.ToDouble(txtissuemeter.Text));
            }
            else
            {
                ds1 = objBs.FabSupplierDetailsless(fromdate, todate, ddlsupplier.SelectedValue, check, ddlcompany.SelectedValue, Convert.ToDouble(txtissuemeter.Text));
            }

            if (ddlsupplier.SelectedValue != "ALL")
            {
                DataSet pass = pass = objBs.GetAdminPass(txtadminpass.Text);
                if (pass.Tables[0].Rows.Count > 0)
                {
                    DataSet ds = objBs.GetSupplierLedgernameadd(Convert.ToInt32(ddlsupplier.SelectedValue));
                    lbladdress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    lblarea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                    lblcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                    lblmobileno.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();


                }
                else
                {
                    lbladdress.Text = "";
                    lblarea.Text = "";
                    lblcity.Text = "";
                    lblmobileno.Text = "";


                }
            }
            else
            {
                lbladdress.Text = "";
                lblarea.Text = "";
                lblcity.Text = "";
                lblmobileno.Text = "";

            }
            gridcatqty.Caption = " COMPANY :- " + ddlcompany.SelectedItem.Text + " , " + " SUPPLIER :- " + ddlsupplier.SelectedItem.Text + " , " + " METER TYPE :- " + check + " <br /> " + " Fabric Detail Report From " + txtFromDate.Text + " To " + txtToDate.Text + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            gridcatqty.DataSource = ds1;
            gridcatqty.DataBind();


        }
        protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlsupplier_SelectedIndexChanged(sender, e);
        }
        protected void txtissuemeter_OnTextChanged(object sender, EventArgs e)
        {
            ddlsupplier_SelectedIndexChanged(sender, e);
        }
        protected void txtadminpass_TextChanged(object sender, EventArgs e)
        {
            if (ddlsupplier.SelectedValue != "ALL")
            {
                DataSet pass = pass = objBs.GetAdminPass(txtadminpass.Text);
                if (pass.Tables[0].Rows.Count > 0)
                {
                    txtissuemeter.Enabled = true;
                    btnlessmeter.Enabled = true;

                    DataSet ds = objBs.GetSupplierLedgernameadd(Convert.ToInt32(ddlsupplier.SelectedValue));
                    lbladdress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    lblarea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                    lblcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                    lblmobileno.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();

                    ddlsupplier_SelectedIndexChanged(sender, e);
                }
                else
                {
                    lbladdress.Text = "";
                    lblarea.Text = "";
                    lblcity.Text = "";
                    lblmobileno.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Correct PassWord. Thank you !!!');", true);
                    ddlsupplier_SelectedIndexChanged(sender, e);
                    return;
                }
            }
            else
            {
                txtissuemeter.Enabled = true;
                btnlessmeter.Enabled = true;

                lbladdress.Text = "";
                lblarea.Text = "";
                lblcity.Text = "";
                lblmobileno.Text = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow For All Suppliers. Thank you !!!');", true);
                ddlsupplier_SelectedIndexChanged(sender, e);
                return;
            }
        }

        protected void rdbfinished_OnCheckedChanged(object sender, EventArgs e)
        {
            ddlsupplier_SelectedIndexChanged(sender, e);
        }
        protected void rdbunfinished_OnCheckedChanged(object sender, EventArgs e)
        {
            ddlsupplier_SelectedIndexChanged(sender, e);
        }
        protected void rdbboth_OnCheckedChanged(object sender, EventArgs e)
        {
            ddlsupplier_SelectedIndexChanged(sender, e);
        }

        protected void gridcatqty_RowCreated1(object sender, GridViewRowEventArgs e)
        {

            #region 5

            // if (rbproqty.Checked == true)
            {

                {

                    bool IsSubTotalRowNeedToAdd = false;
                    bool IsGrandTotalRowNeedtoAdd = false;
                    if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "LedgerName") != null))
                        if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "LedgerName").ToString())
                            IsSubTotalRowNeedToAdd = true;
                    if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "LedgerName") == null))
                    {
                        IsSubTotalRowNeedToAdd = true;
                        IsGrandTotalRowNeedtoAdd = true;
                        intSubTotalIndex = 0;
                    }
                    #region Inserting first Row and populating fist Group Header details
                    if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "LedgerName") != null))
                    {
                        GridView gridPurchase = (GridView)sender;
                        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        TableCell cell = new TableCell();
                        cell.Text = "SUPPLIER NAME :- " + DataBinder.Eval(e.Row.DataItem, "LedgerName").ToString();
                        cell.ColumnSpan = 9;
                        cell.CssClass = "GroupHeaderStyle";
                        row.Cells.Add(cell);
                        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                        intSubTotalIndex++;
                    }
                    #endregion
                    if (IsSubTotalRowNeedToAdd)
                    {
                        #region Adding Sub Total Row
                        GridView gridPurchase = (GridView)sender;
                        // Creating a Row          
                        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        //Adding Total Cell          
                        TableCell cell = new TableCell();
                        cell.Text = "Sub Total";
                        cell.HorizontalAlign = HorizontalAlign.Left;
                        cell.ColumnSpan = 6;
                        cell.CssClass = "SubTotalRowStyle";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = string.Format("{0:0.00}", dblSubTotalQuantitymtr);
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        cell.CssClass = "SubTotalRowStyle";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity);
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        cell.CssClass = "SubTotalRowStyle";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        // cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity);
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        cell.CssClass = "SubTotalRowStyle";
                        row.Cells.Add(cell);




                        //Adding the Row at the RowIndex position in the Grid      
                        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                        intSubTotalIndex++;
                        #endregion
                        #region Adding Next Group Header Details
                        if (DataBinder.Eval(e.Row.DataItem, "LedgerName") != null)
                        {
                            row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                            cell = new TableCell();
                            cell.Text = "SUPPLIER NAME :- " + DataBinder.Eval(e.Row.DataItem, "LedgerName").ToString();
                            cell.ColumnSpan = 9;
                            cell.CssClass = "GroupHeaderStyle";
                            row.Cells.Add(cell);
                            gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                            intSubTotalIndex++;
                        }
                        #endregion
                        #region Reseting the Sub Total Variables
                        dblSubTotalUnitPrice = 0;
                        dblSubTotalQuantity = 0;
                        dblSubTotalQuantitymtr = 0;
                        dblSubTotalDiscount = 0;

                        #endregion
                    }
                    if (IsGrandTotalRowNeedtoAdd)
                    {
                        #region Grand Total Row
                        GridView gridPurchase = (GridView)sender;

                        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                        TableCell cell = new TableCell();
                        cell.Text = "Grand Total";
                        cell.HorizontalAlign = HorizontalAlign.Left;
                        cell.ColumnSpan = 6;
                        cell.CssClass = "GrandTotalRowStyle";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = string.Format("{0:0.00}", dblGrandTotalQuantitymtr);
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        cell.CssClass = "GrandTotalRowStyle";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = string.Format("{0:0.00}", dblGrandTotalQuantity);
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        cell.CssClass = "GrandTotalRowStyle";
                        row.Cells.Add(cell);




                        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                        #endregion
                    }
                }
            }
            #endregion


        }

        protected void gridcatqty_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            // if (rbproqty.Checked == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DataRow pr = ((DataRowView)e.Row.DataItem).Row;
                    //string Meterrow = pr["Meter"].ToString();
                    //string AvaliableMeterrow = pr["AvaliableMeter"].ToString();

                    //if (Meterrow == AvaliableMeterrow)
                    //{
                    //    e.Row.BackColor = System.Drawing.Color.Gray;
                    //    e.Row.ForeColor = System.Drawing.Color.Black;
                    //}

                    strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "LedgerName").ToString();

                    double Meter = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter").ToString());
                    double dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvaliableMeter").ToString());

                    dblSubTotalQuantity += dblQuantity;
                    dblSubTotalQuantitymtr += Meter;

                    dblGrandTotalQuantity += dblQuantity;
                    dblGrandTotalQuantitymtr += Meter;

                }
            }
        }


    }
}