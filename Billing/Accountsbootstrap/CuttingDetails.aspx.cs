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
    public partial class CuttingDetails : System.Web.UI.Page
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
                DataSet dsExcNo = objBs.getAllExcNo("All");
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }

            }
        }


        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Cuttingdetailsnew.aspx");
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "Select ExcNo")
            {
                DataSet ds = objBs.getBuyerOrdervaluesExcel(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add(new DataColumn("Column1"));
                    dt1.Columns.Add(new DataColumn("Column2"));
                    dt1.Columns.Add(new DataColumn("Column3"));
                    dt1.Columns.Add(new DataColumn("Column4"));
                    dt1.Columns.Add(new DataColumn("Column5"));
                    dt1.Columns.Add(new DataColumn("Column6"));

                    DataRow DR1 = dt1.NewRow();
                    DR1["Column1"] = "ExcNo.:";
                    DR1["Column2"] = ds.Tables[0].Rows[0]["ExcNo"].ToString();
                    DR1["Column3"] = "Main Fabric :";
                    DR1["Column4"] = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                    DR1["Column5"] = "Delivery Date:";
                    DR1["Column6"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["DeliveryDate"]).ToString("dd/MM/yyyy");
                    dt1.Rows.Add(DR1);

                    DataRow DR2 = dt1.NewRow();
                    DR2["Column1"] = "Buyer PONo.:";
                    DR2["Column2"] = ds.Tables[0].Rows[0]["BuyerPONo"].ToString();
                    DR2["Column3"] = "Shipment Mode :";
                    DR2["Column4"] = ds.Tables[0].Rows[0]["ShipmentMode"].ToString();
                    DR2["Column5"] = "Order Date:";
                    DR2["Column6"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                    dt1.Rows.Add(DR2);

                    #endregion
                    gvCuttingDetails1.DataSource = dt1;
                    gvCuttingDetails1.DataBind();
                }


                DataSet dsBOSketch = objBs.BuyerOrderSketches(ddlExcNo.SelectedValue);
                if (dsBOSketch.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable DTS = new DataTable();
                    DTS.Columns.Add(new DataColumn("Sketch1"));
                    DTS.Columns.Add(new DataColumn("Sketch2"));
                    DTS.Columns.Add(new DataColumn("Sketch3"));
                    DTS.Columns.Add(new DataColumn("Sketch4"));
                    DTS.Columns.Add(new DataColumn("Sketch5"));
                    DTS.Columns.Add(new DataColumn("Sketch6"));
                    DTS.Columns.Add(new DataColumn("Sketch7"));

                    DataRow DR1 = DTS.NewRow();

                    if (dsBOSketch.Tables[0].Rows.Count >= 1)
                    {
                        DR1["Sketch1"] = dsBOSketch.Tables[0].Rows[0]["Sketch"].ToString();
                    }
                    if (dsBOSketch.Tables[0].Rows.Count >= 2)
                    {
                        DR1["Sketch2"] = dsBOSketch.Tables[0].Rows[1]["Sketch"].ToString();
                    }
                    if (dsBOSketch.Tables[0].Rows.Count >= 3)
                    {
                        DR1["Sketch3"] = dsBOSketch.Tables[0].Rows[2]["Sketch"].ToString();
                    }
                    if (dsBOSketch.Tables[0].Rows.Count >= 4)
                    {
                        DR1["Sketch4"] = dsBOSketch.Tables[0].Rows[3]["Sketch"].ToString();
                    }
                    if (dsBOSketch.Tables[0].Rows.Count >= 5)
                    {
                        DR1["Sketch5"] = dsBOSketch.Tables[0].Rows[4]["Sketch"].ToString();
                    }
                    if (dsBOSketch.Tables[0].Rows.Count >= 6)
                    {
                        DR1["Sketch6"] = dsBOSketch.Tables[0].Rows[5]["Sketch"].ToString();
                    }
                    if (dsBOSketch.Tables[0].Rows.Count >= 7)
                    {
                        DR1["Sketch7"] = dsBOSketch.Tables[0].Rows[6]["Sketch"].ToString();
                    }

                    DTS.Rows.Add(DR1);

                    #endregion
                    gvCuttingImages.DataSource = DTS;
                    gvCuttingImages.DataBind();
                }

                DataSet dsStyle = objBs.getCuttingQty1(ddlExcNo.SelectedValue);
                DataSet dsQty = objBs.getCuttingQty2(ddlExcNo.SelectedValue);
                DataSet dsCQty = objBs.getCuttingQty3(ddlExcNo.SelectedValue);

                #region

                DataSet dsSizes = objBs.getBuyerOrderSizesExcelAll(ddlExcNo.SelectedValue);
                RowCount.Text = (3 + Convert.ToInt32(dsSizes.Tables[0].Rows.Count.ToString())).ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("StyleNo"));
                dt.Columns.Add(new DataColumn("Description"));
                dt.Columns.Add(new DataColumn("Color"));
                foreach (DataRow dr in dsSizes.Tables[0].Rows)
                {
                    dt.Columns.Add(new DataColumn(dr["Size"].ToString()));
                }
                dt.Columns.Add(new DataColumn("Total"));

                foreach (DataRow dr in dsStyle.Tables[0].Rows)
                {
                    int Total = 0;

                    DataRow DRM = dt.NewRow();
                    DRM["StyleNo"] = dr["StyleNo"];
                    DRM["Description"] = dr["Description"];
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
                    dt.Rows.Add(DRM);


                    //Cutting Qty 
                    DataRow DRM1 = dt.NewRow();
                    DRM1["Color"] = "Cutting";

                    Total = 0;

                    foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                    {
                        string Size = DRsS["Size"].ToString();
                        string SizeId = DRsS["SizeId"].ToString();

                        DataRow[] RowsQty = dsCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' and RowId='" + dr["RowId"] + "'  and SizeId='" + SizeId + "' ");
                        if (RowsQty.Length > 0)
                        {
                            DRM1[Size] = RowsQty[0]["RecQty"];
                            Total += Convert.ToInt32(RowsQty[0]["RecQty"]);
                        }
                        else
                        {
                            DRM1[Size] = "0";
                        }
                    }

                    DRM1["Total"] = Total;
                    dt.Rows.Add(DRM1);

                }

                //   gvCuttingDetails2.Rows[0].Cells[0].BackColor = System.Drawing.Color.Crimson;

                //   gvCuttingDetails2.Rows.BackColor = System.Drawing.Color.Crimson;


                //gvCuttingDetails2.Rows[0].Cells[2].Font.Bold = true;
                //gvCuttingDetails2.Rows[0].Cells[2].Font.Size = 10;

                //gvCuttingDetails2.Rows[gvCuttingDetails2.Rows.Count - 1].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //gvCuttingDetails2.Rows[gvCuttingDetails2.Rows.Count - 1].Cells[0].Font.Bold = true;
                //gvCuttingDetails2.Rows[gvCuttingDetails2.Rows.Count - 1].Cells[0].Font.Size = Convert.ToInt32(FontSize.Text);

                #endregion

                if (dt.Rows.Count > 0)
                {
                    gvCuttingDetails2.DataSource = dt;
                    gvCuttingDetails2.DataBind();

                }
                else
                {
                    gvCuttingDetails2.DataSource = null;
                    gvCuttingDetails2.DataBind();
                }


            }
            else
            {
                gvCuttingDetails1.DataSource = null;
                gvCuttingDetails1.DataBind();
            }
        }


        protected void gvCuttingDetails2_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "Cutting")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#d0edef");
                    e.Row.Cells[0].BackColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.White;

                    //e.Row.ForeColor = System.Drawing.Color.Red;
                    //e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;

                    for (int i = 3; i < Convert.ToInt32(RowCount.Text) + 1; i++)
                    {
                        if (e.Row.Cells[i].Text != "0")
                        {
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
    }
}


