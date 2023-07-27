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
  public  class journalsReport
    {
         #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public journalsReport()
        {
            dbObj = new DBAccess();
        }
        #endregion

        #region getJournals report
        public DataSet getJournalsreport(string sTable, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string sQry = "select debtor.ledgername as debtor,creditor.ledgername as creditor, * from ((tblDaybook_" + sTable + " inner join tblJournal_" + sTable + " on tblDaybook_" + sTable + ".TransNo = tblJournal_" + sTable + ".JournalId) inner join tblLedger debtor on tblDaybook_" + sTable + ".DebtorId = debtor.ledgerId) inner join tblLedger creditor on tblDaybook_" + sTable + ".CreditorId = creditor.ledgerId where JV_Date between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";
            ds = dbObj.InlineExecuteDataSet(sQry);
            return ds;
        }
        #endregion
    }
}
