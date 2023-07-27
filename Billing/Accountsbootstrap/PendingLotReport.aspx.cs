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

namespace Billing.Accountsbootstrap
{
    public partial class PendingLotReport : System.Web.UI.Page
    {
        string sTableName = "";
        BSClass objBs = new BSClass();
        double amount1 = 0;
        string brach = string.Empty;
        string strPreviousRowID = string.Empty;

        double meter = 0.00;
        double Qty = 0.00;
        double totalfs = 0;
        int count = 0;
        string designcount = string.Empty;
        int iCntDesign = 1;
        bool bTotal = false;

        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        double dblSubTotalUnitPrice = 0;
        double dblSubTotalQuantity = 0;
        double dblSubTotalDiscount = 0;
        double dblSubTotalAmount = 0;
        double dblSubTotalRAte = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice = 0;
        double dblGrandTotalQuantity = 0;
        double dblGrandTotalDiscount = 0;
        double dblGrandTotalAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExport);
            divLot1.Visible = false;
            if (!IsPostBack)
            {
                reportDetails.Visible = false;
                divprecutting.Visible = false;
                divAllreport.Visible = false;
                divoverall.Visible = false;
                divPurchaseInvoice.Visible = false;

                DataSet dcust = objBs.Select_LotN();
                if (dcust != null)
                {
                    if (dcust.Tables[0].Rows.Count > 0)
                    {
                        ddlLotNo.DataSource = dcust.Tables[0];
                        ddlLotNo.DataTextField = "LotNo";
                        ddlLotNo.DataValueField = "Lotdetailid";
                        ddlLotNo.DataBind();
                        ddlLotNo.Items.Insert(0, "Select Lot No");
                        // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();
                    }
                }
            }
            selected_tab.Value = Request.Form[selected_tab.UniqueID];
        }

        protected void ddlLotNochanged(object sender, EventArgs e)
        {
            reportDetails.Visible = true;
            divAllreport.Visible = true;
            DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
            lblLot.Text = ddlLotNo.SelectedItem.Text;
            if (dshirtall.Tables[0].Rows.Count > 0)
            {
                Gridoverall.DataSource = dshirtall;
                Gridoverall.DataBind();
            }
            else
            {
                Gridoverall.DataSource = null;
                Gridoverall.DataBind();
            }
        }

        protected void btnall_Click(object sender, EventArgs e)
        {


        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {

        }

        public void gvCustsales_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }

        }

        protected void gridPurchase_RowCreated(object sender, GridViewRowEventArgs e)
        {
            #region 1

            //----------start----------//
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "empid") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "empid").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "empid") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "empid") != null))
            {
                GridView gridPurchase = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Employee/Job Worker Name : " + DataBinder.Eval(e.Row.DataItem, "name").ToString();
                cell.ColumnSpan = 7;
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
                cell.ColumnSpan = 4;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column            
                cell = new TableCell();
                cell.Text = string.Format("{0:0}", dblSubTotalQuantity);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice.ToString("N"));
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Discount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount.ToString("N"));
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "empid") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Employee/Job Worker Name : " + DataBinder.Eval(e.Row.DataItem, "name").ToString();
                    cell.ColumnSpan = 7;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalUnitPrice = 0;
                dblSubTotalQuantity = 0;
                dblSubTotalDiscount = 0;

                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "Grand Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 4;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column           
                cell = new TableCell();
                cell.Text = string.Format("{0:0}", dblGrandTotalQuantity);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalUnitPrice.ToString("N"));
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalDiscount.ToString("N"));
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid     
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }

            #endregion
        }

        protected void gridPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "empid").ToString();

                double dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "qty").ToString());
                double dblUnitPrice = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rate").ToString());
                double dblDiscount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ratee").ToString());
                dblSubTotalUnitPrice += dblUnitPrice;
                dblSubTotalQuantity += dblQuantity;
                dblSubTotalDiscount += dblDiscount;
                dblGrandTotalUnitPrice += dblUnitPrice;
                dblGrandTotalQuantity += dblQuantity;
                dblGrandTotalDiscount += dblDiscount;
            }

        }

        protected void Process_Details(object sender, EventArgs e)
        {
            DataSet dgett = new DataSet();
            var button = sender as Button;
            divprecutting.Visible = false;
            divAllreport.Visible = false;
            divoverall.Visible = true;
            divPurchaseInvoice.Visible = false;
            divLot1.Visible = false;
            string but = "All";

            if (but == "All")
            {
                //STICHING PROCESS
                divLot1.Visible = true;
                dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(ddlLotNo.SelectedValue), "3");

                DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                lblLot.Text = ddlLotNo.SelectedItem.Text;
                if (dgett.Tables[0].Rows.Count > 0)
                {
                    reportDetails.Visible = true;
                    DataSet temp = new DataSet();
                    DataTable dtt = new DataTable();

                    dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                    dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                    dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                    dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                    temp.Tables.Add(dtt);



                    for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                    {
                        int pending = 0;
                        int k = 0;
                        int total = 0;

                        string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                        for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                        {
                            if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                            {
                                DataRow dr = dtt.NewRow();
                                dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                dr["PerRate"] = dshirtall.Tables[0].Rows[j]["PerRate"].ToString();
                                dr["Date"] = Convert.ToDateTime(dshirtall.Tables[0].Rows[j]["Date"]).ToString("dd/MM/yyyy");

                                if (k == 0)
                                {
                                    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                    k = 1;
                                    //  dt.Rows.Add(dr);

                                }
                                else
                                {
                                    dr["TotalQty"] = pending.ToString();
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                }
                                temp.Tables[0].Rows.Add(dr);
                            }
                        }

                    }
                    Gridoverall.DataSource = temp;
                    Gridoverall.DataBind();
                }
                else
                {
                    Gridoverall.DataSource = null;
                    Gridoverall.DataBind();
                }


                //KAJA PROCESS

                dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(ddlLotNo.SelectedValue), "5");

                // DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                lblLot.Text = ddlLotNo.SelectedItem.Text;
                if (dgett.Tables[0].Rows.Count > 0)
                {
                    reportDetails.Visible = true;
                    DataSet temp = new DataSet();
                    DataTable dtt = new DataTable();

                    dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                    dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                    dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                    dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                    temp.Tables.Add(dtt);



                    for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                    {
                        int pending = 0;
                        int k = 0;
                        int total = 0;

                        string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                        for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                        {
                            if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                            {
                                DataRow dr = dtt.NewRow();
                                dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                dr["PerRate"] = dshirtall.Tables[0].Rows[j]["PerRate"].ToString();
                                dr["Date"] = Convert.ToDateTime(dshirtall.Tables[0].Rows[j]["Date"]).ToString("dd/MM/yyyy");

                                if (k == 0)
                                {
                                    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                    k = 1;
                                    //  dt.Rows.Add(dr);

                                }
                                else
                                {
                                    dr["TotalQty"] = pending.ToString();
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                }
                                temp.Tables[0].Rows.Add(dr);
                            }
                        }

                    }
                    //Gridoverall.DataSource = temp;
                    //Gridoverall.DataBind();
                    gridkaja.DataSource = temp;
                    gridkaja.DataBind();
                }
                else
                {
                    gridkaja.DataSource = null;
                    gridkaja.DataBind();
                }

                //EMBROIDING

                dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(ddlLotNo.SelectedValue), "6");

                //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                lblLot.Text = ddlLotNo.SelectedItem.Text;
                if (dgett.Tables[0].Rows.Count > 0)
                {
                    reportDetails.Visible = true;
                    DataSet temp = new DataSet();
                    DataTable dtt = new DataTable();

                    dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                    dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                    dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                    dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                    temp.Tables.Add(dtt);



                    for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                    {
                        int pending = 0;
                        int k = 0;
                        int total = 0;

                        string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                        for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                        {
                            if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                            {
                                DataRow dr = dtt.NewRow();
                                dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                dr["PerRate"] = dshirtall.Tables[0].Rows[j]["PerRate"].ToString();
                                dr["Date"] = Convert.ToDateTime(dshirtall.Tables[0].Rows[j]["Date"]).ToString("dd/MM/yyyy");

                                if (k == 0)
                                {
                                    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                    k = 1;
                                    //  dt.Rows.Add(dr);

                                }
                                else
                                {
                                    dr["TotalQty"] = pending.ToString();
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                }
                                temp.Tables[0].Rows.Add(dr);
                            }
                        }

                    }
                    //Gridoverall.DataSource = temp;
                    //Gridoverall.DataBind();
                    gridemb.DataSource = temp;
                    gridemb.DataBind();
                }
                else
                {
                    //Gridoverall.DataSource = null;
                    //Gridoverall.DataBind();
                    gridemb.DataSource = null;
                    gridemb.DataBind();
                }


                //WASHING

                dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(ddlLotNo.SelectedValue), "7");

                //    DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                lblLot.Text = ddlLotNo.SelectedItem.Text;
                if (dgett.Tables[0].Rows.Count > 0)
                {
                    reportDetails.Visible = true;
                    DataSet temp = new DataSet();
                    DataTable dtt = new DataTable();

                    dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                    dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                    dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                    dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                    temp.Tables.Add(dtt);



                    for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                    {
                        int pending = 0;
                        int k = 0;
                        int total = 0;

                        string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                        for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                        {
                            if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                            {
                                DataRow dr = dtt.NewRow();
                                dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                dr["PerRate"] = dshirtall.Tables[0].Rows[j]["PerRate"].ToString();
                                dr["Date"] = Convert.ToDateTime(dshirtall.Tables[0].Rows[j]["Date"]).ToString("dd/MM/yyyy");

                                if (k == 0)
                                {
                                    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                    k = 1;
                                    //  dt.Rows.Add(dr);

                                }
                                else
                                {
                                    dr["TotalQty"] = pending.ToString();
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                }
                                temp.Tables[0].Rows.Add(dr);
                            }
                        }

                    }
                    gridwash.DataSource = temp;
                    gridwash.DataBind();
                    //Gridoverall.DataSource = temp;
                    //Gridoverall.DataBind();
                }
                else
                {
                    gridwash.DataSource = null;
                    gridwash.DataBind();
                    //Gridoverall.DataSource = null;
                    //Gridoverall.DataBind();
                }


                //IRON AND PACKING
                dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(ddlLotNo.SelectedValue), "8");

                //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                lblLot.Text = ddlLotNo.SelectedItem.Text;
                if (dgett.Tables[0].Rows.Count > 0)
                {
                    reportDetails.Visible = true;
                    DataSet temp = new DataSet();
                    DataTable dtt = new DataTable();

                    dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                    dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                    dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                    dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                    dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                    temp.Tables.Add(dtt);



                    for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                    {
                        int pending = 0;
                        int k = 0;
                        int total = 0;

                        string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                        for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                        {
                            if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                            {
                                DataRow dr = dtt.NewRow();
                                dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                dr["PerRate"] = dshirtall.Tables[0].Rows[j]["PerRate"].ToString();
                                dr["Date"] = Convert.ToDateTime(dshirtall.Tables[0].Rows[j]["Date"]).ToString("dd/MM/yyyy");

                                if (k == 0)
                                {
                                    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                    k = 1;
                                    //  dt.Rows.Add(dr);

                                }
                                else
                                {
                                    dr["TotalQty"] = pending.ToString();
                                    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                    dr["Pending"] = pending.ToString();
                                    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                }
                                temp.Tables[0].Rows.Add(dr);
                            }
                        }

                    }
                    gridiron.DataSource = temp;
                    gridiron.DataBind();
                    //Gridoverall.DataSource = temp;
                    //Gridoverall.DataBind();
                }
                else
                {
                    gridiron.DataSource = null;
                    gridiron.DataBind();
                    //Gridoverall.DataSource = null;
                    //Gridoverall.DataBind();
                }
            }

            else if (button.Text == "Purchase Invoice Details")
            {
                divPurchaseInvoice.Visible = true;
                divAllreport.Visible = true;
                DataSet ds = objBs.GetLotDetailsProcessPurchaseInvoice(Convert.ToInt32(ddlLotNo.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null)
                    {
                        PurchaseInvoiceGrid.DataSource = ds;
                        PurchaseInvoiceGrid.DataBind();
                    }
                }
                else
                {
                    PurchaseInvoiceGrid.DataSource = null;
                    PurchaseInvoiceGrid.DataBind();
                }
            }

            else if (button.Text == "Stitching Details")
            {
                divAllreport.Visible = true;
                DataSet ds = objBs.GetLotDetailsProcess("tbllotprocess", "tbltranslotprocessHistory", "lotprocessId", Convert.ToInt32(ddlLotNo.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null)
                    {
                        gridPurchase.DataSource = ds;
                        gridPurchase.DataBind();
                    }
                }
                else
                {
                    gridPurchase.DataSource = null;
                    gridPurchase.DataBind();
                }
            }

            else if (button.Text == "Kaja Details")
            {
                divAllreport.Visible = true;
                DataSet ds = objBs.GetLotDetailsProcess("tblkajaprocess", "tbltranskajaprocessHistory", "kajaprocessId", Convert.ToInt32(ddlLotNo.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null)
                    {
                        gridPurchase.DataSource = ds;
                        gridPurchase.DataBind();
                    }
                }
                else
                {
                    gridPurchase.DataSource = null;
                    gridPurchase.DataBind();
                }
            }

            else if (button.Text == "Embroiding Details")
            {
                divAllreport.Visible = true;
                DataSet ds = objBs.GetLotDetailsProcess("tblEmbroidingprocess", "tblTransEmbroidingProcessHistory", "EmbroidingProcessID", Convert.ToInt32(ddlLotNo.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null)
                    {
                        gridPurchase.DataSource = ds;
                        gridPurchase.DataBind();
                    }
                }
                else
                {
                    gridPurchase.DataSource = null;
                    gridPurchase.DataBind();
                }
            }

            else if (button.Text == "Washing Details")
            {
                divAllreport.Visible = true;
                DataSet ds = objBs.GetLotDetailsProcess("tblwashingprocess", "tblTransWashingProcessHistory", "WashingProcessID", Convert.ToInt32(ddlLotNo.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null)
                    {
                        gridPurchase.DataSource = ds;
                        gridPurchase.DataBind();
                    }
                }
                else
                {
                    gridPurchase.DataSource = null;
                    gridPurchase.DataBind();
                }
            }

            else if (button.Text == "Iron & Packing Details")
            {
                divAllreport.Visible = true;
                DataSet ds = objBs.GetLotDetailsProcess("tblIronandpackProcess", "tbltransIronandpackProcessHistory", "IronandpackProcessID", Convert.ToInt32(ddlLotNo.SelectedValue),"");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null)
                    {
                        gridPurchase.DataSource = ds;
                        gridPurchase.DataBind();
                    }
                }
                else
                {
                    gridPurchase.DataSource = null;
                    gridPurchase.DataBind();
                }
            }
            selected_tab.Value = Request.Form[selected_tab.UniqueID];
        }

        protected void drpsamplechanged(object sender, EventArgs e)
        {
            reportDetails.Visible = true;
            Process_Details(sender, e);
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalfs = totalfs + Convert.ToDouble(e.Row.Cells[7].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //  e.Row.Cells[6].Text = "Total:";
                e.Row.Cells[7].Text = totalfs.ToString();
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                // totalfs = totalfs + Convert.ToDouble(e.Row.Cells[7].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        protected void gridnewprint_RowCreated(object sender, GridViewRowEventArgs e)
        {
            #region 1

            //----------start----------//
            bTotal = false;
            bool IsSubTotalRowNeedToAdd = false;

            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") != null))
            {
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "design").ToString())
                {

                    IsSubTotalRowNeedToAdd = true;
                    iCntDesign = intSubTotalIndex;
                }

            }

            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                iCntDesign = intSubTotalIndex;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
                // iCntDesign = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") != null))
            {
                GridView gridPurchase = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Design Name : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
                designcount = DataBinder.Eval(e.Row.DataItem, "design").ToString();
                cell.ColumnSpan = 10;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                iCntDesign++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                string iSalesID1 = Request.QueryString.Get("iCutID");
                int ddesgin = objBs.getcountforgroup(designcount, Convert.ToInt32(iSalesID1));

                #region Adding Sub Total Row
                //GridView gridPurchase = (GridView)sender;
                //// Creating a Row          
                //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                ////Adding Total Cell          
                //TableCell cell = new TableCell();

                //cell.Text = ">>>>";
                //cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 22;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Quantity Column            
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity / ddesgin);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);
                ////Adding Unit Price Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice / ddesgin);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);
                ////Adding Discount Column         
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount / ddesgin);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Discount Column         
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalRAte / ddesgin);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding the Row at the RowIndex position in the Grid      
                //gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                //iCntDesign = 0;
                //intSubTotalIndex++;
                //iCntDesign++;
                #endregion
                //#region Adding Next Group Header Details
                //if (DataBinder.Eval(e.Row.DataItem, "design") != null)
                //{
                //    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //    cell = new TableCell();
                //    cell.Text = "Design : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
                //    designcount = DataBinder.Eval(e.Row.DataItem, "design").ToString();
                //    cell.ColumnSpan = 9;
                //    cell.CssClass = "GroupHeaderStyle";
                //    row.Cells.Add(cell);
                //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                //    intSubTotalIndex++;
                //    iCntDesign++;
                //}
                //#endregion
                #region Reseting the Sub Total Variables
                dblSubTotalUnitPrice = 0;
                dblSubTotalQuantity = 0;
                dblSubTotalDiscount = 0;
                dblSubTotalRAte = 0;

                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                //cell.Text = "Grand Total";
                //cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 6;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Quantity Column           
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0}", dblGrandTotalQuantity);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);
                ////Adding Unit Price Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalUnitPrice);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalDiscount);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid     
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }

            #endregion

        }

        protected void gridnewprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "design").ToString();

                double dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvgMtr").ToString());
                double dblUnitPrice = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvgRate").ToString());
                double dblDiscount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MarginRAte").ToString());
                double dblrate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MRPRat").ToString());
                dblSubTotalUnitPrice += dblUnitPrice;
                dblSubTotalQuantity += dblQuantity;
                dblSubTotalDiscount += dblDiscount;
                dblSubTotalRAte += dblrate;
                //dblGrandTotalUnitPrice += dblUnitPrice;
                //dblGrandTotalQuantity += dblQuantity;
                //dblGrandTotalDiscount += dblDiscount;

            }

        }

        protected void PreCutting_GetDetails(object sender, EventArgs e)
        {
            divprecutting.Visible = true;
            divLot1.Visible = false;
            divAllreport.Visible = false;
            divoverall.Visible = true;
            divPurchaseInvoice.Visible = false;

            DataSet ds2 = objBs.Cuttingprintreport(Convert.ToInt32(ddlLotNo.SelectedValue));
            if (ds2.Tables.Count > 0)
            {
                if (ds2 != null)
                {

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        divprecutting.Visible = true;
                        lblLot.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                        lbbllott.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                        lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["deliverydate"]).ToString("dd-MM-yyyy");
                        lblwidth.Text = ds2.Tables[0].Rows[0]["width"].ToString();
                        lblfit.Text = ds2.Tables[0].Rows[0]["fit"].ToString();
                        lblcut.Text = ds2.Tables[0].Rows[0]["Cut"].ToString();

                        //gridprint.DataSource = ds2;
                        //gridprint.DataBind();
                        gridnewprint.DataSource = ds2;
                        gridnewprint.DataBind();
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                            {
                                meter = meter + Convert.ToDouble(ds2.Tables[0].Rows[i]["reqmeter"]);
                                Qty = Qty + Convert.ToDouble(ds2.Tables[0].Rows[i]["Qty"]);
                            }
                            Lblvalue.Text = (meter / Qty).ToString("0.00");
                            count = ds2.Tables[0].Rows.Count;
                        }

                        DataSet ds23 = objBs.gettotalqtyCuttingprintreportnew(Convert.ToInt32(ddlLotNo.SelectedValue));
                        if (ds23.Tables[0].Rows.Count > 0)
                        {

                            GridView1.DataSource = ds23;
                            GridView1.DataBind();
                        }

                        DataSet ds234 = objBs.gettotalrateCuttingprintreport(Convert.ToInt32(ddlLotNo.SelectedValue));
                        if (ds234.Tables[0].Rows.Count > 0)
                        {

                            GridView2.DataSource = ds234;
                            GridView2.DataBind();

                            double mrpp = Convert.ToDouble(ds234.Tables[0].Rows[0]["productcost"]);
                            double total = mrpp + Convert.ToDouble(ds234.Tables[0].Rows[0]["tot"]) / Qty;
                            lblratee.Text = total.ToString("0.00");
                            lblmrp.Text = mrpp.ToString("0.00");
                        }
                        DataSet dlabell = objBs.getcustomerlabels(Convert.ToInt32(ddlLotNo.SelectedValue));
                        if (dlabell.Tables[0].Rows.Count > 0)
                        {
                            gridlabel.DataSource = dlabell;
                            gridlabel.DataBind();
                        }
                    }
                    else
                    {
                        Gridoverall.DataSource = null;
                        Gridoverall.DataBind();
                        divprecutting.Visible = false;
                    }
                }
                else
                {
                    Gridoverall.DataSource = null;
                    Gridoverall.DataBind();
                    divprecutting.Visible = false;
                }
            }
            else
            {
                Gridoverall.DataSource = null;
                Gridoverall.DataBind();
                divprecutting.Visible = false;
            }
        }
    }
}