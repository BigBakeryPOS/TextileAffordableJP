using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;


namespace Billing.Accountsbootstrap
{
    public partial class Trim : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string Trimmingid = Request.QueryString.Get("Trimmingid");
            sTableName = Session["User"].ToString();
            if (Trimmingid != null)
            {

                DataSet ds2 = objBs.JpTrimmingfor(Trimmingid);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblLot.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    lblLedgerName.Text = ds2.Tables[0].Rows[0]["name"].ToString();
                    lblPaidAmount.Text = ds2.Tables[0].Rows[0]["PaidAmount"].ToString();
                    lblTotalAmount.Text = ds2.Tables[0].Rows[0]["TotalAmount"].ToString();
                    lblTotalQty.Text = ds2.Tables[0].Rows[0]["Totalqty"].ToString();

                    DataSet ds23 = objBs.Get_JpTrim(Convert.ToString(Trimmingid), "10", "Trm");
                    if (ds23.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds23;
                        GridView1.DataBind();
                    }

                }
            }
        }

        protected void btnclick(object sender, EventArgs e)
        {
            //   Response.Redirect("StitchingGrid.aspx");

            string Trimmingid = Request.QueryString.Get("Trimmingid");

            int isucess = objBs.updprintforall(Trimmingid, "tbltransjpTrimminghistory", "TrimmingId");
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrimmingGrid.aspx");
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    totalfs = totalfs + Convert.ToDouble(e.Row.Cells[13].Text);
            //    //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //    // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    //  e.Row.Cells[6].Text = "Total:";
            //    e.Row.Cells[13].Text = totalfs.ToString();
            //    e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            //    // totalfs = totalfs + Convert.ToDouble(e.Row.Cells[7].Text);
            //    //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //    // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            //}
        }

        protected void gridnewprint_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //#region 1

            ////----------start----------//
            //bTotal = false;
            //bool IsSubTotalRowNeedToAdd = false;

            //bool IsGrandTotalRowNeedtoAdd = false;
            //if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") != null))
            //{
            //    if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "design").ToString())
            //    {

            //        IsSubTotalRowNeedToAdd = true;
            //        iCntDesign = intSubTotalIndex;
            //    }

            //}

            //if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") == null))
            //{
            //    IsSubTotalRowNeedToAdd = true;
            //    iCntDesign = intSubTotalIndex;
            //    IsGrandTotalRowNeedtoAdd = true;
            //    intSubTotalIndex = 0;
            //    // iCntDesign = 0;
            //}
            //#region Inserting first Row and populating fist Group Header details
            //if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") != null))
            //{
            //    GridView gridPurchase = (GridView)sender;
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    TableCell cell = new TableCell();
            //    cell.Text = "Design Name : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
            //    designcount = DataBinder.Eval(e.Row.DataItem, "design").ToString();
            //    cell.ColumnSpan = 10;
            //    cell.CssClass = "GroupHeaderStyle";
            //    row.Cells.Add(cell);
            //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //    intSubTotalIndex++;
            //    iCntDesign++;
            //}
            //#endregion
            //if (IsSubTotalRowNeedToAdd)
            //{
            //    string iSalesID1 = Request.QueryString.Get("iCutID");
            //    int ddesgin = objBs.getcountforgroup(designcount, Convert.ToInt32(iSalesID1));

            //    #region Adding Sub Total Row
            //    GridView gridPurchase = (GridView)sender;
            //    // Creating a Row          
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    //Adding Total Cell          
            //    TableCell cell = new TableCell();

            //    cell.Text = ">>>>";
            //    cell.HorizontalAlign = HorizontalAlign.Left;
            //    cell.ColumnSpan = 22;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Quantity Column            
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity / ddesgin);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);
            //    //Adding Unit Price Column          
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice / ddesgin);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);
            //    //Adding Discount Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount / ddesgin);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Discount Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalRAte / ddesgin);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding the Row at the RowIndex position in the Grid      
            //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //    iCntDesign = 0;
            //    intSubTotalIndex++;
            //    iCntDesign++;
            //    #endregion
            //    #region Adding Next Group Header Details
            //    if (DataBinder.Eval(e.Row.DataItem, "design") != null)
            //    {
            //        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //        cell = new TableCell();
            //        cell.Text = "Design : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
            //        designcount = DataBinder.Eval(e.Row.DataItem, "design").ToString();
            //        cell.ColumnSpan = 9;
            //        cell.CssClass = "GroupHeaderStyle";
            //        row.Cells.Add(cell);
            //        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //        intSubTotalIndex++;
            //        iCntDesign++;
            //    }
            //    #endregion
            //    #region Reseting the Sub Total Variables
            //    dblSubTotalUnitPrice = 0;
            //    dblSubTotalQuantity = 0;
            //    dblSubTotalDiscount = 0;
            //    dblSubTotalRAte = 0;

            //    #endregion
            //}
            //if (IsGrandTotalRowNeedtoAdd)
            //{
            //    #region Grand Total Row
            //    GridView gridPurchase = (GridView)sender;
            //    // Creating a Row      
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    //Adding Total Cell           
            //    TableCell cell = new TableCell();
            //    //cell.Text = "Grand Total";
            //    //cell.HorizontalAlign = HorizontalAlign.Left;
            //    //cell.ColumnSpan = 6;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);

            //    ////Adding Quantity Column           
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0}", dblGrandTotalQuantity);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);
            //    ////Adding Unit Price Column          
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0.00}", dblGrandTotalUnitPrice);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0.00}", dblGrandTotalDiscount);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);

            //    //Adding the Row at the RowIndex position in the Grid     
            //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
            //    #endregion
            //}

            //#endregion

        }

        protected void gridnewprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "design").ToString();

                //double dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvgMtr").ToString());
                //double dblUnitPrice = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvgRate").ToString());
                //double dblDiscount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MarginRAte").ToString());
                //double dblrate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MRPRat").ToString());
                //dblSubTotalUnitPrice += dblUnitPrice;
                //dblSubTotalQuantity += dblQuantity;
                //dblSubTotalDiscount += dblDiscount;
                //dblSubTotalRAte += dblrate;
                //dblGrandTotalUnitPrice += dblUnitPrice;
                //dblGrandTotalQuantity += dblQuantity;
                //dblGrandTotalDiscount += dblDiscount;

            }

        }


    }
}