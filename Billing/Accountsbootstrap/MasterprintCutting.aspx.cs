using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;

namespace Billing.Accountsbootstrap
{
    public partial class MasterprintCutting : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double meter = 0.00;
        double Qty = 0.00;
        double totfabr = 0.00;
        int count = 0;
        double totalfs = 0;
        int iCntDesign = 1;
        string designcount = string.Empty;
        string designcount1 = string.Empty;
        bool bTotal = false;
        string ids = string.Empty;
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

        double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;
        double TotFS = 0; double TotHS = 0; double fhsQty = 0;

        string strPreviousRowID1 = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex1 = 1;
        double dblSubTotalUnitPrice1 = 0;
        double dblSubTotalQuantity1 = 0;
        double dblSubTotalDiscount1 = 0;
        double dblSubTotalRAte1 = 0;
        double dblSubTotalAmount1 = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice1 = 0;
        double dblGrandTotalQuantity1 = 0;
        double dblGrandTotalDiscount1 = 0;
        double dblGrandTotalAmount1 = 0;
        double trate = 0.00;
        double tmet = 0.00;


        double totrawmat = 0;
        double perrawmaterila = 0;
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
                DataSet getlot = new DataSet();
                getlot = objBs.getlotnumberformasteridval1(Convert.ToInt32(iSalesID));
                if (getlot.Tables[0].Rows.Count > 0)
                {
                    //ids = getlot.Tables[0].Rows[0]["LotNo"].ToString();
                    ids = getlot.Tables[0].Rows[0]["cutid"].ToString();
                }

