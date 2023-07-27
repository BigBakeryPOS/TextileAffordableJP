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
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class MaterialProcessEntryPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                string MaterialissueId = Request.QueryString.Get("MaterialissueId");
                if (MaterialissueId != null && MaterialissueId != "")
                {
                    int TotalQty = 0;

                    DataSet ds = objBs.MaterialIssuePrintProcess(Convert.ToInt32(MaterialissueId));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region

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

                        lblFullEntryNo.Text = ds.Tables[0].Rows[0]["FullMaterialNo"].ToString();
                        lblEntryDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["MaterialDate"]).ToString("dd/MM/yyyy");
                       // lblOrderDateBetween.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy") + "  To  " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                        lblprint.Text = ds.Tables[0].Rows[0]["Process"].ToString() + " Issued Print";

                        #endregion


                        DataSet gettransMaterialissue = objBs.TransMaterialIssuePrintProcess(Convert.ToInt32(MaterialissueId));
                        if (gettransMaterialissue.Tables[0].Rows.Count > 0)
                        {


                            gvCuttingProcessEntryStyles.DataSource =gettransMaterialissue;
                            gvCuttingProcessEntryStyles.DataBind();
                        }
                        else
                        {
                            gvCuttingProcessEntryStyles.DataSource = null;
                            gvCuttingProcessEntryStyles.DataBind();
                        }

                    }
                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaterialsIssueGrid.aspx");
        }

    }
}
