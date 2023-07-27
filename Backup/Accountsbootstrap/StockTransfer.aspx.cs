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
    public partial class StockTransfer : System.Web.UI.Page
    {

        BSClass objBs = new BSClass();
        string sTableName = "";
        string YearCode = "";
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
                DataSet dsPONo = objBs.GetEntryNofromStockTransfer();
                txtEntryNo.Text = dsPONo.Tables[0].Rows[0]["EntryNo"].ToString();
                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, "Select CompanyName");
                }
                DataSet dsItemGroup = objBs.GetItemGroup();
                if (dsItemGroup.Tables[0].Rows.Count > 0)
                {
                    ddlItemGroup.DataSource = dsItemGroup.Tables[0];
                    ddlItemGroup.DataTextField = "Itemgroupname";
                    ddlItemGroup.DataValueField = "ItemgroupId";
                    ddlItemGroup.DataBind();
                    ddlItemGroup.Items.Insert(0, "Select ItemGroup");
                }
                DataSet dsColor = objBs.gridColor();
                if (dsColor.Tables[0].Rows.Count > 0)
                {
                    ddlColor.DataSource = dsColor.Tables[0];
                    ddlColor.DataTextField = "Color";
                    ddlColor.DataValueField = "ColorID";
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0, "Select Color");
                }

                DataSet dsAllledger = objBs.GetAllledger();
                if (dsAllledger.Tables[0].Rows.Count > 0)
                {
                    ddlFromPartyCode.DataSource = dsAllledger.Tables[0];
                    ddlFromPartyCode.DataTextField = "ledgerName";
                    ddlFromPartyCode.DataValueField = "ledgerID";
                    ddlFromPartyCode.DataBind();
                    ddlFromPartyCode.Items.Insert(0, "Select From Party");

                    ddlToPartyCode.DataSource = dsAllledger.Tables[0];
                    ddlToPartyCode.DataTextField = "ledgerName";
                    ddlToPartyCode.DataValueField = "ledgerID";
                    ddlToPartyCode.DataBind();
                    ddlToPartyCode.Items.Insert(0, "Select To Party");
                }

                ddlItemName.Items.Insert(0, "Select Item");

                string StockTransferId = Request.QueryString.Get("StockTransferId");
                if (StockTransferId != "" && StockTransferId != null)
                {
                    DataSet dsST1 = objBs.GetStockTransfer(Convert.ToInt32(StockTransferId));
                    if (dsST1.Tables[0].Rows.Count > 0)
                    {
                        #region

                        txtEntryNo.Text = dsST1.Tables[0].Rows[0]["EntryNo"].ToString();
                        txtEntryDate.Text = Convert.ToDateTime(dsST1.Tables[0].Rows[0]["EntryDate"]).ToString("dd/MM/yyyy");

                        txtChallanNo.Text = dsST1.Tables[0].Rows[0]["ChallanNo"].ToString();
                        ddlTransferType.SelectedValue = dsST1.Tables[0].Rows[0]["TransferTypeId"].ToString();
                       
                        ddlFromPartyCode.SelectedValue = dsST1.Tables[0].Rows[0]["FromPartyId"].ToString();
                        ddlFromPartyCode.Enabled = false;
                        ddlToPartyCode.SelectedValue = dsST1.Tables[0].Rows[0]["ToPartyId"].ToString();
                        ddlToPartyCode.Enabled = false;
                        ddlCompany.SelectedValue = dsST1.Tables[0].Rows[0]["CompanyId"].ToString();
                        ddlCompany.Enabled = false;

                        txtRemarks.Text = dsST1.Tables[0].Rows[0]["Remarks"].ToString();

                        btnSave.Text = "Update";

                        #endregion
                    }

                    DataSet dsST2 = objBs.GetTransStockTransfer(Convert.ToInt32(StockTransferId));
                    if (dsST2.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

                        dct = new DataColumn("ItemGroupId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ItemId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ItemName");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ColorId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Color");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Qty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Rate");
                        dttt.Columns.Add(dct);
                        dstd.Tables.Add(dttt);

                        foreach (DataRow Dr in dsST2.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();

                            drNew["ItemGroupId"] = Dr["ItemGroupId"];
                            drNew["ItemId"] = Dr["ItemId"];
                            drNew["ItemName"] = Dr["Description"];
                            drNew["ColorId"] = Dr["ColorId"];
                            drNew["Color"] = Dr["Color"];

                            drNew["Qty"] = Convert.ToDouble(Dr["Qty"]).ToString("f2");
                            drNew["Rate"] = Convert.ToDouble(Dr["Rate"]).ToString("f2");

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();
                    }

                    btnSave.Text = "Update";

                }
            }

        }

        protected void ddlItemGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemGroup.SelectedValue != "" && ddlItemGroup.SelectedValue != "0" && ddlItemGroup.SelectedValue != "select ItemGroup")
            {
                DataSet dsset = objBs.getItems(Convert.ToInt32(ddlItemGroup.SelectedValue));
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlItemName.DataSource = dsset.Tables[0];
                    ddlItemName.DataTextField = "Description";
                    ddlItemName.DataValueField = "ItemMasterId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, "Select Item");
                }
                else
                {
                    ddlItemName.Items.Clear();
                    ddlItemName.Items.Insert(0, "Select Item");
                }
            }
            else
            {
                ddlItemName.Items.Clear();
                ddlItemName.Items.Insert(0, "Select Item");
            }

        }

        private void FirstGridViewRow1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("IssueItem", typeof(string)));
            dt.Columns.Add(new DataColumn("ReceiveItem", typeof(string)));
            dt.Columns.Add(new DataColumn("Process", typeof(string)));

            dt.Columns.Add(new DataColumn("Color", typeof(string)));

            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Extra", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalQuantity", typeof(string)));

            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));

            dr = dt.NewRow();
            dr["IssueItem"] = string.Empty;
            dr["ReceiveItem"] = string.Empty;
            dr["Process"] = string.Empty;

            dr["Color"] = string.Empty;

            dr["Quantity"] = string.Empty;
            dr["Extra"] = string.Empty;
            dr["TotalQuantity"] = string.Empty;

            dr["Rate"] = string.Empty;
            dr["Amount"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dt;

            GVItem.DataSource = dt;
            GVItem.DataBind();

            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("IssueItem");
            dtt.Columns.Add(dct);
            dct = new DataColumn("ReceiveItem");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Process");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dtt.Columns.Add(dct);


            dct = new DataColumn("Quantity");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Extra");
            dtt.Columns.Add(dct);
            dct = new DataColumn("TotalQuantity");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Amount");
            dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);

            drNew = dtt.NewRow();
            drNew["IssueItem"] = 0;
            drNew["ReceiveItem"] = 0;
            drNew["Process"] = 0;

            drNew["Color"] = "";

            drNew["Quantity"] = "";
            drNew["Extra"] = "";
            drNew["TotalQuantity"] = "";

            drNew["Rate"] = "";
            drNew["Amount"] = "";

            dstd.Tables[0].Rows.Add(drNew);

            GVItem.DataSource = dstd;
            GVItem.DataBind();

        }
        private void AddNewRow1()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddlIssueItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlIssueItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtAmount");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["IssueItem"] = ddlIssueItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ReceiveItem"] = ddlReceiveItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Process"] = ddlProcess.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Color"] = ddlReceiveColor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Extra"] = txtExtra.Text;
                        dtCurrentTable.Rows[i - 1]["TotalQuantity"] = txtTotalQuantity.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = txtAmount.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    GVItem.DataSource = dtCurrentTable;
                    GVItem.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData1();

        }
        private void SetRowData1()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddlIssueItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlIssueItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtAmount");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["IssueItem"] = ddlIssueItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ReceiveItem"] = ddlReceiveItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Process"] = ddlProcess.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Color"] = ddlReceiveColor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Extra"] = txtExtra.Text;
                        dtCurrentTable.Rows[i - 1]["TotalQuantity"] = txtTotalQuantity.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = txtAmount.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    GVItem.DataSource = dtCurrentTable;
                    GVItem.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
        }
        private void SetPreviousData1()
        {
            double ItemCost = 0;
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlIssueItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlIssueItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtAmount");

                        ddlIssueItemCode.SelectedValue = dt.Rows[i]["IssueItem"].ToString();
                        ddlReceiveItemCode.SelectedValue = dt.Rows[i]["ReceiveItem"].ToString();
                        ddlProcess.SelectedValue = dt.Rows[i]["Process"].ToString();

                        ddlReceiveColor.SelectedValue = dt.Rows[i]["Color"].ToString();

                        txtQuantity.Text = dt.Rows[i]["Quantity"].ToString();
                        txtExtra.Text = dt.Rows[i]["Extra"].ToString();
                        txtTotalQuantity.Text = dt.Rows[i]["TotalQuantity"].ToString();

                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtAmount.Text = dt.Rows[i]["Amount"].ToString();

                        if (txtAmount.Text == "")
                            txtAmount.Text = "0";

                        ItemCost += Convert.ToDouble(txtAmount.Text);

                        rowIndex++;

                    }
                }
            }


        }

        protected void GVItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();
                }
            }
        }

        protected void GVItem_OnRowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void GVItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument);
                hdRowIndex.Value = RowIndex.ToString();

                GridViewRow row = GVItem.Rows[RowIndex];

                ddlItemGroup.SelectedValue = (row.FindControl("hdItemGroupId") as HiddenField).Value;

                DataSet dsset = objBs.getItems(Convert.ToInt32(ddlItemGroup.SelectedValue));
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlItemName.DataSource = dsset.Tables[0];
                    ddlItemName.DataTextField = "Description";
                    ddlItemName.DataValueField = "ItemMasterId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, "Select Item");
                }
                else
                {
                    ddlItemName.Items.Clear();
                    ddlItemName.Items.Insert(0, "Select Item");
                }

                ddlItemName.SelectedValue = (row.FindControl("hdItemId") as HiddenField).Value;
                ddlColor.SelectedValue = (row.FindControl("hdColorId") as HiddenField).Value;

                txtQty.Text = (row.FindControl("hdQty") as HiddenField).Value;
                txtRate.Text = (row.FindControl("hdRate") as HiddenField).Value;

                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(hdRowIndex.Value);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();
                }

            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
                txtQty.Text = "0";
            if (txtRate.Text == "")
                txtRate.Text = "0";

            if (ddlItemGroup.SelectedValue == "" || ddlItemGroup.SelectedValue == "0" || ddlItemGroup.SelectedValue == "Select ItemGroup")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select ItemGroup.')", true);
                ddlItemGroup.Focus();
                return;
            }
            if (ddlItemName.SelectedValue == "" || ddlItemName.SelectedValue == "0" || ddlItemName.SelectedValue == "Select Item")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Item.')", true);
                ddlItemName.Focus();
                return;
            }
            if (ddlColor.SelectedValue == "" || ddlColor.SelectedValue == "0" || ddlColor.SelectedValue == "Select Color")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Color.')", true);
                ddlColor.Focus();
                return;
            }
            if (Convert.ToDouble(txtQty.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty.')", true);
                txtQty.Focus();
                return;
            }
            //if (Convert.ToDouble(txtRate.Text) == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Rate.')", true);
            //    txtRate.Focus();
            //    return;
            //}

            DataSet dstd = new DataSet();
            DataTable dtddd = new DataTable();
            DataRow drNew;
            DataColumn dct;
            DataTable dttt = new DataTable();

            #region

            dct = new DataColumn("ItemGroupId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ItemId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ItemName");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ColorId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Qty");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);
            dstd.Tables.Add(dttt);

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                drNew = dttt.NewRow();

                drNew["ItemGroupId"] = ddlItemGroup.SelectedValue;
                drNew["ItemId"] = ddlItemName.SelectedValue;
                drNew["ItemName"] = ddlItemName.SelectedItem.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;

                drNew["Qty"] = txtQty.Text;
                drNew["Rate"] = txtRate.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

            }
            else
            {
                drNew = dttt.NewRow();

                drNew["ItemGroupId"] = ddlItemGroup.SelectedValue;
                drNew["ItemId"] = ddlItemName.SelectedValue;
                drNew["ItemName"] = ddlItemName.SelectedItem.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;

                drNew["Qty"] = txtQty.Text;
                drNew["Rate"] = txtRate.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
            }

            #endregion

            ViewState["CurrentTable1"] = dtddd;
            GVItem.DataSource = dtddd;
            GVItem.DataBind();

            ddlItemGroup.ClearSelection();

            ddlItemName.Items.Clear();
            ddlItemName.Items.Insert(0, "Select Item");

            ddlColor.ClearSelection();

            txtQty.Text = "0";
            txtRate.Text = "0";

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                return;
            }

            if (ddlTransferType.Text == "2")
            {
                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    #region

                    HiddenField hdItem = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemName");
                    HiddenField hdColor = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColor");

                    HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                    HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");

                    if (btnSave.Text == "Save")
                    {
                        DataSet ds = objBs.CheckItemProcessOrderStock(Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value));
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Stock in " + hdItem.Value + ' ' + hdColor.Value + ".')", true);
                            return;
                        }
                    }

                    #endregion
                }
            }

            DateTime EntryDate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnSave.Text == "Save")
            {
                DataSet ds = objBs.GetEntryNofromStockTransfer();

                int StockTransferId = objBs.InsertStockTransfer(ds.Tables[0].Rows[0]["EntryNo"].ToString(), EntryDate, Convert.ToInt32(ddlTransferType.SelectedValue), txtChallanNo.Text, Convert.ToInt32(ddlFromPartyCode.SelectedValue), Convert.ToInt32(ddlToPartyCode.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), txtRemarks.Text);

                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    HiddenField hdItemGroupId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemGroupId");
                    HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                    HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");

                    int TransSamplingCostingId = objBs.InsertTransStockTransfer(StockTransferId, Convert.ToInt32(hdItemGroupId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdRate.Value), Convert.ToInt32(ddlCompany.SelectedValue), ddlTransferType.SelectedValue);
                }
            }
            else
            {
                string StockTransferId = Request.QueryString.Get("StockTransferId");
                if (StockTransferId != "" && StockTransferId != null)
                {
                    int Update = objBs.UpdateStockTransfer(EntryDate, Convert.ToInt32(ddlTransferType.SelectedValue),txtChallanNo.Text, txtRemarks.Text, Convert.ToInt32(StockTransferId));

                    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                    {
                        HiddenField hdItemGroupId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemGroupId");
                        HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");
                        HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                        HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");

                        int TransSamplingCostingId = objBs.InsertTransStockTransfer(Convert.ToInt32(StockTransferId), Convert.ToInt32(hdItemGroupId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdRate.Value), Convert.ToInt32(ddlCompany.SelectedValue), ddlTransferType.SelectedValue);
                    }
                }
            }

            Response.Redirect("StockTransferGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("StockTransferGrid.aspx");
        }


    }
}
