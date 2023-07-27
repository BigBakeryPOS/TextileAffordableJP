using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class CuttingProcessEntryOrder : System.Web.UI.Page
    {

        BSClass objBs = new BSClass();
        string sTableName = "";
        string YearCode = "";

        int TotQty = 0;
        int TotIQty = 0;
        int TotRQty = 0;
        int TotDQty = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            //YearCode = Session["YearCode"].ToString();
            YearCode = Request.Cookies["userInfo"]["YearCode"].ToString();
            if (!IsPostBack)
            {
                DataSet dsEntryNo = objBs.GetCuttingProcessEntryNo(YearCode);
                string EntryNo = dsEntryNo.Tables[0].Rows[0]["EntryNo"].ToString().PadLeft(4, '0');
                txtEntryNo.Text = "PRP-" + EntryNo + '/' + YearCode;

                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsProcessLedger = objBs.GetApprovedProcessLedger();
                if (dsProcessLedger.Tables[0].Rows.Count > 0)
                {
                    ddlProcessLedger.DataSource = dsProcessLedger.Tables[0];
                    ddlProcessLedger.DataTextField = "LedgerName";
                    ddlProcessLedger.DataValueField = "LedgerID";
                    ddlProcessLedger.DataBind();
                    ddlProcessLedger.Items.Insert(0, "Select Jobwork Ledger");
                }
                else
                {
                    ddlProcessLedger.Items.Insert(0, "Select Jobwork Ledger");
                }

                DataSet dsExcNo = objBs.GetExcNo_ItemDetails();
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderMasterCuttingId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }
                else
                {
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }

                //ddlProcessFrom.Items.Insert(0, "Select ProcessFrom");
                ddlProcess.Items.Insert(0, "Select Process");

                DataSet dsFinishingProcess = objBs.gridProcess();
                if (dsFinishingProcess.Tables[0].Rows.Count > 0)
                {
                    ddlFinishingProcess.DataSource = dsFinishingProcess.Tables[0];
                    ddlFinishingProcess.DataTextField = "Process";
                    ddlFinishingProcess.DataValueField = "ProcessId";
                    ddlFinishingProcess.DataBind();
                    ddlFinishingProcess.Items.Insert(0, "FinishingProcess");
                }
                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompanyName.DataSource = dsCompany.Tables[0];
                    ddlCompanyName.DataTextField = "CompanyName";
                    ddlCompanyName.DataValueField = "ComapanyID";
                    ddlCompanyName.DataBind();
                    ddlCompanyName.Items.Insert(0, "CompanyName");
                }

                btnupdaterate.Visible = false;
                btnSubmitQty.Visible = true;
                string ProcessEntryId = Request.QueryString.Get("ProcessEntryId");
                if (ProcessEntryId != "" && ProcessEntryId != null)
                {
                    string TPE = Request.QueryString.Get("TPE");
                    if (TPE == "REC")
                    {
                        ChallanNo.Visible = true;
                        #region

                        DataSet dsBuyerOrderMasterCutting = objBs.GetCuttingProcessEntry_Receive(Convert.ToInt32(ProcessEntryId));
                        if (dsBuyerOrderMasterCutting.Tables[0].Rows.Count > 0)
                        {
                            btnSave.Text = "Receive";

                            txtEntryNo.Text = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["FullEntryNo"].ToString();
                            txtEntryDate.Text = Convert.ToDateTime(dsBuyerOrderMasterCutting.Tables[0].Rows[0]["EntryDate"]).ToString("dd/MM/yyyy");

                           // txtFromDate.Text = Convert.ToDateTime(dsBuyerOrderMasterCutting.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                           // txtToDate.Text = Convert.ToDateTime(dsBuyerOrderMasterCutting.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                            ddlFinishingProcess.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["FinishingProcessId"].ToString();
                            ddlCompanyName.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["CompanyId"].ToString();

                            ddlProcessLedger.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["ProcessLedger"].ToString();
                            ddlProcessLedger.Enabled = false;

                            #region ExcNo ReLoad

                            DataSet dsExcNo1 = objBs.GetExcNo_ItemDetailsReceive();
                            if (dsExcNo1.Tables[0].Rows.Count > 0)
                            {
                                ddlExcNo.DataSource = dsExcNo1.Tables[0];
                                ddlExcNo.DataTextField = "ExcNo";
                                ddlExcNo.DataValueField = "BuyerOrderMasterCuttingId";
                                ddlExcNo.DataBind();
                                ddlExcNo.Items.Insert(0, "Select ExcNo");
                            }
                            else
                            {
                                ddlExcNo.Items.Insert(0, "Select ExcNo");
                            }

                            #endregion
                            ddlExcNo.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["BuyerOrderMasterCuttingId"].ToString();
                            ddlExcNo.Enabled = false;

                            #region ProcessFrom ReLoad
                            //DataSet dsProcessFrom = objBs.GetExcNo_ProcessFromReceive(Convert.ToInt32(ddlExcNo.SelectedValue));
                            //if (dsProcessFrom.Tables[0].Rows.Count > 0)
                            //{
                            //    ddlProcessFrom.Items.Clear();
                            //    ddlProcessFrom.DataSource = dsProcessFrom.Tables[0];
                            //    ddlProcessFrom.DataTextField = "Process";
                            //    ddlProcessFrom.DataValueField = "ProcessId";
                            //    ddlProcessFrom.DataBind();
                            //    ddlProcessFrom.Items.Insert(0, "Select ProcessFrom");
                            //}
                            //else
                            //{
                            //    ddlProcessFrom.Items.Clear();
                            //    ddlProcessFrom.Items.Insert(0, "Select ProcessFrom");
                            //}
                            #endregion
                            //ddlProcessFrom.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["ProcessFrom"].ToString();
                            //ddlProcessFrom.Enabled = false;

                            #region Process ReLoad
                            //DataSet dsProcess = objBs.GetExcNo_Process(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlProcessFrom.SelectedValue));
                            //if (dsProcess.Tables[0].Rows.Count > 0)
                            //{
                            //    ddlProcess.Items.Clear();
                            //    ddlProcess.DataSource = dsProcess.Tables[0];
                            //    ddlProcess.DataTextField = "Process";
                            //    ddlProcess.DataValueField = "ProcessId";
                            //    ddlProcess.DataBind();
                            //    ddlProcess.Items.Insert(0, "Select Process");
                            //}
                            #endregion
                            ddlProcess.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["Process"].ToString();
                            ddlProcess.Enabled = false;

                            txtRemarks.Text = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["Remarks"].ToString();

                            DataSet dsCuttingProcessEntryItems = objBs.GetTransCuttingProcessEntryItemsReceive(Convert.ToInt32(ProcessEntryId));
                            if (dsCuttingProcessEntryItems.Tables[0].Rows.Count > 0)
                            {
                                DataSet dstd = new DataSet();
                                DataTable dtddd = new DataTable();
                                DataRow drNew;
                                DataColumn dct;
                                DataTable dttt = new DataTable();

                                #region

                                dct = new DataColumn("TransItemId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("RowId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("StyleNo");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("StyleNoId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("Description");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("ColorId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("Color");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("Rate");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("RangeId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("Size");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("Qty");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("IssueQty");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("ReceiveQty");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("DamageQty");
                                dttt.Columns.Add(dct);

                                dstd.Tables.Add(dttt);

                                foreach (DataRow Dr in dsCuttingProcessEntryItems.Tables[0].Rows)
                                {
                                    drNew = dttt.NewRow();

                                    drNew["TransItemId"] = Dr["TransItemId"];
                                    drNew["RowId"] = Dr["RowId"];
                                    drNew["StyleNo"] = Dr["StyleNo"];
                                    drNew["StyleNoId"] = Dr["StyleNoId"];
                                    drNew["Description"] = Dr["Description"];
                                    drNew["ColorId"] = Dr["ColorId"];
                                    drNew["Color"] = Dr["Color"];
                                    drNew["Rate"] = Dr["Rate"];
                                    drNew["RangeId"] = Dr["RangeId"];
                                    drNew["Size"] = Dr["Range"];

                                    drNew["Qty"] = Dr["Qty"];

                                    drNew["IssueQty"] = 0;
                                    drNew["ReceiveQty"] = 0;
                                    drNew["DamageQty"] = 0;

                                    dstd.Tables[0].Rows.Add(drNew);
                                    dtddd = dstd.Tables[0];
                                }

                                #endregion

                                ViewState["CurrentTable1"] = dtddd;
                                GVItem.DataSource = dtddd;
                                GVItem.DataBind();
                            }

                            DataSet dsCuttingProcessEntrySizes = objBs.GetTransCuttingProcessEntrySizesReceive(Convert.ToInt32(ProcessEntryId));
                            if (dsCuttingProcessEntrySizes.Tables[0].Rows.Count > 0)
                            {
                                DataSet dstd1 = new DataSet();
                                DataTable dtddd1 = new DataTable();
                                DataRow drNew1;
                                DataColumn dct1;
                                DataTable dttt1 = new DataTable();

                                #region

                                dct1 = new DataColumn("TransSizeId");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("RowId");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("SizeId");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("Size");
                                dttt1.Columns.Add(dct1);

                                dct1 = new DataColumn("Qty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("IssueQty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("ReceiveQty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("DamageQty");
                                dttt1.Columns.Add(dct1);

                                dstd1.Tables.Add(dttt1);

                                foreach (DataRow Dr in dsCuttingProcessEntrySizes.Tables[0].Rows)
                                {
                                    drNew1 = dttt1.NewRow();

                                    drNew1["TransSizeId"] = Dr["TransSizeId"];
                                    drNew1["RowId"] = Dr["RowId"];
                                    drNew1["SizeId"] = Dr["SizeId"];
                                    drNew1["Size"] = Dr["Size"];
                                    drNew1["Qty"] = Dr["Qty"];
                                    drNew1["IssueQty"] = 0;
                                    drNew1["ReceiveQty"] = 0;
                                    drNew1["DamageQty"] = 0;

                                    dstd1.Tables[0].Rows.Add(drNew1);
                                    dtddd1 = dstd1.Tables[0];

                                }

                                #endregion

                                ViewState["CurrentTable2"] = dtddd1;
                            }

                        }

                        #endregion
                    }
                    else if (TPE == "VIEW")
                    {
                        btnSave.Text = "Update Rate";
                        btnSave.Enabled = true;
                        btnSubmitQty.Enabled = false;
                        btnSubmitQty.Visible = false;
                        btnupdaterate.Visible = true;

                        #region

                        DataSet dsBuyerOrderMasterCutting = objBs.GetCuttingProcessEntry_Receive(Convert.ToInt32(ProcessEntryId));
                        if (dsBuyerOrderMasterCutting.Tables[0].Rows.Count > 0)
                        {
                           // btnSave.Text = "Receive";

                            ddlFinishingProcess.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["FinishingProcessId"].ToString();
                            ddlCompanyName.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["CompanyId"].ToString();

                            ddlProcessLedger.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["ProcessLedger"].ToString();
                            ddlProcessLedger.Enabled = false;

                            txtEntryNo.Text = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["FullEntryNo"].ToString();
                            txtEntryDate.Text = Convert.ToDateTime(dsBuyerOrderMasterCutting.Tables[0].Rows[0]["EntryDate"]).ToString("dd/MM/yyyy");

                            //txtFromDate.Text = Convert.ToDateTime(dsBuyerOrderMasterCutting.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                            //txtToDate.Text = Convert.ToDateTime(dsBuyerOrderMasterCutting.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                            #region ExcNo ReLoad

                            DataSet dsExcNo1 = objBs.GetExcNo_ItemDetailsReceive();
                            if (dsExcNo1.Tables[0].Rows.Count > 0)
                            {
                                ddlExcNo.DataSource = dsExcNo1.Tables[0];
                                ddlExcNo.DataTextField = "ExcNo";
                                ddlExcNo.DataValueField = "BuyerOrderMasterCuttingId";
                                ddlExcNo.DataBind();
                                ddlExcNo.Items.Insert(0, "Select ExcNo");
                            }
                            else
                            {
                                ddlExcNo.Items.Insert(0, "Select ExcNo");
                            }

                            #endregion
                            ddlExcNo.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["BuyerOrderMasterCuttingId"].ToString();
                            ddlExcNo.Enabled = false;

                            #region ProcessFrom ReLoad
                            //DataSet dsProcessFrom = objBs.GetExcNo_ProcessFromReceive(Convert.ToInt32(ddlExcNo.SelectedValue));
                            //if (dsProcessFrom.Tables[0].Rows.Count > 0)
                            //{
                            //    ddlProcessFrom.Items.Clear();
                            //    ddlProcessFrom.DataSource = dsProcessFrom.Tables[0];
                            //    ddlProcessFrom.DataTextField = "Process";
                            //    ddlProcessFrom.DataValueField = "ProcessId";
                            //    ddlProcessFrom.DataBind();
                            //    ddlProcessFrom.Items.Insert(0, "Select ProcessFrom");
                            //}
                            //else
                            //{
                            //    ddlProcessFrom.Items.Clear();
                            //    ddlProcessFrom.Items.Insert(0, "Select ProcessFrom");
                            //}
                            #endregion
                            //ddlProcessFrom.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["ProcessFrom"].ToString();
                            //ddlProcessFrom.Enabled = false;

                            #region Process ReLoad
                            //DataSet dsProcess = objBs.GetExcNo_Process(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlProcessFrom.SelectedValue));
                            //if (dsProcess.Tables[0].Rows.Count > 0)
                            //{
                            //    ddlProcess.Items.Clear();
                            //    ddlProcess.DataSource = dsProcess.Tables[0];
                            //    ddlProcess.DataTextField = "Process";
                            //    ddlProcess.DataValueField = "ProcessId";
                            //    ddlProcess.DataBind();
                            //    ddlProcess.Items.Insert(0, "Select Process");
                            //}
                            #endregion
                            ddlProcess.SelectedValue = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["Process"].ToString();
                            ddlProcess.Enabled = false;

                            txtRemarks.Text = dsBuyerOrderMasterCutting.Tables[0].Rows[0]["Remarks"].ToString();

                            DataSet dsCuttingProcessEntryItems = objBs.GetTransCuttingProcessEntryItemsView(Convert.ToInt32(ProcessEntryId));
                            if (dsCuttingProcessEntryItems.Tables[0].Rows.Count > 0)
                            {
                                DataSet dstd = new DataSet();
                                DataTable dtddd = new DataTable();
                                DataRow drNew;
                                DataColumn dct;
                                DataTable dttt = new DataTable();

                                #region

                                dct = new DataColumn("TransItemId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("RowId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("StyleNo");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("StyleNoId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("Description");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("ColorId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("Color");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("Rate");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("RangeId");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("Size");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("Qty");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("IssueQty");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("ReceiveQty");
                                dttt.Columns.Add(dct);
                                dct = new DataColumn("DamageQty");
                                dttt.Columns.Add(dct);

                                dstd.Tables.Add(dttt);

                                foreach (DataRow Dr in dsCuttingProcessEntryItems.Tables[0].Rows)
                                {
                                    drNew = dttt.NewRow();

                                    drNew["TransItemId"] = Dr["TransItemId"];
                                    drNew["RowId"] = Dr["RowId"];
                                    drNew["StyleNo"] = Dr["StyleNo"];
                                    drNew["StyleNoId"] = Dr["StyleNoId"];
                                    drNew["Description"] = Dr["Description"];
                                    drNew["ColorId"] = Dr["ColorId"];
                                    drNew["Color"] = Dr["Color"];
                                    drNew["Rate"] = Dr["Rate"];
                                    drNew["RangeId"] = Dr["RangeId"];
                                    drNew["Size"] = Dr["Range"];

                                    drNew["Qty"] = Dr["Qty"];

                                    drNew["IssueQty"] = 0;
                                    drNew["ReceiveQty"] = Dr["TotalReceived"];
                                    drNew["DamageQty"] = Dr["TotalDamaged"];

                                    dstd.Tables[0].Rows.Add(drNew);
                                    dtddd = dstd.Tables[0];
                                }

                                #endregion

                                ViewState["CurrentTable1"] = dtddd;
                                GVItem.DataSource = dtddd;
                                GVItem.DataBind();
                            }

                            DataSet dsCuttingProcessEntrySizes = objBs.GetTransCuttingProcessEntrySizesView(Convert.ToInt32(ProcessEntryId));
                            if (dsCuttingProcessEntrySizes.Tables[0].Rows.Count > 0)
                            {
                                DataSet dstd1 = new DataSet();
                                DataTable dtddd1 = new DataTable();
                                DataRow drNew1;
                                DataColumn dct1;
                                DataTable dttt1 = new DataTable();

                                #region

                                dct1 = new DataColumn("TransSizeId");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("RowId");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("SizeId");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("Size");
                                dttt1.Columns.Add(dct1);

                                dct1 = new DataColumn("Qty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("IssueQty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("ReceiveQty");
                                dttt1.Columns.Add(dct1);
                                dct1 = new DataColumn("DamageQty");
                                dttt1.Columns.Add(dct1);

                                dstd1.Tables.Add(dttt1);

                                foreach (DataRow Dr in dsCuttingProcessEntrySizes.Tables[0].Rows)
                                {
                                    drNew1 = dttt1.NewRow();

                                    drNew1["TransSizeId"] = Dr["TransSizeId"];
                                    drNew1["RowId"] = Dr["RowId"];
                                    drNew1["SizeId"] = Dr["SizeId"];
                                    drNew1["Size"] = Dr["Size"];
                                    drNew1["Qty"] = Dr["Qty"];
                                    drNew1["IssueQty"] = 0;
                                    drNew1["ReceiveQty"] = Dr["TotalReceived"];
                                    drNew1["DamageQty"] = Dr["TotalDamaged"];

                                    dstd1.Tables[0].Rows.Add(drNew1);
                                    dtddd1 = dstd1.Tables[0];

                                }

                                #endregion

                                ViewState["CurrentTable2"] = dtddd1;
                            }



                        }

                        #endregion
                    }

                    GVItem.Columns[6].Visible = false;
                    GVSizes.Columns[3].Visible = false;
                    GVSizesView.Columns[3].Visible = false;
                }
            }
        }

        protected void GVSize_rowdatabound(object sender, GridViewRowEventArgs e)
        {

            int Qty = 0;
            int IQty = 0;
            int RQty = 0;
            int DQty = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQty = (Label)e.Row.FindControl("lblQty");

                TextBox txtIssueQty = (TextBox)e.Row.FindControl("txtIssueQty");
                TextBox txtReceiveQty = (TextBox)e.Row.FindControl("txtReceiveQty");
                TextBox txtDamageQty = (TextBox)e.Row.FindControl("txtDamageQty");

                if (lblQty.Text == "")
                    lblQty.Text = "0";

                if (txtIssueQty.Text == "")
                    txtIssueQty.Text = "0";

                if (txtReceiveQty.Text == "")
                    txtReceiveQty.Text = "0";

                if (txtDamageQty.Text == "")
                    txtDamageQty.Text = "0";

                TotQty += Convert.ToInt32(lblQty.Text);

                TotIQty += Convert.ToInt32(txtIssueQty.Text);
                TotRQty += Convert.ToInt32(txtReceiveQty.Text);
                TotDQty += Convert.ToInt32(txtDamageQty.Text);


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total Qty ";
                e.Row.Cells[2].Text = TotQty.ToString();
                e.Row.Cells[3].Text = TotIQty.ToString();
                e.Row.Cells[4].Text = TotRQty.ToString();
                e.Row.Cells[5].Text = TotDQty.ToString();
            }

        }

        protected void Issue_changed(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtIssueQty = (TextBox)row.FindControl("txtIssueQty");

            Calculations();
            txtIssueQty.Focus();
        }
        protected void Receive_changed(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtReceiveQty = (TextBox)row.FindControl("txtReceiveQty");

            Calculations();
            txtReceiveQty.Focus();
        }
        protected void Damage_changed(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtDamageQty = (TextBox)row.FindControl("txtDamageQty");

            Calculations();
            txtDamageQty.Focus();
        }

        public void Calculations()
        {
            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                Label lblQty = (Label)GVSizes.Rows[vLoop].FindControl("lblQty");

                TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                TextBox txtReceiveQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtReceiveQty");
                TextBox txtDamageQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDamageQty");

                if (txtIssueQty.Text == "")
                    txtIssueQty.Text = "0";
                if (txtReceiveQty.Text == "")
                    txtReceiveQty.Text = "0";
                if (txtDamageQty.Text == "")
                    txtDamageQty.Text = "0";


                TotQty += Convert.ToInt32(lblQty.Text);

                TotIQty += Convert.ToInt32(txtIssueQty.Text);
                TotRQty += Convert.ToInt32(txtReceiveQty.Text);
                TotDQty += Convert.ToInt32(txtDamageQty.Text);
            }


            GVSizes.FooterRow.Cells[2].Text = TotQty.ToString();
            GVSizes.FooterRow.Cells[3].Text = TotIQty.ToString();
            GVSizes.FooterRow.Cells[4].Text = TotRQty.ToString();
            GVSizes.FooterRow.Cells[5].Text = TotDQty.ToString();
        }

        protected void Issue_changed1(object sender, EventArgs e)
        {

            //GVSizes.DataBind();
            //GVSize_rowdatabound(sender,);
            int Qty = 0;
            int IQty = 0;
            int RQty = 0;
            int DQty = 0;

            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                Label lblQty = (Label)GVSizes.Rows[vLoop].FindControl("lblQty");

                TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                TextBox txtReceiveQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtReceiveQty");
                TextBox txtDamageQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDamageQty");

                if (txtIssueQty.Text == "")
                    txtIssueQty.Text = "0";
                if (txtReceiveQty.Text == "")
                    txtReceiveQty.Text = "0";
                if (txtDamageQty.Text == "")
                    txtDamageQty.Text = "0";


                TotQty += Convert.ToInt32(lblQty.Text);

                TotIQty += Convert.ToInt32(txtIssueQty.Text);
                TotRQty += Convert.ToInt32(txtReceiveQty.Text);
                TotDQty += Convert.ToInt32(txtDamageQty.Text);
            }


            GVSizes.FooterRow.Cells[2].Text = TotQty.ToString();
            GVSizes.FooterRow.Cells[3].Text = TotIQty.ToString();
            GVSizes.FooterRow.Cells[4].Text = TotRQty.ToString();
            GVSizes.FooterRow.Cells[5].Text = TotDQty.ToString();
        }
        protected void Receive_changed1(object sender, EventArgs e)
        {
            //GVSizes.DataBind();
            //GVSize_rowdatabound(sender,);
            int Qty = 0;
            int IQty = 0;
            int RQty = 0;
            int DQty = 0;

            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                Label lblQty = (Label)GVSizes.Rows[vLoop].FindControl("lblQty");

                TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                TextBox txtReceiveQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtReceiveQty");
                TextBox txtDamageQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDamageQty");

                if (txtIssueQty.Text == "")
                    txtIssueQty.Text = "0";
                if (txtReceiveQty.Text == "")
                    txtReceiveQty.Text = "0";
                if (txtDamageQty.Text == "")
                    txtDamageQty.Text = "0";


                TotQty += Convert.ToInt32(lblQty.Text);

                TotIQty += Convert.ToInt32(txtIssueQty.Text);
                TotRQty += Convert.ToInt32(txtReceiveQty.Text);
                TotDQty += Convert.ToInt32(txtDamageQty.Text);
            }


            GVSizes.FooterRow.Cells[2].Text = TotQty.ToString();
            GVSizes.FooterRow.Cells[3].Text = TotIQty.ToString();
            GVSizes.FooterRow.Cells[4].Text = TotRQty.ToString();
            GVSizes.FooterRow.Cells[5].Text = TotDQty.ToString();
        }
        protected void Damage_changed1(object sender, EventArgs e)
        {
            //GVSizes.DataBind();
            //GVSize_rowdatabound(sender,);
            int Qty = 0;
            int IQty = 0;
            int RQty = 0;
            int DQty = 0;

            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                Label lblQty = (Label)GVSizes.Rows[vLoop].FindControl("lblQty");

                TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                TextBox txtReceiveQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtReceiveQty");
                TextBox txtDamageQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDamageQty");

                if (txtIssueQty.Text == "")
                    txtIssueQty.Text = "0";
                if (txtReceiveQty.Text == "")
                    txtReceiveQty.Text = "0";
                if (txtDamageQty.Text == "")
                    txtDamageQty.Text = "0";


                TotQty += Convert.ToInt32(lblQty.Text);

                TotIQty += Convert.ToInt32(txtIssueQty.Text);
                TotRQty += Convert.ToInt32(txtReceiveQty.Text);
                TotDQty += Convert.ToInt32(txtDamageQty.Text);
            }


            GVSizes.FooterRow.Cells[2].Text = TotQty.ToString();
            GVSizes.FooterRow.Cells[3].Text = TotIQty.ToString();
            GVSizes.FooterRow.Cells[4].Text = TotRQty.ToString();
            GVSizes.FooterRow.Cells[5].Text = TotDQty.ToString();
        }

        protected void ddlExcNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            #region
            GVItem.DataSource = null;
            GVItem.DataBind();
            ViewState["CurrentTable1"] = null;

            GVSizes.DataSource = null;
            GVSizes.DataBind();
            ViewState["CurrentTable2"] = null;

            GVSizesView.DataSource = null;
            GVSizesView.DataBind();

            ddlProcess.Items.Clear();
            ddlProcess.Items.Insert(0, "Select Process");

            //ddlProcessFrom.Items.Clear();
            //ddlProcessFrom.Items.Insert(0, "Select ProcessFrom");

            GVFabricRawDetails.DataSource = null;
            GVFabricRawDetails.DataBind();

            #endregion

            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "0" && ddlExcNo.SelectedValue != "Select ExcNo")
            {

                DataSet dsMC = objBs.GetOrderMasterCuttingDetail(Convert.ToInt32(ddlExcNo.SelectedValue));
                ddlFinishingProcess.SelectedValue = dsMC.Tables[0].Rows[0]["FinishingProcessId"].ToString();
                ddlCompanyName.SelectedValue = dsMC.Tables[0].Rows[0]["CompanyId"].ToString();

                DataSet dsProcess = objBs.GetProcess_CuttingProcessEntryOrder(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    ddlProcess.Items.Clear();
                    ddlProcess.DataSource = dsProcess.Tables[0];
                    ddlProcess.DataTextField = "Process";
                    ddlProcess.DataValueField = "ProcessId";
                    ddlProcess.DataBind();
                    ddlProcess.Items.Insert(0, "Select Process");
                }
                else
                {
                    ddlProcess.Items.Clear();
                    ddlProcess.Items.Insert(0, "Select Process");      
                }
            }
            else
            {
                ddlProcess.Items.Clear();
                ddlProcess.Items.Insert(0, "Select Process");
            }
        }

        //protected void ddlProcessFrom_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    #region
        //    GVItem.DataSource = null;
        //    GVItem.DataBind();
        //    ViewState["CurrentTable1"] = null;

        //    GVSizes.DataSource = null;
        //    GVSizes.DataBind();
        //    ViewState["CurrentTable2"] = null;

        //    GVSizesView.DataSource = null;
        //    GVSizesView.DataBind();

        //    ddlProcess.Items.Clear();
        //    ddlProcess.Items.Insert(0, "Select Process");

        //    GVFabricRawDetails.DataSource = null;
        //    GVFabricRawDetails.DataBind();

        //    #endregion

        //    if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "0" && ddlExcNo.SelectedValue != "Select ExcNo" && ddlProcessFrom.SelectedValue != "" && ddlProcessFrom.SelectedValue != "0" && ddlProcessFrom.SelectedValue != "Select ProcessFrom")
        //    {
        //        DataSet dsExcNo = objBs.GetExcNo_Process(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlProcessFrom.SelectedValue));
        //        if (dsExcNo.Tables[0].Rows.Count > 0)
        //        {
        //            ddlProcess.Items.Clear();
        //            ddlProcess.DataSource = dsExcNo.Tables[0];
        //            ddlProcess.DataTextField = "Process";
        //            ddlProcess.DataValueField = "ProcessId";
        //            ddlProcess.DataBind();
        //            ddlProcess.Items.Insert(0, "Select Process");
        //        }

        //        #region

        //        DataSet dsCuttingProcessEntryrItems = objBs.GetTransCuttingProcessEntryItems(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlProcessFrom.SelectedValue));
        //        if (dsCuttingProcessEntryrItems.Tables[0].Rows.Count > 0)
        //        {
        //            DataSet dstd = new DataSet();
        //            DataTable dtddd = new DataTable();
        //            DataRow drNew;
        //            DataColumn dct;
        //            DataTable dttt = new DataTable();

        //            #region

        //            dct = new DataColumn("TransItemId");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("RowId");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("StyleNo");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("StyleNoId");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("Description");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("ColorId");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("Color");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("Rate");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("RangeId");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("Size");
        //            dttt.Columns.Add(dct);

        //            dct = new DataColumn("Qty");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("IssueQty");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("ReceiveQty");
        //            dttt.Columns.Add(dct);
        //            dct = new DataColumn("DamageQty");
        //            dttt.Columns.Add(dct);

        //            dstd.Tables.Add(dttt);

        //            foreach (DataRow Dr in dsCuttingProcessEntryrItems.Tables[0].Rows)
        //            {
        //                drNew = dttt.NewRow();

        //                drNew["TransItemId"] = Dr["TransItemId"];
        //                drNew["RowId"] = Dr["RowId"];
        //                drNew["StyleNo"] = Dr["StyleNo"];
        //                drNew["StyleNoId"] = Dr["StyleNoId"];
        //                drNew["Description"] = Dr["Description"];
        //                drNew["ColorId"] = Dr["ColorId"];
        //                drNew["Color"] = Dr["Color"];
        //                drNew["Rate"] = Dr["Rate"];
        //                drNew["RangeId"] = Dr["RangeId"];
        //                drNew["Size"] = Dr["Range"];

        //                drNew["Qty"] = Dr["Qty"];
        //                drNew["IssueQty"] = 0;
        //                drNew["ReceiveQty"] = 0;
        //                drNew["DamageQty"] = 0;

        //                dstd.Tables[0].Rows.Add(drNew);
        //                dtddd = dstd.Tables[0];
        //            }

        //            #endregion

        //            ViewState["CurrentTable1"] = dtddd;
        //            GVItem.DataSource = dtddd;
        //            GVItem.DataBind();

        //        }

        //        DataSet dsCuttingProcessEntrySizes = objBs.GetTransCuttingProcessEntrySizes(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlProcessFrom.SelectedValue));
        //        if (dsCuttingProcessEntrySizes.Tables[0].Rows.Count > 0)
        //        {
        //            DataSet dstd1 = new DataSet();
        //            DataTable dtddd1 = new DataTable();
        //            DataRow drNew1;
        //            DataColumn dct1;
        //            DataTable dttt1 = new DataTable();

        //            #region

        //            dct1 = new DataColumn("TransSizeId");
        //            dttt1.Columns.Add(dct1);
        //            dct1 = new DataColumn("RowId");
        //            dttt1.Columns.Add(dct1);
        //            dct1 = new DataColumn("SizeId");
        //            dttt1.Columns.Add(dct1);
        //            dct1 = new DataColumn("Size");
        //            dttt1.Columns.Add(dct1);

        //            dct1 = new DataColumn("Qty");
        //            dttt1.Columns.Add(dct1);
        //            dct1 = new DataColumn("IssueQty");
        //            dttt1.Columns.Add(dct1);
        //            dct1 = new DataColumn("ReceiveQty");
        //            dttt1.Columns.Add(dct1);
        //            dct1 = new DataColumn("DamageQty");
        //            dttt1.Columns.Add(dct1);

        //            dstd1.Tables.Add(dttt1);

        //            foreach (DataRow Dr in dsCuttingProcessEntrySizes.Tables[0].Rows)
        //            {
        //                drNew1 = dttt1.NewRow();

        //                drNew1["TransSizeId"] = Dr["TransSizeId"];
        //                drNew1["RowId"] = Dr["RowId"];
        //                drNew1["SizeId"] = Dr["SizeId"];
        //                drNew1["Size"] = Dr["Size"];
        //                drNew1["Qty"] = Dr["Qty"];
        //                drNew1["IssueQty"] = 0;
        //                drNew1["ReceiveQty"] = 0;
        //                drNew1["DamageQty"] = 0;

        //                dstd1.Tables[0].Rows.Add(drNew1);
        //                dtddd1 = dstd1.Tables[0];

        //            }

        //            #endregion

        //            ViewState["CurrentTable2"] = dtddd1;
        //        }

        //        GVItem.Columns[7].Visible = false;
        //        GVItem.Columns[8].Visible = false;

        //        GVSizes.Columns[4].Visible = false;
        //        GVSizes.Columns[5].Visible = false;

        //        GVSizesView.Columns[4].Visible = false;
        //        GVSizesView.Columns[5].Visible = false;

        //        #endregion
        //    }
        //    else
        //    {
        //        ddlProcess.Items.Clear();
        //        ddlProcess.Items.Insert(0, "Select Process");
        //    }

        //}

        protected void ddlProcess_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GVFabricRawDetails.DataSource = null;
            GVFabricRawDetails.DataBind();

            if (ddlProcess.SelectedValue != "" && ddlProcess.SelectedValue != "0" && ddlProcess.SelectedValue != "Select Process")
            {
                if (btnSave.Text == "Save")
                {
                    if (Convert.ToInt32(ddlProcess.SelectedValue) != Convert.ToInt32(lblProcessforMasterId.Text))
                    {
                        #region Summary

                        DataTable dtraw = new DataTable();
                        DataSet dsraw = new DataSet();
                        DataRow drraw;

                        dtraw.Columns.Add("Item");
                        dtraw.Columns.Add("ItemId");
                        dtraw.Columns.Add("Color");
                        dtraw.Columns.Add("ColorId");
                        dtraw.Columns.Add("AvlStock");
                        dtraw.Columns.Add("WantedRaw");

                        dsraw.Tables.Add(dtraw);

                        DataSet dsTotalFab = new DataSet();

                        for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                        {
                            HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
                            Label lblIssueQty = (Label)GVItem.Rows[vLoop].FindControl("lblIssueQty");

                            if (Convert.ToDouble(lblIssueQty.Text) > 0)
                            {
                                DataSet dsRaw = objBs.FabDetailsforProcessEntry(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(hdStyleNoId.Value), Convert.ToDouble(lblIssueQty.Text), Convert.ToInt32(ddlProcess.Text));
                                if (dsRaw.Tables[0].Rows.Count > 0)
                                {
                                    dsTotalFab.Merge(dsRaw);
                                }
                            }
                        }
                        if (dsTotalFab.Tables.Count > 0)
                        {
                            if (dsTotalFab.Tables[0].Rows.Count > 0)
                            {
                                DataTable dtraws = new DataTable();

                                dtraws = dsTotalFab.Tables[0];

                                var result1 = from r in dtraws.AsEnumerable()
                                              group r by new { Item = r["Description"], ItemId = r["ItemMasterId"], Color = r["Color"], ColorId = r["ColorId"], AvlStock = r["AvlStock"] } into raw
                                              select new
                                              {
                                                  Item = raw.Key.Item,
                                                  ItemId = raw.Key.ItemId,
                                                  Color = raw.Key.Color,
                                                  ColorId = raw.Key.ColorId,
                                                  AvlStock = raw.Key.AvlStock,
                                                  total = raw.Sum(x => Convert.ToDouble(x["WantedRaw"])),
                                              };


                                foreach (var g in result1)
                                {
                                    drraw = dtraw.NewRow();

                                    drraw["Item"] = g.Item;
                                    drraw["ItemId"] = g.ItemId;
                                    drraw["Color"] = g.Color;
                                    drraw["ColorId"] = g.ColorId;
                                    drraw["AvlStock"] = g.AvlStock;
                                    drraw["WantedRaw"] = (g.total).ToString("f3");

                                    dsraw.Tables[0].Rows.Add(drraw);
                                }

                                GVFabricRawDetails.DataSource = dsraw.Tables[0];
                                GVFabricRawDetails.DataBind();

                            }
                        }


                        #endregion
                    }
                }
            }
        }

        protected void GVItem_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                GVSizesView.DataSource = null;
                GVSizesView.DataBind();

                if (e.CommandArgument.ToString() != "")
                {
                    #region

                    DataTable DTGVSizeDetails = (DataTable)ViewState["CurrentTable1"];
                    DataRow[] RowsGVSizeDetails = DTGVSizeDetails.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    RowId.Text = e.CommandArgument.ToString();
                    TransItemId.Text = RowsGVSizeDetails[0]["TransItemId"].ToString();

                    StyleNo.Text = RowsGVSizeDetails[0]["StyleNo"].ToString();
                    StyleNoId.Text = RowsGVSizeDetails[0]["StyleNoId"].ToString();
                    Description.Text = RowsGVSizeDetails[0]["Description"].ToString();
                    Color.Text = RowsGVSizeDetails[0]["Color"].ToString();
                    ColorId.Text = RowsGVSizeDetails[0]["ColorId"].ToString();

                    Sizes.Text = RowsGVSizeDetails[0]["Size"].ToString();
                    RangeId.Text = RowsGVSizeDetails[0]["RangeId"].ToString();

                    Rate.Text = RowsGVSizeDetails[0]["Rate"].ToString();
                    txtrate.Text = Rate.Text;
                    Qty.Text = RowsGVSizeDetails[0]["Qty"].ToString();

                    IssueQty.Text = RowsGVSizeDetails[0]["IssueQty"].ToString();
                    ReceiveQty.Text = RowsGVSizeDetails[0]["ReceiveQty"].ToString();
                    DamageQty.Text = RowsGVSizeDetails[0]["DamageQty"].ToString();

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();
                    DataRow drNew1;
                    DataColumn dct1;
                    DataTable dttt1 = new DataTable();

                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("TransSizeId");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("IssueQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("ReceiveQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("DamageQty");
                    dttt1.Columns.Add(dct1);

                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["TransSizeId"] = RowsGVSizeQty[i]["TransSizeId"].ToString();

                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();

                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();
                        drNew1["IssueQty"] = RowsGVSizeQty[i]["IssueQty"].ToString();
                        drNew1["ReceiveQty"] = RowsGVSizeQty[i]["ReceiveQty"].ToString();
                        drNew1["DamageQty"] = RowsGVSizeQty[i]["DamageQty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    GVSizes.DataSource = dstd1;
                    GVSizes.DataBind();

                    #endregion
                }
            }
            else if (e.CommandName == "View")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    #region

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();

                    DataRow drNew1;
                    DataColumn dct1;

                    DataTable dttt1 = new DataTable();


                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("IssueQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("ReceiveQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("DamageQty");
                    dttt1.Columns.Add(dct1);

                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();
                        drNew1["IssueQty"] = RowsGVSizeQty[i]["IssueQty"].ToString();
                        drNew1["ReceiveQty"] = RowsGVSizeQty[i]["ReceiveQty"].ToString();
                        drNew1["DamageQty"] = RowsGVSizeQty[i]["DamageQty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    GVSizesView.DataSource = dstd1;
                    GVSizesView.DataBind();


                    #endregion
                }
            }

        }

        protected void btnSubmitQty_OnClick(object sender, EventArgs e)
        {
            if (ddlProcess.SelectedValue == "" || ddlProcess.SelectedValue == "0" || ddlProcess.SelectedValue == "Select Process")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Process.')", true);
                ddlProcess.Focus();
                return;
            }

            if (txtrate.Text == "" || txtrate.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Rate.')", true);
                txtrate.Focus();
                return;
            }

            if (GVSizes.Rows.Count > 0)
            {
                double IssueQty = 0; double ReceiveQty = 0; double DamageQty = 0;
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                    Label lblQty = (Label)GVSizes.Rows[vLoop].FindControl("lblQty");

                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    TextBox txtReceiveQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtReceiveQty");
                    TextBox txtDamageQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDamageQty");

                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";
                    if (txtReceiveQty.Text == "")
                        txtReceiveQty.Text = "0";
                    if (txtDamageQty.Text == "")
                        txtDamageQty.Text = "0";

                    if (Convert.ToDouble(lblQty.Text) < Convert.ToDouble(txtIssueQty.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Size " + lblSize.Text + ".')", true);
                        txtIssueQty.Focus();
                        return;
                    }
                    if (Convert.ToDouble(lblQty.Text) < (Convert.ToDouble(txtReceiveQty.Text) + Convert.ToDouble(txtDamageQty.Text)))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Size " + lblSize.Text + ".')", true);
                        txtReceiveQty.Focus();
                        return;
                    }

                    IssueQty += Convert.ToDouble(txtIssueQty.Text);
                    ReceiveQty += Convert.ToDouble(txtReceiveQty.Text);
                    DamageQty += Convert.ToDouble(txtDamageQty.Text);
                }

                #region CurrentTable Removed

                DataTable DTGVSizeDetails = (DataTable)ViewState["CurrentTable1"];

                DataRow[] DRItem = DTGVSizeDetails.Select("RowId='" + RowId.Text + "'");
                for (int i = 0; i < DRItem.Length; i++)
                    DRItem[i].Delete();
                DTGVSizeDetails.AcceptChanges();

                ViewState["CurrentTable1"] = DTGVSizeDetails;

                DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];

                DataRow[] DRSize = DTGVSizeQty.Select("RowId='" + RowId.Text + "'");
                for (int i = 0; i < DRSize.Length; i++)
                    DRSize[i].Delete();
                DTGVSizeQty.AcceptChanges();

                ViewState["CurrentTable2"] = DTGVSizeQty;

                #endregion


                // string HttpCookieValue = "";

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                #region


                dct = new DataColumn("RowId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("TransItemId");
                dttt.Columns.Add(dct);

                dct = new DataColumn("StyleNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("StyleNoId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Description");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Color");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ColorId");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Size");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RangeId");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Rate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Qty");
                dttt.Columns.Add(dct);

                dct = new DataColumn("IssueQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ReceiveQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DamageQty");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                if (ViewState["CurrentTable1"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable1"];

                    //////HttpCookie nameCookie = Request.Cookies["Name"];
                    //////string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    //////string adad = (Convert.ToInt32(name) + 1).ToString();

                    ////////Set the Cookie value.
                    //////nameCookie.Values["Name"] = adad;
                    ////////Set the Expiry date.
                    //////nameCookie.Expires = DateTime.Now.AddDays(30);
                    ////////Add the Cookie to Browser.
                    //////Response.Cookies.Add(nameCookie);

                    //////HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                    drNew = dttt.NewRow();

                    drNew["RowId"] = RowId.Text;// nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    drNew["TransItemId"] = TransItemId.Text;

                    drNew["StyleNo"] = StyleNo.Text;
                    drNew["StyleNoId"] = StyleNoId.Text;
                    drNew["Description"] = Description.Text;
                    drNew["Color"] = Color.Text;
                    drNew["ColorId"] = ColorId.Text;

                    drNew["Size"] = Sizes.Text;
                    drNew["RangeId"] = RangeId.Text;

                    drNew["Rate"] = txtrate.Text;

                    drNew["Qty"] = Qty.Text;

                    drNew["IssueQty"] = IssueQty;
                    drNew["ReceiveQty"] = ReceiveQty;
                    drNew["DamageQty"] = DamageQty;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                    dtddd.Merge(dt);

                }
                else
                {

                    //////HttpCookie nameCookie = new HttpCookie("Name");
                    //////nameCookie.Values["Name"] = "1";
                    //////nameCookie.Expires = DateTime.Now.AddDays(30);
                    //////Response.Cookies.Add(nameCookie);

                    //////HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                    drNew = dttt.NewRow();

                    drNew["RowId"] = RowId.Text;// nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    drNew["TransSizeId"] = TransItemId.Text;

                    drNew["StyleNo"] = StyleNo.Text;
                    drNew["StyleNoId"] = StyleNoId.Text;
                    drNew["Description"] = Description.Text;
                    drNew["Color"] = Color.Text;
                    drNew["ColorId"] = ColorId.Text;

                    drNew["Size"] = Sizes.Text;
                    drNew["RangeId"] = RangeId.Text;

                    drNew["Rate"] = txtrate.Text;
                    drNew["Qty"] = Qty.Text;

                    drNew["IssueQty"] = IssueQty;
                    drNew["ReceiveQty"] = ReceiveQty;
                    drNew["DamageQty"] = DamageQty;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                }

                #endregion

                ViewState["CurrentTable1"] = dtddd;
                GVItem.DataSource = dtddd;
                GVItem.DataBind();

                DataSet dstd1 = new DataSet();
                DataTable dtddd1 = new DataTable();
                DataRow drNew1;
                DataColumn dct1;
                DataTable dttt1 = new DataTable();

                #region

                dct1 = new DataColumn("RowId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("TransSizeId");
                dttt1.Columns.Add(dct1);

                dct1 = new DataColumn("Size");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("SizeId");
                dttt1.Columns.Add(dct1);

                dct1 = new DataColumn("Qty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("IssueQty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("ReceiveQty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("DamageQty");
                dttt1.Columns.Add(dct1);

                dstd1.Tables.Add(dttt1);


                if (ViewState["CurrentTable2"] != null)
                {
                    HttpCookie nameCookie = Request.Cookies["Name"];

                    DataTable dt1 = (DataTable)ViewState["CurrentTable2"];

                    for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                    {
                        HiddenField hdTransSizeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdTransSizeId");

                        HiddenField hdQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdQty");

                        HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                        Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                        TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                        TextBox txtReceiveQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtReceiveQty");
                        TextBox txtDamageQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDamageQty");

                        if (txtIssueQty.Text == "")
                            txtIssueQty.Text = "0";
                        if (txtReceiveQty.Text == "")
                            txtReceiveQty.Text = "0";
                        if (txtDamageQty.Text == "")
                            txtDamageQty.Text = "0";

                        drNew1 = dttt1.NewRow();

                        drNew1["TransSizeId"] = hdTransSizeId.Value;
                        drNew1["RowId"] = RowId.Text;// HttpCookieValue;

                        drNew1["Size"] = lblSize.Text;
                        drNew1["SizeId"] = hdSize.Value;

                        drNew1["Qty"] = hdQty.Value;

                        drNew1["IssueQty"] = txtIssueQty.Text;
                        drNew1["ReceiveQty"] = txtReceiveQty.Text;
                        drNew1["DamageQty"] = txtDamageQty.Text;

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }
                    dtddd1.Merge(dt1);
                }
                else
                {
                    HttpCookie nameCookie = Request.Cookies["Name"];

                    for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                    {
                        HiddenField hdTransSizeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdTransSizeId");

                        HiddenField hdQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdQty");

                        HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                        Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                        TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                        TextBox txtReceiveQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtReceiveQty");
                        TextBox txtDamageQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDamageQty");

                        if (txtIssueQty.Text == "")
                            txtIssueQty.Text = "0";
                        if (txtReceiveQty.Text == "")
                            txtReceiveQty.Text = "0";
                        if (txtDamageQty.Text == "")
                            txtDamageQty.Text = "0";

                        drNew1 = dttt1.NewRow();

                        string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                        drNew1["RowId"] = RowId.Text;// HttpCookieValue;
                        drNew1["TransSizeId"] = hdTransSizeId.Value;

                        drNew1["Size"] = lblSize.Text;
                        drNew1["SizeId"] = hdSize.Value;

                        drNew1["Qty"] = hdQty.Value;

                        drNew1["IssueQty"] = txtIssueQty.Text;
                        drNew1["ReceiveQty"] = txtReceiveQty.Text;
                        drNew1["DamageQty"] = txtDamageQty.Text;

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }
                }

                #endregion

                ViewState["CurrentTable2"] = dtddd1;

                if (btnSave.Text == "Save")
                {
                    if (Convert.ToInt32(ddlProcess.SelectedValue) != Convert.ToInt32(lblProcessforMasterId.Text))
                    {
                        #region Summary

                        DataTable dtraw = new DataTable();
                        DataSet dsraw = new DataSet();
                        DataRow drraw;

                        dtraw.Columns.Add("Item");
                        dtraw.Columns.Add("ItemId");
                        dtraw.Columns.Add("Color");
                        dtraw.Columns.Add("ColorId");
                        dtraw.Columns.Add("AvlStock");
                        dtraw.Columns.Add("WantedRaw");

                        dsraw.Tables.Add(dtraw);

                        DataSet dsTotalFab = new DataSet();

                        for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                        {
                            HiddenField hdStyleNoId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdStyleNoId");
                            Label lblIssueQty = (Label)GVItem.Rows[vLoop].FindControl("lblIssueQty");

                            if (Convert.ToDouble(lblIssueQty.Text) > 0)
                            {
                                DataSet dsRaw = objBs.FabDetailsforProcessEntry(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(hdStyleNoId.Value), Convert.ToDouble(lblIssueQty.Text), Convert.ToInt32(ddlProcess.Text));
                                if (dsRaw.Tables[0].Rows.Count > 0)
                                {
                                    dsTotalFab.Merge(dsRaw);
                                }
                            }
                        }
                        if (dsTotalFab.Tables.Count > 0)
                        {
                            if (dsTotalFab.Tables[0].Rows.Count > 0)
                            {
                                DataTable dtraws = new DataTable();

                                dtraws = dsTotalFab.Tables[0];

                                var result1 = from r in dtraws.AsEnumerable()
                                              group r by new { Item = r["Description"], ItemId = r["ItemMasterId"], Color = r["Color"], ColorId = r["ColorId"], AvlStock = r["AvlStock"] } into raw
                                              select new
                                              {
                                                  Item = raw.Key.Item,
                                                  ItemId = raw.Key.ItemId,
                                                  Color = raw.Key.Color,
                                                  ColorId = raw.Key.ColorId,
                                                  AvlStock = raw.Key.AvlStock,
                                                  total = raw.Sum(x => Convert.ToDouble(x["WantedRaw"])),
                                              };


                                foreach (var g in result1)
                                {
                                    drraw = dtraw.NewRow();

                                    drraw["Item"] = g.Item;
                                    drraw["ItemId"] = g.ItemId;
                                    drraw["Color"] = g.Color;
                                    drraw["ColorId"] = g.ColorId;
                                    drraw["AvlStock"] = g.AvlStock;
                                    drraw["WantedRaw"] = (g.total).ToString("f3");

                                    dsraw.Tables[0].Rows.Add(drraw);
                                }

                                GVFabricRawDetails.DataSource = dsraw.Tables[0];
                                GVFabricRawDetails.DataBind();

                            }
                        }


                        #endregion
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Records Found.')", true);
                return;
            }

            GVSizes.DataSource = null;
            GVSizes.DataBind();
            txtrate.Text = "";
            GVSizesView.DataSource = null;
            GVSizesView.DataBind();

        }


        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            if (ddlProcess.SelectedValue == "" || ddlProcess.SelectedValue == "0" || ddlProcess.SelectedValue == "Select Process")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Process.')", true);
                ddlProcess.Focus();
                return;
            }

            if (txtrate.Text == "" || txtrate.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Rate.')", true);
                txtrate.Focus();
                return;
            }

            if (GVSizes.Rows.Count > 0)
            {
                double IssueQty = 0; double ReceiveQty = 0; double DamageQty = 0;
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                    Label lblQty = (Label)GVSizes.Rows[vLoop].FindControl("lblQty");

                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    TextBox txtReceiveQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtReceiveQty");
                    TextBox txtDamageQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtDamageQty");

                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";
                    if (txtReceiveQty.Text == "")
                        txtReceiveQty.Text = "0";
                    if (txtDamageQty.Text == "")
                        txtDamageQty.Text = "0";

                  

                    IssueQty += Convert.ToDouble(txtIssueQty.Text);
                    ReceiveQty += Convert.ToDouble(txtReceiveQty.Text);
                    DamageQty += Convert.ToDouble(txtDamageQty.Text);
                }

                #region CurrentTable Removed

                DataTable DTGVSizeDetails = (DataTable)ViewState["CurrentTable1"];

                DataRow[] DRItem = DTGVSizeDetails.Select("RowId='" + RowId.Text + "'");
                for (int i = 0; i < DRItem.Length; i++)
                    DRItem[i].Delete();
                DTGVSizeDetails.AcceptChanges();

                ViewState["CurrentTable1"] = DTGVSizeDetails;

                DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];

                DataRow[] DRSize = DTGVSizeQty.Select("RowId='" + RowId.Text + "'");
                for (int i = 0; i < DRSize.Length; i++)
                    DRSize[i].Delete();
                DTGVSizeQty.AcceptChanges();

                ViewState["CurrentTable2"] = DTGVSizeQty;

                #endregion


                // string HttpCookieValue = "";

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                #region


                dct = new DataColumn("RowId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("TransItemId");
                dttt.Columns.Add(dct);

                dct = new DataColumn("StyleNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("StyleNoId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Description");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Color");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ColorId");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Size");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RangeId");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Rate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Qty");
                dttt.Columns.Add(dct);

                dct = new DataColumn("IssueQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ReceiveQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DamageQty");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                if (ViewState["CurrentTable1"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable1"];

                  

                    drNew = dttt.NewRow();

                    drNew["RowId"] = RowId.Text;// nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    drNew["TransItemId"] = TransItemId.Text;

                    drNew["StyleNo"] = StyleNo.Text;
                    drNew["StyleNoId"] = StyleNoId.Text;
                    drNew["Description"] = Description.Text;
                    drNew["Color"] = Color.Text;
                    drNew["ColorId"] = ColorId.Text;

                    drNew["Size"] = Sizes.Text;
                    drNew["RangeId"] = RangeId.Text;

                    drNew["Rate"] = txtrate.Text;

                    drNew["Qty"] = Qty.Text;

                    drNew["IssueQty"] = IssueQty;
                    drNew["ReceiveQty"] = ReceiveQty;
                    drNew["DamageQty"] = DamageQty;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                    dtddd.Merge(dt);

                }
                else
                {

                 

                    drNew = dttt.NewRow();

                    drNew["RowId"] = RowId.Text;// nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    drNew["TransSizeId"] = TransItemId.Text;

                    drNew["StyleNo"] = StyleNo.Text;
                    drNew["StyleNoId"] = StyleNoId.Text;
                    drNew["Description"] = Description.Text;
                    drNew["Color"] = Color.Text;
                    drNew["ColorId"] = ColorId.Text;

                    drNew["Size"] = Sizes.Text;
                    drNew["RangeId"] = RangeId.Text;

                    drNew["Rate"] = txtrate.Text;
                    drNew["Qty"] = Qty.Text;

                    drNew["IssueQty"] = IssueQty;
                    drNew["ReceiveQty"] = ReceiveQty;
                    drNew["DamageQty"] = DamageQty;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                }

                #endregion

                ViewState["CurrentTable1"] = dtddd;
                GVItem.DataSource = dtddd;
                GVItem.DataBind();

              
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Records Found.')", true);
                return;
            }

            GVSizes.DataSource = null;
            GVSizes.DataBind();
            txtrate.Text = "";
            GVSizesView.DataSource = null;
            GVSizesView.DataBind();

        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Plz Check Style Details.')", true);
                return;
            }

            //DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime FromDate;
            DateTime ToDate;

            //if (FromDate == ToDate)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('FromDate and ToDate have being in Same.')", true);
            //    return;
            //}

            HttpCookie nameCookie = Request.Cookies["Name"];
            string MaxRowId = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

            DateTime EntryDate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);



            if (btnSave.Text == "Save")
            {
                for (int vLoop = 0; vLoop < GVFabricRawDetails.Rows.Count; vLoop++)
                {
                    #region
                    HiddenField hdAvlStock = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdAvlStock");
                    HiddenField hdWantedRaw = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdWantedRaw");

                    if (Convert.ToDouble(hdAvlStock.Value) < Convert.ToDouble(hdWantedRaw.Value))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check WantedRaw in Row " + (Convert.ToInt32(vLoop) + 1) + ".')", true);
                        return;
                    }
                    #endregion
                }

                DataSet dsEntryNo = objBs.GetCuttingProcessEntryNo(YearCode);
                string EntryNo = dsEntryNo.Tables[0].Rows[0]["EntryNo"].ToString().PadLeft(4, '0');
                txtEntryNo.Text = "PRP-" + EntryNo + '/' + YearCode;

                int ProcessEntryId = objBs.InsertCuttingProcessEntry(txtEntryNo.Text, YearCode, Convert.ToInt32(dsEntryNo.Tables[0].Rows[0]["EntryNo"].ToString()), EntryDate, Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlProcess.SelectedValue), MaxRowId, txtRemarks.Text, Convert.ToInt32(ddlFinishingProcess.SelectedValue), Convert.ToInt32(ddlCompanyName.SelectedValue), Convert.ToInt32(ddlProcessLedger.SelectedValue), EntryDate, EntryDate);

                DataTable CurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                int TransItemId = objBs.InsertTransCuttingProcessEntryItems(ProcessEntryId, Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(0), CurrentTable1);

                DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
                int TransSizeId = objBs.InsertTransCuttingProcessEntrySizes(ProcessEntryId, Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(0), CurrentTable2);

                DataSet dsMC = objBs.GetOrderMasterCuttingDetail(Convert.ToInt32(ddlExcNo.SelectedValue));
                int BuyerOrderCutId = Convert.ToInt32(dsMC.Tables[0].Rows[0]["BuyerOrderCutId"].ToString());

                for (int vLoop = 0; vLoop < GVFabricRawDetails.Rows.Count; vLoop++)
                {
                    HiddenField hdItemId = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdAvlStock = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdAvlStock");
                    HiddenField hdWantedRaw = (HiddenField)GVFabricRawDetails.Rows[vLoop].FindControl("hdWantedRaw");

                    int CuttingFabricId = objBs.InsertTransProcessEntryFabricHistory(ProcessEntryId, BuyerOrderCutId, Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlProcess.SelectedValue), Convert.ToInt32(ddlFinishingProcess.SelectedValue), Convert.ToInt32(ddlCompanyName.SelectedValue), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdAvlStock.Value), Convert.ToDouble(hdWantedRaw.Value));
                }
            }
            else if (btnSave.Text == "Update Rate")
            {
                string ProcessEntryId = Request.QueryString.Get("ProcessEntryId");
                DataTable CurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                int TransItemId = objBs.Update_TransCuttingProcessEntryItems(Convert.ToInt32(ProcessEntryId), Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlProcess.SelectedValue), CurrentTable1, txtChallanNo.Text);
            }
            else
            {
                if (txtChallanNo.Text == "" || txtChallanNo.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check ChallanNo.')", true);
                    return;
                }

                string StockStatus = "No";
                if (ddlProcess.SelectedValue == ddlFinishingProcess.SelectedValue)
                {
                    StockStatus = "Yes";
                }

                string ProcessEntryId = Request.QueryString.Get("ProcessEntryId");

                int UpProcessEntryId = objBs.ReceiveUpdateCuttingProcessEntry(Convert.ToInt32(ProcessEntryId), txtRemarks.Text);

                DataTable CurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                int TransItemId = objBs.ReceiveTransCuttingProcessEntryItems(Convert.ToInt32(ProcessEntryId), Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlProcess.SelectedValue), CurrentTable1, txtChallanNo.Text);

                DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
                int TransSizeId = objBs.ReceiveTransCuttingProcessEntrySizes(Convert.ToInt32(ProcessEntryId), Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(ddlProcess.SelectedValue), CurrentTable2, Convert.ToInt32(ddlCompanyName.SelectedValue), StockStatus, CurrentTable1, ddlExcNo.SelectedItem.Text, txtChallanNo.Text);
            }

            Response.Redirect("CuttingProcessEntryGrid.aspx");

        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CuttingProcessEntryGrid.aspx");
        }


    }
}
