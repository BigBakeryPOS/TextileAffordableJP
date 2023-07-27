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
    public partial class MaterialOpeningStock : System.Web.UI.Page
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
                ddlItems.Items.Insert(0, "Select Item");

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    //ddlCompany.Items.Insert(0, "CompanyName");
                }

                DataSet dsProcessOn = objBs.GetCategory_as_Process("ShowPOrder");
                if (dsProcessOn.Tables[0].Rows.Count > 0)
                {
                    ddlProcessOn.DataSource = dsProcessOn.Tables[0];
                    ddlProcessOn.DataTextField = "category";
                    ddlProcessOn.DataValueField = "categoryid";
                    ddlProcessOn.DataBind();
                    ddlProcessOn.Items.Insert(0, "ProcessOn");
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

                
            }

        }

        protected void ddlProcessOn_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProcessOn.SelectedValue != "" && ddlProcessOn.SelectedValue != "0" && ddlProcessOn.SelectedValue != "ProcessOn")
            {
                DataSet dsItemforItemProcess = objBs.GetItemforItemProcess(Convert.ToInt32(ddlProcessOn.SelectedValue));
                if (dsItemforItemProcess.Tables[0].Rows.Count > 0)
                {
                    ddlItems.DataSource = dsItemforItemProcess.Tables[0];
                    ddlItems.DataTextField = "Description";
                    ddlItems.DataValueField = "ItemMasterId";
                    ddlItems.DataBind();
                    ddlItems.Items.Insert(0, "Select Item");
                }
                else
                {
                    ddlItems.Items.Clear();
                    ddlItems.Items.Insert(0, "Select Item");
                }
            }
            else
            {
                ddlItems.Items.Clear();
                ddlItems.Items.Insert(0, "Select Item");

            }

        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            DataSet dstd = new DataSet();
            DataTable dtddd = new DataTable();
            DataRow drNew;
            DataColumn dct;
            DataTable dttt = new DataTable();

            #region

            dct = new DataColumn("ProcessOn");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ProcessOnID");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Item");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ItemId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);
            dct = new DataColumn("ColorId");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Qty");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Remarks");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                drNew = dttt.NewRow();

                drNew["ProcessOn"] = ddlProcessOn.SelectedItem.Text;
                drNew["ProcessOnID"] = ddlProcessOn.SelectedValue;
                drNew["Item"] = ddlItems.SelectedItem.Text;
                drNew["ItemId"] = ddlItems.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;

                drNew["Qty"] = txtQty.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Remarks"] = txtRemarks.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

            }
            else
            {
                drNew = dttt.NewRow();

                drNew["ProcessOn"] = ddlProcessOn.SelectedItem.Text;
                drNew["ProcessOnID"] = ddlProcessOn.SelectedValue;
                drNew["Item"] = ddlItems.SelectedItem.Text;
                drNew["ItemId"] = ddlItems.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["ColorId"] = ddlColor.SelectedValue;

                drNew["Qty"] = txtQty.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Remarks"] = txtRemarks.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
            }

            #endregion

            ViewState["CurrentTable1"] = dtddd;
            GVItem.DataSource = dtddd;
            GVItem.DataBind();

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

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Fill Data.')", true);
                return;
            }

            DataTable CurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            int TransBuyerOrderCuttingId = objBs.InsertMaterialOpeningStockEntry(Convert.ToInt32(ddlCompany.SelectedValue), CurrentTable1);

            Response.Redirect("MaterialOpeningStockGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MaterialOpeningStockGrid.aspx");
        }

    }
}
