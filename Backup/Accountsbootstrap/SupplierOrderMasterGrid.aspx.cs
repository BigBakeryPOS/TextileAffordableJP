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
using System.IO;


namespace Billing.Accountsbootstrap
{
    public partial class SupplierOrderMasterGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string IsSuperAdmin = "";

        double Totamnt = 0, cnt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                lblUser.Text = Session["UserName"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            IsSuperAdmin = Session["IsSuperAdmin"].ToString();

            if (!IsPostBack)
            {
                DataSet dsyear = objBs.GetYear();
                if (dsyear.Tables[0].Rows.Count > 0)
                {
                    drpyear.DataSource = dsyear.Tables[0];
                    drpyear.DataTextField = "yearname";
                    drpyear.DataValueField = "yearname";
                    drpyear.DataBind();
                }


                DataSet ds = objBs.GetBuyerOrderData(drpyear.SelectedValue,"1");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataSet dsMCQty = objBs.GetBuyerOrderData1(drpyear.SelectedValue);
                    DataSet dsShippedQty = objBs.GetBuyerOrderShippedQty(drpyear.SelectedValue);

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("OrderType"));
                    dt.Columns.Add(new DataColumn("LedgerName"));
                    dt.Columns.Add(new DataColumn("CompanyCode"));
                    dt.Columns.Add(new DataColumn("ExcNo"));
                    dt.Columns.Add(new DataColumn("BuyerPoNo"));
                    dt.Columns.Add(new DataColumn("ItemCode"));
                    dt.Columns.Add(new DataColumn("OrderDate"));
                    dt.Columns.Add(new DataColumn("ShipmentDate"));
                    dt.Columns.Add(new DataColumn("DeliveryDate"));
                    dt.Columns.Add(new DataColumn("BQty"));
                    dt.Columns.Add(new DataColumn("CQty"));
                    dt.Columns.Add(new DataColumn("Amount"));
                    dt.Columns.Add(new DataColumn("currencyname"));
                    dt.Columns.Add(new DataColumn("IschangedDeliveryDate"));
                    dt.Columns.Add(new DataColumn("BuyerOrderId"));
                    dt.Columns.Add(new DataColumn("MQty"));
                    dt.Columns.Add(new DataColumn("ShippedQty"));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow[] RowsMCQty = dsMCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' ");
                        DataRow[] RowsShippedQty = dsShippedQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' ");

                        DataRow DR = dt.NewRow();
                        DR["OrderType"] = dr["OrderType"];
                        DR["LedgerName"] = dr["LedgerName"];
                        DR["CompanyCode"] = dr["CompanyCode"];
                        DR["ExcNo"] = dr["ExcNo"];
                        DR["BuyerPoNo"] = dr["BuyerPoNo"];
                        DR["ItemCode"] = dr["ItemCode"];
                        DR["OrderDate"] = Convert.ToDateTime(dr["OrderDate"]).ToString("dd/MM/yyyy");
                        DR["ShipmentDate"] = Convert.ToDateTime(dr["ShipmentDate"]).ToString("dd/MM/yyyy");
                        DR["DeliveryDate"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");
                        DR["BQty"] = dr["BQty"];
                        DR["CQty"] = dr["CQty"];
                        DR["Amount"] = Convert.ToDouble(dr["Amount"]).ToString("f2");
                        DR["currencyname"] = dr["currencyname"];
                        DR["IschangedDeliveryDate"] = dr["IschangedDeliveryDate"];
                        DR["BuyerOrderId"] = dr["BuyerOrderId"];

                        if (RowsMCQty.Length > 0)
                        {
                            DR["MQty"] = RowsMCQty[0]["Qty"];
                        }
                        else
                        {
                            DR["MQty"] = "0";
                        }

                        if (RowsShippedQty.Length > 0)
                        {
                            DR["ShippedQty"] = RowsShippedQty[0]["Qty"];
                        }
                        else
                        {
                            DR["ShippedQty"] = "0";
                        }

                        dt.Rows.Add(DR);

                    }

                    #endregion

                    gvBuyerOrder.DataSource = dt;
                    gvBuyerOrder.DataBind();
                }

