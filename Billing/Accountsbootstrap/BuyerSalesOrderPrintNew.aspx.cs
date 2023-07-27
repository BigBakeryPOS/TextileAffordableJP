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
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Billing.Accountsbootstrap
{
    public partial class BuyerSalesOrderPrintNew : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
       
        double Qty = 0; double Amount = 0;double TotalAmount=0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string POId = Request.QueryString.Get("BuyerOrderSalesId");
                if (POId != null && POId != "")
                {
                    #region PurchaseOrder Print
                    DataSet ds2 = objBs.BuyerSalesOrderPrintnew(Convert.ToInt32(POId));
                    DataSet ds=new DataSet();
                    DataSet ds3 = new DataSet();
                    if (ds2.Tables[0].Rows.Count>0)
                    {
                        if (ds2.Tables[0].Rows[0]["BuyerorderMasterCuttingId"].ToString() == "0")
                        {
                            ds = objBs.BuyerSalesOrderPrintDirect(Convert.ToInt32(POId));
                            ds3= objBs.BuyerSalesOrderPrintDirectdespatch(Convert.ToInt32(POId));
                        }
                        else
                        {

                            ds = objBs.BuyerSalesOrderPrint(Convert.ToInt32(POId));
                        }

                    }

                   
                    
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       
                        //DataSet ds1 = objBs.Getcompanydetails();

                        //DataSet dsCompanyDetails = objBs.GetSelectLedgerDetails(Convert.ToInt32(ds.Tables[0].Rows[0]["buyerid"].ToString()));
                        DataSet dsCompanyDetails = objBs.GetSelectLedgerDetails(Convert.ToInt32(ds.Tables[0].Rows[0]["Companyid"].ToString()));
                        lblFCompany.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();
                        //lblCoName.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();

                        lblFAddress.Text = dsCompanyDetails.Tables[0].Rows[0]["Address"].ToString();
                        lblFAreaandPincode.Text = dsCompanyDetails.Tables[0].Rows[0]["Area"].ToString() + " - " + dsCompanyDetails.Tables[0].Rows[0]["Pincode"].ToString();
                        lblFGST.Text = dsCompanyDetails.Tables[0].Rows[0]["Tin"].ToString();

                        lblFPhone.Text = dsCompanyDetails.Tables[0].Rows[0]["PhoneNo"].ToString();
                        lblFMobile.Text = dsCompanyDetails.Tables[0].Rows[0]["MobileNo"].ToString();

                        //lblFax.Text = dsCompanyDetails.Tables[0].Rows[0]["Fax"].ToString();
                        lblFEmail.Text = dsCompanyDetails.Tables[0].Rows[0]["Email"].ToString();

                        lblcompanyname.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                        lbladdress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        lblCityandPincode.Text = ds.Tables[0].Rows[0]["City"].ToString() + " - " + ds.Tables[0].Rows[0]["Pincode"].ToString();
                        lblArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();

                        lblphoneno.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        lblGST.Text = ds.Tables[0].Rows[0]["GSTINNo"].ToString();

                        lblProcessOrderNo.Text = ds.Tables[0].Rows[0]["FullInvoiceNo"].ToString();
                        lblOrderDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["InvoiceDate"]).ToString("dd/MM/yyyy");
                        imglogo.ImageUrl= dsCompanyDetails.Tables[0].Rows[0]["Imagepath"].ToString();
                        lbltermspayment.Text = ds.Tables[0].Rows[0]["payment_mode"].ToString();
                        lblBrBank.Text = dsCompanyDetails.Tables[0].Rows[0]["BankName"].ToString();
                        lblBrBranch.Text = dsCompanyDetails.Tables[0].Rows[0]["BankAddress"].ToString();
                        lblBrAccNo.Text = dsCompanyDetails.Tables[0].Rows[0]["AccountNumber"].ToString();
                        lblBrIFSC.Text = dsCompanyDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                        if (ds.Tables[0].Rows[0]["Isdespatch"].ToString() == "N")
                        {

                            lblDespatchedBy.Text = "";
                            lbldespatchthrogh.Text = "";
                            //if (ds.Tables[0].Rows[0]["LRDate"].ToString() != "")
                            //{
                                lbltermsofdelivery.Text = "";
                           // }
                        }
                        if (ds.Tables[0].Rows[0]["Isdespatch"].ToString() == "Y")
                        {
                            lblDespatchedBy.Text = ds3.Tables[0].Rows[0]["LRNo"].ToString();
                            lbldespatchthrogh.Text = ds3.Tables[0].Rows[0]["Transport"].ToString();
                            if (ds3.Tables[0].Rows[0]["LRDate"].ToString() != "")
                            {
                                lbltermsofdelivery.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["LRDate"]).ToString("dd/MM/yyyy"); ;
                            }
                        }
                        // lblOrderDateBetween.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy") + "  To  " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");
                        //lblDeliveryPlace.Text = ds.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        //lblProcessOn.Text = ds.Tables[0].Rows[0]["Category"].ToString();

                        DataSet dsItem = objBs.TransBuyerSalesOrderPrint(Convert.ToInt32(POId));
                        gvItemProcessOrder.DataSource = dsItem;
                        gvItemProcessOrder.DataBind();
                      //  double tee = Convert.ToDouble(ds.Tables[0].Rows[0]["roundoff"]);
                        //lblRoundOff.Text = tee.ToString("f2");
                        //lblAmountinwords.Text = objBs.changeToWords(Convert.ToDouble(tee).ToString("f2"), true);// "INR " + objBs.changeToWords(Convert.ToDouble(tee).ToString("f2"), true);


                        #region GSTGrid
                        double cgsttot = 0;
                        double sgsttot = 0;
                        double igsttot = 0;
                        double gsttot = 0;
                        double ratetot = 0;

                        DataTable dtttg;
                        DataRow drNewg;
                        DataColumn dctg;
                        DataSet dstdg = new DataSet();
                        dtttg = new DataTable();

                        dctg = new DataColumn("HSNCode");
                        dtttg.Columns.Add(dctg);

                        dctg = new DataColumn("Rate");
                        dtttg.Columns.Add(dctg);

                        dctg = new DataColumn("CGSTRate");
                        dtttg.Columns.Add(dctg);

                        dctg = new DataColumn("CGSTAmount");
                        dtttg.Columns.Add(dctg);

                        dctg = new DataColumn("SGSTRate");
                        dtttg.Columns.Add(dctg);

                        dctg = new DataColumn("SGSTAmount");
                        dtttg.Columns.Add(dctg);

                        dctg = new DataColumn("IGSTRate");
                        dtttg.Columns.Add(dctg);

                        dctg = new DataColumn("IGSTAmount");
                        dtttg.Columns.Add(dctg);

                        dctg = new DataColumn("Amount");
                        dtttg.Columns.Add(dctg);

                        dstdg.Tables.Add(dtttg);


                        DataSet dsOrGSTHSN = null;
                        dsOrGSTHSN = objBs.GetSalesOrderGSTHSNCode(Convert.ToInt32(POId), ds.Tables[0].Rows[0]["province"].ToString(),ds.Tables[0].Rows[0]["GSTType"].ToString());

                        if (dsOrGSTHSN.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsOrGSTHSN.Tables[0].Rows.Count; j++)
                            {
                                drNewg = dtttg.NewRow();
                                //drNewg["HSNCode"] = "<p>" + dsOrGSTHSN.Tables[0].Rows[j]["HSNSAC"].ToString() + "</p>";
                                //drNewg["Rate"] = "<p>" + "INR " + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["Pricewithouttax"]).ToString("N2") + "</p>";
                                //ratetot = ratetot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["Pricewithouttax"]);
                                //drNewg["CGSTRate"] = "<p>" + dsOrGSTHSN.Tables[0].Rows[j]["CGSTPer"].ToString() + "</p>";
                                //drNewg["CGSTAmount"] = "<p>" + "INR " + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["CGSTAmount"]).ToString("N2") + "</p>";
                                //cgsttot = cgsttot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["CGSTAmount"]);
                                //drNewg["SGSTRate"] = "<p>" + dsOrGSTHSN.Tables[0].Rows[j]["SGSTPer"].ToString() + "</p>";
                                //drNewg["SGSTAmount"] = "<p>" + "INR " + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["SGSTAmount"]).ToString("N2") + "</p>";
                                //sgsttot = sgsttot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["SGSTAmount"]);
                                //drNewg["Amount"] = "<p>" + "INR " + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["TotalAmount"]).ToString("N2") + "</p>";
                                //gsttot = gsttot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["TotalAmount"]);
                                drNewg["HSNCode"] = dsOrGSTHSN.Tables[0].Rows[j]["HSNSAC"].ToString();
                                drNewg["Rate"] = Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["Pricewithouttax"]).ToString("N2");
                                ratetot = ratetot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["Pricewithouttax"]);
                                drNewg["CGSTRate"] = dsOrGSTHSN.Tables[0].Rows[j]["CGSTPer"].ToString();
                                drNewg["CGSTAmount"] = Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["CGSTAmount"]).ToString("N2");
                                cgsttot = cgsttot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["CGSTAmount"]);
                                drNewg["SGSTRate"] = dsOrGSTHSN.Tables[0].Rows[j]["SGSTPer"].ToString();
                                drNewg["SGSTAmount"] = Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["SGSTAmount"]).ToString("N2");
                                sgsttot = sgsttot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["SGSTAmount"]);
                                drNewg["IGSTRate"] = dsOrGSTHSN.Tables[0].Rows[j]["IGSTPer"].ToString();
                                drNewg["IGSTAmount"] = Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["IGSTAmount"]).ToString("N2");
                                igsttot = igsttot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["IGSTAmount"]);
                                drNewg["Amount"] = Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["TotalAmount"]).ToString("N2");
                                gsttot = gsttot + Convert.ToDouble(dsOrGSTHSN.Tables[0].Rows[j]["TotalAmount"]);
                                dstdg.Tables[0].Rows.Add(drNewg);
                            }
                        }

                        //drNewg = dtttg.NewRow();
                        //drNewg["HSNCode"] = "<p>" + "Total" + "</p>";
                        //drNewg["Rate"] = "<p>" + "INR " + Convert.ToDouble(ratetot).ToString("f2") + " </p>";
                        //drNewg["CGSTRate"] = "";
                        //drNewg["CGSTAmount"] = "<p>" + "INR " + Convert.ToDouble(cgsttot).ToString("f2") + " </p>";
                        //drNewg["SGSTRate"] = "";
                        //drNewg["SGSTAmount"] = "<p>" + "INR " + Convert.ToDouble(sgsttot).ToString("f2") + " </p>";
                        //drNewg["Amount"] = "<p>" + "INR " + Convert.ToDouble(gsttot).ToString("f2") + " </p>";
                        //drNewg["Rate"] = Convert.ToDouble(ratetot).ToString("f2");
                        //drNewg["CGSTRate"] = "";
                        //drNewg["CGSTAmount"] = Convert.ToDouble(cgsttot).ToString("f2");
                        //drNewg["SGSTRate"] = "";
                        //drNewg["SGSTAmount"] = Convert.ToDouble(sgsttot).ToString("f2");
                        //drNewg["Amount"] = Convert.ToDouble(gsttot).ToString("f2");
                        lblTaxAmountinwords.Text = objBs.changeToWords(Convert.ToDouble(gsttot).ToString("f2"), true);// "INR " + objBs.changeToWords(Convert.ToDouble(gsttot).ToString("f2"), true);
                                                                                                                      // dstdg.Tables[0].Rows.Add(drNewg);

                        DataRow drg;
                        DataTable dtg;
                        dtg = dstdg.Tables[0];
                        gvGST.DataSource = dtg;
                        gvGST.DataBind();
                        gvGST.Visible = true;
                        #endregion

                    }

                    #endregion

                }
            }
        }
        double HSNRate = 0;
        double HSNCGSTAmount = 0;
        double HSNSGSTAmount = 0;
        double HSNIGSTAmount = 0;
        double HSNAmount = 0;
        protected void gvGST_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((DataBinder.Eval(e.Row.DataItem, "Rate")).ToString() != "")
                {
                    HSNRate += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                }

                if ((DataBinder.Eval(e.Row.DataItem, "CGSTAmount")).ToString() != "")
                {
                    HSNCGSTAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CGSTAmount"));
                }

                if ((DataBinder.Eval(e.Row.DataItem, "SGSTAmount")).ToString() != "")
                {
                    HSNSGSTAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SGSTAmount"));
                }
                if ((DataBinder.Eval(e.Row.DataItem, "IGSTAmount")).ToString() != "")
                {
                    HSNIGSTAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "IGSTAmount"));
                }
                if ((DataBinder.Eval(e.Row.DataItem, "Amount")).ToString() != "")
                {
                    HSNAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = HSNRate.ToString("f2");
                e.Row.Cells[3].Text = HSNCGSTAmount.ToString("f2");
                e.Row.Cells[5].Text = HSNSGSTAmount.ToString("f2");
                e.Row.Cells[7].Text = HSNIGSTAmount.ToString("f2");
                e.Row.Cells[8].Text = HSNAmount.ToString("f2");
            }
        }
        protected void gvItemProcessOrder_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                Amount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                TotalAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total :";
                e.Row.Cells[5].Text = Qty.ToString("f2");
                e.Row.Cells[6].Text = Amount.ToString("f2");
                e.Row.Cells[10].Text = Math.Round(TotalAmount).ToString("f2");
                lblAmountinwords.Text = objBs.changeToWords(Convert.ToDouble(Math.Round(TotalAmount)).ToString("f2"), true);// "INR " + objBs.changeToWords(Convert.ToDouble(tee).ToString("f2"), true);
            }
            //double Cash = Convert.ToDouble(txtCashAmount.Text);
            //double Total = Convert.ToDouble(Amount);
            //double Gtotal = Total - Cash;
            //txtGrandTotal.Text = Convert.ToDouble(Gtotal).ToString("N2");
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderSalesGrid.aspx");
        }

    }
}