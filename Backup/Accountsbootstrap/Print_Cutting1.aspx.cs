//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using CrystalDecisions.Web;
//using CrystalDecisions.Shared;
//using CrystalDecisions.CrystalReports.Engine;
//using System.Data.SqlClient;
//using BusinessLayer;
//using DataLayer;
//using CommonLayer;
//using System.Data;

//namespace Billing.Accountsbootstrap
//{
//    public partial class Print_Cutting1 : System.Web.UI.Page
//    {

//        BSClass objBs = new BSClass();
//        string sTableName = "";

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (Session["User"] != null)
//                sTableName = Session["User"].ToString();
//            else
//                Response.Redirect("login.aspx");
//            sTableName = Session["User"].ToString();

//            string iSalesID = Request.QueryString.Get("iSalesID");


//            if (iSalesID != null)
//            {
//                //SqlConnection connection = new SqlConnection("Data Source=SERVER;Initial Catalog=suxuslive;User ID=sa;Password=P@ss123");
//                //SqlCommand command = new SqlCommand(" select tblfab.Delivery_Challan,tblFab.FabNo as InvoiceNo,tblFab.RegDate,tblFab.Refno,tblFab.TotalMeter,tblLedger.LedgerName, " +
//                //                                    " tblFab.TotalAmount,  tblEmployeeDetails.Name,tblFab.InvDate,tblTransfab.Orderno,tblTransfab.DesignNo,tblTransfab.Color, " +
//                //                                    " tblWidth.Width,  tblTransfab.Meter,tblTransfab.Rate,tblTransfab.Imagepath from tblFab  " +
//                //                                    " inner join tblTransfab on tblFab.Fabid=tblTransfab.Fabid  inner join tblledger on tblFab.SupplierId=tblLedger.LedgerID " +
//                //                                    " inner join tblEmployeeDetails on tblFab.Checkedsign=tblEmployeeDetails.Employee_Id  " +
//                //                                    " inner join tblwidth on tblTransfab.Width=tblWidth.WidthID where tblFab.Fabid ='" + iSalesID + "'", connection);
//                //SqlDataAdapter adapter = new SqlDataAdapter(command);
//                //DataSet dataset = new DataSet();
//                //adapter.Fill(dataset, "FabricDS");


//                DataSet dataset = objBs.FabricreportCR(Convert.ToInt32(iSalesID));


//                ReportDocument CustomerReport = new ReportDocument();
//                CustomerReport.Load(Server.MapPath("~/Accountsbootstrap/crSalesreport.rpt"));
//                //CustomerReport.SetDataSource(dataset.Tables["FabricDS"]);
//                CustomerReport.SetDataSource(dataset.Tables[0]);
//                //CrystalReportViewer1.ReportSource = CustomerReport;
//                //CrystalReportViewer1.DataBind();


//                ParameterFields paramFields = new ParameterFields();
//                ParameterField pfItemYr = new ParameterField();
//                pfItemYr.ParameterFieldName = "No";
//                ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();
//                dcItemYr.Value = "Fabric Register Number - " + iSalesID;
//                pfItemYr.CurrentValues.Add(dcItemYr);
//                paramFields.Add(pfItemYr);
//              //  CrystalReportViewer1.ParameterFieldInfo = paramFields;

//            }
//        }
//    }
//}