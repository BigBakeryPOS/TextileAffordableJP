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
    public partial class ItemProcessReceive : System.Web.UI.Page
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

                DataSet dsPONo = objBs.GettemProcessReceivePONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["RecPONo"].ToString().PadLeft(4, '0');
                txtProRecNo.Text = "IPR - " + PONo + " / " + YearCode;

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                ddlPartyCode.Items.Insert(0, "PartyCode");
                ddlPartyName.Items.Insert(0, "PartyName");
                ddlPoNo.Items.Insert(0, "IPO Challan No");

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

                string ItemPORecId = Request.QueryString.Get("ItemPORecId");
                if (ItemPORecId != "" && ItemPORecId != null)
                {
                    DataSet dsRecPO = objBs.getItemProcessOrderReceive(Convert.ToInt32(ItemPORecId));
                    if (dsRecPO.Tables[0].Rows.Count > 0)
                    {
                        #region


                        ddlProcessOn.SelectedValue = dsRecPO.Tables[0].Rows[0]["ProcessOn"].ToString();
                        ddlProcessOn.Enabled = false;
                        ddlProcessOn_OnSelectedIndexChanged(sender, e);

                        ddlPartyCode.SelectedValue = dsRecPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyName.SelectedValue = dsRecPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyCode.Enabled = false;

                        DataSet dsParty = objBs.GetLedgerDetails(Convert.ToInt32(ddlPartyCode.SelectedValue));
                        if (dsParty.Tables[0].Rows.Count > 0)
                        {
                            txtContPerson.Text = dsParty.Tables[0].Rows[0]["ContacrPerson"].ToString();
                            txtAddress.Text = dsParty.Tables[0].Rows[0]["Address"].ToString();
                            txtPhone.Text = dsParty.Tables[0].Rows[0]["PhoneNo"].ToString();
                            txtCity.Text = dsParty.Tables[0].Rows[0]["City"].ToString();
                        }

                        DataSet dsProOrdNo = objBs.GetItemProcessOrder2(ddlPartyCode.SelectedValue);
                        if (dsProOrdNo.Tables[0].Rows.Count > 0)
                        {
                            ddlPoNo.DataSource = dsProOrdNo.Tables[0];
                            ddlPoNo.DataTextField = "FullPONo";
                            ddlPoNo.DataValueField = "ItemPOId";
                            ddlPoNo.DataBind();
                            ddlPoNo.Items.Insert(0, "IPO Challan No");
                        }
                        else
                        {
                            ddlPoNo.Items.Insert(0, "IPO Challan No");
                        }

                        ddlPoNo.SelectedValue = dsRecPO.Tables[0].Rows[0]["POId"].ToString();
                        ddlPoNo.Enabled = false;

                        txtRecDate.Text = Convert.ToDateTime(dsRecPO.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");

                        DataSet dsPO = objBs.getItemProcessOrder(Convert.ToInt32(dsRecPO.Tables[0].Rows[0]["POId"].ToString()));
                        if (dsPO.Tables[0].Rows.Count > 0)
                        {
                            txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                            txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                            txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");
                        }

                        txtDeliveryPlace.Text = dsRecPO.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        txtProRecNo.Text = dsRecPO.Tables[0].Rows[0]["FullRecPONo"].ToString();


                        ddlCompany.SelectedValue = dsRecPO.Tables[0].Rows[0]["CompanyId"].ToString();
                        ddlCompany.Enabled = false;

                        txtTotalAmount.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");

                        #endregion
                    }

                    DataSet ds2 = objBs.getTransItemProcessOrderReceive(Convert.ToInt32(ItemPORecId));
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

                        dct = new DataColumn("Remarks");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("BalQty");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("PossibleBalQty");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("RecQty");
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

                            drNew["Qty"] = Convert.ToDouble(Dr["IssuedQty"]).ToString("f2");
                            drNew["Shrink"] = Convert.ToDouble(Dr["Shrink"]).ToString("f2");
                            drNew["TotalQty"] = Convert.ToDouble(Dr["TotalQty"]).ToString("f2");
                            drNew["Rate"] = Convert.ToDouble(Dr["Rate"]).ToString("f2");
                            drNew["Amount"] = Convert.ToDouble(Dr["Amount"]).ToString("f2");

                            drNew["Remarks"] = Dr["Remarks"];

                            double BalQty1 = ((Convert.ToDouble(Dr["IssuedQty"]) - Convert.ToDouble(Dr["RecQty"])) + (Convert.ToDouble(Dr["Qty"])));
                            double BalQty = ((Convert.ToDouble(Dr["IssuedQty"]) * Convert.ToDouble(Dr["Shrink"])) / 100);

                            drNew["BalQty"] = BalQty1.ToString("f2");

                            drNew["PossibleBalQty"] = (((Convert.ToDouble(Dr["IssuedQty"]) - BalQty) - Convert.ToDouble(Dr["RecQty"])) + Convert.ToDouble(Dr["Qty"])).ToString("f2");

                            drNew["RecQty"] = Convert.ToDouble(Dr["Qty"]).ToString("f2");

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();


                    }

                    btnSave.Text = "Update";

                }
            }

        }

        protected void ddlProcessOn_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPartyCode.Items.Clear();
            ddlPartyName.Items.Clear();

            ddlPoNo.Items.Clear();
            ddlPoNo.Items.Insert(0, "IPO Challan No");

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
            ddlPoNo.Items.Clear();
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

                DataSet dsProOrdNo = objBs.GetItemProcessOrder1(ddlPartyCode.SelectedValue);
                if (dsProOrdNo.Tables[0].Rows.Count > 0)
                {
                    ddlPoNo.DataSource = dsProOrdNo.Tables[0];
                    ddlPoNo.DataTextField = "FullPONo";
                    ddlPoNo.DataValueField = "ItemPOId";
                    ddlPoNo.DataBind();
                    ddlPoNo.Items.Insert(0, "IPO Challan No");
                }
                else
                {
                    ddlPoNo.Items.Insert(0, "IPO Challan No");
                }

            }
            else
            {
                ddlPoNo.Items.Insert(0, "IPO Challan No");

                ddlPartyName.ClearSelection();

                txtContPerson.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
                txtCity.Text = "";
            }



        }
        protected void ddlPoNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPoNo.SelectedValue != "" && ddlPoNo.SelectedValue != "" && ddlPoNo.SelectedValue != "IPO Challan No")
            {
                DataSet dsPO = objBs.getItemProcessOrder(Convert.ToInt32(ddlPoNo.SelectedValue));
                if (dsPO.Tables[0].Rows.Count > 0)
                {
                    #region

                    ddlCompany.SelectedValue = dsPO.Tables[0].Rows[0]["CompanyId"].ToString();
                    ddlCompany.Enabled = false;

                    txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                    #endregion
                }

                DataSet ds2 = objBs.getTransItemProcessOrderforReceive(Convert.ToInt32(ddlPoNo.SelectedValue));
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

                    dct = new DataColumn("Remarks");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("IsRequest");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("IsReceive");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("BalQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("PossibleBalQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("RecQty");
                    dttt.Columns.Add(dct);

                    dstd.Tables.Add(dttt);

                    foreach (DataRow Dr in ds2.Tables[0].Rows)
                    {
                        double Shrink = (Convert.ToDouble(Dr["Qty"]) * Convert.ToDouble(Dr["Shrink"])) / 100;

                        if ((Convert.ToDouble(Dr["Qty"]) - Shrink) - Convert.ToDouble(Dr["RecQty"]) > 0)
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

                            drNew["BalQty"] = Convert.ToDouble(Convert.ToDouble(Dr["Qty"]) - Convert.ToDouble(Dr["RecQty"])).ToString("f2");

                            drNew["PossibleBalQty"] = ((Convert.ToDouble(Dr["Qty"]) - Shrink) - Convert.ToDouble(Dr["RecQty"])).ToString("f2");
                            drNew["RecQty"] = ((Convert.ToDouble(Dr["Qty"]) - Shrink) - Convert.ToDouble(Dr["RecQty"])).ToString("f2");

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

            //for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            //{
            //    HiddenField hdPossibleBalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPossibleBalQty");
            //    TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");

            //    if (txtQty.Text == "")
            //        txtQty.Text = "0";

            //    if (Convert.ToDouble(hdPossibleBalQty.Value) < Convert.ToDouble(txtQty.Text))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Receive Qty in Row " + (vLoop + 1) + ".')", true);
            //        txtQty.Focus();
            //        return;
            //    }
            //}

            DateTime RecDate = DateTime.ParseExact(txtRecDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnSave.Text == "Save")
            {
                #region
                DataSet dsPONo = objBs.GettemProcessReceivePONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["RecPONo"].ToString().PadLeft(4, '0');
                txtProRecNo.Text = "IPR - " + PONo + " / " + YearCode;

                DataSet dsPoNo = objBs.CheckItemProcessReceivePONo(dsPONo.Tables[0].Rows[0]["RecPONo"].ToString(), 0, YearCode);
                if (dsPoNo.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Pro.Rec. No. was already Exists.')", true);
                    txtProRecNo.Focus();
                    return;
                }
                else
                {
                    int ItemPORecId = objBs.InsertItemProcessReceive(Convert.ToInt32(ddlPoNo.SelectedValue), Convert.ToInt32(ddlProcessOn.SelectedValue), RecDate, txtDeliveryPlace.Text, dsPONo.Tables[0].Rows[0]["RecPONo"].ToString(), YearCode, txtProRecNo.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToDouble(txtTotalAmount.Text));

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

                        HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                        HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                        HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                        HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                        HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                        TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
                        TextBox txtRemarks = (TextBox)GVItem.Rows[vLoop].FindControl("txtRemarks");

                        if (txtQty.Text == "")
                            txtQty.Text = "0";

                        if (Convert.ToDouble(txtQty.Text) > 0)
                        {
                            int TransSamplingCostingId = objBs.InsertTransItemProcessReceive(ItemPORecId, Convert.ToInt32(hdTransId.Value), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdReceiveItemId.Value), Convert.ToInt32(hdIssColorId.Value), Convert.ToInt32(hdRecColorId.Value), Convert.ToInt32(hdProcessId.Value), Convert.ToDouble(txtQty.Text), Convert.ToDouble(hdShrink.Value), 0, Convert.ToDouble(hdRate.Value), (Convert.ToDouble(txtQty.Text) * Convert.ToDouble(hdRate.Value)), txtRemarks.Text, Convert.ToInt32(ddlCompany.SelectedValue));
                        }
                    }
                }

                #endregion
            }
            else
            {
                string ItemPORecId = Request.QueryString.Get("ItemPORecId");
                if (ItemPORecId != "" && ItemPORecId != null)
                {
                    #region

                    int UpdateItemPORecId = objBs.UpdateItemProcessReceive(RecDate, txtDeliveryPlace.Text, Convert.ToDouble(txtTotalAmount.Text), Convert.ToInt32(ItemPORecId));

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

                        HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                        HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                        HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                        HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                        HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                        TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
                        TextBox txtRemarks = (TextBox)GVItem.Rows[vLoop].FindControl("txtRemarks");

                        if (txtQty.Text == "")
                            txtQty.Text = "0";

                        if (Convert.ToDouble(txtQty.Text) > 0)
                        {
                            int TransSamplingCostingId = objBs.InsertTransItemProcessReceive(Convert.ToInt32(ItemPORecId), Convert.ToInt32(hdTransId.Value), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdReceiveItemId.Value), Convert.ToInt32(hdIssColorId.Value), Convert.ToInt32(hdRecColorId.Value), Convert.ToInt32(hdProcessId.Value), Convert.ToDouble(txtQty.Text), Convert.ToDouble(hdShrink.Value), 0, Convert.ToDouble(hdRate.Value), (Convert.ToDouble(txtQty.Text) * Convert.ToDouble(hdRate.Value)), txtRemarks.Text, Convert.ToInt32(ddlCompany.SelectedValue));
                        }
                    }
                    #endregion
                }
            }

            Response.Redirect("ItemProcessReceiveGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ItemProcessReceiveGrid.aspx");
        }

    }
}