                gridprint.Visible = true;
                DataSet ds2 = objBs.Cuttingprintreport(Convert.ToInt32(ids));
                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {



                        lbbllott.Text = getlot.Tables[0].Rows[0]["cutid"].ToString();
                        //gridprint.DataSource = ds2;
                        //gridprint.DataBind();
                        gridnewprint.DataSource = ds2;
                        gridnewprint.DataBind();
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
                    }
                }


                string super = Session["IsSuperAdmin"].ToString();


                if (super == "1")
                {
                    DataSet getval = objBs.getcostprocessval(iSalesID);
                    if (getval.Tables[0].Rows.Count > 0)
                    {

                        gvprocessaccesscost.DataSource = getval;
                        gvprocessaccesscost.DataBind();

                    }
                }
                else
                {
                    gvprocessaccesscost.Visible = false;
                }
                DataSet drawmater = objBs.getusedrawmaterials(iSalesID);
                if (drawmater.Tables[0].Rows.Count > 0)
                {
                    gridrawmaterial.DataSource = drawmater;
                    gridrawmaterial.DataBind();
                }
                else
                {
                    gridrawmaterial.DataSource = null;
                    gridrawmaterial.DataBind();
                }

                DataSet dlabell1 = objBs.MAsterCuttingprintreport(Convert.ToInt32(iSalesID));
                if (dlabell1.Tables[0].Rows.Count > 0)
                {
                    lblitemnarrations.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();


                    lblShirtDescription.Text = dlabell1.Tables[0].Rows[0]["Design"].ToString() + " - " + dlabell1.Tables[0].Rows[0]["Fit"].ToString();

                    lblLot.Text = dlabell1.Tables[0].Rows[0]["lotno"].ToString();
                    // lbbllott.Text = dlabell1.Tables[0].Rows[0]["lotno"].ToString();
                    lblDeldate.Text = Convert.ToDateTime(dlabell1.Tables[0].Rows[0]["deliverydate"]).ToString("dd-MM-yyyy");
                    lblwidth.Text = dlabell1.Tables[0].Rows[0]["width"].ToString();
                    lblfit.Text = ds2.Tables[0].Rows[0]["fit"].ToString();
                    lblcut.Text = getlot.Tables[0].Rows[0]["LedgerName"].ToString();

                    lbllbrand.Text = dlabell1.Tables[0].Rows[0]["LedgerName"].ToString();

                    gridnewmaster.DataSource = dlabell1;
                    gridnewmaster.DataBind();

                    gridnewmaster.Columns[5].Visible = false;
                    gridnewmaster.Columns[6].Visible = false;
                    gridnewmaster.Columns[7].Visible = false;
                    gridnewmaster.Columns[8].Visible = false;
                    gridnewmaster.Columns[9].Visible = false;
                    gridnewmaster.Columns[10].Visible = false;
                    gridnewmaster.Columns[11].Visible = false;
                    gridnewmaster.Columns[12].Visible = false;
                    gridnewmaster.Columns[13].Visible = false;
                    gridnewmaster.Columns[14].Visible = false;
                    gridnewmaster.Columns[15].Visible = false;
                    gridnewmaster.Columns[16].Visible = false;
                    gridnewmaster.Columns[17].Visible = false;

                    gridnewmaster.Columns[18].Visible = false;
                    gridnewmaster.Columns[19].Visible = false;
                    gridnewmaster.Columns[20].Visible = false;
                    gridnewmaster.Columns[21].Visible = false;
                    gridnewmaster.Columns[22].Visible = false;
                    gridnewmaster.Columns[23].Visible = false;
                    gridnewmaster.Columns[24].Visible = false;
                    gridnewmaster.Columns[25].Visible = false;
                    gridnewmaster.Columns[26].Visible = false;
                    gridnewmaster.Columns[27].Visible = false;
                    gridnewmaster.Columns[28].Visible = false;
                    gridnewmaster.Columns[29].Visible = false;

                    gridnewmaster.Columns[30].Visible = false;
                    //gridnewmaster.Columns[31].Visible = false;



                    #region
                    for (int j = 0; j < dlabell1.Tables[0].Rows.Count; j++)
                    {
                        string S30 = dlabell1.Tables[0].Rows[j]["30FS"].ToString();
                        string S32 = dlabell1.Tables[0].Rows[j]["32FS"].ToString();
                        string S34 = dlabell1.Tables[0].Rows[j]["34FS"].ToString();
                        string S36 = dlabell1.Tables[0].Rows[j]["36FS"].ToString();
                        string SXS = dlabell1.Tables[0].Rows[j]["xsFS"].ToString();
                        string SS = dlabell1.Tables[0].Rows[j]["sFS"].ToString();
                        string SM = dlabell1.Tables[0].Rows[j]["mFS"].ToString();
                        string SL = dlabell1.Tables[0].Rows[j]["lFS"].ToString();
                        string SXL = dlabell1.Tables[0].Rows[j]["xlFS"].ToString();
                        string SXXL = dlabell1.Tables[0].Rows[j]["xxlFS"].ToString();
                        string S3XL = dlabell1.Tables[0].Rows[j]["3xlFS"].ToString();
                        string S4XL = dlabell1.Tables[0].Rows[j]["4xlFS"].ToString();


                        string HS30 = dlabell1.Tables[0].Rows[j]["30HS"].ToString();
                        string HS32 = dlabell1.Tables[0].Rows[j]["32HS"].ToString();
                        string HS34 = dlabell1.Tables[0].Rows[j]["34HS"].ToString();
                        string HS36 = dlabell1.Tables[0].Rows[j]["36HS"].ToString();
                        string HSXS = dlabell1.Tables[0].Rows[j]["xsHS"].ToString();
                        string HSS = dlabell1.Tables[0].Rows[j]["sHS"].ToString();
                        string HSM = dlabell1.Tables[0].Rows[j]["mHS"].ToString();
                        string HSL = dlabell1.Tables[0].Rows[j]["lHS"].ToString();
                        string HSXL = dlabell1.Tables[0].Rows[j]["xlHS"].ToString();
                        string HSXXL = dlabell1.Tables[0].Rows[j]["xxlHS"].ToString();
                        string HS3XL = dlabell1.Tables[0].Rows[j]["3xlHS"].ToString();
                        string HS4XL = dlabell1.Tables[0].Rows[j]["4xlHS"].ToString();

                        string TotFS = dlabell1.Tables[0].Rows[j]["TotFS"].ToString();
                        string TotHS = dlabell1.Tables[0].Rows[j]["TotHS"].ToString();
                        string Qty = dlabell1.Tables[0].Rows[j]["Qty"].ToString();

                        if (S30 != "0")
                        {

                            gridnewmaster.Columns[5].Visible = true;
                        }
                        if (S32 != "0")
                        {

                            gridnewmaster.Columns[6].Visible = true;
                        }

                        if (S34 != "0")
                        {

                            gridnewmaster.Columns[7].Visible = true;
                        }

                        if (S36 != "0")
                        {

                            gridnewmaster.Columns[8].Visible = true;
                        }

                        if (SXS != "0")
                        {

                            gridnewmaster.Columns[9].Visible = true;
                        }

                        if (SS != "0")
                        {

                            gridnewmaster.Columns[10].Visible = true;
                        }

                        if (SM != "0")
                        {

                            gridnewmaster.Columns[11].Visible = true;
                        }

                        if (SL != "0")
                        {

                            gridnewmaster.Columns[12].Visible = true;
                        }

                        if (SXL != "0")
                        {

                            gridnewmaster.Columns[13].Visible = true;
                        }

                        if (SXXL != "0")
                        {

                            gridnewmaster.Columns[14].Visible = true;
                        }

                        if (S3XL != "0")
                        {

                            gridnewmaster.Columns[15].Visible = true;
                        }

                        if (S4XL != "0")
                        {

                            gridnewmaster.Columns[16].Visible = true;
                        }


                        if (HS30 != "0")
                        {

                            gridnewmaster.Columns[17].Visible = true;
                        }
                        if (HS32 != "0")
                        {

                            gridnewmaster.Columns[18].Visible = true;
                        }

                        if (HS34 != "0")
                        {

                            gridnewmaster.Columns[19].Visible = true;
                        }

                        if (HS36 != "0")
                        {

                            gridnewmaster.Columns[20].Visible = true;
                        }

                        if (HSXS != "0")
                        {

                            gridnewmaster.Columns[21].Visible = true;
                        }

                        if (HSS != "0")
                        {

                            gridnewmaster.Columns[22].Visible = true;
                        }

                        if (HSM != "0")
                        {

                            gridnewmaster.Columns[23].Visible = true;
                        }

                        if (HSL != "0")
                        {

                            gridnewmaster.Columns[24].Visible = true;
                        }

                        if (HSXL != "0")
                        {

                            gridnewmaster.Columns[25].Visible = true;
                        }

                        if (HSXXL != "0")
                        {

                            gridnewmaster.Columns[26].Visible = true;
                        }

                        if (HS3XL != "0")
                        {

                            gridnewmaster.Columns[27].Visible = true;
                        }

                        if (HS4XL != "0")
                        {

                            gridnewmaster.Columns[28].Visible = true;
                        }
                        if (TotFS != "0")
                        {

                            gridnewmaster.Columns[29].Visible = true;
                        }
                        if (TotHS != "0")
                        {

                            gridnewmaster.Columns[30].Visible = true;
                        }

                    }
                    #endregion

                    if (dlabell1.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dlabell1.Tables[0].Rows.Count; i++)
                        {
                            meter = meter + Convert.ToDouble(dlabell1.Tables[0].Rows[i]["reqmeter"]);
                            Qty = Qty + Convert.ToDouble(dlabell1.Tables[0].Rows[i]["Qty"]);
                        }
                        Lblvalue.Text = (meter / Qty).ToString("0.00");
                        count = dlabell1.Tables[0].Rows.Count;
                    }
                }

                DataSet ddamage = objBs.getdamagednewgrid(Convert.ToInt32(iSalesID), lblwidth.Text);
                if (ddamage.Tables[0].Rows.Count > 0)
                {
                    griddam.DataSource = ddamage;
                    griddam.DataBind();
                }







                DataSet ds23 = objBs.msatergettotalqtyCuttingprintreport(Convert.ToInt32(iSalesID));
                if (ds23.Tables[0].Rows.Count > 0)
                {

                    GridView1.DataSource = ds23;
                    GridView1.DataBind();
                }

                DataSet dsfablist = objBs.gettotalmasterfablistreport(Convert.ToInt32(iSalesID));
                if (dsfablist.Tables[0].Rows.Count > 0)
                {
                    fablistcalcalcuationgrid.DataSource = dsfablist;
                    fablistcalcalcuationgrid.DataBind();
                }


                DataSet ds234 = objBs.mastergettotalrateCuttingprintreport(Convert.ToInt32(iSalesID));
                if (ds234.Tables[0].Rows.Count > 0)
                {

                    //for (int j = 0; j < ddamage.Tables[0].Rows.Count; j++)
                    //{
                    //    string name = ddamage.Tables[0].Rows[j]["reason"].ToString();
                    //    if (name == "Damaged" || name == "Width Shortage")
                    //    {
                    //        trate = trate + Convert.ToDouble(ddamage.Tables[0].Rows[j]["dmgmet"]);
                    //    }
                    //   // tmet = tmet + Convert.ToDouble(ds234.Tables[0].Rows[j]["met"]);
                    //}
                    double finalrate = 0.00;
                    double lessrate = 0.00;
                    DataSet ds;
                    DataTable dt;
                    DataRow drNew;
                    DataColumn dc;


                    ds = new DataSet();
                    dt = new DataTable();
                    dc = new DataColumn("met");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("dsmet");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("amet");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("rat");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("tot");
                    dt.Columns.Add(dc);

                    ds.Tables.Add(dt);

                    drNew = dt.NewRow();
                    if (ddamage.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ddamage.Tables[0].Rows.Count; i++)
                        {
                            string res = ddamage.Tables[0].Rows[i]["reason"].ToString();
                            if (res == "Others")
                            {
                                finalrate = finalrate + Convert.ToDouble(ddamage.Tables[0].Rows[i]["dmgmet"]);
                            }
                            else if (res == "Damaged")
                            {
                                lessrate = lessrate + Convert.ToDouble(ddamage.Tables[0].Rows[i]["dmgmet"]);
                            }
                            else
                            {
                                lessrate = lessrate + Convert.ToDouble(ddamage.Tables[0].Rows[i]["dmgmet"]);
                            }


                        }
                        drNew["met"] = Convert.ToDouble(ds234.Tables[0].Rows[0]["met"]);
                        drNew["dsmet"] = lessrate;
                        drNew["amet"] = Convert.ToDouble(ds234.Tables[0].Rows[0]["met"]) - lessrate;
                        drNew["rat"] = ds234.Tables[0].Rows[0]["rat"];
                        drNew["tot"] = ds234.Tables[0].Rows[0]["tot"]; ;
                        ds.Tables[0].Rows.Add(drNew);
                    }



                    GridView2.DataSource = ds;
                    GridView2.DataBind();



                    double mrpp = Convert.ToDouble(ds234.Tables[0].Rows[0]["cost"]);
                    double total = mrpp + (Convert.ToDouble(ds234.Tables[0].Rows[0]["tot"])) / Qty;
                    lblratee.Text = total.ToString("0.00");
                    lblmrp.Text = mrpp.ToString("0.00");
                    double met = Convert.ToDouble(ds234.Tables[0].Rows[0]["met"]);
                    Lblvalue.Text = (met / Qty).ToString("0.00");
                }
                //DataSet dlabell = objBs.getcustomerlabels(Convert.ToInt32(ids));
                //if (dlabell.Tables[0].Rows.Count > 0)
                //{
                //    gridlabel.DataSource = dlabell;
                //    gridlabel.DataBind();
                //}




            }
        }
        protected void gvfabriccost_rowbound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totfabr = totfabr + Convert.ToDouble(e.Row.Cells[1].Text);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = totfabr.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;

            }
        }
        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("mastercutgrid.aspx");
        }
        protected void Gridrawmaterial_rowdatabound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totrawmat = totrawmat + Convert.ToDouble(e.Row.Cells[3].Text);
                perrawmaterila = perrawmaterila + Convert.ToDouble(e.Row.Cells[2].Text);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[2].Text = perrawmaterila.ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[3].Text = totrawmat.ToString();
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                // lblrawmaterialcost.Text = totrawmat.ToString();
                // lblperrawmaterialcost.Text = perrawmaterila.ToString();

            }
        }
        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalfs = totalfs + Convert.ToDouble(e.Row.Cells[13].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //  e.Row.Cells[6].Text = "Total:";
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
                int ddesgin = objBs.getcountforgroup(designcount, Convert.ToInt32(ids));
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



        protected void gridnewmaster_RowCreated(object sender, GridViewRowEventArgs e)
        {
            #region 1

            //----------start----------//
            bTotal = false;
            bool IsSubTotalRowNeedToAdd1 = false;

            bool IsGrandTotalRowNeedtoAdd1 = false;
            if ((strPreviousRowID1 != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") != null))
            {
                if (strPreviousRowID1 != DataBinder.Eval(e.Row.DataItem, "design").ToString())
                {

                    IsSubTotalRowNeedToAdd1 = true;
                    iCntDesign = intSubTotalIndex1;
                }

            }

            if ((strPreviousRowID1 != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") == null))
            {
                IsSubTotalRowNeedToAdd1 = true;
                iCntDesign = intSubTotalIndex1;
                IsGrandTotalRowNeedtoAdd1 = true;
                intSubTotalIndex1 = 0;
                // iCntDesign = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID1 == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") != null))
            {
                GridView gridPurchase1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Design Name : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
                designcount1 = DataBinder.Eval(e.Row.DataItem, "design").ToString();
                cell.ColumnSpan = 10;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                gridPurchase1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;
                iCntDesign++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd1)
            {
                string iSalesID1 = Request.QueryString.Get("iCutID");
                int ddesgin1 = objBs.getcountforgroupmaster(designcount1, Convert.ToInt32(iSalesID1));

                #region Adding Sub Total Row
                GridView gridPurchase1 = (GridView)sender;
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
                cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity1 / ddesgin1);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice1 / ddesgin1);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Discount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount1 / ddesgin1);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalRAte1 / ddesgin1);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                iCntDesign = 0;
                intSubTotalIndex1++;
                iCntDesign++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "design") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Design : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
                    designcount1 = DataBinder.Eval(e.Row.DataItem, "design").ToString();
                    cell.ColumnSpan = 9;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    gridPurchase1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;
                    iCntDesign++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalUnitPrice1 = 0;
                dblSubTotalQuantity1 = 0;
                dblSubTotalDiscount1 = 0;
                dblSubTotalRAte1 = 0;

                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd1)
            {
                #region Grand Total Row
                GridView gridPurchase1 = (GridView)sender;
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
                gridPurchase1.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }

            #endregion

        }

        protected void gridnewmaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region
                F30 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30FS").ToString());
                F32 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32FS").ToString());
                F34 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34FS").ToString());
                F36 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36FS").ToString());
                FXS += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XSFS").ToString());
                FS += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SFS").ToString());
                FM += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MFS").ToString());
                FL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "LFS").ToString());
                FXL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XLFS").ToString());
                FXXL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XXLFS").ToString());
                F3XL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3XLFS").ToString());
                F4XL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4XLFS").ToString());

                H30 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30HS").ToString());
                H32 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32HS").ToString());
                H34 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34HS").ToString());
                H36 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36HS").ToString());
                HXS += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XSHS").ToString());
                HS += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SHS").ToString());
                HM += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MHS").ToString());
                HL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "LHS").ToString());
                HXL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XLHS").ToString());
                HXXL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XXLHS").ToString());
                H3XL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3XLHS").ToString());
                H4XL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4XLHS").ToString());

                TotFS += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotFS").ToString());
                TotHS += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotHS").ToString());
                fhsQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty").ToString());
                #endregion

                #region




                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "-";
                }
                if (e.Row.Cells[6].Text == "0")
                {
                    e.Row.Cells[6].Text = "-";
                }
                if (e.Row.Cells[7].Text == "0")
                {
                    e.Row.Cells[7].Text = "-";
                }
                if (e.Row.Cells[8].Text == "0")
                {
                    e.Row.Cells[8].Text = "-";
                }
                if (e.Row.Cells[9].Text == "0")
                {
                    e.Row.Cells[9].Text = "-";
                }
                if (e.Row.Cells[10].Text == "0")
                {
                    e.Row.Cells[10].Text = "-";
                }
                if (e.Row.Cells[11].Text == "0")
                {
                    e.Row.Cells[11].Text = "-";
                }
                if (e.Row.Cells[12].Text == "0")
                {
                    e.Row.Cells[12].Text = "-";
                }
                if (e.Row.Cells[13].Text == "0")
                {
                    e.Row.Cells[13].Text = "-";
                }
                if (e.Row.Cells[14].Text == "0")
                {
                    e.Row.Cells[14].Text = "-";
                }

                if (e.Row.Cells[15].Text == "0")
                {
                    e.Row.Cells[15].Text = "-";
                }
                if (e.Row.Cells[16].Text == "0")
                {
                    e.Row.Cells[16].Text = "-";
                }
                if (e.Row.Cells[17].Text == "0")
                {
                    e.Row.Cells[17].Text = "-";
                }
                if (e.Row.Cells[18].Text == "0")
                {
                    e.Row.Cells[18].Text = "-";
                }
                if (e.Row.Cells[19].Text == "0")
                {
                    e.Row.Cells[19].Text = "-";
                }
                if (e.Row.Cells[20].Text == "0")
                {
                    e.Row.Cells[20].Text = "-";
                }
                if (e.Row.Cells[21].Text == "0")
                {
                    e.Row.Cells[21].Text = "-";
                }
                if (e.Row.Cells[22].Text == "0")
                {
                    e.Row.Cells[22].Text = "-";
                }
                if (e.Row.Cells[23].Text == "0")
                {
                    e.Row.Cells[23].Text = "-";
                }
                if (e.Row.Cells[24].Text == "0")
                {
                    e.Row.Cells[24].Text = "-";
                }
                if (e.Row.Cells[25].Text == "0")
                {
                    e.Row.Cells[25].Text = "-";
                }
                if (e.Row.Cells[26].Text == "0")
                {
                    e.Row.Cells[26].Text = "-";
                }
                if (e.Row.Cells[27].Text == "0")
                {
                    e.Row.Cells[27].Text = "-";
                }
                if (e.Row.Cells[28].Text == "0")
                {
                    e.Row.Cells[28].Text = "-";
                }
                #endregion
            }



            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region
                e.Row.Cells[3].Text = "Total";
                e.Row.Cells[5].Text = F30.ToString();
                e.Row.Cells[6].Text = F32.ToString();
                e.Row.Cells[7].Text = F34.ToString();
                e.Row.Cells[8].Text = F36.ToString();
                e.Row.Cells[9].Text = FXS.ToString();
                e.Row.Cells[10].Text = FS.ToString();
                e.Row.Cells[11].Text = FM.ToString();
                e.Row.Cells[12].Text = FL.ToString();
                e.Row.Cells[13].Text = FXL.ToString();
                e.Row.Cells[14].Text = FXXL.ToString();
                e.Row.Cells[15].Text = F3XL.ToString();
                e.Row.Cells[16].Text = F4XL.ToString();

                e.Row.Cells[17].Text = H30.ToString();
                e.Row.Cells[18].Text = H32.ToString();
                e.Row.Cells[19].Text = H34.ToString();
                e.Row.Cells[20].Text = H36.ToString();
                e.Row.Cells[21].Text = HXS.ToString();
                e.Row.Cells[22].Text = HS.ToString();
                e.Row.Cells[23].Text = HM.ToString();
                e.Row.Cells[24].Text = HL.ToString();
                e.Row.Cells[25].Text = HXL.ToString();
                e.Row.Cells[26].Text = HXXL.ToString();
                e.Row.Cells[27].Text = H3XL.ToString();
                e.Row.Cells[28].Text = H3XL.ToString();

                e.Row.Cells[29].Text = TotFS.ToString();
                e.Row.Cells[30].Text = TotHS.ToString();
                e.Row.Cells[31].Text = fhsQty.ToString();

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[23].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[24].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[25].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[26].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[27].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[28].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[29].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[30].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[31].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;
                e.Row.Cells[9].Font.Bold = true;
                e.Row.Cells[10].Font.Bold = true;
                e.Row.Cells[11].Font.Bold = true;
                e.Row.Cells[12].Font.Bold = true;
                e.Row.Cells[13].Font.Bold = true;
                e.Row.Cells[14].Font.Bold = true;

                e.Row.Cells[15].Font.Bold = true;
                e.Row.Cells[16].Font.Bold = true;
                e.Row.Cells[17].Font.Bold = true;
                e.Row.Cells[18].Font.Bold = true;
                e.Row.Cells[19].Font.Bold = true;
                e.Row.Cells[20].Font.Bold = true;
                e.Row.Cells[21].Font.Bold = true;
                e.Row.Cells[22].Font.Bold = true;
                e.Row.Cells[23].Font.Bold = true;
                e.Row.Cells[24].Font.Bold = true;
                e.Row.Cells[25].Font.Bold = true;
                e.Row.Cells[26].Font.Bold = true;
                e.Row.Cells[27].Font.Bold = true;
                e.Row.Cells[28].Font.Bold = true;
                e.Row.Cells[29].Font.Bold = true;
                e.Row.Cells[30].Font.Bold = true;
                e.Row.Cells[31].Font.Bold = true;
                //e.Row.Cells[4].Text = "TOTAL :";
                //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[4].Font.Bold = true;

                //e.Row.Cells[29].Text = TOTAL.ToString();
                //e.Row.Cells[29].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[29].Font.Bold = true;


                #endregion

                grandtotalfhs.Text = (TotFS + TotHS).ToString();

            }


        }


    }
}