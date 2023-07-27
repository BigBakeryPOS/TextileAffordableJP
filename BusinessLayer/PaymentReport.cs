using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using CommonLayer;
using System.Data;


namespace BusinessLayer
{
   public class PaymentReport
    {
        #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public PaymentReport()
        {
            dbObj = new DBAccess();
        }
        #endregion

        #region getPayment byDateWise
        //public DataSet getPaymentbyDateWise(string sTable, string sFmdate, string sToDate)
        //{
        //    DataSet ds = new DataSet();
        //    string sQry = "select A.PaymentID,A.LedgerType,C.Payment_Mode,A.PaymentDate,B.LedgerName,A.Amount,A.BankName,A.Chequeno,A.DaybookId,A.Narration from  tblPayment_" + sTable + " A,tblLedger B,tblPaymentMode C where A.LedgerID=B.LedgerID and A.PayModeID=C.Payment_ID and PaymentDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";
        //    ds = dbObj.InlineExecuteDataSet(sQry);
        //    return ds;
        //}
        #endregion

        #region getPayment byDateWise
        public DataSet getPaymentbyDateWise(string sTable, DateTime sFmdate, DateTime sToDate)
        {


            DataSet dsPayment = new DataSet();
            DataSet dsPayment1 = new DataSet();
            string City = "";
            if (sTable == "All")
            {
                string sqry1 = "select * from tblbranch";
                DataSet ds1 = dbObj.InlineExecuteDataSet(sqry1);

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    string sbranch = ds1.Tables[0].Rows[i]["Branchcode"].ToString();
                    City = ds1.Tables[0].Rows[i]["City"].ToString();

                    string sQry = "select A.PaymentID,A.LedgerType,C.Payment_Mode,A.PaymentDate,B.LedgerName,A.Amount,A.BankName,A.Chequeno,A.DaybookId,A.Narration,'" + City + "' as Branchname from  tblPayment_" + sbranch + " A,tblLedger B,tblPaymentMode1 C where A.LedgerID=B.LedgerID and A.PayModeID=C.Payment_ID and PaymentDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";
                    dsPayment1 = dbObj.InlineExecuteDataSet(sQry);

                    dsPayment.Merge(dsPayment1);
                }
            }
            else
            {
                string sqry1 = "select * from tblbranch where Branchcode ='" + sTable + "'";
                DataSet ds1 = dbObj.InlineExecuteDataSet(sqry1);

                //string sbranch = ds1.Tables[0].Rows[0]["Branchcode"].ToString();
                City = ds1.Tables[0].Rows[0]["City"].ToString();

                string sQry = "select A.PaymentID,A.LedgerType,C.Payment_Mode,A.PaymentDate,B.LedgerName,A.Amount,A.BankName,A.Chequeno,A.DaybookId,A.Narration,'" + City + "' as Branchname from  tblPayment_" + sTable + " A,tblLedger B,tblPaymentMode1 C where A.LedgerID=B.LedgerID and  A.PayModeID=C.Payment_ID and PaymentDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";
                dsPayment = dbObj.InlineExecuteDataSet(sQry);
            }


            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;


            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("PaymentID");
            dt.Columns.Add(dc);

            dc = new DataColumn("DaybookId");
            dt.Columns.Add(dc);

            dc = new DataColumn("LedgerType");
            dt.Columns.Add(dc);

            dc = new DataColumn("PaymentDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("LedgerName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Payment_Mode");
            dt.Columns.Add(dc);

            dc = new DataColumn("Chequeno");
            dt.Columns.Add(dc);

            dc = new DataColumn("Amount");
            dt.Columns.Add(dc);


            dc = new DataColumn("Narration");
            dt.Columns.Add(dc);


            dc = new DataColumn("BranchName");
            dt.Columns.Add(dc);


            ds.Tables.Add(dt);

            if (dsPayment.Tables[0].Rows.Count == 0)
            {
                drNew = dt.NewRow();
                drNew["PaymentID"] = string.Empty;
                drNew["DaybookId"] = string.Empty;
                drNew["LedgerType"] = string.Empty;

                drNew["PaymentDate"] = string.Empty;
                drNew["LedgerName"] = string.Empty;
                drNew["Payment_Mode"] = string.Empty;
                drNew["Chequeno"] = string.Empty;
                drNew["Amount"] = string.Empty;
                drNew["Narration"] = string.Empty;

                drNew["BranchName"] = string.Empty;

                ds.Tables[0].Rows.Add(drNew);
            }
            else
            {
                foreach (DataRow drParentQry in dsPayment.Tables[0].Rows)
                {


                    drNew = dt.NewRow();
                    drNew["PaymentID"] = drParentQry["PaymentID"].ToString();
                    drNew["DaybookId"] = drParentQry["DaybookId"].ToString();
                    drNew["LedgerType"] = Convert.ToString(drParentQry["LedgerType"].ToString());

                    drNew["PaymentDate"] = Convert.ToDateTime(drParentQry["PaymentDate"]).ToString("dd/MM/yyyy");
                    drNew["LedgerName"] = Convert.ToString(drParentQry["LedgerName"].ToString());
                    drNew["Payment_Mode"] = Convert.ToString(drParentQry["Payment_Mode"].ToString());
                    drNew["Chequeno"] = drParentQry["Chequeno"].ToString();
                    drNew["Amount"] = Convert.ToDouble(drParentQry["Amount"]).ToString("0.00");
                    drNew["Narration"] = Convert.ToString(drParentQry["Narration"].ToString());
                    drNew["BranchName"] = Convert.ToString(drParentQry["Branchname"].ToString());

                    ds.Tables[0].Rows.Add(drNew);
                }
            }


            return ds;
        }
        #endregion
    }
}
