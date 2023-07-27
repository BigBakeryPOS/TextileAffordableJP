using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Globalization;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class Purchase : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();

        string sTableName = "";
        string empid = "";
        string compayid = "";
        decimal dTax = 0, dTax1 = 0, dTax2 = 0, dTax3 = 0, dTax4 = 0, dTax5 = 0;
        decimal dTaxAmt = 0, dTaxAmt1 = 0, dTaxAmt2 = 0, dTaxAmt3 = 0, dTaxAmt4 = 0, dTaxAmt5 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            compayid = Session["cmpyid"].ToString();
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            empid = Session["Empid"].ToString();

            if (!IsPostBack)
            {
                txtDCNo.Enabled = false;
                ddlBank.Enabled = false;
                ddlChequeNo.Enabled = false;
                txtaddress.Enabled = false;
                txtpincode.Enabled = false;
                txtcity.Enabled = false;
                txtmobileno.Enabled = false;
                txtArea.Enabled = false;
                DataSet ds = objbs.getDC_NO("tblPurchase_" + sTableName);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["DC_NO"].ToString() == "")
                        {
                            txtDCNo.Text = "1";
                        }
                        else
                        {
                            txtDCNo.Text = ds.Tables[0].Rows[0]["DC_NO"].ToString();
                        }
                    }
                }

                lblPurchaseOrder.Visible = false;
                ddlPurchaseOrder.Visible = false;
                DataSet dsPUrchaseOrder = objbs.selectPurchaseOrdeNo("tblPO_" + sTableName, "tblTransPO_" + sTableName);
                if (dsPUrchaseOrder.Tables[0].Rows.Count > 0)
                {
                    ddlPurchaseOrder.DataSource = dsPUrchaseOrder.Tables[0];
                    ddlPurchaseOrder.DataTextField = "pono";
                    ddlPurchaseOrder.DataValueField = "pono";
                    ddlPurchaseOrder.DataBind();
                    ddlPurchaseOrder.Items.Insert(0, "Select No");
                    // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();
                }
                else
                {
                    ddlPurchaseOrder.Items.Insert(0, "Select No");
                }

                ds = objbs.GetLedgers(Convert.ToInt32(lblUserID.Text), 4);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBank.DataSource = ds;
                        ddlBank.DataTextField = "LedgerName";
                        ddlBank.DataValueField = "LedgerID";
                        ddlBank.DataBind();
                        ddlBank.Items.Insert(0, "Select Bank Name");



                    }
                }
                ddlChequeNo.Items.Insert(0, "Select Chequeno");

                //dischead.Visible = false; tax14.Visible = false;

                // disc.Visible = false; disc1.Visible = false; disc2.Visible = false; disc3.Visible = false; disc4.Visible = false; disc5.Visible = false;
                DataSet dsCustomer = objbs.selectVendor(2);
                if (dsCustomer.Tables[0].Rows.Count > 0)
                {
                    ddlvendor.DataSource = dsCustomer.Tables[0];
                    ddlvendor.DataTextField = "LedgerName";
                    ddlvendor.DataValueField = "LedgerID";
                    ddlvendor.DataBind();
                    ddlvendor.Items.Insert(0, "Select Supplier");
                    // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();

                }
               

                txtpodate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDCDate.Text = DateTime.Today.ToString("dd/MM/yyyy");





                DataSet dsRegistration = objbs.selectregformdet(Convert.ToInt32(Session["UserID"]));
                //lblcompanyname.Text = dsRegistration.Tables[0].Rows[0]["CompanyName"].ToString();
                //lbltinno.Text = dsRegistration.Tables[0].Rows[0]["TinNo"].ToString();



                string DC_NO = Request.QueryString.Get("DC_NO");

                if (DC_NO != null)
                {
                    DataSet ds1 = objbs.updatePUrchase("tblPurchase_" + sTableName, DC_NO);
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Text = "Update";
                            DataSet dsbank = objbs.Getbanknamebranch(4, sTableName);
                            if (dsbank != null)
                            {
                                if (dsbank.Tables[0].Rows.Count > 0)
                                {
                                    ddlBank.DataSource = dsbank;
                                    ddlBank.DataTextField = "LedgerName";
                                    ddlBank.DataValueField = "LedgerID";
                                    ddlBank.DataBind();
                                    ddlBank.Items.Insert(0, "Select Bank Name");

                                }
                            }

                            DataSet vendorname = objbs.Getvendorname(2);
                            if (dsCustomer.Tables[0].Rows.Count > 0)
                            {
                                ddlvendor.DataSource = vendorname.Tables[0];
                                ddlvendor.DataTextField = "LedgerName";
                                ddlvendor.DataValueField = "LedgerID";
                                ddlvendor.DataBind();
                                ddlvendor.Items.Insert(0, "Select Supplier");
                                // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();

                            }




                            ddlPurchaseType.SelectedValue = ds1.Tables[0].Rows[0]["PurchaseType"].ToString();
                            if (ddlPurchaseType.SelectedValue == "2")
                            {
                                ddlPurchaseOrder.Enabled = false;
                                ddlPurchaseOrder.Visible = true;
                                lblPurchaseOrder.Visible = true;

                                DataSet purchaseno = objbs.purchaseno("tblPO_" + sTableName);
                                if (purchaseno.Tables[0].Rows.Count > 0)
                                {
                                    ddlPurchaseOrder.DataSource = purchaseno.Tables[0];
                                    ddlPurchaseOrder.DataTextField = "pono";
                                    ddlPurchaseOrder.DataValueField = "pono";
                                    ddlPurchaseOrder.DataBind();
                                    ddlPurchaseOrder.Items.Insert(0, "Select No");
                                    ddlPurchaseOrder.SelectedValue = ds1.Tables[0].Rows[0]["PurchaseOrderNO"].ToString();
                                }

                            }
                            else
                            {
                                ddlPurchaseOrder.Visible = false;
                            }
                            txtDCNo.Text = ds1.Tables[0].Rows[0]["DC_NO"].ToString();
                            txtDCDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["DC_Date"]).ToString("dd/MM/yyyy");
                            txtpono.Text = ds1.Tables[0].Rows[0]["Bill_NO"].ToString();
                            txtpodate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Bill_date"]).ToString("dd/MM/yyyy");
                            txtFreight.Text = ds1.Tables[0].Rows[0]["Freight"].ToString();
                            txtLU.Text = ds1.Tables[0].Rows[0]["Loading"].ToString();
                            txtroundoff.Text = ds1.Tables[0].Rows[0]["Roundoff"].ToString();    

                            txtcgst.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["CGST"]).ToString("N");
                            txtsgst.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["SGST"]).ToString("N");
                            txtigst.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["IGST"]).ToString("N");


                            txtreceived.Text = ds1.Tables[0].Rows[0]["Received"].ToString();
                            txtchecking.Text = ds1.Tables[0].Rows[0]["Checking"].ToString();
                            lblFile_Path.Text = ds1.Tables[0].Rows[0]["uploadFile"].ToString();
                            img_Photo.ImageUrl = ds1.Tables[0].Rows[0]["uploadFile"].ToString(); 

                            ddlProvince.SelectedValue = ds1.Tables[0].Rows[0]["provinceid"].ToString();

                            txtNarration.Text = ds1.Tables[0].Rows[0]["Narration"].ToString();
                            ddlPayMode.SelectedValue = ds1.Tables[0].Rows[0]["PaymentMode"].ToString();

                            if (ddlPayMode.SelectedValue == "Cash" || ddlPayMode.SelectedValue == "Credit")
                            {
                                ddlChequeNo.Enabled = false;
                                ddlBank.Enabled = false;

                                ddlBank.SelectedValue = ds1.Tables[0].Rows[0]["BankId"].ToString();
                                ddlChequeNo.SelectedValue = ds1.Tables[0].Rows[0]["ChequeNo"].ToString();
                            }
                            else
                            {
                                ddlChequeNo.Enabled = true;
                                ddlBank.Enabled = true;

                                ddlBank.SelectedValue = ds1.Tables[0].Rows[0]["BankId"].ToString();

                                ds = objbs.getBankLedger(4, Convert.ToInt32(ddlBank.SelectedValue), sTableName);

                                ddlChequeNo.DataSource = ds;
                                ddlChequeNo.DataTextField = "chequeno";
                                ddlChequeNo.DataValueField = "TransChequeId";
                                ddlChequeNo.DataBind();
                                ddlChequeNo.Items.Insert(0, "Select Chequeno");

                                ddlChequeNo.SelectedItem.Text = ds1.Tables[0].Rows[0]["ChequeNo"].ToString();
                            }

                            ddlvendor.SelectedValue = ds1.Tables[0].Rows[0]["VendorID"].ToString();
                            ds = objbs.getledgerdet(Convert.ToInt32(ddlvendor.SelectedValue),sTableName);

                            txtcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                            txtArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                            txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                            txtpincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString();
                            txtmobileno.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                            txtMailid.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                          



                            DataSet trans = objbs.selectPUrchase("tblPurchase_" + sTableName, "tblTransPurchase_" + sTableName, DC_NO);

                            int Tpo = trans.Tables[0].Rows.Count;

                            if (trans != null)
                            {
                                if (trans.Tables[0].Rows.Count > 0)
                                {



                                    DataTable dttt;
                                    DataRow drNew;
                                    DataColumn dct;
                                    DataSet dstd = new DataSet();
                                    dttt = new DataTable();

                                    dct = new DataColumn("orderno");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Category");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("productCode");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Product");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("refno");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Cerno");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Stock");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("POQTY");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Qty");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Rate");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Discount");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Tax");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("cst");
                                    dttt.Columns.Add(dct);

                                    dct = new DataColumn("Amount");
                                    dttt.Columns.Add(dct);

                                    dstd.Tables.Add(dttt);

                                    foreach (DataRow dr in trans.Tables[0].Rows)
                                    {
                                        DataSet dsd = objbs.GetStockDetails(Convert.ToInt32(dr["descriptionid"]), "tblStock_" + sTableName);


                                        drNew = dttt.NewRow();
                                        drNew["POQTY"] = dr["Qty"];
                                        drNew["Qty"] = dr["Qty"];
                                        drNew["Refno"] = dr["refno"];
                                        drNew["Cerno"] = dr["cerno"];
                                        drNew["Category"] = dr["categoryid"];
                                        drNew["orderno"] = dr["orderno"];
                                        drNew["Rate"] = dr["Rate"];
                                        drNew["Tax"] = dr["Tax"];
                                        drNew["cst"] = dr["cst1"];
                                        drNew["Amount"] = dr["Amount"];
                                        drNew["Stock"] = dsd.Tables[0].Rows[0]["Available_Qty"].ToString();
                                        drNew["productCode"] = dr["descriptionid"];
                                        drNew["Product"] = dr["descriptionid"];
                                        drNew["Discount"] = dr["Discount"];
                                        dstd.Tables[0].Rows.Add(drNew);
                                    }

                                    ViewState["CurrentTable1"] = dttt;

                                    GridView2.DataSource = dstd;
                                    GridView2.DataBind();


                                    for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                                    {
                                        DropDownList txtt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                                        DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("productCode");
                                        DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                                        TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOQTY");
                                        TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                                        TextBox txtrefno = (TextBox)GridView2.Rows[vLoop].FindControl("txtrefno");
                                        TextBox txtcerno = (TextBox)GridView2.Rows[vLoop].FindControl("txtcerno");
                                        TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                                        TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                                        TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                                        TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                                        TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                                        TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                                        TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                                        txtkttt.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Amount"]).ToString("N2");
                                        txtPOQTY.Text = dstd.Tables[0].Rows[vLoop]["qty"].ToString();
                                        txtttk.Text = dstd.Tables[0].Rows[vLoop]["qty"].ToString();
                                        //txtrefno.Text = dstd.Tables[0].Rows[vLoop]["Refno"].ToString();
                                        //txtcerno.Text = dstd.Tables[0].Rows[vLoop]["Cerno"].ToString();
                                        txtktttt.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Discount"]).ToString("N2");
                                        txttk.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Rate"]).ToString("N2");
                                        txtkt.Text = dstd.Tables[0].Rows[vLoop]["Tax"].ToString();
                                        txtd.SelectedValue = dstd.Tables[0].Rows[vLoop]["productCode"].ToString();
                                        txt.SelectedValue = dstd.Tables[0].Rows[vLoop]["Product"].ToString();
                                        txtt.SelectedValue = dstd.Tables[0].Rows[vLoop]["Category"].ToString();
                                        txtktt.Text = dstd.Tables[0].Rows[vLoop]["Stock"].ToString();
                                        txtno.Text = dstd.Tables[0].Rows[vLoop]["Orderno"].ToString();
                                        txtkttttt.Text = dstd.Tables[0].Rows[vLoop]["Cst"].ToString();
                                    }

                                }
                            }
                            txtdiscount.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Discount"]).ToString("N2");
                            txtTaxamt.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Tax14"]).ToString("N2");
                            txtcstamnt.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["cst"]).ToString("N2");
                            txtgrandtotal.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["TotalAmount"]).ToString("N2");

                           
                        }
                        orderno(sender, e);
                        ButtonAdd1_Click(sender, e);

                    }
                    
                }
                else
                {
                    FirstGridViewRow1();
                }
                txtDCDate.Focus();
            }
        
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
          
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fp_Upload.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
                fp_Upload.PostedFile.SaveAs(Server.MapPath("~/Files/") + fileName);
                lblFile_Path.Text = "~/Files/" + fp_Upload.PostedFile.FileName;
                img_Photo.ImageUrl = "~/Files/" + fp_Upload.PostedFile.FileName;
            }
        }

        protected void orderno(object sender, EventArgs e)
        {
            int number = 0;
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtno1 = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");

                    itemc = txtno1.Text;
                    // number = Convert.ToInt32(txtno.Text);
                    if ((itemc == null) || (itemc == ""))
                    {

                    }
                    else
                    {
                        for (int vLoop1 = 0; vLoop1 < GridView2.Rows.Count; vLoop1++)
                        {
                            TextBox txtno = (TextBox)GridView2.Rows[vLoop1].FindControl("txtno");

                            if (ii == iq)
                            {
                            }
                            else
                            {
                                if (itemc == txtno.Text)
                                {
                                    itemcd = txtno.Text;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                                    return;

                                }
                            }
                            ii = ii + 1;
                        }
                    }
                    iq = iq + 1;
                    ii = 1;

                    if (txtno1.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter OrderNo.')", true);
                        txtno1.Focus();
                        return;
                    }

                }
            }
            //ButtonAdd1_Click(sender, e);
        }


       


        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList ProductCode = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                if (ProductCode.SelectedItem.Text == "Select Product Code" && txt.SelectedItem.Text == "Select Product Code" && txtttk.Text == "" && txttk.Text == "")
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
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                DropDownList ProductCode = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                txtno.Focus();
            }
           

        }

        private void AddNewRow1()
        {
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                DropDownList txttt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("productCode");
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOqty");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                TextBox txtref = (TextBox)GridView2.Rows[vLoop].FindControl("txtrefno");
                TextBox txtcer = (TextBox)GridView2.Rows[vLoop].FindControl("txtCerno");


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

                        TextBox txttno =
                     (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtno");
                        DropDownList drpCategory =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("drpCategory");
                        TextBox txtStock =
                          (TextBox)GridView2.Rows[rowIndex].Cells[2].FindControl("txtStock");
                        TextBox txtRef =
                      (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtrefno");

                        TextBox txtcer =
                       (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtCerno");

                        DropDownList productCode =
                                 (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("productCode");
                        DropDownList drpItem =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("drpItem");
                        TextBox txtRate =
                          (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtRate");
                        TextBox TextBoxAmount =
                          (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtAmount");

                        TextBox txtPOQty =
                        (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtPOQty");
                        TextBox txtQty =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtQty");

                        TextBox txtDiscount =
                          (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtDiscount");
                        TextBox txtTax =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtTax");

                        TextBox txtCst =
                        (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtcst");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["OrderNo"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Category"] = drpCategory.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["productCode"] = productCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Stock"] = txtStock.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = TextBoxAmount.Text;
                        //dtCurrentTable.Rows[i - 1]["Refno"] = txtRef.Text;
                        //dtCurrentTable.Rows[i - 1]["Cerno"] = txtcer.Text;

                        dtCurrentTable.Rows[i - 1]["Product"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Qty"] = txtQty.Text;
                        dtCurrentTable.Rows[i - 1]["POQty"] = txtPOQty.Text;

                        dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;
                        dtCurrentTable.Rows[i - 1]["Tax"] = txtTax.Text;
                        dtCurrentTable.Rows[i - 1]["Cst"] = txtCst.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    GridView2.DataSource = dtCurrentTable;
                    GridView2.DataBind();
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
            SetRowData1();
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();


                    //for (int i = 0; i < GridView2.Rows.Count; i++)
                    //{
                    //    GridView2.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    //}
                    SetPreviousData1();


                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                        txtno.Text = Convert.ToString(i + 1);
                    }
                    txtRate_TextChanged(sender, e);
                }

                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();




                    //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    //{
                    //    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    //}
                    SetPreviousData1();

                    txtRate_TextChanged(sender, e);
                    FirstGridViewRow1();
                }

            }
            
        }

        private void SetRowData1()
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
                        TextBox txttno =
 (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtno");
                        DropDownList drpCategory =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("drpCategory");
                        TextBox txtStock =
                          (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtStock");

                      //  TextBox txtRef =
                      //(TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtrefno");

                      //  TextBox txtcer =
                      // (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtCerno");

                        DropDownList productCode =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[2].FindControl("productCode");

                        DropDownList drpItem =
                          (DropDownList)GridView2.Rows[rowIndex].Cells[2].FindControl("drpItem");
                        TextBox TextBoxAmount =
                          (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                        TextBox txtRate =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtRate");
                        TextBox txtQty =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtQty");

                        TextBox txtPOQTY = (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtPOQTY");

                        TextBox txtDiscount =
                        (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtDiscount");
                        TextBox txtTax =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtTax");

                        TextBox txtcst =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtcst");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Category"] = drpCategory.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["productCode"] = productCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Stock"] = txtStock.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = TextBoxAmount.Text;
                        dtCurrentTable.Rows[i - 1]["Product"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        //dtCurrentTable.Rows[i - 1]["Refno"] = txtRef.Text;
                        //dtCurrentTable.Rows[i - 1]["Cerno"] = txtcer.Text;
                        dtCurrentTable.Rows[i - 1]["Qty"] = txtQty.Text;
                        dtCurrentTable.Rows[i - 1]["POQty"] = txtPOQTY.Text;

                        dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;

                        dtCurrentTable.Rows[i - 1]["Tax"] = txtTax.Text;
                        dtCurrentTable.Rows[i - 1]["Cst"] = txtcst.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    GridView2.DataSource = dtCurrentTable;
                    GridView2.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
        }

        private void FirstGridViewRow1()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Category", typeof(string)));
            dtt.Columns.Add(new DataColumn("productCode", typeof(string)));
            dtt.Columns.Add(new DataColumn("Product", typeof(string)));
            dtt.Columns.Add(new DataColumn("Stock", typeof(string)));
            dtt.Columns.Add(new DataColumn("refno", typeof(string)));
            dtt.Columns.Add(new DataColumn("Cerno", typeof(string)));
            dtt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dtt.Columns.Add(new DataColumn("POQTY", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("Discount", typeof(string)));
            dtt.Columns.Add(new DataColumn("Tax", typeof(string)));
            dtt.Columns.Add(new DataColumn("Cst", typeof(string)));
            dtt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dr = dtt.NewRow();

            dr["OrderNo"] = string.Empty;
            dr["Category"] = string.Empty;
            dr["productCode"] = string.Empty;
            dr["Product"] = string.Empty;
            dr["Stock"] = string.Empty;
            dr["Qty"] = string.Empty;
            dr["refno"] = string.Empty;
            dr["Cerno"] = string.Empty;
            dr["POQTY"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["Discount"] = string.Empty;
            dr["Tax"] = string.Empty;
            dr["Cst"] = string.Empty;
            dr["Amount"] = string.Empty;
            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            GridView2.DataSource = dtt;
            GridView2.DataBind();





            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();


            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Category");
            dttt.Columns.Add(dct);


            dct = new DataColumn("productCode");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Product");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Stock");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Refno");
            dttt.Columns.Add(dct);
            dct = new DataColumn("cerno");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Qty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("POQTY");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Discount");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Tax");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Cst");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Amount");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["OrderNo"] = 0;
            drNew["Qty"] = 0;
            drNew["POQTY"] = 0;
            drNew["Category"] = "";
            drNew["Rate"] = 0;
            drNew["Tax"] = 0;
            drNew["Cst"] = 0;
            drNew["Amount"] = 0;
            drNew["Stock"] = 0;
            drNew["Refno"] = "";
            drNew["cerno"] = "";
            drNew["productCode"] = "";
            drNew["Product"] = "";
            drNew["Discount"] = 0;
            dstd.Tables[0].Rows.Add(drNew);

            GridView2.DataSource = dstd;
            GridView2.DataBind();
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            ButtonAdd1_Click(sender, e);
            double grandtotal = 0;
            double tax = 0;
            double cst = 0;
            double distotal = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOQTY");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                //if (txtttk.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Qty It Cannot be empty!!');", true);
                //    return;
                //}
                if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
                {

                }
                //if (txttk.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate It Cannot be empty!!');", true);
                //    return;
                //}
                //if (txtktttt.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Discount% It Cannot be empty!!');", true);
                //    return;
                //}
                else
                //if (txttk.Text != "" && txtktttt.Text != "" && txtttk.Text != "")
                {
                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx =  Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double cst1 = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkttttt.Text) / 100;
                    double total = tx + DiscountAmount + cst1;
                  
                    txtkttt.Text = string.Format("{0:N2}", total);
                  
                    grandtotal = grandtotal + total ;
                    tax = tax + tx;
                    cst = cst1 + cst;
                    distotal = distotal + Discount;
                }
                //else
                //{

                //}
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
            txtcstamnt.Text = string.Format("{0:N2}", cst);
            txtdiscount.Text = string.Format("{0:N2}", distotal);

            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtcstamnt.Text;

            }




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

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                int cnt = GridView2.Rows.Count;
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                DropDownList ProductCode = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtRate");
                    oldtxttk.Focus();
                }
                    int tot = cnt - vLoop;
                    if (tot == 1)
                    {
                        TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtRate");
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
                

               // txtno.Focus();
            }

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
            granddiscount(sender, e);
        }
        protected void txttax_TextChanged(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double tax = 0;
            double cst = 0;
            double distotal = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOQTY");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

             

                if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "")
                {

                }
                else
                {
                    if (txttk.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate It Cannot be empty!!');", true);
                        txttk.Focus();
                        return;
                    }
                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double cst1 = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkttttt.Text) / 100;
                    double total = tx + DiscountAmount+cst1;
                 

                    txtkttt.Text = string.Format("{0:N2}", total);

                    grandtotal = grandtotal + total ;
                    tax = tax + tx;
                    cst = cst + cst1;
                    distotal = distotal + Discount;
                }
                granddiscount(sender, e);
               
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
            txtdiscount.Text = string.Format("{0:N2}", distotal);

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

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                int cnt = GridView2.Rows.Count;
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                DropDownList ProductCode = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtDiscount");
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtDiscount");
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


                // txtno.Focus();
            }
        }


        protected void txtCst_TextChanged(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double tax = 0;
            double cst = 0;
            double distotal = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOQTY");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                //if (txtttk.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Qty It Cannot be empty!!');", true);
                //    txtttk.Focus();
                //    return;
                //}
                //if (txttk.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate It Cannot be empty!!');", true);
                //    txttk.Focus();
                //    return;
                //}
                //if (txtktttt.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Discount% It Cannot be empty!!');", true);
                //    return;
                //}

                if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "")
                {

                }
                else
                {
                    if (txttk.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate It Cannot be empty!!');", true);
                        txttk.Focus();
                        return;
                    }
                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                   double   cst1 = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkttttt.Text) / 100;
                   double total = tx + DiscountAmount + cst1;
                   

                    txtkttt.Text = string.Format("{0:N2}", total);
                   // txtkttttt.Text = string.Format("{0:N2}", cst);

                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    cst = cst1 + cst;
                    distotal = distotal + Discount;
                }
                granddiscount(sender, e);
                //else
                //{

                //}
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
            txtcstamnt.Text = string.Format("{0:N2}", cst);
            txtdiscount.Text = string.Format("{0:N2}", distotal);


            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtcstamnt.Text;
            }

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

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                int cnt = GridView2.Rows.Count;
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                DropDownList ProductCode = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtDiscount");
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtDiscount");
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


                // txtno.Focus();
            }
        }



        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ButtonAdd1_Click(sender, e);
            double grandtotal = 0;
            double tax = 0;
            double cst = 0;
            double distotal = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOQTY");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                //if (txtttk.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Qty It Cannot be empty!!');", true);
                //    txtttk.Focus();
                //    return;
                //}
                //if (txttk.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate It Cannot be empty!!');", true);
                //    txttk.Focus();
                //    return;
                //}
                //if (txtktttt.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Discount% It Cannot be empty!!');", true);
                //    return;
                //}

                if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "")
                {

                }
                else
                {
                    if (txttk.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate It Cannot be empty!!');", true);
                        txttk.Focus();
                        return;
                    }
                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double cst1 = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkttttt.Text) / 100;
                    double total = tx + DiscountAmount + cst1;


                    txtkttt.Text = string.Format("{0:N2}", total);
                    // txtkttttt.Text = string.Format("{0:N2}", cst);

                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    cst = cst1 + cst;
                    distotal = distotal + Discount;
                }
                granddiscount(sender, e);
                //else
                //{

                //}
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
            txtcstamnt.Text = string.Format("{0:N2}", cst);
            txtdiscount.Text = string.Format("{0:N2}", distotal);


            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtcstamnt.Text;
            }

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

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                int cnt = GridView2.Rows.Count;

                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");

                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                DropDownList ProductCode = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");

                if (txt.SelectedItem.Text == "Select Product Code")
                {

                }
                else
                {

                    if (vLoop >= 1)
                    {
                        //TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtDiscount");
                        TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                        oldtxttk.Focus();
                    }
                    int tot = cnt - vLoop;
                    if (tot == 1)
                    {
                       // TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtDiscount");

                        TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
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

                }
                // txtno.Focus();
            }

        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double tax = 0;
            double cst = 0;
            double distotal = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOQTY");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

               


               

                if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" )
                {

                }
                else
                {
                 if (txttk.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate It Cannot be empty!!');", true);
                    txttk.Focus();
                    return;
                }
                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx =  Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double cst1 = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkttttt.Text) / 100;
                    double total = tx + DiscountAmount+cst1;
                    

                    txtkttt.Text = string.Format("{0:N2}", total);

                    grandtotal = grandtotal + total ;
                    tax = tax + tx;
                    cst = cst + cst1;
                    distotal = distotal + Discount;
                }
                granddiscount(sender, e);
                //else
                //{

                //}
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
            txtcstamnt.Text = string.Format("{0:N2}", cst);
            txtdiscount.Text = string.Format("{0:N2}", distotal);



            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtcstamnt.Text;
            }


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

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                int cnt = GridView2.Rows.Count;
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                DropDownList ProductCode = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtDiscount");
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)GridView2.Rows[vLoop - 1].FindControl("txtDiscount");
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


                // txtno.Focus();
            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;
            double r = 0;
            double cst = 0;

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                //if (txtttk.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Qty It Cannot be empty!!');", true);
                //    return;
                //}
                //if (txttk.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate It Cannot be empty!!');", true);
                //    return;
                //}
                //if (txtktttt.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Discount% It Cannot be empty!!');", true);
                //    return;
                //}

                if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" )
                {

                }
                else
                {
                     if (txtktttt.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Discount% It Cannot be empty!!');", true);
                    txtktttt.Focus();
                    return;
                }
                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double cst1 = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkttttt.Text) / 100;
                    double total = tx + DiscountAmount+cst1;
                   

                    txtkttt.Text = string.Format("{0:N2}", total);

                    grandtotal = grandtotal + total ;
                    tax = tax + tx;
                    cst = cst1 + cst;
                    distotal = distotal + Discount;
                }
                //else
                //{
                //}
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
            txtcstamnt.Text = string.Format("{0:N2}", cst);
            txtdiscount.Text = string.Format("{0:N2}", distotal);

            if (ddlProvince.SelectedValue == "1")
            {
                txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(txtcstamnt.Text) / 2);
                txtigst.Text = "0.00";
            }
            else
            {
                txtcgst.Text = "0.00";
                txtsgst.Text = "0.00";
                txtigst.Text = txtcstamnt.Text;
            }

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

            

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                DropDownList ProductCode = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                txtno.Focus();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (ddlPurchaseType.SelectedValue == "2")
            {
                lblPurchaseOrder.Visible = true;
                ddlPurchaseOrder.Visible = true;
                if (ddlPurchaseOrder.SelectedItem.Text == "Select No")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Purchase Order No!');", true);
                    ddlPurchaseOrder.Focus();
                    return;
                }

            }



            if (ddlPayMode.SelectedValue == "Cheque" || ddlPayMode.SelectedValue == "DD")
            {
                if (ddlBank.SelectedValue == "Select Bank Name" && ddlChequeNo.SelectedValue == "Select Chequeno")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No!');", true);
                    ddlBank.Focus();
                    ddlChequeNo.Focus();

                    return;

                }
                else if (ddlBank.SelectedValue == "Select Bank Name")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name!');", true);
                    ddlBank.Focus();
                    return;

                }


                if (ddlProvince.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Province type')", true);

                    return;
                }

                else if (ddlChequeNo.SelectedValue == "Select Chequeno")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Cheque No!');", true);
                    ddlChequeNo.Focus();
                    return;

                }
            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                DropDownList txttt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("productCode");
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOqty");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                //TextBox txtref = (TextBox)GridView2.Rows[vLoop].FindControl("txtrefno");
                //TextBox txtcer = (TextBox)GridView2.Rows[vLoop].FindControl("txtCerno");

                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                int col = vLoop + 1;
                if (txt.SelectedValue != "0")
                {
                    if (txttk.Text == ".00")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Rate or else enter 0.00 " + col + " ')", true);
                            txttk.Focus();
                           return;
                    }
                    //if ((txtref.Text == "") || (Convert.ToInt32(txtref.Text) == 0))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter ref.No in row " + col + " ')", true);
                    //    txtttk.Focus();
                    //    return;
                    //}

                    //if ((txtcer.Text == "") || (Convert.ToInt32(txtcer.Text) == 0))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Certificate No in row " + col + " ')", true);
                    //    txtttk.Focus();
                    //    return;
                    //}

                    //if ((txtd.SelectedValue == "0") || (txtd.SelectedValue == "Select Product Code"))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Product Code in row " + col + " ')", true);
                    //    txtd.Focus();
                    //    return;
                    //}
                    //if ((txt.SelectedValue == "0") || (txt.SelectedValue == "Select Product"))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Product in row " + col + " ')", true);
                    //    txt.Focus();
                    //    return;
                    //}
                    //if ((txtttk.Text == "") || (Convert.ToInt32(txtttk.Text) == 0))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please qty in row " + col + " ')", true);
                    //    txtttk.Focus();
                    //    return;
                    //}
                    //if ((txttk.Text == "") || (Convert.ToDouble(txttk.Text) == 0.00))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Rate " + col + " ')", true);
                    //    txttk.Focus();
                    //    return;
                    //}
                    //if ((txtktttt.Text == ""))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Discount% in row " + col + " ')", true);
                    //    txtktttt.Focus();
                    //    return;
                    //}

                    //if ((Convert.ToDouble(txtktttt.Text) > 100))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Discount% within 100 in Row " + col + " ')", true);
                    //    txtktttt.Focus();
                    //    return;
                    //}


                    //if (ddlPurchaseType.SelectedValue == "2")
                    //{
                    //    if (Convert.ToInt32(txtttk.Text) > Convert.ToInt32(txtPOQTY.Text))
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Quantity is greater than POQTY')", true);
                    //        txtttk.Focus();
                    //        return;
                    //    }
                    //}
                }
            }

            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                    DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("productCode");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                    TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                    TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                    TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                    TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                    itemc = txt.Text;
                    itemcd = txt.SelectedItem.Text;

                    if ((itemc == null) || (itemc == ""))
                    {
                    }
                    else
                    {
                        for (int vLoop1 = 0; vLoop1 < GridView2.Rows.Count; vLoop1++)
                        {
                            DropDownList txt1 = (DropDownList)GridView2.Rows[vLoop1].FindControl("drpItem");

                            if (ii == iq)
                            {
                            }
                            else
                            {
                                if (itemc == txt1.Text)
                                {

                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                                    //return;

                                }
                            }
                            ii = ii + 1;
                        }
                    }
                    iq = iq + 1;
                    ii = 1;
                }
            }



            //if (ddlPayMode.SelectedItem.Text == "DD" || ddlPayMode.SelectedItem.Text == "Cheque")
            //{
            //    if (ddlBank.SelectedValue == "Select Bank Name" && ddlChequeNo.SelectedValue == "Select Chequeno")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No!');", true);
            //        return;

            //    }
            //    else if (ddlBank.SelectedValue == "Select Bank Name")
            //    {

            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name!');", true);
            //        return;

            //    }
            //    else if (ddlChequeNo.SelectedValue == "Select Chequeno")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Cheque No!');", true);
            //        return;

            //    }

            //}




            int isucess = 0;
            int iSucess = 0;
            //DataTable dt = new DataTable();
            //DataRow dr = null;
            //dt.Columns.Add(new DataColumn("CatID", typeof(int)));
            //if (ddldef.SelectedValue != "Select Description")
            //{
            //    dr = dt.NewRow();
            //    dr["CatID"] = ddldef.SelectedValue;
            //    dt.Rows.Add(dr);
            //}
            //if (ddldef1.SelectedValue != "Select Description")
            //{
            //    dr = dt.NewRow();
            //    dr["CatID"] = ddldef1.SelectedValue;
            //    dt.Rows.Add(dr);
            //}
            //if (ddldef2.SelectedValue != "Select Description")
            //{
            //    dr = dt.NewRow();
            //    dr["CatID"] = ddldef2.SelectedValue;
            //    dt.Rows.Add(dr);
            //}
            //if (ddldef3.SelectedValue != "Select Description")
            //{
            //    dr = dt.NewRow();
            //    dr["CatID"] = ddldef3.SelectedValue;
            //    dt.Rows.Add(dr);
            //}
            //if (ddldef4.SelectedValue != "Select Description")
            //{
            //    dr = dt.NewRow();
            //    dr["CatID"] = ddldef4.SelectedValue;
            //    dt.Rows.Add(dr);
            //}
            //if (ddldef5.SelectedValue != "Select Description")
            //{
            //    dr = dt.NewRow();
            //    dr["CatID"] = ddldef5.SelectedValue;
            //    dt.Rows.Add(dr);
            //}


            //DataTable ds = dt.DefaultView.ToTable(true, "CatID");//Columns.

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataRow dr;

       
          //  dt.Columns.Add(new DataColumn("RefNo", typeof(int)));
            dt.Columns.Add(new DataColumn("billno", typeof(int)));
            dt.Columns.Add(new DataColumn("orderno", typeof(string)));
            dt.Columns.Add(new DataColumn("CategoryId", typeof(string)));
            dt.Columns.Add(new DataColumn("POQty", typeof(int)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Refno", typeof(string)));
            dt.Columns.Add(new DataColumn("Cerno", typeof(string)));
            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemId", typeof(string)));
            dt.Columns.Add(new DataColumn("productCode", typeof(string)));
            dt.Columns.Add(new DataColumn("TaxId", typeof(string)));
            dt.Columns.Add(new DataColumn("cst", typeof(string)));
            dt.Columns.Add(new DataColumn("Discount", typeof(string)));

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                DropDownList txtt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("productCode");
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOqty");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                //TextBox txtref = (TextBox)GridView2.Rows[vLoop].FindControl("txtrefno");
                //TextBox txtcer = (TextBox)GridView2.Rows[vLoop].FindControl("txtCerno");

                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");
                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                txtPOQTY.Text = "0";

                if (txtd.SelectedItem.Text != "Select Product Code")
                {
                    dr = dt.NewRow();
                    dr["orderno"] =txtno.Text;
                    dr["billno"] = Convert.ToInt32(txtDCNo.Text);
                    dr["CategoryId"] = txtt.SelectedValue;
                    dr["productCode"] = txtd.SelectedValue;
                    dr["POQty"] = txtPOQTY.Text;
                    dr["Qty"] = txtttk.Text;
                    dr["refno"] = "";
                    dr["Cerno"] = "";
                    dr["Rate"] = txttk.Text;
                    dr["Amount"] = txtkttt.Text;
                    dr["ItemId"] = txt.SelectedValue;
                    dr["TaxId"] = txtkt.Text;
                    dr["cst"] = txtkttttt.Text;

                    dr["Discount"] = txtktttt.Text;
                    dt.Rows.Add(dr);
                }
            }

            ds.Merge(dt);

            //return;

            DateTime date = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime podate = DateTime.ParseExact(txtpodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (btnadd.Text == "Save")
            {

                int iCustid = 0;
                int Id = 0;

                if (chknew.Checked == true)
                {
                   // iCustid = objbs.insertVendor(txtCustname.Text, txtmobileno.Text, txtArea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtMailid.Text, "tblAuditMaster_" + sTableName, lblUser.Text,sTableName);
                }
                else
                {
                    iCustid = Convert.ToInt32(ddlvendor.SelectedValue);
                }

                string POOrderno;
                int bankid;
                int CreditorID1 = 0;
                int chequeno;

                if (ddlPurchaseType.SelectedValue == "2")
                {
                    POOrderno = ddlPurchaseOrder.SelectedValue;

                }
                else
                {
                    POOrderno = "0";

                }
                if (ddlPayMode.SelectedValue == "Cheque" || ddlPayMode.SelectedValue == "DD")
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                    chequeno = Convert.ToInt32(ddlChequeNo.SelectedItem.Text);

                }
                else
                {
                    bankid = 0;
                    chequeno = 0;
                }

                if (ddlPayMode.SelectedValue == "Cheque" || ddlPayMode.SelectedValue == "DD")
                {
                    CreditorID1 = Convert.ToInt32(ddlBank.SelectedValue);

                }
                if (ddlPayMode.SelectedValue == "Credit")
                {
                    CreditorID1 = Convert.ToInt32(ddlvendor.SelectedValue);
                }
                if (ddlPayMode.SelectedValue == "Cash")
                {
                   // DataSet dsledger = objbs.getCashledgerId("Cash A/C _" + sTableName,sTableName);
                    DataSet dsledger = objbs.getCashledgerId("Cash A/C", sTableName);
                    CreditorID1 = Convert.ToInt32(dsledger.Tables[0].Rows[0]["LedgerID"].ToString());
                }

                DataSet dsledger1 = objbs.getCashledgerId("PurchaseA/C_" + sTableName,sTableName);
                string ledgerid = dsledger1.Tables[0].Rows[0]["LedgerID"].ToString();

                //isucess = objbs.insertpurchase("tblPurchase_" + sTableName, "tblDayBook_" + sTableName, "tblPO_" + sTableName, Convert.ToInt32(ledgerid), Convert.ToString(CreditorID1), Convert.ToInt32(iCustid), Convert.ToInt32(txtDCNo.Text), date, txtpono.Text, podate, "0", Convert.ToDouble(txtdiscount.Text), Convert.ToDouble(0), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtcstamnt.Text), Convert.ToDouble("0"), Convert.ToInt32(ddlPurchaseType.SelectedValue), Convert.ToInt32(POOrderno), ddlPayMode.SelectedValue, Convert.ToInt32(bankid), chequeno, txtNarration.Text, ddlvendor.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text, btnadd.Text, ddlPurchaseType.SelectedItem.Text,txtFreight.Text,txtLU.Text,txtroundoff.Text);

                isucess = objbs.Insert_Purchase("tblPurchase_" + sTableName, "tblDayBook_" + sTableName, "tblPO_" + sTableName, Convert.ToInt32(ledgerid), Convert.ToString(CreditorID1), Convert.ToInt32(iCustid), Convert.ToInt32(txtDCNo.Text), date, txtpono.Text, podate, "0", Convert.ToDouble(txtdiscount.Text), Convert.ToDouble(0), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtcstamnt.Text), Convert.ToDouble("0"), Convert.ToInt32(ddlPurchaseType.SelectedValue), Convert.ToInt32(POOrderno), ddlPayMode.SelectedValue, Convert.ToInt32(bankid), chequeno, txtNarration.Text, ddlvendor.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text, btnadd.Text, ddlPurchaseType.SelectedItem.Text, txtFreight.Text, txtLU.Text, txtroundoff.Text, Convert.ToDouble(txtcgst.Text), Convert.ToDouble(txtsgst.Text), Convert.ToDouble(txtigst.Text), Convert.ToInt32(ddlProvince.SelectedValue), lblFile_Path.Text, txtreceived.Text, txtchecking.Text, empid);



                int iRtn = 0;
                //DataSet dPid = objbs.getpid();
                //string Ipid = (dPid.Tables[0].Rows[0]["P_ID"].ToString());
                int PurchaseOrder = 0;
                if (ddlPurchaseType.SelectedValue == "2")
                {
                    PurchaseOrder = Convert.ToInt32(ddlPurchaseOrder.SelectedValue);

                }
                else
                {
                    PurchaseOrder = 0;
                }

                DataSet dsStockDet = new DataSet();

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drd in ds.Tables[0].Rows)
                        {






                            objbs.inserttranspurchase("tblPurchase_" + sTableName, "tblTransPurchase_" + sTableName, txtDCNo.Text, Convert.ToInt32(drd["categoryId"]), Convert.ToInt32(drd["ItemId"]), Convert.ToDouble(drd["Qty"]), Convert.ToDouble(drd["Rate"]), Convert.ToInt32(0), Convert.ToDouble(drd["Discount"]), Convert.ToDouble(drd["TaxId"]), Convert.ToDouble(drd["Amount"]), PurchaseOrder, "tblTransPO_" + sTableName, btnadd.Text, Convert.ToString(drd["Orderno"]), Convert.ToString(drd["refno"]), Convert.ToString(drd["Cerno"]), Convert.ToDouble(drd["cst"]));

                            DataSet dQty = objbs.GetPurchaseStok(Convert.ToInt32(drd["ItemId"]), "tblStock_" + sTableName);
                            if (dQty.Tables[0].Rows.Count > 0) 
                            {
                                double iAvlqty = Convert.ToDouble(dQty.Tables[0].Rows[0]["Available_QTY"].ToString());

                                decimal dExistPurRate = Convert.ToDecimal(dQty.Tables[0].Rows[0]["PurchaseRate"].ToString());
                                double iAvlStock = iAvlqty + Convert.ToDouble(drd["qty"]);

                                if (dExistPurRate == Convert.ToDecimal(0.00) || iAvlStock == 0)
                                {
                                    decimal dCurrPurRate = Convert.ToDecimal(drd["rate"]);
                                    decimal dExistPurAmt = Convert.ToDecimal(iAvlqty) * dExistPurRate;

       
                                    decimal dCurrPurAmt = Convert.ToDecimal(drd["qty"]) * dCurrPurRate;

                                    decimal dTotPurRate = dExistPurAmt + dCurrPurAmt;
                                    objbs.UpdatePurchaseStok(0, iAvlStock, Convert.ToInt32(drd["ItemId"]), Convert.ToDecimal(drd["Rate"]), "tblStock_" + sTableName);
                                }
                                else
                                {
                                    decimal dCurrPurRate = Convert.ToDecimal(drd["rate"]);
                                    decimal dExistPurAmt = Convert.ToDecimal(iAvlqty) * dExistPurRate;

                                   // int iAvlStock = iAvlqty + Convert.ToInt32(drd["qty"]);
                                    decimal dCurrPurAmt = Convert.ToDecimal(drd["qty"]) * dCurrPurRate;

                                    decimal dTotPurRate = dExistPurAmt + dCurrPurAmt;
                                    decimal dFinal = dTotPurRate / Convert.ToDecimal(iAvlStock);

                                    objbs.UpdatePurchaseStok(0, iAvlStock, Convert.ToInt32(drd["ItemId"]), dFinal, "tblStock_" + sTableName);
                                }
                            }
                        }
                    }
                }


                System.Threading.Thread.Sleep(3000);
                Response.Redirect("Purchase.aspx");
            }

            else if (btnadd.Text == "Update")
            {
                string DC_NO = Request.QueryString.Get("DC_NO");

                if (DC_NO != null)
                {
                    DataSet ChequeNo = objbs.selectChequeNo("tblPurchase_" + sTableName, DC_NO);

                    string Cheque = ChequeNo.Tables[0].Rows[0]["ChequeNo"].ToString();

                    int updatechequeno = objbs.UpdateChequeno(Convert.ToInt32(Cheque));

                    DataSet dsDaybookId = objbs.selectDaybookid("tblPurchase_" + sTableName, DC_NO);
                    string daybookid = dsDaybookId.Tables[0].Rows[0]["DayBookTransNo"].ToString();

                    // int iDelete = objbs.deletepurchaseentry("tblDayBook_" + sTableName, "tblPurchase_" + sTableName, DC_NO, daybookid,"0","0","0","0");
                    DataSet dsTransSales = objbs.GetTransPurchase("tblTransPurchase_" + sTableName, DC_NO);
                    if (dsTransSales.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsTransSales.Tables[0].Rows.Count; i++)
                        {
                            string sddlCat = dsTransSales.Tables[0].Rows[i]["CategoryId"].ToString();
                            string sddlDef = dsTransSales.Tables[0].Rows[i]["DescriptionId"].ToString();
                            string sQty = dsTransSales.Tables[0].Rows[i]["Qty"].ToString();
                            int iSuccs = UpdateEditStock(Convert.ToInt32(sddlCat), Convert.ToInt32(sddlDef), Convert.ToDouble(sQty));

                        }
                    }
                    string purchaseorderno;
                    if (Convert.ToInt32(ddlPurchaseType.SelectedValue) == 1)
                    {
                        purchaseorderno = "0";
                    }
                    else
                    {
                        purchaseorderno = ddlPurchaseOrder.SelectedValue;
                    }




                    int iTransDelete = objbs.DeleteTransPurchase("tblTransPurchase_" + sTableName, DC_NO, Convert.ToInt32(purchaseorderno), "tblTransPO_" + sTableName, "tblPurchase_" + sTableName);

                  //  int iDelete = objbs.deletepurchaseentry("tblDayBook_" + sTableName, "tblPurchase_" + sTableName, DC_NO, daybookid, "0", "0", "0", "0",sTableName);

                    int iCustid = 0;
                    int Id = 0;

                    if (chknew.Checked == true)
                    {
                      //  iCustid = objbs.insertVendor(txtCustname.Text, txtmobileno.Text, txtArea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtMailid.Text, "tblAuditMaster_" + sTableName, lblUser.Text,sTableName);
                    }
                    else
                    {
                        iCustid = Convert.ToInt32(ddlvendor.SelectedValue);
                    }


                    string POOrderno;
                    int bankid;
                    int CreditorID1 = 0;
                    int chequeno;

                    if (ddlPurchaseType.SelectedValue == "2")
                    {
                        POOrderno = ddlPurchaseOrder.SelectedValue;

                    }
                    else
                    {
                        POOrderno = "0";

                    }
                    if (ddlPayMode.SelectedValue == "Cheque" || ddlPayMode.SelectedValue == "DD")
                    {
                        bankid = Convert.ToInt32(ddlBank.SelectedValue);
                        chequeno = Convert.ToInt32(ddlChequeNo.SelectedItem.Text);

                    }
                    else
                    {
                        bankid = 0;
                        chequeno = 0;
                    }

                    if (ddlPayMode.SelectedValue == "Cheque" || ddlPayMode.SelectedValue == "DD")
                    {
                        CreditorID1 = Convert.ToInt32(ddlBank.SelectedValue);

                    }
                    if (ddlPayMode.SelectedValue == "Credit")
                    {
                        CreditorID1 = Convert.ToInt32(ddlvendor.SelectedValue);
                    }
                    if (ddlPayMode.SelectedValue == "Cash")
                    {
                       // DataSet dsledger = objbs.getCashledgerId("Cash A/C _" + sTableName,sTableName);
                        DataSet dsledger = objbs.getCashledgerId("Cash A/C", sTableName);
                        CreditorID1 = Convert.ToInt32(dsledger.Tables[0].Rows[0]["LedgerID"].ToString());
                    }

                    DataSet dsledger1 = objbs.getCashledgerId("PurchaseA/C_" + sTableName, sTableName);

                    string ledgerid = dsledger1.Tables[0].Rows[0]["LedgerID"].ToString();

                    //isucess = objbs.updatepurchase("tblPurchase_" + sTableName, "tblDayBook_" + sTableName, "tblPO_" + sTableName, Convert.ToInt32(ledgerid), Convert.ToString(CreditorID1), Convert.ToInt32(ddlvendor.SelectedValue), Convert.ToInt32(txtDCNo.Text), date, txtpono.Text, podate, "0", Convert.ToDouble(txtdiscount.Text), Convert.ToDouble(0), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtcstamnt.Text), Convert.ToDouble("0"), Convert.ToInt32(ddlPurchaseType.SelectedValue), Convert.ToInt32(POOrderno), ddlPayMode.SelectedValue, Convert.ToInt32(bankid), chequeno, txtNarration.Text, ddlvendor.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text, btnadd.Text, ddlPurchaseType.SelectedItem.Text, DC_NO, daybookid);

                    isucess = objbs.Update_Purchase("tblPurchase_" + sTableName, "tblDayBook_" + sTableName, "tblPO_" + sTableName, Convert.ToInt32(ledgerid), Convert.ToString(CreditorID1), Convert.ToInt32(ddlvendor.SelectedValue), Convert.ToInt32(txtDCNo.Text), date, txtpono.Text, podate, "0", Convert.ToDouble(txtdiscount.Text), Convert.ToDouble(0), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtcstamnt.Text), Convert.ToDouble("0"), Convert.ToInt32(ddlPurchaseType.SelectedValue), Convert.ToInt32(POOrderno), ddlPayMode.SelectedValue, Convert.ToInt32(bankid), chequeno, txtNarration.Text, ddlvendor.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text, btnadd.Text, ddlPurchaseType.SelectedItem.Text, DC_NO, daybookid, Convert.ToDouble(txtcgst.Text), Convert.ToDouble(txtsgst.Text), Convert.ToDouble(txtigst.Text), Convert.ToInt32(ddlProvince.SelectedValue),lblFile_Path.Text,txtreceived.Text,txtchecking.Text,empid);



                    int iRtn = 0;
                    //DataSet dPid = objbs.getpid();
                    //string Ipid = (dPid.Tables[0].Rows[0]["P_ID"].ToString());

                    DataSet dsStockDet = new DataSet();



                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drd in ds.Tables[0].Rows)
                            {
                                objbs.inserttranspurchase("tblPurchase_" + sTableName, "tblTransPurchase_" + sTableName, txtDCNo.Text, Convert.ToInt32(drd["categoryId"]), Convert.ToInt32(drd["ItemId"]), Convert.ToDouble(drd["qty"]), Convert.ToDouble(drd["rate"]), Convert.ToInt32(0), Convert.ToDouble(drd["discount"]), Convert.ToDouble(drd["taxid"]), Convert.ToDouble(drd["amount"]), Convert.ToInt32(purchaseorderno), "tblTransPO_" + sTableName, btnadd.Text, Convert.ToString(drd["orderno"]), Convert.ToString(drd["refno"]), Convert.ToString(drd["Cerno"]), Convert.ToDouble(drd["cst"]));

                                DataSet dQty = objbs.GetPurchaseStok(Convert.ToInt32(drd["ItemId"]), "tblStock_" + sTableName);
                                if (dQty.Tables[0].Rows.Count > 0)
                                {
                                    double iAvlqty = Convert.ToDouble(dQty.Tables[0].Rows[0]["Available_QTY"].ToString());

                                    decimal dExistPurRate = Convert.ToDecimal(dQty.Tables[0].Rows[0]["PurchaseRate"].ToString());
                                          double iAvlStock = iAvlqty + Convert.ToDouble(drd["qty"]);

                                          if (dExistPurRate == Convert.ToDecimal(0.00) || iAvlStock == 0)
                                    {
                                        decimal dCurrPurRate = Convert.ToDecimal(drd["rate"]);
                                        decimal dExistPurAmt = Convert.ToDecimal(iAvlqty) * dExistPurRate;

                                  
                                        decimal dCurrPurAmt = Convert.ToDecimal(drd["qty"]) * dCurrPurRate;

                                        decimal dTotPurRate = dExistPurAmt + dCurrPurAmt;
                                        objbs.UpdatePurchaseStok(0, iAvlStock, Convert.ToInt32(drd["ItemId"]), Convert.ToDecimal(drd["Rate"]), "tblStock_" + sTableName);
                                    }
                                    else
                                    {
                                        decimal dCurrPurRate = Convert.ToDecimal(drd["rate"]);
                                        decimal dExistPurAmt = Convert.ToDecimal(iAvlqty) * dExistPurRate;

                                      //  int iAvlStock = iAvlqty + Convert.ToInt32(drd["qty"]);
                                        decimal dCurrPurAmt = Convert.ToDecimal(drd["qty"]) * dCurrPurRate;

                                        decimal dTotPurRate = dExistPurAmt + dCurrPurAmt;
                                        decimal dFinal = dTotPurRate / Convert.ToDecimal(iAvlStock);

                                        objbs.UpdatePurchaseStok(0, iAvlStock, Convert.ToInt32(drd["ItemId"]), dFinal, "tblStock_" + sTableName);
                                    }
                                }
                            }
                        }
                    }

                   
                    System.Threading.Thread.Sleep(3000);

                    Response.Redirect("Purchase.aspx");
                }

            }

        }

        //protected void txtdisc_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal disc = Convert.ToDecimal(txtdisc.Text);
        //    Decimal damt = Convert.ToDecimal(disc / 100) * Convert.ToDecimal(txtamount.Text);
        //    txtdamt.Text = Convert.ToString(damt);
        //    GrossDiscount();
        //}

        //protected void ddtax_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax = Convert.ToDecimal(ddTax.SelectedItem.Text);
        //    Decimal damt = Convert.ToDecimal(Dtax / 100) * Convert.ToDecimal(txtamount.Text);
        //    txtTamt.Text = Convert.ToString(damt);
        //}

        //protected void txtdisc1_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal disc1 = Convert.ToDecimal(txtdisc1.Text);
        //    Decimal damt1 = Convert.ToDecimal(disc1 / 100) * Convert.ToDecimal(txtamount1.Text);
        //    txtdamt1.Text = Convert.ToString(damt1);
        //    GrossDiscount();
        //}

        //protected void ddTax1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax1 = Convert.ToDecimal(ddTax1.SelectedItem.Text);
        //    Decimal damt1 = Convert.ToDecimal(Dtax1 / 100) * Convert.ToDecimal(txtamount1.Text);
        //    txtTamt1.Text = Convert.ToString(damt1);
        //}

        //protected void txtdisc2_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal disc2 = Convert.ToDecimal(txtdisc2.Text);
        //    Decimal damt2 = Convert.ToDecimal(disc2 / 100) * Convert.ToDecimal(txtamount2.Text);
        //    txtdamt2.Text = Convert.ToString(damt2);
        //    GrossDiscount();
        //}

        //protected void ddTax2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax2 = Convert.ToDecimal(ddTax2.SelectedItem.Text);
        //    Decimal damt2 = Convert.ToDecimal(Dtax2 / 100) * Convert.ToDecimal(txtamount2.Text);
        //    txtTamt2.Text = Convert.ToString(damt2);
        //}

        //protected void txtdisc3_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal disc3 = Convert.ToDecimal(txtdisc3.Text);
        //    Decimal damt3 = Convert.ToDecimal(disc3 / 100) * Convert.ToDecimal(txtamount3.Text);
        //    txtdamt3.Text = Convert.ToString(damt3);
        //    GrossDiscount();
        //}

        //protected void ddTax3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax3 = Convert.ToDecimal(ddTax3.SelectedItem.Text);
        //    Decimal damt3 = Convert.ToDecimal(Dtax3 / 100) * Convert.ToDecimal(txtamount3.Text);
        //    txtTamt3.Text = Convert.ToString(damt3);
        //}

        //protected void txtdisc4_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal disc4 = Convert.ToDecimal(txtdisc4.Text);
        //    Decimal damt4 = Convert.ToDecimal(disc4 / 100) * Convert.ToDecimal(txtamount4.Text);
        //    txtdamt4.Text = Convert.ToString(damt4);
        //    GrossDiscount();
        //}

        //protected void ddTax4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax4 = Convert.ToDecimal(ddTax4.SelectedItem.Text);
        //    Decimal damt4 = Convert.ToDecimal(Dtax4 / 100) * Convert.ToDecimal(txtamount4.Text);
        //    txtTamt4.Text = Convert.ToString(damt4);
        //}

        //protected void txtdisc5_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal disc5 = Convert.ToDecimal(txtdisc5.Text);
        //    Decimal damt5 = Convert.ToDecimal(disc5 / 100) * Convert.ToDecimal(txtamount5.Text);
        //    txtdamt5.Text = Convert.ToString(damt5);
        //    GrossDiscount();
        //}

        //protected void ddTax5_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax5 = Convert.ToDecimal(ddTax5.SelectedItem.Text);
        //    Decimal damt5 = Convert.ToDecimal(Dtax5 / 100) * Convert.ToDecimal(txtamount5.Text);
        //    txtTamt5.Text = Convert.ToString(damt5);
        //}

        //private void GrossDiscount()
        //{
        //    Decimal idiscount1 = 0; Decimal idiscount2 = 0; Decimal idiscount3 = 0; Decimal idiscount4 = 0; Decimal idiscount5 = 0; Decimal idiscount6 = 0;
        //    if (txtdamt.Text != "")
        //        idiscount1 = Convert.ToDecimal(txtdamt.Text);
        //    if (txtdamt1.Text != "")
        //        idiscount2 = Convert.ToDecimal(txtdamt1.Text);
        //    if (txtdamt2.Text != "")
        //        idiscount3 = Convert.ToDecimal(txtdamt2.Text);
        //    if (txtdamt3.Text != "")
        //        idiscount4 = Convert.ToDecimal(txtdamt3.Text);
        //    if (txtdamt4.Text != "")
        //        idiscount5 = Convert.ToDecimal(txtdamt4.Text);
        //    if (txtdamt5.Text != "")
        //        idiscount6 = Convert.ToDecimal(txtdamt5.Text);

        //    Decimal iDisctotal = idiscount1 + idiscount2 + idiscount3 + idiscount4 + idiscount5 + idiscount6;
        //    txtdiscount.Text = Decimal.Round(iDisctotal).ToString("f2");

        //}

        //protected void New_Click(object sender, EventArgs e)
        //{
        //    string url = "itempage.aspx";
        //    string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        //}

        //protected void ddldef_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet dsStock = new DataSet();
        //    dsStock = objbs.GetStockDetails(Convert.ToInt32(ddldef.SelectedValue));
        //    if (dsStock.Tables[0].Rows.Count > 0)
        //    {
        //        txtTax.Text = "";
        //        txtrate.Text = "";

        //        txtrate.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
        //        //txtrate1.Text = Decimal.Round(Irate, 2).ToString("f2");
        //        DataSet dsCategory = objbs.GetTax(Convert.ToInt32(ddldef.SelectedValue));
        //        txtTax.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();
        //    }
        //}

        //protected void ddldef1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet dsStock = new DataSet();
        //    dsStock = objbs.GetStockDetails(Convert.ToInt32(ddldef1.SelectedValue));
        //    if (dsStock.Tables[0].Rows.Count > 0)
        //    {
        //        txtTax1.Text = "";
        //        txtrate1.Text = "";

        //        txtrate1.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
        //        //txtrate1.Text = Decimal.Round(Irate, 2).ToString("f2");
        //        DataSet dsCategory = objbs.GetTax(Convert.ToInt32(ddldef1.SelectedValue));
        //        txtTax1.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();
        //    }
        //}

        //protected void ddldef2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet dsStock = new DataSet();
        //    dsStock = objbs.GetStockDetails(Convert.ToInt32(ddldef2.SelectedValue));
        //    if (dsStock.Tables[0].Rows.Count > 0)
        //    {
        //        txtTax2.Text = "";
        //        txtrate2.Text = "";

        //        txtrate2.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
        //        //txtrate1.Text = Decimal.Round(Irate, 2).ToString("f2");
        //        DataSet dsCategory = objbs.GetTax(Convert.ToInt32(ddldef2.SelectedValue));
        //        txtTax2.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();
        //    }
        //}

        //protected void ddldef3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet dsStock = new DataSet();
        //    dsStock = objbs.GetStockDetails(Convert.ToInt32(ddldef3.SelectedValue));
        //    if (dsStock.Tables[0].Rows.Count > 0)
        //    {
        //        txtTax3.Text = "";
        //        txtrate3.Text = "";

        //        txtrate3.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
        //        //txtrate1.Text = Decimal.Round(Irate, 2).ToString("f2");
        //        DataSet dsCategory = objbs.GetTax(Convert.ToInt32(ddldef3.SelectedValue));
        //        txtTax3.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();
        //    }
        //}

        //protected void ddldef4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet dsStock = new DataSet();
        //    dsStock = objbs.GetStockDetails(Convert.ToInt32(ddldef4.SelectedValue));
        //    if (dsStock.Tables[0].Rows.Count > 0)
        //    {
        //        txtTax4.Text = "";
        //        txtrate4.Text = "";

        //        txtrate4.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
        //        //txtrate1.Text = Decimal.Round(Irate, 2).ToString("f2");
        //        DataSet dsCategory = objbs.GetTax(Convert.ToInt32(ddldef4.SelectedValue));
        //        txtTax4.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();
        //    }
        //}

        //protected void ddldef5_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet dsStock = new DataSet();
        //    dsStock = objbs.GetStockDetails(Convert.ToInt32(ddldef5.SelectedValue));
        //    if (dsStock.Tables[0].Rows.Count > 0)
        //    {
        //        txtTax5.Text = "";
        //        txtrate5.Text = "";

        //        txtrate5.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
        //        //txtrate1.Text = Decimal.Round(Irate, 2).ToString("f2");
        //        DataSet dsCategory = objbs.GetTax(Convert.ToInt32(ddldef5.SelectedValue));
        //        txtTax5.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();
        //    }

        //    //DataSet dSerial = objbs.selectSerial(Convert.ToString(ddldef5.SelectedItem));
        //    //ddlSno5.DataSource = dSerial.Tables[0];
        //    //ddlSno5.DataTextField = "Serial_No";
        //    //ddlSno5.DataBind();
        //}


        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    ddlcategory1.SelectedItem.Text = "Select Category";
        //    ddldef1.SelectedItem.Text = "Select Description";
        //    txtqty1.Text = "";
        //    txtrate1.Text = "0";
        //    //ddlSNo1.SelectedItem.Text = "";
        //    txtdisc1.Text = "0";
        //    txtamount1.Text = "0";
        //    txtTamt1.Text = "0";
        //}

        //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    ddlcategory2.SelectedItem.Text = "Select Category";

        //    ddldef2.SelectedItem.Text = "Select Description";
        //    txtqty2.Text = "";
        //    txtrate2.Text = "0";
        //    //ddlSno2.SelectedItem.Text = "";
        //    txtdisc2.Text = "0";
        //    txtamount2.Text = "0";
        //    txtTamt2.Text = "0";
        //}

        //protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        //{
        //    ddlcategory4.SelectedItem.Text = "Select Category";
        //    ddldef4.SelectedItem.Text = "Select Description";
        //    txtqty4.Text = "";
        //    txtrate4.Text = "0";
        //    //ddlSno4.SelectedItem.Text = "";
        //    txtdisc4.Text = "0";
        //    txtamount4.Text = "0";
        //    txtTamt4.Text = "0";
        //}

        //protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        //{
        //    ddlcategory5.SelectedItem.Text = "Select Category";
        //    ddldef5.SelectedItem.Text = "Select Description";
        //    txtqty5.Text = "";
        //    txtrate5.Text = "0";
        //    //ddlSno5.SelectedItem.Text = "";
        //    txtdisc5.Text = "0";
        //    txtamount5.Text = "0";
        //    txtTamt5.Text = "0";
        //}

        //protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        //{
        //    ddlcategory3.SelectedItem.Text = "Select Category";
        //    ddldef3.SelectedItem.Text = "Select Description";
        //    txtqty3.Text = "";
        //    txtrate3.Text = "0";
        //    //ddlSno3.SelectedItem.Text = "";
        //    txtdisc3.Text = "0";
        //    txtamount3.Text = "0";
        //    txtTamt3.Text = "0";

        //}

        protected void ddlPurchaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPurchaseType.SelectedValue == "2")
            {
                lblPurchaseOrder.Visible = true;
                ddlPurchaseOrder.Visible = true;

            }
            else
            {
                lblPurchaseOrder.Visible = false;
                ddlPurchaseOrder.Visible = false;
            }
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
        }

        protected void ddlPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsCustDet = objbs.PurchaseOrderDet(Convert.ToInt32(ddlPurchaseOrder.SelectedItem.Text), "tblPO_" + sTableName, "tblTransPO_" + sTableName);
            txtcity.Text = "";
            txtArea.Text = "";
            txtaddress.Text = "";
            txtpincode.Text = "";
            txtmobileno.Text = "";
            ddlvendor.SelectedValue = "Select Supplier";

            //txttotal.Text = "0";
            txtdiscount.Text = "0";
            //txtTaxamt5.Text = "0";
            txtTaxamt.Text = "0";
            txtgrandtotal.Text = "0";



            //ddlcategory.SelectedValue = "Select Category";
            //ddldef.SelectedValue = "Select Description";
            //txtqty.Text = "";
            //txtrate.Text = "0";
            //txtdisc.Text = "0";
            //txtTax.Text = "0";
            //txtamount.Text = "0";

            //ddlcategory1.SelectedValue = "Select Category";
            //ddldef1.SelectedValue = "Select Description";
            //txtqty1.Text = "";
            //txtrate1.Text = "0";
            //txtdisc1.Text = "0";
            //txtTax1.Text = "0";
            //txtamount1.Text = "0";

            //ddlcategory2.SelectedValue = "Select Category";
            //ddldef2.SelectedValue = "Select Description";
            //txtqty2.Text = "";
            //txtrate2.Text = "0";
            //txtdisc2.Text = "0";
            //txtTax2.Text = "0";
            //txtamount2.Text = "0";


            //ddlcategory3.SelectedValue = "Select Category";
            //ddldef3.SelectedValue = "Select Description";
            //txtqty3.Text = "";
            //txtrate3.Text = "0";
            //txtdisc3.Text = "0";
            //txtTax3.Text = "0";
            //txtamount3.Text = "0";


            //ddlcategory4.SelectedValue = "Select Category";
            //ddldef4.SelectedValue = "Select Description";
            //txtqty4.Text = "";
            //txtrate4.Text = "0";
            //txtdisc4.Text = "0";
            //txtTax4.Text = "0";
            //txtamount4.Text = "0";

            //ddlcategory5.SelectedValue = "Select Category";
            //ddldef5.SelectedValue = "Select Description";
            //txtqty5.Text = "";
            //txtrate5.Text = "0";
            //txtdisc5.Text = "0";
            //txtTax5.Text = "0";
            //txtamount5.Text = "0";






            if (dsCustDet != null)
            {
                if (dsCustDet.Tables[0].Rows.Count > 0)
                {
                    txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                    txtArea.Text = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                    txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                    txtpincode.Text = dsCustDet.Tables[0].Rows[0]["Pincode"].ToString();
                    txtmobileno.Text = dsCustDet.Tables[0].Rows[0]["MobileNo"].ToString();
                    ddlvendor.SelectedValue = dsCustDet.Tables[0].Rows[0]["LedgerID"].ToString();


                    DataSet trans = objbs.PurchaseOrderDetStatusNnn(Convert.ToInt32(ddlPurchaseOrder.SelectedItem.Text), "tblTransPO_" + sTableName, "tblPO_" + sTableName);

                    int Tpo = trans.Tables[0].Rows.Count;

                    if (trans != null)
                    {
                        if (trans.Tables[0].Rows.Count > 0)
                        {
                            DataTable dttt;
                            DataRow drNew;
                            DataColumn dct;
                            DataSet dstd = new DataSet();
                            dttt = new DataTable();
                            dct = new DataColumn("Purchaseid");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Category");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Product");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Stock");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("POQty");
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



                            dstd.Tables.Add(dttt);

                            foreach (DataRow dr in trans.Tables[0].Rows)
                            {
                                DataSet dsd = objbs.GetStockDetails(Convert.ToInt32(dr["itemname"]),"tblStock_" + sTableName);
                                drNew = dttt.NewRow();
                                drNew["POQty"] = dr["NQty"];
                                drNew["Qty"] = dr["NQty"];
                                drNew["Category"] = dr["categoryid"];
                                drNew["Rate"] = dr["Rate"];
                                drNew["Tax"] = dr["Tax"];
                                drNew["Amount"] = dr["Amount"];
                                drNew["Stock"] = dsd.Tables[0].Rows[0]["Available_Qty"].ToString();
                                drNew["Product"] = dr["itemname"];
                                drNew["Discount"] = dr["disc"];
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            ViewState["CurrentTable1"] = dttt;

                            GridView2.DataSource = dstd;
                            GridView2.DataBind();


                            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                            {
                                DropDownList txttttp = (DropDownList)GridView2.Rows[vLoop].FindControl("txtpurchaseid");
                                DropDownList txtt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                                TextBox txtPOQTY = (TextBox)GridView2.Rows[vLoop].FindControl("txtPOqty");
                                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                                TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                                TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                                TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                                TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                                txttttp.Text = dstd.Tables[0].Rows[vLoop]["Orderno"].ToString();
                                txtkttt.Text = dstd.Tables[0].Rows[vLoop]["Amount"].ToString();
                                txtPOQTY.Text = dstd.Tables[0].Rows[vLoop]["qty"].ToString();
                                txtttk.Text = dstd.Tables[0].Rows[vLoop]["qty"].ToString();
                                txtktttt.Text = dstd.Tables[0].Rows[vLoop]["Discount"].ToString();
                                txttk.Text = dstd.Tables[0].Rows[vLoop]["Rate"].ToString();
                                txtkt.Text = dstd.Tables[0].Rows[vLoop]["Tax"].ToString();
                                txt.SelectedValue = dstd.Tables[0].Rows[vLoop]["Product"].ToString();
                                txtt.SelectedValue = dstd.Tables[0].Rows[vLoop]["Category"].ToString();
                                txtktt.Text = dstd.Tables[0].Rows[vLoop]["Stock"].ToString();
                            }

                        }
                    }



                    if (Tpo <= 1)
                    {

                        //ddlcategory.SelectedValue = dsCustDet.Tables[0].Rows[0]["categoryId"].ToString();
                        //ddldef.SelectedValue = dsCustDet.Tables[0].Rows[0]["ItemName"].ToString();


                        //txtqty.Text = dsCustDet.Tables[0].Rows[0]["Qty"].ToString();
                        //txtrate.Text = dsCustDet.Tables[0].Rows[0]["Rate"].ToString();
                        //txtdisc.Text = dsCustDet.Tables[0].Rows[0]["Disc"].ToString();
                        //txtTax.Text = dsCustDet.Tables[0].Rows[0]["Tax"].ToString();
                        //txtamount.Text = dsCustDet.Tables[0].Rows[0]["Amount"].ToString();

                        //txttotal.Text = dsCustDet.Tables[0].Rows[0]["SubTotal"].ToString();
                        txtdiscount.Text = dsCustDet.Tables[0].Rows[0]["DiscAmt"].ToString();
                        //txtTaxamt5.Text = dsCustDet.Tables[0].Rows[0]["TAX_5"].ToString();
                        txtTaxamt.Text = dsCustDet.Tables[0].Rows[0]["TAX_14"].ToString();
                        txtgrandtotal.Text = dsCustDet.Tables[0].Rows[0]["GrandTotal"].ToString();
                    }

                    else if (Tpo <= 2)
                    {
                        //ddlcategory.SelectedValue = dsCustDet.Tables[0].Rows[0]["categoryId"].ToString();
                        //ddldef.SelectedValue = dsCustDet.Tables[0].Rows[0]["ItemName"].ToString();
                        //txtqty.Text = dsCustDet.Tables[0].Rows[0]["Qty"].ToString();
                        //txtrate.Text = dsCustDet.Tables[0].Rows[0]["Rate"].ToString();
                        //txtdisc.Text = dsCustDet.Tables[0].Rows[0]["Disc"].ToString();
                        //txtTax.Text = dsCustDet.Tables[0].Rows[0]["Tax"].ToString();
                        //txtamount.Text = dsCustDet.Tables[0].Rows[0]["Amount"].ToString();

                        //ddlcategory1.SelectedValue = dsCustDet.Tables[0].Rows[1]["categoryId"].ToString();
                        //ddldef1.SelectedValue = dsCustDet.Tables[0].Rows[1]["ItemName"].ToString();
                        //txtqty1.Text = dsCustDet.Tables[0].Rows[1]["Qty"].ToString();
                        //txtrate1.Text = dsCustDet.Tables[0].Rows[1]["Rate"].ToString();
                        //txtdisc1.Text = dsCustDet.Tables[0].Rows[1]["Disc"].ToString();
                        //txtTax1.Text = dsCustDet.Tables[0].Rows[1]["Tax"].ToString();
                        //txtamount1.Text = dsCustDet.Tables[0].Rows[1]["Amount"].ToString();


                        //txttotal.Text = dsCustDet.Tables[0].Rows[0]["SubTotal"].ToString();
                        txtdiscount.Text = dsCustDet.Tables[0].Rows[0]["DiscAmt"].ToString();
                        //txtTaxamt5.Text = dsCustDet.Tables[0].Rows[0]["TAX_5"].ToString();
                        txtTaxamt.Text = dsCustDet.Tables[0].Rows[0]["TAX_14"].ToString();
                        txtgrandtotal.Text = dsCustDet.Tables[0].Rows[0]["GrandTotal"].ToString();


                    }

                    else if (Tpo <= 3)
                    {

                        //ddlcategory.SelectedValue = dsCustDet.Tables[0].Rows[0]["categoryId"].ToString();
                        //ddldef.SelectedValue = dsCustDet.Tables[0].Rows[0]["ItemName"].ToString();
                        //txtqty.Text = dsCustDet.Tables[0].Rows[0]["Qty"].ToString();
                        //txtrate.Text = dsCustDet.Tables[0].Rows[0]["Rate"].ToString();
                        //txtdisc.Text = dsCustDet.Tables[0].Rows[0]["Disc"].ToString();
                        //txtTax.Text = dsCustDet.Tables[0].Rows[0]["Tax"].ToString();
                        //txtamount.Text = dsCustDet.Tables[0].Rows[0]["Amount"].ToString();

                        //ddlcategory1.SelectedValue = dsCustDet.Tables[0].Rows[1]["categoryId"].ToString();
                        //ddldef1.SelectedValue = dsCustDet.Tables[0].Rows[1]["ItemName"].ToString();
                        //txtqty1.Text = dsCustDet.Tables[0].Rows[1]["Qty"].ToString();
                        //txtrate1.Text = dsCustDet.Tables[0].Rows[1]["Rate"].ToString();
                        //txtdisc1.Text = dsCustDet.Tables[0].Rows[1]["Disc"].ToString();
                        //txtTax1.Text = dsCustDet.Tables[0].Rows[1]["Tax"].ToString();
                        //txtamount1.Text = dsCustDet.Tables[0].Rows[1]["Amount"].ToString();


                        //ddlcategory2.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef2.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty2.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate2.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc2.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax2.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount2.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();


                        //txttotal.Text = dsCustDet.Tables[0].Rows[0]["SubTotal"].ToString();
                        txtdiscount.Text = dsCustDet.Tables[0].Rows[0]["DiscAmt"].ToString();
                        //txtTaxamt5.Text = dsCustDet.Tables[0].Rows[0]["TAX_5"].ToString();
                        txtTaxamt.Text = dsCustDet.Tables[0].Rows[0]["TAX_14"].ToString();
                        txtgrandtotal.Text = dsCustDet.Tables[0].Rows[0]["GrandTotal"].ToString();


                    }
                    else if (Tpo <= 4)
                    {

                        //ddlcategory.SelectedValue = dsCustDet.Tables[0].Rows[0]["categoryId"].ToString();
                        //ddldef.SelectedValue = dsCustDet.Tables[0].Rows[0]["ItemName"].ToString();
                        //txtqty.Text = dsCustDet.Tables[0].Rows[0]["Qty"].ToString();
                        //txtrate.Text = dsCustDet.Tables[0].Rows[0]["Rate"].ToString();
                        //txtdisc.Text = dsCustDet.Tables[0].Rows[0]["Disc"].ToString();
                        //txtTax.Text = dsCustDet.Tables[0].Rows[0]["Tax"].ToString();
                        //txtamount.Text = dsCustDet.Tables[0].Rows[0]["Amount"].ToString();

                        //ddlcategory1.SelectedValue = dsCustDet.Tables[0].Rows[1]["categoryId"].ToString();
                        //ddldef1.SelectedValue = dsCustDet.Tables[0].Rows[1]["ItemName"].ToString();
                        //txtqty1.Text = dsCustDet.Tables[0].Rows[1]["Qty"].ToString();
                        //txtrate1.Text = dsCustDet.Tables[0].Rows[1]["Rate"].ToString();
                        //txtdisc1.Text = dsCustDet.Tables[0].Rows[1]["Disc"].ToString();
                        //txtTax1.Text = dsCustDet.Tables[0].Rows[1]["Tax"].ToString();
                        //txtamount1.Text = dsCustDet.Tables[0].Rows[1]["Amount"].ToString();

                        //ddlcategory2.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef2.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty2.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate2.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc2.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax2.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount2.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //ddlcategory3.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef3.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty3.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate3.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc3.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax3.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount3.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //txttotal.Text = dsCustDet.Tables[0].Rows[0]["SubTotal"].ToString();
                        txtdiscount.Text = dsCustDet.Tables[0].Rows[0]["DiscAmt"].ToString();
                        //txtTaxamt5.Text = dsCustDet.Tables[0].Rows[0]["TAX_5"].ToString();
                        txtTaxamt.Text = dsCustDet.Tables[0].Rows[0]["TAX_14"].ToString();
                        txtgrandtotal.Text = dsCustDet.Tables[0].Rows[0]["GrandTotal"].ToString();
                    }

                    else if (Tpo <= 5)
                    {

                        //ddlcategory.SelectedValue = dsCustDet.Tables[0].Rows[0]["categoryId"].ToString();
                        //ddldef.SelectedValue = dsCustDet.Tables[0].Rows[0]["ItemName"].ToString();
                        //txtqty.Text = dsCustDet.Tables[0].Rows[0]["Qty"].ToString();
                        //txtrate.Text = dsCustDet.Tables[0].Rows[0]["Rate"].ToString();
                        //txtdisc.Text = dsCustDet.Tables[0].Rows[0]["Disc"].ToString();
                        //txtTax.Text = dsCustDet.Tables[0].Rows[0]["Tax"].ToString();
                        //txtamount.Text = dsCustDet.Tables[0].Rows[0]["Amount"].ToString();

                        //ddlcategory1.SelectedValue = dsCustDet.Tables[0].Rows[1]["categoryId"].ToString();
                        //ddldef1.SelectedValue = dsCustDet.Tables[0].Rows[1]["ItemName"].ToString();
                        //txtqty1.Text = dsCustDet.Tables[0].Rows[1]["Qty"].ToString();
                        //txtrate1.Text = dsCustDet.Tables[0].Rows[1]["Rate"].ToString();
                        //txtdisc1.Text = dsCustDet.Tables[0].Rows[1]["Disc"].ToString();
                        //txtTax1.Text = dsCustDet.Tables[0].Rows[1]["Tax"].ToString();
                        //txtamount1.Text = dsCustDet.Tables[0].Rows[1]["Amount"].ToString();

                        //ddlcategory2.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef2.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty2.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate2.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc2.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax2.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount2.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //ddlcategory3.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef3.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty3.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate3.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc3.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax3.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount3.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //ddlcategory4.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef4.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty4.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate4.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc4.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax4.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount4.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //txttotal.Text = dsCustDet.Tables[0].Rows[0]["SubTotal"].ToString();
                        txtdiscount.Text = dsCustDet.Tables[0].Rows[0]["DiscAmt"].ToString();
                        //txtTaxamt5.Text = dsCustDet.Tables[0].Rows[0]["TAX_5"].ToString();
                        txtTaxamt.Text = dsCustDet.Tables[0].Rows[0]["TAX_14"].ToString();
                        txtgrandtotal.Text = dsCustDet.Tables[0].Rows[0]["GrandTotal"].ToString();
                    }
                    else if (Tpo <= 6)
                    {

                        //ddlcategory.SelectedValue = dsCustDet.Tables[0].Rows[0]["categoryId"].ToString();
                        //ddldef.SelectedValue = dsCustDet.Tables[0].Rows[0]["ItemName"].ToString();
                        //txtqty.Text = dsCustDet.Tables[0].Rows[0]["Qty"].ToString();
                        //txtrate.Text = dsCustDet.Tables[0].Rows[0]["Rate"].ToString();
                        //txtdisc.Text = dsCustDet.Tables[0].Rows[0]["Disc"].ToString();
                        //txtTax.Text = dsCustDet.Tables[0].Rows[0]["Tax"].ToString();
                        //txtamount.Text = dsCustDet.Tables[0].Rows[0]["Amount"].ToString();

                        //ddlcategory1.SelectedValue = dsCustDet.Tables[0].Rows[1]["categoryId"].ToString();
                        //ddldef1.SelectedValue = dsCustDet.Tables[0].Rows[1]["ItemName"].ToString();
                        //txtqty1.Text = dsCustDet.Tables[0].Rows[1]["Qty"].ToString();
                        //txtrate1.Text = dsCustDet.Tables[0].Rows[1]["Rate"].ToString();
                        //txtdisc1.Text = dsCustDet.Tables[0].Rows[1]["Disc"].ToString();
                        //txtTax1.Text = dsCustDet.Tables[0].Rows[1]["Tax"].ToString();
                        //txtamount1.Text = dsCustDet.Tables[0].Rows[1]["Amount"].ToString();

                        //ddlcategory2.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef2.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty2.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate2.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc2.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax2.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount2.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //ddlcategory3.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef3.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty3.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate3.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc3.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax3.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount3.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //ddlcategory4.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef4.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty4.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate4.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc4.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax4.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount4.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //ddlcategory5.SelectedValue = dsCustDet.Tables[0].Rows[2]["categoryId"].ToString();
                        //ddldef5.SelectedValue = dsCustDet.Tables[0].Rows[2]["ItemName"].ToString();
                        //txtqty5.Text = dsCustDet.Tables[0].Rows[2]["Qty"].ToString();
                        //txtrate5.Text = dsCustDet.Tables[0].Rows[2]["Rate"].ToString();
                        //txtdisc5.Text = dsCustDet.Tables[0].Rows[2]["Disc"].ToString();
                        //txtTax5.Text = dsCustDet.Tables[0].Rows[2]["Tax"].ToString();
                        //txtamount5.Text = dsCustDet.Tables[0].Rows[2]["Amount"].ToString();

                        //txttotal.Text = dsCustDet.Tables[0].Rows[0]["SubTotal"].ToString();
                        txtdiscount.Text = dsCustDet.Tables[0].Rows[0]["DiscAmt"].ToString();
                        //txtTaxamt5.Text = dsCustDet.Tables[0].Rows[0]["TAX_5"].ToString();
                        txtTaxamt.Text = dsCustDet.Tables[0].Rows[0]["TAX_14"].ToString();
                        txtgrandtotal.Text = dsCustDet.Tables[0].Rows[0]["GrandTotal"].ToString();
                    }
                }
            }
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
        }


        protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlvendor.SelectedItem.Text !="Select Supplier")
            {
            DataSet dsCustDet = objbs.LedgerDetailsList(Convert.ToInt32(ddlvendor.SelectedValue), sTableName);
            if (dsCustDet.Tables[0].Rows.Count > 0)
            {
                txtcity.Text = dsCustDet.Tables[0].Rows[0]["City"].ToString();
                txtaddress.Text = dsCustDet.Tables[0].Rows[0]["Address"].ToString();
                txtpincode.Text = dsCustDet.Tables[0].Rows[0]["Pincode"].ToString();
                txtmobileno.Text = dsCustDet.Tables[0].Rows[0]["MobileNo"].ToString();
                txtArea.Text = dsCustDet.Tables[0].Rows[0]["Area"].ToString();
                txtMailid.Text = dsCustDet.Tables[0].Rows[0]["Email"].ToString();
            }
            }
            txtNarration.Focus();

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
        }

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPayMode.SelectedValue == "Cheque" || ddlPayMode.SelectedValue == "DD")
            {
                ddlBank.Enabled = true;
                ddlChequeNo.Enabled = true;
                ddlBank.Focus();
            }
            else
            {
                ddlBank.ClearSelection();
                ddlChequeNo.ClearSelection();
               // ddlBank.SelectedItem.Text = "Select Bank Name";
               // ddlChequeNo.SelectedItem.Text = "Select Chequeno";
                ddlBank.Enabled = false;
                ddlChequeNo.Enabled = false;
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {

                    DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                    TextBox txtno = (TextBox)GridView2.Rows[vLoop].FindControl("txtno");
                    txtno.Focus();
                }
            }
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
        }

        protected void chknew_CheckedChanged(object sender, EventArgs e)
        {
            if (chknew.Checked == true)
            {
                ddlPurchaseType.Enabled = false;
                ddlPurchaseOrder.Visible = false;
                txtaddress.Text = "";
                txtcity.Text = "";
                txtpincode.Text = "";
                txtmobileno.Text = "";
                txtArea.Text = "";
                //ddlPayMode.SelectedItem.Text = "Select Payment Mode";
                //ddlPurchaseType.SelectedItem.Text = "Select Purchase Type";
                //txtDCNo.Text = "";
                //txtpono.Text = "";
                txtNarration.Text = "";
                ddlvendor.SelectedIndex = 0;
                ddlvendor.Visible = false;
                txtCustname.Visible = true;
                txtCustname.Text = "";
                txtMailid.Text = "";


                //RequiredFieldValidator8.Enabled = true;
                //RequiredFieldValidator9.Enabled = true;
                //RequiredFieldValidator10.Enabled = true;
                //RequiredFieldValidator11.Enabled = true;
                //RequiredFieldValidator12.Enabled = true;
                //RequiredFieldValidator13.Enabled = true;



                txtaddress.Enabled = true;
                txtpincode.Enabled = true;
                txtcity.Enabled = true;
                txtmobileno.Enabled = true;
                txtArea.Enabled = true;
            }
            else
            {
                ddlPurchaseType.Enabled = true;

                txtaddress.Text = "";
                txtcity.Text = "";
                txtpincode.Text = "";
                txtmobileno.Text = "";
                ddlvendor.SelectedIndex = 0;
                ddlvendor.Visible = true;
                txtCustname.Visible = false;
                txtCustname.Text = "";


                txtaddress.Enabled = false;
                txtpincode.Enabled = false;
                txtcity.Enabled = false;
                txtmobileno.Enabled = false;
                txtArea.Enabled = false;

            }
        }
        private int UpdateEditStock(int iCat, int iSubCat, double iQty)
        {
            double iAQty = 0;
                int iSuccess = 0;

            DataSet dsStock = objbs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            if (dsStock.Tables[0].Rows.Count > 0)
            {
                iAQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            }
            double iInsQty = iAQty - iQty;
            iSuccess = objbs.updateSalesStock(iInsQty, iCat, iSubCat, "tblStock_" + sTableName);

            return iSuccess;
        }

        //protected void txtTax_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax = Convert.ToDecimal(txtTax.Text);
        //    Decimal damt = Convert.ToDecimal(Dtax / 100) * Convert.ToDecimal(txtamount.Text);
        //    txtTamt.Text = Convert.ToString(damt);
        //}

        //protected void txtTax1_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax1 = Convert.ToDecimal(txtTax1.Text);
        //    Decimal damt1 = Convert.ToDecimal(Dtax1 / 100) * Convert.ToDecimal(txtamount.Text);
        //    txtTamt1.Text = Convert.ToString(damt1);
        //}

        //protected void txtTax2_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax2 = Convert.ToDecimal(txtTax2.Text);
        //    Decimal damt2 = Convert.ToDecimal(Dtax2 / 100) * Convert.ToDecimal(txtamount.Text);
        //    txtTamt2.Text = Convert.ToString(damt2);
        //}

        //protected void txtTax3_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax3 = Convert.ToDecimal(txtTax3.Text);
        //    Decimal damt3 = Convert.ToDecimal(Dtax3 / 100) * Convert.ToDecimal(txtamount.Text);
        //    txtTamt3.Text = Convert.ToString(damt3);
        //}

        //protected void txtTax4_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax4 = Convert.ToDecimal(txtTax4.Text);
        //    Decimal damt4 = Convert.ToDecimal(Dtax4 / 100) * Convert.ToDecimal(txtamount.Text);
        //    txtTamt4.Text = Convert.ToString(damt4);
        //}

        //protected void txtTax5_TextChanged(object sender, EventArgs e)
        //{
        //    Decimal Dtax5 = Convert.ToDecimal(txtTax5.Text);
        //    Decimal damt5 = Convert.ToDecimal(Dtax5 / 100) * Convert.ToDecimal(txtamount.Text);
        //    txtTamt5.Text = Convert.ToString(damt5);
        //}

        //protected void txtqty_TextChanged1(object sender, EventArgs e)
        //{
        //    Decimal iNetAmount = ((Convert.ToDecimal(txtqty.Text)) * (Convert.ToDecimal(txtrate.Text)));
        //    txtamount.Text = string.Format("{0:N2}", iNetAmount);

        //    Decimal iGross1 = 0; Decimal iGross2 = 0; Decimal iGross3 = 0; Decimal iGross4 = 0; Decimal iGross5 = 0; Decimal iGross6 = 0;
        //    if (txtamount.Text != "")
        //        iGross1 = Convert.ToDecimal(txtamount.Text);
        //    if (txtamount1.Text != "")
        //        iGross2 = Convert.ToDecimal(txtamount1.Text);
        //    if (txtamount2.Text != "")
        //        iGross3 = Convert.ToDecimal(txtamount2.Text);
        //    if (txtamount3.Text != "")
        //        iGross4 = Convert.ToDecimal(txtamount3.Text);
        //    if (txtamount4.Text != "")
        //        iGross5 = Convert.ToDecimal(txtamount4.Text);
        //    if (txtamount5.Text != "")
        //        iGross6 = Convert.ToDecimal(txtamount5.Text);
        //    Decimal iTotalAmount = iGross1 + iGross2 + iGross3 + iGross4 + iGross5 + iGross6;
        //    txttotal.Text = Decimal.Round(iTotalAmount, 2).ToString("f2");
        //    GrossCalc();
        //}

        //protected void txtqty1_TextChanged1(object sender, EventArgs e)
        //{
        //    Decimal iNetAmount1 = ((Convert.ToDecimal(txtqty1.Text)) * (Convert.ToDecimal(txtrate1.Text)));
        //    txtamount1.Text = string.Format("{0:N2}", iNetAmount1);

        //    Decimal iGross1 = 0; Decimal iGross2 = 0; Decimal iGross3 = 0; Decimal iGross4 = 0; Decimal iGross5 = 0; Decimal iGross6 = 0;
        //    if (txtamount.Text != "")
        //        iGross1 = Convert.ToDecimal(txtamount.Text);
        //    if (txtamount1.Text != "")
        //        iGross2 = Convert.ToDecimal(txtamount1.Text);
        //    if (txtamount2.Text != "")
        //        iGross3 = Convert.ToDecimal(txtamount2.Text);
        //    if (txtamount3.Text != "")
        //        iGross4 = Convert.ToDecimal(txtamount3.Text);
        //    if (txtamount4.Text != "")
        //        iGross5 = Convert.ToDecimal(txtamount4.Text);
        //    if (txtamount5.Text != "")
        //        iGross6 = Convert.ToDecimal(txtamount5.Text);
        //    Decimal iTotalAmount = iGross1 + iGross2 + iGross3 + iGross4 + iGross5 + iGross6;
        //    txttotal.Text = Decimal.Round(iTotalAmount, 2).ToString("f2");
        //    GrossCalc();
        //}

        //protected void txtqty2_TextChanged1(object sender, EventArgs e)
        //{
        //    Decimal iNetAmount2 = ((Convert.ToDecimal(txtqty2.Text)) * (Convert.ToDecimal(txtrate2.Text)));
        //    txtamount2.Text = string.Format("{0:N2}", iNetAmount2);

        //    Decimal iGross1 = 0; Decimal iGross2 = 0; Decimal iGross3 = 0; Decimal iGross4 = 0; Decimal iGross5 = 0; Decimal iGross6 = 0;
        //    if (txtamount.Text != "")
        //        iGross1 = Convert.ToDecimal(txtamount.Text);
        //    if (txtamount1.Text != "")
        //        iGross2 = Convert.ToDecimal(txtamount1.Text);
        //    if (txtamount2.Text != "")
        //        iGross3 = Convert.ToDecimal(txtamount2.Text);
        //    if (txtamount3.Text != "")
        //        iGross4 = Convert.ToDecimal(txtamount3.Text);
        //    if (txtamount4.Text != "")
        //        iGross5 = Convert.ToDecimal(txtamount4.Text);
        //    if (txtamount5.Text != "")
        //        iGross6 = Convert.ToDecimal(txtamount5.Text);
        //    Decimal iTotalAmount = iGross1 + iGross2 + iGross3 + iGross4 + iGross5 + iGross6;
        //    txttotal.Text = Decimal.Round(iTotalAmount, 2).ToString("f2");
        //    GrossCalc();
        //}

        //protected void txtqty3_TextChanged1(object sender, EventArgs e)
        //{
        //    Decimal iNetAmount3 = ((Convert.ToDecimal(txtqty3.Text)) * (Convert.ToDecimal(txtrate3.Text)));
        //    txtamount3.Text = string.Format("{0:N2}", iNetAmount3);

        //    Decimal iGross1 = 0; Decimal iGross2 = 0; Decimal iGross3 = 0; Decimal iGross4 = 0; Decimal iGross5 = 0; Decimal iGross6 = 0;
        //    if (txtamount.Text != "")
        //        iGross1 = Convert.ToDecimal(txtamount.Text);
        //    if (txtamount1.Text != "")
        //        iGross2 = Convert.ToDecimal(txtamount1.Text);
        //    if (txtamount2.Text != "")
        //        iGross3 = Convert.ToDecimal(txtamount2.Text);
        //    if (txtamount3.Text != "")
        //        iGross4 = Convert.ToDecimal(txtamount3.Text);
        //    if (txtamount4.Text != "")
        //        iGross5 = Convert.ToDecimal(txtamount4.Text);
        //    if (txtamount5.Text != "")
        //        iGross6 = Convert.ToDecimal(txtamount5.Text);
        //    Decimal iTotalAmount = iGross1 + iGross2 + iGross3 + iGross4 + iGross5 + iGross6;
        //    txttotal.Text = Decimal.Round(iTotalAmount, 2).ToString("f2");
        //    GrossCalc();
        //}

        //protected void txtqty4_TextChanged1(object sender, EventArgs e)
        //{
        //    Decimal iNetAmount4 = ((Convert.ToDecimal(txtqty4.Text)) * (Convert.ToDecimal(txtrate4.Text)));
        //    txtamount4.Text = string.Format("{0:N2}", iNetAmount4);

        //    Decimal iGross1 = 0; Decimal iGross2 = 0; Decimal iGross3 = 0; Decimal iGross4 = 0; Decimal iGross5 = 0; Decimal iGross6 = 0;
        //    if (txtamount.Text != "")
        //        iGross1 = Convert.ToDecimal(txtamount.Text);
        //    if (txtamount1.Text != "")
        //        iGross2 = Convert.ToDecimal(txtamount1.Text);
        //    if (txtamount2.Text != "")
        //        iGross3 = Convert.ToDecimal(txtamount2.Text);
        //    if (txtamount3.Text != "")
        //        iGross4 = Convert.ToDecimal(txtamount3.Text);
        //    if (txtamount4.Text != "")
        //        iGross5 = Convert.ToDecimal(txtamount4.Text);
        //    if (txtamount5.Text != "")
        //        iGross6 = Convert.ToDecimal(txtamount5.Text);
        //    Decimal iTotalAmount = iGross1 + iGross2 + iGross3 + iGross4 + iGross5 + iGross6;
        //    txttotal.Text = Decimal.Round(iTotalAmount, 2).ToString("f2");
        //    GrossCalc();
        //}

        //protected void txtqty5_TextChanged1(object sender, EventArgs e)
        //{
        //    Decimal iNetAmount5 = ((Convert.ToDecimal(txtqty5.Text)) * (Convert.ToDecimal(txtrate5.Text)));
        //    txtamount5.Text = string.Format("{0:N2}", iNetAmount5);

        //    Decimal iGross1 = 0; Decimal iGross2 = 0; Decimal iGross3 = 0; Decimal iGross4 = 0; Decimal iGross5 = 0; Decimal iGross6 = 0;
        //    if (txtamount.Text != "")
        //        iGross1 = Convert.ToDecimal(txtamount.Text);
        //    if (txtamount1.Text != "")
        //        iGross2 = Convert.ToDecimal(txtamount1.Text);
        //    if (txtamount2.Text != "")
        //        iGross3 = Convert.ToDecimal(txtamount2.Text);
        //    if (txtamount3.Text != "")
        //        iGross4 = Convert.ToDecimal(txtamount3.Text);
        //    if (txtamount4.Text != "")
        //        iGross5 = Convert.ToDecimal(txtamount4.Text);
        //    if (txtamount5.Text != "")
        //        iGross6 = Convert.ToDecimal(txtamount5.Text);
        //    Decimal iTotalAmount = iGross1 + iGross2 + iGross3 + iGross4 + iGross5 + iGross6;
        //    txttotal.Text = Decimal.Round(iTotalAmount, 2).ToString("f2");
        //    GrossCalc();
        //}

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseEntryGrid.aspx");
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            if (ddlProvince.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Province type')", true);

                return;
            }

            else
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;

                DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

                DropDownList Def = (DropDownList)row.FindControl("drpItem");

                DropDownList procode = (DropDownList)row.FindControl("ProductCode");

                if (drpCategory.SelectedItem.Text != "Select Category")
                {

                    DataSet dsCategory1 = objbs.selectcategorydecriptionN(Convert.ToInt32(drpCategory.SelectedValue), sTableName);
                    if (dsCategory1.Tables[0].Rows.Count > 0)
                    {
                        Def.Items.Clear();
                        Def.DataSource = dsCategory1.Tables[0];
                        Def.DataTextField = "serial_NO";
                        Def.DataValueField = "categoryuserid";
                        Def.DataBind();
                        //   Def.Items.Insert(0, "Select Product");

                    }
                    else
                    {
                        Def.Items.Clear();
                        Def.Items.Insert(0, "Select Product");
                    }
                }
                else
                {
                }

                if (drpCategory.SelectedItem.Text != "Select Category")
                {

                    DataSet dsCategory1 = objbs.selectcategorydecriptionN(Convert.ToInt32(drpCategory.SelectedValue), sTableName);
                    if (dsCategory1.Tables[0].Rows.Count > 0)
                    {
                        procode.Items.Clear();
                        procode.DataSource = dsCategory1.Tables[0];
                        procode.DataTextField = "Definition";
                        procode.DataValueField = "categoryuserid";
                        procode.DataBind();
                        //procode.Items.Insert(0, "Select Product Code");

                    }
                    else
                    {
                        procode.Items.Clear();
                        procode.Items.Insert(0, "Select Product Code");
                    }
                }
                else
                {
                }

                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txt = (TextBox)row.FindControl("txtDiscount");
                TextBox txtTax = (TextBox)row.FindControl("txtTax");
                TextBox txtcst = (TextBox)row.FindControl("txtcst");
                DropDownList Deff = (DropDownList)row.FindControl("drpItem");
                DropDownList cate = (DropDownList)row.FindControl("drpCategory");
                TextBox txtQty = (TextBox)row.FindControl("txtStock");
                DataSet dsStock = new DataSet();

                if (Deff.SelectedItem.Text != "Select Product")
                {

                    dsStock = objbs.GetStockDetails(Convert.ToInt32(Deff.SelectedValue), "tblStock_" + sTableName);

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        DataSet dsCategory = objbs.GetTax(Convert.ToInt32(Deff.SelectedValue), sTableName);

                        double Itx = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
                        txtcst.Text = Itx.ToString();

                        cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

                        Decimal Irate = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString());
                        txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");

                        double sQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
                        txtQty.Text = sQty.ToString("0.00");

                        txt.Text = "0";
                        txtTax.Text = "0";
                    }
                }
                else
                {

                }

                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {

                    TextBox txtQtty = (TextBox)GridView2.Rows[vLoop].FindControl("txtQty");
                    //TextBox txtref = (TextBox)GridView2.Rows[vLoop].FindControl("txtrefno");
                    txtQtty.Focus();
                }
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                    txtno.Text = Convert.ToString(i + 1);
                }
            }
        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {


            DropDownList ddl1 = (DropDownList)sender;
            GridViewRow row1 = (GridViewRow)ddl1.NamingContainer;

            DropDownList drpCategory1 = (DropDownList)row1.FindControl("drpCategory");
            DropDownList Defitem1 = (DropDownList)row1.FindControl("drpItem");

         

            if (Defitem1.SelectedItem.Text != "Select Product")
            {

                DataSet dsCategory11 = objbs.selectProduct(Convert.ToInt32(Defitem1.SelectedValue), sTableName);
                if (dsCategory11.Tables[0].Rows.Count > 0)
                {
                    drpCategory1.Items.Clear();
                    drpCategory1.DataSource = dsCategory11.Tables[0];
                    drpCategory1.DataTextField = "categoryname";
                    drpCategory1.DataValueField = "categoryid";
                    drpCategory1.DataBind();
                }
                else
                {
                    //drpCategory.Items.Clear();
                    //drpCategory.Items.Insert(0, "Select Category");
                }
            }
            else
            {


            }




            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txti = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                    TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                    TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                    TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                    TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                    TextBox txtref = (TextBox)GridView2.Rows[vLoop].FindControl("txtrefno");

                    itemc = txti.Text;


                    if ((itemc == null) || (itemc == ""))
                    {
                    }
                    else
                    {
                        for (int vLoop1 = 0; vLoop1 < GridView2.Rows.Count; vLoop1++)
                        {
                            DropDownList txt1 = (DropDownList)GridView2.Rows[vLoop1].FindControl("drpItem");
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
                                        //itemcd = txti.SelectedItem.Text;
                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                                        //txt1.Focus();
                                        //return;

                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                    }
                    iq = iq + 1;
                    ii = 1;

                    //DataSet dsStock = new DataSet();

                    //if ((txtktt.Text == "") && (txtkt.Text == "") && (txttk.Text == "") || (txtkttt.Text == ""))
                    //{
                    //    dsStock = objBs.GetStockDetails(Convert.ToInt32(txt.SelectedValue));

                    //    if (dsStock.Tables[0].Rows.Count > 0)
                    //    {
                    //        txttk.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
                    //        txtktt.Text = dsStock.Tables[0].Rows[0]["Available_Qty"].ToString();

                    //        DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue));
                    //        txtkt.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();

                    //        txtkttt.Text = "0";
                    //    }
                    //}
                }
            }
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

            DropDownList Defitem = (DropDownList)row.FindControl("drpItem");

            DropDownList procode = (DropDownList)row.FindControl("ProductCode");

            if (procode.SelectedItem.Text != "Select Product")
            {

                DataSet dsCategory1 = objbs.selectProduct(Convert.ToInt32(Defitem.SelectedValue), sTableName);
                if (dsCategory1.Tables[0].Rows.Count > 0)
                {
                    drpCategory.Items.Clear();
                    drpCategory.DataSource = dsCategory1.Tables[0];
                    drpCategory.DataTextField = "categoryname";
                    drpCategory.DataValueField = "categoryid";
                    drpCategory.DataBind();
                    //drpCategory.Items.Insert(0, "Select Category");

                }
                else
                {
                    drpCategory.Items.Clear();
                    drpCategory.Items.Insert(0, "Select Category");
                }
            }
            else
            {
            }

            if (procode.SelectedItem.Text != "Select Product")
            {

                DataSet dsCategory1 = objbs.selectProduct(Convert.ToInt32(Defitem.SelectedValue), sTableName);
                if (dsCategory1.Tables[0].Rows.Count > 0)
                {
                    procode.SelectedValue = dsCategory1.Tables[0].Rows[0]["CategoryUserID"].ToString();
                    string cmpyid = Session["cmpyid"].ToString();
                    DataSet dst = new DataSet();
                    dst = objbs.selectcategoryalldecriptionbranch(sTableName,"2",cmpyid);
                    procode.Items.Clear();
                    procode.DataSource = dst.Tables[0];
                    procode.DataTextField = "Definition";
                    procode.DataValueField = "categoryuserid";
                    procode.DataBind();


                }
                else
                {
                    procode.Items.Clear();
                    procode.Items.Insert(0, "Select Product Code");
                }
            }
            else
            {
            }



           
            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            TextBox txt = (TextBox)row.FindControl("txtDiscount");
            TextBox txtTax = (TextBox)row.FindControl("txtTax");
            TextBox txtcst = (TextBox)row.FindControl("txtcst");
            DropDownList Def = (DropDownList)row.FindControl("drpItem");
            DropDownList cate = (DropDownList)row.FindControl("drpCategory");
            TextBox txtQty = (TextBox)row.FindControl("txtStock");
            DataSet dsStock = new DataSet();

            if (Def.SelectedItem.Text != "Select Product")
            {

                dsStock = objbs.GetStockDetails(Convert.ToInt32(Def.SelectedValue), "tblStock_" + sTableName);

                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    DataSet dsCategory = objbs.GetTax(Convert.ToInt32(Def.SelectedValue), sTableName);

                    double Itx = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
                    txtcst.Text = Itx.ToString();

                    txtcst.Text = Itx.ToString();

                    cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

                    Decimal Irate = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString());
                    txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");

                    double sQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
                    txtQty.Text = sQty.ToString("0.00");

                    txt.Text = "0";
                    txtTax.Text = "0";
                }
               
            }
            else
            {

            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
              
                TextBox txtQtty = (TextBox)GridView2.Rows[vLoop].FindControl("txtQty");
                //TextBox txtref = (TextBox)GridView2.Rows[vLoop].FindControl("txtrefno");
                txtQtty.Focus();
            }
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
        }


        private void SetPreviousData1()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txttno =
                        (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtno");

                        DropDownList drpCategory =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("drpCategory");
                        TextBox txtStock =
                          (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtStock");
                        DropDownList productCode =
                        (DropDownList)GridView2.Rows[rowIndex].Cells[2].FindControl("productCode");

                        TextBox txtRef =
                      (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtrefno");

                        TextBox txtcer =
                       (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtCerno");

                        DropDownList drpItem =
                          (DropDownList)GridView2.Rows[rowIndex].Cells[2].FindControl("drpItem");
                        TextBox TextBoxAmount =
                          (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                        TextBox txtRate =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtRate");
                        TextBox txtQty =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtQty");

                        TextBox txtPOQTY =
                        (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtPOQty");

                        TextBox txtDiscount =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtDiscount");
                        TextBox txtTax =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtTax");

                        TextBox txtcst =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtcst");

                        drpCategory.SelectedValue = dt.Rows[i]["Category"].ToString();

                        drpItem.Items.Clear();


                        //if ((drpCategory.SelectedValue != "0") && (drpCategory.SelectedValue != "Select Category"))
                        //{
                        //    DataSet dsCategory = objbs.selectcategorydecription(Convert.ToInt32(drpCategory.SelectedValue),sTableName);
                        //    drpItem.Items.Add(new ListItem("Select Product", "0"));
                        //    drpItem.DataSource = dsCategory;
                        //    drpItem.DataBind();
                        //    drpItem.DataTextField = "Definition";
                        //    drpItem.DataValueField = "categoryuserid";
                        //}
                        string cmpyid = Session["cmpyid"].ToString();
                        DataSet dst = objbs.selectcategoryalldecriptionbranch(sTableName,"2",cmpyid);
                        drpItem.Items.Add(new ListItem("Select Product Code", "0"));
                        drpItem.DataSource = dst;
                        drpItem.DataBind();
                        drpItem.DataTextField = "Serial_No";
                        drpItem.DataValueField = "categoryuserid";




                        //productCode.DataSource = dst;
                        //productCode.DataTextField = "Definition";
                        //productCode.DataValueField = "categoryuserid";
                        //productCode.DataBind();
                        //productCode.Items.Insert(0, "Select Product");


                        txtStock.Text = dt.Rows[i]["Stock"].ToString();
                        TextBoxAmount.Text = dt.Rows[i]["Amount"].ToString();
                        productCode.SelectedValue = dt.Rows[i]["ProductCode"].ToString();
                        drpItem.SelectedValue = dt.Rows[i]["Product"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txttno.Text = dt.Rows[i]["OrderNo"].ToString();
                        txtQty.Text = dt.Rows[i]["Qty"].ToString();
                        txtPOQTY.Text = dt.Rows[i]["POQty"].ToString();
                        //txtRef.Text = dt.Rows[i]["Refno"].ToString();
                        //txtcer.Text = dt.Rows[i]["cerno"].ToString();

                        txtDiscount.Text = dt.Rows[i]["Discount"].ToString();
                        txtTax.Text = dt.Rows[i]["Tax"].ToString();
                        txtcst.Text = dt.Rows[i]["cst"].ToString();

                        rowIndex++;

                    }
                }
            }
        }


        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {









            DataSet ds = new DataSet();
            ds = objbs.selectcategorymasterbtn(btnadd.Text, compayid);

            DataSet dst = new DataSet();
            dst = objbs.scategoryalldecriptionbtnname(btnadd.Text, compayid);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtno = (TextBox)e.Row.FindControl("txtno");
                DropDownList txttt = (DropDownList)e.Row.FindControl("drpCategory");
                DropDownList txt = (DropDownList)e.Row.FindControl("drpItem");
                DropDownList txtd = (DropDownList)e.Row.FindControl("productCode");
                TextBox txtttk = (TextBox)e.Row.FindControl("txtqty");
                TextBox txtPOQTY = (TextBox)e.Row.FindControl("txtPOQty");
                TextBox txttk = (TextBox)e.Row.FindControl("txtRate");
                TextBox txtkt = (TextBox)e.Row.FindControl("txtTax");
                TextBox txtkttt = (TextBox)e.Row.FindControl("txtAmount");
                TextBox txtktt = (TextBox)e.Row.FindControl("txtStock");
                TextBox txtktttt = (TextBox)e.Row.FindControl("txtDiscount");
                TextBox txtkttttt = (TextBox)e.Row.FindControl("txtcst");
                txtno.Text = "1";
                txtPOQTY.Text = "0";
                txtttk.Text = "0";
                txttk.Text = "0";
                txtkt.Text = "0";
                txtkttt.Text = "0";
                txtktt.Text = "0";
                txtktttt.Text = "0";
                txtkttttt.Text = "0";



                var ddl = (DropDownList)e.Row.FindControl("drpCategory");
                ddl.DataSource = ds;
                ddl.DataTextField = "category";
                ddl.DataValueField = "categoryid";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Category");

                var ddlt = (DropDownList)e.Row.FindControl("drpItem");
                ddlt.DataSource = dst;
                ddlt.DataTextField = "Definition";
                ddlt.DataValueField = "categoryuserid";
                ddlt.DataBind();
                ddlt.Items.Insert(0, "Select Product");

                var ddlPcode = (DropDownList)e.Row.FindControl("ProductCode");
                ddlPcode.DataSource = dst;
                ddlPcode.DataTextField = "Definition";
                ddlPcode.DataValueField = "categoryuserid";
                ddlPcode.DataBind();
                ddlPcode.Items.Insert(0, "Select Product Code");


            }
        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBank.SelectedItem.Text !="Select Bank Name")
            {
            DataSet ds = objbs.getBankLedger(4, Convert.ToInt32(ddlBank.SelectedValue), sTableName);
            ddlChequeNo.Items.Clear();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlChequeNo.DataSource = ds;
                    ddlChequeNo.DataTextField = "chequeno";
                    ddlChequeNo.DataValueField = "TransChequeId";
                    ddlChequeNo.DataBind();
                    ddlChequeNo.Items.Insert(0, "Select Chequeno");

                }
                else
                {
                    ddlChequeNo.Items.Insert(0, "Select Chequeno");
                }
            }
            else
            {
                ddlChequeNo.Items.Insert(0, "Select Chequeno");
            }
        }
            ddlBank.Focus();


            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
        }

        protected void productCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txti = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                    TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                   

                    TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                    TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                    TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                    itemc = txti.Text;


                    if ((itemc == null) || (itemc == ""))
                    {
                    }
                    else
                    {
                        for (int vLoop1 = 0; vLoop1 < GridView2.Rows.Count; vLoop1++)
                        {
                            DropDownList txt1 = (DropDownList)GridView2.Rows[vLoop1].FindControl("ProductCode");
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
                                        //itemcd = txti.SelectedItem.Text;
                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                                        //txt1.Focus();
                                        //return;

                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                    }
                    iq = iq + 1;
                    ii = 1;

                    //DataSet dsStock = new DataSet();

                    //if ((txtktt.Text == "") && (txtkt.Text == "") && (txttk.Text == "") || (txtkttt.Text == ""))
                    //{
                    //    dsStock = objBs.GetStockDetails(Convert.ToInt32(txt.SelectedValue));

                    //    if (dsStock.Tables[0].Rows.Count > 0)
                    //    {
                    //        txttk.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
                    //        txtktt.Text = dsStock.Tables[0].Rows[0]["Available_Qty"].ToString();

                    //        DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue));
                    //        txtkt.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();

                    //        txtkttt.Text = "0";
                    //    }
                    //}
                }
            }


            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            if (ViewState["CurrentTable1"] != null)
            {

                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

                    DropDownList Def = (DropDownList)row.FindControl("drpItem");



                    DropDownList procode = (DropDownList)row.FindControl("ProductCode");

                    if (procode.SelectedItem.Text != "Select Product Code")
                    {

                        DataSet dsCategory1 = objbs.selectProduct(Convert.ToInt32(procode.SelectedValue), sTableName);
                        if (dsCategory1.Tables[0].Rows.Count > 0)
                        {
                            drpCategory.Items.Clear();
                            drpCategory.DataSource = dsCategory1.Tables[0];
                            drpCategory.DataTextField = "categoryname";
                            drpCategory.DataValueField = "categoryid";
                            drpCategory.DataBind();
                            //drpCategory.Items.Insert(0, "Select Category");

                        }
                        else
                        {
                            drpCategory.Items.Clear();
                            drpCategory.Items.Insert(0, "Select Category");
                        }
                    }
                    else
                    {
                    }

                    if (procode.SelectedItem.Text != "Select Product Code")
                    {

                        DataSet dsCategory1 = objbs.selectProduct(Convert.ToInt32(procode.SelectedValue), sTableName);
                        if (dsCategory1.Tables[0].Rows.Count > 0)
                        {
                            string cmpyid = Session["cmpyid"].ToString();
                            DataSet dst = new DataSet();
                            dst = objbs.selectcategoryalldecriptionbranch(sTableName,"2",cmpyid);
                            Def.Items.Clear();
                            Def.DataSource = dst.Tables[0];
                            Def.DataTextField = "serial_NO";
                            Def.DataValueField = "categoryuserid";
                            Def.DataBind();

                            Def.SelectedValue = dsCategory1.Tables[0].Rows[0]["CategoryUserID"].ToString();

                        }
                        else
                        {
                            Def.Items.Clear();
                            Def.Items.Insert(0, "Select Product");
                        }
                    }
                    else
                    {
                    }
                }
            }


            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            TextBox txt = (TextBox)row.FindControl("txtDiscount");
            TextBox txtTax = (TextBox)row.FindControl("txtTax");
            TextBox txtcst = (TextBox)row.FindControl("txtcst");
            DropDownList Defitem = (DropDownList)row.FindControl("drpItem");
            DropDownList cate = (DropDownList)row.FindControl("drpCategory");
            DropDownList ProductCode = (DropDownList)row.FindControl("productCode");
            TextBox txtQty = (TextBox)row.FindControl("txtStock");
            DataSet dsStock = new DataSet();


            dsStock = objbs.GetStockDetails(Convert.ToInt32(ProductCode.SelectedValue), "tblStock_" + sTableName);

            if (dsStock.Tables[0].Rows.Count > 0)
            {
                DataSet dsCategory = objbs.GetTax(Convert.ToInt32(ProductCode.SelectedValue), sTableName);

                var Itx = Convert.ToDecimal(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
                txtTax.Text = Itx.ToString();

                Decimal Irate = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString());
                txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");

                decimal sQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
                txtQty.Text = sQty.ToString("f2");
                cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

                txt.Text = "0";

                //string value = ProductCode.SelectedValue;
                //DataSet ds = objbs.itemhistorypopup(sTableName, value);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    txtitemhis.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
                //}
                //else
                //{
                //    txtitemhis.Text = "";
                //}


                //string cust = ddlcustomerID.SelectedValue;
                //if (cust == "Select Customer")
                //{
                //}
                //else
                //{
                //    DataSet ds1 = objbs.custhistorypopup(sTableName, value, cust);
                //    if (ds1.Tables[0].Rows.Count > 0)
                //    {
                //        txtcusthis.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
                //    }
                //    else
                //    {
                //        txtcusthis.Text = "";
                //    }
                //}

                // txtTamt5.Text = dsCategory.Tables[0].Rows[0]["Meter1"].ToString();
            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {

                TextBox txtQtty = (TextBox)GridView2.Rows[vLoop].FindControl("txtQty");
                txtQtty.Focus();
            }
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox txtno = (TextBox)GridView2.Rows[i].FindControl("txtno");
                txtno.Text = Convert.ToString(i + 1);
            }
        }

        protected void gridbutton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseEntryGrid.aspx");
        }

        protected void granddiscount(object sender, EventArgs e)
        {
            if (txtgrandtotal.Text != "")
            {
                double grandtotal = 0;
                double tax = 0;
                double distotal = 0;
                double tottqty = 0;
                double mettt = 0;
                double r = 0;

                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                    TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                    TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                    TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                    TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                    TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");
                    TextBox txtkttttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtcst");

                    if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
                    {

                    }
                    else
                    {
                        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                        //double cst = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkttttt.Text) / 100;
                        double total = tx + DiscountAmount ;

                        //  txtkttt.Text = string.Format("{0:N2}", total);

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

                txtgrandtotal.Text = string.Format("{0:N2}", grandtotal);
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

                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                    TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtqty");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                    TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");
                    TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtAmount");
                    TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                    TextBox txtktttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                    if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
                    {

                    }
                    else
                    {
                        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

                        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                        double total = tx + DiscountAmount;
                       

                        //  txtkttt.Text = string.Format("{0:N2}", total);

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

                        grandtotal = grandtotal + total ;
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

    }
}




