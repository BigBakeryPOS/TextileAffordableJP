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
    public partial class Print_Fabric : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double grandmeter = 0; double grandAvaliableMeter = 0; double grandPinningMeter = 0;

        double billMeter = 0; double Meter = 0; double AvaliableMeter = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string iSalesID = Request.QueryString.Get("iSalesID");
            string iReturnPrint = Request.QueryString.Get("iReturnPrint");

            sTableName = Session["User"].ToString();
            if (iSalesID != null)
            {

                gridprint.Visible = true;
                DataSet ds2 = objBs.Fabricreport(Convert.ToInt32(iSalesID));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    //lbldcno.Text = ds2.Tables[0].Rows[0]["Fabno"].ToString();
                    //lbldcdate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["RegDate"].ToString()).ToString("dd/MM/yyyy");

                    fabregno.InnerText = "Fabric Register No -";

                    lbllrno.Text = ds2.Tables[0].Rows[0]["LRNO"].ToString();
                    lbltransport.Text = ds2.Tables[0].Rows[0]["TransportNo"].ToString();

                    hidenar.Visible = false;

                    lblInvoiceNo.Text = ds2.Tables[0].Rows[0]["InvoiceNo"].ToString();
                    lblRegisterdate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["InvDate"].ToString()).ToString("dd/MM/yyyy");
                    lblInRefNo.Text = ds2.Tables[0].Rows[0]["Refno"].ToString();
                    lblTotMtr.Text = ds2.Tables[0].Rows[0]["TotalMeter"].ToString();
                    lblSuppName.Text = ds2.Tables[0].Rows[0]["LedgerName"].ToString();
                    lblCheSign.Text = ds2.Tables[0].Rows[0]["Name"].ToString();
                    lblInvDate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["InvDate"].ToString()).ToString("dd/MM/yyyy");
                    lblchellanno.Text = ds2.Tables[0].Rows[0]["Delivery_Challan"].ToString();
                    lblTotAmt.Text = ds2.Tables[0].Rows[0]["TotalAmount"].ToString();
                    //  lblBrandName.Text = ds2.Tables[0].Rows[0]["BrandName"].ToString();
                    gridprint.DataSource = ds2;
                    gridprint.DataBind();


                    if (ds2.Tables[0].Rows[0]["Province"].ToString() == "1")
                    {
                        cg.Visible = true;
                        sg.Visible = true;
                        ig.Visible = false;

                        lblGrandtotalamt.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["SubTotal"]).ToString("f2");
                        lblCGST.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["CGST"]).ToString("f2");
                        lblSGST.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["SGST"]).ToString("f2");
                        lblIGST.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["IGST"]).ToString("f2");
                        lblBillAmt.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");
                    }
                    else
                    {
                        cg.Visible = false;
                        sg.Visible = false;
                        ig.Visible = true;

                        lblGrandtotalamt.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["SubTotal"]).ToString("f2");
                        lblCGST.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["CGST"]).ToString("f2");
                        lblSGST.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["SGST"]).ToString("f2");
                        lblIGST.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["IGST"]).ToString("f2");
                        lblBillAmt.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");
                    }

                }
            }

            if (iReturnPrint != null)
            {

                gridprint.Visible = true;
                DataSet ds2 = objBs.Fabricreportreturn(Convert.ToInt32(iReturnPrint));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    //lbldcno.Text = ds2.Tables[0].Rows[0]["Fabno"].ToString();
                    //lbldcdate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["RegDate"].ToString()).ToString("dd/MM/yyyy");
                    fabregno.InnerText = "Fabric Return Reg.No -";

                    hidenar.Visible = true;

                    lbllrno.Text = ds2.Tables[0].Rows[0]["LRNO"].ToString();
                    lbltransport.Text = ds2.Tables[0].Rows[0]["Transport"].ToString();
                    lblnarration.Text = ds2.Tables[0].Rows[0]["narration"].ToString();

                    lblInvoiceNo.Text = ds2.Tables[0].Rows[0]["ReturnId"].ToString();
                    lblRegisterdate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["InvDate"].ToString()).ToString("dd/MM/yyyy");
                    lblInRefNo.Text = ds2.Tables[0].Rows[0]["Refno"].ToString();
                    lblTotMtr.Text = ds2.Tables[0].Rows[0]["TotalMeter"].ToString();
                    lblSuppName.Text = ds2.Tables[0].Rows[0]["LedgerName"].ToString();
                    lblCheSign.Text = ds2.Tables[0].Rows[0]["Name"].ToString();
                    lblInvDate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["InvDate"].ToString()).ToString("dd/MM/yyyy");
                    lblchellanno.Text = ds2.Tables[0].Rows[0]["Delivery_Challan"].ToString();
                    lblTotAmt.Text = ds2.Tables[0].Rows[0]["TotalAmount"].ToString();
                    //  lblBrandName.Text = ds2.Tables[0].Rows[0]["BrandName"].ToString();
                    gvreturnprint.DataSource = ds2;
                    gvreturnprint.DataBind();

                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewprocess.aspx");
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                billMeter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "billMeter").ToString());
                Meter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter").ToString());
                AvaliableMeter += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvaliableMeter").ToString());


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total";
                e.Row.Cells[6].Text = billMeter.ToString("f2");
                e.Row.Cells[7].Text = Meter.ToString("f2");
                e.Row.Cells[8].Text = AvaliableMeter.ToString("f2");

                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;
            }
        }

        protected void gvreturnprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double Meter = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter").ToString());
                grandmeter = grandmeter + Meter;

                double AvaliableMeter = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Meter").ToString());
                grandAvaliableMeter = grandAvaliableMeter + AvaliableMeter;

                //double Pinning = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Pinning").ToString());
                //grandPinningMeter = grandPinningMeter + Pinning;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total";
                e.Row.Cells[6].Text = grandmeter.ToString("f2");
                // e.Row.Cells[7].Text = grandAvaliableMeter.ToString("f2");
                // e.Row.Cells[8].Text = grandPinningMeter.ToString("f2");

                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                //  e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                // e.Row.Cells[7].Font.Bold = true;
                // e.Row.Cells[8].Font.Bold = true;
            }
        }
    }
}