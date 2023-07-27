using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
 public   class SalesReturnReport
    {
        #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public SalesReturnReport()
        {
            dbObj = new DBAccess();
        }
        #endregion
        public DataSet sOrdebyVendor(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.Customer_ID,B.CategoryID,B.DescriptionId,A.PaymentMode,G.BrandId,A.Bill_NO,a.Bill_date,E.LedgerName,D.category,C.Definition,B.Qty,B.Rate,A.Amount,F.Payment_Mode,g.BrandName from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C ,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.Customer_ID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderPay(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.Customer_ID,B.CategoryID,B.DescriptionId,A.PaymentMode,G.BrandId,A.Bill_NO,a.Bill_date,E.LedgerName,D.category,C.Definition,B.Qty,B.Rate,A.Amount,F.Payment_Mode,g.BrandName from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C ,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.PaymentMode";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderCategory(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.Customer_ID,B.CategoryID,B.DescriptionId,A.PaymentMode,G.BrandId,A.Bill_NO,a.Bill_date,E.LedgerName,D.category,C.Definition,B.Qty,B.Rate,A.Amount,F.Payment_Mode,g.BrandName from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C ,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by B.CategoryID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderitem(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.Customer_ID,B.CategoryID,B.DescriptionId,A.PaymentMode,G.BrandId,A.Bill_NO,a.Bill_date,E.LedgerName,D.category,C.Definition,B.Qty,B.Rate,A.Amount,F.Payment_Mode,g.BrandName from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C ,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by B.DescriptionId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderBrand(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Customer_ID,B.CategoryID,B.DescriptionId,A.PaymentMode,G.BrandId,A.Bill_NO,a.Bill_date,E.LedgerName,D.category,C.Definition,B.Qty,B.Rate,A.Amount,F.Payment_Mode,g.BrandName from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C ,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by G.BrandId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }

        public DataSet ordebyVendor2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_NO,a.Bill_date,E.LedgerName,D.category,C.Definition,g.BrandName ,B.Qty,B.Rate, F.Payment_Mode,A.Amount from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.Customer_ID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }

        public DataSet ordebyPaymode2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select F.Payment_Mode,A.Bill_NO,a.Bill_date,E.LedgerName,D.category,C.Definition,g.BrandName ,B.Qty,B.Rate, A.Amount from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.PaymentMode";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyCategory2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select D.category,A.Bill_NO,a.Bill_date,E.LedgerName,C.Definition,g.BrandName ,B.Qty,B.Rate,F.Payment_Mode, A.Amount from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by B.CategoryID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyDifinition2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select C.Definition,D.category,A.Bill_NO,a.Bill_date,E.LedgerName,g.BrandName ,B.Qty,B.Rate,F.Payment_Mode, A.Amount from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by B.DescriptionId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyBrand2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select g.BrandName,C.Definition,D.category,A.Bill_NO,a.Bill_date,E.LedgerName,B.Qty,B.Rate,F.Payment_Mode, A.Amount from tblSalesReturn_" + sTableName + " A,tblTransSaleReturn_" + sTableName + " B,tblCategoryUser C,tblcategory D,tblLedger E,tblPaymentMode F,tblBrand G where A.SR_ID=B.SR_Id and B.DescriptionId=C.CategoryUserID and B.CategoryId=D.categoryid and A.Customer_ID=E.LedgerID and A.PaymentMode=F.Payment_ID and C.BrandCode=G.BrandId and B.CategoryId=D.categoryid and  A.Bill_date between Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by G.BrandId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
    }
}
