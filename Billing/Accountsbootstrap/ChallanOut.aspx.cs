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
    public partial class ChallanOut : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        int Issued = 0; int Received = 0; int Damaged = 0;

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

                DataSet dsProcess = objBs.gridProcess();
                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    ddlProcess.DataSource = dsProcess.Tables[0];
                    ddlProcess.DataTextField = "Process";
                    ddlProcess.DataValueField = "ProcessID";
                    ddlProcess.DataBind();
                    ddlProcess.Items.Insert(0, "All");
                }

                DataSet dsProcessLedger = objBs.GetApprovedProcessLedger();
                if (dsProcessLedger.Tables[0].Rows.Count > 0)
                {
                    ddlProcessLedger.DataSource = dsProcessLedger.Tables[0];
                    ddlProcessLedger.DataTextField = "LedgerName";
                    ddlProcessLedger.DataValueField = "LedgerID";
                    ddlProcessLedger.DataBind();
                    ddlProcessLedger.Items.Insert(0, "All");
                }

                DataSet dsExcNo = objBs.getAllExcNo("All");
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExcNo.Tables[0];
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderId";
                    chkExcNo.DataBind();
                }

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

            string ProcessType = "ProcessType='Issued'";
            DataSet ds = objBs.ChallanOut(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
            DataSet ds1 = objBs.ChallanOut1(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
            DataSet ds2 = objBs.ChallanOut2(ProcessType, BuyerOrderId, chkUseDate.Checked, From, To, ddlProcessLedger.SelectedValue, ddlProcess.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable DT = new DataTable();
                DT.Columns.Add(new DataColumn("Challan"));
                DT.Columns.Add(new DataColumn("ChallanNo"));
                DT.Columns.Add(new DataColumn("Name"));
                DT.Columns.Add(new DataColumn("Process"));
                DT.Columns.Add(new DataColumn("ExcNo"));
                DT.Columns.Add(new DataColumn("StyleNo"));
                DT.Columns.Add(new DataColumn("StyleDescription"));
                DT.Columns.Add(new DataColumn("Color"));
                DT.Columns.Add(new DataColumn("IsuType"));
                DT.Columns.Add(new DataColumn("TtlQty"));

                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    DataRow DRM = DT.NewRow();
                    DRM["Challan"] = Convert.ToDateTime(dr["DefaultDate"]).ToString("dd/MM/yyyy");
                    DT.Rows.Add(DRM);

                    int GrandTtlQty = 0;
                    DataRow[] Rows1 = ds2.Tables[0].Select("Date ='" + dr["Date"] + "' ");
                    for (int j = 0; j < Rows1.Length; j++)
                    {
                        int TtlQty = 0;
                        DataRow[] Rows = ds.Tables[0].Select("Date ='" + dr["Date"] + "' and Process ='" + Rows1[j]["Process"] + "' ");
                        for (int i = 0; i < Rows.Length; i++)
                        {
                            DataRow DRM1 = DT.NewRow();
                            DRM1["Challan"] = Convert.ToDateTime(Rows[i]["DefaultDate"]).ToString("dd/MM/yyyy");
                            DRM1["ChallanNo"] = Rows[i]["FullEntryNo"];
                            DRM1["Name"] = Rows[i]["LedgerName"];
                            DRM1["Process"] = Rows[i]["Process"];
                            DRM1["ExcNo"] = Rows[i]["ExcNo"];
                            DRM1["StyleNo"] = Rows[i]["StyleNo"];
                            DRM1["StyleDescription"] = Rows[i]["Description"];
                            DRM1["Color"] = Rows[i]["Color"];
                            DRM1["IsuType"] = "";
                            DRM1["TtlQty"] = Rows[i]["Issued"];
                            TtlQty += Convert.ToInt32(Rows[i]["Issued"]);
                            GrandTtlQty += Convert.ToInt32(Rows[i]["Issued"]);
                            DT.Rows.Add(DRM1);
                        }

                        DataRow DRM2 = DT.NewRow();
                        DRM2["TtlQty"] = TtlQty;
                        DT.Rows.Add(DRM2);
                    }

                    DataRow DRM3 = DT.NewRow();
                    DRM3["Color"] = "Total Pcs. :";
                    DRM3["TtlQty"] = GrandTtlQty;
                    DT.Rows.Add(DRM3);

                }

                gvChallanOut.DataSource = DT;
                gvChallanOut.DataBind();

            }
            else
            {
                gvChallanOut.DataSource = null;
                gvChallanOut.DataBind();
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= ChallanOut.xls");
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

        protected void gvChallanOut_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Issued += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Issued"));
                Received += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Received"));
                Damaged += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Damaged"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[9].Text = "Total:";
                e.Row.Cells[10].Text = Issued.ToString();
                e.Row.Cells[11].Text = Received.ToString();
                e.Row.Cells[12].Text = Damaged.ToString();
            }
        }
    }
}


