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
    public partial class CuttingQtyDetails : System.Web.UI.Page
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
                }

            }
        }
        protected void buyer_order(object sender, EventArgs e)
        {
            DataSet dsExcNo = objBs.getAllExcNo(ddlBuyerCode.SelectedValue);
            if (dsExcNo.Tables[0].Rows.Count > 0)
            {
                chkExcNo.DataSource = dsExcNo.Tables[0];
                chkExcNo.DataTextField = "ExcNo";
                chkExcNo.DataValueField = "BuyerOrderId";
                chkExcNo.DataBind();
            }
        }

        protected void ddlReportType_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlReportType.SelectedValue == "1")
            {
                lblDate.Text = "Order Date";
                chkUseDate.Enabled = true;
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                lblDate.Text = "Received Date";
                chkUseDate.Checked = true;
                chkUseDate.Enabled = false;
            }
        }

        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Cuttingdetailsnew.aspx");
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            gvCuttingQtyDetails.DataSource = null;
            gvCuttingQtyDetails.DataBind();

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
            if (ddlReportType.SelectedValue == "1")
            {
                DataSet ds = objBs.getBuyerOrderQty1(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataSet dsCQty = objBs.getCuttingQty3(BuyerOrderId);

                    #region

                    DataSet dsSizes = objBs.getBuyerOrderSizesExcelAll(BuyerOrderId);

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("ExcNo"));
                    dt.Columns.Add(new DataColumn("Style"));
                    dt.Columns.Add(new DataColumn("Description"));
                    dt.Columns.Add(new DataColumn("Color"));
                    dt.Columns.Add(new DataColumn("OrderQty"));
                    foreach (DataRow dr in dsSizes.Tables[0].Rows)
                    {
                        dt.Columns.Add(new DataColumn(dr["Size"].ToString()));
                    }
                    dt.Columns.Add(new DataColumn("Total"));
                    dt.Columns.Add(new DataColumn("Remarks"));


                    int OrderQty = 0; int GrandTotal = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int Total = 0;

                        DataRow DRM = dt.NewRow();

                        DRM["ExcNo"] = dr["ExcNo"];
                        DRM["Style"] = dr["StyleNo"];
                        DRM["Description"] = dr["Description"];
                        DRM["Color"] = dr["Color"];
                        DRM["OrderQty"] = dr["AffectedQty"];
                        OrderQty += Convert.ToInt32(dr["AffectedQty"]);

                        foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                        {
                            string Size = DRsS["Size"].ToString();
                            string SizeId = DRsS["SizeId"].ToString();

                            DataRow[] RowsQty = dsCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' and RowId='" + dr["RowId"] + "'  and SizeId='" + SizeId + "' ");
                            if (RowsQty.Length > 0)
                            {
                                DRM[Size] = RowsQty[0]["RecQty"];
                                Total += Convert.ToInt32(RowsQty[0]["RecQty"]);
                                GrandTotal += Convert.ToInt32(RowsQty[0]["RecQty"]);
                            }
                            else
                            {
                                DRM[Size] = "0";
                            }
                        }

                        DRM["Total"] = Total;

                        if (Convert.ToInt32(dr["AffectedQty"]) - Total > 0)
                        {
                            DRM["Remarks"] = (Convert.ToInt32(dr["AffectedQty"]) - Total);
                        }
                        else
                        {
                            DRM["Remarks"] = "";
                        }

                        dt.Rows.Add(DRM);



                    }

                    DataRow DRM1 = dt.NewRow();
                    DRM1["Color"] = "Total :";
                    DRM1["OrderQty"] = OrderQty;
                    DRM1["Total"] = GrandTotal;
                    dt.Rows.Add(DRM1);


                    #endregion

                    if (dt.Rows.Count > 0)
                    {
                        gvCuttingQtyDetails.ShowHeader = true;
                        gvCuttingQtyDetails.Caption = "Cutting Qty Summary Report";
                        gvCuttingQtyDetails.DataSource = dt;
                        gvCuttingQtyDetails.DataBind();
                    }
                    else
                    {
                        gvCuttingQtyDetails.DataSource = null;
                        gvCuttingQtyDetails.DataBind();
                    }
                }
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                DataSet ds = objBs.getCuttingDetailedReport1(BuyerOrderId, From, To, ddlBuyerCode.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataSet dsCQty = objBs.getCuttingDetailedReport2(From, To);

                    #region

                    DataSet dsSizes = objBs.getBuyerOrderSizesExcelAll(BuyerOrderId);

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Date"));
                    dt.Columns.Add(new DataColumn("ExcNo"));
                    dt.Columns.Add(new DataColumn("Style"));
                    dt.Columns.Add(new DataColumn("Description"));
                    dt.Columns.Add(new DataColumn("Color"));
                  
                    foreach (DataRow dr in dsSizes.Tables[0].Rows)
                    {
                        dt.Columns.Add(new DataColumn(dr["Size"].ToString()));
                    }
                    dt.Columns.Add(new DataColumn("Total"));
                    dt.Columns.Add(new DataColumn("DayQty"));

                    string PrevoiusValue = "";
                    int GrandTotal = 0; int DayQty = 0; 
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int Total = 0;


                        if (PrevoiusValue != "" & PrevoiusValue != Convert.ToDateTime(dr["CuttingReceivedDate"]).ToString("dd/MM/yyyy"))
                        {

                            DataRow DRM1 = dt.NewRow();
                            DRM1["DayQty"] = DayQty;
                            dt.Rows.Add(DRM1);
                            DayQty = 0;

                            DataRow DRM = dt.NewRow();

                            PrevoiusValue = Convert.ToDateTime(dr["CuttingReceivedDate"]).ToString("dd/MM/yyyy");
                            DRM["Date"] = Convert.ToDateTime(dr["CuttingReceivedDate"]).ToString("dd/MM/yyyy");
                            DRM["ExcNo"] = dr["ExcNo"];
                            DRM["Style"] = dr["StyleNo"];
                            DRM["Description"] = dr["Description"];
                            DRM["Color"] = dr["Color"];
                            foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                            {
                                string Size = DRsS["Size"].ToString();
                                string SizeId = DRsS["SizeId"].ToString();

                                DataRow[] RowsQty = dsCQty.Tables[0].Select("BuyerOrderMasterCuttingId='" + dr["BuyerOrderMasterCuttingId"] + "' and RowId='" + dr["RowId"] + "' and SizeId='" + SizeId + "' and CuttingReceivedDate='" + dr["CuttingReceivedDate"] + "'  ");
                                if (RowsQty.Length > 0)
                                {
                                    DRM[Size] = RowsQty[0]["RecQty"];
                                    Total += Convert.ToInt32(RowsQty[0]["RecQty"]);
                                    DayQty += Convert.ToInt32(RowsQty[0]["RecQty"]);
                                    GrandTotal += Convert.ToInt32(RowsQty[0]["RecQty"]);
                                }
                                else
                                {
                                    DRM[Size] = "0";
                                }
                            }
                            DRM["Total"] = Total;
                            dt.Rows.Add(DRM);

                        }
                        else
                        {
                            DataRow DRM = dt.NewRow();

                            PrevoiusValue = Convert.ToDateTime(dr["CuttingReceivedDate"]).ToString("dd/MM/yyyy");
                            DRM["Date"] = Convert.ToDateTime(dr["CuttingReceivedDate"]).ToString("dd/MM/yyyy");
                            DRM["ExcNo"] = dr["ExcNo"];
                            DRM["Style"] = dr["StyleNo"];
                            DRM["Description"] = dr["Description"];
                            DRM["Color"] = dr["Color"];
                            foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                            {
                                string Size = DRsS["Size"].ToString();
                                string SizeId = DRsS["SizeId"].ToString();

                                DataRow[] RowsQty = dsCQty.Tables[0].Select("BuyerOrderMasterCuttingId='" + dr["BuyerOrderMasterCuttingId"] + "' and RowId='" + dr["RowId"] + "' and SizeId='" + SizeId + "' and CuttingReceivedDate='" + dr["CuttingReceivedDate"] + "'  ");
                                if (RowsQty.Length > 0)
                                {
                                    DRM[Size] = RowsQty[0]["RecQty"];
                                    Total += Convert.ToInt32(RowsQty[0]["RecQty"]);
                                    DayQty += Convert.ToInt32(RowsQty[0]["RecQty"]);
                                    GrandTotal += Convert.ToInt32(RowsQty[0]["RecQty"]);
                                }
                                else
                                {
                                    DRM[Size] = "0";
                                }
                            }
                            DRM["Total"] = Total;
                            dt.Rows.Add(DRM);
                        }


                    }

                    DataRow DRM2 = dt.NewRow();
                    DRM2["DayQty"] = DayQty;
                    dt.Rows.Add(DRM2);
                    DayQty = 0;

                    DataRow DRM3 = dt.NewRow();
                    DRM3["Total"] = "Total :";
                    DRM3["DayQty"] = GrandTotal;
                    dt.Rows.Add(DRM3);


                    #endregion

                    if (dt.Rows.Count > 0)
                    {
                        gvCuttingQtyDetails.ShowHeader = true;
                        gvCuttingQtyDetails.Caption = "Cutting Qty Detailed Report";
                        gvCuttingQtyDetails.DataSource = dt;
                        gvCuttingQtyDetails.DataBind();
                    }
                    else
                    {
                        gvCuttingQtyDetails.DataSource = null;
                        gvCuttingQtyDetails.DataBind();
                    }
                }
            }
        }

        protected void gvBuyerOrderBookingSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("USD"));

                gvCuttingQtyDetails.FooterRow.Cells[1].Text = "Total";
                gvCuttingQtyDetails.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvCuttingQtyDetails.FooterRow.Cells[3].Text = total.ToString();

            }

        }

    }
}


