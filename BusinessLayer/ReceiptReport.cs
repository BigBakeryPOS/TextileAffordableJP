using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using CommonLayer;
using System.Data;


namespace BusinessLayer
{
   public class ReceiptReport
    {
         
       #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public ReceiptReport()
        {
            dbObj = new DBAccess();
        }
        #endregion

        #region getReceipt byDateWise
        public DataSet getReceiptbyDateWise(string sTable, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string sQry = "select A.ReceiptNo,A.LedgerType,B.LedgerName,A.ReceiptDate,C.Payment_Mode,A.BankName,A.Chequeno,A.DaybookId ,A.Amount,A.Narration from tblReceipt_" + sTable + " A, tblLedger B,tblPaymentMode C where A.PaymodeID=C.Payment_ID and A.LedgerID=B.LedgerID  and ReceiptDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";
            ds = dbObj.InlineExecuteDataSet(sQry);
            return ds;
        }
        #endregion

        #region getReceipt byDateWise
        public DataSet getReceiptbyDateWise(string sTable, DateTime sFmdate, DateTime sToDate)
        {
            DataSet ds1 = new DataSet();
            DataSet dsReceipt = new DataSet();
            string City = "";

            if (sTable == "All")
            {

                string sqry1 = "select * from tblbranch";
                DataSet ds11 = dbObj.InlineExecuteDataSet(sqry1);

                for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
                {
                    string sbranch = ds11.Tables[0].Rows[i]["Branchcode"].ToString();
                    City = ds11.Tables[0].Rows[i]["Branchname"].ToString();

                    //string sQry = "select A.ReceiptNo,A.LedgerType,B.LedgerName,A.ReceiptDate,C.Payment_Mode,A.BankName,A.Chequeno,A.DaybookId ,A.Amount,A.Narration,'" + City + "' as Branchname from tblReceipt_" + sbranch + " A, tblLedger B,tblPaymentMode C where A.PaymodeID=C.Payment_ID and A.LedgerID=B.LedgerID and ReceiptDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";

                    //string sQry = "select A.ReceiptNo,A.LedgerType,B.LedgerName,A.ReceiptDate,C.Payment_Mode,A.BankName,A.Chequeno,A.DaybookId ,A.Amount,A.Narration,'" + City + "' as Branchname from tblReceipt_" + sbranch + " A, tblLedger B,tblPaymentMode C where A.PaymodeID=C.Payment_ID and A.LedgerID=B.LedgerID and ReceiptDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' " +
                    //              " union all " +
                    //              "select A.BillNo as ReceiptNo,'' as LedgerType,B.LedgerName,A.BillDate,C.Payment_Mode,'' as BankName,'' as Chequeno,A.DaybookId ,A.NetAmount as Amount,A.Narration,'" + City + "' as Branchname from tblSales_" + sbranch + " A, tblLedger B,tblPaymentMode C where A.Paymode=C.Payment_ID and A.customerID=B.LedgerID and BillDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' and A.PayMode=1";

                    string sQry = "select A.ReceiptNo,A.LedgerType,B.LedgerName,A.ReceiptDate,C.Payment_Mode,A.BankName,A.Chequeno,A.DaybookId ,A.Amount,A.Narration,'" + City + "' as Branchname from tblReceipt_" + sbranch + " A, tblLedger B,tblPaymentMode C where A.PaymodeID=C.Payment_ID and A.LedgerID=B.LedgerID and ReceiptDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "'";



                    ds1 = dbObj.InlineExecuteDataSet(sQry);

                    dsReceipt.Merge(ds1);

                }
            }
            else
            {

                string sqry1 = "select * from tblbranch where Branchcode ='" + sTable + "'";
                DataSet ds11 = dbObj.InlineExecuteDataSet(sqry1);

                //string sbranch = ds1.Tables[0].Rows[0]["Branchcode"].ToString();
                City = ds11.Tables[0].Rows[0]["Branchname"].ToString();
                //string sQry = "select A.ReceiptNo,A.LedgerType,B.LedgerName,A.ReceiptDate,C.Payment_Mode,A.BankName,A.Chequeno,A.DaybookId ,A.Amount,A.Narration,'" + City + "' as Branchname from tblReceipt_" + sTable + " A, tblLedger B,tblPaymentMode C where A.PaymodeID=C.Payment_ID and A.LedgerID=B.LedgerID and  ReceiptDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";

                //string sQry = "select A.ReceiptNo,A.LedgerType,B.LedgerName,A.ReceiptDate,C.Payment_Mode,A.BankName,A.Chequeno,A.DaybookId ,A.Amount,A.Narration,'" + City + "' as Branchname from tblReceipt_" + sTable + " A, tblLedger B,tblPaymentMode C where A.PaymodeID=C.Payment_ID and A.LedgerID=B.LedgerID and  ReceiptDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "'" +
                //              "union all " +
                //              "select A.BillNo as ReceiptNo,'' as LedgerType,B.LedgerName,A.BillDate,C.Payment_Mode,'' as BankName,'' as Chequeno,A.DaybookId ,A.NetAmount as Amount,A.Narration,'" + City + "' as Branchname from tblSales_" + sTable + " A, tblLedger B,tblPaymentMode C where A.Paymode=C.Payment_ID and A.customerID=B.LedgerID and  BillDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' and A.PayMode=1";

                string sQry = "select A.ReceiptNo,A.LedgerType,B.LedgerName,A.ReceiptDate,C.Payment_Mode,A.BankName,A.Chequeno,A.DaybookId ,A.Amount,A.Narration,'" + City + "' as Branchname from tblReceipt_" + sTable + " A, tblLedger B,tblPaymentMode C where A.PaymodeID=C.Payment_ID and A.LedgerID=B.LedgerID and  ReceiptDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "'";


                dsReceipt = dbObj.InlineExecuteDataSet(sQry);
            }



            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;


            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("ReceiptNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("DaybookId");
            dt.Columns.Add(dc);

            dc = new DataColumn("LedgerType");
            dt.Columns.Add(dc);

            dc = new DataColumn("ReceiptDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("LedgerName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Payment_Mode");
            dt.Columns.Add(dc);

            dc = new DataColumn("BankName");
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

            if (dsReceipt.Tables[0].Rows.Count == 0)
            {
                drNew = dt.NewRow();
                drNew["ReceiptNo"] = string.Empty;
                drNew["DaybookId"] = string.Empty;
                drNew["LedgerType"] = string.Empty;

                drNew["ReceiptDate"] = string.Empty;
                drNew["LedgerName"] = string.Empty;
                drNew["Payment_Mode"] = string.Empty;
                drNew["Chequeno"] = string.Empty;
                drNew["BankName"] = string.Empty;
                drNew["Amount"] = string.Empty;
                drNew["Narration"] = string.Empty;

                drNew["BranchName"] = string.Empty;

                ds.Tables[0].Rows.Add(drNew);
            }
            else
            {
                foreach (DataRow drParentQry in dsReceipt.Tables[0].Rows)
                {


                    drNew = dt.NewRow();
                    drNew["ReceiptNo"] = drParentQry["ReceiptNo"].ToString();
                    drNew["DaybookId"] = drParentQry["DaybookId"].ToString();
                    drNew["LedgerType"] = Convert.ToString(drParentQry["LedgerType"].ToString());

                    drNew["ReceiptDate"] = Convert.ToDateTime(drParentQry["ReceiptDate"]).ToString("dd/MM/yyyy");
                    drNew["LedgerName"] = Convert.ToString(drParentQry["LedgerName"].ToString());
                    drNew["Payment_Mode"] = Convert.ToString(drParentQry["Payment_Mode"].ToString());
                    drNew["Chequeno"] = drParentQry["Chequeno"].ToString();
                    drNew["BankName"] = drParentQry["BankName"].ToString();
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
