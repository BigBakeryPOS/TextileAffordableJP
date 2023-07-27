using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.IO;
using System.Text;
using System.Globalization;
using System.Web.UI.HtmlControls;
namespace Billing.Accountsbootstrap
{
    public partial class DayBook : System.Web.UI.Page
    {
        double dDebit = 0;
        double dCredit = 0;
        double dDebit1 = 0;
        double dCredit1 = 0;
        string sTableName = "";
        BSClass objBs = new BSClass();
        decimal totalDebit = 0;
        decimal totalCredit = 0;
        int totalItems = 0;
        string sAdmin = "";
        double opCr = 0; 
        double opDr = 0;
        double netOp = 0;
        double opday = 0;
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        double dblSubTotalUnitPrice = 0;
        double dblSubTotalQuantity = 0;
        double dblSubTotalDiscount = 0;
        double dblSubTotalAmount = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice = 0;
        double dblGrandTotalQuantity = 0;
        double dblGrandTotalDiscount = 0;
        double dblGrandTotalAmount = 0;

        decimal totalDebit1 = 0;
        decimal totalCredit1 = 0;
        int totalItems1 = 0;
        string sAdmin1 = "";
        double opCr1 = 0;
        double opDr1 = 0;
        double netOp1 = 0;

        string strPreviousRowID1 = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex1 = 1;
        double dblSubTotalUnitPrice1 = 0;
        double dblSubTotalQuantity1= 0;
        double dblSubTotalDiscount1 = 0;
        double dblSubTotalAmount1 = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice1 = 0;
        double dblGrandTotalQuantity1 = 0;
        double dblGrandTotalDiscount1 = 0;
        double dblGrandTotalAmount1 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sTableName = string.Empty;
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            sAdmin = Session["IsSuperAdmin"].ToString();
            if (!IsPostBack)
            {
               
                txtfromdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                if (sAdmin == "1")
                {
                    ddloutlet.Enabled = true;
                    DataSet dsbranchto = objBs.Branchto();
                    ddloutlet.DataSource = dsbranchto.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                  //  ddloutlet.Items.Insert(0, "All");
                }
                else
                {
                    DataSet dsbranch = new DataSet();
                    dsbranch = objBs.Branchfrom(sTableName);
                    ddloutlet.DataSource = dsbranch.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    ddloutlet.Enabled = false;
                }
            }
        }

