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
    public partial class PurchaseGRNGrid : System.Web.UI.Page
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
                txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet dsyear = objBs.GetYear();
                if (dsyear.Tables[0].Rows.Count > 0)
                {
                    drpyear.DataSource = dsyear.Tables[0];
                    drpyear.DataTextField = "yearname";
                    drpyear.DataValueField = "yearname";
                    drpyear.DataBind();
                }

                DataSet ds = objBs.gridPurchaseGRNEntryGrid(drpyear.SelectedValue, From, To);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVItemProcessOrder.DataSource = ds;
                    GVItemProcessOrder.DataBind();
                }

                else
                {
                    GVItemProcessOrder.DataSource = null;
                    GVItemProcessOrder.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseGRN.aspx");
        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectcustomerDet(1, 1);
            }
            else
            {
                ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVItemProcessOrder.DataSource = ds;
                    GVItemProcessOrder.PageIndex = e.NewPageIndex;
                    GVItemProcessOrder.DataBind();
                }

                else
                {
                    GVItemProcessOrder.DataSource = null;
                    GVItemProcessOrder.DataBind();
                }
            }
            else
            {
                GVItemProcessOrder.DataSource = null;
                GVItemProcessOrder.DataBind();
            }

        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet ds = objBs.gridPurchaseGRNEntryGrid(drpyear.SelectedValue, From, To);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVItemProcessOrder.DataSource = ds;
                GVItemProcessOrder.DataBind();
            }

            else
            {
                GVItemProcessOrder.DataSource = null;
                GVItemProcessOrder.DataBind();
            }
        }

        protected void drpyear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            
            DataSet ds = objBs.gridPurchaseGRNEntryGrid(drpyear.SelectedValue, From, To);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVItemProcessOrder.DataSource = ds;
                GVItemProcessOrder.DataBind();
            }

            else
            {
                GVItemProcessOrder.DataSource = null;
                GVItemProcessOrder.DataBind();
            }
        }


        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/BuyerOrderMasterGrid.aspx");

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVItemProcessOrder.DataSource = ds;
                    GVItemProcessOrder.DataBind();
                }
                else
                {
                    GVItemProcessOrder.DataSource = null;
                    GVItemProcessOrder.DataBind();
                }
            }
            else
            {
                GVItemProcessOrder.DataSource = null;
                GVItemProcessOrder.DataBind();
            }
        }

        protected void GVItemProcessOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("PurchaseGRN.aspx?POGRNId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete")
            {
              //  int iSucess = objBs.deletecustomer(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text);
                // Delete PRocess
                {
                    int idletepo = objBs.IdeletePUGRN(e.CommandArgument.ToString());
                    //Response.Redirect("PurchaseOrderGrid.aspx");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase GRN Deleted Successfully.Thank You!!!.')", true);
                    DataSet ds = objBs.gridPurchaseGRN();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GVItemProcessOrder.DataSource = ds;
                        GVItemProcessOrder.DataBind();
                    }

                    else
                    {
                        GVItemProcessOrder.DataSource = null;
                        GVItemProcessOrder.DataBind();
                    }
                }


               // Response.Redirect("purchaseGrnGrid.aspx");
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

                DataSet ds = objBs.getBuyerOrdervaluesExcel(Convert.ToInt32(e.CommandArgument.ToString()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataSet dsSizes = objBs.getBuyerOrderSizesExcel(Convert.ToInt32(e.CommandArgument.ToString()));

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

                    foreach (DataRow dr in dsSizes.Tables[0].Rows)
                    {
                        DRE2[dr["Size"].ToString()] = "Size :-";
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


                    ds = objBs.getTransBuyerOrdervaluesExcel(Convert.ToInt32(e.CommandArgument.ToString()));
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
                        dr_export["Column8"] = dr["Rate"];
                        dr_export["Column9"] = "";

                        dr_export["Column10"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"])).ToString("f2");
                        TotalAmt += (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));

                        dt.Rows.Add(dr_export);

                    }

                    DataRow DRE3 = dt.NewRow();
                    DRE3["Column1"] = "";
                    DRE3["Column2"] = "";

                    DRE3["Column3"] = "";
                    DRE3["Column4"] = "";

                    DRE3["Column5"] = "";

                    DRE3["Column6"] = "";
                    DRE3["Column7"] = "";

                    DRE3["Column8"] = "";
                    DRE3["Column9"] = "";
                    DRE3["Column10"] = "";

                    dt.Rows.Add(DRE3);


                    DataRow DRE4 = dt.NewRow();
                    DRE4["Column1"] = "";
                    DRE4["Column2"] = "";

                    DRE4["Column3"] = "";
                    DRE4["Column4"] = "Total:-";

                    DRE4["Column5"] = "";

                    DRE4["Column6"] = "";
                    DRE4["Column7"] = TotalQty;

                    DRE4["Column8"] = "";
                    DRE4["Column9"] = CurrencyName;
                    DRE4["Column10"] = TotalAmt;

                    dt.Rows.Add(DRE4);


                    DataSet dsLabels = objBs.getBuyerOrderLabelsvaluesExcel(Convert.ToInt32(e.CommandArgument.ToString()));
                    foreach (DataRow Sdr in dsLabels.Tables[0].Rows)
                    {
                        DataRow DRS = dt.NewRow();

                        DRS["Column1"] = Sdr["ItemCode"];
                        DRS["Column2"] = Sdr["LabelText"];

                        DRS["Column3"] = "";
                        DRS["Column4"] = "";

                        DRS["Column5"] = "";

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

                    DRE5["Column3"] = "";
                    DRE5["Column4"] = "";

                    DRE5["Column5"] = "";

                    DRE5["Column6"] = "";
                    DRE5["Column7"] = "";

                    DRE5["Column8"] = "";
                    DRE5["Column9"] = "";
                    DRE5["Column10"] = "";

                    dt.Rows.Add(DRE5);

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

                    ExportToExcel(filename, dt);
                }

                #endregion
            }
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

        protected void GVItemProcessOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objBs.CeckIfVendorUsed("tblDayBook_" + sTableName, int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    //((Image)e.Row.FindControl("img")).Visible = false;
                    //((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;


                    ((Image)e.Row.FindControl("dlt")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = true;
                }

            }
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = Button3.Text;
            {
                button = Button3.Text;
                //Response.Redirect("categorymaster.aspx");
                Response.Redirect("customermaster.aspx?name=" + button.ToString());
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

        }



    }
}


