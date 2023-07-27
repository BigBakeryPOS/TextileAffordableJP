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
    public partial class BuyerOrderMasterCutting : System.Web.UI.Page
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
                //////HttpCookie nameCookie = new HttpCookie("Name");
                //////nameCookie.Values["Name"] = "0";
                //////nameCookie.Expires = DateTime.Now.AddDays(30);
                //////Response.Cookies.Add(nameCookie);

                txtMasterCuttingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtShipmentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                #region

                DataSet dsExcNo = objBs.GetExcNoFromCutting("No");
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderCuttingId";
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

                    ddlFinishingProcess.DataSource = dsProcess.Tables[0];
                    ddlFinishingProcess.DataTextField = "Process";
                    ddlFinishingProcess.DataValueField = "ProcessId";
                    ddlFinishingProcess.DataBind();
                    ddlFinishingProcess.Items.Insert(0, "FinishingProcess");

                }


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

                }

                #endregion


                string TYP = Request.QueryString.Get("TYP");

                btn_finishprocess.Visible = false;
                

                if (TYP == "REC" || TYP == "VIEW")
                {
                    
                    ddlExcNo.Items.Clear();
                    DataSet dsExcNo1 = objBs.GetExcNoFromCutting("Yes");
                    if (dsExcNo1.Tables[0].Rows.Count > 0)
                    {
                        ddlExcNo.DataSource = dsExcNo1.Tables[0];
                        ddlExcNo.DataTextField = "ExcNo";
                        ddlExcNo.DataValueField = "BuyerOrderCuttingId";
                        ddlExcNo.DataBind();
                        ddlExcNo.Items.Insert(0, "Select ExcNo");
                    }
                    else
                    {
                        ddlExcNo.Items.Insert(0, "Select ExcNo");
                    }

                    if (TYP == "REC")
                    {
                        string BuyerOrderCutId = Request.QueryString.Get("BuyerOrderCutId");
                        DataSet dsBuyerOrderCutting = objBs.GetBuyerOrderCutting(Convert.ToInt32(BuyerOrderCutId));
                        if (dsBuyerOrderCutting.Tables[0].Rows.Count > 0)
                        {
                            #region

                            txtMasterCuttingDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["CuttingDate"]).ToString("dd/MM/yyyy");
                            txtRemarks.Text = dsBuyerOrderCutting.Tables[0].Rows[0]["Remarks"].ToString();
                            ddlExcNo.SelectedValue = BuyerOrderCutId;
                            ddlExcNo.Enabled = false;

                            ddlCompanyName.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["CompanyId"].ToString();
                            ddlFinishingProcess.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["FinishingProcessId"].ToString();

                            DataSet dsBuyerOrder = objBs.getBuyerOrdervalues(Convert.ToInt32(dsBuyerOrderCutting.Tables[0].Rows[0]["BuyerOrderId"].ToString()));
                            if (dsBuyerOrder.Tables[0].Rows.Count > 0)
                            {
                                #region

                                ddlOrderType.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["OrderTypeId"].ToString();
                                ddlBuyerCode.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["BuyerCodeID"].ToString();
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

                            DataSet dsMaster1 = objBs.GetBuyerOrderCuttingMaster(Convert.ToInt32(ddlExcNo.SelectedValue));
                            if (dsMaster1.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsMaster1.Tables[0].Rows.Count; j++)
                                {
                                    chkCuttingBy.Items.FindByValue(dsMaster1.Tables[0].Rows[j]["EmployeeId"].ToString()).Selected = true;
                                }
                            }
                            DataSet dsProcess1 = objBs.GetBuyerOrderCuttingProcess(Convert.ToInt32(ddlExcNo.SelectedValue));
                            if (dsProcess1.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsProcess1.Tables[0].Rows.Count; j++)
                                {
                                    chkProcess.Items.FindByValue(dsProcess1.Tables[0].Rows[j]["ProcessId"].ToString()).Selected = true;
                                }
                            }

                            //////DataSet dsBOMCutting = objBs.GetBuyerOrderMasterCutting(Convert.ToInt32(BuyerOrderCutId));
                            //////HttpCookie nameCookie1 = new HttpCookie("Name");
                            //////nameCookie1.Values["Name"] = dsBOMCutting.Tables[0].Rows[0]["MaxRowId"].ToString();
                            //////nameCookie1.Expires = DateTime.Now.AddDays(30);
                            //////Response.Cookies.Add(nameCookie1);

                            //////string HttpCookieValues = nameCookie1 != null ? nameCookie1.Value.Split('=')[1] : "undefined";

                            DataSet dsBuyerOrderCuttingrItems = objBs.GetTransBuyerOrderCuttingItems_Rec(Convert.ToInt32(ddlExcNo.SelectedValue));
                            if (dsBuyerOrderCuttingrItems.Tables[0].Rows.Count > 0)
                            {
                                DataSet dstd = new DataSet();
                                DataTable dtddd = new DataTable();
                                DataRow drNew;
                                DataColumn dct;
                                DataTable dttt = new DataTable();

                                #region

                                dct = new DataColumn("TransItemId");
                                dttt.Columns.Add(dct);
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

                                dct = new DataColumn("BalQty");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("RecQty");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("DmgQty");
                                dttt.Columns.Add(dct);

                                dstd.Tables.Add(dttt);

                                foreach (DataRow Dr in dsBuyerOrderCuttingrItems.Tables[0].Rows)
                                {
                                    drNew = dttt.NewRow();
                                    drNew["TransItemId"] = Dr["TransBuyerOrderCuttingId"];
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

                                    drNew["BalQty"] = Dr["BalQty"];
                                    drNew["RecQty"] = 0;
                                    drNew["DmgQty"] = 0;

                                    dstd.Tables[0].Rows.Add(drNew);
                                    dtddd = dstd.Tables[0];
                                }

                                #endregion

                                ViewState["CurrentTable1"] = dtddd;
                                GVItem.DataSource = dtddd;
                                GVItem.DataBind();

                            }
                            DataSet dsBuyerOrderCuttingSizes = objBs.GetTransBuyerOrderCuttingSizes_Rec(Convert.ToInt32(ddlExcNo.SelectedValue));
                            if (dsBuyerOrderCuttingSizes.Tables[0].Rows.Count > 0)
                            {
                                DataSet dstd1 = new DataSet();
                                DataTable dtddd1 = new DataTable();
                                DataRow drNew1;
                                DataColumn dct1;
                                DataTable dttt1 = new DataTable();

                                #region

                                dct1 = new DataColumn("TransSizeId");
                                dttt1.Columns.Add(dct1);
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

                                dct1 = new DataColumn("BalQty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("RecQty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("DmgQty");
                                dttt1.Columns.Add(dct1);

                                dstd1.Tables.Add(dttt1);

                                foreach (DataRow Dr in dsBuyerOrderCuttingSizes.Tables[0].Rows)
                                {
                                    drNew1 = dttt1.NewRow();

                                    drNew1["TransSizeId"] = Dr["TransBuyerOrderCuttingSizeId"];
                                    drNew1["RowId"] = Dr["RowId"];
                                    drNew1["SizeId"] = Dr["SizeId"];
                                    drNew1["Size"] = Dr["Size"];
                                    drNew1["Ratio"] = Dr["Ratio"];
                                    drNew1["Qty"] = Dr["Qty"];
                                    drNew1["CQty"] = Dr["CQty"];
                                    drNew1["CRatio"] = Dr["CRatio"];

                                    drNew1["BalQty"] = Dr["BalQty"];
                                    drNew1["RecQty"] = 0;
                                    drNew1["DmgQty"] = 0;

                                    dstd1.Tables[0].Rows.Add(drNew1);
                                    dtddd1 = dstd1.Tables[0];

                                }

                                #endregion

                                ViewState["CurrentTable2"] = dtddd1;
                            }

                            DataSet dsBuyerOrderCuttingFabric = objBs.GetTransBuyerOrderCuttingFabric(Convert.ToInt32(ddlExcNo.SelectedValue));
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
                                dct2 = new DataColumn("UsedQty");
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
                                    drNew2["UsedQty"] = Dr2["UsedQty"];

                                    dstd2.Tables[0].Rows.Add(drNew2);
                                    dtddd2 = dstd2.Tables[0];
                                }

                                #endregion

                                ViewState["CurrentTable3"] = dtddd2;

                                GVFabricDetails.DataSource = dtddd2;
                                GVFabricDetails.DataBind();

                            }

                            #endregion
                        }
                        #region Summary

                        DataTable dtraw = new DataTable();
                        DataSet dsraw = new DataSet();
                        DataRow drraw;

                        dtraw.Columns.Add("Item");
                        dtraw.Columns.Add("ItemId");
                        dtraw.Columns.Add("Color");
                        dtraw.Columns.Add("ColorId");
                        dtraw.Columns.Add("AvlStock");
                        dtraw.Columns.Add("WantedRaw");

                        dsraw.Tables.Add(dtraw);

                        DataSet dsTotalFab = new DataSet();

                        for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                        {
                            HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
                            Label lblRecQty = (Label)GVItem.Rows[vLoop].FindControl("lblRecQty");

                            if (Convert.ToDouble(lblRecQty.Text) > 0)
                            {
                                DataSet dsRaw = objBs.FabDetailsforStyleNo(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(hdStyleNoId.Value), Convert.ToDouble(lblRecQty.Text), Convert.ToInt32(lblProcessforMasterId.Text));
                                if (dsRaw.Tables[0].Rows.Count > 0)
                                {
                                    dsTotalFab.Merge(dsRaw);
                                }
                            }
                        }
                        if (dsTotalFab.Tables.Count > 0)
                        {
                            if (dsTotalFab.Tables[0].Rows.Count > 0)
                            {
                                DataTable dtraws = new DataTable();

                                dtraws = dsTotalFab.Tables[0];

                                var result1 = from r in dtraws.AsEnumerable()
                                              group r by new { Item = r["Description"], ItemId = r["ItemMasterId"], Color = r["Color"], ColorId = r["ColorId"], AvlStock = r["AvlStock"] } into raw
                                              select new
                                              {
                                                  Item = raw.Key.Item,
                                                  ItemId = raw.Key.ItemId,
                                                  Color = raw.Key.Color,
                                                  ColorId = raw.Key.ColorId,
                                                  AvlStock = raw.Key.AvlStock,
                                                  total = raw.Sum(x => Convert.ToDouble(x["WantedRaw"])),
                                              };


                                foreach (var g in result1)
                                {
                                    drraw = dtraw.NewRow();

                                    drraw["Item"] = g.Item;
                                    drraw["ItemId"] = g.ItemId;
                                    drraw["Color"] = g.Color;
                                    drraw["ColorId"] = g.ColorId;
                                    drraw["AvlStock"] = g.AvlStock;
                                    drraw["WantedRaw"] = (g.total).ToString("f3");

                                    dsraw.Tables[0].Rows.Add(drraw);
                                }

                                GVFabricRawDetails.DataSource = dsraw.Tables[0];
                                GVFabricRawDetails.DataBind();

                            }
                        }


                        #endregion
                    }
                    else if (TYP == "VIEW")
                    {
                        btn_finishprocess.Visible = true;
                        string BuyerOrderMasterCuttingId = Request.QueryString.Get("BuyerOrderMasterCuttingId");
                        DataSet dsBuyerOrderCutting = objBs.GetBuyerOrderCutting_Master(Convert.ToInt32(BuyerOrderMasterCuttingId));
                        if (dsBuyerOrderCutting.Tables[0].Rows.Count > 0)
                        {
                            #region

                            btnSave.Text = "Save";
                            btnSave.Enabled = false;

                            ddlExcNo.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["BuyerOrderCutId"].ToString();
                            ddlExcNo.Enabled = false;
                            ddlCompanyName.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["CompanyId"].ToString();
                            ddlFinishingProcess.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["FinishingProcessId"].ToString();

                            txtMasterCuttingDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["MasterCuttingDate"]).ToString("dd/MM/yyyy");
                            txtRemarks.Text = dsBuyerOrderCutting.Tables[0].Rows[0]["Remarks"].ToString();

                            DataSet dsBuyerOrder = objBs.getBuyerOrdervalues(Convert.ToInt32(dsBuyerOrderCutting.Tables[0].Rows[0]["BuyerOrderId"].ToString()));
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

                            DataSet dsMaster1 = objBs.GetBuyerOrderCuttingMaster_Master(Convert.ToInt32(BuyerOrderMasterCuttingId));
                            if (dsMaster1.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsMaster1.Tables[0].Rows.Count; j++)
                                {
                                    chkCuttingBy.Items.FindByValue(dsMaster1.Tables[0].Rows[j]["EmployeeId"].ToString()).Selected = true;
                                }
                            }
                            DataSet dsProcess1 = objBs.GetBuyerOrderCuttingProcess_Master(Convert.ToInt32(BuyerOrderMasterCuttingId));
                            if (dsProcess1.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsProcess1.Tables[0].Rows.Count; j++)
                                {
                                    chkProcess.Items.FindByValue(dsProcess1.Tables[0].Rows[j]["ProcessId"].ToString()).Selected = true;
                                }
                            }

                            //////HttpCookie nameCookie1 = new HttpCookie("Name");
                            //////nameCookie1.Values["Name"] = dsBuyerOrderCutting.Tables[0].Rows[0]["MaxRowId"].ToString();
                            //////nameCookie1.Expires = DateTime.Now.AddDays(30);
                            //////Response.Cookies.Add(nameCookie1);

                            //////string HttpCookieValues = nameCookie1 != null ? nameCookie1.Value.Split('=')[1] : "undefined";

                            DataSet dsBuyerOrderCuttingrItems = objBs.GetTransBuyerOrderCuttingItems_Master(Convert.ToInt32(BuyerOrderMasterCuttingId));
                            if (dsBuyerOrderCuttingrItems.Tables[0].Rows.Count > 0)
                            {
                                DataSet dstd = new DataSet();
                                DataTable dtddd = new DataTable();
                                DataRow drNew;
                                DataColumn dct;
                                DataTable dttt = new DataTable();

                                #region

                                dct = new DataColumn("TransItemId");
                                dttt.Columns.Add(dct);
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

                                dct = new DataColumn("BalQty");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("RecQty");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("DmgQty");
                                dttt.Columns.Add(dct);

                                dstd.Tables.Add(dttt);

                                foreach (DataRow Dr in dsBuyerOrderCuttingrItems.Tables[0].Rows)
                                {
                                    drNew = dttt.NewRow();

                                    drNew["TransItemId"] = Dr["TransItemId"];
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

                                    drNew["BalQty"] = 0;// Dr["BalQty"];
                                    drNew["RecQty"] = Dr["RecQty"];
                                    drNew["DmgQty"] = Dr["DmgQty"];

                                    dstd.Tables[0].Rows.Add(drNew);
                                    dtddd = dstd.Tables[0];
                                }

                                #endregion

                                ViewState["CurrentTable1"] = dtddd;
                                GVItem.DataSource = dtddd;
                                GVItem.DataBind();

                            }

                            GVItem.Columns[6].Visible = false;


                            DataSet dsBuyerOrderCuttingSizes = objBs.GetTransBuyerOrderCuttingSizes_Master(Convert.ToInt32(BuyerOrderMasterCuttingId));
                            if (dsBuyerOrderCuttingSizes.Tables[0].Rows.Count > 0)
                            {
                                DataSet dstd1 = new DataSet();
                                DataTable dtddd1 = new DataTable();
                                DataRow drNew1;
                                DataColumn dct1;
                                DataTable dttt1 = new DataTable();

                                #region

                                dct1 = new DataColumn("TransSizeId");
                                dttt1.Columns.Add(dct1);
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

                                dct1 = new DataColumn("BalQty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("RecQty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("DmgQty");
                                dttt1.Columns.Add(dct1);

                                foreach (DataRow Dr in dsBuyerOrderCuttingSizes.Tables[0].Rows)
                                {
                                    drNew1 = dttt1.NewRow();

                                    drNew1["TransSizeId"] = Dr["TransSizeId"];
                                    drNew1["RowId"] = Dr["RowId"];
                                    drNew1["SizeId"] = Dr["SizeId"];
                                    drNew1["Size"] = Dr["Size"];
                                    drNew1["Ratio"] = Dr["Ratio"];
                                    drNew1["Qty"] = Dr["Qty"];
                                    drNew1["CQty"] = Dr["CQty"];
                                    drNew1["CRatio"] = Dr["CRatio"];

                                    drNew1["BalQty"] = 0;// Dr["BalQty"];
                                    drNew1["RecQty"] = Dr["RecQty"];
                                    drNew1["DmgQty"] = Dr["DmgQty"];

                                    dstd1.Tables[0].Rows.Add(drNew1);
                                    dtddd1 = dstd1.Tables[0];

                                }

                                #endregion

                                ViewState["CurrentTable2"] = dtddd1;
                            }

                            //    DataSet dsBuyerOrderCuttingFabric = objBs.GetTransBuyerOrderCuttingFabric_Master(Convert.ToInt32(BuyerOrderMasterCuttingId));
                            DataSet dsBuyerOrderCuttingFabric = objBs.GetTransBuyerOrderCuttingFabric_Master(Convert.ToInt32(BuyerOrderMasterCuttingId));
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
                                dct2 = new DataColumn("UsedQty");
                                dttt2.Columns.Add(dct2);

                                dstd2.Tables.Add(dttt2);

                                foreach (DataRow Dr2 in dsBuyerOrderCuttingFabric.Tables[0].Rows)
                                {
                                    drNew2 = dttt2.NewRow();

                                    drNew2["Item"] = Dr2["Item"];
                                    drNew2["ItemId"] = Dr2["ItemId"];
                                    drNew2["Color"] = Dr2["Color"];
                                    drNew2["ColorId"] = Dr2["ColorId"];
                                    drNew2["AvlStock"] = 0;// Dr2["AvlStock"];
                                    drNew2["RequiredStock"] = 0;// Dr2["RequiredStock"];
                                    drNew2["IssueQty"] = 0;// Dr2["IssueStock"];
                                    drNew2["UsedQty"] = Dr2["IssueStock"];

                                    dstd2.Tables[0].Rows.Add(drNew2);
                                    dtddd2 = dstd2.Tables[0];
                                }

                                #endregion

                                ViewState["CurrentTable3"] = dtddd2;

                                GVFabricDetails.DataSource = dtddd2;
                                GVFabricDetails.DataBind();

                            }

                            #endregion
                        }

                        GVFabricDetails.Columns[3].Visible = false;
                        GVFabricDetails.Columns[4].Visible = false;
                        GVFabricDetails.Columns[5].Visible = false;

                    }



                }
            }
        }

        protected void ddlExcNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "0" && ddlExcNo.SelectedValue != "Select ExcNo")
            {
                DataSet dsBuyerOrderCutting = objBs.GetBuyerOrderCutting(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (dsBuyerOrderCutting.Tables[0].Rows.Count > 0)
                {
                    txtMasterCuttingDate.Text = Convert.ToDateTime(dsBuyerOrderCutting.Tables[0].Rows[0]["CuttingDate"]).ToString("dd/MM/yyyy");
                    txtRemarks.Text = dsBuyerOrderCutting.Tables[0].Rows[0]["Remarks"].ToString();

                    ddlCompanyName.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["CompanyId"].ToString();
                    ddlFinishingProcess.SelectedValue = dsBuyerOrderCutting.Tables[0].Rows[0]["FinishingProcessId"].ToString();

                    DataSet dsBuyerOrder = objBs.getBuyerOrdervalues(Convert.ToInt32(dsBuyerOrderCutting.Tables[0].Rows[0]["BuyerOrderId"].ToString()));
                    if (dsBuyerOrder.Tables[0].Rows.Count > 0)
                    {
                        #region

                        ddlOrderType.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["OrderTypeId"].ToString();
                        ddlBuyerCode.SelectedValue = dsBuyerOrder.Tables[0].Rows[0]["BuyerCodeID"].ToString();
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

                    DataSet dsMaster1 = objBs.GetBuyerOrderCuttingMaster(Convert.ToInt32(ddlExcNo.SelectedValue));
                    if (dsMaster1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsMaster1.Tables[0].Rows.Count; j++)
                        {
                            chkCuttingBy.Items.FindByValue(dsMaster1.Tables[0].Rows[j]["EmployeeId"].ToString()).Selected = true;
                        }
                    }
                    DataSet dsProcess1 = objBs.GetBuyerOrderCuttingProcess(Convert.ToInt32(ddlExcNo.SelectedValue));
                    if (dsProcess1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsProcess1.Tables[0].Rows.Count; j++)
                        {
                            chkProcess.Items.FindByValue(dsProcess1.Tables[0].Rows[j]["ProcessId"].ToString()).Selected = true;
                        }
                    }

                    DataSet dsBuyerOrderCuttingrItems = objBs.GetTransBuyerOrderCuttingItems(Convert.ToInt32(ddlExcNo.SelectedValue));
                    if (dsBuyerOrderCuttingrItems.Tables[0].Rows.Count > 0)
                    {
                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

                        #region

                        dct = new DataColumn("TransItemId");
                        dttt.Columns.Add(dct);
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

                        dct = new DataColumn("BalQty");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("RecQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("DmgQty");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        foreach (DataRow Dr in dsBuyerOrderCuttingrItems.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();
                            drNew["TransItemId"] = Dr["TransBuyerOrderCuttingId"];
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

                            drNew["BalQty"] = Dr["BalQty"];
                            drNew["RecQty"] = 0;
                            drNew["DmgQty"] = 0;

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();

                    }
                    DataSet dsBuyerOrderCuttingSizes = objBs.GetTransBuyerOrderCuttingSizes(Convert.ToInt32(ddlExcNo.SelectedValue));
                    if (dsBuyerOrderCuttingSizes.Tables[0].Rows.Count > 0)
                    {
                        DataSet dstd1 = new DataSet();
                        DataTable dtddd1 = new DataTable();
                        DataRow drNew1;
                        DataColumn dct1;
                        DataTable dttt1 = new DataTable();

                        #region

                        dct1 = new DataColumn("TransSizeId");
                        dttt1.Columns.Add(dct1);
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

                        dct1 = new DataColumn("BalQty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RecQty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("DmgQty");
                        dttt1.Columns.Add(dct1);

                        dstd1.Tables.Add(dttt1);

                        foreach (DataRow Dr in dsBuyerOrderCuttingSizes.Tables[0].Rows)
                        {
                            drNew1 = dttt1.NewRow();

                            drNew1["TransSizeId"] = Dr["TransBuyerOrderCuttingSizeId"];
                            drNew1["RowId"] = Dr["RowId"];
                            drNew1["SizeId"] = Dr["SizeId"];
                            drNew1["Size"] = Dr["Size"];
                            drNew1["Ratio"] = Dr["Ratio"];
                            drNew1["Qty"] = Dr["Qty"];
                            drNew1["CQty"] = Dr["CQty"];
                            drNew1["CRatio"] = Dr["CRatio"];

                            drNew1["BalQty"] = Dr["BalQty"];
                            drNew1["RecQty"] = 0;
                            drNew1["DmgQty"] = 0;

                            dstd1.Tables[0].Rows.Add(drNew1);
                            dtddd1 = dstd1.Tables[0];

                        }

                        #endregion

                        ViewState["CurrentTable2"] = dtddd1;
                    }

                    DataSet dsBuyerOrderCuttingFabric = objBs.GetTransBuyerOrderCuttingFabric(Convert.ToInt32(ddlExcNo.SelectedValue));
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
                        dct2 = new DataColumn("UsedQty");
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
                            drNew2["UsedQty"] = Dr2["UsedQty"];

                            dstd2.Tables[0].Rows.Add(drNew2);
                            dtddd2 = dstd2.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable3"] = dtddd2;

                        GVFabricDetails.DataSource = dtddd2;
                        GVFabricDetails.DataBind();

                    }
                }
            }
            else
            {

                ViewState["CurrentTable1"] = null;
                GVItem.DataSource = null;
                GVItem.DataBind();

                ViewState["CurrentTable2"] = null;

                ViewState["CurrentTable3"] = null;
                GVFabricDetails.DataSource = null;
                GVFabricDetails.DataBind();

            }
        }


        protected void Finish_Process(object sender, EventArgs e)
        {
            string BuyerOrderCutId = Request.QueryString.Get("BuyerOrderMasterCuttingId");

            if (ddlFinishingProcess.SelectedValue == "FinishingProcess")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Valid Finishing Process.Thank you!!!.')", true);
                return;
            }
            else
            {
                // Update 

                int iss = objBs.UpdatefinishingPRocess(BuyerOrderCutId,ddlFinishingProcess.SelectedValue);
                Response.Redirect("BuyerOrderMasterCutting.aspx?TYP=VIEW&&BuyerOrderMasterCuttingId=" + BuyerOrderCutId);
            }

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
            if (e.CommandName == "Modify")
            {
                GVSizesView.DataSource = null;
                GVSizesView.DataBind();

                if (e.CommandArgument.ToString() != "")
                {
                    #region

                    DataTable DTGVSizeDetails = (DataTable)ViewState["CurrentTable1"];

                    DataRow[] RowsGVSizeDetails = DTGVSizeDetails.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    RowId.Text = e.CommandArgument.ToString();

                    TransItemId.Text = RowsGVSizeDetails[0]["TransItemId"].ToString();

                    StyleNo.Text = RowsGVSizeDetails[0]["StyleNo"].ToString();
                    StyleNoId.Text = RowsGVSizeDetails[0]["StyleNoId"].ToString();
                    Description.Text = RowsGVSizeDetails[0]["Description"].ToString();

                    Color.Text = RowsGVSizeDetails[0]["Color"].ToString();
                    ColorId.Text = RowsGVSizeDetails[0]["ColorId"].ToString();

                    Rate.Text = RowsGVSizeDetails[0]["Rate"].ToString();
                    Qty.Text = RowsGVSizeDetails[0]["Qty"].ToString();
                    CQty.Text = RowsGVSizeDetails[0]["CQty"].ToString();

                    Cratio.Text = RowsGVSizeDetails[0]["Cratio"].ToString();
                    AffectedQty.Text = RowsGVSizeDetails[0]["AffectedQty"].ToString();

                    RangeId.Text = RowsGVSizeDetails[0]["RangeId"].ToString();
                    Sizes.Text = RowsGVSizeDetails[0]["Size"].ToString();

                    BalQty.Text = RowsGVSizeDetails[0]["BalQty"].ToString();

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();

                    DataRow drNew1;
                    DataColumn dct1;

                    DataTable dttt1 = new DataTable();

                    dct1 = new DataColumn("TransSizeId");
                    dttt1.Columns.Add(dct1);
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

                    dct1 = new DataColumn("BalQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("RecQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("DmgQty");
                    dttt1.Columns.Add(dct1);

                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["TransSizeId"] = RowsGVSizeQty[i]["TransSizeId"].ToString();
                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["Ratio"] = RowsGVSizeQty[i]["Ratio"].ToString();
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();
                        drNew1["CQty"] = RowsGVSizeQty[i]["CQty"].ToString();
                        drNew1["CRatio"] = RowsGVSizeQty[i]["CRatio"].ToString();

                        drNew1["BalQty"] = RowsGVSizeQty[i]["BalQty"].ToString();
                        drNew1["RecQty"] = RowsGVSizeQty[i]["RecQty"].ToString();
                        drNew1["DmgQty"] = RowsGVSizeQty[i]["DmgQty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    GVSizes.DataSource = dstd1;
                    GVSizes.DataBind();




                    #endregion
                }
            }
            else if (e.CommandName == "View")
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

                    dct1 = new DataColumn("BalQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("RecQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("DmgQty");
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

                        drNew1["BalQty"] = RowsGVSizeQty[i]["BalQty"].ToString();
                        drNew1["RecQty"] = RowsGVSizeQty[i]["RecQty"].ToString();
                        drNew1["DmgQty"] = RowsGVSizeQty[i]["DmgQty"].ToString();

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
            if (GVSizes.Rows.Count > 0)
            {
                #region CurrentTable Removed

                DataTable DTGVSizeDetails = (DataTable)ViewState["CurrentTable1"];

                DataRow[] DRItem = DTGVSizeDetails.Select("RowId='" + RowId.Text + "'");
                for (int i = 0; i < DRItem.Length; i++)
                    DRItem[i].Delete();
                DTGVSizeDetails.AcceptChanges();

                ViewState["CurrentTable1"] = DTGVSizeDetails;

                DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];

                DataRow[] DRSize = DTGVSizeQty.Select("RowId='" + RowId.Text + "'");
                for (int i = 0; i < DRSize.Length; i++)
                    DRSize[i].Delete();
                DTGVSizeQty.AcceptChanges();

                ViewState["CurrentTable2"] = DTGVSizeQty;

                #endregion

                double RecQty = 0; double DmgQty = 0;
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    TextBox txtRecQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRecQty");
                    TextBox txtDmgQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDmgQty");

                    if (txtRecQty.Text == "")
                        txtRecQty.Text = "0";
                    if (txtDmgQty.Text == "")
                        txtDmgQty.Text = "0";

                    RecQty += Convert.ToDouble(txtRecQty.Text);
                    DmgQty += Convert.ToDouble(txtDmgQty.Text);
                }


                // string HttpCookieValue = "";

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                #region

                dct = new DataColumn("TransItemId");
                dttt.Columns.Add(dct);
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
                dct = new DataColumn("Size");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CRatio");
                dttt.Columns.Add(dct);

                dct = new DataColumn("BalQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RecQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DmgQty");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                if (ViewState["CurrentTable1"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable1"];

                    //////HttpCookie nameCookie = Request.Cookies["Name"];
                    //////string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    //////string adad = (Convert.ToInt32(name) + 1).ToString();

                    ////////Set the Cookie value.
                    //////nameCookie.Values["Name"] = adad;
                    ////////Set the Expiry date.
                    //////nameCookie.Expires = DateTime.Now.AddDays(30);
                    ////////Add the Cookie to Browser.
                    //////Response.Cookies.Add(nameCookie);

                    //////HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                    drNew = dttt.NewRow();
                    drNew["TransItemId"] = TransItemId.Text;
                    drNew["RowId"] = RowId.Text;// nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    drNew["StyleNo"] = StyleNo.Text;
                    drNew["StyleNoId"] = StyleNoId.Text;
                    drNew["Description"] = Description.Text;
                    drNew["ColorId"] = ColorId.Text;
                    drNew["Color"] = Color.Text;
                    drNew["Rate"] = Rate.Text;
                    drNew["Qty"] = Qty.Text;
                    drNew["CQty"] = CQty.Text;
                    drNew["AffectedQty"] = AffectedQty.Text;
                    drNew["RangeId"] = RangeId.Text;
                    drNew["Size"] = Sizes.Text;
                    drNew["CRatio"] = Cratio.Text;

                    drNew["BalQty"] = BalQty.Text;
                    drNew["RecQty"] = RecQty;
                    drNew["DmgQty"] = DmgQty;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                    dtddd.Merge(dt);

                }
                else
                {

                    //////HttpCookie nameCookie = new HttpCookie("Name");
                    //////nameCookie.Values["Name"] = "1";
                    //////nameCookie.Expires = DateTime.Now.AddDays(30);
                    //////Response.Cookies.Add(nameCookie);

                    //////HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                    drNew = dttt.NewRow();
                    drNew["TransItemId"] = TransItemId.Text;
                    drNew["RowId"] = RowId.Text;// nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    drNew["StyleNo"] = StyleNo.Text;
                    drNew["StyleNoId"] = StyleNoId.Text;
                    drNew["Description"] = Description.Text;
                    drNew["ColorId"] = ColorId.Text;
                    drNew["Color"] = Color.Text;
                    drNew["Rate"] = Rate.Text;
                    drNew["Qty"] = Qty.Text;
                    drNew["CQty"] = CQty.Text;
                    drNew["AffectedQty"] = AffectedQty.Text;
                    drNew["RangeId"] = RangeId.Text;
                    drNew["Size"] = Sizes.Text;
                    drNew["CRatio"] = Cratio.Text;

                    drNew["BalQty"] = BalQty.Text;
                    drNew["RecQty"] = RecQty;
                    drNew["DmgQty"] = DmgQty;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                }

                #endregion

                ViewState["CurrentTable1"] = dtddd;
                GVItem.DataSource = dtddd;
                GVItem.DataBind();

                DataSet dstd1 = new DataSet();
                DataTable dtddd1 = new DataTable();
                DataRow drNew1;
                DataColumn dct1;
                DataTable dttt1 = new DataTable();

                #region

                dct1 = new DataColumn("TransSizeId");
                dttt1.Columns.Add(dct1);
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

                dct1 = new DataColumn("BalQty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RecQty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("DmgQty");
                dttt1.Columns.Add(dct1);

                dstd1.Tables.Add(dttt1);


                if (ViewState["CurrentTable2"] != null)
                {
                    //HttpCookie nameCookie = Request.Cookies["Name"];

                    DataTable dt1 = (DataTable)ViewState["CurrentTable2"];

                    for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                    {
                        HiddenField hdTransSizeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdTransSizeId");

                        HiddenField hdRatio = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdRatio");
                        HiddenField hdQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdQty");
                        HiddenField hdCQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdCQty");
                        HiddenField hdCRatio = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdCRatio");
                        HiddenField hdBalQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdBalQty");

                        HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                        Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                        TextBox txtRecQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRecQty");
                        TextBox txtDmgQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDmgQty");

                        if (txtRecQty.Text == "")
                            txtRecQty.Text = "0";
                        if (txtDmgQty.Text == "")
                            txtDmgQty.Text = "0";

                        drNew1 = dttt1.NewRow();

                        drNew1["TransSizeId"] = hdTransSizeId.Value;

                        drNew1["RowId"] = RowId.Text;// HttpCookieValue;

                        drNew1["SizeId"] = hdSize.Value;
                        drNew1["Size"] = lblSize.Text;
                        drNew1["Ratio"] = hdRatio.Value;
                        drNew1["Qty"] = hdQty.Value;
                        drNew1["CQty"] = hdCQty.Value;
                        drNew1["CRatio"] = hdCRatio.Value;

                        drNew1["BalQty"] = hdBalQty.Value;
                        drNew1["RecQty"] = txtRecQty.Text;
                        drNew1["DmgQty"] = txtDmgQty.Text;

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }
                    dtddd1.Merge(dt1);
                }
                else
                {
                    //  HttpCookie nameCookie = Request.Cookies["Name"];

                    for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                    {
                        HiddenField hdTransSizeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdTransSizeId");

                        HiddenField hdRatio = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdRatio");
                        HiddenField hdQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdQty");
                        HiddenField hdCQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdCQty");
                        HiddenField hdCRatio = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdCRatio");
                        HiddenField hdBalQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdBalQty");

                        HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                        Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                        TextBox txtRecQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRecQty");
                        TextBox txtDmgQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDmgQty");

                        drNew1 = dttt1.NewRow();

                        //  string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                        drNew1["TransSizeId"] = hdTransSizeId.Value;
                        drNew1["RowId"] = RowId.Text;// HttpCookieValue;

                        drNew1["SizeId"] = hdSize.Value;
                        drNew1["Size"] = lblSize.Text;
                        drNew1["Ratio"] = hdRatio.Value;
                        drNew1["Qty"] = hdQty.Value;
                        drNew1["CQty"] = hdCQty.Value;
                        drNew1["CRatio"] = hdCRatio.Value;

                        drNew1["BalQty"] = hdBalQty.Value;
                        drNew1["RecQty"] = txtRecQty.Text;
                        drNew1["DmgQty"] = txtDmgQty.Text;


                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }
                }

                #endregion

                ViewState["CurrentTable2"] = dtddd1;

                #region Summary

                DataTable dtraw = new DataTable();
                DataSet dsraw = new DataSet();
                DataRow drraw;

                dtraw.Columns.Add("Item");
                dtraw.Columns.Add("ItemId");
                dtraw.Columns.Add("Color");
                dtraw.Columns.Add("ColorId");
                dtraw.Columns.Add("AvlStock");
                dtraw.Columns.Add("WantedRaw");

                dsraw.Tables.Add(dtraw);

                DataSet dsTotalFab = new DataSet();

                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
                    Label lblRecQty = (Label)GVItem.Rows[vLoop].FindControl("lblRecQty");

                    if (Convert.ToDouble(lblRecQty.Text) > 0)
                    {
                        DataSet dsRaw = objBs.FabDetailsforStyleNo(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(hdStyleNoId.Value), Convert.ToDouble(lblRecQty.Text), Convert.ToInt32(lblProcessforMasterId.Text));
                        if (dsRaw.Tables[0].Rows.Count > 0)
                        {
                            dsTotalFab.Merge(dsRaw);
                        }
                    }
                }
                if (dsTotalFab.Tables.Count > 0)
                {
                    if (dsTotalFab.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtraws = new DataTable();

                        dtraws = dsTotalFab.Tables[0];

                        var result1 = from r in dtraws.AsEnumerable()
                                      group r by new { Item = r["Description"], ItemId = r["ItemMasterId"], Color = r["Color"], ColorId = r["ColorId"], AvlStock = r["AvlStock"] } into raw
                                      select new
                                      {
                                          Item = raw.Key.Item,
                                          ItemId = raw.Key.ItemId,
                                          Color = raw.Key.Color,
                                          ColorId = raw.Key.ColorId,
                                          AvlStock = raw.Key.AvlStock,
                                          total = raw.Sum(x => Convert.ToDouble(x["WantedRaw"])),
                                      };


                        foreach (var g in result1)
                        {
                            drraw = dtraw.NewRow();

                            drraw["Item"] = g.Item;
                            drraw["ItemId"] = g.ItemId;
                            drraw["Color"] = g.Color;
                            drraw["ColorId"] = g.ColorId;
                            drraw["AvlStock"] = g.AvlStock;
                            drraw["WantedRaw"] = (g.total).ToString("f3");

                            dsraw.Tables[0].Rows.Add(drraw);
                        }

                        GVFabricRawDetails.DataSource = dsraw.Tables[0];
                        GVFabricRawDetails.DataBind();

                    }
                }


                #endregion
            }

            GVSizes.DataSource = null;
            GVSizes.DataBind();
            GVSizesView.DataSource = null;
            GVSizesView.DataBind();

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (chkCuttingBy.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Cutting By.')", true);
                chkCuttingBy.Focus();
                return;
            }
            if (chkProcess.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Process.')", true);
                chkProcess.Focus();
                return;
            }
            if (GVFabricDetails.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Fabric Details.')", true);
                return;
            }
            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Style Details.')", true);
                return;
            }
            if (GVFabricRawDetails.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Fabric Details.')", true);
                return;
            }

            //for (int vLoop = 0; vLoop < GVFabricRawDetails.Rows.Count; vLoop++)
            //{
            //    HiddenField hdAvlStock = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdAvlStock");
            //    HiddenField hdWantedRaw = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdWantedRaw");

            //    if (Convert.ToDouble(hdAvlStock.Value) < Convert.ToDouble(hdWantedRaw.Value))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check WantedRaw in Row " + (Convert.ToInt32(vLoop) + 1) + ".')", true);
            //        return;
            //    }
            //}

            //HttpCookie nameCookie = Request.Cookies["Name"];
            //string MaxRowId = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
            string MaxRowId = "0";


            DateTime MasterCuttingDate = DateTime.ParseExact(txtMasterCuttingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnSave.Text == "Save")
            {
                string TYP = Request.QueryString.Get("TYP");
                if (TYP == "REC")
                {
                    #region

                    string BuyerOrderCutId = Request.QueryString.Get("BuyerOrderCutId");
                    DataSet dsBuyerOrderMasterCuttingId = objBs.GetBuyerOrderMasterCuttingId(Convert.ToInt32(BuyerOrderCutId));
                    int BuyerOrderMasterCuttingId = Convert.ToInt32(dsBuyerOrderMasterCuttingId.Tables[0].Rows[0]["BuyerOrderMasterCuttingId"].ToString());

                    int TransHistoryId = objBs.InsertBuyerOrderCuttingHistory_Master(MasterCuttingDate, BuyerOrderMasterCuttingId, Convert.ToInt32(ddlExcNo.SelectedValue), MaxRowId, txtRemarks.Text, Convert.ToInt32(ddlCompanyName.SelectedValue), Convert.ToInt32(ddlFinishingProcess.SelectedValue));

                    DataTable CurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                    int TransItemId = objBs.InsertTransBuyerOrderCuttingItems_Master(BuyerOrderMasterCuttingId, CurrentTable1, Convert.ToInt32(lblProcessforMasterId.Text));

                    DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
                    int TransSizeId = objBs.InsertTransBuyerOrderCuttingSizes_Master(BuyerOrderMasterCuttingId, CurrentTable2, Convert.ToInt32(lblProcessforMasterId.Text));

                    for (int vLoop = 0; vLoop < GVFabricRawDetails.Rows.Count; vLoop++)
                    {
                        HiddenField hdItemId = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdColorId");
                        HiddenField hdAvlStock = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdAvlStock");
                        HiddenField hdWantedRaw = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdWantedRaw");

                        int CuttingFabricId = objBs.InsertTransBuyerOrderCuttingFabric_Master(Convert.ToInt32(ddlExcNo.SelectedValue), BuyerOrderMasterCuttingId, Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdAvlStock.Value), Convert.ToDouble(hdWantedRaw.Value));
                    }

                    #endregion
                }
                else
                {
                    #region

                    int BuyerOrderMasterCuttingId = objBs.InsertBuyerOrderCutting_Master(MasterCuttingDate, Convert.ToInt32(ddlExcNo.SelectedValue), MaxRowId, txtRemarks.Text, Convert.ToInt32(ddlCompanyName.SelectedValue), Convert.ToInt32(ddlFinishingProcess.SelectedValue));

                    int TransHistoryId = objBs.InsertBuyerOrderCuttingHistory_Master(MasterCuttingDate, BuyerOrderMasterCuttingId, Convert.ToInt32(ddlExcNo.SelectedValue), MaxRowId, txtRemarks.Text, Convert.ToInt32(ddlCompanyName.SelectedValue), Convert.ToInt32(ddlFinishingProcess.SelectedValue));

                    foreach (ListItem CuttingBy in chkCuttingBy.Items)
                    {
                        if (CuttingBy.Selected)
                        {
                            int TransMasterId = objBs.InsertBuyerOrderCuttingMaster_Master(BuyerOrderMasterCuttingId, Convert.ToInt32(CuttingBy.Value));
                        }
                    }
                    foreach (ListItem Process in chkProcess.Items)
                    {
                        if (Process.Selected)
                        {
                            int TransMasterId = objBs.InsertBuyerOrderCuttingProcess_Master(BuyerOrderMasterCuttingId, Convert.ToInt32(Process.Value));
                        }
                    }


                    DataTable CurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                    int TransItemId = objBs.InsertTransBuyerOrderCuttingItems_Master(BuyerOrderMasterCuttingId, CurrentTable1, Convert.ToInt32(lblProcessforMasterId.Text));

                    DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
                    int TransSizeId = objBs.InsertTransBuyerOrderCuttingSizes_Master(BuyerOrderMasterCuttingId, CurrentTable2, Convert.ToInt32(lblProcessforMasterId.Text));

                    //DataTable CurrentTable3 = (DataTable)ViewState["CurrentTable3"];
                    //int CuttingFabricId = objBs.InsertTransBuyerOrderCuttingFabric_Master(BuyerOrderMasterCuttingId, CurrentTable3);

                    for (int vLoop = 0; vLoop < GVFabricRawDetails.Rows.Count; vLoop++)
                    {
                        HiddenField hdItemId = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdColorId");
                        HiddenField hdAvlStock = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdAvlStock");
                        HiddenField hdWantedRaw = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdWantedRaw");

                        int CuttingFabricId = objBs.InsertTransBuyerOrderCuttingFabric_Master(Convert.ToInt32(ddlExcNo.SelectedValue), BuyerOrderMasterCuttingId, Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdAvlStock.Value), Convert.ToDouble(hdWantedRaw.Value));
                    }

                    #endregion
                }

            }

            Response.Redirect("BuyerOrderMasterCuttingGrid.aspx");

        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderMasterCuttingGrid.aspx");
        }


    }
}
