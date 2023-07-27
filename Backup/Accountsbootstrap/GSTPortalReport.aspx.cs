using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;
using ClosedXML.Excel;
using System.Data.OleDb;
using System.Data.Common;
using Newtonsoft;
using Newtonsoft.Json;
namespace Billing.Accountsbootstrap
{
    public partial class GSTPortalReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        public Double damt = 0.0;
        public Double camt = 0.0;
        public Double dDiffamt = 0.0;
        public Double cDiffamt = 0.0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sTableName = string.Empty;
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
                string super = Session["IsSuperAdmin"].ToString();
                //string sTableName = Session["User"].ToString();

                if (super == "1")
                {
                    ddloutlet.Enabled = true;
                    DataSet dsbranchto = objBs.Branchto();
                    ddloutlet.DataSource = dsbranchto.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    ddloutlet.Items.Insert(0, "All");
                }
                else
                {
                    DataSet dsbranch = new DataSet();
                    dsbranch = objBs.Branchfrom(sTableName);
                    ddloutlet.DataSource = dsbranch.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    ddloutlet.Enabled = false;
                }

                DataSet dsbranchNew = new DataSet();
                dsbranchNew = objBs.Branchfrom(sTableName);
                if (dsbranchNew.Tables[0].Rows.Count > 0)
                {
                    txtGSTNo.Text = dsbranchNew.Tables[0].Rows[0]["cstno"].ToString();
                }

                //ddloutlet.Enabled = true;
                //DataSet dsbranchto = objBs.Branchto();
                //ddloutlet.DataSource = dsbranchto.Tables[0];
                //ddloutlet.DataTextField = "branchName";
                //ddloutlet.DataValueField = "branchcode";
                //ddloutlet.DataBind();
                //ddloutlet.Items.Insert(0, "All");

