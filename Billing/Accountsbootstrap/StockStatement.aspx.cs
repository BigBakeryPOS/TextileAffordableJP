using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class StockStatement : System.Web.UI.Page
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

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    //  ddlCompany.Items.Insert(0, "All");
                }
                DataSet dsIH = objBs.selectcategorymaster();
                if (dsIH.Tables[0].Rows.Count > 0)
                {
                    ddlItemHead.DataSource = dsIH.Tables[0];
                    ddlItemHead.DataTextField = "category";
                    ddlItemHead.DataValueField = "categoryid";
                    ddlItemHead.DataBind();
                    //  ddlItemHead.Items.Insert(0, "All");
                    ddlItemHead_OnSelectedIndexChanged(sender, e);
                    ddlItemGroup_OnSelectedIndexChanged(sender, e);
                }

                //ddlItemGroup.Items.Insert(0, "All");
                //ddlItem.Items.Insert(0, "All");


                DataSet dsColor = objBs.gridColor();
                if (dsColor.Tables[0].Rows.Count > 0)
                {
                    ddlColor.DataSource = dsColor.Tables[0];
                    ddlColor.DataTextField = "Color";
                    ddlColor.DataValueField = "ColorID";
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0, "All");
                }


            }
        }

        protected void ddlItemHead_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlItem.Items.Clear();
            ddlItem.Items.Insert(0, "All");

            if (ddlItemHead.SelectedValue == "" || ddlItemHead.SelectedValue == "0" || ddlItemHead.SelectedValue == "All")
            {
                ddlItemGroup.Items.Clear();
                ddlItemGroup.Items.Insert(0, "All");
            }
            else
            {
                DataSet dsItem = objBs.GetHeadItemGroup(Convert.ToInt32(ddlItemHead.SelectedValue));
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlItemGroup.DataSource = dsItem.Tables[0];
                    ddlItemGroup.DataTextField = "Itemgroupname";
                    ddlItemGroup.DataValueField = "ItemgroupId";
                    ddlItemGroup.DataBind();
                    //   ddlItemGroup.Items.Insert(0, "All");
                    ddlItemGroup_OnSelectedIndexChanged(sender, e);
                }
            }


        }
        protected void ddlItemGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlItem.Items.Clear();
            ddlItem.Items.Insert(0, "All");

            if (ddlItemHead.SelectedValue == "" || ddlItemHead.SelectedValue == "0" || ddlItemHead.SelectedValue == "All" || ddlItemGroup.SelectedValue == "" || ddlItemGroup.SelectedValue == "0" || ddlItemGroup.SelectedValue == "All")
            {
                ddlItem.Items.Clear();
                ddlItem.Items.Insert(0, "All");
            }
            else
            {
                DataSet dsItem = objBs.getAllItemsfor_HeadandGroup(Convert.ToInt32(ddlItemHead.SelectedValue), Convert.ToInt32(ddlItemGroup.SelectedValue));
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataSource = dsItem.Tables[0];
                    ddlItem.DataTextField = "Description";
                    ddlItem.DataValueField = "ItemMasterId";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "All");
                }
            }
        }


        protected void btnSearch_OnClick(object sender, EventArgs e)
        {

            DataSet dsPORates = objBs.GetPORates(ddlItem.SelectedValue);
            DataSet dsIPOERates = objBs.GetIPOERates(ddlItem.SelectedValue);
            DataSet dsIPORRates = objBs.GetIPORRates(ddlItem.SelectedValue);
            DataSet dsOPSRates = objBs.GetOPSRates(ddlItem.SelectedValue);

            DataSet dsPORates1 = objBs.GetPORates("All");
            DataSet dsIPOERates1 = objBs.GetIPOERates("All");

            DateTime InitialDate = DateTime.ParseExact(lblInitialDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds_ST_OP = objBs.ST_OP(From, ddlCompany.SelectedValue);
            DataSet ds_ST_CL = objBs.ST_CL(From, To, ddlCompany.SelectedValue);

            DataSet ds_ST_OP_PurchaseGRN = objBs.ST_OP_PurchaseGRN(From, ddlCompany.SelectedValue);
            DataSet ds_ST_CL_PurchaseGRN = objBs.ST_CL_PurchaseGRN(From, To, ddlCompany.SelectedValue);

            DataSet ds_ST_OP_ProcessOrder = objBs.ST_OP_ProcessOrder(From, ddlCompany.SelectedValue);
            DataSet ds_ST_CL_ProcessOrder = objBs.ST_CL_ProcessOrder(From, To, ddlCompany.SelectedValue);

            DataSet ds_ST_OP_ProcessOrderReceive = objBs.ST_OP_ProcessOrderReceive(From, ddlCompany.SelectedValue);
            DataSet ds_ST_CL_ProcessOrderReceive = objBs.ST_CL_ProcessOrderReceive(From, To, ddlCompany.SelectedValue);

            DataSet ds_ST_OP_Cutting = objBs.ST_OP_Cutting(From, ddlCompany.SelectedValue);
            DataSet ds_ST_CL_Cutting = objBs.ST_CL_Cutting(From, To, ddlCompany.SelectedValue);

            DataSet ds_ST_OP_StockTransferIN = objBs.ST_OP_StockTransfer("1", From, ddlCompany.SelectedValue);
            DataSet ds_ST_CL_StockTransferIN = objBs.ST_CL_StockTransfer("1", From, To, ddlCompany.SelectedValue);

            DataSet ds_ST_OP_StockTransferOut = objBs.ST_OP_StockTransfer("2", From, ddlCompany.SelectedValue);
            DataSet ds_ST_CL_StockTransferOut = objBs.ST_CL_StockTransfer("2", From, To, ddlCompany.SelectedValue);

            DataSet ds_Items = objBs.ST_Items(ddlItemHead.SelectedValue, ddlItemGroup.SelectedValue, ddlItem.SelectedValue);

            DataSet ds_ItemGroup = objBs.ItemGroup(Convert.ToInt32(ddlItemHead.SelectedValue), Convert.ToInt32(ddlItemGroup.SelectedValue));
            DataSet dsCategory = objBs.gridCategory();
            DataSet dsColor = objBs.gridColorSingle(ddlColor.SelectedValue);

            if (ds_ItemGroup.Tables[0].Rows.Count > 0)
            {
                #region

                DataTable DTS = new DataTable();
                DTS.Columns.Add(new DataColumn("ItemCode"));
                DTS.Columns.Add(new DataColumn("ItemDescription"));
                DTS.Columns.Add(new DataColumn("Color"));
                DTS.Columns.Add(new DataColumn("OP"));
                DTS.Columns.Add(new DataColumn("CL"));
                DTS.Columns.Add(new DataColumn("Units"));
                DTS.Columns.Add(new DataColumn("Rate"));
                DTS.Columns.Add(new DataColumn("Value"));
                DTS.Columns.Add(new DataColumn("ItemIdandColorId"));
                foreach (DataRow DRDS in ds_ItemGroup.Tables[0].Rows)
                {

                    DataRow DR = DTS.NewRow();
                    DR["ItemCode"] = DRDS["Itemgroupname"];
                    DTS.Rows.Add(DR);

                    foreach (DataRow Cat in dsCategory.Tables[0].Rows)
                    {
                        double TtlClosing = 0; double TtlValue = 0;
                        double TtlRate = 0; int TtlCount = 0; double TtlShrink = 0;
                        double TtlRate1 = 0; int TtlCount1 = 0;

                        string IssueItemId = ""; string IssueColorId = "";
                        

                        DataRow[] RowsItems = ds_Items.Tables[0].Select("ItemgroupId='" + DRDS["ItemgroupId"] + "' and CategoryId='" + Cat["CategoryId"] + "' ");
                        if (RowsItems.Length > 0)
                        {
                            DataRow DR1 = DTS.NewRow();
                            DR1["ItemCode"] = DRDS["Itemgroupname"];
                            DR1["ItemDescription"] = Cat["Category"];
                            DTS.Rows.Add(DR1);

                            for (int R = 0; R < RowsItems.Length; R++)
                            {
                                foreach (DataRow CLR in dsColor.Tables[0].Rows)
                                {
                                    string ColorId = CLR["ColorId"].ToString();

                                    if (ColorId == "3")
                                    {

                                    }

                                    double ST_OP = 0; double ST_OP_PurchaseGRN = 0; double ST_OP_ProcessOrder = 0; double ST_OP_ProcessOrderReceive = 0; double ST_OP_Cutting = 0; double ST_OP_StockTransferIn = 0; double ST_OP_StockTransferOut = 0;
                                    double ST_CL = 0; double ST_CL_PurchaseGRN = 0; double ST_CL_ProcessOrder = 0; double ST_CL_ProcessOrderReceive = 0; double ST_CL_Cutting = 0; double ST_CL_StockTransferIn = 0; double ST_CL_StockTransferOut = 0;

                                    #region OP

                                    DataRow[] Rows_ST_OP = ds_ST_OP.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_OP.Length > 0)
                                    {
                                        ST_OP = Convert.ToDouble(Rows_ST_OP[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_OP_PurchaseGRN = ds_ST_OP_PurchaseGRN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_OP_PurchaseGRN.Length > 0)
                                    {
                                        ST_OP_PurchaseGRN = Convert.ToDouble(Rows_ST_OP_PurchaseGRN[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_OP_ProcessOrder = ds_ST_OP_ProcessOrder.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + RowsItems[R]["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
                                    if (Rows_ST_OP_ProcessOrder.Length > 0)
                                    {
                                        ST_OP_ProcessOrder = Convert.ToDouble(Rows_ST_OP_ProcessOrder[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_OP_ProcessOrderReceive = ds_ST_OP_ProcessOrderReceive.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + RowsItems[R]["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
                                    if (Rows_ST_OP_ProcessOrderReceive.Length > 0)
                                    {
                                        ST_OP_ProcessOrderReceive = Convert.ToDouble(Rows_ST_OP_ProcessOrderReceive[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_OP_Cutting = ds_ST_OP_Cutting.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_OP_Cutting.Length > 0)
                                    {
                                        ST_OP_Cutting = Convert.ToDouble(Rows_ST_OP_Cutting[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_OP_StockTransferIN = ds_ST_OP_StockTransferIN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_OP_StockTransferIN.Length > 0)
                                    {
                                        ST_OP_StockTransferIn = Convert.ToDouble(Rows_ST_OP_StockTransferIN[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_OP_StockTransferOut = ds_ST_OP_StockTransferOut.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_OP_StockTransferOut.Length > 0)
                                    {
                                        ST_OP_StockTransferOut = Convert.ToDouble(Rows_ST_OP_StockTransferOut[0]["Qty"]);
                                    }

                                    #endregion

                                    #region CL

                                    DataRow[] Rows_ST_CL = ds_ST_CL.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_CL.Length > 0)
                                    {
                                        ST_CL = Convert.ToDouble(Rows_ST_CL[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_CL_PurchaseGRN = ds_ST_CL_PurchaseGRN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_CL_PurchaseGRN.Length > 0)
                                    {
                                        ST_CL_PurchaseGRN = Convert.ToDouble(Rows_ST_CL_PurchaseGRN[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_CL_ProcessOrder = ds_ST_CL_ProcessOrder.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + RowsItems[R]["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
                                    if (Rows_ST_CL_ProcessOrder.Length > 0)
                                    {
                                        ST_CL_ProcessOrder = Convert.ToDouble(Rows_ST_CL_ProcessOrder[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_CL_ProcessOrderReceive = ds_ST_CL_ProcessOrderReceive.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + RowsItems[R]["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
                                    if (Rows_ST_CL_ProcessOrderReceive.Length > 0)
                                    {
                                        ST_CL_ProcessOrderReceive = Convert.ToDouble(Rows_ST_CL_ProcessOrderReceive[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_CL_Cutting = ds_ST_CL_Cutting.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_CL_Cutting.Length > 0)
                                    {
                                        ST_CL_Cutting = Convert.ToDouble(Rows_ST_CL_Cutting[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_CL_StockTransferIN = ds_ST_CL_StockTransferIN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_CL_StockTransferIN.Length > 0)
                                    {
                                        ST_CL_StockTransferIn = Convert.ToDouble(Rows_ST_CL_StockTransferIN[0]["Qty"]);
                                    }
                                    DataRow[] Rows_ST_CL_StockTransferOut = ds_ST_CL_StockTransferOut.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                    if (Rows_ST_CL_StockTransferOut.Length > 0)
                                    {
                                        ST_CL_StockTransferOut = Convert.ToDouble(Rows_ST_CL_StockTransferOut[0]["Qty"]);
                                    }

                                    #endregion

                                    double OP = (ST_OP + ST_OP_PurchaseGRN + ST_OP_ProcessOrderReceive + ST_OP_StockTransferIn) - (ST_OP_ProcessOrder + ST_OP_Cutting + ST_OP_StockTransferOut);
                                    double CL = (ST_CL + ST_CL_PurchaseGRN + ST_CL_ProcessOrderReceive + ST_CL_StockTransferIn) - (ST_CL_ProcessOrder + ST_CL_Cutting + ST_CL_StockTransferOut);

                                    if (OP != 0 || CL != 0)
                                    {
                                        #region

                                        string IsReceive = "No";
                                        double CLQty = 0;

                                        DataRow DR2 = DTS.NewRow();
                                        DR2["ItemCode"] = RowsItems[R]["ItemCode"];
                                        DR2["ItemDescription"] = RowsItems[R]["Description"];

                                        DR2["Color"] = CLR["Color"].ToString();

                                        DR2["OP"] = OP.ToString("f2");

                                        if ((OP - CL) < 0)
                                        {
                                            //DR2["CL"] = CL.ToString("f2");
                                            //CLQty = CL;
                                            DR2["CL"] = (OP + CL).ToString("f2");
                                            CLQty = (OP + CL);
                                        }
                                        else
                                        {
                                            //DR2["CL"] = (OP - CL).ToString("f2");
                                            //CLQty = (OP - CL);
                                            DR2["CL"] = (OP + CL).ToString("f2");
                                            CLQty = (OP + CL);
                                        }

                                        DR2["Units"] = RowsItems[R]["Units"];

                                        if (dsPORates.Tables[0].Rows.Count > 0)
                                        {
                                            #region
                                            DataRow[] RowsPORates = dsPORates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                            if (RowsPORates.Length > 0)
                                            {
                                                for (int i = 0; i < RowsPORates.Length; i++)
                                                {
                                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                        break;

                                                    TtlRate += Convert.ToDouble(RowsPORates[i]["Rate"]);
                                                    TtlCount++;

                                                }

                                            }
                                            else
                                            {
                                                DataRow[] RowsIPOERates = dsIPOERates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + RowsItems[R]["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
                                                if (RowsIPOERates.Length > 0)
                                                {
                                                    for (int i = 0; i < RowsIPOERates.Length; i++)
                                                    {
                                                        if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                            break;

                                                        TtlRate += Convert.ToDouble(RowsIPOERates[i]["Rate"]);
                                                        TtlCount++;

                                                    }
                                                }
                                                else
                                                {
                                                    DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + RowsItems[R]["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
                                                    if (RowsIPORRates.Length > 0)
                                                    {
                                                        IsReceive = "Yes";

                                                        #region

                                                         IssueItemId = RowsIPORRates[0]["IssueItemId"].ToString();
                                                         IssueColorId = RowsIPORRates[0]["IssueColorId"].ToString();


                                                        for (int i = 0; i < RowsIPORRates.Length; i++)
                                                        {
                                                            if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                                break;

                                                            TtlRate += Convert.ToDouble(RowsIPORRates[i]["Rate"]);
                                                            TtlCount++;

                                                            TtlShrink += Convert.ToDouble(RowsIPORRates[i]["Shrink"]);
                                                        }

                                                        #region

                                                        DataRow[] RowsPORates1 = dsPORates1.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + IssueItemId + "' and ColorId='" + IssueColorId + "' ");
                                                        if (RowsPORates1.Length > 0)
                                                        {
                                                            for (int i = 0; i < RowsPORates1.Length; i++)
                                                            {
                                                                if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                                    break;

                                                                TtlRate1 += Convert.ToDouble(RowsPORates1[i]["Rate"]);
                                                                TtlCount1++;

                                                            }

                                                        }
                                                        else
                                                        {
                                                            DataRow[] RowsIPOERates1 = dsIPOERates1.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + IssueItemId + "' and IssueColorId='" + IssueColorId + "' ");
                                                            if (RowsIPOERates1.Length > 0)
                                                            {
                                                                for (int i = 0; i < RowsIPOERates1.Length; i++)
                                                                {
                                                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                                        break;

                                                                    TtlRate1 += Convert.ToDouble(RowsIPOERates1[i]["Rate"]);
                                                                    TtlCount1++;

                                                                }
                                                            }
                                                        }

                                                        #endregion




                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                                        if (RowsOPSRates.Length > 0)
                                                        {
                                                            for (int i = 0; i < RowsOPSRates.Length; i++)
                                                            {
                                                                if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                                    break;

                                                                TtlRate += Convert.ToDouble(RowsOPSRates[i]["ItemRate"]);
                                                                TtlCount++;

                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            #endregion
                                        }
                                        else if (dsIPOERates.Tables[0].Rows.Count > 0)
                                        {
                                            #region

                                            DataRow[] RowsIPOERates = dsIPOERates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + RowsItems[R]["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
                                            if (RowsIPOERates.Length > 0)
                                            {
                                                for (int i = 0; i < RowsIPOERates.Length; i++)
                                                {
                                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                        break;

                                                    TtlRate += Convert.ToDouble(RowsIPOERates[i]["Rate"]);
                                                    TtlCount++;

                                                }
                                            }
                                            else
                                            {
                                                IsReceive = "Yes";

                                                DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + RowsItems[R]["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
                                                if (RowsIPORRates.Length > 0)
                                                {
                                                     IssueItemId = RowsIPORRates[0]["IssueItemId"].ToString();
                                                     IssueColorId = RowsIPORRates[0]["IssueColorId"].ToString();

                                                    for (int i = 0; i < RowsIPORRates.Length; i++)
                                                    {
                                                        if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                            break;

                                                        TtlRate += Convert.ToDouble(RowsIPORRates[i]["Rate"]);
                                                        TtlCount++;

                                                        TtlShrink += Convert.ToDouble(RowsIPORRates[i]["Shrink"]);
                                                    }

                                                    #region

                                                    DataRow[] RowsPORates1 = dsPORates1.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + IssueItemId + "' and ColorId='" + IssueColorId + "' ");
                                                    if (RowsPORates1.Length > 0)
                                                    {
                                                        for (int i = 0; i < RowsPORates1.Length; i++)
                                                        {
                                                            if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                                break;

                                                            TtlRate1 += Convert.ToDouble(RowsPORates1[i]["Rate"]);
                                                            TtlCount1++;

                                                        }

                                                    }
                                                    else
                                                    {
                                                        DataRow[] RowsIPOERates1 = dsIPOERates1.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + IssueItemId + "' and IssueColorId='" + IssueColorId + "' ");
                                                        if (RowsIPOERates1.Length > 0)
                                                        {
                                                            for (int i = 0; i < RowsIPOERates1.Length; i++)
                                                            {
                                                                if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                                    break;

                                                                TtlRate1 += Convert.ToDouble(RowsIPOERates1[i]["Rate"]);
                                                                TtlCount1++;

                                                            }
                                                        }
                                                    }

                                                    #endregion
                                                }
                                                else
                                                {
                                                    DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                                    if (RowsOPSRates.Length > 0)
                                                    {
                                                        for (int i = 0; i < RowsOPSRates.Length; i++)
                                                        {
                                                            if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                                break;

                                                            TtlRate += Convert.ToDouble(RowsOPSRates[i]["ItemRate"]);
                                                            TtlCount++;

                                                        }
                                                    }
                                                }
                                            }

                                            #endregion
                                        }
                                        else if (dsIPORRates.Tables[0].Rows.Count > 0)
                                        {
                                            IsReceive = "Yes";

                                            #region

                                            DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + RowsItems[R]["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
                                            if (RowsIPORRates.Length > 0)
                                            {
                                                 IssueItemId = RowsIPORRates[0]["IssueItemId"].ToString();
                                                 IssueColorId = RowsIPORRates[0]["IssueColorId"].ToString();


                                                for (int i = 0; i < RowsIPORRates.Length; i++)
                                                {
                                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                        break;

                                                    TtlRate += Convert.ToDouble(RowsIPORRates[i]["Rate"]);
                                                    TtlCount++;

                                                    TtlShrink += Convert.ToDouble(RowsIPORRates[i]["Shrink"]);
                                                }

                                                #region

                                                DataRow[] RowsPORates1 = dsPORates1.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + IssueItemId + "' and ColorId='" + IssueColorId + "' ");
                                                if (RowsPORates1.Length > 0)
                                                {
                                                    for (int i = 0; i < RowsPORates1.Length; i++)
                                                    {
                                                        if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                            break;

                                                        TtlRate1 += Convert.ToDouble(RowsPORates1[i]["Rate"]);
                                                        TtlCount1++;

                                                    }

                                                }
                                                else
                                                {
                                                    DataRow[] RowsIPOERates1 = dsIPOERates1.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + IssueItemId + "' and IssueColorId='" + IssueColorId + "' ");
                                                    if (RowsIPOERates1.Length > 0)
                                                    {
                                                        for (int i = 0; i < RowsIPOERates1.Length; i++)
                                                        {
                                                            if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                                break;

                                                            TtlRate1 += Convert.ToDouble(RowsIPOERates1[i]["Rate"]);
                                                            TtlCount1++;

                                                        }
                                                    }
                                                }

                                                #endregion


                                            }
                                            else
                                            {
                                                DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                                if (RowsOPSRates.Length > 0)
                                                {
                                                    for (int i = 0; i < RowsOPSRates.Length; i++)
                                                    {
                                                        if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                            break;

                                                        TtlRate += Convert.ToDouble(RowsOPSRates[i]["ItemRate"]);
                                                        TtlCount++;

                                                    }
                                                }
                                            }

                                            #endregion
                                        }
                                        else if (dsOPSRates.Tables[0].Rows.Count > 0)
                                        {
                                            #region

                                            DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + RowsItems[R]["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                                            if (RowsOPSRates.Length > 0)
                                            {
                                                for (int i = 0; i < RowsOPSRates.Length; i++)
                                                {
                                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                        break;

                                                    TtlRate += Convert.ToDouble(RowsOPSRates[i]["ItemRate"]);
                                                    TtlCount++;

                                                }
                                            }

                                            #endregion
                                        }

                                        double Shrink = TtlShrink / TtlCount;
                                        double Rate = TtlRate / TtlCount;
                                        double Rate1 = TtlRate1 / TtlCount1;

                                        if (IsReceive == "Yes")
                                        {
                                            if (Rate.ToString() == "NaN")
                                            {
                                                DR2["Rate"] = 0;
                                                DR2["Value"] = 0;
                                            }
                                            else
                                            {
                                                double ReceiveRate = Rate + Rate1+(Rate1 * Shrink / 100);

                                                DR2["Rate"] = Convert.ToDouble(ReceiveRate).ToString("f2");
                                                DR2["Value"] = (CLQty * ReceiveRate).ToString("f2");

                                                TtlClosing += CLQty;
                                                TtlValue += (CLQty * ReceiveRate);
                                            }
                                        }
                                        else
                                        {
                                            if (Rate.ToString() == "NaN")
                                            {
                                                DR2["Rate"] = 0;
                                                DR2["Value"] = 0;
                                            }
                                            else
                                            {
                                                DR2["Rate"] = Convert.ToDouble(Rate).ToString("f2");
                                                DR2["Value"] = (CLQty * Rate).ToString("f2");

                                                TtlClosing += CLQty;
                                                TtlValue += (CLQty * Rate);
                                            }
                                        }



                                        DR2["ItemIdandColorId"] = ddlCompany.SelectedValue + "#" + RowsItems[R]["ItemMasterId"].ToString() + "#" + ColorId.ToString() + "#" + IsReceive + "#" + IssueItemId + "#" + IssueColorId;

                                        IsReceive = "No";
                                        TtlRate1 = 0; TtlCount1 = 0; TtlShrink = 0;
                                        TtlRate = 0; TtlCount = 0;
                                        IssueItemId = ""; IssueColorId = "";

                                        DTS.Rows.Add(DR2);


                                        #endregion
                                    }
                                }

                            }
                        }
                        if (TtlClosing > 0)
                        {
                            DataRow DR3 = DTS.NewRow();
                            DR3["CL"] = "-----------";
                            DR3["Value"] = "-----------";
                            DR3["Value"] = "-----------";
                            DTS.Rows.Add(DR3);

                            DataRow DR4 = DTS.NewRow();
                            DR4["CL"] = TtlClosing.ToString("f2");
                            DR4["Value"] = TtlValue.ToString("f2");
                            DTS.Rows.Add(DR4);
                        }
                    }

                }

                #endregion

                gvMaterialStockEntry.DataSource = DTS;
                gvMaterialStockEntry.DataBind();
            }
            else
            {
                gvMaterialStockEntry.DataSource = null;
                gvMaterialStockEntry.DataBind();
            }


        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= StockStatement.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Excel.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void gvMaterialStockEntry_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.TableSection = TableRowSection.TableHeader;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        protected void gridviewhrm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ItemIdandColorId")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    string[] IDs = e.CommandArgument.ToString().Split('#');

                    string yourUrl = "ItemRateChecking.aspx?CMP=" + IDs[0] + "&&ITM=" + IDs[1] + "&&CLR=" + IDs[2] + "&&ISR=" + IDs[3] + "&&ITM1=" + IDs[4] + "&&CLR1=" + IDs[5];
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);
                }
            }
        }

    }
}


