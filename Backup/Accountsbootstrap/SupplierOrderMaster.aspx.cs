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
    public partial class SupplierOrderMaster : System.Web.UI.Page
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

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtShipmentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtShipmentDate_OnTextChanged(sender, e);

                #region

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
                DataSet dsCurrency = objBs.gridCurrency();
                if (dsCurrency.Tables[0].Rows.Count > 0)
                {
                    ddlCurrency.DataSource = dsCurrency.Tables[0];
                    ddlCurrency.DataTextField = "CurrencyName";
                    ddlCurrency.DataValueField = "CurrencyId";
                    ddlCurrency.DataBind();
                    ddlCurrency.SelectedValue = lblCurrency.Text;
                }
                DataSet dssize = objBs.GetOrderTypenew("1");
                if (dssize.Tables[0].Rows.Count > 0)
                {
                    ddlOrderType.DataSource = dssize.Tables[0];
                    ddlOrderType.DataTextField = "OrderType";
                    ddlOrderType.DataValueField = "OrderTypeId";
                    ddlOrderType.DataBind();
                }

                DataSet dset = objBs.GetSetNo();
                if (dset.Tables[0].Rows.Count > 0)
                {
                    drpset.DataSource = dset.Tables[0];
                    drpset.DataTextField = "SetName";
                    drpset.DataValueField = "SetNo";
                    drpset.DataBind();
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

                //DataSet dsStyleNo = objBs.GetStyleNo_BO();
                //if (dsStyleNo.Tables[0].Rows.Count > 0)
                //{
                //    ddlStyle.DataSource = dsStyleNo.Tables[0];
                //    ddlStyle.DataTextField = "StyleNo";
                //    ddlStyle.DataValueField = "SamplingCostingId";
                //    ddlStyle.DataBind();
                //    ddlStyle.Items.Insert(0, "Style");
                //}
                ddlStyle.Items.Insert(0, "Style");


                DataSet dsColor = objBs.gridColor();
                if (dsColor.Tables[0].Rows.Count > 0)
                {
                    ddlColor.DataSource = dsColor.Tables[0];
                    ddlColor.DataTextField = "Color";
                    ddlColor.DataValueField = "ColorId";
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0, "Color");
                }
                DataSet dsSizeRange = objBs.gridSizeRange();
                if (dsSizeRange.Tables[0].Rows.Count > 0)
                {
                    ddlSize.DataSource = dsSizeRange.Tables[0];
                    ddlSize.DataTextField = "Range";
                    ddlSize.DataValueField = "RangeId";
                    ddlSize.DataBind();
                    ddlSize.Items.Insert(0, "Size");
                }

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

                #endregion

                string BuyerOrderId = Request.QueryString.Get("BuyerOrderId");
                if (BuyerOrderId != "" && BuyerOrderId != null)
                {
                    DataSet dsBuyerOrde = objBs.getBuyerOrdervalues(Convert.ToInt32(BuyerOrderId));
                    if (dsBuyerOrde.Tables[0].Rows.Count > 0)
                    {
                        #region

                        #region

                        ddlOrderType.SelectedValue = dsBuyerOrde.Tables[0].Rows[0]["OrderTypeId"].ToString();
                        ddlOrderType.Enabled = false;
                        ddlBuyerCode.SelectedValue = dsBuyerOrde.Tables[0].Rows[0]["BuyerCodeID"].ToString();
                        ddlBuyerCode.Enabled = false;

                        DataSet dsStyleNo = objBs.GetStyleNo_BO(Convert.ToInt32(ddlBuyerCode.SelectedValue));
                        if (dsStyleNo.Tables[0].Rows.Count > 0)
                        {
                            ddlStyle.DataSource = dsStyleNo.Tables[0];
                            ddlStyle.DataTextField = "StyleNo";
                            ddlStyle.DataValueField = "SamplingCostingId";
                            ddlStyle.DataBind();
                            ddlStyle.Items.Insert(0, "Style");
                        }
                        else
                        {
                            ddlStyle.Items.Insert(0, "Style");
                        }

                        ddlBuyerName.SelectedValue = dsBuyerOrde.Tables[0].Rows[0]["BuyerCodeID"].ToString();
                        txtExcNo.Text = dsBuyerOrde.Tables[0].Rows[0]["ExcNo"].ToString();
                        txtBuyerPONo.Text = dsBuyerOrde.Tables[0].Rows[0]["BuyerPoNo"].ToString();
                        ddlFabricCode.SelectedValue = dsBuyerOrde.Tables[0].Rows[0]["FabricCodeId"].ToString();
                        DataSet dsgetfabricname = objBs.getiItemTypeforHead_fabricnamelist(ddlFabricCode.SelectedValue);
                        if (dsgetfabricname.Tables[0].Rows.Count > 0)
                        {
                            ddlFabricName.DataSource = dsgetfabricname.Tables[0];
                            ddlFabricName.DataTextField = "description";
                            ddlFabricName.DataValueField = "itemmasterid";
                            ddlFabricName.DataBind();
                            ddlFabricName.Items.Insert(0, "FabricName");
                        }
                        ddlFabricName.SelectedValue = dsBuyerOrde.Tables[0].Rows[0]["FabricDescId"].ToString();

                        txtOrderDate.Text = Convert.ToDateTime(dsBuyerOrde.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                        txtShipmentDate.Text = Convert.ToDateTime(dsBuyerOrde.Tables[0].Rows[0]["ShipmentDate"]).ToString("dd/MM/yyyy");
                        txtDeliveryDate.Text = Convert.ToDateTime(dsBuyerOrde.Tables[0].Rows[0]["DeliveryDate"]).ToString("dd/MM/yyyy");
                        txtODeliveryDate.Text = Convert.ToDateTime(dsBuyerOrde.Tables[0].Rows[0]["DeliveryDate"]).ToString("dd/MM/yyyy");

                        ddlShipmentMode.SelectedValue = dsBuyerOrde.Tables[0].Rows[0]["ShipmentModeId"].ToString();
                        ddlPaymentMode.SelectedValue = dsBuyerOrde.Tables[0].Rows[0]["PaymentModeId"].ToString();
                        txtPaymentTerms.Text = dsBuyerOrde.Tables[0].Rows[0]["PaymentTerms"].ToString();
                        txtBytheWayof.Text = dsBuyerOrde.Tables[0].Rows[0]["BytheWayof"].ToString();
                        ddlCurrency.SelectedValue = dsBuyerOrde.Tables[0].Rows[0]["CurrencyId"].ToString();
                        txtAmount.Text = Convert.ToDouble(dsBuyerOrde.Tables[0].Rows[0]["Amount"]).ToString("f2");

                        txtRemarks.Text = dsBuyerOrde.Tables[0].Rows[0]["Remarks"].ToString();


                        if (dsBuyerOrde.Tables[0].Rows[0]["Shipped"].ToString() == "True")
                        {
                            chkShipped.Checked = true;
                        }
                        if (dsBuyerOrde.Tables[0].Rows[0]["Cancel"].ToString() == "True")
                        {
                            chkCancel.Checked = true;
                        }
                        if (dsBuyerOrde.Tables[0].Rows[0]["Approved"].ToString() == "True")
                        {
                            chkApproved.Checked = true;
                        }
                        if (dsBuyerOrde.Tables[0].Rows[0]["Hold"].ToString() == "True")
                        {
                            chkHold.Checked = true;
                        }
                        if (dsBuyerOrde.Tables[0].Rows[0]["Lock"].ToString() == "True")
                        {
                            chkLock.Checked = true;
                        }

                        #endregion
                        btnSave.Text = "Update";

                        HttpCookie nameCookie1 = new HttpCookie("Name");
                        nameCookie1.Values["Name"] = dsBuyerOrde.Tables[0].Rows[0]["MaxRowId"].ToString();
                        nameCookie1.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(nameCookie1);

                        string HttpCookieValues = nameCookie1 != null ? nameCookie1.Value.Split('=')[1] : "undefined";

                        DataSet dsBuyerOrderItems = objBs.getTransBuyerOrderItemsvalues(Convert.ToInt32(BuyerOrderId));
                        if (dsBuyerOrderItems.Tables[0].Rows.Count > 0)
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
                            dct = new DataColumn("Size");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("CQty");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("CRatio");
                            dttt.Columns.Add(dct);




                            dstd.Tables.Add(dttt);

                            foreach (DataRow Dr in dsBuyerOrderItems.Tables[0].Rows)
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

                                drNew["CQty"] = Dr["CQty"];
                                drNew["CRatio"] = Dr["CRatio"];


                                drNew["AffectedQty"] = Dr["AffectedQty"];
                                drNew["RangeId"] = Dr["RangeId"];
                                drNew["Size"] = Dr["Range"];

                                dstd.Tables[0].Rows.Add(drNew);
                                dtddd = dstd.Tables[0];
                            }

                            #endregion

                            ViewState["CurrentTable1"] = dtddd;
                            GVItem.DataSource = dtddd;
                            GVItem.DataBind();

                            string cond = "";

                            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                            {
                                HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
                                cond += " d.samplingcostingid='" + hdStyleNoId.Value + "' ,";


                            }
                            cond = cond.TrimEnd(',');
                            cond = cond.Replace(",", "or");
                            if (cond != "")
                            {


                                DataSet dsDCConditions = objBs.getiItemTypeforHead("ShowLabelDetails", cond);
                                if (dsDCConditions.Tables[0].Rows.Count > 0)
                                {
                                    gvLabels.DataSource = dsDCConditions;
                                    gvLabels.DataBind();
                                }
                            }


                        }
                        DataSet dsBuyerOrderSizes = objBs.getTransBuyerOrderSizesvalues(Convert.ToInt32(BuyerOrderId));
                        if (dsBuyerOrderSizes.Tables[0].Rows.Count > 0)
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

                            foreach (DataRow Dr in dsBuyerOrderSizes.Tables[0].Rows)
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


                        DataSet dsBuyerOrderLabel = objBs.getTransBuyerOrderLabelvalues(Convert.ToInt32(BuyerOrderId));
                        if (dsBuyerOrderLabel.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < gvLabels.Rows.Count; i++)
                            {
                                HiddenField hdItemId = (HiddenField)gvLabels.Rows[i].FindControl("hdItemId");

                                CheckBox chkLabelItem = (CheckBox)gvLabels.Rows[i].FindControl("chkLabelItem");
                                TextBox txtLabelText = (TextBox)gvLabels.Rows[i].FindControl("txtLabelText");

                                Image img_Photo = (Image)gvLabels.Rows[i].FindControl("img_Photo");
                                Label lblFile_Path = (Label)gvLabels.Rows[i].FindControl("lblFile_Path");

                                DataRow[] RowsGVSizeDetails = dsBuyerOrderLabel.Tables[0].Select("LabelItemId='" + hdItemId.Value + "'");
                                if (RowsGVSizeDetails.Length > 0)
                                {
                                    chkLabelItem.Checked = true;
                                    txtLabelText.Text = RowsGVSizeDetails[0]["LabelText"].ToString();

                                    img_Photo.ImageUrl = RowsGVSizeDetails[0]["LabelImage"].ToString();
                                    lblFile_Path.Text = RowsGVSizeDetails[0]["LabelImage"].ToString();
                                }
                                //hdItemId.Value.Items.FindByValue(dsBuyerOrderLabel.Tables[0].Rows[i]["CategoryId"].ToString()).Selected = true;

                            }
                        }

                        #endregion

                        DataSet ds_RSChecking = objBs.Check_Precutting(Convert.ToInt32(BuyerOrderId));
                        if (ds_RSChecking.Tables[0].Rows.Count > 0)
                        {
                            btnupdateShipped.Visible = true;
                            btnSave.Enabled = false;

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Pre Cutting was Assigned cannot be Update,you can update OrderDate,ShipmentDate,DeliveryDate,Cancel,Hold,Shipped only ,.')", true);
                            return;
                        }
                    }

                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void itemtype_chnaged(object sender, EventArgs e)
        {
            setwise.Visible = false;
            if (raditemtype.SelectedValue == "1")
            {
            }
            else
            {
                //setwise.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Set Wise Is In Process.')", true);
                return;
            }
        }

        protected void Style_changed(object sender, EventArgs e)
        {
            if (ddlStyle.SelectedValue == "Style")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Valid Style.')", true);
                ddlStyle.Focus();
                lblitemdesc.Text = "";
                return;
            }
            else
            {
                // ddlStyle.Items.Insert(0, "Style");

                DataSet getstyleno = objBs.GetStyleNo_Description(ddlStyle.SelectedValue);
                if (getstyleno.Tables[0].Rows.Count > 0)
                {
                    lblitemdesc.Text = getstyleno.Tables[0].Rows[0]["Description"].ToString();

                }
            }
        }

        protected void btnStyleRefresh_OnClick(object sender, EventArgs e)
        {
            ddlStyle.Items.Clear();

            if (ddlBuyerCode.SelectedValue != "" && ddlBuyerCode.SelectedValue != "0" && ddlBuyerCode.SelectedValue != "BuyerCode")
            {
                DataSet dsStyleNo = objBs.GetStyleNo_BO(Convert.ToInt32(ddlBuyerCode.SelectedValue));
                if (dsStyleNo.Tables[0].Rows.Count > 0)
                {
                    ddlStyle.DataSource = dsStyleNo.Tables[0];
                    ddlStyle.DataTextField = "StyleNo";
                    ddlStyle.DataValueField = "SamplingCostingId";
                    ddlStyle.DataBind();
                    ddlStyle.Items.Insert(0, "Style");
                }
                else
                {
                    ddlStyle.Items.Insert(0, "Style");
                }
            }
            else
            {
                ddlStyle.Items.Insert(0, "Style");
            }

            ViewState["CurrentTable1"] = null;
            GVItem.DataSource = null;
            GVItem.DataBind();
            ViewState["CurrentTable2"] = null;
            GVSizesView.DataSource = null;
            GVSizesView.DataBind();

        }
        protected void btnColorRefresh_OnClick(object sender, EventArgs e)
        {
            ddlColor.Items.Clear();

            DataSet dsColor = objBs.gridColor();
            if (dsColor.Tables[0].Rows.Count > 0)
            {
                ddlColor.DataSource = dsColor.Tables[0];
                ddlColor.DataTextField = "Color";
                ddlColor.DataValueField = "ColorId";
                ddlColor.DataBind();
                ddlColor.Items.Insert(0, "Color");
            }
        }
        protected void btnSizeRefresh_OnClick(object sender, EventArgs e)
        {
            ddlSize.Items.Clear();

            DataSet dsSizeRange = objBs.gridSizeRange();
            if (dsSizeRange.Tables[0].Rows.Count > 0)
            {
                ddlSize.DataSource = dsSizeRange.Tables[0];
                ddlSize.DataTextField = "Range";
                ddlSize.DataValueField = "RangeId";
                ddlSize.DataBind();
                ddlSize.Items.Insert(0, "Size");
            }

        }


        protected void Check_BuyerOrder(object sender, EventArgs e)
        {
            if (txtBuyerPONo.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Valid Buyer Order No.')", true);
                txtBuyerPONo.Focus();
                return;
            }
            else
            {

                if (ddlBuyerCode.SelectedValue == "BuyerCode")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select  Valid Buyer Code.')", true);
                    ddlBuyerCode.Focus();
                    return;
                }


                // Check Buyer Code Duplicate

                DataSet dcheckbuyercode = objBs.GetBuyerCodeDuplicate(ddlBuyerCode.SelectedValue, txtBuyerPONo.Text);
                if (dcheckbuyercode.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Buyer PO.No,Already Exists.')", true);
                    txtBuyerPONo.Focus();
                    return;
                }


            }
        }


        protected void ddlBuyerCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStyle.Items.Clear();

            if (ddlBuyerCode.SelectedValue != "" && ddlBuyerCode.SelectedValue != "0" && ddlBuyerCode.SelectedValue != "BuyerCode")
            {
                ddlBuyerName.SelectedValue = ddlBuyerCode.SelectedValue;

                string[] OrderType = ddlOrderType.SelectedValue.Split('$');

                DataSet dsOrderNO = objBs.GetOrderNO(Convert.ToInt32(OrderType[0].ToString()), YearCode);

                string OrderNo = dsOrderNO.Tables[0].Rows[0]["OrderNo"].ToString().PadLeft(4, '0');

                if (OrderType.Length >= 2)
                {
                    txtExcNo.Text = OrderType[1].ToString() + " - " + ddlBuyerCode.SelectedItem.Text + " - " + OrderNo + " / " + YearCode;
                }
                else
                {
                    txtExcNo.Text = ddlBuyerCode.SelectedItem.Text + " - " + OrderNo + " / " + YearCode;
                }

                DataSet dsStyleNo = objBs.GetStyleNo_BO(Convert.ToInt32(ddlBuyerCode.SelectedValue));
                if (dsStyleNo.Tables[0].Rows.Count > 0)
                {
                    ddlStyle.DataSource = dsStyleNo.Tables[0];
                    ddlStyle.DataTextField = "StyleNo";
                    ddlStyle.DataValueField = "SamplingCostingId";
                    ddlStyle.DataBind();
                    ddlStyle.Items.Insert(0, "Style");
                }
                else
                {
                    ddlStyle.Items.Insert(0, "Style");
                }
            }
            else
            {
                ddlBuyerName.ClearSelection();
                txtExcNo.Text = "";

                ddlStyle.Items.Insert(0, "Style");
            }

            ViewState["CurrentTable1"] = null;
            GVItem.DataSource = null;
            GVItem.DataBind();
            ViewState["CurrentTable2"] = null;
            GVSizesView.DataSource = null;
            GVSizesView.DataBind();

            UpdatePanel2.Update();
        }

        protected void ddlFabricCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFabricCode.SelectedValue != "" && ddlFabricCode.SelectedValue != "0" && ddlFabricCode.SelectedValue != "FabricCode")
            {
                DataSet dsgetfabricname = objBs.getiItemTypeforHead_fabricnamelist(ddlFabricCode.SelectedValue);
                if (dsgetfabricname.Tables[0].Rows.Count > 0)
                {
                    ddlFabricName.DataSource = dsgetfabricname.Tables[0];
                    ddlFabricName.DataTextField = "description";
                    ddlFabricName.DataValueField = "itemmasterid";
                    ddlFabricName.DataBind();
                    ddlFabricName.Items.Insert(0, "FabricName");
                }
                else
                {
                    ddlFabricName.Items.Clear();
                    ddlFabricName.Items.Insert(0, "FabricName");
                }

            }
            else
            {
                ddlFabricName.Items.Clear();
                ddlFabricName.Items.Insert(0, "FabricName");
            }
        }
        protected void ddlFabricName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFabricName.SelectedValue != "" && ddlFabricName.SelectedValue != "0" && ddlFabricName.SelectedValue != "FabricCode")
            {
                //  ddlFabricCode.SelectedValue = ddlFabricName.SelectedValue;

                DataSet getcodelist = objBs.getiItemTypeforHead_fabricodelist(ddlFabricName.SelectedValue);
                if (getcodelist.Tables[0].Rows.Count > 0)
                {
                    ddlFabricCode.SelectedValue = getcodelist.Tables[0].Rows[0]["itemid"].ToString();
                }
            }
            else
            {
                ddlFabricCode.ClearSelection();
                ddlFabricCode.Items.Insert(0, "FabricCode");
            }
        }

        protected void ddlSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSize.SelectedValue != "" && ddlSize.SelectedValue != "0" && ddlSize.SelectedValue != "Size")
            {
                DataSet dsSizeRange = objBs.getSizeRangeSize(Convert.ToInt32(ddlSize.SelectedValue));
                if (dsSizeRange.Tables[0].Rows.Count > 0)
                {
                    GVSizes.DataSource = dsSizeRange;
                    GVSizes.DataBind();
                }
                else
                {
                    GVSizes.DataSource = null;
                    GVSizes.DataBind();
                }
            }
            else
            {
                GVSizes.DataSource = null;
                GVSizes.DataBind();
            }
        }

        protected void btnCheckDetails_OnClick(object sender, EventArgs e)
        {
            #region Validations

            if (ddlStyle.SelectedValue == "Style" || ddlStyle.SelectedValue == "" || ddlStyle.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Style.')", true);
                UpdatePanel2.Update();
                return;
            }
            if (ddlColor.SelectedValue == "Color" || ddlColor.SelectedValue == "" || ddlColor.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Color.')", true);
                UpdatePanel2.Update();
                return;
            }
            if (ddlSize.SelectedValue == "Size" || ddlSize.SelectedValue == "" || ddlSize.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Size.')", true);
                UpdatePanel2.Update();
                return;
            }

            #endregion

            if (txtQty.Text == "")
                txtQty.Text = "0";
            double Qty = Convert.ToDouble(txtQty.Text);

            if (Qty == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty.')", true);
                UpdatePanel2.Update();
                return;
            }

            double TtlRatio = 0;
            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
                if (txtRatio.Text == "")
                    txtRatio.Text = "0";
                TtlRatio += Convert.ToDouble(txtRatio.Text);
            }

            //if (TtlRatio == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Ratio.')", true);
            //    return;
            //}

            double QtyRatio = (Qty / TtlRatio);
            if (QtyRatio.ToString() == "NaN")
                QtyRatio = 0;
            if (QtyRatio.ToString() == "Infinity")
                QtyRatio = 0;


            int AffectedQty = 0;
            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
                TextBox txtQtys = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");

                Label lblCqty = (Label)GVSizes.Rows[vLoop].FindControl("lblCqty");
                Label lblCRatio = (Label)GVSizes.Rows[vLoop].FindControl("lblCRatio");
                lblCRatio.Text = txtcuttingratio.Text;
                if (txtRatio.Text == "")
                    txtRatio.Text = "0";

                if (txtcuttingratio.Text == "")
                    txtcuttingratio.Text = "0";

                double sff = (QtyRatio * Convert.ToDouble(txtRatio.Text));

                int RoundofQty = Convert.ToInt32(Math.Round((QtyRatio * Convert.ToDouble(txtRatio.Text)), MidpointRounding.AwayFromZero));

                txtQtys.Text = RoundofQty.ToString();

                double cuttingqty = (Convert.ToDouble(txtQtys.Text) * Convert.ToDouble(txtcuttingratio.Text)) / 100;

                int Roundcuttingqty = Convert.ToInt32(Math.Round((cuttingqty), MidpointRounding.AwayFromZero));

                lblCqty.Text = (Roundcuttingqty + Convert.ToDouble(txtQtys.Text)).ToString();
                // TtlCQty += Convert.ToDouble(lblCqty.Text);

                AffectedQty += RoundofQty;
            }

            txtAffectedQty.Text = AffectedQty.ToString();

            UpdatePanel1.Update();
            UpdatePanel2.Update();
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

            #region Validations

            if (ddlStyle.SelectedValue == "Style" || ddlStyle.SelectedValue == "" || ddlStyle.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Style.')", true);
                UpdatePanel2.Update();
                return;
            }
            if (ddlColor.SelectedValue == "Color" || ddlColor.SelectedValue == "" || ddlColor.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Color.')", true);
                UpdatePanel2.Update();
                return;
            }
            if (ddlSize.SelectedValue == "Size" || ddlSize.SelectedValue == "" || ddlSize.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Size.')", true);
                UpdatePanel2.Update();
                return;
            }

            if (txtRate.Text == "")
                txtRate.Text = "0";
            if (Convert.ToDouble(txtRate.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Rate.')", true);
                UpdatePanel2.Update();
                return;
            }

            if (txtQty.Text == "")
                txtQty.Text = "0";
            double Qty = Convert.ToDouble(txtQty.Text);
            if (Qty == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty.')", true);
                UpdatePanel2.Update();
                return;
            }

            double TtlRatio = 0; double TtlQty = 0; double TtlCQty = 0;
            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
                TextBox txtQty_G = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");
                Label lblCqty = (Label)GVSizes.Rows[vLoop].FindControl("lblCqty");
                Label lblCRatio = (Label)GVSizes.Rows[vLoop].FindControl("lblCRatio");
                lblCRatio.Text = txtcuttingratio.Text;


                if (txtRatio.Text == "")
                    txtRatio.Text = "0";
                if (txtQty_G.Text == "")
                    txtQty_G.Text = "0";
                TtlRatio += Convert.ToDouble(txtRatio.Text);
                TtlQty += Convert.ToDouble(txtQty_G.Text);

                double cuttingqty = (Convert.ToDouble(txtQty_G.Text) * Convert.ToDouble(txtcuttingratio.Text)) / 100;

                int Roundcuttingqty = Convert.ToInt32(Math.Round((cuttingqty), MidpointRounding.AwayFromZero));

                lblCqty.Text = (Roundcuttingqty + Convert.ToDouble(txtQty_G.Text)).ToString();
                TtlCQty += Convert.ToDouble(lblCqty.Text);

            }

            txtCQty.Text = TtlCQty.ToString();
            txtAffectedQty.Text = TtlQty.ToString();

            if (TtlQty == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty.')", true);
                UpdatePanel2.Update();
                return;
            }
            if (Qty != TtlQty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty and Affected Qty was Mismatched.')", true);
                UpdatePanel2.Update();
                return;
            }

            #endregion

            #region AmountCalculations

            double TtlAmount_1 = 0; double TtlAmount_2 = 0;
            //if (GVItem.Rows.Count == 0)
            //{
            string[] Currency = ddlCurrency.SelectedItem.Text.Split('&');
            TtlAmount_1 = Convert.ToDouble(TtlQty) * Convert.ToDouble(txtRate.Text);
            //}
            //else
            //{
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                Label lblQty = (Label)GVItem.Rows[vLoop].FindControl("lblQty");
                Label lblRate = (Label)GVItem.Rows[vLoop].FindControl("lblRate");

                TtlAmount_2 += Convert.ToDouble(lblQty.Text) * Convert.ToDouble(lblRate.Text);
            }
            //}
            txtAmount.Text = (TtlAmount_1 + TtlAmount_2).ToString();

            #endregion


            string HttpCookieValue = "";

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
            dct = new DataColumn("Size");
            dttt.Columns.Add(dct);

            dct = new DataColumn("CQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("CRatio");
            dttt.Columns.Add(dct);

            //lblCRatio

            dstd.Tables.Add(dttt);


            //  string[] Style = ddlStyle.SelectedItem.Text.Split('&');
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                HttpCookie nameCookie = Request.Cookies["Name"];
                string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                string adad = (Convert.ToInt32(name) + 1).ToString();

                //Set the Cookie value.
                nameCookie.Values["Name"] = adad;
                //Set the Expiry date.
                nameCookie.Expires = DateTime.Now.AddDays(30);
                //Add the Cookie to Browser.
                Response.Cookies.Add(nameCookie);

                HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                //string CO_RowId = Request.Cookies["C_RowId"]["RowId"].ToString();

                //HttpCookie HC = new HttpCookie("C_RowId");

                //string RowId=(Convert.ToInt32(CO_RowId) + 1).ToString();
                //HC.Values["RowId"] = RowId.ToString();

                //HC.Expires.AddYears(4);
                //Response.Cookies.Add(HC);


                //  string CO_RowsIds = Request.Cookies["C_RowId"]["RowId"].ToString();

                drNew = dttt.NewRow();
                drNew["RowId"] = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                drNew["StyleNo"] = ddlStyle.SelectedItem.Text;
                drNew["StyleNoId"] = ddlStyle.SelectedValue;
                drNew["Description"] = lblitemdesc.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Qty"] = txtQty.Text;
                drNew["CQty"] = txtCQty.Text;
                drNew["AffectedQty"] = TtlQty;
                drNew["RangeId"] = ddlSize.SelectedValue;
                drNew["Size"] = ddlSize.SelectedItem.Text;
                drNew["CRatio"] = txtcuttingratio.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

            }
            else
            {
                //string RowId = "1";

                //HttpCookie HC = new HttpCookie("C_RowId");
                //HC.Values["RowId"] = RowId.ToString();
                //HC.Expires.AddYears(4);
                //Response.Cookies.Add(HC);

                //string CO_RowsIds = Request.Cookies["C_RowId"]["RowId"].ToString();


                HttpCookie nameCookie = new HttpCookie("Name");
                nameCookie.Values["Name"] = "1";
                nameCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(nameCookie);

                HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                drNew = dttt.NewRow();
                drNew["RowId"] = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                drNew["StyleNo"] = ddlStyle.SelectedItem.Text;
                drNew["StyleNoId"] = ddlStyle.SelectedValue;
                drNew["Description"] = lblitemdesc.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Qty"] = txtQty.Text;
                drNew["CQty"] = txtCQty.Text;
                drNew["AffectedQty"] = TtlQty;
                drNew["RangeId"] = ddlSize.SelectedValue;
                drNew["Size"] = ddlSize.SelectedItem.Text;
                drNew["CRatio"] = txtcuttingratio.Text;

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

            //  string CO_RsowId = Request.Cookies["C_RowId"]["RowId"].ToString();


            if (ViewState["CurrentTable2"] != null)
            {
                HttpCookie nameCookie = Request.Cookies["Name"];

                DataTable dt1 = (DataTable)ViewState["CurrentTable2"];

                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                    Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                    TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
                    TextBox txtQty1 = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");

                    Label lblCqty = (Label)GVSizes.Rows[vLoop].FindControl("lblCqty");

                    Label lblCRatio = (Label)GVSizes.Rows[vLoop].FindControl("lblCRatio");

                    if (txtRatio.Text == "")
                        txtRatio.Text = "0";
                    if (txtQty1.Text == "")
                        txtQty1.Text = "0";

                    drNew1 = dttt1.NewRow();

                    drNew1["RowId"] = HttpCookieValue;
                    drNew1["SizeId"] = hdSize.Value;
                    drNew1["Size"] = lblSize.Text;
                    drNew1["Ratio"] = txtRatio.Text;
                    drNew1["Qty"] = txtQty1.Text;
                    drNew1["CQty"] = lblCqty.Text;
                    drNew1["CRatio"] = lblCRatio.Text;

                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }
                dtddd1.Merge(dt1);
            }
            else
            {
                HttpCookie nameCookie = Request.Cookies["Name"];

                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                    Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                    TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
                    TextBox txtQty1 = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");

                    Label lblCqty = (Label)GVSizes.Rows[vLoop].FindControl("lblCqty");

                    Label lblCRatio = (Label)GVSizes.Rows[vLoop].FindControl("lblCRatio");

                    if (txtRatio.Text == "")
                        txtRatio.Text = "0";
                    if (txtQty1.Text == "")
                        txtQty1.Text = "0";

                    drNew1 = dttt1.NewRow();

                    string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                    drNew1["RowId"] = HttpCookieValue;
                    drNew1["SizeId"] = hdSize.Value;
                    drNew1["Size"] = lblSize.Text;
                    drNew1["Ratio"] = txtRatio.Text;
                    drNew1["Qty"] = txtQty1.Text;
                    drNew1["CQty"] = lblCqty.Text;
                    drNew1["CRatio"] = lblCRatio.Text;

                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }
            }

            #endregion

            ViewState["CurrentTable2"] = dtddd1;

            UpdatePanel1.Update();
            UpdatePanel2.Update();

        }
        protected void btnSubmit1_OnClick(object sender, EventArgs e)
        {
            #region Validations

            if (ddlStyle.SelectedValue == "Style" || ddlStyle.SelectedValue == "" || ddlStyle.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Style.')", true);
                UpdatePanel2.Update();
                return;
            }
            if (ddlColor.SelectedValue == "Color" || ddlColor.SelectedValue == "" || ddlColor.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Color.')", true);
                UpdatePanel2.Update();
                return;
            }
            if (ddlSize.SelectedValue == "Size" || ddlSize.SelectedValue == "" || ddlSize.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Size.')", true);
                UpdatePanel2.Update();
                return;
            }

            if (txtRate.Text == "")
                txtRate.Text = "0";
            if (Convert.ToDouble(txtRate.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Rate.')", true);
                UpdatePanel2.Update();
                return;
            }

            if (txtQty.Text == "")
                txtQty.Text = "0";
            double Qty = Convert.ToDouble(txtQty.Text);
            if (Qty == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty.')", true);
                UpdatePanel2.Update();
                return;
            }

            //double TtlRatio = 0; double TtlQty = 0;
            //for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            //{
            //    TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
            //    TextBox txtQty_G = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");
            //    if (txtRatio.Text == "")
            //        txtRatio.Text = "0";
            //    if (txtQty_G.Text == "")
            //        txtQty_G.Text = "0";
            //    TtlRatio += Convert.ToDouble(txtRatio.Text);
            //    TtlQty += Convert.ToDouble(txtQty_G.Text);
            //}

            double TtlRatio = 0; double TtlQty = 0; double TtlCQty = 0;
            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
                TextBox txtQty_G = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");
                Label lblCqty = (Label)GVSizes.Rows[vLoop].FindControl("lblCqty");

                Label lblCRatio = (Label)GVSizes.Rows[vLoop].FindControl("lblCRatio");
                lblCRatio.Text = txtcuttingratio.Text;


                if (txtRatio.Text == "")
                    txtRatio.Text = "0";
                if (txtQty_G.Text == "")
                    txtQty_G.Text = "0";
                TtlRatio += Convert.ToDouble(txtRatio.Text);
                TtlQty += Convert.ToDouble(txtQty_G.Text);

                double cuttingqty = (Convert.ToDouble(txtQty_G.Text) * Convert.ToDouble(txtcuttingratio.Text)) / 100;

                //lblCqty.Text = (cuttingqty + Convert.ToDouble(txtQty_G.Text)).ToString();
                //lblCqty.Text = (cuttingqty + Convert.ToDouble(txtQty_G.Text)).ToString();
                int Roundcuttingqty = Convert.ToInt32(Math.Round((cuttingqty), MidpointRounding.AwayFromZero));

                lblCqty.Text = (Roundcuttingqty + Convert.ToDouble(txtQty_G.Text)).ToString();


                TtlCQty += Convert.ToDouble(lblCqty.Text);

            }

            txtCQty.Text = TtlCQty.ToString();
            txtAffectedQty.Text = TtlQty.ToString();

            if (TtlQty == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty.')", true);
                UpdatePanel2.Update();
                return;
            }
            if (Qty != TtlQty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty and Affected Qty was Mismatched.')", true);
                UpdatePanel2.Update();
                return;
            }

            #endregion

            #region AmountCalculations

            double TtlAmount_1 = 0; double TtlAmount_2 = 0;
            //if (GVItem.Rows.Count == 0)
            //{
            string[] Currency = ddlCurrency.SelectedItem.Text.Split('&');
            TtlAmount_1 = Convert.ToDouble(TtlQty) * Convert.ToDouble(txtRate.Text);
            //}
            //else
            //{
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                Label lblQty = (Label)GVItem.Rows[vLoop].FindControl("lblQty");
                Label lblRate = (Label)GVItem.Rows[vLoop].FindControl("lblRate");

                TtlAmount_2 += Convert.ToDouble(lblQty.Text) * Convert.ToDouble(lblRate.Text);
            }
            //}
            txtAmount.Text = (TtlAmount_1 + TtlAmount_2).ToString();

            #endregion


            string HttpCookieValue = "";

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
            dct = new DataColumn("Size");
            dttt.Columns.Add(dct);

            dct = new DataColumn("CQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("CRatio");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);


            // string[] Style = ddlStyle.SelectedItem.Text.Split('&');
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                HttpCookie nameCookie = Request.Cookies["Name"];
                string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                string adad = (Convert.ToInt32(name) + 1).ToString();

                //Set the Cookie value.
                nameCookie.Values["Name"] = adad;
                //Set the Expiry date.
                nameCookie.Expires = DateTime.Now.AddDays(30);
                //Add the Cookie to Browser.
                Response.Cookies.Add(nameCookie);

                HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                //string CO_RowId = Request.Cookies["C_RowId"]["RowId"].ToString();

                //HttpCookie HC = new HttpCookie("C_RowId");

                //string RowId=(Convert.ToInt32(CO_RowId) + 1).ToString();
                //HC.Values["RowId"] = RowId.ToString();

                //HC.Expires.AddYears(4);
                //Response.Cookies.Add(HC);


                //  string CO_RowsIds = Request.Cookies["C_RowId"]["RowId"].ToString();

                drNew = dttt.NewRow();
                drNew["RowId"] = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                drNew["StyleNo"] = ddlStyle.SelectedItem.Text;
                drNew["StyleNoId"] = ddlStyle.SelectedValue;
                drNew["Description"] = lblitemdesc.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Qty"] = txtQty.Text;
                drNew["CQty"] = txtCQty.Text;
                drNew["AffectedQty"] = TtlQty;
                drNew["RangeId"] = ddlSize.SelectedValue;
                drNew["Size"] = ddlSize.SelectedItem.Text;
                drNew["CRatio"] = txtcuttingratio.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

            }
            else
            {
                //string RowId = "1";

                //HttpCookie HC = new HttpCookie("C_RowId");
                //HC.Values["RowId"] = RowId.ToString();
                //HC.Expires.AddYears(4);
                //Response.Cookies.Add(HC);

                //string CO_RowsIds = Request.Cookies["C_RowId"]["RowId"].ToString();


                HttpCookie nameCookie = new HttpCookie("Name");
                nameCookie.Values["Name"] = "1";
                nameCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(nameCookie);

                HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                drNew = dttt.NewRow();
                drNew["RowId"] = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                drNew["StyleNo"] = ddlStyle.SelectedItem.Text;
                drNew["StyleNoId"] = ddlStyle.SelectedValue;
                drNew["Description"] = lblitemdesc.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Qty"] = txtQty.Text;
                drNew["CQty"] = txtCQty.Text;
                drNew["AffectedQty"] = TtlQty;
                drNew["RangeId"] = ddlSize.SelectedValue;
                drNew["Size"] = ddlSize.SelectedItem.Text;
                drNew["CRatio"] = txtcuttingratio.Text;

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

            //  string CO_RsowId = Request.Cookies["C_RowId"]["RowId"].ToString();


            if (ViewState["CurrentTable2"] != null)
            {
                HttpCookie nameCookie = Request.Cookies["Name"];

                DataTable dt1 = (DataTable)ViewState["CurrentTable2"];

                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                    Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                    TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
                    TextBox txtQty1 = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");

                    Label lblCqty = (Label)GVSizes.Rows[vLoop].FindControl("lblCqty");

                    Label lblCRatio = (Label)GVSizes.Rows[vLoop].FindControl("lblCRatio");

                    if (txtRatio.Text == "")
                        txtRatio.Text = "0";
                    if (txtQty1.Text == "")
                        txtQty1.Text = "0";

                    drNew1 = dttt1.NewRow();

                    drNew1["RowId"] = HttpCookieValue;
                    drNew1["SizeId"] = hdSize.Value;
                    drNew1["Size"] = lblSize.Text;
                    drNew1["Ratio"] = txtRatio.Text;
                    drNew1["Qty"] = txtQty1.Text;
                    drNew1["CQty"] = lblCqty.Text;
                    drNew1["CRatio"] = lblCRatio.Text;

                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }
                dtddd1.Merge(dt1);
            }
            else
            {
                HttpCookie nameCookie = Request.Cookies["Name"];

                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                    Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                    TextBox txtRatio = (TextBox)GVSizes.Rows[vLoop].FindControl("txtRatio");
                    TextBox txtQty1 = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");

                    Label lblCqty = (Label)GVSizes.Rows[vLoop].FindControl("lblCqty");


                    Label lblCRatio = (Label)GVSizes.Rows[vLoop].FindControl("lblCRatio");

                    if (txtRatio.Text == "")
                        txtRatio.Text = "0";
                    if (txtQty1.Text == "")
                        txtQty1.Text = "0";

                    drNew1 = dttt1.NewRow();

                    string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                    drNew1["RowId"] = HttpCookieValue;
                    drNew1["SizeId"] = hdSize.Value;
                    drNew1["Size"] = lblSize.Text;
                    drNew1["Ratio"] = txtRatio.Text;
                    drNew1["Qty"] = txtQty1.Text;
                    drNew1["CQty"] = lblCqty.Text;
                    drNew1["CRatio"] = lblCRatio.Text;

                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }
            }

            #endregion

            ViewState["CurrentTable2"] = dtddd1;

            ddlStyle.ClearSelection();
            ddlColor.ClearSelection();

            txtRate.Text = "0";
            txtQty.Text = "0";
            txtAffectedQty.Text = "0";

            ddlSize.ClearSelection();

            GVSizes.DataSource = null;
            GVSizes.DataBind();

            UpdatePanel1.Update();
            UpdatePanel2.Update();
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

                    ddlStyle.SelectedValue = RowsGVSizeDetails[0]["StyleNoId"].ToString();
                    ddlColor.SelectedValue = RowsGVSizeDetails[0]["ColorId"].ToString();
                    lblitemdesc.Text = RowsGVSizeDetails[0]["Description"].ToString();

                    txtRate.Text = RowsGVSizeDetails[0]["Rate"].ToString();
                    txtQty.Text = RowsGVSizeDetails[0]["Qty"].ToString();
                    txtcuttingratio.Text = RowsGVSizeDetails[0]["Cratio"].ToString();
                    txtAffectedQty.Text = RowsGVSizeDetails[0]["AffectedQty"].ToString();

                    ddlSize.SelectedValue = RowsGVSizeDetails[0]["RangeId"].ToString();



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
                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("CQty");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("CRatio");
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
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();
                        drNew1["CQty"] = RowsGVSizeQty[i]["CQty"].ToString();
                        drNew1["CRatio"] = RowsGVSizeQty[i]["CRatio"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    GVSizes.DataSource = dstd1;
                    GVSizes.DataBind();

                    DataRow[] DRItem = DTGVSizeDetails.Select("RowId='" + e.CommandArgument.ToString() + "'");
                    for (int i = 0; i < DRItem.Length; i++)
                        DRItem[i].Delete();
                    DTGVSizeDetails.AcceptChanges();

                    ViewState["CurrentTable1"] = DTGVSizeDetails;
                    GVItem.DataSource = DTGVSizeDetails;
                    GVItem.DataBind();



                    DataRow[] DRSize = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");
                    for (int i = 0; i < DRSize.Length; i++)
                        DRSize[i].Delete();
                    DTGVSizeQty.AcceptChanges();

                    ViewState["CurrentTable2"] = DTGVSizeQty;


                    #endregion

                    #region AmountCalculations

                    double TtlAmount_2 = 0;
                    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                    {
                        Label lblQty = (Label)GVItem.Rows[vLoop].FindControl("lblQty");
                        Label lblRate = (Label)GVItem.Rows[vLoop].FindControl("lblRate");

                        TtlAmount_2 += Convert.ToDouble(lblQty.Text) * Convert.ToDouble(lblRate.Text);
                    }
                    txtAmount.Text = TtlAmount_2.ToString();

                    #endregion
                }
                UpdatePanel1.Update();
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
                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("CQty");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("CRatio");
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
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();
                        drNew1["CQty"] = RowsGVSizeQty[i]["CQty"].ToString();
                        drNew1["CRatio"] = RowsGVSizeQty[i]["CRatio"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    GVSizesView.DataSource = dstd1;
                    GVSizesView.DataBind();



                    #endregion
                }
            }

        }

        protected void btnCurrency_OnClick(object sender, EventArgs e)
        {
            string yourUrl = "CurrencyMaster.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);
        }
        protected void btnCurrencyRef_OnClick(object sender, EventArgs e)
        {
            ddlCurrency.Items.Clear();

            DataSet dsCurrency = objBs.gridCurrencywithValue();
            if (dsCurrency.Tables[0].Rows.Count > 0)
            {
                ddlCurrency.DataSource = dsCurrency.Tables[0];
                ddlCurrency.DataTextField = "CurrencyName";
                ddlCurrency.DataValueField = "CurrencyId";
                ddlCurrency.DataBind();
            }

        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            int Row = 1;
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                DropDownList ddlItemCode = (DropDownList)GVItem.Rows[vLoop].FindControl("ddlItemCode");
                DropDownList ddlColor = (DropDownList)GVItem.Rows[vLoop].FindControl("ddlColor");

                TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                TextBox txtQuantity = (TextBox)GVItem.Rows[vLoop].FindControl("txtQuantity");
                TextBox txtCost = (TextBox)GVItem.Rows[vLoop].FindControl("txtCost");


                if (txtRate.Text == "")
                    txtRate.Text = "0";
                if (txtQuantity.Text == "")
                    txtQuantity.Text = "0";
                if (txtCost.Text == "")
                    txtCost.Text = "0";



                if (ddlItemCode.SelectedValue == "" || ddlItemCode.SelectedValue == "0" || ddlItemCode.SelectedValue == "Select ItemCode")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select ItemCode in Row " + Row + " ')", true);
                    ddlItemCode.Focus();
                    return;
                }
                if (ddlColor.SelectedValue == "" || ddlColor.SelectedValue == "0" || ddlColor.SelectedValue == "Select Color")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Color in Row " + Row + " ')", true);
                    ddlItemCode.Focus();
                    return;
                }

                if (Convert.ToDouble(txtRate.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Rate in Row " + Row + " ')", true);
                    txtRate.Focus();
                    return;
                }
                if (Convert.ToDouble(txtQuantity.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Quantity in Row " + Row + " ')", true);
                    txtQuantity.Focus();
                    return;
                }
                if (Convert.ToDouble(txtCost.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Cost in Row " + Row + " ')", true);
                    txtCost.Focus();
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

            dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
            dt.Columns.Add(new DataColumn("Color", typeof(string)));
            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Cost", typeof(string)));

            dr = dt.NewRow();
            dr["ItemCode"] = string.Empty;
            dr["Color"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["Quantity"] = string.Empty;
            dr["Cost"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["CurrentTable2"] = dt;

            GVItem.DataSource = dt;
            GVItem.DataBind();

            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("ItemCode");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Color");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Quantity");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Cost");
            dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);

            drNew = dtt.NewRow();
            drNew["ItemCode"] = 0;
            drNew["Color"] = "";

            drNew["Rate"] = "";
            drNew["Quantity"] = "";
            drNew["Cost"] = "";

            dstd.Tables[0].Rows.Add(drNew);

            GVItem.DataSource = dstd;
            GVItem.DataBind();

        }
        private void AddNewRow1()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable2"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtCost");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Color"] = ddlColor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Cost"] = txtCost.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable2"] = dtCurrentTable;

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

            if (ViewState["CurrentTable2"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtRate");
                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                        TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtCost");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Color"] = ddlColor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Cost"] = txtCost.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable2"] = dtCurrentTable;
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
            if (ViewState["CurrentTable2"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable2"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtCost");

                        ddlItemCode.SelectedValue = dt.Rows[i]["ItemCode"].ToString();
                        ddlColor.SelectedValue = dt.Rows[i]["Color"].ToString();

                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtQuantity.Text = dt.Rows[i]["Quantity"].ToString();
                        txtCost.Text = dt.Rows[i]["Cost"].ToString();

                        ItemCost += Convert.ToDouble(txtCost.Text);

                        rowIndex++;

                    }
                }
            }



        }

        protected void GVItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsItems = objBs.getItems();
            DataSet dsColor = objBs.gridColor();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddlItemCode = (DropDownList)e.Row.FindControl("ddlItemCode");
                ddlItemCode.DataSource = dsItems;
                ddlItemCode.DataTextField = "ItemCode";
                ddlItemCode.DataValueField = "ItemId";
                ddlItemCode.DataBind();

                var ddlColor = (DropDownList)e.Row.FindControl("ddlColor");
                ddlColor.DataSource = dsColor.Tables[0];
                ddlColor.DataTextField = "Color";
                ddlColor.DataValueField = "ColorID";
                ddlColor.DataBind();

            }

        }
        protected void GVItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData1();
            if (ViewState["CurrentTable2"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable2"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable2"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();

                    SetPreviousData1();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable2"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();

                    SetPreviousData1();
                    FirstGridViewRow1();
                }
            }


        }

        protected void txtShipmentDate_OnTextChanged(object sender, EventArgs e)
        {
            DateTime Dat = DateTime.ParseExact(txtShipmentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string Date = Dat.AddDays(Convert.ToInt32(lblShipmentDate.Text)).ToString();
            txtDeliveryDate.Text = Convert.ToDateTime(Date).ToString("dd/MM/yyyy");

            divnarration.Visible = false;
            if (btnSave.Text == "Update")
            {
                if (txtODeliveryDate.Text == txtDeliveryDate.Text)
                {
                }
                else
                {
                    divnarration.Visible = true;
                    if (txtdeliveryreason.Text == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Valid Reson For Changing the Delivery Date.')", true);
                        txtdeliveryreason.Focus();
                        return;
                    }
                }
            }
        }

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            Button ddl = (Button)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            FileUpload fp_Upload = (FileUpload)row.FindControl("fp_Upload");

            Label lblFile_Path = (Label)row.FindControl("lblFile_Path");
            Image img_Photo = (Image)row.FindControl("img_Photo");

            if (fp_Upload.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
                fp_Upload.PostedFile.SaveAs(Server.MapPath("~/Sampling/") + fileName.Replace(" ", ""));
                lblFile_Path.Text = "~/Sampling/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
                img_Photo.ImageUrl = "~/Sampling/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
            }

            mpecost.Show();
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            DataSet dsCur = objBs.getiCurrencyvalues(ddlCurrency.SelectedValue);
            double CurrencyVale = Convert.ToDouble(dsCur.Tables[0].Rows[0]["Value"].ToString());


            HttpCookie nameCookie = Request.Cookies["Name"];
            string MaxRowId = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

            DateTime OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ShipmentDate = DateTime.ParseExact(txtShipmentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DeliveryDate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnSave.Text == "Save")
            {
                #region Generate ExcNo

                string[] OrderType = ddlOrderType.SelectedValue.Split('$');

                DataSet dsOrderNO = objBs.GetOrderNO(Convert.ToInt32(OrderType[0]), YearCode);

                string OrderNo = dsOrderNO.Tables[0].Rows[0]["OrderNo"].ToString().PadLeft(4, '0');

                if (OrderType.Length >= 2)
                {
                    txtExcNo.Text = OrderType[1].ToString() + " - " + ddlBuyerCode.SelectedItem.Text + " - " + OrderNo + " / " + YearCode;
                }
                else
                {
                    txtExcNo.Text = ddlBuyerCode.SelectedItem.Text + " - " + OrderNo + " / " + YearCode;
                }

                #endregion

                string[] OrderType1 = ddlOrderType.SelectedValue.Split('$');
                int BuyerOrderId = objBs.InsertBuyerOrderMaster(Convert.ToInt32(OrderNo), Convert.ToInt32(OrderType1[0]), Convert.ToInt32(ddlBuyerCode.SelectedValue), txtExcNo.Text, txtBuyerPONo.Text, Convert.ToInt32(ddlFabricCode.SelectedValue), Convert.ToInt32(ddlFabricName.SelectedValue), OrderDate, ShipmentDate, DeliveryDate, Convert.ToInt32(ddlShipmentMode.SelectedValue), Convert.ToInt32(ddlPaymentMode.SelectedValue), txtPaymentTerms.Text, txtBytheWayof.Text, Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToDouble(txtAmount.Text), chkShipped.Checked, chkCancel.Checked, chkApproved.Checked, chkHold.Checked, chkLock.Checked, MaxRowId, txtRemarks.Text, CurrencyVale, YearCode);

                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
                    HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdRangeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRangeId");
                    HiddenField hdRowId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRowId");

                    Label lblRate = (Label)GVItem.Rows[vLoop].FindControl("lblRate");
                    Label lblQty = (Label)GVItem.Rows[vLoop].FindControl("lblQty");
                    Label lblAffectedQty = (Label)GVItem.Rows[vLoop].FindControl("lblAffectedQty");

                    Label lblCRatio = (Label)GVItem.Rows[vLoop].FindControl("lblCRatio");

                    Label lblCQty = (Label)GVItem.Rows[vLoop].FindControl("lblCQty");

                    int TransBuyerOrderId = objBs.InsertTransBuyerOrderItems(BuyerOrderId, Convert.ToInt32(hdStyleNoId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(lblRate.Text), Convert.ToInt32(lblQty.Text), Convert.ToInt32(lblAffectedQty.Text), Convert.ToInt32(hdRangeId.Value), Convert.ToInt32(hdRowId.Value), Convert.ToInt32(lblCQty.Text), lblCRatio.Text,"Add");

                }

                DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
                int TransBuyerOrderSizeId = objBs.InsertTransBuyerOrderSizes(BuyerOrderId, CurrentTable2,"Add");

                for (int vLoop = 0; vLoop < gvLabels.Rows.Count; vLoop++)
                {
                    CheckBox chkLabelItem = (CheckBox)gvLabels.Rows[vLoop].FindControl("chkLabelItem");
                    HiddenField hdItemId = (HiddenField)gvLabels.Rows[vLoop].FindControl("hdItemId");
                    TextBox txtLabelText = (TextBox)gvLabels.Rows[vLoop].FindControl("txtLabelText");
                    Label lblFile_Path = (Label)gvLabels.Rows[vLoop].FindControl("lblFile_Path");

                    if (chkLabelItem.Checked == true)
                    {
                        int TransBuyerOrderLabelId = objBs.InsertTransBuyerOrderLabels(BuyerOrderId, Convert.ToInt32(hdItemId.Value), txtLabelText.Text, lblFile_Path.Text, "Add");
                    }
                }
            }
            else
            {

                DateTime ODeliveryDate = DateTime.ParseExact(txtODeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string BuyerOrderId = Request.QueryString.Get("BuyerOrderId");


                DataSet ds_RSChecking = objBs.Check_Precutting(Convert.ToInt32(BuyerOrderId));
                if (ds_RSChecking.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Pre Cutting was Assigned cannot be Update.')", true);
                    return;
                }

                string IschangedDeliveryDate = "N";

                if (txtODeliveryDate.Text == txtDeliveryDate.Text)
                {
                }
                else
                {
                    if (txtdeliveryreason.Text == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Valid Reson For Changing the Delivery Date.')", true);
                        return;
                    }
                    else
                    {
                        IschangedDeliveryDate = "Y";

                    }
                }

                if (GVItem.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Style Details are Empty.')", true);
                    return;
                }

                string[] OrderType1 = ddlOrderType.SelectedValue.Split('$');
                int UpdateBuyerOrderId = objBs.UpdateBuyerOrderMaster(Convert.ToInt32(OrderType1[0]), Convert.ToInt32(ddlBuyerCode.SelectedValue), txtExcNo.Text, txtBuyerPONo.Text, Convert.ToInt32(ddlFabricCode.SelectedValue), Convert.ToInt32(ddlFabricName.SelectedValue), OrderDate, ShipmentDate, DeliveryDate, Convert.ToInt32(ddlShipmentMode.SelectedValue), Convert.ToInt32(ddlPaymentMode.SelectedValue), txtPaymentTerms.Text, txtBytheWayof.Text, Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToDouble(txtAmount.Text), chkShipped.Checked, chkCancel.Checked, chkApproved.Checked, chkHold.Checked, chkLock.Checked, MaxRowId, Convert.ToInt32(BuyerOrderId), txtRemarks.Text, txtdeliveryreason.Text, IschangedDeliveryDate, lblUserID.Text, lblUser.Text, ODeliveryDate, CurrencyVale);

                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
                    HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdRangeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRangeId");
                    HiddenField hdRowId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRowId");

                    Label lblRate = (Label)GVItem.Rows[vLoop].FindControl("lblRate");
                    Label lblQty = (Label)GVItem.Rows[vLoop].FindControl("lblQty");
                    Label lblAffectedQty = (Label)GVItem.Rows[vLoop].FindControl("lblAffectedQty");

                    Label lblCRatio = (Label)GVItem.Rows[vLoop].FindControl("lblCRatio");

                    Label lblCQty = (Label)GVItem.Rows[vLoop].FindControl("lblCQty");

                    int TransBuyerOrderId = objBs.InsertTransBuyerOrderItems(Convert.ToInt32(BuyerOrderId), Convert.ToInt32(hdStyleNoId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(lblRate.Text), Convert.ToInt32(lblQty.Text), Convert.ToInt32(lblAffectedQty.Text), Convert.ToInt32(hdRangeId.Value), Convert.ToInt32(hdRowId.Value), Convert.ToInt32(lblCQty.Text), lblCRatio.Text,"Update");

                }

                DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
                int TransBuyerOrderSizeId = objBs.InsertTransBuyerOrderSizes(Convert.ToInt32(BuyerOrderId), CurrentTable2,"Update");

                for (int vLoop = 0; vLoop < gvLabels.Rows.Count; vLoop++)
                {
                    CheckBox chkLabelItem = (CheckBox)gvLabels.Rows[vLoop].FindControl("chkLabelItem");
                    HiddenField hdItemId = (HiddenField)gvLabels.Rows[vLoop].FindControl("hdItemId");
                    TextBox txtLabelText = (TextBox)gvLabels.Rows[vLoop].FindControl("txtLabelText");
                    Label lblFile_Path = (Label)gvLabels.Rows[vLoop].FindControl("lblFile_Path");

                    if (chkLabelItem.Checked == true)
                    {
                        int TransBuyerOrderLabelId = objBs.InsertTransBuyerOrderLabels(Convert.ToInt32(BuyerOrderId), Convert.ToInt32(hdItemId.Value), txtLabelText.Text, lblFile_Path.Text,"Update");
                    }
                }
            }

            Response.Redirect("SupplierOrderMasterGrid.aspx");

        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SupplierOrderMasterGrid.aspx");
        }
        protected void btnupdateShipped_OnClick(object sender, EventArgs e)
        {

            
            DateTime ODeliveryDate = DateTime.ParseExact(txtODeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
         
            string IschangedDeliveryDate = "N";

            if (txtODeliveryDate.Text == txtDeliveryDate.Text)
            {
            }
            else
            {
                if (txtdeliveryreason.Text == "")
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Valid Reson For Changing the Delivery Date.')", true);
                    return;
                }
                else
                {
                    IschangedDeliveryDate = "Y";

                }
            }

            string BuyerOrderId = Request.QueryString.Get("BuyerOrderId");

            DataSet dsCur = objBs.getiCurrencyvalues(ddlCurrency.SelectedValue);
            double CurrencyVale = Convert.ToDouble(dsCur.Tables[0].Rows[0]["Value"].ToString());

            DateTime OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ShipmentDate = DateTime.ParseExact(txtShipmentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DeliveryDate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int UpdateBuyerOrderId = objBs.UpdateBuyerOrderMaster1(ODeliveryDate, IschangedDeliveryDate, txtdeliveryreason.Text, OrderDate, ShipmentDate, DeliveryDate, chkCancel.Checked, chkHold.Checked, chkShipped.Checked, Convert.ToInt32(BuyerOrderId), lblUserID.Text, lblUser.Text, CurrencyVale);

            Response.Redirect("SupplierOrderMasterGrid.aspx");
        }

        protected void btnLabel_OnClick(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                string cond = "";

                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
                    cond += " d.samplingcostingid='" + hdStyleNoId.Value + "' ,";


                }
                cond = cond.TrimEnd(',');
                cond = cond.Replace(",", "or");
                if (cond != "")
                {


                    DataSet dsDCConditions = objBs.getiItemTypeforHead("ShowLabelDetails", cond);
                    if (dsDCConditions.Tables[0].Rows.Count > 0)
                    {
                        gvLabels.DataSource = dsDCConditions;
                        gvLabels.DataBind();
                    }
                }

                UpdatePanel1.Update();
                UpdatePanel3.Update();
            }
            mpecost.Show();

            //UpdatePanel2.Update();




            //    hdpopupid.Value = "0";


            //    DataSet dsView = objBs.GetMobAppOrderChecking(e.CommandArgument.ToString());
            //    if (dsView.Tables[0].Rows.Count > 0)
            //    {
            //        dsView = objBs.GetMobAppOrderItemdetails(e.CommandArgument.ToString());

            //        hdpopupid.Value = Convert.ToInt32(e.CommandArgument).ToString();

            //        gvLabels.DataSource = dsView;
            //        gvLabels.DataBind();

            //        mpecost.Show();


            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Qty was Completed');", true);
            //        return;
            //    }

        }
        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            mpecost.Hide();
        }
    }
}
