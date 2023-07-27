using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer;
using DataLayer;
using BusinessLayer;
using System.Data;

namespace BusinessLayer
{
   public  class CreditDebitReport
    {
        #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public CreditDebitReport()
        {
            dbObj = new DBAccess();
        }
        #endregion

        #region getCredit DebitReport
        public DataSet getCreditDebitReport(string sTable, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string sQry = "select A.ID,B.LedgerName,A.Date,A.Type,A.Amount,A.Narration,A.Note_NO,A.DayBook_ID from tblCreditDebitNote_" + sTable + " A,tblLedger B where A.Ledger_Name=B.LedgerID and Date between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";
            ds = dbObj.InlineExecuteDataSet(sQry);
            return ds;
        }
        #endregion
    }
}
