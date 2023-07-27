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
    public partial class SamplingCosting_Approval : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


                //////DataSet dsItemCode_Description_ItemType = objBs.getItemCodeDescriptionItemType();
                //////if (dsItemCode_Description_ItemType.Tables[0].Rows.Count > 0)
                //////{
                //////    GVdsItemCodeDescriptionItemType.DataSource = dsItemCode_Description_ItemType;
                //////    GVdsItemCodeDescriptionItemType.DataBind();
                //////}
                //////else
                //////{
                //////    GVdsItemCodeDescriptionItemType.DataSource = null;
                //////    GVdsItemCodeDescriptionItemType.DataBind();
                //////}

                #region

                DataSet dsDCConditions = objBs.GetProcess();
                if (dsDCConditions.Tables[0].Rows.Count > 0)
                {
                    chkpcsprocess.DataSource = dsDCConditions.Tables[0];
                    chkpcsprocess.DataTextField = "Process";
                    chkpcsprocess.DataValueField = "Processid";
                    chkpcsprocess.DataBind();

                }

                DataSet dstax = objBs.SelectTax();
                if (dstax.Tables[0].Rows.Count > 0)
                {
                    ddltax.DataSource = dstax.Tables[0];
                    ddltax.DataTextField = "tax";
                    ddltax.DataValueField = "taxid";
                    ddltax.DataBind();
                    ddltax.Items.Insert(0, "Select GST");
                    //ddlcategory.Items.Insert(0, "Select Category");
                }

                DataSet dsStyle = objBs.GetAllStyleNo_Approval();
                if (dsStyle.Tables[0].Rows.Count > 0)
                {
                    ddlapprovalStyles.DataSource = dsStyle.Tables[0];
                    ddlapprovalStyles.DataTextField = "StyleNo";
                    ddlapprovalStyles.DataValueField = "SamplingCostingId";
                    ddlapprovalStyles.DataBind();
                    ddlapprovalStyles.Items.Insert(0, "Select StyleNo");
                }

                DataSet dsItems = objBs.getAlliItems();
                if (dsItems.Tables[0].Rows.Count > 0)
                {
                    ddlItems.DataSource = dsItems.Tables[0];
                    ddlItems.DataTextField = "Description";
                    ddlItems.DataValueField = "ItemMasterId";
                    ddlItems.DataBind();
                    ddlItems.Items.Insert(0, "Select Item");
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
                DataSet dsCurrency = objBs.gridCurrencywithValue();
                if (dsCurrency.Tables[0].Rows.Count > 0)
                {
                    ddlCostCurrency.DataSource = dsCurrency.Tables[0];
                    ddlCostCurrency.DataTextField = "CurrencyName";
                    ddlCostCurrency.DataValueField = "CurrencyId";
                    ddlCostCurrency.DataBind();
                }
                DataSet dssize = objBs.selectsize();
                if (dssize.Tables[0].Rows.Count > 0)
                {
                    ddlSize.DataSource = dssize.Tables[0];
                    ddlSize.DataTextField = "Size";
                    ddlSize.DataValueField = "SizeId";
                    ddlSize.DataBind();
                }
                #endregion

                //////FirstGridViewRow1();



                //string SamplingCostingId = Request.QueryString.Get("SamplingCostingId");
                //if (SamplingCostingId != "" && SamplingCostingId != null)
                //{
                //    ddlStyles.Enabled = false;

                //    DataSet dsItemMaster = objBs.getiSamplingandCostingvalues(Convert.ToInt32(SamplingCostingId));
                //    if (dsItemMaster.Tables[0].Rows.Count > 0)
                //    {
                //        #region

                //        txtStyleNo.Text = dsItemMaster.Tables[0].Rows[0]["StyleNo"].ToString();
                //        ddlBuyerCode.SelectedValue = dsItemMaster.Tables[0].Rows[0]["BuyerCodeId"].ToString();
                //        ddlBuyerName.SelectedValue = dsItemMaster.Tables[0].Rows[0]["BuyerCodeId"].ToString();

                //        txtBuyerPrintStyle.Text = dsItemMaster.Tables[0].Rows[0]["BuyerPrintStyle"].ToString();

                //        txtDescription.Text = dsItemMaster.Tables[0].Rows[0]["Description"].ToString();
                //        ddlSize.SelectedValue = dsItemMaster.Tables[0].Rows[0]["SizeId"].ToString();

                //        txtFabricationCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["FabricationCost"]).ToString("f2");
                //        txtEmbroideryMachineCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["EmbroideryMachineCost"]).ToString("f2");
                //        txtEmbroideryHandCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["EmbroideryHandCost"]).ToString("f2");
                //        txtPieceProcessCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["PieceProcessCost"]).ToString("f2");
                //        txtFinishingandPackingCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["FinishingandPackingCost"]).ToString("f2");

                //        txtDate.Text = Convert.ToDateTime(dsItemMaster.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");

                //        txtRejection.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["Rejection"]).ToString("f2");
                //        txtExtraMargin.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["ExtraMargin"]).ToString("f2");

                //        txtSmpCostPerPiece.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["SmpCostPerPiece"]).ToString("f2");
                //        txtPrdCostPerPiece.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["PrdCostPerPiece"]).ToString("f2");

                //        txtTotalSmpCostINR.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["TotalSmpCostINR"]).ToString("f2");
                //        txtTotalPrdCostINR.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["TotalPrdCostINR"]).ToString("f2");

                //        ddlCostCurrency.SelectedValue = dsItemMaster.Tables[0].Rows[0]["TotalCostOtherId"].ToString();

                //        txtTotalSmpCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["TotalSmpCostOther"]).ToString("f2");
                //        txtTotalPrdCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["TotalPrdCostOther"]).ToString("f2");

                //        lblItemSmpCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["ItemSmpCost"]).ToString("f2");
                //        lblItemPrdCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["ItemPrdCost"]).ToString("f2");

                //        lblFile_Path.Text = dsItemMaster.Tables[0].Rows[0]["Sketch"].ToString();
                //        img_Photo.ImageUrl = dsItemMaster.Tables[0].Rows[0]["Sketch"].ToString();

                //        btnSave.Text = "Update";

                //        #endregion
                //    }

                //    DataSet ds2 = objBs.getiTransSamplingandCostingvalues(Convert.ToInt32(SamplingCostingId));
                //    if (ds2.Tables[0].Rows.Count > 0)
                //    {
                //        #region

                //        DataTable dtt;
                //        DataRow drNew;
                //        DataColumn dct;
                //        DataSet dstd = new DataSet();
                //        dtt = new DataTable();




                //        dct = new DataColumn("Item");
                //        dtt.Columns.Add(dct);
                //        dct = new DataColumn("ItemId");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("Rate");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("SmpAvg");
                //        dtt.Columns.Add(dct);
                //        dct = new DataColumn("PrdAvg");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("SmpCost");
                //        dtt.Columns.Add(dct);
                //        dct = new DataColumn("PrdCost");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("ColorId");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("IsSelected");
                //        dtt.Columns.Add(dct);

                //        dstd.Tables.Add(dtt);

                //        foreach (DataRow Dr in ds2.Tables[0].Rows)
                //        {
                //            drNew = dtt.NewRow();

                //            drNew["Item"] = Dr["Description"];
                //            drNew["ItemId"] = Dr["ItemCodeId"];

                //            drNew["Rate"] = Dr["Rate"];

                //            drNew["SmpAvg"] = Dr["SmpAvg"];
                //            drNew["PrdAvg"] = Dr["PrdAvg"];

                //            drNew["SmpCost"] = Dr["SmpCost"];
                //            drNew["PrdCost"] = Dr["PrdCost"];

                //            drNew["ColorId"] = Dr["ColorId"];

                //            drNew["IsSelected"] = Dr["IsSelected"];

                //            dstd.Tables[0].Rows.Add(drNew);
                //        }

                //        ViewState["CurrentTable1"] = dtt;

                //        GVdsItemCodeDescriptionItemType.DataSource = dstd;
                //        GVdsItemCodeDescriptionItemType.DataBind();

                //        for (int vLoop = 0; vLoop < GVdsItemCodeDescriptionItemType.Rows.Count; vLoop++)
                //        {
                //            CheckBox chkSelect = (CheckBox)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("chkSelect");
                //            DropDownList ddlColor = (DropDownList)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("ddlColor");

                //            if (dstd.Tables[0].Rows[vLoop]["IsSelected"].ToString() == "True")
                //            {
                //                chkSelect.Checked = true;
                //            }

                //            ddlColor.SelectedValue = dstd.Tables[0].Rows[vLoop]["ColorId"].ToString();

                //        }

                //        #endregion
                //    }

                //    DataSet ds3 = objBs.getiTransSamplingandCostingvalues1(Convert.ToInt32(SamplingCostingId));
                //    if (ds3.Tables[0].Rows.Count > 0)
                //    {
                //        #region

                //        DataTable dtt;
                //        DataRow drNew;
                //        DataColumn dct;
                //        DataSet dstd = new DataSet();
                //        dtt = new DataTable();



                //        dct = new DataColumn("Item");
                //        dtt.Columns.Add(dct);
                //        dct = new DataColumn("ItemId");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("Rate");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("SmpAvg");
                //        dtt.Columns.Add(dct);
                //        dct = new DataColumn("PrdAvg");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("SmpCost");
                //        dtt.Columns.Add(dct);
                //        dct = new DataColumn("PrdCost");
                //        dtt.Columns.Add(dct);

                //        dct = new DataColumn("Color");
                //        dtt.Columns.Add(dct);
                //        dct = new DataColumn("ColorId");
                //        dtt.Columns.Add(dct);

                //        dstd.Tables.Add(dtt);

                //        foreach (DataRow Dr in ds3.Tables[0].Rows)
                //        {
                //            drNew = dtt.NewRow();

                //            drNew["Item"] = Dr["Description"];
                //            drNew["ItemId"] = Dr["ItemCodeId"];

                //            drNew["Rate"] = Dr["Rate"];

                //            drNew["SmpAvg"] = Dr["SmpAvg"];
                //            drNew["PrdAvg"] = Dr["PrdAvg"];

                //            drNew["SmpCost"] = Dr["SmpCost"];
                //            drNew["PrdCost"] = Dr["PrdCost"];

                //            drNew["Color"] = Dr["Color"];
                //            drNew["ColorId"] = Dr["ColorId"];

                //            dstd.Tables[0].Rows.Add(drNew);
                //        }

                //        ViewState["CurrentTable2"] = dtt;

                //        GVItem.DataSource = dstd;
                //        GVItem.DataBind();

                //        #endregion

                //        #region Supplier binding

                //        DataSet ds4 = objBs.getiTransSamplingsupplier(Convert.ToInt32(SamplingCostingId));
                //        if (ds4.Tables[0].Rows.Count > 0)
                //        {

                //            DataTable dtt1;
                //            DataRow drNew1;
                //            DataColumn dct1;
                //            DataSet dstd1 = new DataSet();
                //            dtt1 = new DataTable();

                //            dct1 = new DataColumn("Item");
                //            dtt1.Columns.Add(dct1);
                //            dct1 = new DataColumn("ItemId");
                //            dtt1.Columns.Add(dct1);

                //            dct1 = new DataColumn("Rate");
                //            dtt1.Columns.Add(dct1);


                //            dct1 = new DataColumn("Color");
                //            dtt1.Columns.Add(dct1);
                //            dct1 = new DataColumn("ColorId");
                //            dtt1.Columns.Add(dct1);

                //            dct1 = new DataColumn("partytype");
                //            dtt1.Columns.Add(dct1);
                //            dct1 = new DataColumn("supplierid");
                //            dtt1.Columns.Add(dct1);

                //            dstd1.Tables.Add(dtt1);

                //            foreach (DataRow Dr1 in ds4.Tables[0].Rows)
                //            {
                //                drNew1 = dtt1.NewRow();

                //                drNew1["Item"] = Dr1["Description"];
                //                drNew1["ItemId"] = Dr1["ItemCodeId"];

                //                drNew1["Rate"] = Dr1["Rate"];



                //                drNew1["Color"] = Dr1["Color"];
                //                drNew1["ColorId"] = Dr1["ColorId"];

                //                drNew1["partytype"] = Dr1["PartyType"];
                //                drNew1["supplierid"] = Dr1["SuggestedSupplierId"];

                //                dstd1.Tables[0].Rows.Add(drNew1);
                //            }

                //            Gridsupplierdetails.DataSource = dstd1;
                //            Gridsupplierdetails.DataBind();

                //            for (int vLoop = 0; vLoop < Gridsupplierdetails.Rows.Count; vLoop++)
                //            {

                //                DropDownList ddlPartyType = (DropDownList)Gridsupplierdetails.Rows[vLoop].FindControl("ddlPartyType");

                //                CheckBoxList chksupplierlist = (CheckBoxList)Gridsupplierdetails.Rows[vLoop].FindControl("chksupplierlist");
                //                Label selectedsupplierlistID = (Label)Gridsupplierdetails.Rows[vLoop].FindControl("selectedsupplierlistID");

                //                ddlPartyType.SelectedValue = dstd1.Tables[0].Rows[vLoop]["PartyType"].ToString();

                //                if (ddlPartyType.SelectedValue != "Select PartyType")
                //                {

                //                    DataSet get_ledger = objBs.Get_ledger_List(ddlPartyType.SelectedValue);
                //                    if (get_ledger.Tables[0].Rows.Count > 0)
                //                    {
                //                        chksupplierlist.DataSource = get_ledger.Tables[0];
                //                        chksupplierlist.DataTextField = "Ledgername";
                //                        chksupplierlist.DataValueField = "Ledgerid";
                //                        chksupplierlist.DataBind();
                //                    }

                //                }

                //                selectedsupplierlistID.Text = dstd1.Tables[0].Rows[vLoop]["supplierid"].ToString();
                //            }


                //            for (int vLoop = 0; vLoop < Gridsupplierdetails.Rows.Count; vLoop++)
                //            {
                //                CheckBoxList chksupplierlist = (CheckBoxList)Gridsupplierdetails.Rows[vLoop].FindControl("chksupplierlist");
                //                Label selectedsupplierlistID = (Label)Gridsupplierdetails.Rows[vLoop].FindControl("selectedsupplierlistID");
                //                TextBox selectedsupplierlist = (TextBox)Gridsupplierdetails.Rows[vLoop].FindControl("selectedsupplierlist");

                //                string cond1 = "";

                //                string hobby = selectedsupplierlistID.Text;
                //                string[] hobbies = hobby.Split(new[] { "," }, StringSplitOptions.None);


                //                foreach (ListItem li in chksupplierlist.Items)
                //                {
                //                    li.Selected = hobbies.Contains(li.Value);
                //                    if (li.Selected == true)
                //                    {

                //                        cond1 += "" + li.Text + " ,";
                //                    }
                //                }
                //                cond1 = cond1.TrimEnd(',');

                //                selectedsupplierlist.Text = cond1;

                //            }

                //        }

                //        #endregion
                //    }

                //    DataSet ds5 = objBs.getiTransSamplingProcess(Convert.ToInt32(SamplingCostingId));
                //    if (ds5.Tables[0].Rows.Count > 0)
                //    {
                //        #region

                //        if (ds5.Tables[0].Rows.Count > 0)
                //        {
                //            for (int i = 0; i < chkpcsprocess.Items.Count; i++)
                //            {
                //                for (int j = 0; j < ds5.Tables[0].Rows.Count; j++)
                //                {
                //                    if (chkpcsprocess.Items[i].Value == ds5.Tables[0].Rows[j]["Processid"].ToString())
                //                    {
                //                        chkpcsprocess.Items[i].Selected = true;
                //                    }
                //                }
                //            }

                //            Pcsnew_process_Click(sender, e);
                //        }

                //        #endregion
                //    }
                //}
            }
        }


        protected void Pcsnew_process_Click(object sender, EventArgs e)
        {

            gvPcsProcessDetails.DataSource = null;
            gvPcsProcessDetails.DataBind();


            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("Pid");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Pname");
            dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);


            if (chkpcsprocess.SelectedIndex >= 0)
            {
                foreach (ListItem listItem in chkpcsprocess.Items)
                {
                    if (listItem.Selected)
                    {
                        drNew = dtt.NewRow();
                        drNew["Pid"] = listItem.Value;
                        drNew["Pname"] = listItem.Text;
                        dstd.Tables[0].Rows.Add(drNew);
                    }
                }

                gvPcsProcessDetails.DataSource = dstd;
                gvPcsProcessDetails.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Atleast One Process.Thank You.');", true);
                return;
            }


        }

        protected void Party_chnaged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList ddlPartyType = (DropDownList)row.FindControl("ddlPartyType");


            CheckBoxList chksupplierlist = (CheckBoxList)row.FindControl("chksupplierlist");



            if (ddlPartyType.SelectedValue != "Select PartyType")
            {

                DataSet get_ledger = objBs.Get_ledger_List(ddlPartyType.SelectedValue);
                if (get_ledger.Tables[0].Rows.Count > 0)
                {
                    chksupplierlist.DataSource = get_ledger.Tables[0];
                    chksupplierlist.DataTextField = "Ledgername";
                    chksupplierlist.DataValueField = "Ledgerid";
                    chksupplierlist.DataBind();
                }

            }
        }

        protected void Pcs_process_Click(object sender, EventArgs e)
        {

            Button ddl = (Button)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList ddlPartyType = (DropDownList)row.FindControl("ddlPartyType");

            CheckBoxList chksupplierlist = (CheckBoxList)row.FindControl("chksupplierlist");

            TextBox selectedsupplierlist = (TextBox)row.FindControl("selectedsupplierlist");

            Label selectedsupplierlistID = (Label)row.FindControl("selectedsupplierlistID");



            string cond1 = "";

            string condid = "";


            if (chksupplierlist.SelectedIndex >= 0)
            {
                foreach (ListItem listItem in chksupplierlist.Items)
                {
                    if (listItem.Selected)
                    {
                        cond1 += "" + listItem.Text + " ,";
                        condid += "" + listItem.Value + ",";
                    }
                }
                cond1 = cond1.TrimEnd(',');
                condid = condid.TrimEnd(',');


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Atleast One Process.Thank You.');", true);
                return;
            }

            selectedsupplierlist.Text = cond1;
            selectedsupplierlistID.Text = condid;
        }

        protected void Grid_Supplier(object sender, GridViewRowEventArgs e)
        {
            DataSet dsContactType = objBs.gridPartyType();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddlPartyType = (DropDownList)e.Row.FindControl("ddlPartyType");
                if (dsContactType.Tables[0].Rows.Count > 0)
                {
                    ddlPartyType.DataSource = dsContactType.Tables[0];
                    ddlPartyType.DataTextField = "PartyType";
                    ddlPartyType.DataValueField = "PartyTypeID";
                    ddlPartyType.DataBind();
                    ddlPartyType.Items.Insert(0, "Select PartyType");
                }
            }
        }

        protected void style_NO(object sender, EventArgs e)
        {

            if (txtStyleNo.Text != "")
            {
                txtBuyerPrintStyle.Text = txtStyleNo.Text;
                txtStyleNo.Focus();
            }


        }

        protected void ddlStyles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlapprovalStyles.SelectedValue != "" && ddlapprovalStyles.SelectedValue != "0" && ddlapprovalStyles.SelectedValue != "Select StyleNo")
            {
                btnSave.Text = "Approve";

                DataSet dsItemMaster = objBs.getiSamplingandCostingvalues(Convert.ToInt32(ddlapprovalStyles.SelectedValue));
                if (dsItemMaster.Tables[0].Rows.Count > 0)
                {
                    #region

                    txtStyleNo.Text = dsItemMaster.Tables[0].Rows[0]["StyleNo"].ToString();
                    ddlBuyerCode.SelectedValue = dsItemMaster.Tables[0].Rows[0]["BuyerCodeId"].ToString();
                    ddlBuyerName.SelectedValue = dsItemMaster.Tables[0].Rows[0]["BuyerCodeId"].ToString();
                    ddltax.SelectedValue = dsItemMaster.Tables[0].Rows[0]["TaxId"].ToString();
                    txtHSNCode.Text = dsItemMaster.Tables[0].Rows[0]["HSNCode"].ToString();
                    txtBuyerPrintStyle.Text = dsItemMaster.Tables[0].Rows[0]["BuyerPrintStyle"].ToString();

                    txtDescription.Text = dsItemMaster.Tables[0].Rows[0]["Description"].ToString();
                    ddlSize.SelectedValue = dsItemMaster.Tables[0].Rows[0]["SizeId"].ToString();

                    txtFabricationCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["FabricationCost"]).ToString("f2");
                    txtEmbroideryMachineCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["EmbroideryMachineCost"]).ToString("f2");
                    txtEmbroideryHandCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["EmbroideryHandCost"]).ToString("f2");
                    txtPieceProcessCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["PieceProcessCost"]).ToString("f2");
                    txtFinishingandPackingCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["FinishingandPackingCost"]).ToString("f2");
                    txtLogisticsCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["LogisticsCost"]).ToString("f2");

                    txtDate.Text = Convert.ToDateTime(dsItemMaster.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");

                    txtRejection.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["Rejection"]).ToString("f2");
                    txtExtraMargin.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["ExtraMargin"]).ToString("f2");

                    txtSmpCostPerPiece.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["SmpCostPerPiece"]).ToString("f2");
                    txtPrdCostPerPiece.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["PrdCostPerPiece"]).ToString("f2");

                    txtTotalSmpCostINR.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["TotalSmpCostINR"]).ToString("f2");
                    txtTotalPrdCostINR.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["TotalPrdCostINR"]).ToString("f2");

                    ddlCostCurrency.SelectedValue = dsItemMaster.Tables[0].Rows[0]["TotalCostOtherId"].ToString();

                    txtTotalSmpCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["TotalSmpCostOther"]).ToString("f2");
                    txtTotalPrdCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["TotalPrdCostOther"]).ToString("f2");

                    lblItemSmpCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["ItemSmpCost"]).ToString("f2");
                    lblItemPrdCost.Text = Convert.ToDouble(dsItemMaster.Tables[0].Rows[0]["ItemPrdCost"]).ToString("f2");

                    lblFile_Path.Text = dsItemMaster.Tables[0].Rows[0]["Sketch"].ToString();
                    img_Photo.ImageUrl = dsItemMaster.Tables[0].Rows[0]["Sketch"].ToString();

                    #endregion
                }

                DataSet ds2 = objBs.getiTransSamplingandCostingvalues(Convert.ToInt32(ddlapprovalStyles.SelectedValue));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dtt;
                    DataRow drNew;
                    DataColumn dct;
                    DataSet dstd = new DataSet();
                    dtt = new DataTable();

                    dct = new DataColumn("Item");
                    dtt.Columns.Add(dct);
                    dct = new DataColumn("ItemId");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("Rate");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("SmpAvg");
                    dtt.Columns.Add(dct);
                    dct = new DataColumn("PrdAvg");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("SmpCost");
                    dtt.Columns.Add(dct);
                    dct = new DataColumn("PrdCost");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("ColorId");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("IsSelected");
                    dtt.Columns.Add(dct);

                    dstd.Tables.Add(dtt);

                    foreach (DataRow Dr in ds2.Tables[0].Rows)
                    {
                        drNew = dtt.NewRow();

                        drNew["Item"] = Dr["Description"];
                        drNew["ItemId"] = Dr["ItemCodeId"];

                        drNew["Rate"] = Dr["Rate"];

                        drNew["SmpAvg"] = Dr["SmpAvg"];
                        drNew["PrdAvg"] = Dr["PrdAvg"];

                        drNew["SmpCost"] = Dr["SmpCost"];
                        drNew["PrdCost"] = Dr["PrdCost"];

                        drNew["ColorId"] = Dr["ColorId"];

                        drNew["IsSelected"] = Dr["IsSelected"];

                        dstd.Tables[0].Rows.Add(drNew);
                    }

                    ViewState["CurrentTable1"] = dtt;

                    GVdsItemCodeDescriptionItemType.DataSource = dstd;
                    GVdsItemCodeDescriptionItemType.DataBind();

                    for (int vLoop = 0; vLoop < GVdsItemCodeDescriptionItemType.Rows.Count; vLoop++)
                    {
                        CheckBox chkSelect = (CheckBox)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("chkSelect");
                        DropDownList ddlColor = (DropDownList)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("ddlColor");

                        if (dstd.Tables[0].Rows[vLoop]["IsSelected"].ToString() == "True")
                        {
                            chkSelect.Checked = true;
                        }

                        ddlColor.SelectedValue = dstd.Tables[0].Rows[vLoop]["ColorId"].ToString();

                    }

                    #endregion
                }

                DataSet ds3 = objBs.getiTransSamplingandCostingvalues1(Convert.ToInt32(ddlapprovalStyles.SelectedValue));
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dtt;
                    DataRow drNew;
                    DataColumn dct;
                    DataSet dstd = new DataSet();
                    dtt = new DataTable();

                    dct = new DataColumn("Item");
                    dtt.Columns.Add(dct);
                    dct = new DataColumn("ItemId");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("Rate");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("SmpAvg");
                    dtt.Columns.Add(dct);
                    dct = new DataColumn("PrdAvg");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("SmpCost");
                    dtt.Columns.Add(dct);
                    dct = new DataColumn("PrdCost");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("Color");
                    dtt.Columns.Add(dct);
                    dct = new DataColumn("ColorId");
                    dtt.Columns.Add(dct);

                    dstd.Tables.Add(dtt);

                    foreach (DataRow Dr in ds3.Tables[0].Rows)
                    {
                        drNew = dtt.NewRow();

                        drNew["Item"] = Dr["Description"];
                        drNew["ItemId"] = Dr["ItemCodeId"];

                        drNew["Rate"] = Dr["Rate"];

                        drNew["SmpAvg"] = Dr["SmpAvg"];
                        drNew["PrdAvg"] = Dr["PrdAvg"];

                        drNew["SmpCost"] = Dr["SmpCost"];
                        drNew["PrdCost"] = Dr["PrdCost"];

                        drNew["Color"] = Dr["Color"];
                        drNew["ColorId"] = Dr["ColorId"];

                        dstd.Tables[0].Rows.Add(drNew);
                    }

                    ViewState["CurrentTable2"] = dtt;

                    GVItem.DataSource = dstd;
                    GVItem.DataBind();

                    // Gridsupplierdetails.DataSource = dstd;
                    // Gridsupplierdetails.DataBind();

                    #region Supplier binding

                    DataSet ds4 = objBs.getiTransSamplingsupplier(Convert.ToInt32(ddlapprovalStyles.SelectedValue));
                    if (ds4.Tables[0].Rows.Count > 0)
                    {

                        DataTable dtt1;
                        DataRow drNew1;
                        DataColumn dct1;
                        DataSet dstd1 = new DataSet();
                        dtt1 = new DataTable();

                        dct1 = new DataColumn("Item");
                        dtt1.Columns.Add(dct1);
                        dct1 = new DataColumn("ItemId");
                        dtt1.Columns.Add(dct1);

                        dct1 = new DataColumn("Rate");
                        dtt1.Columns.Add(dct1);


                        dct1 = new DataColumn("Color");
                        dtt1.Columns.Add(dct1);
                        dct1 = new DataColumn("ColorId");
                        dtt1.Columns.Add(dct1);

                        dct1 = new DataColumn("partytype");
                        dtt1.Columns.Add(dct1);
                        dct1 = new DataColumn("supplierid");
                        dtt1.Columns.Add(dct1);

                        dstd1.Tables.Add(dtt1);

                        foreach (DataRow Dr1 in ds4.Tables[0].Rows)
                        {
                            drNew1 = dtt1.NewRow();

                            drNew1["Item"] = Dr1["Description"];
                            drNew1["ItemId"] = Dr1["ItemCodeId"];

                            drNew1["Rate"] = Dr1["Rate"];



                            drNew1["Color"] = Dr1["Color"];
                            drNew1["ColorId"] = Dr1["ColorId"];

                            drNew1["partytype"] = Dr1["PartyType"];
                            drNew1["supplierid"] = Dr1["SuggestedSupplierId"];

                            dstd1.Tables[0].Rows.Add(drNew1);
                        }

                        Gridsupplierdetails.DataSource = dstd1;
                        Gridsupplierdetails.DataBind();

                        for (int vLoop = 0; vLoop < Gridsupplierdetails.Rows.Count; vLoop++)
                        {

                            DropDownList ddlPartyType = (DropDownList)Gridsupplierdetails.Rows[vLoop].FindControl("ddlPartyType");

                            CheckBoxList chksupplierlist = (CheckBoxList)Gridsupplierdetails.Rows[vLoop].FindControl("chksupplierlist");
                            Label selectedsupplierlistID = (Label)Gridsupplierdetails.Rows[vLoop].FindControl("selectedsupplierlistID");

                            ddlPartyType.SelectedValue = dstd1.Tables[0].Rows[vLoop]["PartyType"].ToString();

                            if (ddlPartyType.SelectedValue != "Select PartyType")
                            {

                                DataSet get_ledger = objBs.Get_ledger_List(ddlPartyType.SelectedValue);
                                if (get_ledger.Tables[0].Rows.Count > 0)
                                {
                                    chksupplierlist.DataSource = get_ledger.Tables[0];
                                    chksupplierlist.DataTextField = "Ledgername";
                                    chksupplierlist.DataValueField = "Ledgerid";
                                    chksupplierlist.DataBind();
                                }

                            }

                            selectedsupplierlistID.Text = dstd1.Tables[0].Rows[vLoop]["supplierid"].ToString();
                        }


                        for (int vLoop = 0; vLoop < Gridsupplierdetails.Rows.Count; vLoop++)
                        {
                            CheckBoxList chksupplierlist = (CheckBoxList)Gridsupplierdetails.Rows[vLoop].FindControl("chksupplierlist");
                            Label selectedsupplierlistID = (Label)Gridsupplierdetails.Rows[vLoop].FindControl("selectedsupplierlistID");
                            TextBox selectedsupplierlist = (TextBox)Gridsupplierdetails.Rows[vLoop].FindControl("selectedsupplierlist");

                            string cond1 = "";

                            string hobby = selectedsupplierlistID.Text;
                            string[] hobbies = hobby.Split(new[] { "," }, StringSplitOptions.None);


                            foreach (ListItem li in chksupplierlist.Items)
                            {
                                li.Selected = hobbies.Contains(li.Value);
                                if (li.Selected == true)
                                {

                                    cond1 += "" + li.Text + " ,";
                                }
                            }
                            cond1 = cond1.TrimEnd(',');

                            selectedsupplierlist.Text = cond1;

                        }

                    }

                    #endregion

                    #endregion
                }
                DataSet ds5 = objBs.getiTransSamplingProcess(Convert.ToInt32(ddlapprovalStyles.SelectedValue));
                if (ds5.Tables[0].Rows.Count > 0)
                {
                    #region

                    if (ds5.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < chkpcsprocess.Items.Count; i++)
                        {
                            for (int j = 0; j < ds5.Tables[0].Rows.Count; j++)
                            {
                                if (chkpcsprocess.Items[i].Value == ds5.Tables[0].Rows[j]["Processid"].ToString())
                                {
                                    chkpcsprocess.Items[i].Selected = true;
                                }
                            }
                        }

                        Pcsnew_process_Click(sender, e);
                    }

                    #endregion
                }
            }
        }

        protected void ddlItems_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItems.SelectedValue != "" && ddlItems.SelectedValue != "0" && ddlItems.SelectedValue != "Select Item")
            {
                DataSet dsItemMaster = objBs.getItemMaster_ItemDetails(Convert.ToInt32(ddlItems.SelectedValue));
                if (dsItemMaster.Tables[0].Rows.Count > 0)
                {
                    lblItemDetails.Text = "ItemCode:(" + dsItemMaster.Tables[0].Rows[0]["ItemCode"].ToString() + "),    ItemType:(" + dsItemMaster.Tables[0].Rows[0]["Itemgroupname"].ToString() + "),   Category:(" + dsItemMaster.Tables[0].Rows[0]["Category"].ToString() + "),   Size:(" + dsItemMaster.Tables[0].Rows[0]["Size"].ToString() + "),   OrderUOM:(" + dsItemMaster.Tables[0].Rows[0]["OrderUOM"].ToString() + "),   IssueUOM:(" + dsItemMaster.Tables[0].Rows[0]["IssueUOM"].ToString() + ")";
                }
                else
                {
                    lblItemDetails.Text = "";
                }

            }
            else
            {
                lblItemDetails.Text = "";
            }

            txtRate.Focus();
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            //GVdsItemCodeDescriptionItemType.DataSource = null;
            //GVdsItemCodeDescriptionItemType.DataBind();
            //ViewState["CurrentTable1"] = null;

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
            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SmpAvg");
            dttt.Columns.Add(dct);
            dct = new DataColumn("PrdAvg");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SmpCost");
            dttt.Columns.Add(dct);
            dct = new DataColumn("PrdCost");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                drNew = dttt.NewRow();

                drNew["Item"] = ddlItems.SelectedItem.Text;
                drNew["ItemId"] = ddlItems.SelectedValue;
                drNew["Rate"] = txtRate.Text;

                drNew["SmpAvg"] = txtSmpAvg.Text;
                drNew["PrdAvg"] = txtPrdAvg.Text;

                drNew["SmpCost"] = Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtSmpAvg.Text);
                drNew["PrdCost"] = Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtPrdAvg.Text);

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

            }
            else
            {

                drNew = dttt.NewRow();

                drNew["Item"] = ddlItems.SelectedItem.Text;
                drNew["ItemId"] = ddlItems.SelectedValue;
                drNew["Rate"] = txtRate.Text;

                drNew["SmpAvg"] = txtSmpAvg.Text;
                drNew["PrdAvg"] = txtPrdAvg.Text;

                drNew["SmpCost"] = Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtSmpAvg.Text);
                drNew["PrdCost"] = Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtPrdAvg.Text);

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
            }

            #endregion

            ViewState["CurrentTable1"] = dtddd;

            GVdsItemCodeDescriptionItemType.DataSource = dtddd;
            GVdsItemCodeDescriptionItemType.DataBind();


        }
        protected void btnsupplierdetails_OnClick(object sender, EventArgs e)
        {
            Gridsupplierdetails.DataSource = (DataTable)ViewState["CurrentTable2"];
            Gridsupplierdetails.DataBind();


        }
        protected void btnAddItems_OnClick(object sender, EventArgs e)
        {
            GVItem.DataSource = null;
            GVItem.DataBind();
            ViewState["CurrentTable2"] = null;

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
            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SmpAvg");
            dttt.Columns.Add(dct);
            dct = new DataColumn("PrdAvg");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SmpCost");
            dttt.Columns.Add(dct);
            dct = new DataColumn("PrdCost");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ColorId");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);


            for (int i = 0; i < GVdsItemCodeDescriptionItemType.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("chkSelect");

                HiddenField hdItemId = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("hdItemId");
                HiddenField hdItem = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("hdItem");
                HiddenField hdRate = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("hdRate");
                HiddenField hdSmpAvg = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("hdSmpAvg");
                HiddenField hdPrdAvg = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("hdPrdAvg");
                HiddenField hdSmpCost = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("hdSmpCost");
                HiddenField hdPrdCost = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("hdPrdCost");

                DropDownList ddlColor = (DropDownList)GVdsItemCodeDescriptionItemType.Rows[i].FindControl("ddlColor");

                if (chkSelect.Checked == true)
                {
                    if (ViewState["CurrentTable2"] != null)
                    {
                        DataTable dt = (DataTable)ViewState["CurrentTable2"];

                        drNew = dttt.NewRow();

                        drNew["Item"] = hdItem.Value;
                        drNew["ItemId"] = hdItemId.Value;
                        drNew["Rate"] = hdRate.Value;

                        drNew["SmpAvg"] = hdSmpAvg.Value;
                        drNew["PrdAvg"] = hdPrdAvg.Value;

                        drNew["SmpCost"] = hdSmpCost.Value;
                        drNew["PrdCost"] = hdPrdCost.Value;

                        drNew["Color"] = ddlColor.SelectedItem.Text;
                        drNew["ColorId"] = ddlColor.SelectedValue;

                        dstd.Tables[0].Rows.Add(drNew);
                        dtddd = dstd.Tables[0];
                        dtddd.Merge(dt);

                    }
                    else
                    {

                        drNew = dttt.NewRow();

                        drNew["Item"] = hdItem.Value;
                        drNew["ItemId"] = hdItemId.Value;
                        drNew["Rate"] = hdRate.Value;

                        drNew["SmpAvg"] = hdSmpAvg.Value;
                        drNew["PrdAvg"] = hdPrdAvg.Value;

                        drNew["SmpCost"] = hdSmpCost.Value;
                        drNew["PrdCost"] = hdPrdCost.Value;

                        drNew["Color"] = ddlColor.SelectedItem.Text;
                        drNew["ColorId"] = ddlColor.SelectedValue;

                        dstd.Tables[0].Rows.Add(drNew);
                        dtddd = dstd.Tables[0];
                    }
                }
            }

            #endregion

            ViewState["CurrentTable2"] = dtddd;

            GVItem.DataSource = dtddd;
            GVItem.DataBind();
            Gridsupplierdetails.DataSource = dtddd;
            Gridsupplierdetails.DataBind();

            Calculations();
        }

        protected void ddlBuyerCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBuyerCode.SelectedValue != "" && ddlBuyerCode.SelectedValue != "0" && ddlBuyerCode.SelectedValue != "BuyerCode")
            {
                ddlBuyerName.SelectedValue = ddlBuyerCode.SelectedValue;
            }
            else
            {
                ddlBuyerName.ClearSelection();
            }
        }

        protected void txtFabricationCost_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
            txtFabricationCost.Focus();
        }
        protected void txtEmbroideryMachineCost_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
            txtEmbroideryMachineCost.Focus();
        }
        protected void txtEmbroideryHandCost_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
            txtEmbroideryHandCost.Focus();
        }
        protected void txtPieceProcessCost_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
            txtPieceProcessCost.Focus();
        }
        protected void txtLogisticsCost_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
            txtLogisticsCost.Focus();
        }
        protected void txtFinishingandPackingCost_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
            txtFinishingandPackingCost.Focus();
        }

        protected void txtRejection_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
            txtRejection.Focus();
        }
        protected void txtExtraMargin_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
            txtExtraMargin.Focus();
        }

        protected void txtRate_OnTextChanged(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtRate = (TextBox)row.FindControl("txtRate");

            double ItemCost = 0;
            for (int i = 0; i < GVItem.Rows.Count; i++)
            {
                TextBox txtRate1 = (TextBox)GVItem.Rows[i].FindControl("txtRate");
                TextBox txtQuantity1 = (TextBox)GVItem.Rows[i].FindControl("txtQuantity");
                TextBox txtCost1 = (TextBox)GVItem.Rows[i].FindControl("txtCost");

                CheckBox chkSelect = (CheckBox)GVItem.Rows[i].FindControl("chkSelect");

                if (chkSelect.Checked == true)
                {
                    if (txtRate1.Text == "")
                        txtRate1.Text = "0";
                    if (txtQuantity1.Text == "")
                        txtQuantity1.Text = "0";

                    txtCost1.Text = (Convert.ToDouble(txtRate1.Text) * Convert.ToDouble(txtQuantity1.Text)).ToString("f2");
                    ItemCost += (Convert.ToDouble(txtCost1.Text));
                }
            }

            lblItemSmpCost.Text = ItemCost.ToString("f2");

            Calculations();
            txtRate.Focus();
        }
        protected void txtQuantity_OnTextChanged(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");

            double ItemCost = 0;
            for (int i = 0; i < GVItem.Rows.Count; i++)
            {
                TextBox txtRate1 = (TextBox)GVItem.Rows[i].FindControl("txtRate");
                TextBox txtQuantity1 = (TextBox)GVItem.Rows[i].FindControl("txtQuantity");
                TextBox txtCost1 = (TextBox)GVItem.Rows[i].FindControl("txtCost");
                CheckBox chkSelect = (CheckBox)GVItem.Rows[i].FindControl("chkSelect");

                if (chkSelect.Checked == true)
                {
                    if (txtRate1.Text == "")
                        txtRate1.Text = "0";
                    if (txtQuantity1.Text == "")
                        txtQuantity1.Text = "0";

                    txtCost1.Text = (Convert.ToDouble(txtRate1.Text) * Convert.ToDouble(txtQuantity1.Text)).ToString("f2");
                    ItemCost += (Convert.ToDouble(txtCost1.Text));
                }
            }

            lblItemSmpCost.Text = ItemCost.ToString("f2");

            Calculations();
            txtQuantity.Focus();
        }
        protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox ddl = (CheckBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            CheckBox chkSelect1 = (CheckBox)row.FindControl("chkSelect");

            double ItemCost = 0;
            for (int i = 0; i < GVItem.Rows.Count; i++)
            {
                TextBox txtRate1 = (TextBox)GVItem.Rows[i].FindControl("txtRate");
                TextBox txtQuantity1 = (TextBox)GVItem.Rows[i].FindControl("txtQuantity");
                TextBox txtCost1 = (TextBox)GVItem.Rows[i].FindControl("txtCost");
                CheckBox chkSelect = (CheckBox)GVItem.Rows[i].FindControl("chkSelect");

                if (chkSelect.Checked == true)
                {
                    if (txtRate1.Text == "")
                        txtRate1.Text = "0";
                    if (txtQuantity1.Text == "")
                        txtQuantity1.Text = "0";

                    txtCost1.Text = (Convert.ToDouble(txtRate1.Text) * Convert.ToDouble(txtQuantity1.Text)).ToString("f2");
                    ItemCost += (Convert.ToDouble(txtCost1.Text));
                }
            }

            lblItemSmpCost.Text = ItemCost.ToString("f2");

            Calculations();
            chkSelect1.Focus();
        }

        protected void ddlCostCurrency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Calculations();
            ddlCostCurrency.Focus();
        }

        public void Calculations()
        {
            double ItemSmpCost = 0; double ItemPrdCost = 0;
            for (int i = 0; i < GVItem.Rows.Count; i++)
            {
                HiddenField hdSmpCost = (HiddenField)GVItem.Rows[i].FindControl("hdSmpCost");
                HiddenField hdPrdCost = (HiddenField)GVItem.Rows[i].FindControl("hdPrdCost");

                ItemSmpCost += (Convert.ToDouble(hdSmpCost.Value));
                ItemPrdCost += (Convert.ToDouble(hdPrdCost.Value));

            }

            lblItemSmpCost.Text = ItemSmpCost.ToString("f2");
            lblItemPrdCost.Text = ItemPrdCost.ToString("f2");


            if (txtFabricationCost.Text == "")
                txtFabricationCost.Text = "0";
            if (txtEmbroideryMachineCost.Text == "")
                txtEmbroideryMachineCost.Text = "0";
            if (txtEmbroideryHandCost.Text == "")
                txtEmbroideryHandCost.Text = "0";
            if (txtPieceProcessCost.Text == "")
                txtPieceProcessCost.Text = "0";
            if (txtFinishingandPackingCost.Text == "")
                txtFinishingandPackingCost.Text = "0";

            if (txtRejection.Text == "")
                txtRejection.Text = "0";
            if (txtExtraMargin.Text == "")
                txtExtraMargin.Text = "0";
            if (txtLogisticsCost.Text == "")
                txtLogisticsCost.Text = "0";
            //SmpCost
            txtSmpCostPerPiece.Text = (Convert.ToDouble(txtFabricationCost.Text) + Convert.ToDouble(txtEmbroideryMachineCost.Text) + Convert.ToDouble(txtEmbroideryHandCost.Text) + Convert.ToDouble(txtPieceProcessCost.Text) + Convert.ToDouble(txtFinishingandPackingCost.Text) + Convert.ToDouble(lblItemSmpCost.Text) + Convert.ToDouble(txtLogisticsCost.Text)).ToString("f2");

            double SmpCostPerPiece_ItemCost = Convert.ToDouble(txtSmpCostPerPiece.Text);

            double SmpRejection = (SmpCostPerPiece_ItemCost * Convert.ToDouble(txtRejection.Text)) / 100;
            double SmpExtraMargin = ((SmpCostPerPiece_ItemCost + SmpRejection) * Convert.ToDouble(txtExtraMargin.Text)) / 100;

            double TotalSmpCostINR = SmpCostPerPiece_ItemCost + SmpRejection + SmpExtraMargin;
            txtTotalSmpCostINR.Text = TotalSmpCostINR.ToString("f2");

            string[] SmpCostCurrency = ddlCostCurrency.SelectedItem.Text.Split('&');
            txtTotalSmpCost.Text = (TotalSmpCostINR / Convert.ToDouble(SmpCostCurrency[1].ToString())).ToString("f2");

            //PrdCost
            txtPrdCostPerPiece.Text = (Convert.ToDouble(txtFabricationCost.Text) + Convert.ToDouble(txtEmbroideryMachineCost.Text) + Convert.ToDouble(txtEmbroideryHandCost.Text) + Convert.ToDouble(txtPieceProcessCost.Text) + Convert.ToDouble(txtFinishingandPackingCost.Text) + Convert.ToDouble(lblItemPrdCost.Text) + Convert.ToDouble(txtLogisticsCost.Text)).ToString("f2");

            double PrdCostPerPiece_ItemCost = Convert.ToDouble(txtPrdCostPerPiece.Text);

            double PrdRejection = (PrdCostPerPiece_ItemCost * Convert.ToDouble(txtRejection.Text)) / 100;
            double PrdExtraMargin = ((PrdCostPerPiece_ItemCost + PrdRejection) * Convert.ToDouble(txtExtraMargin.Text)) / 100;

            double TotalPrdCostINR = PrdCostPerPiece_ItemCost + PrdRejection + PrdExtraMargin;
            txtTotalPrdCostINR.Text = TotalPrdCostINR.ToString("f2");

            string[] PrdCostCurrency = ddlCostCurrency.SelectedItem.Text.Split('&');
            txtTotalPrdCost.Text = (TotalPrdCostINR / Convert.ToDouble(PrdCostCurrency[1].ToString())).ToString("f2");

        }

        protected void btnCostCurrency_OnClick(object sender, EventArgs e)
        {
            string yourUrl = "CurrencyMaster.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);
        }
        protected void btnCostCurrencyRef_OnClick(object sender, EventArgs e)
        {
            ddlCostCurrency.Items.Clear();

            DataSet dsCurrency = objBs.gridCurrencywithValue();
            if (dsCurrency.Tables[0].Rows.Count > 0)
            {
                ddlCostCurrency.DataSource = dsCurrency.Tables[0];
                ddlCostCurrency.DataTextField = "CurrencyName";
                ddlCostCurrency.DataValueField = "CurrencyId";
                ddlCostCurrency.DataBind();
            }

        }

        protected void btnSizeRefresh_OnClick(object sender, EventArgs e)
        {
            ddlSize.Items.Clear();

            DataSet dssize = objBs.selectsize();
            if (dssize.Tables[0].Rows.Count > 0)
            {
                ddlSize.DataSource = dssize.Tables[0];
                ddlSize.DataTextField = "Size";
                ddlSize.DataValueField = "SizeId";
                ddlSize.DataBind();
            }

        }

        protected void GVdsItemCodeDescriptionItemType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int vLoop = GVItem.Rows.Count;
                if (vLoop == 1)
                {
                    DropDownList ddlItemCode = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlItemCode");
                    DropDownList ddlDescription = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlDescription");
                    DropDownList ddlItemType = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlItemType");

                    if (ddlItemCode.SelectedValue == "Select ItemCode")
                    {
                        ddlItemCode.SelectedValue = e.CommandArgument.ToString();
                        ddlDescription.SelectedValue = e.CommandArgument.ToString();
                        ddlItemType.SelectedValue = e.CommandArgument.ToString();

                        AddNewRow1();
                    }
                    else
                    {
                        AddNewRow1();
                        vLoop = GVItem.Rows.Count;

                        DropDownList ddlItemCode1 = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlItemCode");
                        DropDownList ddlDescription1 = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlDescription");
                        DropDownList ddlItemType1 = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlItemType");

                        ddlItemCode1.SelectedValue = e.CommandArgument.ToString();
                        ddlDescription1.SelectedValue = e.CommandArgument.ToString();
                        ddlItemType1.SelectedValue = e.CommandArgument.ToString();
                    }
                }
                else
                {
                    DropDownList ddlItemCode = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlItemCode");
                    DropDownList ddlDescription = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlDescription");
                    DropDownList ddlItemType = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlItemType");

                    if (ddlItemCode.SelectedValue == "Select ItemCode")
                    {
                        ddlItemCode.SelectedValue = e.CommandArgument.ToString();
                        ddlDescription.SelectedValue = e.CommandArgument.ToString();
                        ddlItemType.SelectedValue = e.CommandArgument.ToString();

                        AddNewRow1();
                    }
                    else
                    {
                        AddNewRow1();
                        vLoop = GVItem.Rows.Count;

                        DropDownList ddlItemCode1 = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlItemCode");
                        DropDownList ddlDescription1 = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlDescription");
                        DropDownList ddlItemType1 = (DropDownList)GVItem.Rows[vLoop - 1].FindControl("ddlItemType");

                        ddlItemCode1.SelectedValue = e.CommandArgument.ToString();
                        ddlDescription1.SelectedValue = e.CommandArgument.ToString();
                        ddlItemType1.SelectedValue = e.CommandArgument.ToString();
                    }
                }

            }
        }

        protected void ddlItemCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList TX = (DropDownList)sender;
            GridViewRow row = (GridViewRow)TX.NamingContainer;

            DropDownList ddlItemCode = (DropDownList)row.FindControl("ddlItemCode");
            DropDownList ddlDescription = (DropDownList)row.FindControl("ddlDescription");
            DropDownList ddlItemType = (DropDownList)row.FindControl("ddlItemType");

            if (ddlItemCode.SelectedValue != "" && ddlItemCode.SelectedValue != "0" && ddlItemCode.SelectedValue != "Select ItemCode")
            {
                ddlDescription.SelectedValue = ddlItemCode.SelectedValue;
                ddlItemType.SelectedValue = ddlItemCode.SelectedValue;
            }
            else
            {
                ddlDescription.ClearSelection();
                ddlItemType.ClearSelection();
            }

        }
        protected void ddlDescription_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList TX = (DropDownList)sender;
            GridViewRow row = (GridViewRow)TX.NamingContainer;

            DropDownList ddlItemCode = (DropDownList)row.FindControl("ddlItemCode");
            DropDownList ddlDescription = (DropDownList)row.FindControl("ddlDescription");
            DropDownList ddlItemType = (DropDownList)row.FindControl("ddlItemType");

            if (ddlDescription.SelectedValue != "" && ddlDescription.SelectedValue != "0" && ddlDescription.SelectedValue != "Select Description")
            {
                ddlItemCode.SelectedValue = ddlDescription.SelectedValue;
                ddlItemType.SelectedValue = ddlDescription.SelectedValue;
            }
            else
            {
                ddlItemCode.ClearSelection();
                ddlItemType.ClearSelection();
            }

        }
        protected void ddlItemType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList TX = (DropDownList)sender;
            GridViewRow row = (GridViewRow)TX.NamingContainer;

            DropDownList ddlItemCode = (DropDownList)row.FindControl("ddlItemCode");
            DropDownList ddlDescription = (DropDownList)row.FindControl("ddlDescription");
            DropDownList ddlItemType = (DropDownList)row.FindControl("ddlItemType");

            if (ddlItemType.SelectedValue != "" && ddlItemType.SelectedValue != "0" && ddlItemType.SelectedValue != "Select ItemType")
            {
                ddlItemCode.SelectedValue = ddlItemType.SelectedValue;
                ddlDescription.SelectedValue = ddlItemType.SelectedValue;
            }
            else
            {
                ddlItemCode.ClearSelection();
                ddlDescription.ClearSelection();
            }

        }

        protected void GVdsItemCodeDescriptionItemType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsColor = objBs.gridColor();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddlColor = (DropDownList)e.Row.FindControl("ddlColor");
                ddlColor.DataSource = dsColor.Tables[0];
                ddlColor.DataTextField = "Color";
                ddlColor.DataValueField = "ColorID";
                ddlColor.DataBind();
            }

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
                        DropDownList ddlDescription = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlDescription");
                        DropDownList ddlItemType = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemType");

                        DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtCost");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlDescription.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemType.SelectedValue;

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
                        DropDownList ddlDescription = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlDescription");
                        DropDownList ddlItemType = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemType");

                        DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtRate");
                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                        TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtCost");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlDescription.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemType.SelectedValue;

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
                        DropDownList ddlDescription = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlDescription");
                        DropDownList ddlItemType = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemType");

                        DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtCost");

                        ddlItemCode.SelectedValue = dt.Rows[i]["ItemCode"].ToString();
                        ddlDescription.SelectedValue = dt.Rows[i]["ItemCode"].ToString();
                        ddlItemType.SelectedValue = dt.Rows[i]["ItemCode"].ToString();

                        ddlColor.SelectedValue = dt.Rows[i]["Color"].ToString();

                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtQuantity.Text = dt.Rows[i]["Quantity"].ToString();
                        txtCost.Text = dt.Rows[i]["Cost"].ToString();

                        if (txtCost.Text == "")
                            txtCost.Text = "0";

                        ItemCost += Convert.ToDouble(txtCost.Text);

                        rowIndex++;

                    }
                }
            }

            lblItemSmpCost.Text = ItemCost.ToString("f2");

        }

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            if (fp_Upload.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
                fp_Upload.PostedFile.SaveAs(Server.MapPath("~/Sampling/") + fileName.Replace(" ", ""));
                lblFile_Path.Text = "~/Sampling/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
                img_Photo.ImageUrl = "~/Sampling/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
            }
        }

        protected void GVdsItemCodeDescriptionItemType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //  SetRowData1();
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
                    GVdsItemCodeDescriptionItemType.DataSource = dt;
                    GVdsItemCodeDescriptionItemType.DataBind();

                    //   SetPreviousData1();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVdsItemCodeDescriptionItemType.DataSource = dt;
                    GVdsItemCodeDescriptionItemType.DataBind();

                    //  SetPreviousData1();
                    //  FirstGridViewRow1();
                }
            }
        }

        protected void GVItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //SetRowData1();
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

                    // SetPreviousData1();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable2"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();

                    // SetPreviousData1();
                    // FirstGridViewRow1();
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {



            int GVdsItemCodeDescriptionItemType_Count = 0;
            for (int vLoop = 0; vLoop < GVdsItemCodeDescriptionItemType.Rows.Count; vLoop++)
            {
                #region

                CheckBox chkSelect = (CheckBox)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("chkSelect");
                HiddenField hdItemId = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("hdItemId");
                HiddenField hdItem = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("hdItem");

                if (chkSelect.Checked == true)
                {
                    GVdsItemCodeDescriptionItemType_Count++;

                    string IS = "No";

                    for (int vLoop1 = 0; vLoop1 < GVItem.Rows.Count; vLoop1++)
                    {
                        HiddenField hdItemId1 = (HiddenField)GVItem.Rows[vLoop1].FindControl("hdItemId");

                        if (hdItemId.Value == hdItemId1.Value)
                        {
                            IS = "Yes";
                        }
                    }

                    if (IS == "No")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item " + hdItem.Value + " was Not Exists. ')", true);
                        return;
                    }
                }

                #endregion

            }

            if (GVdsItemCodeDescriptionItemType_Count != GVItem.Rows.Count)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Item details.')", true);
                return;
            }

            string[] CostCurrency = ddlCostCurrency.SelectedItem.Text.Split('&');

            DateTime Date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime CurDate = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);


            {
                string SamplingCostingId = ddlapprovalStyles.SelectedValue;





                DataSet dsStyleNo = objBs.SamplingandCostingsrchgrid(txtStyleNo.Text, Convert.ToInt32(SamplingCostingId));
                if (dsStyleNo.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This StyleNo was already Exists.')", true);
                    txtStyleNo.Focus();
                    return;
                }
                else
                {

                    string status = "Y";
                    string narration = txtnotes.Text;

                    int iapprovaestatus = objBs.Iapprovestatus(ddlapprovalStyles.SelectedValue, status, lblUser.Text, narration, CurDate);


                    int iStatus = objBs.UpdateSamplingandCosting(txtStyleNo.Text, Convert.ToInt32(ddlBuyerCode.SelectedValue), txtDescription.Text, Convert.ToInt32(ddlSize.SelectedValue), Convert.ToDouble(txtFabricationCost.Text), Convert.ToDouble(txtEmbroideryMachineCost.Text), Convert.ToDouble(txtEmbroideryHandCost.Text), Convert.ToDouble(txtPieceProcessCost.Text), Convert.ToDouble(txtFinishingandPackingCost.Text), Date, Convert.ToDouble(txtRejection.Text), Convert.ToDouble(txtExtraMargin.Text), Convert.ToDouble(txtSmpCostPerPiece.Text), Convert.ToDouble(txtPrdCostPerPiece.Text), Convert.ToDouble(txtTotalSmpCostINR.Text), Convert.ToDouble(txtTotalPrdCostINR.Text), Convert.ToInt32(ddlCostCurrency.SelectedValue), Convert.ToDouble(CostCurrency[1].ToString()), Convert.ToDouble(txtTotalSmpCost.Text), Convert.ToDouble(txtTotalPrdCost.Text), lblFile_Path.Text, Convert.ToInt32(SamplingCostingId), txtBuyerPrintStyle.Text, Convert.ToDouble(lblItemSmpCost.Text), Convert.ToDouble(lblItemPrdCost.Text), Convert.ToDouble(txtLogisticsCost.Text), "", 0);




                    for (int vLoop = 0; vLoop < GVdsItemCodeDescriptionItemType.Rows.Count; vLoop++)
                    {
                        CheckBox chkSelect = (CheckBox)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("chkSelect");

                        HiddenField hdItemId = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("hdItemId");
                        DropDownList ddlColor = (DropDownList)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("ddlColor");

                        HiddenField txtRate = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("hdRate");

                        HiddenField hdSmpAvg = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("hdSmpAvg");
                        HiddenField hdPrdAvg = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("hdPrdAvg");

                        HiddenField hdSmpCost = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("hdSmpCost");
                        HiddenField hdPrdCost = (HiddenField)GVdsItemCodeDescriptionItemType.Rows[vLoop].FindControl("hdPrdCost");


                        int TransSamplingCostingId = objBs.InsertTransSamplingandCosting(Convert.ToInt32(SamplingCostingId), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(ddlColor.SelectedValue), Convert.ToDouble(txtRate.Value), Convert.ToDouble(hdSmpAvg.Value), Convert.ToDouble(hdPrdAvg.Value), Convert.ToDouble(hdSmpCost.Value), Convert.ToDouble(hdPrdCost.Value), chkSelect.Checked);

                    }

                    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                    {
                        HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");

                        HiddenField txtRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");

                        HiddenField hdSmpAvg = (HiddenField)GVItem.Rows[vLoop].FindControl("hdSmpAvg");
                        HiddenField hdPrdAvg = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPrdAvg");

                        HiddenField hdSmpCost = (HiddenField)GVItem.Rows[vLoop].FindControl("hdSmpCost");
                        HiddenField hdPrdCost = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPrdCost");

                        int TransSamplingCostingId = objBs.InsertTransSamplingandCosting1(Convert.ToInt32(SamplingCostingId), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(txtRate.Value), Convert.ToDouble(hdSmpAvg.Value), Convert.ToDouble(hdPrdAvg.Value), Convert.ToDouble(hdSmpCost.Value), Convert.ToDouble(hdPrdCost.Value));

                    }

                    for (int vLoop = 0; vLoop < Gridsupplierdetails.Rows.Count; vLoop++)
                    {
                        HiddenField hdItemId = (HiddenField)Gridsupplierdetails.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)Gridsupplierdetails.Rows[vLoop].FindControl("hdColorId");

                        HiddenField txtRate = (HiddenField)Gridsupplierdetails.Rows[vLoop].FindControl("hdRate");

                        DropDownList ddlPartyType = (DropDownList)Gridsupplierdetails.Rows[vLoop].FindControl("ddlPartyType");

                        string partyid = "0";

                        if (ddlPartyType.SelectedValue == "Select PartyType")
                        {

                        }
                        else
                        {
                            partyid = ddlPartyType.SelectedValue;
                        }


                        Label selectedsupplierlistID = (Label)Gridsupplierdetails.Rows[vLoop].FindControl("selectedsupplierlistID");


                        int TRansSamplingSupplier = objBs.InsertSamplingSupplier(Convert.ToInt32(SamplingCostingId), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(txtRate.Value), partyid, selectedsupplierlistID.Text);

                    }

                    for (int vLoop = 0; vLoop < gvPcsProcessDetails.Rows.Count; vLoop++)
                    {

                        Label lblpid = (Label)gvPcsProcessDetails.Rows[vLoop].FindControl("lblpid");

                        int TRansSamplingProcess = objBs.InsertSamplingProcess(Convert.ToInt32(SamplingCostingId), lblpid.Text);
                    }


                    string text2 = "Record Approved Successfully.Thank you!!!.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "success", "<script>showpop1('" + text2 + "');window.location='SamplingCosting_Approval.aspx';</script>", false);

                    //Response.Redirect("SamplingCosting_Approval.aspx");
                }

            }

        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SamplingCosting_Approval.aspx");
        }



    }
}
