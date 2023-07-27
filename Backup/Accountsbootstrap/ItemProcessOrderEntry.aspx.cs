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
    public partial class ItemProcessOrderEntry : System.Web.UI.Page
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

                DataSet dsPONo = objBs.GettemProcessOrderEntryPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["PONo"].ToString().PadLeft(4, '0');
                txtProOrderNo.Text = "IPO - " + PONo + " / " + YearCode;

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

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

                ddlPurchaseFor_OnSelectedIndexChanged(sender, e);



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

                string ItemEntryId = Request.QueryString.Get("ItemEntryId");
                if (ItemEntryId != "" && ItemEntryId != null)
                {
                    DataSet dsPO = objBs.getItemProcessOrderEntry(Convert.ToInt32(ItemEntryId));
                    if (dsPO.Tables[0].Rows.Count > 0)
                    {
                        #region

                        ddlProcessOn.SelectedValue = dsPO.Tables[0].Rows[0]["ProcessOn"].ToString();
                        ddlProcessOn.Enabled = false;
                        ddlProcessOn_OnSelectedIndexChanged(sender, e);

                        ddlPartyCode.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyCode.Enabled = false;
                        ddlPartyName.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyCode_OnSelectedIndexChanged(sender, e);

                        ddlCompany.SelectedValue = dsPO.Tables[0].Rows[0]["CompanyId"].ToString();
                        ddlCompany.Enabled = false;

                        txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                        txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                        txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                        txtDeliveryPlace.Text = dsPO.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        txtProOrderNo.Text = dsPO.Tables[0].Rows[0]["FullPONo"].ToString();

                        txtTotalAmount.Text = Convert.ToDouble(dsPO.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");

                        #endregion
                    }

                    DataSet ds2 = objBs.getTransItemProcessOrderEntry(Convert.ToInt32(ItemEntryId));
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

                            drNew["Qty"] = Convert.ToDouble(Dr["Qty"]).ToString("f2");
                            drNew["Shrink"] = Convert.ToDouble(Dr["Shrink"]).ToString("f2");
                            drNew["TotalQty"] = Convert.ToDouble(Dr["TotalQty"]).ToString("f2");
                            drNew["Rate"] = Convert.ToDouble(Dr["Rate"]).ToString("f2");
                            drNew["Amount"] = Convert.ToDouble(Dr["Amount"]).ToString("f2");

                            drNew["Remarks"] = Dr["Remarks"];
                            drNew["IsRequest"] = Dr["Request"];
                            drNew["IsReceive"] = Dr["Receive"];

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();


                    }

                    btnSave.Text = "Update";

                    DataSet ds3 = objBs.getItemProcessOrderEntryEditCheck(Convert.ToInt32(ItemEntryId));
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        hdIsupdate.Value = "No";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ItemProcess Order Challan was Created,you can update Rate only.')", true);
                    }

                }
            }

        }

        protected void ddlProcessOn_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPartyCode.Items.Clear();
            ddlPartyName.Items.Clear();
            if (ddlProcessOn.SelectedValue != "" && ddlProcessOn.SelectedValue != "0" && ddlProcessOn.SelectedValue != "ProcessOn")
            {
                DataSet dsItemforItemProcess = objBs.GetItemforItemProcess(Convert.ToInt32(ddlProcessOn.SelectedValue));
                if (dsItemforItemProcess.Tables[0].Rows.Count > 0)
                {
                    ddlIssueItems.DataSource = dsItemforItemProcess.Tables[0];
                    ddlIssueItems.DataTextField = "Description";
                    ddlIssueItems.DataValueField = "FullDetailsId";
                    ddlIssueItems.DataBind();
                    ddlIssueItems.Items.Insert(0, "Select Issue Item");

                    ddlReceiveItems.DataSource = dsItemforItemProcess.Tables[0];
                    ddlReceiveItems.DataTextField = "Description";
                    ddlReceiveItems.DataValueField = "FullDetailsId";
                    ddlReceiveItems.DataBind();
                    ddlReceiveItems.Items.Insert(0, "Select Receive Item");

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
                    ddlIssueItems.Items.Clear();
                    ddlIssueItems.Items.Insert(0, "Select Issue Item");

                    ddlReceiveItems.Items.Clear();
                    ddlReceiveItems.Items.Insert(0, "Select Receive Item");
                }
            }
            else
            {
                ddlIssueItems.Items.Clear();
                ddlIssueItems.Items.Insert(0, "Select Issue Item");

                ddlReceiveItems.Items.Clear();
                ddlReceiveItems.Items.Insert(0, "Select Receive Item");
            }
            GVItem.DataSource = null;
            GVItem.DataBind();
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

        protected void GVItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (btnSubmit.Text == "Update")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Submit Records.')", true);
                return;
            }

            if (e.CommandName == "Edit")
            {
                #region

                int RowIndex = Convert.ToInt32(e.CommandArgument);
                hdRowIndex.Value = RowIndex.ToString();

                GridViewRow row = GVItem.Rows[RowIndex];
                TransId.Value = (row.FindControl("hdTransId") as HiddenField).Value;

                ddlPurchaseFor.SelectedValue = (row.FindControl("hdPurchaseForId") as HiddenField).Value;
                if (ddlPurchaseFor.SelectedValue == "1")
                {
                    #region
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

                    #endregion
                    ddlExcNo.SelectedValue = (row.FindControl("hdPurchaseForTypeId") as HiddenField).Value;
                }
                else
                {
                    #region

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

                    #endregion
                    ddlBuyerCode.SelectedValue = (row.FindControl("hdPurchaseForTypeId") as HiddenField).Value;
                }

                DataSet dsItemforItemProcess = objBs.GetItemforItemProcess(Convert.ToInt32(ddlProcessOn.SelectedValue));
                if (dsItemforItemProcess.Tables[0].Rows.Count > 0)
                {
                    ddlIssueItems.DataSource = dsItemforItemProcess.Tables[0];
                    ddlIssueItems.DataTextField = "Description";
                    ddlIssueItems.DataValueField = "FullDetailsId";
                    ddlIssueItems.DataBind();
                    ddlIssueItems.Items.Insert(0, "Select Issue Item");

                    ddlReceiveItems.DataSource = dsItemforItemProcess.Tables[0];
                    ddlReceiveItems.DataTextField = "Description";
                    ddlReceiveItems.DataValueField = "FullDetailsId";
                    ddlReceiveItems.DataBind();
                    ddlReceiveItems.Items.Insert(0, "Select Receive Item");
                }
                else
                {
                    ddlIssueItems.Items.Clear();
                    ddlIssueItems.Items.Insert(0, "Select Issue Item");

                    ddlReceiveItems.Items.Clear();
                    ddlReceiveItems.Items.Insert(0, "Select Issue Item");
                }


                string hdIssueItemId = (row.FindControl("hdIssueItemId") as HiddenField).Value;
                DataSet dsIssueItemId = objBs.GetitemProcess_Update(Convert.ToInt32(hdIssueItemId));
                if (dsIssueItemId.Tables[0].Rows.Count > 0)
                {
                    ddlIssueItems.SelectedValue = dsIssueItemId.Tables[0].Rows[0]["FullDetailsId"].ToString();
                    ddlIssueItems_OnSelectedIndexChanged(sender, e);
                }
                string hdReceiveItemId = (row.FindControl("hdReceiveItemId") as HiddenField).Value;
                DataSet dsReceiveItemId = objBs.GetitemProcess_Update(Convert.ToInt32(hdReceiveItemId));
                if (dsReceiveItemId.Tables[0].Rows.Count > 0)
                {
                    ddlReceiveItems.SelectedValue = dsReceiveItemId.Tables[0].Rows[0]["FullDetailsId"].ToString();
                    ddlReceiveItems_OnSelectedIndexChanged(sender, e);
                }

                ddlIssueColor.SelectedValue = (row.FindControl("hdIssColorId") as HiddenField).Value;
                ddlReceiveColor.SelectedValue = (row.FindControl("hdRecColorId") as HiddenField).Value;
                ddlProcess.SelectedValue = (row.FindControl("hdProcessId") as HiddenField).Value;

                txtQty.Text = (row.FindControl("hdQty") as HiddenField).Value;
                txtShrink.Text = (row.FindControl("hdShrink") as HiddenField).Value;
                txtTotalQty.Text = (row.FindControl("hdTotalQty") as HiddenField).Value;
                txtRate.Text = (row.FindControl("hdRate") as HiddenField).Value;
                txtAmount.Text = (row.FindControl("hdAmount") as HiddenField).Value;
                txtRemarks.Text = (row.FindControl("hdRemarks") as HiddenField).Value;

                if (hdIsupdate.Value == "No")
                {
                    ddlPurchaseFor.Enabled = false;
                    ddlBuyerCode.Enabled = false;

                    ddlIssueItems.Enabled = false;
                    ddlIssueColor.Enabled = false;
                    ddlReceiveItems.Enabled = false;
                    ddlReceiveColor.Enabled = false;

                    ddlProcess.Enabled = false;

                    txtQty.Enabled = false;
                    txtShrink.Enabled = false;
                    txtTotalQty.Enabled = false;
                    txtAmount.Enabled = false;
                    txtRemarks.Enabled = false;

                    btnSubmit.Text = "Update";
                }

                #region
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable1"];
                    DataRow drCurrentRow = null;
                    int rowIndex = Convert.ToInt32(hdRowIndex.Value);
                    if (dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        drCurrentRow = dt.NewRow();
                        ViewState["CurrentTable1"] = dt;
                        GVItem.DataSource = dt;
                        GVItem.DataBind();
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        drCurrentRow = dt.NewRow();
                        ViewState["CurrentTable1"] = dt;
                        GVItem.DataSource = dt;
                        GVItem.DataBind();
                    }
                }
                #endregion

                #endregion

                Calculations();
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
        protected void GVItem_OnRowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void txtQty_OnTextChanged(object sender, EventArgs e)
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


            txtQty.Focus();
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

        public void Calculations()
        {
            double TotalAmount = 0;

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");
                TotalAmount += Convert.ToDouble(hdAmount.Value);
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


            if (btnSubmit.Text == "Update")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Submit Records.')", true);
                return;
            }

            DateTime OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DeliveryFrom = DateTime.ParseExact(txtDeliveryFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DeliveryTo = DateTime.ParseExact(txtDeliveryTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            if (btnSave.Text == "Save")
            {
                #region
                DataSet dsPONo = objBs.GettemProcessOrderEntryPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["PONo"].ToString().PadLeft(4, '0');
                txtProOrderNo.Text = "IPO - " + PONo + " / " + YearCode;

                DataSet dsPoNo = objBs.CheckItemProcessOrderEntryPONo(dsPONo.Tables[0].Rows[0]["PONo"].ToString(), 0,YearCode);
                if (dsPoNo.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Pro.Ord. No. was already Exists.')", true);
                    txtProOrderNo.Focus();
                    return;
                }
                else
                {
                    int ItemPOId = objBs.InsertItemProcessOrderEntry(Convert.ToInt32(ddlProcessOn.SelectedValue), OrderDate, DeliveryFrom, DeliveryTo, txtDeliveryPlace.Text, dsPONo.Tables[0].Rows[0]["PONo"].ToString(), YearCode, txtProOrderNo.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToDouble(txtTotalAmount.Text), Convert.ToInt32(ddlCompany.SelectedValue));

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

                        HiddenField hdRemarks = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRemarks");
                        HiddenField hdIsRequest = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsRequest");
                        HiddenField hdIsReceive = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsReceive");

                        int TransSamplingCostingId = objBs.InsertTransItemProcessOrderEntry(ItemPOId, Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdReceiveItemId.Value), Convert.ToInt32(hdIssColorId.Value), Convert.ToInt32(hdRecColorId.Value), Convert.ToInt32(hdProcessId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(hdRate.Value), Convert.ToDouble(hdAmount.Value), hdRemarks.Value, hdIsRequest.Value, hdIsReceive.Value, Convert.ToInt32(ddlCompany.SelectedValue), hdIsupdate.Value, Convert.ToInt32(hdTransId.Value));


                    }


                }
                #endregion
            }
            else
            {
                string ItemEntryId = Request.QueryString.Get("ItemEntryId");
                if (ItemEntryId != "" && ItemEntryId != null)
                {
                    int UpdateItemPOId = objBs.UpdateItemProcessOrderEntry(Convert.ToInt32(ddlProcessOn.SelectedValue), OrderDate, DeliveryFrom, DeliveryTo, txtDeliveryPlace.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToDouble(txtTotalAmount.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ItemEntryId), hdIsupdate.Value);

                    #region

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

                        HiddenField hdRemarks = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRemarks");
                        HiddenField hdIsRequest = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsRequest");
                        HiddenField hdIsReceive = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsReceive");

                        int TransSamplingCostingId = objBs.InsertTransItemProcessOrderEntry(Convert.ToInt32(ItemEntryId), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdIssueItemId.Value), Convert.ToInt32(hdReceiveItemId.Value), Convert.ToInt32(hdIssColorId.Value), Convert.ToInt32(hdRecColorId.Value), Convert.ToInt32(hdProcessId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(hdRate.Value), Convert.ToDouble(hdAmount.Value), hdRemarks.Value, hdIsRequest.Value, hdIsReceive.Value, Convert.ToInt32(ddlCompany.SelectedValue), hdIsupdate.Value, Convert.ToInt32(hdTransId.Value));


                    }

                    if (hdIsupdate.Value == "No")
                    {
                        int Update_IP = objBs.UpdateTransItemProcess(Convert.ToInt32(ItemEntryId));
                    }

                    #endregion
                }
            }

            Response.Redirect("ItemProcessOrderEntryGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ItemProcessOrderEntryGrid.aspx");
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

                drNew["TransId"] = TransId.Value;
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

                drNew["TransId"] = TransId.Value;
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

            ddlIssueItems.ClearSelection();
            ddlReceiveItems.ClearSelection();

            ddlIssueColor.ClearSelection();
            ddlReceiveColor.ClearSelection();

            ddlProcess.ClearSelection();

            txtQty.Text = "";
            txtShrink.Text = "";
            txtTotalQty.Text = "";
            txtRate.Text = "";
            txtAmount.Text = "";
            txtRemarks.Text = "";

            lblIssue1.Text = "";
            lblIssue2.Text = "";
            lblRec1.Text = "";
            lblRec2.Text = "";

            TransId.Value = "0";

            btnSubmit.Text = "Submit";

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
