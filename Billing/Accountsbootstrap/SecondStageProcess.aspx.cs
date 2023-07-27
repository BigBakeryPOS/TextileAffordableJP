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
    public partial class SecondStageProcess : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iDealer = "";
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
                btnPrint.Visible = false;
                btnDelete.Visible = false;
                //   txtbillno.Enabled = true;

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                //DataSet ds = objBs.SalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text);
                //   DataSet ds = objBs.getSalesBillno(Convert.ToInt32(ddlPayMode.SelectedValue), btnadd.Text, "Sales");
                DataSet ds = new DataSet();
                ds = objBs.getmaaxBillnoforseocondstage();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtinvno.Text = ds.Tables[0].Rows[0]["billId"].ToString();
                    txtdelidate.Text = dtaa;


                    //// int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                    ////if (ds.Tables[0].Rows[0]["BillNo"].ToString() == "")
                    ////{
                    ////    txtbillno.Text = "1";
                    ////    txtpackingslip.Text = "1";
                    ////    txtbillcheck(sender, e);
                    ////}
                    ////else
                    ////{
                    ////    txtbillno.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                    ////    txtpackingslip.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                    ////    txtbillcheck(sender, e);

                    ////}




                    FirstGridViewRow();
                    FirstGridViewRow1();
                    //txtvoudate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //txtlrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //txtduedate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //txtorderdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //txtdate1.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    //DataSet dsCategory = new DataSet();

                    //ddlBank.Enabled = false;
                    //txtCheque.Enabled = false;
                    //DataSet dsup = objBs.getnewsupplierforfab();
                    //if (dsup.Tables[0].Rows.Count > 0)
                    //{

                    //    drpsupplier.DataSource = dsup.Tables[0];
                    //    drpsupplier.DataTextField = "LEdgerName";
                    //    drpsupplier.DataValueField = "LedgerID";
                    //    drpsupplier.DataBind();
                    //    drpsupplier.Items.Insert(0, "Select Supplier");
                    //}

                    //DataSet dst = objBs.hrmgridview();
                    //if (dst != null)
                    //{
                    //    if (dst.Tables[0].Rows.Count > 0)
                    //    {
                    //        drpchecked.DataSource = dst.Tables[0];
                    //        drpchecked.DataTextField = "Name";
                    //        drpchecked.DataValueField = "Employee_Id";
                    //        drpchecked.DataBind();
                    //        drpchecked.Items.Insert(0, "Select Employee Name");
                    //    }
                    //}

                    //DataSet dswidth = objBs.GetWidth();
                    //if (dswidth != null)
                    //{
                    //    if (dswidth.Tables[0].Rows.Count > 0)
                    //    {
                    //        drpwidth.DataSource = dswidth.Tables[0];
                    //        drpwidth.DataTextField = "Width";
                    //        drpwidth.DataValueField = "WidthID";
                    //        drpwidth.DataBind();
                    //        drpwidth.Items.Insert(0, "Select Width");
                    //    }
                    //}

                    //txtregdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //txtinvdate.Text = DateTime.Now.ToString("dd/MM/yyyy");



                    //dagent = objBs.getagentlist(sTableName);
                    //if (dagent.Tables[0].Rows.Count > 0)
                    //{

                    //    ddlRepname.DataSource = dagent.Tables[0];
                    //    ddlRepname.DataTextField = "LEdgerName";
                    //    ddlRepname.DataValueField = "LedgerID";
                    //    ddlRepname.DataBind();
                    //    ddlRepname.Items.Insert(0, "Select Agent");
                    //}
                    //// DataSet dCnt = objBs.GetContact();
                    ////bblbillto.DataSource = dCnt.Tables[0];
                    ////bblbillto.DataTextField = "ContactType";
                    ////bblbillto.DataValueField = "ContactID";
                    ////bblbillto.DataBind();
                    ////DataSet dsCust = objBs.GetCustName(Convert.ToInt32(bblbillto.SelectedValue));
                    ////if (dsCust.Tables[0].Rows.Count > 0)
                    ////{
                    ////    ddlcustomerID.DataSource = dsCust.Tables[0];
                    ////    ddlcustomerID.DataTextField = "LedgerName";
                    ////    ddlcustomerID.DataValueField = "LedgerID";
                    ////    ddlcustomerID.DataBind();
                    ////    ddlcustomerID.Items.Insert(0, "Select Customer");

                    ////    //ddlRepname.DataSource = dsCust.Tables[0];
                    ////    //ddlRepname.DataTextField = "LedgerName";
                    ////    //ddlRepname.DataValueField = "LedgerID";
                    ////    //ddlRepname.DataBind();
                    ////    //ddlRepname.Items.Insert(0, "Select Rep. Name");
                    ////}
                    //string iSalesID = Request.QueryString.Get("iSalesID");
                    //iDealer = Request.QueryString.Get("iDealer");
                    //if (iSalesID != null)
                    //{

                    //    DataSet dContact = objBs.checkContack(Convert.ToInt32(iSalesID), Convert.ToInt32(lblUserID.Text), "tblsales_" + sTableName);

                    //    //int icusttype = Convert.ToInt32(dContact.Tables[0].Rows[0]["ContactID"].ToString());

                    //    ////if (icusttype == 2)
                    //    //btnadd.Visible = false;

                    //    DataSet ds1 = objBs.CustomerSalesGirdget(iSalesID, "tblSales_" + sTableName, sTableName);
                    //    if (ds1.Tables[0].Rows.Count > 0)
                    //    {
                    //        //bblbillto.SelectedValue = ds1.Tables[0].Rows[0]["ContactTypeID"].ToString();
                    //        DataSet dsCustomer = objBs.GetCustNamenew(sTableName);
                    //        if (dsCustomer.Tables[0].Rows.Count > 0)
                    //        {
                    //            ddlcustomerID.DataSource = dsCustomer.Tables[0];
                    //            ddlcustomerID.DataTextField = "LedgerName";
                    //            ddlcustomerID.DataValueField = "LedgerID";
                    //            ddlcustomerID.DataBind();
                    //            ddlcustomerID.Items.Insert(0, "Select Customer");

                    //            // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();

                    //        }
                    //        btnadd.Text = "Update";

                    //        ddlbook.Enabled = false;
                    //        btnPrint.Visible = true;
                    //        btnDelete.Visible = true;
                    //        txtbillno.Enabled = false;


                    //        //txtcuscode.Text = ds1.Tables[0].Rows[0]["CustomerID"].ToString();
                    //        txtbillno.Text = ds1.Tables[0].Rows[0]["BillNo"].ToString();
                    //        txtdate1.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");
                    //        //ddlcustomerID.SelectedItem.Text = ds1.Tables[0].Rows[0]["CustomerName"].ToString();
                    //        ddlcustomerID.SelectedValue = ds1.Tables[0].Rows[0]["CustomerID1"].ToString();


                    //        DataSet dsCustDet = objBs.GetCustomerDetailsforsales(Convert.ToInt32(ddlcustomerID.SelectedValue), sTableName);
                    //        string area = string.Empty;

                    //        if (dsCustDet.Tables[0].Rows.Count > 0)
                    //        {
                    //            //txtcustomername.Text = dsCustDet.Tables[0].Rows[0]["LedgerName"].ToString();
                    //            //txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                    //            //txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                    //            ////txtarea.Text = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                    //            //txtpincode.Text = dsCustDet.Tables[0].Rows[0]["Pincode"].ToString();
                    //            //txtcuscode.Text = dsCustDet.Tables[0].Rows[0]["LedgerID"].ToString();
                    //            //txtmobileno.Text = dsCustDet.Tables[0].Rows[0]["MobileNo"].ToString();
                    //            int agent = Convert.ToInt32(dsCustDet.Tables[0].Rows[0]["Agentid"]);
                    //            // txtTransport.Text = dsCustDet.Tables[0].Rows[0]["Transport"].ToString();

                    //            DataSet dagent1 = objBs.getcustomeragent(agent, sTableName);
                    //            if (dagent.Tables[0].Rows.Count > 0)
                    //            {

                    //                ddlRepname.DataSource = dagent1.Tables[0];
                    //                ddlRepname.DataTextField = "LEdgerName";
                    //                ddlRepname.DataValueField = "LedgerID";
                    //                ddlRepname.DataBind();
                    //                // ddlcustomerID.Items.Insert(0, "Select Agent");
                    //            }
                    //            ddlRepname.SelectedValue = dsCustDet.Tables[0].Rows[0]["Agentid"].ToString();


                    //            //txtcustomername.Text = ds1.Tables[0].Rows[0]["CustomerName"].ToString();
                    //            txtaddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
                    //            area = dsCustDet.Tables[0].Rows[0]["State1"].ToString();
                    //            txtcity.Text = dsCustDet.Tables[0].Rows[0]["City1"].ToString();
                    //            txtpincode.Text = ds1.Tables[0].Rows[0]["pincode"].ToString();
                    //            txtmobileno.Text = ds1.Tables[0].Rows[0]["MobileNo"].ToString();
                    //            ddlbook.SelectedValue = ds1.Tables[0].Rows[0]["Book"].ToString();
                    //            txtvouno.Text = ds1.Tables[0].Rows[0]["vouno"].ToString();
                    //            txtvoudate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");
                    //            ddlvouchertype.SelectedValue = ds1.Tables[0].Rows[0]["vouchertype"].ToString();
                    //            ddlProvince.SelectedValue = ds1.Tables[0].Rows[0]["province"].ToString();
                    //            ddlCash.SelectedValue = ds1.Tables[0].Rows[0]["cashac"].ToString();
                    //            txtlrno.Text = ds1.Tables[0].Rows[0]["lrno"].ToString();
                    //            txtTransport.Text = ds1.Tables[0].Rows[0]["transport"].ToString();
                    //            txtdestination.Text = ds1.Tables[0].Rows[0]["destination"].ToString();
                    //            txtorderno.Text = ds1.Tables[0].Rows[0]["orderno"].ToString();
                    //            ddlRepname.SelectedValue = ds1.Tables[0].Rows[0]["repname"].ToString();
                    //            txtpackingslip.Text = ds1.Tables[0].Rows[0]["packingno"].ToString();
                    //            //txtDisc.Text = ds1.Tables[0].Rows[0]["discper"].ToString();
                    //            txtlrdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["lrdate"]).ToString("dd/MM/yyyy");
                    //            txtduedate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["duedate"]).ToString("dd/MM/yyyy");
                    //            txtorderdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["orderdate"]).ToString("dd/MM/yyyy");
                    //            txtnopackage.Text = ds1.Tables[0].Rows[0]["noofpackage"].ToString();
                    //            txtTransport.Text = ds1.Tables[0].Rows[0]["Transport1"].ToString();
                    //            txtFreight.Text = ds1.Tables[0].Rows[0]["Freight"].ToString();
                    //            txtLU.Text = ds1.Tables[0].Rows[0]["Loading"].ToString();
                    //            txtroundoff.Text = ds1.Tables[0].Rows[0]["Roundoff"].ToString();
                    //            lblarea.Text = dsCustDet.Tables[0].Rows[0]["rtozone1"].ToString();

                    //            //   txtnopackage.Text = ds1.Tables[0].Rows[0]["noofpackage"].ToString();
                    //            //   Itot = Convert.ToDecimal(ds1.Tables[0].Rows[0]["Total"].ToString());
                    //            //  txttotal.Text = Decimal.Round(Itot, 2).ToString("f2");

                    //            txtNarration.Text = ds1.Tables[0].Rows[0]["Narration"].ToString();
                    //            txtCheque.Text = ds1.Tables[0].Rows[0]["ChequeNo"].ToString();
                    //            ddlBank.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["Bank"]).ToString();

                    //            ddlPayMode.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["Paymode"]).ToString();

                    //            lbladdress.Text = txtaddress.Text + " ," + txtcity.Text + " ," + area + ", " + txtmobileno.Text + " , " + txtpincode.Text;
                    //            txtadd.Text = ds1.Tables[0].Rows[0]["CashAddress"].ToString();
                    //            if (txtadd.Text == "")
                    //            {
                    //                txtadd.Text = txtaddress.Text + " ," + txtcity.Text + " ," + area + ", " + txtmobileno.Text + " , " + txtpincode.Text;
                    //            }
                    //            else
                    //            {

                    //            }

                    //            ddlcustomerID.Focus();

                    //            //Itax = Convert.ToDecimal(ds1.Tables[0].Rows[0]["Tax"].ToString());
                    //            //txttax.Text = Decimal.Round(Itax, 2).ToString("f2");

                    //            //Idisc = Convert.ToDecimal(ds1.Tables[0].Rows[0]["Discount"].ToString());
                    //            //txtdiscount.Text = Decimal.Round(Idisc, 2).ToString("f2");

                    //            //Igrandtot = Convert.ToDecimal(ds1.Tables[0].Rows[0]["NetAmount"].ToString());
                    //            //txtgrandtotal.Text = Decimal.Round(Igrandtot, 2).ToString("f2");

                    //            //Igrandtot = Convert.ToDecimal(ds1.Tables[0].Rows[0]["NetAmount"].ToString());
                    //            //txtgrandtotal.Text = Decimal.Round(Igrandtot, 2).ToString("f2");

                    //            //iAdvance = Convert.ToDecimal(ds1.Tables[0].Rows[0]["Advance"].ToString());
                    //            //txtadvance.Text = Decimal.Round(iAdvance, 2).ToString("f2");
                    //            //ddlcategory.Text = ds1.Tables[0].Rows[0]["CustomerName"].ToString();

                    //        }

                    //        //if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
                    //        //{
                    //        //    //rowAgainst.Visible = true;

                    //        //    DataSet Data = new DataSet();
                    //        //    Data = objBs.GetReceiptBill(iSalesID, "tblReceipt_" + sTableName, "tblDayBook_" + sTableName, "tblTransReceipt_" + sTableName);

                    //        //    int gg = 1;
                    //        //    if (Data.Tables[0].Rows.Count > 0)
                    //        //    {
                    //        //        foreach (DataRow dr in Data.Tables[0].Rows)
                    //        //        {
                    //        //            if (dr["Paymodeid"].ToString() == "1")
                    //        //            {
                    //        //                txtAgainstAmount2.Text = dr["Amount"].ToString();
                    //        //            }
                    //        //            else
                    //        //            {
                    //        //                if (gg == 1)
                    //        //                {
                    //        //                    ddlAgainst.SelectedValue = Convert.ToInt32(dr["BankName"]).ToString();

                    //        //                    txtAgainstAmount.Text = dr["Amount"].ToString();
                    //        //                    txtchequedd.Text = dr["ChequeNo"].ToString();
                    //        //                }
                    //        //                if (gg == 2)
                    //        //                {
                    //        //                    ddlAgainst1.SelectedValue = Convert.ToInt32(dr["BankName"]).ToString();

                    //        //                    txtAgainstAmount1.Text = dr["Amount"].ToString();
                    //        //                    txtchequedd1.Text = dr["ChequeNo"].ToString();

                    //        //                }
                    //        //                gg = gg + 1;
                    //        //            }
                    //        //        }
                    //        //    }
                    //        //}
                    //        //else
                    //        //{
                    //        //    //rowAgainst.Visible = false;
                    //        //}

                    //        //Retreive Sales Trans Details
                    //        DataSet ds2 = objBs.GetUpdateSalesTrans(iSalesID, "tblTransSales_" + sTableName);
                    //        {
                    //            if (ds2.Tables[0].Rows.Count > 0)
                    //            {
                    //                int Tpo = ds2.Tables[0].Rows.Count;


                    //                DataTable dttt;
                    //                DataRow drNew;
                    //                DataColumn dct;
                    //                DataSet dstd = new DataSet();
                    //                dttt = new DataTable();

                    //                dct = new DataColumn("OrderNo");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Category");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("ProductCode");
                    //                dttt.Columns.Add(dct);


                    //                dct = new DataColumn("Product");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Stock");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Refno");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Cerno");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Qty");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Rate");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Discount");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Tax");
                    //                dttt.Columns.Add(dct);

                    //                dct = new DataColumn("Amount");
                    //                dttt.Columns.Add(dct);

                    //                dstd.Tables.Add(dttt);

                    //                foreach (DataRow dr in ds2.Tables[0].Rows)
                    //                {
                    //                    int dpro = Convert.ToInt32(dr["SubCategoryID"]);
                    //                    DataSet dd = objBs.Getcurrentstock(dpro, sTableName);
                    //                    drNew = dttt.NewRow();
                    //                    drNew["Qty"] = dr["Quantity"];
                    //                    drNew["Category"] = dr["categoryId"];
                    //                    drNew["OrderNo"] = dr["orderno"];
                    //                    drNew["Rate"] = dr["UnitPrice"];
                    //                    drNew["Tax"] = dr["Tax"];
                    //                    drNew["Amount"] = dr["Amount"];
                    //                    drNew["Refno"] = dr["RefNo"];
                    //                    drNew["Cerno"] = dr["Cerno"];
                    //                    drNew["Stock"] = dd.Tables[0].Rows[0]["Available_QTY"].ToString();
                    //                    drNew["ProductCode"] = dr["SubCategoryID"];
                    //                    drNew["Product"] = dr["SubCategoryID"];
                    //                    drNew["Discount"] = dr["Disc"];
                    //                    dstd.Tables[0].Rows.Add(drNew);
                    //                }

                    //                ViewState["CurrentTable1"] = dttt;

                    //                gvcustomerorder.DataSource = dstd;
                    //                gvcustomerorder.DataBind();


                    //                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                    //                {
                    //                    DropDownList txtt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpCategory");
                    //                    DropDownList txtd = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                    //                    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                    //                    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
                    //                    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                    //                    TextBox txtref = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrefno");
                    //                    TextBox txtcer = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtCerno");

                    //                    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    //                    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                    //                    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                    //                    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                    //                    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                    //                    txtkttt.Text = dstd.Tables[0].Rows[vLoop]["Amount"].ToString();
                    //                    txtttk.Text = dstd.Tables[0].Rows[vLoop]["qty"].ToString();
                    //                    txtktttt.Text = dstd.Tables[0].Rows[vLoop]["Discount"].ToString();
                    //                    txttk.Text = dstd.Tables[0].Rows[vLoop]["Rate"].ToString();
                    //                    txtkt.Text = dstd.Tables[0].Rows[vLoop]["Tax"].ToString();
                    //                    txtref.Text = dstd.Tables[0].Rows[vLoop]["refno"].ToString();
                    //                    txtcer.Text = dstd.Tables[0].Rows[vLoop]["Cerno"].ToString();
                    //                    txtd.SelectedValue = dstd.Tables[0].Rows[vLoop]["ProductCode"].ToString();
                    //                    txt.SelectedValue = dstd.Tables[0].Rows[vLoop]["Product"].ToString();
                    //                    txtt.SelectedValue = dstd.Tables[0].Rows[vLoop]["Category"].ToString();
                    //                    txtktt.Text = dstd.Tables[0].Rows[vLoop]["Stock"].ToString();
                    //                    txtno.Text = dstd.Tables[0].Rows[vLoop]["Orderno"].ToString();
                    //                    txtno.Focus();
                    //                }

                    //                txtdiscount.Text = ds2.Tables[0].Rows[0]["DiscAmt"].ToString();
                    //                //txtTaxamt5.Text = ds2.Tables[0].Rows[0]["TAX_5"].ToString();
                    //                txtTaxamt.Text = ds2.Tables[0].Rows[0]["TAX_14"].ToString();
                    //                txtgrandtotal.Text = ds2.Tables[0].Rows[0]["GrandTotal"].ToString();
                    //                totqty.Text = ds2.Tables[0].Rows[0]["totalqty"].ToString();
                    //                totmeter.Text = ds2.Tables[0].Rows[0]["totalmeter"].ToString();

                    //            }


                    //        }
                    //        orderno(sender, e);

                    //        ButtonAdd1_Click(sender, e);
                    //    }
                    //    else
                    //    {
                    //        DataSet ds12 = objBs.SalesBillno("tblSales_" + sTableName);
                    //        if (ds12.Tables[0].Rows.Count > 0)
                    //        {
                    //            // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                    //            if (ds12.Tables[0].Rows[0]["billno"].ToString() == "")
                    //            {
                    //                txtbillno.Text = "1";
                    //                txtpackingslip.Text = "1";
                    //            }
                    //            else
                    //            {
                    //                txtbillno.Text = ds12.Tables[0].Rows[0]["billno"].ToString();
                    //                txtpackingslip.Text = ds12.Tables[0].Rows[0]["billno"].ToString();

                    //                //  txtdate1.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    //                btnadd.Text = "Save";
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    DataSet ds12 = objBs.SalesBillno("tblSales_" + sTableName);
                    //    if (ds12.Tables[0].Rows.Count > 0)
                    //    {
                    //        // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                    //        if (ds12.Tables[0].Rows[0]["billno"].ToString() == "")
                    //        {
                    //            txtbillno.Text = "1";
                    //            txtpackingslip.Text = "1";
                    //        }
                    //        else
                    //        {
                    //            txtbillno.Text = ds12.Tables[0].Rows[0]["billno"].ToString();
                    //            txtpackingslip.Text = ds12.Tables[0].Rows[0]["billno"].ToString();

                    //            //  txtdate1.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    //            btnadd.Text = "Save";
                    //        }
                    //    }
                    //}

                    // }
                }

                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
            //ddlbook.Focus();
        }


        protected void gridbutton_click(object sender, EventArgs e)
        {
            Response.Redirect("salesgrid.aspx");
        }



        protected void newcstomer_click(object sender, EventArgs e)
        {
            Response.Redirect("customermaster.aspx");
        }

        protected void TextBox9_TextChanged(object sender, EventArgs e)
        {
        }
        protected void bblbillto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtbillcheck(object sender, EventArgs e)
        {
            if (txtmobile.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Mobile No!!!.');", true);
                return;
            }
            else
            {
                DataSet ds = new DataSet();

                ds = objBs.getmobilenumbercheck(txtmobile.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtcustomername.Text = ds.Tables[0].Rows[0]["ledgerName"].ToString();
                    txtadd.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    txttincst.Text = ds.Tables[0].Rows[0]["TinNo"].ToString();
                    txttransport.Focus();
                }
                else
                {
                    txtcustomername.Focus();
                }
            }

        }

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlrep_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        protected void gvcustomerorderchanged(object sender, EventArgs e)
        {
            //Get the selected row
            GridViewRow row = gvcustomerorder.SelectedRow;
            if (row != null)
            {

            }
        }

        protected void ddlcustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtmobileno_TextChanged2(object sender, EventArgs e)
        {


        }

        protected void checkbox1_changed(object sender, EventArgs e)
        {
        }

        protected void chknew_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataSet ds = new DataSet();
            //ds = objBs.categorymaster(sTableName);

            //DataSet dst = new DataSet();
            //dst = objBs.selectcategoryalldecriptionbranch(sTableName);

            //DataSet dswidth = objBs.GetWidth();




            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txtno = (TextBox)e.Row.FindControl("txtno");
            //    TextBox txtdesign = (TextBox)e.Row.FindControl("txtdesno");
            //    TextBox txtcolor = (TextBox)e.Row.FindControl("txtcolor");
            //    TextBox txtmeter = (TextBox)e.Row.FindControl("txtmeter");
            //    TextBox txtrate = (TextBox)e.Row.FindControl("txtRate");
            //    DropDownList drpwid = (DropDownList)e.Row.FindControl("drpwid");

            //    txtno.Text = "1";
            //    txtdesign.Text = "";
            //    txtmeter.Text = "0";
            //    txtcolor.Text = "";
            //    txtrate.Text = "0";

            //    var ddl = (DropDownList)e.Row.FindControl("drpwid");
            //    ddl.DataSource = dswidth;
            //    ddl.DataTextField = "Width";
            //    ddl.DataValueField = "Widthid";
            //    ddl.DataBind();
            //    ddl.Items.Insert(0, "Select Width");

            //}

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataSet ds = new DataSet();
            //ds = objBs.categorymaster(sTableName);

            //DataSet dst = new DataSet();
            //dst = objBs.selectcategoryalldecriptionbranch(sTableName);

            //DataSet dswidth = objBs.GetWidth();




            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txtno = (TextBox)e.Row.FindControl("txtno");
            //    TextBox txtdesign = (TextBox)e.Row.FindControl("txtdesno");
            //    TextBox txtcolor = (TextBox)e.Row.FindControl("txtcolor");
            //    TextBox txtmeter = (TextBox)e.Row.FindControl("txtmeter");
            //    TextBox txtrate = (TextBox)e.Row.FindControl("txtRate");
            //    DropDownList drpwid = (DropDownList)e.Row.FindControl("drpwid");

            //    txtno.Text = "1";
            //    txtdesign.Text = "";
            //    txtmeter.Text = "0";
            //    txtcolor.Text = "";
            //    txtrate.Text = "0";

            //    var ddl = (DropDownList)e.Row.FindControl("drpwid");
            //    ddl.DataSource = dswidth;
            //    ddl.DataTextField = "Width";
            //    ddl.DataValueField = "Widthid";
            //    ddl.DataBind();
            //    ddl.Items.Insert(0, "Select Width");

            //}

        }



        protected void ddlDef_SelectedIndexChanged1(object sender, EventArgs e)
        {


        }

        protected void ButtonAdd2_Click1(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {

                TextBox txtsam = (TextBox)GridView1.Rows[vLoop].FindControl("txtsamno");


                if (txtsam.Text == "")
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

                AddNewRow1();
            }
            else
            {

            }

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
            //    if (vLoop == gvcustomerorder.Rows.Count - 1)
            //    {
            //        TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtdesno");
            //        TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtcolor");
            //        TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtmeter");
            //        DropDownList drpwidth = (DropDownList)gvcustomerorder.Rows[vLoop - 1].FindControl("drpwid");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");

            //        TextBox txtdes = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
            //        TextBox txtcol = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
            //        TextBox txtme = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
            //        TextBox txtra = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

            //        txtdes.Text = txtdesign.Text;
            //        txtdes.Enabled = false;
            //        int iCol = Convert.ToInt32(txtcolor.Text) + 1;
            //        txtcol.Text = Convert.ToString(iCol);

            //        txtme.Text = txtmeter.Text;
            //        txtra.Text = txtrate.Text;
            //        // txtra.Enabled = false;
            //        drpwid.SelectedValue = drpwidth.SelectedValue;
            //        drpwid.Enabled = false;


            //    }

            //    txtcolor1.Focus();




            //}






        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            //txtmeter_textchanged(sender, e);
            //txtrrattee_textchanged(sender, e);
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtsample = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsamno");


                if (txtsample.Text == "")
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





        }


        private void AddNewRow()
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;

            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        TextBox txtsam =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtsamno");

                        DropDownList drpitem =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpitemtype");


                        CheckBox chkf =
                      (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("chkF");

                        CheckBox chkh =
                       (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("chkH");

                        TextBox txt36s =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt36s");

                        TextBox txt38m =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt38m");

                        TextBox txt40l =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt40l");

                        TextBox txt42xl =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt42xl");

                        TextBox txt44xxl =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt44xxl");




                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Sample"] = txtsam.Text;
                        dtCurrentTable.Rows[i - 1]["Item"] = drpitem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["36/S"] = txt36s.Text;
                        dtCurrentTable.Rows[i - 1]["38/M"] = txt38m.Text;
                        dtCurrentTable.Rows[i - 1]["40/L"] = txt40l.Text;
                        dtCurrentTable.Rows[i - 1]["42/XL"] = txt42xl.Text;
                        dtCurrentTable.Rows[i - 1]["44/XXL"] = txt44xxl.Text;
                        dtCurrentTable.Rows[i - 1]["F"] = chkf.Checked;
                        dtCurrentTable.Rows[i - 1]["H"] = chkh.Checked;


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

                        TextBox txtsample =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtsamno");

                        DropDownList drpitem =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpitemtype");

                        CheckBox chkf =
                      (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("chkF");

                        CheckBox chkh =
                       (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("chkH");

                        TextBox txt36s =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt36s");

                        TextBox txt38m =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt38m");

                        TextBox txt40l =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt40l");

                        TextBox txt42xl =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt42xl");

                        TextBox txt44xxl =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt44xxl");



                        txtsample.Text = dt.Rows[i]["sample"].ToString();
                        drpitem.SelectedValue = dt.Rows[i]["Item"].ToString();
                        if ((dt.Rows[i]["F"]).ToString() == "")
                        {
                            chkf.Checked = false;
                        }
                        else
                        {
                            chkf.Checked = Convert.ToBoolean(dt.Rows[i]["F"]);
                        }


                        if ((dt.Rows[i]["H"]).ToString() == "")
                        {
                            chkh.Checked = false;
                        }
                        else
                        {
                            chkh.Checked = Convert.ToBoolean(dt.Rows[i]["H"]);
                        }




                        txt36s.Text = dt.Rows[i]["36/S"].ToString();
                        txt38m.Text = dt.Rows[i]["38/M"].ToString();
                        txt40l.Text = dt.Rows[i]["40/L"].ToString();
                        txt42xl.Text = dt.Rows[i]["42/XL"].ToString();
                        txt44xxl.Text = dt.Rows[i]["44/XXL"].ToString();

                        rowIndex++;

                    }
                }
            }
        }


        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            //  dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Sample", typeof(string)));
            dtt.Columns.Add(new DataColumn("Item", typeof(string)));
            dtt.Columns.Add(new DataColumn("F", typeof(string)));
            dtt.Columns.Add(new DataColumn("H", typeof(string)));
            dtt.Columns.Add(new DataColumn("36/S", typeof(string)));
            dtt.Columns.Add(new DataColumn("38/M", typeof(string)));
            dtt.Columns.Add(new DataColumn("40/L", typeof(string)));
            dtt.Columns.Add(new DataColumn("42/XL", typeof(string)));
            dtt.Columns.Add(new DataColumn("44/XXL", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Qty", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Discount", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Tax", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dr = dtt.NewRow();
            //  dr["OrderNo"] = string.Empty;
            dr["Sample"] = string.Empty;
            dr["Item"] = string.Empty;
            dr["F"] = string.Empty;
            dr["H"] = string.Empty;
            dr["36/S"] = string.Empty;
            dr["38/M"] = string.Empty;
            dr["40/L"] = string.Empty;
            dr["42/XL"] = string.Empty;
            dr["44/XXL"] = string.Empty;

            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            //dct = new DataColumn("OrderNo");
            //dttt.Columns.Add(dct);

            dct = new DataColumn("Sample");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Item");
            dttt.Columns.Add(dct);

            dct = new DataColumn("F");
            dttt.Columns.Add(dct);

            dct = new DataColumn("H");
            dttt.Columns.Add(dct);

            dct = new DataColumn("36/S");
            dttt.Columns.Add(dct);

            dct = new DataColumn("38/M");
            dttt.Columns.Add(dct);

            dct = new DataColumn("40/L");
            dttt.Columns.Add(dct);

            dct = new DataColumn("42/XL");
            dttt.Columns.Add(dct);

            dct = new DataColumn("44/XXL");
            dttt.Columns.Add(dct);


            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            // drNew["OrderNo"] = 1;
            drNew["Sample"] = "";
            drNew["Item"] = 0;
            drNew["F"] = "";
            drNew["H"] = 0;
            drNew["36/S"] = 0;
            drNew["38/M"] = 0;
            drNew["40/L"] = 0;
            drNew["42/XL"] = 0;
            drNew["44/XXL"] = 0;

            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();


        }




        private void AddNewRow1()
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;

            int rowIndex = 0;

            if (ViewState["CurrentTable12"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable12"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        TextBox txtsam =
                          (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("txtsamno");

                        DropDownList drpitem =
                       (DropDownList)GridView1.Rows[rowIndex].Cells[4].FindControl("drpitemtype");

                        TextBox txt28t =
                          (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt28t");

                        TextBox txt30t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt30t");

                        TextBox txt32t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt32t");

                        TextBox txt34t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt34t");

                        TextBox txt36t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt36t");


                        TextBox txt38t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt38t");

                        TextBox txt40t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt40t");

                        TextBox txt42t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt42t");




                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Sample"] = txtsam.Text;
                        dtCurrentTable.Rows[i - 1]["Item"] = drpitem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["28"] = txt28t.Text;
                        dtCurrentTable.Rows[i - 1]["30"] = txt30t.Text;
                        dtCurrentTable.Rows[i - 1]["32"] = txt32t.Text;
                        dtCurrentTable.Rows[i - 1]["34"] = txt34t.Text;
                        dtCurrentTable.Rows[i - 1]["36"] = txt36t.Text;
                        dtCurrentTable.Rows[i - 1]["38"] = txt38t.Text;
                        dtCurrentTable.Rows[i - 1]["40"] = txt40t.Text;
                        dtCurrentTable.Rows[i - 1]["42"] = txt42t.Text;


                        rowIndex++;


                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable12"] = dtCurrentTable;

                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
        }

        private void SetPreviousData1()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable12"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable12"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        TextBox txtsample =
                          (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("txtsamno");

                        DropDownList drpitem =
                      (DropDownList)GridView1.Rows[rowIndex].Cells[4].FindControl("drpitemtype");

                        TextBox txt28t =
                           (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt28t");

                        TextBox txt30t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt30t");

                        TextBox txt32t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt32t");

                        TextBox txt34t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt34t");

                        TextBox txt36t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt36t");


                        TextBox txt38t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt38t");

                        TextBox txt40t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt40t");

                        TextBox txt42t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt42t");



                        txtsample.Text = dt.Rows[i]["sample"].ToString();
                        drpitem.SelectedValue = dt.Rows[i]["Item"].ToString();
                        txt28t.Text = dt.Rows[i]["28"].ToString();
                        txt30t.Text = dt.Rows[i]["30"].ToString();
                        txt32t.Text = dt.Rows[i]["32"].ToString();
                        txt34t.Text = dt.Rows[i]["34"].ToString();
                        txt36t.Text = dt.Rows[i]["36"].ToString();
                        txt38t.Text = dt.Rows[i]["38"].ToString();
                        txt40t.Text = dt.Rows[i]["40"].ToString();
                        txt42t.Text = dt.Rows[i]["42"].ToString();

                        rowIndex++;

                    }
                }
            }
        }


        private void FirstGridViewRow1()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            //  dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Sample", typeof(string)));
            dtt.Columns.Add(new DataColumn("Item", typeof(string)));
            dtt.Columns.Add(new DataColumn("28", typeof(string)));
            dtt.Columns.Add(new DataColumn("30", typeof(string)));
            dtt.Columns.Add(new DataColumn("32", typeof(string)));
            dtt.Columns.Add(new DataColumn("34", typeof(string)));
            dtt.Columns.Add(new DataColumn("36", typeof(string)));
            dtt.Columns.Add(new DataColumn("38", typeof(string)));
            dtt.Columns.Add(new DataColumn("40", typeof(string)));
            dtt.Columns.Add(new DataColumn("42", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Qty", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Discount", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Tax", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dr = dtt.NewRow();
            //  dr["OrderNo"] = string.Empty;
            dr["Sample"] = string.Empty;
            dr["Item"] = string.Empty;
            dr["28"] = string.Empty;
            dr["30"] = string.Empty;
            dr["32"] = string.Empty;
            dr["34"] = string.Empty;
            dr["36"] = string.Empty;
            dr["38"] = string.Empty;
            dr["40"] = string.Empty;
            dr["42"] = string.Empty;

            dtt.Rows.Add(dr);

            ViewState["CurrentTable12"] = dtt;

            GridView1.DataSource = dtt;
            GridView1.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            //dct = new DataColumn("OrderNo");
            //dttt.Columns.Add(dct);

            dct = new DataColumn("Sample");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Item");
            dttt.Columns.Add(dct);

            dct = new DataColumn("28");
            dttt.Columns.Add(dct);

            dct = new DataColumn("30");
            dttt.Columns.Add(dct);

            dct = new DataColumn("32");
            dttt.Columns.Add(dct);

            dct = new DataColumn("34");
            dttt.Columns.Add(dct);

            dct = new DataColumn("36");
            dttt.Columns.Add(dct);

            dct = new DataColumn("38");
            dttt.Columns.Add(dct);

            dct = new DataColumn("40");
            dttt.Columns.Add(dct);

            dct = new DataColumn("42");
            dttt.Columns.Add(dct);


            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            // drNew["OrderNo"] = 1;
            drNew["Sample"] = "";
            drNew["Item"] = "";
            drNew["28"] = 0;
            drNew["30"] = 0;
            drNew["32"] = 0;
            drNew["34"] = 0;
            drNew["36"] = 0;
            drNew["38"] = 0;
            drNew["40"] = 0;
            drNew["42"] = 0;

            dstd.Tables[0].Rows.Add(drNew);

            GridView1.DataSource = dstd;
            GridView1.DataBind();


        }


        protected void sampletro(object sender, EventArgs e)
        {
            ButtonAdd2_Click1(sender, e);

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                TextBox txtsam = (TextBox)GridView1.Rows[vLoop].FindControl("txtsamno");
                if (txtsam.Text != "")
                {
                    DataSet ds = objBs.getfirststageprocess(txtsam.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Item"].ToString() == "3")
                        {
                            DropDownList drpitem = (DropDownList)GridView1.Rows[vLoop].FindControl("drpitemtype");
                            drpitem.SelectedValue = ds.Tables[0].Rows[0]["Item"].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Scan Trousers Item Only.Thanks You!!!.');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Scan Not Valid.Thanks You!!!.');", true);
                        return;
                    }
                }
            }
            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                int cnt = GridView1.Rows.Count;

                TextBox txttk = (TextBox)GridView1.Rows[vLoop].FindControl("txt28t");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt28t");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt28t");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }

        protected void sample_check(object sender, EventArgs e)
        {
            ButtonAdd1_Click(sender, e);
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtsam = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsamno");
                if (txtsam.Text != "")
                {
                    DataSet ds = objBs.getfirststageprocess(txtsam.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Item"].ToString() == "3")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Scan Shirt/Casual Item Only.Thanks You!!!.');", true);
                            return;
                        }
                        else
                        {
                            DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitemtype");
                            drpitem.SelectedValue = ds.Tables[0].Rows[0]["Item"].ToString();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Scan Not Valid.Thanks You!!!.');", true);
                        return;
                    }

                }
            }
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;

                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt36s");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt36s");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }


        protected void txtAdvance_TextChanged(object sender, EventArgs e)
        {

        }

        protected void granddiscount(object sender, EventArgs e)
        {


        }


        protected void txtLchange(object sender, EventArgs e)
        {


        }

        protected void txttax_textchanged(object sender, EventArgs e)
        {


        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {


        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtmeter_textchanged(object sender, EventArgs e)
        {
            //   double gndmeter = 0;
            //   //double tax = 0;
            //   //double distotal = 0;
            //   //double r = 0;

            //   for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //   {

            //       TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
            //       TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

            //       gndmeter = gndmeter + Convert.ToDouble(txtmeter.Text);

            //       txtrate.Focus();


            //   }
            ////   txttotmet.Text = gndmeter.ToString();
            //   //  txtmeter_textchanged(sender, e);
            //   txtrrattee_textchanged(sender, e);
        }

        protected void txtrrattee_textchanged(object sender, EventArgs e)
        {
            //double gndmeter = 0;
            ////double tax = 0;
            ////double distotal = 0;
            ////double r = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{

            //    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
            //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

            //    gndmeter = gndmeter + (Convert.ToDouble(txtmeter.Text) * Convert.ToDouble(txtrate.Text));

            //    txtrate.Focus();


            //}
            //txttoal.Text = gndmeter.ToString();

        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        protected void orderno(object sender, EventArgs e)
        {

        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData1();

            if (ViewState["CurrentTable12"] != null)
            {
                DataSet ds = new DataSet();
                DataTable dt = (DataTable)ViewState["CurrentTable12"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {

                    ds.Merge(dt);


                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();

                    ViewState["CurrentTable12"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    SetPreviousData1();


                    grandtotalforsecondgrid(sender, e);

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable12"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    SetPreviousData1();

                    grandtotalforsecondgrid(sender, e);
                    FirstGridViewRow1();
                }
            }

        }

        private void SetRowData1()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable12"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable12"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        TextBox txtsam =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtsamno");

                        DropDownList drpitem =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpitemtype");


                        TextBox txt28t =
                             (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt28t");

                        TextBox txt30t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt30t");

                        TextBox txt32t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt32t");

                        TextBox txt34t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt34t");

                        TextBox txt36t =
                         (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt36t");


                        TextBox txt38t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt38t");

                        TextBox txt40t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt40t");

                        TextBox txt42t =
                      (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txt42t");




                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Sample"] = txtsam.Text;
                        dtCurrentTable.Rows[i - 1]["Item"] = drpitem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["28"] = txt28t.Text;
                        dtCurrentTable.Rows[i - 1]["30"] = txt30t.Text;
                        dtCurrentTable.Rows[i - 1]["32"] = txt32t.Text;
                        dtCurrentTable.Rows[i - 1]["34"] = txt34t.Text;
                        dtCurrentTable.Rows[i - 1]["36"] = txt36t.Text;
                        dtCurrentTable.Rows[i - 1]["38"] = txt38t.Text;
                        dtCurrentTable.Rows[i - 1]["40"] = txt40t.Text;
                        dtCurrentTable.Rows[i - 1]["42"] = txt42t.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable12"] = dtCurrentTable;
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
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

                    ds.Merge(dt);


                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();

                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();

                    //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    //{
                    //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                    //    txtno.Text = Convert.ToString(i + 1);
                    //}

                    grandtotalforfirstgrid(sender, e);
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();

                    grandtotalforfirstgrid(sender, e);
                    FirstGridViewRow();
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

                        TextBox txtsam =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtsamno");

                        DropDownList drpitem =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpitemtype");


                        CheckBox chkf =
                      (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("chkF");

                        CheckBox chkh =
                       (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("chkH");

                        TextBox txt36s =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt36s");

                        TextBox txt38m =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt38m");

                        TextBox txt40l =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt40l");

                        TextBox txt42xl =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt42xl");

                        TextBox txt44xxl =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txt44xxl");




                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Sample"] = txtsam.Text;
                        dtCurrentTable.Rows[i - 1]["Item"] = drpitem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["36/S"] = txt36s.Text;
                        dtCurrentTable.Rows[i - 1]["38/M"] = txt38m.Text;
                        dtCurrentTable.Rows[i - 1]["40/L"] = txt40l.Text;
                        dtCurrentTable.Rows[i - 1]["42/XL"] = txt42xl.Text;
                        dtCurrentTable.Rows[i - 1]["44/XXL"] = txt44xxl.Text;
                        dtCurrentTable.Rows[i - 1]["F"] = chkf.Checked;
                        dtCurrentTable.Rows[i - 1]["H"] = chkh.Checked;

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

        protected void gvcustomerorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void ddlCategort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        protected void gvcustomerorder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int UpdateStockAvailable(int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;

            return iSuccess;
        }
        private int InsertStockAvailable(int iCat, int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;

            return iSuccess;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("SecondstageGrid.aspx");
        }

        protected void Gridview1_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void Add_Click(object sender, EventArgs e)
        {
            int isucess = 0;

            if (txtmobile.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Mobile No!!!.');", true);
                return;

            }

            if (txttransport.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Transport!!!.');", true);
                return;
            }
            if (chktype.SelectedIndex >= 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Any Type.thanks You!!!.');", true);
                return;
            }
            if (btnadd.Text == "Save")
            {
                string cond = "";

                foreach (ListItem listItem in chktype.Items)
                {

                    if (listItem.Selected)
                    {
                        cond += listItem.Value + ",";
                    }

                }
                cond = cond.TrimEnd(',');

                double trouser = Convert.ToDouble(txttott.Text) + Convert.ToDouble(txttott1.Text);
                ////   cond = cond.Replace(",", ",");

                DateTime regdate = DateTime.ParseExact(txtdelidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet ds = new DataSet();
                ds = objBs.getmobilenumber(txtmobile.Text, txtcustomername.Text, txtadd.Text, txttincst.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ledgerid = ds.Tables[0].Rows[0]["ledgerid"].ToString();

                    isucess = objBs.insertsecondstage(ledgerid, txttransport.Text, regdate, txttincst.Text, txtmobile.Text, cond, txttotfs.Text, txttoths.Text, txttotcfs.Text, txttotchs.Text, trouser);

                    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                    {

                        TextBox txtsam = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsamno");
                        DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitemtype");
                        if (txtsam.Text != "")
                        {

                            CheckBox chkf = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkF");
                            CheckBox chkh = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkH");
                            TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
                            TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
                            TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
                            TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
                            TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");

                            isucess = objBs.inserttranssecondstage(txtsam.Text, drpitem.SelectedValue, chkf.Checked, chkh.Checked, txt36s.Text, txt38m.Text, txt40l.Text, txt42xl.Text, txt44xxl.Text, "0", "0", "0");


                        }
                    }
                    for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                    {
                        double separat = 0.00;
                        TextBox txtsam = (TextBox)GridView1.Rows[vLoop].FindControl("txtsamno");
                        DropDownList drpitem = (DropDownList)GridView1.Rows[vLoop].FindControl("drpitemtype");
                        if (txtsam.Text != "")
                        {
                            TextBox txt28 = (TextBox)GridView1.Rows[vLoop].FindControl("txt28t");
                            TextBox txt30 = (TextBox)GridView1.Rows[vLoop].FindControl("txt30t");
                            TextBox txt32 = (TextBox)GridView1.Rows[vLoop].FindControl("txt32t");
                            TextBox txt34 = (TextBox)GridView1.Rows[vLoop].FindControl("txt34t");
                            TextBox txt36 = (TextBox)GridView1.Rows[vLoop].FindControl("txt36t");
                            TextBox txt38 = (TextBox)GridView1.Rows[vLoop].FindControl("txt38t");
                            TextBox txt40 = (TextBox)GridView1.Rows[vLoop].FindControl("txt40t");
                            TextBox txt42 = (TextBox)GridView1.Rows[vLoop].FindControl("txt42t");

                            isucess = objBs.inserttranssecondstage(txtsam.Text, drpitem.SelectedValue, false, false, txt28.Text, txt30.Text, txt32.Text, txt34.Text, txt36.Text, txt38.Text, txt40.Text, txt42.Text);
                        }
                    }


                }
                else
                {

                }

            }
            Response.Redirect("secondStageGrid.aspx");
            //string itemc = string.Empty;
            //string itemcd = string.Empty;

            ////DataSet dss = new DataSet();
            ////if (ddlbook.SelectedItem.Value == "1")
            ////{
            ////    ddlvouchertype.SelectedValue = "2";

            ////}
            ////else
            ////{
            ////    ddlvouchertype.SelectedValue = "1";

            ////}
            ////string iSalesIDnew = Request.QueryString.Get("iSalesID");
            //if (btnadd.Text == "Save")
            //{
            //    if (drpsupplier.SelectedValue == "Select Supplier")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Supplier Name');", true);
            //        return;
            //    }

            //    if (drpchecked.SelectedValue == "Select Employee Name")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name');", true);
            //        return;
            //    }
            //    //if (drpwidth.SelectedValue == "Select Width")
            //    //{
            //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Width');", true);
            //    //    return;
            //    //}


            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //        TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
            //        TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
            //        TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
            //        Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

            //        DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");



            //        int col = vLoop + 1;

            //        if (drpwid.SelectedValue == "Select Width")
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Width in " + col + " in this row');", true);
            //            //  txt1.Focus();
            //            return;
            //        }


            //        txtno.Focus();

            //        itemc = txtdesign.Text;
            //        itemcd = txtcolor.Text;


            //        if ((itemc == null) || (itemc == ""))
            //        {
            //        }
            //        else
            //        {
            //            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
            //            {
            //                //  DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ProductCode");
            //                TextBox txtdesign1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtdesno");
            //                TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtcolor");
            //                if (txtdesign1.Text == "")
            //                {
            //                }
            //                else
            //                {

            //                    if (ii == iq)
            //                    {
            //                    }
            //                    else
            //                    {
            //                        if (itemc == txtdesign1.Text && itemcd == txtcolor1.Text)
            //                        {
            //                            itemc = txtdesign.Text;
            //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemc + "  already exists in the Grid.');", true);
            //                            //  txt1.Focus();
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

            //    DateTime regdate = DateTime.ParseExact(txtregdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    DateTime invdate = DateTime.ParseExact(txtinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //    int iStatus23 = objBs.insertfab(txtinvno.Text, drpsupplier.SelectedValue, regdate, drpchecked.SelectedValue, txtinrefno.Text, invdate, txttotmet.Text, "0", txtDelChalan.Text, txttoal.Text);

            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        //Label txtno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");
            //        //Label orderno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

            //        //string orderno = gvcustomerorder.Rows[i].Cells[0].Text;
            //        TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

            //        TextBox txtdesign = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesno");


            //        TextBox txtcolor = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcolor");


            //        TextBox txtmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtmeter");


            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

            //        Label imgpath = (Label)gvcustomerorder.Rows[i].FindControl("imgpreview");

            //        DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpwid");






            //        int iStatus2 = objBs.insertTransfab(txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue);

            //        // iStockSuccess = UpdateStockAvailable(Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty));


            //    }


            // }
            Response.Redirect("ViewProcess.aspx");
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



        }

        protected void chktype_Changed(object sender, EventArgs e)
        {
            if (chktype.SelectedIndex >= 0)
            {
                Panel1.Visible = false;
                Panel2.Visible = false;

                foreach (ListItem item in chktype.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {
                        if (item.Value == "1")
                        {
                            Panel1.Visible = true;

                        }
                        else if (item.Value == "2")
                        {
                            Panel2.Visible = true;
                        }


                    }
                }
            }
            else
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
            }


        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {


        }

        protected void grandtotalforfirstgrid(object sender, EventArgs e)
        {
            double s36fs = 0; double s42hs = 0;
            double s38fs = 0; double s36hs = 0;
            double s40fs = 0; double s38hs = 0;
            double s42fs = 0; double s40hs = 0;
            double s44fs = 0; double s44hs = 0;

            double sfs = 0; double mfs = 0;
            double lfs = 0; double xlfs = 0;
            double xxlfs = 0;

            double shs = 0; double mhs = 0;
            double lhs = 0; double xlhs = 0;
            double xxlhs = 0;





            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                double separat = 0.00;
                TextBox txtsam = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsamno");
                DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitemtype");
                if (txtsam.Text != "")
                {
                    if (drpitem.SelectedValue == "1")
                    {
                        CheckBox chkf = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkF");
                        CheckBox chkh = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkH");



                        if (chkf.Checked == true && chkh.Checked == true)
                        {
                            TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
                            TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
                            TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
                            TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
                            TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");
                            if (txt36s.Text == "")
                            {
                                txt36s.Text = "0";
                            }

                            separat = Convert.ToDouble(txt36s.Text) / 2;
                            s36fs = s36fs + separat;
                            s36hs = s36hs + separat;

                            if (txt38m.Text == "")
                            {
                                txt38m.Text = "0";
                            }

                            separat = Convert.ToDouble(txt38m.Text) / 2;
                            s38fs = s38fs + separat;
                            s38hs = s38hs + separat;



                            if (txt40l.Text == "")
                            {
                                txt40l.Text = "0";
                            }

                            separat = Convert.ToDouble(txt40l.Text) / 2;
                            s40fs = s40fs + separat;
                            s40hs = s40hs + separat;



                            if (txt42xl.Text == "")
                            {
                                txt42xl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt42xl.Text) / 2;
                            s42fs = s42fs + separat;
                            s42hs = s42hs + separat;



                            if (txt44xxl.Text == "")
                            {
                                txt44xxl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt44xxl.Text) / 2;
                            s44fs = s44fs + separat;
                            s44hs = s44hs + separat;





                        }
                        else if (chkf.Checked == true)
                        {
                            TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
                            TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
                            TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
                            TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
                            TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");

                            if (txt36s.Text == "")
                            {
                                txt36s.Text = "0";
                            }

                            separat = Convert.ToDouble(txt36s.Text);
                            s36fs = s36fs + separat;
                            //s36hs = s36hs + separat;



                            if (txt38m.Text == "")
                            {
                                txt38m.Text = "0";
                            }

                            separat = Convert.ToDouble(txt38m.Text);
                            s38fs = s38fs + separat;
                            // s38hs = s38hs + separat;



                            if (txt40l.Text == "")
                            {
                                txt40l.Text = "0";
                            }

                            separat = Convert.ToDouble(txt40l.Text);
                            s40fs = s40fs + separat;
                            // s40hs = s40hs + separat;



                            if (txt42xl.Text == "")
                            {
                                txt42xl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt42xl.Text);
                            s42fs = s42fs + separat;
                            // s42hs = s42hs + separat;



                            if (txt44xxl.Text == "")
                            {
                                txt44xxl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt44xxl.Text);
                            s44fs = s44fs + separat;
                            // s44hs = s44hs + separat;




                        }
                        else if (chkh.Checked == true)
                        {
                            TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
                            TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
                            TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
                            TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
                            TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");

                            if (txt36s.Text == "")
                            {
                                txt36s.Text = "0";
                            }

                            separat = Convert.ToDouble(txt36s.Text);
                            // s36fs = s36fs + separat;
                            s36hs = s36hs + separat;


                            if (txt38m.Text == "")
                            {
                                txt38m.Text = "0";
                            }

                            separat = Convert.ToDouble(txt38m.Text);
                            // s38fs = s38fs + separat;
                            s38hs = s38hs + separat;



                            if (txt40l.Text == "")
                            {
                                txt40l.Text = "0";
                            }

                            separat = Convert.ToDouble(txt40l.Text);
                            //s40fs = s40fs + separat;
                            s40hs = s40hs + separat;



                            if (txt42xl.Text == "")
                            {
                                txt42xl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt42xl.Text);
                            /// s42fs = s42fs + separat;
                            s42hs = s42hs + separat;



                            if (txt44xxl.Text == "")
                            {
                                txt44xxl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt44xxl.Text);
                            // s44fs = s44fs + separat;
                            s44hs = s44hs + separat;
                        }
                    }
                    else if (drpitem.SelectedValue == "2")
                    {
                        CheckBox chkf = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkF");
                        CheckBox chkh = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkH");



                        if (chkf.Checked == true && chkh.Checked == true)
                        {
                            TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
                            TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
                            TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
                            TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
                            TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");


                            if (txt36s.Text == "")
                            {
                                txt36s.Text = "0";
                            }

                            separat = Convert.ToDouble(txt36s.Text) / 2;
                            sfs = sfs + separat;
                            shs = shs + separat;

                            if (txt38m.Text == "")
                            {
                                txt38m.Text = "0";
                            }

                            separat = Convert.ToDouble(txt38m.Text) / 2;
                            mfs = mfs + separat;
                            mhs = mhs + separat;



                            if (txt40l.Text == "")
                            {
                                txt40l.Text = "0";
                            }

                            separat = Convert.ToDouble(txt40l.Text) / 2;
                            lfs = lfs + separat;
                            lhs = lhs + separat;



                            if (txt42xl.Text == "")
                            {
                                txt42xl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt42xl.Text) / 2;
                            xlfs = xlfs + separat;
                            xlhs = xlhs + separat;



                            if (txt44xxl.Text == "")
                            {
                                txt44xxl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt44xxl.Text) / 2;
                            xxlfs = xxlfs + separat;
                            xxlhs = xxlhs + separat;

                        }
                        else if (chkf.Checked == true)
                        {
                            TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
                            TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
                            TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
                            TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
                            TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");

                            if (txt36s.Text == "")
                            {
                                txt36s.Text = "0";
                            }

                            separat = Convert.ToDouble(txt36s.Text);
                            sfs = sfs + separat;
                            //  shs = shs + separat;

                            if (txt38m.Text == "")
                            {
                                txt38m.Text = "0";
                            }

                            separat = Convert.ToDouble(txt38m.Text);
                            mfs = mfs + separat;
                            //   mhs = mhs + separat;



                            if (txt40l.Text == "")
                            {
                                txt40l.Text = "0";
                            }

                            separat = Convert.ToDouble(txt40l.Text);
                            lfs = lfs + separat;
                            //  lhs = lhs + separat;



                            if (txt42xl.Text == "")
                            {
                                txt42xl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt42xl.Text);
                            xlfs = xlfs + separat;
                            //  xlhs = xlhs + separat;



                            if (txt44xxl.Text == "")
                            {
                                txt44xxl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt44xxl.Text);
                            xxlfs = xxlfs + separat;
                            //  xxlhs = xxlhs + separat;



                        }
                        else if (chkh.Checked == true)
                        {
                            TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
                            TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
                            TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
                            TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
                            TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");

                            if (txt36s.Text == "")
                            {
                                txt36s.Text = "0";
                            }

                            separat = Convert.ToDouble(txt36s.Text);
                            //  sfs = sfs + separat;
                            shs = shs + separat;

                            if (txt38m.Text == "")
                            {
                                txt38m.Text = "0";
                            }

                            separat = Convert.ToDouble(txt38m.Text);
                            //  mfs = mfs + separat;
                            mhs = mhs + separat;



                            if (txt40l.Text == "")
                            {
                                txt40l.Text = "0";
                            }

                            separat = Convert.ToDouble(txt40l.Text);
                            //   lfs = lfs + separat;
                            lhs = lhs + separat;



                            if (txt42xl.Text == "")
                            {
                                txt42xl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt42xl.Text);
                            //  xlfs = xlfs + separat;
                            xlhs = xlhs + separat;



                            if (txt44xxl.Text == "")
                            {
                                txt44xxl.Text = "0";
                            }

                            separat = Convert.ToDouble(txt44xxl.Text);
                            // xxlfs = xxlfs + separat;
                            xxlhs = xxlhs + separat;

                        }
                    }
                }
            }

            txt36fs.Text = s36fs.ToString(); txt36hs.Text = s36hs.ToString();
            txt38fs.Text = s38fs.ToString(); txt38hs.Text = s38hs.ToString();
            txt40fs.Text = s40fs.ToString(); txt40hs.Text = s40hs.ToString();
            txt42fs.Text = s42fs.ToString(); txt42hs.Text = s42hs.ToString();
            txt44fs.Text = s44fs.ToString(); txt44hs.Text = s44hs.ToString();

            txttotfs.Text = (s36fs + s38fs + s40fs + s42fs + s44fs).ToString();

            txttoths.Text = (s36hs + s38hs + s40hs + s42hs + s44hs).ToString();


            txtsfs.Text = sfs.ToString(); txtshs.Text = shs.ToString();
            txtmfs.Text = mfs.ToString(); txtmhs.Text = mhs.ToString();
            txtlfs.Text = lfs.ToString(); txtlhs.Text = lhs.ToString();
            txtxlfs.Text = xlfs.ToString(); txtxlhs.Text = xlhs.ToString();
            txtxxlfs.Text = xxlfs.ToString(); txtxxlhs.Text = xxlhs.ToString();

            txttotcfs.Text = (sfs + mfs + lfs + xlfs + xxlfs).ToString();
            txttotchs.Text = (shs + mhs + lhs + xlhs + xxlhs).ToString();

        }

        protected void txt36s_textchanged(object sender, EventArgs e)
        {
            grandtotalforfirstgrid(sender, e);
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop -1].FindControl("txt36s");
            //    TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop-1].FindControl("txt38m");
            //    TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop-1].FindControl("txt40l");
            //    TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop-1].FindControl("txt42xl");
            //    TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop-1].FindControl("txt44xxl");

            //    txt38m.Focus();
            //}
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;

                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt38m");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt38m");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }

        }
        protected void txt38m_textchanged(object sender, EventArgs e)
        {
            grandtotalforfirstgrid(sender, e);
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
            //    TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
            //    TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
            //    TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
            //    TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");

            //    txt40l.Focus();
            //}
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;

                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt40l");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt40l");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }
        protected void txt40l_textchanged(object sender, EventArgs e)
        {
            grandtotalforfirstgrid(sender, e);
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
            //    TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
            //    TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
            //    TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
            //    TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");

            //    txt42xl.Focus();
            //}
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;

                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt42xl");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt42xl");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }
        protected void txt42xl_textchanged(object sender, EventArgs e)
        {
            grandtotalforfirstgrid(sender, e);
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txt36s = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt36s");
            //    TextBox txt38m = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt38m");
            //    TextBox txt40l = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt40l");
            //    TextBox txt42xl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt42xl");
            //    TextBox txt44xxl = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");

            //    txt44xxl.Focus();
            //}
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;

                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txt44xxl");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt44xxl");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txt44xxl");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }
        protected void txt44xxl_textchanged(object sender, EventArgs e)
        {
            grandtotalforfirstgrid(sender, e);
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtsam = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsamno");
                txtsam.Focus();
            }
        }



        protected void grandtotalforsecondgrid(object sender, EventArgs e)
        {
            double t28 = 0; double t38 = 0;
            double t30 = 0; double t40 = 0;
            double t32 = 0; double t42 = 0;
            double t34 = 0; double t36 = 0;

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                double separat = 0.00;
                TextBox txtsam = (TextBox)GridView1.Rows[vLoop].FindControl("txtsamno");
                DropDownList drpitem = (DropDownList)GridView1.Rows[vLoop].FindControl("drpitemtype");
                if (txtsam.Text != "")
                {
                    if (drpitem.SelectedValue == "3")
                    {

                        TextBox txt28 = (TextBox)GridView1.Rows[vLoop].FindControl("txt28t");
                        TextBox txt30 = (TextBox)GridView1.Rows[vLoop].FindControl("txt30t");
                        TextBox txt32 = (TextBox)GridView1.Rows[vLoop].FindControl("txt32t");
                        TextBox txt34 = (TextBox)GridView1.Rows[vLoop].FindControl("txt34t");
                        TextBox txt36 = (TextBox)GridView1.Rows[vLoop].FindControl("txt36t");
                        TextBox txt38 = (TextBox)GridView1.Rows[vLoop].FindControl("txt38t");
                        TextBox txt40 = (TextBox)GridView1.Rows[vLoop].FindControl("txt40t");
                        TextBox txt42 = (TextBox)GridView1.Rows[vLoop].FindControl("txt42t");

                        if (txt28.Text == "")
                        {
                            txt28.Text = "0";
                        }

                        separat = Convert.ToDouble(txt28.Text);
                        t28 = t28 + separat;

                        if (txt30.Text == "")
                        {
                            txt30.Text = "0";
                        }

                        separat = Convert.ToDouble(txt30.Text);
                        t30 = t30 + separat;

                        if (txt32.Text == "")
                        {
                            txt32.Text = "0";
                        }

                        separat = Convert.ToDouble(txt32.Text);
                        t32 = t32 + separat;

                        if (txt34.Text == "")
                        {
                            txt34.Text = "0";
                        }

                        separat = Convert.ToDouble(txt34.Text);
                        t34 = t34 + separat;

                        if (txt36.Text == "")
                        {
                            txt36.Text = "0";
                        }

                        separat = Convert.ToDouble(txt36.Text);
                        t36 = t36 + separat;

                        if (txt38.Text == "")
                        {
                            txt38.Text = "0";
                        }

                        separat = Convert.ToDouble(txt38.Text);
                        t38 = t38 + separat;

                        if (txt40.Text == "")
                        {
                            txt40.Text = "0";
                        }

                        separat = Convert.ToDouble(txt40.Text);
                        t40 = t40 + separat;

                        if (txt42.Text == "")
                        {
                            txt42.Text = "0";
                        }

                        separat = Convert.ToDouble(txt42.Text);
                        t42 = t42 + separat;



                    }

                }

            }

            txtt28.Text = t28.ToString(); txtt36.Text = t36.ToString();
            txtt30.Text = t30.ToString(); txtt38.Text = t38.ToString();
            txtt32.Text = t32.ToString(); txtt40.Text = t40.ToString();
            txtt34.Text = t34.ToString(); txtt42.Text = t42.ToString();

            txttott.Text = (t28 + t30 + t32 + t34).ToString();

            txttott1.Text = (t36 + t38 + t40 + t42).ToString();

        }
        protected void txt28t_textchanged(object sender, EventArgs e)
        {
            grandtotalforsecondgrid(sender, e);

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                int cnt = GridView1.Rows.Count;

                TextBox txttk = (TextBox)GridView1.Rows[vLoop].FindControl("txt30t");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt30t");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt30t");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }


        }

        protected void txt30t_textchanged(object sender, EventArgs e)
        {
            grandtotalforsecondgrid(sender, e);

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                int cnt = GridView1.Rows.Count;

                TextBox txttk = (TextBox)GridView1.Rows[vLoop].FindControl("txt32t");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt32t");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt32t");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }


        protected void txt32t_textchanged(object sender, EventArgs e)
        {
            grandtotalforsecondgrid(sender, e);

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                int cnt = GridView1.Rows.Count;

                TextBox txttk = (TextBox)GridView1.Rows[vLoop].FindControl("txt34t");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt34t");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt34t");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }


        protected void txt34t_textchanged(object sender, EventArgs e)
        {
            grandtotalforsecondgrid(sender, e);
            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                int cnt = GridView1.Rows.Count;

                TextBox txttk = (TextBox)GridView1.Rows[vLoop].FindControl("txt36t");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt36t");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt36t");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }

        protected void txt36t_textchanged(object sender, EventArgs e)
        {
            grandtotalforsecondgrid(sender, e);

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                int cnt = GridView1.Rows.Count;

                TextBox txttk = (TextBox)GridView1.Rows[vLoop].FindControl("txt38t");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt38t");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt38t");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }

        protected void txt38t_textchanged(object sender, EventArgs e)
        {
            grandtotalforsecondgrid(sender, e);

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                int cnt = GridView1.Rows.Count;

                TextBox txttk = (TextBox)GridView1.Rows[vLoop].FindControl("txt40t");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt40t");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt40t");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }

        protected void txt40t_textchanged(object sender, EventArgs e)
        {
            grandtotalforsecondgrid(sender, e);
            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                int cnt = GridView1.Rows.Count;

                TextBox txttk = (TextBox)GridView1.Rows[vLoop].FindControl("txt42t");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt42t");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView1.Rows[vLoop - 1].FindControl("txt42t");
                    if (oldtxttk.Text == "0")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }

            }
        }

        protected void txt42t_textchanged(object sender, EventArgs e)
        {
            grandtotalforsecondgrid(sender, e);

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                TextBox txtsam = (TextBox)GridView1.Rows[vLoop].FindControl("txtsamno");
                txtsam.Focus();

            }
        }

    }
}