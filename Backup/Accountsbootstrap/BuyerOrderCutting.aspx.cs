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
    public partial class BuyerOrderCutting : System.Web.UI.Page
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
                HttpCookie nameCookie = new HttpCookie("Name");
                nameCookie.Values["Name"] = "0";
                nameCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(nameCookie);

                DataSet dsCuttingNo = objBs.GetCuttingNo(YearCode);
                string CuttingNo = dsCuttingNo.Tables[0].Rows[0]["CuttingNo"].ToString().PadLeft(4, '0');
                txtCuttingNo.Text = "PRP - " + CuttingNo + " / " + YearCode;

                txtCuttingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtShipmentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtCuttingFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtCuttingToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                #region

                DataSet dsExcNo = objBs.GetAllExcNoforPreCut();
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }
                else
                {
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }

                DataSet dsset = objBs.getLedger(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlBuyerCode.DataSource = dsset.Tables[0];
                    ddlBuyerCode.DataTextField = "CompanyCode";
                    ddlBuyerCode.DataValueField = "LedgerID";
                    ddlBuyerCode.DataBind();
                    ddlBuyerCode.Items.Insert(0, "BuyerCode");

                    ddlBuyerName.DataSource = dsset.Tables[0];
                    ddlBuyerName.DataTextField = "LedgerName";
                    ddlBuyerName.DataValueField = "LedgerID";
                    ddlBuyerName.DataBind();
                    ddlBuyerName.Items.Insert(0, "BuyerName");
                }

                DataSet dssize = objBs.GetOrderType();
                if (dssize.Tables[0].Rows.Count > 0)
                {
                    ddlOrderType.DataSource = dssize.Tables[0];
                    ddlOrderType.DataTextField = "OrderType";
                    ddlOrderType.DataValueField = "OrderTypeId";
                    ddlOrderType.DataBind();
                }


                DataSet dsFabriccode = objBs.getiItemTypeforHead_fabcode_Itemtype("ShowMainFabricDetails");
                if (dsFabriccode.Tables[0].Rows.Count > 0)
                {
                    ddlFabricCode.DataSource = dsFabriccode.Tables[0];
                    ddlFabricCode.DataTextField = "itemdescription";
                    ddlFabricCode.DataValueField = "itemid";
                    ddlFabricCode.DataBind();
                    ddlFabricCode.Items.Insert(0, "FabricCode");
                }
                ddlFabricName.Items.Insert(0, "FabricName");


                DataSet dsShipmentMode = objBs.GetShipmentMode();
                if (dsShipmentMode.Tables[0].Rows.Count > 0)
                {
                    ddlShipmentMode.DataSource = dsShipmentMode.Tables[0];
                    ddlShipmentMode.DataTextField = "ShipmentMode";
                    ddlShipmentMode.DataValueField = "ShipmentId";
                    ddlShipmentMode.DataBind();
                }
                DataSet dsPaymentmodes = objBs.getPaymentmodes();
                if (dsPaymentmodes.Tables[0].Rows.Count > 0)
                {
                    ddlPaymentMode.DataSource = dsPaymentmodes.Tables[0];
                    ddlPaymentMode.DataTextField = "Payment_Mode";
                    ddlPaymentMode.DataValueField = "Payment_ID";
                    ddlPaymentMode.DataBind();
                }

                DataSet dsItemforItemProcess = objBs.GetItemforItemProcess(Convert.ToInt32(lblProcess.Text));
                if (dsItemforItemProcess.Tables[0].Rows.Count > 0)
                {
                    ddlItems.DataSource = dsItemforItemProcess.Tables[0];
                    ddlItems.DataTextField = "Description";
                    ddlItems.DataValueField = "ItemMasterId";
                    ddlItems.DataBind();
                    ddlItems.Items.Insert(0, "Select Item");
                }
                else
                {
                    ddlItems.Items.Clear();
                    ddlItems.Items.Insert(0, "Select Item");
                }


                DataSet dsEmp = objBs.GetEmployeeDetails(lblEmployee.Text);
                if (dsEmp.Tables[0].Rows.Count > 0)
                {
                    chkCuttingBy.DataSource = dsEmp.Tables[0];
                    chkCuttingBy.DataTextField = "Name";
                    chkCuttingBy.DataValueField = "Employee_Id";
                    chkCuttingBy.DataBind();
                }
                DataSet dsProcess = objBs.GetProcess();
                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    chkProcess.DataSource = dsProcess.Tables[0];
                    chkProcess.DataTextField = "Process";
                    chkProcess.DataValueField = "ProcessId";
                    chkProcess.DataBind();



                }
                ddlFinishingProcess.Items.Insert(0, "FinishingProcess");

                DataSet dsColor = objBs.gridColor();
                if (dsColor.Tables[0].Rows.Count > 0)
                {
                    ddlColor.DataSource = dsColor.Tables[0];
                    ddlColor.DataTextField = "Color";
                    ddlColor.DataValueField = "ColorId";
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0, "Select Color");
                }

                DataSet dcompany = objBs.GetCompanyDetails();
                if (dcompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompanyName.DataSource = dcompany.Tables[0];
                    ddlCompanyName.DataTextField = "Companyname";
                    ddlCompanyName.DataValueField = "comapanyId";
                    ddlCompanyName.DataBind();
                    // ddlCompanyName.Items.Insert(0, "Select Company");
                }


                DataSet dsProcessLedger = objBs.GetApprovedProcessLedger();
                if (dsProcessLedger.Tables[0].Rows.Count > 0)
                {
                    ddlCuttingLedger.DataSource = dsProcessLedger.Tables[0];
                    ddlCuttingLedger.DataTextField = "LedgerName";
                    ddlCuttingLedger.DataValueField = "LedgerID";
                    ddlCuttingLedger.DataBind();
                    ddlCuttingLedger.Items.Insert(0, "Select Cutting Ledger");
                }
                else
                {
                    ddlCuttingLedger.Items.Insert(0, "Select Cutting Ledger");
                }

                #endregion

                string BuyerOrderCuttingId = Request.QueryString.Get("BuyerOrderCuttingId");
                if (BuyerOrderCuttingId != "" && BuyerOrderCuttingId != null)
                {
                    #region

                    DataSet dsBuyerOrderCutting = objBs.GetBuyerOrderCutting(Convert.ToInt32(BuyerOrderCuttingId));
                    if (dsBuyerOrderCutting.Tables[0].Rows.Count > 0)
                    {
                        btnSave.Text = "Save";
                        btnSave.Enabled = false;

                        DataSet dsExcNo1 = objBs.GetAllExcNo();
                        if (dsExcNo1.Tables[0].Rows.Count > 0)
                        {
                            ddlExcNo.DataSource = dsExcNo1.Tables[0];
                            ddlExcNo.DataTextField = "ExcNo";
                            ddlExcNo.DataValueField = "BuyerOrderId";
                            ddlExcNo.DataBind();
                            ddlExcNo.Items.Insert(0, "Select ExcNo");
                        }
                        else
                        {
                            ddlExcNo.Items.Insert(0, "Select ExcNo");
                        }

                        txtCuttingNo.Text = dsBuyerOrderCutting.Tables[0].Rows[0]["FullCuttingNo"].ToString();

                        ddlCuttingLedger.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["CuttingLedger"].ToString();
                        ddlCuttingLedger.Enabled = false;

                        ddlExcNo.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["BuyerOrderId"].ToString();
                        ddlExcNo.Enabled = false;

                        txtCuttingDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["CuttingDate"]).ToString("dd/MM/yyyy");
                        txtRemarks.Text = dsBuyerOrderCutting.Tables[0].Rows[0]["Remarks"].ToString();

                        ddlCompanyName.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["CompanyId"].ToString();
                        ddlCompanyName.Enabled = false;

                        txtCuttingFromDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                        txtCuttingToDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                        DataSet dsBuyerOrder = objBs.getBuyerOrdervalues(Convert.ToInt32(ddlExcNo.SelectedValue));
                        if (dsBuyerOrder.Tables[0].Rows.Count > 0)
                        {
                            #region

                            ddlOrderType.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["OrderTypeId"].ToString();
                            ddlOrderType.Enabled = false;
                            ddlBuyerCode.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["BuyerCodeID"].ToString();
                            ddlBuyerCode.Enabled = false;
                            ddlBuyerName.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["BuyerCodeID"].ToString();
                            ddlFabricCode.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["FabricCodeId"].ToString();

                            DataSet dsgetfabricname = objBs.getiItemTypeforHead_fabricnamelist(ddlFabricCode.SelectedValue);
                            if (dsgetfabricname.Tables[0].Rows.Count > 0)
                            {
                                ddlFabricName.DataSource = dsgetfabricname.Tables[0];
                                ddlFabricName.DataTextField = "description";
                                ddlFabricName.DataValueField = "itemmasterid";
                                ddlFabricName.DataBind();
                                ddlFabricName.Items.Insert(0, "FabricName");
                            }
                            ddlFabricName.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["FabricDescId"].ToString();

                            txtOrderDate.Text = Convert.ToDateTime(dsBuyerOrder.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                            txtShipmentDate.Text = Convert.ToDateTime(dsBuyerOrder.Tables[0].Rows[0]["ShipmentDate"]).ToString("dd/MM/yyyy");
                            txtDeliveryDate.Text = Convert.ToDateTime(dsBuyerOrder.Tables[0].Rows[0]["DeliveryDate"]).ToString("dd/MM/yyyy");

                            ddlShipmentMode.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["ShipmentModeId"].ToString();
                            ddlPaymentMode.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["PaymentModeId"].ToString();

                            #endregion
                        }

                        DataSet dsMaster1 = objBs.GetBuyerOrderCuttingMaster(Convert.ToInt32(BuyerOrderCuttingId));
                        if (dsMaster1.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsMaster1.Tables[0].Rows.Count; j++)
                            {
                                chkCuttingBy.Items.FindByValue(dsMaster1.Tables[0].Rows[j]["EmployeeId"].ToString()).Selected = true;
                            }
                        }
                        DataSet dsProcess1 = objBs.GetBuyerOrderCuttingProcess(Convert.ToInt32(BuyerOrderCuttingId));
                        if (dsProcess1.Tables[0].Rows.Count > 0)
                        {
                            ddlFinishingProcess.DataSource = dsProcess.Tables[0];
                            ddlFinishingProcess.DataTextField = "Process";
                            ddlFinishingProcess.DataValueField = "ProcessId";
                            ddlFinishingProcess.DataBind();
                            ddlFinishingProcess.Items.Insert(0, "FinishingProcess");

                            for (int j = 0; j < dsProcess1.Tables[0].Rows.Count; j++)
                            {
                                chkProcess.Items.FindByValue(dsProcess1.Tables[0].Rows[j]["ProcessId"].ToString()).Selected = true;
                            }
                        }
                        ddlFinishingProcess.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["FinishingProcessId"].ToString();
                        ddlFinishingProcess.Enabled = false;

                        DataSet dsBuyerOrderCuttingrItems = objBs.GetTransBuyerOrderCuttingItems1(Convert.ToInt32(BuyerOrderCuttingId));
                        if (dsBuyerOrderCuttingrItems.Tables[0].Rows.Count > 0)
                        {
                            DataSet dstd = new DataSet();
                            DataTable dtddd = new DataTable();
                            DataRow drNew;
                            DataColumn dct;
                            DataTable dttt = new DataTable();

                            #region

                            dct = new DataColumn("RowId");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("StyleNo");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("StyleNoId");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Description");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("ColorId");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Color");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Rate");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Qty");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("AffectedQty");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("RangeId");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("CQty");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("CRatio");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Size");
                            dttt.Columns.Add(dct);

                            dstd.Tables.Add(dttt);

                            foreach (DataRow Dr in dsBuyerOrderCuttingrItems.Tables[0].Rows)
                            {
                                drNew = dttt.NewRow();
                                drNew["RowId"] = Dr["RowId"];
                                drNew["StyleNo"] = Dr["StyleNo"];
                                drNew["StyleNoId"] = Dr["StyleNoId"];
                                drNew["Description"] = Dr["Description"];
                                drNew["ColorId"] = Dr["ColorId"];
                                drNew["Color"] = Dr["Color"];
                                drNew["Rate"] = Dr["Rate"];
                                drNew["Qty"] = Dr["Qty"];
                                drNew["AffectedQty"] = Dr["AffectedQty"];
                                drNew["RangeId"] = Dr["RangeId"];
                                drNew["CQty"] = Dr["CQty"];
                                drNew["CRatio"] = Dr["CRatio"];
                                drNew["Size"] = Dr["Range"];

                                dstd.Tables[0].Rows.Add(drNew);
                                dtddd = dstd.Tables[0];
                            }

                            #endregion

                            ViewState["CurrentTable1"] = dtddd;
                            GVItem.DataSource = dtddd;
                            GVItem.DataBind();


                        }
                        DataSet dsBuyerOrderCuttingSizes = objBs.GetTransBuyerOrderCuttingSizes1(Convert.ToInt32(BuyerOrderCuttingId));
                        if (dsBuyerOrderCuttingSizes.Tables[0].Rows.Count > 0)
                        {
                            DataSet dstd1 = new DataSet();
                            DataTable dtddd1 = new DataTable();
                            DataRow drNew1;
                            DataColumn dct1;
                            DataTable dttt1 = new DataTable();

                            #region

                            dct1 = new DataColumn("RowId");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("SizeId");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("Size");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("Ratio");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("Qty");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("CQty");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("CRatio");
                            dttt1.Columns.Add(dct1);
                            dstd1.Tables.Add(dttt1);

                            foreach (DataRow Dr in dsBuyerOrderCuttingSizes.Tables[0].Rows)
                            {
                                drNew1 = dttt1.NewRow();

                                drNew1["RowId"] = Dr["RowId"];
                                drNew1["SizeId"] = Dr["SizeId"];
                                drNew1["Size"] = Dr["Size"];
                                drNew1["Ratio"] = Dr["Ratio"];
                                drNew1["Qty"] = Dr["Qty"];
                                drNew1["CQty"] = Dr["CQty"];
                                drNew1["CRatio"] = Dr["CRatio"];

                                dstd1.Tables[0].Rows.Add(drNew1);
                                dtddd1 = dstd1.Tables[0];

                            }

                            #endregion

                            ViewState["CurrentTable2"] = dtddd1;
                        }

                        DataSet dsBuyerOrderCuttingFabric = objBs.GetTransBuyerOrderCuttingFabric(Convert.ToInt32(BuyerOrderCuttingId));
                        if (dsBuyerOrderCuttingFabric.Tables[0].Rows.Count > 0)
                        {
                            DataSet dstd2 = new DataSet();
                            DataTable dtddd2 = new DataTable();
                            DataRow drNew2;
                            DataColumn dct2;
                            DataTable dttt2 = new DataTable();

                            #region

                            dct2 = new DataColumn("Item");
                            dttt2.Columns.Add(dct2);
                            dct2 = new DataColumn("ItemId");
                            dttt2.Columns.Add(dct2);
                            dct2 = new DataColumn("Color");
                            dttt2.Columns.Add(dct2);
                            dct2 = new DataColumn("ColorId");
                            dttt2.Columns.Add(dct2);
                            dct2 = new DataColumn("AvlStock");
                            dttt2.Columns.Add(dct2);
                            dct2 = new DataColumn("RequiredStock");
                            dttt2.Columns.Add(dct2);
                            dct2 = new DataColumn("IssueQty");
                            dttt2.Columns.Add(dct2);

                            dstd2.Tables.Add(dttt2);

                            foreach (DataRow Dr2 in dsBuyerOrderCuttingFabric.Tables[0].Rows)
                            {
                                drNew2 = dttt2.NewRow();

                                drNew2["Item"] = Dr2["Item"];
                                drNew2["ItemId"] = Dr2["ItemId"];
                                drNew2["Color"] = Dr2["Color"];
                                drNew2["ColorId"] = Dr2["ColorId"];
                                drNew2["AvlStock"] = Dr2["AvlStock"];
                                drNew2["RequiredStock"] = Dr2["RequiredStock"];
                                drNew2["IssueQty"] = Dr2["IssueStock"];

                                dstd2.Tables[0].Rows.Add(drNew2);
                                dtddd2 = dstd2.Tables[0];
                            }

                            #endregion

                            ViewState["CurrentTable3"] = dtddd2;

                            GVFabricDetails.DataSource = dtddd2;
                            GVFabricDetails.DataBind();

                        }

                        GVFabricDetails.Columns[3].Visible = false;

                        if (GVFabricDetails.Rows.Count > 0)
                        {
                            for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                            {
                                HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                                HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                                Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");

                                DataSet dsstock = objBs.GetAvlStock(hdItemId.Value, hdColorId.Value, dsBuyerOrderCutting.Tables[0].Rows[0]["CompanyId"].ToString());
                                if (dsstock.Tables[0].Rows.Count > 0)
                                {
                                    lblAvlStock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                                }
                                else
                                {
                                    lblAvlStock.Text = "0";
                                }
                            }
                        }
                    }

                    #endregion
                }
            }
        }

        protected void ddlExcNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "0" && ddlExcNo.SelectedValue != "Select ExcNo")
            {
                DataSet dsBuyerOrderCutting = objBs.getBuyerOrdervalues(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (dsBuyerOrderCutting.Tables[0].Rows.Count > 0)
                {
                    #region

                    ddlOrderType.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["OrderTypeId"].ToString();
                    ddlOrderType.Enabled = false;
                    ddlBuyerCode.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["BuyerCodeID"].ToString();
                    ddlBuyerCode.Enabled = false;
                    ddlBuyerName.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["BuyerCodeID"].ToString();
                    ddlFabricCode.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["FabricCodeId"].ToString();
                    DataSet dsgetfabricname = objBs.getiItemTypeforHead_fabricnamelist(ddlFabricCode.SelectedValue);
                    if (dsgetfabricname.Tables[0].Rows.Count > 0)
                    {
                        ddlFabricName.DataSource = dsgetfabricname.Tables[0];
                        ddlFabricName.DataTextField = "description";
                        ddlFabricName.DataValueField = "itemmasterid";
                        ddlFabricName.DataBind();
                        ddlFabricName.Items.Insert(0, "FabricName");
                    }
                    ddlFabricName.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["FabricDescId"].ToString();

                    txtOrderDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                    txtShipmentDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["ShipmentDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["DeliveryDate"]).ToString("dd/MM/yyyy");

                    ddlShipmentMode.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["ShipmentModeId"].ToString();
                    ddlPaymentMode.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["PaymentModeId"].ToString();

                    txtRemarks.Text = dsBuyerOrderCutting.Tables[0].Rows[0]["Remarks"].ToString();

                    #endregion

                    DataSet dsBuyerOrderCuttingrItems = objBs.getTransBuyerOrderItemsvalues(Convert.ToInt32(ddlExcNo.SelectedValue));
                    if (dsBuyerOrderCuttingrItems.Tables[0].Rows.Count > 0)
                    {
                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

                        #region

                        dct = new DataColumn("RowId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("StyleNo");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("StyleNoId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Description");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ColorId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Color");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Rate");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Qty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("AffectedQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("RangeId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("CQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("CRatio");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Size");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        foreach (DataRow Dr in dsBuyerOrderCuttingrItems.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();
                            drNew["RowId"] = Dr["RowId"];
                            drNew["StyleNo"] = Dr["StyleNo"];
                            drNew["StyleNoId"] = Dr["StyleNoId"];
                            drNew["Description"] = Dr["Description"];
                            drNew["ColorId"] = Dr["ColorId"];
                            drNew["Color"] = Dr["Color"];
                            drNew["Rate"] = Dr["Rate"];
                            drNew["Qty"] = Dr["Qty"];
                            drNew["AffectedQty"] = Dr["AffectedQty"];
                            drNew["RangeId"] = Dr["RangeId"];
                            drNew["CQty"] = Dr["CQty"];
                            drNew["CRatio"] = Dr["CRatio"];
                            drNew["Size"] = Dr["Range"];

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();
                    }
                    DataSet dsBuyerOrderCuttingSizes = objBs.getTransBuyerOrderSizesvalues(Convert.ToInt32(ddlExcNo.SelectedValue));
                    if (dsBuyerOrderCuttingSizes.Tables[0].Rows.Count > 0)
                    {
                        DataSet dstd1 = new DataSet();
                        DataTable dtddd1 = new DataTable();
                        DataRow drNew1;
                        DataColumn dct1;
                        DataTable dttt1 = new DataTable();

                        #region

                        dct1 = new DataColumn("RowId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("SizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Size");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Ratio");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Qty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("CQty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("CRatio");
                        dttt1.Columns.Add(dct1);
                        dstd1.Tables.Add(dttt1);

                        foreach (DataRow Dr in dsBuyerOrderCuttingSizes.Tables[0].Rows)
                        {
                            drNew1 = dttt1.NewRow();

                            drNew1["RowId"] = Dr["RowId"];
                            drNew1["SizeId"] = Dr["SizeId"];
                            drNew1["Size"] = Dr["Size"];
                            drNew1["Ratio"] = Dr["Ratio"];
                            drNew1["Qty"] = Dr["Qty"];
                            drNew1["CQty"] = Dr["CQty"];
                            drNew1["CRatio"] = Dr["CRatio"];

                            dstd1.Tables[0].Rows.Add(drNew1);
                            dtddd1 = dstd1.Tables[0];

                        }

                        #endregion

                        ViewState["CurrentTable2"] = dtddd1;
                    }

                    DataSet dsProcess = objBs.GetProcessfromRS(Convert.ToInt32(ddlExcNo.SelectedValue));
                    if (dsProcess.Tables[0].Rows.Count > 0)
                    {
                        ddlFinishingProcess.DataSource = dsProcess.Tables[0];
                        ddlFinishingProcess.DataTextField = "Process";
                        ddlFinishingProcess.DataValueField = "ProcessId";
                        ddlFinishingProcess.DataBind();
                        ddlFinishingProcess.Items.Insert(0, "FinishingProcess");

                        for (int j = 0; j < dsProcess.Tables[0].Rows.Count; j++)
                        {
                            chkProcess.Items.FindByValue(dsProcess.Tables[0].Rows[j]["ProcessId"].ToString()).Selected = true;
                        }
                    }
                    else
                    {
                        ddlFinishingProcess.Items.Clear();
                        ddlFinishingProcess.Items.Insert(0, "FinishingProcess");
                    }

                    DataSet dsFab = objBs.GetFabDetailsfromRS(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlCompanyName.SelectedValue));
                    if (dsFab.Tables[0].Rows.Count > 0)
                    {
                        GVFabricDetails.DataSource = dsFab;
                        GVFabricDetails.DataBind();

                        for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                        {
                            HiddenField lblitemid = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                            HiddenField lblitemcolorid = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");

                            
                            Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");
                          //  Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");

                            

                            // Get STock

                            DataSet dsstock = objBs.GetAvlStock(lblitemid.Value, lblitemcolorid.Value, ddlCompanyName.SelectedValue);
                            if (dsstock.Tables[0].Rows.Count > 0)
                            {
                                lblAvlStock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                            }
                            else
                            {
                                lblAvlStock.Text = "0.00";

                            }

                            //double getremainstock = Convert.ToDouble(lblprodavg.Text) - Convert.ToDouble(lblavlstock.Text);
                            //if (getremainstock < 0)
                            //{
                            //    lblpurchasestock.Text = "0.00";
                            //}
                            //else
                            //{
                            //    lblpurchasestock.Text = getremainstock.ToString("0.00");
                            //}


                        }


                    }
                    else
                    {
                        GVFabricDetails.DataSource = dsFab;
                        GVFabricDetails.DataBind();
                    }

                    #region GET AVALIABLE STOCK FabricDetails

                    if (GVFabricDetails.Rows.Count > 0)
                    {
                        for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                        {
                            HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                            HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                            Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");

                            DataSet dsstock = objBs.GetAvlStock(hdItemId.Value, hdColorId.Value, ddlCompanyName.SelectedValue);
                            if (dsstock.Tables[0].Rows.Count > 0)
                            {
                                lblAvlStock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                            }
                            else
                            {
                                lblAvlStock.Text = "0";
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region

                    ViewState["CurrentTable1"] = null;
                    GVItem.DataSource = null;
                    GVItem.DataBind();

                    ViewState["CurrentTable2"] = null;
                    GVSizesView.DataSource = null;
                    GVSizesView.DataBind();

                    GVFabricDetails.DataSource = null;
                    GVFabricDetails.DataBind();

                    chkCuttingBy.ClearSelection();
                    chkProcess.ClearSelection();

                    ddlFinishingProcess.Items.Clear();
                    ddlFinishingProcess.Items.Insert(0, "FinishingProcess");

                    #endregion
                }
            }
            else
            {
                #region

                ViewState["CurrentTable1"] = null;
                GVItem.DataSource = null;
                GVItem.DataBind();

                ViewState["CurrentTable2"] = null;
                GVSizesView.DataSource = null;
                GVSizesView.DataBind();

                GVFabricDetails.DataSource = null;
                GVFabricDetails.DataBind();

                chkCuttingBy.ClearSelection();
                chkProcess.ClearSelection();

                ddlFinishingProcess.Items.Clear();
                ddlFinishingProcess.Items.Insert(0, "FinishingProcess");

                #endregion
            }

        }

        protected void ddlCompanyName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            #region GET AVALIABLE STOCK FabricDetails

            if (GVFabricDetails.Rows.Count > 0)
            {
                for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                {
                    HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                    Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");

                    DataSet dsstock = objBs.GetAvlStock(hdItemId.Value, hdColorId.Value, ddlCompanyName.SelectedValue);
                    if (dsstock.Tables[0].Rows.Count > 0)
                    {
                        lblAvlStock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                    }
                    else
                    {
                        lblAvlStock.Text = "0";
                    }
                }
            }

            #endregion
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            DataSet dstd = new DataSet();
            DataTable dtddd = new DataTable();
            DataRow drNew;
            DataColumn dct;
            DataTable dttt = new DataTable();

            #region


            dct = new DataColumn("Item");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ItemId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ColorId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("AvlStock");
            dttt.Columns.Add(dct);
            dct = new DataColumn("IssueStock");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            if (ViewState["CurrentTable3"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable3"];

                drNew = dttt.NewRow();

                drNew["Item"] = ddlItems.SelectedItem.Text;
                drNew["ItemId"] = ddlItems.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["AvlStock"] = txtAvlStock.Text;
                drNew["IssueStock"] = txtIssueStock.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

            }
            else
            {

                drNew = dttt.NewRow();

                drNew["Item"] = ddlItems.SelectedItem.Text;
                drNew["ItemId"] = ddlItems.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["AvlStock"] = txtAvlStock.Text;
                drNew["IssueStock"] = txtIssueStock.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
            }

            #endregion

            ViewState["CurrentTable3"] = dtddd;

            GVFabricDetails.DataSource = dtddd;
            GVFabricDetails.DataBind();


        }

        protected void GVItem_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "View")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    #region

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();

                    DataRow drNew1;
                    DataColumn dct1;

                    DataTable dttt1 = new DataTable();


                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Ratio");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("CQty");
                    dttt1.Columns.Add(dct1);

                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["Ratio"] = RowsGVSizeQty[i]["Ratio"].ToString();
                        drNew1["CQty"] = RowsGVSizeQty[i]["CQty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    GVSizesView.DataSource = dstd1;
                    GVSizesView.DataBind();



                    #endregion
                }
            }

        }

        protected void btnSubmitQty_OnClick(object sender, EventArgs e)
        {


        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (chkCuttingBy.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Check Cutting By.')", true);
                chkCuttingBy.Focus();
                return;
            }
            if (chkProcess.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Check Process.')", true);
                chkProcess.Focus();
                return;
            }

            if (GVFabricDetails.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Check Fabric Details.')", true);
                return;
            }
            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Check Style Details.')", true);
                return;
            }

            DateTime FromDate = DateTime.ParseExact(txtCuttingFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(txtCuttingToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (FromDate == ToDate)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('FromDate and ToDate have being in Same.')", true);
                return;
            }

            for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
            {
                HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                HiddenField hdRequiredStock = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdRequiredStock");
                Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");
                TextBox txtIssueQty = (TextBox)GVFabricDetails.Rows[vLoop].FindControl("txtIssueQty");

                if (txtIssueQty.Text == "")
                    txtIssueQty.Text = "0";

                if (Convert.ToDouble(txtIssueQty.Text) > 0)
                {
                    if (Convert.ToDouble(lblAvlStock.Text) < Convert.ToDouble(txtIssueQty.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Avl.Stock  in Row " + (Convert.ToInt32(vLoop) + 1) + ".')", true);
                        txtIssueQty.Focus();
                        return;
                    }
                    //////else if (Convert.ToDouble(hdRequiredStock.Value) < Convert.ToDouble(txtIssueQty.Text))
                    //////{
                    //////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Issue Qty in Row " + (Convert.ToInt32(vLoop) + 1) + ".')", true);
                    //////    txtIssueQty.Focus();
                    //////    return;
                    //////}
                }

            }

            HttpCookie nameCookie = Request.Cookies["Name"];
            string MaxRowId = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

            DateTime CuttingDate = DateTime.ParseExact(txtCuttingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);



            if (btnSave.Text == "Save")
            {
                DataSet dsCuttingNo = objBs.GetCuttingNo(YearCode);
                string CuttingNo = dsCuttingNo.Tables[0].Rows[0]["CuttingNo"].ToString().PadLeft(4, '0');
                txtCuttingNo.Text = "PRP - " + CuttingNo + " / " + YearCode;

                int BuyerOrderCuttingId = objBs.InsertBuyerOrderCutting(Convert.ToInt32(dsCuttingNo.Tables[0].Rows[0]["CuttingNo"].ToString()), CuttingDate, Convert.ToInt32(ddlExcNo.SelectedValue), MaxRowId, txtRemarks.Text, Convert.ToInt32(ddlFinishingProcess.SelectedValue), Convert.ToInt32(ddlCompanyName.SelectedValue), txtCuttingNo.Text, YearCode, FromDate, ToDate, Convert.ToInt32(ddlCuttingLedger.SelectedValue));

                foreach (ListItem CuttingBy in chkCuttingBy.Items)
                {
                    if (CuttingBy.Selected)
                    {
                        int TransMasterId = objBs.InsertBuyerOrderCuttingMaster(BuyerOrderCuttingId, Convert.ToInt32(CuttingBy.Value));
                    }
                }
                foreach (ListItem Process in chkProcess.Items)
                {
                    if (Process.Selected)
                    {
                        int TransMasterId = objBs.InsertBuyerOrderCuttingProcess(BuyerOrderCuttingId, Convert.ToInt32(Process.Value));
                    }
                }

                DataTable CurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                int TransBuyerOrderCuttingId = objBs.InsertTransBuyerOrderCuttingItems(BuyerOrderCuttingId, CurrentTable1);

                DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
                int TransBuyerOrderCuttingSizeId = objBs.InsertTransBuyerOrderCuttingSizes(BuyerOrderCuttingId, CurrentTable2);

                //////DataTable CurrentTable3 = (DataTable)ViewState["CurrentTable3"];
                //////int CuttingFabricId = objBs.InsertTransBuyerOrderCuttingFabric(BuyerOrderCuttingId, CurrentTable3);

                for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                {
                    HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdRequiredStock = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdRequiredStock");

                    Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");
                    TextBox txtIssueQty = (TextBox)GVFabricDetails.Rows[vLoop].FindControl("txtIssueQty");

                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";


                    int CuttingFabricId = objBs.InsertTransBuyerOrderCuttingFabric(BuyerOrderCuttingId, Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(lblAvlStock.Text), Convert.ToDouble(hdRequiredStock.Value), Convert.ToDouble(txtIssueQty.Text), Convert.ToInt32(ddlCompanyName.SelectedValue));

                }

            }
            //else
            //{
            //    string BuyerOrderId = Request.QueryString.Get("BuyerOrderId");

            //    string[] OrderType1 = ddlOrderType.SelectedValue.Split('$');
            //    int UpdateBuyerOrderId = objBs.UpdateBuyerOrderMaster(Convert.ToInt32(OrderType1[0]), Convert.ToInt32(ddlBuyerCode.SelectedValue), txtExcNo.Text, txtBuyerPONo.Text, Convert.ToInt32(ddlFabricCode.SelectedValue), OrderDate, ShipmentDate, DeliveryDate, Convert.ToInt32(ddlShipmentMode.SelectedValue), Convert.ToInt32(ddlPaymentMode.SelectedValue), txtPaymentTerms.Text, txtBytheWayof.Text, Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToDouble(txtAmount.Text), chkShipped.Checked, chkCancel.Checked, chkApproved.Checked, chkHold.Checked, chkLock.Checked, MaxRowId, Convert.ToInt32(BuyerOrderId), txtRemarks.Text);

            //    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            //    {
            //        HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
            //        HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");
            //        HiddenField hdRangeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRangeId");
            //        HiddenField hdRowId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRowId");

            //        Label lblRate = (Label)GVItem.Rows[vLoop].FindControl("lblRate");
            //        Label lblQty = (Label)GVItem.Rows[vLoop].FindControl("lblQty");
            //        Label lblAffectedQty = (Label)GVItem.Rows[vLoop].FindControl("lblAffectedQty");

            //        int TransBuyerOrderId = objBs.InsertTransBuyerOrderItems(Convert.ToInt32(BuyerOrderId), Convert.ToInt32(hdStyleNoId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(lblRate.Text), Convert.ToInt32(lblQty.Text), Convert.ToInt32(lblAffectedQty.Text), Convert.ToInt32(hdRangeId.Value), Convert.ToInt32(hdRowId.Value));

            //    }

            //    DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
            //    int TransBuyerOrderSizeId = objBs.InsertTransBuyerOrderSizes(Convert.ToInt32(BuyerOrderId), CurrentTable2);

            //    for (int vLoop = 0; vLoop < gvLabels.Rows.Count; vLoop++)
            //    {
            //        CheckBox chkLabelItem = (CheckBox)gvLabels.Rows[vLoop].FindControl("chkLabelItem");
            //        HiddenField hdItemId = (HiddenField)gvLabels.Rows[vLoop].FindControl("hdItemId");
            //        TextBox txtLabelText = (TextBox)gvLabels.Rows[vLoop].FindControl("txtLabelText");

            //        if (chkLabelItem.Checked == true)
            //        {
            //            int TransBuyerOrderLabelId = objBs.InsertTransBuyerOrderLabels(Convert.ToInt32(BuyerOrderId), Convert.ToInt32(hdItemId.Value), txtLabelText.Text);
            //        }
            //    }
            //}

            Response.Redirect("BuyerOrderCuttingGrid.aspx");

        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderCuttingGrid.aspx");
        }


    }
}
