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
    public partial class ProductionReport : System.Web.UI.Page
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

                }

                DataSet dsExcNo = objBs.getAllExcNo(ddlBuyerCode.SelectedValue);
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExcNo.Tables[0];
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderId";
                    chkExcNo.DataBind();
                }

            }
        }

        protected void buyer_order(object sender, EventArgs e)
        {
            DataSet dsExcNo = objBs.getAllExcNo(ddlBuyerCode.SelectedValue);
            if (dsExcNo.Tables[0].Rows.Count > 0)
            {
                chkExcNo.DataSource = dsExcNo.Tables[0];
                chkExcNo.DataTextField = "ExcNo";
                chkExcNo.DataValueField = "BuyerOrderId";
                chkExcNo.DataBind();


            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string BuyerOrderId = "";
            string IsFirst = "Yes";

            foreach (ListItem listItem in chkExcNo.Items)
            {
                #region
                if (chkExcNo.SelectedIndex < 0)
                {
                    if (IsFirst == "Yes")
                    {
                        BuyerOrderId = listItem.Value;
                        IsFirst = "No";
                    }
                    else
                    {
                        BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                    }
                }
                else
                {
                    if (listItem.Selected)
                    {
                        if (IsFirst == "Yes")
                        {
                            BuyerOrderId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                        }
                    }
                }

                #endregion
            }

            string ProductionQty = ""; string ReportType = "";
            if (ddlReportType.SelectedValue == "1")
            {
                ProductionQty = "Sum(Issued) as ProductionQty";
                ReportType = "Issued";
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                ProductionQty = "Sum(Received) as ProductionQty";
                ReportType = "Received";
            }
            else if (ddlReportType.SelectedValue == "3")
            {
                ProductionQty = "Sum(Damaged) as ProductionQty";
                ReportType = "Received";
            }

            DateTime CFD = new DateTime(); DateTime CLD = new DateTime(); DateTime PFD = new DateTime(); DateTime PLD = new DateTime();

            DataSet dsCQty = objBs.getCuttingProcessQty(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
            DataSet dsPQty = objBs.getProcessQty(ProductionQty, ReportType, BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
            if (dsCQty.Tables[0].Rows.Count > 0)
            {
                CFD = dsCQty.Tables[0].AsEnumerable()
                  .Select(cols => cols.Field<DateTime>(dsCQty.Tables[0].Columns[0].ColumnName))
                  .OrderBy(p => p.Ticks)
                  .FirstOrDefault();
                CLD = dsCQty.Tables[0].AsEnumerable()
                 .Select(cols => cols.Field<DateTime>(dsCQty.Tables[0].Columns[0].ColumnName))
                 .OrderByDescending(p => p.Ticks)
                 .FirstOrDefault();
            }
            else
            {
                if (dsPQty.Tables[0].Rows.Count > 0)
                {
                    CFD = dsPQty.Tables[0].AsEnumerable()
                .Select(cols => cols.Field<DateTime>(dsPQty.Tables[0].Columns[0].ColumnName))
                .OrderBy(p => p.Ticks)
                .FirstOrDefault();
                    CLD = dsPQty.Tables[0].AsEnumerable()
                     .Select(cols => cols.Field<DateTime>(dsPQty.Tables[0].Columns[0].ColumnName))
                     .OrderByDescending(p => p.Ticks)
                     .FirstOrDefault();
                }
            }

            if (dsPQty.Tables[0].Rows.Count > 0)
            {
                PFD = dsPQty.Tables[0].AsEnumerable()
                 .Select(cols => cols.Field<DateTime>(dsPQty.Tables[0].Columns[0].ColumnName))
                 .OrderBy(p => p.Ticks)
                 .FirstOrDefault();
                PLD = dsPQty.Tables[0].AsEnumerable()
                 .Select(cols => cols.Field<DateTime>(dsPQty.Tables[0].Columns[0].ColumnName))
                 .OrderByDescending(p => p.Ticks)
                 .FirstOrDefault();
            }
            else
            {
                if (dsCQty.Tables[0].Rows.Count > 0)
                {
                    PFD = dsCQty.Tables[0].AsEnumerable()
                      .Select(cols => cols.Field<DateTime>(dsCQty.Tables[0].Columns[0].ColumnName))
                      .OrderBy(p => p.Ticks)
                      .FirstOrDefault();
                    PLD = dsCQty.Tables[0].AsEnumerable()
                     .Select(cols => cols.Field<DateTime>(dsCQty.Tables[0].Columns[0].ColumnName))
                     .OrderByDescending(p => p.Ticks)
                     .FirstOrDefault();
                }
            }

            DateTime Min = new DateTime();
            DateTime Max = new DateTime();

            DateTime C_FD = DateTime.ParseExact(Convert.ToDateTime(CFD).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime P_FD = DateTime.ParseExact(Convert.ToDateTime(PFD).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (C_FD < P_FD)
            {
                Min = C_FD;
            }
            else
            {
                Min = P_FD;
            }

            DateTime C_LD = DateTime.ParseExact(Convert.ToDateTime(CLD).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime P_LD = DateTime.ParseExact(Convert.ToDateTime(PLD).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (C_LD < P_LD)
            {
                Max = P_LD;
            }
            else
            {
                Max = C_LD;
            }

            int TotalDays = Convert.ToInt32((Max - Min).TotalDays);

            #region

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Date"));

            DataSet dsP1 = objBs.GetProcessforOne(Convert.ToInt32(lblProcessforMasterId.Text));
            dt.Columns.Add(new DataColumn(dsP1.Tables[0].Rows[0]["Process"].ToString()));
            string CuttingProcess = dsP1.Tables[0].Rows[0]["Process"].ToString();

            DataSet dsP2 = objBs.getProcessEntryProcess(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
            foreach (DataRow dr in dsP2.Tables[0].Rows)
            {
                dt.Columns.Add(new DataColumn(dr["Process"].ToString()));
            }

            for (int i = 0; i <= TotalDays; i++)
            {
                DataRow DRM = dt.NewRow();

                DRM["Date"] = Convert.ToDateTime(Min.AddDays(i)).ToString("dd/MM/yyyy");

                string MinDate = Convert.ToDateTime(Min.AddDays(i)).ToString("dd/MM/yyyy");
                DataRow[] RowsCQty = dsCQty.Tables[0].Select("CDate ='" + MinDate + "'  ");
                if (RowsCQty.Length > 0)
                {
                    DRM[CuttingProcess] = RowsCQty[0]["RecQty"];
                }
                else
                {
                    DRM[CuttingProcess] = "0";
                }

                foreach (DataRow DRP in dsP2.Tables[0].Rows)
                {
                    string Process = DRP["Process"].ToString();
                    string ProcessId = DRP["ProcessId"].ToString();

                    DataRow[] RowsQty = dsPQty.Tables[0].Select("Process='" + ProcessId + "' and RecDate ='" + MinDate + "'  ");
                    if (RowsQty.Length > 0)
                    {
                        DRM[Process] = RowsQty[0]["ProductionQty"];
                    }
                    else
                    {
                        DRM[Process] = "0";
                    }
                }

                dt.Rows.Add(DRM);
            }

            #endregion

            if (dt.Rows.Count > 0)
            {
                gvProductionReport.ShowHeader = true;
                gvProductionReport.Caption = "Production " + ddlReportType.SelectedItem.Text + " Qty Report";
                gvProductionReport.DataSource = dt;
                gvProductionReport.DataBind();

                //int total = dt.AsEnumerable().Sum(row => row.Field<int>("Ironing"));
                //gvProductionReport.FooterRow.Cells[1].Text = "Total";
                //gvProductionReport.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                //gvProductionReport.FooterRow.Cells[3].Text = total.ToString("N2");
            }
            else
            {
                gvProductionReport.DataSource = null;
                gvProductionReport.DataBind();
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= Production " + ddlReportType.SelectedItem.Text + " Qty Report.xls");
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

    }
}


