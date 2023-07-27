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
    public partial class BuyerOrderExportPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
       
        double Qty = 0; double Amount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string POId = Request.QueryString.Get("BuyerOrderExporterSalesId");
                if (POId != null && POId != "")
                {
                    #region PurchaseOrder Print


                    DataSet ds = objBs.BuyerOrderExportPrint(Convert.ToInt32(POId));
                   
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet ds1 = objBs.Getcompanydetails();

                        //DataSet dsCompanyDetails = objBs.GetSelectLedgerDetails(Convert.ToInt32(ds.Tables[0].Rows[0]["buyerid"].ToString()));
                        DataSet dsCompanyDetails = objBs.GetSelectLedgerDetails1(Convert.ToInt32(ds1.Tables[0].Rows[1]["Comapanyid"].ToString()));
                        //lblFCompany.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();
                        lblCoName.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();
                        lblcoaddress.Text = dsCompanyDetails.Tables[0].Rows[0]["Address"].ToString();
                        lblpincode.Text = dsCompanyDetails.Tables[0].Rows[0]["cityname"].ToString() + "-" + dsCompanyDetails.Tables[0].Rows[0]["pincode"].ToString() + "," + "(" + dsCompanyDetails.Tables[0].Rows[0]["statename"].ToString() +")"+ " " + dsCompanyDetails.Tables[0].Rows[0]["countryname"].ToString();
                        lblbankname.Text = dsCompanyDetails.Tables[0].Rows[0]["BankName"].ToString();
                        lblbankaddress.Text = dsCompanyDetails.Tables[0].Rows[0]["BankAddress"].ToString();
                        lblaccountnumer.Text = dsCompanyDetails.Tables[0].Rows[0]["AccountNumber"].ToString();
                        lblswiftcode.Text = dsCompanyDetails.Tables[0].Rows[0]["Swiftcode"].ToString();
                        lbldestination.Text = ds.Tables[0].Rows[0]["Finaldestination1"].ToString();
                        // lblFAddress.Text = dsCompanyDetails.Tables[0].Rows[0]["Address"].ToString();
                        //lblFAreaandPincode.Text = dsCompanyDetails.Tables[0].Rows[0]["Area"].ToString() + " - " + dsCompanyDetails.Tables[0].Rows[0]["Pincode"].ToString();
                        // lblFGST.Text = dsCompanyDetails.Tables[0].Rows[0]["Tin"].ToString();

                        // lblFPhone.Text = dsCompanyDetails.Tables[0].Rows[0]["PhoneNo"].ToString();
                        // lblFMobile.Text = dsCompanyDetails.Tables[0].Rows[0]["MobileNo"].ToString();

                        // lblFax.Text = dsCompanyDetails.Tables[0].Rows[0]["Fax"].ToString();
                        // lblFEmail.Text = dsCompanyDetails.Tables[0].Rows[0]["Email"].ToString();

                        lblcompanyname.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                        lbladdress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        lblCityandPincode.Text = ds.Tables[0].Rows[0]["City"].ToString() + " - " + ds.Tables[0].Rows[0]["Pincode"].ToString();
                        lblArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();

                        lblphoneno.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();

                        lblcompanyname1.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                        lbladdress1.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        lblCityandPincode1.Text = ds.Tables[0].Rows[0]["City"].ToString() + " - " + ds.Tables[0].Rows[0]["Pincode"].ToString();
                        lblArea1.Text = ds.Tables[0].Rows[0]["Area"].ToString();

                        lblphoneno1.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        // lblGST.Text = ds.Tables[0].Rows[0]["GSTIN"].ToString();

                        lblProcessOrderNo.Text = ds.Tables[0].Rows[0]["FullInvoiceNo"].ToString();
                        lblOrderDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["InvoiceDate"]).ToString("dd/MM/yyyy");
                        lblprecarrierby.Text = ds.Tables[0].Rows[0]["precarriageby"].ToString();
                        lblplaceofcarrier.Text = ds.Tables[0].Rows[0]["placeofprecarrier1"].ToString();
                        lblorigin.Text = ds.Tables[0].Rows[0]["originplace"].ToString();
                       // lbldestination.Text = ds.Tables[0].Rows[0]["FinalDestination"].ToString();
                        lblloadingport.Text = ds.Tables[0].Rows[0]["Portofloading1"].ToString();
                        lbldischargeport.Text = ds.Tables[0].Rows[0]["portofdischarge1"].ToString();
                        lbldeliveryplace.Text = ds.Tables[0].Rows[0]["portofdelivery1"].ToString();
                        // lblOrderDateBetween.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy") + "  To  " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");
                        //lblDeliveryPlace.Text = ds.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        //lblProcessOn.Text = ds.Tables[0].Rows[0]["Category"].ToString();
                       // lblbuyerorderno.Text = ds.Tables[0].Rows[0][""]
                        DataSet dsItem = objBs.TransBuyerOrderExportPrint(Convert.ToInt32(POId));
                        gvItemProcessOrder.DataSource = dsItem;
                        gvItemProcessOrder.DataBind();

                    }
                  
                    #endregion

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
                e.Row.Cells[5].Text = "Total :";
                e.Row.Cells[6].Text = Qty.ToString("f2");
                e.Row.Cells[8].Text = Amount.ToString("f2");
            }
        }


       

      
        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderSalesGrid.aspx");
        }

    }
}