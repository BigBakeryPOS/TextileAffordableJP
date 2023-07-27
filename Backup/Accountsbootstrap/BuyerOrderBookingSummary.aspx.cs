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
    public partial class BuyerOrderBookingSummary : System.Web.UI.Page
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
                //DataSet dsRecPONo = objBs.GetBuyerOrderExcNo();
                //if (dsRecPONo.Tables[0].Rows.Count > 0)
                //{
                //    ddlYears.DataSource = dsRecPONo.Tables[0];
                //    ddlYears.DataTextField = "ExcNo";
                //    ddlYears.DataValueField = "BuyerOrderId";
                //    ddlYears.DataBind();
                //    ddlYears.Items.Insert(0, "Select ExcNo");
                //}

            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlYears.SelectedValue != "" && ddlYears.SelectedValue != "" && ddlYears.SelectedValue != "Select ExcNo")
            {
                DataSet dsCurrency = objBs.gridCurrency();
                int CurrencyCount = dsCurrency.Tables[0].Rows.Count;

                DataTable DTS = new DataTable();
                DTS.Columns.Add(new DataColumn("Month"));
                DTS.Columns.Add(new DataColumn("BuyerOrderQty"));

                foreach (DataRow dr in dsCurrency.Tables[0].Rows)
                {
                    DTS.Columns.Add(new DataColumn(dr["CurrencyName"].ToString()));
                }

                string[] Years = ddlYears.SelectedItem.Text.Split('-');

                DateTime FromYear = DateTime.ParseExact("01/04/" + Years[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToYear = DateTime.ParseExact("31/03/" + Years[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet dsQty = objBs.GetBOBookingSummaryQty(ddlType.SelectedValue,FromYear, ToYear);
                DataSet dsAmt = objBs.GetBOBookingSummaryAmount(ddlType.SelectedValue,FromYear, ToYear);

                string FromY = Years[0].Substring(Years[0].Length - 2);
                string ToY = Years[1].Substring(Years[1].Length - 2);

                var TotalMonths = new[] { "April", "May", "June", "July", "August", "September", "October", "November", "December", "January", "February", "March" };
                var TotalMonthsFill = new[] { "April-" + FromY, "May-" + FromY, "June-" + FromY, "July-" + FromY, "Aug-" + FromY, "Sept-" + FromY, "Oct-" + FromY, "Nov-" + FromY, "Dec-" + FromY, "Jan-" + ToY, "Feb-" + ToY, "Mar-" + ToY };

                for (int i = 0; i <= 11; i++)
                {
                    #region

                    DataRow[] RowsQty = dsQty.Tables[0].Select("Months='" + TotalMonths[i] + "'");
                    if (RowsQty.Length > 0)
                    {
                        DataRow DR = DTS.NewRow();

                        DR["Month"] = TotalMonthsFill[i];
                        DR["BuyerOrderQty"] =Convert.ToInt32(RowsQty[0]["CQty"]);

                        foreach (DataRow dr in dsCurrency.Tables[0].Rows)
                        {
                            string CurrencyName = dr["CurrencyName"].ToString();

                            DataRow[] RowsCurrency = dsAmt.Tables[0].Select("Months='" + TotalMonths[i] + "' and CurrencyId='" + dr["CurrencyId"] + "' ");
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
                    else
                    {
                        DataRow DR = DTS.NewRow();

                        DR["Month"] = TotalMonthsFill[i];
                        DR["BuyerOrderQty"] = 0;
                        foreach (DataRow dr in dsCurrency.Tables[0].Rows)
                        {
                            string CurrencyName = dr["CurrencyName"].ToString();
                            DR[CurrencyName] = 0;
                        }
                        DTS.Rows.Add(DR);
                    }
                    #endregion
                }

                ViewState["CurrentTable"] = DTS;
                gvBuyerOrderBookingSummary.DataSource = DTS;
                gvBuyerOrderBookingSummary.DataBind();

                //gvBuyerOrderBookingSummary.Row
                //gvBuyerOrderBookingSummary.Rows[0].Cells[0].Font.Bold = true;

              //  int total = DTS.AsEnumerable().Sum(row => row.Field<Int32>("BuyerOrderQty"));

                //int totaal = 0;
                //for (int k = 1; k < DTS.Columns.Count - 1; k++)
                //{
                //    totaal = DTS.AsEnumerable().Sum(row => row.Field<Int32>(DTS.Columns[k].ToString()));
                //    gvBuyerOrderBookingSummary.FooterRow.Cells[k].Text = totaal.ToString();
                //    gvBuyerOrderBookingSummary.FooterRow.Cells[k].Font.Bold = true;
                //    gvBuyerOrderBookingSummary.FooterRow.BackColor = System.Drawing.Color.Beige;
                //}

            }
            else
            {
                gvBuyerOrderBookingSummary.DataSource = null;
                gvBuyerOrderBookingSummary.DataBind();
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= BuyerOrderBookingSummary.xls");
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataTable dt = (DataTable)ViewState["CurrentTable"];
                //decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("USD"));

                //gvBuyerOrderBookingSummary.FooterRow.Cells[1].Text = "Total";
                //gvBuyerOrderBookingSummary.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                //gvBuyerOrderBookingSummary.FooterRow.Cells[3].Text = total.ToString();

                //int i = 0;
                //int j = 0;
                //int sum = 0;
                //for (i = 0; i <= gvBuyerOrderBookingSummary.Columns.Count - 1; i++)
                //{
                //    for (j = 0; j <= gvBuyerOrderBookingSummary.Rows.Count - 1; j++)
                //    {
                //        sum += Convert.ToInt32(gvBuyerOrderBookingSummary.Rows(j).Cells(i).Text);
                //    }
                //    gvBuyerOrderBookingSummary.FooterRow.Cells(i).Text = Math.Round((sum / j), 2).ToString();
                //    sum = 0;
                //}
            }

        }

    }
}


