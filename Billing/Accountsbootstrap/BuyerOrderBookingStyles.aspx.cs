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
    public partial class BuyerOrderBookingStyles : System.Web.UI.Page
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
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            DataSet dsCurrency = objBs.gridCurrency();
            int CurrencyCount = dsCurrency.Tables[0].Rows.Count;

            DataTable DTS = new DataTable();
            DTS.Columns.Add(new DataColumn("SNo"));
            DTS.Columns.Add(new DataColumn("StyleNo"));
            DTS.Columns.Add(new DataColumn("BuyerOrderQty"));

            foreach (DataRow dr in dsCurrency.Tables[0].Rows)
            {
                DTS.Columns.Add(new DataColumn(dr["CurrencyName"].ToString()));
            }

            DateTime FromYear = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToYear = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dsQty = objBs.GetBOBookingSummaryStyleQty(ddlType.SelectedValue, FromYear, ToYear);
            DataSet dsAmt = objBs.GetBOBookingSummaryStyleAmount(ddlType.SelectedValue, FromYear, ToYear);

            int SNo = 1;

            foreach (DataRow dr in dsQty.Tables[0].Rows)
            {
                DataRow DR = DTS.NewRow();

                DR["SNo"] = SNo++;
                DR["StyleNo"] = dr["StyleNo"];
                DR["BuyerOrderQty"] = dr["Qty"];

                foreach (DataRow drC in dsCurrency.Tables[0].Rows)
                {
                    string CurrencyName = drC["CurrencyName"].ToString();

                    DataRow[] RowsCurrency = dsAmt.Tables[0].Select("SamplingCostingId='" + dr["SamplingCostingId"] + "' and CurrencyId='" + drC["CurrencyId"] + "' ");
                    if (RowsCurrency.Length > 0)
                    {
                        DR[CurrencyName] = RowsCurrency[0]["Amount"];
                    }
                    else
                    {
                        DR[CurrencyName] = 0;
                    }
                }

                DTS.Rows.Add(DR);

            }

            ViewState["CurrentTable"] = DTS;
            gvBuyerOrderBookingSummary.DataSource = DTS;
            gvBuyerOrderBookingSummary.DataBind();



        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= BuyerOrderBookingStyle.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Excel.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void gvBuyerOrderBookingSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("USD"));

                gvBuyerOrderBookingSummary.FooterRow.Cells[1].Text = "Total";
                gvBuyerOrderBookingSummary.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvBuyerOrderBookingSummary.FooterRow.Cells[3].Text = total.ToString();

            }

        }

    }
}


