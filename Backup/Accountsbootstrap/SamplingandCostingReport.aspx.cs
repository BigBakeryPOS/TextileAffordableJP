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
    public partial class SamplingandCostingReport : System.Web.UI.Page
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

                DataSet dsset = objBs.getLedger(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlBuyerCode.DataSource = dsset.Tables[0];
                    ddlBuyerCode.DataTextField = "CompanyCode";
                    ddlBuyerCode.DataValueField = "LedgerID";
                    ddlBuyerCode.DataBind();
                    ddlBuyerCode.Items.Insert(0, "All");

                    ddlBuyerName.DataSource = dsset.Tables[0];
                    ddlBuyerName.DataTextField = "LedgerName";
                    ddlBuyerName.DataValueField = "LedgerID";
                    ddlBuyerName.DataBind();
                    ddlBuyerName.Items.Insert(0, "All");
                }

                DataSet dsExcNo = objBs.getAllStyleNo();
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkStyleNo.DataSource = dsExcNo.Tables[0];
                    chkStyleNo.DataTextField = "StyleNo";
                    chkStyleNo.DataValueField = "SamplingCostingId";
                    chkStyleNo.DataBind();

                }

            }
        }

        protected void buyer_order(object sender, EventArgs e)
        {

            // get Buyer ORder Wise
            DataSet dsExcNo = objBs.getAllStyleNo(ddlBuyerCode.SelectedValue);
            if (dsExcNo.Tables[0].Rows.Count > 0)
            {
                chkStyleNo.DataSource = dsExcNo.Tables[0];
                chkStyleNo.DataTextField = "StyleNo";
                chkStyleNo.DataValueField = "SamplingCostingId";
                chkStyleNo.DataBind();

            }


        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "1")
            {
                #region

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string SamplingCostingId = "";
                string IsFirst = "Yes";
                foreach (ListItem listItem in chkStyleNo.Items)
                {
                    #region
                    if (chkStyleNo.SelectedIndex < 0)
                    {
                        if (IsFirst == "Yes")
                        {
                            SamplingCostingId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            SamplingCostingId = SamplingCostingId + "," + listItem.Value;
                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                SamplingCostingId = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                SamplingCostingId = SamplingCostingId + "," + listItem.Value;
                            }
                        }
                    }
                    #endregion
                }

                DataSet dsStylesItems = objBs.SamplingandCosting_StylesItems_Report(SamplingCostingId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsStyles = objBs.SamplingandCosting_Styles_Report(SamplingCostingId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (dsStyles.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable DT = new DataTable();
                    DT.Columns.Add(new DataColumn("Column1"));
                    DT.Columns.Add(new DataColumn("Column2"));
                    DT.Columns.Add(new DataColumn("Column3"));
                    DT.Columns.Add(new DataColumn("Column4"));
                    DT.Columns.Add(new DataColumn("Column5"));
                    DT.Columns.Add(new DataColumn("Column6"));
                    DT.Columns.Add(new DataColumn("Column7"));
                    DT.Columns.Add(new DataColumn("Column8"));
                    DT.Columns.Add(new DataColumn("Column9"));
                    DT.Columns.Add(new DataColumn("Column10"));

                    foreach (DataRow DR_1 in dsStyles.Tables[0].Rows)
                    {
                        DataRow DR1 = DT.NewRow();
                        DR1["Column1"] = "Style No :";
                        DR1["Column2"] = DR_1["StyleNo"].ToString();
                        DR1["Column9"] = "Date :";
                        DR1["Column10"] = Convert.ToDateTime(DR_1["Date"].ToString()).ToString("dd/MM/yyyy");
                        DT.Rows.Add(DR1);

                        DataRow DR2 = DT.NewRow();
                        DR2["Column1"] = "Buyer Name :";
                        DR2["Column2"] = DR_1["LedgerName"].ToString();
                        DT.Rows.Add(DR2);

                        DataRow DR3 = DT.NewRow();
                        DR3["Column1"] = "Description :";
                        DR3["Column2"] = DR_1["Description"].ToString();

                        DR3["Column3"] = "Type-";
                        DR3["Column4"] = "ItemName-";
                        DR3["Column5"] = "Category-";
                        DR3["Column6"] = "SizeType-";
                        DR3["Column7"] = "Smp.Avg/Qty-";
                        DR3["Column8"] = "UOM-";
                        DR3["Column9"] = "Rate-";
                        DR3["Column10"] = "Cost-";

                        DT.Rows.Add(DR3);

                        DataRow[] rows = dsStylesItems.Tables[0].Select("SamplingCostingId='" + DR_1["SamplingCostingId"] + "'");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            DataRow DR4 = DT.NewRow();
                            DR4["Column3"] = rows[i]["Itemgroupname"].ToString();
                            DR4["Column4"] = rows[i]["Description"].ToString();
                            DR4["Column5"] = rows[i]["Category"].ToString();
                            DR4["Column6"] = rows[i]["SUOM"].ToString();
                            DR4["Column7"] = rows[i]["SmpAvg"].ToString();
                            DR4["Column8"] = rows[i]["IUOM"].ToString();
                            DR4["Column9"] = rows[i]["Rate"].ToString();
                            DR4["Column10"] = rows[i]["SmpCost"].ToString();
                            DT.Rows.Add(DR4);

                        }

                        DataRow DR55 = DT.NewRow();
                        DR55["Column1"] = "";
                        DR55["Column2"] = "";
                        DR55["Column3"] = "";
                        DR55["Column4"] = "";
                        DR55["Column5"] = "";
                        DR55["Column6"] = "";
                        DR55["Column7"] = "";
                        DR55["Column8"] = "";
                        DR55["Column9"] = "";
                        DR55["Column10"] = "";
                        DT.Rows.Add(DR55);

                        DataRow DR5 = DT.NewRow();
                        DR5["Column1"] = "Fabrication Cost:";
                        DR5["Column2"] = DR_1["FabricationCost"].ToString();
                        DR5["Column3"] = "";

                        DR5["Column4"] = "Piece Process Cost:";
                        DR5["Column5"] = DR_1["PieceProcessCost"].ToString();
                        DR5["Column6"] = "";

                        DR5["Column7"] = "Extra Margin:";
                        DR5["Column8"] = DR_1["ExtraMargin"].ToString();
                        DR5["Column9"] = "";
                        DR5["Column10"] = "";
                        DT.Rows.Add(DR5);

                        DataRow DR51 = DT.NewRow();
                        DR51["Column1"] = "Logistics Cost:";
                        DR51["Column2"] = DR_1["LogisticsCost"].ToString();
                        DR51["Column3"] = "";

                        DR51["Column4"] = "";
                        DR51["Column5"] = "";
                        DR51["Column6"] = "";

                        DR51["Column7"] = "";
                        DR51["Column8"] = "";
                        DR51["Column9"] = "";
                        DR51["Column10"] = "";
                        DT.Rows.Add(DR51);

                        DataRow DR6 = DT.NewRow();
                        DR6["Column1"] = "Embroidery[Machine] Cost:";
                        DR6["Column2"] = DR_1["EmbroideryMachineCost"].ToString();
                        DR6["Column3"] = "";

                        DR6["Column4"] = "Finishing and Packing Cost:";
                        DR6["Column5"] = DR_1["FinishingandPackingCost"].ToString();
                        DR6["Column6"] = "";

                        DR6["Column7"] = "Total Cost [INR]:";
                        DR6["Column8"] = DR_1["TotalSmpCostINR"].ToString();
                        DR6["Column9"] = "";
                        DR6["Column10"] = "";
                        DT.Rows.Add(DR6);

                        DataRow DR7 = DT.NewRow();
                        DR7["Column1"] = "Embroidery[Hand]Cost:";
                        DR7["Column2"] = DR_1["EmbroideryHandCost"].ToString();
                        DR7["Column3"] = "";

                        DR7["Column4"] = "Rejection:";
                        DR7["Column5"] = DR_1["Rejection"].ToString();
                        DR7["Column6"] = "";

                        DR7["Column7"] = "Total Cost [" + DR_1["CurrencyName"].ToString() + "]";
                        DR7["Column8"] = DR_1["TotalSmpCostOther"].ToString();
                        DR7["Column9"] = "";
                        DR7["Column10"] = "";
                        DT.Rows.Add(DR7);

                        DataRow DR8 = DT.NewRow();
                        DR8["Column1"] = "";
                        DR8["Column2"] = "";
                        DR8["Column3"] = "";
                        DR8["Column4"] = "";
                        DR8["Column5"] = "";
                        DR8["Column6"] = "";
                        DR8["Column7"] = "";
                        DR8["Column8"] = "";
                        DR8["Column9"] = "";
                        DR8["Column10"] = "";
                        DT.Rows.Add(DR8);

                        DataRow DR9 = DT.NewRow();
                        DR9["Column1"] = "-----------------------";
                        DR9["Column2"] = "-----------------------";
                        DR9["Column3"] = "-----------------------";
                        DR9["Column4"] = "-----------------------";
                        DR9["Column5"] = "-----------------------";
                        DR9["Column6"] = "-----------------------";
                        DR9["Column7"] = "-----------------------";
                        DR9["Column8"] = "-----------------------";
                        DR9["Column9"] = "-----------------------";
                        DR9["Column10"] = "-----------------------";
                        DT.Rows.Add(DR9);

                    }

                    #endregion

                    gvSamplingandCosting.ShowHeader = false;
                    gvSamplingandCosting.Caption = "Sampling & Costing Report";
                    gvSamplingandCosting.DataSource = DT;
                    gvSamplingandCosting.DataBind();
                }
                else
                {
                    gvSamplingandCosting.DataSource = null;
                    gvSamplingandCosting.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                #region

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string SamplingCostingId = "";
                string IsFirst = "Yes";
                foreach (ListItem listItem in chkStyleNo.Items)
                {
                    #region
                    if (chkStyleNo.SelectedIndex < 0)
                    {
                        if (IsFirst == "Yes")
                        {
                            SamplingCostingId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            SamplingCostingId = SamplingCostingId + "," + listItem.Value;
                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                SamplingCostingId = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                SamplingCostingId = SamplingCostingId + "," + listItem.Value;
                            }
                        }
                    }
                    #endregion
                }

                DataSet dsStylesItems = objBs.SamplingandCosting_StylesItems_Report(SamplingCostingId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsStyles = objBs.SamplingandCosting_Styles_Report(SamplingCostingId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (dsStyles.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable DT = new DataTable();
                    DT.Columns.Add(new DataColumn("Column1"));
                    DT.Columns.Add(new DataColumn("Column2"));
                    DT.Columns.Add(new DataColumn("Column3"));
                    DT.Columns.Add(new DataColumn("Column4"));
                    DT.Columns.Add(new DataColumn("Column5"));
                    DT.Columns.Add(new DataColumn("Column6"));
                    DT.Columns.Add(new DataColumn("Column7"));
                    DT.Columns.Add(new DataColumn("Column8"));
                    DT.Columns.Add(new DataColumn("Column9"));
                    DT.Columns.Add(new DataColumn("Column10"));

                    foreach (DataRow DR_1 in dsStyles.Tables[0].Rows)
                    {
                        DataRow DR1 = DT.NewRow();
                        DR1["Column1"] = "Style No :";
                        DR1["Column2"] = DR_1["StyleNo"].ToString();
                        DR1["Column9"] = "Date :";
                        DR1["Column10"] = Convert.ToDateTime(DR_1["Date"].ToString()).ToString("dd/MM/yyyy");
                        DT.Rows.Add(DR1);

                        DataRow DR2 = DT.NewRow();
                        DR2["Column1"] = "Buyer Name :";
                        DR2["Column2"] = DR_1["LedgerName"].ToString();
                        DT.Rows.Add(DR2);

                        DataRow DR3 = DT.NewRow();
                        DR3["Column1"] = "Description :";
                        DR3["Column2"] = DR_1["Description"].ToString();

                        DR3["Column3"] = "Type-";
                        DR3["Column4"] = "ItemName-";
                        DR3["Column5"] = "Category-";
                        DR3["Column6"] = "SizeType-";
                        DR3["Column7"] = "Smp.Avg/Qty-";
                        DR3["Column8"] = "UOM-";
                        DR3["Column9"] = "Avg/Qty.Changed To-";
                        DR3["Column10"] = "";

                        DT.Rows.Add(DR3);

                        DataRow[] rows = dsStylesItems.Tables[0].Select("SamplingCostingId='" + DR_1["SamplingCostingId"] + "'");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            DataRow DR4 = DT.NewRow();
                            DR4["Column3"] = rows[i]["Itemgroupname"].ToString();
                            DR4["Column4"] = rows[i]["Description"].ToString();
                            DR4["Column5"] = rows[i]["Category"].ToString();
                            DR4["Column6"] = rows[i]["SUOM"].ToString();
                            DR4["Column7"] = rows[i]["SmpAvg"].ToString();
                            DR4["Column8"] = rows[i]["IUOM"].ToString();
                            DR4["Column9"] = "";// rows[i]["Rate"].ToString();
                            DR4["Column10"] = "";// rows[i]["Cost"].ToString();
                            DT.Rows.Add(DR4);

                        }

                        DataRow DR55 = DT.NewRow();
                        DR55["Column1"] = "";
                        DR55["Column2"] = "";
                        DR55["Column3"] = "";
                        DR55["Column4"] = "";
                        DR55["Column5"] = "";
                        DR55["Column6"] = "";
                        DR55["Column7"] = "";
                        DR55["Column8"] = "";
                        DR55["Column9"] = "";
                        DR55["Column10"] = "";
                        DT.Rows.Add(DR55);

                        DataRow DR5 = DT.NewRow();
                        DR5["Column1"] = "Fabrication Cost:";
                        DR5["Column2"] = DR_1["FabricationCost"].ToString();
                        DR5["Column3"] = "";

                        DR5["Column4"] = "Piece Process Cost:";
                        DR5["Column5"] = DR_1["PieceProcessCost"].ToString();
                        DR5["Column6"] = "";

                        DR5["Column7"] = "";
                        DR5["Column8"] = "";
                        DR5["Column9"] = "";
                        DR5["Column10"] = "";
                        DT.Rows.Add(DR5);

                        DataRow DR6 = DT.NewRow();
                        DR6["Column1"] = "Embroidery[Machine] Cost:";
                        DR6["Column2"] = DR_1["EmbroideryMachineCost"].ToString();
                        DR6["Column3"] = "";

                        DR6["Column4"] = "Finishing and Packing Cost:";
                        DR6["Column5"] = DR_1["FinishingandPackingCost"].ToString();
                        DR6["Column6"] = "";

                        DR6["Column7"] = "";
                        DR6["Column8"] = "";
                        DR6["Column9"] = "";
                        DR6["Column10"] = "";
                        DT.Rows.Add(DR6);

                        DataRow DR7 = DT.NewRow();
                        DR7["Column1"] = "Embroidery[Hand]Cost:";
                        DR7["Column2"] = DR_1["EmbroideryHandCost"].ToString();
                        DR7["Column3"] = "";

                        DR7["Column4"] = "";
                        DR7["Column5"] = "";
                        DR7["Column6"] = "";

                        DR7["Column7"] = "";
                        DR7["Column8"] = "";
                        DR7["Column9"] = "";
                        DR7["Column10"] = "";
                        DT.Rows.Add(DR7);

                        DataRow DR8 = DT.NewRow();
                        DR8["Column1"] = "";
                        DR8["Column2"] = "";
                        DR8["Column3"] = "";
                        DR8["Column4"] = "";
                        DR8["Column5"] = "";
                        DR8["Column6"] = "";
                        DR8["Column7"] = "";
                        DR8["Column8"] = "";
                        DR8["Column9"] = "";
                        DR8["Column10"] = "";
                        DT.Rows.Add(DR8);

                        DataRow DR9 = DT.NewRow();
                        DR9["Column1"] = "-----------------------";
                        DR9["Column2"] = "-----------------------";
                        DR9["Column3"] = "-----------------------";
                        DR9["Column4"] = "-----------------------";
                        DR9["Column5"] = "-----------------------";
                        DR9["Column6"] = "-----------------------";
                        DR9["Column7"] = "-----------------------";
                        DR9["Column8"] = "-----------------------";
                        DR9["Column9"] = "-----------------------";
                        DR9["Column10"] = "-----------------------";
                        DT.Rows.Add(DR9);

                    }

                    #endregion

                    gvSamplingandCosting.ShowHeader = false;
                    gvSamplingandCosting.Caption = "Sampling & Costing Report";
                    gvSamplingandCosting.DataSource = DT;
                    gvSamplingandCosting.DataBind();
                }
                else
                {
                    gvSamplingandCosting.DataSource = null;
                    gvSamplingandCosting.DataBind();
                }

                #endregion
            }
            else if (ddlReportType.SelectedValue == "3")
            {
                #region

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string SamplingCostingId = "";
                string IsFirst = "Yes";
                foreach (ListItem listItem in chkStyleNo.Items)
                {
                    #region
                    if (chkStyleNo.SelectedIndex < 0)
                    {
                        if (IsFirst == "Yes")
                        {
                            SamplingCostingId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            SamplingCostingId = SamplingCostingId + "," + listItem.Value;
                        }
                    }
                    else
                    {
                        if (listItem.Selected)
                        {
                            if (IsFirst == "Yes")
                            {
                                SamplingCostingId = listItem.Value;
                                IsFirst = "No";
                            }
                            else
                            {
                                SamplingCostingId = SamplingCostingId + "," + listItem.Value;
                            }
                        }
                    }
                    #endregion
                }


                DataSet dsCompanyDetails = objBs.GetCompanyDetailsAll();

                DataSet dsSummaryStyle = objBs.SamplingandCosting_SummaryStyle_Report(SamplingCostingId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);

                DataSet dsSummary = objBs.SamplingandCosting_Summary_Report(SamplingCostingId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
                if (dsSummary.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable DT = new DataTable();
                    DT.Columns.Add(new DataColumn("Column1"));
                    DT.Columns.Add(new DataColumn("Column2"));
                    DT.Columns.Add(new DataColumn("Column3"));
                    DT.Columns.Add(new DataColumn("Column4"));


                    DataRow DRC1 = DT.NewRow();
                    DRC1["Column3"] = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();
                    DT.Rows.Add(DRC1);

                    DataRow DRC2 = DT.NewRow();
                    DRC2["Column3"] = dsCompanyDetails.Tables[0].Rows[0]["SubName"].ToString();
                    DT.Rows.Add(DRC2);

                    DataRow DRC3 = DT.NewRow();
                    DRC3["Column3"] = dsCompanyDetails.Tables[0].Rows[0]["Address"].ToString();
                    DT.Rows.Add(DRC3);

                    DataRow DRC4 = DT.NewRow();
                    DRC4["Column3"] = dsCompanyDetails.Tables[0].Rows[0]["Area"].ToString() + " " + dsCompanyDetails.Tables[0].Rows[0]["City"].ToString() + " - " + dsCompanyDetails.Tables[0].Rows[0]["Pincode"].ToString() + "  (" + dsCompanyDetails.Tables[0].Rows[0]["Name"].ToString() + ") ";
                    DT.Rows.Add(DRC4);

                    DataRow DRC5 = DT.NewRow();
                    DRC5["Column3"] = "Ph.: " + dsCompanyDetails.Tables[0].Rows[0]["PhoneNo"].ToString();
                    DT.Rows.Add(DRC5);

                    DataRow DRC6 = DT.NewRow();
                    DRC6["Column3"] = "Fax : " + dsCompanyDetails.Tables[0].Rows[0]["Fax"].ToString() + " " + "Mob. : " + dsCompanyDetails.Tables[0].Rows[0]["MobileNo"].ToString();
                    DT.Rows.Add(DRC6);

                    DataRow DRC7 = DT.NewRow();
                    DRC7["Column3"] = "E-mail : " + dsCompanyDetails.Tables[0].Rows[0]["Email"].ToString();
                    DT.Rows.Add(DRC7);

                    DataRow DRE = DT.NewRow();
                    DRE["Column1"] = "";
                    DRE["Column2"] = "";
                    DRE["Column3"] = "";
                    DRE["Column4"] = "";
                    DT.Rows.Add(DRE);

                    foreach (DataRow DR_1 in dsSummary.Tables[0].Rows)
                    {
                        DataRow DR1 = DT.NewRow();
                        DR1["Column1"] = "Buyer Name:";
                        DR1["Column2"] = DR_1["LedgerName"].ToString();
                        DR1["Column3"] = "";
                        DR1["Column4"] = "";
                        DT.Rows.Add(DR1);

                        DataRow DR2 = DT.NewRow();
                        DR2["Column1"] = "Style No.";
                        DR2["Column2"] = "Descriptions";
                        DR2["Column3"] = "";
                        DR2["Column4"] = "Cost Per Piece in " + DR_1["CurrencyName"].ToString();
                        DT.Rows.Add(DR2);

                        DataRow[] rows = dsSummaryStyle.Tables[0].Select("SamplingCostingId='" + DR_1["SamplingCostingId"] + "'");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            DataRow DR4 = DT.NewRow();
                            DR4["Column1"] = rows[i]["StyleNo"].ToString();
                            DR4["Column2"] = rows[i]["Description"].ToString();
                            DR4["Column4"] = rows[i]["TotalSmpCostOther"].ToString();
                            DT.Rows.Add(DR4);

                        }

                        DataRow DR8 = DT.NewRow();
                        DR8["Column1"] = "";
                        DR8["Column2"] = "";
                        DR8["Column3"] = "";
                        DR8["Column4"] = "";
                        DT.Rows.Add(DR8);

                        DataRow DR9 = DT.NewRow();
                        DR9["Column1"] = "";
                        DR9["Column2"] = "";
                        DR9["Column3"] = "";
                        DR9["Column4"] = "";
                        DT.Rows.Add(DR9);

                    }

                    #endregion

                    gvSamplingandCosting.ShowHeader = false;
                    gvSamplingandCosting.Caption = "Sampling & Costing Report Summary";
                    gvSamplingandCosting.DataSource = DT;
                    gvSamplingandCosting.DataBind();
                }
                else
                {
                    gvSamplingandCosting.DataSource = null;
                    gvSamplingandCosting.DataBind();
                }

                #endregion
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= SamplingandCostingReport.xls");
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

        public void ExportToExcel(string filename, DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.Caption = "Buyer Order Details";
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.ShowHeader = false;
                dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.HeaderStyle.Font.Bold = true;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }

    }
}


