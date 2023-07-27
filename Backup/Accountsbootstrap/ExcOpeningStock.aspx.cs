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
    public partial class ExcOpeningStock : System.Web.UI.Page
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
                #region

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    //ddlCompany.Items.Insert(0, "CompanyName");
                }

                DataSet dsStyleNo = objBs.GetStyleNo();
                if (dsStyleNo.Tables[0].Rows.Count > 0)
                {
                    ddlStyle.DataSource = dsStyleNo.Tables[0];
                    ddlStyle.DataTextField = "StyleNo1";
                    ddlStyle.DataValueField = "SamplingCostingId";
                    ddlStyle.DataBind();
                    ddlStyle.Items.Insert(0, "Select Style");
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

                DataSet dsSizeRange = objBs.gridSizeRange();
                if (dsSizeRange.Tables[0].Rows.Count > 0)
                {
                    ddlSize.DataSource = dsSizeRange.Tables[0];
                    ddlSize.DataTextField = "Range";
                    ddlSize.DataValueField = "RangeId";
                    ddlSize.DataBind();
                    ddlSize.Items.Insert(0, "Select Size");
                }

                #endregion

                string OPExcStockId = Request.QueryString.Get("OPExcStockId");
                if (OPExcStockId != "" && OPExcStockId != null)
                {
                    DataSet dsOPExc = objBs.getExcOpeningStockEntry(Convert.ToInt32(OPExcStockId));
                    if (dsOPExc.Tables[0].Rows.Count > 0)
                    {
                        ddlCompany.SelectedValue = dsOPExc.Tables[0].Rows[0]["CompanyId"].ToString();
                        ddlCompany.Enabled = false;

                        txtExcNo.Text = dsOPExc.Tables[0].Rows[0]["ExcNo"].ToString();
                        txtExcNo.Enabled = false;

                        btnSave.Text = "Save";
                        btnSave.Enabled = false;

                        HttpCookie nameCookie1 = new HttpCookie("Name");
                        nameCookie1.Values["Name"] = dsOPExc.Tables[0].Rows[0]["MaxRowId"].ToString();
                        nameCookie1.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(nameCookie1);

                        string HttpCookieValues = nameCookie1 != null ? nameCookie1.Value.Split('=')[1] : "undefined";

                        DataSet dsOPExcItems = objBs.getTransExcOpeningStockEntryItems(Convert.ToInt32(OPExcStockId));
                        if (dsOPExcItems.Tables[0].Rows.Count > 0)
                        {
                            DataSet dstd = new DataSet();
                            DataTable dtddd = new DataTable();
                            DataRow drNew;
                            DataColumn dct;
                            DataTable dttt = new DataTable();

                            #region

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
                            dct = new DataColumn("Qty");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("RangeId");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Size");
                            dttt.Columns.Add(dct);
                            dstd.Tables.Add(dttt);

                            foreach (DataRow Dr in dsOPExcItems.Tables[0].Rows)
                            {
                                drNew = dttt.NewRow();

                                drNew["RowId"] = Dr["RowId"];
                                drNew["StyleNo"] = Dr["StyleNo"];
                                drNew["StyleNoId"] = Dr["StyleNoId"];
                                drNew["Description"] = Dr["Description"];
                                drNew["ColorId"] = Dr["ColorId"];
                                drNew["Color"] = Dr["Color"];
                                drNew["Rate"] = Dr["Rate"];
                                drNew["Qty"] = Dr["Qty"];
                                drNew["RangeId"] = Dr["RangeId"];
                                drNew["Size"] = Dr["Range"];

                                dstd.Tables[0].Rows.Add(drNew);
                                dtddd = dstd.Tables[0];
                            }

                            #endregion

                            ViewState["CurrentTable1"] = dtddd;
                            GVItem.DataSource = dtddd;
                            GVItem.DataBind();
                        }

                        DataSet dsOPExcSizes = objBs.getTransExcOpeningStockEntrySize(Convert.ToInt32(OPExcStockId));
                        if (dsOPExcSizes.Tables[0].Rows.Count > 0)
                        {
                            DataSet dstd1 = new DataSet();
                            DataTable dtddd1 = new DataTable();
                            DataRow drNew1;
                            DataColumn dct1;
                            DataTable dttt1 = new DataTable();

                            #region

                            dct1 = new DataColumn("RowId");
                            dttt1.Columns.Add(dct1);

                            dct1 = new DataColumn("StyleNoId");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("ColorId");
                            dttt1.Columns.Add(dct1);

                            dct1 = new DataColumn("SizeId");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("Size");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("Qty");
                            dttt1.Columns.Add(dct1);
                            dct1 = new DataColumn("Rate");
                            dttt1.Columns.Add(dct1);
                            dstd1.Tables.Add(dttt1);

                            foreach (DataRow Dr in dsOPExcSizes.Tables[0].Rows)
                            {
                                drNew1 = dttt1.NewRow();

                                drNew1["RowId"] = Dr["RowId"];
                                drNew1["StyleNoId"] = Dr["StyleNoId"];
                                drNew1["ColorId"] = Dr["ColorId"];
                                drNew1["SizeId"] = Dr["SizeId"];
                                drNew1["Size"] = Dr["Size"];
                                drNew1["Rate"] = Dr["Rate"];
                                drNew1["Qty"] = Dr["Qty"];


                                dstd1.Tables[0].Rows.Add(drNew1);
                                dtddd1 = dstd1.Tables[0];

                            }

                            #endregion

                            ViewState["CurrentTable2"] = dtddd1;
                        }
                    }
                }
            }

        }


        protected void ddlSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsSizeRange = objBs.getSizeRangeSize(Convert.ToInt32(ddlSize.SelectedValue));
            if (dsSizeRange.Tables[0].Rows.Count > 0)
            {
                GVSizes.DataSource = dsSizeRange;
                GVSizes.DataBind();
            }
            else
            {
                GVSizes.DataSource = null;
                GVSizes.DataBind();
            }
        }
        protected void btnSubmit1_OnClick(object sender, EventArgs e)
        {
            #region Validations

            if (ddlStyle.SelectedValue == "Style" || ddlStyle.SelectedValue == "" || ddlStyle.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Style.')", true);
                ddlStyle.Focus();
                return;
            }
            if (ddlColor.SelectedValue == "Color" || ddlColor.SelectedValue == "" || ddlColor.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Color.')", true);
                ddlColor.Focus();
                return;
            }
            if (ddlSize.SelectedValue == "Size" || ddlSize.SelectedValue == "" || ddlSize.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Size.')", true);
                ddlSize.Focus();
                return;
            }

            if (txtRate.Text == "")
                txtRate.Text = "0";
            if (Convert.ToDouble(txtRate.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Rate.')", true);
                txtRate.Focus();
                return;
            }

            double TtlQty = 0;
            for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
            {
                TextBox txtQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");
                if (txtQty.Text == "")
                    txtQty.Text = "0"; ;
                TtlQty += Convert.ToDouble(txtQty.Text);

            }

            if (TtlQty == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty.')", true);
                return;
            }

            #endregion

            string HttpCookieValue = "";

            DataSet dstd = new DataSet();
            DataTable dtddd = new DataTable();
            DataRow drNew;
            DataColumn dct;
            DataTable dttt = new DataTable();

            #region

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
            dct = new DataColumn("Qty");
            dttt.Columns.Add(dct);
            dct = new DataColumn("RangeId");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Size");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);


            string[] Style = ddlStyle.SelectedItem.Text.Split('&');
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                HttpCookie nameCookie = Request.Cookies["Name"];
                string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                string adad = (Convert.ToInt32(name) + 1).ToString();

                //Set the Cookie value.
                nameCookie.Values["Name"] = adad;
                //Set the Expiry date.
                nameCookie.Expires = DateTime.Now.AddDays(30);
                //Add the Cookie to Browser.
                Response.Cookies.Add(nameCookie);

                HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                drNew = dttt.NewRow();
                drNew["RowId"] = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                drNew["StyleNo"] = Style[0];
                drNew["StyleNoId"] = ddlStyle.SelectedValue;
                drNew["Description"] = Style[1];
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Qty"] = TtlQty;
                drNew["RangeId"] = ddlSize.SelectedValue;
                drNew["Size"] = ddlSize.SelectedItem.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

            }
            else
            {
                HttpCookie nameCookie = new HttpCookie("Name");
                nameCookie.Values["Name"] = "1";
                nameCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(nameCookie);

                HttpCookieValue = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                drNew = dttt.NewRow();
                drNew["RowId"] = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                drNew["StyleNo"] = Style[0];
                drNew["StyleNoId"] = ddlStyle.SelectedValue;
                drNew["Description"] = Style[1];
                drNew["ColorId"] = ddlColor.SelectedValue;
                drNew["Color"] = ddlColor.SelectedItem.Text;
                drNew["Rate"] = txtRate.Text;
                drNew["Qty"] = TtlQty;
                drNew["RangeId"] = ddlSize.SelectedValue;
                drNew["Size"] = ddlSize.SelectedItem.Text;

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

            dct1 = new DataColumn("StyleNoId");
            dttt1.Columns.Add(dct1);
            dct1 = new DataColumn("ColorId");
            dttt1.Columns.Add(dct1);

            dct1 = new DataColumn("SizeId");
            dttt1.Columns.Add(dct1);
            dct1 = new DataColumn("Size");
            dttt1.Columns.Add(dct1);
            dct1 = new DataColumn("Qty");
            dttt1.Columns.Add(dct1);
            dct1 = new DataColumn("Rate");
            dttt1.Columns.Add(dct1);

            dstd1.Tables.Add(dttt1);



            if (ViewState["CurrentTable2"] != null)
            {
                HttpCookie nameCookie = Request.Cookies["Name"];

                DataTable dt1 = (DataTable)ViewState["CurrentTable2"];

                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                    Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                    TextBox txtQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");
                    if (txtQty.Text == "")
                        txtQty.Text = "0";

                    drNew1 = dttt1.NewRow();

                    drNew1["RowId"] = HttpCookieValue;

                    drNew1["StyleNoId"] = ddlStyle.SelectedValue;
                    drNew1["ColorId"] = ddlColor.SelectedValue;

                    drNew1["SizeId"] = hdSize.Value;
                    drNew1["Size"] = lblSize.Text;
                    drNew1["Qty"] = txtQty.Text;
                    drNew1["Rate"] = txtRate.Text;

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
                    HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                    Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");

                    TextBox txtQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtQty");
                    if (txtQty.Text == "")
                        txtQty.Text = "0";

                    drNew1 = dttt1.NewRow();

                    string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

                    drNew1["RowId"] = HttpCookieValue;

                    drNew1["StyleNoId"] = ddlStyle.SelectedValue;
                    drNew1["ColorId"] = ddlColor.SelectedValue;

                    drNew1["SizeId"] = hdSize.Value;
                    drNew1["Size"] = lblSize.Text;
                    drNew1["Qty"] = txtQty.Text;
                    drNew1["Rate"] = txtRate.Text;

                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }
            }

            #endregion

            ViewState["CurrentTable2"] = dtddd1;

            ddlStyle.ClearSelection();
            ddlColor.ClearSelection();
            ddlSize.ClearSelection();
            txtRate.Text = "0";

            GVSizes.DataSource = null;
            GVSizes.DataBind();
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

                    ddlStyle.SelectedValue = RowsGVSizeDetails[0]["StyleNoId"].ToString();
                    ddlColor.SelectedValue = RowsGVSizeDetails[0]["ColorId"].ToString();
                    txtRate.Text = RowsGVSizeDetails[0]["Rate"].ToString();

                    ddlSize.SelectedValue = RowsGVSizeDetails[0]["RangeId"].ToString();

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();

                    DataRow drNew1;
                    DataColumn dct1;

                    DataTable dttt1 = new DataTable();


                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("StyleNoId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("ColorId");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Rate");
                    dttt1.Columns.Add(dct1);

                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();

                        drNew1["StyleNoId"] = RowsGVSizeQty[i]["StyleNoId"].ToString();
                        drNew1["ColorId"] = RowsGVSizeQty[i]["ColorId"].ToString();

                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();
                        drNew1["Rate"] = RowsGVSizeQty[i]["Rate"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    GVSizes.DataSource = dstd1;
                    GVSizes.DataBind();

                    DataRow[] DRItem = DTGVSizeDetails.Select("RowId='" + e.CommandArgument.ToString() + "'");
                    for (int i = 0; i < DRItem.Length; i++)
                        DRItem[i].Delete();
                    DTGVSizeDetails.AcceptChanges();

                    ViewState["CurrentTable1"] = DTGVSizeDetails;
                    GVItem.DataSource = DTGVSizeDetails;
                    GVItem.DataBind();



                    DataRow[] DRSize = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");
                    for (int i = 0; i < DRSize.Length; i++)
                        DRSize[i].Delete();
                    DTGVSizeQty.AcceptChanges();

                    ViewState["CurrentTable2"] = DTGVSizeQty;


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
                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);

                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    GVSizesView.DataSource = dstd1;
                    GVSizesView.DataBind();



                    #endregion
                }
            }

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (txtExcNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check ExcNo.')", true);
                return;
            }
            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Fill Data.')", true);
                return;
            }
            DataSet dsExcNo = objBs.CheckExcNoforOP(txtExcNo.Text);
            if (dsExcNo.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This ExcNo was Already Exists.')", true);
                txtExcNo.Focus();
                return;
            }

            HttpCookie nameCookie = Request.Cookies["Name"];
            string MaxRowId = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";

            int OPExcStockId = objBs.InsertExcOpeningStockEntry(Convert.ToInt32(ddlCompany.SelectedValue), txtExcNo.Text, MaxRowId);

            DataTable CurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            int TransOPExcStockId = objBs.InsertTransExcOpeningStockEntryItems(OPExcStockId, CurrentTable1);

            DataTable CurrentTable2 = (DataTable)ViewState["CurrentTable2"];
            int TransOPExcStockSizeId = objBs.InsertTransExcOpeningStockEntrySize(txtExcNo.Text, Convert.ToInt32(ddlCompany.SelectedValue), OPExcStockId, CurrentTable2, CurrentTable1);

            Response.Redirect("ExcOpeningStockGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ExcOpeningStockGrid.aspx");
        }

    }
}
