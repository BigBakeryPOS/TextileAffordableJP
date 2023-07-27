using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
 public  class PurchaseOrderReportcs
    {
         #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public PurchaseOrderReportcs()
        {
            dbObj = new DBAccess();
        }
        #endregion
        public DataSet sOrdebyVendor(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.VendorName,B.CategoryId,B.ItemName,G.BrandId,A.pono,a.PoDate,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,g.BrandName from tblPO_" + sTableName + " A,tblTransPO_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblBrand G where A.pono=B.PO_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.ItemName and A.VendorName=E.LedgerID  and D.BrandCode=G.BrandId and  A.PoDate between   Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.VendorName";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        
        public DataSet sorderCategory(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.VendorName,B.CategoryId,B.ItemName,G.BrandId,A.pono,a.PoDate,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,g.BrandName from tblPO_" + sTableName + " A,tblTransPO_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblBrand G where A.pono=B.PO_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.ItemName and A.VendorName=E.LedgerID  and D.BrandCode=G.BrandId and  A.PoDate between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by B.CategoryId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderitem(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.VendorName,B.CategoryId,B.ItemName,G.BrandId,A.pono,a.PoDate,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,g.BrandName from tblPO_" + sTableName + " A,tblTransPO_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblBrand G where A.pono=B.PO_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.ItemName and A.VendorName=E.LedgerID  and D.BrandCode=G.BrandId and  A.PoDate between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by  B.ItemName";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderBrand(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.VendorName,B.CategoryId,B.ItemName,G.BrandId,A.pono,a.PoDate,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,g.BrandName from tblPO_" + sTableName + " A,tblTransPO_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblBrand G where A.pono=B.PO_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.ItemName and A.VendorName=E.LedgerID  and D.BrandCode=G.BrandId and  A.PoDate between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by G.BrandId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyVendor2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.pono,a.PoDate,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,g.BrandName from tblPO_" + sTableName + " A,tblTransPO_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblBrand G where A.pono=B.PO_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.ItemName and A.VendorName=E.LedgerID  and D.BrandCode=G.BrandId and  A.PoDate between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.VendorName";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }

       
        public DataSet ordebyCategory2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.pono,a.PoDate,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,g.BrandName from tblPO_" + sTableName + " A,tblTransPO_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblBrand G where A.pono=B.PO_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.ItemName and A.VendorName=E.LedgerID  and D.BrandCode=G.BrandId and  A.PoDate between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by B.CategoryId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyDifinition2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.pono,a.PoDate,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,g.BrandName from tblPO_" + sTableName + " A,tblTransPO_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblBrand G where A.pono=B.PO_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.ItemName and A.VendorName=E.LedgerID  and D.BrandCode=G.BrandId and  A.PoDate between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by  B.ItemName";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyBrand2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.pono,a.PoDate,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,g.BrandName from tblPO_" + sTableName + " A,tblTransPO_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblBrand G where A.pono=B.PO_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.ItemName and A.VendorName=E.LedgerID  and D.BrandCode=G.BrandId and  A.PoDate between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by G.BrandId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
    }
}
