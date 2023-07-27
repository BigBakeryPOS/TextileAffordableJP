using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CommonLayer;
using DataLayer;
using System.Globalization;

namespace BusinessLayer
{
  public  class OpeningStockEntry
  {
      #region User Defined Objects
      DBAccess dbObj = null;
      #endregion

      #region Constructors
      public OpeningStockEntry()
      {
          dbObj = new DBAccess();
      }
      #endregion
      public DataSet getCategory(string sBrand)
      {
          DataSet ds = new DataSet();
          string paygird = "select Distinct B.category,B.categoryid from tblCategoryUser A,tblcategory B where A.CategoryID=B.categoryid and A.BrandCode='" + sBrand + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getGrid()
      {
          DataSet ds = new DataSet();
          string paygird = "select A.OpenStockID,A.StockDate,B.BrandName,C.category,D.Definition,A.Nos from tblOpeningStock A,tblBrand B,tblcategory C,tblCategoryUser D where A.StockBrand=B.BrandId and A.StockCategory=C.categoryid and A.StockItem=D.CategoryUserID order by A.OpenStockID desc";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getByBrandGrid(string sBrand)
      {
          DataSet ds = new DataSet();
          string paygird = "  select A.OpenStockID,A.StockDate,B.BrandName,C.category,D.Definition,A.Nos from tblOpeningStock A,tblBrand B,tblcategory C,tblCategoryUser D where A.StockBrand=B.BrandId and A.StockCategory=C.categoryid and A.StockItem=D.CategoryUserID and B.BrandName like '%" + sBrand + "%'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getByCateGoryGrid(string sBrand)
      {
          DataSet ds = new DataSet();
          string paygird = "  select A.OpenStockID,A.StockDate,B.BrandName,C.category,D.Definition,A.Nos from tblOpeningStock A,tblBrand B,tblcategory C,tblCategoryUser D where A.StockBrand=B.BrandId and A.StockCategory=C.categoryid and A.StockItem=D.CategoryUserID and C.category like '%" + sBrand + "%'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getByItemGrid(string sBrand)
      {
          DataSet ds = new DataSet();
          string paygird = "  select A.OpenStockID,A.StockDate,B.BrandName,C.category,D.Definition,A.Nos from tblOpeningStock A,tblBrand B,tblcategory C,tblCategoryUser D where A.StockBrand=B.BrandId and A.StockCategory=C.categoryid and A.StockItem=D.CategoryUserID and D.Definition like '%" + sBrand + "%'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getItem(string sBrand, string sCategory)
      {
          DataSet ds = new DataSet();
          string paygird = "      select A.CategoryUserID,A.Definition from tblCategoryUser A,tblcategory B where A.CategoryID=B.categoryid and A.BrandCode='" + sBrand + "' and A.CategoryID='" + sCategory + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getBrand()
      {
          DataSet ds = new DataSet();
          string paygird = "select * from tblBrand";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }

      public DataSet getbyOpnStck(int iStockID)
      {
          DataSet ds = new DataSet();
          string paygird = "  select * from tblOpeningStock where OpenStockID='" + iStockID + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      #region Insert OpeningStock
      public int InsertOpeningStock(DateTime sDate, string sBrand, string sCategory, string sitem, int iNos, string tblAuditMaster, string userid, string Categoryname, string itemname)
      {
          int iSuccess = 0;

          string paygird = "select * from tblBrand where brandname ='" + sBrand + "' ";
          DataSet dsd = dbObj.InlineExecuteDataSet(paygird);

          string sQry = "insert into tblOpeningStock(StockDate,StockBrand,StockCategory,StockItem,Nos) values ('" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "','" + dsd.Tables[0].Rows[0]["brandid"].ToString() + "','" + sCategory + "','" + sitem + "','" + iNos + "')";
          iSuccess = dbObj.InlineExecuteNonQuery(sQry);
          string cdate1 = DateTime.Now.ToString("dd/MM/yyyy");
          string shortDate1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

          DateTime cdate = DateTime.ParseExact(cdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
          DateTime shortDate = DateTime.ParseExact(shortDate1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);



          string sQry1 = "insert into " + tblAuditMaster + "(Type,Screen,BillNo,Amount,Bill_Date,Audit_Date,UserId,Ledgername,Description,Date) values ('Add','Opening Stock Entry',0,0,'" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "','" + Convert.ToDateTime(shortDate).ToString("yyyy/MM/dd HH:mm:ss") + "','" + userid + "','" + itemname + "','" + Categoryname + "-" + itemname + "-" + iNos + "','" + Convert.ToDateTime(cdate).ToString("yyyy/MM/dd") + "')";

          iSuccess = dbObj.InlineExecuteNonQuery(sQry1);



          return iSuccess;

      }



      public DataSet openingstockalreadyexists(string item, string branchcode)
      {
          DataSet ds = new DataSet();
          string sqry = "select * from tblOpeningStock where StockItem=" + item + " and BranchCode='" + branchcode + "'";

          ds = dbObj.InlineExecuteDataSet(sqry);
          return ds;

      }
      #endregion

      public int DeleteOpeningStock(string sLedgerID, string tblAuditMaster, string userid, string BranchCode)
      {
          int iSucess = 0;
          DataSet ds = new DataSet();
          string Cmd = "select A.OpenStockID,A.StockDate,C.category,D.Definition,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where D.categoryid=C.categoryid and A.StockItem=D.CategoryUserID and A.OpenStockID='" + sLedgerID + "'";
          ds = dbObj.InlineExecuteDataSet(Cmd);

          //string BrandName = ds.Tables[0].Rows[0]["BrandName"].ToString();
          string category = ds.Tables[0].Rows[0]["category"].ToString();
          string Definition = ds.Tables[0].Rows[0]["Definition"].ToString();
          string Nos = ds.Tables[0].Rows[0]["Nos"].ToString();
          string StockDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["StockDate"]).ToString("dd/MM/yyyy");


          string cdate1 = DateTime.Now.ToString("dd/MM/yyyy");
          string shortDate1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

          DateTime cdate = DateTime.ParseExact(StockDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
          DateTime shortDate = DateTime.ParseExact(shortDate1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);



          string sQry1 = "insert into " + tblAuditMaster + "( Type,Screen,BillNo,Amount,Bill_Date,Audit_Date,UserId,Ledgername,Description,Date) values ('Delete','Opening Stock Entry',0,0,'" + Convert.ToDateTime(cdate).ToString("yyyy/MM/dd") + "','" + Convert.ToDateTime(shortDate).ToString("yyyy/MM/dd HH:mm:ss") + "','" + userid + "','" + Definition + "','" + category + "-" + Definition + "-" + Nos + "','" + Convert.ToDateTime(cdate).ToString("yyyy/MM/dd") + "')";

          iSucess = dbObj.InlineExecuteNonQuery(sQry1);



          string sQry = " delete from tblOpeningStock  where OpenStockID='" + sLedgerID + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQry);
          return iSucess;
      }
      public int UpdateStock(string sBrand, string sCateGory, string sItem, string sNos, DateTime sDate, string sStockid, string Categryname, string Brandname, string Itemname, string tblAuditMaster, string userid)
      {
          int iSucess = 0;
          int sQty = 0;
          int sStock = 0;

          string paygird = "select * from tblStock A where A.SubCategoryID='" + sItem + "'";
          DataSet ds = dbObj.InlineExecuteDataSet(paygird);
          if (ds.Tables[0].Rows.Count > 0)
          {
              sStock = Convert.ToInt32(ds.Tables[0].Rows[0]["Available_QTY"]);
          }
          string d = "select * from tblOpeningStock where OpenStockID='" + sStockid + "' ";
          DataSet dsd = dbObj.InlineExecuteDataSet(d);
          if (dsd.Tables[0].Rows.Count > 0)
          {
              sQty = Convert.ToInt32(dsd.Tables[0].Rows[0]["Nos"]);
          }

          string sQry = "update tblStock set Available_QTY='" + (sStock - sQty) + "'   where SubCategoryID='" + sItem + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQry);

          string sQryt = " update  tblOpeningStock set StockBrand='" + sBrand + "', StockCategory='" + sCateGory + "',StockItem='" + sItem + "',Nos='" + sNos + "',StockDate='" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "' where OpenStockID='" + sStockid + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQryt);

          string cdate1 = DateTime.Now.ToString("dd/MM/yyyy");
          string shortDate1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

          DateTime cdate = DateTime.ParseExact(cdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
          DateTime shortDate = DateTime.ParseExact(shortDate1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);



          string sQry1 = "insert into " + tblAuditMaster + "( Type,Screen,BillNo,Amount,Bill_Date,Audit_Date,UserId,Ledgername,Description,Date) values ('Edit','Opening Stock Entry',0,0,'" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "','" + Convert.ToDateTime(shortDate).ToString("yyyy/MM/dd HH:mm:ss") + "','" + userid + "','" + Itemname + "','" + Brandname + "-" + Categryname + "-" + Itemname + "-" + sNos + "','" + Convert.ToDateTime(cdate).ToString("yyyy/MM/dd") + "')";

          iSucess = dbObj.InlineExecuteNonQuery(sQry1);

          return iSucess;
      }

      public int UpdateStock1(string sID, string sQty, string tblStock)
      {
          int iSucess = 0;
          string sQry = "update " + tblStock + " set Available_QTY='" + sQty + "'   where SubCategoryID='" + sID + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQry);
          return iSucess;
      }


      public DataSet CheckStock(string sCategory, string sItem, string tblStock)
      {
          DataSet ds = new DataSet();
          string paygird = "select * from " + tblStock + " A where A.SubCategoryID='" + sItem + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet geBrand1()
      {
          DataSet ds = new DataSet();
          string paygird = "select * from tblBrand";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getbyquery2(string sStockID, string tblStock)
      {
          DataSet ds = new DataSet();
          string paygird = "    select SubCategoryID,Available_QTY from " + tblStock + " where SubCategoryID='" + sStockID + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getbyquery(string sStockID)
      {
          DataSet ds = new DataSet();
          string paygird = "select StockItem,Nos from tblOpeningStock where OpenStockID='" + sStockID + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      #region insertstock
      public int InsertStock(int UserID, int iCategoryID, int iSubCategoryID, int iQty, string sddlBrand)
      {
          int iSuccess = 0;
          string sQry = "insert into tblstock(UserID, CategoryID,SubCategoryID,Quantity,UnitPrice,Available_Qty, MinQty,DealerUnitPrice,PressUnitPrice,PurchaseRate,BrandID) values ('" + UserID + "','" + iCategoryID + "','" + iSubCategoryID + "','" + iQty + "','0','" + iQty + "','0','0','0','0','" + sddlBrand + "')";
          iSuccess = dbObj.InlineExecuteNonQuery(sQry);
          return iSuccess;

      }
      #endregion

      #region insertstock1
      public int InsertStock1(int UserID, int iCategoryID, int iSubCategoryID, int brondid, string tblAuditMaster, string userid, string brandname, string categoryname, string isactive, string productcode, string tax, string tblStock, string unitprice, string dealer, string purchase, string min, string type)
      {
          int iSuccess = 0;



          string sqry12 = "select * from tblbranch where isactive='Yes'";
          DataSet ds12 = dbObj.InlineExecuteDataSet(sqry12);

          for (int ii = 0; ii < ds12.Tables[0].Rows.Count; ii++)
          {
              string sbranch = ds12.Tables[0].Rows[ii]["Branchcode"].ToString();
              if (type == "1")
              {


                  string sqry1 = "select * from tblSalesSize ";
                  DataSet ds1 = dbObj.InlineExecuteDataSet(sqry1);

                  for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                  {
                      string salessizeid = ds1.Tables[0].Rows[i]["SalesSizeId"].ToString();
                      string SizeName = ds1.Tables[0].Rows[i]["SizeName"].ToString();

                      string sQry = "insert into tblstock_" + sbranch + "(UserID, CategoryID,SubCategoryID,UnitPrice,Available_Qty, MinQty,DealerUnitPrice,PurchaseRate,BrandID,DamageQty,AdjustQtyplus,AdjustQtyminus,SizeId,SizeName,Type) values ('" + UserID + "','" + iCategoryID + "','" + iSubCategoryID + "','" + unitprice + "','0','" + min + "','" + dealer + "','" + purchase + "'," + brondid + ",'0','0','0','" + salessizeid + "','" + SizeName + "','S')";
                      iSuccess = dbObj.InlineExecuteNonQuery(sQry);
                  }
              }
              else
              {
                  string sQry = "insert into tblstock_" + sbranch + "(UserID, CategoryID,SubCategoryID,UnitPrice,Available_Qty, MinQty,DealerUnitPrice,PurchaseRate,BrandID,DamageQty,AdjustQtyplus,AdjustQtyminus,SizeId,SizeName,Type) values ('" + UserID + "','" + iCategoryID + "','" + iSubCategoryID + "','" + unitprice + "','0','" + min + "','" + dealer + "','" + purchase + "'," + brondid + ",'0','0','0','0','0','R')";
                  iSuccess = dbObj.InlineExecuteNonQuery(sQry);

              }
          }

          //string sUser = tblAuditMaster;

          //string[] branchid = sUser.Split('_');

          //string branchid1 = branchid[1].ToString();

          //    string cdate = DateTime.Now.ToString("dd/MM/yyyy");
          //    string shortDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

          ////DateTime cdate = DateTime.ParseExact(cdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
          ////DateTime shortDate = DateTime.ParseExact(shortDate1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);


          //string sQry1 = "insert into " + tblAuditMaster + "( Type,Screen,BillNo,Amount,Bill_Date,Audit_Date,UserId,Ledgername,Description,Date) values ('Add','Stock',0,0,'" + Convert.ToDateTime(cdate).ToString("yyyy/MM/dd") + "','" + Convert.ToDateTime(shortDate).ToString("yyyy/MM/dd HH:mm:ss") + "','" + userid + "','" + productcode + "','" + categoryname + "-" + brandname + "-" + isactive + "-" + tax + "','" + Convert.ToDateTime(cdate).ToString("yyyy/MM/dd") + "')";

          //iSuccess = dbObj.InlineExecuteNonQuery(sQry1);

          return iSuccess;



          return iSuccess;

      }
      #endregion

      public int updatestockdet(int iCategoryID, int iSubCategoryID, string sStockID)
      {
          int iSucess = 0;
          string sQry = "update tblstock set CategoryID='" + iCategoryID + "',SubCategoryID='" + iSubCategoryID + "'   where   SubCategoryID='" + sStockID + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQry);
          return iSucess;
      }

      public int updatestockdetDefinition(int iCategoryID, int iSubCategoryID, string sStockID, int Brandid)
      {
          int iSucess = 0;
          string sQry = "update tblstock set CategoryID='" + iCategoryID + "',Brandid=" + Brandid + "  where   SubCategoryID='" + sStockID + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQry);
          return iSucess;
      }

      public int updatestock2(int iQty, string sSubcategory, string tblStock)
      {
          int iSucess = 0;
          string sQry = " update " + tblStock + " set Available_QTY='" + iQty + "' where SubCategoryID='" + sSubcategory + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQry);
          return iSucess;
      }
      public DataSet getCategoryCode(string sCategory, string sBrandCode, string sItem)
      {
          DataSet ds = new DataSet();
          string paygird = "  select CategoryUserID from tblCategoryUser where CategoryID='" + sCategory + "' and BrandCode='" + sBrandCode + "' and Definition='" + sItem + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }

      public DataSet getCategoryCode1(string sItem)
      {
          DataSet ds = new DataSet();
          string paygird = "  select CategoryUserID from tblCategoryUser where  Definition='" + sItem + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }

      public DataSet selectcategorydecription(int iCategory)
      {
          DataSet ds = new DataSet();
          string sQry = "select * from tblcategoryuser where categoryid=" + iCategory + "  and isdelete=0 ";
          ds = dbObj.InlineExecuteDataSet(sQry);
          return ds;
      }

      public DataSet selectcategorymaster()
      {
          DataSet ds = new DataSet();
          string sQry = "select * from tblcategory  where isdelete=0 order by category desc ";
          ds = dbObj.InlineExecuteDataSet(sQry);
          return ds;
      }

      public DataSet selectbrandmaster()
      {
          DataSet ds = new DataSet();
          string sQry = "select * from tblbrand  order by brandname desc ";
          ds = dbObj.InlineExecuteDataSet(sQry);
          return ds;
      }
      public DataSet selectcategoryalldecription11()
      {
          DataSet ds = new DataSet();
          string sQry = "select * from tblcategoryuser";
          ds = dbObj.InlineExecuteDataSet(sQry);
          return ds;
      }

      public DataSet getCategoryCodenew(string sCategory, string sBrandCode, string sItem)
      {
          DataSet ds = new DataSet();
          string paygird = "  select CategoryUserID from tblCategoryUser where CategoryID='" + sCategory + "' and Definition='" + sItem + "' and Branchcode='" + sBrandCode + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }

      public int updatestockdetDefinitionnew(int iCategoryID, int iSubCategoryID, string sStockID, int Brandid, string tblStock, string unitprice, string dealer, string purchae, string mimqty, int UserID, string min, string type)
      {
          int iSucess = 0;
          int iSuccess = 0;

          if (type == "1")
          {

              string sqry1 = "select * from tblSalesSize ";
              DataSet ds1 = dbObj.InlineExecuteDataSet(sqry1);

              for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
              {
                  string salessizeid = ds1.Tables[0].Rows[i]["SalesSizeId"].ToString();
                  string SizeName = ds1.Tables[0].Rows[i]["SizeName"].ToString();

                  string sq = "Select * from " + tblStock + " where subcategoryid='" + iSubCategoryID + "' and sizeid='" + salessizeid + "'";
                  DataSet dss = dbObj.InlineExecuteDataSet(sq);
                  if (dss.Tables[0].Rows.Count > 0)
                  {
                      string stockid = dss.Tables[0].Rows[0]["Stockid"].ToString();

                      string sQry = "update " + tblStock + " set Type='S',CategoryID='" + iCategoryID + "',unitprice='" + unitprice + "',dealerunitprice='" + dealer + "',purchaserate='" + purchae + "',Minqty='" + mimqty + "'  where   stockid='" + stockid + "'";
                      iSucess = dbObj.InlineExecuteNonQuery(sQry);
                  }
                  else
                  {

                      {
                          string sQryy = "insert into " + tblStock + "(UserID, CategoryID,SubCategoryID,UnitPrice,Available_Qty, MinQty,DealerUnitPrice,PurchaseRate,BrandID,DamageQty,AdjustQtyplus,AdjustQtyminus,SizeId,SizeName,Type) values ('" + UserID + "','" + iCategoryID + "','" + iSubCategoryID + "','" + unitprice + "','0','" + min + "','" + dealer + "','" + purchae + "'," + Brandid + ",'0','0','0','" + salessizeid + "','" + SizeName + "','S')";
                          iSuccess = dbObj.InlineExecuteNonQuery(sQryy);

                      }
                  }

              }
          }
          else
          {
              string sq = "Select * from " + tblStock + " where subcategoryid='" + iSubCategoryID + "' and Type='R'";
              DataSet dss = dbObj.InlineExecuteDataSet(sq);
              if (dss.Tables[0].Rows.Count > 0)
              {
                  string stockid = dss.Tables[0].Rows[0]["Stockid"].ToString();

                  string sQry = "update " + tblStock + " set Type='R',CategoryID='" + iCategoryID + "',unitprice='" + unitprice + "',dealerunitprice='" + dealer + "',purchaserate='" + purchae + "',Minqty='" + mimqty + "'  where   stockid='" + stockid + "'";
                  iSucess = dbObj.InlineExecuteNonQuery(sQry);
              }
              else
              {

                  {
                      string sQryy = "insert into " + tblStock + "(UserID, CategoryID,SubCategoryID,UnitPrice,Available_Qty, MinQty,DealerUnitPrice,PurchaseRate,BrandID,DamageQty,AdjustQtyplus,AdjustQtyminus,SizeId,SizeName,Type) values ('" + UserID + "','" + iCategoryID + "','" + iSubCategoryID + "','" + unitprice + "','0','" + min + "','" + dealer + "','" + purchae + "'," + Brandid + ",'0','0','0','0','0','R')";
                      iSuccess = dbObj.InlineExecuteNonQuery(sQryy);

                  }
              }

          }
          return iSucess;
      }

      public int InsertOpeningStock_New(DateTime sDate, string sCategory, string sitem, double iNos, string tblAuditMaster, string userid, string Categoryname, string itemname, string branchcode,string empid)
      {
          int iSuccess = 0;

          //string paygird = "select * from tblBrand where brandname ='" + sBrand + "' ";
          //DataSet dsd = dbObj.InlineExecuteDataSet(paygird);

          string sQry = "insert into tblOpeningStock(StockDate,StockBrand,StockCategory,StockItem,Nos,Branchcode,Empid,LastUEmpid) values ('" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "','','" + sCategory + "','" + sitem + "','" + iNos + "','" + branchcode + "','"+empid+"','"+empid+"')";
          iSuccess = dbObj.InlineExecuteNonQuery(sQry);
          string cdate1 = DateTime.Now.ToString("dd/MM/yyyy");
          string shortDate1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

          DateTime cdate = DateTime.ParseExact(cdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
          DateTime shortDate = DateTime.ParseExact(shortDate1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);



          string sQry1 = "insert into " + tblAuditMaster + "(Type,Screen,BillNo,Amount,Bill_Date,Audit_Date,UserId,Ledgername,Description,Date) values ('Add','Opening Stock Entry',0,0,'" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "','" + Convert.ToDateTime(shortDate).ToString("yyyy/MM/dd HH:mm:ss") + "','" + userid + "','" + itemname + "','" + Categoryname + "-" + itemname + "-" + iNos + "','" + Convert.ToDateTime(cdate).ToString("yyyy/MM/dd") + "')";

          iSuccess = dbObj.InlineExecuteNonQuery(sQry1);



          return iSuccess;

      }

      public DataSet getGrid_New(string BranchCode)
      {
          DataSet ds = new DataSet();
          string paygird = "select 'C' as branch,A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where D.categoryid=C.categoryid and A.StockItem=D.CategoryUserID  order by A.OpenStockID desc";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getGrid_New1(string BranchCode)
      {
          DataSet ds = new DataSet();
          if (BranchCode == "CO1")
          {
              string paygird = "select 'Tenkasi' as branch,A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where D.categoryid=C.categoryid and A.StockItem=D.CategoryUserID and A.BranchCode='" + BranchCode + "' order by A.OpenStockID desc";
              ds = dbObj.InlineExecuteDataSet(paygird);
          }
          else if (BranchCode == "CO2")
          {
              string paygird = "select 'Coimbatore' as branch,A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where D.categoryid=C.categoryid and A.StockItem=D.CategoryUserID and A.BranchCode='" + BranchCode + "' order by A.OpenStockID desc";
              ds = dbObj.InlineExecuteDataSet(paygird);
          }
          else if (BranchCode == "CO3")
          {
              string paygird = "select 'Chennai' as branch,A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where D.categoryid=C.categoryid and A.StockItem=D.CategoryUserID and A.BranchCode='" + BranchCode + "' order by A.OpenStockID desc";
              ds = dbObj.InlineExecuteDataSet(paygird);
          }

          return ds;
      }

      public DataSet getGrid_Newwithamount(string BranchCode)
      {
          DataSet ds = new DataSet();
          // string paygird = "select A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where D.categoryid=C.categoryid and A.StockItem=D.CategoryUserID and A.BranchCode='" + BranchCode + "' order by A.OpenStockID desc";
          string paygrid = "select A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos,e.purchaserate,(A.nos * e.purchaseRate) as amnt " +
                           " from tblOpeningStock A,tblcategory C,tblCategoryUser D,tblstock_" + BranchCode + " e where D.categoryid=C.categoryid and e.subcategoryid=a.stockitem " +
                             " and A.StockItem=D.CategoryUserID and A.BranchCode='" + BranchCode + "' order by A.OpenStockID desc";

          ds = dbObj.InlineExecuteDataSet(paygrid);
          return ds;
      }

      public DataSet getCategory_New(string Categoryid)
      {
          DataSet ds = new DataSet();
          string paygird = "  select Distinct B.category,B.categoryid from tblCategoryUser A,tblcategory B where A.CategoryID=B.categoryid and b.categoryid='" + Categoryid + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }

      public DataSet getItem_New(string sCategory)
      {
          DataSet ds = new DataSet();
          string paygird = "    select A.CategoryUserID,A.Definition from tblCategoryUser A,tblcategory B where A.CategoryID=B.categoryid  and A.CategoryID='" + sCategory + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }


      public DataSet getCategory_New()
      {
          DataSet ds = new DataSet();
          string paygird = "select Distinct B.category,B.categoryid from tblCategoryUser A,tblcategory B where A.CategoryID=B.categoryid";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }

      public int UpdateStock_New(string sCateGory, string sItem, string sNos, DateTime sDate, string sStockid, string Categryname, string Itemname, string tblAuditMaster, string userid, string tblStock,string empid)
      {
          int iSucess = 0;
          double sQty = 0;
          double sStock = 0;

          string cdate1 = DateTime.Now.ToString("dd/MM/yyyy");
          string shortDate1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

          DateTime cdate = DateTime.ParseExact(cdate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
          DateTime shortDate = DateTime.ParseExact(shortDate1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

          string paygird = "select * from " + tblStock + " A where A.SubCategoryID='" + sItem + "'";
          DataSet ds = dbObj.InlineExecuteDataSet(paygird);
          if (ds.Tables[0].Rows.Count > 0)
          {
              sStock = Convert.ToDouble(ds.Tables[0].Rows[0]["Available_QTY"]);
          }
          string d = "select * from tblOpeningStock where OpenStockID='" + sStockid + "' ";
          DataSet dsd = dbObj.InlineExecuteDataSet(d);
          if (dsd.Tables[0].Rows.Count > 0)
          {
              sQty = Convert.ToDouble(dsd.Tables[0].Rows[0]["Nos"]);
          }

          string sQry = "update " + tblStock + " set Available_QTY='" + (sStock - sQty) + "'   where SubCategoryID='" + sItem + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQry);

          string sQryt = " update  tblOpeningStock set LastUEmpid='" + empid + "',LastUDate='" + Convert.ToDateTime(shortDate).ToString("yyyy/MM/dd HH:mm:ss") + "',StockBrand='', StockCategory='',StockItem='" + sItem + "',Nos='" + sNos + "',StockDate='" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "' where OpenStockID='" + sStockid + "'";
          iSucess = dbObj.InlineExecuteNonQuery(sQryt);

        



          string sQry1 = "insert into " + tblAuditMaster + "( Type,Screen,BillNo,Amount,Bill_Date,Audit_Date,UserId,Ledgername,Description,Date) values ('Edit','Opening Stock Entry',0,0,'" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "','" + Convert.ToDateTime(shortDate).ToString("yyyy/MM/dd HH:mm:ss") + "','" + userid + "','" + Itemname + "','" + Categryname + "-" + Itemname + "-" + sNos + "','" + Convert.ToDateTime(cdate).ToString("yyyy/MM/dd") + "')";

          iSucess = dbObj.InlineExecuteNonQuery(sQry1);

          return iSucess;
      }


      public DataSet getByCateGoryGrid_New(string sBrand, string BranchCode)
      {
          DataSet ds = new DataSet();
          string paygird = "select A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where A.StockCategory=C.categoryid and A.StockItem=D.CategoryUserID and C.category like '%" + sBrand + "%' and D.BranchCode='" + BranchCode + "' AND C.BranchCode='" + BranchCode + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
      public DataSet getByItemGrid_New(string sBrand, string BranchCode)
      {
          DataSet ds = new DataSet();
          string paygird = "  select A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where  A.StockCategory=C.categoryid and A.StockItem=D.CategoryUserID and D.Definition like '%" + sBrand + "%' and D.BranchCode='" + BranchCode + "' AND C.BranchCode='" + BranchCode + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }

      public DataSet getByItemGridproduct(string sBrand, string BranchCode)
      {
          DataSet ds = new DataSet();
          string paygird = "  select A.OpenStockID,A.StockDate,C.category,D.Definition,D.Serial_No,A.Nos from tblOpeningStock A,tblcategory C,tblCategoryUser D where  A.StockCategory=C.categoryid and A.StockItem=D.CategoryUserID and D.Serial_No like '%" + sBrand + "%' and D.BranchCode='" + BranchCode + "' AND C.BranchCode='" + BranchCode + "'";
          ds = dbObj.InlineExecuteDataSet(paygird);
          return ds;
      }
  }
}
