using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using CommonLayer;
using System.Text;
using System.Data;

namespace Billing.Accountsbootstrap
{
    public partial class PaymentPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double grandmeter = 0;
        string CmpyId = "";
        double Quantity1 = 0;
        double PieceRate1 = 0;
        double Amount1 = 0;
        double DebitAmount = 0;
        double Miscellaneous = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string iSalesID = Request.QueryString.Get("PaymentID");


            sTableName = Session["User"].ToString();
            if (iSalesID != null)
            {

                gridprint.Visible = true;
                DataSet ds2 = objBs.paymentprint(Convert.ToInt32(iSalesID));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblpayno.Text = ds2.Tables[0].Rows[0]["PaymentNo"].ToString();
                    lblpaydate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["PaymentDate"].ToString()).ToString("dd/MM/yyyy");
                    lbljobworker.Text = ds2.Tables[0].Rows[0]["LedgerName"].ToString();
                    lblgastin.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString();
                    lblTotAmt.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["Amount"]).ToString("f2");
                    lblnarrationpay.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                    lblProcessType.Text = ds2.Tables[0].Rows[0]["ProcessType"].ToString();

                    CmpyId = ds2.Tables[0].Rows[0]["Companyid"].ToString();

                    DataSet dsdetails = new DataSet();

                    string sdsd = ds2.Tables[0].Rows[0]["Processid"].ToString();

                    if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "2")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpStiching", "StichingId");
                    }
                    else if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "3")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpEmbroiding", "EmbroidingId");
                    }
                    else if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "1")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpKajaButton", "KajaButtonId");
                    }
                    else if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "4")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpWashing", "WashingId");
                    }
                    else if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "7")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpPrinting", "PrintingId");
                    }
                    else if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "5")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpIroning", "IroningId");
                    }
                    else if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "8")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpBarTag", "BarTagId");
                    }
                    else if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "9")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpTrimming", "TrimmingId");
                    }
                    else if (ds2.Tables[0].Rows[0]["Processid"].ToString() == "10")
                    {
                        dsdetails = objBs.paymentprdetails(Convert.ToInt32(iSalesID), "tblJpConsai", "ConsaiId");
                    }
                    else
                    {

                    }
                    if (dsdetails.Tables.Count > 0)
                    {
                        if (dsdetails.Tables[0].Rows.Count > 0)
                        {
                            gridprint.DataSource = dsdetails;
                            gridprint.DataBind();
                        }
                        else
                        {
                            gridprint.DataSource = null;
                            gridprint.DataBind();
                        }
                    }
                    else
                    {
                        gridprint.DataSource = null;
                        gridprint.DataBind();
                    }
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
        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Quantity1 = Quantity1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity1"));
                PieceRate1 = PieceRate1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PieceRate1"));
                Amount1 = Amount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount1"));

                DebitAmount = DebitAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DebitAmount"));
                Miscellaneous = Miscellaneous + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Miscellaneous"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:- ";
                e.Row.Cells[3].Text = DebitAmount.ToString("f2");
                e.Row.Cells[4].Text = Quantity1.ToString();
                e.Row.Cells[6].Text = Amount1.ToString("f2");
                e.Row.Cells[7].Text = Miscellaneous.ToString("f2");

                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[2].Font.Bold = true;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentGridView.aspx");
        }

        //protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //if (e.Row.RowType == DataControlRowType.DataRow)
        //    //{
        //    //    double Meter = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter").ToString());
        //    //    grandmeter = grandmeter + Meter;
        //    //}
        //    //if (e.Row.RowType == DataControlRowType.Footer)
        //    //{
        //    //    e.Row.Cells[5].Text = "Total";
        //    //    e.Row.Cells[6].Text = grandmeter.ToString();
        //    //}
        //}


    }
}