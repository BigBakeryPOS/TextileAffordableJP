using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using System.Text;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
namespace Billing.Accountsbootstrap
{
    public partial class dash : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            string super = Session["IsSuperAdmin"].ToString();
            string sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                if (super == "1")
                {
                    DropDownList1.Enabled = true;
                    DataSet dsbranchto = objbs.Branchto();
                    DropDownList1.DataSource = dsbranchto.Tables[0];
                    DropDownList1.DataTextField = "branchName";
                    DropDownList1.DataValueField = "branchcode";
                    DropDownList1.DataBind();
                    //DropDownList1.Items.Insert(0, "All");
                }
                else
                {
                    DataSet dsbranch = new DataSet();
                    dsbranch = objbs.Branchfrom(sTableName);
                    DropDownList1.DataSource = dsbranch.Tables[0];
                    DropDownList1.DataTextField = "branchName";
                    DropDownList1.DataValueField = "branchcode";
                    DropDownList1.DataBind();
                    DropDownList1.Enabled = false;
                }
            }

            //dashl.Text = ddlDD.SelectedValue + " for " + DropDownList1.SelectedItem.Text;

            chart_bind(ddlDD.SelectedValue, DropDownList1.SelectedValue);
            piechart_bind(ddlDD.SelectedValue, DropDownList1.SelectedValue);
            chart_bind123(ddlDD.SelectedValue, DropDownList1.SelectedValue);
        }

        protected void ddlDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart_bind(ddlDD.SelectedValue, DropDownList1.SelectedValue);
            piechart_bind(ddlDD.SelectedValue, DropDownList1.SelectedValue);
            chart_bind123(ddlDD.SelectedValue, DropDownList1.SelectedValue);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart_bind(ddlDD.SelectedValue, DropDownList1.SelectedValue);
            piechart_bind(ddlDD.SelectedValue, DropDownList1.SelectedValue);
            chart_bind123(ddlDD.SelectedValue, DropDownList1.SelectedValue);
        }

        private void piechart_bind(string dd, string company)
        {
            DataSet dsFinal = new DataSet();
            if (dd == "Sales")
            {
                dsFinal = objbs.GenerateSalesReportDash(company);
            }
            else
            {
                dsFinal = objbs.GeneratePurchaseReportDash(company);
            }

            string[] xValues = new string[dsFinal.Tables[0].Rows.Count];
            double[] yValues = new double[dsFinal.Tables[0].Rows.Count];

            if (dsFinal != null)
            {
                if (dsFinal.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsFinal.Tables[0].Rows.Count; i++)
                    {
                        xValues[i] = dsFinal.Tables[0].Rows[i]["monthname"].ToString();
                        yValues[i] = Convert.ToDouble(dsFinal.Tables[0].Rows[i]["Amount"]);

                        Chart2.Series["Series123"].Points.DataBindXY(xValues, yValues);

                    }
                }
            }

            Chart2.Series["Series123"].ChartType = SeriesChartType.Pie;

            Chart2.Series["Series123"]["PieLabelStyle"] = "Disabled";

            Chart2.ChartAreas["ChartArea123"].Area3DStyle.Enable3D = true;

            Chart2.Legends[0].Enabled = true;
        }

        private void chart_bind(string dd, string company)
        {
            Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            //Chart1.Series["Series1"]["DrawingStyle"] = "Emboss";
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Series["Series1"].IsValueShownAsLabel = true;

            DataSet dsFinal = new DataSet();
            if (dd == "Sales")
            {
                dsFinal = objbs.GenerateSalesReportDash(company);
            }
            else
            {
                dsFinal = objbs.GeneratePurchaseReportDash(company);
            }

            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.ColumnName = "Month";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Amount";
            dt.Columns.Add(dc);

            if (dsFinal != null)
            {
                if (dsFinal.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsFinal.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();

                        if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "1")
                        {
                            dr["Month"] = "January";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "2")
                        {
                            dr["Month"] = "February";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "3")
                        {
                            dr["Month"] = "March";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "4")
                        {
                            dr["Month"] = "April";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "5")
                        {
                            dr["Month"] = "May";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "6")
                        {
                            dr["Month"] = "June";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "7")
                        {
                            dr["Month"] = "July";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "8")
                        {
                            dr["Month"] = "August";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "9")
                        {
                            dr["Month"] = "September";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "10")
                        {
                            dr["Month"] = "October";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "11")
                        {
                            dr["Month"] = "November";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "12")
                        {
                            dr["Month"] = "December";
                        }

                        dr["Amount"] = dsFinal.Tables[0].Rows[i]["Amount"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
            }

            Chart1.DataSource = dt;
            Chart1.Series["Series1"].XValueMember = "Month";
            Chart1.Series["Series1"].YValueMembers = "Amount";
            Chart1.DataBind();
            //string file = "C:/inetpub/wwwroot/Flat/" + sPatientID + ".jpg";
            //if (!File.Exists(file))
            //{
            //    this.Chart1.SaveImage("C:/inetpub/wwwroot/Flat/" + sPatientID + ".jpg", ChartImageFormat.Jpeg);
            //}
        }


        private void chart_bind123(string dd, string company)
        {
            Chart3.Series["Ser1"].ChartType = SeriesChartType.FastLine;
            //Chart1.Series["Series1"]["DrawingStyle"] = "Emboss";
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart3.Series["Ser1"].IsValueShownAsLabel = true;

            DataSet dsFinal = new DataSet();
            if (dd == "Sales")
            {
                dsFinal = objbs.GenerateSalesReportDash(company);
            }
            else
            {
                dsFinal = objbs.GeneratePurchaseReportDash(company);
            }

            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn();
            dc.ColumnName = "Month";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Amount";
            dt.Columns.Add(dc);

            if (dsFinal != null)
            {
                if (dsFinal.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsFinal.Tables[0].Rows.Count; i++)
                    {

                        DataRow dr;
                        dr = dt.NewRow();

                        if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "1")
                        {
                            dr["Month"] = "January";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "2")
                        {
                            dr["Month"] = "February";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "3")
                        {
                            dr["Month"] = "March";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "4")
                        {
                            dr["Month"] = "April";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "5")
                        {
                            dr["Month"] = "May";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "6")
                        {
                            dr["Month"] = "June";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "7")
                        {
                            dr["Month"] = "July";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "8")
                        {
                            dr["Month"] = "August";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "9")
                        {
                            dr["Month"] = "September";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "10")
                        {
                            dr["Month"] = "October";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "11")
                        {
                            dr["Month"] = "November";
                        }
                        else if (dsFinal.Tables[0].Rows[i]["monthname"].ToString() == "12")
                        {
                            dr["Month"] = "December";
                        }

                        dr["Amount"] = dsFinal.Tables[0].Rows[i]["Amount"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
            }

            Chart3.DataSource = dt;
            Chart3.Series["Ser1"].XValueMember = "Month";
            Chart3.Series["Ser1"].YValueMembers = "Amount";
            Chart3.DataBind();
            //string file = "C:/inetpub/wwwroot/Flat/" + sPatientID + ".jpg";
            //if (!File.Exists(file))
            //{
            //    this.Chart1.SaveImage("C:/inetpub/wwwroot/Flat/" + sPatientID + ".jpg", ChartImageFormat.Jpeg);
            //}

        }

    }
}