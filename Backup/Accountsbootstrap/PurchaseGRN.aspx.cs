using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class PurchaseGRN : System.Web.UI.Page
    {

        BSClass objBs = new BSClass();
        string sTableName = "";
        string YearCode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            //YearCode = Session["YearCode"].ToString();
            YearCode = Request.Cookies["userInfo"]["YearCode"].ToString();
            if (!IsPostBack)
            {
                txtRecDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsPONo = objBs.GetPurchaseRecPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["RecPONo"].ToString().PadLeft(4, '0');
                txtRecNo.Text = PONo + " / " + YearCode;

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, "CompanyName");
                }

                DataSet dsPaymode = objBs.Getpaymentmode();
                if (dsPaymode.Tables[0].Rows.Count > 0)
                {
                    ddlPayMode.DataSource = dsPaymode.Tables[0];
                    ddlPayMode.DataTextField = "Payment_Mode";
                    ddlPayMode.DataValueField = "Payment_ID";
                    ddlPayMode.DataBind();
                    ddlPayMode.Items.Insert(0, "Select Paymode");
                }


                DataSet dsb = objBs.GetLedgers(4, sTableName);
                if (dsb != null)
                {
                    if (dsb.Tables[0].Rows.Count > 0)
                    {
                        ddlBank.DataSource = dsb;
                        ddlBank.DataTextField = "LedgerName";
                        ddlBank.DataValueField = "LedgerID";
                        ddlBank.DataBind();
                        ddlBank.Items.Insert(0, "Select Bank Name");
                    }
                }

                DataSet dsProcessOn = objBs.GetCategory_as_Process("ShowPOrder");
                if (dsProcessOn.Tables[0].Rows.Count > 0)
                {
                    ddlProcessOn.DataSource = dsProcessOn.Tables[0];
                    ddlProcessOn.DataTextField = "category";
                    ddlProcessOn.DataValueField = "categoryid";
                    ddlProcessOn.DataBind();
                    ddlProcessOn.Items.Insert(0, "ProcessOn");
                }


                DataSet dsset = objBs.getLedger_New(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlPartyCode.DataSource = dsset.Tables[0];
                    ddlPartyCode.DataTextField = "CompanyCode";
                    ddlPartyCode.DataValueField = "LedgerID";
                    ddlPartyCode.DataBind();
                    ddlPartyCode.Items.Insert(0, "PartyCode");

                    ddlPartyName.DataSource = dsset.Tables[0];
                    ddlPartyName.DataTextField = "LedgerName";
                    ddlPartyName.DataValueField = "LedgerID";
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, "PartyName");
                }


                string POGRNId = Request.QueryString.Get("POGRNId");
                if (POGRNId != "" && POGRNId != null)
                {
                    DataSet dsRecPO = objBs.getPurchaseGRN(Convert.ToInt32(POGRNId));
                    if (dsRecPO.Tables[0].Rows.Count > 0)
                    {
                        #region



                        ddlPoNo.SelectedValue = dsRecPO.Tables[0].Rows[0]["POId"].ToString();
                        ddlPoNo.Enabled = false;
                        ddlProcessOn.SelectedValue = dsRecPO.Tables[0].Rows[0]["ProcessOn"].ToString();

                        txtRecDate.Text = Convert.ToDateTime(dsRecPO.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");

                        DataSet dsPO = objBs.getPurchaseOrder(Convert.ToInt32(dsRecPO.Tables[0].Rows[0]["POId"].ToString()));
                        if (dsPO.Tables[0].Rows.Count > 0)
                        {
                            txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                            txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                            txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");
                        }

                        txtDeliveryPlace.Text = dsRecPO.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        txtRecNo.Text = dsRecPO.Tables[0].Rows[0]["FullRecPONo"].ToString();
                        ddlCompany.SelectedValue = dsRecPO.Tables[0].Rows[0]["companyid"].ToString();

                        ddlPartyCode.SelectedValue = dsRecPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyName.SelectedValue = dsRecPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyCode_OnSelectedIndexChanged(sender, e);

                        DataSet dsItemProcessPo = objBs.GetPo(ddlPartyName.SelectedValue, "U");
                        if (dsItemProcessPo.Tables[0].Rows.Count > 0)
                        {
                            ddlPoNo.DataSource = dsItemProcessPo.Tables[0];
                            ddlPoNo.DataTextField = "FullPONo";
                            ddlPoNo.DataValueField = "PoId";
                            ddlPoNo.DataBind();
                            ddlPoNo.Items.Insert(0, "PONo");
                        }
                        else
                        {
                            ddlPoNo.Items.Insert(0, "PONo");
                        }

                        txtChallanNo.Text = dsRecPO.Tables[0].Rows[0]["ChallanNo"].ToString();

                        txtTotalAmount.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");
                        ddlProvince.SelectedValue = dsRecPO.Tables[0].Rows[0]["ProvinceId"].ToString();
                        drpGSTType.SelectedValue = dsRecPO.Tables[0].Rows[0]["GSTType"].ToString();
                        ddlPayMode.SelectedValue = dsRecPO.Tables[0].Rows[0]["PaymentMode"].ToString();
                        ddlPayMode_SelectedIndexChanged(sender, e);
                        ddlBank.SelectedValue = dsRecPO.Tables[0].Rows[0]["BankId"].ToString();
                        txtCheque.Text = dsRecPO.Tables[0].Rows[0]["ChequeNo"].ToString();
                        txtRoundoff.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["Roundoff"]).ToString("f2");
                        txtTotCGST.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["CGST"]).ToString("f2");
                        txtTotSGST.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["SGST"]).ToString("f2");
                        txtTotIGST.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["IGST"]).ToString("f2");
                        txtTotBeforeTAX.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["BeforeTAXAmount"]).ToString("f2");
                        txtGrandTotal.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["NetTotal"]).ToString("f2");
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;

                        #endregion
                    }

                    DataSet ds2 = objBs.getTransPurchaseGRN(Convert.ToInt32(POGRNId));
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

                        dct = new DataColumn("TransId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("PurchaseFor");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("PurchaseForId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("PurchaseForType");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("PurchaseForTypeId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Item");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ItemId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Color");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ColorId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Qty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Shrink");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("TotalQty");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("RQty");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("BalQty");
                        dttt.Columns.Add(dct);


                        dct = new DataColumn("Rate");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Amount");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("RecQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Remarks");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("TaxID");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Tax");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("TotAmount");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("BeforeTAX");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        foreach (DataRow Dr in ds2.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();

                            drNew["TransId"] = Dr["POTransId"];

                            drNew["PurchaseFor"] = Dr["PurchaseFor"];
                            drNew["PurchaseForID"] = Dr["PurchaseforId"];
                            drNew["PurchaseForType"] = Dr["PurchaseforType"];
                            drNew["PurchaseForTypeId"] = Dr["PurchaseforTypeId"];

                            drNew["Item"] = Dr["Item"];
                            drNew["ItemId"] = Dr["ItemId"];

                            drNew["Color"] = Dr["Color"];
                            drNew["ColorId"] = Dr["ColorId"];

                            drNew["Qty"] = Dr["Qty"];
                            drNew["Shrink"] = Dr["Shrink"];
                            drNew["TotalQty"] = Dr["TotalQty"];

                            drNew["RQty"] = Dr["RecQty"];

                            drNew["BalQty"] = (Convert.ToDouble(Dr["TotalQty"]) - Convert.ToDouble(Dr["RecQty"])).ToString();

                            drNew["Rate"] = Dr["Rate"];
                            drNew["Amount"] = Dr["Amount"];

                            drNew["RecQty"] = Dr["RecQty"];

                            drNew["Remarks"] = Dr["Remarks"];
                            drNew["TaxID"] = Dr["TaxID"];
                            drNew["Tax"] = Dr["Tax"];
                            drNew["TotAmount"] = Dr["NetTotal"];
                            drNew["BeforeTAX"] = Dr["BeforeTAX"];

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();


                    }
                }
            }

        }


        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBank.Enabled = false;

            txtCheque.Enabled = false;
            if (ddlPayMode.SelectedValue == "2" || ddlPayMode.SelectedValue == "3" || ddlPayMode.SelectedValue == "4" || ddlPayMode.SelectedValue == "5")
            {
                Div7.Visible = true;
                Div17.Visible = true;

                ddlBank.Enabled = true;
                txtCheque.Enabled = true;
                po.Update();
            }
            else
            {
                Div7.Visible = false;
                Div17.Visible = false;

                ddlBank.Enabled = false;
                txtCheque.Enabled = false;
                po.Update();
            }
        }

        protected void Party_Click_chnaged(object sender, EventArgs e)
        {
            ddlPoNo.ClearSelection();
            ddlPoNo.Items.Clear();

            if (ddlPartyName.SelectedValue == "PartyName")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Valid Party Name.')", true);
                return;
            }
            else
            {
                DataSet dsDetails = objBs.GetLedgerCheck(Convert.ToInt32(ddlPartyName.SelectedValue));
                if (dsDetails.Tables[0].Rows.Count > 0)
                {
                    ddlProvince.SelectedValue = dsDetails.Tables[0].Rows[0]["province"].ToString();
                    drpGSTType.SelectedValue = dsDetails.Tables[0].Rows[0]["GSTType"].ToString();
                    UpdatePanel1.Update();
                }

                DataSet dsItemProcessPo = objBs.GetPo(ddlPartyName.SelectedValue, "S");
                if (dsItemProcessPo.Tables[0].Rows.Count > 0)
                {
                    ddlPoNo.DataSource = dsItemProcessPo.Tables[0];
                    ddlPoNo.DataTextField = "FullPONo";
                    ddlPoNo.DataValueField = "PoId";
                    ddlPoNo.DataBind();
                    ddlPoNo.Items.Insert(0, "PONo");
                }
                else
                {
                    ddlPoNo.Items.Insert(0, "PONo");
                }
            }


        }

        protected void ddlPoNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPoNo.SelectedValue != "" && ddlPoNo.SelectedValue != "" && ddlPoNo.SelectedValue != "PONo")
            {
                DataSet dsPO = objBs.getPurchaseOrder(Convert.ToInt32(ddlPoNo.SelectedValue));
                if (dsPO.Tables[0].Rows.Count > 0)
                {
                    #region

                    ddlProcessOn.SelectedValue = dsPO.Tables[0].Rows[0]["ProcessOn"].ToString();

                    txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                    ddlPartyCode.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                    ddlPartyName.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                    ddlPartyCode_OnSelectedIndexChanged(sender, e);

                    #endregion
                }

                DataSet ds2 = objBs.getTransPurchaseGRNPO(Convert.ToInt32(ddlPoNo.SelectedValue));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataSet dstd = new DataSet();
                    DataTable dtddd = new DataTable();
                    DataRow drNew;
                    DataColumn dct;
                    DataTable dttt = new DataTable();

                    dct = new DataColumn("TransId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("PurchaseFor");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("PurchaseForId");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("PurchaseForType");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("PurchaseForTypeId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Item");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("ItemId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Color");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("ColorId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Qty");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("Shrink");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("TotalQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("RQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("BalQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Rate");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("Amount");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("RecQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Remarks");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("IsRequest");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("IsReceive");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("TaxID");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("Tax");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("TotAmount");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("BeforeTAX");
                    dttt.Columns.Add(dct);

                    dstd.Tables.Add(dttt);

                    foreach (DataRow Dr in ds2.Tables[0].Rows)
                    {
                        drNew = dttt.NewRow();

                        drNew["TransId"] = Dr["TransId"];

                        drNew["PurchaseFor"] = Dr["PurchaseFor"];
                        drNew["PurchaseForID"] = Dr["PurchaseforId"];
                        drNew["PurchaseForType"] = Dr["PurchaseforType"];
                        drNew["PurchaseForTypeId"] = Dr["PurchaseforTypeId"];

                        drNew["Item"] = Dr["Item"];
                        drNew["ItemId"] = Dr["ItemId"];

                        drNew["Color"] = Dr["Color"];
                        drNew["ColorId"] = Dr["ColorId"];

                        drNew["Qty"] = Dr["Qty"];
                        drNew["Shrink"] = Dr["Shrink"];
                        drNew["TotalQty"] = Dr["TotalQty"];

                        drNew["RQty"] = Dr["RecQty"];

                        drNew["BalQty"] = (Convert.ToDouble(Dr["TotalQty"]) - Convert.ToDouble(Dr["RecQty"])).ToString();


                        drNew["Rate"] = Dr["Rate"];
                        drNew["Amount"] = Dr["Amount"];

                        drNew["Remarks"] = Dr["Remarks"];
                        drNew["IsRequest"] = Dr["Request"];
                        drNew["IsReceive"] = Dr["Receive"];
                        drNew["TaxID"] = Dr["TaxID"];
                        drNew["Tax"] = Dr["Tax"];
                        drNew["TotAmount"] = 0;
                        drNew["BeforeTAX"] = 0;
                        drNew["RecQty"] = 0;// Convert.ToDouble(Dr["Qty"]) - Convert.ToDouble(Dr["RecQty"]);

                        dstd.Tables[0].Rows.Add(drNew);
                        dtddd = dstd.Tables[0];
                    }

                    #endregion

                    ViewState["CurrentTable1"] = dtddd;
                    GVItem.DataSource = dtddd;
                    GVItem.DataBind();
                }
                else
                {
                    ViewState["CurrentTable1"] = null;
                    GVItem.DataSource = null;
                    GVItem.DataBind();
                }

            }
            else
            {
                ViewState["CurrentTable1"] = null;
                GVItem.DataSource = null;
                GVItem.DataBind();
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double beforetaxtotal = 0;
            double tax = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                TextBox txtqty = (TextBox)GVItem.Rows[vLoop].FindControl("txtqty");
                TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                Label txtTax = (Label)GVItem.Rows[vLoop].FindControl("txtTax");
                Label txtAmount = (Label)GVItem.Rows[vLoop].FindControl("txtAmount");
                Label txtTotAmount = (Label)GVItem.Rows[vLoop].FindControl("txtTotAmount");
                {
                    if (drpGSTType.SelectedValue == "1")
                    {
                        double iNetAmount = ((Convert.ToDouble(txtqty.Text)) * (Convert.ToDouble(txtRate.Text)));
                        txtAmount.Text = string.Format("{0:n2}", iNetAmount);
                        double tx = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtTax.Text) / 100;
                        double total = tx + iNetAmount;
                        txtTotAmount.Text = string.Format("{0:n2}", total);
                        grandtotal = grandtotal + total;
                        beforetaxtotal = beforetaxtotal + iNetAmount;
                        tax = tax + tx;
                    }
                    else
                    {
                        double iNetAmount = ((Convert.ToDouble(txtqty.Text)) * (Convert.ToDouble(txtRate.Text)));

                        double tx = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtTax.Text) / (100 + Convert.ToDouble(txtTax.Text));
                        double total = iNetAmount;
                        txtTotAmount.Text = string.Format("{0:n2}", total);
                        txtAmount.Text = string.Format("{0:n2}", (iNetAmount - tx));
                        grandtotal = grandtotal + total;
                        beforetaxtotal = beforetaxtotal + (iNetAmount - tx);
                        tax = tax + tx;
                    }
                }
            }

            txtGrandTotal.Text = string.Format("{0:n2}", (grandtotal));
            txtTotBeforeTAX.Text = string.Format("{0:n2}", (beforetaxtotal));

            if (ddlProvince.SelectedValue == "1")
            {
                txtTotCGST.Text = string.Format("{0:n2}", Convert.ToDouble(tax) / 2);
                txtTotSGST.Text = string.Format("{0:n2}", Convert.ToDouble(tax) / 2);
                txtTotIGST.Text = "0.00";
            }
            else
            {
                txtTotCGST.Text = "0.00";
                txtTotSGST.Text = "0.00";
                txtTotIGST.Text = string.Format("{0:n2}", Convert.ToDouble(tax));
            }

            double roundoff = Convert.ToDouble(txtGrandTotal.Text) - Math.Floor(Convert.ToDouble(txtGrandTotal.Text));
            if (roundoff > 0.5)
            {
                r = Math.Round(Convert.ToDouble(txtGrandTotal.Text), MidpointRounding.AwayFromZero);
            }
            else
            {
                r = Math.Floor(Convert.ToDouble(txtGrandTotal.Text));
            }
            txtRoundoff.Text = (r).ToString("0.00");
        }

        protected void drpGSTType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQty_TextChanged(sender, e);
            up1.Update();
        }

        protected void ddlPartyCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyCode.SelectedValue != "" && ddlPartyCode.SelectedValue != "0" && ddlPartyCode.SelectedValue != "PartyCode")
            {
                ddlPartyName.SelectedValue = ddlPartyCode.SelectedValue;

                DataSet dsParty = objBs.GetLedgerDetails(Convert.ToInt32(ddlPartyCode.SelectedValue));
                if (dsParty.Tables[0].Rows.Count > 0)
                {
                    txtContPerson.Text = dsParty.Tables[0].Rows[0]["ContacrPerson"].ToString();
                    txtAddress.Text = dsParty.Tables[0].Rows[0]["Address"].ToString();
                    txtPhone.Text = dsParty.Tables[0].Rows[0]["PhoneNo"].ToString();
                    txtCity.Text = dsParty.Tables[0].Rows[0]["City"].ToString();
                }

            }
            else
            {
                ddlPartyName.ClearSelection();

                txtContPerson.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
                txtCity.Text = "";
            }



        }

        private void FirstGridViewRow1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Item", typeof(string)));
            dt.Columns.Add(new DataColumn("ReceiveItem", typeof(string)));
            dt.Columns.Add(new DataColumn("Process", typeof(string)));

            dt.Columns.Add(new DataColumn("Color", typeof(string)));

            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Extra", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalQuantity", typeof(string)));

            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));

            dr = dt.NewRow();
            dr["Item"] = string.Empty;
            dr["ReceiveItem"] = string.Empty;
            dr["Process"] = string.Empty;

            dr["Color"] = string.Empty;

            dr["Quantity"] = string.Empty;
            dr["Extra"] = string.Empty;
            dr["TotalQuantity"] = string.Empty;

            dr["Rate"] = string.Empty;
            dr["Amount"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dt;

            GVItem.DataSource = dt;
            GVItem.DataBind();

            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("Item");
            dtt.Columns.Add(dct);
            dct = new DataColumn("ReceiveItem");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Process");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dtt.Columns.Add(dct);


            dct = new DataColumn("Quantity");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Extra");
            dtt.Columns.Add(dct);
            dct = new DataColumn("TotalQuantity");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Amount");
            dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);

            drNew = dtt.NewRow();
            drNew["Item"] = 0;
            drNew["ReceiveItem"] = 0;
            drNew["Process"] = 0;

            drNew["Color"] = "";

            drNew["Quantity"] = "";
            drNew["Extra"] = "";
            drNew["TotalQuantity"] = "";

            drNew["Rate"] = "";
            drNew["Amount"] = "";

            dstd.Tables[0].Rows.Add(drNew);

            GVItem.DataSource = dstd;
            GVItem.DataBind();

        }
        private void AddNewRow1()
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
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtAmount");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Item"] = ddlItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ReceiveItem"] = ddlReceiveItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Process"] = ddlProcess.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Color"] = ddlReceiveColor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Extra"] = txtExtra.Text;
                        dtCurrentTable.Rows[i - 1]["TotalQuantity"] = txtTotalQuantity.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = txtAmount.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    GVItem.DataSource = dtCurrentTable;
                    GVItem.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData1();

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
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtAmount");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Item"] = ddlItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ReceiveItem"] = ddlReceiveItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Process"] = ddlProcess.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Color"] = ddlReceiveColor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Extra"] = txtExtra.Text;
                        dtCurrentTable.Rows[i - 1]["TotalQuantity"] = txtTotalQuantity.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = txtAmount.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    GVItem.DataSource = dtCurrentTable;
                    GVItem.DataBind();
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
            double ItemCost = 0;
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtAmount");

                        ddlItemCode.SelectedValue = dt.Rows[i]["Item"].ToString();
                        ddlReceiveItemCode.SelectedValue = dt.Rows[i]["ReceiveItem"].ToString();
                        ddlProcess.SelectedValue = dt.Rows[i]["Process"].ToString();

                        ddlReceiveColor.SelectedValue = dt.Rows[i]["Color"].ToString();

                        txtQuantity.Text = dt.Rows[i]["Quantity"].ToString();
                        txtExtra.Text = dt.Rows[i]["Extra"].ToString();
                        txtTotalQuantity.Text = dt.Rows[i]["TotalQuantity"].ToString();

                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtAmount.Text = dt.Rows[i]["Amount"].ToString();

                        if (txtAmount.Text == "")
                            txtAmount.Text = "0";

                        ItemCost += Convert.ToDouble(txtAmount.Text);

                        rowIndex++;

                    }
                }
            }


        }


        protected void GVItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // SetRowData1();
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
                    GVItem.DataSource = dt;
                    GVItem.DataBind();

                    //  SetPreviousData1();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();

                    //  SetPreviousData1();
                    //  FirstGridViewRow1();
                }
            }

            Calculations();
        }

        protected void txtQty_OnTextChanged(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtQty = (TextBox)row.FindControl("txtQty");

            Calculations();
            txtQty.Focus();
        }


        protected void txtRate_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        public void Calculations()
        {
            double TotalAmount = 0;

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");
                TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");

                if (txtRate.Text == "")
                    txtRate.Text = "0";

                if (txtQty.Text == "")
                    txtQty.Text = "0";

                TotalAmount += Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtQty.Text);
                hdAmount.Value = (Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtQty.Text)).ToString();
            }

            txtTotalAmount.Text = TotalAmount.ToString();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                return;
            }

            //if (txtTotalAmount.Text == "")
            //    txtTotalAmount.Text = "0";
            //if (Convert.ToDouble(txtTotalAmount.Text) == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
            //    return;
            //}

            if (txtChallanNo.Text == "" || txtChallanNo.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check ChallanNo.')", true);
                return;
            }

            if (ddlPayMode.SelectedValue == "2" || ddlPayMode.SelectedValue == "3")
            {
                if (ddlBank.SelectedValue == "Select Bank Name")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No!');", true);
                    ddlBank.Focus();
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

                else if (txtCheque.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter Cheque No!');", true);
                    txtCheque.Focus();
                    return;
                }
            }


            if (ddlPayMode.SelectedValue == "4")
            {
                if (ddlBank.SelectedValue == "Select Bank Name")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Bank Name and Enter the Cheque No!');", true);
                    ddlBank.Focus();
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

                else if (txtCheque.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Enter NEFT/RTGS/IMPS No!');", true);
                    txtCheque.Focus();
                    return;

                }
            }


            DateTime RecDate = DateTime.ParseExact(txtRecDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int Id = 0;

            if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 1) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 6))
            {
                Id = 0;
            }
            else
            {
                //Id = Convert.ToInt32(ddlBank.SelectedValue);
            }

            if (Convert.ToInt32(ddlPayMode.SelectedValue) == 2)
            {
                Id = Convert.ToInt32(ddlBank.SelectedValue);
            }
            if (Convert.ToInt32(ddlPayMode.SelectedValue) == 3)
            {
                Id = Convert.ToInt32(ddlBank.SelectedValue);
            }
            if (Convert.ToInt32(ddlPayMode.SelectedValue) == 4)
            {
                Id = Convert.ToInt32(ddlBank.SelectedValue);
            }
            if (Convert.ToInt32(ddlPayMode.SelectedValue) == 5)
            {
                Id = Convert.ToInt32(ddlBank.SelectedValue);
            }


            if (btnSave.Text == "Save")
            {
                int bankid;
                int CreditorID1 = 0;
                string chequeno;
                // CHECK CHALLAN NUMBER

                DataSet dcheckchallan = objBs.check_challan(txtChallanNo.Text, ddlPartyCode.SelectedValue, "0");
                if (dcheckchallan.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Challan No Already Exists.For This Particular Party Code / Name.Thank You!!!.')", true);
                    txtChallanNo.Focus();
                    return;
                }

                if (ddlPayMode.SelectedValue == "2" || ddlPayMode.SelectedValue == "3")
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                    chequeno = txtCheque.Text;

                }
                else
                {
                    bankid = 0;
                    chequeno = "0";
                }

                if (ddlPayMode.SelectedValue == "4")
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                    chequeno = txtCheque.Text;

                }
                else
                {
                    bankid = 0;
                    chequeno = "0";
                }

                if (ddlPayMode.SelectedValue == "2" || ddlPayMode.SelectedValue == "3" || ddlPayMode.SelectedValue == "4")
                {
                    CreditorID1 = Convert.ToInt32(ddlBank.SelectedValue);

                }
                if (ddlPayMode.SelectedValue == "6")
                {
                    CreditorID1 = Convert.ToInt32(ddlPartyName.SelectedValue);
                }
                if (ddlPayMode.SelectedValue == "1")
                {
                    //DataSet dsledger = objbs.getCashledgerId("Cash A/C _" + sTableName);
                    DataSet dsset1 = objBs.getSetting("2", "Cash AC", sTableName);
                    CreditorID1 = Convert.ToInt32(dsset1.Tables[0].Rows[0]["LedgerID"].ToString());
                }

                //DataSet dsledger1 = objbs.getCashledgerId("PurchaseA/C_" + sTableName);

                DataSet dsset = objBs.getSetting("2", "Purchase AC", sTableName);
                string ledgerid = dsset.Tables[0].Rows[0]["LedgerID"].ToString();
                
                DataSet dsPONo = objBs.GetPurchaseRecPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["RecPONo"].ToString().PadLeft(4, '0');
                txtRecNo.Text = PONo + " / " + YearCode;

                DataSet dsPoNo = objBs.CheckPurchaseGRNPONo(txtRecNo.Text, 0);
                if (dsPoNo.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Pro.Rec. No. was already Exists.')", true);
                    txtRecNo.Focus();
                    return;
                }
                else
                {
                    int POGRNId = objBs.InsertPurchaseGRN(Convert.ToInt32(ddlPoNo.SelectedValue), Convert.ToInt32(ledgerid), Convert.ToString(CreditorID1), Convert.ToInt32(ddlProcessOn.SelectedValue), RecDate, txtDeliveryPlace.Text, dsPONo.Tables[0].Rows[0]["RecPONo"].ToString(), YearCode, txtRecNo.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToDouble(0), txtChallanNo.Text,"tblDayBook_" + sTableName,Convert.ToInt32(ddlProvince.SelectedValue),Convert.ToInt32(drpGSTType.SelectedValue),ddlPayMode.SelectedValue,Convert.ToInt32(Id),txtCheque.Text,Convert.ToDouble(txtRoundoff.Text),Convert.ToDouble(txtTotCGST.Text),Convert.ToDouble(txtTotSGST.Text),Convert.ToDouble(txtTotIGST.Text),Convert.ToDouble(txtGrandTotal.Text),Convert.ToDouble(txtTotBeforeTAX.Text));

                    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                    {
                        HiddenField hdTransId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransId");
                        HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");
                        HiddenField hdPurchaseForTypeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
                        HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");

                        HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                        HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                        HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                        //HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                        HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                        TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");

                        TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
                        TextBox txtRemarks = (TextBox)GVItem.Rows[vLoop].FindControl("txtRemarks");
                        Label txtTax = (Label)GVItem.Rows[vLoop].FindControl("txtTax");
                        Label txtTaxID = (Label)GVItem.Rows[vLoop].FindControl("txtTaxID");
                        Label txtTotAmount = (Label)GVItem.Rows[vLoop].FindControl("txtTotAmount");//nettotal
                        Label txtAmount = (Label)GVItem.Rows[vLoop].FindControl("txtAmount");//beforetax


                        if (txtQty.Text == "")
                            txtQty.Text = "0";

                        if (Convert.ToDouble(txtQty.Text) > 0)
                        {
                            int TransSamplingCostingId = objBs.InsertTransPurchaseGRN(POGRNId, Convert.ToInt32(hdTransId.Value), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(txtRate.Text), Convert.ToDouble(hdAmount.Value), Convert.ToDouble(txtQty.Text), txtRemarks.Text, Convert.ToInt32(ddlCompany.SelectedValue),Convert.ToInt32(txtTaxID.Text),txtTax.Text,Convert.ToDouble(txtTotAmount.Text),Convert.ToDouble(txtAmount.Text));                            
                        }
                    }
                }
            }
            else
            {
                // CHECK CHALLAN NUMBER
                string POGRNId = Request.QueryString.Get("POGRNId");
                DataSet dcheckchallan = objBs.check_challan(txtChallanNo.Text, ddlPartyCode.SelectedValue, POGRNId);
                if (dcheckchallan.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Challan No Already Exists.For This Particular Party Code / Name.Thank You!!!.')", true);
                    txtChallanNo.Focus();
                    return;
                }

                int Iupdatestock = objBs.UpdatestockGRN(POGRNId);

                int bankid;
                int CreditorID1 = 0;
                string chequeno;

                if (ddlPayMode.SelectedValue == "2" || ddlPayMode.SelectedValue == "3")
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                    chequeno = txtCheque.Text;

                }
                else
                {
                    bankid = 0;
                    chequeno = "0";
                }

                if (ddlPayMode.SelectedValue == "4")
                {
                    bankid = Convert.ToInt32(ddlBank.SelectedValue);
                    chequeno = txtCheque.Text;

                }
                else
                {
                    bankid = 0;
                    chequeno = "0";
                }

                if (ddlPayMode.SelectedValue == "2" || ddlPayMode.SelectedValue == "3" || ddlPayMode.SelectedValue == "4")
                {
                    CreditorID1 = Convert.ToInt32(ddlBank.SelectedValue);

                }
                if (ddlPayMode.SelectedValue == "6")
                {
                    CreditorID1 = Convert.ToInt32(ddlPartyName.SelectedValue);
                }
                if (ddlPayMode.SelectedValue == "1")
                {
                    //DataSet dsledger = objbs.getCashledgerId("Cash A/C _" + sTableName);
                    DataSet dsset1 = objBs.getSetting("2", "Cash AC", sTableName);
                    CreditorID1 = Convert.ToInt32(dsset1.Tables[0].Rows[0]["LedgerID"].ToString());
                }

                //DataSet dsledger1 = objbs.getCashledgerId("PurchaseA/C_" + sTableName);

                DataSet dsset = objBs.getSetting("2", "Purchase AC", sTableName);
                string ledgerid = dsset.Tables[0].Rows[0]["LedgerID"].ToString();

                DataSet dsDaybookId = objBs.selectDaybookidNew("tblPurchaseGRN", POGRNId);
                string daybookid = dsDaybookId.Tables[0].Rows[0]["DayBookTransNo"].ToString();

                int UPDPOID = objBs.UpdatePurchaseGRN(Convert.ToInt32(POGRNId), Convert.ToInt32(ledgerid), Convert.ToString(CreditorID1), Convert.ToInt32(ddlProcessOn.SelectedValue), RecDate, txtDeliveryPlace.Text, "", YearCode, ddlPoNo.SelectedItem.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToDouble(0), txtChallanNo.Text, POGRNId, "tblDayBook_" + sTableName, Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(drpGSTType.SelectedValue), ddlPayMode.SelectedValue, Convert.ToInt32(Id), txtCheque.Text, daybookid, Convert.ToDouble(txtRoundoff.Text), Convert.ToDouble(txtTotCGST.Text), Convert.ToDouble(txtTotSGST.Text), Convert.ToDouble(txtTotIGST.Text), Convert.ToDouble(txtGrandTotal.Text), Convert.ToDouble(txtTotBeforeTAX.Text));

                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    HiddenField hdTransId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransId");
                    HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");
                    HiddenField hdPurchaseForTypeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
                    HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");

                    HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                    HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                    HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                    //HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                    TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                    HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                    TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
                    TextBox txtRemarks = (TextBox)GVItem.Rows[vLoop].FindControl("txtRemarks");


                    Label txtTax = (Label)GVItem.Rows[vLoop].FindControl("txtTax");
                    Label txtTaxID = (Label)GVItem.Rows[vLoop].FindControl("txtTaxID");
                    Label txtTotAmount = (Label)GVItem.Rows[vLoop].FindControl("txtTotAmount");//nettotal
                    Label txtAmount = (Label)GVItem.Rows[vLoop].FindControl("txtAmount");//beforetax

                    if (txtQty.Text == "")
                        txtQty.Text = "0";

                    if (Convert.ToDouble(txtQty.Text) > 0)
                    {
                        int TransSamplingCostingId = objBs.UpdateTransPurchaseGRN(Convert.ToInt32(POGRNId), Convert.ToInt32(hdTransId.Value), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(txtRate.Text), Convert.ToDouble(hdAmount.Value), Convert.ToDouble(txtQty.Text), txtRemarks.Text, Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(txtTaxID.Text), txtTax.Text, Convert.ToDouble(txtTotAmount.Text), Convert.ToDouble(txtAmount.Text));
                    }
                }
            }
            Response.Redirect("PurchaseGRNGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseGRNGrid.aspx");
        }

    }
}
