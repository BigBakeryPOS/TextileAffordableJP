using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
  public  class PurchaseReturnReport
    {
        #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public PurchaseReturnReport()
        {
            dbObj = new DBAccess();
        }
        #endregion

       
        public DataSet sOrdebyVendor(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.VendorID,A.PaymentMode,B.CategoryId,B.DescriptionId,G.BrandId,A.PR_NO,a.PR_Date,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and  A.PR_Date between  '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by A.VendorID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderPay(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.VendorID,A.PaymentMode,B.CategoryId,B.DescriptionId,G.BrandId,A.PR_NO,a.PR_Date,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and  A.PR_Date between  '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by A.PaymentMode";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderCategory(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.VendorID,A.PaymentMode,B.CategoryId,B.DescriptionId,G.BrandId,A.PR_NO,a.PR_Date,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and  A.PR_Date between  '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by B.CategoryId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderitem(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.VendorID,A.PaymentMode,B.CategoryId,B.DescriptionId,G.BrandId,A.PR_NO,a.PR_Date,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and  A.PR_Date between  '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by  B.DescriptionId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderBrand(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.VendorID,A.PaymentMode,B.CategoryId,B.DescriptionId,G.BrandId,A.PR_NO,a.PR_Date,E.LedgerName,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and  A.PR_Date between  '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by G.BrandId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }

        public DataSet ordebyVendor2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select E.LedgerName,A.PR_NO,a.PR_Date,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and   A.PR_Date between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by A.VendorID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }

        public DataSet ordebyPaymode2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select E.LedgerName,A.PR_NO,a.PR_Date,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and   A.PR_Date between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by A.PaymentMode";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyCategory2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select E.LedgerName,A.PR_NO,a.PR_Date,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and   A.PR_Date between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by B.CategoryId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyDifinition2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select E.LedgerName,A.PR_NO,a.PR_Date,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and   A.PR_Date between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by  B.DescriptionId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyBrand2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select E.LedgerName,A.PR_NO,a.PR_Date,D.Definition,C.category,B.Qty,B.Rate,A.TotalAmount,F.Payment_Mode,g.BrandName from tblPurchaseReturn_" + sTableName + " A,tblTransPurchaseReturn_" + sTableName + " B,tblcategory C ,tblCategoryUser D,tblLedger E,tblPaymentMode F,tblBrand G where A.P_ID=B.P_Id and B.CategoryId=C.categoryid and D.CategoryUserID=B.DescriptionId and A.VendorID=E.LedgerID and A.PaymentMode=F.Payment_ID and D.BrandCode=G.BrandId and   A.PR_Date between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' order by G.BrandId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
    }
}
