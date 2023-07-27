using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class cashsales : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iDealer = "";
        string baseUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            //btnadd.Attributes.Add("onclick", "return valchk();"); // Validation Check lines
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {

                DataSet dsSalesInv = objBs.getSalesInv();
                if (dsSalesInv.Tables[0].Rows.Count > 0)
                {
                    lblprefix.Text = dsSalesInv.Tables[0].Rows[0]["PreFix"].ToString();
                    lblsufix.Text = dsSalesInv.Tables[0].Rows[0]["Sufix"].ToString();
                }



                btnPrint.Visible = false;
                btnDelete.Visible = false;
                txtbillno.Enabled = true;

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                //DataSet ds = objBs.SalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text);
                //   DataSet ds = objBs.getSalesBillno(Convert.ToInt32(ddlPayMode.SelectedValue), btnadd.Text, "Sales", sTableName);
                //   if (ds.Tables[0].Rows.Count > 0)
                {





                    FirstGridViewRow();
                    txtvoudate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtlrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtduedate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtorderdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtdate1.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    DataSet dsCategory = new DataSet();

                    ddlBank.Enabled = false;
                    txtCheque.Enabled = false;
                    DataSet dagent = objBs.Getcustomerss(sTableName);
                    if (dagent.Tables[0].Rows.Count > 0)
                    {

                        ddlcustomerID.DataSource = dagent.Tables[0];
                        ddlcustomerID.DataTextField = "LEdgerName";
                        ddlcustomerID.DataValueField = "LedgerID";
                        ddlcustomerID.DataBind();
                        ddlcustomerID.Items.Insert(0, "Select Customer");
                    }



                    dagent = objBs.getagentlist(sTableName);
                    if (dagent.Tables[0].Rows.Count > 0)
                    {

                        ddlRepname.DataSource = dagent.Tables[0];
                        ddlRepname.DataTextField = "LEdgerName";
                        ddlRepname.DataValueField = "LedgerID";
                        ddlRepname.DataBind();
                        ddlRepname.Items.Insert(0, "Select Agent");
                    }

                    string iSalesID = Request.QueryString.Get("iSalesID");



                    iDealer = Request.QueryString.Get("iDealer");
                    if (iSalesID != null)
                    {

                        DataSet dContact = objBs.checkContack(Convert.ToInt32(iSalesID), Convert.ToInt32(lblUserID.Text), "tblsales_" + sTableName);


                        DataSet ds1 = objBs.CustomerSalesGirdgetNew(iSalesID, "tblSales_" + sTableName);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            //bblbillto.SelectedValue = ds1.Tables[0].Rows[0]["ContactTypeID"].ToString();
                            DataSet dsCustomer = objBs.Getcustomerss(sTableName);
                            if (dsCustomer.Tables[0].Rows.Count > 0)
                            {
                                ddlcustomerID.DataSource = dsCustomer.Tables[0];
                                ddlcustomerID.DataTextField = "LedgerName";
                                ddlcustomerID.DataValueField = "LedgerID";
                                ddlcustomerID.DataBind();
                                ddlcustomerID.Items.Insert(0, "Select Customer");

                                // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();

                            }
                            btnadd.Text = "Update";

                            ddlbook.Enabled = false;


                            btnPrint.Visible = true;


                            btnDelete.Visible = true;
                            txtbillno.Enabled = false;


                            //txtcuscode.Text = ds1.Tables[0].Rows[0]["CustomerID"].ToString();
                            txtbillno.Text = ds1.Tables[0].Rows[0]["BillNo"].ToString();
                            txtdate1.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");
                            //ddlcustomerID.SelectedItem.Text = ds1.Tables[0].Rows[0]["CustomerName"].ToString();
                            ddlcustomerID.SelectedValue = ds1.Tables[0].Rows[0]["CustomerID1"].ToString();

                            DataSet dsCustDet = objBs.GetCustomerDetailsforsales((ddlcustomerID.SelectedValue), sTableName);
                            string area = string.Empty;

                            if (dsCustDet.Tables[0].Rows.Count > 0)
                            {

                                int agent = Convert.ToInt32(dsCustDet.Tables[0].Rows[0]["Agentid"]);


                                DataSet dagent1 = objBs.getcustomeragent(agent, sTableName);
                                if (dagent.Tables[0].Rows.Count > 0)
                                {

                                    ddlRepname.DataSource = dagent1.Tables[0];
                                    ddlRepname.DataTextField = "LEdgerName";
                                    ddlRepname.DataValueField = "LedgerID";
                                    ddlRepname.DataBind();

                                }
                                ddlRepname.SelectedValue = dsCustDet.Tables[0].Rows[0]["Agentid"].ToString();



                                txtaddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
                                area = dsCustDet.Tables[0].Rows[0]["area"].ToString();
                                txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                                txtpincode.Text = ds1.Tables[0].Rows[0]["pincode"].ToString();

                                ddlbook.SelectedValue = ds1.Tables[0].Rows[0]["Book"].ToString();

                                if (ds1.Tables[0].Rows[0]["Book"].ToString() == "2")
                                {
                                    kl.Visible = true;
                                    exiscust.Visible = false;
                                    txtmobileno.Text = ds1.Tables[0].Rows[0]["MobileNo1"].ToString();
                                }
                                else
                                {
                                    kl.Visible = false;
                                    exiscust.Visible = true;
                                    txtmobileno.Text = ds1.Tables[0].Rows[0]["MobileNo"].ToString();
                                }
                                txtvouno.Text = ds1.Tables[0].Rows[0]["vouno"].ToString();
                                TextBox2.Text = ds1.Tables[0].Rows[0]["name"].ToString();

                                txtvoudate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");
                                ddlvouchertype.SelectedValue = ds1.Tables[0].Rows[0]["vouchertype"].ToString();

                                ddlProvince.SelectedValue = ds1.Tables[0].Rows[0]["province"].ToString();
                                ddlCash.SelectedValue = ds1.Tables[0].Rows[0]["cashac"].ToString();
                                txtlrno.Text = ds1.Tables[0].Rows[0]["lrno"].ToString();
                                txtTransport.Text = ds1.Tables[0].Rows[0]["transport"].ToString();
                                txtdestination.Text = ds1.Tables[0].Rows[0]["destination"].ToString();
                                txtorderno.Text = ds1.Tables[0].Rows[0]["orderno"].ToString();
                                txtbilledby.Text = ds1.Tables[0].Rows[0]["BilledBy"].ToString();
                                ddlRepname.SelectedValue = ds1.Tables[0].Rows[0]["repname"].ToString();
                                txtpackingslip.Text = ds1.Tables[0].Rows[0]["packingno"].ToString();
                                //txtDisc.Text = ds1.Tables[0].Rows[0]["discper"].ToString();
                                txtlrdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["lrdate"]).ToString("dd/MM/yyyy");
                                txtduedate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["duedate"]).ToString("dd/MM/yyyy");
                                txtorderdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["orderdate"]).ToString("dd/MM/yyyy");
                                txtnopackage.Text = ds1.Tables[0].Rows[0]["noofpackage"].ToString();
                                txtTransport.Text = ds1.Tables[0].Rows[0]["Transport"].ToString();
                                txtFreight.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Freight"]).ToString("N");
                                txtLU.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Loading"]).ToString("N");
                                txtroundoff.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Roundoff"]).ToString("N");


                                lblarea.Text = "";
                                txtlrnoupd.Text = ds1.Tables[0].Rows[0]["UpLrno"].ToString();
                                ttxlorryno.Text = ds1.Tables[0].Rows[0]["UpLorryNo"].ToString();
                                txttransportupd.Text = ds1.Tables[0].Rows[0]["UpTransport"].ToString();
                                txtthrough.Text = ds1.Tables[0].Rows[0]["through"].ToString();

                                txtpack.Text = ds1.Tables[0].Rows[0]["Packing"].ToString();
                                txtchecking.Text = ds1.Tables[0].Rows[0]["CheckSta"].ToString();
                                txtrecheck.Text = ds1.Tables[0].Rows[0]["Recheck"].ToString();


                                chkcertificate.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["Certificate"]);


                                txtcgst.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["CGST"]).ToString("N");
                                txtsgst.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["SGST"]).ToString("N");
                                txtigst.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["IGST"]).ToString("N");


                                txtNarration.Text = ds1.Tables[0].Rows[0]["Narration"].ToString();
                                txtCheque.Text = ds1.Tables[0].Rows[0]["ChequeNo"].ToString();
                                ddlBank.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["Bank"]).ToString();

                                ddlPayMode.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["Paymode"]).ToString();

                                if (ddlPayMode.SelectedValue == "1")
                                {
                                    txtadd.Text = ds1.Tables[0].Rows[0]["CashAddress"].ToString();
                                }
                                else
                                {
                                    txtadd.Text = txtaddress.Text + " ," + txtcity.Text + " ," + area + ", " + txtmobileno.Text + " , " + txtpincode.Text;
                                }


                                ddlcustomerID.Focus();


                            }
                            //Retreive Sales Trans Details
                            DataSet ds2 = objBs.GetUpdateSalesTrans(iSalesID, "tblTransSales_" + sTableName);
                            {
                                if (ds2.Tables[0].Rows.Count > 0)
                                {
                                    int Tpo = ds2.Tables[0].Rows.Count;


                                    DataTable dttt;
                                    DataRow drNew;
                                    DataColumn dct;
                                    DataSet dstd = new DataSet();
                                    dttt = new DataTable();

                                    dct = new DataColumn("OrderNo");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Product");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Stock");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Qty");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Rate");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Discount");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Tax");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Amount");
                                    dttt.Columns.Add(dct);


                                    dct = new DataColumn("Description");
                                    dttt.Columns.Add(dct);



                                    dstd.Tables.Add(dttt);

                                    string dd1 = "0";
                                    foreach (DataRow dr in ds2.Tables[0].Rows)
                                    {

                                        drNew = dttt.NewRow();
                                        drNew["Qty"] = dr["Quantity"];

                                        drNew["OrderNo"] = dr["orderno"];
                                        drNew["Rate"] = dr["UnitPrice"];
                                        drNew["Tax"] = dr["Tax"];
                                        drNew["Amount"] = dr["Amount"];

                                        drNew["Product"] = dr["SubCategoryID"];
                                        drNew["Discount"] = dr["Disc"];
                                        drNew["Description"] = dr["Description"];

                                        dstd.Tables[0].Rows.Add(drNew);
                                    }

                                    ViewState["CurrentTable1"] = dttt;

                                    gvcustomerorder.DataSource = dstd;
                                    gvcustomerorder.DataBind();


                                    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                                    {

                                        DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");




                                        TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                                        TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");


                                        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                                        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                                        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                                        //TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                                        TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                                        TextBox txtDesc = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDesc");



                                        txtkttt.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Amount"]).ToString("N");



                                        txtttk.Text = dstd.Tables[0].Rows[vLoop]["qty"].ToString();
                                        txtktttt.Text = dstd.Tables[0].Rows[vLoop]["Discount"].ToString();
                                        txttk.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Rate"]).ToString("N");
                                        txtkt.Text = dstd.Tables[0].Rows[vLoop]["Tax"].ToString();

                                        txt.SelectedValue = dstd.Tables[0].Rows[vLoop]["Product"].ToString();

                                        txtDesc.Text = dstd.Tables[0].Rows[vLoop]["Description"].ToString();

                                        txtno.Text = dstd.Tables[0].Rows[vLoop]["Orderno"].ToString();
                                        txtno.Focus();
                                    }

                                    txtdiscount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["DiscAmt"]).ToString("N");
                                    //txtTaxamt5.Text = ds2.Tables[0].Rows[0]["TAX_5"].ToString();
                                    txtTaxamt.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["TAX_14"]).ToString("N");
                                    txtgrandtotal.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["GrandTotal"]).ToString("N");
                                    totqty.Text = ds2.Tables[0].Rows[0]["totalqty"].ToString();
                                    totmeter.Text = ds2.Tables[0].Rows[0]["totalmeter"].ToString();

                                }


                            }
                            orderno(sender, e);

                            ButtonAdd1_Click(sender, e);
                        }
                        else
                        {
                            DataSet ds12 = objBs.SalesBillno("tblSales_" + sTableName);
                            if (ds12.Tables[0].Rows.Count > 0)
                            {
                                // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                                if (ds12.Tables[0].Rows[0]["billno"].ToString() == "")
                                {
                                    txtbillno.Text = "1";
                                    txtpackingslip.Text = "1";
                                }
                                else
                                {
                                    txtbillno.Text = ds12.Tables[0].Rows[0]["billno"].ToString();
                                    txtpackingslip.Text = ds12.Tables[0].Rows[0]["billno"].ToString();

                                    //  txtdate1.Text = DateTime.Today.ToString("dd/MM/yyyy");

                                    btnadd.Text = "Save";
                                }
                            }
                        }
                    }
                    else
                    {
                        DataSet ds12 = objBs.SalesBillno("tblSales_" + sTableName);
                        if (ds12.Tables[0].Rows.Count > 0)
                        {
                            // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                            if (ds12.Tables[0].Rows[0]["billno"].ToString() == "")
                            {
                                txtbillno.Text = "1";
                                txtpackingslip.Text = "1";
                            }
                            else
                            {
                                txtbillno.Text = ds12.Tables[0].Rows[0]["billno"].ToString();
                                txtpackingslip.Text = ds12.Tables[0].Rows[0]["billno"].ToString();

                                //  txtdate1.Text = DateTime.Today.ToString("dd/MM/yyyy");

                                btnadd.Text = "Save";
                            }
                        }
                        kl.Visible = false;
                    }

                    // }
                }

                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
            ddlbook.Focus();

        }

        protected void drpPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rbtype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtype.SelectedValue == "1")
            {
                Div22.Visible = false;
                Div23.Visible = false;
            }
            else
            {
                Div22.Visible = true;
                Div23.Visible = true;
            }
        }

        protected void chkshipaddr_CheckedChanged(Object sender, EventArgs args)
        {
            if (chkshipaddr.Checked == true)
            {
                txtShipaddress.Text = txtadd.Text;
            }
            else
            {
                txtShipaddress.Text = "";
            }
        }

        protected void gridbutton_click(object sender, EventArgs e)
        {
            Response.Redirect("salesgrid.aspx");
        }

        //protected void Refbutton_click(object sender, EventArgs e)
        //{
        //    string url = "http://www.bigdbiz.com";
        //    string s = "window.open('" + url + "', 'popup_window', 'width=900,height=500,resizable=yes');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        //}

        protected void newcstomer_click(object sender, EventArgs e)
        {
            Response.Redirect("customermaster.aspx");
        }

        protected void TextBox9_TextChanged(object sender, EventArgs e)
        {
        }
        protected void bblbillto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet dsCustomer = objBs.GetCustName((bblbillto.SelectedValue));
            //if (bblbillto.SelectedValue == "1")
            //{
            //    if (chknew.Checked == true)
            //    {
            //        txtCustname.Visible = true;
            //        ddlcustomerID.Visible = false;
            //        txtcustomername.Text = "";
            //    }
            //    else
            //    {
            //        txtCustname.Visible = false;
            //        ddlcustomerID.Visible = true;
            //        txtcustomername.Text = "";
            //    }
            //    txtaddress.Text = "";
            //    txtcity.Text = "";
            //    //txtarea.Text = "";
            //    txtpincode.Text = "";
            //    txtcuscode.Text = "";
            //    ////advance.Visible = true;
            //    //tax.Visible = false;
            //}
            //else
            //{
            //    if (chknew.Checked == true)
            //    {
            //        txtCustname.Visible = true;
            //        ddlcustomerID.Visible = false;
            //        txtcustomername.Text = "";
            //    }
            //    else
            //    {
            //        txtCustname.Visible = false;
            //        ddlcustomerID.Visible = true;
            //        txtcustomername.Text = "";
            //    }
            //    //advance.Visible = false;

            //    if (dsCustomer.Tables[0].Rows.Count > 0)
            //    {
            //        ddlcustomerID.DataSource = dsCustomer.Tables[0];
            //        ddlcustomerID.DataTextField = "CustomerName";
            //        ddlcustomerID.DataValueField = "CustomerID";
            //        ddlcustomerID.DataBind();
            //        ddlcustomerID.Items.Insert(0, "Select Dealer");

            //        // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();

            //    }
            //}
        }
        protected void txtbillcheck(object sender, EventArgs e)
        {
            //DataSet dss = new DataSet();
            //if (ddlbook.SelectedItem.Value == "1")
            //{
            //    ddlvouchertype.SelectedValue = "2";

            //}
            //else
            //{
            //    ddlvouchertype.SelectedValue = "1";

            //}
            //string iSalesID = Request.QueryString.Get("iSalesID");
            //if (btnadd.Text == "Save")
            //{

            //    dss = objBs.checkfortempSalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text, txtbillno.Text, "");
            //}
            //else
            //{
            //    dss = objBs.checkfortempSalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text, txtbillno.Text, iSalesID);
            //}
            //if (dss.Tables[0].Rows.Count > 0)
            //{
            //    txtbillno.Focus();
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Billno Number Already Exists.');", true);
            //    return;

            //}
            //else
            //{
            //    txtvoudate.Focus();
            //}
        }

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
            //{

            //    ddlBank.Enabled = false;
            //    txtCheque.Enabled = false;
            //}
            //else if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 2) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 4) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 5))
            //{
            //    ddlBank.Enabled = true;
            //    txtCheque.Enabled = true;

            //}


            ddlBank.Enabled = false;
            txtCheque.Enabled = false;

            if (ddlbook.SelectedItem.Value == "1")
            {
                ddlvouchertype.SelectedValue = "2";
                ddlCash.SelectedValue = "2";
                ddlPayMode.SelectedValue = "3";
                exiscust.Visible = true;
                kl.Visible = false;
                //  DataSet ds = objBs.SalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text);
                //DataSet ds = objBs.getSalesBillno(Convert.ToInt32(ddlPayMode.SelectedValue), btnadd.Text, "Sales");
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                //    if (ds.Tables[0].Rows[0]["BillNo"].ToString() == "")
                //    {
                //        txtbillno.Text = "1";
                //        txtpackingslip.Text = "1";
                //    }
                //    else
                //    {
                //        txtbillno.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                //        txtpackingslip.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();

                //        //  txtdate1.Text = DateTime.Today.ToString("dd/MM/yyyy");

                //        //btnadd.Text = "Save";
                //    }
                //}


            }
            else
            {
                ddlvouchertype.SelectedValue = "1";
                ddlCash.SelectedValue = "1";
                ddlPayMode.SelectedValue = "1";
                //  exiscust.Visible = false;
                // ddlcustomerID.SelectedValue = "1";
                kl.Visible = false;
                //   DataSet ds = objBs.SalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text);
                //DataSet ds = objBs.getSalesBillno(Convert.ToInt32(ddlPayMode.SelectedValue), btnadd.Text, "Sales", sTableName);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                //    if (ds.Tables[0].Rows[0]["BillNo"].ToString() == "")
                //    {
                //        txtbillno.Text = "1";
                //        txtpackingslip.Text = "1";
                //    }
                //    else
                //    {
                //        txtbillno.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                //        txtpackingslip.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();

                //        //  txtdate1.Text = DateTime.Today.ToString("dd/MM/yyyy");

                //        //btnadd.Text = "Save";
                //    }
                //}


            }
            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
            txtbillcheck(sender, e);

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            //}
        }
        protected void ddlrep_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet dsCustDet = objBs.GetCustomerDetailsagent(Convert.ToInt32(ddlRepname.SelectedValue),sTableName);
            //if (dsCustDet.Tables[0].Rows.Count > 0)
            //{
            //    ddlcustomerID.DataSource = dsCustDet.Tables[0];
            //    ddlcustomerID.DataTextField = "LedgerName";
            //    ddlcustomerID.DataValueField = "LedgerID";
            //    ddlcustomerID.DataBind();
            //    ddlcustomerID.Items.Insert(0, "Select Customer");
            //}

        }
        protected void gvcustomerorderchanged(object sender, EventArgs e)
        {
            //Get the selected row
            GridViewRow row = gvcustomerorder.SelectedRow;
            if (row != null)
            {
                //First find the control in template column and then get the value
                //Change the cell index(1) of column as per your design
                // Label2.Text = (row.FindControl("lblLocalTime") as Label).Text;
                //  DropDownList drop = (row.FindControl("lblLocalTime") as DropDownList).Text;
            }
        }

        protected void ddlcustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string area = string.Empty;
            if (ddlcustomerID.SelectedValue == "Select Customer")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Customer.Thank You!!!');", true);
                return;
            }

            DataSet dsCustDet = objBs.GetCustomerDetailsforsales((ddlcustomerID.SelectedValue), sTableName);

            DataSet dagent = objBs.getagentlist(sTableName);
            if (dagent.Tables[0].Rows.Count > 0)
            {

                ddlRepname.DataSource = dagent.Tables[0];
                ddlRepname.DataTextField = "LedgerName";
                ddlRepname.DataValueField = "LedgerID";
                ddlRepname.DataBind();


            }
            else
            {
                ddlRepname.Items.Insert(0, "Select Agent");
            }
            if (dsCustDet.Tables[0].Rows.Count > 0)
            {
                txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                area = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                txtpincode.Text = dsCustDet.Tables[0].Rows[0]["Pincode"].ToString();
                txtcuscode.Text = dsCustDet.Tables[0].Rows[0]["LedgerID"].ToString();
                txtmobileno.Text = dsCustDet.Tables[0].Rows[0]["MobileNo"].ToString();
                //int agent = Convert.ToInt32(dsCustDet.Tables[0].Rows[0]["Agentid"]);
                //txtTransport.Text = dsCustDet.Tables[0].Rows[0]["Transport"].ToString();
                //ddlRepname.SelectedValue = dsCustDet.Tables[0].Rows[0]["Agentid"].ToString();
                ddlProvince.SelectedValue = dsCustDet.Tables[0].Rows[0]["province"].ToString();
                //lblarea.Text = dsCustDet.Tables[0].Rows[0]["rtozone1"].ToString();
                lblarea.Text = area;




                //dagent = objBs.getcustomeragent(agent, sTableName);
                //if (dagent.Tables[0].Rows.Count > 0)
                //{

                //    ddlRepname.DataSource = dagent.Tables[0];
                //    ddlRepname.DataTextField = "LEdgerName";
                //    ddlRepname.DataValueField = "LedgerID";
                //    ddlRepname.DataBind();
                //    ddlRepname.SelectedValue = dsCustDet.Tables[0].Rows[0]["Agentid"].ToString();
                //    ddlcustomerID.Items.Insert(0, "Select Agent");
                //}


            }
            ViewState["ledgerid"] = ddlcustomerID.SelectedValue;
            //DataSet dagent = objBs.Getagent();
            //if (dagent.Tables[0].Rows.Count > 0)
            //{

            //        ddlRepname.DataSource = dagent.Tables[0];
            //        ddlRepname.DataTextField = "LEdgerName";
            //        ddlRepname.DataValueField = "LedgerID";
            //        ddlRepname.DataBind();
            //        ddlRepname.Items.Insert(0, "Select Agent");
            //}
            txtadd.Text = txtaddress.Text + " ," + txtcity.Text + " ," + area + ", " + txtmobileno.Text + " , " + txtpincode.Text;

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            //}
            if (Convert.ToInt32(ddlbook.SelectedValue) == 1)
            {
                txtadd.Focus();
            }
            else
            {
                TextBox2.Focus();
            }
        }
        protected void txtmobileno_TextChanged2(object sender, EventArgs e)
        {
            DataSet dCustid = objBs.GerCustID(txtmobileno.Text);
            if (dCustid.Tables[0].Rows.Count > 0)
            {
                txtCustname.Text = dCustid.Tables[0].Rows[0]["CustomerName"].ToString();
                txtaddress.Text = dCustid.Tables[0].Rows[0]["Address"].ToString();
                txtcity.Text = dCustid.Tables[0].Rows[0]["City"].ToString();
                //txtarea.Text = dCustid.Tables[0].Rows[0]["Area"].ToString();
                txtpincode.Text = dCustid.Tables[0].Rows[0]["Pincode"].ToString();
                txtcuscode.Text = dCustid.Tables[0].Rows[0]["CustomerID"].ToString();
                txtmobileno.Text = dCustid.Tables[0].Rows[0]["MobileNo"].ToString();
                lblmoberror.InnerText = "Reapeated Customer";
                //btnadd.Visible = false;
            }
            else
            {
                lblmoberror.InnerText = "";
                btnadd.Visible = true;
            }
        }

        protected void checkbox1_changed(object sender, EventArgs e)
        {
            if (chknewcust.Checked == true)
            {
                //string url = "customermaster.aspx";
                //string s = "window.open('" + url + "', 'popup_window', 'width=900,height=500,resizable=yes');";
                //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                //   ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'customermaster.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                //DataSet dspricelist = objBs.PriceList(sTableName);

                //if (dspricelist != null)
                //{
                //    if (dspricelist.Tables[0].Rows.Count > 0)
                //    {
                //        ddlPriceList.DataSource = dspricelist.Tables[0];
                //        ddlPriceList.DataTextField = "PricelistName";
                //        ddlPriceList.DataValueField = "PricelistID";
                //        ddlPriceList.DataBind();

                //    }
                //}
                //Div18.Visible = true;
                //txtadd.Text = "";
                //txtadd.Enabled = true;
                ////ddlPayMode.SelectedItem.Text = "Select Payment Mode";
                ////ddlPurchaseType.SelectedItem.Text = "Select Purchase Type";
                ////txtDCNo.Text = "";
                ////txtpono.Text = "";
                //txtNarration.Text = "";
                //ddlcustomerID.SelectedIndex = 0;
                //ddlcustomerID.Visible = false;
                //txtCustname.Visible = true;
                //txtCustname.Text = "";
                //RequiredFieldValidator8.Enabled = true;
                //RequiredFieldValidator9.Enabled = true;
                //RequiredFieldValidator10.Enabled = true;
                //RequiredFieldValidator11.Enabled = true;
                //RequiredFieldValidator12.Enabled = true;
                //RequiredFieldValidator13.Enabled = true;
            }
            else
            {
                //Div18.Visible = false;
                //txtadd.Enabled = false;
                //txtaddress.Text = "";
                //txtcity.Text = "";
                //txtpincode.Text = "";
                //txtmobileno.Text = "";
                //ddlcustomerID.SelectedIndex = 0;
                //ddlcustomerID.Visible = true;
                //txtCustname.Visible = false;
                //txtCustname.Text = "";


                //txtaddress.Enabled = false;
                //txtpincode.Enabled = false;
                //txtcity.Enabled = false;
                //txtmobileno.Enabled = false;


            }
        }

        protected void chknew_CheckedChanged(object sender, EventArgs e)
        {
            if (chknew.Checked == true)
            {
                txtaddress.Text = "";
                txtcity.Text = "";
                txtpincode.Text = "";
                txtmobileno.Text = "";
                ddlcustomerID.SelectedIndex = 0;
                ddlcustomerID.Visible = false;
                txtCustname.Visible = true;
                txtCustname.Text = "";
            }
            else
            {
                txtaddress.Text = "";
                txtcity.Text = "";
                txtpincode.Text = "";
                txtmobileno.Text = "";
                ddlcustomerID.SelectedIndex = 0;
                ddlcustomerID.Visible = true;
                txtCustname.Visible = false;
                txtCustname.Text = "";
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DataSet dst = new DataSet();
            dst = objBs.getitemlist();



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtno = (TextBox)e.Row.FindControl("txtno");

                TextBox txtttk = (TextBox)e.Row.FindControl("txtqty");
                TextBox txttk = (TextBox)e.Row.FindControl("txtRate");
                TextBox txtkt = (TextBox)e.Row.FindControl("txtTax");
                TextBox txtkttt = (TextBox)e.Row.FindControl("txtAmount");
                //  TextBox txtktt = (TextBox)e.Row.FindControl("txtStock");
                TextBox txtktttt = (TextBox)e.Row.FindControl("txtDiscount");

                txtno.Text = "1";
                txtttk.Text = "0";
                txttk.Text = "0";
                txtkt.Text = "0";
                txtkttt.Text = "0";
                // txtktt.Text = "0";
                txtktttt.Text = "0";
                // txtno.Text = "1";


                var ddlt = (DropDownList)e.Row.FindControl("drpItem");
                ddlt.DataSource = dst;
                ddlt.DataTextField = "itemname";
                ddlt.DataValueField = "itemid";
                ddlt.DataBind();
                ddlt.Items.Insert(0, "Select Product");
            }

        }



        protected void ddlDef_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //DropDownList ddlcategory1 = (DropDownList)row.FindControl("ddlcategory1");
            //TextBox txtStock = (TextBox)row.FindControl("txtStockQTY");
            //TextBox txtDis = (TextBox)row.FindControl("txtDis");
            //TextBox ddlTax = (TextBox)row.FindControl("ddlTax");
            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
            //TextBox txtamount = (TextBox)row.FindControl("txtAmount");
            //DropDownList ddlDef1 = (DropDownList)row.FindControl("ddlDef1");
            //TextBox txtQty = (TextBox)row.FindControl("txtQty");
            //Label lblExpiry = (Label)row.FindControl("lblExpiryDate");
            //// TextBox txtStock = (TextBox)row.FindControl("txtStockQTY");
            ////DataSet dsCategory = objBs.getCatID(Convert.ToInt32(Def.SelectedValue));
            ////if (dsCategory.Tables[0].Rows.Count > 0)
            ////{

            ////    Label lblCatID = (Label)row.FindControl("catid1");
            ////    lblCatID.Text = dsCategory.Tables[0].Rows[0]["CategoryID"].ToString();
            ////    Decimal Irate = Convert.ToDecimal(dsCategory.Tables[0].Rows[0]["Rate"].ToString());
            ////    txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");
            ////    txtQty.Focus();

            ////}

            //DataSet dsStock = new DataSet();
            //if (bblbillto.SelectedItem.Text == "Customer")
            //{

            //    dsStock = objBs.getsStkprice(ddlcategory1.SelectedValue, ddlDef1.SelectedValue, "tblStock_" + sTableName, sTableName);
            //}
            //else
            //{
            //    dsStock = objBs.getsStkprice1(ddlcategory1.SelectedValue, ddlDef1.SelectedValue, "tblStock_" + sTableName, sTableName);
            //}

            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    decimal sQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Available_QTY"].ToString());
            //    decimal sTax = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Tax"].ToString());
            //    decimal sunitPrice;
            //    if (bblbillto.SelectedItem.Text == "Customer")
            //    {
            //        sunitPrice = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["UnitPrice"].ToString());
            //    }
            //    else
            //    {

            //        sunitPrice = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["DealerUnitPrice"].ToString());
            //    }
            //    // txtQty.Text = sQty.ToString("f2");
            //    txtStock.Text = sQty.ToString("f2");


            //    dsStock = objBs.getsprice1(ddlcustomerID.SelectedValue, ddlDef1.SelectedValue, sTableName);
            //    if (dsStock.Tables[0].Rows.Count > 0)
            //    {
            //        txtRate.Text = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Price"]).ToString();
            //    }

            //    ddlTax.Text = sTax.ToString("f2");

            //    //// Decimal Irate = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["UnitPrice"].ToString());
            //    //DateTime sDate = Convert.ToDateTime(dsStock.Tables[0].Rows[0]["Expirydate"].ToString());
            //    //// txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");
            //    //lblExpiry.Text = sDate.ToShortDateString();
            //    //DateTime Date = DateTime.Now;
            //    //string sNow = Date.ToShortDateString();
            //    //if (lblExpiry.Text == sNow)
            //    //{
            //    //    lblExpiry.ForeColor = System.Drawing.Color.Red;
            //    //    lblExpiry.Text = lblExpiry.Text + "" + "Expired";
            //    //}
            //}
            //else
            //{
            //    txtQty.Text = "";
            //    txtRate.Text = "";
            //    txtStock.Text = "";
            //    txtDis.Text = "";
            //    ddlTax.Text = "";
            //    txtamount.Text = "";
            //    //txtAmount.text = "";
            //}
            //ddlDef1.Focus();
            //ddlDef1.Enabled = true;

        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                //   DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                //  TextBox txtref = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrefno");
                //  TextBox txtcer = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtCerno");
                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                // TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                if (txt.SelectedItem.Text == "Select Product")
                {
                    No = 0;
                    break;
                }
                else
                {
                    No = 1;
                }
            }

            if (No == 1)
            {

                AddNewRow();
            }
            else
            {

            }
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                txtno.Focus();
            }

            // AddNewRow();
            // int Sno = 0;
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList txttt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpCategory");
            //    DropDownList txttd = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //    //Sno++;
            //    //txtno.Text = Convert.ToString(Sno);
            //    txtno.Focus();
            //    string qty = txtttk.Text;
            //    string rate = txttk.Text;
            //    string tax = txtkt.Text;
            //    string amount = txtkttt.Text;
            //    string stock = txtktt.Text;
            //    string discount = txtktttt.Text;
            //    string no = txtno.Text;

            //    if (qty != "" || rate != "" || tax != "" || amount != "" || stock != "" || discount != "" || no != "")
            //    {

            //    }
            //    else
            //    {
            //        txtttk.Text = "0";
            //        txttk.Text = "0";
            //        txtkt.Text = "0";
            //        txtkttt.Text = "0";
            //        txtktt.Text = "0";
            //        txtktttt.Text = "0";

            //    }

            //}

        }

        private void AddNewRow()
        {
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                //  DropDownList txttt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpCategory");
                //  DropDownList txttd = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                // DropDownList drpitemprintname = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpsaleitem");

                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                //TextBox txtref = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrefno");
                //TextBox txtcer = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtCerno");
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                // Label txtno = (Label)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                //TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                //TextBox txtKidCode = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtKidCode");
                //TextBox txtToRefNo = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtToRefNo");

                TextBox txtDesc = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDesc");

                int col = vLoop + 1;


                txtno.Focus();


            }

            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {



                        //TextBox txtStock =
                        //  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtStock");


                        DropDownList drpItem =
                         (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpItem");

                        TextBox txtRate =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtRate");
                        TextBox TextBoxAmount =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtAmount");




                        TextBox txtQty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtQty");

                        TextBox txttno =
                     (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");



                        TextBox txtDiscount =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtDiscount");
                        TextBox txtTax =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtTax");




                        TextBox txtDesc = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtDesc");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["OrderNo"] = i + 1;


                        //  dtCurrentTable.Rows[i - 1]["Stock"] = txtStock.Text;

                        dtCurrentTable.Rows[i - 1]["Amount"] = TextBoxAmount.Text;




                        dtCurrentTable.Rows[i - 1]["Product"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;



                        dtCurrentTable.Rows[i - 1]["Qty"] = txtQty.Text;
                        dtCurrentTable.Rows[i - 1]["orderno"] = txttno.Text;

                        dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;
                        dtCurrentTable.Rows[i - 1]["Tax"] = txtTax.Text;
                        dtCurrentTable.Rows[i - 1]["Description"] = txtDesc.Text;


                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DropDownList drpItem =
                          (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");



                        TextBox TextBoxAmount =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");




                        TextBox txtQty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtQty");
                        TextBox txttno =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");





                        TextBox txtDiscount =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtDiscount");
                        TextBox txtTax =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtTax");



                        TextBox txtDesc =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtDesc");



                        drpItem.Items.Clear();



                        DataSet dst = objBs.getitemlist();
                        drpItem.Items.Add(new ListItem("Select Product", "0"));
                        drpItem.DataSource = dst;
                        drpItem.DataBind();
                        drpItem.DataTextField = "itemname";
                        drpItem.DataValueField = "itemid";





                        // txtStock.Text = dt.Rows[i]["Stock"].ToString();
                        TextBoxAmount.Text = dt.Rows[i]["Amount"].ToString();
                        //  ProductCode.SelectedValue = dt.Rows[i]["ProductCode"].ToString();
                        drpItem.SelectedValue = dt.Rows[i]["Product"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();


                        txtQty.Text = dt.Rows[i]["Qty"].ToString();
                        txttno.Text = dt.Rows[i]["OrderNo"].ToString();
                        txtDiscount.Text = dt.Rows[i]["Discount"].ToString();
                        txtTax.Text = dt.Rows[i]["Tax"].ToString();

                        txtDesc.Text = dt.Rows[i]["Description"].ToString();


                        rowIndex++;

                    }
                }
            }
        }


        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            // dtt.Columns.Add(new DataColumn("Category", typeof(string)));
            // dtt.Columns.Add(new DataColumn("ProductCode", typeof(string)));
            dtt.Columns.Add(new DataColumn("Product", typeof(string)));
            //dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
            //dtt.Columns.Add(new DataColumn("KidCode", typeof(string)));
            //dtt.Columns.Add(new DataColumn("refno", typeof(string)));
            //dtt.Columns.Add(new DataColumn("torefno", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Cerno", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Stock", typeof(string)));

            //dtt.Columns.Add(new DataColumn("Sizes", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Sizem", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Sizel", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Sizexl", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Sizexxl", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size3xl", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size4xl", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Sizexs", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size50", typeof(string)));


            //dtt.Columns.Add(new DataColumn("Size20", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size22", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size24", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size26", typeof(string)));


            //dtt.Columns.Add(new DataColumn("Size28", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size30", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size32", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size34", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size36", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size38", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size40", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size42", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size44", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Size46", typeof(string)));
            //dtt.Columns.Add(new DataColumn("meter", typeof(string)));
            // dtt.Columns.Add(new DataColumn("segment", typeof(string)));
            dtt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("Discount", typeof(string)));
            dtt.Columns.Add(new DataColumn("Tax", typeof(string)));
            dtt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dtt.Columns.Add(new DataColumn("Description", typeof(string)));
            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            //dr["Category"] = string.Empty;
            //dr["ProductCode"] = string.Empty;
            dr["Product"] = string.Empty;
            //dr["Itemname"] = string.Empty;
            //dr["KidCode"] = string.Empty;
            //dr["torefno"] = string.Empty;
            //dr["refno"] = string.Empty;
            //dr["Cerno"] = string.Empty;
            //dr["Stock"] = string.Empty;

            //dr["Sizes"] = 0;
            //dr["Sizem"] = 0;
            //dr["Sizel"] = 0;
            //dr["Sizexl"] = 0;


            //dr["Sizexxl"] = 0;
            //dr["Size3xl"] = 0;
            //dr["Size4xl"] = 0;
            //dr["Sizexs"] = 0;
            //dr["Size50"] = 0;


            //dr["Size20"] = string.Empty;
            //dr["Size22"] = string.Empty;
            //dr["Size24"] = string.Empty;
            //dr["Size26"] = string.Empty;


            //dr["Size28"] = string.Empty;
            //dr["Size30"] = string.Empty;
            //dr["Size32"] = string.Empty;
            //dr["Size34"] = string.Empty;
            //dr["Size36"] = string.Empty;
            //dr["Size38"] = string.Empty;
            //dr["Size40"] = string.Empty;
            //dr["Size42"] = string.Empty;
            //dr["Size44"] = string.Empty;
            //dr["Size46"] = string.Empty;
            //dr["meter"] = string.Empty;
            //dr["segment"] = string.Empty;
            dr["Qty"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["Discount"] = string.Empty;
            dr["Tax"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["Description"] = string.Empty;
            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            //dct = new DataColumn("Category");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("ProductCode");
            //dttt.Columns.Add(dct);

            dct = new DataColumn("Product");
            dttt.Columns.Add(dct);

            //dct = new DataColumn("Itemname");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("KidCode");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Refno");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("torefno");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("CerNo");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Stock");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Sizes");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Sizem");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Sizel");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Sizexl");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Sizexxl");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size3xl");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size4xl");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Sizexs");
            //dttt.Columns.Add(dct);
            //dct = new DataColumn("Size50");
            //dttt.Columns.Add(dct);



            //dct = new DataColumn("Size20");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size22");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size24");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size26");
            //dttt.Columns.Add(dct);






            //dct = new DataColumn("Size28");
            //dttt.Columns.Add(dct);
            //dct = new DataColumn("Size30");
            //dttt.Columns.Add(dct);
            //dct = new DataColumn("Size32");
            //dttt.Columns.Add(dct);


            //dct = new DataColumn("Size34");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size36");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size38");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size40");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size42");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size44");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Size46");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("meter");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("segment");
            //dttt.Columns.Add(dct);

            dct = new DataColumn("Qty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Discount");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Tax");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Amount");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Description");
            dttt.Columns.Add(dct);


            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();

            //drNew["Size20"] = 0;
            //drNew["Size22"] = 0;
            //drNew["Size24"] = 0;
            //drNew["Size26"] = 0;

            //drNew["Sizes"] = 0;
            //drNew["Sizem"] = 0;
            //drNew["Sizel"] = 0;
            //drNew["Sizexl"] = 0;


            //drNew["Sizexxl"] = 0;
            //drNew["Size3xl"] = 0;
            //drNew["Size4xl"] = 0;
            //drNew["Sizexs"] = 0;
            //drNew["Size50"] = 0;


            //drNew["Size28"] = 0;
            //drNew["Size30"] = 0;
            //drNew["Size32"] = 0;
            //drNew["Size34"] = 0;
            //drNew["Size36"] = 0;
            //drNew["Size38"] = 0;
            //drNew["Size40"] = 0;
            //drNew["Size42"] = 0;
            //drNew["Size44"] = 0;
            //drNew["Size46"] = 0;
            //drNew["meter"] = 0;
            //drNew["segment"] = 0;

            drNew["Qty"] = 0;
            drNew["OrderNo"] = 0;
            // drNew["Category"] = "";
            drNew["Rate"] = 0;

            //drNew["KidCode"] = 0;
            //drNew["torefno"] = 0;

            drNew["Tax"] = 0;
            drNew["Amount"] = 0;
            // drNew["Stock"] = 0;
            // drNew["ProductCode"] = "";
            drNew["Product"] = "";
            //drNew["Itemname"] = "";
            //drNew["refno"] = 0;
            //drNew["cerno"] = 0;
            drNew["Discount"] = 0;
            drNew["Description"] = "";
            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();


            //TextBox txn = (TextBox)grvStudentDetails.Rows[0].Cells[1].FindControl("txtName");
            //txn.Focus();
            //Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
            //Page.Form.DefaultFocus = btnAdd.ClientID;

        }



        protected void txtAdvance_TextChanged(object sender, EventArgs e)
        {
            //if (txtDiscount.Text != "")
            //{
            //    decimal dDiscount = Convert.ToDecimal(txtDiscount.Text);
            //    decimal dSubTotal = Convert.ToDecimal(txtSubTotal.Text);
            //    decimal Advance = Convert.ToDecimal(txtAdvance.Text);
            //    decimal dDiscAmt = dSubTotal - ((dDiscount * dSubTotal) / 100);
            //    decimal dAmount = dDiscAmt - Advance;
            //    txttotal.Text = dAmount.ToString("f2");
            //}
            //else
            //{

            //}

        }

        protected void granddiscount(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;
            double tottqty = 0;
            double mettt = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                //   TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");
                if (txtktttt.Text == "")
                    txtktttt.Text = "0";

                if (txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
                {

                }
                else
                {

                    double iNetAmount1 = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount1 = Convert.ToDouble(iNetAmount1) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount1 = Convert.ToDouble(iNetAmount1) - Discount1;
                    double tx1 = Convert.ToDouble(DiscountAmount1) * Convert.ToDouble(txtkt.Text) / 100;
                    double total1 = tx1 + DiscountAmount1;




                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double total = tx + DiscountAmount;

                    txtkttt.Text = string.Format("{0:N2}", total1);




                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    distotal = distotal + Discount;
                    tottqty = tottqty + Convert.ToDouble(txtttk.Text);
                    txttk.Focus();

                }

            }
            double dFreight = 0;
            double dLU = 0;
            double sumLUFreight = 0;
            if (txtFreight.Text.Trim() != "")
            {
                dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            }
            if (txtLU.Text.Trim() != "")
            {
                dLU = Convert.ToDouble(txtLU.Text.Trim());
            }
            sumLUFreight = dFreight + dLU;
            txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            txtTaxamt.Text = string.Format("{0:N2}", tax);
            txtdiscountamount.Text = string.Format("{0:N2}", distotal);

            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtTaxamt.Text;

            }
            //  txtdiscount.Text = string.Format("{0:N2}", distotal);
            totqty.Text = tottqty.ToString();
            totmeter.Text = string.Format("{0:N2}", mettt);
            txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text));
            double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            if (roundoff > 0.5)
            {
                r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            }
            else
            {
                r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            }
            txtroundoff.Text = Convert.ToString(r);
        }
        //{
        //    if (txtgrandtotal.Text != "")
        //    {
        //        double grandtotal = 0;
        //        double tax = 0;
        //        double distotal = 0;
        //        double tottqty = 0;
        //        double mettt = 0;
        //        double r = 0;
        //        double disc = 0;

        //        for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
        //        {
        //            DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
        //            TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
        //            TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
        //            TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
        //            TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
        //            TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
        //            TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

        //            if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
        //            {

        //            }
        //            else
        //            {
        //                double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

        //                double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

        //                double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
        //                double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
        //                double total = tx + DiscountAmount;

        //                //  txtkttt.Text = string.Format("{0:N2}", total);

        //                if (txt.SelectedIndex == -1 || txt.SelectedIndex == 0)
        //                {
        //                }
        //                else
        //                {
        //                    //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
        //                    //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
        //                    //double totm = Convert.ToDouble(txtttk.Text) * mett;
        //                    //mettt = mettt + totm;
        //                }

        //                grandtotal = grandtotal + total;
        //                tax = tax + tx;
        //                distotal = distotal + Discount;
        //                tottqty = tottqty + Convert.ToDouble(txtttk.Text);

        //            }
        //        }
        //        double dFreight = 0;
        //        double dLU = 0;
        //        double sumLUFreight = 0;
        //        if (txtFreight.Text.Trim() != "")
        //        {
        //            dFreight = Convert.ToDouble(txtFreight.Text.Trim());
        //        }
        //        if (txtLU.Text.Trim() != "")
        //        {
        //            dLU = Convert.ToDouble(txtLU.Text.Trim());
        //        }
        //        sumLUFreight = dFreight + dLU;

        //        txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
        //        txtdiscountamount.Text = string.Format("{0:N2}", distotal);
        //        double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
        //        if (roundoff > 0.5)
        //        {
        //            r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
        //        }
        //        else
        //        {
        //            r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
        //        }
        //        txtroundoff.Text = Convert.ToString(r);
        //        //  txtTaxamt.Text = string.Format("{0:N2}", tax);
        //        //  txtdiscount.Text = string.Format("{0:N2}", distotal);
        //        //  totqty.Text = tottqty.ToString();
        //        //  totmeter.Text = string.Format("{0:N2}", mettt);
        //        //   txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
        //        // txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
        //        // txtRate_TextChanged(sender, e);
        //        // txtQty_TextChanged(sender, e);

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Item.Grand total is Empty!!!.Thank You');", true);
        //        return;
        //    }

        //}


        protected void txtLchange(object sender, EventArgs e)
        {
            if (txtgrandtotal.Text != "")
            {
                double grandtotal = 0;
                double tax = 0;
                double distotal = 0;
                double tottqty = 0;
                double mettt = 0;
                double r = 0;

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");
                    if (txtktttt.Text == "")
                        txtktttt.Text = "0";

                    if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
                    {

                    }
                    else
                    {

                        double iNetAmount1 = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                        double Discount1 = Convert.ToDouble(iNetAmount1) * Convert.ToDouble(txtktttt.Text) / 100;

                        double DiscountAmount1 = Convert.ToDouble(iNetAmount1) - Discount1;
                        double tx1 = Convert.ToDouble(DiscountAmount1) * Convert.ToDouble(txtkt.Text) / 100;
                        double total1 = tx1 + DiscountAmount1;


                        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                        double total = tx + DiscountAmount;


                        txtkttt.Text = string.Format("{0:N2}", total1);

                        if (txt.SelectedIndex == -1 || txt.SelectedIndex == 0)
                        {
                        }
                        else
                        {
                            //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
                            //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
                            //double totm = Convert.ToDouble(txtttk.Text) * mett;
                            //mettt = mettt + totm;
                        }

                        grandtotal = grandtotal + total;
                        tax = tax + tx;
                        distotal = distotal + Discount;
                        tottqty = tottqty + Convert.ToDouble(txtttk.Text);

                    }
                }
                double dFreight = 0;
                double dLU = 0;
                double sumLUFreight = 0;
                if (txtFreight.Text.Trim() != "")
                {
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
                }
                if (txtLU.Text.Trim() != "")
                {
                    dLU = Convert.ToDouble(txtLU.Text.Trim());
                }
                sumLUFreight = dFreight + dLU;

                txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
                double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
                }
                txtroundoff.Text = Convert.ToString(r);
                //  txtTaxamt.Text = string.Format("{0:N2}", tax);
                //  txtdiscount.Text = string.Format("{0:N2}", distotal);
                //  totqty.Text = tottqty.ToString();
                //  totmeter.Text = string.Format("{0:N2}", mettt);
                //   txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
                // txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
                // txtRate_TextChanged(sender, e);
                // txtQty_TextChanged(sender, e);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Item.Grand total is Empty!!!.Thank You');", true);
                return;
            }

        }

        protected void txttax_textchanged(object sender, EventArgs e)
        {
            // ButtonAdd1_Click(sender, e);
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;
            double tottqty = 0;
            double mettt = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                // TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");
                if (txtktttt.Text == "")
                    txtktttt.Text = "0";

                if (txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
                {

                }
                else
                {

                    double iNetAmount1 = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount1 = Convert.ToDouble(iNetAmount1) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount1 = Convert.ToDouble(iNetAmount1) - Discount1;
                    double tx1 = Convert.ToDouble(DiscountAmount1) * Convert.ToDouble(txtkt.Text) / 100;
                    double total1 = tx1 + DiscountAmount1;

                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double total = tx + DiscountAmount;

                    txtkttt.Text = string.Format("{0:N2}", total1);

                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    distotal = distotal + Discount;
                    tottqty = tottqty + Convert.ToDouble(txtttk.Text);
                    txttk.Focus();

                }

            }
            double dFreight = 0;
            double dLU = 0;
            double sumLUFreight = 0;
            if (txtFreight.Text.Trim() != "")
            {
                dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            }
            if (txtLU.Text.Trim() != "")
            {
                dLU = Convert.ToDouble(txtLU.Text.Trim());
            }
            sumLUFreight = dFreight + dLU;
            txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            txtTaxamt.Text = string.Format("{0:N2}", tax);
            txtdiscountamount.Text = string.Format("{0:N2}", distotal);


            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtTaxamt.Text;
            }

            //  txtdiscount.Text = string.Format("{0:N2}", distotal);
            totqty.Text = tottqty.ToString();
            totmeter.Text = string.Format("{0:N2}", mettt);
            txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text));
            double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            if (roundoff > 0.5)
            {
                r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            }
            else
            {
                r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            }
            txtroundoff.Text = Convert.ToString(r);

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                txtno.Text = Convert.ToString(i + 1);
                // ProductCode.Focus();
            }
            granddiscount(sender, e);

        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            ButtonAdd1_Click(sender, e);
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;
            double tottqty = 0;
            double mettt = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                // TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");
                if (txtktttt.Text == "")
                    txtktttt.Text = "0";

                if (txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
                {

                }
                else
                {
                    double iNetAmount1 = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount1 = Convert.ToDouble(iNetAmount1) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount1 = Convert.ToDouble(iNetAmount1) - Discount1;
                    double tx1 = Convert.ToDouble(DiscountAmount1) * Convert.ToDouble(txtkt.Text) / 100;
                    double total1 = tx1 + DiscountAmount1;


                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double total = tx + DiscountAmount;

                    txtkttt.Text = string.Format("{0:N2}", total1);


                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    distotal = distotal + Discount;
                    tottqty = tottqty + Convert.ToDouble(txtttk.Text);
                    txttk.Focus();

                }

            }
            double dFreight = 0;
            double dLU = 0;
            double sumLUFreight = 0;
            if (txtFreight.Text.Trim() != "")
            {
                dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            }
            if (txtLU.Text.Trim() != "")
            {
                dLU = Convert.ToDouble(txtLU.Text.Trim());
            }
            sumLUFreight = dFreight + dLU;
            txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            txtTaxamt.Text = string.Format("{0:N2}", tax);
            txtdiscountamount.Text = string.Format("{0:N2}", distotal);

            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtTaxamt.Text;

            }
            //  txtdiscount.Text = string.Format("{0:N2}", distotal);
            totqty.Text = tottqty.ToString();
            totmeter.Text = string.Format("{0:N2}", mettt);
            txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text));
            double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            if (roundoff > 0.5)
            {
                r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            }
            else
            {
                r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            }
            txtroundoff.Text = Convert.ToString(r);




            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
                    if (oldtxttk.Text == "0.00")
                    {
                        oldtxttk.Text = ".00";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            }
            //Lable Sno:
            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            //}

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
            granddiscount(sender, e);

        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                //   DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                //  TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");
                if (txtktttt.Text == "")
                    txtktttt.Text = "0";
                if (txt.SelectedItem.Text == "Select Product" || txtttk.Text == "")
                {

                }
                else
                {
                    if ((txttk.Text == "") || (Convert.ToString(txttk.Text) == ".00"))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Rate')", true);
                        txttk.Focus();
                        return;
                    }


                    double iNetAmount1 = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount1 = Convert.ToDouble(iNetAmount1) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount1 = Convert.ToDouble(iNetAmount1) - Discount1;
                    double tx1 = Convert.ToDouble(DiscountAmount1) * Convert.ToDouble(txtkt.Text) / 100;
                    double total1 = tx1 + DiscountAmount1;


                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double total = tx + DiscountAmount;

                    txtkttt.Text = string.Format("{0:N2}", total1);

                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    distotal = distotal + Discount;
                }
            }
            double dFreight = 0;
            double dLU = 0;
            double sumLUFreight = 0;
            if (txtFreight.Text.Trim() != "")
            {
                dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            }
            if (txtLU.Text.Trim() != "")
            {
                dLU = Convert.ToDouble(txtLU.Text.Trim());
            }
            sumLUFreight = dFreight + dLU;
            txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            txtTaxamt.Text = string.Format("{0:N2}", tax);

            txtdiscountamount.Text = string.Format("{0:N2}", distotal);
            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtTaxamt.Text;
            }

            //  txtdiscount.Text = string.Format("{0:N2}", distotal);
            txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text));
            double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            if (roundoff > 0.5)
            {
                r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            }
            else
            {
                r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            }
            txtroundoff.Text = Convert.ToString(r);

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                // txtno.Focus();
            }

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                txtno.Focus();
            }
            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            //}
            granddiscount(sender, e);
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
                {

                }
                else
                {

                    double iNetAmount1 = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount1 = Convert.ToDouble(iNetAmount1) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount1 = Convert.ToDouble(iNetAmount1) - Discount1;
                    double tx1 = Convert.ToDouble(DiscountAmount1) * Convert.ToDouble(txtkt.Text) / 100;
                    double total1 = tx1 + DiscountAmount1;


                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double total = tx + DiscountAmount;

                    txtkttt.Text = string.Format("{0:N2}", total1);

                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    distotal = distotal + Discount;
                }
            }
            double dFreight = 0;
            double dLU = 0;
            double sumLUFreight = 0;
            if (txtFreight.Text.Trim() != "")
            {
                dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            }
            if (txtLU.Text.Trim() != "")
            {
                dLU = Convert.ToDouble(txtLU.Text.Trim());
            }
            sumLUFreight = dFreight + dLU;

            txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            txtTaxamt.Text = string.Format("{0:N2}", tax);
            txtdiscountamount.Text = string.Format("{0:N2}", distotal);
            // txtdiscount.Text = string.Format("{0:N2}", distotal);
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpCategory");
            //        DropDownList txtt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //        if (txt.SelectedIndex != 0)
            //        {
            //            TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //            if (txtno.Text == "")
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter OrderNo.')", true);
            //                txtno.Focus();
            //                return;
            //            }
            //        }
            //        DropDownList ddl = (DropDownList)sender;
            //        GridViewRow row = (GridViewRow)ddl.NamingContainer;

            //        DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

            //        DropDownList Def = (DropDownList)row.FindControl("drpItem");

            //        DropDownList procode = (DropDownList)row.FindControl("ProductCode");
            //        TextBox qty = (TextBox)row.FindControl("txtQty");

            //        if (drpCategory.SelectedItem.Text != "Select Category")
            //        {

            //            DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(drpCategory.SelectedValue), sTableName);
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                Def.Items.Clear();
            //                Def.DataSource = dsCategory1.Tables[0];
            //                Def.DataTextField = "serial_NO";
            //                Def.DataValueField = "categoryuserid";
            //                Def.DataBind();
            //                //   Def.Items.Insert(0, "Select Product");

            //            }
            //            else
            //            {
            //                Def.Items.Clear();
            //                Def.Items.Insert(0, "Select Product");
            //            }
            //        }
            //        else
            //        {
            //        }

            //        if (drpCategory.SelectedItem.Text != "Select Category")
            //        {

            //            DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(drpCategory.SelectedValue), sTableName);
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                procode.Items.Clear();
            //                procode.DataSource = dsCategory1.Tables[0];
            //                procode.DataTextField = "Definition";
            //                procode.DataValueField = "categoryuserid";
            //                procode.DataBind();
            //                //procode.Items.Insert(0, "Select Product Code");

            //            }
            //            else
            //            {
            //                procode.Items.Clear();
            //                procode.Items.Insert(0, "Select Product Code");
            //            }
            //        }
            //        else
            //        {
            //        }


            //        //if (txt.SelectedIndex != 0)
            //        //{

            //        //    if ((txtt.SelectedValue == "0") || (txtt.SelectedValue == "") || (txtt.SelectedValue == "Select Product"))
            //        //    {
            //        //        DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(txt.SelectedValue));
            //        //        if (dsCategory1.Tables[0].Rows.Count > 0)
            //        //        {
            //        //            txtt.Items.Clear();
            //        //            txtt.DataSource = dsCategory1.Tables[0];
            //        //            txtt.DataTextField = "Definition";
            //        //            txtt.DataValueField = "categoryuserid";
            //        //            txtt.DataBind();
            //        //            txtt.Items.Insert(0, "Select Product");

            //        //        }
            //        //        else
            //        //        {
            //        //            txtt.Items.Clear();
            //        //            txtt.Items.Insert(0, "Select Product");
            //        //        }
            //        //    }
            //        //}
            //        qty.Focus();
            //    }
            //}

        }

        protected void orderno(object sender, EventArgs e)
        {
            //int number = 0;
            //int iq = 1;
            //int ii = 1;
            //string itemc = string.Empty;
            //string itemcd = string.Empty;
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        TextBox txtno1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");

            //        itemc = txtno1.Text;
            //        // number = Convert.ToInt32(txtno.Text);
            //        if ((itemc == null) || (itemc == ""))
            //        {

            //        }
            //        else
            //        {
            //            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
            //            {
            //                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtno");

            //                if (ii == iq)
            //                {
            //                }
            //                else
            //                {
            //                    if (itemc == txtno.Text)
            //                    {
            //                        itemcd = txtno.Text;
            //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
            //                        return;

            //                    }
            //                }
            //                ii = ii + 1;
            //            }
            //        }
            //        iq = iq + 1;
            //        ii = 1;

            //        if (txtno1.Text == "")
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter OrderNo.')", true);
            //            txtno1.Focus();
            //            return;
            //        }

            //    }
            //}
            //ButtonAdd1_Click(sender, e);
        }
        protected void drpsegment_SelectedIndexChanged(object sender, EventArgs e)
        {


            //int number = 0;
            //int iq = 1;
            //int ii = 1;
            //string itemc = string.Empty;
            //string itemcd = string.Empty;
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txti = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //        TextBox txtqty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtQty");
            //        TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");


            //        itemc = txti.Text;


            //        if ((itemc == null) || (itemc == ""))
            //        {
            //        }
            //        else
            //        {
            //            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
            //            {
            //                DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpItem");
            //                if (txt1.Text == "")
            //                {
            //                }
            //                else
            //                {

            //                    if (ii == iq)
            //                    {
            //                    }
            //                    else
            //                    {
            //                        if (itemc == txt1.Text)
            //                        {
            //                            itemcd = txti.SelectedItem.Text;
            //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
            //                            txt1.Focus();
            //                            return;

            //                        }
            //                    }
            //                    ii = ii + 1;
            //                }
            //            }
            //        }
            //        iq = iq + 1;
            //        ii = 1;


            //    }
            //}


            //string avaliablestock = string.Empty;

            //DropDownList ddll = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddll.NamingContainer;
            //DropDownList drpsegment = (DropDownList)row.FindControl("drpsegment");
            //DropDownList drpItem = (DropDownList)row.FindControl("drpItem");
            //TextBox txt34 = (TextBox)row.FindControl("txt34");
            //TextBox txt36 = (TextBox)row.FindControl("txt36");
            //TextBox txt38 = (TextBox)row.FindControl("txt38");
            //TextBox txt40 = (TextBox)row.FindControl("txt40");
            //TextBox txt42 = (TextBox)row.FindControl("txt42");
            //TextBox txt44 = (TextBox)row.FindControl("txt44");
            ////lblbitem.Text = drpItem.SelectedValue;
            ////lbllblsegment.Text = drpsegment.SelectedValue;
            //int fcus = 0;
            //DataSet dsize = new DataSet();
            //dsize = objBs.getisegment(drpsegment.SelectedValue);
            //if (dsize.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dsize.Tables[0].Rows.Count; i++)
            //    {

            //        string sizename = dsize.Tables[0].Rows[i]["SalesSize"].ToString();
            //        string SalesSizeId = dsize.Tables[0].Rows[i]["SalesSizeId"].ToString();
            //        avaliablestock = "0";
            //        DataSet dcheckstock = objBs.getstockid(SalesSizeId, drpItem.SelectedValue, sTableName);
            //        if (dcheckstock.Tables[0].Rows.Count > 0)
            //        {
            //            //   double stock = Convert.ToDouble(dcheckstock.Tables[0].Rows[0]["Available_QTY"]).ToString("0.00");

            //            avaliablestock = Convert.ToDouble(dcheckstock.Tables[0].Rows[0]["Available_QTY"]).ToString("0.00");
            //        }
            //        else
            //        {
            //            avaliablestock = "0";
            //        }
            //    }
            //}

        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvince.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Province.');", true);
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList txti = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                    if (txti.SelectedValue != "Select Product")
                    {
                        txti.SelectedValue = "Select Product";
                    }
                }
                return;
            }
            {
                int iq = 1;
                int ii = 1;
                string itemc = string.Empty;
                string itemcd = string.Empty;
                if (ViewState["CurrentTable1"] != null)
                {
                    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                    {
                        DropDownList txti = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                        TextBox txtqty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtQty");
                        // TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");


                        itemc = txti.Text;


                        if ((itemc == null) || (itemc == ""))
                        {
                        }
                        else
                        {
                            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                            {
                                DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpItem");
                                if (txt1.Text == "")
                                {
                                }
                                else
                                {

                                    if (ii == iq)
                                    {
                                    }
                                    else
                                    {
                                        if (itemc == txt1.Text)
                                        {
                                            itemcd = txti.SelectedItem.Text;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                                            txt1.Focus();
                                            return;

                                        }
                                    }
                                    ii = ii + 1;
                                }
                            }
                        }
                        iq = iq + 1;
                        ii = 1;


                    }
                }


                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                // DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");


                DropDownList Defitem = (DropDownList)row.FindControl("drpItem");

                // DropDownList procode = (DropDownList)row.FindControl("ProductCode");

                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txt = (TextBox)row.FindControl("txtDiscount");
                TextBox txtTax = (TextBox)row.FindControl("txtTax");
                DropDownList Def = (DropDownList)row.FindControl("drpItem");
                //  DropDownList cate = (DropDownList)row.FindControl("drpCategory");
                TextBox txtQty = (TextBox)row.FindControl("txtStock");
                TextBox qty = (TextBox)row.FindControl("txtQty");
                //  TextBox refno = (TextBox)row.FindControl("txtrefno");
                //   TextBox KidCode = (TextBox)row.FindControl("txtKidCode");
                if (Defitem.SelectedItem.Text != "Select Product")
                {

                    DataSet dsCategory1 = objBs.getitemlistforparticulat(Defitem.SelectedValue);
                    if (dsCategory1.Tables[0].Rows.Count > 0)
                    {
                        Defitem.SelectedValue = dsCategory1.Tables[0].Rows[0]["itemid"].ToString();
                        txtRate.Text = Convert.ToDouble(dsCategory1.Tables[0].Rows[0]["Rate"]).ToString("0.00");
                        txtTax.Text = dsCategory1.Tables[0].Rows[0]["Tax"].ToString();
                    }
                    else
                    {
                        txtRate.Text = "0.00";
                        txtTax.Text = "0";
                    }






                    //DataSet dsStock = new DataSet();


                    //dsStock = objBs.GetStockDetails(Convert.ToInt32(Def.SelectedValue), "tblStock_" + sTableName);

                    //if (dsStock.Tables[0].Rows.Count > 0)
                    //{
                    //    DataSet dsCategory = objBs.GetTax(Convert.ToInt32(Def.SelectedValue), sTableName);

                    //    var Itx = Convert.ToDecimal(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
                    //    txtTax.Text = Itx.ToString();

                    //    decimal ratte = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["UnitPrice"]);
                    //    txtRate.Text = Decimal.Round(ratte, 2).ToString("f2");

                    //    double sQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
                    //    //txtQty.Text = sQty.ToString("f2");
                    //    txtQty.Text = sQty.ToString();
                    //    cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

                    //    txt.Text = "0";
                    //    txt34.Text = "0";
                    //    txt36.Text = "0";
                    //    txt38.Text = "0";
                    //    txt40.Text = "0";
                    //    txt42.Text = "0";
                    //    txt44.Text = "0";

                    //    string value = Def.SelectedValue;
                    //    DataSet ds = objBs.itemhistorypopup(sTableName, value);
                    //    if (ds.Tables[0].Rows.Count > 0)
                    //    {
                    //        txtitemhis.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
                    //    }
                    //    else
                    //    {
                    //        txtitemhis.Text = "";
                    //    }




                    //}




                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                        txtno.Text = Convert.ToString(i + 1);
                    }

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        TextBox txtDesc = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDesc");
                        txtDesc.Focus();
                    }
                }
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        //TextBox txtStock =
                        //  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtStock");

                        DropDownList drpItem =
                          (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");
                        TextBox TextBoxAmount =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");






                        TextBox txtQty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtQty");

                        TextBox txttno =
                  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");

                        TextBox txtDiscount =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtDiscount");
                        TextBox txtTax =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtTax");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;

                        //  dtCurrentTable.Rows[i - 1]["Stock"] = txtStock.Text;



                        dtCurrentTable.Rows[i - 1]["Amount"] = TextBoxAmount.Text;

                        dtCurrentTable.Rows[i - 1]["Product"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Qty"] = txtQty.Text;
                        dtCurrentTable.Rows[i - 1]["Orderno"] = txttno.Text;

                        dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;

                        dtCurrentTable.Rows[i - 1]["Tax"] = txtTax.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }


        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();

            if (ViewState["CurrentTable1"] != null)
            {
                DataSet ds = new DataSet();
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    txtitemhis.Text = "";
                    txtcusthis.Text = "";
                    ds.Merge(dt);


                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();

                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();






                    SetPreviousData();

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                        txtno.Text = Convert.ToString(i + 1);
                    }

                    txtRate_TextChanged(sender, e);
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();

                    txtRate_TextChanged(sender, e);
                    FirstGridViewRow();
                }
            }

        }
        private void adSetRowData()
        {
            //int rowIndex = 0;

            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            //    DataRow drCurrentRow = null;
            //    if (dtCurrentTable.Rows.Count > 0)
            //    {
            //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //        {

            //            DropDownList drpCategory =
            //             (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpCategory");


            //            DropDownList drpsegment =
            //             (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpsegment");

            //            DropDownList drpsaleitem =
            //             (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("drpsaleitem");

            //            TextBox txtStock =
            //              (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtStock");
            //            DropDownList ProductCode =
            //             (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("ProductCode");

            //            TextBox txtRef =
            //          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrefno");

            //            TextBox txtKidCode =
            //        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtKidCode");
            //            TextBox txtToRefNo =
            //        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtToRefNo");


            //            TextBox txtcer =
            //           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtCerno");

            //            DropDownList drpItem =
            //              (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");
            //            TextBox TextBoxAmount =
            //              (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtAmount");
            //            TextBox txtRate =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");


            //            TextBox txt20 =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt20");
            //            TextBox txt22 =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt22");
            //            TextBox txt24 =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt24");
            //            TextBox txt26 =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt26");



            //            TextBox txts =
            //           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txts");
            //            TextBox txtm =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtm");
            //            TextBox txtl =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtl");
            //            TextBox txtxl =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtxl");
            //            TextBox txtxxl =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtxxl");
            //            TextBox txt3xl =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt3xl");

            //            TextBox txt4xl =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt4xl");

            //            TextBox txtxs =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtxs");

            //            TextBox txt50 =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt50");


            //            TextBox txt28 =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt28");

            //            TextBox txt30 =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt30");

            //            TextBox txt32 =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt32");




            //            TextBox txt34 =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt34");

            //            TextBox txt36 =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt36");

            //            TextBox txt38 =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt38");

            //            TextBox txt40 =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt40");

            //            TextBox txt42 =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt42");

            //            TextBox txt44 =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt44");
            //            TextBox txt46 =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt46");

            //            TextBox txtmeter =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtmeter");



            //            TextBox txtQty =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtQty");

            //            TextBox txttno =
            //      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");

            //            TextBox txtDiscount =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtDiscount");
            //            TextBox txtTax =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtTax");

            //            TextBox txtDesc =
            //            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtDesc");

            //            drCurrentRow = dtCurrentTable.NewRow();
            //            drCurrentRow["Orderno"] = i + 1;
            //            dtCurrentTable.Rows[i - 1]["Category"] = drpCategory.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["Stock"] = txtStock.Text;

            //            dtCurrentTable.Rows[i - 1]["itemname"] = drpsaleitem.SelectedValue;
            //            //dtCurrentTable.Rows[i - 1]["torefno"] = txtToRefNo.Text;

            //            dtCurrentTable.Rows[i - 1]["Amount"] = TextBoxAmount.Text;
            //            dtCurrentTable.Rows[i - 1]["ProductCode"] = ProductCode.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["segment"] = drpsegment.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["Product"] = drpItem.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
            //            //dtCurrentTable.Rows[i - 1]["Refno"] = txtRef.Text;
            //            //dtCurrentTable.Rows[i - 1]["Cerno"] = txtcer.Text;

            //            dtCurrentTable.Rows[i - 1]["Size20"] = txt20.Text;
            //            dtCurrentTable.Rows[i - 1]["Size22"] = txt22.Text;
            //            dtCurrentTable.Rows[i - 1]["Size24"] = txt24.Text;
            //            dtCurrentTable.Rows[i - 1]["Size26"] = txt26.Text;


            //            dtCurrentTable.Rows[i - 1]["Sizes"] = txts.Text;
            //            dtCurrentTable.Rows[i - 1]["Sizem"] = txtm.Text;
            //            dtCurrentTable.Rows[i - 1]["Sizel"] = txtl.Text;
            //            dtCurrentTable.Rows[i - 1]["Sizexl"] = txtxl.Text;
            //            dtCurrentTable.Rows[i - 1]["Sizexxl"] = txtxxl.Text;
            //            dtCurrentTable.Rows[i - 1]["Size3xl"] = txt3xl.Text;
            //            dtCurrentTable.Rows[i - 1]["Size4xl"] = txt4xl.Text;
            //            dtCurrentTable.Rows[i - 1]["Sizexs"] = txtxs.Text;
            //            dtCurrentTable.Rows[i - 1]["Size50"] = txt50.Text;


            //            dtCurrentTable.Rows[i - 1]["Size28"] = txt28.Text;
            //            dtCurrentTable.Rows[i - 1]["Size30"] = txt30.Text;
            //            dtCurrentTable.Rows[i - 1]["Size32"] = txt32.Text;

            //            dtCurrentTable.Rows[i - 1]["Size34"] = txt34.Text;
            //            dtCurrentTable.Rows[i - 1]["Size36"] = txt36.Text;
            //            dtCurrentTable.Rows[i - 1]["Size38"] = txt38.Text;
            //            dtCurrentTable.Rows[i - 1]["Size40"] = txt40.Text;
            //            dtCurrentTable.Rows[i - 1]["Size42"] = txt42.Text;
            //            dtCurrentTable.Rows[i - 1]["Size44"] = txt44.Text;
            //            dtCurrentTable.Rows[i - 1]["Size46"] = txt46.Text;
            //            dtCurrentTable.Rows[i - 1]["meter"] = txtmeter.Text;
            //            dtCurrentTable.Rows[i - 1]["Qty"] = txtQty.Text;
            //            dtCurrentTable.Rows[i - 1]["Orderno"] = txttno.Text;

            //            dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;

            //            dtCurrentTable.Rows[i - 1]["Tax"] = txtTax.Text;
            //            dtCurrentTable.Rows[i - 1]["Description"] = txtDesc.Text;

            //            rowIndex++;

            //        }

            //        ViewState["CurrentTable1"] = dtCurrentTable;
            //        gvcustomerorder.DataSource = dtCurrentTable;
            //        gvcustomerorder.DataBind();
            //    }
            //}
            //else
            //{
            //    Response.Write("ViewState is null");
            //}
            //SetPreviousData();
        }

        protected void gvcustomerorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataSet dsCategory = new DataSet();

            //DataSet dscat = new DataSet();

            //string OrderNo = Request.QueryString.Get("OrderNo");
            ////if (OrderNo != "")
            ////{
            ////    /// dsCategory = objBs.GetCAT_OrderForm();
            ////}
            ////else
            //dsCategory = objBs.selectcategorybrandcat(sTableName);

            //dscat = objBs.selectcatuser();



            ////else
            ////    dsCategory = objBs.selectcategorymaster_Dealer("tblStock_" + sTableName);


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    DropDownList ddlCategory1 = (DropDownList)(e.Row.FindControl("ddlCategory1") as DropDownList);
            //    ddlCategory1.Focus();
            //    ddlCategory1.Enabled = true;
            //    ddlCategory1.DataSource = dsCategory.Tables[0];
            //    ddlCategory1.DataTextField = "productname";
            //    ddlCategory1.DataValueField = "CategoryUserID";
            //    ddlCategory1.DataBind();
            //    ddlCategory1.Items.Insert(0, "Select");

            //    DropDownList ddlDef1 = (DropDownList)(e.Row.FindControl("ddlDef1") as DropDownList);
            //    ddlDef1.Focus();
            //    ddlDef1.Enabled = true;
            //    ddlDef1.DataSource = dscat.Tables[0];
            //    ddlDef1.DataTextField = "Definition";
            //    ddlDef1.DataValueField = "categoryuserid";
            //    ddlDef1.DataBind();
            //    ddlDef1.Items.Insert(0, "Select Product");

            //    //DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(ddlCategory1.SelectedItem.Text));
            //    //if (dsCategory1.Tables[0].Rows.Count > 0)
            //    //{
            //    //    //DropDownList Def1 = (DropDownList)row.FindControl("ddlDef1");
            //    //    ////Label lblCatID = (Label)row.FindControl("catid");
            //    //    ////lblCatID.Text = dsCategory.Tables[0].Rows[0]["CategoryID"].ToString();

            //    //    ddlDef1.DataSource = dsCategory.Tables[0];
            //    //    ddlDef1.DataTextField = "Definition";
            //    //    ddlDef1.DataValueField = "categoryuserid";
            //    //    ddlDef1.DataBind();
            //    //    ddlDef1.Items.Insert(0, "Select Product");
            //    //    ddlDef1.Focus();
            //    //}

            //    //DataSet dDef = objBs.selectcategorydecription(Convert.ToInt32(ddlCategory1.SelectedValue));
            //    //DropDownList Def = (DropDownList)e.Row.FindControl("ddlDef1");

            //    //Def.DataSource = dDef.Tables[0];
            //    //Def.DataTextField = "Definition";
            //    //Def.DataValueField = "categoryuserid";
            //    //Def.DataBind();
            //    //#region Databind
            //    //string billno = Convert.ToString(Request.QueryString["iSalesID"]);

            //    //if (billno != null)
            //    //{



            //    //    DataSet dBilling = objBs.GetSalesnew("tblSales_" + sTableName, billno);
            //    //    if (dBilling.Tables[0].Rows.Count > 0)
            //    //    {



            //    //        //txtcustomername.Text = dBilling.Tables[0].Rows[0]["CustomerName"].ToString();
            //    //       // txtmobileno.Text = dBilling.Tables[0].Rows[0]["PhoneNo"].ToString();
            //    //        txtSubTotal.Text = dBilling.Tables[0].Rows[0]["Total"].ToString();
            //    //       // txtAgainstAmount.Text = dBilling.Tables[0].Rows[0]["Advance"].ToString();
            //    //        ddlbook.Text = dBilling.Tables[0].Rows[0]["Book"].ToString();
            //    //       // txttotal.Text = dBilling.Tables[0].Rows[0]["Balance"].ToString();
            //    //        int iCount = dBilling.Tables[0].Rows.Count;

            //    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //    //        DataRow drCurrentRow = null;
            //    //        DataSet dBilling1 = objBs.GettransnewSales("tblTransSales_" + sTableName, billno);
            //    //        for (int i = 0; i < iCount; i++)
            //    //        {

            //    //            TextBox txtRate = (TextBox)e.Row.FindControl("txtRate");
            //    //            TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
            //    //            TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmount");
            //    //            DropDownList ddlCat = (DropDownList)e.Row.FindControl("ddlCategory");
            //    //           // DropDownList ddlDef = (DropDownList)e.Row.FindControl("ddlDef");

            //    //            ddlCat.SelectedValue = dBilling1.Tables[0].Rows[i]["SubCategoryID"].ToString();


            //    //          //  ddlDef.SelectedValue = dBilling.Tables[0].Rows[i]["SubCategoryID"].ToString();


            //    //            txtQty.Text = dBilling1.Tables[0].Rows[i]["Qty"].ToString();


            //    //            txtRate.Text = dBilling1.Tables[0].Rows[i]["Rate"].ToString();


            //    //            txtAmt.Text = dBilling1.Tables[0].Rows[i]["Amount"].ToString();


            //    //            if (dtCurrentTable.Rows.Count > 0)
            //    //            {
            //    //                for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
            //    //                {

            //    //                    drCurrentRow = dtCurrentTable.NewRow();
            //    //                    drCurrentRow["sno"] = j + 1;

            //    //                    dtCurrentTable.Rows[j - 1]["SubCategoryID"] = ddlCategory1.Text;
            //    //                   // dtCurrentTable.Rows[j - 1]["Item"] = ddlDef.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Qty"] = txtQty.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Rate"] = txtRate.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Amount"] = txtAmt.Text;


            //    //                }
            //    //                dtCurrentTable.Rows.Add(drCurrentRow);
            //    //                ViewState["CurrentTable"] = dtCurrentTable;

            //    //                gvcustomerorder.DataSource = dtCurrentTable;
            //    //                gvcustomerorder.DataBind();
            //    //            }

            //    //        }
            //    //    }

            //    //}

            //}
        }


        protected void ddlCategort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //DropDownList ddlCategory1 = (DropDownList)row.FindControl("ddlCategory1");
            //DropDownList ddDef1 = (DropDownList)row.FindControl("ddlDef1");
            //DataSet dsCategory = objBs.selectcategorydecription(Convert.ToInt32(ddlCategory1.SelectedValue));
            //if (dsCategory.Tables[0].Rows.Count > 0)
            //{
            //    DropDownList Def1 = (DropDownList)row.FindControl("ddlDef1");
            //    //Label lblCatID = (Label)row.FindControl("catid");
            //    //lblCatID.Text = dsCategory.Tables[0].Rows[0]["CategoryID"].ToString();
            //    Def1.DataSource = dsCategory.Tables[0];
            //    Def1.DataTextField = "Definition";
            //    Def1.DataValueField = "categoryuserid";
            //    Def1.DataBind();
            //    Def1.Items.Insert(0, "Select Product");
            //    Def1.Focus();
            //    Def1.ClearSelection();
            //}
            //else
            //{

            //    ddDef1.ClearSelection();
            //}
            //ddlCategory1.Focus();
            //ddlCategory1.Enabled = true;

            ddlDef_SelectedIndexChanged1(sender, e);
            // ButtonAdd1_Click(sender, e);
        }



        protected void gvcustomerorder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private int UpdateStockAvailable(int iSubCat, int iQty)
        //{
        //    int iAQty = 0, iSuccess = 0;
        //    //if (sTableName == "admin")
        //    //{

        //    DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
        //    if (dsStock.Tables[0].Rows.Count > 0)
        //    {
        //        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

        //    }
        //    int iRemQty = iAQty - iQty;
        //    iSuccess = objBs.updateSalesStocknew(iRemQty, iSubCat, "tblStock_" + sTableName);

        //    //}
        //    //else
        //    //{
        //    //    DataSet dsStock = objBs.GetStockDetails_Dealer(iSubCat, "tblStock_" + sTableName);
        //    //    if (dsStock.Tables[0].Rows.Count > 0)
        //    //    {
        //    //        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

        //    //    }
        //    //    int iRemQty = iAQty - iQty;
        //    //    iSuccess = objBs.updateSalesStock_dealer(iRemQty, iCat, iSubCat,"tblStock_"+sTableName);
        //    //}
        //    return iSuccess;
        //}

        private int InsertUpdateStockAvailableNew(int iSubCat, int iQty)
        {
            double iAQty = 0;
            int iSuccess = 0;


            //DataSet dsStock = objBs.GetStockDetails1new(iSubCat, "tblStock_" + sTableName);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //double iRemQty = iAQty - iQty;
            //iSuccess = objBs.updateStocknew1(iRemQty, iSubCat, "tblStock_" + sTableName);


            return iSuccess;
        }

        private int InsertUpdateStockAvailable(int iSubCat, int iQty, String Sizeid)
        {
            double iAQty = 0;
            int iSuccess = 0;


            //DataSet dsStock = objBs.GetStockDetails1(iSubCat, "tblStock_" + sTableName, Sizeid);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //double iRemQty = iAQty - iQty;
            //iSuccess = objBs.updateStocknew(iRemQty, iSubCat, "tblStock_" + sTableName, Sizeid);


            return iSuccess;
        }


        private int InsertUpdateStockAvailableformeter(int iSubCat, double iQty, String Sizeid)
        {
            double iAQty = 0;
            int iSuccess = 0;


            //DataSet dsStock = objBs.GetStockDetails1(iSubCat, "tblStock_" + sTableName, Sizeid);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //double iRemQty = iAQty - iQty;
            //iSuccess = objBs.updateStocknew(iRemQty, iSubCat, "tblStock_" + sTableName, Sizeid);


            return iSuccess;
        }

        private int UPDUpdateStockAvailableformeter(int iSubCat, double iQty, String Sizeid)
        {
            double iAQty = 0;
            int iSuccess = 0;


            //DataSet dsStock = objBs.GetStockDetails1(iSubCat, "tblStock_" + sTableName, Sizeid);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //double iRemQty = iAQty + iQty;
            //iSuccess = objBs.updateStocknew(iRemQty, iSubCat, "tblStock_" + sTableName, Sizeid);


            return iSuccess;
        }

        private int InsertStockAvailable(int iCat, int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            //if (sTableName == "admin")
            //{

            string iQrySalesID = Request.QueryString.Get("iSalesID");
            if (iQrySalesID != null)
            {
                DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

                }
                int iRemQty = iAQty + iQty;
                iSuccess = objBs.updateSalesStock(iRemQty, iCat, iSubCat, "tblStock_" + sTableName);
            }

            //}
            //else
            //{
            //    DataSet dsStock = objBs.GetStockDetails_Dealer(iSubCat, "tblStock_" + sTableName);
            //    if (dsStock.Tables[0].Rows.Count > 0)
            //    {
            //        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //    }
            //    int iRemQty = iAQty - iQty;
            //    iSuccess = objBs.updateSalesStock_dealer(iRemQty, iCat, iSubCat,"tblStock_"+sTableName);
            //}
            return iSuccess;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("salesgrid.aspx");
        }

        protected void Gridview1_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            // GridViewRow row = gvcustomerorder.SelectedRow;


            if (e.CommandName == "Select")
            {

                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                DropDownList item = row.FindControl("drpitem") as DropDownList;
            }
            else if (e.CommandName == "Itemhis")
            {
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                DropDownList item = row.FindControl("drpitem") as DropDownList;
                //  DropDownList drpsegment = row.FindControl("drpsegment") as DropDownList;

                var yourValue = item.SelectedItem.Text;
                if (yourValue == "Select Product")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product.')", true);
                    return;

                }
                string value = item.SelectedValue;
                DataSet ds = objBs.itemhistorypopup(sTableName, value);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }
                    mpe.Show();
                }
            }
            else if (e.CommandName == "Select1")
            {
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                DropDownList yourTextbox = row.FindControl("drpitem") as DropDownList;

                var yourValue = yourTextbox.Text;

                if (yourValue == "Select Product")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product.')", true);
                    return;

                }
                if (ddlcustomerID.SelectedValue == "Select Customer")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Customer Name')", true);
                    return;

                }

                string value1 = yourTextbox.SelectedValue;

                string cust = ddlcustomerID.SelectedValue;
                DataSet ds = objBs.custhistorypopup(sTableName, value1, cust);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }
                    mpe.Show();
                }
            }
            //  mpe.Show();
        }

        protected void Add_Click(object sender, EventArgs e)
        {

            DataSet dss = new DataSet();
            if (ddlbook.SelectedItem.Value == "1")
            {
                ddlvouchertype.SelectedValue = "2";

            }
            else
            {
                ddlvouchertype.SelectedValue = "1";
                //  ddlcustomerID.SelectedValue = "17";

            }
            string custid = string.Empty;
            if (chknewcust.Checked == false)
            {
                if (ddlcustomerID.SelectedValue == "Select Customer")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Please Select Customer Name')", true);

                    return;

                }
            }
            else
            {

            }
            if (ddlProvince.SelectedValue == "Select Province type")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Province type')", true);

                return;
            }




            if (txtbilledby.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Billed By ')", true);
                txtbilledby.Focus();
                return;

            }

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                int col = vLoop + 1;
                if (txt.Text == "")
                {
                }
                else
                {
                    if (txt.SelectedItem.Text == "Select Product")
                    {
                        if (gvcustomerorder.Rows.Count == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Atleast one product.Thank you!!! ')", true);
                            return;
                        }
                    }
                    else
                    {


                        if ((txttk.Text == "") || (Convert.ToString(txttk.Text) == ".00"))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Rate " + col + " ')", true);
                            txttk.Focus();
                            return;
                        }


                    }
                }
            }

            DateTime billldate = DateTime.ParseExact(txtvoudate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime lrrdate = DateTime.ParseExact(txtlrdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime orddate = DateTime.ParseExact(txtorderdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dueedate = DateTime.ParseExact(txtduedate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime vouchdate = DateTime.ParseExact(txtvoudate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet dsss = objBs.customerid(1, 2);
            if (dsss.Tables[0].Rows.Count > 0)
            {
                if (dsss.Tables[0].Rows[0]["CustomerId"].ToString() == "")
                    custid = "1";
                else
                    custid = dsss.Tables[0].Rows[0]["CustomerId"].ToString();
            }
            #region

            if (btnadd.Text == "Save")
            {
                int iStatus2 = 0, iStatus3 = 0, iStatus4 = 0, iStatus5 = 0, iStockSuccess = 0;
                int iCustid = 0;
                int Id = 0;
                {
                    iCustid = Convert.ToInt32(ddlcustomerID.SelectedValue);
                }
                //if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 1) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 0) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 3) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 5) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 6))
                {
                    Id = 0;
                }

                string narration = string.Empty;
                string inara = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                    if (ddldef.SelectedItem.Text == "Select Product")
                    {

                    }
                    else
                    {
                        DataSet narat = objBs.insertnarration(Convert.ToString(ddldef.SelectedValue));
                        inara = Convert.ToString(narat.Tables[0].Rows[0]["category"].ToString());
                        if (narration == "")
                        {
                            narration = "Sales Bill No :" + txtbillno.Text;
                            narration = narration + " (" + inara.ToString();
                        }
                        else
                        {
                            narration = narration + "," + inara.ToString();
                        }
                    }
                }
                narration = narration + ")";
                string billtype = string.Empty;
                if (rbtype.SelectedValue == "1")
                {
                    billtype = "S";
                }



                int iStat = objBs.Insert_SalesNew(sTableName, "tblDayBook_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(lblUserID.Text), txtbillno.Text, billldate, Convert.ToInt32(iCustid), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtdiscount.Text), Convert.ToInt32("0"), Convert.ToInt32(1), Convert.ToDouble("0"), Convert.ToInt32(ddlPayMode.SelectedValue), narration, Id, txtCheque.Text, Convert.ToInt32(ddlbook.SelectedValue), txtvouno.Text, vouchdate, Convert.ToInt32(ddlvouchertype.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(ddlCash.SelectedValue), txtlrno.Text, txtTransport.Text, txtdestination.Text, txtorderno.Text, Convert.ToInt32("0"), txtpackingslip.Text, Convert.ToString(txtDisc.Text), lrrdate, dueedate, orddate, txtnopackage.Text, txtFreight.Text, txtLU.Text, txtroundoff.Text, txtadd.Text, txtdiscountamount.Text, TextBox2.Text, txtbilledby.Text, txtmobileno.Text, txtlrnoupd.Text, txttransportupd.Text, ttxlorryno.Text, cmpnameyear.Text, yearss.Text, chkcertificate.Checked, Convert.ToDouble(txtcgst.Text), Convert.ToDouble(txtsgst.Text), Convert.ToDouble(txtigst.Text), Convert.ToInt32(drpSalesType.SelectedValue), txtthrough.Text, txtpack.Text, txtchecking.Text, txtrecheck.Text, txtShipaddress.Text, billtype, lblprefix.Text,lblsufix.Text);

                #region

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {


                    TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                    DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");

                    int idef = Convert.ToInt32(ddldef.SelectedValue);
                    if (ddldef.SelectedItem.Text == "Select Product")
                    {

                    }
                    else
                    {


                        DropDownList drpItem = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");

                        TextBox txtDesc = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDesc");

                        int icat = Convert.ToInt32(0);





                        TextBox Qty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtQty");
                        double dQty = Convert.ToDouble(Qty.Text);


                        int dorno = Convert.ToInt32(orderno.Text);


                        TextBox Dis = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDiscount");
                        double dDis = Convert.ToDouble(Dis.Text);
                        TextBox Rate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                        double DRate = Convert.ToDouble(Rate.Text);
                        TextBox Tax = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTax");
                        double DTax = Convert.ToDouble(Tax.Text);

                        TextBox Amount = (TextBox)gvcustomerorder.Rows[i].FindControl("txtAmount");
                        double dAmount = Convert.ToDouble(Amount.Text);

                        double dmeter = 0;


                        iStatus2 = objBs.Insert_TransSales("tblTransSales_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty), Convert.ToDouble(DRate), Convert.ToDouble(dDis), Convert.ToDouble(dAmount), Convert.ToInt32(0), Convert.ToDouble(DTax), txtdiscount.Text, txtgrandtotal.Text, txtTaxamt.Text, totqty.Text, dmeter, dorno, "", "", "", "", txtDesc.Text);


                    }

                }

                #endregion

                Response.Redirect("cashsales.aspx");
            }

            #endregion
            #region
            else if (btnadd.Text == "Update")
            {

                int iStatus2 = 0, iStatus3 = 0, iStatus4 = 0, iStatus5 = 0, iStockSuccess = 0;
                int udSize46 = 0, udSize28 = 0, udSize30 = 0, udSize32 = 0, udSizes = 0, udSizem = 0, udSizel = 0, udSizexl = 0, udSizexxl = 0, udSize3xl = 0, udSize4xl = 0, udSizexs = 0, udSize50 = 0, udSize34 = 0, udSize36 = 0, udSize38 = 0, udSize40 = 0, udSize42 = 0, udSize44 = 0, udSize20 = 0, udSize22 = 0, udSize24 = 0, udSize26 = 0;
                double udSizemeter = 0;


                string iSalesID = Request.QueryString.Get("iSalesID");
                iDealer = Request.QueryString.Get("iDealer");


                if (iSalesID != null)
                {

                    if (txtgrandtotal.Text != "")
                    {
                        int isalesid = Convert.ToInt32(txtbillno.Text);
                        int oldsalesid = 0;



                        int iTransDelete = objBs.deletesalseordervalues("tblTransSales_" + sTableName, iSalesID);
                        try
                        {
                            int iCustid = 0;
                            int Id = 0;

                            iCustid = Convert.ToInt32(ddlcustomerID.SelectedValue);

                            if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 1) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 0) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 3) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 5) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 6))
                            {
                                Id = 0;
                            }
                            else
                            {

                            }
                            string narration = string.Empty;
                            //string inara = string.Empty;
                            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                            //{

                            //    DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");

                            //    if (ddldef.SelectedValue == "Select Product")
                            //    {

                            //    }
                            //    else
                            //    {
                            //        DataSet narat = objBs.insertnarration(Convert.ToString(ddldef.SelectedValue));
                            //        inara = Convert.ToString(narat.Tables[0].Rows[0]["category"].ToString());
                            //        if (narration == "")
                            //        {
                            //            narration = "Sales Bill No :" + txtbillno.Text;
                            //            narration = narration + " (" + inara.ToString();
                            //        }
                            //        else
                            //        {
                            //            narration = narration + "," + inara.ToString();
                            //        }
                            //    }
                            //}
                            //narration = narration + ")";

                            int iStat = objBs.Update_Sales(sTableName, "tblDayBook_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(lblUserID.Text), txtbillno.Text, billldate, Convert.ToInt32(iCustid), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtdiscount.Text), Convert.ToInt32("0"), Convert.ToInt32(1), Convert.ToDouble("0"), Convert.ToInt32(ddlPayMode.SelectedValue), narration, Id, txtCheque.Text, Convert.ToInt32(ddlbook.SelectedValue), txtvouno.Text, vouchdate, Convert.ToInt32(ddlvouchertype.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(ddlCash.SelectedValue), txtlrno.Text, txtTransport.Text, txtdestination.Text, txtorderno.Text, Convert.ToInt32("0"), txtpackingslip.Text, Convert.ToString(txtDisc.Text), lrrdate, dueedate, orddate, txtnopackage.Text, iSalesID, txtFreight.Text, txtLU.Text, txtroundoff.Text, txtadd.Text, txtdiscountamount.Text, TextBox2.Text, txtbilledby.Text, txtmobileno.Text, txtlrnoupd.Text, txttransportupd.Text, ttxlorryno.Text, chkcertificate.Checked, Convert.ToDouble(txtcgst.Text), Convert.ToDouble(txtsgst.Text), Convert.ToDouble(txtigst.Text), Convert.ToInt32(0), txtthrough.Text, txtpack.Text, txtchecking.Text, txtrecheck.Text);

                            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                            {

                                TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");


                                DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");

                                TextBox txtDesc = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDesc");
                                if (ddldef.SelectedItem.Text == "Select Product")
                                {

                                }
                                else
                                {
                                    int icat = Convert.ToInt32(0);
                                    int idef = Convert.ToInt32(ddldef.SelectedValue);
                                    int dSize46 = 0, dSize28 = 0, dSize30 = 0, dSize32 = 0, dSizes = 0, dSizem = 0, dSizel = 0, dSizexl = 0, dSizexxl = 0, dSize3xl = 0, dSize4xl = 0, dSizexs = 0, dSize50 = 0, dSize34 = 0, dSize36 = 0, dSize38 = 0, dSize40 = 0, dSize42 = 0, dSize44 = 0, dSize20 = 0, dSize22 = 0, dSize24 = 0, dSize26 = 0;
                                    double dSizemeter = 0;

                                    TextBox Qty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtQty");
                                    double dQty = Convert.ToDouble(Qty.Text);
                                    int dorno = Convert.ToInt32(orderno.Text);
                                    TextBox Dis = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDiscount");
                                    double dDis = Convert.ToDouble(Dis.Text);
                                    TextBox Rate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                                    double DRate = Convert.ToDouble(Rate.Text);
                                    TextBox Tax = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTax");
                                    double DTax = Convert.ToDouble(Tax.Text);
                                    TextBox Amount = (TextBox)gvcustomerorder.Rows[i].FindControl("txtAmount");
                                    double dAmount = Convert.ToDouble(Amount.Text);
                                    double dmeter = 0;
                                    iStatus2 = objBs.Update_TransSales("tblTransSales_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty), Convert.ToDouble(DRate), Convert.ToDouble(dDis), Convert.ToDouble(dAmount), Convert.ToInt32(0), Convert.ToDouble(DTax), txtdiscount.Text, txtgrandtotal.Text, txtTaxamt.Text, totqty.Text, dmeter, dorno, iSalesID, "", "", "", "", dSize28, dSize30, dSize32, dSize34, dSize36, dSize38, dSize40, dSize42, dSize44, dSize46, dSizemeter, "0", dSize20, dSize22, dSize24, dSize26, dSizes, dSizem, dSizel, dSizexl, dSizexxl, dSize3xl, dSize4xl, dSizexs, dSize50, "0", txtDesc.Text);

                                }

                            }


                            //  System.Threading.Thread.Sleep(3000);

                            Response.Redirect("cashsales.aspx");
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                        }


                    }

                }

            }
            #endregion

        }


        private int UpdateStockAvailable(int iSubCat, int iQty, String Sizeid)
        {
            double iAQty = 0;
            int iSuccess = 0;


            //DataSet dsStock = objBs.GetStockDetails1(iSubCat, "tblStock_" + sTableName, Sizeid);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //double iRemQty = iAQty + iQty;
            //iSuccess = objBs.updateStocknew(iRemQty, iSubCat, "tblStock_" + sTableName, Sizeid);


            return iSuccess;
        }
        //protected void textchanged(object sender, EventArgs e)
        //{

        //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
        //    {
        //        TextBox barcode = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbarcode");

        //       // TextBox barcode = (TextBox)row.FindControl("txtbarcode");
        //        if (barcode.Text != "")
        //        {
        //            string bar = barcode.Text;

        //            DataSet ds = objBs.getbarcode(bar);



        //        }
        //    }
        //  //  Add_Click(sender, e);
        //}


        protected void ProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {

            //int iq = 1;
            //int ii = 1;
            //string itemc = string.Empty;
            //string itemcd = string.Empty;
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txti = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");

            //        TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //        itemc = txti.Text;


            //        if ((itemc == null) || (itemc == ""))
            //        {
            //        }
            //        else
            //        {
            //            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
            //            {
            //                DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ProductCode");
            //                if (txt1.Text == "")
            //                {
            //                }
            //                else
            //                {

            //                    if (ii == iq)
            //                    {
            //                    }
            //                    else
            //                    {
            //                        if (itemc == txt1.Text)
            //                        {
            //                            //itemcd = txti.SelectedItem.Text;
            //                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
            //                            //txt1.Focus();
            //                            //return;

            //                        }
            //                    }
            //                    ii = ii + 1;
            //                }
            //            }
            //        }
            //        iq = iq + 1;
            //        ii = 1;

            //        //DataSet dsStock = new DataSet();

            //        //if ((txtktt.Text == "") && (txtkt.Text == "") && (txttk.Text == "") || (txtkttt.Text == ""))
            //        //{
            //        //    dsStock = objBs.GetStockDetails(Convert.ToInt32(txt.SelectedValue));

            //        //    if (dsStock.Tables[0].Rows.Count > 0)
            //        //    {
            //        //        txttk.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
            //        //        txtktt.Text = dsStock.Tables[0].Rows[0]["Available_Qty"].ToString();

            //        //        DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue));
            //        //        txtkt.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();

            //        //        txtkttt.Text = "0";
            //        //    }
            //        //}
            //    }
            //}


            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //if (ViewState["CurrentTable1"] != null)
            //{

            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

            //        DropDownList Def = (DropDownList)row.FindControl("drpItem");

            //        TextBox qty = (TextBox)row.FindControl("txtQty");

            //        DropDownList procode = (DropDownList)row.FindControl("ProductCode");

            //        if (procode.SelectedItem.Text != "Select Product Code")
            //        {

            //            DataSet dsCategory1 = objBs.selectProduct(Convert.ToInt32(procode.SelectedValue), sTableName);
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                drpCategory.Items.Clear();
            //                drpCategory.DataSource = dsCategory1.Tables[0];
            //                drpCategory.DataTextField = "categoryname";
            //                drpCategory.DataValueField = "categoryid";
            //                drpCategory.DataBind();
            //                //drpCategory.Items.Insert(0, "Select Category");

            //            }
            //            else
            //            {
            //                drpCategory.Items.Clear();
            //                drpCategory.Items.Insert(0, "Select Category");
            //            }
            //        }
            //        else
            //        {
            //        }

            //        if (procode.SelectedItem.Text != "Select Product Code")
            //        {

            //            DataSet dsCategory1 = objBs.selectProduct(Convert.ToInt32(procode.SelectedValue), sTableName);
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                DataSet dst = new DataSet();
            //                dst = objBs.selectcategoryalldecriptionbranch(sTableName, "0,1");
            //                Def.Items.Clear();
            //                Def.DataSource = dst.Tables[0];
            //                Def.DataTextField = "serial_NO";
            //                Def.DataValueField = "categoryuserid";
            //                Def.DataBind();

            //                Def.SelectedValue = dsCategory1.Tables[0].Rows[0]["CategoryUserID"].ToString();

            //            }
            //            else
            //            {
            //                Def.Items.Clear();
            //                Def.Items.Insert(0, "Select Product");
            //            }
            //        }
            //        else
            //        {
            //        }

            //        qty.Focus();
            //    }
            //}


            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
            //TextBox txt = (TextBox)row.FindControl("txtDiscount");
            //TextBox txtTax = (TextBox)row.FindControl("txtTax");
            //DropDownList Defitem = (DropDownList)row.FindControl("drpItem");
            //DropDownList cate = (DropDownList)row.FindControl("drpCategory");
            //DropDownList ProductCode = (DropDownList)row.FindControl("ProductCode");
            //TextBox txtQty = (TextBox)row.FindControl("txtStock");
            //DataSet dsStock = new DataSet();

            //if (ProductCode.SelectedItem.Text != "Select Product Code")
            //{
            //    dsStock = objBs.GetStockDetails(Convert.ToInt32(ProductCode.SelectedValue), "tblStock_" + sTableName);

            //    if (dsStock.Tables[0].Rows.Count > 0)
            //    {
            //        DataSet dsCategory = objBs.GetTax(Convert.ToInt32(ProductCode.SelectedValue), sTableName);

            //        var Itx = Convert.ToDecimal(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
            //        txtTax.Text = Itx.ToString();

            //        decimal rattee = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Unitprice"]);
            //        txtRate.Text = Decimal.Round(rattee, 2).ToString("f2");
            //        //Decimal Irate = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString());
            //        //txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");


            //        //if (ddlcustomerID.SelectedValue != "Select Customer")
            //        //{
            //        //    DataSet dsStockd = objBs.getsprice1(ddlcustomerID.SelectedValue, ProductCode.SelectedValue, sTableName);
            //        //    if (dsStockd.Tables[0].Rows.Count > 0)
            //        //    {
            //        //        decimal rattee = Convert.ToDecimal(dsStockd.Tables[0].Rows[0]["Price"]);
            //        //        txtRate.Text = Decimal.Round(rattee, 2).ToString("f2");
            //        //    }
            //        //    else
            //        //    {
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    txtRate.Text = "0.00";
            //        //}


            //        decimal sQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
            //        txtQty.Text = sQty.ToString("f2");
            //        cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

            //        txt.Text = "0";

            //        string value = ProductCode.SelectedValue;
            //        DataSet ds = objBs.itemhistorypopup(sTableName, value);
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            txtitemhis.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
            //        }
            //        else
            //        {
            //            txtitemhis.Text = "";
            //        }


            //        string cust = ddlcustomerID.SelectedValue;
            //        if (cust == "Select Customer")
            //        {
            //        }
            //        else
            //        {
            //            DataSet ds1 = objBs.custhistorypopup(sTableName, value, cust);
            //            if (ds1.Tables[0].Rows.Count > 0)
            //            {
            //                txtcusthis.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
            //            }
            //            else
            //            {
            //                txtcusthis.Text = "";
            //            }
            //        }

            //        // txtTamt5.Text = dsCategory.Tables[0].Rows[0]["Meter1"].ToString();
            //    }
            //}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    txtno.Text = Convert.ToString(i + 1);
            //}



            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string iSalesID = Request.QueryString.Get("iSalesID");
            Response.Redirect("Print_Sales_Invoice_PackingNEW.aspx?iSalesID=" + iSalesID);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string iSalesID = Request.QueryString.Get("iSalesID");

            //DataSet salescertid = objBs.upsalescert(Convert.ToInt32(txtbillno.Text), sTableName);
            // int certid = Convert.ToInt32(salescertid.Tables[0].Rows[0]["SaleCertificateId"].ToString());

            int iSucess = objBs.DeleteSales("tblSales_" + sTableName, iSalesID, "tblDayBook_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, btnDelete.Text, sTableName);

            int isalesid = Convert.ToInt32(iSalesID);



            // int certi = objBs.delcerti(certid);
            // int trnscerti = objBs.deltrnscerti(certid);
            int iStatus2 = 0, iStatus3 = 0, iStatus4 = 0, iStatus5 = 0, iStockSuccess = 0;
            int udSize46 = 0, udSize28 = 0, udSize30 = 0, udSize32 = 0, udSizes = 0, udSizem = 0, udSizel = 0, udSizexl = 0, udSizexxl = 0, udSize3xl = 0, udSize4xl = 0, udSizexs = 0, udSize50 = 0, udSize34 = 0, udSize36 = 0, udSize38 = 0, udSize40 = 0, udSize42 = 0, udSize44 = 0, udSize20 = 0, udSize22 = 0, udSize24 = 0, udSize26 = 0;
            double udSizemeter = 0;

            int iTransDelete = objBs.DeleteTransSales("tblTransSales_" + sTableName, iSalesID);

            Response.Redirect("salesgrid.aspx");

        }
        private int UpdateEditStock(int iCat, int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            //if (sTableName == "admin")
            //{
            DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            if (dsStock.Tables[0].Rows.Count > 0)
            {
                iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            }
            int iInsQty = iAQty + iQty;
            iSuccess = objBs.updateSalesStock(iInsQty, iCat, iSubCat, "tblStock_" + sTableName);

            return iSuccess;
        }


        protected void ddlSalestype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlSalestype.SelectedValue == "1")
            //{
            //    gvcustomerorder.Columns[5].Visible = false;
            //    gvcustomerorder.Columns[6].Visible = false;
            //    gvcustomerorder.Columns[7].Visible = false;
            //    gvcustomerorder.Columns[8].Visible = false;
            //    gvcustomerorder.Columns[9].Visible = false;
            //    gvcustomerorder.Columns[10].Visible = false;

            //}
            //else if (ddlSalestype.SelectedValue == "2")
            //{
            //    gvcustomerorder.Columns[5].Visible = true;
            //    gvcustomerorder.Columns[6].Visible = true;
            //    gvcustomerorder.Columns[7].Visible = true;
            //    gvcustomerorder.Columns[8].Visible = true;
            //    gvcustomerorder.Columns[9].Visible = true;
            //    gvcustomerorder.Columns[10].Visible = true;
            //}
        }
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonAdd1_Click(sender, e);
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;
            double tottqty = 0;
            double mettt = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                //   TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                if (txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
                {

                }
                else
                {

                    double iNetAmount1 = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount1 = Convert.ToDouble(iNetAmount1) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount1 = Convert.ToDouble(iNetAmount1) - Discount1;
                    double tx1 = Convert.ToDouble(DiscountAmount1) * Convert.ToDouble(txtkt.Text) / 100;
                    double total1 = tx1 + DiscountAmount1;


                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double total = tx + DiscountAmount;

                    txtkttt.Text = string.Format("{0:N2}", total1);
                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    distotal = distotal + Discount;
                    tottqty = tottqty + Convert.ToDouble(txtttk.Text);
                    txttk.Focus();

                }

            }
            double dFreight = 0;
            double dLU = 0;
            double sumLUFreight = 0;
            if (txtFreight.Text.Trim() != "")
            {
                dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            }
            if (txtLU.Text.Trim() != "")
            {
                dLU = Convert.ToDouble(txtLU.Text.Trim());
            }
            sumLUFreight = dFreight + dLU;
            txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            txtTaxamt.Text = string.Format("{0:N2}", tax);
            txtdiscountamount.Text = string.Format("{0:N2}", distotal);

            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtTaxamt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtTaxamt.Text;

            }

            totqty.Text = tottqty.ToString();
            totmeter.Text = string.Format("{0:N2}", mettt);
            txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text));
            double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            if (roundoff > 0.5)
            {
                r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            }
            else
            {
                r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            }
            txtroundoff.Text = Convert.ToString(r);




            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    int cnt = gvcustomerorder.Rows.Count;
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    if (vLoop >= 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            //        oldtxttk.Focus();
            //    }
            //    int tot = cnt - vLoop;
            //    if (tot == 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            //        if (oldtxttk.Text == "0.00")
            //        {
            //            oldtxttk.Text = ".00";
            //            oldtxttk.Focus();
            //        }
            //        else
            //        {
            //            oldtxttk.Focus();
            //        }
            //    }
            //  //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            //}

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
            granddiscount(sender, e);

        }


        protected void txt34_TextChanged(object sender, EventArgs e)
        {
            ButtonAdd1_Click(sender, e);

            calc_Qty();
            txtQty_TextChanged(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt36 = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt36");
            if (txt36.Text == "0")
            {
                txt36.Text = "";
            }
            txt36.Focus();

        }

        protected void txt36_TextChanged(object sender, EventArgs e)
        {
            calc_Qty();
            txtQty_TextChanged(sender, e);
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt38 = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt38");
            if (txt38.Text == "0")
            {
                txt38.Text = "";
            }
            txt38.Focus();
        }


        protected void txt38_TextChanged(object sender, EventArgs e)
        {
            calc_Qty();
            txtQty_TextChanged(sender, e);
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt40 = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt40");
            if (txt40.Text == "0")
            {
                txt40.Text = "";
            }
            txt40.Focus();
        }


        protected void txt40_TextChanged(object sender, EventArgs e)
        {
            calc_Qty();
            txtQty_TextChanged(sender, e);
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt42 = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt42");
            if (txt42.Text == "0")
            {
                txt42.Text = "";
            }
            txt42.Focus();
        }


        protected void txt42_TextChanged(object sender, EventArgs e)
        {
            calc_Qty();
            txtQty_TextChanged(sender, e);
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt44 = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt44");
            if (txt44.Text == "0")
            {
                txt44.Text = "";
            }
            txt44.Focus();
        }


        protected void txt44_TextChanged(object sender, EventArgs e)
        {
            calc_Qty();
            txtQty_TextChanged(sender, e);
        }

        protected void calc_Qty()
        {


        }


        #region New Sales Chnages
        protected void btnaddqtynew_Click(object sender, EventArgs e)
        {

        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
        }

        protected void Itemname_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        protected void ItemSegment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

    }
}