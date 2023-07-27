using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
  public  class StockStatementBS
    {
         #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public StockStatementBS()
        {
            dbObj = new DBAccess();
        }
        #endregion
        public DataSet sStockStatement(string sTableName)
        {
            DataSet ds = new DataSet();
            string paygird = "select distinct A.CategoryUserID,A.Definition,case when sum(B.Nos) is null then 0 else sum(B.Nos) end OpeningStockQuantity  ,case when sum(C.Quantity) is null then 0 else sum(C.Quantity) end as SalesQuntity,case when sum(convert(int,D.Qty)) is null then 0 else sum(convert(int,D.Qty)) end as PurchaseQuntity ,case when sum(E.Qty) is null then 0 else sum(E.Qty) End as  DeliveryNoteInQuantity,case when SUM(F.Qty) is null then 0 else SUM(F.Qty) end  as DeliveryNoteOutQuantity,case when SUM(convert(int,G.Qty))is null then 0 else SUM(convert(int,G.Qty)) end  as PurchaseReturnQuantity,case when SUM(convert(int,H.Qty))is null then 0 else SUM(convert(int,H.Qty)) end  as SalesReturnQuantity ,SUM(ISNULL(B.Nos,0)+ISNULL(D.Qty,0)+ISNULL(E.Qty,0)+ISNULL(H.Qty,0))-(SUM(ISNULL(C.Quantity,0)+ISNULL(F.Qty,0)+ISNULL(G.Qty,0))) as Total from tblCategoryUser A full join tblOpeningStock B on A.CategoryUserID=B.StockItem full join tblTransSales_"+sTableName+" C on A.CategoryUserID=C.SubCategoryID full join tblTransPurchase_"+sTableName+" D on A.CategoryUserID=D.DescriptionId full join tblTransDeliveryNoteIN_"+sTableName+" as E on A.CategoryUserID=E.Product full join tblTransDeliveryNoteOUT_"+sTableName+" F on A.CategoryUserID=F.Product full join tblTransPurchaseReturn_"+sTableName+" as G on A.CategoryUserID=G.DescriptionId full join tblTransSaleReturn_"+sTableName+" as H on A.CategoryUserID=H.DescriptionId group by A.CategoryUserID,A.Definition order by CategoryUserID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }

        public DataSet StockStatement()
        {


            DataSet ds1 = new DataSet();
            DataSet sales = new DataSet();
            DataSet salesreturn = new DataSet();
            DataSet PurchaseQty = new DataSet();
            DataSet PurcRety = new DataSet();
            DataSet DelivNoteOutQty = new DataSet();
            DataSet DelivNoteInQty = new DataSet();

            string Qry = "Select Branchcode from tblbranch";
            DataSet ds = dbObj.InlineExecuteDataSet(Qry);


            DataSet dsNew = new DataSet();
            DataTable dtNew = new DataTable();
            DataColumn dcNew = new DataColumn();
            DataRow drNew;









            //Sales 
            DataSet sdsNew = new DataSet();
            DataTable sdtNew = new DataTable();
            DataColumn sdcNew = new DataColumn();
            DataRow sdrNew;

            sdcNew = new DataColumn("Branchcode");
            sdtNew.Columns.Add(sdcNew);
            sdcNew = new DataColumn("Totalqty");
            sdtNew.Columns.Add(sdcNew);
            sdcNew = new DataColumn("SalesQuantity");
            sdtNew.Columns.Add(sdcNew);

            sdsNew.Tables.Add(sdtNew);

            //Sales Return

            DataSet srdsNew = new DataSet();
            DataTable srdtNew = new DataTable();
            DataColumn srdcNew = new DataColumn();
            DataRow srdrNew;
            srdcNew = new DataColumn("Branchcode");
            srdtNew.Columns.Add(srdcNew);
            srdcNew = new DataColumn("Totalqty");
            srdtNew.Columns.Add(srdcNew);
            srdcNew = new DataColumn("SalesReturnQuantity");
            srdtNew.Columns.Add(srdcNew);

            srdsNew.Tables.Add(srdtNew);


            //Purchase
            DataSet pdsNew = new DataSet();
            DataTable pdtNew = new DataTable();
            DataColumn pdcNew = new DataColumn();
            DataRow pdrNew;
            pdcNew = new DataColumn("Branchcode");
            pdtNew.Columns.Add(pdcNew);
            pdcNew = new DataColumn("Totalqty");
            pdtNew.Columns.Add(pdcNew);
            pdcNew = new DataColumn("PurchaseQuntity");
            pdtNew.Columns.Add(pdcNew);

            pdsNew.Tables.Add(pdtNew);


            //Purchase Return
            DataSet prdsNew = new DataSet();
            DataTable prdtNew = new DataTable();
            DataColumn prdcNew = new DataColumn();
            DataRow prdrNew;
            prdcNew = new DataColumn("Branchcode");
            prdtNew.Columns.Add(prdcNew);
            prdcNew = new DataColumn("Totalqty");
            prdtNew.Columns.Add(prdcNew);
            prdcNew = new DataColumn("PurchaseReturnQuantity");
            prdtNew.Columns.Add(prdcNew);

            prdsNew.Tables.Add(prdtNew);

            //Delivery Note Out
            DataSet dnodsNew = new DataSet();
            DataTable dnodtNew = new DataTable();
            DataColumn dnodcNew = new DataColumn();
            DataRow dnodrNew;
            dnodcNew = new DataColumn("Branchcode");
            dnodtNew.Columns.Add(dnodcNew);
            dnodcNew = new DataColumn("Totalqty");
            dnodtNew.Columns.Add(dnodcNew);
            dnodcNew = new DataColumn("DeliveryNoteOutQuantity");
            dnodtNew.Columns.Add(dnodcNew);

            dnodsNew.Tables.Add(dnodtNew);


            //Delivery Note In
            DataSet dnidsNew = new DataSet();
            DataTable dnidtNew = new DataTable();
            DataColumn dnidcNew = new DataColumn();
            DataRow dnidrNew;
            dnidcNew = new DataColumn("Branchcode");
            dnidtNew.Columns.Add(dnidcNew);
            dnidcNew = new DataColumn("Totalqty");
            dnidtNew.Columns.Add(dnidcNew);
            dnidcNew = new DataColumn("DeliveryNoteInQuantity");
            dnidtNew.Columns.Add(dnidcNew);

            dnidsNew.Tables.Add(dnidtNew);



            dcNew = new DataColumn("CategoryUserID");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("Definition");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("OpeningStockQuantity");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("SalesQuantity");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("SalesreturnQuantity");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("PurchaseQuntity");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("PurchaseReturnQuantity");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("DeliveryNoteOutQuantity");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("DeliveryNoteInQuantity");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("Total");
            dtNew.Columns.Add(dcNew);






            dsNew.Tables.Add(dtNew);


            string Cmd = "SELECT CategoryUserID,Definition from tblCategoryUser";
            DataSet Categoy = dbObj.InlineExecuteDataSet(Cmd);







            if (Categoy != null)
            {
                if (Categoy.Tables[0] != null)
                {
                    foreach (DataRow dr in Categoy.Tables[0].Rows)
                    {
                        int sqty = 0;
                        int srqty = 0;
                        int pqty = 0;
                        int prqty = 0;
                        int dnoqty = 0;
                        int dniqty = 0;
                        int totalqty = 0;
                        int total = 0;
                        string dssales = null;


                        drNew = dtNew.NewRow();
                        drNew["CategoryUserID"] = Convert.ToInt32(dr["CategoryUserID"]);
                        drNew["Definition"] = Convert.ToString(dr["Definition"]);

                        string openstock = " SELECT a.CategoryUserID,a.Definition,case when SUM(b.Nos) is null then 0 else sum(b.Nos) end OpeningStockQuantity from tblCategoryUser a full join tblOpeningStock b on a.CategoryUserID=b.StockItem  where b.StockItem=" + Convert.ToInt32(dr["CategoryUserID"]) + " group by CategoryUserID,a.Definition";
                        DataSet ds3 = dbObj.InlineExecuteDataSet(openstock);


                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in ds3.Tables[0].Rows)
                            {
                                drNew["OpeningStockQuantity"] = Convert.ToString(dr1["OpeningStockQuantity"]);
                                total = Convert.ToInt32((dr1["OpeningStockQuantity"]));
                                drNew["Total"] = total;
                            }
                        }
                        else
                        {
                            drNew["OpeningStockQuantity"] = 0;


                        }


                        foreach (DataRow dr2 in ds.Tables[0].Rows)
                        {
                            sdrNew = sdtNew.NewRow();

                            sdrNew["Branchcode"] = Convert.ToString(dr2["Branchcode"]);
                            string sal = " SELECT a.CategoryUserID,a.Definition,case when SUM(b.Quantity)is null then 0 else sum(b.Quantity) end SalesQuantity  from tblCategoryUser a full join tblTransSales_" + (dr2["Branchcode"]) + " b on a.CategoryUserID=b.SubCategoryID  where b.SubCategoryID = '" + Convert.ToInt32(dr["CategoryUserID"]) + "' group by CategoryUserID,a.Definition";

                            sales = dbObj.InlineExecuteDataSet(sal);


                            if (sales.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr3 in sales.Tables[0].Rows)
                                {

                                    sdrNew["SalesQuantity"] = Convert.ToString(dr3["SalesQuantity"]);
                                    totalqty = Convert.ToInt32(dr3["SalesQuantity"]);
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                }

                                sdrNew["Totalqty"] = totalqty;
                            }
                            else
                            {

                                sdrNew["SalesQuantity"] = 0;
                            }
                            sdsNew.Tables[0].Rows.Add(sdrNew);
                        }



                        if (sdsNew.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr4 in sdsNew.Tables[0].Rows)
                            {

                                drNew["SalesQuantity"] = Convert.ToString(dr4["SalesQuantity"]);
                                sqty = sqty + Convert.ToInt32(dr4["SalesQuantity"]);
                                drNew["SalesQuantity"] = sqty;

                                total = total - Convert.ToInt32(dr4["SalesQuantity"]);
                                drNew["Total"] = total;

                            }

                        }
                        else
                        {
                            drNew["SalesQuantity"] = 0;
                        }
                        sdsNew.Clear();








                        foreach (DataRow dr4 in ds.Tables[0].Rows)
                        {
                            srdrNew = srdtNew.NewRow();

                            srdrNew["Branchcode"] = Convert.ToString(dr4["Branchcode"]);

                            string salesre = "   SELECT a.CategoryUserID,a.Definition, case when SUM(b.Qty)is null then 0 else sum(b.Qty) end SalesReturnQuantity  from tblCategoryUser a full join tblTransSaleReturn_" + (dr4["Branchcode"]) + " b on a.CategoryUserID=b.DescriptionId where b.DescriptionId = '" + Convert.ToInt32(dr["CategoryUserID"]) + "' group by CategoryUserID,a.Definition";
                            salesreturn = dbObj.InlineExecuteDataSet(salesre);

                            if (salesreturn.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dt in salesreturn.Tables[0].Rows)
                                {

                                    srdrNew["SalesReturnQuantity"] = Convert.ToString(dt["SalesReturnQuantity"]);
                                    totalqty = Convert.ToInt32(dt["SalesReturnQuantity"]);
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                }

                                srdrNew["Totalqty"] = totalqty;
                            }
                            else
                            {

                                srdrNew["SalesReturnQuantity"] = 0;
                            }
                            srdsNew.Tables[0].Rows.Add(srdrNew);

                        }

                        if (srdsNew.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr5 in srdsNew.Tables[0].Rows)
                            {

                                drNew["SalesreturnQuantity"] = Convert.ToString(dr5["SalesReturnQuantity"]);

                                srqty = srqty + Convert.ToInt32(dr5["SalesReturnQuantity"]);
                                drNew["SalesReturnQuantity"] = srqty;

                                total = total + Convert.ToInt32(dr5["SalesReturnQuantity"]);
                                drNew["Total"] = total;
                            }
                        }
                        else
                        {
                            drNew["SalesreturnQuantity"] = 0;
                        }
                        srdsNew.Clear();


                        foreach (DataRow dr6 in ds.Tables[0].Rows)
                        {
                            pdrNew = pdtNew.NewRow();

                            pdrNew["Branchcode"] = Convert.ToString(dr6["Branchcode"]);

                            string Purchase = "SELECT case when SUM(b.Qty) is null then 0 else sum(b.Qty) end PurchaseQuntity   from tblCategoryUser a full join  tblTransPurchase_" + (dr6["Branchcode"]) + " b on a.CategoryUserID=b.DescriptionId where b.DescriptionId=" + Convert.ToInt32(dr["CategoryUserID"]) + " group by CategoryUserID,a.Definition";
                            PurchaseQty = dbObj.InlineExecuteDataSet(Purchase);


                            if (PurchaseQty.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dt1 in PurchaseQty.Tables[0].Rows)
                                {

                                    pdrNew["PurchaseQuntity"] = Convert.ToString(dt1["PurchaseQuntity"]);
                                    totalqty = Convert.ToInt32(dt1["PurchaseQuntity"]);
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                }

                                pdrNew["Totalqty"] = totalqty;
                            }
                            else
                            {

                                pdrNew["PurchaseQuntity"] = 0;
                            }
                            pdsNew.Tables[0].Rows.Add(pdrNew);
                        }
                        if (pdsNew.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr7 in pdsNew.Tables[0].Rows)
                            {

                                drNew["PurchaseQuntity"] = Convert.ToString(dr7["PurchaseQuntity"]);

                                pqty = pqty + Convert.ToInt32(dr7["PurchaseQuntity"]);
                                drNew["PurchaseQuntity"] = pqty;

                                total = total + Convert.ToInt32(dr7["PurchaseQuntity"]);
                                drNew["Total"] = total;
                            }

                        }
                        else
                        {
                            drNew["PurchaseQuntity"] = 0;
                        }
                        pdsNew.Clear();


                        foreach (DataRow dr8 in ds.Tables[0].Rows)
                        {
                            prdrNew = prdtNew.NewRow();

                            prdrNew["Branchcode"] = Convert.ToString(dr8["Branchcode"]);
                            string PurchaseReturnQty = "   SELECT case when SUM(b.Qty) is null then 0 else sum(b.Qty) end PurchaseReturnQuantity   from tblCategoryUser a full join   tblTransPurchaseReturn_" + (dr8["Branchcode"]) + " b on a.CategoryUserID=b.DescriptionId where b.DescriptionId =" + Convert.ToInt32(dr["CategoryUserID"]) + " group by CategoryUserID,a.Definition";
                            PurcRety = dbObj.InlineExecuteDataSet(PurchaseReturnQty);

                            if (PurcRety.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dt2 in PurcRety.Tables[0].Rows)
                                {

                                    prdrNew["PurchaseReturnQuantity"] = Convert.ToString(dt2["PurchaseReturnQuantity"]);
                                    totalqty = Convert.ToInt32(dt2["PurchaseReturnQuantity"]);
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                }

                                prdrNew["Totalqty"] = totalqty;
                            }
                            else
                            {

                                prdrNew["PurchaseReturnQuantity"] = 0;
                            }
                            prdsNew.Tables[0].Rows.Add(prdrNew);
                        }

                        if (prdsNew.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr9 in prdsNew.Tables[0].Rows)
                            {

                                drNew["PurchaseReturnQuantity"] = Convert.ToString(dr9["PurchaseReturnQuantity"]);

                                prqty = prqty + Convert.ToInt32(dr9["PurchaseReturnQuantity"]);
                                drNew["PurchaseReturnQuantity"] = prqty;
                                total = total - Convert.ToInt32(dr9["PurchaseReturnQuantity"]);
                                drNew["Total"] = total;
                            }

                        }
                        else
                        {
                            drNew["PurchaseReturnQuantity"] = 0;
                        }
                        prdsNew.Clear();




                        foreach (DataRow dr10 in ds.Tables[0].Rows)
                        {
                            dnodrNew = dnodtNew.NewRow();

                            dnodrNew["Branchcode"] = Convert.ToString(dr10["Branchcode"]);
                            string DeliveryNoteOutQty = "   SELECT case when SUM(b.Qty) is null then 0 else sum(b.Qty) end DeliveryNoteOutQuantity   from tblCategoryUser a full join   tblTransDeliveryNoteOUT_" + (dr10["Branchcode"]) + " b on a.CategoryUserID=b.Product where b.Product=" + Convert.ToInt32(dr["CategoryUserID"]) + " group by CategoryUserID,a.Definition";
                            DelivNoteOutQty = dbObj.InlineExecuteDataSet(DeliveryNoteOutQty);

                            if (DelivNoteOutQty.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dt3 in DelivNoteOutQty.Tables[0].Rows)
                                {

                                    dnodrNew["DeliveryNoteOutQuantity"] = Convert.ToString(dt3["DeliveryNoteOutQuantity"]);
                                    totalqty = Convert.ToInt32(dt3["DeliveryNoteOutQuantity"]);
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                }

                                dnodrNew["Totalqty"] = totalqty;
                            }
                            else
                            {

                                dnodrNew["DeliveryNoteOutQuantity"] = 0;
                            }
                            dnodsNew.Tables[0].Rows.Add(dnodrNew);
                        }

                        if (dnodsNew.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr11 in dnodsNew.Tables[0].Rows)
                            {


                                drNew["DeliveryNoteOutQuantity"] = Convert.ToString(dr11["DeliveryNoteOutQuantity"]);

                                dnoqty = dnoqty + Convert.ToInt32(dr11["DeliveryNoteOutQuantity"]);
                                drNew["DeliveryNoteOutQuantity"] = dnoqty;
                                total = total - Convert.ToInt32(dr11["DeliveryNoteOutQuantity"]);
                                drNew["Total"] = total;
                            }
                        }
                        else
                        {
                            drNew["DeliveryNoteOutQuantity"] = 0;
                        }
                        dnodsNew.Clear();







                        foreach (DataRow dr12 in ds.Tables[0].Rows)
                        {
                            dnidrNew = dnidtNew.NewRow();
                            dnidrNew["Branchcode"] = Convert.ToString(dr12["Branchcode"]);

                            string DeliveryNoteInQuty = " SELECT case when SUM(b.Qty) is null then 0 else sum(b.Qty) end DeliveryNoteInQuantity   from tblCategoryUser a full join   tblTransDeliveryNoteIN_" + (dr12["Branchcode"]) + " b on a.CategoryUserID=b.Product where b.Product =" + Convert.ToInt32(dr["CategoryUserID"]) + " group by CategoryUserID,a.Definition";
                            DelivNoteInQty = dbObj.InlineExecuteDataSet(DeliveryNoteInQuty);


                            if (DelivNoteInQty.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dt4 in DelivNoteInQty.Tables[0].Rows)
                                {

                                    dnidrNew["DeliveryNoteInQuantity"] = Convert.ToString(dt4["DeliveryNoteInQuantity"]);
                                    totalqty = Convert.ToInt32(dt4["DeliveryNoteInQuantity"]);
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                    //total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
                                }

                                dnidrNew["Totalqty"] = totalqty;
                            }
                            else
                            {

                                dnidrNew["DeliveryNoteInQuantity"] = 0;
                            }
                            dnidsNew.Tables[0].Rows.Add(dnidrNew);
                        }

                        if (dnidsNew.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr13 in dnidsNew.Tables[0].Rows)
                            {

                                drNew["DeliveryNoteInQuantity"] = Convert.ToString(dr13["DeliveryNoteInQuantity"]);

                                dniqty = dniqty + Convert.ToInt32(dr13["DeliveryNoteInQuantity"]);
                                drNew["DeliveryNoteInQuantity"] = dniqty;

                                total = total + Convert.ToInt32(dr13["DeliveryNoteInQuantity"]);
                                drNew["Total"] = total;

                            }

                        }
                        else
                        {
                            drNew["DeliveryNoteInQuantity"] = 0;
                        }
                        dnidsNew.Clear();


                        dsNew.Tables[0].Rows.Add(drNew);





                    }
                }

            }

            return dsNew;












            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        int total = 0;
            //        drNew = dtNew.NewRow();
            //        drNew["Branchcode"] = Convert.ToString(dr["Branchcode"]);

            //        string sal = " SELECT a.CategoryUserID,a.Definition,case when SUM(b.Quantity)is null then 0 else sum(b.Quantity) end SalesQuantity  from tblCategoryUser a full join tblTransSales_" + (dr["Branchcode"]) + " b on a.CategoryUserID=b.SubCategoryID  where b.SubCategoryID = '" + CategoryUserID + "' group by CategoryUserID,a.Definition";

            //        sales = dbObj.InlineExecuteDataSet(sal);
            //        if (sales.Tables[0].Rows.Count > 0)
            //        {
            //            foreach (DataRow dr1 in sales.Tables[0].Rows)
            //            {

            //                drNew["SalesQuantity"] = Convert.ToString(dr1["SalesQuantity"]);
            //                total = Convert.ToInt32(dr1["SalesQuantity"]);
            //                total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
            //                total = total + Convert.ToInt32(dr1["SalesQuantity"]);  
            //            }

            //            drNew["Total"] = total;
            //        }
            //        else
            //        {

            //            drNew["SalesQuantity"] = 0;
            //        }



            //        dsNew.Tables[0].Rows.Add(drNew);

            //    }

            //    return dsNew;
            //}




        }
    }
}
