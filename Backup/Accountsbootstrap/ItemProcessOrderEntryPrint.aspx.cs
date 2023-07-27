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

namespace Billing.Accountsbootstrap
{
    public partial class ItemProcessOrderEntryPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        double Qty = 0; double Amount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string ItemEntryId = Request.QueryString.Get("ItemEntryId");
                if (ItemEntryId != null && ItemEntryId != "")
                {
                    #region Purchase Print



                    DataSet ds = objBs.ItemProcessOrderEntryPrint(Convert.ToInt32(ItemEntryId));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
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

                        lblProcessOrderNo.Text = ds.Tables[0].Rows[0]["FullPONo"].ToString();
                        lblOrderDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                        lblOrderDateBetween.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy") + "  To  " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");
                        lblDeliveryPlace.Text = ds.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        lblProcessOn.Text = ds.Tables[0].Rows[0]["Category"].ToString();

                        DataSet dsItem = objBs.TransItemProcessOrderEntryPrint(Convert.ToInt32(ItemEntryId));
                        gvItemProcessOrder.DataSource = dsItem;
                        gvItemProcessOrder.DataBind();

                    }


                    #endregion

                    DataSet dsBOSketch = objBs.ItemProcessOrderEntrySketchPrint(Convert.ToInt32(ItemEntryId));
                    if (dsBOSketch.Tables[0].Rows.Count > 0)
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

                        if (dsBOSketch.Tables[0].Rows.Count >= 1)
                        {
                            DR1["Sketch1"] = dsBOSketch.Tables[0].Rows[0]["Sketch"].ToString();
                        }
                        if (dsBOSketch.Tables[0].Rows.Count >= 2)
                        {
                            DR1["Sketch2"] = dsBOSketch.Tables[0].Rows[1]["Sketch"].ToString();
                        }
                        if (dsBOSketch.Tables[0].Rows.Count >= 3)
                        {
                            DR1["Sketch3"] = dsBOSketch.Tables[0].Rows[2]["Sketch"].ToString();
                        }
                        if (dsBOSketch.Tables[0].Rows.Count >= 4)
                        {
                            DR1["Sketch4"] = dsBOSketch.Tables[0].Rows[3]["Sketch"].ToString();
                        }
                        if (dsBOSketch.Tables[0].Rows.Count >= 5)
                        {
                            DR1["Sketch5"] = dsBOSketch.Tables[0].Rows[4]["Sketch"].ToString();
                        }
                        if (dsBOSketch.Tables[0].Rows.Count >= 6)
                        {
                            DR1["Sketch6"] = dsBOSketch.Tables[0].Rows[5]["Sketch"].ToString();
                        }
                        if (dsBOSketch.Tables[0].Rows.Count >= 7)
                        {
                            DR1["Sketch7"] = dsBOSketch.Tables[0].Rows[6]["Sketch"].ToString();
                        }

                        DTBOS.Rows.Add(DR1);

                        #endregion

                        gvBuyerOrderImages.DataSource = DTBOS;
                        gvBuyerOrderImages.DataBind();
                    }
                }
            }
        }

        protected void gvItemProcessOrder_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                Amount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total :";
                e.Row.Cells[6].Text = Qty.ToString("f2");
                e.Row.Cells[8].Text = Amount.ToString("f2");
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ItemProcessOrderEntryGrid.aspx");
        }

    }
}