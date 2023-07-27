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
using System.Text.RegularExpressions;


namespace Billing.Accountsbootstrap
{
    public partial class Fabprocess : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iDealer = "";
        string empid = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            empid = Session["Empid"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                btnPrint.Visible = false;
                btnDelete.Visible = false;

                ddlordno.Enabled = false;
                DataSet brandName = new DataSet();
                brandName = objBs.getBrandName();

                if (brandName != null)
                {
                    if (brandName.Tables[0].Rows.Count > 0)
                    {
                        ddlBrand.DataSource = brandName.Tables[0];
                        ddlBrand.DataTextField = "BrandName";
                        ddlBrand.DataValueField = "BrandId";
                        ddlBrand.DataBind();
                        ddlBrand.Items.Insert(0, "Select Brand");
                    }
                }

                DataSet dcompany = objBs.GetCompanyDetails();
                if (dcompany.Tables[0].Rows.Count > 0)
                {
                    drpcompany.DataSource = dcompany.Tables[0];
                    drpcompany.DataTextField = "Companycode";
                    drpcompany.DataValueField = "comapanyId";
                    drpcompany.DataBind();
                    //  ddlBrand.Items.Insert(0, "Select Company");
                }

                DataSet dsd = new DataSet();
                dsd = objBs.getorder();

                if (dsd != null)
                {
                    if (dsd.Tables[0].Rows.Count > 0)
                    {
                        ddlordno.DataSource = dsd.Tables[0];
                        ddlordno.DataTextField = "orderno";
                        ddlordno.DataValueField = "orderid";
                        ddlordno.DataBind();
                        ddlordno.Items.Insert(0, "Select ");
                    }
                }

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                DataSet ds = new DataSet();
                ds = objBs.getmaaxBillno();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtinvno.Text = ds.Tables[0].Rows[0]["billId"].ToString();
                    FirstGridViewRow();

                    DataSet dsup = objBs.getnewsupplierforfab();
                    if (dsup.Tables[0].Rows.Count > 0)
                    {

                        drpsupplier.DataSource = dsup.Tables[0];
                        drpsupplier.DataTextField = "LEdgerName";
                        drpsupplier.DataValueField = "LedgerID";
                        drpsupplier.DataBind();
                        drpsupplier.Items.Insert(0, "Select Supplier");
                    }

                    DataSet dst = objBs.hrmgridviewnew();
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            drpchecked.DataSource = dst.Tables[0];
                            drpchecked.DataTextField = "Name";
                            drpchecked.DataValueField = "Employee_Id";
                            drpchecked.DataBind();
                            drpchecked.Items.Insert(0, "Select Employee Name");
                        }
                    }
                    txtregdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtinvdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }

                #region "Filling Details for Update"

                string iFabID = Request.QueryString.Get("iid");
                if (iFabID != "" && iFabID != null)
                {
                    DataSet dsFab = objBs.Get_Fabric(Convert.ToInt32(iFabID));
                    if (dsFab.Tables[0].Rows.Count > 0)
                    {
                        txtregdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        btnadd.Text = "Update";


                        DataSet dsorder = new DataSet();
                        dsorder = objBs.getorderNo();

                        if (dsorder != null)
                        {
                            if (dsorder.Tables[0].Rows.Count > 0)
                            {
                                ddlordno.DataSource = dsorder.Tables[0];
                                ddlordno.DataTextField = "orderno";
                                ddlordno.DataValueField = "orderid";
                                ddlordno.DataBind();
                                ddlordno.Items.Insert(0, "Select ");
                            }
                        }

                        ddltype.SelectedValue = Convert.ToInt32(dsFab.Tables[0].Rows[0]["OrderType"]).ToString();
                        ddlordno.SelectedValue = Convert.ToInt32(dsFab.Tables[0].Rows[0]["OrderNo"]).ToString();
                        txtinvno.Text = Convert.ToString(dsFab.Tables[0].Rows[0]["FabNo"]).ToString();


                        txtinvdate.Text = Convert.ToDateTime(dsFab.Tables[0].Rows[0]["InvDate"].ToString()).ToString("dd/MM/yyyy");
                        txtinrefno.Text = dsFab.Tables[0].Rows[0]["Refno"].ToString();
                        txtsupplieroderno.Text = dsFab.Tables[0].Rows[0]["SupplierOrderno"].ToString();
                        drpsupplier.SelectedValue = Convert.ToInt32(dsFab.Tables[0].Rows[0]["Supplierid"]).ToString();
                        txtTransport.Text = dsFab.Tables[0].Rows[0]["TransportNo"].ToString();
                        txtbaleQty.Text = dsFab.Tables[0].Rows[0]["BaleQty"].ToString();
                        txtregdate.Text = Convert.ToDateTime(dsFab.Tables[0].Rows[0]["RegDate"].ToString()).ToString("dd/MM/yyyy");
                        txtlrno.Text = dsFab.Tables[0].Rows[0]["LRNO"].ToString();
                        txtDelChalan.Text = dsFab.Tables[0].Rows[0]["Delivery_Challan"].ToString();
                        drpchecked.SelectedValue = Convert.ToInt32(dsFab.Tables[0].Rows[0]["Checkedsign"]).ToString();
                        drpcompany.SelectedValue = Convert.ToInt32(dsFab.Tables[0].Rows[0]["CompanyType"]).ToString();
                        ddlBrand.SelectedValue = Convert.ToInt32(dsFab.Tables[0].Rows[0]["BrandID"]).ToString();
                        lblroll.Text = dsFab.Tables[0].Rows[0]["TotalRoll"].ToString();
                        txtnetingcharge.Text = Convert.ToDouble(dsFab.Tables[0].Rows[0]["netcharge"]).ToString("0.00");

                        drppurchasetype.SelectedValue = dsFab.Tables[0].Rows[0]["PurchaseType"].ToString();
                        chkchargesapply.Checked = Convert.ToBoolean(dsFab.Tables[0].Rows[0]["AllowCharge"]);

                        txtsubtotal.Text = dsFab.Tables[0].Rows[0]["SubTotal"].ToString();

                        txttotmet.Text = dsFab.Tables[0].Rows[0]["TotalMeter"].ToString();
                        txttoal.Text = dsFab.Tables[0].Rows[0]["TotalAmount"].ToString();
                        imgpreview1.Text = dsFab.Tables[0].Rows[0]["ImagePath"].ToString();

                        txtigst.Text = dsFab.Tables[0].Rows[0]["igst"].ToString();
                        txtsgst.Text = dsFab.Tables[0].Rows[0]["sgst"].ToString();
                        txtcgst.Text = dsFab.Tables[0].Rows[0]["cgst"].ToString();

                        txtcom.Text = dsFab.Tables[0].Rows[0]["Commission"].ToString();
                        txtFreight.Text = dsFab.Tables[0].Rows[0]["Freight"].ToString();
                        txtLU.Text = dsFab.Tables[0].Rows[0]["Loading"].ToString();

                        txtNarration.Text = dsFab.Tables[0].Rows[0]["Narration"].ToString();
                        txtbillmet.Text = dsFab.Tables[0].Rows[0]["TotBillmeter"].ToString();

                        txtdisamount.Text = dsFab.Tables[0].Rows[0]["DiscPerKg"].ToString();
                        txttotdisamount.Text = dsFab.Tables[0].Rows[0]["TotalDisc"].ToString();

                        if (txtdisamount.Text == "")
                            txtdisamount.Text = "0";

                        if (txttotdisamount.Text == "")
                            txttotdisamount.Text = "0";



                        ddlProvince.SelectedValue = dsFab.Tables[0].Rows[0]["Province"].ToString();

                        DataSet dsdd = objBs.Get_TransFabric(Convert.ToInt32(iFabID));
                        if (dsdd.Tables[0].Rows.Count > 0)
                        {


                            DataTable dttt;
                            DataRow drNew;
                            DataColumn dct;
                            DataSet dstd = new DataSet();
                            dttt = new DataTable();

                            dct = new DataColumn("OrderNo");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("TransId");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("ChkCutId");
                            dttt.Columns.Add(dct);


                            dct = new DataColumn("Design");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Item");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Color");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Colorid");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Piece");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Width");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Meter");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("billMeter");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("RemMeter");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Rate");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("FileUpload");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Image");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("ImageLabel");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("orderpiece");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Barcode");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Cancel");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("CancelMeter");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Type");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Style");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Mode");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Shrinkage");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Pinning");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("tax");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("taxamount");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("amount");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("issueId");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Narration");
                            dttt.Columns.Add(dct);


                            dstd.Tables.Add(dttt);
                            foreach (DataRow dr in dsdd.Tables[0].Rows)
                            {
                                drNew = dttt.NewRow();
                                drNew["OrderNo"] = dr["Orderno"];
                                drNew["TransId"] = dr["Transid"];
                                drNew["ChkCutId"] = dr["ChkCutId"];

                                drNew["Design"] = dr["DesignNo"];
                                drNew["Item"] = dr["ItemName"];
                                drNew["Color"] = dr["Color"];
                                drNew["Colorid"] = dr["Colorid"];
                                drNew["Piece"] = dr["Piece"];
                                drNew["Width"] = dr["Width"];

                                drNew["Meter"] = dr["AvaliableMeter"];
                                drNew["BillMeter"] = dr["BillMeter"];

                                drNew["RemMeter"] = dr["remainingmeter"];
                                drNew["Rate"] = dr["Rate"];
                                drNew["Cancel"] = dr["Cancel"];
                                drNew["imageLabel"] = dr["imagepath"];
                                drNew["CancelMeter"] = dr["CancelMeter"];
                                drNew["orderpiece"] = dr["orderpiece"];
                                drNew["Barcode"] = dr["CancelMeter"];

                                drNew["Type"] = dr["FabType"];
                                drNew["Style"] = dr["FabStyle"];
                                drNew["Mode"] = dr["FabMode"];
                                drNew["Shrinkage"] = dr["Shrinkage"];
                                drNew["Pinning"] = dr["Pinning"];

                                drNew["tax"] = dr["tax"];
                                drNew["taxamount"] = dr["taxamount"];
                                drNew["amount"] = dr["amount"];

                                drNew["issueid"] = dr["issueid"];
                                drNew["narration"] = dr["narration"];

                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            ViewState["CurrentTable1"] = dttt;

                            gvcustomerorder.DataSource = dstd;
                            gvcustomerorder.DataBind();

                            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                            {

                              

                                TextBox orderno = (TextBox)gvcustomerorder.Rows[vLoop].Cells[0].FindControl("txtno");
                                HiddenField hdtransid = (HiddenField)gvcustomerorder.Rows[vLoop].Cells[0].FindControl("hdtransid");
                                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");


                                TextBox txtitem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                                TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtorderpiece");
                                TextBox txtremmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtremmeter");

                                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");



                                TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");

                                CheckBox cancel = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkid");
                                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                                Label imgpath = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

                                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                                TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");
                                Image imgg = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                                DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");

                                DropDownList ddlfabtype = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabtype");
                                DropDownList ddlfabstyle = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabstyle");
                                DropDownList ddlfabmode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabmode");
                                TextBox txtShrinkage = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtShrinkage");
                                TextBox txtPinning = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtPinning");

                                TextBox txttax = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttax");
                                TextBox txttaxamount = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttaxamount");
                                TextBox txtamount = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtamount");


                                DropDownList drpextralist = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpextralist");
                                TextBox txtnarration = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtnarration");

                                HiddenField hdChkCutId = (HiddenField)gvcustomerorder.Rows[vLoop].Cells[0].FindControl("hdChkCutId");
                                hdChkCutId.Value = dstd.Tables[0].Rows[vLoop]["ChkCutId"].ToString();


                                drpextralist.SelectedValue = dstd.Tables[0].Rows[vLoop]["issueid"].ToString();
                                txtnarration.Text = dstd.Tables[0].Rows[vLoop]["narration"].ToString();

                                ddlfabtype.SelectedValue = dstd.Tables[0].Rows[vLoop]["Type"].ToString();
                                ddlfabstyle.SelectedValue = dstd.Tables[0].Rows[vLoop]["Style"].ToString();
                                ddlfabmode.SelectedValue = dstd.Tables[0].Rows[vLoop]["Mode"].ToString();
                                txtShrinkage.Text = dstd.Tables[0].Rows[vLoop]["Shrinkage"].ToString();
                                txtPinning.Text = dstd.Tables[0].Rows[vLoop]["Pinning"].ToString();

                                orderno.Text = dstd.Tables[0].Rows[vLoop]["orderno"].ToString();
                                hdtransid.Value = dstd.Tables[0].Rows[vLoop]["TransId"].ToString();
                                txtdesign.Text = dstd.Tables[0].Rows[vLoop]["design"].ToString();
                                txtitem.Text = dstd.Tables[0].Rows[vLoop]["Item"].ToString();
                                txtpiece.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["piece"]).ToString();
                                txtorderpiece.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["orderpiece"]).ToString();
                                txtremmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["RemMeter"]).ToString();
                                txtmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["meter"]).ToString("N2");

                                txtbillmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["BillMeter"]).ToString("N2");

                                txtrate.Text = dstd.Tables[0].Rows[vLoop]["rate"].ToString();
                                imgg.ImageUrl = dstd.Tables[0].Rows[vLoop]["imageLabel"].ToString();

                                txttax.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["tax"]).ToString("N2");
                                txttaxamount.Text = dstd.Tables[0].Rows[vLoop]["taxamount"].ToString();
                                txtamount.Text = dstd.Tables[0].Rows[vLoop]["amount"].ToString();
                                //if (dstd.Tables[0].Rows[vLoop]["cancel"].ToString() == "1")
                                //{
                                //    cancel.Checked = true;
                                //}
                                //else
                                //{
                                //    cancel.Checked = false;
                                //}
                                if (dstd.Tables[0].Rows[vLoop]["cancel"].ToString() == "True")
                                {
                                    cancel.Checked = true;
                                }
                                else
                                {
                                    cancel.Checked = false;
                                }
                                txtcancelmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["CancelMeter"]).ToString("N2");
                                //////txtcancelmeter.Text = Convert.ToDouble(0).ToString("N2");
                                drpwid.SelectedValue = dstd.Tables[0].Rows[vLoop]["width"].ToString();
                                txtcolor.Text = dstd.Tables[0].Rows[vLoop]["color"].ToString();
                                drpcolor.SelectedValue = dstd.Tables[0].Rows[vLoop]["Colorid"].ToString();
                                imgpath.Text = dstd.Tables[0].Rows[vLoop]["imageLabel"].ToString();
                            }
                        }
                    }
                }

                #endregion

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void chargesapply_checkedchnage(object sender, EventArgs e)
        {
            if (chkchargesapply.Checked == true)
            {
                double gndmeter = 0;
                double tax = 0;
                double subtotal = 0;
                double distotal = 0;
                double roll = 0;
                //double r = 0;

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {

                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");

                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txttax = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttax");
                    TextBox txtamount = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtamount");

                    if (txtbillmeter.Text == "")
                    {
                        txtbillmeter.Text = "0";
                    }

                    if (txtmeter.Text == "")
                    {
                        txtmeter.Text = "0";
                    }
                    if (txttax.Text == "")
                    {
                        txttax.Text = "0";
                    }
                    if (txtpiece.Text == "")
                    {
                        txtpiece.Text = "0";
                    }
                    //gndmeter = gndmeter + (Convert.ToDouble(txtmeter.Text) * Convert.ToDouble(txtrate.Text));

                    txtrate.Focus();

                    double rate = Convert.ToDouble(txtrate.Text) - Convert.ToDouble(txtdisamount.Text);



                    double iNetAmount = ((Convert.ToDouble(txtbillmeter.Text)) * (Convert.ToDouble(rate)));
                    double Idisc = ((Convert.ToDouble(txtbillmeter.Text)) * (Convert.ToDouble(txtdisamount.Text)));
                    double tx = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txttax.Text) / 100;
                    double total = tx + iNetAmount;

                    txtamount.Text = string.Format("{0:N2}", total);

                    gndmeter = gndmeter + total;
                    tax = tax + tx;
                    distotal = distotal + Idisc;
                    subtotal = subtotal + iNetAmount;
                    roll = roll + Convert.ToDouble(txtpiece.Text);


                }
                double r = 0;
                double roundoff = Convert.ToDouble(gndmeter) - Math.Floor(Convert.ToDouble(gndmeter));
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(gndmeter), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(gndmeter));
                }
                if (txtnetingcharge.Text == "")
                    txtnetingcharge.Text = "0";

                txtsubtotal.Text = (subtotal + Convert.ToDouble(txtnetingcharge.Text)).ToString("f2");
                lblroll.Text = roll.ToString("f2");
                txttoal.Text = r.ToString();

                double netcharge = (Convert.ToDouble(txtnetingcharge.Text) * Convert.ToDouble(lblnetingcharge.Text)) / 100;
                tax += netcharge;

                if (ddlProvince.SelectedValue == "1")
                {
                    txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(tax) / 2);
                    txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(tax) / 2);
                    txtigst.Text = "0.00";
                }
                else
                {
                    txtcgst.Text = "0.00";
                    txtsgst.Text = "0.00";
                    txtigst.Text = tax.ToString();

                }


                double dFreight = 0;
                double dLU = 0;
                double sumLUFreight = 0;
                double dcom = 0;
                if (txtcom.Text.Trim() != "")
                {
                    dcom = Convert.ToDouble(txtcom.Text.Trim());
                }
                if (txtFreight.Text.Trim() != "")
                {
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
                }
                if (txtLU.Text.Trim() != "")
                {
                    dLU = Convert.ToDouble(txtLU.Text.Trim());
                }
                if (txtnetingcharge.Text == "")
                    txtnetingcharge.Text = "0";
                // double netcharge = (Convert.ToDouble(txtnetingcharge.Text) * Convert.ToDouble(lblnetingcharge.Text)) / 100;
                double totnetcharge = Convert.ToDouble(txtnetingcharge.Text) + netcharge;
                sumLUFreight = dFreight + dLU + dcom + totnetcharge;

                txttoal.Text = string.Format("{0:N2}", (gndmeter + sumLUFreight));
                double roundoff1 = Convert.ToDouble(txttoal.Text) - Math.Floor(Convert.ToDouble(txttoal.Text));
                if (roundoff1 > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(txttoal.Text), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(txttoal.Text));
                }
                txttoal.Text = Convert.ToString(r);
            }
            else
            {
                double gndmeter = 0;
                double tax = 0;
                double subtotal = 0;
                double distotal = 0;
                double roll = 0;
                //double r = 0;

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {

                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");

                    TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txttax = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttax");
                    TextBox txtamount = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtamount");

                    if (txtbillmeter.Text == "")
                    {
                        txtbillmeter.Text = "0";
                    }

                    if (txtmeter.Text == "")
                    {
                        txtmeter.Text = "0";
                    }
                    if (txttax.Text == "")
                    {
                        txttax.Text = "0";
                    }
                    //gndmeter = gndmeter + (Convert.ToDouble(txtmeter.Text) * Convert.ToDouble(txtrate.Text));

                    txtrate.Focus();

                    double rate = Convert.ToDouble(txtrate.Text) - Convert.ToDouble(txtdisamount.Text);



                    double iNetAmount = ((Convert.ToDouble(txtmeter.Text)) * (Convert.ToDouble(rate)));
                    double Idisc = ((Convert.ToDouble(txtbillmeter.Text)) * (Convert.ToDouble(txtdisamount.Text)));
                    double tx = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txttax.Text) / 100;
                    double total = tx + iNetAmount;

                    txtamount.Text = string.Format("{0:N2}", total);

                    gndmeter = gndmeter + total;
                    subtotal = subtotal + iNetAmount;
                    distotal = distotal + Idisc;
                    tax = tax + tx;
                    roll = roll + Convert.ToDouble(txtpiece.Text);


                }
                double r = 0;
                double roundoff = Convert.ToDouble(gndmeter) - Math.Floor(Convert.ToDouble(gndmeter));
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(gndmeter), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(gndmeter));
                }
                if (txtnetingcharge.Text == "")
                    txtnetingcharge.Text = "0";

                txtsubtotal.Text = (subtotal + Convert.ToDouble(txtnetingcharge.Text)).ToString("f2");
                txttoal.Text = r.ToString();
                lblroll.Text = roll.ToString("f2");

                double netcharge = (Convert.ToDouble(txtnetingcharge.Text) * Convert.ToDouble(lblnetingcharge.Text)) / 100;
                tax += netcharge;

                if (ddlProvince.SelectedValue == "1")
                {
                    txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(tax) / 2);
                    txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(tax) / 2);
                    txtigst.Text = "0.00";
                }
                else
                {
                    txtcgst.Text = "0.00";
                    txtsgst.Text = "0.00";
                    txtigst.Text = tax.ToString();

                }


                double dFreight = 0;
                double dLU = 0;
                double sumLUFreight = 0;
                double dcom = 0;


                if (txtnetingcharge.Text == "")
                    txtnetingcharge.Text = "0";
                // double netcharge = (Convert.ToDouble(txtnetingcharge.Text) * Convert.ToDouble(lblnetingcharge.Text)) / 100;
                double totnetcharge = Convert.ToDouble(txtnetingcharge.Text) + netcharge;
                sumLUFreight = dFreight + dLU + dcom + totnetcharge;

                txttoal.Text = string.Format("{0:N2}", (gndmeter + sumLUFreight));
                double roundoff1 = Convert.ToDouble(txttoal.Text) - Math.Floor(Convert.ToDouble(txttoal.Text));
                if (roundoff1 > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(txttoal.Text), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(txttoal.Text));
                }
                txttoal.Text = Convert.ToString(r);
            }

        }

        protected void Net_charge(object sender, EventArgs e)
        {
            txtrrattee_textchanged(sender, e);
        }

        protected void select(object sender, EventArgs e)
        {
            txtrrattee_textchanged(sender, e);
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
                //First find the control in template column and then get the value
                //Change the cell index(1) of column as per your design
                // Label2.Text = (row.FindControl("lblLocalTime") as Label).Text;
                //  DropDownList drop = (row.FindControl("lblLocalTime") as DropDownList).Text;
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
            if (chknewcust.Checked == true)
            {

            }
            else
            {

            }
        }

        protected void chknew_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // DataSet dscheckfab = new DataSet();

            DataSet dswidth = objBs.GetWidth();
            DataSet dsfabrictype = objBs.Getfabrictype();
            DataSet dsGetStyle = objBs.GetStyle();


            DataSet dscolor = objBs.gridColor();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hdChkCutId = (HiddenField)e.Row.FindControl("hdChkCutId");

                TextBox txtno = (TextBox)e.Row.FindControl("txtno");
                HiddenField hdtransid = (HiddenField)e.Row.FindControl("hdtransid");
                TextBox txtdesign = (TextBox)e.Row.FindControl("txtdesno");
                TextBox txtcolor = (TextBox)e.Row.FindControl("txtcolor");
                DropDownList drpcolor = (DropDownList)e.Row.FindControl("drpcolor");
                TextBox txtpiece = (TextBox)e.Row.FindControl("txtpiece");
                TextBox txtmeter = (TextBox)e.Row.FindControl("txtmeter");
                TextBox txtbillmeter = (TextBox)e.Row.FindControl("txtbillmeter");
                TextBox txtrate = (TextBox)e.Row.FindControl("txtRate");
                DropDownList drpwid = (DropDownList)e.Row.FindControl("drpwid");

                DropDownList ddlfabtypef = (DropDownList)e.Row.FindControl("ddlfabtype");
                DropDownList ddlfabmode = (DropDownList)e.Row.FindControl("ddlfabmode");
                TextBox txttax = (TextBox)e.Row.FindControl("txttax");
                TextBox txttaxamount = (TextBox)e.Row.FindControl("txttaxamount");
                TextBox txtamount = (TextBox)e.Row.FindControl("txtamount");
                DropDownList drpextralist = (DropDownList)e.Row.FindControl("drpextralist");
                Button btnadd = (Button)e.Row.FindControl("btnadd");
                Button btnaddColor = (Button)e.Row.FindControl("btnaddColor");

              

                txtno.Text = "1";
                txtdesign.Text = "";
                txtpiece.Text = "";
                txtmeter.Text = "0";
                txtbillmeter.Text = "0";
                txtcolor.Text = "";
                txtrate.Text = "0";

                #region
                var ddl = (DropDownList)e.Row.FindControl("drpwid");
                ddl.DataSource = dswidth;
                ddl.DataTextField = "Width";
                ddl.DataValueField = "Widthid";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Width");

                var ddlfabtype = (DropDownList)e.Row.FindControl("ddlfabtype");
                ddlfabtype.DataSource = dsfabrictype;
                ddlfabtype.DataTextField = "FabricType";
                ddlfabtype.DataValueField = "FabricTypeID";
                ddlfabtype.DataBind();
                ddlfabtype.Items.Insert(0, "Type");



                var ddlfabstyle = (DropDownList)e.Row.FindControl("ddlfabstyle");
                ddlfabstyle.DataSource = dsGetStyle;
                ddlfabstyle.DataTextField = "Style";
                ddlfabstyle.DataValueField = "StyleID";
                ddlfabstyle.DataBind();


                var ddl1 = (DropDownList)e.Row.FindControl("drpcolor");
                ddl1.DataSource = dscolor;
                ddl1.DataTextField = "Color";
                ddl1.DataValueField = "Colorid";
                ddl1.DataBind();
                ddl1.Items.Insert(0, "Select Color");
                #endregion

                if (hdChkCutId.Value != "" && hdChkCutId.Value != "0")
                {
                    ddlfabtypef.Enabled = false;
                    ddlfabmode.Enabled = false;
                    txtcolor.Enabled = false;
                    txtpiece.Enabled = false;
                    drpwid.Enabled = false;
                    txtbillmeter.Enabled = false;
                    txtmeter.Enabled = false;
                    txtrate.Enabled = false;
                    txttax.Enabled = false;
                    txttaxamount.Enabled = false;
                    txtamount.Enabled = false;
                    drpextralist.Enabled = false;
                    btnadd.Enabled = false;
                    btnaddColor.Enabled = false;
                    drpcolor.Enabled = false;

                    e.Row.Cells[29].Enabled = false;
                }

            }

        }

        protected void ddlfabtype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            DropDownList ddlfabtype = (DropDownList)row.FindControl("ddlfabtype");
            DropDownList ddlfabstyle = (DropDownList)row.FindControl("ddlfabstyle");

            TextBox txtitem = (TextBox)row.FindControl("txtitem");

            DataSet Stylesel = objBs.GetStyleselected(Convert.ToInt32(ddlfabtype.SelectedValue));
            if (Stylesel.Tables[0].Rows.Count > 0)
            {
                ddlfabstyle.Items.Clear();
                ddlfabstyle.DataSource = Stylesel.Tables[0];
                ddlfabstyle.DataTextField = "Style";
                ddlfabstyle.DataValueField = "StyleID";
                ddlfabstyle.DataBind();
                //  ddlfabstyle.Items.Insert(0, "Style");

                txtitem.Text = ddlfabtype.SelectedItem.Text;
            }
            else
            {
                ddlfabstyle.Items.Clear();
                //  ddlfabstyle.Items.Insert(0, "Style");
            }

        }

        protected void ddlcolortype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            DropDownList drpcolor = (DropDownList)row.FindControl("drpcolor");
            TextBox txtcolor = (TextBox)row.FindControl("txtcolor");

            if (drpcolor.SelectedValue == "Select Color")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select ')", true);
                return;
            }
            else
            {
                txtcolor.Text = drpcolor.SelectedItem.Text;
            }
        }

        protected void ddlDef_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ButtonAdd2_Click1(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtitem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");
                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                DropDownList ddlfabtype1 = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabtype");


                //TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                //TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                if (ddlfabtype1.SelectedValue == "Type" || ddlfabtype1.SelectedValue == "")
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
                TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                //DropDownList drpcol = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");
                if (vLoop == gvcustomerorder.Rows.Count - 1)
                {
                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtdesno");
                    TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtItem");
                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtcolor");
                    //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop - 1].FindControl("drpcolor");
                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtpiece");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtmeter");
                    TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtbillmeter");
                    TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtorderpiece");
                    DropDownList drpwidth = (DropDownList)gvcustomerorder.Rows[vLoop - 1].FindControl("drpwid");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");

                    DropDownList ddlfabtype = (DropDownList)gvcustomerorder.Rows[vLoop - 1].FindControl("ddlfabtype");
                    DropDownList ddlfabstyle = (DropDownList)gvcustomerorder.Rows[vLoop - 1].FindControl("ddlfabstyle");
                    TextBox txtPinning = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtPinning");

                    TextBox txtdes = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                    TextBox txtIte = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                    TextBox txtpie = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                    TextBox txtme = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtbillme = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");
                    TextBox txtordpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtorderpiece");
                    TextBox txtra = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txtcol = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                    DropDownList ddlfabtypenew = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabtype");
                    DropDownList ddlfabstylenew = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabstyle");
                    TextBox txtPinningnew = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtPinning");

                    txtdes.Text = txtdesign.Text;
                    txtIte.Text = txtItem.Text;
                    txtpie.Text = txtpiece.Text;
                    txtordpiece.Text = txtorderpiece.Text;

                    ddlfabtypenew.SelectedValue = ddlfabtype.SelectedValue;
                    ddlfabstylenew.SelectedValue = ddlfabstyle.SelectedValue;
                    txtPinningnew.Text = txtPinning.Text;



                    //txtdes.Text = txtdesign.Text;
                    //txtdes.Enabled = false;
                    //Regex regex = new Regex(@"^[0-9]+$");
                    //if (!regex.IsMatch(txtcolor.Text))
                    //{
                    //    txtcol.Text = "";
                    //}
                    //else
                    {
                        string iCol = txtcolor.Text;
                        txtcol.Text = iCol;
                    }

                    txtme.Text = txtmeter.Text;
                    txtra.Text = txtrate.Text;

                    if (txtbillme.Text == "0" || txtbillme.Text == "")
                    {
                        txtbillme.Text = txtbillmeter.Text;
                    }
                    // txtra.Enabled = false;
                    drpwid.SelectedValue = drpwidth.SelectedValue;
                    //drpcol.SelectedValue = drpcolor.SelectedValue;
                    drpwid.Enabled = true;


                }

                txtcolor1.Focus();




            }
            txtmeter_textchanged(sender, e);
            txtrrattee_textchanged(sender, e);

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                //DropDownList drpcol = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");

                txtcolor.Focus();
            }



        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            txtmeter_textchanged(sender, e);
            txtrrattee_textchanged(sender, e);
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");
                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");
                //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                DropDownList ddlfabtype1 = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabtype");


                //TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                //TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                if (ddlfabtype1.SelectedValue == "Type" || ddlfabtype1.SelectedValue == "")
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
                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");

                txtdesign.Focus();
            }

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtorderpiece");
                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtitem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");

                if (txtdesign.Text != "" || txtcolor.Text != "")
                {
                }
                else
                {
                    txtmeter.Text = "0";
                    txtrate.Text = "0";
                    if (ddltype.SelectedValue == "0")
                    {
                        txtorderpiece.Text = "0";
                    }
                }
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

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                FileUpload FileUpload2 = (FileUpload)gvcustomerorder.Rows[vLoop].FindControl("idupload");
                Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");
                if (FileUpload2.HasFile)
                {
                    FileUpload2.SaveAs(MapPath("~/Files/" + FileUpload2.FileName));
                    imgurl.ImageUrl = imgurl.ImageUrl = "~/Files/" + FileUpload2.FileName;
                    //lbl1.Text = FileUpload2.FileName;
                    txtkttt.Text = imgurl.ImageUrl;
                }
            }
        }

        private void AddNewRow()
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemname = string.Empty;
            string itemcd = string.Empty;
            string itemwidth = string.Empty;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtitem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtitem");
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");

                TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");

                TextBox txtremmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtremmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

                TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtorderpiece");
                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                DropDownList ddlfabtype = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabtype");
                DropDownList ddlfabstyle = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabstyle");
                DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");
                TextBox txtShrinkage = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtShrinkage");
                TextBox txtPinning = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtPinning");

                int col = vLoop + 1;


                txtno.Focus();


                itemc = ddlfabtype.SelectedValue;
                itemname = ddlfabtype.SelectedItem.Text;
                itemcd = txtcolor.Text;
                itemwidth = drpwid.SelectedValue;


                if ((itemc == null) || (itemc == "" || itemc == "Type"))
                {
                }
                else
                {
                    for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                    {
                        //  DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ProductCode");
                        DropDownList ddlfabtype1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ddlfabtype");
                        TextBox txtdesign1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtdesno");
                        TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtcolor");
                        DropDownList drpwid1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpwid");
                        if (ddlfabtype1.SelectedValue == "Type" || ddlfabtype1.SelectedValue == "")
                        {
                        }
                        else
                        {

                            if (ii == iq)
                            {
                            }
                            else
                            {
                                if (itemc == ddlfabtype1.SelectedValue && itemcd == txtcolor1.Text && itemwidth == drpwid1.SelectedValue)
                                {
                                    itemname = ddlfabtype1.SelectedItem.Text;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemname + '-' + txtcolor1.Text + '-' + drpwid1.SelectedItem.Text + "  already exists in the Grid.');", true);
                                    //  txt1.Focus();
                                    return;

                                }
                            }
                            ii = ii + 1;
                        }
                    }
                }
                iq = iq + 1;
                ii = 1;





                if (drpwid.SelectedValue == "Select Width")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Width in row " + col + " ')", true);
                    //  txt.Focus();
                    return;
                }
                if (drpcolor.SelectedValue == "Select Color")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Color in row " + col + " ')", true);
                    //  txt.Focus();
                    drpcolor.Focus();
                    return;
                }
                if (ddlfabtype.SelectedValue == "Type")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Type in row " + col + " ')", true);
                    //  txt.Focus();
                    return;
                }
                if (ddlfabstyle.SelectedValue == "Style")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Style in row " + col + " ')", true);
                    //  txt.Focus();
                    return;
                }
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
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");

                        HiddenField hdtransid =
                    (HiddenField)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("hdtransid");


                        HiddenField hdChkCutId =
                  (HiddenField)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("hdChkCutId");

                        TextBox txtorderpiece =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtorderpiece");
                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtitem =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtitem");

                        TextBox txtcolor =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcolor");

                        TextBox txtpiece =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtpiece");

                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        TextBox txtbillmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtbillmeter");

                        TextBox txtremmeter =
                     (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtremmeter");

                        TextBox txtrate =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        TextBox txtbarcode =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtbarcode");

                        DropDownList drpwid =
                         (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpwid");

                        FileUpload fileupload =
                         (FileUpload)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("idupload");

                        Button imgbtn = (Button)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("btnUpload");

                        Image imgpre = (Image)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgurl");


                        Label lblimg =
                         (Label)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgpreview");



                        DropDownList ddlfabtype =
                         (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabtype");
                        DropDownList ddlfabstyle =
                         (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabstyle");

                        DropDownList ddlfabmode =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabmode");


                        DropDownList drpcolor =
                    (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpcolor");

                        TextBox txtShrinkage =
                    (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtShrinkage");
                        TextBox txtPinning =
                    (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtPinning");

                        TextBox txttax =
                  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txttax");

                        TextBox txttaxamount =
                  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txttaxamount");

                        TextBox txtamount =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtamount");

                        DropDownList drpextralist =
                (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpextralist");

                        TextBox txtnarration =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtnarration");



                        
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["ChkCutId"] = hdChkCutId.Value;

                        dtCurrentTable.Rows[i - 1]["TransId"] = hdtransid.Value;
                        dtCurrentTable.Rows[i - 1]["Design"] = txtdesign.Text;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["Colorid"] = drpcolor.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Item"] = txtitem.Text;
                        dtCurrentTable.Rows[i - 1]["Piece"] = txtpiece.Text;
                        dtCurrentTable.Rows[i - 1]["Meter"] = txtmeter.Text;

                        dtCurrentTable.Rows[i - 1]["billMeter"] = txtbillmeter.Text;

                        dtCurrentTable.Rows[i - 1]["RemMeter"] = txtremmeter.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtrate.Text;
                        dtCurrentTable.Rows[i - 1]["ImageLabel"] = imgpre.ImageUrl;
                        dtCurrentTable.Rows[i - 1]["ImageLabel"] = lblimg.Text;
                        dtCurrentTable.Rows[i - 1]["Orderno"] = txttno.Text;
                        dtCurrentTable.Rows[i - 1]["Barcode"] = txtbarcode.Text;
                        dtCurrentTable.Rows[i - 1]["Width"] = drpwid.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["orderpiece"] = txtorderpiece.Text;

                        dtCurrentTable.Rows[i - 1]["Type"] = ddlfabtype.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Style"] = ddlfabstyle.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Mode"] = ddlfabmode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Shrinkage"] = txtShrinkage.Text;
                        dtCurrentTable.Rows[i - 1]["Pinning"] = txtPinning.Text;

                        dtCurrentTable.Rows[i - 1]["tax"] = txttax.Text;
                        dtCurrentTable.Rows[i - 1]["taxamount"] = txttaxamount.Text;

                        dtCurrentTable.Rows[i - 1]["amount"] = txtamount.Text;

                        dtCurrentTable.Rows[i - 1]["Issueid"] = drpextralist.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Narration"] = txtnarration.Text;

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
                        TextBox txttno =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");


                        HiddenField hdtransid =
                     (HiddenField)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("hdtransid");

                        HiddenField hdChkCutId =
                     (HiddenField)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("hdChkCutId");

                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtcolor =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcolor");

                        DropDownList drpcolor =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("drpcolor");

                        TextBox txtitem =
                     (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtItem");

                        TextBox txtpiece =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtpiece");
                        TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtorderpiece");
                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        TextBox txtbillmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtbillmeter");

                        TextBox txtremmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtRemmeter");

                        TextBox txtrate =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        DropDownList drpwid =
                        (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpwid");



                        FileUpload fileupload =
                         (FileUpload)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("idupload");

                        Button imgbtn = (Button)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("btnUpload");

                        Image imgpre = (Image)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgurl");


                        Label lblimg =
                         (Label)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgpreview");

                        TextBox txtbarcode =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtbarcode");


                        DropDownList ddlfabtype =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabtype");
                        DropDownList ddlfabstyle =
                        (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabstyle");

                        DropDownList ddlfabmode =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabmode");

                        TextBox txtShrinkage =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtShrinkage");
                        TextBox txtPinning =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtPinning");

                        TextBox txttax =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txttax");

                        TextBox txttaxAmount =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txttaxAmount");

                        TextBox txtamount =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtamount");


                        DropDownList drpextralist =
                (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpextralist");

                        TextBox txtnarration =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtnarration");





                        hdChkCutId.Value = dt.Rows[i]["ChkCutId"].ToString();
                        ddlfabtype.SelectedValue = dt.Rows[i]["Type"].ToString();
                        ddlfabstyle.SelectedValue = dt.Rows[i]["Style"].ToString();
                        ddlfabmode.SelectedValue = dt.Rows[i]["Mode"].ToString();
                        txtShrinkage.Text = dt.Rows[i]["Shrinkage"].ToString();
                        txtPinning.Text = dt.Rows[i]["Pinning"].ToString();


                        txttno.Text = dt.Rows[i]["OrderNo"].ToString();
                        hdtransid.Value = dt.Rows[i]["TransId"].ToString();
                        txtdesign.Text = dt.Rows[i]["design"].ToString();
                        txtitem.Text = dt.Rows[i]["Item"].ToString();
                        txtcolor.Text = dt.Rows[i]["Color"].ToString();
                        drpcolor.SelectedValue = dt.Rows[i]["Colorid"].ToString();
                        txtpiece.Text = dt.Rows[i]["Piece"].ToString();
                        txtorderpiece.Text = dt.Rows[i]["orderpiece"].ToString();
                        txtmeter.Text = dt.Rows[i]["meter"].ToString();
                        txtbillmeter.Text = dt.Rows[i]["billmeter"].ToString();
                        txtremmeter.Text = dt.Rows[i]["Remmeter"].ToString();
                        txtrate.Text = dt.Rows[i]["Rate"].ToString();
                        txtbarcode.Text = dt.Rows[i]["Barcode"].ToString();
                        imgpre.ImageUrl = dt.Rows[i]["ImageLabel"].ToString();
                        lblimg.Text = dt.Rows[i]["ImageLabel"].ToString();
                        drpwid.SelectedValue = dt.Rows[i]["Width"].ToString();

                        txttax.Text = dt.Rows[i]["tax"].ToString();
                        txttaxAmount.Text = dt.Rows[i]["taxAmount"].ToString();

                        txtamount.Text = dt.Rows[i]["amount"].ToString();

                        drpextralist.SelectedValue = dt.Rows[i]["Issueid"].ToString();
                        txtnarration.Text = dt.Rows[i]["Narration"].ToString();


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
            dtt.Columns.Add(new DataColumn("TransId", typeof(string)));
            dtt.Columns.Add(new DataColumn("ChkCutId", typeof(string)));

            dtt.Columns.Add(new DataColumn("Design", typeof(string)));
            dtt.Columns.Add(new DataColumn("Item", typeof(string)));
            dtt.Columns.Add(new DataColumn("Color", typeof(string)));
            dtt.Columns.Add(new DataColumn("Colorid", typeof(string)));
            dtt.Columns.Add(new DataColumn("Piece", typeof(string)));
            dtt.Columns.Add(new DataColumn("Width", typeof(string)));
            dtt.Columns.Add(new DataColumn("Meter", typeof(string)));
            dtt.Columns.Add(new DataColumn("BillMeter", typeof(string)));
            dtt.Columns.Add(new DataColumn("RemMeter", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("Fileupload", typeof(string)));
            dtt.Columns.Add(new DataColumn("Image", typeof(string)));
            dtt.Columns.Add(new DataColumn("ImageLabel", typeof(string)));
            dtt.Columns.Add(new DataColumn("orderpiece", typeof(string)));
            dtt.Columns.Add(new DataColumn("Barcode", typeof(string)));

            dtt.Columns.Add(new DataColumn("Type", typeof(string)));
            dtt.Columns.Add(new DataColumn("Style", typeof(string)));
            dtt.Columns.Add(new DataColumn("Mode", typeof(string)));
            dtt.Columns.Add(new DataColumn("Shrinkage", typeof(string)));
            dtt.Columns.Add(new DataColumn("Pinning", typeof(string)));

            //dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Discount", typeof(string)));
            dtt.Columns.Add(new DataColumn("Tax", typeof(string)));
            dtt.Columns.Add(new DataColumn("TaxAmount", typeof(string)));
            dtt.Columns.Add(new DataColumn("Amount", typeof(string)));

            dtt.Columns.Add(new DataColumn("issueid", typeof(string)));
            dtt.Columns.Add(new DataColumn("narration", typeof(string)));

            dr = dtt.NewRow();
            dr["Type"] = string.Empty;
            dr["Style"] = string.Empty;
            dr["Mode"] = string.Empty;
            dr["Shrinkage"] = string.Empty;
            dr["Pinning"] = string.Empty;

            dr["OrderNo"] = string.Empty;
            dr["TransId"] = string.Empty;
            dr["ChkCutId"] = string.Empty;
            

            dr["Design"] = string.Empty;
            dr["Item"] = string.Empty;
            dr["Color"] = string.Empty;
            dr["Colorid"] = string.Empty;
            dr["Piece"] = string.Empty;
            dr["Meter"] = string.Empty;
            dr["BillMeter"] = string.Empty;
            dr["RemMeter"] = string.Empty;
            dr["Width"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["Fileupload"] = string.Empty;
            dr["Image"] = string.Empty;
            dr["ImageLabel"] = string.Empty;
            dr["orderpiece"] = string.Empty;
            dr["Barcode"] = string.Empty;
            //dr["Rate"] = string.Empty;
            //dr["Discount"] = string.Empty;
            dr["Tax"] = string.Empty;
            dr["TaxAmount"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["issueid"] = string.Empty;
            dr["Narration"] = string.Empty;
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

            dct = new DataColumn("TransId");
            dttt.Columns.Add(dct);

            dct = new DataColumn("ChkCutId");
            dttt.Columns.Add(dct); 
            

            dct = new DataColumn("Design");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Item");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Colorid");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Piece");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Meter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("BillMeter");
            dttt.Columns.Add(dct);


            dct = new DataColumn("RemMeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Width");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("FileUpload");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Image");
            dttt.Columns.Add(dct);

            dct = new DataColumn("ImageLabel");
            dttt.Columns.Add(dct);
            dct = new DataColumn("orderpiece");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Type");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Style");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Mode");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Shrinkage");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Pinning");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Barcode");
            dttt.Columns.Add(dct);
            //dct = new DataColumn("Rate");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Discount");
            //dttt.Columns.Add(dct);

            dct = new DataColumn("Tax");
            dttt.Columns.Add(dct);

            dct = new DataColumn("TaxAmount");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Amount");
            dttt.Columns.Add(dct);

            dct = new DataColumn("IssueId");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Narration");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["Type"] = 1;
            drNew["Style"] = "";
            drNew["Mode"] = "";
            drNew["Shrinkage"] = "";
            drNew["Pinning"] = "";

            drNew["OrderNo"] = 1;
            drNew["TransId"] = "";
            drNew["ChkCutId"] = "0";
            
            drNew["Design"] = "";
            drNew["Item"] = "";
            drNew["Color"] = "";
            drNew["Colorid"] = "";
            drNew["Piece"] = "";
            drNew["Meter"] = 0;
            drNew["BillMeter"] = 0;
            drNew["RemMeter"] = 0;
            drNew["Width"] = "";
            drNew["Rate"] = 0;
            drNew["Fileupload"] = 0;
            drNew["image"] = "";
            drNew["imageLabel"] = "";
            drNew["Barcode"] = "";

            drNew["Tax"] = 0;
            drNew["Taxamount"] = 0;
            drNew["Amount"] = 0;

            drNew["Issueid"] = 0;
            drNew["Narration"] = 0;

            //drNew["ProductCode"] = "";
            //drNew["Product"] = "";
            //drNew["refno"] = 0;
            //drNew["cerno"] = 0;
            //drNew["Discount"] = 0;
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
            //if (txtgrandtotal.Text != "")
            //{
            //    double grandtotal = 0;
            //    double tax = 0;
            //    double distotal = 0;
            //    int tottqty = 0;
            //    double mettt = 0;
            //    double r = 0;
            //    double disc = 0;

            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //        TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //        TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //        TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //        if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
            //        {

            //        }
            //        else
            //        {
            //            double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //            double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

            //            double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //            double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //            double total = tx + DiscountAmount;

            //            //  txtkttt.Text = string.Format("{0:N2}", total);

            //            if (txt.SelectedIndex == -1 || txt.SelectedIndex == 0)
            //            {
            //            }
            //            else
            //            {
            //                //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
            //                //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
            //                //double totm = Convert.ToDouble(txtttk.Text) * mett;
            //                //mettt = mettt + totm;
            //            }

            //            grandtotal = grandtotal + total;
            //            tax = tax + tx;
            //            distotal = distotal + Discount;
            //            tottqty = tottqty + Convert.ToInt32(txtttk.Text);

            //        }
            //    }
            //    double dFreight = 0;
            //    double dLU = 0;
            //    double sumLUFreight = 0;
            //    if (txtFreight.Text.Trim() != "")
            //    {
            //        dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //    }
            //    if (txtLU.Text.Trim() != "")
            //    {
            //        dLU = Convert.ToDouble(txtLU.Text.Trim());
            //    }
            //    sumLUFreight = dFreight + dLU;

            //    txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //    txtdiscountamount.Text = string.Format("{0:N2}", distotal);
            //    double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //    }
            //    txtroundoff.Text = Convert.ToString(r);
            //    //  txtTaxamt.Text = string.Format("{0:N2}", tax);
            //    //  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //    //  totqty.Text = tottqty.ToString();
            //    //  totmeter.Text = string.Format("{0:N2}", mettt);
            //    //   txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //    // txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //    // txtRate_TextChanged(sender, e);
            //    // txtQty_TextChanged(sender, e);

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Item.Grand total is Empty!!!.Thank You');", true);
            //    return;
            //}

        }

        protected void txtLchange(object sender, EventArgs e)
        {
            //if (txtgrandtotal.Text != "")
            //{
            //    double grandtotal = 0;
            //    double tax = 0;
            //    double distotal = 0;
            //    int tottqty = 0;
            //    double mettt = 0;
            //    double r = 0;

            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //        TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //        TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //        TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //        if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
            //        {

            //        }
            //        else
            //        {
            //            double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //            double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

            //            double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //            double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //            double total = tx + DiscountAmount;


            //            //  txtkttt.Text = string.Format("{0:N2}", total);

            //            if (txt.SelectedIndex == -1 || txt.SelectedIndex == 0)
            //            {
            //            }
            //            else
            //            {
            //                //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
            //                //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
            //                //double totm = Convert.ToDouble(txtttk.Text) * mett;
            //                //mettt = mettt + totm;
            //            }

            //            grandtotal = grandtotal + total;
            //            tax = tax + tx;
            //            distotal = distotal + Discount;
            //            tottqty = tottqty + Convert.ToInt32(txtttk.Text);

            //        }
            //    }
            //    double dFreight = 0;
            //    double dLU = 0;
            //    double sumLUFreight = 0;
            //    if (txtFreight.Text.Trim() != "")
            //    {
            //        dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //    }
            //    if (txtLU.Text.Trim() != "")
            //    {
            //        dLU = Convert.ToDouble(txtLU.Text.Trim());
            //    }
            //    sumLUFreight = dFreight + dLU;

            //    txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //    double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //    }
            //    txtroundoff.Text = Convert.ToString(r);
            //    //  txtTaxamt.Text = string.Format("{0:N2}", tax);
            //    //  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //    //  totqty.Text = tottqty.ToString();
            //    //  totmeter.Text = string.Format("{0:N2}", mettt);
            //    //   txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //    // txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //    // txtRate_TextChanged(sender, e);
            //    // txtQty_TextChanged(sender, e);

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Item.Grand total is Empty!!!.Thank You');", true);
            //    return;
            //}

        }

        protected void txttax_textchanged(object sender, EventArgs e)
        {
            //// ButtonAdd1_Click(sender, e);
            //double grandtotal = 0;
            //double tax = 0;
            //double distotal = 0;
            //int tottqty = 0;
            //double mettt = 0;
            //double r = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //    if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
            //    {

            //    }
            //    else
            //    {

            //        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

            //        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //        double total = tx + DiscountAmount;

            //        txtkttt.Text = string.Format("{0:N2}", total);

            //        if (ProductCode.SelectedItem.Text == "Select Product Code")
            //        {

            //        }
            //        else
            //        {
            //            //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
            //            //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
            //            //double totm = Convert.ToDouble(txtttk.Text) * mett;
            //            //mettt = mettt + totm;
            //        }


            //        grandtotal = grandtotal + total;
            //        tax = tax + tx;
            //        distotal = distotal + Discount;
            //        tottqty = tottqty + Convert.ToInt32(txtttk.Text);
            //        txttk.Focus();

            //    }

            //}
            //double dFreight = 0;
            //double dLU = 0;
            //double sumLUFreight = 0;
            //if (txtFreight.Text.Trim() != "")
            //{
            //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //}
            //if (txtLU.Text.Trim() != "")
            //{
            //    dLU = Convert.ToDouble(txtLU.Text.Trim());
            //}
            //sumLUFreight = dFreight + dLU;
            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
            ////  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //totqty.Text = tottqty.ToString();
            //totmeter.Text = string.Format("{0:N2}", mettt);
            //txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //if (roundoff > 0.5)
            //{
            //    r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //}
            //txtroundoff.Text = Convert.ToString(r);




            ////for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            ////{
            ////    int cnt = gvcustomerorder.Rows.Count;
            ////    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            ////    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            ////    if (vLoop >= 1)
            ////    {
            ////        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            ////        //    oldtxttk.Text = ".00";
            ////        oldtxttk.Focus();
            ////    }
            ////    int tot = cnt - vLoop;
            ////    if (tot == 1)
            ////    {
            ////        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            ////        if (oldtxttk.Text == "0.00")
            ////        {
            ////            oldtxttk.Text = ".00";
            ////            oldtxttk.Focus();
            ////        }
            ////        else
            ////        {
            ////            oldtxttk.Focus();
            ////        }
            ////    }
            ////    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            ////}
            ////Lable Sno:
            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
            //    txtno.Text = Convert.ToString(i + 1);
            //    // ProductCode.Focus();
            //}
            //granddiscount(sender, e);

        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            //ButtonAdd1_Click(sender, e);
            //double grandtotal = 0;
            //double tax = 0;
            //double distotal = 0;
            //int tottqty = 0;
            //double mettt = 0;
            //double r = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //    if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
            //    {

            //    }
            //    else
            //    {

            //        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

            //        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //        double total = tx + DiscountAmount;

            //        txtkttt.Text = string.Format("{0:N2}", total);

            //        if (ProductCode.SelectedItem.Text == "Select Product Code")
            //        {

            //        }
            //        else
            //        {
            //            //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
            //            //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
            //            //double totm = Convert.ToDouble(txtttk.Text) * mett;
            //            //mettt = mettt + totm;
            //        }


            //        grandtotal = grandtotal + total;
            //        tax = tax + tx;
            //        distotal = distotal + Discount;
            //        tottqty = tottqty + Convert.ToInt32(txtttk.Text);
            //        txttk.Focus();

            //    }

            //}
            //double dFreight = 0;
            //double dLU = 0;
            //double sumLUFreight = 0;
            //if (txtFreight.Text.Trim() != "")
            //{
            //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //}
            //if (txtLU.Text.Trim() != "")
            //{
            //    dLU = Convert.ToDouble(txtLU.Text.Trim());
            //}
            //sumLUFreight = dFreight + dLU;
            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
            ////  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //totqty.Text = tottqty.ToString();
            //totmeter.Text = string.Format("{0:N2}", mettt);
            //txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //if (roundoff > 0.5)
            //{
            //    r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //}
            //txtroundoff.Text = Convert.ToString(r);




            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    int cnt = gvcustomerorder.Rows.Count;
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    if (vLoop >= 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            //        //    oldtxttk.Text = ".00";
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
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            //}
            ////Lable Sno:
            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    txtno.Text = Convert.ToString(i + 1);
            //}
            //granddiscount(sender, e);

        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            //double grandtotal = 0;
            //double tax = 0;
            //double distotal = 0;
            //double r = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");
            //    if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "")
            //    {

            //    }
            //    else
            //    {
            //        if ((txttk.Text == "") || (Convert.ToString(txttk.Text) == ".00"))
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Rate')", true);
            //            txttk.Focus();
            //            return;
            //        }
            //        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

            //        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //        double total = tx + DiscountAmount;

            //        txtkttt.Text = string.Format("{0:N2}", total);

            //        grandtotal = grandtotal + total;
            //        tax = tax + tx;
            //        distotal = distotal + Discount;
            //    }
            //}
            //double dFreight = 0;
            //double dLU = 0;
            //double sumLUFreight = 0;
            //if (txtFreight.Text.Trim() != "")
            //{
            //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //}
            //if (txtLU.Text.Trim() != "")
            //{
            //    dLU = Convert.ToDouble(txtLU.Text.Trim());
            //}
            //sumLUFreight = dFreight + dLU;
            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
            ////  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //if (roundoff > 0.5)
            //{
            //    r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //}
            //txtroundoff.Text = Convert.ToString(r);

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    // txtno.Focus();
            //}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    txtno.Text = Convert.ToString(i + 1);
            //}

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    txtno.Focus();
            //}
            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}
            //granddiscount(sender, e);
        }

        protected void txtbillmeter_textchanged(object sender, EventArgs e)
        {
            txtmeter_textchanged(sender, e);
        }

        protected void txtmeter_textchanged(object sender, EventArgs e)
        {
            double gndmeter = 0;
            double billmeter = 0;
            double roll = 0;
            //double tax = 0;
            //double distotal = 0;
            //double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");

                TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");
                if (txtbillmeter.Text == "")
                {
                    txtbillmeter.Text = "0";
                }

                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                if (txtmeter.Text == "")
                {
                    txtmeter.Text = "0";
                }
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");


                if (txtpiece.Text == "")
                {
                    txtpiece.Text = "0";
                }


                if (txtmeter.Text == "0" || txtmeter.Text == "")
                {
                    txtmeter.Text = txtbillmeter.Text;
                }

                gndmeter = gndmeter + Convert.ToDouble(txtmeter.Text);
                billmeter = billmeter + Convert.ToDouble(txtbillmeter.Text);
                roll = roll + Convert.ToDouble(txtpiece.Text);
                txtrate.Focus();
            }
            txttotmet.Text = gndmeter.ToString();
            txtbillmet.Text = billmeter.ToString();
            lblroll.Text = roll.ToString("f2");


            //  txtmeter_textchanged(sender, e);
            txtrrattee_textchanged(sender, e);
            if (ddltype.SelectedValue == "1")
            {
                int RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
                for (int vLoop1 = RowIndex; vLoop1 <= RowIndex; vLoop1++)
                {

                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtRate");

                    txtrate.Focus();
                }
            }

        }

        protected void disc_Chnaged(object sender, EventArgs e)
        {
            txtrrattee_textchanged(sender, e);
        }

        protected void txtrrattee_textchanged(object sender, EventArgs e)
        {
            double gndmeter = 0;
            double billmeter = 0;
            double tax = 0;
            double subtotal = 0;
            double distotal = 0;
            double roll = 0;
            //double r = 0;

            if (chkchargesapply.Checked == true)
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {

                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");

                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txttax = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttax");
                    TextBox txttaxAmount = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttaxAmount");
                    TextBox txtamount = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtamount");

                    if (txtmeter.Text == "")
                    {
                        txtmeter.Text = "0";
                    }
                    if (txtbillmeter.Text == "")
                    {
                        txtbillmeter.Text = "0";
                    }

                    if (txttax.Text == "")
                    {
                        txttax.Text = "0";
                    }
                    if (txtpiece.Text == "")
                    {
                        txtpiece.Text = "0";
                    }
                    //gndmeter = gndmeter + (Convert.ToDouble(txtmeter.Text) * Convert.ToDouble(txtrate.Text));

                    txtrate.Focus();

                    double rate = Convert.ToDouble(txtrate.Text) - Convert.ToDouble(txtdisamount.Text);



                    //  double iNetAmount = ((Convert.ToDouble(txtmeter.Text)) * (Convert.ToDouble(txtrate.Text)));
                    double iNetAmount = ((Convert.ToDouble(txtbillmeter.Text)) * (Convert.ToDouble(rate)));
                    double tx = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txttax.Text) / 100;
                    double Idisc = ((Convert.ToDouble(txtbillmeter.Text)) * (Convert.ToDouble(txtdisamount.Text)));
                    double total = tx + iNetAmount;

                    txtamount.Text = string.Format("{0:N2}", total);

                    txttaxAmount.Text = string.Format("{0:N2}", tx);

                    subtotal = subtotal + iNetAmount;
                    gndmeter = gndmeter + total;
                    tax = tax + tx;
                    distotal = distotal + Idisc;
                    roll = roll + Convert.ToDouble(txtpiece.Text);


                }
                double r = 0;
                double roundoff = Convert.ToDouble(gndmeter) - Math.Floor(Convert.ToDouble(gndmeter));
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(gndmeter), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(gndmeter));
                }
                if (txtnetingcharge.Text == "")
                    txtnetingcharge.Text = "0";

                txtsubtotal.Text = (subtotal + Convert.ToDouble(txtnetingcharge.Text)).ToString("f2");

                txttoal.Text = r.ToString();
                txttotdisamount.Text = distotal.ToString("f2");
                double netcharge = (Convert.ToDouble(txtnetingcharge.Text) * Convert.ToDouble(lblnetingcharge.Text)) / 100;
                tax += netcharge;

                if (ddlProvince.SelectedValue == "1")
                {
                    txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(tax) / 2);
                    txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(tax) / 2);
                    txtigst.Text = "0.00";
                }
                else
                {
                    txtcgst.Text = "0.00";
                    txtsgst.Text = "0.00";
                    txtigst.Text = tax.ToString();

                }


                double dFreight = 0;
                double dLU = 0;
                double sumLUFreight = 0;
                double dcom = 0;
                if (txtcom.Text.Trim() != "")
                {
                    dcom = Convert.ToDouble(txtcom.Text.Trim());
                }
                if (txtFreight.Text.Trim() != "")
                {
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
                }
                if (txtLU.Text.Trim() != "")
                {
                    dLU = Convert.ToDouble(txtLU.Text.Trim());
                }
                if (txtnetingcharge.Text == "")
                    txtnetingcharge.Text = "0";

                double totnetcharge = Convert.ToDouble(txtnetingcharge.Text) + netcharge;
                sumLUFreight = dFreight + dLU + dcom + totnetcharge;

                txttoal.Text = string.Format("{0:N2}", (gndmeter + sumLUFreight));
                double roundoff1 = Convert.ToDouble(txttoal.Text) - Math.Floor(Convert.ToDouble(txttoal.Text));
                if (roundoff1 > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(txttoal.Text), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(txttoal.Text));
                }
                // txtsubtotal.Text = subtotal.ToString("f2");
                txttoal.Text = Convert.ToString(r);
                lblroll.Text = roll.ToString("f2");
            }
            else
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbillmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txttax = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttax");
                    TextBox txttaxAmount = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttaxAmount");
                    TextBox txtamount = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtamount");

                    if (txtmeter.Text == "")
                    {
                        txtmeter.Text = "0";
                    }

                    if (txtbillmeter.Text == "")
                    {
                        txtbillmeter.Text = "0";
                    }

                    if (txttax.Text == "")
                    {
                        txttax.Text = "0";
                    }
                    if (txtpiece.Text == "")
                    {
                        txtpiece.Text = "0";
                    }
                    //gndmeter = gndmeter + (Convert.ToDouble(txtmeter.Text) * Convert.ToDouble(txtrate.Text));

                    txtrate.Focus();


                    double rate = Convert.ToDouble(txtrate.Text) - Convert.ToDouble(txtdisamount.Text);


                    double iNetAmount = ((Convert.ToDouble(txtbillmeter.Text)) * (Convert.ToDouble(rate)));


                    double tx = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txttax.Text) / 100;
                    double Idisc = ((Convert.ToDouble(txtbillmeter.Text)) * (Convert.ToDouble(txtdisamount.Text)));
                    double total = tx + iNetAmount;

                    txtamount.Text = string.Format("{0:N2}", total);
                    txttaxAmount.Text = string.Format("{0:N2}", tx);
                    subtotal = subtotal + iNetAmount;
                    gndmeter = gndmeter + total;
                    distotal = distotal + Idisc;
                    tax = tax + tx;
                    roll = roll + Convert.ToDouble(txtpiece.Text);

                }
                double r = 0;
                double roundoff = Convert.ToDouble(gndmeter) - Math.Floor(Convert.ToDouble(gndmeter));

                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(gndmeter), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(gndmeter));
                }


                if (txtnetingcharge.Text == "")
                    txtnetingcharge.Text = "0";

                txtsubtotal.Text = (subtotal + Convert.ToDouble(txtnetingcharge.Text)).ToString("f2");


                // txtsubtotal.Text = subtotal.ToString("f2");
                txttoal.Text = r.ToString();
                txttotdisamount.Text = distotal.ToString("f2");

                double netcharge = (Convert.ToDouble(txtnetingcharge.Text) * Convert.ToDouble(lblnetingcharge.Text)) / 100;
                tax += netcharge;
                if (ddlProvince.SelectedValue == "1")
                {
                    txtcgst.Text = string.Format("{0:N2}", Convert.ToDouble(tax) / 2);
                    txtsgst.Text = string.Format("{0:N2}", Convert.ToDouble(tax) / 2);
                    txtigst.Text = "0.00";
                }
                else
                {
                    txtcgst.Text = "0.00";
                    txtsgst.Text = "0.00";
                    txtigst.Text = tax.ToString();

                }


                double dFreight = 0;
                double dLU = 0;
                double sumLUFreight = 0;
                double dcom = 0;

                if (txtnetingcharge.Text == "")
                    txtnetingcharge.Text = "0";
                // double netcharge = (Convert.ToDouble(txtnetingcharge.Text) * Convert.ToDouble(lblnetingcharge.Text)) / 100;
                double totnetcharge = Convert.ToDouble(txtnetingcharge.Text) + netcharge;
                sumLUFreight = dFreight + dLU + dcom + totnetcharge;

                // sumLUFreight = dFreight + dLU + dcom;

                txttoal.Text = string.Format("{0:N2}", (gndmeter + sumLUFreight));
                double roundoff1 = Convert.ToDouble(txttoal.Text) - Math.Floor(Convert.ToDouble(txttoal.Text));
                if (roundoff1 > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(txttoal.Text), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(txttoal.Text));
                }
                txttoal.Text = Convert.ToString(r);
                lblroll.Text = roll.ToString("f2");
            }

            if (ddltype.SelectedValue == "1")
            {

                int count = gvcustomerorder.Rows.Count;
                if (count == 1)
                {
                }
                else
                {
                    int RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
                    for (int vLoop1 = RowIndex; vLoop1 <= RowIndex + 1; vLoop1++)
                    {
                        if (count == vLoop1)
                        {
                        }
                        else
                        {
                            TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtbillmeter");

                            TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtmeter");
                            TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtRate");
                            if (txtbillmeter.Text == "0.00" || (txtbillmeter.Text == "0"))
                            {
                                txtbillmeter.Text = "";
                                txtbillmeter.Focus();
                            }
                        }
                    }
                }
            }
            //  txtmeter_textchanged(sender, e);
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //double grandtotal = 0;
            //double tax = 0;
            //double distotal = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //    if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
            //    {

            //    }
            //    else
            //    {
            //        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

            //        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //        double total = tx + DiscountAmount;

            //        txtkttt.Text = string.Format("{0:N2}", total);

            //        grandtotal = grandtotal + total;
            //        tax = tax + tx;
            //        distotal = distotal + Discount;
            //    }
            //}
            //double dFreight = 0;
            //double dLU = 0;
            //double sumLUFreight = 0;
            //if (txtFreight.Text.Trim() != "")
            //{
            //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //}
            //if (txtLU.Text.Trim() != "")
            //{
            //    dLU = Convert.ToDouble(txtLU.Text.Trim());
            //}
            //sumLUFreight = dFreight + dLU;

            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
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

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
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
            //DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");


            //DropDownList Defitem = (DropDownList)row.FindControl("drpItem");

            //DropDownList procode = (DropDownList)row.FindControl("ProductCode");

            //if (procode.SelectedItem.Text != "Select Product Code")
            //{

            //    DataSet dsCategory1 = objBs.selectProduct(Convert.ToInt32(Defitem.SelectedValue), sTableName);
            //    if (dsCategory1.Tables[0].Rows.Count > 0)
            //    {
            //        drpCategory.Items.Clear();
            //        drpCategory.DataSource = dsCategory1.Tables[0];
            //        drpCategory.DataTextField = "categoryname";
            //        drpCategory.DataValueField = "categoryid";
            //        drpCategory.DataBind();
            //        //drpCategory.Items.Insert(0, "Select Category");

            //    }
            //    else
            //    {
            //        drpCategory.Items.Clear();
            //        drpCategory.Items.Insert(0, "Select Category");
            //    }
            //}
            //else
            //{
            //}

            //if (procode.SelectedItem.Text != "Select Product")
            //{

            //    DataSet dsCategory1 = objBs.selectProduct(Convert.ToInt32(Defitem.SelectedValue), sTableName);
            //    if (dsCategory1.Tables[0].Rows.Count > 0)
            //    {
            //        procode.SelectedValue = dsCategory1.Tables[0].Rows[0]["CategoryUserID"].ToString();

            //        DataSet dst = new DataSet();
            //        dst = objBs.selectcategoryalldecriptionbranch(sTableName);
            //        procode.Items.Clear();
            //        procode.DataSource = dst.Tables[0];
            //        procode.DataTextField = "Definition";
            //        procode.DataValueField = "categoryuserid";
            //        procode.DataBind();
            //    }
            //    else
            //    {
            //        procode.Items.Clear();
            //        procode.Items.Insert(0, "Select Product Code");
            //    }
            //}
            //else
            //{
            //}

            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
            //TextBox txt = (TextBox)row.FindControl("txtDiscount");
            //TextBox txtTax = (TextBox)row.FindControl("txtTax");
            //DropDownList Def = (DropDownList)row.FindControl("drpItem");
            //DropDownList cate = (DropDownList)row.FindControl("drpCategory");
            //TextBox txtQty = (TextBox)row.FindControl("txtStock");
            //TextBox qty = (TextBox)row.FindControl("txtQty");
            //TextBox refno = (TextBox)row.FindControl("txtrefno");
            //DataSet dsStock = new DataSet();


            //dsStock = objBs.GetStockDetails(Convert.ToInt32(Def.SelectedValue), "tblStock_" + sTableName);

            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    DataSet dsCategory = objBs.GetTax(Convert.ToInt32(Def.SelectedValue), sTableName);

            //    var Itx = Convert.ToDecimal(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
            //    txtTax.Text = Itx.ToString();

            //    decimal ratte = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["UnitPrice"]);
            //    txtRate.Text = Decimal.Round(ratte, 2).ToString("f2");

            //    //Decimal Irate = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString());
            //    //txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");

            //    //if (ddlcustomerID.SelectedValue != "Select Customer")
            //    //{
            //    //    DataSet dsStockd = objBs.getsprice1(ddlcustomerID.SelectedValue, Def.SelectedValue, sTableName);
            //    //    if (dsStockd.Tables[0].Rows.Count > 0)
            //    //    {
            //    //        decimal ratte = Convert.ToDecimal(dsStockd.Tables[0].Rows[0]["Price"]);
            //    //        txtRate.Text = Decimal.Round(ratte, 2).ToString("f2");
            //    //    }
            //    //    else
            //    //    {
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    txtRate.Text = "0.00";
            //    //}

            //    decimal sQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
            //    txtQty.Text = sQty.ToString("f2");
            //    cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

            //    txt.Text = "0";

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


            //    string cust = ddlcustomerID.SelectedValue;
            //    if (cust == "Select Customer")
            //    {
            //    }
            //    else
            //    {
            //        DataSet ds1 = objBs.custhistorypopup(sTableName, value, cust);
            //        if (ds1.Tables[0].Rows.Count > 0)
            //        {
            //            txtcusthis.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
            //        }
            //        else
            //        {
            //            txtcusthis.Text = "";
            //        }
            //    }
            //    refno.Focus();
            //    // txtTamt5.Text = dsCategory.Tables[0].Rows[0]["Meter1"].ToString();
            //}


            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}


            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    txtno.Text = Convert.ToString(i + 1);
            //}
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

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                        txtno.Text = Convert.ToString(i + 1);
                    }

                    txtmeter_textchanged(sender, e);
                    txtrrattee_textchanged(sender, e);
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();

                    txtmeter_textchanged(sender, e);
                    txtrrattee_textchanged(sender, e);
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
                        TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtorderpiece");
                        TextBox txttno =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");

                        HiddenField hdtransid =
                     (HiddenField)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("hdtransid");

                        HiddenField hdChkCutId =
                   (HiddenField)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("hdChkCutId");

                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtitem =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtItem");

                        TextBox txtpiece =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtpiece");

                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        TextBox txtbillmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtbillmeter");

                        TextBox txtremmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtremmeter");

                        DropDownList drpwid =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpwid");

                        DropDownList drpcolor =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpcolor");

                        TextBox txtcolor = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");

                        TextBox txtrate =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        TextBox txtbarcode =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtbarcode");

                        FileUpload fileupload =
                         (FileUpload)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("idupload");

                        Button imgbtn = (Button)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("btnUpload");

                        Image imgpre = (Image)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgurl");


                        Label lblimg =
                         (Label)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgpreview");

                        DropDownList ddlfabtype =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabtype");
                        DropDownList ddlfabstyle =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabstyle");

                        DropDownList ddlfabmode =
                     (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("ddlfabmode");

                        TextBox txtShrinkage = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtShrinkage");
                        TextBox txtPinning = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtPinning");

                        TextBox txttax =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txttax");

                        TextBox txttaxAmount =
                    (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txttaxAmount");

                        TextBox txtamount =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtamount");

                        DropDownList drpextralist =
                    (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpextralist");

                        TextBox txtnarration =
                    (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtnarration");




                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["ChkCutId"] = hdChkCutId.Value;
                        dtCurrentTable.Rows[i - 1]["transid"] = hdtransid.Value;
                        dtCurrentTable.Rows[i - 1]["orderpiece"] = txtorderpiece.Text;
                        dtCurrentTable.Rows[i - 1]["Design"] = txtdesign.Text;
                        dtCurrentTable.Rows[i - 1]["Item"] = txtitem.Text;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["Colorid"] = drpcolor.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Piece"] = txtpiece.Text;
                        dtCurrentTable.Rows[i - 1]["meter"] = txtmeter.Text;
                        dtCurrentTable.Rows[i - 1]["meter"] = txtremmeter.Text;
                        dtCurrentTable.Rows[i - 1]["billmeter"] = txtbillmeter.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtrate.Text;
                        dtCurrentTable.Rows[i - 1]["Barcode"] = txtbarcode.Text;
                        dtCurrentTable.Rows[i - 1]["ImageLabel"] = imgpre.ImageUrl;
                        dtCurrentTable.Rows[i - 1]["ImageLabel"] = lblimg.Text;
                        dtCurrentTable.Rows[i - 1]["Width"] = drpwid.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Type"] = ddlfabtype.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Style"] = ddlfabstyle.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Mode"] = ddlfabmode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Shrinkage"] = txtShrinkage.Text;
                        dtCurrentTable.Rows[i - 1]["Pinning"] = txtPinning.Text;

                        dtCurrentTable.Rows[i - 1]["amount"] = txtamount.Text;

                        dtCurrentTable.Rows[i - 1]["Tax"] = txttax.Text;
                        dtCurrentTable.Rows[i - 1]["TaxAmount"] = txttaxAmount.Text;

                        dtCurrentTable.Rows[i - 1]["Issueid"] = drpextralist.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Narration"] = txtnarration.Text;



                        //dtCurrentTable.Rows[i - 1]["Qty"] = txtQty.Text;
                        //dtCurrentTable.Rows[i - 1]["Orderno"] = txttno.Text;

                        //dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;

                        //dtCurrentTable.Rows[i - 1]["Tax"] = txtTax.Text;

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

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddltype.SelectedValue == "0")
            {
                ddlordno.Enabled = false;
            }
            else
            {
                ddlordno.Enabled = true;
            }
        }

        protected void ddlordno_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet brandName = new DataSet();
            brandName = objBs.getBrandName();

            if (brandName != null)
            {
                if (brandName.Tables[0].Rows.Count > 0)
                {
                    ddlBrand.DataSource = brandName.Tables[0];
                    ddlBrand.DataTextField = "BrandName";
                    ddlBrand.DataValueField = "BrandId";
                    ddlBrand.DataBind();
                    ddlBrand.Items.Insert(0, "Select Brand");
                }
            }
            DataSet dsd = objBs.getorderforid(Convert.ToInt32(ddlordno.SelectedValue));
            if (dsd.Tables[0].Rows.Count > 0)
            {
                drpsupplier.SelectedValue = Convert.ToInt32(dsd.Tables[0].Rows[0]["Supplierid"]).ToString();
                txtsupplieroderno.Text = dsd.Tables[0].Rows[0]["Supplierorderno"].ToString();
                drpcompany.SelectedValue = dsd.Tables[0].Rows[0]["Companyid"].ToString();

                DataSet dfabb = objBs.getfabusingorder(Convert.ToInt32(ddlordno.SelectedValue));
                if (dfabb.Tables[0].Rows.Count > 0)
                {
                    txtinrefno.Focus();
                    txtinrefno.Text = dfabb.Tables[0].Rows[0]["Refno"].ToString();
                    txtTransport.Text = dfabb.Tables[0].Rows[0]["TransportNo"].ToString();
                    txtbaleQty.Text = dfabb.Tables[0].Rows[0]["BaleQty"].ToString();
                    //txtregdate.Text = Convert.ToDateTime(dfabb.Tables[0].Rows[0]["RegDate"]).ToString();
                    txtlrno.Text = dfabb.Tables[0].Rows[0]["LRNO"].ToString();
                    txtDelChalan.Text = dfabb.Tables[0].Rows[0]["Delivery_Challan"].ToString();
                    if (dfabb.Tables[0].Rows[0]["BrandID"].ToString() == "0")
                    {
                        ddlBrand.SelectedValue = "Select Brand";
                    }
                    else
                    {
                        ddlBrand.SelectedValue = dfabb.Tables[0].Rows[0]["BrandID"].ToString();
                    }
                }
                else
                {
                    txtinrefno.Focus();
                    txtinrefno.Text = "";
                    txtTransport.Text = "";
                    txtbaleQty.Text = "";
                    //txtregdate.Text = Convert.ToDateTime(dfabb.Tables[0].Rows[0]["RegDate"]).ToString();
                    txtlrno.Text = "";
                    txtDelChalan.Text = "";
                    //   ddlBrand.SelectedValue = "Select Brand";
                }

                //txtinvno.Text= Convert.ToString(dsd.Tables[0].Rows[0]["orderno"]).ToString();
                //txtinvdate.Text = dsd.Tables[0].Rows[0]["orderdate"].ToString();
                //ddlSupplier.SelectedValue = Convert.ToInt32(dsd.Tables[0].Rows[0]["Supplierid"]).ToString();

                DataSet dsdd = objBs.gettransorderforid(Convert.ToInt32(ddlordno.SelectedValue));
                if (dsdd.Tables[0].Rows.Count > 0)
                {
                    DataTable dttt;
                    DataRow drNew;
                    DataColumn dct;
                    DataSet dstd = new DataSet();
                    dttt = new DataTable();

                    dct = new DataColumn("OrderNo");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Design");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Item");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Color");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("orderPiece");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("Piece");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Meter");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("RemMeter");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Width");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Rate");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("FileUpload");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Image");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("ImageLabel");
                    dttt.Columns.Add(dct);

                    dstd.Tables.Add(dttt);
                    foreach (DataRow dr in dsdd.Tables[0].Rows)
                    {
                        drNew = dttt.NewRow();
                        drNew["OrderNo"] = dr["Orderno"];
                        drNew["Design"] = dr["DesignNo"];
                        drNew["Item"] = dr["ItemName"];
                        drNew["Color"] = dr["Color"];
                        drNew["Piece"] = dr["Piece"];
                        drNew["orderPiece"] = dr["ordermtr"];
                        drNew["RemMeter"] = dr["retmeter"];
                        drNew["Meter"] = "0";
                        drNew["Width"] = dr["Width"];
                        drNew["Rate"] = dr["Rate"];
                        //drNew["Fileupload"] = dr["UnitPrice"];
                        //drNew["image"] = dr["UnitPrice"];
                        drNew["imageLabel"] = dr["imagepath"];

                        dstd.Tables[0].Rows.Add(drNew);
                    }

                    ViewState["CurrentTable1"] = dttt;

                    gvcustomerorder.DataSource = dstd;
                    gvcustomerorder.DataBind();

                    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                    {
                        TextBox orderno = (TextBox)gvcustomerorder.Rows[vLoop].Cells[0].FindControl("txtno");

                        TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                        txtdesign.Enabled = false;

                        TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                        txtItem.Enabled = false;
                        TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                        txtpiece.Enabled = false;
                        TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtorderpiece");

                        TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                        TextBox txtremmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtremmeter");


                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                        Label imgpath = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

                        DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");
                        drpwid.Enabled = false;
                        TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                        txtcolor.Enabled = false;
                        //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");

                        orderno.Text = dstd.Tables[0].Rows[vLoop]["orderno"].ToString();
                        txtdesign.Text = dstd.Tables[0].Rows[vLoop]["design"].ToString();
                        txtItem.Text = dstd.Tables[0].Rows[vLoop]["Item"].ToString();
                        txtpiece.Text = Convert.ToInt32(dstd.Tables[0].Rows[vLoop]["piece"]).ToString();
                        txtorderpiece.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["orderPiece"]).ToString();
                        txtmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["meter"]).ToString("N2");
                        txtremmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Remmeter"]).ToString("N2");
                        txtrate.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["rate"]).ToString("N2");
                        drpwid.SelectedValue = dstd.Tables[0].Rows[vLoop]["width"].ToString();
                        txtcolor.Text = dstd.Tables[0].Rows[vLoop]["color"].ToString();
                        //drpcolor.SelectedValue = dstd.Tables[0].Rows[vLoop]["color"].ToString();
                        imgpath.Text = dstd.Tables[0].Rows[vLoop]["imageLabel"].ToString();
                    }
                }
            }
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

            //    ddlDef_SelectedIndexChanged1(sender, e);
            // ButtonAdd1_Click(sender, e);
        }

        protected void gvcustomerorder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int UpdateStockAvailable(int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            ////if (sTableName == "admin")
            ////{

            //DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //int iRemQty = iAQty - iQty;
            //iSuccess = objBs.updateSalesStocknew(iRemQty, iSubCat, "tblStock_" + sTableName);

            ////}
            ////else
            ////{
            ////    DataSet dsStock = objBs.GetStockDetails_Dealer(iSubCat, "tblStock_" + sTableName);
            ////    if (dsStock.Tables[0].Rows.Count > 0)
            ////    {
            ////        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            ////    }
            ////    int iRemQty = iAQty - iQty;
            ////    iSuccess = objBs.updateSalesStock_dealer(iRemQty, iCat, iSubCat,"tblStock_"+sTableName);
            ////}
            return iSuccess;
        }

        private int InsertStockAvailable(int iCat, int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            ////if (sTableName == "admin")
            ////{

            //string iQrySalesID = Request.QueryString.Get("iSalesID");
            //if (iQrySalesID != null)
            //{
            //    DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            //    if (dsStock.Tables[0].Rows.Count > 0)
            //    {
            //        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //    }
            //    int iRemQty = iAQty + iQty;
            //    iSuccess = objBs.updateSalesStock(iRemQty, iCat, iSubCat, "tblStock_" + sTableName);
            //}

            ////}
            ////else
            ////{
            ////    DataSet dsStock = objBs.GetStockDetails_Dealer(iSubCat, "tblStock_" + sTableName);
            ////    if (dsStock.Tables[0].Rows.Count > 0)
            ////    {
            ////        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            ////    }
            ////    int iRemQty = iAQty - iQty;
            ////    iSuccess = objBs.updateSalesStock_dealer(iRemQty, iCat, iSubCat,"tblStock_"+sTableName);
            ////}
            return iSuccess;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Fabric_Grid.aspx");
        }

        protected void Gridview1_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            // GridViewRow row = gvcustomerorder.SelectedRow;


            //if (e.CommandName == "Select")
            //{

            //    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            //    DropDownList yourTextbox = row.FindControl("drpitem") as DropDownList;
            //    var yourValue = yourTextbox.Text;
            //    if (yourValue == "Select Product")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product.')", true);
            //        return;

            //    }
            //    string value = yourTextbox.SelectedValue;
            //    DataSet ds = objBs.itemhistorypopup(sTableName, value);
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            GridView1.DataSource = ds;
            //            GridView1.DataBind();
            //        }
            //        else
            //        {
            //            GridView1.DataSource = null;
            //            GridView1.DataBind();
            //        }
            //        mpe.Show();
            //    }
            //}
            //else if (e.CommandName == "Select1")
            //{
            //    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            //    DropDownList yourTextbox = row.FindControl("drpitem") as DropDownList;

            //    var yourValue = yourTextbox.Text;

            //    if (yourValue == "Select Product")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product.')", true);
            //        return;

            //    }
            //    if (ddlcustomerID.SelectedValue == "Select Customer")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Customer Name')", true);
            //        return;

            //    }

            //    string value1 = yourTextbox.SelectedValue;

            //    string cust = ddlcustomerID.SelectedValue;
            //    DataSet ds = objBs.custhistorypopup(sTableName, value1, cust);
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            GridView1.DataSource = ds;
            //            GridView1.DataBind();
            //        }
            //        else
            //        {
            //            GridView1.DataSource = null;
            //            GridView1.DataBind();
            //        }
            //        mpe.Show();
            //    }
            //}
        }

        protected void previewclick(object sender, EventArgs e)
        {

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");

                int piece = 0;
                if (txtmeter.Text != "0.00" && txtmeter.Text != "0")
                {
                    for (int bLoop = 0; bLoop < gvcustomerorder.Rows.Count; bLoop++)
                    {

                        TextBox txtdesign1 = (TextBox)gvcustomerorder.Rows[bLoop].FindControl("txtdesno");
                        if (txtdesign1.Text == txtdesign.Text)
                        {
                            TextBox txtpiece = (TextBox)gvcustomerorder.Rows[bLoop].FindControl("txtpiece");
                            piece = piece + Convert.ToInt32(txtpiece.Text);
                        }

                    }

                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txtbarcode = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbarcode");
                    int month = DateTime.Now.Month;


                    txtbarcode.Text = txtinrefno.Text + "/" + month + "/" + drpcompany.SelectedValue + "/" + Convert.ToDouble(txtrate.Text).ToString("#.##") + "/" + "P" + piece.ToString();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            int iq = 1;
            int ii = 1;
            int brand = 0;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            string itemname = string.Empty;
            string itemwidth = string.Empty;

            double roll = 0;
            //double tax = 0;
            //double distotal = 0;
            //double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");

                roll = roll + Convert.ToDouble(txtpiece.Text);
                
            }
            lblroll.Text = roll.ToString("0.00");

            if (btnadd.Text == "Save")
            {


                if (txtinrefno.Text == "" || txtinrefno.Text=="0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Supplier Invoice No.');", true);
                    return;
                }
                DataSet dsfabinv = objBs.checkfabinvnumbers(txtinrefno.Text,"0");
                if (dsfabinv.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Supplier Invoice No. was already Exist.');", true);
                    return;
                }
                #region

                if (drpsupplier.SelectedValue == "Select Supplier")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Supplier Name');", true);
                    return;
                }

                if (drpchecked.SelectedValue == "Select Employee Name")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name');", true);
                    return;
                }

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                    TextBox txtitem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtitem");
                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtremmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtremmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                    Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

                    TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtorderpiece");
                    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                    DropDownList ddlfabtype = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabtype");
                    DropDownList ddlfabstyle = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ddlfabstyle");
                    DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");
                    TextBox txtShrinkage = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtShrinkage");
                    TextBox txtPinning = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtPinning");

                    int col = vLoop + 1;


                    txtno.Focus();


                    itemc = ddlfabtype.SelectedValue;
                    itemname = ddlfabtype.SelectedItem.Text;
                    itemcd = txtcolor.Text;
                    itemwidth = drpwid.SelectedValue;


                    if ((itemc == null) || (itemc == "" || itemc == "Type"))
                    {
                    }
                    else
                    {
                        for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                        {

                            DropDownList ddlfabtype1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ddlfabtype");
                            TextBox txtdesign1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtdesno");
                            TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtcolor");
                            DropDownList drpwid1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpwid");
                            if (ddlfabtype1.SelectedValue == "Type" || ddlfabtype1.SelectedValue == "")
                            {
                            }
                            else
                            {

                                if (ii == iq)
                                {
                                }
                                else
                                {
                                    if (itemc == ddlfabtype1.SelectedValue && itemcd == txtcolor1.Text && itemwidth == drpwid1.SelectedValue)
                                    {
                                        itemname = ddlfabtype1.SelectedItem.Text;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemname + '-' + txtcolor1.Text + '-' + drpwid1.SelectedItem.Text + "  already exists in the Grid.');", true);

                                        return;

                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                    }
                    iq = iq + 1;
                    ii = 1;





                    if (drpwid.SelectedValue == "Select Width")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Width in row " + col + " ')", true);

                        return;
                    }
                    if (drpcolor.SelectedValue == "Select Color")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Color in row " + col + " ')", true);

                        drpcolor.Focus();
                        return;
                    }
                    if (ddlfabtype.SelectedValue == "Type")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Type in row " + col + " ')", true);

                        return;
                    }
                    if (ddlfabstyle.SelectedValue == "Style")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Style in row " + col + " ')", true);

                        return;
                    }
                }


                DateTime regdate = DateTime.ParseExact(txtregdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime invdate = DateTime.ParseExact(txtinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (ddlBrand.SelectedValue == "Select Brand" || ddlBrand.SelectedValue == "")
                {
                    brand = 0;
                }
                else
                {
                    brand = Convert.ToInt32(ddlBrand.SelectedValue);
                }

                if (ddltype.SelectedValue == "0")
                {
                    int iStatus23 = objBs.insertfab(txtinvno.Text, drpsupplier.SelectedValue, regdate, drpchecked.SelectedValue, txtinrefno.Text,
                        invdate, txttotmet.Text, "0", txtDelChalan.Text, txttoal.Text, brand, txtlrno.Text, txtTransport.Text, ddltype.SelectedValue, "0", drpcompany.SelectedValue, txtbaleQty.Text, txtsupplieroderno.Text, imgpreview1.Text, empid, Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtcgst.Text), Convert.ToDouble(txtsgst.Text), Convert.ToDouble(txtigst.Text), drpchecked.SelectedItem.Text, Convert.ToDouble(txtcom.Text), Convert.ToDouble(txtFreight.Text), Convert.ToDouble(txtLU.Text), drppurchasetype.SelectedValue, chkchargesapply.Checked, txtsubtotal.Text, txtNarration.Text, txtbillmet.Text, txtdisamount.Text, txttotdisamount.Text, txtnetingcharge.Text,lblroll.Text);
                }
                else
                {
                    int iStatus23 = objBs.insertfab(txtinvno.Text, drpsupplier.SelectedValue, regdate, drpchecked.SelectedValue, txtinrefno.Text,
                        invdate, txttotmet.Text, "0", txtDelChalan.Text, txttoal.Text, brand, txtlrno.Text, txtTransport.Text, ddltype.SelectedValue, ddlordno.SelectedValue, drpcompany.SelectedValue, txtbaleQty.Text, txtsupplieroderno.Text, imgpreview1.Text, empid, Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtcgst.Text), Convert.ToDouble(txtsgst.Text), Convert.ToDouble(txtigst.Text), drpchecked.SelectedItem.Text, Convert.ToDouble(txtcom.Text), Convert.ToDouble(txtFreight.Text), Convert.ToDouble(txtLU.Text), drppurchasetype.SelectedValue, chkchargesapply.Checked, txtsubtotal.Text, txtNarration.Text, txtbillmet.Text, txtdisamount.Text, txttotdisamount.Text, txtnetingcharge.Text, lblroll.Text);
                }

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtbillmeter");
                    if (txtbillmeter.Text != "0.00" && txtbillmeter.Text != "0" && txtbillmeter.Text != "")
                    {
                        TextBox txtmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtmeter");

                        TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                        TextBox txtdesign = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesno");


                        TextBox txtItem = (TextBox)gvcustomerorder.Rows[i].FindControl("txtItem");

                        TextBox txtpiece = (TextBox)gvcustomerorder.Rows[i].FindControl("txtpiece");

                        TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[i].FindControl("txtorderpiece");

                        TextBox txtremmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemmeter");
                        TextBox txtbarcode = (TextBox)gvcustomerorder.Rows[i].FindControl("txtbarcode");

                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                        Label imgpath = (Label)gvcustomerorder.Rows[i].FindControl("imgpreview");

                        DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpwid");

                        TextBox txtcolor = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcolor");
                        DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpcolor");

                        CheckBox cancel = (CheckBox)gvcustomerorder.Rows[i].FindControl("chkid");

                        TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcancelmeter");

                        DropDownList ddlfabtype = (DropDownList)gvcustomerorder.Rows[i].FindControl("ddlfabtype");
                        DropDownList ddlfabstyle = (DropDownList)gvcustomerorder.Rows[i].FindControl("ddlfabstyle");
                        DropDownList ddlfabmode = (DropDownList)gvcustomerorder.Rows[i].FindControl("ddlfabmode");
                        TextBox txtShrinkage = (TextBox)gvcustomerorder.Rows[i].FindControl("txtShrinkage");
                        TextBox txtPinning = (TextBox)gvcustomerorder.Rows[i].FindControl("txtPinning");

                        TextBox txttax = (TextBox)gvcustomerorder.Rows[i].FindControl("txttax");
                        TextBox txttaxamount = (TextBox)gvcustomerorder.Rows[i].FindControl("txttaxamount");
                        TextBox txtamount = (TextBox)gvcustomerorder.Rows[i].FindControl("txtamount");


                        DropDownList drpextralist = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpextralist");
                        TextBox txtnarration = (TextBox)gvcustomerorder.Rows[i].FindControl("txtnarration");


                        if (ddltype.SelectedValue == "1")
                        {


                            int iStatus2 = objBs.insertTransfab(txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(ddlordno.SelectedValue), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                        }
                        else
                        {
                            int iStatus2 = objBs.insertTransfab(txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(0), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                        }
                    }

                }
                #endregion

            }
            else
            {
                string ifabID = Request.QueryString.Get("iid");
                if (txtinrefno.Text == "" || txtinrefno.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Supplier Invoice No.');", true);
                    return;
                }
                DataSet dsfabinv = objBs.checkfabinvnumbers(txtinrefno.Text, ifabID);
                if (dsfabinv.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Supplier Invoice No. was already Exist.');", true);
                    return;
                }

                #region

                if (drpsupplier.SelectedValue == "Select Supplier")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Supplier Name');", true);
                    return;
                }

                if (drpchecked.SelectedValue == "Select Employee Name")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name');", true);
                    return;
                }

                DateTime regdate = DateTime.ParseExact(txtregdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime invdate = DateTime.ParseExact(txtinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (ddlBrand.SelectedValue == "Select Brand" || ddlBrand.SelectedValue == "")
                {
                    brand = 0;
                }
                else
                {
                    brand = Convert.ToInt32(ddlBrand.SelectedValue);
                }



                string ifabID1 = Request.QueryString.Get("iid");

                DataSet dsdd = objBs.Get_TransFabric(Convert.ToInt32(ifabID1));



                if (ddltype.SelectedValue == "0")
                {
                    int iStatus23 = objBs.UpdateFab(ifabID, txtinvno.Text, drpsupplier.SelectedValue, regdate, drpchecked.SelectedValue, txtinrefno.Text,
                        invdate, txttotmet.Text, "0", txtDelChalan.Text, txttoal.Text, brand, txtlrno.Text, txtTransport.Text, ddltype.SelectedValue, "0", drpcompany.SelectedValue, txtbaleQty.Text, txtsupplieroderno.Text, imgpreview1.Text, empid, Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtcgst.Text), Convert.ToDouble(txtsgst.Text), Convert.ToDouble(txtigst.Text), drpchecked.SelectedItem.Text, Convert.ToDouble(txtcom.Text), Convert.ToDouble(txtFreight.Text), Convert.ToDouble(txtLU.Text), drppurchasetype.SelectedValue, chkchargesapply.Checked, txtsubtotal.Text, txtNarration.Text, txtbillmet.Text, txtdisamount.Text, txttotdisamount.Text, txtnetingcharge.Text, lblroll.Text);
                }
                else
                {
                    int iStatus23 = objBs.UpdateFab(ifabID, txtinvno.Text, drpsupplier.SelectedValue, regdate, drpchecked.SelectedValue, txtinrefno.Text,
                        invdate, txttotmet.Text, "0", txtDelChalan.Text, txttoal.Text, brand, txtlrno.Text, txtTransport.Text, ddltype.SelectedValue, ddlordno.SelectedValue, drpcompany.SelectedValue, txtbaleQty.Text, txtsupplieroderno.Text, imgpreview1.Text, empid, Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToDouble(txtcgst.Text), Convert.ToDouble(txtsgst.Text), Convert.ToDouble(txtigst.Text), drpchecked.SelectedItem.Text, Convert.ToDouble(txtcom.Text), Convert.ToDouble(txtFreight.Text), Convert.ToDouble(txtLU.Text), drppurchasetype.SelectedValue, chkchargesapply.Checked, txtsubtotal.Text, txtNarration.Text, txtbillmet.Text, txtdisamount.Text, txttotdisamount.Text, txtnetingcharge.Text, lblroll.Text);
                }

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    TextBox txttax = (TextBox)gvcustomerorder.Rows[i].FindControl("txttax");
                    TextBox txttaxamount = (TextBox)gvcustomerorder.Rows[i].FindControl("txttaxamount");
                    TextBox txtamount = (TextBox)gvcustomerorder.Rows[i].FindControl("txtamount");

                    TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                    HiddenField hdtransid = (HiddenField)gvcustomerorder.Rows[i].Cells[0].FindControl("hdtransid");

                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesno");


                    TextBox txtItem = (TextBox)gvcustomerorder.Rows[i].FindControl("txtItem");

                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[i].FindControl("txtpiece");

                    TextBox txtorderpiece = (TextBox)gvcustomerorder.Rows[i].FindControl("txtorderpiece");

                    TextBox txtremmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemmeter");
                    TextBox txtbarcode = (TextBox)gvcustomerorder.Rows[i].FindControl("txtbarcode");

                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                    Label imgpath = (Label)gvcustomerorder.Rows[i].FindControl("imgpreview");

                    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpwid");

                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcolor");

                    CheckBox cancel = (CheckBox)gvcustomerorder.Rows[i].FindControl("chkid");

                    TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcancelmeter");

                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtmeter");

                    TextBox txtbillmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtbillmeter");

                    DropDownList ddlfabtype = (DropDownList)gvcustomerorder.Rows[i].FindControl("ddlfabtype");
                    DropDownList ddlfabstyle = (DropDownList)gvcustomerorder.Rows[i].FindControl("ddlfabstyle");
                    DropDownList ddlfabmode = (DropDownList)gvcustomerorder.Rows[i].FindControl("ddlfabmode");
                    TextBox txtShrinkage = (TextBox)gvcustomerorder.Rows[i].FindControl("txtShrinkage");
                    TextBox txtPinning = (TextBox)gvcustomerorder.Rows[i].FindControl("txtPinning");

                    DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpcolor");

                    DropDownList drpextralist = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpextralist");
                    TextBox txtnarration = (TextBox)gvcustomerorder.Rows[i].FindControl("txtnarration");

                    if (txtbillmeter.Text != "0.00" && txtbillmeter.Text != "0" && txtbillmeter.Text != "")
                    {

                        if (ddltype.SelectedValue == "1")
                        {

                            if (hdtransid.Value == "")
                            {
                                int iStatus2 = objBs.newinsertTransfab(ifabID, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(ddlordno.SelectedValue), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);

                            }
                            else
                            {
                                int iStatus2 = objBs.Update_Transfab(Convert.ToInt32(hdtransid.Value), txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(ddlordno.SelectedValue), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                            }
                        }
                        else
                        {
                            if (hdtransid.Value == "")
                            {
                                int iStatus2 = objBs.newinsertTransfab(ifabID, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(0), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                            }
                            else
                            {
                                int iStatus2 = objBs.Update_Transfab(Convert.ToInt32(hdtransid.Value), txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(0), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                            }
                        }
                    }
                    else
                    {
                        if (ddltype.SelectedValue == "1")
                        {

                            if (hdtransid.Value == "")
                            {
                                int iStatus2 = objBs.newinsertTransfab(ifabID, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(ddlordno.SelectedValue), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                            }
                            else
                            {
                                int iStatus2 = objBs.Update_Transfab(Convert.ToInt32(hdtransid.Value), txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(ddlordno.SelectedValue), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                            }
                        }
                        else
                        {
                            if (hdtransid.Value == "")
                            {
                                int iStatus2 = objBs.newinsertTransfab(ifabID, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(0), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                            }
                            else
                            {
                                int iStatus2 = objBs.Update_Transfab(Convert.ToInt32(hdtransid.Value), txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, txtorderpiece.Text, Convert.ToInt32(0), txtbarcode.Text, ddlfabtype.SelectedItem.Text, txtremmeter.Text, cancel.Checked, txtcancelmeter.Text, Convert.ToInt32(ddlfabtype.SelectedValue), Convert.ToInt32(ddlfabstyle.SelectedValue), txtShrinkage.Text, txtPinning.Text, Convert.ToInt32(ddlfabmode.SelectedValue), Convert.ToDouble(txttax.Text), Convert.ToDouble(txtamount.Text), drpcolor.SelectedValue, drpextralist.SelectedValue, txtnarration.Text, txtbillmeter.Text, txttaxamount.Text);
                            }
                        }
                    }
                }
                #endregion
            }
            Response.Redirect("Fabric_Grid.aspx");
        }

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
            //                dst = objBs.selectcategoryalldecriptionbranch(sTableName);
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

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //string iSalesID = Request.QueryString.Get("iSalesID");
            //Response.Redirect("Print_Sales.aspx?iSalesID=" + iSalesID);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //string iSalesID = Request.QueryString.Get("iSalesID");
            //int iSucess = objBs.DeleteSales("tblSales_" + sTableName, iSalesID, "tblDayBook_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, btnDelete.Text, sTableName);

            //int isalesid = Convert.ToInt32(iSalesID);

            //DataSet dsTransSales = objBs.GetTransSales("tblTransSales_" + sTableName, iSalesID);
            //if (dsTransSales.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dsTransSales.Tables[0].Rows.Count; i++)
            //    {
            //        string sddlCat = dsTransSales.Tables[0].Rows[i]["CategoryID"].ToString();
            //        string sddlDef = dsTransSales.Tables[0].Rows[i]["SubCategoryID"].ToString();
            //        string sQty = dsTransSales.Tables[0].Rows[i]["Quantity"].ToString();
            //        int iSuccs = UpdateEditStock(Convert.ToInt32(sddlCat), Convert.ToInt32(sddlDef), Convert.ToInt32(sQty));

            //    }
            //}

            //int iTransDelete = objBs.DeleteTransSales("tblTransSales_" + sTableName, iSalesID);

            //Response.Redirect("salesgrid.aspx");

        }

        private int UpdateEditStock(int iCat, int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            ////if (sTableName == "admin")
            ////{
            //DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //int iInsQty = iAQty + iQty;
            //iSuccess = objBs.updateSalesStock(iInsQty, iCat, iSubCat, "tblStock_" + sTableName);

            return iSuccess;
        }


        protected void btnUpload2_Click(object sender, EventArgs e)
        {
            if (idupload1.HasFile)
            {
                idupload1.SaveAs(MapPath("~/Files/" + idupload1.FileName));
                imgpreview1.Text = MapPath("~/Files/" + idupload1.FileName);
            }
        }


        protected void txtcancelmeter_TextChanged(object sender, EventArgs e)
        {

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");
                TextBox txtremmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtremmeter");

                int col = vLoop + 1;
                if (Convert.ToDouble(txtcancelmeter.Text) > Convert.ToDouble(txtremmeter.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cancel Meter is greater than Remaining Meter in row " + col + " ')", true);
                    txtcancelmeter.Focus();
                }


            }

        }



    }
}
