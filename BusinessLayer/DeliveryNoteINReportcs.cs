using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
   public class DeliveryNoteINReportcs
    {
         #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public DeliveryNoteINReportcs()
        {
            dbObj = new DBAccess();
        }
        #endregion


        public DataSet sOrdebyVendor(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,A.LedgerID,C.categoryid,D.CategoryUserID,G.Payment_Mode,A.Payment_Mode as PayID,E.BrandId from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.LedgerID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderPay(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,A.LedgerID,C.categoryid,D.CategoryUserID,G.Payment_Mode,A.Payment_Mode as PayID,E.BrandId from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.Payment_Mode";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderCategory(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,A.LedgerID,C.categoryid,D.CategoryUserID,G.Payment_Mode,A.Payment_Mode as PayID,E.BrandId from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by C.categoryid";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderitem(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,A.LedgerID,C.categoryid,D.CategoryUserID,G.Payment_Mode,A.Payment_Mode as PayID,E.BrandId from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by D.CategoryUserID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet sorderBrand(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = " select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,A.LedgerID,C.categoryid,D.CategoryUserID,G.Payment_Mode,A.Payment_Mode as PayID,E.BrandId from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by E.BrandId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }

        public DataSet ordebyVendor2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,G.Payment_Mode from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.LedgerID";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }

        public DataSet ordebyPaymode2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,G.Payment_Mode from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by A.Payment_Mode";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyCategory2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,G.Payment_Mode from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by C.categoryid";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyDifinition2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,G.Payment_Mode from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by D.CategoryUserID ";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
        public DataSet ordebyBrand2(string sTableName, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string paygird = "select A.Bill_No,A.Bill_Date,B.Rate,B.Qty,B.Amount,F.LedgerName,E.BrandName,C.category,D.Definition,G.Payment_Mode from tblDeliveryNoteIN_" + sTableName + " A,tblTransDeliveryNoteIN_" + sTableName + " B,tblcategory C,tblCategoryUser D,tblBrand E,tblLedger F,tblPaymentMode G where A.Bill_No=B.Bill_No and B.Category=C.categoryid and B.Product=D.CategoryUserID and D.BrandCode=E.BrandId and A.LedgerID=F.LedgerID and A.Payment_Mode=G.Payment_ID and  A.Bill_Date between  Convert(DateTime,'" + sFmdate + "',103) and Convert(DateTime,'" + sToDate + "',103) order by E.BrandId";
            ds = dbObj.InlineExecuteDataSet(paygird);
            return ds;
        }
    }
}
