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
    public partial class Print_Sales_Invoice_Packing : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        public double debitTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string iSalesID = Request.QueryString.Get("iSalesID");
            sTableName = Session["User"].ToString();

            if (iSalesID != null)
            {

                if (sTableName == "CO1")
                {
                    Co1.Visible = true;
                    CO2.Visible = false;
                    Co3.Visible = false;
                }
                else if (sTableName == "CO2")
                {
                    Co1.Visible = false;
                    CO2.Visible = true;
                    Co3.Visible = false;
                }
                else if (sTableName == "CO3")
                {
                    Co1.Visible = false;
                    CO2.Visible = false;
                    Co3.Visible = true;
                }

                DataSet ds1 = new DataSet();
                ////// ds1 = objBs.CustomerSalesGirdgetnew(iSalesID, "tblSales_" + sTableName, sTableName);
                ds1 = objBs.CustomerSalesGirdgetnew();


                DataSet dbranch = new DataSet();
                ////// dbranch = objBs.getbranchlistforprint(sTableName);
                //dbranch = objBs.getbranchlistforprint(sTableName);

                DataSet transsalesdetails = objBs.salesprint("tblSales_" + sTableName, "tblTransSales_" + sTableName, iSalesID);
                if (transsalesdetails.Tables[0].Rows.Count > 0)
                {
                    #region

                    int i = 0;

                    lblCNddame.Text = ds1.Tables[0].Rows[0]["CompanyName"].ToString();
                    lblcompany.Text = ds1.Tables[0].Rows[0]["CompanyName"].ToString();
                    lblCAddress.Text = ds1.Tables[0].Rows[0]["Area"].ToString();
                    lblCAddressnew.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
                    lblCCity.Text = ds1.Tables[0].Rows[0]["City"].ToString();
                    lblCPin.Text = ds1.Tables[0].Rows[0]["Pincode"].ToString();
                    lblCPhoneno.Text = ds1.Tables[0].Rows[0]["PhoneNo"].ToString();
                    lblCmobile.Text = ds1.Tables[0].Rows[0]["MobileNo"].ToString();
                    lblCEmail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();

                    lblcompanygstno.Text = ds1.Tables[0].Rows[0]["tin"].ToString();
                    lblcompanypanno.Text = ds1.Tables[0].Rows[0]["pan"].ToString();

                    lblvechicle.Text = transsalesdetails.Tables[0].Rows[0]["lrno"].ToString();
                    lblpacking.Text = transsalesdetails.Tables[0].Rows[0]["Packing"].ToString();
                    lblcheck.Text = transsalesdetails.Tables[0].Rows[0]["CheckSta"].ToString();
                    lblrecheck.Text = transsalesdetails.Tables[0].Rows[0]["Recheck"].ToString();


                    lblorderno.Text = transsalesdetails.Tables[0].Rows[0]["orderno"].ToString();
                    lblorderdate.Text = Convert.ToDateTime(transsalesdetails.Tables[0].Rows[0]["orderdate"]).ToString("dd/MM/yyyy");
                    lblthrough.Text = transsalesdetails.Tables[0].Rows[0]["through"].ToString();
                   
                    
                   ////// lbltinno.Text = transsalesdetails.Tables[0].Rows[0]["GSTIN"].ToString();
                    lblInvno.Text = transsalesdetails.Tables[0].Rows[0]["FullBillNo"].ToString();
                    lbldate.Text = Convert.ToDateTime(transsalesdetails.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");

                    lblorderdate1.Text = Convert.ToDateTime(transsalesdetails.Tables[0].Rows[0]["orderdate"]).ToString("dd/MM/yyyy");
                    lbltransport.Text = transsalesdetails.Tables[0].Rows[0]["upTransport"].ToString();

                   ////// lblroundoff.Text = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["rff"]).ToString("N");

                    DataSet dscust = objBs.getsalesledger(Convert.ToInt32(transsalesdetails.Tables[0].Rows[0]["CustomerID"].ToString()));

                    lbllLedgerName.Text = dscust.Tables[0].Rows[0]["LedgerName"].ToString();
                    lbllAddress.Text = dscust.Tables[0].Rows[0]["Address"].ToString();
                    lbllPincode.Text = dscust.Tables[0].Rows[0]["Pincode"].ToString();
                    lbllMobileNo.Text = dscust.Tables[0].Rows[0]["MobileNo"].ToString();
                    lbllPhoneNo.Text = dscust.Tables[0].Rows[0]["PhoneNo"].ToString();
                    lblgstin.Text = dscust.Tables[0].Rows[0]["GSTIN"].ToString();
                    if (transsalesdetails.Tables[0].Rows[0]["Billingtype"].ToString() == "S")
                    {
                        lbllLedgerName1.Text = dscust.Tables[0].Rows[0]["LedgerName"].ToString();
                        lbllAddress1.Text = dscust.Tables[0].Rows[0]["Address"].ToString();
                        lbllPincode1.Text = dscust.Tables[0].Rows[0]["Pincode"].ToString();
                        lbllMobileNo1.Text = dscust.Tables[0].Rows[0]["MobileNo"].ToString();
                        lbllPhoneNo1.Text = dscust.Tables[0].Rows[0]["PhoneNo"].ToString();
                    }
                    else
                    {
                        lbllshipping.Text = transsalesdetails.Tables[0].Rows[0]["Shipaddress"].ToString();
                        

                    }


                    lblGrandtotalamt.Text = (Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["NetAmount"]) - Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["Tax1"])).ToString();

                   // lblbilledby.Text = ds1.Tables[0].Rows[0]["BilledBy"].ToString();
                    ////pbilldate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");
                    //  Porderdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["orderDate"]).ToString("dd/MM/yyyy");
                    //  plrno.Text = ds1.Tables[0].Rows[0]["lrno"].ToString();
                    //  plrdated.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["lrDate"]).ToString("dd/MM/yyyy");
                    //  Pdestination.Text = ds1.Tables[0].Rows[0]["destination"].ToString();
                    //  Pnopackage.Text = ds1.Tables[0].Rows[0]["noofpackage"].ToString();
                    //  lbltransport.Text = ds1.Tables[0].Rows[0]["transport"].ToString();
                    //  lblno.Text = ds1.Tables[0].Rows[0]["Billno"].ToString();
                    //  lbldate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");
                    //  lblamount.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Roundoff"]).ToString("N");
                    //  lblamiunt.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Roundoff"]).ToString("N");
                    // lbltransport1.Text = ds1.Tables[0].Rows[0]["transport"].ToString();
                    //lblcompanyname.Text = ds1.Tables[0].Rows[0]["LedgerName"].ToString();
                    //lbltoaddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
                    //lblArea.Text = ds1.Tables[0].Rows[0]["State"].ToString();
                    //lblCity.Text = ds1.Tables[0].Rows[0]["City1"].ToString();
                  //  lblCName2.Text = buyerdetails.Tables[0].Rows[0]["LedgerName"].ToString();

                    
                    //lblPaymode.Text = ds1.Tables[0].Rows[0]["PaymentMode"].ToString();
                    //lblpodate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");
                    ////lblpono.Text = ds1.Tables[0].Rows[0]["BillNo"].ToString();
                    //custin.Text = ds1.Tables[0].Rows[0]["TinNo"].ToString();
                    //lbllorry.Text = ds1.Tables[0].Rows[0]["Transport1"].ToString();
                    //lblGrandtotalamt.Text = ds1.Tables[0].Rows[0]["Total"].ToString();
                    //lblDiscount.Text = ds1.Tables[0].Rows[0]["Discount"].ToString();
                    //lblTax.Text = ds1.Tables[0].Rows[0]["Tax"].ToString();
                    double tte = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["TAX_14"]);
                    lblVAT.Text = tte.ToString("f2");
                    //double tee = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["GrandTotal"]);
                    //lbltotal.Text = tee.ToString("f2");
                    double tee = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["roundoff"]);
                    lblBillAmt.Text = tee.ToString("f2");
                    //TextBox txtitemnames = (TextBox)grvPODetails.Rows[i].Cells[1].FindControl("txtitemname");
                    //TextBox txtpoQtys = (TextBox)grvPODetails.Rows[i].Cells[2].FindControl("txtpoQty");
                    //TextBox txtrateQtys = (TextBox)grvPODetails.Rows[i].Cells[3].FindControl("txtrateQty");
                    //TextBox txtdiss = (TextBox)grvPODetails.Rows[i].Cells[4].FindControl("txtdis");
                    //TextBox txtdisamts = (TextBox)grvPODetails.Rows[i].Cells[5].FindControl("txtdisamt");
                    //TextBox txtamts = (TextBox)grvPODetails.Rows[i].Cells[6].FindControl("txtamt");


                 //   gridprint.DataSource = transsalesdetails;
                 //   gridprint.DataBind();

          

                    //double cgcstpercent = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["Tax"]);
                    double cgcstpercent = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["Tax"])/2;
                   // lblcgstpercent.Text = cgcstpercent.ToString("f2");

                    double cgcstamt = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["CGST"]);
                    lblCGST.Text = cgcstamt.ToString("f2");


                  //  double sgstpercent = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["Tax"]);
                    double sgstpercent = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["Tax"])/2;
                   // lblsgstpercent.Text = sgstpercent.ToString("f2");

                    double sgstamt = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["SGST"]);
                    lblSGST.Text = sgstamt.ToString("f2");


                    double igstpercent = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["Tax"]);
                  //  lbligstpercent.Text = igstpercent.ToString("f2");

                    double igcstamt = Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["IGST"]);
                    lblIGST.Text = igcstamt.ToString("f2");


                    lblTaxAmount.Text = Convert.ToDouble(cgcstamt + sgstamt + igcstamt).ToString("0.00");

                    if (lblIGST.Text == "0.00")
                    {
                       // lbligstpercent.Text = "0";
                        cg.Visible = true;
                        sg.Visible = true;
                        ig.Visible = false;
                        
                    }

                    if (lblSGST.Text == "0.00")
                    {
                        cg.Visible = false;
                        sg.Visible = false;
                        ig.Visible = true;
                      //  lblcgstpercent.Text = "0";
                       // lblsgstpercent.Text = "0";
                    }


                    lblDiscountamt.Text = transsalesdetails.Tables[0].Rows[0]["discamount"].ToString();
                    string diss = transsalesdetails.Tables[0].Rows[0]["Discount"].ToString();
                    if (diss == "0")
                    {
                        dg.Visible = false;
                    }
                    else
                    {
                        dg.Visible = true;
                        lblDis.Text = "Discount@" + transsalesdetails.Tables[0].Rows[0]["Discount"].ToString() + " % ";
                       // lbl
                    }

                    string freight = Convert.ToDouble(Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["Freight"]) + Convert.ToDouble(transsalesdetails.Tables[0].Rows[0]["Loading"])).ToString("0.00");

                    if (freight == "0.00")
                    {
                        fg.Visible = false;
                    }
                    else
                    {
                        fg.Visible = true;
                        lblFreightAmt.Text = freight;

                    }

                    



                  ////////  int salestype = Convert.ToInt32(ds1.Tables[0].Rows[0]["Salestype"].ToString());
                  //////  DataSet dsPODet = objBs.GettrnsSalesDet("tblTransSales_" + sTableName, iSalesID);
                    if (transsalesdetails.Tables[0].Rows.Count > 0)
                    {

                        DataRow dr;
                        DataTable dt;
                        dt = transsalesdetails.Tables[0];
                        for (int ii = dt.Rows.Count; ii < 10; ii++)
                        {
                            dr = dt.NewRow();
                            dt.Rows.Add(dr);
                        }
                        dt.AcceptChanges();

                        gridprint.DataSource = dt;
                        gridprint.DataBind();


                    }

                    #endregion

                    lblamtinwords.InnerText = objBs.changeToWords(lblBillAmt.Text, true);
                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("salesgrid.aspx");
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ////e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                Label lblamt = (Label)e.Row.FindControl("lblamt");
                if (lblamt != null && lblamt.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblamt.Text);
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }

           ////// lblGrandtotalamt.Text = debitTotal.ToString("f2");
            lblTaxableValue.Text = Convert.ToDouble(Convert.ToDouble(debitTotal) + Convert.ToDouble(lblFreightAmt.Text)).ToString("0.00");
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {

        }
    }
}