                else
                {
                    gvBuyerOrder.DataSource = null;
                    gvBuyerOrder.DataBind();
                }


                DataSet dss = objBs.GetBuyerOrderData_summary(drpyear.SelectedValue, "1");
                if (dss.Tables[0].Rows.Count > 0)
                {

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add(new DataColumn("currencyname"));
                    dt1.Columns.Add(new DataColumn("cnt"));
                    dt1.Columns.Add(new DataColumn("amnt"));
                    dt1.Columns.Add(new DataColumn("Bqty"));

                    foreach (DataRow dr in dss.Tables[0].Rows)
                    {
                        DataRow DR = dt1.NewRow();
                        DR["currencyname"] = dr["currencyname"];
                        DR["cnt"] = dr["cnt"];
                        DR["amnt"] = dr["amnt"];

                        DataSet dss1 = objBs.GetBuyerOrderData_summarycount(drpyear.SelectedValue, "1", dr["currencyid"].ToString());
                        if (dss1.Tables[0].Rows.Count > 0)
                        {
                            DR["Bqty"] = dss1.Tables[0].Rows[0]["Bqty"].ToString();
                        }
                        else
                        {
                            DR["Bqty"] = "0";
                        }

                        dt1.Rows.Add(DR);
                    }

                    gridsummary.DataSource = dt1;
                    gridsummary.DataBind();
                }
                else
                {
                    gridsummary.DataSource = null;
                    gridsummary.DataBind();
                }
            }
        }

        protected void Year_selected(object sender, EventArgs e)
        {
            DataSet ds = objBs.GetBuyerOrderData(drpyear.SelectedValue, "1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region

                DataSet dsMCQty = objBs.GetBuyerOrderData1(drpyear.SelectedValue);
                DataSet dsShippedQty = objBs.GetBuyerOrderShippedQty(drpyear.SelectedValue);

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("OrderType"));
                dt.Columns.Add(new DataColumn("LedgerName"));
                dt.Columns.Add(new DataColumn("CompanyCode"));
                dt.Columns.Add(new DataColumn("ExcNo"));
                dt.Columns.Add(new DataColumn("BuyerPoNo"));
                dt.Columns.Add(new DataColumn("ItemCode"));
                dt.Columns.Add(new DataColumn("OrderDate"));
                dt.Columns.Add(new DataColumn("ShipmentDate"));
                dt.Columns.Add(new DataColumn("DeliveryDate"));
                dt.Columns.Add(new DataColumn("BQty"));
                dt.Columns.Add(new DataColumn("CQty"));
                dt.Columns.Add(new DataColumn("Amount"));
                dt.Columns.Add(new DataColumn("currencyname"));
                dt.Columns.Add(new DataColumn("IschangedDeliveryDate"));
                dt.Columns.Add(new DataColumn("BuyerOrderId"));
                dt.Columns.Add(new DataColumn("MQty"));
                dt.Columns.Add(new DataColumn("ShippedQty"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow[] RowsMCQty = dsMCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' ");
                    DataRow[] RowsShippedQty = dsShippedQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' ");

                    DataRow DR = dt.NewRow();
                    DR["OrderType"] = dr["OrderType"];
                    DR["LedgerName"] = dr["LedgerName"];
                    DR["CompanyCode"] = dr["CompanyCode"];
                    DR["ExcNo"] = dr["ExcNo"];
                    DR["BuyerPoNo"] = dr["BuyerPoNo"];
                    DR["ItemCode"] = dr["ItemCode"];
                    DR["OrderDate"] = Convert.ToDateTime(dr["OrderDate"]).ToString("dd/MM/yyyy");
                    DR["ShipmentDate"] = Convert.ToDateTime(dr["ShipmentDate"]).ToString("dd/MM/yyyy");
                    DR["DeliveryDate"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");
                    DR["BQty"] = dr["BQty"];
                    DR["CQty"] = dr["CQty"];
                    DR["Amount"] = Convert.ToDouble(dr["Amount"]).ToString("f2");
                    DR["currencyname"] = dr["currencyname"];
                    DR["IschangedDeliveryDate"] = dr["IschangedDeliveryDate"];
                    DR["BuyerOrderId"] = dr["BuyerOrderId"];

                    if (RowsMCQty.Length > 0)
                    {
                        DR["MQty"] = RowsMCQty[0]["Qty"];
                    }
                    else
                    {
                        DR["MQty"] = "0";
                    }
                    if (RowsShippedQty.Length > 0)
                    {
                        DR["ShippedQty"] = RowsShippedQty[0]["Qty"];
                    }
                    else
                    {
                        DR["ShippedQty"] = "0";
                    }

                    dt.Rows.Add(DR);

                }

                #endregion

                gvBuyerOrder.DataSource = dt;
                gvBuyerOrder.DataBind();
            }
            else
            {
                gvBuyerOrder.DataSource = null;
                gvBuyerOrder.DataBind();
            }

            DataSet dss = objBs.GetBuyerOrderData_summary(drpyear.SelectedValue, "1");
            if (dss.Tables[0].Rows.Count > 0)
            {

                DataTable dt1 = new DataTable();
                dt1.Columns.Add(new DataColumn("currencyname"));
                dt1.Columns.Add(new DataColumn("cnt"));
                dt1.Columns.Add(new DataColumn("amnt"));
                dt1.Columns.Add(new DataColumn("Bqty"));

                foreach (DataRow dr in dss.Tables[0].Rows)
                {
                    DataRow DR = dt1.NewRow();
                    DR["currencyname"] = dr["currencyname"];
                    DR["cnt"] = dr["cnt"];
                    DR["amnt"] = dr["amnt"];

                    DataSet dss1 = objBs.GetBuyerOrderData_summarycount(drpyear.SelectedValue, "1", dr["currencyid"].ToString());
                    if (dss1.Tables[0].Rows.Count > 0)
                    {
                        DR["Bqty"] = dss1.Tables[0].Rows[0]["Bqty"].ToString();
                    }
                    else
                    {
                        DR["Bqty"] = "0";
                    }

                    dt1.Rows.Add(DR);
                }

                gridsummary.DataSource = dt1;
                gridsummary.DataBind();
            }
            else
            {
                gridsummary.DataSource = null;
                gridsummary.DataBind();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {

            string button = string.Empty;
            button = btnadd.Text;
            {
                button = btnadd.Text;
                Response.Redirect("SupplierOrderMaster.aspx");
            }
            //  Response.Redirect("../Accountsbootstrap/customermaster.aspx");

        }

        protected void gvBuyerOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("SupplierOrderMaster.aspx?BuyerOrderId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "ExportExcel")
            {
                #region

                string Remarks = "";
                string CurrencyName = "";
                int TotalQty = 0; double TotalAmt = 0;

                HtmlForm form = new HtmlForm();
                Response.Clear();
                Response.Buffer = true;
                string filename = "BuyerOrderDetails_" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + ".xls";

                DataSet ds = objBs.getBuyerOrdervaluesExcel1(Convert.ToInt32(e.CommandArgument.ToString()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //DataSet dsRange = objBs.getBuyerOrderSizesRangeExcel(Convert.ToInt32(e.CommandArgument.ToString()));
                    //DataSet dsTransRange = objBs.getTransRangeSize(Convert.ToInt32(dsRange.Tables[0].Rows[0]["RangeId"].ToString()));

                    DataSet dsSizes = objBs.getBuyerOrderSizesExcel(Convert.ToInt32(e.CommandArgument.ToString()));

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Column1"));
                    dt.Columns.Add(new DataColumn("Column2"));
                    //  dt.Columns.Add(new DataColumn("Column3"));
                    dt.Columns.Add(new DataColumn("Column4"));
                    //  dt.Columns.Add(new DataColumn("Column5"));

                    foreach (DataRow dr in dsSizes.Tables[0].Rows)
                    {
                        dt.Columns.Add(new DataColumn(dr["Size"].ToString()));
                    }


                    dt.Columns.Add(new DataColumn("Column6"));
                    dt.Columns.Add(new DataColumn("Column7"));
                    dt.Columns.Add(new DataColumn("Column8"));
                    dt.Columns.Add(new DataColumn("Column9"));
                    dt.Columns.Add(new DataColumn("Column10"));

                    #region

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Remarks = dr["Remarks"].ToString();

                        DataRow dr_export = dt.NewRow();

                        dr_export["Column1"] = "Exc No. :";
                        dr_export["Column2"] = dr["ExcNo"];

                        //   dr_export["Column3"] = "Main Fabric :";
                        dr_export["Column4"] = "Main Fabric :" + dr["Description"];

                        //dr_export["Column5"] = "";

                        dr_export["Column6"] = "Delivery Date :";
                        dr_export["Column7"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");

                        dr_export["Column8"] = "Shipment Mode :";
                        dr_export["Column9"] = dr["ShipmentMode"];
                        dr_export["Column10"] = "";

                        dt.Rows.Add(dr_export);
                    }
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();

                        dr_export["Column1"] = "Buyer PO No. :";
                        dr_export["Column2"] = dr["BuyerPONo"];

                        // dr_export["Column3"] = "";
                        dr_export["Column4"] = "";

                        //dr_export["Column5"] = "";

                        dr_export["Column6"] = "Order Date :";
                        dr_export["Column7"] = Convert.ToDateTime(dr["OrderDate"]).ToString("dd/MM/yyyy");

                        dr_export["Column8"] = "";
                        dr_export["Column9"] = "";
                        dr_export["Column10"] = "";

                        dt.Rows.Add(dr_export);
                    }


                    DataRow DRE1 = dt.NewRow();
                    DRE1["Column1"] = "";
                    DRE1["Column2"] = "";

                    // DRE1["Column3"] = "";
                    DRE1["Column4"] = "";

                    //DRE1["Column5"] = "";

                    DRE1["Column6"] = "";
                    DRE1["Column7"] = "";

                    DRE1["Column8"] = "";
                    DRE1["Column9"] = "";
                    DRE1["Column10"] = "";

                    dt.Rows.Add(DRE1);

                    DataRow DRE2 = dt.NewRow();
                    DRE2["Column1"] = "Style No. :";
                    DRE2["Column2"] = "Description :";

                    //   DRE2["Column3"] = "";
                    DRE2["Column4"] = "Color :";

                    //DRE2["Column5"] = "Color :";

                    foreach (DataRow dr in dsSizes.Tables[0].Rows)
                    {
                        DRE2[dr["Size"].ToString()] = "";
                        break;
                    }

                    DRE2["Column6"] = "";
                    DRE2["Column7"] = "Quantity :";

                    DRE2["Column8"] = "Rate :";
                    DRE2["Column9"] = "";
                    DRE2["Column10"] = "Amount :";

                    dt.Rows.Add(DRE2);


                    DataRow DRSC = dt.NewRow();

                    DRSC["Column1"] = "";
                    DRSC["Column2"] = "";

                    //   DRSC["Column3"] = "";
                    DRSC["Column4"] = "";

                    //      DRSC["Column5"] = "";

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


                    ds = objBs.getTransBuyerOrdervaluesExcel(Convert.ToInt32(e.CommandArgument.ToString()));
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        CurrencyName = dr["CurrencyName"].ToString();
                        DataRow dr_export = dt.NewRow();

                        dr_export["Column1"] = dr["StyleNo"];
                        dr_export["Column2"] = dr["Description"];

                        //      dr_export["Column3"] = "";
                        dr_export["Column4"] = dr["Color"];

                        //   dr_export["Column5"] = dr["Color"];

                        dr_export["Column6"] = "";


                        TotalQty += Convert.ToInt32(dr["Qty"]);

                        foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                        {
                            string Size = DRsS["Size"].ToString();
                            string SizeId = DRsS["SizeId"].ToString();

                            DataSet dsQty = objBs.getBuyerOrderRowsExcel(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(dr["RowId"]), SizeId);
                            if (dsQty.Tables[0].Rows.Count > 0)
                            {
                                dr_export[Size] = dsQty.Tables[0].Rows[0]["Qty"].ToString();
                            }
                            else
                            {
                                dr_export[Size] = "0";
                            }

                        }


                        dr_export["Column7"] = dr["Qty"];
                        dr_export["Column8"] = Convert.ToDouble(dr["Rate"]).ToString("f2");
                        dr_export["Column9"] = CurrencyName;

                        dr_export["Column10"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"])).ToString("f2");
                        TotalAmt += (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));

                        dt.Rows.Add(dr_export);

                    }

                    DataRow DRE3 = dt.NewRow();
                    DRE3["Column1"] = "";
                    DRE3["Column2"] = "";

                    //   DRE3["Column3"] = "";
                    DRE3["Column4"] = "";

                    //DRE3["Column5"] = "";

                    DRE3["Column6"] = "";
                    DRE3["Column7"] = "";

                    DRE3["Column8"] = "";
                    DRE3["Column9"] = "";
                    DRE3["Column10"] = "";

                    dt.Rows.Add(DRE3);





                    DataSet dsLabels = objBs.getBuyerOrderLabelsvalues_Excel_New(Convert.ToInt32(e.CommandArgument.ToString()));
                    foreach (DataRow Sdr in dsLabels.Tables[0].Rows)
                    {
                        DataRow DRS = dt.NewRow();

                        DRS["Column1"] = Sdr["Itemdescription"];
                        DRS["Column2"] = Sdr["LabelText"];

                        //       DRS["Column3"] = "";
                        DRS["Column4"] = "";

                        //DRS["Column5"] = "";

                        DRS["Column6"] = "";
                        DRS["Column7"] = "";

                        DRS["Column8"] = "";
                        DRS["Column9"] = "";
                        DRS["Column10"] = "";

                        dt.Rows.Add(DRS);
                    }

                    DataRow DRE5 = dt.NewRow();
                    DRE5["Column1"] = "";
                    DRE5["Column2"] = "";

                    //   DRE5["Column3"] = "";
                    DRE5["Column4"] = "";

                    //DRE5["Column5"] = "";

                    DRE5["Column6"] = "";
                    DRE5["Column7"] = "";

                    DRE5["Column8"] = "";
                    DRE5["Column9"] = "";
                    DRE5["Column10"] = "";

                    dt.Rows.Add(DRE5);

                    DataRow DRE4 = dt.NewRow();
                    DRE4["Column1"] = "";
                    DRE4["Column2"] = "";

                    //    DRE4["Column3"] = "";
                    DRE4["Column4"] = "Total:";

                    //DRE4["Column5"] = "Total:";

                    DRE4["Column6"] = "";
                    DRE4["Column7"] = TotalQty;

                    DRE4["Column8"] = "";
                    DRE4["Column9"] = CurrencyName;
                    DRE4["Column10"] = TotalAmt;

                    dt.Rows.Add(DRE4);

                    DataRow DRE6 = dt.NewRow();
                    DRE6["Column1"] = "Remarks:";
                    DRE6["Column2"] = Remarks;

                    //    DRE6["Column3"] = "";
                    DRE6["Column4"] = "";

                    //DRE6["Column5"] = "";

                    DRE6["Column6"] = "";
                    DRE6["Column7"] = "";

                    DRE6["Column8"] = "";
                    DRE6["Column9"] = "";
                    DRE6["Column10"] = "";

                    dt.Rows.Add(DRE6);

                    #endregion

                    DataTable dt_S = new DataTable();
                    dt_S.Columns.Add(new DataColumn("Sketch"));

                    #region  Sketch

                    DataSet dsSketches = objBs.BuyerOrderSketches(e.CommandArgument.ToString());
                    foreach (DataRow DR_S in dsSketches.Tables[0].Rows)
                    {
                        DataRow DR_S1 = dt_S.NewRow();
                        DR_S1["Sketch"] = ("http://" + Request.Url.Authority + DR_S["Sketch"].ToString().Replace("~", string.Empty));
                        dt_S.Rows.Add(DR_S1);
                    }

                    #endregion

                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "SupplierOrderMaster.xls"));
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvSketches.AllowPaging = false;

                    gvSketches.DataSource = dt;
                    gvSketches.DataBind();

                    GridView1.DataSource = dt_S;
                    GridView1.DataBind();

                    gvSketches.RenderControl(htw);
                    GridView1.RenderControl(htw);

                    Response.Write(sw.ToString());
                    Response.End();



                }

                #endregion
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    DataSet dsup = objBs.DeleteBOMasterCheck(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (dsup.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot be Delete,Please Check Requirement Order Sheet Details.');", true);
                        return;
                    }
                    else
                    {
                        int iSucess = objBs.DeleteBOMaster(Convert.ToInt32(e.CommandArgument.ToString()));
                        Response.Redirect("BuyerOrderMasterGrid.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Admin.');", true);
                    return;
                }
            }
            else if (e.CommandName == "RateChange")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    hdpopupid.Value = e.CommandArgument.ToString();

                    DataSet ds = objBs.getTransBuyerOrderItemsvalues(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GVItem.DataSource = ds;
                        GVItem.DataBind();
                    }

                    mpecost.Show();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void gvcust_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //Label IschangedDeliveryDate = (Label)gvBuyerOrder.FindControl("IschangedDeliveryDate");
                TableCell cell = e.Row.Cells[8];
                string Stus = e.Row.Cells[10].Text.ToString();
                //TableCell cell = e.Row.Cells[8];

                if (Stus == "Y")
                {
                    cell.ForeColor = System.Drawing.Color.Black;
                    //cell.Font.Size = "20px";
                    cell.BackColor = System.Drawing.Color.Red;
                }

                //if (objBs.CheckIfbrandUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                //{
                //    ((Image)e.Row.FindControl("dlt")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;
                //}

            }
        }

        protected void gridsummary_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    //Label IschangedDeliveryDate = (Label)gvBuyerOrder.FindControl("IschangedDeliveryDate");
            //    TableCell cell = e.Row.Cells[8];
            //    string amnt = e.Row.Cells[1].Text.ToString();
            //    string Billcnt = e.Row.Cells[2].Text.ToString();
            //    //TableCell cell = e.Row.Cells[8];

            //    Totamnt +=  Convert.ToDouble(amnt);

            //    cnt += Convert.ToDouble(Billcnt);

            //    //if (objBs.CheckIfbrandUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
            //    //{
            //    //    ((Image)e.Row.FindControl("dlt")).Visible = false;
            //    //    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;
            //    //}

            //}

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[0].Text = "Total :";

            //}
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdTransBuyerOrderId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransBuyerOrderId");
                TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");

                if (txtRate.Text == "")
                    txtRate.Text = "0";

                if (Convert.ToDouble(txtRate.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Rate in Row " + (vLoop + 1) + ".')", true);
                    txtRate.Focus();
                    mpecost.Show();
                    return;
                }
            }

            double TtlAmount = 0;
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdTransBuyerOrderId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransBuyerOrderId");
                TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");

                if (txtRate.Text == "")
                    txtRate.Text = "0";

                int Update = objBs.UpdateBuyerOrderItemsRate(hdTransBuyerOrderId.Value, Convert.ToDouble(txtRate.Text));

                TtlAmount += (Convert.ToDouble(hdQty.Value) * Convert.ToDouble(txtRate.Text));
            }

            int Update1 = objBs.UpdateBuyerOrder(hdpopupid.Value, TtlAmount);

            Response.Redirect("BuyerOrderMasterGrid.aspx");
        }
        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderMasterGrid.aspx");
        }
    }
}


