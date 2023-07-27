using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;


namespace Billing.Accountsbootstrap
{
    public partial class PrintCutting : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double meter = 0.00;
        double Qty = 0.00;
        double totalfs = 0;
        int count = 0;
        string designcount = string.Empty;
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
                    DataSet dsize = objBs.Getsizetype();
                    if (dsize != null)
                    {
                        if (dsize.Tables[0].Rows.Count > 0)
                        {
                            chkSizes.DataSource = dsize.Tables[0];
                            chkSizes.DataTextField = "Size";
                            chkSizes.DataValueField = "Sizeid";
                            chkSizes.DataBind();
                        }
                    }



                    lblLot.Text = ds2.Tables[0].Rows[0]["CompanyFullLotNo"].ToString();
                    lbbllott.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["deliverydate"]).ToString("dd-MM-yyyy");
                    lblwidth.Text = ds2.Tables[0].Rows[0]["width"].ToString();
                    lblfit.Text = ds2.Tables[0].Rows[0]["fitid"].ToString();
                    lblcut.Text = ds2.Tables[0].Rows[0]["Cut"].ToString();


                    DataSet dsizee = objBs.Getfitseize(lblfit.Text);
                    if ((dsizee.Tables[0].Rows.Count > 0))
                    {

                        for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                        {

                            string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();




                            {

                                chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                            }

                        }
                    }


                    //gridprint.DataSource = ds2;
                    //gridprint.DataBind();
                    gridnewprint.DataSource = ds2;
                    gridnewprint.DataBind();



                    if (chkSizes.SelectedIndex >= 0)
                    {
                        gridnewprint.Columns[6].Visible = false; //30FS
                        gridnewprint.Columns[7].Visible = false; //32FS

                        gridnewprint.Columns[8].Visible = false;//34Fs
                        gridnewprint.Columns[9].Visible = false;//36Fs

                        gridnewprint.Columns[10].Visible = false; //XSFS
                        gridnewprint.Columns[11].Visible = false; //SFS

                        gridnewprint.Columns[12].Visible = false; //MFS
                        gridnewprint.Columns[13].Visible = false; //LFS

                        gridnewprint.Columns[14].Visible = false; //XLFS
                        gridnewprint.Columns[15].Visible = false; //xxlFS

                        gridnewprint.Columns[16].Visible = false; //3xlHS
                        gridnewprint.Columns[17].Visible = false; //4xlHS

                        gridnewprint.Columns[18].Visible = false; //30HS

                        gridnewprint.Columns[19].Visible = false; //32HS

                        gridnewprint.Columns[20].Visible = false; //34HS
                        gridnewprint.Columns[21].Visible = false; //36HS

                        gridnewprint.Columns[22].Visible = false; //XSHS
                        gridnewprint.Columns[23].Visible = false; //SHS

                        gridnewprint.Columns[24].Visible = false; //MHS
                        gridnewprint.Columns[25].Visible = false; //LHS

                        gridnewprint.Columns[26].Visible = false; //XLHS
                        gridnewprint.Columns[27].Visible = false; //XXLHS

                        gridnewprint.Columns[28].Visible = false; //3XLHS
                        gridnewprint.Columns[29].Visible = false; //4XLHS




                        int lop = 0;
                        //Loop through each item of checkboxlist
                        foreach (ListItem item in chkSizes.Items)
                        {
                            //check if item selected

                            if (item.Selected)
                            {

                                {
                                    if (item.Text == "30FS")
                                    {
                                        gridnewprint.Columns[6].Visible = true;
                                    }
                                    if (item.Text == "32FS")
                                    {
                                        gridnewprint.Columns[7].Visible = true;
                                    }
                                    if (item.Text == "34FS")
                                    {
                                        gridnewprint.Columns[8].Visible = true;
                                    }
                                    if (item.Text == "36FS")
                                    {
                                        gridnewprint.Columns[9].Visible = true;
                                    }
                                    if (item.Text == "XSFS")
                                    {
                                        gridnewprint.Columns[10].Visible = true;
                                    }
                                    if (item.Text == "SFS")
                                    {
                                        gridnewprint.Columns[11].Visible = true;
                                    }
                                    if (item.Text == "MFS")
                                    {
                                        gridnewprint.Columns[12].Visible = true;
                                    }
                                    if (item.Text == "LFS")
                                    {
                                        gridnewprint.Columns[13].Visible = true;
                                    }
                                    if (item.Text == "XLFS")
                                    {
                                        gridnewprint.Columns[14].Visible = true;
                                    }
                                    if (item.Text == "XXLFS")
                                    {
                                        gridnewprint.Columns[15].Visible = true;
                                    }
                                    if (item.Text == "3XLFS")
                                    {
                                        gridnewprint.Columns[16].Visible = true;
                                    }
                                    if (item.Text == "4XLFS")
                                    {
                                        gridnewprint.Columns[17].Visible = true;
                                    }


                                    // FOR HS

                                    if (item.Text == "30HS")
                                    {
                                        gridnewprint.Columns[18].Visible = true;
                                    }

                                    if (item.Text == "32HS")
                                    {
                                        gridnewprint.Columns[19].Visible = true;
                                    }

                                    if (item.Text == "34HS")
                                    {
                                        gridnewprint.Columns[20].Visible = true;
                                    }

                                    if (item.Text == "36HS")
                                    {
                                        gridnewprint.Columns[21].Visible = true;

                                    }

                                    if (item.Text == "XSHS")
                                    {
                                        gridnewprint.Columns[22].Visible = true;
                                    }

                                    if (item.Text == "SHS")
                                    {
                                        gridnewprint.Columns[23].Visible = true;
                                    }

                                    if (item.Text == "MHS")
                                    {
                                        gridnewprint.Columns[24].Visible = true;
                                    }

                                    if (item.Text == "LHS")
                                    {
                                        gridnewprint.Columns[25].Visible = true;
                                    }

                                    if (item.Text == "XLHS")
                                    {
                                        gridnewprint.Columns[26].Visible = true;
                                    }

                                    if (item.Text == "XXLHS")
                                    {
                                        gridnewprint.Columns[27].Visible = true;
                                    }

                                    if (item.Text == "3XLHS")
                                    {
                                        gridnewprint.Columns[28].Visible = true;
                                    }

                                    if (item.Text == "4XLHS")
                                    {
                                        gridnewprint.Columns[29].Visible = true;
                                    }





                                    lop++;

                                }
                            }
                        }
                        //gvcustomerorder.DataSource = dssmer;
                        //gvcustomerorder.DataBind();
                    }
                    else
                    {
                        gridnewprint.Columns[6].Visible = false; //30FS
                        gridnewprint.Columns[7].Visible = false; //32FS

                        gridnewprint.Columns[8].Visible = false;//34Fs
                        gridnewprint.Columns[9].Visible = false;//36Fs

                        gridnewprint.Columns[10].Visible = false; //XSFS
                        gridnewprint.Columns[11].Visible = false; //SFS

                        gridnewprint.Columns[12].Visible = false; //MFS
                        gridnewprint.Columns[13].Visible = false; //LFS

                        gridnewprint.Columns[14].Visible = false; //XLFS
                        gridnewprint.Columns[15].Visible = false; //xxlFS

                        gridnewprint.Columns[16].Visible = false; //3xlHS
                        gridnewprint.Columns[17].Visible = false; //4xlHS

                        gridnewprint.Columns[18].Visible = false; //30HS

                        gridnewprint.Columns[19].Visible = false; //32HS

                        gridnewprint.Columns[20].Visible = false; //34HS
                        gridnewprint.Columns[21].Visible = false; //36HS

                        gridnewprint.Columns[22].Visible = false; //XSHS
                        gridnewprint.Columns[23].Visible = false; //SHS

                        gridnewprint.Columns[24].Visible = false; //MHS
                        gridnewprint.Columns[25].Visible = false; //LHS

                        gridnewprint.Columns[26].Visible = false; //XLHS
                        gridnewprint.Columns[27].Visible = false; //XXLHS

                        gridnewprint.Columns[28].Visible = false; //3XLHS
                        gridnewprint.Columns[29].Visible = false; //4XLHS

                    }



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




                    DataSet ds23 = objBs.gettotalqtyCuttingprintreportnew(Convert.ToInt32(iSalesID));
                    if (ds23.Tables[0].Rows.Count > 0)
                    {

                        GridView1.DataSource = ds23;
                        GridView1.DataBind();
                    }



                    DataSet ds234 = objBs.gettotalrateCuttingprintreport(Convert.ToInt32(iSalesID));
                     if (ds234.Tables[0].Rows.Count > 0)
                     {

                         GridView2.DataSource = ds234;
                         GridView2.DataBind();

                         double mrpp = Convert.ToDouble(ds234.Tables[0].Rows[0]["productcost"]);
                         double total = mrpp + Convert.ToDouble(ds234.Tables[0].Rows[0]["tot"]) / Qty;
                         lblratee.Text = total.ToString("0.00");
                         lblmrp.Text = mrpp.ToString("0.00");
                     }

                     DataSet dsfablist = objBs.gettotalfablistreport(Convert.ToInt32(iSalesID));
                     if (dsfablist.Tables[0].Rows.Count > 0)
                     {
                         fablistcalcalcuationgrid.DataSource = dsfablist;
                         fablistcalcalcuationgrid.DataBind();
                     }

                     //DataSet dlabell = objBs.getcustomerlabels(Convert.ToInt32(iSalesID));
                     //if (dlabell.Tables[0].Rows.Count > 0)
                     //{
                     //    gridlabel.DataSource = dlabell;
                     //    gridlabel.DataBind();
                     //}

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
                 totalfs = totalfs + Convert.ToDouble(e.Row.Cells[13].Text);
             //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
            else if(e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total:";
                e.Row.Cells[13].Text = totalfs.ToString();
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
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
                    GridView gridPurchase = (GridView)sender;
                    // Creating a Row          
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell          
                    TableCell cell = new TableCell();
                   
                    cell.Text = ">>>>";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 22;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);

                    //Adding Quantity Column            
                    cell = new TableCell();
                    cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity / ddesgin);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);
                    //Adding Unit Price Column          
                    cell = new TableCell();
                    cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice / ddesgin);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);
                    //Adding Discount Column         
                    cell = new TableCell();
                    cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount / ddesgin);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "SubTotalRowStyle";
                    row.Cells.Add(cell);

                    //Adding Discount Column         
                    cell = new TableCell();
                    cell.Text = string.Format("{0:0.00}", dblSubTotalRAte / ddesgin);
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
                        designcount = DataBinder.Eval(e.Row.DataItem, "design").ToString();
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


    }
}