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
    public partial class CostingDetailsNew : System.Web.UI.Page
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
                DataSet dsRecPONo = objBs.GetAllStyleNo();
                if (dsRecPONo.Tables[0].Rows.Count > 0)
                {
                    ddlStyleNo.DataSource = dsRecPONo.Tables[0];
                    ddlStyleNo.DataTextField = "StyleNo";
                    ddlStyleNo.DataValueField = "SamplingCostingId";
                    ddlStyleNo.DataBind();
                    ddlStyleNo.Items.Insert(0, "Select StyleNo");
                }
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            DataSet dsStyles = objBs.GetCostingDetailsNew(Convert.ToInt32(ddlStyleNo.SelectedValue));
            if (dsStyles.Tables[0].Rows.Count > 0)
            {

                double FabricationCost = Convert.ToDouble(dsStyles.Tables[0].Rows[0]["FabricationCost"]);
                double EmbroideryMachineCost = Convert.ToDouble(dsStyles.Tables[0].Rows[0]["EmbroideryMachineCost"]);
                double EmbroideryHandCost = Convert.ToDouble(dsStyles.Tables[0].Rows[0]["EmbroideryHandCost"]);
                double PieceProcessCost = Convert.ToDouble(dsStyles.Tables[0].Rows[0]["PieceProcessCost"]);
                double FinishingandPackingCost = Convert.ToDouble(dsStyles.Tables[0].Rows[0]["FinishingandPackingCost"]);
                double LogisticsCost = Convert.ToDouble(dsStyles.Tables[0].Rows[0]["LogisticsCost"]);

                DataSet dsCD = objBs.GetCostingDetailsOthersNew(Convert.ToInt32(ddlStyleNo.SelectedValue));

                DataTable DT = new DataTable();
                DT.Columns.Add(new DataColumn("Item"));
                DT.Columns.Add(new DataColumn("Avg"));
                DT.Columns.Add(new DataColumn("Rate"));
                DT.Columns.Add(new DataColumn("Price"));

                DataRow DR1 = DT.NewRow();
                DR1["Item"] = "StyleNo " + dsStyles.Tables[0].Rows[0]["StyleNo"].ToString();
                DR1["Avg"] = "Avg";
                DR1["Rate"] = "Rate";
                DR1["Price"] = "Amount";
                DT.Rows.Add(DR1);

                double TotalAmount = 0;
                foreach (DataRow DR in dsCD.Tables[0].Rows)
                {
                    DataRow DR2 = DT.NewRow();
                    DR2["Item"] = DR["Description"].ToString();
                    DR2["Avg"] = Convert.ToDouble(DR["PrdAvg"]).ToString("f2");
                    DR2["Rate"] = Convert.ToDouble(DR["Rate"]).ToString("f2");
                    DR2["Price"] = Convert.ToDouble(DR["Amount"]).ToString("f2");
                    DT.Rows.Add(DR2);

                    TotalAmount += Convert.ToDouble(DR["Amount"]);
                }

                DataRow DR3 = DT.NewRow();
                DR3["Item"] = "";
                DR3["Avg"] = "";
                DR3["Rate"] = "";
                DR3["Price"] = "";
                DT.Rows.Add(DR3);

                DataRow DR4 = DT.NewRow();
                DR4["Item"] = "Item Cost";
                DR4["Avg"] = "";
                DR4["Rate"] = "";
                DR4["Price"] = TotalAmount.ToString("f2");
                DT.Rows.Add(DR4);

                if (FabricationCost != 0)
                {
                    DataRow DR3551 = DT.NewRow();
                    DR3551["Item"] = "Fabrication Cost";
                    DR3551["Avg"] = "";
                    DR3551["Rate"] = "";
                    DR3551["Price"] = Convert.ToDouble(FabricationCost).ToString("f2");
                    DT.Rows.Add(DR3551);
                }

                if (EmbroideryMachineCost != 0)
                {
                    DataRow DR3552 = DT.NewRow();
                    DR3552["Item"] = "Embroidery Machine Cost";
                    DR3552["Avg"] = "";
                    DR3552["Rate"] = "";
                    DR3552["Price"] = Convert.ToDouble(EmbroideryMachineCost).ToString("f2");
                    DT.Rows.Add(DR3552);
                }

                if (EmbroideryHandCost != 0)
                {
                    DataRow DR3553 = DT.NewRow();
                    DR3553["Item"] = "Embroidery Hand Cost";
                    DR3553["Avg"] = "";
                    DR3553["Rate"] = "";
                    DR3553["Price"] = Convert.ToDouble(EmbroideryHandCost).ToString("f2");
                    DT.Rows.Add(DR3553);
                }

                if (PieceProcessCost != 0)
                {
                    DataRow DR3554 = DT.NewRow();
                    DR3554["Item"] = "Piece Process Cost";
                    DR3554["Avg"] = "";
                    DR3554["Rate"] = "";
                    DR3554["Price"] = Convert.ToDouble(PieceProcessCost).ToString("f2");
                    DT.Rows.Add(DR3554);
                }

                if (FinishingandPackingCost != 0)
                {

                    DataRow DR3555 = DT.NewRow();
                    DR3555["Item"] = "Finishing and Packing Cost";
                    DR3555["Avg"] = "";
                    DR3555["Rate"] = "";
                    DR3555["Price"] = Convert.ToDouble(FinishingandPackingCost).ToString("f2");
                    DT.Rows.Add(DR3555);
                }
                if (LogisticsCost != 0)
                {

                    DataRow DR3556 = DT.NewRow();
                    DR3556["Item"] = "Logistics Cost";
                    DR3556["Avg"] = "";
                    DR3556["Rate"] = "";
                    DR3556["Price"] = Convert.ToDouble(LogisticsCost).ToString("f2");
                    DT.Rows.Add(DR3556);
                }
                DataRow DR355 = DT.NewRow();
                DR355["Item"] = "";
                DR355["Avg"] = "";
                DR355["Rate"] = "";
                DR355["Price"] = "";
                DT.Rows.Add(DR355);

                double Totamnt = TotalAmount + FabricationCost + EmbroideryMachineCost + EmbroideryHandCost + PieceProcessCost + FinishingandPackingCost + LogisticsCost;



                DataRow DR5 = DT.NewRow();
                DR5["Item"] = "Rejection " + dsStyles.Tables[0].Rows[0]["Rejection"].ToString() + "%";
                DR5["Avg"] = "";
                DR5["Rate"] = "";
                DR5["Price"] = (Totamnt * Convert.ToDouble(dsStyles.Tables[0].Rows[0]["Rejection"].ToString()) / 100).ToString("f2");
                DT.Rows.Add(DR5);
                double Rejection = (Totamnt * Convert.ToDouble(dsStyles.Tables[0].Rows[0]["Rejection"].ToString()) / 100);

                DataRow DR6 = DT.NewRow();
                DR6["Item"] = "Total ";
                DR6["Avg"] = "";
                DR6["Rate"] = "";
                DR6["Price"] = (Totamnt + Rejection).ToString("f2");
                DT.Rows.Add(DR6);

                DataRow DR7 = DT.NewRow();
                DR7["Item"] = "ExtraMargin " + dsStyles.Tables[0].Rows[0]["ExtraMargin"].ToString() + "%";
                DR7["Avg"] = "";
                DR7["Rate"] = "";
                DR7["Price"] = ((Totamnt + Rejection) * Convert.ToDouble(dsStyles.Tables[0].Rows[0]["ExtraMargin"].ToString()) / 100).ToString("f2");
                DT.Rows.Add(DR7);
                double ExtraMargin = (Totamnt + Rejection) * Convert.ToDouble(dsStyles.Tables[0].Rows[0]["ExtraMargin"].ToString()) / 100;

                DataRow DR88 = DT.NewRow();
                DR88["Item"] = "";
                DR88["Avg"] = "";
                DR88["Rate"] = "";
                DR88["Price"] = "";
                DT.Rows.Add(DR88);

                DataRow DR8 = DT.NewRow();
                DR8["Item"] = "TotalCost ";
                DR8["Avg"] = "";
                DR8["Rate"] = "";
                DR8["Price"] = (Totamnt + Rejection + ExtraMargin).ToString("f2");
                DT.Rows.Add(DR8);

                DataRow DR11 = DT.NewRow();
                DR11["Item"] = "";
                DR11["Avg"] = "";
                DR11["Rate"] = "";
                DR11["Price"] = "";
                DT.Rows.Add(DR11);

                DataRow DR9 = DT.NewRow();
                DR9["Item"] = "Prices in " + dsStyles.Tables[0].Rows[0]["CurrencyName"].ToString();
                DR9["Avg"] = "(INR- " + Convert.ToDouble(dsStyles.Tables[0].Rows[0]["CurrencyValue"]).ToString("f2") + ")";
                DR9["Rate"] = "";
                DR9["Price"] = ((Totamnt + Rejection + ExtraMargin) / Convert.ToDouble(dsStyles.Tables[0].Rows[0]["CurrencyValue"].ToString())).ToString("f2");
                DT.Rows.Add(DR9);

                gvCostingDetails1.DataSource = dsStyles;
                gvCostingDetails1.DataBind();

                gvCostingDetails2.DataSource = DT;
                gvCostingDetails2.DataBind();

                gvCostingDetails2.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                gvCostingDetails2.Rows[0].Cells[0].Font.Bold = true;
                gvCostingDetails2.Rows[0].Cells[0].Font.Size = Convert.ToInt32(FontSize.Text);

                gvCostingDetails2.Rows[0].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                gvCostingDetails2.Rows[0].Cells[1].Font.Bold = true;
                gvCostingDetails2.Rows[0].Cells[1].Font.Size = 10;

                gvCostingDetails2.Rows[0].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                gvCostingDetails2.Rows[0].Cells[2].Font.Bold = true;
                gvCostingDetails2.Rows[0].Cells[2].Font.Size = 10;

                gvCostingDetails2.Rows[0].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                gvCostingDetails2.Rows[0].Cells[3].Font.Bold = true;
                gvCostingDetails2.Rows[0].Cells[3].Font.Size = 10;

                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[0].Font.Bold = true;
                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[0].Font.Size = Convert.ToInt32(FontSize.Text);

                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[1].Font.Bold = true;
                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[1].Font.Size = 9;

                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[3].Font.Bold = true;
                gvCostingDetails2.Rows[gvCostingDetails2.Rows.Count - 1].Cells[3].Font.Size = Convert.ToInt32(FontSize.Text);
            }
            else
            {
                gvCostingDetails1.DataSource = null;
                gvCostingDetails1.DataBind();

                gvCostingDetails2.DataSource = null;
                gvCostingDetails2.DataBind();
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


