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
    public partial class BuyerOrderDetails : System.Web.UI.Page
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
                DataSet dsRecPONo = objBs.GetBuyerOrderExcNo();
                if (dsRecPONo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsRecPONo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }

            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {

            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "Select ExcNo")
            {
                DataSet dsBODetails = objBs.GetBuyerOrderExcNoDetails(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (dsBODetails.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable DTBO = new DataTable();
                    DTBO.Columns.Add(new DataColumn("BuyerOrderExcNoDetails"));

                    DataRow DR1 = DTBO.NewRow();
                    DR1["BuyerOrderExcNoDetails"] = "ExcNo : " + dsBODetails.Tables[0].Rows[0]["ExcNo"].ToString();
                    DTBO.Rows.Add(DR1);

                    DataRow DR2 = DTBO.NewRow();
                    DR2["BuyerOrderExcNoDetails"] = "BuyerPoNo : " + dsBODetails.Tables[0].Rows[0]["BuyerPoNo"].ToString();
                    DTBO.Rows.Add(DR2);

                    DataRow DR3 = DTBO.NewRow();
                    DR3["BuyerOrderExcNoDetails"] = "OrderDate : " + Convert.ToDateTime(dsBODetails.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                    DTBO.Rows.Add(DR3);

                    DataRow DR4 = DTBO.NewRow();
                    DR4["BuyerOrderExcNoDetails"] = "DeliveryDate : " + Convert.ToDateTime(dsBODetails.Tables[0].Rows[0]["DeliveryDate"]).ToString("dd/MM/yyyy");
                    DTBO.Rows.Add(DR4);

                    DataRow DR5 = DTBO.NewRow();
                    DR5["BuyerOrderExcNoDetails"] = "ShipmentDate : " + Convert.ToDateTime(dsBODetails.Tables[0].Rows[0]["ShipmentDate"]).ToString("dd/MM/yyyy");
                    DTBO.Rows.Add(DR5);

                    DataRow DR6 = DTBO.NewRow();
                    DR6["BuyerOrderExcNoDetails"] = "MainFabric : " + dsBODetails.Tables[0].Rows[0]["ItemDescription"].ToString();
                    DTBO.Rows.Add(DR6);

                    DataRow DR7 = DTBO.NewRow();
                    DR7["BuyerOrderExcNoDetails"] = "Currency : " + dsBODetails.Tables[0].Rows[0]["CurrencyName"].ToString();
                    DTBO.Rows.Add(DR7);

                    DataRow DR8 = DTBO.NewRow();
                    if (dsBODetails.Tables[0].Rows[0]["IschangedDeliveryDate"].ToString() == "N")
                    {
                        DR8["BuyerOrderExcNoDetails"] = "IsChanged : No";
                    }
                    else
                    {
                        DR8["BuyerOrderExcNoDetails"] = "IsChanged : Yes";
                    }
                    DTBO.Rows.Add(DR8);

                    #endregion

                    gvBuyerOrderDetails.DataSource = DTBO;
                    gvBuyerOrderDetails.DataBind();

                    gvBuyerOrderDetails.Rows[7].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    gvBuyerOrderDetails.Rows[7].Cells[0].Font.Bold = true;
                    gvBuyerOrderDetails.Rows[7].Cells[0].Font.Size = 10;

                    if (dsBODetails.Tables[0].Rows[0]["IschangedDeliveryDate"].ToString() == "Y")
                    {
                        gvBuyerOrderDetails.Rows[7].Cells[0].ForeColor = System.Drawing.Color.Red;
                    }
                }

                DataSet dsBOSketch = objBs.GetBuyerOrderSketchDetails(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (dsBOSketch.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable DTBOS = new DataTable();
                    DTBOS.Columns.Add(new DataColumn("Sketch1"));
                    DTBOS.Columns.Add(new DataColumn("Sketch2"));
                    DTBOS.Columns.Add(new DataColumn("Sketch3"));
                    DTBOS.Columns.Add(new DataColumn("Sketch4"));
                    DTBOS.Columns.Add(new DataColumn("Sketch5"));
                    DTBOS.Columns.Add(new DataColumn("Sketch6"));
                    DTBOS.Columns.Add(new DataColumn("Sketch7"));

                    DataRow DR1 = DTBOS.NewRow();

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

                    DTBOS.Rows.Add(DR1);

                    #endregion

                    gvBuyerOrderImages.DataSource = DTBOS;
                    gvBuyerOrderImages.DataBind();
                }



                int TotalQty = 0; double TotalAmt = 0;
                DataSet dsSizes = objBs.getBuyerOrderSizesExcel(Convert.ToInt32(ddlExcNo.SelectedValue));

                // int[] odds = new int[dsSizes.Tables[0].Rows.Count];

                //List<string> list = new List<string>();
                //list.Add("one");
                //list.Add("two");
                //list.Add("three");

                //   string[] array = list.ToArray();


                //string aaf="";

                DataTable DTS = new DataTable();
                DTS.Columns.Add(new DataColumn("Column1"));
                DTS.Columns.Add(new DataColumn("Column2"));
                DTS.Columns.Add(new DataColumn("Column3"));


                foreach (DataRow dr in dsSizes.Tables[0].Rows)
                {
                    DTS.Columns.Add(new DataColumn(dr["Size"].ToString()));

                    //   list.Add(dr["Size"].ToString());

                    //  List<string> lists = new List<string>();
                }

                //   var Sifze = new[] { list };



                DTS.Columns.Add(new DataColumn("Column4"));
                DTS.Columns.Add(new DataColumn("Column5"));
                DTS.Columns.Add(new DataColumn("Column6"));


                DataRow DRS1 = DTS.NewRow();
                DRS1["Column1"] = "StyleNo";
                DRS1["Column2"] = "Description";
                DRS1["Column3"] = "Color";
                foreach (DataRow DRSs in dsSizes.Tables[0].Rows)
                {
                    string Size = DRSs["Size"].ToString();
                    DRS1[Size] = Size;

                    // int sdfsf = 0;
                    //  int[] sdfsfd = new int[] { };



                }
                DRS1["Column4"] = "Qty";
                DRS1["Column5"] = "Rate";
                DRS1["Column6"] = "Amount";
                DTS.Rows.Add(DRS1);

                //int[] arr = new int[] { 1,2,3,4,5 };
                //int sum = 0; 
                //for (int i = 0; i < arr.Length; i++) { sum += arr[i]; } Console.WriteLine(sum);



                DataSet ds = objBs.getTransBuyerOrdervaluesExcel(Convert.ToInt32(ddlExcNo.SelectedValue));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow DR1 = DTS.NewRow();
                    DR1["Column1"] = dr["StyleNo"];
                    DR1["Column2"] = dr["Description"];
                    DR1["Column3"] = dr["Color"];

                    TotalQty += Convert.ToInt32(dr["Qty"]);

                    //  int[] array = { 25, 85, 95, 87, 25, 87, 96, 25, 45 };   

                    foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                    {
                        string Size = DRsS["Size"].ToString();
                        string SizeId = DRsS["SizeId"].ToString();

                        DataSet dsQty = objBs.getBuyerOrderRowsExcel(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(dr["RowId"]), SizeId);
                        if (dsQty.Tables[0].Rows.Count > 0)
                        {
                            DR1[Size] = dsQty.Tables[0].Rows[0]["Qty"].ToString();

                        }
                        else
                        {
                            DR1[Size] = "0";
                        }
                    }

                    DR1["Column4"] = dr["Qty"];
                    DR1["Column5"] = Convert.ToDouble(dr["Rate"]).ToString("f2");
                    DR1["Column6"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"])).ToString("f2");
                    TotalAmt += (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));
                    DTS.Rows.Add(DR1);

                }



                DataRow DRS2 = DTS.NewRow();
                DRS2["Column1"] = "";
                DRS2["Column2"] = "Total :";
                DRS2["Column3"] = "";
                DRS2["Column4"] = TotalQty.ToString("f0");
                DRS2["Column5"] = "";
                DRS2["Column6"] = TotalAmt.ToString("f2");
                DTS.Rows.Add(DRS2);

                gvBuyerOrderStyles.DataSource = DTS;
                gvBuyerOrderStyles.DataBind();

                gvBuyerOrderStyles.Rows[0].Font.Bold = true;
                gvBuyerOrderStyles.Rows[0].Font.Size = 9;

                //gvBuyerOrderStyles.Rows[gvBuyerOrderStyles.Rows.Count - 1].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                //gvBuyerOrderStyles.Rows[gvBuyerOrderStyles.Rows.Count - 1].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                //gvBuyerOrderStyles.Rows[gvBuyerOrderStyles.Rows.Count - 1].Cells[1].HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                gvBuyerOrderDetails.DataSource = null;
                gvBuyerOrderDetails.DataBind();

                gvBuyerOrderImages.DataSource = null;
                gvBuyerOrderImages.DataBind();

                gvBuyerOrderStyles.DataSource = null;
                gvBuyerOrderStyles.DataBind();
            }
        }

        protected void gvCostingDetails2_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = Convert.ToInt32(FontSize.Text);
                }
            }

        }

    }
}


