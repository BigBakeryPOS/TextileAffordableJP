using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;


namespace Billing.Accountsbootstrap
{
    public partial class BCPreviewLot : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string CmpyId = "";
        double meter = 0.00;
        double Qty = 0.00;
        double totalfs = 0;
        int count = 0;
        string designcount = string.Empty;
        int iCntDesign = 1;
        bool bTotal = false;
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        double dblSubTotalUnitPrice = 0;
        double dblSubTotalQuantity = 0;
        double dblSubTotalDiscount = 0;
        double dblSubTotalRAte = 0;
        double dblSubTotalAmount = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice = 0;
        double dblGrandTotalQuantity = 0;
        double dblGrandTotalDiscount = 0;
        double dblGrandTotalAmount = 0;
        double ttlissuemtr = 0; double ttlActualMeter = 0; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string iSalesID = Request.QueryString.Get("iCutID");
            sTableName = Session["User"].ToString();
            if (iSalesID != null)
            {

            
                DataSet ds2 = objBs.printpreviewcut(Convert.ToInt32(iSalesID));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                   

                    lblLot.Text = ds2.Tables[0].Rows[0]["LotNo"].ToString();
                   // lbbllott.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["issuedate"]).ToString("dd/MM/yyyy");
                    lblrecdate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["DeliverDate"]).ToString("dd/MM/yyyy");
                    lblwidth.Text = ds2.Tables[0].Rows[0]["width"].ToString();
                    //lblfit.Text = ds2.Tables[0].Rows[0]["fitid"].ToString();
                    lblcut.Text = ds2.Tables[0].Rows[0]["LedgerName"].ToString();

                    lblgastin.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString();

                    lblrolltaka.Text = ds2.Tables[0].Rows[0]["RollTaka"].ToString();

                    CmpyId = ds2.Tables[0].Rows[0]["Branch"].ToString();

                    //DataSet dsizee = objBs.Getfitseize(lblfit.Text);
                    //if ((dsizee.Tables[0].Rows.Count > 0))
                    //{

                    //    for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                    //    {

                    //        string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();




                    //        {

                    //            chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                    //        }

                    //    }
                    //}
                    gridnewprint.DataSource = ds2;
                    gridnewprint.DataBind();

                }
            }

            if (CmpyId == "3")
            {
             

                lblmblrpll.Visible = true;
                lblmblbc.Visible = false;
            }
            else
            {
             

                lblmblrpll.Visible = false;
                lblmblbc.Visible = true;
            }
        }

        protected void RatioShirtProcess_OnDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ttlissuemtr = ttlissuemtr + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter"));
                ttlActualMeter = ttlActualMeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualMeter"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[1].Text = "Total :";
                e.Row.Cells[2].Text = ttlissuemtr.ToString();
                e.Row.Cells[3].Text = ttlActualMeter.ToString();
            }
        }

    }
}