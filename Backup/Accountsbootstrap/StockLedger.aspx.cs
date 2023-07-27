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
    public partial class StockLedger : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double Qty = 0; double Value = 0;

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
                    ddlItemHead.Items.Insert(0, "All");
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
                    ddlItemGroup.Items.Insert(0, "All");
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


            DataSet ds_ST_IO = objBs.ST_IO(From, To, ddlCompany.SelectedValue);
            DataSet ds_ST_IO_PurchaseGRN = objBs.ST_IO_PurchaseGRN(From, To, ddlCompany.SelectedValue);
            DataSet ds_ST_IO_ProcessOrder = objBs.ST_IO_ProcessOrder(From, To, ddlCompany.SelectedValue);
            DataSet ds_ST_IO_ProcessOrderReceive = objBs.ST_IO_ProcessOrderReceive(From, To, ddlCompany.SelectedValue);
            DataSet ds_ST_IO_Cutting = objBs.ST_IO_Cutting(From, To, ddlCompany.SelectedValue);
            DataSet ds_ST_IO_StockTransferIN = objBs.ST_IO_StockTransfer("1", From, To, ddlCompany.SelectedValue);
            DataSet ds_ST_IO_StockTransferOut = objBs.ST_IO_StockTransfer("2", From, To, ddlCompany.SelectedValue);

            DataSet ds_ItemGroup = objBs.GetStockLedger(From, To, Convert.ToInt32(ddlCompany.SelectedValue), ddlItemHead.SelectedValue, ddlItemGroup.SelectedValue, ddlItem.SelectedValue, ddlColor.SelectedValue);

            if (ds_ItemGroup.Tables[0].Rows.Count > 0)
            {
                #region

                DataTable DTS = new DataTable();
                DTS.Columns.Add(new DataColumn("ChallanDate"));
                DTS.Columns.Add(new DataColumn("ChallanNo"));
                DTS.Columns.Add(new DataColumn("PartyName"));
                DTS.Columns.Add(new DataColumn("OrderNo"));
                DTS.Columns.Add(new DataColumn("ExcNo"));
                DTS.Columns.Add(new DataColumn("Party"));
                DTS.Columns.Add(new DataColumn("OP"));
                DTS.Columns.Add(new DataColumn("InWard"));
                DTS.Columns.Add(new DataColumn("OutWard"));
                DTS.Columns.Add(new DataColumn("CL"));
                DTS.Columns.Add(new DataColumn("Units"));
                DTS.Columns.Add(new DataColumn("Rate"));
                DTS.Columns.Add(new DataColumn("Value"));

                foreach (DataRow DRDS in ds_ItemGroup.Tables[0].Rows)
                {
                    double TtlClosing = 0; double TtlValue = 0;
                    double TtlRate = 0; int TtlCount = 0; double TtlShrink = 0;
                    double TtlRate1 = 0; int TtlCount1 = 0;

                    string IssueItemId = ""; string IssueColorId = "";

                    string ColorId = DRDS["ColorId"].ToString();

                    double ST_OP = 0; double ST_OP_PurchaseGRN = 0; double ST_OP_ProcessOrder = 0; double ST_OP_ProcessOrderReceive = 0; double ST_OP_Cutting = 0; double ST_OP_StockTransferIn = 0; double ST_OP_StockTransferOut = 0;
                    double ST_CL = 0; double ST_CL_PurchaseGRN = 0; double ST_CL_ProcessOrder = 0; double ST_CL_ProcessOrderReceive = 0; double ST_CL_Cutting = 0; double ST_CL_StockTransferIn = 0; double ST_CL_StockTransferOut = 0;

                    #region OP

                    DataRow[] Rows_ST_OP = ds_ST_OP.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_OP.Length > 0)
                    {
                        ST_OP = Convert.ToDouble(Rows_ST_OP[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_OP_PurchaseGRN = ds_ST_OP_PurchaseGRN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_OP_PurchaseGRN.Length > 0)
                    {
                        ST_OP_PurchaseGRN = Convert.ToDouble(Rows_ST_OP_PurchaseGRN[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_OP_ProcessOrder = ds_ST_OP_ProcessOrder.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + DRDS["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
                    if (Rows_ST_OP_ProcessOrder.Length > 0)
                    {
                        ST_OP_ProcessOrder = Convert.ToDouble(Rows_ST_OP_ProcessOrder[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_OP_ProcessOrderReceive = ds_ST_OP_ProcessOrderReceive.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + DRDS["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
                    if (Rows_ST_OP_ProcessOrderReceive.Length > 0)
                    {
                        ST_OP_ProcessOrderReceive = Convert.ToDouble(Rows_ST_OP_ProcessOrderReceive[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_OP_Cutting = ds_ST_OP_Cutting.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_OP_Cutting.Length > 0)
                    {
                        ST_OP_Cutting = Convert.ToDouble(Rows_ST_OP_Cutting[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_OP_StockTransferIN = ds_ST_OP_StockTransferIN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_OP_StockTransferIN.Length > 0)
                    {
                        ST_OP_StockTransferIn = Convert.ToDouble(Rows_ST_OP_StockTransferIN[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_OP_StockTransferOut = ds_ST_OP_StockTransferOut.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_OP_StockTransferOut.Length > 0)
                    {
                        ST_OP_StockTransferOut = Convert.ToDouble(Rows_ST_OP_StockTransferOut[0]["Qty"]);
                    }

                    #endregion

                    #region CL

                    DataRow[] Rows_ST_CL = ds_ST_CL.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_CL.Length > 0)
                    {
                        ST_CL = Convert.ToDouble(Rows_ST_CL[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_CL_PurchaseGRN = ds_ST_CL_PurchaseGRN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_CL_PurchaseGRN.Length > 0)
                    {
                        ST_CL_PurchaseGRN = Convert.ToDouble(Rows_ST_CL_PurchaseGRN[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_CL_ProcessOrder = ds_ST_CL_ProcessOrder.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + DRDS["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
                    if (Rows_ST_CL_ProcessOrder.Length > 0)
                    {
                        ST_CL_ProcessOrder = Convert.ToDouble(Rows_ST_CL_ProcessOrder[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_CL_ProcessOrderReceive = ds_ST_CL_ProcessOrderReceive.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + DRDS["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
                    if (Rows_ST_CL_ProcessOrderReceive.Length > 0)
                    {
                        ST_CL_ProcessOrderReceive = Convert.ToDouble(Rows_ST_CL_ProcessOrderReceive[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_CL_Cutting = ds_ST_CL_Cutting.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_CL_Cutting.Length > 0)
                    {
                        ST_CL_Cutting = Convert.ToDouble(Rows_ST_CL_Cutting[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_CL_StockTransferIN = ds_ST_CL_StockTransferIN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_CL_StockTransferIN.Length > 0)
                    {
                        ST_CL_StockTransferIn = Convert.ToDouble(Rows_ST_CL_StockTransferIN[0]["Qty"]);
                    }
                    DataRow[] Rows_ST_CL_StockTransferOut = ds_ST_CL_StockTransferOut.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                    if (Rows_ST_CL_StockTransferOut.Length > 0)
                    {
                        ST_CL_StockTransferOut = Convert.ToDouble(Rows_ST_CL_StockTransferOut[0]["Qty"]);
                    }

                    #endregion

                    double OP = (ST_OP + ST_OP_PurchaseGRN + ST_OP_ProcessOrderReceive + ST_OP_StockTransferIn) - (ST_OP_ProcessOrder + ST_OP_Cutting + ST_OP_StockTransferOut);
                    double CL = (ST_CL + ST_CL_PurchaseGRN + ST_CL_ProcessOrderReceive + ST_CL_StockTransferIn) - (ST_CL_ProcessOrder + ST_CL_Cutting + ST_CL_StockTransferOut);

                    //if (OP != 0 || CL != 0)
                    if (ST_OP != 0 || ST_OP_PurchaseGRN != 0 || ST_OP_ProcessOrderReceive != 0 || ST_OP_StockTransferIn != 0 || ST_OP_ProcessOrder != 0 || ST_OP_Cutting != 0 || ST_OP_StockTransferOut != 0 || ST_CL != 0 || ST_CL_PurchaseGRN != 0 || ST_CL_ProcessOrderReceive != 0 || ST_CL_StockTransferIn != 0 || ST_CL_ProcessOrder != 0 || ST_CL_Cutting != 0 || ST_CL_StockTransferOut != 0)
                    {
                        #region OP

                        DataRow DR = DTS.NewRow();
                        DR["ChallanDate"] = DRDS["ItemCode"];
                        DR["PartyName"] = DRDS["Description"] + " " + DRDS["Color"].ToString();
                        DR["OP"] = OP.ToString("f2");
                        DTS.Rows.Add(DR);

                        #endregion

                        #region IN and OUT

                        //1
                        DataRow[] Row_ST_IO = ds_ST_IO.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                        if (Row_ST_IO.Length > 0)
                        {
                            for (int R = 0; R < Row_ST_IO.Length; R++)
                            {
                                DataRow DRIO = DTS.NewRow();

                                DRIO["ChallanDate"] = Convert.ToDateTime(Row_ST_IO[R]["Date"]).ToString("dd/MM/yyyy");
                                DRIO["ChallanNo"] = "";
                                DRIO["PartyName"] = "Opening Stock Entry";
                                DRIO["OrderNo"] = "";
                                DRIO["ExcNo"] = "";
                                DRIO["Party"] = "";
                                DRIO["InWard"] = Row_ST_IO[R]["Qty"];
                                DRIO["Rate"] = Row_ST_IO[R]["Rate"];
                                DTS.Rows.Add(DRIO);
                            }
                        }

                        //2
                        DataRow[] Row_ST_IO_PurchaseGRN = ds_ST_IO_PurchaseGRN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                        if (Row_ST_IO_PurchaseGRN.Length > 0)
                        {
                            for (int R = 0; R < Row_ST_IO_PurchaseGRN.Length; R++)
                            {
                                DataRow DRIO = DTS.NewRow();

                                DRIO["ChallanDate"] = Convert.ToDateTime(Row_ST_IO_PurchaseGRN[R]["Date"]).ToString("dd/MM/yyyy");
                                DRIO["ChallanNo"] = Row_ST_IO_PurchaseGRN[R]["ChallanNo"];
                                DRIO["PartyName"] = Row_ST_IO_PurchaseGRN[R]["LedgerName"];
                                DRIO["OrderNo"] = Row_ST_IO_PurchaseGRN[R]["FullRecPONo"];

                                if (Row_ST_IO_PurchaseGRN[R]["PurchaseforId"].ToString() == "1")
                                {
                                    DRIO["ExcNo"] = Row_ST_IO_PurchaseGRN[R]["PurchaseFor"];
                                }
                                else
                                {
                                    DRIO["Party"] = Row_ST_IO_PurchaseGRN[R]["PurchaseFor"];
                                }

                                DRIO["InWard"] = Row_ST_IO_PurchaseGRN[R]["Qty"];
                                DRIO["Rate"] = Row_ST_IO_PurchaseGRN[R]["Rate"];
                                DTS.Rows.Add(DRIO);
                            }
                        }

                        //3
                        DataRow[] Row_ST_IO_ProcessOrder = ds_ST_IO_ProcessOrder.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + DRDS["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
                        if (Row_ST_IO_ProcessOrder.Length > 0)
                        {
                            for (int R = 0; R < Row_ST_IO_ProcessOrder.Length; R++)
                            {
                                DataRow DRIO = DTS.NewRow();

                                DRIO["ChallanDate"] = Convert.ToDateTime(Row_ST_IO_ProcessOrder[R]["Date"]).ToString("dd/MM/yyyy");
                                DRIO["ChallanNo"] = "";
                                DRIO["PartyName"] = Row_ST_IO_ProcessOrder[R]["LedgerName"];
                                DRIO["OrderNo"] = Row_ST_IO_ProcessOrder[R]["FullPONo"];

                                if (Row_ST_IO_ProcessOrder[R]["PurchaseforId"].ToString() == "1")
                                {
                                    DRIO["ExcNo"] = Row_ST_IO_ProcessOrder[R]["PurchaseFor"];
                                }
                                else
                                {
                                    DRIO["Party"] = Row_ST_IO_ProcessOrder[R]["PurchaseFor"];
                                }

                                DRIO["OutWard"] = Row_ST_IO_ProcessOrder[R]["Qty"];
                                DRIO["Rate"] = Row_ST_IO_ProcessOrder[R]["Rate"];
                                DTS.Rows.Add(DRIO);
                            }
                        }

                        //4
                        DataRow[] Row_ST_IO_ProcessOrderReceive = ds_ST_IO_ProcessOrderReceive.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + DRDS["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
                        if (Row_ST_IO_ProcessOrderReceive.Length > 0)
                        {
                            for (int R = 0; R < Row_ST_IO_ProcessOrderReceive.Length; R++)
                            {
                                DataRow DRIO = DTS.NewRow();

                                DRIO["ChallanDate"] = Convert.ToDateTime(Row_ST_IO_ProcessOrderReceive[R]["Date"]).ToString("dd/MM/yyyy");
                                DRIO["ChallanNo"] = "";
                                DRIO["PartyName"] = Row_ST_IO_ProcessOrderReceive[R]["LedgerName"];
                                DRIO["OrderNo"] = Row_ST_IO_ProcessOrderReceive[R]["FullRecPONo"];

                                if (Row_ST_IO_ProcessOrderReceive[R]["PurchaseforId"].ToString() == "1")
                                {
                                    DRIO["ExcNo"] = Row_ST_IO_ProcessOrderReceive[R]["PurchaseFor"];
                                }
                                else
                                {
                                    DRIO["Party"] = Row_ST_IO_ProcessOrderReceive[R]["PurchaseFor"];
                                }

                                DRIO["InWard"] = Row_ST_IO_ProcessOrderReceive[R]["Qty"];
                                DRIO["Rate"] = Row_ST_IO_ProcessOrderReceive[R]["Rate"];
                                DTS.Rows.Add(DRIO);
                            }
                        }

                        //5
                        DataRow[] Row_ST_IO_Cutting = ds_ST_IO_Cutting.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                        if (Row_ST_IO_Cutting.Length > 0)
                        {
                            for (int R = 0; R < Row_ST_IO_Cutting.Length; R++)
                            {
                                DataRow DRIO = DTS.NewRow();

                                DRIO["ChallanDate"] = Convert.ToDateTime(Row_ST_IO_Cutting[R]["Date"]).ToString("dd/MM/yyyy");
                                DRIO["ChallanNo"] = "";
                                DRIO["PartyName"] = Row_ST_IO_Cutting[R]["LedgerName"];
                                DRIO["OrderNo"] = Row_ST_IO_Cutting[R]["FullCuttingNo"];
                                DRIO["ExcNo"] = Row_ST_IO_Cutting[R]["ExcNo"];
                                DRIO["Party"] = "";
                                DRIO["OutWard"] = Row_ST_IO_Cutting[R]["Qty"];
                                DRIO["Rate"] = "";
                                DTS.Rows.Add(DRIO);
                            }
                        }

                        //6
                        DataRow[] Row_ST_IO_StockTransferIN = ds_ST_IO_StockTransferIN.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                        if (Row_ST_IO_StockTransferIN.Length > 0)
                        {
                            for (int R = 0; R < Row_ST_IO_StockTransferIN.Length; R++)
                            {
                                DataRow DRIO = DTS.NewRow();

                                DRIO["ChallanDate"] = Convert.ToDateTime(Row_ST_IO_StockTransferIN[R]["Date"]).ToString("dd/MM/yyyy");
                                DRIO["ChallanNo"] = Row_ST_IO_StockTransferIN[R]["ChallanNo"];
                                DRIO["PartyName"] = Row_ST_IO_StockTransferIN[R]["LedgerName"];
                                DRIO["OrderNo"] = "";
                                DRIO["ExcNo"] = "";
                                DRIO["Party"] = "Fabric Store";
                                DRIO["InWard"] = Row_ST_IO_StockTransferIN[R]["Qty"];
                                DRIO["Rate"] = Row_ST_IO_StockTransferIN[R]["Rate"];
                                DTS.Rows.Add(DRIO);
                            }
                        }

                        //7
                        DataRow[] Row_ST_IO_StockTransferOut = ds_ST_IO_StockTransferOut.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
                        if (Row_ST_IO_StockTransferOut.Length > 0)
                        {
                            for (int R = 0; R < Row_ST_IO_StockTransferOut.Length; R++)
                            {
                                DataRow DRIO = DTS.NewRow();

                                DRIO["ChallanDate"] = Convert.ToDateTime(Row_ST_IO_StockTransferOut[R]["Date"]).ToString("dd/MM/yyyy");
                                DRIO["ChallanNo"] = Row_ST_IO_StockTransferOut[R]["ChallanNo"];
                                DRIO["PartyName"] = Row_ST_IO_StockTransferOut[R]["LedgerName"];
                                DRIO["OrderNo"] = "";
                                DRIO["ExcNo"] = "";
                                DRIO["Party"] = "Fabric Store";
                                DRIO["OutWard"] = Row_ST_IO_StockTransferOut[R]["Qty"];
                                DRIO["Rate"] = Row_ST_IO_StockTransferOut[R]["Rate"];
                                DTS.Rows.Add(DRIO);
                            }
                        }

                        #endregion

                        #region CL

                        string IsReceive = "No";
                        double CLQty = 0;

                        DataRow DR2 = DTS.NewRow();
                        DR2["ChallanDate"] = "";// DRDS["ChallanDate"];
                        DR2["PartyName"] = "";// DRDS["Description"];

                        DR2["OrderNo"] = "";// CLR["OrderNo"].ToString();

                        //DR2["OP"] = OP.ToString("f2");

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

                        DR2["Units"] = DRDS["Units"];

                        if (dsPORates.Tables[0].Rows.Count > 0)
                        {
                            #region
                            DataRow[] RowsPORates = dsPORates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
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
                                DataRow[] RowsIPOERates = dsIPOERates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + DRDS["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
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
                                    DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + DRDS["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
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
                                        DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
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

                            DataRow[] RowsIPOERates = dsIPOERates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and IssueItemId='" + DRDS["ItemMasterId"] + "' and IssueColorId='" + ColorId + "' ");
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

                                DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + DRDS["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
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
                                    DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
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

                            DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ReceiveItemId='" + DRDS["ItemMasterId"] + "' and ReceiveColorId='" + ColorId + "' ");
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
                                DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
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

                            DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + ddlCompany.SelectedValue + "' and ItemId='" + DRDS["ItemMasterId"] + "' and ColorId='" + ColorId + "' ");
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
                            double ReceiveRate = Rate + Rate1 + (Rate1 * Shrink / 100);

                            if (Rate.ToString() == "NaN")
                            {
                                DR2["Rate"] = 0;
                                DR2["Value"] = 0;
                            }
                            else
                            {
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

                        TtlRate = 0; TtlCount = 0;

                        DTS.Rows.Add(DR2);

                        #endregion
                    }


                    //if (TtlClosing > 0)
                    //{
                    //    DataRow DR3 = DTS.NewRow();
                    //    DR3["CL"] = "-----------";
                    //    DR3["Value"] = "-----------";
                    //    DR3["Value"] = "-----------";
                    //    DTS.Rows.Add(DR3);

                    //    DataRow DR4 = DTS.NewRow();
                    //    DR4["CL"] = TtlClosing.ToString("f2");
                    //    DR4["Value"] = TtlValue.ToString("f2");
                    //    DTS.Rows.Add(DR4);
                    //}


                }

                #endregion

                gvStockLedger.DataSource = DTS;
                gvStockLedger.DataBind();
            }
            else
            {
                gvStockLedger.DataSource = null;
                gvStockLedger.DataBind();
            }


        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= StockLedger.xls");
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

        protected void gvStockLedger_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}


