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
    public partial class MaterialStockReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double Qty = 0; double Value = 0;

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

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, "All");
                }
                DataSet dsIH = objBs.selectcategorymaster();
                if (dsIH.Tables[0].Rows.Count > 0)
                {
                    ddlItemHead.DataSource = dsIH.Tables[0];
                    ddlItemHead.DataTextField = "category";
                    ddlItemHead.DataValueField = "categoryid";
                    ddlItemHead.DataBind();
                    ddlItemHead.Items.Insert(0, "All");
                }

                ddlItemGroup.Items.Insert(0, "All");
                ddlItem.Items.Insert(0, "All");


                DataSet dsColor = objBs.gridColor();
                if (dsColor.Tables[0].Rows.Count > 0)
                {
                    ddlColor.DataSource = dsColor.Tables[0];
                    ddlColor.DataTextField = "Color";
                    ddlColor.DataValueField = "ColorID";
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0, "All");
                }


            }
        }

        protected void ddlItemHead_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemHead.SelectedValue == "" || ddlItemHead.SelectedValue == "0" || ddlItemHead.SelectedValue == "All")
            {
                ddlItemGroup.Items.Clear();
                ddlItemGroup.Items.Insert(0, "All");
            }
            else
            {
                DataSet dsItem = objBs.GetHeadItemGroup(Convert.ToInt32(ddlItemHead.SelectedValue));
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlItemGroup.DataSource = dsItem.Tables[0];
                    ddlItemGroup.DataTextField = "Itemgroupname";
                    ddlItemGroup.DataValueField = "ItemgroupId";
                    ddlItemGroup.DataBind();
                    ddlItemGroup.Items.Insert(0, "All");
                }
            }

            ddlItem.Items.Clear();
            ddlItem.Items.Insert(0, "All");
        }
        protected void ddlItemGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemHead.SelectedValue == "" || ddlItemHead.SelectedValue == "0" || ddlItemHead.SelectedValue == "All" || ddlItemGroup.SelectedValue == "" || ddlItemGroup.SelectedValue == "0" || ddlItemGroup.SelectedValue == "All")
            {
                ddlItem.Items.Clear();
                ddlItem.Items.Insert(0, "All");
            }
            else
            {
                DataSet dsItem = objBs.getAllItemsfor_HeadandGroup(Convert.ToInt32(ddlItemHead.SelectedValue), Convert.ToInt32(ddlItemGroup.SelectedValue));
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataSource = dsItem.Tables[0];
                    ddlItem.DataTextField = "Description";
                    ddlItem.DataValueField = "ItemMasterId";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "All");
                }
            }
        }


        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            DataSet dsPORates = objBs.GetPORates(ddlItem.SelectedValue);
            DataSet dsIPOERates = objBs.GetIPOERates(ddlItem.SelectedValue);
            DataSet dsIPORRates = objBs.GetIPORRates(ddlItem.SelectedValue);
            DataSet dsOPSRates = objBs.GetOPSRates(ddlItem.SelectedValue);

            DataSet dsPORates1 = objBs.GetPORates("All");
            DataSet dsIPOERates1 = objBs.GetIPOERates("All");

            DataSet ds = objBs.GetMaterialStockReport(ddlCompany.SelectedValue, ddlItemHead.SelectedValue, ddlItemGroup.SelectedValue, ddlItem.SelectedValue, ddlColor.SelectedValue, ddlQtyCheck.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region

                DataTable DTS = new DataTable();
                DTS.Columns.Add(new DataColumn("CompanyName"));
                DTS.Columns.Add(new DataColumn("Description"));
                DTS.Columns.Add(new DataColumn("Color"));
                DTS.Columns.Add(new DataColumn("Qty"));
                DTS.Columns.Add(new DataColumn("Rate"));
                DTS.Columns.Add(new DataColumn("Value"));

                foreach (DataRow DRDS in ds.Tables[0].Rows)
                {
                    double TtlRate = 0; int TtlCount = 0; double TtlShrink = 0;
                    double TtlRate1 = 0; int TtlCount1 = 0;

                    string IsReceive = "No";
                    string IssueItemId = ""; string IssueColorId = "";

                    DataRow DR = DTS.NewRow();

                    DR["CompanyName"] = DRDS["CompanyName"];
                    DR["Description"] = DRDS["Description"];
                    DR["Color"] = DRDS["Color"];
                    DR["Qty"] = Convert.ToDouble(DRDS["Qty"]).ToString("f2");

                    if (IsChecked.Checked == true)
                    {
                        if (dsPORates.Tables[0].Rows.Count > 0)
                        {
                            #region
                            DataRow[] RowsPORates = dsPORates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ItemId='" + DRDS["ItemId"] + "' and ColorId='" + DRDS["ColorId"] + "' ");
                            if (RowsPORates.Length > 0)
                            {
                                for (int i = 0; i < RowsPORates.Length; i++)
                                {
                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                        break;

                                    TtlRate += Convert.ToDouble(RowsPORates[i]["Rate"]);
                                    TtlCount++;

                                }

                            }
                            else
                            {
                                DataRow[] RowsIPOERates = dsIPOERates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and IssueItemId='" + DRDS["ItemId"] + "' and IssueColorId='" + DRDS["ColorId"] + "' ");
                                if (RowsIPOERates.Length > 0)
                                {
                                    for (int i = 0; i < RowsIPOERates.Length; i++)
                                    {
                                        if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                            break;

                                        TtlRate += Convert.ToDouble(RowsIPOERates[i]["Rate"]);
                                        TtlCount++;

                                    }
                                }
                                else
                                {
                                    DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ReceiveItemId='" + DRDS["ItemId"] + "' and ReceiveColorId='" + DRDS["ColorId"] + "' ");
                                    if (RowsIPORRates.Length > 0)
                                    {
                                        IsReceive = "Yes";

                                        #region

                                        IssueItemId = RowsIPORRates[0]["IssueItemId"].ToString();
                                        IssueColorId = RowsIPORRates[0]["IssueColorId"].ToString();


                                        for (int i = 0; i < RowsIPORRates.Length; i++)
                                        {
                                            if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                break;

                                            TtlRate += Convert.ToDouble(RowsIPORRates[i]["Rate"]);
                                            TtlCount++;

                                            TtlShrink += Convert.ToDouble(RowsIPORRates[i]["Shrink"]);
                                        }

                                        #region

                                        DataRow[] RowsPORates1 = dsPORates1.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ItemId='" + IssueItemId + "' and ColorId='" + IssueColorId + "' ");
                                        if (RowsPORates1.Length > 0)
                                        {
                                            for (int i = 0; i < RowsPORates1.Length; i++)
                                            {
                                                if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                    break;

                                                TtlRate1 += Convert.ToDouble(RowsPORates1[i]["Rate"]);
                                                TtlCount1++;

                                            }

                                        }
                                        else
                                        {
                                            DataRow[] RowsIPOERates1 = dsIPOERates1.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and IssueItemId='" + IssueItemId + "' and IssueColorId='" + IssueColorId + "' ");
                                            if (RowsIPOERates1.Length > 0)
                                            {
                                                for (int i = 0; i < RowsIPOERates1.Length; i++)
                                                {
                                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                        break;

                                                    TtlRate1 += Convert.ToDouble(RowsIPOERates1[i]["Rate"]);
                                                    TtlCount1++;

                                                }
                                            }
                                        }

                                        #endregion




                                        #endregion
                                    }
                                    else
                                    {
                                        DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ItemId='" + DRDS["ItemId"] + "' and ColorId='" + DRDS["ColorId"] + "' ");
                                        if (RowsOPSRates.Length > 0)
                                        {
                                            for (int i = 0; i < RowsOPSRates.Length; i++)
                                            {
                                                if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                    break;

                                                TtlRate += Convert.ToDouble(RowsOPSRates[i]["ItemRate"]);
                                                TtlCount++;

                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        else if (dsIPOERates.Tables[0].Rows.Count > 0)
                        {
                            #region

                            DataRow[] RowsIPOERates = dsIPOERates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and IssueItemId='" + DRDS["ItemId"] + "' and IssueColorId='" + DRDS["ColorId"] + "' ");
                            if (RowsIPOERates.Length > 0)
                            {
                                for (int i = 0; i < RowsIPOERates.Length; i++)
                                {
                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                        break;

                                    TtlRate += Convert.ToDouble(RowsIPOERates[i]["Rate"]);
                                    TtlCount++;

                                }
                            }
                            else
                            {
                                IsReceive = "Yes";

                                DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ReceiveItemId='" + DRDS["ItemId"] + "' and ReceiveColorId='" + DRDS["ColorId"] + "' ");
                                if (RowsIPORRates.Length > 0)
                                {
                                    IssueItemId = RowsIPORRates[0]["IssueItemId"].ToString();
                                    IssueColorId = RowsIPORRates[0]["IssueColorId"].ToString();

                                    for (int i = 0; i < RowsIPORRates.Length; i++)
                                    {
                                        if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                            break;

                                        TtlRate += Convert.ToDouble(RowsIPORRates[i]["Rate"]);
                                        TtlCount++;

                                        TtlShrink += Convert.ToDouble(RowsIPORRates[i]["Shrink"]);
                                    }

                                    #region

                                    DataRow[] RowsPORates1 = dsPORates1.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ItemId='" + IssueItemId + "' and ColorId='" + IssueColorId + "' ");
                                    if (RowsPORates1.Length > 0)
                                    {
                                        for (int i = 0; i < RowsPORates1.Length; i++)
                                        {
                                            if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                break;

                                            TtlRate1 += Convert.ToDouble(RowsPORates1[i]["Rate"]);
                                            TtlCount1++;

                                        }

                                    }
                                    else
                                    {
                                        DataRow[] RowsIPOERates1 = dsIPOERates1.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and IssueItemId='" + IssueItemId + "' and IssueColorId='" + IssueColorId + "' ");
                                        if (RowsIPOERates1.Length > 0)
                                        {
                                            for (int i = 0; i < RowsIPOERates1.Length; i++)
                                            {
                                                if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                    break;

                                                TtlRate1 += Convert.ToDouble(RowsIPOERates1[i]["Rate"]);
                                                TtlCount1++;

                                            }
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ItemId='" + DRDS["ItemId"] + "' and ColorId='" + DRDS["ColorId"] + "' ");
                                    if (RowsOPSRates.Length > 0)
                                    {
                                        for (int i = 0; i < RowsOPSRates.Length; i++)
                                        {
                                            if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                break;

                                            TtlRate += Convert.ToDouble(RowsOPSRates[i]["ItemRate"]);
                                            TtlCount++;

                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        else if (dsIPORRates.Tables[0].Rows.Count > 0)
                        {
                            IsReceive = "Yes";

                            #region

                            DataRow[] RowsIPORRates = dsIPORRates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ReceiveItemId='" + DRDS["ItemId"] + "' and ReceiveColorId='" + DRDS["ColorId"] + "' ");
                            if (RowsIPORRates.Length > 0)
                            {
                                IssueItemId = RowsIPORRates[0]["IssueItemId"].ToString();
                                IssueColorId = RowsIPORRates[0]["IssueColorId"].ToString();


                                for (int i = 0; i < RowsIPORRates.Length; i++)
                                {
                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                        break;

                                    TtlRate += Convert.ToDouble(RowsIPORRates[i]["Rate"]);
                                    TtlCount++;

                                    TtlShrink += Convert.ToDouble(RowsIPORRates[i]["Shrink"]);
                                }

                                #region

                                DataRow[] RowsPORates1 = dsPORates1.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ItemId='" + IssueItemId + "' and ColorId='" + IssueColorId + "' ");
                                if (RowsPORates1.Length > 0)
                                {
                                    for (int i = 0; i < RowsPORates1.Length; i++)
                                    {
                                        if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                            break;

                                        TtlRate1 += Convert.ToDouble(RowsPORates1[i]["Rate"]);
                                        TtlCount1++;

                                    }

                                }
                                else
                                {
                                    DataRow[] RowsIPOERates1 = dsIPOERates1.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and IssueItemId='" + IssueItemId + "' and IssueColorId='" + IssueColorId + "' ");
                                    if (RowsIPOERates1.Length > 0)
                                    {
                                        for (int i = 0; i < RowsIPOERates1.Length; i++)
                                        {
                                            if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                                break;

                                            TtlRate1 += Convert.ToDouble(RowsIPOERates1[i]["Rate"]);
                                            TtlCount1++;

                                        }
                                    }
                                }

                                #endregion


                            }
                            else
                            {
                                DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ItemId='" + DRDS["ItemId"] + "' and ColorId='" + DRDS["ColorId"] + "' ");
                                if (RowsOPSRates.Length > 0)
                                {
                                    for (int i = 0; i < RowsOPSRates.Length; i++)
                                    {
                                        if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                            break;

                                        TtlRate += Convert.ToDouble(RowsOPSRates[i]["ItemRate"]);
                                        TtlCount++;

                                    }
                                }
                            }

                            #endregion
                        }
                        else if (dsOPSRates.Tables[0].Rows.Count > 0)
                        {
                            #region

                            DataRow[] RowsOPSRates = dsOPSRates.Tables[0].Select("CompanyId='" + DRDS["CompanyId"] + "' and ItemId='" + DRDS["ItemId"] + "' and ColorId='" + DRDS["ColorId"] + "' ");
                            if (RowsOPSRates.Length > 0)
                            {
                                for (int i = 0; i < RowsOPSRates.Length; i++)
                                {
                                    if (i >= Convert.ToInt32(lblMaximumRows.Text))
                                        break;

                                    TtlRate += Convert.ToDouble(RowsOPSRates[i]["ItemRate"]);
                                    TtlCount++;

                                }
                            }

                            #endregion

                        }
                        gvMaterialStockEntry.Columns[5].Visible = true;
                        gvMaterialStockEntry.Columns[6].Visible = true;
                    }
                    else
                    {
                        gvMaterialStockEntry.Columns[5].Visible = false;
                        gvMaterialStockEntry.Columns[6].Visible = false;
                    }

                    double Shrink = TtlShrink / TtlCount;
                    double Rate = TtlRate / TtlCount;
                    double Rate1 = TtlRate1 / TtlCount1;


                    if (Convert.ToDouble(DRDS["Qty"]) > 0)
                    {
                        if (IsReceive == "Yes")
                        {
                            double ReceiveRate = Rate + Rate1 + (Rate1 * Shrink / 100);

                            if (ReceiveRate.ToString() == "NaN")
                            {
                                DR["Rate"] = 0;
                                DR["Value"] = 0;
                            }
                            else
                            {
                                DR["Rate"] = Convert.ToDouble(ReceiveRate).ToString("f2");
                                DR["Value"] = (Convert.ToDouble(DRDS["Qty"]) * ReceiveRate).ToString("f2");
                            }
                        }
                        else
                        {
                            if (Rate.ToString() == "NaN")
                            {
                                DR["Rate"] = 0;
                                DR["Value"] = 0;
                            }
                            else
                            {
                                DR["Rate"] = Convert.ToDouble(Rate).ToString("f2");
                                DR["Value"] = (Convert.ToDouble(DRDS["Qty"]) * Rate).ToString("f2");
                            }
                        }

                    }
                    else
                    {
                        DR["Rate"] = 0;
                        DR["Value"] = 0;
                    }

                    DTS.Rows.Add(DR);

                    IsReceive = "No";
                    TtlRate1 = 0; TtlCount1 = 0; TtlShrink = 0;
                    TtlRate = 0; TtlCount = 0;
                    IssueItemId = ""; IssueColorId = "";

                }
                #endregion

                gvMaterialStockEntry.DataSource = DTS;
                gvMaterialStockEntry.DataBind();
            }
            else
            {
                gvMaterialStockEntry.DataSource = null;
                gvMaterialStockEntry.DataBind();
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= MaterialStockReport.xls");
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

        protected void gvMaterialStockEntry_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                Value += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Value"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Total:- ";
                e.Row.Cells[4].Text = Qty.ToString("f2");
                e.Row.Cells[6].Text = Value.ToString("f2");
            }

        }
    }
}


