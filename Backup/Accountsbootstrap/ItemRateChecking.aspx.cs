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
    public partial class ItemRateChecking : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            if (!IsPostBack)
            {
                string ID = Request.QueryString.Get("CMP");
                if (ID != "" || ID != null)
                {
                    string CMP = Request.QueryString.Get("CMP");
                    string ITM = Request.QueryString.Get("ITM");
                    string CLR = Request.QueryString.Get("CLR");

                    string ISR = Request.QueryString.Get("ISR");
                    string ITM1 = Request.QueryString.Get("ITM1");
                    string CLR1 = Request.QueryString.Get("CLR1");

                    DataSet dsPORates = objBs.GetPORates1(CMP, ITM, CLR);
                    if (dsPORates.Tables[0].Rows.Count > 0)
                    {
                        gvPORates.DataSource = dsPORates;
                        gvPORates.DataBind();
                    }
                    else
                    {
                        gvPORates.DataSource = null;
                        gvPORates.DataBind();
                    }
                    DataSet dsIPOERates = objBs.GetIPOERates1(CMP, ITM, CLR);
                    if (dsIPOERates.Tables[0].Rows.Count > 0)
                    {
                        gvIPOERates.DataSource = dsIPOERates;
                        gvIPOERates.DataBind();
                    }
                    else
                    {
                        gvIPOERates.DataSource = null;
                        gvIPOERates.DataBind();
                    }



                    DataSet dsIPORRates = objBs.GetIPORRates1(CMP, ITM, CLR);
                    if (dsIPORRates.Tables[0].Rows.Count > 0)
                    {
                        gvIPORRates.DataSource = dsIPORRates;
                        gvIPORRates.DataBind();
                    }
                    else
                    {
                        gvIPORRates.DataSource = null;
                        gvIPORRates.DataBind();
                    }

                    if (ISR == "Yes")
                    {
                        DataSet dsIPORRates1 = objBs.GetPORates1(CMP, ITM1, CLR1);
                        if (dsIPORRates1.Tables[0].Rows.Count > 0)
                        {
                            gvIPORRates1.DataSource = dsIPORRates1;
                            gvIPORRates1.DataBind();
                        }
                        else
                        {
                            gvIPORRates1.DataSource = null;
                            gvIPORRates1.DataBind();
                        }
                        DataSet dsIPORRates2 = objBs.GetIPOERates1(CMP, ITM1, CLR1);
                        if (dsIPORRates2.Tables[0].Rows.Count > 0)
                        {
                            gvIPORRates2.DataSource = dsIPORRates2;
                            gvIPORRates2.DataBind();
                        }
                        else
                        {
                            gvIPORRates2.DataSource = null;
                            gvIPORRates2.DataBind();
                        }
                    }



                    DataSet dsOPSRates = objBs.GetOPSRates1(CMP, ITM, CLR);
                    if (dsOPSRates.Tables[0].Rows.Count > 0)
                    {
                        gvOPSRates.DataSource = dsOPSRates;
                        gvOPSRates.DataBind();
                    }
                    else
                    {
                        gvOPSRates.DataSource = null;
                        gvOPSRates.DataBind();
                    }

                }
            }
        }

    }
}


