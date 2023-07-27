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
    public partial class BuyerOrderReport : System.Web.UI.Page
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

                DataSet dsset = objBs.getLedger(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlBuyerCode.DataSource = dsset.Tables[0];
                    ddlBuyerCode.DataTextField = "CompanyCode";
                    ddlBuyerCode.DataValueField = "LedgerID";
                    ddlBuyerCode.DataBind();
                    ddlBuyerCode.Items.Insert(0, "All");

                }

                DataSet dsExcNo = objBs.getAllExcNo(ddlBuyerCode.SelectedValue);
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExcNo.Tables[0];
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderId";
                    chkExcNo.DataBind();

                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "All");
                }

            }
        }

        protected void buyer_code(object sender, EventArgs e)
        {
            DataSet dsExcNo = objBs.getAllExcNo(ddlBuyerCode.SelectedValue);
            if (dsExcNo.Tables[0].Rows.Count > 0)
            {
                chkExcNo.DataSource = dsExcNo.Tables[0];
                chkExcNo.DataTextField = "ExcNo";
                chkExcNo.DataValueField = "BuyerOrderId";
                chkExcNo.DataBind();

                ddlExcNo.DataSource = dsExcNo.Tables[0];
                ddlExcNo.DataTextField = "ExcNo";
                ddlExcNo.DataValueField = "BuyerOrderId";
                ddlExcNo.DataBind();
                ddlExcNo.Items.Insert(0, "All");
            }
        }

        protected void ddlReportType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "1" || ddlReportType.SelectedValue == "5" || ddlReportType.SelectedValue == "6" || ddlReportType.SelectedValue == "7")
            {
                #region
                AccountingYear.Visible = false;

                BuyerCode.Visible = true;
                ExcNo.Visible = true;

                Months.Visible = false;
                Date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblDate.Text = "Order Date";
                lblFrom.Text = "From";
                lblTo.Text = "To";
                #endregion
            }
            else if (ddlReportType.SelectedValue == "2" || ddlReportType.SelectedValue == "10")
            {
                #region
                AccountingYear.Visible = true;

                BuyerCode.Visible = true;
                ExcNo.Visible = false;

                Months.Visible = true;

                Date.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;
                #endregion
            }
            else if (ddlReportType.SelectedValue == "3")
            {
                #region
                AccountingYear.Visible = false;

                BuyerCode.Visible = true;
                ExcNo.Visible = false;
                Months.Visible = false;

                Date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblDate.Text = "Shipment Date";
                lblFrom.Text = "From";
                lblTo.Text = "To";

                #endregion
            }
            else if (ddlReportType.SelectedValue == "4")//Shipment Wise Details
            {
                #region
                AccountingYear.Visible = false;

                BuyerCode.Visible = true;
                ExcNo.Visible = false;
                Months.Visible = false;

                Date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblDate.Text = "Shipment Date";
                lblFrom.Text = "From";
                lblTo.Text = "To";

                #endregion
            }

            gvBuyerOrderSheet.DataSource = null;
            gvBuyerOrderSheet.DataBind();

            gvBuyerOrder.DataSource = null;
            gvBuyerOrder.DataBind();

            gvSketches.DataSource = null;
            gvSketches.DataBind();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            gvBuyerOrderSheet.DataSource = null;
            gvBuyerOrderSheet.DataBind();

            gvBuyerOrder.DataSource = null;
            gvBuyerOrder.DataBind();

            gvSketches.DataSource = null;
            gvSketches.DataBind();

            gvBuyerOrderQty.DataSource = null;
            gvBuyerOrderQty.DataBind();


            if (ddlReportType.SelectedValue == "1")//Buyer Order Sheet
            {
                #region

                string Remarks = "";
                string CurrencyName = "";

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Column1"));
                dt.Columns.Add(new DataColumn("Column2"));
                dt.Columns.Add(new DataColumn("Column3"));
                dt.Columns.Add(new DataColumn("Column4"));
                dt.Columns.Add(new DataColumn("Column5"));

                dt.Columns.Add(new DataColumn("Column6"));
                dt.Columns.Add(new DataColumn("Column7"));
                dt.Columns.Add(new DataColumn("Column8"));
                dt.Columns.Add(new DataColumn("Column9"));
                dt.Columns.Add(new DataColumn("Column10"));
                dt.Columns.Add(new DataColumn("Column11"));

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                foreach (ListItem listItem in chkExcNo.Items)
                {
                    if (chkExcNo.SelectedIndex < 0)
                    {
                        DataSet ds = objBs.BuyerOrder_BuyerOrderSheet_Report(listItem.Value, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            #region

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Remarks = dr["Remarks"].ToString();

                                DataRow dr_export = dt.NewRow();

                                dr_export["Column1"] = "Exc No. :-";
                                dr_export["Column2"] = dr["ExcNo"];

                                dr_export["Column3"] = "Main Fabric :-";
                                dr_export["Column4"] = dr["ItemCode"];

                                dr_export["Column5"] = "";

                                dr_export["Column6"] = "Delivery Date :-";
                                dr_export["Column7"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");

                                dr_export["Column8"] = "Shipment Mode :-";
                                dr_export["Column9"] = dr["ShipmentMode"];
                                dr_export["Column10"] = "";

                                dt.Rows.Add(dr_export);
                            }
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DataRow dr_export = dt.NewRow();

                                dr_export["Column1"] = "Buyer PO No. :-";
                                dr_export["Column2"] = dr["BuyerPONo"];

                                dr_export["Column3"] = "";
                                dr_export["Column4"] = "";

                                dr_export["Column5"] = "";

                                dr_export["Column6"] = "Order Date :-";
                                dr_export["Column7"] = Convert.ToDateTime(dr["OrderDate"]).ToString("dd/MM/yyyy");

                                dr_export["Column8"] = "";
                                dr_export["Column9"] = "";
                                dr_export["Column10"] = "";

                                dt.Rows.Add(dr_export);
                            }


                            DataRow DRE1 = dt.NewRow();
                            DRE1["Column1"] = "";
                            DRE1["Column2"] = "";

                            DRE1["Column3"] = "";
                            DRE1["Column4"] = "";

                            DRE1["Column5"] = "";

                            DRE1["Column6"] = "";
                            DRE1["Column7"] = "";

                            DRE1["Column8"] = "";
                            DRE1["Column9"] = "";
                            DRE1["Column10"] = "";

                            dt.Rows.Add(DRE1);

                            DataRow DRE2 = dt.NewRow();
                            DRE2["Column1"] = "Style No. :-";
                            DRE2["Column2"] = "Description :-";

                            DRE2["Column3"] = "";
                            DRE2["Column4"] = "";

                            DRE2["Column5"] = "Color :-";

                            DRE2["Column6"] = "";
                            DRE2["Column7"] = "Quantity :-";

                            DRE2["Column8"] = "Rate :-";
                            DRE2["Column9"] = "";
                            DRE2["Column10"] = "Amount :-";

                            dt.Rows.Add(DRE2);


                            DataRow DRSC = dt.NewRow();

                            DRSC["Column1"] = "";
                            DRSC["Column2"] = "";

                            DRSC["Column3"] = "";
                            DRSC["Column4"] = "";

                            DRSC["Column5"] = "";

                            DRSC["Column6"] = "";
                            DRSC["Column7"] = "";

                            DRSC["Column8"] = "";
                            DRSC["Column9"] = "";

                            DRSC["Column10"] = "";

                            dt.Rows.Add(DRSC);


                            ds = objBs.getTransBuyerOrdervaluesExcel(Convert.ToInt32(listItem.Value));
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                CurrencyName = dr["CurrencyName"].ToString();
                                DataRow dr_export = dt.NewRow();

                                dr_export["Column1"] = dr["StyleNo"];
                                dr_export["Column2"] = dr["Description"];

                                dr_export["Column3"] = "";
                                dr_export["Column4"] = "";

                                dr_export["Column5"] = dr["Color"];
                                dr_export["Column6"] = "";

                                dr_export["Column7"] = dr["Qty"];
                                dr_export["Column8"] = dr["Rate"];
                                dr_export["Column9"] = CurrencyName;
                                dr_export["Column10"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"])).ToString("f2");

                                dt.Rows.Add(dr_export);

                            }


                            DataRow DREmp1 = dt.NewRow();
                            DREmp1["Column1"] = "";
                            DREmp1["Column2"] = "";
                            DREmp1["Column3"] = "";
                            DREmp1["Column4"] = "";
                            DREmp1["Column5"] = "";
                            DREmp1["Column6"] = "";
                            DREmp1["Column7"] = "";
                            DREmp1["Column8"] = "";
                            DREmp1["Column9"] = "";
                            DREmp1["Column10"] = "";
                            dt.Rows.Add(DREmp1);

                            DataRow DREmp2 = dt.NewRow();
                            DREmp2["Column1"] = "";
                            DREmp2["Column2"] = "";
                            DREmp2["Column3"] = "";
                            DREmp2["Column4"] = "";
                            DREmp2["Column5"] = "";
                            DREmp2["Column6"] = "";
                            DREmp2["Column7"] = "";
                            DREmp2["Column8"] = "";
                            DREmp2["Column9"] = "";
                            DREmp2["Column10"] = "";
                            dt.Rows.Add(DREmp2);

                            DataRow DREmp3 = dt.NewRow();
                            DREmp3["Column1"] = "";
                            DREmp3["Column2"] = "";
                            DREmp3["Column3"] = "";
                            DREmp3["Column4"] = "";
                            DREmp3["Column5"] = "";
                            DREmp3["Column6"] = "";
                            DREmp3["Column7"] = "";
                            DREmp3["Column8"] = "";
                            DREmp3["Column9"] = "";
                            DREmp3["Column10"] = "";
                            dt.Rows.Add(DREmp3);

                            DataRow DRE6 = dt.NewRow();
                            DRE6["Column1"] = "Remarks:-";
                            DRE6["Column2"] = Remarks;
                            DRE6["Column3"] = "";
                            DRE6["Column4"] = "";
                            DRE6["Column5"] = "";
                            DRE6["Column6"] = "";
                            DRE6["Column7"] = "";
                            DRE6["Column8"] = "";
                            DRE6["Column9"] = "";
                            DRE6["Column10"] = "";
                            dt.Rows.Add(DRE6);

                            DataSet dsSketches = objBs.BuyerOrderSketches(listItem.Value);
                            foreach (DataRow DRS in dsSketches.Tables[0].Rows)
                            {
                                DataRow DR7 = dt.NewRow();
                                DR7["Column11"] = ("http://" + Request.Url.Authority + DRS["Sketch"].ToString().Replace("~", string.Empty));
                                dt.Rows.Add(DR7);
                            }

                            DataRow DREmp4 = dt.NewRow();
                            DREmp4["Column1"] = "";
                            DREmp4["Column2"] = "";
                            DREmp4["Column3"] = "";
                            DREmp4["Column4"] = "";
                            DREmp4["Column5"] = "";
                            DREmp4["Column6"] = "";
                            DREmp4["Column7"] = "";
                            DREmp4["Column8"] = "";
                            DREmp4["Column9"] = "";
                            DREmp4["Column10"] = "";
                            dt.Rows.Add(DREmp4);


                            #endregion

                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            DataSet ds = objBs.BuyerOrder_BuyerOrderSheet_Report(listItem.Value, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                #region

                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    Remarks = dr["Remarks"].ToString();

                                    DataRow dr_export = dt.NewRow();

                                    dr_export["Column1"] = "Exc No. :-";
                                    dr_export["Column2"] = dr["ExcNo"];

                                    dr_export["Column3"] = "Main Fabric :-";
                                    dr_export["Column4"] = dr["ItemCode"];

                                    dr_export["Column5"] = "";

                                    dr_export["Column6"] = "Delivery Date :-";
                                    dr_export["Column7"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");

                                    dr_export["Column8"] = "Shipment Mode :-";
                                    dr_export["Column9"] = dr["ShipmentMode"];
                                    dr_export["Column10"] = "";

                                    dt.Rows.Add(dr_export);
                                }
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    DataRow dr_export = dt.NewRow();

                                    dr_export["Column1"] = "Buyer PO No. :-";
                                    dr_export["Column2"] = dr["BuyerPONo"];

                                    dr_export["Column3"] = "";
                                    dr_export["Column4"] = "";

                                    dr_export["Column5"] = "";

                                    dr_export["Column6"] = "Order Date :-";
                                    dr_export["Column7"] = Convert.ToDateTime(dr["OrderDate"]).ToString("dd/MM/yyyy");

                                    dr_export["Column8"] = "";
                                    dr_export["Column9"] = "";
                                    dr_export["Column10"] = "";

                                    dt.Rows.Add(dr_export);
                                }


                                DataRow DRE1 = dt.NewRow();
                                DRE1["Column1"] = "";
                                DRE1["Column2"] = "";

                                DRE1["Column3"] = "";
                                DRE1["Column4"] = "";

                                DRE1["Column5"] = "";

                                DRE1["Column6"] = "";
                                DRE1["Column7"] = "";

                                DRE1["Column8"] = "";
                                DRE1["Column9"] = "";
                                DRE1["Column10"] = "";

                                dt.Rows.Add(DRE1);

                                DataRow DRE2 = dt.NewRow();
                                DRE2["Column1"] = "Style No. :-";
                                DRE2["Column2"] = "Description :-";

                                DRE2["Column3"] = "";
                                DRE2["Column4"] = "";

                                DRE2["Column5"] = "Color :-";

                                DRE2["Column6"] = "";
                                DRE2["Column7"] = "Quantity :-";

                                DRE2["Column8"] = "Rate :-";
                                DRE2["Column9"] = "";
                                DRE2["Column10"] = "Amount :-";

                                dt.Rows.Add(DRE2);


                                DataRow DRSC = dt.NewRow();

                                DRSC["Column1"] = "";
                                DRSC["Column2"] = "";

                                DRSC["Column3"] = "";
                                DRSC["Column4"] = "";

                                DRSC["Column5"] = "";

                                DRSC["Column6"] = "";
                                DRSC["Column7"] = "";

                                DRSC["Column8"] = "";
                                DRSC["Column9"] = "";

                                DRSC["Column10"] = "";

                                dt.Rows.Add(DRSC);


                                ds = objBs.getTransBuyerOrdervaluesExcel(Convert.ToInt32(listItem.Value));
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    CurrencyName = dr["CurrencyName"].ToString();
                                    DataRow dr_export = dt.NewRow();

                                    dr_export["Column1"] = dr["StyleNo"];
                                    dr_export["Column2"] = dr["Description"];

                                    dr_export["Column3"] = "";
                                    dr_export["Column4"] = "";

                                    dr_export["Column5"] = dr["Color"];
                                    dr_export["Column6"] = "";

                                    dr_export["Column7"] = dr["Qty"];
                                    dr_export["Column8"] = dr["Rate"];
                                    dr_export["Column9"] = CurrencyName;
                                    dr_export["Column10"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"])).ToString("f2");

                                    dt.Rows.Add(dr_export);

                                }


                                DataRow DREmp1 = dt.NewRow();
                                DREmp1["Column1"] = "";
                                DREmp1["Column2"] = "";
                                DREmp1["Column3"] = "";
                                DREmp1["Column4"] = "";
                                DREmp1["Column5"] = "";
                                DREmp1["Column6"] = "";
                                DREmp1["Column7"] = "";
                                DREmp1["Column8"] = "";
                                DREmp1["Column9"] = "";
                                DREmp1["Column10"] = "";
                                dt.Rows.Add(DREmp1);

                                DataRow DREmp2 = dt.NewRow();
                                DREmp2["Column1"] = "";
                                DREmp2["Column2"] = "";
                                DREmp2["Column3"] = "";
                                DREmp2["Column4"] = "";
                                DREmp2["Column5"] = "";
                                DREmp2["Column6"] = "";
                                DREmp2["Column7"] = "";
                                DREmp2["Column8"] = "";
                                DREmp2["Column9"] = "";
                                DREmp2["Column10"] = "";
                                dt.Rows.Add(DREmp2);

                                DataRow DREmp3 = dt.NewRow();
                                DREmp3["Column1"] = "";
                                DREmp3["Column2"] = "";
                                DREmp3["Column3"] = "";
                                DREmp3["Column4"] = "";
                                DREmp3["Column5"] = "";
                                DREmp3["Column6"] = "";
                                DREmp3["Column7"] = "";
                                DREmp3["Column8"] = "";
                                DREmp3["Column9"] = "";
                                DREmp3["Column10"] = "";
                                dt.Rows.Add(DREmp3);

                                DataRow DRE6 = dt.NewRow();
                                DRE6["Column1"] = "Remarks:-";
                                DRE6["Column2"] = Remarks;
                                DRE6["Column3"] = "";
                                DRE6["Column4"] = "";
                                DRE6["Column5"] = "";
                                DRE6["Column6"] = "";
                                DRE6["Column7"] = "";
                                DRE6["Column8"] = "";
                                DRE6["Column9"] = "";
                                DRE6["Column10"] = "";
                                dt.Rows.Add(DRE6);

                                DataSet dsSketches = objBs.BuyerOrderSketches(listItem.Value);
                                foreach (DataRow DRS in dsSketches.Tables[0].Rows)
                                {
                                    DataRow DR7 = dt.NewRow();
                                    DR7["Column11"] = ("http://" + Request.Url.Authority + DRS["Sketch"].ToString().Replace("~", string.Empty));
                                    dt.Rows.Add(DR7);
                                }

                                DataRow DREmp4 = dt.NewRow();
                                DREmp4["Column1"] = "";
                                DREmp4["Column2"] = "";
                                DREmp4["Column3"] = "";
                                DREmp4["Column4"] = "";
                                DREmp4["Column5"] = "";
                                DREmp4["Column6"] = "";
                                DREmp4["Column7"] = "";
                                DREmp4["Column8"] = "";
                                DREmp4["Column9"] = "";
                                DREmp4["Column10"] = "";
                                dt.Rows.Add(DREmp4);


                                #endregion

                            }
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    gvBuyerOrderSheet.ShowHeader = false;
                    gvBuyerOrderSheet.Caption = "Buyer Order Sheet";
                    gvBuyerOrderSheet.DataSource = dt;
                    gvBuyerOrderSheet.DataBind();
                }
                else
                {
                    gvBuyerOrderSheet.DataSource = null;
                    gvBuyerOrderSheet.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "2")//Month Wise Order List 
            {
                if (txtYear.Text.Length < 4)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Accounting Year.');", true);
                    return;
                }

                string IsFirst = "Yes";
                string Months = "All";

                foreach (ListItem listItem in chkMonths.Items)
                {
                    #region
                    if (chkMonths.SelectedIndex >= 0)
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                Months = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                Months = Months + "," + listItem.Value;
                            }
                        }
                    }
                    #endregion
                }

                #region

                DateTime FromYear = DateTime.ParseExact("01/04/" + txtYear.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToYear = DateTime.ParseExact("31/03/" + txtYear.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1); ;

                DataSet dsMonthWiseOrderList_Style = objBs.BuyerOrder_MonthWiseOrderList_Style_Report(FromYear, ToYear, ddlBuyerCode.SelectedValue, Months);

                DataSet dsMonthWiseOrderList = objBs.BuyerOrder_MonthWiseOrderList_Report(FromYear, ToYear, ddlBuyerCode.SelectedValue, Months);
                if (dsMonthWiseOrderList.Tables[0].Rows.Count > 0)
                {
                    #region
                    int SNo = 1;
                    DataTable dt_2 = new DataTable();
                    dt_2.Columns.Add(new DataColumn("SNo"));
                    dt_2.Columns.Add(new DataColumn("Month"));
                    dt_2.Columns.Add(new DataColumn("Buyer"));
                    dt_2.Columns.Add(new DataColumn("ExcNo"));
                    dt_2.Columns.Add(new DataColumn("BuyerPoNo"));
                    dt_2.Columns.Add(new DataColumn("ShipmentDate"));
                    dt_2.Columns.Add(new DataColumn("ShipmentMode"));
                    dt_2.Columns.Add(new DataColumn("Qty"));
                    dt_2.Columns.Add(new DataColumn("Amount"));
                    dt_2.Columns.Add(new DataColumn("CurrencyName"));
                    dt_2.Columns.Add(new DataColumn("StyleNo"));

                    foreach (DataRow DR_2 in dsMonthWiseOrderList.Tables[0].Rows)
                    {
                        DataRow[] rows;

                        DataRow DR2 = dt_2.NewRow();
                        DR2["SNo"] = SNo++;
                        DR2["Month"] = Convert.ToDateTime(DR_2["OrderDate"]).ToString("MMM");
                        DR2["Buyer"] = DR_2["CompanyCode"];
                        DR2["ExcNo"] = DR_2["ExcNo"];
                        DR2["BuyerPoNo"] = DR_2["BuyerPoNo"];
                        DR2["ShipmentDate"] = Convert.ToDateTime(DR_2["ShipmentDate"]).ToString("dd/MM/yyyy");
                        DR2["ShipmentMode"] = DR_2["ShipmentMode"];
                        DR2["Qty"] = DR_2["Qty"];
                        DR2["Amount"] = DR_2["Amount"];
                        DR2["CurrencyName"] = DR_2["CurrencyName"];

                        string StyleNo = "";
                        rows = dsMonthWiseOrderList_Style.Tables[0].Select("BuyerOrderId='" + DR_2["BuyerOrderId"] + "'");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (i == 0)
                            {
                                StyleNo = rows[i]["StyleNo"].ToString();
                            }
                            else
                            {
                                StyleNo = StyleNo + " / " + rows[i]["StyleNo"].ToString();
                            }
                        }

                        DR2["StyleNo"] = StyleNo;

                        dt_2.Rows.Add(DR2);
                    }

                    #endregion

                    gvBuyerOrder.ShowHeader = true;
                    gvBuyerOrder.Caption = "Month Wise Order List";
                    gvBuyerOrder.DataSource = dt_2;
                    gvBuyerOrder.DataBind();
                }
                else
                {
                    gvBuyerOrder.DataSource = null;
                    gvBuyerOrder.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "3")//Shipment Wise Summary 
            {
                #region

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;

                DataSet dsShipmentWiseSummary_Style = objBs.BuyerOrder_ShipmentWiseSummary_Style_Report(chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsShipmentWiseSummary = objBs.BuyerOrder_ShipmentWiseSummary_Report(chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (dsShipmentWiseSummary.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt_2 = new DataTable();
                    dt_2.Columns.Add(new DataColumn("SNo"));
                    dt_2.Columns.Add(new DataColumn("ExcNo"));
                    dt_2.Columns.Add(new DataColumn("StyleNo"));
                    dt_2.Columns.Add(new DataColumn("Description"));
                    dt_2.Columns.Add(new DataColumn("MainFabric"));
                    dt_2.Columns.Add(new DataColumn("ShipmentDate"));
                    dt_2.Columns.Add(new DataColumn("Qty"));
                    dt_2.Columns.Add(new DataColumn("Hold"));

                    int SNo = 1;

                    foreach (DataRow DR_2 in dsShipmentWiseSummary.Tables[0].Rows)
                    {
                        DataRow[] rows;

                        DataRow DR2 = dt_2.NewRow();

                        DR2["SNo"] = SNo++;
                        DR2["ExcNo"] = DR_2["ExcNo"];

                        string StyleNo = ""; string Description = "";
                        rows = dsShipmentWiseSummary_Style.Tables[0].Select("BuyerOrderId='" + DR_2["BuyerOrderId"] + "'");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (i == 0)
                            {
                                StyleNo = rows[i]["StyleNo"].ToString();
                                Description = rows[i]["Description"].ToString();
                            }
                            else
                            {
                                StyleNo = StyleNo + " / " + rows[i]["StyleNo"].ToString();
                                Description = Description + " / " + rows[i]["Description"].ToString();
                            }
                        }

                        DR2["StyleNo"] = StyleNo;
                        DR2["Description"] = Description;

                        DR2["MainFabric"] = DR_2["ItemDescription"];
                        DR2["ShipmentDate"] = Convert.ToDateTime(DR_2["ShipmentDate"]).ToString("dd/MM/yyyy");
                        DR2["Qty"] = DR_2["Qty"];
                        DR2["Hold"] = DR_2["Hold"];

                        dt_2.Rows.Add(DR2);
                    }

                    #endregion

                    gvBuyerOrder.ShowHeader = true;
                    gvBuyerOrder.Caption = "Shipment Wise Summary";
                    gvBuyerOrder.DataSource = dt_2;
                    gvBuyerOrder.DataBind();

                    int RI = 0;
                    foreach (DataRow dr in dt_2.Rows)
                    {
                        if (dr["Hold"].ToString() == "Yes")
                        {
                            gvBuyerOrder.Rows[RI].BackColor = System.Drawing.ColorTranslator.FromHtml("#d0edef");
                        }
                        RI++;
                    }
                }
                else
                {
                    gvBuyerOrder.DataSource = null;
                    gvBuyerOrder.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "4")//Shipment Wise Details 
            {
                #region

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet dsShipmentWiseSummary_Style = objBs.BuyerOrder_ShipmentWiseDetails_Style_Report(chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsShipmentWiseSummary = objBs.BuyerOrder_ShipmentWiseDetails_Report(chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (dsShipmentWiseSummary.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt_2 = new DataTable();
                    dt_2.Columns.Add(new DataColumn("ExcNo"));
                    dt_2.Columns.Add(new DataColumn("DeliveryDate"));
                    dt_2.Columns.Add(new DataColumn("ShipmentDate"));
                    dt_2.Columns.Add(new DataColumn("ShipmentMode"));

                    dt_2.Columns.Add(new DataColumn("StyleNo"));
                    dt_2.Columns.Add(new DataColumn("Description"));

                    dt_2.Columns.Add(new DataColumn("Rate"));
                    dt_2.Columns.Add(new DataColumn("Qty"));
                    dt_2.Columns.Add(new DataColumn("Amount"));
                    dt_2.Columns.Add(new DataColumn("Currency"));


                    foreach (DataRow DR_2 in dsShipmentWiseSummary.Tables[0].Rows)
                    {
                        DataRow[] rows;

                        DataRow DR2 = dt_2.NewRow();

                        DR2["ExcNo"] = DR_2["ExcNo"];
                        DR2["DeliveryDate"] = Convert.ToDateTime(DR_2["DeliveryDate"]).ToString("dd/MM/yyyy");
                        DR2["ShipmentDate"] = Convert.ToDateTime(DR_2["ShipmentDate"]).ToString("dd/MM/yyyy");
                        DR2["ShipmentMode"] = DR_2["ShipmentMode"];

                        rows = dsShipmentWiseSummary_Style.Tables[0].Select("BuyerOrderId='" + DR_2["BuyerOrderId"] + "'");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (i == 0)
                            {
                                DR2["StyleNo"] = rows[i]["StyleNo"].ToString();
                                DR2["Description"] = rows[i]["Description"].ToString();

                                DR2["Rate"] = rows[i]["Rate"].ToString();
                                DR2["Qty"] = rows[i]["Qty"].ToString();
                                DR2["Amount"] = Convert.ToDouble(rows[i]["Qty"].ToString()) * Convert.ToDouble(rows[i]["Rate"].ToString());
                                DR2["Currency"] = DR_2["CurrencyName"].ToString();
                                dt_2.Rows.Add(DR2);
                            }
                            else
                            {
                                DataRow DR4 = dt_2.NewRow();
                                DR4["ExcNo"] = "";
                                DR4["ShipmentDate"] = "";
                                DR4["ShipmentMode"] = "";

                                DR4["StyleNo"] = rows[i]["StyleNo"].ToString();
                                DR4["Description"] = rows[i]["Description"].ToString();

                                DR4["Rate"] = rows[i]["Rate"].ToString();
                                DR4["Qty"] = rows[i]["Qty"].ToString();
                                DR4["Amount"] = Convert.ToDouble(rows[i]["Qty"].ToString()) * Convert.ToDouble(rows[i]["Rate"].ToString());
                                DR4["Currency"] = DR_2["CurrencyName"].ToString();
                                dt_2.Rows.Add(DR4);
                            }
                        }


                        DataRow DR5 = dt_2.NewRow();

                        DR5["StyleNo"] = "";
                        DR5["Description"] = "";
                        DR5["Rate"] = "";
                        DR5["Currency"] = "";

                        dt_2.Rows.Add(DR5);

                    }

                    #endregion

                    gvBuyerOrder.ShowHeader = true;
                    gvBuyerOrder.Caption = "Shipment Wise Details";
                    gvBuyerOrder.DataSource = dt_2;
                    gvBuyerOrder.DataBind();
                }
                else
                {
                    gvBuyerOrder.DataSource = null;
                    gvBuyerOrder.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "5")//Exc. Wise Rate of Style 
            {
                #region

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string BuyerOrderId = "";
                string IsFirst = "Yes";
                foreach (ListItem listItem in chkExcNo.Items)
                {
                    if (chkExcNo.SelectedIndex < 0)
                    {
                        if (IsFirst == "Yes")
                        {
                            BuyerOrderId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                BuyerOrderId = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                            }
                        }
                    }
                }

                DataSet dsShipmentWiseSummary_Style = objBs.BuyerOrder_ExcWiseRateofStyle_Style_Report(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsExcWiseRateofStyle = objBs.BuyerOrder_ExcWiseRateofStyle_Report(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (dsExcWiseRateofStyle.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt_2 = new DataTable();
                    dt_2.Columns.Add(new DataColumn("ExcNo"));

                    dt_2.Columns.Add(new DataColumn("StyleNo"));
                    dt_2.Columns.Add(new DataColumn("Description"));

                    dt_2.Columns.Add(new DataColumn("Rate"));
                    dt_2.Columns.Add(new DataColumn("Currency"));


                    foreach (DataRow DR_2 in dsExcWiseRateofStyle.Tables[0].Rows)
                    {
                        DataRow DR2 = dt_2.NewRow();
                        DR2["ExcNo"] = DR_2["ExcNo"].ToString();

                        DataRow[] rows = dsShipmentWiseSummary_Style.Tables[0].Select("BuyerOrderId='" + DR_2["BuyerOrderId"] + "'");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (i == 0)
                            {
                                DR2["StyleNo"] = rows[i]["StyleNo"].ToString();
                                DR2["Description"] = rows[i]["Description"].ToString();
                                DR2["Rate"] = rows[i]["Rate"].ToString();
                                DR2["Currency"] = rows[i]["CurrencyName"].ToString();
                                dt_2.Rows.Add(DR2);
                            }
                            else
                            {
                                DataRow DR4 = dt_2.NewRow();
                                DR4["ExcNo"] = "";
                                DR4["StyleNo"] = rows[i]["StyleNo"].ToString();
                                DR4["Description"] = rows[i]["Description"].ToString();
                                DR4["Rate"] = rows[i]["Rate"].ToString();
                                DR4["Currency"] = rows[i]["CurrencyName"].ToString();

                                dt_2.Rows.Add(DR4);
                            }

                        }


                        //rows = dsShipmentWiseSummary_Style.Tables[0].Select("BuyerOrderId='" + DR_2["BuyerOrderId"] + "'");
                        //for (int i = 0; i < rows.Length; i++)
                        //{
                        //    DataRow DR4 = dt_2.NewRow();
                        //    DR4["StyleNo"] = rows[i]["StyleNo"].ToString();
                        //    DR4["Description"] = rows[i]["Description"].ToString();
                        //    DR4["Rate"] = rows[i]["Rate"].ToString();
                        //    DR4["Currency"] = rows[i]["CurrencyName"].ToString();

                        //    dt_2.Rows.Add(DR4);
                        //}

                        DataRow DR5 = dt_2.NewRow();

                        DR5["StyleNo"] = "";
                        DR5["Description"] = "";
                        DR5["Rate"] = "";
                        DR5["Currency"] = "";

                        dt_2.Rows.Add(DR5);

                    }

                    #endregion

                    gvBuyerOrder.ShowHeader = true;
                    gvBuyerOrder.Caption = "Exc. Wise Rate of Style";
                    gvBuyerOrder.DataSource = dt_2;
                    gvBuyerOrder.DataBind();
                }
                else
                {
                    gvBuyerOrder.DataSource = null;
                    gvBuyerOrder.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "6")//Style Average Sheet    
            {
                #region

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string BuyerOrderId = "";
                string IsFirst = "Yes";
                foreach (ListItem listItem in chkExcNo.Items)
                {
                    if (chkExcNo.SelectedIndex < 0)
                    {
                        if (IsFirst == "Yes")
                        {
                            BuyerOrderId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                BuyerOrderId = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                            }
                        }
                    }
                }

                DataSet dsShipmentWiseSummary_MainStyle = objBs.BuyerOrder_StyleAverageSheet_MainStyle_Report(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                DataSet dsShipmentWiseSummary_Style = objBs.BuyerOrder_StyleAverageSheet_Style_Report(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsExcWiseRateofStyle = objBs.BuyerOrder_StyleAverageSheet_Report(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (dsExcWiseRateofStyle.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt_2 = new DataTable();

                    dt_2.Columns.Add(new DataColumn("Column1"));
                    dt_2.Columns.Add(new DataColumn("Column2"));
                    dt_2.Columns.Add(new DataColumn("Column3"));
                    dt_2.Columns.Add(new DataColumn("Column4"));
                    dt_2.Columns.Add(new DataColumn("Column5"));

                    foreach (DataRow DR_2 in dsExcWiseRateofStyle.Tables[0].Rows)
                    {
                        DataRow[] rows;

                        DataRow DR1 = dt_2.NewRow();
                        DR1["Column1"] = "Order Code :";
                        DR1["Column2"] = DR_2["ExcNo"].ToString();
                        DR1["Column3"] = "Shipment Date :";
                        DR1["Column4"] = Convert.ToDateTime(DR_2["ShipmentDate"]).ToString("dd/MM/yyyy");
                        dt_2.Rows.Add(DR1);

                        DataRow DR2 = dt_2.NewRow();
                        DR2["Column1"] = "Buyer PoNo :";
                        DR2["Column2"] = DR_2["BuyerPoNo"].ToString();
                        DR2["Column3"] = "Issue Date :";
                        DR2["Column4"] = Convert.ToDateTime(DR_2["ShipmentDate"]).ToString("dd/MM/yyyy");
                        dt_2.Rows.Add(DR2);

                        DataRow DR3 = dt_2.NewRow();
                        DR3["Column1"] = "ItemName :";
                        DR3["Column2"] = DR_2["ItemDescription"].ToString();
                        DR3["Column3"] = "";
                        DR3["Column4"] = "";
                        dt_2.Rows.Add(DR3);


                        DataRow[] rowsM = dsShipmentWiseSummary_MainStyle.Tables[0].Select("BuyerOrderId='" + DR_2["BuyerOrderId"] + "'");
                        for (int i = 0; i < rowsM.Length; i++)
                        {
                            DataRow DR4 = dt_2.NewRow();
                            DR4["Column1"] = "Style No :";
                            DR4["Column2"] = rowsM[i]["StyleNo"].ToString();
                            DR4["Column3"] = "";
                            DR4["Column4"] = "";
                            dt_2.Rows.Add(DR4);
                            DataRow DR5 = dt_2.NewRow();
                            DR5["Column1"] = "Style :";
                            DR5["Column2"] = rowsM[i]["Description"].ToString();
                            DR5["Column3"] = "";
                            DR5["Column4"] = "";
                            dt_2.Rows.Add(DR5);
                            DataRow DR6 = dt_2.NewRow();
                            DR6["Column2"] = "ItemType :";
                            DR6["Column3"] = "ItemName :";
                            DR6["Column4"] = "Samp.Avg :";
                            DR6["Column5"] = "UOM :";
                            dt_2.Rows.Add(DR6);

                            rows = dsShipmentWiseSummary_Style.Tables[0].Select("BuyerOrderId='" + DR_2["BuyerOrderId"] + "' and SamplingCostingId='" + rowsM[i]["SamplingCostingId"].ToString() + "'");
                            for (int j = 0; j < rows.Length; j++)
                            {
                                DataRow DR7 = dt_2.NewRow();
                                DR7["Column2"] = rows[j]["Itemgroupname"].ToString();
                                DR7["Column3"] = rows[j]["Description"].ToString();
                                DR7["Column4"] = rows[j]["Quantity"].ToString();
                                DR7["Column5"] = rows[j]["Units"].ToString();

                                dt_2.Rows.Add(DR7);
                            }
                        }

                        DataRow DR8 = dt_2.NewRow();
                        DR8["Column2"] = "";
                        DR8["Column3"] = "";
                        DR8["Column4"] = "";
                        DR8["Column5"] = "";
                        dt_2.Rows.Add(DR8);
                        DataRow DR9 = dt_2.NewRow();
                        DR9["Column1"] = "--------------------------------------------------------";
                        DR9["Column2"] = "--------------------------------------------------------";
                        DR9["Column3"] = "--------------------------------------------------------";
                        DR9["Column4"] = "--------------------------------------------------------";
                        DR9["Column5"] = "--------------------------------------------------------";
                        dt_2.Rows.Add(DR9);

                    }

                    #endregion


                    gvBuyerOrder.ShowHeader = false;
                    gvBuyerOrder.Caption = "Style Average Sheet";
                    gvBuyerOrder.DataSource = dt_2;
                    gvBuyerOrder.DataBind();
                }
                else
                {
                    gvBuyerOrder.DataSource = null;
                    gvBuyerOrder.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "7")//Order Wise Sketches
            {
                #region

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string BuyerOrderId = "";
                string IsFirst = "Yes";
                foreach (ListItem listItem in chkExcNo.Items)
                {
                    if (chkExcNo.SelectedIndex < 0)
                    {
                        if (IsFirst == "Yes")
                        {
                            BuyerOrderId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                BuyerOrderId = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                            }
                        }
                    }
                }

                DataSet dsOrderWiseSketches = objBs.BuyerOrder_OrderWiseSketches_Report(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (dsOrderWiseSketches.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt_2 = new DataTable();
                    dt_2.Columns.Add(new DataColumn("ExcNo"));
                    dt_2.Columns.Add(new DataColumn("StyleNo"));
                    dt_2.Columns.Add(new DataColumn("Sketch"));

                    foreach (DataRow DR_2 in dsOrderWiseSketches.Tables[0].Rows)
                    {
                        DataRow DR7 = dt_2.NewRow();
                        DR7["ExcNo"] = DR_2["ExcNo"];
                        DR7["StyleNo"] = DR_2["StyleNo"];
                        DR7["Sketch"] = ("http://" + Request.Url.Authority + DR_2["Sketch"].ToString().Replace("~", string.Empty));
                        dt_2.Rows.Add(DR7);

                        DataRow DR5 = dt_2.NewRow();
                        DR5["ExcNo"] = "";
                        DR5["StyleNo"] = "";
                        DR5["Sketch"] = "";
                        dt_2.Rows.Add(DR5);

                    }

                    #endregion

                    gvSketches.ShowHeader = false;
                    gvSketches.Caption = "Order Wise Sketches";
                    gvSketches.DataSource = dt_2;
                    gvSketches.DataBind();
                }
                else
                {
                    gvSketches.DataSource = null;
                    gvSketches.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "8")//Buyer Order Qty
            {
                #region
                string BuyerOrderId = "";
                string IsFirst = "Yes";

                foreach (ListItem listItem in chkExcNo.Items)
                {
                    #region
                    if (chkExcNo.SelectedIndex < 0)
                    {
                        if (IsFirst == "Yes")
                        {
                            BuyerOrderId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                BuyerOrderId = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                            }
                        }
                    }

                    #endregion
                }

                string Remarks = "";
                string CurrencyName = "";
                int TotalQty = 0; double TotalAmt = 0;

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet ds = objBs.getBuyerOrdervaluesExcelAll(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataSet dsSizes = objBs.getBuyerOrderSizesExcelAll(BuyerOrderId);

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Column1"));
                    dt.Columns.Add(new DataColumn("Column2"));
                    dt.Columns.Add(new DataColumn("Column3"));
                    dt.Columns.Add(new DataColumn("Column4"));
                    dt.Columns.Add(new DataColumn("Column5"));

                    foreach (DataRow dr in dsSizes.Tables[0].Rows)
                    {
                        dt.Columns.Add(new DataColumn(dr["Size"].ToString()));
                    }


                    dt.Columns.Add(new DataColumn("Column6"));
                    dt.Columns.Add(new DataColumn("Column7"));
                    dt.Columns.Add(new DataColumn("Column8"));
                    dt.Columns.Add(new DataColumn("Column9"));
                    dt.Columns.Add(new DataColumn("Column10"));
                    dt.Columns.Add(new DataColumn("Column11"));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Remarks = dr["Remarks"].ToString();

                        DataRow dr_export = dt.NewRow();
                        dr_export["Column1"] = "Exc No. :-";
                        dr_export["Column2"] = dr["ExcNo"];

                        dr_export["Column3"] = "Main Fabric :-";
                        dr_export["Column4"] = dr["ItemCode"];

                        dr_export["Column5"] = "";

                        dr_export["Column6"] = "Delivery Date :-";
                        dr_export["Column7"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");

                        dr_export["Column8"] = "Shipment Mode :-";
                        dr_export["Column9"] = dr["ShipmentMode"];
                        dr_export["Column10"] = "";
                        dt.Rows.Add(dr_export);

                        DataRow dr_export1 = dt.NewRow();
                        dr_export1["Column1"] = "Buyer PO No. :-";
                        dr_export1["Column2"] = dr["BuyerPONo"];

                        dr_export1["Column3"] = "";
                        dr_export1["Column4"] = "";

                        dr_export1["Column5"] = "";

                        dr_export1["Column6"] = "Order Date :-";
                        dr_export1["Column7"] = Convert.ToDateTime(dr["OrderDate"]).ToString("dd/MM/yyyy");

                        dr_export1["Column8"] = "";
                        dr_export1["Column9"] = "";
                        dr_export1["Column10"] = "";

                        dt.Rows.Add(dr_export1);



                        DataRow DRE1 = dt.NewRow();
                        DRE1["Column1"] = "";
                        DRE1["Column2"] = "";

                        DRE1["Column3"] = "";
                        DRE1["Column4"] = "";

                        DRE1["Column5"] = "";

                        DRE1["Column6"] = "";
                        DRE1["Column7"] = "";

                        DRE1["Column8"] = "";
                        DRE1["Column9"] = "";
                        DRE1["Column10"] = "";

                        dt.Rows.Add(DRE1);

                        DataRow DRE2 = dt.NewRow();
                        DRE2["Column1"] = "Style No. :-";
                        DRE2["Column2"] = "Description :-";

                        DRE2["Column3"] = "";
                        DRE2["Column4"] = "";

                        DRE2["Column5"] = "Color :-";

                        foreach (DataRow dr1 in dsSizes.Tables[0].Rows)
                        {
                            DRE2[dr1["Size"].ToString()] = "Size :-";
                            break;
                        }

                        DRE2["Column6"] = "";
                        DRE2["Column7"] = "Quantity :-";

                        DRE2["Column8"] = "Rate :-";
                        DRE2["Column9"] = "";
                        DRE2["Column10"] = "Amount :-";

                        dt.Rows.Add(DRE2);


                        DataRow DRSC = dt.NewRow();

                        DRSC["Column1"] = "";
                        DRSC["Column2"] = "";

                        DRSC["Column3"] = "";
                        DRSC["Column4"] = "";

                        DRSC["Column5"] = "";

                        DRSC["Column6"] = "";
                        DRSC["Column7"] = "";

                        foreach (DataRow DRSs in dsSizes.Tables[0].Rows)
                        {
                            string Size = DRSs["Size"].ToString();

                            DRSC[Size] = Size;
                        }

                        DRSC["Column8"] = "";
                        DRSC["Column9"] = "";

                        DRSC["Column10"] = "";

                        dt.Rows.Add(DRSC);

                        int Qty = 0; double Amt = 0;

                        ds = objBs.getTransBuyerOrdervaluesExcel(Convert.ToInt32(dr["BuyerOrderId"]));
                        foreach (DataRow dr2 in ds.Tables[0].Rows)
                        {
                            CurrencyName = dr2["CurrencyName"].ToString();
                            DataRow dr_export2 = dt.NewRow();

                            dr_export2["Column1"] = dr2["StyleNo"];
                            dr_export2["Column2"] = dr2["Description"];

                            dr_export2["Column3"] = "";
                            dr_export2["Column4"] = "";

                            dr_export2["Column5"] = dr2["Color"];

                            dr_export2["Column6"] = "";


                            Qty += Convert.ToInt32(dr2["Qty"]);

                            foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                            {
                                string Size = DRsS["Size"].ToString();
                                string SizeId = DRsS["SizeId"].ToString();

                                DataSet dsQty = objBs.getBuyerOrderRowsExcel(Convert.ToInt32(dr2["BuyerOrderId"]), Convert.ToInt32(dr2["RowId"]), SizeId);
                                if (dsQty.Tables[0].Rows.Count > 0)
                                {
                                    dr_export2[Size] = dsQty.Tables[0].Rows[0]["Qty"].ToString();
                                }
                                else
                                {
                                    dr_export2[Size] = "0";
                                }

                            }


                            dr_export2["Column7"] = dr2["Qty"];
                            dr_export2["Column8"] = dr2["Rate"];
                            dr_export2["Column9"] = "";

                            dr_export2["Column10"] = (Convert.ToDouble(dr2["Qty"]) * Convert.ToDouble(dr2["Rate"])).ToString("f2");
                            Amt += (Convert.ToDouble(dr2["Qty"]) * Convert.ToDouble(dr2["Rate"]));

                            dt.Rows.Add(dr_export2);
                        }

                        DataRow DRE4 = dt.NewRow();
                        DRE4["Column1"] = "";
                        DRE4["Column2"] = "";
                        DRE4["Column3"] = "";
                        DRE4["Column4"] = "Total:-";
                        DRE4["Column5"] = "";
                        DRE4["Column6"] = "";
                        DRE4["Column7"] = Qty;
                        DRE4["Column8"] = "";
                        DRE4["Column9"] = CurrencyName;
                        DRE4["Column10"] = Convert.ToDouble(Amt).ToString("f2");
                        dt.Rows.Add(DRE4);

                        TotalQty += Qty; TotalAmt += Amt;
                        Qty = 0; Amt = 0;

                        //DataSet dsLabels = objBs.getBuyerOrderLabelsvaluesExcel(Convert.ToInt32(dr2["BuyerOrderId"]));
                        //foreach (DataRow Sdr in dsLabels.Tables[0].Rows)
                        //{
                        //    DataRow DRS = dt.NewRow();

                        //    DRS["Column1"] = Sdr["ItemCode"];
                        //    DRS["Column2"] = Sdr["LabelText"];

                        //    DRS["Column3"] = "";
                        //    DRS["Column4"] = "";

                        //    DRS["Column5"] = "";

                        //    DRS["Column6"] = "";
                        //    DRS["Column7"] = "";

                        //    DRS["Column8"] = "";
                        //    DRS["Column9"] = "";
                        //    DRS["Column10"] = "";

                        //    dt.Rows.Add(DRS);
                        //}

                        DataRow DRE6 = dt.NewRow();
                        DRE6["Column1"] = "Remarks:-";
                        DRE6["Column2"] = Remarks;
                        DRE6["Column3"] = "";
                        DRE6["Column4"] = "";
                        DRE6["Column5"] = "";
                        DRE6["Column6"] = "";
                        DRE6["Column7"] = "";
                        DRE6["Column8"] = "";
                        DRE6["Column9"] = "";
                        DRE6["Column10"] = "";
                        dt.Rows.Add(DRE6);

                        DataRow DRE8 = dt.NewRow();
                        DRE8["Column1"] = "";
                        DRE8["Column2"] = "";
                        DRE8["Column3"] = "";
                        DRE8["Column4"] = "";
                        DRE8["Column5"] = "";
                        DRE8["Column6"] = "";
                        DRE8["Column7"] = "";
                        DRE8["Column8"] = "";
                        DRE8["Column9"] = "";
                        DRE8["Column10"] = "";
                        dt.Rows.Add(DRE8);

                        DataSet dsSketches = objBs.BuyerOrderSketches(dr["BuyerOrderId"].ToString());
                        foreach (DataRow DRS in dsSketches.Tables[0].Rows)
                        {
                            DataRow DR7 = dt.NewRow();
                            DR7["Column11"] = ("http://" + Request.Url.Authority + DRS["Sketch"].ToString().Replace("~", string.Empty));
                            dt.Rows.Add(DR7);
                        }
                    }

                    DataRow DRE7 = dt.NewRow();
                    DRE7["Column1"] = "";
                    DRE7["Column2"] = "";
                    DRE7["Column3"] = "";
                    DRE7["Column4"] = "Final Total:-";
                    DRE7["Column5"] = "";
                    DRE7["Column6"] = "";
                    DRE7["Column7"] = TotalQty;
                    DRE7["Column8"] = "";
                    DRE7["Column9"] = "";
                    DRE7["Column10"] = Convert.ToDouble(TotalAmt).ToString("f2");
                    dt.Rows.Add(DRE7);

                    #endregion

                    if (dt.Rows.Count > 0)
                    {
                        gvBuyerOrderQty.ShowHeader = false;
                        gvBuyerOrderQty.Caption = "Buyer Order QtySheet";
                        gvBuyerOrderQty.DataSource = dt;
                        gvBuyerOrderQty.DataBind();
                    }
                    else
                    {
                        gvBuyerOrderQty.DataSource = null;
                        gvBuyerOrderQty.DataBind();
                    }
                }
                else
                {
                    gvBuyerOrderQty.DataSource = null;
                    gvBuyerOrderQty.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "9")//Qty Detailed
            {
                #region
                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string BuyerOrderId = "";
                string IsFirst = "Yes";

                foreach (ListItem listItem in chkExcNo.Items)
                {
                    #region
                    if (chkExcNo.SelectedIndex < 0)
                    {
                        if (IsFirst == "Yes")
                        {
                            BuyerOrderId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                BuyerOrderId = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                            }
                        }
                    }

                    #endregion
                }

                DataSet ds = objBs.getBuyerOrderQty1(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataSet dsQty = objBs.getBuyerOrderQty2(BuyerOrderId);
                    DataSet dsCQty = objBs.getBuyerOrderQty3(BuyerOrderId);

                    #region

                    DataSet dsSizes = objBs.getBuyerOrderSizesExcelAll(BuyerOrderId);

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Month"));
                    dt.Columns.Add(new DataColumn("PONo"));
                    dt.Columns.Add(new DataColumn("ExcNo"));
                    dt.Columns.Add(new DataColumn("Style"));
                    dt.Columns.Add(new DataColumn("Description"));
                    dt.Columns.Add(new DataColumn("Fabric"));
                    dt.Columns.Add(new DataColumn("Color"));
                    foreach (DataRow dr in dsSizes.Tables[0].Rows)
                    {
                        dt.Columns.Add(new DataColumn(dr["Size"].ToString()));
                    }
                    dt.Columns.Add(new DataColumn("Total"));
                    dt.Columns.Add(new DataColumn("Cutting"));



                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int Total = 0;

                        DataRow DRM = dt.NewRow();

                        DRM["Month"] = Convert.ToDateTime(dr["OrderDate"]).ToString("dd/MMM");
                        DRM["PONo"] = dr["BuyerPoNo"];
                        DRM["ExcNo"] = dr["ExcNo"];
                        DRM["Style"] = dr["StyleNo"];
                        DRM["Description"] = dr["Description"];
                        DRM["Fabric"] = dr["ItemCode"];
                        DRM["Color"] = dr["Color"];

                        Total += Convert.ToInt32(dr["Qty"]);

                        foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                        {
                            string Size = DRsS["Size"].ToString();
                            string SizeId = DRsS["SizeId"].ToString();

                            DataRow[] RowsQty = dsQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' and RowId='" + dr["RowId"] + "'  and SizeId='" + SizeId + "' ");
                            if (RowsQty.Length > 0)
                            {
                                DRM[Size] = RowsQty[0]["Qty"];
                            }
                            else
                            {
                                DRM[Size] = "0";
                            }
                        }

                        DRM["Total"] = Total;

                        DataRow[] RowsCQty = dsCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' and RowId='" + dr["RowId"] + "' ");
                        if (RowsCQty.Length > 0)
                        {
                            DRM["Cutting"] = RowsCQty[0]["RecQty"];
                        }
                        else
                        {
                            DRM["Cutting"] = "0";
                        }

                        dt.Rows.Add(DRM);

                        DataRow DRM1 = dt.NewRow();
                        DRM1["PONo"] = "";
                        dt.Rows.Add(DRM1);

                    }



                    #endregion

                    if (dt.Rows.Count > 0)
                    {
                        gvBuyerOrderQty.ShowHeader = true;
                        gvBuyerOrderQty.Caption = "Qty Detailed";
                        gvBuyerOrderQty.DataSource = dt;
                        gvBuyerOrderQty.DataBind();
                    }
                    else
                    {
                        gvBuyerOrderQty.DataSource = null;
                        gvBuyerOrderQty.DataBind();
                    }
                }
                else
                {
                    gvBuyerOrderQty.DataSource = null;
                    gvBuyerOrderQty.DataBind();
                }
                #endregion
            }
            else if (ddlReportType.SelectedValue == "10")//Buyer Wise Order List 
            {
                if (txtYear.Text.Length < 4)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Accounting Year.');", true);
                    return;
                }

                string IsFirst = "Yes";
                string Months = "All";

                foreach (ListItem listItem in chkMonths.Items)
                {
                    #region
                    if (chkMonths.SelectedIndex >= 0)
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                Months = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                Months = Months + "," + listItem.Value;
                            }
                        }
                    }
                    #endregion
                }

                #region

                DateTime FromYear = DateTime.ParseExact("01/04/" + txtYear.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToYear = DateTime.ParseExact("31/03/" + txtYear.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1);

                DataSet dsBuyerWiseOrderList = objBs.BuyerOrder_BuyerWiseOrderList_Report1(FromYear, ToYear, ddlBuyerCode.SelectedValue, Months);

                DataSet dsMonthWiseOrderList_Style = objBs.BuyerOrder_MonthWiseOrderList_Style_Report(FromYear, ToYear, ddlBuyerCode.SelectedValue, Months);

                DataSet dsMonthWiseOrderList = objBs.BuyerOrder_MonthWiseOrderList_Report1(FromYear, ToYear, ddlBuyerCode.SelectedValue, Months);
                if (dsMonthWiseOrderList.Tables[0].Rows.Count > 0)
                {
                    #region
                    int SNo = 1;
                    DataTable dt_2 = new DataTable();
                    dt_2.Columns.Add(new DataColumn("SNo"));
                    dt_2.Columns.Add(new DataColumn("Buyer"));
                    dt_2.Columns.Add(new DataColumn("ExcNo"));
                    dt_2.Columns.Add(new DataColumn("BuyerPoNo"));
                    dt_2.Columns.Add(new DataColumn("OrderDate"));
                    dt_2.Columns.Add(new DataColumn("ShipmentDate"));
                    dt_2.Columns.Add(new DataColumn("ShipmentMode"));
                    dt_2.Columns.Add(new DataColumn("Qty"));
                    dt_2.Columns.Add(new DataColumn("Amount"));
                    dt_2.Columns.Add(new DataColumn("CurrencyName"));
                    dt_2.Columns.Add(new DataColumn("StyleNo"));


                    foreach (DataRow DR_1 in dsBuyerWiseOrderList.Tables[0].Rows)
                    {
                        int TtlQty = 0; double TtlAmount = 0;

                        DataRow[] RowsBuyer = dsMonthWiseOrderList.Tables[0].Select("LedgerID='" + DR_1["LedgerID"] + "'");
                        for (int R = 0; R < RowsBuyer.Length; R++)
                        {
                            DataRow DR2 = dt_2.NewRow();
                            DR2["SNo"] = SNo++;
                            DR2["OrderDate"] = Convert.ToDateTime(RowsBuyer[R]["OrderDate"]).ToString("dd/MM/yyyy");
                            DR2["Buyer"] = RowsBuyer[R]["CompanyCode"];
                            DR2["ExcNo"] = RowsBuyer[R]["ExcNo"];
                            DR2["BuyerPoNo"] = RowsBuyer[R]["BuyerPoNo"];
                            DR2["ShipmentDate"] = Convert.ToDateTime(RowsBuyer[R]["ShipmentDate"]).ToString("dd/MM/yyyy");
                            DR2["ShipmentMode"] = RowsBuyer[R]["ShipmentMode"];
                            DR2["Qty"] = RowsBuyer[R]["Qty"];
                            DR2["Amount"] = RowsBuyer[R]["Amount"];
                            DR2["CurrencyName"] = RowsBuyer[R]["CurrencyName"];

                            TtlQty += Convert.ToInt32(RowsBuyer[R]["Qty"]);
                            TtlAmount += Convert.ToDouble(RowsBuyer[R]["Amount"]);

                            string StyleNo = "";
                            DataRow[] rows = dsMonthWiseOrderList_Style.Tables[0].Select("BuyerOrderId='" + RowsBuyer[R]["BuyerOrderId"] + "'");
                            for (int i = 0; i < rows.Length; i++)
                            {
                                if (i == 0)
                                {
                                    StyleNo = rows[i]["StyleNo"].ToString();
                                }
                                else
                                {
                                    StyleNo = StyleNo + " / " + rows[i]["StyleNo"].ToString();
                                }
                            }

                            DR2["StyleNo"] = StyleNo;

                            dt_2.Rows.Add(DR2);
                        }

                        DataRow DR3 = dt_2.NewRow();
                        DR3["ShipmentMode"] = "Total :";
                        DR3["Qty"] = TtlQty;
                        DR3["Amount"] = TtlAmount;
                        dt_2.Rows.Add(DR3);

                    }

                    #endregion

                    //gvBuyerOrder.Columns[7].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    //gvBuyerOrder.Columns[8].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    gvBuyerOrder.ShowHeader = true;
                    gvBuyerOrder.Caption = "Buyer Wise Order List";
                    gvBuyerOrder.DataSource = dt_2;
                    gvBuyerOrder.DataBind();
                }
                else
                {
                    gvBuyerOrder.DataSource = null;
                    gvBuyerOrder.DataBind();
                }

                #endregion
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= BuyerOrderReport.xls");
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

        public void ExportToExcel(string filename, DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.Caption = "Buyer Order Details";
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.ShowHeader = false;
                dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.HeaderStyle.Font.Bold = true;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }

    }
}