        protected void btngen_Click(object sender, EventArgs e)
        {
            Calculate();

            BSClass objbs = new BSClass();

            lblMessage.Text = "Day Book Report From  '" + txtfromdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            //DateTime stdt = Convert.ToDateTime(txtfromdate.Text);
            //DateTime etdt = Convert.ToDateTime(txttodate.Text);
            string Branch = ddloutlet.SelectedValue;
            DateTime stdt = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime etdt = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string dateee = string.Empty;

            DataSet ddaybookdate = new DataSet();
            ddaybookdate = objBs.getdaybookdate(Session["Yearid"].ToString());
            if (ddaybookdate.Tables[0].Rows.Count > 0)
            {
                dateee = ddaybookdate.Tables[0].Rows[0]["DayBookDate"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Daybook Not Exists.So Please Contact Administrator!!!');", true);
                return;
            }

            DateTime startDate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime Checkdate = DateTime.ParseExact(dateee, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //////DataSet dsday = objbs.generateDayBook2(stdt, etdt, Branch);

            ////////double obaldr = objbs.getLedgerOpeningBalanceday(0, "debit", Branch);
            ////////double obalcr = objbs.getLedgerOpeningBalanceday(0, "credit", Branch);

            //////double obaldr = objbs.getLedgerOpBalanceday(0, "debit", Branch, "Cash A/C _" + Branch);
            //////double obalcr = objbs.getLedgerOpBalanceday(0, "credit", Branch, "Cash A/C _" + Branch);

            //////if (dsday.Tables[0].Rows.Count > 0)
            //////{


            //////    foreach (DataRow dday in dsday.Tables[0].Rows)
            //////    {
            //////        if (dday["Debit"] != "")
            //////        {
            //////            opDr = opDr + Convert.ToDouble(dday["Debit"]);
            //////        }

            //////        if (dday["Credit"] != "")
            //////        {
            //////            opCr = opCr + Convert.ToDouble(dday["Credit"]);
            //////        }

            //////    }
            //////}

            //////opCr = opCr + obalcr;
            //////opDr = opDr + obaldr;

            //////netOp = opDr - opCr;
            if (stdt <= Checkdate)
            {
                opday = netOp1;
            }
            if (opday > 0)
            {
                dblSubTotalQuantity = dblSubTotalQuantity + opday;

            }
            else
            {
                dblSubTotalUnitPrice=dblSubTotalUnitPrice+(-(opday));
            }

            if (opday > 0)
            {

                lblopbal.Text = opday + " Dr";
            }
            else
            {
                lblopbal.Text =(-opday) + " Cr";
            }

                     //opCr = objBs.getOpening(0, 0, 0, "credit", startDate, ddloutlet.SelectedValue);
            //opDr = objBs.getOpening(0, 0, 0, "debit", startDate, ddloutlet.SelectedValue);


            //if (opDr > opCr)
            //{
            //    netOp = opDr - opCr;
            //    //lblOBDR.Text = netOp.ToString("f2");
            //    //lblOBCR.Text = "0.00";
            //}
            //else
            //{
            //    netOp = opCr - opDr;
            //    //lblOBDR.Text = "0.00";
            //    //lblOBCR.Text = netOp.ToString("f2");
            //}


            DataSet ds = objbs.generateDayBook(stdt, etdt, Branch);
       
            DataSet dstd = new DataSet();

            if (ds != null)
            {
                DataTable dt;
                DataRow drNew;
                DataColumn dc; 
                
                dt = new DataTable();
                dc = new DataColumn("Date");
                dt.Columns.Add(dc);

                dc = new DataColumn("Branchcode");
                dt.Columns.Add(dc);

                dc = new DataColumn("Particulars");
                dt.Columns.Add(dc);

                dc = new DataColumn("Narration");
                dt.Columns.Add(dc);

                dc = new DataColumn("Debit");
                dt.Columns.Add(dc);

                dc = new DataColumn("Credit");
                dt.Columns.Add(dc);

                dstd.Tables.Add(dt);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["type"].ToString() == "Sales" || dr["type"].ToString() == "Purchase")
                        {
                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            if (dr["Debit"] != "")
                            {
                                drNew["Debit"] =Convert.ToDouble(dr["Debit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Debit"] = "";

                            }
                            drNew["Credit"] = "";
                            drNew["Particulars"] = dr["Debitor"].ToString();
                            if (dr["Debitor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            drNew["Debit"] = "";
                            if (dr["Credit"] != "")
                            {
                                drNew["Credit"] = Convert.ToDouble(dr["Credit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Credit"] = "";

                            }
                            drNew["Particulars"] = dr["Creditor"].ToString();
                            if (dr["Creditor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }
                        }
                        else
                        {
                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] =dr["Date"];
                            drNew["Debit"] = "";
                            if (dr["Credit"] != "")
                            {
                                drNew["Credit"] =Convert.ToDouble(dr["Credit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Credit"] = "";
                            }
                            drNew["Particulars"] = dr["Creditor"].ToString();
                            if (dr["Creditor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            if (dr["Debit"] != "")
                            {
                                drNew["Debit"] =Convert.ToDouble(dr["Debit"]).ToString("f2");

                            }
                            drNew["Credit"] = "";
                            drNew["Particulars"] = dr["Debitor"].ToString();
                            if (dr["Debitor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                        }
                    }
                }
            }


            gvLedger.DataSource = dstd;
            gvLedger.DataBind();

           


            //Calculate();
            idt.Visible = true;
            ViewState["MyDataSet"] = dstd;
        }

        protected void gridPurchase_RowCreated(object sender, GridViewRowEventArgs e)
        {
            

            #region 1
            
                //----------start----------//
                bool IsSubTotalRowNeedToAdd = false;
                bool IsGrandTotalRowNeedtoAdd = false;
                bool IsTotalRowNeedToAdd = false;
                if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") != null))
                    if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "Date").ToString())
                    {
                        IsSubTotalRowNeedToAdd = true;
                        IsTotalRowNeedToAdd = true;
                    }
                if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") == null))
                {
                    IsSubTotalRowNeedToAdd = true;
                    IsTotalRowNeedToAdd = true;
                    intSubTotalIndex = 0;
                }
                #region Inserting first Row and populating fist Group Header details
                if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") != null))
                {
                    GridView gridPurchase = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);

                    //Adding Quantity Column            
                    cell = new TableCell();
                    if (opday > 0)
                    {
                        cell.Text = string.Format("{0:0.00}", opday);
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    //Adding Unit Price Column          
                    cell = new TableCell();
                    if (opday < 0)
                    {

                        cell.Text = string.Format("{0:0.00}", -(opday));
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
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
                    cell.Text = "Grand Total";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 3;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);

                    //Adding Quantity Column            
                    cell = new TableCell();
                    if (dblSubTotalQuantity > 0)
                    {
                        cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity);

                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);

                    //Adding Unit Price Column          
                    cell = new TableCell();
                    if (dblSubTotalUnitPrice > 0)
                    {
                        cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice);
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);

                    //Adding the Row at the RowIndex position in the Grid      
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                    #endregion
                    #region Adding Next Group Header Details
                    //if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                    //{
                    //    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //    cell = new TableCell();
                    //    cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                    //    cell.ColumnSpan = 9;
                    //    cell.CssClass = "GroupHeaderStyle";
                    //    row.Cells.Add(cell);
                    //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    //    intSubTotalIndex++;
                    //}
                    #endregion
                    #region Reseting the Sub Total Variables
                    //dblSubTotalUnitPrice = 0;
                    //dblSubTotalQuantity = 0;
                    //dblSubTotalDiscount = 0;
                    if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                    {


                        // Creating a Row      
                        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        //Adding Total Cell           
                        cell = new TableCell();
                        cell.Text = "";
                        cell.HorizontalAlign = HorizontalAlign.Left;
                        cell.ColumnSpan = 3;
                        cell.CssClass = "GrandTotalRowStyle1";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = "";
                        cell.HorizontalAlign = HorizontalAlign.Left;
                      //  cell.ColumnSpan = 3;
                        cell.CssClass = "GrandTotalRowStyle1";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = "";
                        cell.HorizontalAlign = HorizontalAlign.Left;
                      //  cell.ColumnSpan = 3;
                        cell.CssClass = "GrandTotalRowStyle1";
                        row.Cells.Add(cell);

                        //Adding Quantity Column           


                        //Adding the Row at the RowIndex position in the Grid 
                        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                        intSubTotalIndex++;



                    }
                    #endregion
                }
                if (IsTotalRowNeedToAdd)
                {
                    #region Adding Sub Total Row
                    GridView gridPurchase = (GridView)sender;
                    // Creating a Row          
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell          
                    TableCell cell = new TableCell();
                    cell.Text = "Balance";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle";
                    row.Cells.Add(cell);

                    int i = 0;
                    double ii = dblSubTotalQuantity - dblSubTotalUnitPrice;
                    
                    //Adding Quantity Column            
                    cell = new TableCell();
                    if (ii > 0)
                    {
                        cell.Text = string.Format("{0:0.00}", ii);
                        i = 1;
                        dblSubTotalQuantity = ii;
                        dblSubTotalUnitPrice = 0;
                    }
                    else
                    {
                        if (ii < 0)
                        {
                            ii = -(ii);
                        }
                        cell.Text = string.Format("{0:0.00}", "");
                        i = 0;
                        dblSubTotalQuantity = 0;
                        dblSubTotalUnitPrice = ii;
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GrandTotalRowStyle";
                    row.Cells.Add(cell);
                    //Adding Unit Price Column          
                    cell = new TableCell();
                    if (i == 1)
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", ii);
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GrandTotalRowStyle";
                    row.Cells.Add(cell);

                    //Adding the Row at the RowIndex position in the Grid      
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                    if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                    {
                      
                     
                        // Creating a Row      
                         row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        //Adding Total Cell           
                         cell = new TableCell();
                        cell.Text = "";
                        cell.HorizontalAlign = HorizontalAlign.Left;
                        cell.ColumnSpan = 3;
                        cell.CssClass = "GrandTotalRowStyle1";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = "";
                        cell.HorizontalAlign = HorizontalAlign.Left;
                        ///cell.ColumnSpan = 3;
                        cell.CssClass = "GrandTotalRowStyle1";
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = "";
                        cell.HorizontalAlign = HorizontalAlign.Left;
                        //cell.ColumnSpan = 3;
                        cell.CssClass = "GrandTotalRowStyle1";
                        row.Cells.Add(cell);

                        //Adding Quantity Column           


                        //Adding the Row at the RowIndex position in the Grid 
                        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                        intSubTotalIndex++;



                    }
                    #endregion
                    #region Adding Next Group Header Details
                    if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                    {
                        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        cell = new TableCell();
                        cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                        cell.ColumnSpan = 3;
                        cell.CssClass = "GroupHeaderStyle";
                        row.Cells.Add(cell);

                        //Adding Quantity Column            
                        cell = new TableCell();
                        if (i == 1)
                        {
                            cell.Text = string.Format("{0:0.00}", ii);
                        }
                        else
                        {
                            cell.Text = string.Format("{0:0.00}", "");
                        }
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        cell.CssClass = "GroupHeaderStyle";
                        row.Cells.Add(cell);
                        //Adding Unit Price Column          
                        cell = new TableCell();
                        if (i == 0)
                        {
                            cell.Text = string.Format("{0:0.00}", ii);
                        }
                        else
                        {
                            cell.Text = string.Format("{0:0.00}", "");
                        }
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        cell.CssClass = "GroupHeaderStyle";
                        row.Cells.Add(cell);

                        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                        intSubTotalIndex++;
                    }
                    else
                    {
                        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                    }
                    #endregion
                    #region Reseting the Sub Total Variables
                    //dblSubTotalUnitPrice = 0;
                    //dblSubTotalQuantity = 0;

                    #endregion
                }
            if(IsSubTotalRowNeedToAdd==false && IsTotalRowNeedToAdd==false)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {

                    GridView gridPurchase = (GridView)sender;
                    // Creating a Row      
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    TableCell cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    cell = new TableCell();

                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    // cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    cell = new TableCell();

                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
            }
                if (IsGrandTotalRowNeedtoAdd)
                {
                    #region Grand Total Row
                    GridView gridPurchase = (GridView)sender;
                    // Creating a Row      
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    TableCell cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    //Adding Quantity Column           
                    

                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                
                    #endregion
                }
            
            #endregion
        }

        protected void gridPur_RowCreated(object sender, GridViewRowEventArgs e)
        {


            #region 1

            //----------start----------//
            bool IsSubTotalRowNeedToAdd1 = false;
            bool IsGrandTotalRowNeedtoAdd1= false;
            bool IsTotalRowNeedToAdd1 = false;
            if ((strPreviousRowID1 != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") != null))
                if (strPreviousRowID1 != DataBinder.Eval(e.Row.DataItem, "Date").ToString())
                {
                    IsSubTotalRowNeedToAdd1 = true;
                    IsTotalRowNeedToAdd1= true;
                }
            if ((strPreviousRowID1 != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") == null))
            {
                IsSubTotalRowNeedToAdd1 = true;
                IsTotalRowNeedToAdd1 = true;
                intSubTotalIndex1 = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID1 == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") != null))
            {
                GridView gridPurchase = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                cell.ColumnSpan = 3;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column            
                cell = new TableCell();
                if (netOp1 > 0)
                {
                    cell.Text = string.Format("{0:0.00}", netOp1);
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                if (netOp1 < 0)
                {

                    cell.Text = string.Format("{0:0.00}", -(netOp1));
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);

                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd1)
            {
                #region Adding Sub Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Grand Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column            
                cell = new TableCell();
                if (dblSubTotalQuantity1 > 0)
                {
                    cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity1);

                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column          
                cell = new TableCell();
                if (dblSubTotalUnitPrice1 > 0)
                {
                    cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice1);
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;
                #endregion
                #region Adding Next Group Header Details
                //if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                //{
                //    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //    cell = new TableCell();
                //    cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                //    cell.ColumnSpan = 9;
                //    cell.CssClass = "GroupHeaderStyle";
                //    row.Cells.Add(cell);
                //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                //    intSubTotalIndex++;
                //}
                #endregion
                #region Reseting the Sub Total Variables
                //dblSubTotalUnitPrice = 0;
                //dblSubTotalQuantity = 0;
                //dblSubTotalDiscount = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {


                    // Creating a Row      
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;



                }
                #endregion
            }
            if (IsTotalRowNeedToAdd1)
            {
                #region Adding Sub Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Balance";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                int i = 0;
                double ii = dblSubTotalQuantity1 - dblSubTotalUnitPrice1;

                //Adding Quantity Column            
                cell = new TableCell();
                if (ii > 0)
                {
                    opday = ii;
                    cell.Text = string.Format("{0:0.00}", ii);
                    i = 1;
                    dblSubTotalQuantity1 = ii;
                    dblSubTotalUnitPrice1 = 0;
                }
                else
                {
                    opday = ii;
                    if (ii < 0)
                    {
                        ii = -(ii);
                    }
                    cell.Text = string.Format("{0:0.00}", "");
                    i = 0;
                    dblSubTotalQuantity1 = 0;
                    dblSubTotalUnitPrice1 = ii;
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                if (i == 1)
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", ii);
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {


                    // Creating a Row      
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    ///cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;



                }
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);

                    //Adding Quantity Column            
                    cell = new TableCell();
                    if (i == 1)
                    {
                        cell.Text = string.Format("{0:0.00}", ii);
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    //Adding Unit Price Column          
                    cell = new TableCell();
                    if (i == 0)
                    {
                        cell.Text = string.Format("{0:0.00}", ii);
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);

                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;
                }
                else
                {
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                }
                #endregion
                #region Reseting the Sub Total Variables
                //dblSubTotalUnitPrice = 0;
                //dblSubTotalQuantity = 0;

                #endregion
            }
            if (IsSubTotalRowNeedToAdd1 == false && IsTotalRowNeedToAdd1 == false)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {

                    GridView gridPurchase = (GridView)sender;
                    // Creating a Row      
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    TableCell cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    cell = new TableCell();

                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    // cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    cell = new TableCell();

                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;
                }
            }
            if (IsGrandTotalRowNeedtoAdd1)
            {
                #region Grand Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 6;
                cell.CssClass = "GrandTotalRowStyle1";
                row.Cells.Add(cell);

                //Adding Quantity Column           


                //Adding the Row at the RowIndex position in the Grid 
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;

                #endregion
            }

            #endregion
        }

        protected void Calculate()
        {
            string Branch = ddloutlet.SelectedValue;
            string varib = string.Empty;
            DateTime stdt = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime etdt = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ddaybookdate = new DataSet();
            ddaybookdate = objBs.getdaybookdate(Session["Yearid"].ToString());
            if (ddaybookdate.Tables[0].Rows.Count > 0)
            {
                varib = ddaybookdate.Tables[0].Rows[0]["DayBookDate"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Daybook Not Exists.So Please Contact Administrator!!!');", true);
                return;
            }

             DateTime stdt1 = DateTime.ParseExact(varib, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = stdt.AddDays(-1);

            DateTime startDate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataSet dsday = objBs.generateDayBook2(stdt1, todate, Branch);

            //double obaldr = objbs.getLedgerOpeningBalanceday(0, "debit", Branch);
            //double obalcr = objbs.getLedgerOpeningBalanceday(0, "credit", Branch);

            double obaldr1 = objBs.getLedgerOpBalancedaynew(0, "debit", Branch, "Cash A/C _" + Branch);
            double obalcr1 = objBs.getLedgerOpBalancedaynew(0, "credit", Branch, "Cash A/C _" + Branch);

            if (dsday.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow dday in dsday.Tables[0].Rows)
                {
                    if (dday["Debit"] != "")
                    {
                        opDr1 = opDr1 + Convert.ToDouble(dday["Debit"]);
                    }

                    if (dday["Credit"] != "")
                    {
                        opCr1 = opCr1 + Convert.ToDouble(dday["Credit"]);
                    }

                }
            }

            opCr1 = opCr1 + obalcr1;
            opDr1 = opDr1 + obaldr1;

            netOp1 = opDr1 - opCr1;

            if (netOp1 > 0)
            {
                dblSubTotalQuantity1 = dblSubTotalQuantity1 + netOp1;

            }
            else
            {
                dblSubTotalUnitPrice1 = dblSubTotalUnitPrice1 + (-(netOp1));
            }



            //opCr = objBs.getOpening(0, 0, 0, "credit", startDate, ddloutlet.SelectedValue);
            //opDr = objBs.getOpening(0, 0, 0, "debit", startDate, ddloutlet.SelectedValue);


            //if (opDr > opCr)
            //{
            //    netOp = opDr - opCr;
            //    //lblOBDR.Text = netOp.ToString("f2");
            //    //lblOBCR.Text = "0.00";
            //}
            //else
            //{
            //    netOp = opCr - opDr;
            //    //lblOBDR.Text = "0.00";
            //    //lblOBCR.Text = netOp.ToString("f2");
            //}


            DataSet ds = objBs.generateDayBook(stdt1, todate, Branch);

            DataSet dstd = new DataSet();

            if (ds != null)
            {
                DataTable dt;
                DataRow drNew;
                DataColumn dc;

                dt = new DataTable();
                dc = new DataColumn("Date");
                dt.Columns.Add(dc);

                dc = new DataColumn("Branchcode");
                dt.Columns.Add(dc);

                dc = new DataColumn("Particulars");
                dt.Columns.Add(dc);

                dc = new DataColumn("Narration");
                dt.Columns.Add(dc);

                dc = new DataColumn("Debit");
                dt.Columns.Add(dc);

                dc = new DataColumn("Credit");
                dt.Columns.Add(dc);

                dstd.Tables.Add(dt);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["type"].ToString() == "Sales" || dr["type"].ToString() == "Purchase")
                        {
                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            if (dr["Debit"] != "")
                            {
                                drNew["Debit"] = Convert.ToDouble(dr["Debit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Debit"] = "";

                            }
                            drNew["Credit"] = "";
                            drNew["Particulars"] = dr["Debitor"].ToString();
                            if (dr["Debitor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            drNew["Debit"] = "";
                            if (dr["Credit"] != "")
                            {
                                drNew["Credit"] = Convert.ToDouble(dr["Credit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Credit"] = "";

                            }
                            drNew["Particulars"] = dr["Creditor"].ToString();
                            if (dr["Creditor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }
                        }
                        else
                        {
                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            drNew["Debit"] = "";
                            if (dr["Credit"] != "")
                            {
                                drNew["Credit"] = Convert.ToDouble(dr["Credit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Credit"] = "";
                            }
                            drNew["Particulars"] = dr["Creditor"].ToString();
                            if (dr["Creditor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            if (dr["Debit"] != "")
                            {
                                drNew["Debit"] = Convert.ToDouble(dr["Debit"]).ToString("f2");

                            }
                            drNew["Credit"] = "";
                            drNew["Particulars"] = dr["Debitor"].ToString();
                            if (dr["Debitor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                        }
                    }
                }
            }

            if (dstd != null)
            {
                if (dstd.Tables[0].Rows.Count > 0)
                {
                    gvLed.DataSource = dstd;
                    gvLed.DataBind();
                }
                else
                {
                    opday = netOp1;
                }
            }
            else
            {
                opday = netOp1;
            }



         //   DateTime stDate;
         ////   stDate = Convert.ToDateTime(txtfromdate.Text.Trim());
         //   BSClass objbs = new BSClass();

         //   DateTime stdt = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
         //   DateTime etdt = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

         

         //   string Branch = ddloutlet.SelectedValue;

         //   lblOB.Text = objbs.GetDayBookOB(stdt, Branch).ToString("N2");
        }

        protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                double dblQuantity = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Debit").ToString() == "")
                {

                    dblQuantity = 0;

                }
                else
                {

                     dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit").ToString());
                }
                double dblUnitPrice = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Credit").ToString() == "")
                {
                    dblUnitPrice = 0;
                }
                else
                {
                     dblUnitPrice = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit").ToString());
                }
                    dblSubTotalUnitPrice += dblUnitPrice;
                    dblSubTotalQuantity += dblQuantity;
                    dblGrandTotalUnitPrice += dblUnitPrice;
                    dblGrandTotalQuantity += dblQuantity;
            
            }
        }


        protected void gvLed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID1 = DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                double dblQuantity1 = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Debit").ToString() == "")
                {

                    dblQuantity1 = 0;

                }
                else
                {

                    dblQuantity1 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit").ToString());
                }
                double dblUnitPrice1 = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Credit").ToString() == "")
                {
                    dblUnitPrice1 = 0;
                }
                else
                {
                    dblUnitPrice1 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit").ToString());
                }
                dblSubTotalUnitPrice1 += dblUnitPrice1;
                dblSubTotalQuantity1 += dblQuantity1;
                dblGrandTotalUnitPrice1 += dblUnitPrice1;
                dblGrandTotalQuantity1 += dblQuantity1;

            }
        }

        protected void btnexcel_Click(object sender, EventArgs e)
        {
            GridView gvdaybook = new GridView();
            gvdaybook.DataSource = objBs.AdminDaydookReport(); ;
            gvdaybook.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition",
                "attachment;filename=DayBookReport.xls");
            Response.ContentType = "applicatio/excel";
            StringWriter sw = new StringWriter(); ;
            HtmlTextWriter htm = new HtmlTextWriter(sw);
            gvdaybook.RenderControl(htm);
            Response.Write(sw.ToString());
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            DataSet myDS = (DataSet)ViewState["MyDataSet"];
          
            gvLedger.DataSource = myDS;
            gvLedger.DataBind();


            gvLedger.PagerSettings.Visible = false;
            //  GridView1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvLedger.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            //  sb.Append("test1");
            sb.Append("printWin.document.write(\"");
            sb.Append("ARK </br>");
            sb.Append("Daybook Report </br>");
            sb.Append(" </br></br>");
            sb.Append("</br>");

           // sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
           // ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvLedger.PagerSettings.Visible = true;
        }
    }
}