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
    public partial class ItemProcessOrder : System.Web.UI.Page
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
                ddlExcNo.CausesValidation = true;
                ddlBuyerCode.CausesValidation = false;

                ddlIssueItems.Items.Insert(0, "Select Issue Item");
                ddlReceiveItems.Items.Insert(0, "Select Receive Item");

                DataSet dsPONo = objBs.GettemProcessOrderPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["PONo"].ToString().PadLeft(4, '0');
                txtProOrderNo.Text = "IPC - " + PONo + " / " + YearCode;

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                ddlPartyCode.Items.Insert(0, "PartyCode");
                ddlPartyName.Items.Insert(0, "PartyName");
                ddlProOrdNo.Items.Insert(0, "Pro.Ord.No.");

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, "CompanyName");
                }

                DataSet dsProcessOn = objBs.GetCategory_as_Process("ShowItemProcess");
                if (dsProcessOn.Tables[0].Rows.Count > 0)
                {
                    ddlProcessOn.DataSource = dsProcessOn.Tables[0];
                    ddlProcessOn.DataTextField = "category";
                    ddlProcessOn.DataValueField = "categoryid";
                    ddlProcessOn.DataBind();
                    ddlProcessOn.Items.Insert(0, "ProcessOn");
                }


                DataSet dsPurchaseFor = objBs.GetPurchaseFor();
                if (dsPurchaseFor.Tables[0].Rows.Count > 0)
                {
                    ddlPurchaseFor.DataSource = dsPurchaseFor.Tables[0];
                    ddlPurchaseFor.DataTextField = "PurchaseFor";
                    ddlPurchaseFor.DataValueField = "PurchaseForId";
                    ddlPurchaseFor.DataBind();
                }


                DataSet dsColor = objBs.gridColor();
                if (dsColor.Tables[0].Rows.Count > 0)
                {
                    ddlIssueColor.DataSource = dsColor.Tables[0];
                    ddlIssueColor.DataTextField = "Color";
                    ddlIssueColor.DataValueField = "ColorID";
                    ddlIssueColor.DataBind();
                    ddlIssueColor.Items.Insert(0, "Select IssueColor");

                    ddlReceiveColor.DataSource = dsColor.Tables[0];
                    ddlReceiveColor.DataTextField = "Color";
                    ddlReceiveColor.DataValueField = "ColorID";
                    ddlReceiveColor.DataBind();
                    ddlReceiveColor.Items.Insert(0, "Select ReceiveColor");
                }
                DataSet dsProcess = objBs.GetProcess();
                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    ddlProcess.DataSource = dsProcess.Tables[0];
                    ddlProcess.DataTextField = "Process";
                    ddlProcess.DataValueField = "ProcessId";
                    ddlProcess.DataBind();
                    ddlProcess.Items.Insert(0, "Process");
                }

                string ItemPOId = Request.QueryString.Get("ItemPOId");
                if (ItemPOId != "" && ItemPOId != null)
                {
                    DataSet dsPO = objBs.getItemProcessOrder(Convert.ToInt32(ItemPOId));
                    if (dsPO.Tables[0].Rows.Count > 0)
                    {
                        #region

                        ddlProcessOn.SelectedValue = dsPO.Tables[0].Rows[0]["ProcessOn"].ToString();
                        ddlProcessOn.Enabled = false;
                        ddlProcessOn_OnSelectedIndexChanged(sender, e);

                        ddlPartyCode.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyName.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyCode.Enabled = false;

                        DataSet dsParty = objBs.GetLedgerDetails(Convert.ToInt32(ddlPartyCode.SelectedValue));
                        if (dsParty.Tables[0].Rows.Count > 0)
                        {
                            txtContPerson.Text = dsParty.Tables[0].Rows[0]["ContacrPerson"].ToString();
                            txtAddress.Text = dsParty.Tables[0].Rows[0]["Address"].ToString();
                            txtPhone.Text = dsParty.Tables[0].Rows[0]["PhoneNo"].ToString();
                            txtCity.Text = dsParty.Tables[0].Rows[0]["City"].ToString();
                        }

                        DataSet dsProOrdNo = objBs.GetItemProcessOrderEntry2(ddlPartyCode.SelectedValue);
                        if (dsProOrdNo.Tables[0].Rows.Count > 0)
                        {
                            ddlProOrdNo.DataSource = dsProOrdNo.Tables[0];
                            ddlProOrdNo.DataTextField = "FullPONo";
                            ddlProOrdNo.DataValueField = "ItemEntryId";
                            ddlProOrdNo.DataBind();
                            ddlProOrdNo.Items.Insert(0, "Pro.Ord.No.");
                        }
                        else
                        {
                            ddlProOrdNo.Items.Insert(0, "Pro.Ord.No.");
                        }

                        ddlProOrdNo.SelectedValue = dsPO.Tables[0].Rows[0]["ItemEntryId"].ToString();
                        ddlProOrdNo.Enabled = false;


                        txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                        txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                        txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                        txtDeliveryPlace.Text = dsPO.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        txtProOrderNo.Text = dsPO.Tables[0].Rows[0]["FullPONo"].ToString();

                        ddlCompany.SelectedValue = dsPO.Tables[0].Rows[0]["CompanyId"].ToString();
                        ddlCompany.Enabled = false;

                        txtTotalAmount.Text = Convert.ToDouble(dsPO.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");

                        //btnSave.Text = "Update";
                        btnSave.Enabled = false;

                        #endregion
                    }

                    DataSet ds2 = objBs.getTransItemProcessOrder(Convert.ToInt32(ItemPOId));
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        DataSet ds3 = objBs.getTransItemProcessOrder1(Convert.ToInt32(ItemPOId));
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

                        dct = new DataColumn("IssueItem");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("IssueItemId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ReceiveItem");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ReceiveItemID");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("IssColor");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("IssColorId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("RecColor");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("RecColorId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Process");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ProcessId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Qty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Shrink");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("TotalQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Rate");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Amount");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Remarks");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("IsRequest");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("IsReceive");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("AvlQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("BalQty");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("IssuedQty");
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

                            drNew["IssueItem"] = Dr["IssueItem"];
                            drNew["IssueItemId"] = Dr["IssueItemId"];
                            drNew["ReceiveItem"] = Dr["ReceiveItem"];
                            drNew["ReceiveItemID"] = Dr["ReceiveItemId"];

                            drNew["IssColor"] = Dr["IssueColor"];
                            drNew["IssColorId"] = Dr["IssueColorId"];
                            drNew["RecColor"] = Dr["ReceiveColor"];
                            drNew["RecColorId"] = Dr["ReceiveColorId"];
                            drNew["Process"] = Dr["Process"];
                            drNew["ProcessId"] = Dr["ProcessId"];

                            drNew["Qty"] = Convert.ToDouble(Dr["OrderQty"]).ToString("f2");
                            drNew["Shrink"] = Convert.ToDouble(Dr["Shrink"]).ToString("f2");
                            drNew["TotalQty"] = Convert.ToDouble(Dr["TotalQty"]).ToString("f2");
                            drNew["Rate"] = Convert.ToDouble(Dr["Rate"]).ToString("f2");
                            drNew["Amount"] = Convert.ToDouble(Dr["Amount"]).ToString("f2");

                            drNew["Remarks"] = Dr["Remarks"];
                            drNew["IsRequest"] = Dr["Request"];
                            drNew["IsReceive"] = Dr["Receive"];

                            DataRow[] RowsQty = ds3.Tables[0].Select("IssueItemId='" + Dr["IssueItemId"] + "' and IssueColorId='" + Dr["IssueColorId"] + "' ");
                            drNew["AvlQty"] = (Convert.ToDouble(Dr["AvlQty"]) + Convert.ToDouble(RowsQty[0]["Qty"])).ToString("f2");

                            // drNew["AvlQty"] = (Convert.ToDouble(Dr["AvlQty"]) + Convert.ToDouble(Dr["Qty"])).ToString("f2");

                            drNew["BalQty"] = (Convert.ToDouble(Dr["BalanceQty"]) + Convert.ToDouble(Dr["Qty"])).ToString("f2");

                            drNew["IssuedQty"] = Convert.ToDouble(Dr["Qty"]).ToString("f2");

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();
                    }

                    btnSave.Text = "Update";

                    DataSet ds4 = objBs.getItemProcessOrderEditCheck(Convert.ToInt32(ItemPOId));
                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        btnSave.Enabled = false;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ItemProcess Order Receive was Created.')", true);
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }
                }
            }

        }

        protected void ddlProcessOn_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPartyCode.Items.Clear();
            ddlPartyName.Items.Clear();

            ddlProOrdNo.Items.Clear();
            ddlProOrdNo.Items.Insert(0, "Pro.Ord.No.");

            GVItem.DataSource = null;
            GVItem.DataBind();

            if (ddlProcessOn.SelectedValue != "" && ddlProcessOn.SelectedValue != "0" && ddlProcessOn.SelectedValue != "ProcessOn")
            {
                DataSet dsset = objBs.getpurchaseOrder_PArtyBind(ddlProcessOn.SelectedValue);
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
                else
                {
                    ddlPartyCode.Items.Insert(0, "PartyCode");
                    ddlPartyName.Items.Insert(0, "PartyName");
                }
            }
            else
            {
                ddlPartyCode.Items.Insert(0, "PartyCode");
                ddlPartyName.Items.Insert(0, "PartyName");
            }

        }
        protected void ddlPartyCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProOrdNo.Items.Clear();
            GVItem.DataSource = null;
            GVItem.DataBind();

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

                DataSet dsProOrdNo = objBs.GetItemProcessOrderEntry(ddlPartyCode.SelectedValue);
                if (dsProOrdNo.Tables[0].Rows.Count > 0)
                {
                    ddlProOrdNo.DataSource = dsProOrdNo.Tables[0];
                    ddlProOrdNo.DataTextField = "FullPONo";
                    ddlProOrdNo.DataValueField = "ItemEntryId";
                    ddlProOrdNo.DataBind();
                    ddlProOrdNo.Items.Insert(0, "Pro.Ord.No.");
                }
                else
                {
                    ddlProOrdNo.Items.Insert(0, "Pro.Ord.No.");
                }

            }
            else
            {
                ddlProOrdNo.Items.Insert(0, "Pro.Ord.No.");

                ddlPartyName.ClearSelection();

                txtContPerson.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
                txtCity.Text = "";
            }



        }
        protected void ddlProOrdNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProOrdNo.SelectedValue != "" && ddlProOrdNo.SelectedValue != "" && ddlProOrdNo.SelectedValue != "Pro.Ord.No.")
            {
                DataSet dsPO = objBs.getItemProcessOrderEntry(Convert.ToInt32(ddlProOrdNo.SelectedValue));
                if (dsPO.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.SelectedValue = dsPO.Tables[0].Rows[0]["CompanyId"].ToString();
                    ddlCompany.Enabled = false;

                    txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                }

                DataSet ds2 = objBs.getTransItemProcessOrderEntry(Convert.ToInt32(ddlProOrdNo.SelectedValue));
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

                    dct = new DataColumn("IssueItem");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("IssueItemId");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("ReceiveItem");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("ReceiveItemID");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("IssColor");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("IssColorId");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("RecColor");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("RecColorId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Process");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("ProcessId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Qty");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("Shrink");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("TotalQty");
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

                    dct = new DataColumn("AvlQty");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("BalQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("IssuedQty");
                    dttt.Columns.Add(dct);

                    dstd.Tables.Add(dttt);

                    foreach (DataRow Dr in ds2.Tables[0].Rows)
                    {
                        if (Convert.ToDouble(Dr["TotalQty"]) - (Convert.ToDouble(Dr["RecQty"]) + Convert.ToDouble(Dr["CancelQty"])) > 0)
                        {
                            drNew = dttt.NewRow();

                            drNew["TransId"] = Dr["TransId"];

                            drNew["PurchaseFor"] = Dr["PurchaseFor"];
                            drNew["PurchaseForID"] = Dr["PurchaseforId"];
                            drNew["PurchaseForType"] = Dr["PurchaseforType"];
                            drNew["PurchaseForTypeId"] = Dr["PurchaseforTypeId"];

                            drNew["IssueItem"] = Dr["IssueItem"];
                            drNew["IssueItemId"] = Dr["IssueItemId"];
                            drNew["ReceiveItem"] = Dr["ReceiveItem"];
                            drNew["ReceiveItemID"] = Dr["ReceiveItemId"];

                            drNew["IssColor"] = Dr["IssueColor"];
                            drNew["IssColorId"] = Dr["IssueColorId"];
                            drNew["RecColor"] = Dr["ReceiveColor"];
                            drNew["RecColorId"] = Dr["ReceiveColorId"];
                            drNew["Process"] = Dr["Process"];
                            drNew["ProcessId"] = Dr["ProcessId"];

                            drNew["Qty"] = Convert.ToDouble(Dr["Qty"]).ToString("f2");
                            drNew["Shrink"] = Convert.ToDouble(Dr["Shrink"]).ToString("f2");
                            drNew["TotalQty"] = Convert.ToDouble(Dr["TotalQty"]).ToString("f2");
                            drNew["Rate"] = Convert.ToDouble(Dr["Rate"]).ToString("f2");
                            drNew["Amount"] = Convert.ToDouble(Dr["Amount"]).ToString("f2");

                            drNew["Remarks"] = Dr["Remarks"];
                            drNew["IsRequest"] = Dr["Request"];
                            drNew["IsReceive"] = Dr["Receive"];

                            drNew["AvlQty"] = Dr["AvlQty"];
                            drNew["BalQty"] = Convert.ToDouble((Convert.ToDouble(Dr["TotalQty"]) - (Convert.ToDouble(Dr["RecQty"]) + Convert.ToDouble(Dr["CancelQty"])))).ToString("f2");

                            drNew["IssuedQty"] = Convert.ToDouble((Convert.ToDouble(Dr["TotalQty"]) - (Convert.ToDouble(Dr["RecQty"]) + Convert.ToDouble(Dr["CancelQty"])))).ToString("f2");

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }
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

            Calculations();
        }

        protected void ddlPurchaseFor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPurchaseFor.SelectedValue == "1")
            {
                ddlExcNo.CausesValidation = true;
                ddlBuyerCode.CausesValidation = false;

                BuyerExcNo.Visible = true;
                BuyerCode.Visible = false;
                BuyerShipment.Visible = true;
                StockCity.Visible = false;

                ddlBuyerCode.Items.Clear();
                ddlBuyerCode.Items.Insert(0, "BuyerCode");
                txtBuyerName.Text = "";
                txtBuyerCity.Text = "";

                DataSet ds = objBs.GetExcNoforPurchaseOrder();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = ds.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "ExcNo");
                }

            }
            else
            {
                ddlExcNo.CausesValidation = false;
                ddlBuyerCode.CausesValidation = true;

                BuyerExcNo.Visible = false;
                BuyerCode.Visible = true;
                BuyerShipment.Visible = false;
                StockCity.Visible = true;

                ddlExcNo.Items.Clear();
                ddlExcNo.Items.Insert(0, "ExcNo");
                txtBuyerName.Text = "";
                txtShipmentDate.Text = "";

                DataSet ds = objBs.getLedger("1");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBuyerCode.DataSource = ds.Tables[0];
                    ddlBuyerCode.DataTextField = "CompanyCode";
                    ddlBuyerCode.DataValueField = "ledgerId";
                    ddlBuyerCode.DataBind();
                    ddlBuyerCode.Items.Insert(0, "BuyerCode");
                }
            }
        }
        protected void ddlExcNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "0" && ddlExcNo.SelectedValue != "ExcNo")
            {
                DataSet ds = objBs.GetExcNo(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtBuyerName.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                    txtShipmentDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ShipmentDate"]).ToString("dd/MM/yyyy");
                }
            }
            else
            {
                txtBuyerName.Text = "";
                txtShipmentDate.Text = "";
            }
        }
        protected void ddlBuyerCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBuyerCode.SelectedValue != "" && ddlBuyerCode.SelectedValue != "0" && ddlBuyerCode.SelectedValue != "BuyerCode")
            {
                DataSet ds = objBs.GetCustomerDetails(Convert.ToInt32(ddlBuyerCode.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtBuyerName.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                    txtBuyerCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                }
            }
            else
            {
                txtBuyerName.Text = "";
                txtBuyerCity.Text = "";
            }
        }

        protected void txtQty_OnTextChanged(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtQty = (TextBox)row.FindControl("txtQty");

            Calculations();
            txtQty.Focus();
        }
        public void Calculations()
        {
            double TotalAmount = 0;

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");

                if (txtQty.Text == "")
                    txtQty.Text = "0";

                TotalAmount += Convert.ToDouble(hdRate.Value) * Convert.ToDouble(txtQty.Text);
            }

            txtTotalAmount.Text = TotalAmount.ToString();
        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            int Row = 1;
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                DropDownList ddlIssueItemCode = (DropDownList)GVItem.Rows[vLoop].FindControl("ddlIssueItemCode");
                DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[vLoop].FindControl("ddlReceiveItemCode");
                DropDownList ddlProcess = (DropDownList)GVItem.Rows[vLoop].FindControl("ddlProcess");

                TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                TextBox txtQuantity = (TextBox)GVItem.Rows[vLoop].FindControl("txtQuantity");
                TextBox txtAmount = (TextBox)GVItem.Rows[vLoop].FindControl("txtAmount");


                if (txtRate.Text == "")
                    txtRate.Text = "0";
                if (txtQuantity.Text == "")
                    txtQuantity.Text = "0";
                if (txtAmount.Text == "")
                    txtAmount.Text = "0";

                if (ddlIssueItemCode.SelectedValue == "" || ddlIssueItemCode.SelectedValue == "0" || ddlIssueItemCode.SelectedValue == "IssueItem")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select IssueItem in Row " + Row + " ')", true);
                    ddlIssueItemCode.Focus();
                    return;
                }
                if (ddlReceiveItemCode.SelectedValue == "" || ddlReceiveItemCode.SelectedValue == "0" || ddlReceiveItemCode.SelectedValue == "ReceiveItem")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select ReceiveItem in Row " + Row + " ')", true);
                    ddlReceiveItemCode.Focus();
                    return;
                }
                if (ddlProcess.SelectedValue == "" || ddlProcess.SelectedValue == "0" || ddlProcess.SelectedValue == "Process")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Process in Row " + Row + " ')", true);
                    ddlProcess.Focus();
                    return;
                }

                if (Convert.ToDouble(txtQuantity.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Quantity in Row " + Row + " ')", true);
                    txtQuantity.Focus();
                    return;
                }
                if (Convert.ToDouble(txtRate.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Rate in Row " + Row + " ')", true);
                    txtRate.Focus();
                    return;
                }
                if (Convert.ToDouble(txtAmount.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Cost in Row " + Row + " ')", true);
                    txtAmount.Focus();
                    return;
                }
                Row++;
            }
            AddNewRow1();
        }
        private void FirstGridViewRow1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("IssueItem", typeof(string)));
            dt.Columns.Add(new DataColumn("ReceiveItem", typeof(string)));
            dt.Columns.Add(new DataColumn("Process", typeof(string)));

            dt.Columns.Add(new DataColumn("Color", typeof(string)));

            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Extra", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalQuantity", typeof(string)));

            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));

            dr = dt.NewRow();
            dr["IssueItem"] = string.Empty;
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

            dct = new DataColumn("IssueItem");
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
            drNew["IssueItem"] = 0;
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
                        DropDownList ddlIssueItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlIssueItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtAmount");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["IssueItem"] = ddlIssueItemCode.SelectedValue;
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
                        DropDownList ddlIssueItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlIssueItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtAmount");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["IssueItem"] = ddlIssueItemCode.SelectedValue;
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
                        DropDownList ddlIssueItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlIssueItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtAmount");

                        ddlIssueItemCode.SelectedValue = dt.Rows[i]["IssueItem"].ToString();
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


        protected void txtShrink_OnTextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
                txtQty.Text = "0";
            if (txtShrink.Text == "")
                txtShrink.Text = "0";
            if (txtRate.Text == "")
                txtRate.Text = "0";

            double TQ = Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtShrink.Text) / 100;
            txtTotalQty.Text = (Convert.ToDouble(txtQty.Text) + TQ).ToString("f2");

            txtAmount.Text = (Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text)).ToString("f2");

            txtShrink.Focus();
        }
        protected void txtRate_OnTextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
                txtQty.Text = "0";
            if (txtShrink.Text == "")
                txtShrink.Text = "0";
            if (txtRate.Text == "")
                txtRate.Text = "0";

            double TQ = Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtShrink.Text) / 100;
            txtTotalQty.Text = (Convert.ToDouble(txtQty.Text) + TQ).ToString("f2");

            txtAmount.Text = (Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text)).ToString("f2");

            txtRate.Focus();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                return;
            }

            if (txtTotalAmount.Text == "")
                txtTotalAmount.Text = "0";
            if (Convert.ToDouble(txtTotalAmount.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                return;
            }

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                #region

                double IssuedQty = 0; double TotalQty = 0;

                HiddenField hdIssueItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssueItemId");
                HiddenField hdIssColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssColorId");

                HiddenField hdIssueItem = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssueItem");
                HiddenField hdIssColor = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssColor");

                for (int vLoop1 = 0; vLoop1 < GVItem.Rows.Count; vLoop1++)
                {
                    #region

                    HiddenField hdIssueItemId1 = (HiddenField)GVItem.Rows[vLoop1].FindControl("hdIssueItemId");
                    HiddenField hdIssColorId1 = (HiddenField)GVItem.Rows[vLoop1].FindControl("hdIssColorId");

                    HiddenField hdIssuedQty = (HiddenField)GVItem.Rows[vLoop1].FindControl("hdIssuedQty");

                    TextBox txtQty = (TextBox)GVItem.Rows[vLoop1].FindControl("txtQty");
                    if (txtQty.Text == "")
                        txtQty.Text = "0";

                    if ((hdIssueItemId.Value == hdIssueItemId1.Value) && (hdIssColorId.Value == hdIssColorId1.Value))
                    {
                        IssuedQty += Convert.ToDouble(hdIssuedQty.Value);
                        TotalQty += Convert.ToDouble(txtQty.Text);
                    }

                    #endregion
                }

                if (btnSave.Text == "Save")
                {
                    DataSet dsIPOStock = objBs.CheckItemProcessOrderStock(Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdIssColorId.Value), TotalQty);
                    if (dsIPOStock.Tables[0].Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Stock in " + hdIssueItem.Value + ' ' + hdIssColor.Value + ".')", true);
                        return;
                    }
                }
                else
                {
                    if (TotalQty > IssuedQty)
                    {
                        double Qty = TotalQty - IssuedQty;

                        DataSet dsIPOStock = objBs.CheckItemProcessOrderStock(Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdIssColorId.Value), Qty);
                        if (dsIPOStock.Tables[0].Rows.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Stock in " + hdIssueItem.Value + ' ' + hdIssColor.Value + ".')", true);
                            return;
                        }
                    }
                }

                #endregion
            }


            #region

            //for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            //{


            //    HiddenField hdIssueItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssueItemId");
            //    HiddenField hdIssColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssColorId");

            //    TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
            //    if (txtQty.Text == "")
            //        txtQty.Text = "0";
            //    HiddenField hdAvlQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAvlQty");
            //    HiddenField hdBalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdBalQty");

            //    if (Convert.ToDouble(hdBalQty.Value) < Convert.ToDouble(txtQty.Text))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Iss.Qty in Row " + (vLoop + 1) + ".')", true);
            //        txtQty.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        if (btnSave.Text == "Save")
            //        {
            //            DataSet dsIPOStock = objBs.CheckItemProcessOrderStock(Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdIssColorId.Value), Convert.ToDouble(txtQty.Text));
            //            if (dsIPOStock.Tables[0].Rows.Count == 0)
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Stock in Row " + (vLoop + 1) + ".')", true);
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            double Qty = 0;
            //            HiddenField hdIssuedQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssuedQty");
            //            if (Convert.ToDouble(txtQty.Text) > Convert.ToDouble(hdIssuedQty.Value))
            //            {
            //                Qty = Convert.ToDouble(txtQty.Text) - Convert.ToDouble(hdIssuedQty.Value);

            //                DataSet dsIPOStock = objBs.CheckItemProcessOrderStock(Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdIssColorId.Value), Qty);
            //                if (dsIPOStock.Tables[0].Rows.Count == 0)
            //                {
            //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Stock in Row " + (vLoop + 1) + ".')", true);
            //                    return;
            //                }
            //            }
            //        }
            //    }

            //}

            #endregion

            DateTime OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DeliveryFrom = DateTime.ParseExact(txtDeliveryFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DeliveryTo = DateTime.ParseExact(txtDeliveryTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnSave.Text == "Save")
            {
                #region
                DataSet dsPONo = objBs.GettemProcessOrderPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["PONo"].ToString().PadLeft(4, '0');
                txtProOrderNo.Text = "IPC - " + PONo + " / " + YearCode;

                DataSet dsPoNo = objBs.CheckItemProcessOrderPONo(dsPONo.Tables[0].Rows[0]["PONo"].ToString(), 0, YearCode);
                if (dsPoNo.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Pro.Ord. No. was already Exists.')", true);
                    txtProOrderNo.Focus();
                    return;
                }
                else
                {
                    int ItemPOId = objBs.InsertItemProcessOrder(Convert.ToInt32(ddlProcessOn.SelectedValue), OrderDate, DeliveryFrom, DeliveryTo, txtDeliveryPlace.Text, dsPONo.Tables[0].Rows[0]["PONo"].ToString(), YearCode, txtProOrderNo.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToDouble(txtTotalAmount.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlProOrdNo.SelectedValue));

                    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                    {
                        HiddenField hdTransId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransId");

                        HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");
                        HiddenField hdPurchaseForTypeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
                        HiddenField hdIssueItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssueItemId");
                        HiddenField hdReceiveItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdReceiveItemId");
                        HiddenField hdIssColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssColorId");
                        HiddenField hdRecColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRecColorId");
                        HiddenField hdProcessId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdProcessId");


                        HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                        HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                        HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                        HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                        HiddenField hdRemarks = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRemarks");
                        HiddenField hdIsRequest = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsRequest");
                        HiddenField hdIsReceive = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsReceive");

                        TextBox hdQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
                        if (hdQty.Text == "")
                            hdQty.Text = "0";

                        if (Convert.ToDouble(hdQty.Text) > 0)
                        {
                            int TransSamplingCostingId = objBs.InsertTransItemProcessOrder(ItemPOId, Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdReceiveItemId.Value), Convert.ToInt32(hdIssColorId.Value), Convert.ToInt32(hdRecColorId.Value), Convert.ToInt32(hdProcessId.Value), Convert.ToDouble(hdQty.Text), Convert.ToDouble(hdShrink.Value), 0, Convert.ToDouble(hdRate.Value), (Convert.ToDouble(hdQty.Text) * Convert.ToDouble(hdRate.Value)), hdRemarks.Value, hdIsRequest.Value, hdIsReceive.Value, Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(hdTransId.Value));
                        }

                    }


                }
                #endregion
            }
            else
            {
                string ItemPOId = Request.QueryString.Get("ItemPOId");
                if (ItemPOId != "" && ItemPOId != null)
                {
                    #region

                    int UpdateItemPOId = objBs.UpdateItemProcessOrder(OrderDate, txtDeliveryPlace.Text, Convert.ToDouble(txtTotalAmount.Text), Convert.ToInt32(ItemPOId));

                    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                    {
                        HiddenField hdTransId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransId");

                        HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");
                        HiddenField hdPurchaseForTypeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
                        HiddenField hdIssueItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssueItemId");
                        HiddenField hdReceiveItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdReceiveItemId");
                        HiddenField hdIssColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssColorId");
                        HiddenField hdRecColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRecColorId");
                        HiddenField hdProcessId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdProcessId");


                        HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                        HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                        HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                        HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                        HiddenField hdRemarks = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRemarks");
                        HiddenField hdIsRequest = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsRequest");
                        HiddenField hdIsReceive = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsReceive");

                        TextBox hdQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
                        if (hdQty.Text == "")
                            hdQty.Text = "0";

                        if (Convert.ToDouble(hdQty.Text) > 0)
                        {
                            int TransSamplingCostingId = objBs.InsertTransItemProcessOrder(Convert.ToInt32(ItemPOId), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdReceiveItemId.Value), Convert.ToInt32(hdIssColorId.Value), Convert.ToInt32(hdRecColorId.Value), Convert.ToInt32(hdProcessId.Value), Convert.ToDouble(hdQty.Text), Convert.ToDouble(hdShrink.Value), 0, Convert.ToDouble(hdRate.Value), (Convert.ToDouble(hdQty.Text) * Convert.ToDouble(hdRate.Value)), hdRemarks.Value, hdIsRequest.Value, hdIsReceive.Value, Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(hdTransId.Value));
                        }

                    }

                    #endregion
                }
            }

            Response.Redirect("ItemProcessOrderGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ItemProcessOrderGrid.aspx");
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

            if (Convert.ToDouble(txtAmount.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Amount.')", true);
                return;
            }

            DataSet dstd = new DataSet();
            DataTable dtddd = new DataTable();
            DataRow drNew;
            DataColumn dct;
            DataTable dttt = new DataTable();

            #region

            dct = new DataColumn("PurchaseFor");
            dttt.Columns.Add(dct);
            dct = new DataColumn("PurchaseForId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("PurchaseForType");
            dttt.Columns.Add(dct);
            dct = new DataColumn("PurchaseForTypeId");
            dttt.Columns.Add(dct);

            dct = new DataColumn("IssueItem");
            dttt.Columns.Add(dct);
            dct = new DataColumn("IssueItemId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ReceiveItem");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ReceiveItemID");
            dttt.Columns.Add(dct);


            dct = new DataColumn("IssColor");
            dttt.Columns.Add(dct);
            dct = new DataColumn("IssColorId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("RecColor");
            dttt.Columns.Add(dct);
            dct = new DataColumn("RecColorId");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Process");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ProcessId");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Qty");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Shrink");
            dttt.Columns.Add(dct);
            dct = new DataColumn("TotalQty");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Amount");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Remarks");
            dttt.Columns.Add(dct);
            dct = new DataColumn("IsRequest");
            dttt.Columns.Add(dct);
            dct = new DataColumn("IsReceive");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            string PurchaseForType = "";
            int PurchaseForTypeId = 0;

            if (ddlPurchaseFor.SelectedValue == "1")
            {
                PurchaseForType = ddlExcNo.SelectedItem.Text;
                PurchaseForTypeId = Convert.ToInt32(ddlExcNo.SelectedValue);
            }
            else
            {
                PurchaseForType = ddlBuyerCode.SelectedItem.Text;
                PurchaseForTypeId = Convert.ToInt32(ddlBuyerCode.SelectedValue);
            }

            string[] IssueItems = ddlIssueItems.SelectedValue.Split('$');
            string[] ReceiveItems = ddlReceiveItems.SelectedValue.Split('$');

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                drNew = dttt.NewRow();

                drNew["PurchaseFor"] = ddlPurchaseFor.SelectedItem.Text;
                drNew["PurchaseForID"] = ddlPurchaseFor.SelectedValue;
                drNew["PurchaseForType"] = PurchaseForType;
                drNew["PurchaseForTypeId"] = PurchaseForTypeId;

                drNew["IssueItem"] = ddlIssueItems.SelectedItem.Text;
                drNew["IssueItemId"] = IssueItems[0];
                drNew["ReceiveItem"] = ddlReceiveItems.SelectedItem.Text;
                drNew["ReceiveItemID"] = ReceiveItems[0];

                drNew["IssColor"] = ddlIssueColor.SelectedItem.Text;
                drNew["IssColorId"] = ddlIssueColor.SelectedValue;
                drNew["RecColor"] = ddlReceiveColor.SelectedItem.Text;
                drNew["RecColorId"] = ddlReceiveColor.SelectedValue;

                drNew["Process"] = ddlProcess.SelectedItem.Text;
                drNew["ProcessId"] = ddlProcess.SelectedValue;

                drNew["Qty"] = txtQty.Text;
                drNew["Shrink"] = txtShrink.Text;
                drNew["TotalQty"] = txtTotalQty.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Amount"] = txtAmount.Text;

                drNew["Remarks"] = txtRemarks.Text;
                drNew["IsRequest"] = chkReq.Checked;
                drNew["IsReceive"] = chkRec.Checked;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

            }
            else
            {

                drNew = dttt.NewRow();

                drNew["PurchaseFor"] = ddlPurchaseFor.SelectedItem.Text;
                drNew["PurchaseForID"] = ddlPurchaseFor.SelectedValue;
                drNew["PurchaseForType"] = PurchaseForType;
                drNew["PurchaseForTypeId"] = PurchaseForTypeId;

                drNew["IssueItem"] = ddlIssueItems.SelectedItem.Text;
                drNew["IssueItemId"] = IssueItems[0];
                drNew["ReceiveItem"] = ddlReceiveItems.SelectedItem.Text;
                drNew["ReceiveItemID"] = ReceiveItems[0];

                drNew["Process"] = ddlProcess.SelectedItem.Text;
                drNew["ProcessId"] = ddlProcess.SelectedValue;
                drNew["IssColor"] = ddlIssueColor.SelectedItem.Text;
                drNew["IssColorId"] = ddlIssueColor.SelectedValue;
                drNew["RecColor"] = ddlReceiveColor.SelectedItem.Text;
                drNew["RecColorId"] = ddlReceiveColor.SelectedValue;

                drNew["Qty"] = txtQty.Text;
                drNew["Shrink"] = txtShrink.Text;
                drNew["TotalQty"] = txtTotalQty.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Amount"] = txtAmount.Text;

                drNew["Remarks"] = txtRemarks.Text;
                drNew["IsRequest"] = chkReq.Checked;
                drNew["IsReceive"] = chkRec.Checked;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
            }

            #endregion

            ViewState["CurrentTable1"] = dtddd;
            GVItem.DataSource = dtddd;
            GVItem.DataBind();

            Calculations();
            //for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            //{
            //    TextBox txtAmount = (TextBox)GVItem.Rows[vLoop].FindControl("txtAmount");

            //    ddlItemCode.SelectedValue = dstd.Tables[0].Rows[vLoop]["ItemCode"].ToString();

            //}
        }

        protected void ddlIssueItems_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueItems.SelectedValue != "" && ddlIssueItems.SelectedValue != "0" && ddlIssueItems.SelectedValue != "Select Issue Item")
            {
                string[] IssueItems = ddlIssueItems.SelectedValue.Split('$');

                lblIssue1.Text = "Type: " + IssueItems[1].ToString() + "/" + " Size: " + IssueItems[3].ToString() + " " + IssueItems[4].ToString();
                lblIssue2.Text = "Categories:" + IssueItems[2].ToString();
            }
            else
            {
                lblIssue1.Text = "";
                lblIssue2.Text = "";
            }
        }
        protected void ddlReceiveItems_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReceiveItems.SelectedValue != "" && ddlReceiveItems.SelectedValue != "0" && ddlReceiveItems.SelectedValue != "Select Receive Item")
            {
                string[] ReceiveItems = ddlReceiveItems.SelectedValue.Split('$');

                lblRec1.Text = "Type: " + ReceiveItems[1].ToString() + "/" + " Size: " + ReceiveItems[3].ToString() + " " + ReceiveItems[4].ToString();
                lblRec2.Text = "Categories:" + ReceiveItems[2].ToString();
            }
            else
            {
                lblRec1.Text = "";
                lblRec2.Text = "";
            }
        }
    }
}
