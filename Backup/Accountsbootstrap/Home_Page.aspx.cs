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
    public partial class Home_Page : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string IsSuperAdmin = "";
        string Companyid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            IsSuperAdmin = Session["IsSuperAdmin"].ToString();
            Companyid = Session["CmpyId"].ToString();

            if (!IsPostBack)
            {
                DataSet dsOrderDetails = objBs.GetOrderDetails(Request.Cookies["userInfo"]["YearCode"].ToString());
                if (dsOrderDetails.Tables[0].Rows.Count > 0)
                {
                    gvOrderDetails.DataSource = dsOrderDetails;
                    gvOrderDetails.DataBind();
                }

                DataSet dsCuttingDetails = objBs.GetCuttingDetails(Request.Cookies["userInfo"]["YearCode"].ToString());
                if (dsCuttingDetails.Tables[0].Rows.Count > 0)
                {
                    gvCuttingDetails.DataSource = dsCuttingDetails;
                    gvCuttingDetails.DataBind();
                }

                DataSet dsPODetails = objBs.GetPODetails(Request.Cookies["userInfo"]["YearCode"].ToString());
                if (dsPODetails.Tables[0].Rows.Count > 0)
                {
                    gvPo.DataSource = dsPODetails;
                    gvPo.DataBind();
                }
                DataSet dsPurchaseDetails = objBs.GetPurchaseDetails(Request.Cookies["userInfo"]["YearCode"].ToString());
                if (dsPurchaseDetails.Tables[0].Rows.Count > 0)
                {
                    gvPurchase.DataSource = dsPurchaseDetails;
                    gvPurchase.DataBind();
                }

                DataSet dsItemProcessIssueDetails = objBs.GetItemProcessIssueDetails(Request.Cookies["userInfo"]["YearCode"].ToString());
                if (dsItemProcessIssueDetails.Tables[0].Rows.Count > 0)
                {
                    gvItemProcessIssueDetails.DataSource = dsItemProcessIssueDetails;
                    gvItemProcessIssueDetails.DataBind();
                }
                DataSet dsItemProcessReceiveDetails = objBs.GetItemProcessReceiveDetails(Request.Cookies["userInfo"]["YearCode"].ToString());
                if (dsItemProcessReceiveDetails.Tables[0].Rows.Count > 0)
                {
                    gvItemProcessReceiveDetails.DataSource = dsItemProcessReceiveDetails;
                    gvItemProcessReceiveDetails.DataBind();
                }

                DataSet dsProcessIssueDetails = objBs.GetProcessIssueDetails(Request.Cookies["userInfo"]["YearCode"].ToString());
                if (dsProcessIssueDetails.Tables.Count > 0)
                {
                    if (dsProcessIssueDetails.Tables[0].Rows.Count > 0)
                    {
                        gvProcessIssueDetails.DataSource = dsProcessIssueDetails;
                        gvProcessIssueDetails.DataBind();
                    }
                    else
                    {
                        gvProcessIssueDetails.DataSource = null;
                        gvProcessIssueDetails.DataBind();
                    }
                }
                else
                {
                    gvProcessIssueDetails.DataSource = null;
                    gvProcessIssueDetails.DataBind();
                }
                DataSet dsProcessReceiveDetails = objBs.GetProcessReceiveDetails(Request.Cookies["userInfo"]["YearCode"].ToString());
                if (dsProcessReceiveDetails.Tables.Count > 0)
                {
                    if (dsProcessReceiveDetails.Tables[0].Rows.Count > 0)
                    {
                        gvProcessReceiveDetails.DataSource = dsProcessReceiveDetails;
                        gvProcessReceiveDetails.DataBind();
                    }
                    else
                    {
                        gvProcessReceiveDetails.DataSource = null;
                        gvProcessReceiveDetails.DataBind();
                    }
                }
                else
                {
                    gvProcessReceiveDetails.DataSource = null;
                    gvProcessReceiveDetails.DataBind();
                }
            }
        }

    }
}