using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;

namespace Billing.Accountsbootstrap
{
    public partial class Customerlabelprint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double meter = 0.00;
        double Qty = 0.00;
        int count = 0;
        int iCntDesign = 1;
        bool bTotal = false;
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        double dblSubTotalUnitPrice = 0;
        double dblSubTotalQuantity = 0;
        double dblSubTotalDiscount = 0;
        double dblSubTotalRAte = 0;
        double dblSubTotalAmount = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice = 0;
        double dblGrandTotalQuantity = 0;
        double dblGrandTotalDiscount = 0;
        double dblGrandTotalAmount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string iSalesID = Request.QueryString.Get("iCutID");
            sTableName = Session["User"].ToString();
            if (iSalesID != null)
            {

                gridprint.Visible = true;
                DataSet ds2 = objBs.Cuttingprintreport(Convert.ToInt32(iSalesID));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblLot.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    lbbllott.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["deliverydate"]).ToString("dd-MM-yyyy");
                    lblwidth.Text = ds2.Tables[0].Rows[0]["width"].ToString();
                    lblfit.Text = ds2.Tables[0].Rows[0]["fit"].ToString();
                    lblcut.Text = ds2.Tables[0].Rows[0]["Cut"].ToString();
                    


                    //gridprint.DataSource = ds2;
                    //gridprint.DataBind();
                    //gridnewprint.DataSource = ds2;
                    //gridnewprint.DataBind();
                    //if (ds2.Tables[0].Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    //    {
                    //        meter = meter + Convert.ToDouble(ds2.Tables[0].Rows[i]["reqmeter"]);
                    //        Qty = Qty + Convert.ToDouble(ds2.Tables[0].Rows[i]["Qty"]);
                    //    }
                    //    Lblvalue.Text = (meter / Qty).ToString("0.00");
                    //    count = ds2.Tables[0].Rows.Count;
                    //}




                    //DataSet ds23 = objBs.gettotalqtyCuttingprintreport(Convert.ToInt32(iSalesID));
                    //if (ds23.Tables[0].Rows.Count > 0)
                    //{

                    //    GridView1.DataSource = ds23;
                    //    GridView1.DataBind();
                    //}



                    //DataSet ds234 = objBs.gettotalrateCuttingprintreport(Convert.ToInt32(iSalesID));
                    // if (ds234.Tables[0].Rows.Count > 0)
                    // {

                    //     GridView2.DataSource = ds234;
                    //     GridView2.DataBind();


                    //     double total = 90 + Convert.ToDouble(ds234.Tables[0].Rows[0]["tot"]) / Qty;
                    //     lblratee.Text = total.ToString("0.00");
                    // }
                     DataSet dlabell = objBs.getcustomerlabels(Convert.ToInt32(iSalesID));
                     if (dlabell.Tables[0].Rows.Count > 0)
                     {
                         gridlabel.DataSource = dlabell;
                         gridlabel.DataBind();
                     }

                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewcutting.aspx");
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                    #region Adding Sub Total Row
                    GridView gridPurchase = (GridView)sender;
                    // Creating a Row          
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell          
                    TableCell cell = new TableCell();
                   
                    cell.Text = ">>>>";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 20;
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
                    cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);
                    //Adding Discount Column         
                    cell = new TableCell();
                    cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);

                    //Adding Discount Column         
                    cell = new TableCell();
                    cell.Text = string.Format("{0:0.00}", dblSubTotalRAte );
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);

                    //Adding the Row at the RowIndex position in the Grid      
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    iCntDesign = 0;
                    intSubTotalIndex++;
                    iCntDesign++;
                    #endregion
                    #region Adding Next Group Header Details
                    if (DataBinder.Eval(e.Row.DataItem, "design") != null)
                    {
                        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        cell = new TableCell();
                        cell.Text = "Design : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
                        cell.ColumnSpan = 9;
                        cell.CssClass = "GroupHeaderStyle";
                        row.Cells.Add(cell);
                        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                        intSubTotalIndex++;
                        iCntDesign++;
                    }
                    #endregion
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
                    ////strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "design").ToString();

                    ////double dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvgMtr").ToString());
                    ////double dblUnitPrice = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvgRate").ToString());
                    ////double dblDiscount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MarginRAte").ToString());
                    ////double dblrate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MRPRat").ToString()); 
                    ////dblSubTotalUnitPrice += dblUnitPrice;
                    ////dblSubTotalQuantity += dblQuantity;
                    ////dblSubTotalDiscount += dblDiscount;
                    ////dblSubTotalRAte += dblrate;
                    //dblGrandTotalUnitPrice += dblUnitPrice;
                    //dblGrandTotalQuantity += dblQuantity;
                    //dblGrandTotalDiscount += dblDiscount;

                }
            
        }


    }
}