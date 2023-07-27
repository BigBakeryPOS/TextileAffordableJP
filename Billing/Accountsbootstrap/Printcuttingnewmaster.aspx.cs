using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
namespace Billing.Accountsbootstrap
{
    public partial class Printcuttingnewmaster : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double meter = 0.00;
        double Qty = 0.00;
        int count = 0;
        string ids = string.Empty;
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

                gridprint.Visible = true;

                DataSet getlot = new DataSet();
                getlot = objBs.getlotnumberformasterid(Convert.ToInt32(iSalesID));
                if (getlot.Tables[0].Rows.Count > 0)
                {
                    ids = getlot.Tables[0].Rows[0]["LotNo"].ToString();
                }


                DataSet ds2 = objBs.Cuttingprintreportfornew(Convert.ToInt32(ids));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblLot.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    lblllot.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["deliverydate"]).ToString("dd-MM-yyyy");
                    lblwidth.Text = ds2.Tables[0].Rows[0]["width"].ToString();
                    //  lblfit.Text = ds2.Tables[0].Rows[0]["fit"].ToString();
                    lblcut.Text = ds2.Tables[0].Rows[0]["Cut"].ToString();



                    gridprint.DataSource = ds2;
                    gridprint.DataBind();
                    //if (ds2.Tables[0].Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    //    {
                    //        meter = meter + Convert.ToDouble(ds2.Tables[0].Rows[i]["reqmeter"]);
                    //        Qty = Qty + Convert.ToDouble(ds2.Tables[0].Rows[i]["Qty"]);
                    //    }
                    //    Lblvalue.Text = (meter / Qty).ToString("0.00");
                    //    count = ds2.Tables[0].Rows.Count;
                    //}




                    //DataSet ds23 = objBs.gettotalqtyCuttingprintreport(Convert.ToInt32(iSalesID));
                    //if (ds23.Tables[0].Rows.Count > 0)
                    //{

                    //    GridView1.DataSource = ds23;
                    //    GridView1.DataBind();
                    //}



                    //DataSet ds234 = objBs.gettotalrateCuttingprintreport(Convert.ToInt32(iSalesID));
                    //if (ds234.Tables[0].Rows.Count > 0)
                    //{

                    //    GridView2.DataSource = ds234;
                    //    GridView2.DataBind();


                    //    double total = 90 + Convert.ToDouble(ds234.Tables[0].Rows[0]["tot"]) / Qty;
                    //    lblratee.Text = total.ToString("0.00");
                    //}

                }
                DataSet ds22 = objBs.Cuttingprintreportfornewformaster(Convert.ToInt32(iSalesID));
                if (ds22.Tables[0].Rows.Count > 0)
                {
                    gridmaster.DataSource = ds22;
                    gridmaster.DataBind();
                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("mastercutgrid.aspx");
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
        }


    }
}