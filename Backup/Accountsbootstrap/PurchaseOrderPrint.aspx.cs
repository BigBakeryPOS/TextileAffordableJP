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
    public partial class PurchaseOrderPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        double Qty = 0; double Amount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string POId = Request.QueryString.Get("POId");
                if (POId != null && POId != "")
                {
                    #region PurchaseOrder Print



                    DataSet ds = objBs.PurchaseOrderPrint(Convert.ToInt32(POId));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet dsCompanyDetails = objBs.GetSelectCompanyDetails(Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString()));
                        lblFCompany.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();
                        lblCoName.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();

                        lblFAddress.Text = dsCompanyDetails.Tables[0].Rows[0]["Address"].ToString();
                        lblFAreaandPincode.Text = dsCompanyDetails.Tables[0].Rows[0]["Area"].ToString() + " - " + dsCompanyDetails.Tables[0].Rows[0]["Pincode"].ToString();
                        lblFGST.Text = dsCompanyDetails.Tables[0].Rows[0]["Tin"].ToString();

                        lblFPhone.Text = dsCompanyDetails.Tables[0].Rows[0]["PhoneNo"].ToString();
                        lblFMobile.Text = dsCompanyDetails.Tables[0].Rows[0]["MobileNo"].ToString();

                        lblFax.Text = dsCompanyDetails.Tables[0].Rows[0]["Fax"].ToString();
                        lblFEmail.Text = dsCompanyDetails.Tables[0].Rows[0]["Email"].ToString();

                        lblcompanyname.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                        lbladdress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        lblCityandPincode.Text = ds.Tables[0].Rows[0]["City"].ToString() + " - " + ds.Tables[0].Rows[0]["Pincode"].ToString();
                        lblArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();

                        lblphoneno.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        lblGST.Text = ds.Tables[0].Rows[0]["GSTIN"].ToString();

                        lblProcessOrderNo.Text = ds.Tables[0].Rows[0]["FullPONo"].ToString();
                        lblOrderDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                        lblOrderDateBetween.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy") + "  To  " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");
                        lblDeliveryPlace.Text = ds.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        lblProcessOn.Text = ds.Tables[0].Rows[0]["Category"].ToString();

                        DataSet dsItem = objBs.TransPurchaseOrderPrint(Convert.ToInt32(POId));
                        gvItemProcessOrder.DataSource = dsItem;
                        gvItemProcessOrder.DataBind();

                    }


                    #endregion

                }
            }
        }

        protected void gvItemProcessOrder_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                Amount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total :";
                e.Row.Cells[6].Text = Qty.ToString("f2");
                e.Row.Cells[8].Text = Amount.ToString("f2");
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseOrderGrid.aspx");
        }

    }
}