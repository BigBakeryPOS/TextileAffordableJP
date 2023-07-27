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
    public partial class PurchaseOrder : System.Web.UI.Page
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

               // ddlIssueItems.Items.Insert(0, "Select Issue Item");

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsPONo = objBs.GetPurchaseOrderPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["PONo"].ToString().PadLeft(4, '0');
                txtProOrderNo.Text = lblsuffexname.Text + " / " + PONo + " / " + YearCode;



                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, "CompanyName");
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
                    ddlColor.DataSource = dsColor.Tables[0];
                    ddlColor.DataTextField = "Color";
                    ddlColor.DataValueField = "ColorID";
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0, "Select Color");

                }

                string POId = Request.QueryString.Get("POId");
                if (POId != "" && POId != null)
                {
                    DataSet dsPO = objBs.getPurchaseOrder(Convert.ToInt32(POId));
                    if (dsPO.Tables[0].Rows.Count > 0)
                    {
                        #region

                        ddlProcessOn.SelectedValue = dsPO.Tables[0].Rows[0]["ProcessOn"].ToString();

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

                        txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                        txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                        txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                        txtDeliveryPlace.Text = dsPO.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        txtProOrderNo.Text = dsPO.Tables[0].Rows[0]["FullPONo"].ToString();

                        ddlCompany.SelectedValue = dsPO.Tables[0].Rows[0]["CompanyId"].ToString();

                        ddlPartyCode.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyName.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyCode_OnSelectedIndexChanged(sender, e);

                        txtTotalAmount.Text = Convert.ToDouble(dsPO.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");

                        btnSave.Text = "Update";
                        ddlProcessOn_OnSelectedIndexChanged(sender, e);
                        //btnSave.Enabled = false;

                        #endregion
                    }


                    // Getting Overall PO change
                    DataSet dcheckoverall = objBs.getsumPurchaseOrder(Convert.ToInt32(POId));
                    if (dcheckoverall.Tables[0].Rows.Count > 0)
                    {
                        double recqty = Convert.ToDouble(dcheckoverall.Tables[0].Rows[0]["Rqty"]);

                        if (recqty <= 0)
                        {
                            ddlPartyCode.Enabled = true;
                            ddlProcessOn.Enabled = true;
                            ddlCompany.Enabled = true;
                        }
                        else
                        {
                            ddlPartyCode.Enabled = false;
                            ddlProcessOn.Enabled = false;
                            ddlCompany.Enabled = false;
                        }
                    }

                    DataSet ds2 = objBs.getTransPurchaseOrder(Convert.ToInt32(POId));
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

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
                        dct = new DataColumn("RecQty");
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
                            drNew["RecQty"] = Dr["RecQty"];
                            drNew["Rate"] = Dr["Rate"];
                            drNew["Amount"] = Dr["Amount"];

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
                }
                else
                {
                    ddlIssueItems.Items.Clear();
                    ddlIssueItems.Items.Insert(0, "Select Issue Item");
                }

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

                if (ddlIssueItemCode.SelectedValue == "" || ddlIssueItemCode.SelectedValue == "0" || ddlIssueItemCode.SelectedValue == "Item")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Item in Row " + Row + " ')", true);
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

                        dtCurrentTable.Rows[i - 1]["Item"] = ddlIssueItemCode.SelectedValue;
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

                        dtCurrentTable.Rows[i - 1]["Item"] = ddlIssueItemCode.SelectedValue;
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

                        ddlIssueItemCode.SelectedValue = dt.Rows[i]["Item"].ToString();
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

                double REcQty = Convert.ToDouble(dt.Rows[rowIndex]["Recqty"]);

                if (REcQty <= 0)
                {

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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Particular Fabric Received.So not Able To Delete.Thank You!!!.')", true);
                    return;
                }
            }

            Calculations();
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

        protected void GVItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument);
                hdRowIndex.Value = RowIndex.ToString();
                //DataTable dt = (DataTable)ViewState["CurrentTable1"];
                //DataRow drCurrentRow = null;

                //dt.Rows.Remove(dt.Rows[rowIndex]);
                //drCurrentRow = dt.NewRow();
                //ViewState["CurrentTable1"] = dt;
                //GVdsItemCodeDescriptionItemType.DataSource = dt;
                //GVdsItemCodeDescriptionItemType.DataBind();

                GridViewRow row = GVItem.Rows[RowIndex];

                DataSet dsItemforItemProcess = objBs.GetItemforItemProcess(Convert.ToInt32(ddlProcessOn.SelectedValue));
                if (dsItemforItemProcess.Tables[0].Rows.Count > 0)
                {
                    ddlIssueItems.DataSource = dsItemforItemProcess.Tables[0];
                    ddlIssueItems.DataTextField = "Description";
                    ddlIssueItems.DataValueField = "FullDetailsId";
                    ddlIssueItems.DataBind();
                    ddlIssueItems.Items.Insert(0, "Select Issue Item");
                }
                else
                {
                    ddlIssueItems.Items.Clear();
                    ddlIssueItems.Items.Insert(0, "Select Issue Item");
                }

                //ddlItems.SelectedValue = (row.FindControl("hdItemId") as HiddenField).Value;

                double recqty = Convert.ToDouble((row.FindControl("hdRecQty") as HiddenField).Value);

                ddlPurchaseFor.SelectedValue = (row.FindControl("hdPurchaseForId") as HiddenField).Value;
                if (ddlPurchaseFor.SelectedValue == "1")
                {
                    DataSet ds = objBs.GetExcNoforPurchaseOrder();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlExcNo.DataSource = ds.Tables[0];
                        ddlExcNo.DataTextField = "ExcNo";
                        ddlExcNo.DataValueField = "BuyerOrderId";
                        ddlExcNo.DataBind();
                        ddlExcNo.Items.Insert(0, "ExcNo");
                    }

                    ddlExcNo.SelectedValue = (row.FindControl("hdPurchaseForTypeId") as HiddenField).Value;
                }
                else
                {
                    DataSet ds = objBs.getLedger("1");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBuyerCode.DataSource = ds.Tables[0];
                        ddlBuyerCode.DataTextField = "CompanyCode";
                        ddlBuyerCode.DataValueField = "ledgerId";
                        ddlBuyerCode.DataBind();
                        ddlBuyerCode.Items.Insert(0, "BuyerCode");
                    }

                    ddlBuyerCode.SelectedValue = (row.FindControl("hdPurchaseForTypeId") as HiddenField).Value;
                }
               // ddlIssueItems.SelectedValue = (row.FindControl("hdItemId") as HiddenField).Value;
                string ItemId = (row.FindControl("hdItemId") as HiddenField).Value;
                DataSet getitemidd = objBs.GetitemProcess_Update(Convert.ToInt32(ItemId));
                if (getitemidd.Tables[0].Rows.Count > 0)
                {
                    ddlIssueItems.SelectedValue = getitemidd.Tables[0].Rows[0]["FullDetailsId"].ToString();
                    ddlIssueItems_OnSelectedIndexChanged(sender, e);
                }
                else
                {

                }
                
                ddlColor.SelectedValue = (row.FindControl("hdColorId") as HiddenField).Value;
                txtQty.Text = (row.FindControl("hdQty") as HiddenField).Value;
                txtShrink.Text = (row.FindControl("hdShrink") as HiddenField).Value;
                txtTotalQty.Text = (row.FindControl("hdTotalQty") as HiddenField).Value;
                txtRate.Text = (row.FindControl("hdRate") as HiddenField).Value;
                txtAmount.Text = (row.FindControl("hdAmount") as HiddenField).Value;
                txtRemarks.Text = (row.FindControl("hdRemarks") as HiddenField).Value;
                //chkReq.Checked = Convert.ToBoolean((row.FindControl("IsRequest") as HiddenField).Value);
                //chkRec.Checked = Convert.ToBoolean((row.FindControl("IsReceive") as HiddenField).Value);


                if (recqty <= 0)
                {
                    ddlExcNo.Enabled = true;
                    ddlBuyerCode.Enabled = true;
                    ddlPurchaseFor.Enabled = true;

                    ddlIssueItems.Enabled = true;
                    ddlColor.Enabled = true;
                    txtQty.Enabled = true;
                    txtShrink.Enabled = true;
                    txtTotalQty.Enabled = true;

                    
                }
                else
                {
                    ddlExcNo.Enabled = false;
                    ddlBuyerCode.Enabled = false;
                    ddlPurchaseFor.Enabled = false;
                    ddlIssueItems.Enabled = false;
                    ddlColor.Enabled = false;
                    txtQty.Enabled = false;
                    txtShrink.Enabled = false;
                    txtTotalQty.Enabled = false;
                 
                }
                btnSubmit.Text = "Update";
            }
        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {

        }


        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            DateTime OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DeliveryFrom = DateTime.ParseExact(txtDeliveryFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DeliveryTo = DateTime.ParseExact(txtDeliveryTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnSave.Text == "Save")
            {

                DataSet dsPONo = objBs.GetPurchaseOrderPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["PONo"].ToString().PadLeft(4, '0');
                txtProOrderNo.Text = PONo + " / " + YearCode;

                DataSet dsPoNo = objBs.CheckPurchaseOrderPONo(txtProOrderNo.Text, 0);
                if (dsPoNo.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Pro.Ord. No. was already Exists.')", true);
                    txtProOrderNo.Focus();
                    return;
                }
                else
                {
                    int POId = objBs.InsertPurchaseOrder(Convert.ToInt32(ddlProcessOn.SelectedValue), OrderDate, DeliveryFrom, DeliveryTo, txtDeliveryPlace.Text, txtProOrderNo.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToDouble(txtTotalAmount.Text), dsPONo.Tables[0].Rows[0]["PONo"].ToString(), YearCode, Convert.ToInt32(ddlCompany.SelectedValue));

                    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                    {
                        HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");
                        HiddenField hdPurchaseForTypeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
                        HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");

                        HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                        HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                        HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                        HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                        HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                        HiddenField hdRemarks = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRemarks");
                        HiddenField hdIsRequest = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsRequest");
                        HiddenField hdIsReceive = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsReceive");

                        int TransSamplingCostingId = objBs.InsertTransPurchaseOrder(POId, Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(hdRate.Value), Convert.ToDouble(hdAmount.Value), hdRemarks.Value, hdIsRequest.Value, hdIsReceive.Value);


                    }


                }
            }
            else if (btnSave.Text == "Update")
            {

                string POId = Request.QueryString.Get("POId");

                // Update tblpo

                int updId = objBs.Update_PurchaseOrder(Convert.ToInt32(ddlProcessOn.SelectedValue), OrderDate, DeliveryFrom, DeliveryTo, txtDeliveryPlace.Text, txtProOrderNo.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToDouble(txtTotalAmount.Text), "", YearCode, Convert.ToInt32(ddlCompany.SelectedValue), POId);

                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");
                    HiddenField hdPurchaseForTypeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
                    HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");

                    HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                    HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                    HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                    HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                    HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                    HiddenField hdRemarks = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRemarks");
                    HiddenField hdIsRequest = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsRequest");
                    HiddenField hdIsReceive = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIsReceive");
                    HiddenField hdRecQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRecQty");

                    if (Convert.ToDouble(hdRecQty.Value) <= 0)
                    {

                        int TransSamplingCostingId = objBs.InsertTransPurchaseOrder(Convert.ToInt32(POId), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(hdRate.Value), Convert.ToDouble(hdAmount.Value), hdRemarks.Value, hdIsRequest.Value, hdIsReceive.Value);
                    }
                    else
                    {
                        int TransSamplingCostingId = objBs.UpdateTransPurchaseOrder(Convert.ToInt32(POId), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(hdRate.Value), Convert.ToDouble(hdAmount.Value), hdRemarks.Value, hdIsRequest.Value, hdIsReceive.Value);
                    }
                }

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Po Update Is In Process.Thank You.')", true);
                //return;
            }
            Response.Redirect("PurchaseOrderGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseOrderGrid.aspx");
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
          


            #region Validation

            if (ddlIssueItems.SelectedValue == "Select Issue Item")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Item.')", true);
                return;
            }
            if (ddlColor.SelectedValue == "Select Color")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Color.')", true);
                return;
            }

            if (txtQty.Text == "" || txtQty.Text =="0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Valid Value.')", true);
                return;
            }

            if (ddlPurchaseFor.SelectedValue == "1")
            {

                if (ddlExcNo.SelectedValue == "ExcNo")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Valid EXC.NO.')", true);
                    return;
                }
            }
            else
            {

                if (ddlBuyerCode.SelectedValue == "BuyerCode")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Valid Buyer Code.')", true);
                    return;
                }
            }

            #endregion

            if (Convert.ToDouble(txtAmount.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Amount.')", true);
                return;
            }


            double recqty = 0;
            if (hdRowIndex.Value != "")
            {
                #region
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable1"];
                    DataRow drCurrentRow = null;
                    int rowIndex = Convert.ToInt32(hdRowIndex.Value);
                     recqty = Convert.ToDouble(dt.Rows[rowIndex]["RecQty"]);
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
            }

            #region CHECKING SAME ITEM

            string[] IssueItems1 = ddlIssueItems.SelectedValue.Split('$');
            string purchasetypeid1 =string.Empty;
            string purchasefor = ddlPurchaseFor.SelectedValue;
            if (purchasefor == "1")
            {
                purchasetypeid1 = ddlExcNo.SelectedValue;
            }
            else
            {
                purchasetypeid1 = ddlBuyerCode.SelectedValue;
            }
            

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");

                HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");

                HiddenField PurchaseForTypeId1 = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
               

                if (hdItemId.Value == IssueItems1[0] && hdColorId.Value == ddlColor.SelectedValue && hdPurchaseForId.Value == purchasefor && PurchaseForTypeId1.Value == purchasetypeid1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already Have This ITem.Not Be Duplicate.Thank YOU!!!.')", true);
                    return;
                }
            }

            #endregion

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
                dct = new DataColumn("RecQty");
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

                if (ViewState["CurrentTable1"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable1"];

                    drNew = dttt.NewRow();

                    drNew["PurchaseFor"] = ddlPurchaseFor.SelectedItem.Text;
                    drNew["PurchaseForID"] = ddlPurchaseFor.SelectedValue;
                    drNew["PurchaseForType"] = PurchaseForType;
                    drNew["PurchaseForTypeId"] = PurchaseForTypeId;

                    drNew["Item"] = ddlIssueItems.SelectedItem.Text;
                    drNew["ItemId"] = IssueItems[0];

                    drNew["Color"] = ddlColor.SelectedItem.Text;
                    drNew["ColorId"] = ddlColor.SelectedValue;

                    drNew["Qty"] = txtQty.Text;
                    drNew["Shrink"] = txtShrink.Text;
                    drNew["TotalQty"] = txtTotalQty.Text;
                    drNew["RecQty"] = recqty.ToString();
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

                    drNew["Item"] = ddlIssueItems.SelectedItem.Text;
                    drNew["ItemId"] = IssueItems[0];

                    drNew["Color"] = ddlColor.SelectedItem.Text;
                    drNew["ColorId"] = ddlColor.SelectedValue;

                    drNew["Qty"] = txtQty.Text;
                    drNew["Shrink"] = txtShrink.Text;
                    drNew["TotalQty"] = txtTotalQty.Text;
                    drNew["RecQty"] = recqty.ToString();
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

            btnSubmit.Text = "Submit";
            txtQty.Text = "";
            txtShrink.Text = "";
            txtTotalQty.Text = "";
            txtRate.Text = "";
            txtAmount.Text = "";
            txtRemarks.Text = "";

            DataSet dsItemforItemProcess = objBs.GetItemforItemProcess(Convert.ToInt32(ddlProcessOn.SelectedValue));
            if (dsItemforItemProcess.Tables[0].Rows.Count > 0)
            {
                ddlIssueItems.DataSource = dsItemforItemProcess.Tables[0];
                ddlIssueItems.DataTextField = "Description";
                ddlIssueItems.DataValueField = "FullDetailsId";
                ddlIssueItems.DataBind();
                ddlIssueItems.Items.Insert(0, "Select Issue Item");
            }
            else
            {
                ddlIssueItems.Items.Clear();
                ddlIssueItems.Items.Insert(0, "Select Issue Item");
            }

            ddlExcNo.Enabled = true;
            ddlBuyerCode.Enabled = true;
            ddlPurchaseFor.Enabled = true;
            ddlIssueItems.Enabled = true;
            ddlColor.Enabled = true;
            txtQty.Enabled = true;
            txtShrink.Enabled = true;
            txtTotalQty.Enabled = true;

        }

        protected void ddlIssueItems_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueItems.SelectedValue != "" && ddlIssueItems.SelectedValue != "0" && ddlIssueItems.SelectedValue != "Select Issue Item")
            {
                string[] IssueItems = ddlIssueItems.SelectedValue.Split('$');

                lblIssue1.Text = "Type: " + IssueItems[1].ToString() + " / " + " Size: " + IssueItems[3].ToString() + " " + IssueItems[4].ToString();
                lblIssue2.Text = "Categories:" + IssueItems[2].ToString();
            }
            else
            {
                lblIssue1.Text = "";
                lblIssue2.Text = "";
            }
        }
    }
}