                string Branch = ddloutlet.SelectedValue;
                txtfrmdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }


        public DataSet getDataSetExportToExcel()
        {
            DateTime frmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet dsNEW = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt2New = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();
            DataTable dt9 = new DataTable();
            DataTable dt9New = new DataTable();
            DataTable dt10 = new DataTable();
            DataTable dt11 = new DataTable();
            DataTable dtB2B = new DataTable();

            #region B2B
            DataSet ds = objBs.gstreportB2B(frmdate, todate, ddloutlet.SelectedValue);

            DataRow dr_export1b2b = dtB2B.NewRow();
            dtB2B.Columns.Add(new DataColumn("GSTIN/UIN of Recipient"));
            dtB2B.Columns.Add(new DataColumn("Receiver Name"));
            dtB2B.Columns.Add(new DataColumn("INVOICE NUMBER"));
            dtB2B.Columns.Add(new DataColumn("INVOICE DATE"));
            dtB2B.Columns.Add(new DataColumn("INVOICE VALUE"));
            dtB2B.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            dtB2B.Columns.Add(new DataColumn("REVERSE CHARGE"));
            dtB2B.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dtB2B.Columns.Add(new DataColumn("INVOICE TYPE"));
            dtB2B.Columns.Add(new DataColumn("E-Commerce GSTIN"));
            //dt.Columns.Add(new DataColumn("SERIAL NUMBER"));
            dtB2B.Columns.Add(new DataColumn("RATE"));
            dtB2B.Columns.Add(new DataColumn("TAXABLE VALUE"));
            //dt.Columns.Add(new DataColumn("INTEGRATED TAX"));
            //dt.Columns.Add(new DataColumn("CENTRAL TAX"));
            //dt.Columns.Add(new DataColumn("STATE OR UT TAX"));
            dtB2B.Columns.Add(new DataColumn("CESS AMOUNT"));
            dtB2B.TableName = "b2b";

            dr_export1b2b["GSTIN/UIN of Recipient"] = "Summary For B2B(4)";
            dr_export1b2b["Receiver Name"] = "";
            dr_export1b2b["INVOICE NUMBER"] = "";
            dr_export1b2b["INVOICE DATE"] = "";
            dr_export1b2b["INVOICE VALUE"] = "";
            dr_export1b2b["PLACE OF SUPPLY"] = "";
            dr_export1b2b["REVERSE CHARGE"] = "";
            dr_export1b2b["APPLICABLE % OF TAX RATE"] = "";
            dr_export1b2b["INVOICE TYPE"] = "";
            dr_export1b2b["E-Commerce GSTIN"] = "";
            //dr_export["SERIAL NUMBER"] = dr["SerialNo"];
            dr_export1b2b["RATE"] = "";
            dr_export1b2b["TAXABLE VALUE"] = "";
            dr_export1b2b["CESS AMOUNT"] = "";
            dtB2B.Rows.Add(dr_export1b2b);

            dr_export1b2b = dtB2B.NewRow();
            dr_export1b2b["GSTIN/UIN of Recipient"] = "";
            dr_export1b2b["Receiver Name"] = "";
            dr_export1b2b["INVOICE NUMBER"] = "";
            dr_export1b2b["INVOICE DATE"] = "";
            dr_export1b2b["INVOICE VALUE"] = "";
            dr_export1b2b["PLACE OF SUPPLY"] = "";
            dr_export1b2b["REVERSE CHARGE"] = "";
            dr_export1b2b["APPLICABLE % OF TAX RATE"] = "";
            dr_export1b2b["INVOICE TYPE"] = "";
            dr_export1b2b["E-Commerce GSTIN"] = "";
            //dr_export["SERIAL NUMBER"] = dr["SerialNo"];
            dr_export1b2b["RATE"] = "";
            dr_export1b2b["TAXABLE VALUE"] = "";
            dr_export1b2b["CESS AMOUNT"] = "";
            dtB2B.Rows.Add(dr_export1b2b);

            dr_export1b2b = dtB2B.NewRow();
            dr_export1b2b["GSTIN/UIN of Recipient"] = "";
            dr_export1b2b["Receiver Name"] = "";
            dr_export1b2b["INVOICE NUMBER"] = "";
            dr_export1b2b["INVOICE DATE"] = "";
            dr_export1b2b["INVOICE VALUE"] = "";
            dr_export1b2b["PLACE OF SUPPLY"] = "";
            dr_export1b2b["REVERSE CHARGE"] = "";
            dr_export1b2b["APPLICABLE % OF TAX RATE"] = "";
            dr_export1b2b["INVOICE TYPE"] = "";
            dr_export1b2b["E-Commerce GSTIN"] = "";
            //dr_export["SERIAL NUMBER"] = dr["SerialNo"];
            dr_export1b2b["RATE"] = "";
            dr_export1b2b["TAXABLE VALUE"] = "";
            dr_export1b2b["CESS AMOUNT"] = "";
            dtB2B.Rows.Add(dr_export1b2b);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region
                    //dt.Columns.Add(new DataColumn("GSTIN/UIN of Recipient"));
                    //dt.Columns.Add(new DataColumn("Receiver Name"));
                    //dt.Columns.Add(new DataColumn("INVOICE NUMBER"));
                    //dt.Columns.Add(new DataColumn("INVOICE DATE"));
                    //dt.Columns.Add(new DataColumn("INVOICE VALUE"));
                    //dt.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    //dt.Columns.Add(new DataColumn("REVERSE CHARGE"));
                    //dt.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                    //dt.Columns.Add(new DataColumn("INVOICE TYPE"));
                    //dt.Columns.Add(new DataColumn("E-Commerce GSTIN"));
                    ////dt.Columns.Add(new DataColumn("SERIAL NUMBER"));
                    //dt.Columns.Add(new DataColumn("RATE"));
                    //dt.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    ////dt.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    ////dt.Columns.Add(new DataColumn("CENTRAL TAX"));
                    ////dt.Columns.Add(new DataColumn("STATE OR UT TAX"));
                    //dt.Columns.Add(new DataColumn("CESS AMOUNT"));
                    //dt.TableName = "B2B";


                    dr_export1b2b = dtB2B.NewRow();
                    dr_export1b2b["GSTIN/UIN of Recipient"] = "GSTIN/UIN of Recipient";
                    dr_export1b2b["Receiver Name"] = "Receiver Name";
                    dr_export1b2b["INVOICE NUMBER"] = "INVOICE NUMBER";
                    dr_export1b2b["INVOICE DATE"] = "INVOICE DATE";
                    dr_export1b2b["INVOICE VALUE"] = "INVOICE VALUE";
                    dr_export1b2b["PLACE OF SUPPLY"] = "PLACE OF SUPPLY";
                    dr_export1b2b["REVERSE CHARGE"] = "REVERSE CHARGE";
                    dr_export1b2b["APPLICABLE % OF TAX RATE"] = "APPLICABLE % OF TAX RATE";
                    dr_export1b2b["INVOICE TYPE"] = "INVOICE TYPE";
                    dr_export1b2b["E-Commerce GSTIN"] = "E-Commerce GSTIN";
                    //dr_export["SERIAL NUMBER"] = dr["SerialNo"];
                    dr_export1b2b["RATE"] = "RATE";
                    dr_export1b2b["TAXABLE VALUE"] = "TAXABLE VALUE";
                    dr_export1b2b["CESS AMOUNT"] = "CESS AMOUNT";
                    dtB2B.Rows.Add(dr_export1b2b);


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dtB2B.NewRow();
                        dr_export["GSTIN/UIN of Recipient"] = dr["GSTINNo"];
                        dr_export["Receiver Name"] = "";
                        dr_export["INVOICE NUMBER"] = dr["FullBillNo"];
                        dr_export["INVOICE DATE"] = Convert.ToDateTime(dr["BillDate"]).ToString("dd-MMM-yy");
                        dr_export["INVOICE VALUE"] = Convert.ToDouble(dr["RoundOff"]).ToString("F2");
                        dr_export["PLACE OF SUPPLY"] = dr["PlaceOfSupply"];
                        dr_export["REVERSE CHARGE"] = dr["ReverseCharge"];
                        dr_export["APPLICABLE % OF TAX RATE"] = dr["ApplicablePer"];
                        dr_export["INVOICE TYPE"] = dr["InvoiceType"];
                        dr_export["E-Commerce GSTIN"] = "";
                        //dr_export["SERIAL NUMBER"] = dr["SerialNo"];
                        dr_export["RATE"] = dr["Rate"];
                        DataSet dscnt = objBs.gstcountreportB2B(ddloutlet.SelectedValue, dr["FullBillNo"].ToString());
                        if (dscnt.Tables[0].Rows.Count > 0)
                        {
                            // dr_export["TAXABLE VALUE"] = Convert.ToDouble(Convert.ToDouble(dr["RoundOff"]) - Convert.ToDouble(Convert.ToDouble(dr["TotalTax"]) / Convert.ToDouble(dscnt.Tables[0].Rows[0]["count"]))).ToString("F2");
                            dr_export["TAXABLE VALUE"] = Convert.ToDouble(Convert.ToDouble(dr["RoundOff"]) - Convert.ToDouble(Convert.ToDouble(dr["TotalTax"]) / 1)).ToString("F2");
                        }
                        else
                        {
                            dr_export["TAXABLE VALUE"] = Convert.ToDouble(Convert.ToDouble(dr["RoundOff"]) - Convert.ToDouble(Convert.ToDouble(dr["TotalTax"]) / 1)).ToString("F2");
                        }
                        if (dr["Province"].ToString() == "1")
                        {
                            //dr_export["INTEGRATED TAX"] = Convert.ToDouble("0").ToString("N2");
                            //dr_export["CENTRAL TAX"] = Convert.ToDouble(Convert.ToDouble(dr["TaxableValue"]) / 2).ToString("F2");
                            //dr_export["STATE OR UT TAX"] = Convert.ToDouble(Convert.ToDouble(dr["TaxableValue"]) / 2).ToString("F2");
                        }
                        else
                        {
                            //dr_export["INTEGRATED TAX"] = Convert.ToDouble(dr["TaxableValue"]).ToString("F2");
                            //dr_export["CENTRAL TAX"] = Convert.ToDouble("0").ToString("F2");
                            //dr_export["STATE OR UT TAX"] = Convert.ToDouble("0").ToString("F2");
                        }

                        dr_export["CESS AMOUNT"] = Convert.ToDouble(dr["CESS"]).ToString("F2");
                        dtB2B.Rows.Add(dr_export);
                    }

                    #endregion
                }
                else
                {
                    #region
                    //dt.Columns.Add(new DataColumn("GSTIN/UIN of Recipient"));
                    //dt.Columns.Add(new DataColumn("Receiver Name"));
                    //dt.Columns.Add(new DataColumn("INVOICE NUMBER"));
                    //dt.Columns.Add(new DataColumn("INVOICE DATE"));
                    //dt.Columns.Add(new DataColumn("INVOICE VALUE"));
                    //dt.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    //dt.Columns.Add(new DataColumn("REVERSE CHARGE"));
                    //dt.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                    //dt.Columns.Add(new DataColumn("INVOICE TYPE"));
                    //dt.Columns.Add(new DataColumn("E-Commerce GSTIN"));
                    ////dt.Columns.Add(new DataColumn("SERIAL NUMBER"));
                    //dt.Columns.Add(new DataColumn("RATE"));
                    //dt.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    ////dt.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    ////dt.Columns.Add(new DataColumn("CENTRAL TAX"));
                    ////dt.Columns.Add(new DataColumn("STATE OR UT TAX"));
                    //dt.Columns.Add(new DataColumn("CESS AMOUNT"));
                    //dt.TableName = "B2B";
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    DataRow dr_export = dt.NewRow();
                    //    dr_export["GSTIN/UIN of Recipient"] = "";
                    //    dr_export["Receiver Name"] = "";
                    //    dr_export["INVOICE NUMBER"] = "";
                    //    dr_export["INVOICE DATE"] = "";
                    //    dr_export["INVOICE VALUE"] = "";
                    //    dr_export["PLACE OF SUPPLY"] = "";
                    //    dr_export["REVERSE CHARGE"] = "";
                    //    dr_export["APPLICABLE % OF TAX RATE"] = "";
                    //    dr_export["INVOICE TYPE"] = "";
                    //    dr_export["E-Commerce GSTIN"] = "";
                    //    //dr_export["SERIAL NUMBER"] = "";
                    //    dr_export["RATE"] = "";
                    //    dr_export["TAXABLE VALUE"] = "";
                    //    //dr_export["INTEGRATED TAX"] = "";
                    //    //dr_export["CENTRAL TAX"] = "";
                    //    //dr_export["STATE OR UT TAX"] = "";
                    //    dr_export["CESS AMOUNT"] = "";
                    //    dt.Rows.Add(dr_export);
                    //}

                    #endregion
                }
            }
            else
            {
                #region
                //dt.Columns.Add(new DataColumn("GSTIN/UIN of Recipient"));
                //dt.Columns.Add(new DataColumn("Receiver Name"));
                //dt.Columns.Add(new DataColumn("INVOICE NUMBER"));
                //dt.Columns.Add(new DataColumn("INVOICE DATE"));
                //dt.Columns.Add(new DataColumn("INVOICE VALUE"));
                //dt.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                //dt.Columns.Add(new DataColumn("REVERSE CHARGE"));
                //dt.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                //dt.Columns.Add(new DataColumn("INVOICE TYPE"));
                //dt.Columns.Add(new DataColumn("E-Commerce GSTIN"));
                ////dt.Columns.Add(new DataColumn("SERIAL NUMBER"));
                //dt.Columns.Add(new DataColumn("RATE"));
                //dt.Columns.Add(new DataColumn("TAXABLE VALUE"));
                ////dt.Columns.Add(new DataColumn("INTEGRATED TAX"));
                ////dt.Columns.Add(new DataColumn("CENTRAL TAX"));
                ////dt.Columns.Add(new DataColumn("STATE OR UT TAX"));
                //dt.Columns.Add(new DataColumn("CESS AMOUNT"));
                //dt.TableName = "B2B";
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    DataRow dr_export = dt.NewRow();
                //    dr_export["GSTIN/UIN of Recipient"] = "";
                //    dr_export["Receiver Name"] = "";
                //    dr_export["INVOICE NUMBER"] = "";
                //    dr_export["INVOICE DATE"] = "";
                //    dr_export["INVOICE VALUE"] = "";
                //    dr_export["PLACE OF SUPPLY"] = "";
                //    dr_export["REVERSE CHARGE"] = "";
                //    dr_export["APPLICABLE % OF TAX RATE"] = "";
                //    dr_export["INVOICE TYPE"] = "";
                //    dr_export["E-Commerce GSTIN"] = "";
                //    //dr_export["SERIAL NUMBER"] = "";
                //    dr_export["RATE"] = "";
                //    dr_export["TAXABLE VALUE"] = "";
                //    //dr_export["INTEGRATED TAX"] = "";
                //    //dr_export["CENTRAL TAX"] = "";
                //    //dr_export["STATE OR UT TAX"] = "";
                //    dr_export["CESS AMOUNT"] = "";
                //    dt.Rows.Add(dr_export);
                //}

                #endregion
            }


            #endregion

            #region B2CL
            DataSet ds1 = objBs.gstreportB2CL(frmdate, todate, ddloutlet.SelectedValue);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    #region

                    dt1.Columns.Add(new DataColumn("INVOICE NUMBER"));
                    dt1.Columns.Add(new DataColumn("INVOICE DATE"));
                    dt1.Columns.Add(new DataColumn("INVOICE VALUE"));
                    dt1.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    dt1.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));

                    //dt1.Columns.Add(new DataColumn("SERIAL NUMBER"));
                    dt1.Columns.Add(new DataColumn("RATE"));
                    dt1.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    //dt1.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    dt1.Columns.Add(new DataColumn("CESS AMOUNT"));
                    dt1.Columns.Add(new DataColumn("E-Commerce GSTIN"));
                    dt1.TableName = "b2cl";
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        DataRow dr_export1 = dt1.NewRow();
                        dr_export1["INVOICE NUMBER"] = dr1["FullBillNo"];
                        dr_export1["INVOICE DATE"] = Convert.ToDateTime(dr1["BillDate"]).ToString("dd-MMM-yy");
                        dr_export1["INVOICE VALUE"] = Convert.ToDouble(dr1["RoundOff"]).ToString("F2");
                        dr_export1["PLACE OF SUPPLY"] = dr1["PlaceOfSupply"];
                        dr_export1["APPLICABLE % OF TAX RATE"] = dr1["ApplicablePer"];
                        //dr_export1["SERIAL NUMBER"] = dr1["SerialNo"];
                        dr_export1["RATE"] = dr1["Rate"];
                        dr_export1["TAXABLE VALUE"] = Convert.ToDouble(dr1["TaxableValue"]).ToString("F2");
                        //dr_export1["INTEGRATED TAX"] = Convert.ToDouble(dr1["TaxableValue"]).ToString("F2");
                        dr_export1["CESS AMOUNT"] = Convert.ToDouble(dr1["CESS"]).ToString("F2");
                        dr_export1["E-Commerce GSTIN"] = dr1["GSTINNo"];
                        dt1.Rows.Add(dr_export1);
                    }

                    #endregion
                }
                else
                {
                    #region

                    dt1.Columns.Add(new DataColumn("INVOICE NUMBER"));
                    dt1.Columns.Add(new DataColumn("INVOICE DATE"));
                    dt1.Columns.Add(new DataColumn("INVOICE VALUE"));
                    dt1.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    dt1.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                    //dt1.Columns.Add(new DataColumn("SERIAL NUMBER"));
                    dt1.Columns.Add(new DataColumn("RATE"));
                    dt1.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    //dt1.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    dt1.Columns.Add(new DataColumn("CESS AMOUNT"));
                    dt1.Columns.Add(new DataColumn("E-Commerce GSTIN"));

                    dt1.TableName = "b2cl";
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        DataRow dr_export1 = dt1.NewRow();
                        dr_export1["INVOICE NUMBER"] = "";
                        dr_export1["INVOICE DATE"] = "";
                        dr_export1["INVOICE VALUE"] = "";
                        dr_export1["PLACE OF SUPPLY"] = "";
                        dr_export1["APPLICABLE % OF TAX RATE"] = "";
                        //dr_export1["SERIAL NUMBER"] = "";
                        dr_export1["RATE"] = "";
                        dr_export1["TAXABLE VALUE"] = "";
                        //dr_export1["INTEGRATED TAX"] = "";
                        dr_export1["CESS AMOUNT"] = "";
                        dr_export1["E-Commerce GSTIN"] = "";
                        dt1.Rows.Add(dr_export1);
                    }

                    #endregion
                }
            }
            else
            {
                #region

                dt1.Columns.Add(new DataColumn("INVOICE NUMBER"));
                dt1.Columns.Add(new DataColumn("INVOICE DATE"));
                dt1.Columns.Add(new DataColumn("INVOICE VALUE"));
                dt1.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                dt1.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                //dt1.Columns.Add(new DataColumn("SERIAL NUMBER"));
                dt1.Columns.Add(new DataColumn("RATE"));
                dt1.Columns.Add(new DataColumn("TAXABLE VALUE"));
                //dt1.Columns.Add(new DataColumn("INTEGRATED TAX"));
                dt1.Columns.Add(new DataColumn("CESS AMOUNT"));
                dt1.Columns.Add(new DataColumn("E-Commerce GSTIN"));
                dt1.TableName = "b2cl";
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    DataRow dr_export1 = dt1.NewRow();
                    dr_export1["INVOICE NUMBER"] = "";
                    dr_export1["INVOICE DATE"] = "";
                    dr_export1["INVOICE VALUE"] = "";
                    dr_export1["PLACE OF SUPPLY"] = "";
                    dr_export1["APPLICABLE % OF TAX RATE"] = "";
                    //dr_export1["SERIAL NUMBER"] = "";
                    dr_export1["RATE"] = "";
                    dr_export1["TAXABLE VALUE"] = "";
                    //dr_export1["INTEGRATED TAX"] = "";
                    dr_export1["CESS AMOUNT"] = "";
                    dr_export1["E-Commerce GSTIN"] = "";
                    dt1.Rows.Add(dr_export1);
                }

                #endregion
            }
            #endregion

            #region B2CS
            DataSet ds11 = objBs.gstreportB2CS(frmdate, todate, ddloutlet.SelectedValue);


            if (ds11 != null)
            {
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    #region

                    //DataTable dt1 = new DataTable();
                    dt2New.Columns.Add(new DataColumn("TYPE"));
                    dt2New.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    //// dt2.Columns.Add(new DataColumn("SUPPLY TYPE"));
                    dt2New.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                    dt2New.Columns.Add(new DataColumn("RATE"));
                    dt2New.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    //// dt2.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    ////  dt2.Columns.Add(new DataColumn("CENTRAL TAX"));
                    ////  dt2.Columns.Add(new DataColumn("STATE OR UT TAX"));
                    dt2New.Columns.Add(new DataColumn("CESS AMOUNT"));
                    dt2New.Columns.Add(new DataColumn("E-Commerce GSTIN"));

                    dt2New.TableName = "b2cs";
                    foreach (DataRow dr1 in ds11.Tables[0].Rows)
                    {
                        DataRow dr_export2 = dt2New.NewRow();
                        //dr_export1["JobType_Name"] = dr1["JobType_Name"];
                        //dr_export1["IsActive"] = dr1["IsActive"];
                        dr_export2["TYPE"] = "OE";
                        dr_export2["PLACE OF SUPPLY"] = dr1["PlaceOfSupply"];
                        ////dr_export2["SUPPLY TYPE"] = "";
                        dr_export2["APPLICABLE % OF TAX RATE"] = "";
                        dr_export2["RATE"] = dr1["Rate"];
                        DataSet dscnt = objBs.gstcountreportB2B(ddloutlet.SelectedValue, dr1["FullBillNo"].ToString());
                        if (dscnt.Tables[0].Rows.Count > 0)
                        {
                            //dr_export2["TAXABLE VALUE"] = Convert.ToDouble(Convert.ToDouble(dr1["RoundOff"]) - Convert.ToDouble(Convert.ToDouble(dr1["TotalTax"]) / Convert.ToDouble(dscnt.Tables[0].Rows[0]["count"]))).ToString("F2");
                            dr_export2["TAXABLE VALUE"] = Convert.ToDouble(Convert.ToDouble(dr1["RoundOff"]) - Convert.ToDouble(Convert.ToDouble(dr1["TotalTax"]) / 1)).ToString("F2");
                        }
                        else
                        {
                            //dr_export2["TAXABLE VALUE"] = Convert.ToDouble(Convert.ToDouble(dr1["RoundOff"]) - Convert.ToDouble(Convert.ToDouble(dr1["TotalTax"]) / 1)).ToString("F2");
                        }
                        ////dr_export2["INTEGRATED TAX"] = "";
                        ////dr_export2["CENTRAL TAX"] = "";
                        ////dr_export2["STATE OR UT TAX"] = "";
                        dr_export2["CESS AMOUNT"] = "";
                        dr_export2["E-Commerce GSTIN"] = "";
                        dt2New.Rows.Add(dr_export2);
                    }

                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            }

            DataSet dsc = new DataSet();
            dsc.Tables.Add(dt2New);

            dt2.Columns.Add(new DataColumn("TYPE"));
            dt2.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            dt2.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt2.Columns.Add(new DataColumn("RATE"));
            dt2.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt2.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt2.Columns.Add(new DataColumn("E-Commerce GSTIN"));
            dt2.TableName = "b2cs";


            var result = from r in dsc.Tables[0].AsEnumerable()
                         group r by new { type = r["Type"], placeofsupply = r["PLACE OF SUPPLY"], rate = r["RATE"] } into g
                         select new
                         {
                             type = g.Key.type,
                             placeofsupply = g.Key.placeofsupply,
                             rate = g.Key.rate,
                             taxablevalue = g.Sum(x => Convert.ToDouble(x["TAXABLE VALUE"])),
                         };

            int gg = 0;
            foreach (var g in result)
            {
                DataRow dr_export2 = dt2.NewRow();
                dr_export2["TYPE"] = g.type;
                dr_export2["PLACE OF SUPPLY"] = g.placeofsupply;
                dr_export2["APPLICABLE % OF TAX RATE"] = "";
                dr_export2["RATE"] = g.rate;
                dr_export2["TAXABLE VALUE"] = Convert.ToDouble(g.taxablevalue).ToString("F2");
                dr_export2["CESS AMOUNT"] = "";
                dr_export2["E-Commerce GSTIN"] = "";
                gg++;
                dt2.Rows.Add(dr_export2);
            }
            #endregion

            //#region NIL
            ////DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            ////if (ds1 != null)
            ////{
            ////    if (ds1.Tables[0].Rows.Count > 0)
            ////    {
            //#region

            ////DataTable dt1 = new DataTable();
            //dt3.Columns.Add(new DataColumn("NATURE OF SUPPLY"));
            //dt3.Columns.Add(new DataColumn("NIL RATED SUPPLIES"));
            //dt3.Columns.Add(new DataColumn("EXEMPTED"));
            //dt3.Columns.Add(new DataColumn("NON-GST SUPPLIES"));
            //dt3.TableName = "NIL";
            ////foreach (DataRow dr1 in ds1.Tables[0].Rows)
            ////{
            //DataRow dr_export3 = dt3.NewRow();
            ////dr_export1["JobType_Name"] = dr1["JobType_Name"];
            ////dr_export1["IsActive"] = dr1["IsActive"];
            //dr_export3["NATURE OF SUPPLY"] = "";
            //dr_export3["NIL RATED SUPPLIES"] = "";
            //dr_export3["EXEMPTED"] = "";
            //dr_export3["NON-GST SUPPLIES"] = "";
            //dt3.Rows.Add(dr_export3);
            ////}

            //#endregion
            ////    }
            ////    else
            ////    {
            ////        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            ////    }
            ////}
            ////else
            ////{
            ////    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            ////}
            //#endregion

            #region EXP
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt4.Columns.Add(new DataColumn("EXPORT TYPE"));
            dt4.Columns.Add(new DataColumn("INVOICE NUMBER"));
            dt4.Columns.Add(new DataColumn("INVOICE DATE"));
            dt4.Columns.Add(new DataColumn("INVOICE VALUE"));
            dt4.Columns.Add(new DataColumn("PORT CODE"));
            dt4.Columns.Add(new DataColumn("SHIPPING BILL NUMBER"));
            dt4.Columns.Add(new DataColumn("SHIPPING BILL DATE"));
            dt4.Columns.Add(new DataColumn("RATE"));
            dt4.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt4.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt4.TableName = "exp";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export4 = dt4.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export4["EXPORT TYPE"] = "";
            dr_export4["INVOICE NUMBER"] = "";
            dr_export4["INVOICE DATE"] = "";
            dr_export4["INVOICE VALUE"] = "";
            dr_export4["PORT CODE"] = "";
            dr_export4["SHIPPING BILL NUMBER"] = "";
            dr_export4["SHIPPING BILL DATE"] = "";
            dr_export4["RATE"] = "";
            dr_export4["TAXABLE VALUE"] = "";
            dr_export4["CESS AMOUNT"] = "";
            dt4.Rows.Add(dr_export4);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            #region AT
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt5.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            //dt5.Columns.Add(new DataColumn("SUPPLY TYPE"));
            dt5.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt5.Columns.Add(new DataColumn("RATE"));
            dt5.Columns.Add(new DataColumn("GROSS ADVANCE RECEIVED"));
            //dt5.Columns.Add(new DataColumn("INTEGRATED TAX"));
            //dt5.Columns.Add(new DataColumn("CENTRAL TAX"));
            //dt5.Columns.Add(new DataColumn("STATE OR UT TAX"));
            dt5.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt5.TableName = "at";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export5 = dt5.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export5["PLACE OF SUPPLY"] = "";
            //dr_export5["SUPPLY TYPE"] = "";
            dr_export5["APPLICABLE % OF TAX RATE"] = "";
            dr_export5["RATE"] = "";
            dr_export5["GROSS ADVANCE RECEIVED"] = "";
            //dr_export5["INTEGRATED TAX"] = "";
            //dr_export5["CENTRAL TAX"] = "";
            //dr_export5["STATE OR UT TAX"] = "";
            dr_export5["CESS AMOUNT"] = "";
            dt5.Rows.Add(dr_export5);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            #region ATADJ
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt6.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            //dt6.Columns.Add(new DataColumn("SUPPLY TYPE"));
            dt6.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt6.Columns.Add(new DataColumn("RATE"));
            dt6.Columns.Add(new DataColumn("GROSS ADVANCE ADJUSTED"));
            //dt6.Columns.Add(new DataColumn("INTEGRATED TAX"));
            //dt6.Columns.Add(new DataColumn("CENTRAL TAX"));
            //dt6.Columns.Add(new DataColumn("STATE OR UT TAX"));
            dt6.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt6.TableName = "atadj";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export6 = dt6.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export6["PLACE OF SUPPLY"] = "";
            //dr_export6["SUPPLY TYPE"] = "";
            dr_export6["APPLICABLE % OF TAX RATE"] = "";
            dr_export6["RATE"] = "";
            dr_export6["GROSS ADVANCE ADJUSTED"] = "";
            //dr_export6["INTEGRATED TAX"] = "";
            //dr_export6["CENTRAL TAX"] = "";
            //dr_export6["STATE OR UT TAX"] = "";
            dr_export6["CESS AMOUNT"] = "";
            dt6.Rows.Add(dr_export6);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            #region CDNR
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt7.Columns.Add(new DataColumn("GSTIN/UIN of RECIPIENT"));
            dt7.Columns.Add(new DataColumn("RECEIVER NAME"));
            dt7.Columns.Add(new DataColumn("NOTE NUMBER"));
            dt7.Columns.Add(new DataColumn("NOTE DATE"));
            dt7.Columns.Add(new DataColumn("NOTE TYPE"));
            dt7.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            dt7.Columns.Add(new DataColumn("REVERSE CHARGE"));
            dt7.Columns.Add(new DataColumn("NOTE SUPPLY TYPE"));
            dt7.Columns.Add(new DataColumn("NOTE VALUE"));
            dt7.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt7.Columns.Add(new DataColumn("RATE"));
            dt7.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt7.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt7.TableName = "cdnr";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export7 = dt7.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export7["GSTIN/UIN of RECIPIENT"] = "";
            dr_export7["RECEIVER NAME"] = "";
            dr_export7["NOTE NUMBER"] = "";
            dr_export7["NOTE DATE"] = "";
            dr_export7["NOTE TYPE"] = "";
            dr_export7["PLACE OF SUPPLY"] = "";
            dr_export7["REVERSE CHARGE"] = "";
            dr_export7["NOTE SUPPLY TYPE"] = "";
            dr_export7["NOTE VALUE"] = "";
            dr_export7["APPLICABLE % OF TAX RATE"] = "";
            dr_export7["RATE"] = "";
            dr_export7["TAXABLE VALUE"] = "";
            dr_export7["CESS AMOUNT"] = "";
            dt7.Rows.Add(dr_export7);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            #region CDNUR
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt8.Columns.Add(new DataColumn("UR TYPE"));
            dt8.Columns.Add(new DataColumn("NOTE NUMBER"));
            dt8.Columns.Add(new DataColumn("NOTE DATE"));
            dt8.Columns.Add(new DataColumn("NOTE TYPE"));
            dt8.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            dt8.Columns.Add(new DataColumn("NOTE VALUE"));
            dt8.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt8.Columns.Add(new DataColumn("RATE"));
            dt8.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt8.Columns.Add(new DataColumn("CESS AMOUNT"));

            dt8.TableName = "cdnur";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export8 = dt8.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export8["UR TYPE"] = "";
            dr_export8["NOTE NUMBER"] = "";
            dr_export8["NOTE DATE"] = "";
            dr_export8["NOTE TYPE"] = "";
            dr_export8["PLACE OF SUPPLY"] = "";
            dr_export8["NOTE VALUE"] = "";
            dr_export8["APPLICABLE % OF TAX RATE"] = "";
            dr_export8["RATE"] = "";
            dr_export8["TAXABLE VALUE"] = "";
            dr_export8["CESS AMOUNT"] = "";
            dt8.Rows.Add(dr_export8);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            #region EXEMP
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt11.Columns.Add(new DataColumn("DESCRIPTION"));
            dt11.Columns.Add(new DataColumn("NIL RATED SUPPLIES"));
            dt11.Columns.Add(new DataColumn("Exempted (other than nil rated/non GST supply )"));
            dt11.Columns.Add(new DataColumn("Non-GST supplies"));
            dt11.TableName = "exemp";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export11 = dt11.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export11["DESCRIPTION"] = "";
            dr_export11["NIL RATED SUPPLIES"] = "";
            dr_export11["Exempted (other than nil rated/non GST supply )"] = "";
            dr_export11["Non-GST supplies"] = "";
            dt11.Rows.Add(dr_export11);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            #region HSN
            DataSet ds13 = objBs.gstreportHSN(frmdate, todate, ddloutlet.SelectedValue);
            if (ds13 != null)
            {
                if (ds13.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt13 = new DataTable();
                    dt9New.Columns.Add(new DataColumn("HSN"));
                    dt9New.Columns.Add(new DataColumn("DESCRIPTION"));
                    dt9New.Columns.Add(new DataColumn("UQC"));
                    dt9New.Columns.Add(new DataColumn("TOTAL QUANTITY"));
                    dt9New.Columns.Add(new DataColumn("TOTAL VALUE"));
                    dt9New.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    dt9New.Columns.Add(new DataColumn("INTEGRATED TAX AMOUNT"));
                    dt9New.Columns.Add(new DataColumn("CENTRAL TAX AMOUNT"));
                    dt9New.Columns.Add(new DataColumn("STATE/UT TAX AMOUNT"));
                    dt9New.Columns.Add(new DataColumn("CESS AMOUNT"));
                    dt9New.TableName = "hsn";
                    foreach (DataRow dr13 in ds13.Tables[0].Rows)
                    {
                        DataRow dr_export9 = dt9New.NewRow();
                        dr_export9["HSN"] = dr13["HSN"];
                        dr_export9["DESCRIPTION"] = dr13["Description"];
                        dr_export9["UQC"] = dr13["UQC"];
                        dr_export9["TOTAL QUANTITY"] = dr13["TotalQuantity"];
                        dr_export9["TOTAL VALUE"] = Convert.ToDouble(dr13["TotalAmount"]).ToString("F2");
                        dr_export9["TAXABLE VALUE"] = Convert.ToDouble(dr13["TaxableAmount"]).ToString("F2");
                        if (dr13["igst"].ToString() == "")
                        {
                            dr_export9["INTEGRATED TAX AMOUNT"] = "0";
                        }
                        else
                        {
                            dr_export9["INTEGRATED TAX AMOUNT"] = Convert.ToDouble(dr13["igst"]).ToString("F2");
                        }

                        if (dr13["cgst"].ToString() == "")
                        {
                            dr_export9["CENTRAL TAX AMOUNT"] = "0";
                        }
                        else
                        {
                            dr_export9["CENTRAL TAX AMOUNT"] = Convert.ToDouble(dr13["cgst"]).ToString("F2");
                        }

                        if (dr13["sgst"].ToString() == "")
                        {
                            dr_export9["STATE/UT TAX AMOUNT"] = "0";
                        }
                        else
                        {
                            dr_export9["STATE/UT TAX AMOUNT"] = Convert.ToDouble(dr13["sgst"]).ToString("F2");
                        }
                        dr_export9["CESS AMOUNT"] = "";
                        dt9New.Rows.Add(dr_export9);
                    }

                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            }


            DataSet dsch = new DataSet();
            dsch.Tables.Add(dt9New);

            dt9.Columns.Add(new DataColumn("HSN"));
            dt9.Columns.Add(new DataColumn("DESCRIPTION"));
            dt9.Columns.Add(new DataColumn("UQC"));
            dt9.Columns.Add(new DataColumn("TOTAL QUANTITY"));
            dt9.Columns.Add(new DataColumn("TOTAL VALUE"));
            dt9.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt9.Columns.Add(new DataColumn("INTEGRATED TAX AMOUNT"));
            dt9.Columns.Add(new DataColumn("CENTRAL TAX AMOUNT"));
            dt9.Columns.Add(new DataColumn("STATE/UT TAX AMOUNT"));
            dt9.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt9.TableName = "hsn";


            var resulth = from r in dsch.Tables[0].AsEnumerable()
                          group r by new { Description = r["Description"], UQC = r["UQC"], hsn = r["HSN"] } into ggm
                          select new
                          {
                              hsn = ggm.Key.hsn,
                              Description = ggm.Key.Description,
                              UQC = ggm.Key.UQC,
                              cgst = ggm.Sum(x => Convert.ToDouble(x["CENTRAL TAX AMOUNT"])),
                              sgst = ggm.Sum(x => Convert.ToDouble(x["STATE/UT TAX AMOUNT"])),
                              igst = ggm.Sum(x => Convert.ToDouble(x["INTEGRATED TAX AMOUNT"])),
                              TotalQuantity = ggm.Sum(x => Convert.ToDouble(x["TOTAL QUANTITY"])),
                              TotalAmount = ggm.Sum(x => Convert.ToDouble(x["TOTAL VALUE"])),
                              TaxableAmount = ggm.Sum(x => Convert.ToDouble(x["TAXABLE VALUE"])),
                          };

            int gggf = 0;
            foreach (var ggm in resulth)
            {
                DataRow dr_export9 = dt9.NewRow();

                dr_export9["HSN"] = ggm.hsn;
                dr_export9["DESCRIPTION"] = ggm.Description;
                dr_export9["UQC"] = ggm.UQC;
                dr_export9["TOTAL QUANTITY"] = ggm.TotalQuantity;
                dr_export9["TOTAL VALUE"] = Convert.ToDouble(ggm.TotalAmount).ToString("F2");
                dr_export9["TAXABLE VALUE"] = Convert.ToDouble(ggm.TaxableAmount).ToString("F2");
                dr_export9["INTEGRATED TAX AMOUNT"] = Convert.ToDouble(ggm.igst).ToString("F2");
                dr_export9["CENTRAL TAX AMOUNT"] = Convert.ToDouble(ggm.cgst).ToString("F2");
                dr_export9["STATE/UT TAX AMOUNT"] = Convert.ToDouble(ggm.sgst).ToString("F2");
                dr_export9["CESS AMOUNT"] = "";
                gggf++;
                dt9.Rows.Add(dr_export9);
            }
            #endregion

            #region DOCS
            DataSet ds14 = objBs.gstreportDOCS(frmdate, todate, ddloutlet.SelectedValue);
            if (ds14 != null)
            {
                if (ds14.Tables[0].Rows.Count > 0)
                {
                    #region

                    //DataTable dt1 = new DataTable();
                    dt10.Columns.Add(new DataColumn("NATURE OF DOCUMENT"));
                    dt10.Columns.Add(new DataColumn("Sr. No. From"));
                    dt10.Columns.Add(new DataColumn("Sr. No. To"));
                    dt10.Columns.Add(new DataColumn("TOTAL NUMBER"));
                    dt10.Columns.Add(new DataColumn("CANCELLED"));
                    dt10.TableName = "docs";
                    //foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    //{
                    DataRow dr_export10 = dt10.NewRow();
                    dr_export10["NATURE OF DOCUMENT"] = "Invoices for outward supply";
                    dr_export10["Sr. No. From"] = ds14.Tables[0].Rows[0]["fullbillno"].ToString();
                    dr_export10["Sr. No. To"] = ds14.Tables[0].Rows[ds14.Tables[0].Rows.Count - 1]["fullbillno"].ToString();
                    dr_export10["TOTAL NUMBER"] = ds14.Tables[0].Rows.Count;
                    dr_export10["CANCELLED"] = "0";
                    dt10.Rows.Add(dr_export10);
                    //}

                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            }
            #endregion

            dsNEW.Tables.Add(dtB2B);//B2B
            dsNEW.Tables.Add(dt1); //B2CL
            dsNEW.Tables.Add(dt2); //B2CS
            // dsNEW.Tables.Add(dt3); //NIL
            dsNEW.Tables.Add(dt7); //CDNR
            dsNEW.Tables.Add(dt8); //CDNUR
            dsNEW.Tables.Add(dt4); //EXP
            dsNEW.Tables.Add(dt5); //AT
            dsNEW.Tables.Add(dt6); //ATADJ
            dsNEW.Tables.Add(dt11); //EXEMP
            dsNEW.Tables.Add(dt9); //HSN
            dsNEW.Tables.Add(dt10); //DOCS
            return dsNEW;
        }


        protected void btnreport_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "GST REPORT  From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            DataSet ds = getDataSetExportToExcel();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                //wb.Tables.FirstOrDefault().ShowAutoFilter = false;   
                // wb.ShowRowColHeaders = false;

                //--- Get ShowRowColHeaders
                bool showHeaders = wb.ShowRowColHeaders;

                //--- Set ShowRowColHeaders
                wb.ShowRowColHeaders = false;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GSTR1.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "GST REPORT  From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            DataSet ds = getDataSetExportToExcel();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                //wb.Tables.FirstOrDefault().ShowAutoFilter = false;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GSTR1.csv");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }



        protected void btnJSON_Click(object sender, EventArgs e)
        {
            DateTime frmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();
            DataTable dt9 = new DataTable();
            DataTable dt10 = new DataTable();
            DataTable dtExcelRecords = new DataTable();


            #region B2B
            DataSet ds = objBs.gstreportB2B(frmdate, todate, ddloutlet.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region
                    dt.Columns.Add(new DataColumn("GSTIN/UIN of Recipient"));
                    dt.Columns.Add(new DataColumn("Receiver Name"));
                    dt.Columns.Add(new DataColumn("INVOICE NUMBER"));
                    dt.Columns.Add(new DataColumn("INVOICE DATE"));
                    dt.Columns.Add(new DataColumn("INVOICE VALUE"));
                    dt.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    dt.Columns.Add(new DataColumn("REVERSE CHARGE"));
                    dt.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                    dt.Columns.Add(new DataColumn("INVOICE TYPE"));
                    dt.Columns.Add(new DataColumn("E-Commerce GSTIN"));
                    //dt.Columns.Add(new DataColumn("SERIAL NUMBER"));
                    dt.Columns.Add(new DataColumn("RATE"));
                    dt.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    //dt.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    //dt.Columns.Add(new DataColumn("CENTRAL TAX"));
                    //dt.Columns.Add(new DataColumn("STATE OR UT TAX"));
                    dt.Columns.Add(new DataColumn("CESS AMOUNT"));
                    dt.TableName = "B2B";
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["GSTIN/UIN of Recipient"] = dr["GSTINNo"];
                        dr_export["Receiver Name"] = "";
                        dr_export["INVOICE NUMBER"] = dr["FullBillNo"];
                        dr_export["INVOICE DATE"] = Convert.ToDateTime(dr["BillDate"]).ToString("dd-MM-yyyy");
                        dr_export["INVOICE VALUE"] = Convert.ToDouble(dr["RoundOff"]).ToString("F2");
                        dr_export["PLACE OF SUPPLY"] = dr["PlaceOfSupply"];
                        dr_export["REVERSE CHARGE"] = dr["ReverseCharge"];
                        dr_export["APPLICABLE % OF TAX RATE"] = dr["ApplicablePer"];
                        dr_export["INVOICE TYPE"] = dr["InvoiceType"];
                        dr_export["E-Commerce GSTIN"] = dr["GSTINNo"];
                        //dr_export["SERIAL NUMBER"] = dr["SerialNo"];
                        dr_export["RATE"] = dr["Rate"];
                        dr_export["TAXABLE VALUE"] = Convert.ToDouble(dr["TaxableValue"]).ToString("F2");
                        if (dr["Province"].ToString() == "1")
                        {
                            //dr_export["INTEGRATED TAX"] = Convert.ToDouble("0").ToString("N2");
                            //dr_export["CENTRAL TAX"] = Convert.ToDouble(Convert.ToDouble(dr["TaxableValue"]) / 2).ToString("F2");
                            //dr_export["STATE OR UT TAX"] = Convert.ToDouble(Convert.ToDouble(dr["TaxableValue"]) / 2).ToString("F2");
                        }
                        else
                        {
                            //dr_export["INTEGRATED TAX"] = Convert.ToDouble(dr["TaxableValue"]).ToString("F2");
                            //dr_export["CENTRAL TAX"] = Convert.ToDouble("0").ToString("F2");
                            //dr_export["STATE OR UT TAX"] = Convert.ToDouble("0").ToString("F2");
                        }

                        dr_export["CESS AMOUNT"] = Convert.ToDouble(dr["CESS"]).ToString("F2");
                        dt.Rows.Add(dr_export);
                    }

                    #endregion
                }
                else
                {
                    #region
                    dt.Columns.Add(new DataColumn("GSTIN/UIN of Recipient"));
                    dt.Columns.Add(new DataColumn("Receiver Name"));
                    dt.Columns.Add(new DataColumn("INVOICE NUMBER"));
                    dt.Columns.Add(new DataColumn("INVOICE DATE"));
                    dt.Columns.Add(new DataColumn("INVOICE VALUE"));
                    dt.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    dt.Columns.Add(new DataColumn("REVERSE CHARGE"));
                    dt.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                    dt.Columns.Add(new DataColumn("INVOICE TYPE"));
                    dt.Columns.Add(new DataColumn("E-Commerce GSTIN"));
                    //dt.Columns.Add(new DataColumn("SERIAL NUMBER"));
                    dt.Columns.Add(new DataColumn("RATE"));
                    dt.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    //dt.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    //dt.Columns.Add(new DataColumn("CENTRAL TAX"));
                    //dt.Columns.Add(new DataColumn("STATE OR UT TAX"));
                    dt.Columns.Add(new DataColumn("CESS AMOUNT"));
                    dt.TableName = "B2B";
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["GSTIN/UIN of Recipient"] = "";
                        dr_export["Receiver Name"] = "";
                        dr_export["INVOICE NUMBER"] = "";
                        dr_export["INVOICE DATE"] = "";
                        dr_export["INVOICE VALUE"] = "";
                        dr_export["PLACE OF SUPPLY"] = "";
                        dr_export["REVERSE CHARGE"] = "";
                        dr_export["APPLICABLE % OF TAX RATE"] = "";
                        dr_export["INVOICE TYPE"] = "";
                        dr_export["E-Commerce GSTIN"] = "";
                        //dr_export["SERIAL NUMBER"] = "";
                        dr_export["RATE"] = "";
                        dr_export["TAXABLE VALUE"] = "";
                        //dr_export["INTEGRATED TAX"] = "";
                        //dr_export["CENTRAL TAX"] = "";
                        //dr_export["STATE OR UT TAX"] = "";
                        dr_export["CESS AMOUNT"] = "";
                        dt.Rows.Add(dr_export);
                    }

                    #endregion
                }
            }
            else
            {
                #region
                dt.Columns.Add(new DataColumn("GSTIN/UIN of Recipient"));
                dt.Columns.Add(new DataColumn("Receiver Name"));
                dt.Columns.Add(new DataColumn("INVOICE NUMBER"));
                dt.Columns.Add(new DataColumn("INVOICE DATE"));
                dt.Columns.Add(new DataColumn("INVOICE VALUE"));
                dt.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                dt.Columns.Add(new DataColumn("REVERSE CHARGE"));
                dt.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                dt.Columns.Add(new DataColumn("INVOICE TYPE"));
                dt.Columns.Add(new DataColumn("E-Commerce GSTIN"));
                //dt.Columns.Add(new DataColumn("SERIAL NUMBER"));
                dt.Columns.Add(new DataColumn("RATE"));
                dt.Columns.Add(new DataColumn("TAXABLE VALUE"));
                //dt.Columns.Add(new DataColumn("INTEGRATED TAX"));
                //dt.Columns.Add(new DataColumn("CENTRAL TAX"));
                //dt.Columns.Add(new DataColumn("STATE OR UT TAX"));
                dt.Columns.Add(new DataColumn("CESS AMOUNT"));
                dt.TableName = "B2B";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["GSTIN/UIN of Recipient"] = "";
                    dr_export["Receiver Name"] = "";
                    dr_export["INVOICE NUMBER"] = "";
                    dr_export["INVOICE DATE"] = "";
                    dr_export["INVOICE VALUE"] = "";
                    dr_export["PLACE OF SUPPLY"] = "";
                    dr_export["REVERSE CHARGE"] = "";
                    dr_export["APPLICABLE % OF TAX RATE"] = "";
                    dr_export["INVOICE TYPE"] = "";
                    dr_export["E-Commerce GSTIN"] = "";
                    //dr_export["SERIAL NUMBER"] = "";
                    dr_export["RATE"] = "";
                    dr_export["TAXABLE VALUE"] = "";
                    //dr_export["INTEGRATED TAX"] = "";
                    //dr_export["CENTRAL TAX"] = "";
                    //dr_export["STATE OR UT TAX"] = "";
                    dr_export["CESS AMOUNT"] = "";
                    dt.Rows.Add(dr_export);
                }

                #endregion
            }
            #endregion

            dtExcelRecords.Merge(dt);
            dtExcelRecords.AcceptChanges();

            #region B2CL
            DataSet ds1 = objBs.gstreportB2CL(frmdate, todate, ddloutlet.SelectedValue);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    #region

                    dt1.Columns.Add(new DataColumn("INVOICE NUMBER"));
                    dt1.Columns.Add(new DataColumn("INVOICE DATE"));
                    dt1.Columns.Add(new DataColumn("INVOICE VALUE"));
                    dt1.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    dt1.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                    //dt1.Columns.Add(new DataColumn("SERIAL NUMBER"));
                    dt1.Columns.Add(new DataColumn("RATE"));
                    dt1.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    //dt1.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    dt1.Columns.Add(new DataColumn("CESS AMOUNT"));
                    dt1.Columns.Add(new DataColumn("E-Commerce GSTIN"));

                    dt1.TableName = "B2CL";
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        DataRow dr_export1 = dt1.NewRow();
                        dr_export1["INVOICE NUMBER"] = dr1["FullBillNo"];
                        dr_export1["INVOICE DATE"] = Convert.ToDateTime(dr1["BillDate"]).ToString("dd-MM-yyyy");
                        dr_export1["INVOICE VALUE"] = Convert.ToDouble(dr1["RoundOff"]).ToString("F2");
                        dr_export1["PLACE OF SUPPLY"] = dr1["PlaceOfSupply"];
                        dr_export1["APPLICABLE % OF TAX RATE"] = dr1["ApplicablePer"];
                        //dr_export1["SERIAL NUMBER"] = dr1["SerialNo"];
                        dr_export1["RATE"] = dr1["Rate"];
                        dr_export1["TAXABLE VALUE"] = Convert.ToDouble(dr1["TaxableValue"]).ToString("F2");
                        //dr_export1["INTEGRATED TAX"] = Convert.ToDouble(dr1["TaxableValue"]).ToString("F2");
                        dr_export1["CESS AMOUNT"] = Convert.ToDouble(dr1["CESS"]).ToString("F2");
                        dr_export1["E-Commerce GSTIN"] = dr1["GSTINNo"];
                        dt1.Rows.Add(dr_export1);
                    }

                    #endregion
                }
                else
                {
                    #region

                    dt1.Columns.Add(new DataColumn("INVOICE NUMBER"));
                    dt1.Columns.Add(new DataColumn("INVOICE DATE"));
                    dt1.Columns.Add(new DataColumn("INVOICE VALUE"));
                    dt1.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                    dt1.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                    //dt1.Columns.Add(new DataColumn("SERIAL NUMBER"));
                    dt1.Columns.Add(new DataColumn("RATE"));
                    dt1.Columns.Add(new DataColumn("TAXABLE VALUE"));
                    //dt1.Columns.Add(new DataColumn("INTEGRATED TAX"));
                    dt1.Columns.Add(new DataColumn("CESS AMOUNT"));
                    dt1.Columns.Add(new DataColumn("E-Commerce GSTIN"));

                    dt1.TableName = "B2CL";
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        DataRow dr_export1 = dt1.NewRow();
                        dr_export1["INVOICE NUMBER"] = "";
                        dr_export1["INVOICE DATE"] = "";
                        dr_export1["INVOICE VALUE"] = "";
                        dr_export1["PLACE OF SUPPLY"] = "";
                        dr_export1["APPLICABLE % OF TAX RATE"] = "";
                        //dr_export1["SERIAL NUMBER"] = "";
                        dr_export1["RATE"] = "";
                        dr_export1["TAXABLE VALUE"] = "";
                        //dr_export1["INTEGRATED TAX"] = "";
                        dr_export1["CESS AMOUNT"] = "";
                        dr_export1["E-Commerce GSTIN"] = "";
                        dt1.Rows.Add(dr_export1);
                    }

                    #endregion
                }
            }
            else
            {
                #region

                dt1.Columns.Add(new DataColumn("INVOICE NUMBER"));
                dt1.Columns.Add(new DataColumn("INVOICE DATE"));
                dt1.Columns.Add(new DataColumn("INVOICE VALUE"));
                dt1.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
                dt1.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
                //dt1.Columns.Add(new DataColumn("SERIAL NUMBER"));
                dt1.Columns.Add(new DataColumn("RATE"));
                dt1.Columns.Add(new DataColumn("TAXABLE VALUE"));
                //dt1.Columns.Add(new DataColumn("INTEGRATED TAX"));
                dt1.Columns.Add(new DataColumn("CESS AMOUNT"));
                dt1.Columns.Add(new DataColumn("E-Commerce GSTIN"));

                dt1.TableName = "B2CL";
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    DataRow dr_export1 = dt1.NewRow();
                    dr_export1["INVOICE NUMBER"] = "";
                    dr_export1["INVOICE DATE"] = "";
                    dr_export1["INVOICE VALUE"] = "";
                    dr_export1["PLACE OF SUPPLY"] = "";
                    dr_export1["APPLICABLE % OF TAX RATE"] = "";
                    //dr_export1["SERIAL NUMBER"] = "";
                    dr_export1["RATE"] = "";
                    dr_export1["TAXABLE VALUE"] = "";
                    //dr_export1["INTEGRATED TAX"] = "";
                    dr_export1["CESS AMOUNT"] = "";
                    dr_export1["E-Commerce GSTIN"] = "";
                    dt1.Rows.Add(dr_export1);
                }

                #endregion
            }
            #endregion

            dtExcelRecords.Merge(dt1);
            dtExcelRecords.AcceptChanges();

            #region B2CS
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt2.Columns.Add(new DataColumn("TYPE"));
            dt2.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            //dt2.Columns.Add(new DataColumn("SUPPLY TYPE"));
            dt2.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt2.Columns.Add(new DataColumn("RATE"));
            dt2.Columns.Add(new DataColumn("TAXABLE VALUE"));
            //dt2.Columns.Add(new DataColumn("INTEGRATED TAX"));
            //dt2.Columns.Add(new DataColumn("CENTRAL TAX"));
            //dt2.Columns.Add(new DataColumn("STATE OR UT TAX"));
            dt2.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt2.Columns.Add(new DataColumn("E-Commerce GSTIN"));

            dt2.TableName = "B2CS";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export2 = dt2.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export2["TYPE"] = "";
            dr_export2["PLACE OF SUPPLY"] = "";
            //dr_export2["SUPPLY TYPE"] = "";
            dr_export2["APPLICABLE % OF TAX RATE"] = "";
            dr_export2["RATE"] = "";
            dr_export2["TAXABLE VALUE"] = "";
            //dr_export2["INTEGRATED TAX"] = "";
            //dr_export2["CENTRAL TAX"] = "";
            //dr_export2["STATE OR UT TAX"] = "";
            dr_export2["CESS AMOUNT"] = "";
            dr_export2["E-Commerce GSTIN"] = "";
            dt2.Rows.Add(dr_export2);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            dtExcelRecords.Merge(dt2);
            dtExcelRecords.AcceptChanges();

            //#region NIL
            ////DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            ////if (ds1 != null)
            ////{
            ////    if (ds1.Tables[0].Rows.Count > 0)
            ////    {
            //#region

            ////DataTable dt1 = new DataTable();
            //dt3.Columns.Add(new DataColumn("NATURE OF SUPPLY"));
            //dt3.Columns.Add(new DataColumn("NIL RATED SUPPLIES"));
            //dt3.Columns.Add(new DataColumn("EXEMPTED"));
            //dt3.Columns.Add(new DataColumn("NON-GST SUPPLIES"));
            //dt3.TableName = "NIL";
            ////foreach (DataRow dr1 in ds1.Tables[0].Rows)
            ////{
            //DataRow dr_export3 = dt3.NewRow();
            ////dr_export1["JobType_Name"] = dr1["JobType_Name"];
            ////dr_export1["IsActive"] = dr1["IsActive"];
            //dr_export3["NATURE OF SUPPLY"] = "";
            //dr_export3["NIL RATED SUPPLIES"] = "";
            //dr_export3["EXEMPTED"] = "";
            //dr_export3["NON-GST SUPPLIES"] = "";
            //dt3.Rows.Add(dr_export3);
            ////}

            //#endregion
            ////    }
            ////    else
            ////    {
            ////        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            ////    }
            ////}
            ////else
            ////{
            ////    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            ////}
            //#endregion

            //dtExcelRecords.Merge(dt3);
            //dtExcelRecords.AcceptChanges();

            #region EXP
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt4.Columns.Add(new DataColumn("EXPORT TYPE"));
            dt4.Columns.Add(new DataColumn("INVOICE NUMBER"));
            dt4.Columns.Add(new DataColumn("INVOICE DATE"));
            dt4.Columns.Add(new DataColumn("INVOICE VALUE"));
            dt4.Columns.Add(new DataColumn("SHIPPING BILL NO. OR BILL OF EXPORT NO."));
            dt4.Columns.Add(new DataColumn("SHIPPING BILL DATE OR BILL OF EXPORT DATE"));
            dt4.Columns.Add(new DataColumn("SHIPPING BILL PORT CODE"));
            dt4.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt4.Columns.Add(new DataColumn("RATE"));
            dt4.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt4.Columns.Add(new DataColumn("INTEGRATED TAX"));
            dt4.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt4.TableName = "EXP";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export4 = dt4.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export4["EXPORT TYPE"] = "";
            dr_export4["INVOICE NUMBER"] = "";
            dr_export4["INVOICE DATE"] = "";
            dr_export4["INVOICE VALUE"] = "";
            dr_export4["SHIPPING BILL NO. OR BILL OF EXPORT NO."] = "";
            dr_export4["SHIPPING BILL DATE OR BILL OF EXPORT DATE"] = "";
            dr_export4["SHIPPING BILL PORT CODE"] = "";
            dr_export4["APPLICABLE % OF TAX RATE"] = "";
            dr_export4["RATE"] = "";
            dr_export4["TAXABLE VALUE"] = "";
            dr_export4["INTEGRATED TAX"] = "";
            dr_export4["CESS AMOUNT"] = "";
            dt4.Rows.Add(dr_export4);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            dtExcelRecords.Merge(dt4);
            dtExcelRecords.AcceptChanges();

            #region AT
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt5.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            dt5.Columns.Add(new DataColumn("SUPPLY TYPE"));
            dt5.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt5.Columns.Add(new DataColumn("RATE"));
            dt5.Columns.Add(new DataColumn("GROSS ADVANCE RECEIVED"));
            dt5.Columns.Add(new DataColumn("INTEGRATED TAX"));
            dt5.Columns.Add(new DataColumn("CENTRAL TAX"));
            dt5.Columns.Add(new DataColumn("STATE OR UT TAX"));
            dt5.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt5.TableName = "AT";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export5 = dt5.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export5["PLACE OF SUPPLY"] = "";
            dr_export5["SUPPLY TYPE"] = "";
            dr_export5["APPLICABLE % OF TAX RATE"] = "";
            dr_export5["RATE"] = "";
            dr_export5["GROSS ADVANCE RECEIVED"] = "";
            dr_export5["INTEGRATED TAX"] = "";
            dr_export5["CENTRAL TAX"] = "";
            dr_export5["STATE OR UT TAX"] = "";
            dr_export5["CESS AMOUNT"] = "";
            dt5.Rows.Add(dr_export5);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            dtExcelRecords.Merge(dt5);
            dtExcelRecords.AcceptChanges();

            #region TXP
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt6.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            dt6.Columns.Add(new DataColumn("SUPPLY TYPE"));
            dt6.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt6.Columns.Add(new DataColumn("RATE"));
            dt6.Columns.Add(new DataColumn("GROSS ADVANCE ADJUSTED"));
            dt6.Columns.Add(new DataColumn("INTEGRATED TAX"));
            dt6.Columns.Add(new DataColumn("CENTRAL TAX"));
            dt6.Columns.Add(new DataColumn("STATE OR UT TAX"));
            dt6.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt6.TableName = "TXP";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export6 = dt6.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export6["PLACE OF SUPPLY"] = "";
            dr_export6["SUPPLY TYPE"] = "";
            dr_export6["APPLICABLE % OF TAX RATE"] = "";
            dr_export6["RATE"] = "";
            dr_export6["GROSS ADVANCE ADJUSTED"] = "";
            dr_export6["INTEGRATED TAX"] = "";
            dr_export6["CENTRAL TAX"] = "";
            dr_export6["STATE OR UT TAX"] = "";
            dr_export6["CESS AMOUNT"] = "";
            dt6.Rows.Add(dr_export6);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            dtExcelRecords.Merge(dt6);
            dtExcelRecords.AcceptChanges();

            #region CDNR
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt7.Columns.Add(new DataColumn("GSTIN/UIN of RECIPIENT"));
            dt7.Columns.Add(new DataColumn("RECEIVER NAME"));
            dt7.Columns.Add(new DataColumn("NOTE NUMBER"));
            dt7.Columns.Add(new DataColumn("NOTE DATE"));
            dt7.Columns.Add(new DataColumn("NOTE TYPE"));
            dt7.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            dt7.Columns.Add(new DataColumn("REVERSE CHARGE"));
            dt7.Columns.Add(new DataColumn("NOTE SUPPLY TYPE"));
            dt7.Columns.Add(new DataColumn("NOTE VALUE"));
            dt7.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt7.Columns.Add(new DataColumn("RATE"));
            dt7.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt7.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt7.TableName = "CDNR";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export7 = dt7.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export7["GSTIN/UIN of RECIPIENT"] = "";
            dr_export7["RECEIVER NAME"] = "";
            dr_export7["NOTE NUMBER"] = "";
            dr_export7["NOTE DATE"] = "";
            dr_export7["NOTE TYPE"] = "";
            dr_export7["PLACE OF SUPPLY"] = "";
            dr_export7["REVERSE CHARGE"] = "";
            dr_export7["NOTE SUPPLY TYPE"] = "";
            dr_export7["NOTE VALUE"] = "";
            dr_export7["APPLICABLE % OF TAX RATE"] = "";
            dr_export7["RATE"] = "";
            dr_export7["TAXABLE VALUE"] = "";
            dr_export7["CESS AMOUNT"] = "";
            dt7.Rows.Add(dr_export7);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            dtExcelRecords.Merge(dt7);
            dtExcelRecords.AcceptChanges();

            #region CDNUR
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt8.Columns.Add(new DataColumn("UR TYPE"));
            dt8.Columns.Add(new DataColumn("NOTE NUMBER"));
            dt8.Columns.Add(new DataColumn("NOTE DATE"));
            dt8.Columns.Add(new DataColumn("NOTE TYPE"));
            dt8.Columns.Add(new DataColumn("PLACE OF SUPPLY"));
            dt8.Columns.Add(new DataColumn("NOTE VALUE"));
            dt8.Columns.Add(new DataColumn("APPLICABLE % OF TAX RATE"));
            dt8.Columns.Add(new DataColumn("RATE"));
            dt8.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt8.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt8.TableName = "CDNUR";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export8 = dt8.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export8["UR TYPE"] = "";
            dr_export8["NOTE NUMBER"] = "";
            dr_export8["NOTE DATE"] = "";
            dr_export8["NOTE TYPE"] = "";
            dr_export8["PLACE OF SUPPLY"] = "";
            dr_export8["NOTE VALUE"] = "";
            dr_export8["APPLICABLE % OF TAX RATE"] = "";
            dr_export8["RATE"] = "";
            dr_export8["TAXABLE VALUE"] = "";
            dr_export8["CESS AMOUNT"] = "";
            dt8.Rows.Add(dr_export8);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            dtExcelRecords.Merge(dt8);
            dtExcelRecords.AcceptChanges();

            #region HSN
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt9.Columns.Add(new DataColumn("SERIAL NUMBER"));
            dt9.Columns.Add(new DataColumn("HSN OR SAC CODE"));
            dt9.Columns.Add(new DataColumn("DESCRIPTION"));
            dt9.Columns.Add(new DataColumn("UQC"));
            dt9.Columns.Add(new DataColumn("TOTAL QUANTITY"));
            dt9.Columns.Add(new DataColumn("TOTAL VALUE"));
            dt9.Columns.Add(new DataColumn("TAXABLE VALUE"));
            dt9.Columns.Add(new DataColumn("INTEGRATED TAX"));
            dt9.Columns.Add(new DataColumn("CENTRAL TAX"));
            dt9.Columns.Add(new DataColumn("STATE OR UT TAX"));
            dt9.Columns.Add(new DataColumn("CESS AMOUNT"));
            dt9.TableName = "HSN";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export9 = dt9.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export9["SERIAL NUMBER"] = "";
            dr_export9["HSN OR SAC CODE"] = "";
            dr_export9["DESCRIPTION"] = "";
            dr_export9["UQC"] = "";
            dr_export9["TOTAL QUANTITY"] = "";
            dr_export9["TOTAL VALUE"] = "";
            dr_export9["TAXABLE VALUE"] = "";
            dr_export9["INTEGRATED TAX"] = "";
            dr_export9["CENTRAL TAX"] = "";
            dr_export9["STATE OR UT TAX"] = "";
            dr_export9["CESS AMOUNT"] = "";
            dt9.Rows.Add(dr_export9);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            dtExcelRecords.Merge(dt9);
            dtExcelRecords.AcceptChanges();

            #region DOC_ISSUED
            //DataSet ds1 = objBs.SearchEmployeeJobTypeName(ddlactiveselect.SelectedValue, txtsearch.Text);           
            //if (ds1 != null)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            #region

            //DataTable dt1 = new DataTable();
            dt10.Columns.Add(new DataColumn("NATURE OF DOCUMENT"));
            dt10.Columns.Add(new DataColumn("SERIAL NUMBER"));
            dt10.Columns.Add(new DataColumn("FROM SERIAL NUMBER"));
            dt10.Columns.Add(new DataColumn("TO SERIAL NUMBER"));
            dt10.Columns.Add(new DataColumn("TOTAL NUMBER"));
            dt10.Columns.Add(new DataColumn("CANCELLED"));
            dt10.Columns.Add(new DataColumn("NET ISSUED"));
            dt10.TableName = "DOC_ISSUED";
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            DataRow dr_export10 = dt10.NewRow();
            //dr_export1["JobType_Name"] = dr1["JobType_Name"];
            //dr_export1["IsActive"] = dr1["IsActive"];
            dr_export10["NATURE OF DOCUMENT"] = "";
            dr_export10["SERIAL NUMBER"] = "";
            dr_export10["FROM SERIAL NUMBER"] = "";
            dr_export10["TO SERIAL NUMBER"] = "";
            dr_export10["TOTAL NUMBER"] = "";
            dr_export10["CANCELLED"] = "";
            dr_export10["NET ISSUED"] = "";
            dt10.Rows.Add(dr_export10);
            //}

            #endregion
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Records Found');", true);
            //}
            #endregion

            dtExcelRecords.Merge(dt10);
            dtExcelRecords.AcceptChanges();

            System.IO.File.WriteAllText(Server.MapPath("~/Files/GSTR1.json"), ConvertDataTableTojSonString(dtExcelRecords));

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("~/Files/GSTR1.json"));
            Response.WriteFile("~/Files/GSTR1.json");
            Response.End();

        }

        public static string[] GetSheetNamesFromExcel(string connectionString)
        {
            try
            {
                DataTable dt = null;
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        String[] excelSheetNames = new String[dt.Rows.Count];
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheetNames[i] = row["TABLE_NAME"].ToString();
                            i++;
                        }
                        return excelSheetNames;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static DataSet ConvertExcelSheetToDataset(string filePath)
        {
            try
            {
                //string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", filePath);

                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                //         fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                DataSet ds = new DataSet();
                foreach (var excelSheetName in GetSheetNamesFromExcel(connectionString))
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        var dataTable = new DataTable(excelSheetName.ToString().Replace("$", string.Empty));
                        string query = string.Format("SELECT * FROM [{0}]", excelSheetName);
                        connection.Open();
                        OleDbDataAdapter da = new OleDbDataAdapter(query, connection);
                        da.Fill(dataTable);
                        ds.Tables.Add(dataTable);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (!string.IsNullOrEmpty(filename))
                {
                    DataSet ds = ConvertExcelSheetToDataset(filename);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        // MessageBox.Show("ExcelSheet To Dataset converted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnGSTR3B_Click(object sender, EventArgs e)
        {
            DateTime frmdate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            #region osup_det
            var jsonosup_det = "";
            DataTable dtosup_det = new DataTable();
            DataRow drosup_det;
            dtosup_det.Columns.Add(new DataColumn("txval", typeof(double)));
            dtosup_det.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtosup_det.Columns.Add(new DataColumn("camt", typeof(double)));
            dtosup_det.Columns.Add(new DataColumn("samt", typeof(double)));
            dtosup_det.Columns.Add(new DataColumn("csamt", typeof(double)));

            DataSet dsosup_det = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            for (int v = 0; v < dsosup_det.Tables[0].Rows.Count; v++)
            {
                drosup_det = dtosup_det.NewRow();
                drosup_det["txval"] =  Convert.ToDouble(dsosup_det.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_det["iamt"] = Convert.ToDouble(dsosup_det.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_det["camt"] = Convert.ToDouble(dsosup_det.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_det["samt"] =  Convert.ToDouble(dsosup_det.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_det["csamt"] = Convert.ToDouble(dsosup_det.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                dtosup_det.Rows.Add(drosup_det);
            }
            jsonosup_det = DataTableToJSONWithJSONNet(dtosup_det);
            #endregion

            #region osup_zero

            var jsonosup_zero = "";
            DataTable dtosup_zero = new DataTable();
            DataRow drosup_zero;
            dtosup_zero.Columns.Add(new DataColumn("txval", typeof(double)));
            dtosup_zero.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtosup_zero.Columns.Add(new DataColumn("camt", typeof(double)));
            dtosup_zero.Columns.Add(new DataColumn("samt", typeof(double)));
            dtosup_zero.Columns.Add(new DataColumn("csamt", typeof(double)));

            DataSet dsosup_zero = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            for (int v = 0; v < dsosup_zero.Tables[0].Rows.Count; v++)
            {
                drosup_zero = dtosup_zero.NewRow();
                drosup_zero["txval"] = Convert.ToDouble(dsosup_zero.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_zero["iamt"] = Convert.ToDouble(dsosup_zero.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_zero["camt"] = Convert.ToDouble(dsosup_zero.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_zero["samt"] = Convert.ToDouble(dsosup_zero.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_zero["csamt"] = Convert.ToDouble(dsosup_zero.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                dtosup_zero.Rows.Add(drosup_zero);
            }
            jsonosup_zero = DataTableToJSONWithJSONNet(dtosup_zero);
            #endregion

            #region osup_nil_exmp

            var jsonosup_nil_exmp = "";
            DataTable dtosup_nil_exmp = new DataTable();
            DataRow drosup_nil_exmp;
            dtosup_nil_exmp.Columns.Add(new DataColumn("txval", typeof(double)));
            dtosup_nil_exmp.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtosup_nil_exmp.Columns.Add(new DataColumn("camt", typeof(double)));
            dtosup_nil_exmp.Columns.Add(new DataColumn("samt", typeof(double)));
            dtosup_nil_exmp.Columns.Add(new DataColumn("csamt", typeof(double)));

            DataSet dsosup_nil_exmp = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            for (int v = 0; v < dsosup_nil_exmp.Tables[0].Rows.Count; v++)
            {
                drosup_nil_exmp = dtosup_nil_exmp.NewRow();
                drosup_nil_exmp["txval"] = Convert.ToDouble(dsosup_nil_exmp.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_nil_exmp["iamt"] = Convert.ToDouble(dsosup_nil_exmp.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_nil_exmp["camt"] = Convert.ToDouble(dsosup_nil_exmp.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_nil_exmp["samt"] = Convert.ToDouble(dsosup_nil_exmp.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_nil_exmp["csamt"] = Convert.ToDouble(dsosup_nil_exmp.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                dtosup_nil_exmp.Rows.Add(drosup_nil_exmp);
            }
            jsonosup_nil_exmp = DataTableToJSONWithJSONNet(dtosup_nil_exmp);
            #endregion

            #region isup_rev

            var jsonosup_rev = "";
            DataTable dtosup_rev = new DataTable();
            DataRow drosup_rev;
            dtosup_rev.Columns.Add(new DataColumn("txval", typeof(double)));
            dtosup_rev.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtosup_rev.Columns.Add(new DataColumn("camt", typeof(double)));
            dtosup_rev.Columns.Add(new DataColumn("samt", typeof(double)));
            dtosup_rev.Columns.Add(new DataColumn("csamt", typeof(double)));

            DataSet dsosup_rev = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            for (int v = 0; v < dsosup_rev.Tables[0].Rows.Count; v++)
            {
                drosup_rev = dtosup_rev.NewRow();
                drosup_rev["txval"] = Convert.ToDouble(dsosup_rev.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_rev["iamt"] = Convert.ToDouble(dsosup_rev.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_rev["camt"] = Convert.ToDouble(dsosup_rev.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_rev["samt"] = Convert.ToDouble(dsosup_rev.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_rev["csamt"] = Convert.ToDouble(dsosup_rev.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                dtosup_rev.Rows.Add(drosup_rev);
            }
            jsonosup_rev = DataTableToJSONWithJSONNet(dtosup_rev);
            #endregion

            #region osup_nongst

            var jsonosup_nongst = "";
            DataTable dtosup_nongst = new DataTable();
            DataRow drosup_nongst;
            dtosup_nongst.Columns.Add(new DataColumn("txval", typeof(double)));
            dtosup_nongst.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtosup_nongst.Columns.Add(new DataColumn("camt", typeof(double)));
            dtosup_nongst.Columns.Add(new DataColumn("samt", typeof(double)));
            dtosup_nongst.Columns.Add(new DataColumn("csamt", typeof(double)));

            DataSet dsosup_nongst = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            for (int v = 0; v < dsosup_nongst.Tables[0].Rows.Count; v++)
            {
                drosup_nongst = dtosup_nongst.NewRow();
                drosup_nongst["txval"] = Convert.ToDouble(dsosup_nongst.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_nongst["iamt"] = Convert.ToDouble(dsosup_nongst.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_nongst["camt"] = Convert.ToDouble(dsosup_nongst.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_nongst["samt"] = Convert.ToDouble(dsosup_nongst.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                drosup_nongst["csamt"] = Convert.ToDouble(dsosup_nongst.Tables[0].Rows[v]["TaxableValue"]).ToString("f2");
                dtosup_nongst.Rows.Add(drosup_nongst);
            }
            jsonosup_nongst = DataTableToJSONWithJSONNet(dtosup_nongst);
            #endregion

            #region itc_avl

            var jsonitc_avl = "";
            DataTable dtitc_avl = new DataTable();
            DataRow dritc_avl;
            dtitc_avl.Columns.Add(new DataColumn("ty", typeof(string)));
            dtitc_avl.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtitc_avl.Columns.Add(new DataColumn("camt", typeof(double)));
            dtitc_avl.Columns.Add(new DataColumn("samt", typeof(double)));
            dtitc_avl.Columns.Add(new DataColumn("csamt", typeof(double)));

            //DataSet dsitc_avl = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            //for (int v = 0; v < dsitc_avl.Tables[0].Rows.Count; v++)
            //{
                dritc_avl = dtitc_avl.NewRow();
                dritc_avl["ty"] = "IMPG";
                dritc_avl["iamt"] = "0.00";
                dritc_avl["camt"] = "0.00";
                dritc_avl["samt"] = "0.00";
                dritc_avl["csamt"] = "0.00";
                dtitc_avl.Rows.Add(dritc_avl);

                dritc_avl = dtitc_avl.NewRow();
                dritc_avl["ty"] = "IMPS";
                dritc_avl["iamt"] = "0.00";
                dritc_avl["camt"] = "0.00";
                dritc_avl["samt"] = "0.00";
                dritc_avl["csamt"] = "0.00";
                dtitc_avl.Rows.Add(dritc_avl);


                dritc_avl = dtitc_avl.NewRow();
                dritc_avl["ty"] = "ISRC";
                dritc_avl["iamt"] = "0.00";
                dritc_avl["camt"] = "0.00";
                dritc_avl["samt"] = "0.00";
                dritc_avl["csamt"] = "0.00";
                dtitc_avl.Rows.Add(dritc_avl);


                dritc_avl = dtitc_avl.NewRow();
                dritc_avl["ty"] = "ISD";
                dritc_avl["iamt"] = "0.00";
                dritc_avl["camt"] = "0.00";
                dritc_avl["samt"] = "0.00";
                dritc_avl["csamt"] = "0.00";
                dtitc_avl.Rows.Add(dritc_avl);

                dritc_avl = dtitc_avl.NewRow();
                dritc_avl["ty"] = "OTH";
                dritc_avl["iamt"] = "0.00";
                dritc_avl["camt"] = "0.00";
                dritc_avl["samt"] = "0.00";
                dritc_avl["csamt"] = "0.00";
                dtitc_avl.Rows.Add(dritc_avl);
            //}
            jsonitc_avl = DataTableToJSONWithJSONNet(dtitc_avl);
            #endregion

            #region itc_rev
            var jsonitc_rev = "";
            DataTable dtitc_rev = new DataTable();
            DataRow dritc_rev;
            dtitc_rev.Columns.Add(new DataColumn("ty", typeof(string)));
            dtitc_rev.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtitc_rev.Columns.Add(new DataColumn("camt", typeof(double)));
            dtitc_rev.Columns.Add(new DataColumn("samt", typeof(double)));
            dtitc_rev.Columns.Add(new DataColumn("csamt", typeof(double)));

            //DataSet dsitc_avl = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            //for (int v = 0; v < dsitc_avl.Tables[0].Rows.Count; v++)
            //{
            dritc_rev = dtitc_rev.NewRow();
            dritc_rev["ty"] = "RUL";
            dritc_rev["iamt"] = "0.00";
            dritc_rev["camt"] = "0.00";
            dritc_rev["samt"] = "0.00";
            dritc_rev["csamt"] = "0.00";
            dtitc_rev.Rows.Add(dritc_rev);

            dritc_rev = dtitc_rev.NewRow();
            dritc_rev["ty"] = "OTH";
            dritc_rev["iamt"] = "0.00";
            dritc_rev["camt"] = "0.00";
            dritc_rev["samt"] = "0.00";
            dritc_rev["csamt"] = "0.00";
            dtitc_rev.Rows.Add(dritc_rev);
            //}
            jsonitc_rev = DataTableToJSONWithJSONNet(dtitc_rev);
            #endregion

            #region itc_net
            var jsonitc_net = "";
            DataTable dtitc_net = new DataTable();
            DataRow dritc_net;           
            dtitc_net.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtitc_net.Columns.Add(new DataColumn("camt", typeof(double)));
            dtitc_net.Columns.Add(new DataColumn("samt", typeof(double)));
            dtitc_net.Columns.Add(new DataColumn("csamt", typeof(double)));

            //DataSet dsitc_avl = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            //for (int v = 0; v < dsitc_avl.Tables[0].Rows.Count; v++)
            //{
            dritc_net = dtitc_net.NewRow();
            dritc_net["iamt"] = "0.00";
            dritc_net["camt"] = "0.00";
            dritc_net["samt"] = "0.00";
            dritc_net["csamt"] = "0.00";
            dtitc_net.Rows.Add(dritc_net);

            //}
            jsonitc_net = DataTableToJSONWithJSONNet(dtitc_net);
            #endregion

            #region itc_inelg
            var jsonitc_inelg = "";
            DataTable dtitc_inelg = new DataTable();
            DataRow dritc_inelg;
            dtitc_inelg.Columns.Add(new DataColumn("ty", typeof(string)));
            dtitc_inelg.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtitc_inelg.Columns.Add(new DataColumn("camt", typeof(double)));
            dtitc_inelg.Columns.Add(new DataColumn("samt", typeof(double)));
            dtitc_inelg.Columns.Add(new DataColumn("csamt", typeof(double)));

            //DataSet dsitc_avl = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            //for (int v = 0; v < dsitc_avl.Tables[0].Rows.Count; v++)
            //{
            dritc_inelg = dtitc_inelg.NewRow();
            dritc_inelg["ty"] = "RUL";
            dritc_inelg["iamt"] = "0.00";
            dritc_inelg["camt"] = "0.00";
            dritc_inelg["samt"] = "0.00";
            dritc_inelg["csamt"] = "0.00";
            dtitc_inelg.Rows.Add(dritc_inelg);


            dritc_inelg = dtitc_inelg.NewRow();
            dritc_inelg["ty"] = "OTH";
            dritc_inelg["iamt"] = "0.00";
            dritc_inelg["camt"] = "0.00";
            dritc_inelg["samt"] = "0.00";
            dritc_inelg["csamt"] = "0.00";
            dtitc_inelg.Rows.Add(dritc_inelg);

            //}
            jsonitc_inelg = DataTableToJSONWithJSONNet(dtitc_inelg);
            #endregion

            #region isup_details
            var jsonisup_details = "";
            DataTable dtisup_details = new DataTable();
            DataRow drisup_details;
            dtisup_details.Columns.Add(new DataColumn("ty", typeof(string)));
            dtisup_details.Columns.Add(new DataColumn("inter", typeof(double)));
            dtisup_details.Columns.Add(new DataColumn("intra", typeof(double)));

            //DataSet dsitc_avl = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            //for (int v = 0; v < dsitc_avl.Tables[0].Rows.Count; v++)
            //{
            drisup_details = dtisup_details.NewRow();
            drisup_details["ty"] = "GST";
            drisup_details["inter"] = "0.00";
            drisup_details["intra"] = "0.00";
            dtisup_details.Rows.Add(drisup_details);


            drisup_details = dtisup_details.NewRow();
            drisup_details["ty"] = "NONGST";
            drisup_details["inter"] = "0.00";
            drisup_details["intra"] = "0.00";
            dtisup_details.Rows.Add(drisup_details);

            //}
            jsonisup_details = DataTableToJSONWithJSONNet(dtisup_details);
            #endregion

            #region intr_details
            var jsonintr_details = "";
            DataTable dtintr_details = new DataTable();
            DataRow drintr_details;

            dtintr_details.Columns.Add(new DataColumn("iamt", typeof(double)));
            dtintr_details.Columns.Add(new DataColumn("camt", typeof(double)));
            dtintr_details.Columns.Add(new DataColumn("samt", typeof(double)));
            dtintr_details.Columns.Add(new DataColumn("csamt", typeof(double)));

            //DataSet dsitc_avl = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            //for (int v = 0; v < dsitc_avl.Tables[0].Rows.Count; v++)
            //{
            drintr_details = dtintr_details.NewRow();
            drintr_details["iamt"] = "0.00";
            drintr_details["camt"] = "0.00";
            drintr_details["samt"] = "0.00";
            drintr_details["csamt"] = "0.00";
            dtintr_details.Rows.Add(drintr_details);

            //}
            jsonintr_details = DataTableToJSONWithJSONNet(dtintr_details);
            #endregion

            #region unreg_details
            var jsonunreg_details = "";
            DataTable dtunreg_details = new DataTable();
            DataRow drunreg_details;

            dtunreg_details.Columns.Add(new DataColumn("pos", typeof(string)));
            dtunreg_details.Columns.Add(new DataColumn("txval", typeof(double)));
            dtunreg_details.Columns.Add(new DataColumn("iamt", typeof(double)));
            
            //DataSet dsitc_avl = objBs.gstreportGSTR2B2BGroup(frmdate, todate, ddloutlet.SelectedValue);
            //for (int v = 0; v < dsitc_avl.Tables[0].Rows.Count; v++)
            //{
            drunreg_details = dtunreg_details.NewRow();
            drunreg_details["pos"] = "32";
            drunreg_details["txval"] = "0.00";
            drunreg_details["iamt"] = "0.00";
            dtunreg_details.Rows.Add(drunreg_details);

            //}
            jsonunreg_details = DataTableToJSONWithJSONNet(dtunreg_details);
            #endregion


            string gstbrn = txtGSTNo.Text;
            string fileyr = txtFileYear.Text;
            string Result = @"{""gstin"":" + "" + gstbrn + "" + @",""ret_period"":" + "" + fileyr + "" + @",""sup_details"":{""osup_det"" : " + jsonosup_det.Replace(@"[{", "{").Replace(@"}]", "}") + @",""osup_zero"":{ " + jsonosup_zero.Replace(@"[{", "").Replace(@"}]", "") + @"},""osup_nil_exmp"":{ " + jsonosup_nil_exmp.Replace(@"[{", "").Replace(@"}]", "") + @"},""isup_rev"":{ " + jsonosup_rev.Replace(@"[{", "").Replace(@"}]", "") + @"},""osup_nongst"":{ " + jsonosup_nongst.Replace(@"[{", "").Replace(@"}]", "") + @"}},""itc_elg"":{""itc_avl"":" + jsonitc_avl + @",""itc_rev"":" + jsonitc_rev + @",""itc_net"":{" + jsonitc_net.Replace(@"[{", "").Replace(@"}]", "") + @"},""itc_inelg"":" + jsonitc_inelg + @""",""inward_sup"":{""isup_details"" : " + jsonisup_details + @"},""intr_ltfee"":{""intr_details"" : " + jsonintr_details.Replace(@"[{", "").Replace(@"}]", "") + @",""ltfee_details"":{}}"",""inter_sup"":{""unreg_details"":" + jsonunreg_details + @",""comp_details"":[],""uin_details"":[]}}";
            System.IO.File.WriteAllText(Server.MapPath("~/Files/GSTR3B.json"), Result);

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("~/Files/GSTR3B.json"));
            Response.WriteFile("~/Files/GSTR3B.json");
            Response.End();
        }


        protected void btnUpload123_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (filename != "")
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Files/" + filename));
                    txtDoc.Text = "~/Files/" + filename;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Upload File.Thank You!!!');", true);
                    return;
                }

                string connectionString = "";
                string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=" + "\"";
                char[] specialCharactersArray = specialCharacters.ToCharArray();

                if (FileUpload1.HasFile)
                {

                    #region
                    string datett = DateTime.Now.ToString();
                    string dtaa = Convert.ToDateTime(datett).ToString("dd-MM-yyyy-hh-mm-ss");
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName) + dtaa;
                    string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string fileLocation = Server.MapPath("~/App_Data/" + fileName);
                    FileUpload1.SaveAs(fileLocation);
                    if (fileExtension == ".xls")
                    {
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Attach Correct Format file Extension.(.xls or .xlsx)');", true);
                        return;
                    }

                    DataTable dtResult = null;
                    OleDbConnection con = new OleDbConnection(connectionString);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;

                    OleDbCommand cmdB2CS = new OleDbCommand();
                    cmdB2CS.CommandType = System.Data.CommandType.Text;
                    cmdB2CS.Connection = con;

                    OleDbCommand cmdHSN = new OleDbCommand();
                    cmdHSN.CommandType = System.Data.CommandType.Text;
                    cmdHSN.Connection = con;

                    OleDbCommand cmdDOCS = new OleDbCommand();
                    cmdDOCS.CommandType = System.Data.CommandType.Text;
                    cmdDOCS.Connection = con;

                    OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                    DataTable dtExcelRecords = new DataTable();
                    con.Open();
                    DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    DataSet ds = new DataSet();
                    string getExcelSheetNameB2B = "";
                    DataTable dtExcelRecordsB2B = new DataTable();
                    DataSet dsB2B = new DataSet();
                    DataSet dsB2B1 = new DataSet();

                    string getExcelSheetNameB2CS = "";
                    DataTable dtExcelRecordsB2CS;
                    DataSet dsB2CS = new DataSet();

                    string getExcelSheetNameHSN = "";
                    DataTable dtExcelRecordsHSN;
                    DataSet dsHSN = new DataSet();

                    string getExcelSheetNameDOCS = "";
                    DataTable dtExcelRecordsDOCS;
                    DataSet dsDOCS = new DataSet();

                    var jsonB2B = "";
                    var jsonB2B2 = "";
                    var jsonB2CS = "";
                    var jsonHSN = "";
                    var jsonB2B1 = "";
                    var jsonB2B11 = "";
                    var jsonnil = "";
                    var jsonHSNItem = "";
                    var jsondocs = "";

                    DataTable dtB2B = new DataTable();
                    DataRow drB2B;
                    dtB2B.Columns.Add(new DataColumn("ctin", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("inv", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("idt", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("val", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("pos", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("rchrg", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("inv_typ", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("num", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("txval", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("rt", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("camt", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("samt", typeof(string)));
                    dtB2B.Columns.Add(new DataColumn("csamt", typeof(string)));

                    DataTable dtB2CS = new DataTable();
                    DataRow drB2CS;
                    dtB2CS.Columns.Add(new DataColumn("sply_ty", typeof(string)));
                    dtB2CS.Columns.Add(new DataColumn("rt", typeof(int)));
                    dtB2CS.Columns.Add(new DataColumn("typ", typeof(string)));
                    dtB2CS.Columns.Add(new DataColumn("pos", typeof(string)));
                    dtB2CS.Columns.Add(new DataColumn("txval", typeof(double)));
                    dtB2CS.Columns.Add(new DataColumn("camt", typeof(double)));
                    dtB2CS.Columns.Add(new DataColumn("samt", typeof(double)));
                    dtB2CS.Columns.Add(new DataColumn("csamt", typeof(int)));


                    DataTable dtHSN = new DataTable();
                    DataRow drHSN;
                    dtHSN.Columns.Add(new DataColumn("num", typeof(int)));
                    dtHSN.Columns.Add(new DataColumn("desc", typeof(string)));
                    dtHSN.Columns.Add(new DataColumn("uqc", typeof(string)));
                    dtHSN.Columns.Add(new DataColumn("qty", typeof(int)));
                    dtHSN.Columns.Add(new DataColumn("val", typeof(double)));
                    dtHSN.Columns.Add(new DataColumn("txval", typeof(double)));
                    dtHSN.Columns.Add(new DataColumn("iamt", typeof(int)));
                    dtHSN.Columns.Add(new DataColumn("samt", typeof(double)));
                    dtHSN.Columns.Add(new DataColumn("camt", typeof(double)));
                    dtHSN.Columns.Add(new DataColumn("csamt", typeof(int)));


                    DataTable dtDOCS = new DataTable();
                    DataRow drDOCS;
                    dtDOCS.Columns.Add(new DataColumn("num", typeof(int)));
                    dtDOCS.Columns.Add(new DataColumn("from", typeof(string)));
                    dtDOCS.Columns.Add(new DataColumn("to", typeof(string)));
                    dtDOCS.Columns.Add(new DataColumn("totnum", typeof(int)));
                    dtDOCS.Columns.Add(new DataColumn("cancel", typeof(int)));
                    dtDOCS.Columns.Add(new DataColumn("net_issue", typeof(int)));


                    if (dtExcelSheetName.Rows.Count > 0)
                    {
                        //for (int g = 0; g < dtExcelSheetName.Rows.Count; g++)
                        //{
                        //B2B
                        dtExcelRecordsB2B = new DataTable(getExcelSheetNameB2B.ToString().Replace("$", string.Empty));
                        getExcelSheetNameB2B = dtExcelSheetName.Rows[2]["Table_Name"].ToString();
                        if (getExcelSheetNameB2B == "b2b$")
                        {
                            //cmd.CommandText = "SELECT * FROM [" + getExcelSheetNameB2B + "A4:M43]";
                            cmd.CommandText = "SELECT * FROM [" + getExcelSheetNameB2B + "A5:M];";
                            //cmd.CommandText = "SELECT * FROM [" + getExcelSheetNameB2B + "A4:M43] where GSTIN/UIN of Recipient='33AAAFV7777R1Z9'";
                            dAdapter.SelectCommand = cmd;
                            dAdapter.Fill(dtExcelRecordsB2B);
                            dsB2B1.Tables.Add(dtExcelRecordsB2B);

                            DataView dvEmp = dsB2B1.Tables[0].DefaultView;
                            dvEmp.Sort = "GSTIN/UIN of Recipient asc";
                            dsB2B.Tables.Add(dvEmp.ToTable());
                        }

                        //B2CS                           
                        dtExcelRecordsB2CS = new DataTable(getExcelSheetNameB2CS.ToString().Replace("$", string.Empty));
                        getExcelSheetNameB2CS = dtExcelSheetName.Rows[4]["Table_Name"].ToString();
                        if (getExcelSheetNameB2CS == "b2cs$")
                        {
                            //cmdB2CS.CommandText = "SELECT * FROM [" + getExcelSheetNameB2CS + "A4:G5]";
                            cmdB2CS.CommandText = "SELECT * FROM [" + getExcelSheetNameB2CS + "A1:G];";
                            dAdapter.SelectCommand = cmdB2CS;
                            dAdapter.Fill(dtExcelRecordsB2CS);
                            dsB2CS.Tables.Add(dtExcelRecordsB2CS);
                        }

                        //HSN                           
                        dtExcelRecordsHSN = new DataTable(getExcelSheetNameB2CS.ToString().Replace("$", string.Empty));
                        getExcelSheetNameHSN = dtExcelSheetName.Rows[10]["Table_Name"].ToString();
                        if (getExcelSheetNameHSN == "hsn$")
                        {
                            //cmdHSN.CommandText = "SELECT * FROM [" + getExcelSheetNameHSN + "A4:J29]";
                            cmdHSN.CommandText = "SELECT * FROM [" + getExcelSheetNameHSN + "A1:J];";
                            dAdapter.SelectCommand = cmdHSN;
                            dAdapter.Fill(dtExcelRecordsHSN);
                            dsHSN.Tables.Add(dtExcelRecordsHSN);
                        }
                        //}

                        //DOCS                          
                        dtExcelRecordsDOCS = new DataTable(getExcelSheetNameB2CS.ToString().Replace("$", string.Empty));
                        getExcelSheetNameDOCS = dtExcelSheetName.Rows[7]["Table_Name"].ToString();
                        if (getExcelSheetNameDOCS == "docs$")
                        {
                            //cmdHSN.CommandText = "SELECT * FROM [" + getExcelSheetNameHSN + "A4:J29]";
                            cmdDOCS.CommandText = "SELECT * FROM [" + getExcelSheetNameDOCS + "A1:E];";
                            dAdapter.SelectCommand = cmdDOCS;
                            dAdapter.Fill(dtExcelRecordsDOCS);
                            dsDOCS.Tables.Add(dtExcelRecordsDOCS);
                        }
                        //}

                        //B2B
                        if (getExcelSheetNameB2B != "")
                        {
                            if (getExcelSheetNameB2B == "b2b$")
                            {
                                for (int v = 0; v < dsB2B.Tables[0].Rows.Count; v++)
                                {
                                    if (dsB2B.Tables[0].Rows[v]["GSTIN/UIN of Recipient"].ToString() != "")
                                    {
                                        drB2B = dtB2B.NewRow();
                                        drB2B["ctin"] = dsB2B.Tables[0].Rows[v]["GSTIN/UIN of Recipient"].ToString();
                                        drB2B["inv"] = dsB2B.Tables[0].Rows[v]["Invoice Number"].ToString();
                                        drB2B["idt"] = Convert.ToDateTime(dsB2B.Tables[0].Rows[v]["Invoice date"]).ToString("dd-MM-yyyy");
                                        drB2B["val"] = Convert.ToDecimal(dsB2B.Tables[0].Rows[v]["Invoice Value"].ToString());
                                        drB2B["pos"] = Convert.ToString(dsB2B.Tables[0].Rows[v]["Place Of Supply"]).Substring(0, 2);
                                        drB2B["rchrg"] = dsB2B.Tables[0].Rows[v]["Reverse Charge"].ToString();
                                        drB2B["inv_typ"] = "R";
                                        drB2B["num"] = "1801";
                                        drB2B["txval"] = Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Taxable Value"]).ToString("f2");
                                        drB2B["rt"] = dsB2B.Tables[0].Rows[v]["Rate"].ToString();


                                        //drB2B["camt"] = Convert.ToDouble((Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Rate"]) / 100) / 2).ToString("f2");
                                        //drB2B["samt"] = Convert.ToDouble((Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Rate"]) / 100) / 2).ToString("f2");

                                        double ttax = (Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Rate"]) / 100) / 2;
                                        double d = (double)ttax;
                                        if ((d % 1) > 0)
                                        {
                                            drB2B["camt"] = Convert.ToDouble((Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Rate"]) / 100) / 2).ToString("f2");
                                            drB2B["samt"] = Convert.ToDouble((Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Rate"]) / 100) / 2).ToString("f2");

                                        }
                                        else
                                        {
                                            drB2B["camt"] = (Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Rate"]) / 100) / 2;
                                            drB2B["samt"] = (Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2B.Tables[0].Rows[v]["Rate"]) / 100) / 2;

                                        }

                                        drB2B["csamt"] = 0;
                                        dtB2B.Rows.Add(drB2B);
                                    }
                                }
                            }

                            //List<B2BItems> b2bList = new List<B2BItems>();    
                            //for (int i = 0; i < dtB2B.Rows.Count; i++)
                            //{
                            //    B2BItems b2bitems = new B2BItems();
                            //    b2bitems.num = dtB2B.Rows[i]["num"].ToString();
                            //    b2bitems.txval = dtB2B.Rows[i]["txval"].ToString();
                            //    b2bitems.rt = dtB2B.Rows[i]["rt"].ToString();
                            //    b2bitems.camt = dtB2B.Rows[i]["camt"].ToString();
                            //    b2bitems.samt = dtB2B.Rows[i]["samt"].ToString();
                            //    b2bitems.csamt = dtB2B.Rows[i]["csamt"].ToString();
                            //    b2bList.Add(b2bitems);
                            //}

                            List<B2Bctin> b2bctin = new List<B2Bctin>();
                            List<B2BParent> b2bParent2 = new List<B2BParent>();
                            List<B2BParent> b2bParent = new List<B2BParent>();
                            List<B2B> b2bMain = new List<B2B>();
                            List<B2B2> b2bMain2 = new List<B2B2>();
                            List<B2BItems> b2bList = new List<B2BItems>();

                            B2BParent b2bpitems = new B2BParent();
                            for (int i = 0; i < dtB2B.Rows.Count; i++)
                            {
                                B2B b2bdetails = new B2B();
                                if (i == 0)
                                {
                                    // b2bdetails.ctin = dtB2B.Rows[i]["ctin"].ToString();

                                    string strval1 = "{\"inum\":\"" + dtB2B.Rows[i]["inv"].ToString() + "\",\"idt\":\"" + dtB2B.Rows[i]["idt"].ToString() + "\",\"val\":" + dtB2B.Rows[i]["val"].ToString() + ",\"pos\":\"" + dtB2B.Rows[i]["pos"].ToString() + "\",\"rchrg\":" + dtB2B.Rows[i]["rchrg"].ToString() + ",\"inv_typ\":" + dtB2B.Rows[i]["inv_typ"].ToString() + "}";
                                    jsonB2B11 = JsonConvert.SerializeObject(strval1);

                                    // b2bdetails.ctin = dtB2B.Rows[i]["ctin"].ToString() + "," + jsonB2B11;
                                    //string gg = dtB2B.Rows[i]["ctin"].ToString() + ",\"inv\":";

                                    b2bpitems.ctin = dtB2B.Rows[i]["ctin"].ToString();// gg.Replace(@"\", "") + jsonB2B11;

                                    //b2bdetails.inv = jsonB2B11;// dtB2B.Rows[i]["inv"].ToString();
                                    //b2bdetails.idt = dtB2B.Rows[i]["idt"].ToString();
                                    //b2bdetails.val = dtB2B.Rows[i]["val"].ToString();
                                    //b2bdetails.pos = dtB2B.Rows[i]["pos"].ToString();
                                    //b2bdetails.rchrg = dtB2B.Rows[i]["rchrg"].ToString();
                                    //b2bdetails.inv_typ = dtB2B.Rows[i]["inv_typ"].ToString();
                                    //for (int m = i; m < dtB2B.Rows.Count; m++)
                                    //{
                                    B2BItems b2bitems = new B2BItems();
                                    b2bitems.num = dtB2B.Rows[i]["num"].ToString();
                                    string strval = "{\"txval\":" + Convert.ToDouble(dtB2B.Rows[i]["txval"]).ToString("f2") + ",\"rt\":" + dtB2B.Rows[i]["rt"].ToString() + ",\"camt\":" + dtB2B.Rows[i]["camt"].ToString() + ",\"samt\":" + dtB2B.Rows[i]["samt"].ToString() + ",\"csamt\":" + dtB2B.Rows[i]["csamt"].ToString() + "}";
                                    jsonB2B1 = JsonConvert.SerializeObject(strval);
                                    b2bitems.itm_det = jsonB2B1;// strval.Replace(@"\", "");//dtB2B.Rows[i]["txval"].ToString();
                                    //b2bitems.rt = dtB2B.Rows[i]["rt"].ToString();
                                    //b2bitems.camt = dtB2B.Rows[i]["camt"].ToString();
                                    //b2bitems.samt = dtB2B.Rows[i]["samt"].ToString();
                                    //b2bitems.csamt = dtB2B.Rows[i]["csamt"].ToString();
                                    b2bList = new List<B2BItems>();
                                    b2bList.Add(b2bitems);
                                    // }
                                    b2bdetails.itms = b2bList;
                                    //b2bdetails.num = dtB2B.Rows[i]["num"].ToString();
                                    //b2bdetails.txval = dtB2B.Rows[i]["txval"].ToString();
                                    //b2bdetails.rt = dtB2B.Rows[i]["rt"].ToString();
                                    //b2bdetails.camt = dtB2B.Rows[i]["camt"].ToString();
                                    //b2bdetails.samt = dtB2B.Rows[i]["samt"].ToString();
                                    //b2bdetails.csamt = dtB2B.Rows[i]["csamt"].ToString();
                                    b2bMain.Add(b2bdetails);
                                    //b2bpitems.inv = b2bMain;

                                    string strval2 = "{\"inum\":\"" + dtB2B.Rows[i]["inv"].ToString() + "\",\"idt\":\"" + dtB2B.Rows[i]["idt"].ToString() +
                                                    "\",\"val\":" + dtB2B.Rows[i]["val"].ToString() + ",\"pos\":\"" + dtB2B.Rows[i]["pos"].ToString() +
                                                    "\",\"rchrg\":\"" + dtB2B.Rows[i]["rchrg"].ToString() + "\",\"inv_typ\":\"" + dtB2B.Rows[i]["inv_typ"].ToString() +
                                                    "\",\"itms\":[{\"num\":" + dtB2B.Rows[i]["num"].ToString() + ",\"itm_det\":" + strval + "}]}";

                                    b2bMain2.Add(new B2B2
                                    {
                                        ctin = dtB2B.Rows[i]["ctin"].ToString(),
                                        val = strval2
                                    });
                                    b2bctin.Add(new B2Bctin
                                    {
                                        ctin = dtB2B.Rows[i]["ctin"].ToString()
                                    });
                                }
                                else
                                {
                                    if (dtB2B.Rows[i]["ctin"].ToString() == dtB2B.Rows[i - 1]["ctin"].ToString())
                                    {
                                        //b2bdetails.ctin = dtB2B.Rows[i]["ctin"].ToString();
                                        string strval1 = "{\"inum\":\"" + dtB2B.Rows[i]["inv"].ToString() + "\",\"idt\":\"" + dtB2B.Rows[i]["idt"].ToString() + "\",\"val\":" + dtB2B.Rows[i]["val"].ToString() + ",\"pos\":" + dtB2B.Rows[i]["pos"].ToString() + ",\"rchrg\":" + dtB2B.Rows[i]["rchrg"].ToString() + ",\"inv_typ\":" + dtB2B.Rows[i]["inv_typ"].ToString() + "}";
                                        jsonB2B11 = JsonConvert.SerializeObject(strval1);
                                        //b2bdetails.ctin = jsonB2B11;// dtB2B.Rows[i]["inv"].ToString();
                                        //b2bdetails.idt = dtB2B.Rows[i]["idt"].ToString();
                                        //b2bdetails.val = dtB2B.Rows[i]["val"].ToString();
                                        //b2bdetails.pos = dtB2B.Rows[i]["pos"].ToString();
                                        //b2bdetails.rchrg = dtB2B.Rows[i]["rchrg"].ToString();
                                        //b2bdetails.inv_typ = dtB2B.Rows[i]["inv_typ"].ToString();
                                        //for (int m = i; m < dtB2B.Rows.Count; m++)
                                        //{
                                        B2BItems b2bitems = new B2BItems();
                                        b2bitems.num = dtB2B.Rows[i]["num"].ToString();
                                        string strval = "{\"txval\":" + Convert.ToDouble(dtB2B.Rows[i]["txval"]).ToString("f2") + ",\"rt\":" + dtB2B.Rows[i]["rt"].ToString() + ",\"camt\":" + dtB2B.Rows[i]["camt"].ToString() + ",\"samt\":" + dtB2B.Rows[i]["samt"].ToString() + ",\"csamt\":" + dtB2B.Rows[i]["csamt"].ToString() + "}";
                                        jsonB2B1 = JsonConvert.SerializeObject(strval);
                                        b2bitems.itm_det = jsonB2B1;// strval.Replace(@"\", "");//dtB2B.Rows[i]["txval"].ToString();
                                        //b2bitems.rt = dtB2B.Rows[i]["rt"].ToString();
                                        //b2bitems.camt = dtB2B.Rows[i]["camt"].ToString();
                                        //b2bitems.samt = dtB2B.Rows[i]["samt"].ToString();
                                        //b2bitems.csamt = dtB2B.Rows[i]["csamt"].ToString();
                                        b2bList = new List<B2BItems>();
                                        b2bList.Add(b2bitems);
                                        // }
                                        b2bdetails.itms = b2bList;
                                        //b2bdetails.num = dtB2B.Rows[i]["num"].ToString();
                                        //b2bdetails.txval = dtB2B.Rows[i]["txval"].ToString();
                                        //b2bdetails.rt = dtB2B.Rows[i]["rt"].ToString();
                                        //b2bdetails.camt = dtB2B.Rows[i]["camt"].ToString();
                                        //b2bdetails.samt = dtB2B.Rows[i]["samt"].ToString();
                                        //b2bdetails.csamt = dtB2B.Rows[i]["csamt"].ToString();
                                        b2bMain.Add(b2bdetails);
                                        //b2bpitems.inv = b2bMain;



                                        string strval2 = "{\"inum\":\"" + dtB2B.Rows[i]["inv"].ToString() + "\",\"idt\":\"" + dtB2B.Rows[i]["idt"].ToString() +
                                                        "\",\"val\":" + dtB2B.Rows[i]["val"].ToString() + ",\"pos\":\"" + dtB2B.Rows[i]["pos"].ToString() +
                                                        "\",\"rchrg\":\"" + dtB2B.Rows[i]["rchrg"].ToString() + "\",\"inv_typ\":\"" + dtB2B.Rows[i]["inv_typ"].ToString() +
                                                        "\",\"itms\":[{\"num\":" + dtB2B.Rows[i]["num"].ToString() + ",\"itm_det\":" + strval + "}]}";

                                        b2bMain2.Add(new B2B2
                                        {
                                            ctin = dtB2B.Rows[i]["ctin"].ToString(),
                                            val = strval2
                                        });
                                    }
                                    else
                                    {
                                        b2bParent.Add(b2bpitems);
                                        // b2bdetails.ctin = dtB2B.Rows[i]["ctin"].ToString();
                                        string strval1 = "{\"inum\":\"" + dtB2B.Rows[i]["inv"].ToString() + "\",\"idt\":\"" + dtB2B.Rows[i]["idt"].ToString() + "\",\"val\":" + dtB2B.Rows[i]["val"].ToString() + ",\"pos\":\"" + dtB2B.Rows[i]["pos"].ToString() + "\",\"rchrg\":" + dtB2B.Rows[i]["rchrg"].ToString() + ",\"inv_typ\":" + dtB2B.Rows[i]["inv_typ"].ToString() + "}";
                                        jsonB2B11 = JsonConvert.SerializeObject(strval1);

                                        // b2bdetails.ctin = dtB2B.Rows[i]["ctin"].ToString() + "," + jsonB2B11;
                                        //string gg = dtB2B.Rows[i]["ctin"].ToString() + ",\"inv\":";

                                        b2bpitems.ctin = dtB2B.Rows[i]["ctin"].ToString();// gg.Replace(@"\", "") + jsonB2B11;

                                        //b2bdetails.inv = jsonB2B11;// dtB2B.Rows[i]["inv"].ToString();
                                        //b2bdetails.idt = dtB2B.Rows[i]["idt"].ToString();
                                        //b2bdetails.val = dtB2B.Rows[i]["val"].ToString();
                                        //b2bdetails.pos = dtB2B.Rows[i]["pos"].ToString();
                                        //b2bdetails.rchrg = dtB2B.Rows[i]["rchrg"].ToString();
                                        //b2bdetails.inv_typ = dtB2B.Rows[i]["inv_typ"].ToString();
                                        //for (int m = i; m < dtB2B.Rows.Count; m++)
                                        //{
                                        B2BItems b2bitems = new B2BItems();
                                        b2bitems.num = dtB2B.Rows[i]["num"].ToString();
                                        string strval = "{\"txval\":" + Convert.ToDouble(dtB2B.Rows[i]["txval"]).ToString("f2") + ",\"rt\":" + dtB2B.Rows[i]["rt"].ToString() + ",\"camt\":" + dtB2B.Rows[i]["camt"].ToString() + ",\"samt\":" + dtB2B.Rows[i]["samt"].ToString() + ",\"csamt\":" + dtB2B.Rows[i]["csamt"].ToString() + "}";
                                        jsonB2B1 = JsonConvert.SerializeObject(strval);
                                        b2bitems.itm_det = jsonB2B1;// strval.Replace(@"\", "");//dtB2B.Rows[i]["txval"].ToString();
                                        //b2bitems.rt = dtB2B.Rows[i]["rt"].ToString();
                                        //b2bitems.camt = dtB2B.Rows[i]["camt"].ToString();
                                        //b2bitems.samt = dtB2B.Rows[i]["samt"].ToString();
                                        //b2bitems.csamt = dtB2B.Rows[i]["csamt"].ToString();
                                        b2bList = new List<B2BItems>();
                                        b2bList.Add(b2bitems);
                                        // }
                                        b2bdetails.itms = b2bList;
                                        //b2bdetails.num = dtB2B.Rows[i]["num"].ToString();
                                        //b2bdetails.txval = dtB2B.Rows[i]["txval"].ToString();
                                        //b2bdetails.rt = dtB2B.Rows[i]["rt"].ToString();
                                        //b2bdetails.camt = dtB2B.Rows[i]["camt"].ToString();
                                        //b2bdetails.samt = dtB2B.Rows[i]["samt"].ToString();
                                        //b2bdetails.csamt = dtB2B.Rows[i]["csamt"].ToString();
                                        b2bMain.Add(b2bdetails);
                                        //b2bpitems.inv = b2bMain;


                                        string strval2 = "{\"inum\":\"" + dtB2B.Rows[i]["inv"].ToString() + "\",\"idt\":\"" + dtB2B.Rows[i]["idt"].ToString() +
                                                        "\",\"val\":" + dtB2B.Rows[i]["val"].ToString() + ",\"pos\":\"" + dtB2B.Rows[i]["pos"].ToString() +
                                                         "\",\"rchrg\":\"" + dtB2B.Rows[i]["rchrg"].ToString() + "\",\"inv_typ\":\"" + dtB2B.Rows[i]["inv_typ"].ToString() +
                                                        "\",\"itms\":[{\"num\":" + dtB2B.Rows[i]["num"].ToString() + ",\"itm_det\":" + strval + "}]}";

                                        b2bMain2.Add(new B2B2
                                        {
                                            ctin = dtB2B.Rows[i]["ctin"].ToString(),
                                            val = strval2
                                        });


                                        b2bctin.Add(new B2Bctin
                                        {
                                            ctin = dtB2B.Rows[i]["ctin"].ToString()
                                        });

                                        //*************


                                        ////b2bdetails.ctin = dtB2B.Rows[i]["ctin"].ToString();
                                        //string strval1 = "{\"inum\":" + dtB2B.Rows[i]["inv"].ToString() + ",\"idt\":" + dtB2B.Rows[i]["idt"].ToString() + ",\"val\":" + dtB2B.Rows[i]["val"].ToString() + ",\"pos\":" + dtB2B.Rows[i]["pos"].ToString() + ",\"rchrg\":" + dtB2B.Rows[i]["rchrg"].ToString() + ",\"inv_typ\":" + dtB2B.Rows[i]["inv_typ"].ToString() + "}";
                                        //jsonB2B11 = JsonConvert.SerializeObject(strval1);
                                        ////b2bdetails.inv = jsonB2B11;// dtB2B.Rows[i]["inv"].ToString();
                                        //// b2bdetails.ctin = dtB2B.Rows[i]["ctin"].ToString() +","+ jsonB2B11;

                                        //string gg = dtB2B.Rows[i]["ctin"].ToString() + ",\"inv\":";

                                        //b2bpitems.ctin = dtB2B.Rows[i]["ctin"].ToString();
                                        ////b2bdetails.ctin = gg.Replace(@"\", "") + jsonB2B11;
                                        ////b2bdetails.idt = dtB2B.Rows[i]["idt"].ToString();
                                        ////b2bdetails.val = dtB2B.Rows[i]["val"].ToString();
                                        ////b2bdetails.pos = dtB2B.Rows[i]["pos"].ToString();
                                        ////b2bdetails.rchrg = dtB2B.Rows[i]["rchrg"].ToString();
                                        ////b2bdetails.inv_typ = dtB2B.Rows[i]["inv_typ"].ToString();
                                        ////for (int m = i; m < dtB2B.Rows.Count; m++)
                                        ////{
                                        //B2BItems b2bitems = new B2BItems();
                                        //b2bitems.num = dtB2B.Rows[i]["num"].ToString();
                                        //string strval = "{\"txval\":" + dtB2B.Rows[i]["txval"].ToString() + ",\"rt\":" + dtB2B.Rows[i]["rt"].ToString() + ",\"camt\":" + dtB2B.Rows[i]["camt"].ToString() + ",\"samt\":" + dtB2B.Rows[i]["samt"].ToString() + ",\"csamt\":" + dtB2B.Rows[i]["csamt"].ToString() + "}";
                                        //jsonB2B1 = JsonConvert.SerializeObject(strval);
                                        //b2bitems.itm_det = jsonB2B1;// strval.Replace(@"\", "");//dtB2B.Rows[i]["txval"].ToString();
                                        ////b2bitems.rt = dtB2B.Rows[i]["rt"].ToString();
                                        ////b2bitems.camt = dtB2B.Rows[i]["camt"].ToString();
                                        ////b2bitems.samt = dtB2B.Rows[i]["samt"].ToString();
                                        ////b2bitems.csamt = dtB2B.Rows[i]["csamt"].ToString();
                                        //b2bList = new List<B2BItems>();
                                        //b2bList.Add(b2bitems);
                                        //// }
                                        //b2bdetails.itms = b2bList;
                                        ////b2bdetails.num = dtB2B.Rows[i]["num"].ToString();
                                        ////b2bdetails.txval = dtB2B.Rows[i]["txval"].ToString();
                                        ////b2bdetails.rt = dtB2B.Rows[i]["rt"].ToString();
                                        ////b2bdetails.camt = dtB2B.Rows[i]["camt"].ToString();
                                        ////b2bdetails.samt = dtB2B.Rows[i]["samt"].ToString();
                                        ////b2bdetails.csamt = dtB2B.Rows[i]["csamt"].ToString();
                                        //b2bMain.Add(b2bdetails);
                                        //b2bpitems.inv = b2bMain;
                                    }
                                }
                            }


                            b2bParent.Add(b2bpitems);

                            foreach (var ctin in b2bctin)
                            {
                                List<B2B3> b2b3 = new List<B2B3>();
                                B2B3 b2b3i = new B2B3();
                                string strval = "";
                                foreach (var b2b2 in b2bMain2)
                                {
                                    if (b2b2.ctin == ctin.ctin)
                                    {
                                        strval += b2b2.val + ",";
                                    }
                                }
                                b2b3i.val = strval.Substring(0, strval.Length - 1);
                                b2b3.Add(b2b3i);

                                b2bParent2.Add(new B2BParent
                                {
                                    ctin = ctin.ctin,
                                    inv = "[" + strval.Substring(0, strval.Length - 1) + "]"
                                });
                            }

                            jsonB2B2 = JsonConvert.SerializeObject(b2bMain2);

                            jsonB2B = JsonConvert.SerializeObject(b2bParent2);

                            // jsonB2B = DataTableToJSONWithJSONNet(dtB2B);

                            #region OLD
                            //using (var rdr = cmd.ExecuteReader())
                            //{
                            //    //LINQ query - when executed will create anonymous objects for each row
                            //    var query =
                            //        from DbDataRecord row in rdr
                            //        select new
                            //        {
                            //            ctin = row[0],
                            //            inv = row[2],
                            //            idt = Convert.ToDateTime(row[3]).ToString("dd-MM-yyyy"),
                            //            val = row[4],
                            //            pos = Convert.ToString(row[5]).Substring(0, 2),
                            //            rchrg = row[6],
                            //            inv_typ = "R",
                            //            num = "1801",
                            //            txval = row[11],
                            //            rt = row[10],
                            //            camt = (Convert.ToDouble(row[11]) * Convert.ToDouble(row[10]) / 100) / 2,
                            //            samt = (Convert.ToDouble(row[11]) * Convert.ToDouble(row[10]) / 100) / 2,
                            //            csamt = 0,
                            //        };
                            //    //Generates JSON from the LINQ query
                            //    jsonB2B = JsonConvert.SerializeObject(query);
                            //}
                            #endregion
                        }

                        //B2CS
                        if (getExcelSheetNameB2CS != "")
                        {
                            if (getExcelSheetNameB2CS == "b2cs$")
                            {
                                for (int v = 0; v < dsB2CS.Tables[0].Rows.Count; v++)
                                {
                                    if (dsB2CS.Tables[0].Rows[v]["Type"].ToString() != "")
                                    {
                                        drB2CS = dtB2CS.NewRow();
                                        drB2CS["sply_ty"] = "INTRA";
                                        drB2CS["rt"] = dsB2CS.Tables[0].Rows[v]["Rate"].ToString();
                                        drB2CS["typ"] = dsB2CS.Tables[0].Rows[v]["Type"].ToString();
                                        drB2CS["pos"] = Convert.ToString(dsB2CS.Tables[0].Rows[v]["Place Of Supply"]).Substring(0, 2);
                                        drB2CS["txval"] = Convert.ToDouble(dsB2CS.Tables[0].Rows[v]["Taxable Value"]).ToString("f2");
                                        drB2CS["camt"] = Convert.ToDouble((Convert.ToDouble(dsB2CS.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2CS.Tables[0].Rows[v]["Rate"]) / 100) / 2).ToString("f2");
                                        drB2CS["samt"] = Convert.ToDouble((Convert.ToDouble(dsB2CS.Tables[0].Rows[v]["Taxable Value"]) * Convert.ToDouble(dsB2CS.Tables[0].Rows[v]["Rate"]) / 100) / 2).ToString("f2");
                                        drB2CS["csamt"] = 0;
                                        dtB2CS.Rows.Add(drB2CS);
                                    }
                                }
                            }

                            jsonB2CS = DataTableToJSONWithJSONNet(dtB2CS);

                            #region OLD
                            //using (var rdr = cmdB2CS.ExecuteReader())
                            //{
                            //    //LINQ query - when executed will create anonymous objects for each row
                            //    var query2 =
                            //        from DbDataRecord row in rdr
                            //        select new
                            //        {
                            //            sply_ty = "INTRA",
                            //            rt = row[3],
                            //            typ = row[0],
                            //            pos = Convert.ToString(row[1]).Substring(0, 2),
                            //            txval = row[4],
                            //            camt = (Convert.ToDouble(row[4]) * Convert.ToDouble(row[3]) / 100) / 2,
                            //            samt = (Convert.ToDouble(row[4]) * Convert.ToDouble(row[3]) / 100) / 2,
                            //            csamt = 0,
                            //        };
                            //    //Generates JSON from the LINQ query
                            //    jsonB2CS = JsonConvert.SerializeObject(query2);
                            //}
                            #endregion
                        }

                        List<HSNItems> hsnList = new List<HSNItems>();
                        HSN hs = new HSN();
                        //HSN
                        if (getExcelSheetNameHSN != "")
                        {
                            if (getExcelSheetNameHSN == "hsn$")
                            {
                                for (int v = 0; v < dsHSN.Tables[0].Rows.Count; v++)
                                {
                                    if (dsHSN.Tables[0].Rows[v]["Description"].ToString() != "")
                                    {
                                        drHSN = dtHSN.NewRow();
                                        drHSN["num"] = Convert.ToInt32(v + 1);
                                        drHSN["desc"] = dsHSN.Tables[0].Rows[v]["Description"].ToString();
                                        drHSN["uqc"] = Convert.ToString(dsHSN.Tables[0].Rows[v]["UQC"]).Substring(0, 3);
                                        drHSN["qty"] = Convert.ToInt32(dsHSN.Tables[0].Rows[v]["Total Quantity"]).ToString();
                                        drHSN["val"] = dsHSN.Tables[0].Rows[v]["Total Value"].ToString();
                                        drHSN["txval"] = Convert.ToDouble(dsHSN.Tables[0].Rows[v]["Taxable Value"]).ToString("f2");
                                        drHSN["csamt"] = 0;
                                        drHSN["iamt"] = 0;
                                        drHSN["samt"] = Convert.ToDouble(dsHSN.Tables[0].Rows[v]["Central Tax Amount"]).ToString("f2");
                                        drHSN["camt"] = Convert.ToDouble(dsHSN.Tables[0].Rows[v]["State/UT Tax Amount"]).ToString("f2");

                                        dtHSN.Rows.Add(drHSN);
                                    }
                                }
                            }

                            ////for (int m = 0; m < dtHSN.Rows.Count; m++)
                            ////{
                            ////    HSNItems hsnitems = new HSNItems();
                            ////    string strval = "{\"num\":" + dtHSN.Rows[m]["num"].ToString() + ",\"desc\":" + dtHSN.Rows[m]["desc"].ToString() + ",\"uqc\":" + dtHSN.Rows[m]["uqc"].ToString() + ",\"qty\":" + dtHSN.Rows[m]["qty"].ToString() + ",\"val\":" + dtHSN.Rows[m]["val"].ToString() + ",\"txval\":" + dtHSN.Rows[m]["txval"].ToString() + ",\"csamt\":" + dtHSN.Rows[m]["csamt"].ToString() + ",\"iamt\":" + dtHSN.Rows[m]["iamt"].ToString() + ",\"samt\":" + dtHSN.Rows[m]["samt"].ToString() + ",\"camt\":" + dtHSN.Rows[m]["camt"].ToString() + "}";
                            ////    jsonHSNItem = JsonConvert.SerializeObject(strval);
                            ////    hsnitems.hsn = jsonHSNItem;
                            ////    hsnList = new List<HSNItems>();
                            ////    hsnList.Add(hsnitems);
                            ////    //hs.itms = hsnList;
                            ////}


                            ////jsonHSN = JsonConvert.SerializeObject(hsnList);

                            jsonHSN = DataTableToJSONWithJSONNet(dtHSN);

                            #region OLD
                            //using (var rdr = cmdHSN.ExecuteReader())
                            //{
                            //    //LINQ query - when executed will create anonymous objects for each row
                            //    var query3 =
                            //        from DbDataRecord row in rdr
                            //        select new
                            //        {
                            //            num = "1",
                            //            desc = row[1],
                            //            uqc = Convert.ToString(row[2]).Substring(0, 3),
                            //            qty = row[3],
                            //            val = row[4],
                            //            txval = row[5],
                            //            iamt = 0,
                            //            samt = row[7],
                            //            camt = row[8],
                            //            csamt = 0
                            //        };
                            //    //Generates JSON from the LINQ query
                            //    jsonHSN = JsonConvert.SerializeObject(query3);
                            //}
                            #endregion
                        }


                        //DOCS
                        if (getExcelSheetNameDOCS != "")
                        {
                            if (getExcelSheetNameDOCS == "docs$")
                            {
                                for (int v = 0; v < dsDOCS.Tables[0].Rows.Count; v++)
                                {
                                    if (dsDOCS.Tables[0].Rows[v]["NATURE OF DOCUMENT"].ToString() != "")
                                    {
                                        drDOCS = dtDOCS.NewRow();
                                        drDOCS["num"] = Convert.ToInt32(v + 1);
                                        drDOCS["from"] = dsDOCS.Tables[0].Rows[v]["Sr# No# From"].ToString();
                                        drDOCS["to"] = Convert.ToString(dsDOCS.Tables[0].Rows[v]["Sr# No# To"]).ToString();
                                        drDOCS["totnum"] = Convert.ToInt32(dsDOCS.Tables[0].Rows[v]["TOTAL NUMBER"]).ToString();
                                        drDOCS["cancel"] = Convert.ToInt32(dsDOCS.Tables[0].Rows[v]["CANCELLED"]).ToString();
                                        drDOCS["net_issue"] = Convert.ToInt32(dsDOCS.Tables[0].Rows[v]["TOTAL NUMBER"]).ToString();

                                        dtDOCS.Rows.Add(drDOCS);
                                    }
                                }
                            }

                            jsondocs = DataTableToJSONWithJSONNet(dtDOCS);

                        }
                    }

                    //string ggg = "nil":{"inv":[{"sply_ty":"INTRB2B","expt_amt":0,"nil_amt":0,"ngsup_amt":0},{"sply_ty":"INTRAB2B","expt_amt":0,"nil_amt":0,"ngsup_amt":0},{"sply_ty":"INTRB2C","expt_amt":0,"nil_amt":0,"ngsup_amt":0},{"sply_ty":"INTRAB2C","expt_amt":0,"nil_amt":0,"ngsup_amt":0}]}

                    string nil = @"{""inv"":[{""sply_ty"":""INTRB2B"",""expt_amt"":0,""nil_amt"":0,""ngsup_amt"":0},{""sply_ty"":""INTRAB2B"",""expt_amt"":0,""nil_amt"":0,""ngsup_amt"":0},{""sply_ty"":""INTRB2C"",""expt_amt"":0,""nil_amt"":0,""ngsup_amt"":0},{""sply_ty"":""INTRAB2C"",""expt_amt"":0,""nil_amt"":0,""ngsup_amt"":0}]}";
                    jsonnil = JsonConvert.SerializeObject(nil);


                    //string Result = @"{""gstin"":""33BQEPS6629A1ZZ"",""fp"":""032021"",""version"":""GST3.0.1"",""hash"":""hash"",""b2b"":" + jsonB2B.Replace(@"\", "").Replace("{\"ctin\":\"\"", "").Replace("{\"val\":\"", "").Replace("\"[", "[").Replace("]\"", "]") + @",""b2cs"":" + jsonB2CS + @",""hsn"" :{""data"":" + jsonHSN + @"}}"; "{ }"

                    //{"num":"1","desc":"JTC 10 GM ASAFOETIDA CONTAINER","uqc":"NOS","qty":"115000","val":"128915","txval":"109250","iamt":"0","samt":"9832.50","camt":"9832.50","csamt":"0"}

                    //33BQEPS6629A1ZZ - Vairam
                    //string Result = @"{""gstin"":""33BQEPS6629A1ZZ"",""fp"":""032021"",""version"":""GST3.0.1"",""hash"":""hash"",""b2b"":" + jsonB2B.Replace(@"\", "").Replace("{\"ctin\":\"\"", "").Replace("{\"val\":\"", "").Replace("\"[", "[").Replace("]\"", "]") + @",""b2cs"":" + jsonB2CS + @",""nil"":" + jsonnil.Replace(@"\", "").Replace("\"{", "{").Replace("}\"", "}") + @",""hsn"" :{""data"":" + jsonHSN + @"}}";

                    ////33AABFB6986B1ZO - Banu
                    //string Result = @"{""gstin"":""33AABFB6986B1ZO"",""fp"":""032021"",""version"":""GST3.0.1"",""hash"":""hash"",""b2b"":" + jsonB2B.Replace(@"\", "").Replace("{\"ctin\":\"\"", "").Replace("{\"val\":\"", "").Replace("\"[", "[").Replace("]\"", "]") + @",""b2cs"":" + jsonB2CS + @",""nil"":" + jsonnil.Replace(@"\", "").Replace("\"{", "{").Replace("}\"", "}") + @",""hsn"" :{""data"":" + jsonHSN + @"}}";
                    string gstbrn = txtGSTNo.Text;
                    string fileyr = txtFileYear.Text;
                    string Result = @"{""gstin"":" + gstbrn + @",""fp"":" + fileyr + @",""version"":""GST3.0.1"",""hash"":""hash"",""b2b"":" + jsonB2B.Replace(@"\", "").Replace("{\"ctin\":\"\"", "").Replace("{\"val\":\"", "").Replace("\"[", "[").Replace("]\"", "]") + @",""b2cs"":" + jsonB2CS + @",""nil"":" + jsonnil.Replace(@"\", "").Replace("\"{", "{").Replace("}\"", "}") + @",""docs"" :" + jsondocs + @"}]},""hsn"" :{""data"":" + jsonHSN + @"}}";
                    System.IO.File.WriteAllText(Server.MapPath("~/Files/GSTR1.json"), Result);

                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("~/Files/GSTR1.json"));
                    Response.WriteFile("~/Files/GSTR1.json");
                    Response.End();

                    #endregion
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please upload Xls file.It Cannot be empty.');", true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (filename != "")
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Files/" + filename));
                    txtDoc.Text = "~/Files/" + filename;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Upload File.Thank You!!!');", true);
                    return;
                }

                string connectionString = "";
                string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=" + "\"";
                char[] specialCharactersArray = specialCharacters.ToCharArray();

                if (FileUpload1.HasFile)
                {
                    #region
                    string datett = DateTime.Now.ToString();
                    string dtaa = Convert.ToDateTime(datett).ToString("dd-MM-yyyy-hh-mm-ss");
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName) + dtaa;
                    string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string fileLocation = Server.MapPath("~/App_Data/" + fileName);
                    FileUpload1.SaveAs(fileLocation);
                    if (fileExtension == ".xls")
                    {
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Attach Correct Format file Extension.(.xls or .xlsx)');", true);
                        return;
                    }

                    DataTable dtResult = null;
                    OleDbConnection con = new OleDbConnection(connectionString);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                    DataTable dtExcelRecords = new DataTable();
                    con.Open();
                    DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    DataSet ds = new DataSet();
                    string getExcelSheetName = "";
                    if (dtExcelSheetName.Rows.Count > 0)
                    {
                        for (int g = 0; g < dtExcelSheetName.Rows.Count; g++)
                        {
                            var dtExcelRecords1 = new DataTable(getExcelSheetName.ToString().Replace("$", string.Empty));
                            getExcelSheetName = dtExcelSheetName.Rows[g]["Table_Name"].ToString();
                            cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                            dAdapter.SelectCommand = cmd;
                            dAdapter.Fill(dtExcelRecords1);


                            DataTable dtCloned = dtExcelRecords1.Clone();
                            for (int m = 0; m < dtCloned.Columns.Count; m++)
                            {
                                dtCloned.Columns[m].DataType = typeof(String);
                            }

                            for (int i = 3; i < dtExcelRecords1.Rows.Count; i++)
                            {
                                DataRow row = dtExcelRecords1.Rows[i];
                                dtCloned.ImportRow(row);
                            }
                            //dtCloned.Columns[0].DataType = typeof(String);
                            //foreach (DataRow row in dtExcelRecords1.Rows)
                            //{
                            //    dtCloned.ImportRow(row);
                            //}

                            ds.Tables.Add(dtCloned);
                        }
                    }

                    #endregion

                    DataTable dtResult1 = CustomMerge(ds);
                    //DataTable dtResult1 = new DataTable("Order");  
                    //for (int g1 = 0; g1 < dtExcelSheetName.Rows.Count; g1++)
                    //{
                    //    dtResult1 = ds.Tables[g1];
                    //    dtResult.Merge(dtResult1);  
                    //}


                    if (ds == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Uploading Excel is Empty');", true);
                        return;
                    }


                    System.IO.File.WriteAllText(Server.MapPath("~/Files/GSTR1.json"), DataTableToJSONWithJSONNet(dtResult1));

                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("~/Files/GSTR1.json"));
                    Response.WriteFile("~/Files/GSTR1.json");
                    Response.End();

                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please upload Xls file.It Cannot be empty.');", true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //Response.Redirect("ItemMasterGrid.aspx");

        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public DataTable CustomMerge(DataSet ds)
        {
            DataTable MergedDataTable = new DataTable();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                //ds.Tables[i].Merge(MergedDataTable, true, MissingSchemaAction.Ignore);
                // MergedDataTable = dataTableColsToOtherType(MergedDataTable, typeof(string));

                MergedDataTable.Merge(ds.Tables[i]);
            }
            return MergedDataTable;
        }

        public DataTable dataTableColsToOtherType(DataTable dt, Type type, List<string> colsForTypeChange = default(List<string>))
        {
            var dt2 = new DataTable();
            foreach (DataColumn c in dt.Columns)
            {
                if (colsForTypeChange != null && colsForTypeChange.Count > 0)
                {
                    if (colsForTypeChange.Contains(c.ColumnName))
                        dt2.Columns.Add(c.ColumnName, type);//Change column type if found in list "colsForTypeChange"
                    else dt2.Columns.Add(c.ColumnName, c.DataType);//No change in Column Type
                }
                else
                {
                    dt2.Columns.Add(c.ColumnName, type);//change all columns type to provided type
                }

            }
            dt2.Load(dt.CreateDataReader(), System.Data.LoadOption.OverwriteChanges);
            return dt2;
        }

        public String ConvertDataTableTojSonString(DataTable dataTable)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                   new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                tableRows.Add(row);
            }
            return serializer.Serialize(tableRows);
        }
    }
}
