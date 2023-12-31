﻿using System;
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
    public partial class BuyerOrderSales : System.Web.UI.Page
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
                //DataSet dsInvNo = objBs.BuyerOrderSalesInv(YearCode);
               

                DataSet dsset = objBs.getLedger_New1(lblContactTypeId.Text,Convert.ToInt32(rdbselect.SelectedValue));
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlPartyCode.DataSource = dsset.Tables[0];
                    ddlPartyCode.DataTextField = "CompanyCode";
                    ddlPartyCode.DataValueField = "LedgerID";
                    ddlPartyCode.DataBind();
                    ddlPartyCode.Items.Insert(0, "PartyCode");
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

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    //ddlCompany.Items.Insert(0, "CompanyName");
                }
                DataSet dsInvNo = objBs.BuyerOrderSalesInvwithcompany(YearCode, ddlCompany.SelectedValue);
                string InvoiceNo = dsInvNo.Tables[0].Rows[0]["InvoiceNo"].ToString().PadLeft(4, '0');
                if(ddlCompany.SelectedItem.Text== "NANDHINI FASHIONS")
                {
                    txtInvNo.Text = " N -  " + InvoiceNo + " / " + YearCode;
                }
                if (ddlCompany.SelectedItem.Text == "Nandhini Fab")
                {
                    txtInvNo.Text = " P -  " + InvoiceNo + " / " + YearCode;
                }
                txtInvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

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

               

                string BuyerOrderSalesId = Request.QueryString.Get("BuyerOrderSalesId");
                if (BuyerOrderSalesId != "" && BuyerOrderSalesId != null)
                {
                    #region
                    DataSet dsBOS = objBs.GetInsertBuyerOrderSales(BuyerOrderSalesId);
                    if (dsBOS.Tables[0].Rows.Count > 0)
                    {
                        btnSubmitQty.Visible = false;
                        btnSave.Enabled = false;

                        txtInvNo.Text = dsBOS.Tables[0].Rows[0]["FullInvoiceNo"].ToString();
                        txtInvDate.Text = Convert.ToDateTime(dsBOS.Tables[0].Rows[0]["InvoiceDate"]).ToString("dd/MM/yyyy");
                        ddlPartyCode.SelectedValue = dsBOS.Tables[0].Rows[0]["BuyerId"].ToString();
                        txtNarration.Text = dsBOS.Tables[0].Rows[0]["Narrations"].ToString();
                        ddlCompany.SelectedValue = dsBOS.Tables[0].Rows[0]["CompanyId"].ToString();

                        DataSet dsExcStyle = objBs.BuyerOrderSalesStyles1(BuyerOrderSalesId);

                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

                        #region

                        dct = new DataColumn("AllID");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("BuyerOrderMasterCuttingId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ExcNo");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("StyleNo");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Color");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Range");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Qty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("RowId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("IssueQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Rate");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Amount");
                        dttt.Columns.Add(dct);
                        dstd.Tables.Add(dttt);

                        foreach (DataRow Dr in dsExcStyle.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();
                            drNew["AllID"] = Dr["AllID"];
                            drNew["BuyerOrderMasterCuttingId"] = Dr["BuyerOrderMasterCuttingId"];
                            drNew["ExcNo"] = Dr["ExcNo"];
                            drNew["StyleNo"] = Dr["StyleNo"];
                            drNew["Color"] = Dr["Color"];
                            drNew["Range"] = Dr["Range"];
                            drNew["Qty"] = Dr["Qty"];
                            drNew["RowId"] = Dr["RowId"];
                            drNew["IssueQty"] = Dr["IssueQty"];
                            drNew["Rate"] = Dr["Rate"];
                            //drNew["Amount"] = (Convert.ToDouble(Dr["Qty"]) * Convert.ToDouble(Dr["Rate"])).ToString("f2");
                            drNew["Amount"] = ( Convert.ToDouble(Dr["Rate"])).ToString("f2");
                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();

                        DataSet dsExcStyleSize = objBs.BuyerOrderSalesStylesSize1(BuyerOrderSalesId);

                        DataSet dstd1 = new DataSet();
                        DataTable dtddd1 = new DataTable();
                        DataRow drNew1;
                        DataColumn dct1;
                        DataTable dttt1 = new DataTable();

                        #region

                        dct1 = new DataColumn("ExcStockId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("StyleId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("ColorId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("SizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Qty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RangeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RowId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("TransSizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Size");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("IssueQty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Rate");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Taxid");
                        dttt1.Columns.Add(dct1);
                        dstd1.Tables.Add(dttt1);

                        foreach (DataRow Dr in dsExcStyleSize.Tables[0].Rows)
                        {
                            drNew1 = dttt1.NewRow();

                            drNew1["ExcStockId"] = Dr["ExcStockId"];
                            drNew1["StyleId"] = Dr["StyleId"];
                            drNew1["ColorId"] = Dr["ColorId"];
                            drNew1["SizeId"] = Dr["SizeId"];
                            drNew1["Qty"] = Dr["Qty"];
                            drNew1["BuyerOrderMasterCuttingId"] = Dr["BuyerOrderMasterCuttingId"];
                            drNew1["RangeId"] = Dr["RangeId"];
                            drNew1["RowId"] = Dr["RowId"];
                            drNew1["TransSizeId"] = Dr["TransSizeId"];
                            drNew1["Size"] = Dr["Size"];
                            drNew1["IssueQty"] = Dr["IssueQty"];
                            drNew1["Rate"] = 0;
                            drNew1["Taxid"] = Dr["taxid"];
                            dstd1.Tables[0].Rows.Add(drNew1);
                            dtddd1 = dstd1.Tables[0];

                        }

                        #endregion

                        ViewState["CurrentTable2"] = dtddd1;

                    }

                    #endregion
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
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsInvNo = objBs.BuyerOrderSalesInvwithcompany(YearCode, ddlCompany.SelectedValue);
            string InvoiceNo = dsInvNo.Tables[0].Rows[0]["InvoiceNo"].ToString().PadLeft(4, '0');
            txtBillNo.Text = InvoiceNo.ToString();

            if (ddlCompany.SelectedItem.Text == "NANDHINI FASHIONS")
            {
                // txtInvNo.Text = " N -  " + InvoiceNo + " / " + YearCode;
                txtInvNo.Text = " N -  " + txtBillNo.Text + " / " + YearCode;
            }
            if (ddlCompany.SelectedItem.Text == "Nandhini Fab")
            {
                //txtInvNo.Text = " P -  " + InvoiceNo + " / " + YearCode;
                txtInvNo.Text = " P -  " + txtBillNo.Text + " / " + YearCode;
            }
            txtInvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
            protected void ddlPartyCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           
                divexec.Visible = true;
                ViewState["CurrentTable1"] = null;
                ViewState["CurrentTable2"] = null;
                GVItem.DataSource = null;
                GVItem.DataBind();
                GVSizes.DataSource = null;
                GVSizes.DataBind();

                if (ddlPartyCode.SelectedValue != "" && ddlPartyCode.SelectedValue != "0" && ddlPartyCode.SelectedValue != "PartyCode")
                {
                    DataSet dsDetails = objBs.GetLedgerCheck(Convert.ToInt32(ddlPartyCode.SelectedValue));
                    if (dsDetails.Tables[0].Rows.Count > 0)
                    {
                        ddlProvince.SelectedValue = dsDetails.Tables[0].Rows[0]["province"].ToString();
                        drpGSTType.SelectedValue = dsDetails.Tables[0].Rows[0]["GSTType"].ToString();
                    }


                    DataSet dsExc = objBs.BuyerOrderSalesExc(Convert.ToInt32(ddlPartyCode.SelectedValue),Convert.ToInt32(rdbselect.SelectedValue),Convert.ToInt32(ddlCompany.SelectedValue));
                    if (dsExc.Tables[0].Rows.Count > 0)
                    {
                        chkExcNo.DataSource = dsExc;
                        chkExcNo.DataTextField = "ExcNo";
                        chkExcNo.DataValueField = "BuyerOrderMasterCuttingId";
                        chkExcNo.DataBind();
                    }
                    else
                    {
                        chkExcNo.Items.Clear();
                    }
                }
                else
                {
                    chkExcNo.Items.Clear();
                }
                txtExcNo.Focus();
          
        }
        protected void rdbselect_SelectedIdexChanged(object sender, EventArgs e)
        {
            if (rdbselect.SelectedValue == "1")
            {
               
            }
            else if (rdbselect.SelectedValue == "2")
            {
               
            }
        }
            protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            GVSizes.DataSource = null;
            GVSizes.DataBind();

            if (chkExcNo.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                chkExcNo.Focus();
                return;
            }

            string IsFirst = "Yes"; string Ids = "All";
            foreach (ListItem listItem in chkExcNo.Items)
            {
                if (listItem.Selected)
                {
                    if (IsFirst == "Yes")
                    {
                        if (rdbselect.SelectedValue == "1")
                        {
                            Ids = "'"+listItem.Text+"'";
                            IsFirst = "No";
                        }
                        else
                        {
                            Ids = listItem.Value;
                            IsFirst = "No";
                        }
                    }
                    else
                    {
                        if (rdbselect.SelectedValue == "1")
                        {
                            Ids =Ids + "," +"'"+ listItem.Text+"'";
                        }
                        else
                        {
                            Ids = Ids + "," + listItem.Value;
                        }
                    }
                }
            }

            DataSet dsExcStyle = objBs.BuyerOrderSalesStyles(Ids,Convert.ToInt32(rdbselect.SelectedValue),Convert.ToInt32(ddlCompany.SelectedValue));
            if (dsExcStyle.Tables[0].Rows.Count > 0)
            {
                DataSet dsExcStyleRate = objBs.BuyerOrderSalesStylesRate(Ids, Convert.ToInt32(rdbselect.SelectedValue),Convert.ToInt32(ddlCompany.SelectedValue));

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                #region

                dct = new DataColumn("AllID");
                dttt.Columns.Add(dct);
                dct = new DataColumn("BuyerOrderMasterCuttingId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ExcNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("StyleNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Color");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Range");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Qty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RowId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("IssueQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Rate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Amount");
                dttt.Columns.Add(dct);
                dct = new DataColumn("HSNCode");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Tax");
                dttt.Columns.Add(dct);
                dct = new DataColumn("TaxID");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CGST");
                dttt.Columns.Add(dct);
                dct = new DataColumn("SGST");
                dttt.Columns.Add(dct);
                dct = new DataColumn("IGST");
                dttt.Columns.Add(dct);
                dct = new DataColumn("BeforeTAX");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DiscPer");
                dttt.Columns.Add(dct);
                dstd.Tables.Add(dttt);

                foreach (DataRow Dr in dsExcStyle.Tables[0].Rows)
                {
                    drNew = dttt.NewRow();
                    drNew["AllID"] = Dr["AllID"];
                    drNew["BuyerOrderMasterCuttingId"] = Dr["BuyerOrderMasterCuttingId"];
                    drNew["ExcNo"] = Dr["ExcNo"];
                    drNew["StyleNo"] = Dr["StyleNo"];
                    drNew["Color"] = Dr["Color"];
                    drNew["Range"] = Dr["Range"];
                    drNew["Qty"] = Dr["Qty"];
                    drNew["RowId"] = Dr["RowId"];
                    drNew["IssueQty"] = Dr["IssueQty"];
                    drNew["HSNCode"] = Dr["HSNCode"];
                    drNew["Tax"] = Dr["Tax"];
                    drNew["TaxID"] = Dr["TaxID"];
                    drNew["CGST"] = "0";
                    drNew["SGST"] = "0";
                    drNew["IGST"] = "0";
                    drNew["BeforeTAX"] = "0";                    
                    DataSet dsDetails = objBs.GetLedgerCheck(Convert.ToInt32(ddlPartyCode.SelectedValue));
                    if (dsDetails.Tables[0].Rows.Count > 0)
                    {
                        drNew["DiscPer"] = dsDetails.Tables[0].Rows[0]["DiscPer"].ToString();
                    }
                    else
                    {
                        drNew["DiscPer"] = "0";
                    }
                    // drNew["Rate"] = Dr["Rate"];
                    if (rdbselect.SelectedValue == "1")
                    {
                        DataRow[] RowsStyleQty = dsExcStyleRate.Tables[0].Select("BuyerOrderMasterCuttingId='" + Dr["StyleNo"] + "'   ");
                        drNew["Rate"] = RowsStyleQty[0]["Rate"].ToString();
                    }
                    else
                    {
                        DataRow[] RowsStyleQty = dsExcStyleRate.Tables[0].Select("BuyerOrderMasterCuttingId='" + Dr["BuyerOrderMasterCuttingId"] + "' and RowId='" + Dr["RowId"] + "'   ");
                        drNew["Rate"] = RowsStyleQty[0]["Rate"].ToString();
                    }
                    drNew["Amount"] = 0;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                }

                #endregion

                ViewState["CurrentTable1"] = dtddd;
                GVItem.DataSource = dtddd;
                GVItem.DataBind();

                DataSet dsExcStyleSize = objBs.BuyerOrderSalesStylesSize(Ids,Convert.ToInt32(rdbselect.SelectedValue),Convert.ToInt32(ddlCompany.SelectedValue));

                DataSet dstd1 = new DataSet();
                DataTable dtddd1 = new DataTable();
                DataRow drNew1;
                DataColumn dct1;
                DataTable dttt1 = new DataTable();

                #region

                dct1 = new DataColumn("ExcStockId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("StyleId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("ColorId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("SizeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Qty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RangeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RowId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("TransSizeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Size");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("IssueQty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Rate");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Taxid");
                dttt1.Columns.Add(dct1);
                dstd1.Tables.Add(dttt1);

                foreach (DataRow Dr in dsExcStyleSize.Tables[0].Rows)
                {
                    drNew1 = dttt1.NewRow();

                    drNew1["ExcStockId"] = Dr["ExcStockId"];
                    drNew1["StyleId"] = Dr["StyleId"];
                    drNew1["ColorId"] = Dr["ColorId"];
                    drNew1["SizeId"] = Dr["SizeId"];
                    drNew1["Qty"] = Dr["Qty"];
                    drNew1["BuyerOrderMasterCuttingId"] = Dr["BuyerOrderMasterCuttingId"];
                    drNew1["RangeId"] = Dr["RangeId"];
                    drNew1["RowId"] = Dr["RowId"];
                    drNew1["TransSizeId"] = Dr["TransSizeId"];
                    drNew1["Size"] = Dr["Size"];
                    drNew1["IssueQty"] = Dr["IssueQty"];
                   drNew1["Rate"] = Dr["Rate"];
                    drNew1["Taxid"] = Dr["Taxid"];
                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }

                #endregion

                ViewState["CurrentTable2"] = dtddd1;

            }
            else
            {
                ViewState["CurrentTable1"] = null;
                ViewState["CurrentTable2"] = null;
                GVItem.DataSource = null;
                GVItem.DataBind();
            }


        }

        protected void GVItem_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AssignQty")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    string[] Ids = e.CommandArgument.ToString().Split('#');
                    
                    #region
                    if (rdbselect.SelectedValue=="1")
                    {
                        DataTable DTStyle = (DataTable)ViewState["CurrentTable1"];
                        DataRow[] RowsStyleQty = DTStyle.Select("styleno='" + Ids[0].ToString() + "' ");
                        AllID.Text = RowsStyleQty[0]["AllID"].ToString();
                        BuyerOrderMasterCuttingId.Text = RowsStyleQty[0]["BuyerOrderMasterCuttingId"].ToString();
                        RowId.Text = RowsStyleQty[0]["RowId"].ToString();

                        ExcNo.Text = RowsStyleQty[0]["ExcNo"].ToString();
                        StyleNo.Text = RowsStyleQty[0]["StyleNo"].ToString();
                        Color.Text = RowsStyleQty[0]["Color"].ToString();
                        Range.Text = RowsStyleQty[0]["Range"].ToString();
                        Qty.Text = RowsStyleQty[0]["Qty"].ToString();
                        IssueQty.Text = RowsStyleQty[0]["IssueQty"].ToString();
                        Rate.Text = RowsStyleQty[0]["Rate"].ToString();
                        Amount.Text = RowsStyleQty[0]["Amount"].ToString();

                        HSNCode.Text = RowsStyleQty[0]["HSNCode"].ToString();
                        Tax.Text = RowsStyleQty[0]["Tax"].ToString();
                        TaxID.Text = RowsStyleQty[0]["TaxID"].ToString();

                        CGST.Text = RowsStyleQty[0]["CGST"].ToString();
                        SGST.Text = RowsStyleQty[0]["SGST"].ToString();
                        IGST.Text = RowsStyleQty[0]["IGST"].ToString();
                        BeforeTAX.Text = RowsStyleQty[0]["BeforeTAX"].ToString();
                        DiscPer.Text = RowsStyleQty[0]["DiscPer"].ToString();
                        #region

                        DataSet dstd1 = new DataSet();
                        DataTable dtddd1 = new DataTable();

                        DataRow drNew1;
                        DataColumn dct1;

                        DataTable dttt1 = new DataTable();

                        dct1 = new DataColumn("ExcStockId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("StyleId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("ColorId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("SizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Qty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RangeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RowId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("TransSizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Size");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("IssueQty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Rate");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Taxid");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("DiscPer");
                        dttt1.Columns.Add(dct1);
                        dstd1.Tables.Add(dttt1);

                        DataTable DTSize = (DataTable)ViewState["CurrentTable2"];

                        DataRow[] RowsSizeQty = DTSize.Select("BuyerOrderMasterCuttingId='" + Ids[0].ToString() + "'  ");

                        for (int i = 0; i < RowsSizeQty.Length; i++)
                        {
                            drNew1 = dttt1.NewRow();
                            drNew1["ExcStockId"] = RowsSizeQty[i]["ExcStockId"].ToString();
                            drNew1["StyleId"] = RowsSizeQty[i]["StyleId"].ToString();
                            drNew1["ColorId"] = RowsSizeQty[i]["ColorId"].ToString();
                            drNew1["SizeId"] = RowsSizeQty[i]["SizeId"].ToString();
                            drNew1["Qty"] = RowsSizeQty[i]["Qty"].ToString();
                            drNew1["BuyerOrderMasterCuttingId"] = RowsSizeQty[i]["BuyerOrderMasterCuttingId"].ToString();
                            drNew1["RangeId"] = RowsSizeQty[i]["RangeId"].ToString();
                            drNew1["RowId"] = RowsSizeQty[i]["RowId"].ToString();
                            drNew1["TransSizeId"] = RowsSizeQty[i]["TransSizeId"].ToString();
                            drNew1["Size"] = RowsSizeQty[i]["Size"].ToString();
                            drNew1["IssueQty"] = RowsSizeQty[i]["IssueQty"].ToString();
                            drNew1["Rate"] = RowsSizeQty[i]["Rate"].ToString();
                            drNew1["Taxid"] = RowsSizeQty[i]["Taxid"].ToString();
                            DataSet dsDetails = objBs.GetLedgerCheck(Convert.ToInt32(ddlPartyCode.SelectedValue));
                            if (dsDetails.Tables[0].Rows.Count > 0)
                            {
                                drNew1["DiscPer"] = dsDetails.Tables[0].Rows[0]["DiscPer"].ToString();
                            }
                            else
                            {
                                drNew1["DiscPer"] = "0";
                            }
                            dstd1.Tables[0].Rows.Add(drNew1);
                            dtddd1 = dstd1.Tables[0];
                        }



                        GVSizes.DataSource = dstd1;
                        GVSizes.DataBind();

                        #endregion

                    }
                    else
                    {
                        DataTable DTStyle = (DataTable)ViewState["CurrentTable1"];
                        DataRow[] RowsStyleQty = DTStyle.Select("BuyerOrderMasterCuttingId='" + Ids[0].ToString() + "' and RowId='" + Ids[1].ToString() + "' ");
                        AllID.Text = RowsStyleQty[0]["AllID"].ToString();
                        BuyerOrderMasterCuttingId.Text = RowsStyleQty[0]["BuyerOrderMasterCuttingId"].ToString();
                        RowId.Text = RowsStyleQty[0]["RowId"].ToString();

                        ExcNo.Text = RowsStyleQty[0]["ExcNo"].ToString();
                        StyleNo.Text = RowsStyleQty[0]["StyleNo"].ToString();
                        Color.Text = RowsStyleQty[0]["Color"].ToString();
                        Range.Text = RowsStyleQty[0]["Range"].ToString();
                        Qty.Text = RowsStyleQty[0]["Qty"].ToString();
                        IssueQty.Text = RowsStyleQty[0]["IssueQty"].ToString();
                        Rate.Text = RowsStyleQty[0]["Rate"].ToString();
                        Amount.Text = RowsStyleQty[0]["Amount"].ToString();

                        HSNCode.Text = RowsStyleQty[0]["HSNCode"].ToString();
                        Tax.Text = RowsStyleQty[0]["Tax"].ToString();
                        TaxID.Text = RowsStyleQty[0]["TaxID"].ToString();

                        CGST.Text = RowsStyleQty[0]["CGST"].ToString();
                        SGST.Text = RowsStyleQty[0]["SGST"].ToString();
                        IGST.Text = RowsStyleQty[0]["IGST"].ToString();
                        BeforeTAX.Text = RowsStyleQty[0]["BeforeTAX"].ToString();
                        DiscPer.Text = RowsStyleQty[0]["DiscPer"].ToString();
                       
                        #region

                        DataSet dstd1 = new DataSet();
                        DataTable dtddd1 = new DataTable();

                        DataRow drNew1;
                        DataColumn dct1;

                        DataTable dttt1 = new DataTable();

                        dct1 = new DataColumn("ExcStockId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("StyleId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("ColorId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("SizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Qty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RangeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RowId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("TransSizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Size");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("IssueQty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Rate");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Taxid");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("DiscPer");
                        dttt1.Columns.Add(dct1);
                        dstd1.Tables.Add(dttt1);

                        DataTable DTSize = (DataTable)ViewState["CurrentTable2"];

                        DataRow[] RowsSizeQty = DTSize.Select("BuyerOrderMasterCuttingId='" + Ids[0].ToString() + "' and RowId='" + Ids[1].ToString() + "' ");

                        for (int i = 0; i < RowsSizeQty.Length; i++)
                        {
                            drNew1 = dttt1.NewRow();
                            drNew1["ExcStockId"] = RowsSizeQty[i]["ExcStockId"].ToString();
                            drNew1["StyleId"] = RowsSizeQty[i]["StyleId"].ToString();
                            drNew1["ColorId"] = RowsSizeQty[i]["ColorId"].ToString();
                            drNew1["SizeId"] = RowsSizeQty[i]["SizeId"].ToString();
                            drNew1["Qty"] = RowsSizeQty[i]["Qty"].ToString();
                            drNew1["BuyerOrderMasterCuttingId"] = RowsSizeQty[i]["BuyerOrderMasterCuttingId"].ToString();
                            drNew1["RangeId"] = RowsSizeQty[i]["RangeId"].ToString();
                            drNew1["RowId"] = RowsSizeQty[i]["RowId"].ToString();
                            drNew1["TransSizeId"] = RowsSizeQty[i]["TransSizeId"].ToString();
                            drNew1["Size"] = RowsSizeQty[i]["Size"].ToString();
                            drNew1["IssueQty"] = RowsSizeQty[i]["IssueQty"].ToString();
                            drNew1["Rate"] = RowsStyleQty[0]["Rate"].ToString();
                            drNew1["Taxid"] = RowsSizeQty[0]["Taxid"].ToString();
                            drNew1["DiscPer"] = RowsSizeQty[0]["DiscPer"].ToString();
                            dstd1.Tables[0].Rows.Add(drNew1);
                            dtddd1 = dstd1.Tables[0];
                        }



                        GVSizes.DataSource = dstd1;
                        GVSizes.DataBind();

                        #endregion

                    }



                    #endregion

                    //#region  shanthi 24 may 2023

                    //DataSet dstd1 = new DataSet();
                    //DataTable dtddd1 = new DataTable();

                    //DataRow drNew1;
                    //DataColumn dct1;

                    //DataTable dttt1 = new DataTable();

                    //dct1 = new DataColumn("ExcStockId");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("StyleId");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("ColorId");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("SizeId");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("Qty");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("RangeId");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("RowId");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("TransSizeId");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("Size");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("IssueQty");
                    //dttt1.Columns.Add(dct1);
                    //dct1 = new DataColumn("Rate");
                    //dttt1.Columns.Add(dct1);
                    //dstd1.Tables.Add(dttt1);

                    //DataTable DTSize = (DataTable)ViewState["CurrentTable2"];
                   
                    //    DataRow[] RowsSizeQty = DTSize.Select("BuyerOrderMasterCuttingId='" + Ids[0].ToString() + "' and RowId='" + Ids[1].ToString() + "' ");

                    //    for (int i = 0; i < RowsSizeQty.Length; i++)
                    //    {
                    //        drNew1 = dttt1.NewRow();
                    //        drNew1["ExcStockId"] = RowsSizeQty[i]["ExcStockId"].ToString();
                    //        drNew1["StyleId"] = RowsSizeQty[i]["StyleId"].ToString();
                    //        drNew1["ColorId"] = RowsSizeQty[i]["ColorId"].ToString();
                    //        drNew1["SizeId"] = RowsSizeQty[i]["SizeId"].ToString();
                    //        drNew1["Qty"] = RowsSizeQty[i]["Qty"].ToString();
                    //        drNew1["BuyerOrderMasterCuttingId"] = RowsSizeQty[i]["BuyerOrderMasterCuttingId"].ToString();
                    //        drNew1["RangeId"] = RowsSizeQty[i]["RangeId"].ToString();
                    //        drNew1["RowId"] = RowsSizeQty[i]["RowId"].ToString();
                    //        drNew1["TransSizeId"] = RowsSizeQty[i]["TransSizeId"].ToString();
                    //        drNew1["Size"] = RowsSizeQty[i]["Size"].ToString();
                    //        drNew1["IssueQty"] = RowsSizeQty[i]["IssueQty"].ToString();
                    //        drNew1["Rate"] = RowsSizeQty[i]["Rate"].ToString();
                    //        dstd1.Tables[0].Rows.Add(drNew1);
                    //        dtddd1 = dstd1.Tables[0];
                    //    }
                   
                    

                    //GVSizes.DataSource = dstd1;
                    //GVSizes.DataBind();

                    //#endregion
                }
            }
        }
        protected void btnSubmitQty_OnClick(object sender, EventArgs e)
        {
            if (GVSizes.Rows.Count > 0)
            {
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdQty");
                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    Label lblRate = (Label)GVSizes.Rows[vLoop].FindControl("lblRate");
                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";
                   

                    if (Convert.ToInt32(hdQty.Value) < Convert.ToInt32(txtIssueQty.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check IssueQty.')", true);
                        txtIssueQty.Focus();
                        return;
                    }
                }



                #region CurrentTable Removed

                DataTable DTStyle = (DataTable)ViewState["CurrentTable1"];
                DataRow[] RowsStyleQty = DTStyle.Select("StyleNo='" + StyleNo.Text + "' and RowId='" + RowId.Text + "' ");

                for (int i = 0; i < RowsStyleQty.Length; i++)
                    RowsStyleQty[i].Delete();
                DTStyle.AcceptChanges();

                ViewState["CurrentTable1"] = DTStyle;

                DataTable DTSize = (DataTable)ViewState["CurrentTable2"];
                DataRow[] RowsSizeQty = DTSize.Select("BuyerordermastercuttingId='" + StyleNo.Text  + "' and RowId='" + RowId.Text + "' ");

                for (int i = 0; i < RowsSizeQty.Length; i++)
                    RowsSizeQty[i].Delete();
                DTSize.AcceptChanges();

                ViewState["CurrentTable2"] = DTSize;

                #endregion

                double IssueQty = 0;
                double TotRate = 0;
                
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    double mRate = 0;
                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";
                    IssueQty += Convert.ToDouble(txtIssueQty.Text);
                    Label lblRate = (Label)GVSizes.Rows[vLoop].FindControl("lblRate");
                    //if (txtIssueQty.Text == "")
                    //    txtIssueQty.Text = "0";
                    TextBox txtDiscPer = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDiscPer");
                    if (txtDiscPer.Text == "")
                        txtDiscPer.Text = "0";
                    if (Convert.ToDouble(txtIssueQty.Text) > 0)
                    {
                        mRate = Convert.ToDouble(lblRate.Text) - (Convert.ToDouble(lblRate.Text) * Convert.ToDouble(txtIssueQty.Text) * Convert.ToDouble(txtDiscPer.Text) / 100);
                    }
                    //TotRate += Convert.ToDouble(lblRate.Text) * Convert.ToDouble(txtIssueQty.Text);
                    TotRate += mRate;
                }
               

                //for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                //{
                //    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                //    Label  lblRate = (Label)GVSizes.Rows[vLoop].FindControl("lblRate");
                //    //if (txtIssueQty.Text == "")
                //    //    txtIssueQty.Text = "0";
                //    TotRate += Convert.ToDouble(lblRate.Text ) * Convert.ToDouble(txtIssueQty.Text );
                //   // IssueQty += Convert.ToDouble(txtIssueQty.Text);
                //}

                double Totamount = 0;
                double TotCGST = 0;
                double TotSGST = 0;
                double TotIGST = 0;
                double TotBeforeTAX = 0;

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                #region

                dct = new DataColumn("AllID");
                dttt.Columns.Add(dct);
                dct = new DataColumn("BuyerOrderMasterCuttingId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ExcNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("StyleNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Color");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Range");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Qty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RowId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("IssueQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Rate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Amount");
                dttt.Columns.Add(dct);
                dct = new DataColumn("HSNCode");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Tax");
                dttt.Columns.Add(dct);
                dct = new DataColumn("TaxID");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CGST");
                dttt.Columns.Add(dct);
                dct = new DataColumn("SGST");
                dttt.Columns.Add(dct);
                dct = new DataColumn("IGST");
                dttt.Columns.Add(dct);
                dct = new DataColumn("BeforeTAX");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DiscPer");
                dttt.Columns.Add(dct);
                dstd.Tables.Add(dttt);

                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                drNew = dttt.NewRow();
                drNew["AllID"] = AllID.Text;
                drNew["BuyerOrderMasterCuttingId"] = BuyerOrderMasterCuttingId.Text;
                drNew["ExcNo"] = ExcNo.Text;
                drNew["StyleNo"] = StyleNo.Text;
                drNew["Color"] = Color.Text;
                drNew["Range"] = Range.Text;
                drNew["Qty"] = Qty.Text;
                drNew["RowId"] = RowId.Text;
                drNew["IssueQty"] = IssueQty;
               // drNew["Rate"] = Rate.Text;
                drNew["Rate"] = TotRate;

                drNew["HSNCode"] = HSNCode.Text;
                drNew["Tax"] = Tax.Text;
                drNew["TaxID"] = TaxID.Text;
                drNew["DiscPer"] = DiscPer.Text;
                /*  if (drpGSTType.SelectedValue == "1")
                  {
                      drNew["Amount"] = Convert.ToDouble(Convert.ToDouble(IssueQty * Convert.ToDouble(Rate.Text)) + (Convert.ToDouble((IssueQty * Convert.ToDouble(Rate.Text)) * Convert.ToDouble(Tax.Text) / 100))).ToString("f2");
                      if (ddlProvince.SelectedValue == "1")
                      {
                          drNew["CGST"] = Convert.ToDouble(((IssueQty * Convert.ToDouble(Rate.Text)) * Convert.ToDouble(Tax.Text) / 100) / 2).ToString("f2");
                          drNew["SGST"] = Convert.ToDouble(((IssueQty * Convert.ToDouble(Rate.Text)) * Convert.ToDouble(Tax.Text) / 100) / 2).ToString("f2");
                          drNew["IGST"] = "0";
                      }
                      else
                      {
                          drNew["CGST"] = "0";
                          drNew["SGST"] = "0";
                          drNew["IGST"] = Convert.ToDouble((IssueQty * Convert.ToDouble(Rate.Text)) * Convert.ToDouble(Tax.Text) / 100).ToString("f2");
                      }
                      drNew["BeforeTAX"] = Convert.ToDouble(Convert.ToDouble(IssueQty * Convert.ToDouble(Rate.Text))).ToString("f2");
                  }
                  else
                  {
                      drNew["Amount"] = Convert.ToDouble(Convert.ToDouble(IssueQty * Convert.ToDouble(Rate.Text))).ToString("f2");
                      if (ddlProvince.SelectedValue == "1")
                      {
                          drNew["CGST"] = Convert.ToDouble(((IssueQty * Convert.ToDouble(Rate.Text)) * Convert.ToDouble(Tax.Text) / (100 + Convert.ToDouble(Tax.Text))) / 2).ToString("f2");
                          drNew["SGST"] = Convert.ToDouble(((IssueQty * Convert.ToDouble(Rate.Text)) * Convert.ToDouble(Tax.Text) / (100 + Convert.ToDouble(Tax.Text))) / 2).ToString("f2");
                          drNew["IGST"] = "0";
                      }
                      else
                      {
                          drNew["CGST"] = "0";
                          drNew["SGST"] = "0";
                          drNew["IGST"] = Convert.ToDouble(((IssueQty * Convert.ToDouble(Rate.Text)) * Convert.ToDouble(Tax.Text) / (100 + Convert.ToDouble(Tax.Text)))).ToString("f2");
                      }
                      drNew["BeforeTAX"] = Convert.ToDouble(Convert.ToDouble(IssueQty * Convert.ToDouble(Rate.Text)) - (Convert.ToDouble((IssueQty * Convert.ToDouble(Rate.Text)) * Convert.ToDouble(Tax.Text) / (100 + Convert.ToDouble(Tax.Text))))).ToString("f2");
                  }*/


                //TotRate = TotRate - ((TotRate * Convert.ToDouble(DiscPer.Text)) / 100);

                if (drpGSTType.SelectedValue == "1")
                {
                   
                    //drNew["Amount"] = Convert.ToDouble(Convert.ToDouble( TotRate ) + (Convert.ToDouble((IssueQty * TotRate) * Convert.ToDouble(Tax.Text) / 100))).ToString("f2");
                    drNew["Amount"] = Convert.ToDouble(Convert.ToDouble(TotRate) + (Convert.ToDouble((TotRate) * Convert.ToDouble(Tax.Text) / 100))).ToString("f2");
                    if (ddlProvince.SelectedValue == "1")
                    {
                        drNew["CGST"] = Convert.ToDouble((( TotRate) * Convert.ToDouble(Tax.Text) / 100) / 2).ToString("f2");
                        drNew["SGST"] = Convert.ToDouble((( TotRate) * Convert.ToDouble(Tax.Text) / 100) / 2).ToString("f2");
                        drNew["IGST"] = "0";
                    }
                    else
                    {
                        drNew["CGST"] = "0";
                        drNew["SGST"] = "0";
                        drNew["IGST"] = Convert.ToDouble(( TotRate) * Convert.ToDouble(Tax.Text) / 100).ToString("f2");
                    }
                    drNew["BeforeTAX"] = Convert.ToDouble(Convert.ToDouble( TotRate)).ToString("f2");
                }
                else
                {
                    drNew["Amount"] = Convert.ToDouble(Convert.ToDouble( TotRate)).ToString("f2");
                    if (ddlProvince.SelectedValue == "1")
                    {
                        drNew["CGST"] = Convert.ToDouble((( TotRate) * Convert.ToDouble(Tax.Text) / (100 + Convert.ToDouble(Tax.Text))) / 2).ToString("f2");
                        drNew["SGST"] = Convert.ToDouble((( TotRate) * Convert.ToDouble(Tax.Text) / (100 + Convert.ToDouble(Tax.Text))) / 2).ToString("f2");
                        drNew["IGST"] = "0";
                    }
                    else
                    {
                        drNew["CGST"] = "0";
                        drNew["SGST"] = "0";
                        drNew["IGST"] = Convert.ToDouble((( TotRate) * Convert.ToDouble(Tax.Text) / (100 + Convert.ToDouble(Tax.Text)))).ToString("f2");
                    }
                    drNew["BeforeTAX"] = Convert.ToDouble(Convert.ToDouble( TotRate) - (Convert.ToDouble(( TotRate) * Convert.ToDouble(Tax.Text) / (100 + Convert.ToDouble(Tax.Text))))).ToString("f2");
                }

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

                #endregion

                ViewState["CurrentTable1"] = dtddd;
                GVItem.DataSource = dstd;
                GVItem.DataBind();


                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    Label txtAmount = (Label)GVItem.Rows[vLoop].FindControl("txtAmount");
                    Label txtCGST = (Label)GVItem.Rows[vLoop].FindControl("txtCGST");
                    Label txtSGST = (Label)GVItem.Rows[vLoop].FindControl("txtSGST");
                    Label txtIGST = (Label)GVItem.Rows[vLoop].FindControl("txtIGST");
                    Label txtBeforeTAX = (Label)GVItem.Rows[vLoop].FindControl("txtBeforeTAX");
                    
                    if (txtAmount.Text == "")
                        txtAmount.Text = "0";
                    if (txtCGST.Text == "")
                        txtCGST.Text = "0";
                    if (txtSGST.Text == "")
                        txtSGST.Text = "0";
                    if (txtIGST.Text == "")
                        txtIGST.Text = "0";
                    if (txtBeforeTAX.Text == "")
                        txtBeforeTAX.Text = "0";

                    Totamount += Convert.ToDouble(txtAmount.Text);
                    TotCGST += Convert.ToDouble(txtCGST.Text);
                    TotSGST += Convert.ToDouble(txtSGST.Text);
                    TotIGST += Convert.ToDouble(txtIGST.Text);
                    TotBeforeTAX += Convert.ToDouble(txtBeforeTAX.Text);
                }


                txtGrandTotal.Text = Convert.ToDouble(Totamount).ToString("f2");
                txtTotCGST.Text = Convert.ToDouble(TotCGST).ToString("f2");
                txtTotSGST.Text = Convert.ToDouble(TotSGST).ToString("f2");
                txtTotIGST.Text = Convert.ToDouble(TotIGST).ToString("f2");
                txtTotBeforeTAX.Text = Convert.ToDouble(TotBeforeTAX).ToString("f2");

                double r = 0;
                double roundoff = Convert.ToDouble(txtGrandTotal.Text) - Math.Floor(Convert.ToDouble(txtGrandTotal.Text));
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(txtGrandTotal.Text), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(txtGrandTotal.Text));
                }
                txtRoundoff.Text = Convert.ToString(r);


                DataSet dstd1 = new DataSet();
                DataTable dtddd1 = new DataTable();
                DataRow drNew1;
                DataColumn dct1;
                DataTable dttt1 = new DataTable();

                #region

                dct1 = new DataColumn("ExcStockId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("StyleId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("ColorId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("SizeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Qty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RangeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RowId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("TransSizeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Size");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("IssueQty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Rate");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Taxid");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("DiscPer");
                dttt1.Columns.Add(dct1);
                dstd1.Tables.Add(dttt1);

                DataTable dt1 = (DataTable)ViewState["CurrentTable2"];

                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdExcStockId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdExcStockId");
                    HiddenField hdStyleId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdStyleId");
                    HiddenField hdColorId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdSizeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSizeId");
                    HiddenField hdQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdQty");
                    HiddenField hdBuyerOrderMasterCuttingId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdBuyerOrderMasterCuttingId");
                    HiddenField hdRangeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdRangeId");
                    HiddenField hdRowId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdRowId");
                    HiddenField hdTransSizeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdTransSizeId");
                    HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                    HiddenField hdTaxID = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdTaxID");
                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    Label lblRate = (Label)GVSizes.Rows[vLoop].FindControl("lblRate");
                    HiddenField hdDiscper = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdDiscper");
                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";

                    drNew1 = dttt1.NewRow();
                    drNew1["ExcStockId"] = hdExcStockId.Value;
                    drNew1["StyleId"] = hdStyleId.Value;
                    drNew1["ColorId"] = hdColorId.Value;
                    drNew1["SizeId"] = hdSizeId.Value;
                    drNew1["Qty"] = hdQty.Value;
                    drNew1["BuyerOrderMasterCuttingId"] = hdBuyerOrderMasterCuttingId.Value;
                    drNew1["RangeId"] = hdRangeId.Value;
                    drNew1["RowId"] = hdRowId.Value;
                    drNew1["TransSizeId"] = hdTransSizeId.Value;
                    drNew1["Size"] = hdSize.Value;
                    drNew1["IssueQty"] = txtIssueQty.Text;
                   // drNew1["Rate"] = Rate.Text;
                    drNew1["Rate"] = lblRate.Text ;
                    drNew1["TaxId"] = hdTaxID.Value;
                    drNew1["DiscPer"] = hdDiscper.Value;
                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }

                dtddd1.Merge(dt1);

                #endregion

                ViewState["CurrentTable2"] = dtddd1;

            }

            GVSizes.DataSource = null;
            GVSizes.DataBind();

        }

        protected void txtDiscPer_TextChanged(object sender, EventArgs e)
        {
            double grandtotal = 0;
            double tax = 0;
            double distotal = 0;
            double r = 0;

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                DropDownList ProductCode = (DropDownList)GVItem.Rows[vLoop].FindControl("ProductCode");
                //DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                Label lblitemName = (Label)GVItem.Rows[vLoop].FindControl("lblitemName");
                TextBox txtttk = (TextBox)GVItem.Rows[vLoop].FindControl("txtqty");
                TextBox txttk = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                TextBox txtkt = (TextBox)GVItem.Rows[vLoop].FindControl("txtTax");
                TextBox txtkttt = (TextBox)GVItem.Rows[vLoop].FindControl("txtAmount");
                TextBox txtsubkttt = (TextBox)GVItem.Rows[vLoop].FindControl("txtSubAmount");

                TextBox txtktt = (TextBox)GVItem.Rows[vLoop].FindControl("txtStock");
                TextBox txtktttt = (TextBox)GVItem.Rows[vLoop].FindControl("txtDiscount");
                TextBox txtkttttPer = (TextBox)GVItem.Rows[vLoop].FindControl("txtDiscountPer");

                if (lblitemName.Text == "")
                {

                }
                else
                {
                    if (txtktttt.Text == "") txtktttt.Text = "0";
                    if (txtkttttPer.Text == "") txtkttttPer.Text = "0";

                    double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

                    //DataSet dstockcheck = objBs.checkitemstockcheckornot(txt.SelectedValue);
                    //if (dstockcheck.Tables[0].Rows.Count > 0)
                    //{

                    //    if ((Convert.ToDouble(txtktt.Text) < (Convert.ToDouble(txtttk.Text))))
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Allow To Sale This Product " + txt.SelectedItem.Text + ".Thank You!!!')", true);
                    //        txtttk.Focus();
                    //        return;
                    //    }
                    //}


                    txtsubkttt.Text = string.Format("{0:N2}", iNetAmount);

                    double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

                    double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
                    //double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    double tx = 0;
                    if (drpGSTType.SelectedValue == "1")
                    {
                        tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
                    }
                    else
                    {
                        tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / (100 + Convert.ToDouble(txtkt.Text));
                    }

                    //double total = tx + DiscountAmount;
                    double total = 0;
                    if (drpGSTType.SelectedValue == "1")
                    {
                        total = tx + DiscountAmount;
                    }
                    else
                    {
                        total = DiscountAmount;
                    }

                    txtkttt.Text = string.Format("{0:N2}", total);

                    grandtotal = grandtotal + total;
                    tax = tax + tx;
                    distotal = distotal + Discount;
                }
            }
           
            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
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
           

            // txtdiscount.Text = string.Format("{0:N2}", distotal);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region Validations
            if(txtBillNo.Text == "" || txtBillNo.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter BillNo.')", true);
                txtBillNo.Focus();
                return;
            }

            if (txtInvNo.Text == "" || txtInvNo.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter InvoiceNo.')", true);
                txtInvNo.Focus();
                return;
            }

            DataSet dsDetails = objBs.CheckBuyerOrderSalesFullBillNo(txtInvNo.Text);
            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('FullInvoiceNo Already Exists.')", true);
                txtInvNo.Focus();
                return;
            }

            if (ddlPartyCode.SelectedValue == "" || ddlPartyCode.SelectedValue == "0" || ddlPartyCode.SelectedValue == "PartyCode")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select PartyCode.')", true);
                ddlPartyCode.Focus();
                return;
            }
            if (ddlCompany.SelectedValue == "" || ddlCompany.SelectedValue == "0" || ddlCompany.SelectedValue == "Select")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Stock From.')", true);
                ddlCompany.Focus();
                return;
            }
            if (ddlPayMode.SelectedValue == "" || ddlPayMode.SelectedValue == "0" || ddlPayMode.SelectedValue == "Select")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Paymode.')", true);
                ddlCompany.Focus();
                return;
            }

            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                return;
            }

            int IssueQty = 0;
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdIssueQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssueQty");
                IssueQty += Convert.ToInt32(hdIssueQty.Value);
            }

            if (IssueQty == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check IssueQty.')", true);
                return;
            }
            #endregion

            DateTime InvDate = DateTime.ParseExact(txtInvDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable DTSizes = (DataTable)ViewState["CurrentTable2"];

            int Id = 0;

            if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 1) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 0) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 3) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 5) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 6) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 7))
            {
                Id = 0;
            }
            else
            {
                //Id = Convert.ToInt32(ddlBank.SelectedValue);
            }

            if (Convert.ToInt32(ddlPayMode.SelectedValue) == 5)
            {
                Id = Convert.ToInt32(ddlBank.SelectedValue);
            }
            //if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
            //{
            //    Id = Convert.ToInt32(ddlBank.SelectedValue);
            //}
            if (Convert.ToInt32(ddlPayMode.SelectedValue) == 7)
            {
                Id = Convert.ToInt32(ddlBank.SelectedValue);
            }

            //
            //
            int TransHistoryId = objBs.InsertBuyerOrderSalesStyles(YearCode, InvDate, Convert.ToInt32(ddlPartyCode.SelectedValue), txtNarration.Text, DTSizes, sTableName, Convert.ToInt32(ddlPayMode.SelectedValue), Id, txtCheque.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), "tblDaybook_" + sTableName, Convert.ToDouble(txtGrandTotal.Text), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(drpGSTType.SelectedValue), Convert.ToDouble(txtTotCGST.Text), Convert.ToDouble(txtTotSGST.Text), Convert.ToDouble(txtTotIGST.Text), Convert.ToDouble(txtTotBeforeTAX.Text), Convert.ToDouble(txtRoundoff.Text), Convert.ToInt32(rdbselect.SelectedValue),Convert.ToInt32(ddlCompany.SelectedValue),ddlCompany.SelectedItem.Text,txtBillNo.Text, txtInvNo.Text);


            Response.Redirect("BuyerOrderSalesGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderSalesGrid.aspx");
        }

        protected void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            // txtBillNo.Text = InvoiceNo.ToString();
            string InvoiceNo = txtBillNo.Text;
            if (ddlCompany.SelectedItem.Text == "NANDHINI FASHIONS")
            {
                // txtInvNo.Text = " N -  " + InvoiceNo + " / " + YearCode;
                txtInvNo.Text = " N -  " + InvoiceNo.ToString().PadLeft(4, '0') + " / " + YearCode;
            }
            if (ddlCompany.SelectedItem.Text == "Nandhini Fab")
            {
                //txtInvNo.Text = " P -  " + InvoiceNo + " / " + YearCode;
                txtInvNo.Text = " P -  " + InvoiceNo.ToString().PadLeft(4, '0') + " / " + YearCode;
            }
        }
    }
}