using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using CommonLayer;
using System.Text;
using System.Data;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class CuttingProcessReceivePrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        double Qty = 0; double Amount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                string ProcessEntryId = Request.QueryString.Get("ProcessEntryId");
                if (ProcessEntryId != null && ProcessEntryId != "")
                {
                    int TotalQty = 0;

                    DataSet ds = objBs.CuttingProcessEntryPrint(Convert.ToInt32(ProcessEntryId));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataSet dsCompanyDetails = objBs.GetSelectCompanyDetails(Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString()));
                        lblFCompany.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();
                        lblCoName.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();

                        lblFAddress.Text = dsCompanyDetails.Tables[0].Rows[0]["Address"].ToString();
                        lblFAreaandPincode.Text = dsCompanyDetails.Tables[0].Rows[0]["Area"].ToString() + " - " + dsCompanyDetails.Tables[0].Rows[0]["Pincode"].ToString();
                        lblFGST.Text = dsCompanyDetails.Tables[0].Rows[0]["Tin"].ToString();

                        lblFPhone.Text = dsCompanyDetails.Tables[0].Rows[0]["PhoneNo"].ToString();
                        lblFMobile.Text = dsCompanyDetails.Tables[0].Rows[0]["MobileNo"].ToString();

                        lblFax.Text = dsCompanyDetails.Tables[0].Rows[0]["Fax"].ToString();
                        lblFEmail.Text = dsCompanyDetails.Tables[0].Rows[0]["Email"].ToString();

                        lblcompanyname.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                        lbladdress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        lblCityandPincode.Text = ds.Tables[0].Rows[0]["City"].ToString() + " - " + ds.Tables[0].Rows[0]["Pincode"].ToString();
                        lblArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();

                        lblphoneno.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        lblGST.Text = ds.Tables[0].Rows[0]["GSTIN"].ToString();

                        lblFullEntryNo.Text = ds.Tables[0].Rows[0]["FullEntryNo"].ToString();
                        lblEntryDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EntryDate"]).ToString("dd/MM/yyyy");
                        lblOrderDateBetween.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy") + "  To  " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                        lblprint.Text = ds.Tables[0].Rows[0]["Process"].ToString() + " Received Print";

                        #endregion

                        #region Received

                        DataTable DTS = new DataTable();
                        DTS.Columns.Add(new DataColumn("ChallanNo"));
                        DTS.Columns.Add(new DataColumn("Date"));
                        DTS.Columns.Add(new DataColumn("ExcNo"));
                        DTS.Columns.Add(new DataColumn("StyleNo"));
                        DTS.Columns.Add(new DataColumn("Description"));
                        DTS.Columns.Add(new DataColumn("Color"));
                        DTS.Columns.Add(new DataColumn("Qty"));

                        DataSet dsSizes = objBs.getProcessEntrySizesPrint(Convert.ToInt32(ProcessEntryId));
                        foreach (DataRow dr in dsSizes.Tables[0].Rows)
                        {
                            DTS.Columns.Add(new DataColumn(dr["Size"].ToString()));
                        }

                        DataSet dsSizesQty = objBs.getProcessEntrySizesQtyRecPrint(Convert.ToInt32(ProcessEntryId), "Received");

                        DataSet dsStyle = objBs.getProcessEntryStyleRecPrint(Convert.ToInt32(ProcessEntryId), "Received");
                        foreach (DataRow dr in dsStyle.Tables[0].Rows)
                        {
                            DataRow DR1 = DTS.NewRow();
                            DR1["ChallanNo"] = dr["ChallanNo"];
                            DR1["Date"] = dr["ItemDate"];
                            DR1["ExcNo"] = dr["ExcNo"];
                            DR1["StyleNo"] = dr["StyleNo"];
                            DR1["Description"] = dr["Description"];
                            DR1["Color"] = dr["Color"];
                            DR1["Qty"] = dr["ItemQty"];

                            TotalQty += Convert.ToInt32(dr["ItemQty"]);

                            foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                            {
                                string Size = DRsS["Size"].ToString();
                                string SizeId = DRsS["SizeId"].ToString();

                                DataRow[] RowsQty = dsSizesQty.Tables[0].Select("ProcessEntryId='" + dr["ProcessEntryId"] + "' and RowId='" + dr["RowId"] + "' and SizeId='" + SizeId + "' and SizeDate ='" + dr["ItemDate"] + "' and ChallanNo ='" + dr["ChallanNo"] + "' ");
                                if (RowsQty.Length > 0)
                                {
                                    DR1[Size] = RowsQty[0]["SizeQty"];
                                }
                                else
                                {
                                    DR1[Size] = "0";
                                }
                            }

                            DTS.Rows.Add(DR1);

                        }

                        DataRow DRS2 = DTS.NewRow();
                        DRS2["Color"] = "Total :";
                        DRS2["Qty"] = TotalQty.ToString("f0");
                        DTS.Rows.Add(DRS2);

                        #endregion

                        gvCuttingProcessEntryStyles.DataSource = DTS;
                        gvCuttingProcessEntryStyles.DataBind();

                        TotalQty = 0;

                        #region Damaged

                        DataTable DTS_D = new DataTable();
                        DTS_D.Columns.Add(new DataColumn("ChallanNo"));
                        DTS_D.Columns.Add(new DataColumn("Date"));
                        DTS_D.Columns.Add(new DataColumn("ExcNo"));
                        DTS_D.Columns.Add(new DataColumn("StyleNo"));
                        DTS_D.Columns.Add(new DataColumn("Description"));
                        DTS_D.Columns.Add(new DataColumn("Color"));
                        DTS_D.Columns.Add(new DataColumn("Qty"));


                        foreach (DataRow dr in dsSizes.Tables[0].Rows)
                        {
                            DTS_D.Columns.Add(new DataColumn(dr["Size"].ToString()));
                        }

                        DataSet dsSizesQty_D = objBs.getProcessEntrySizesQtyRecPrint(Convert.ToInt32(ProcessEntryId), "Damaged");

                        DataSet dsStyle_D = objBs.getProcessEntryStyleRecPrint(Convert.ToInt32(ProcessEntryId), "Damaged");
                        foreach (DataRow dr in dsStyle_D.Tables[0].Rows)
                        {
                            DataRow DR1 = DTS_D.NewRow();
                            DR1["ChallanNo"] = dr["ChallanNo"];
                            DR1["Date"] = dr["ItemDate"];
                            DR1["ExcNo"] = dr["ExcNo"];
                            DR1["StyleNo"] = dr["StyleNo"];
                            DR1["Description"] = dr["Description"];
                            DR1["Color"] = dr["Color"];
                            DR1["Qty"] = dr["ItemQty"];

                            TotalQty += Convert.ToInt32(dr["ItemQty"]);

                            foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                            {
                                string Size = DRsS["Size"].ToString();
                                string SizeId = DRsS["SizeId"].ToString();

                                DataRow[] RowsQty = dsSizesQty_D.Tables[0].Select("ProcessEntryId='" + dr["ProcessEntryId"] + "' and RowId='" + dr["RowId"] + "' and SizeId='" + SizeId + "' and SizeDate ='" + dr["ItemDate"] + "' and ChallanNo ='" + dr["ChallanNo"] + "' ");
                                if (RowsQty.Length > 0)
                                {
                                    DR1[Size] = RowsQty[0]["SizeQty"];
                                }
                                else
                                {
                                    DR1[Size] = "0";
                                }
                            }

                            DTS_D.Rows.Add(DR1);

                        }

                        DataRow DRS3 = DTS_D.NewRow();
                        DRS3["Color"] = "Total :";
                        DRS3["Qty"] = TotalQty.ToString("f0");
                        DTS_D.Rows.Add(DRS3);

                        #endregion

                        gvCuttingProcessEntryStylesDmg.DataSource = DTS_D;
                        gvCuttingProcessEntryStylesDmg.DataBind();

                    }

                    DateTime FromDate = DateTime.ParseExact(Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ToDate = DateTime.ParseExact(Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    int TotalDays = Convert.ToInt32((ToDate - FromDate).TotalDays);
                    double PerDayQty = TotalQty / TotalDays;

                    DataTable DTT = new DataTable();
                    DTT.Columns.Add(new DataColumn("Date"));
                    DTT.Columns.Add(new DataColumn("Qty"));

                    for (int i = 1; i <= TotalDays; i++)
                    {
                        DataRow DR1 = DTT.NewRow();
                        DR1["Date"] = Convert.ToDateTime(FromDate.AddDays(i)).ToString("dd/MM/yyyy");
                        DR1["Qty"] = PerDayQty;
                        DTT.Rows.Add(DR1);
                    }

                    gvDailyTarget.DataSource = DTT;
                    gvDailyTarget.DataBind();

                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuttingProcessEntryGrid.aspx");
        }

    }
}