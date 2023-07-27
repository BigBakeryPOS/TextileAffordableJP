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
    public partial class CuttingProcessEntryPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

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

                        lblprint.Text = ds.Tables[0].Rows[0]["Process"].ToString() + " Issued Print";

                        #endregion



                        DataTable DTS = new DataTable();
                        DTS.Columns.Add(new DataColumn("ExcNo"));
                        DTS.Columns.Add(new DataColumn("StyleNo"));
                        DTS.Columns.Add(new DataColumn("Description"));
                        DTS.Columns.Add(new DataColumn("Color"));
                       // DTS.Columns.Add(new DataColumn("Rate"));
                        DTS.Columns.Add(new DataColumn("Qty"));

                        DataSet dsSizes = objBs.getProcessEntrySizesPrint(Convert.ToInt32(ProcessEntryId));
                        foreach (DataRow dr in dsSizes.Tables[0].Rows)
                        {
                            DTS.Columns.Add(new DataColumn(dr["Size"].ToString()));
                        }

                        DataSet dsSizesQty = objBs.getProcessEntrySizesQtyPrint(Convert.ToInt32(ProcessEntryId));

                        DataSet dsStyle = objBs.getProcessEntryStylePrint(Convert.ToInt32(ProcessEntryId));
                        foreach (DataRow dr in dsStyle.Tables[0].Rows)
                        {

                            DataRow DR1 = DTS.NewRow();
                            DR1["ExcNo"] = dr["ExcNo"];
                            DR1["StyleNo"] = dr["StyleNo"];
                            DR1["Description"] = dr["Description"];
                            DR1["Color"] = dr["Color"];
                          //  DR1["Rate"] = dr["Rate"];
                            DR1["Qty"] = dr["TotalIssued"];

                            TotalQty += Convert.ToInt32(dr["TotalIssued"]);

                            foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                            {
                                string Size = DRsS["Size"].ToString();
                                string SizeId = DRsS["SizeId"].ToString();

                                DataRow[] RowsQty = dsSizesQty.Tables[0].Select("ProcessEntryId='" + dr["ProcessEntryId"] + "' and RowId='" + dr["RowId"] + "' and SizeId='" + SizeId + "' ");
                                if (RowsQty.Length > 0)
                                {
                                    DR1[Size] = RowsQty[0]["TotalIssued"];
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

                        gvCuttingProcessEntryStyles.DataSource = DTS;
                        gvCuttingProcessEntryStyles.DataBind();

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
                        DR1["Date"] =Convert.ToDateTime(FromDate.AddDays(i)).ToString("dd/MM/yyyy");
                        DR1["Qty"] = PerDayQty;
                        DTT.Rows.Add(DR1);
                    }

                    gvDailyTarget.DataSource = DTT;
                    gvDailyTarget.DataBind();

                    DataSet dsSketch = objBs.getProcessEntrySketchPrint(Convert.ToInt32(ProcessEntryId));
                    if (dsSketch.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataTable DTBOS = new DataTable();
                        DTBOS.Columns.Add(new DataColumn("Sketch1"));
                        DTBOS.Columns.Add(new DataColumn("Sketch2"));
                        DTBOS.Columns.Add(new DataColumn("Sketch3"));
                        DTBOS.Columns.Add(new DataColumn("Sketch4"));
                        DTBOS.Columns.Add(new DataColumn("Sketch5"));
                        DTBOS.Columns.Add(new DataColumn("Sketch6"));
                        DTBOS.Columns.Add(new DataColumn("Sketch7"));

                        DataRow DR1 = DTBOS.NewRow();

                        gvProcessEntryStylesImages.Columns[0].Visible = false;
                        gvProcessEntryStylesImages.Columns[1].Visible = false;
                        gvProcessEntryStylesImages.Columns[2].Visible = false;
                        gvProcessEntryStylesImages.Columns[3].Visible = false;
                        gvProcessEntryStylesImages.Columns[4].Visible = false;
                        gvProcessEntryStylesImages.Columns[5].Visible = false;
                        gvProcessEntryStylesImages.Columns[6].Visible = false;

                        if (dsSketch.Tables[0].Rows.Count >= 1)
                        {
                            gvProcessEntryStylesImages.Columns[0].Visible = true;
                            DR1["Sketch1"] = dsSketch.Tables[0].Rows[0]["Sketch"].ToString();
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 2)
                        {
                            gvProcessEntryStylesImages.Columns[1].Visible = true;
                            DR1["Sketch2"] = dsSketch.Tables[0].Rows[1]["Sketch"].ToString();
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 3)
                        {
                            gvProcessEntryStylesImages.Columns[2].Visible = true;
                            DR1["Sketch3"] = dsSketch.Tables[0].Rows[2]["Sketch"].ToString();
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 4)
                        {
                            gvProcessEntryStylesImages.Columns[3].Visible = true;
                            DR1["Sketch4"] = dsSketch.Tables[0].Rows[3]["Sketch"].ToString();
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 5)
                        {
                            gvProcessEntryStylesImages.Columns[4].Visible = true;
                            DR1["Sketch5"] = dsSketch.Tables[0].Rows[4]["Sketch"].ToString();
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 6)
                        {
                            gvProcessEntryStylesImages.Columns[5].Visible = true;
                            DR1["Sketch6"] = dsSketch.Tables[0].Rows[5]["Sketch"].ToString();
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 7)
                        {
                            gvProcessEntryStylesImages.Columns[6].Visible = true;
                            DR1["Sketch7"] = dsSketch.Tables[0].Rows[6]["Sketch"].ToString();
                        }

                        DTBOS.Rows.Add(DR1);

                        #endregion

                        gvProcessEntryStylesImages.DataSource = DTBOS;
                        gvProcessEntryStylesImages.DataBind();
                    }
                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuttingProcessEntryGrid.aspx");
        }

    }
}