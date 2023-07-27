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
    public partial class MaterialsIssue : System.Web.UI.Page
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

                DataSet dsCuttingNo = objBs.GetMAterialIssueNo(YearCode);
                string Matno = dsCuttingNo.Tables[0].Rows[0]["MaterialIssueNo"].ToString().PadLeft(4, '0');
                txtmaterialissno.Text = "STR - " + Matno + " / " + YearCode;

                txtmaterialdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

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

                DataSet dcompany = objBs.GetCompanyDetails();
                if (dcompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompanyName.DataSource = dcompany.Tables[0];
                    ddlCompanyName.DataTextField = "Companyname";
                    ddlCompanyName.DataValueField = "comapanyId";
                    ddlCompanyName.DataBind();
                }


                issuetype_change(sender, e);

                string MaterialissueId = Request.QueryString.Get("MaterialissueId");
                if (MaterialissueId != "" && MaterialissueId != null)
                {
                    btnSave.Enabled = false;
                    btnSave.Text = "Update";

                    DataSet dsmaterialissue = objBs.GetMAterialIssue_Update(Convert.ToInt32(MaterialissueId));
                    if (dsmaterialissue.Tables[0].Rows.Count > 0)
                    {
                        #region

                        txtmaterialissno.Text = dsmaterialissue.Tables[0].Rows[0]["FullMaterialNo"].ToString();
                        txtmaterialdate.Text = Convert.ToDateTime(dsmaterialissue.Tables[0].Rows[0]["MaterialDate"]).ToString("dd/MM/yyyy");
                        drpissuetype.SelectedValue = dsmaterialissue.Tables[0].Rows[0]["MisueTypeId"].ToString();


                        
                        if (drpissuetype.SelectedValue == "1")
                        {

                            DataSet dsExcNo = objBs.GetAllPreCutting();
                            if (dsExcNo.Tables[0].Rows.Count > 0)
                            {
                                ddlExcNo.DataSource = dsExcNo.Tables[0];
                                ddlExcNo.DataTextField = "ExcNo";
                                ddlExcNo.DataValueField = "BuyerOrderCuttingId";
                                ddlExcNo.DataBind();
                                ddlExcNo.Items.Insert(0, "Select ExcNo");
                            }
                        }
                        else if (drpissuetype.SelectedValue == "2")
                        {

                            DataSet dsExcNo = objBs.GetExcNo_ItemDetails();
                            if (dsExcNo.Tables[0].Rows.Count > 0)
                            {
                                ddlExcNo.DataSource = dsExcNo.Tables[0];
                                ddlExcNo.DataTextField = "ExcNo";
                                ddlExcNo.DataValueField = "BuyerOrderMasterCuttingId";
                                ddlExcNo.DataBind();
                                ddlExcNo.Items.Insert(0, "Select ExcNo");
                            }
                        }

                        ddlExcNo.SelectedValue = dsmaterialissue.Tables[0].Rows[0]["EXCid"].ToString();
                        ddlExcNo.Enabled = false;

                        if (drpissuetype.SelectedValue == "1")
                        {
                            DataSet ds = objBs.GetBuyerOrderCutting(Convert.ToInt32(ddlExcNo.SelectedValue));
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                drpprpno.DataSource = ds.Tables[0];
                                drpprpno.DataTextField = "FullCuttingNo";
                                drpprpno.DataValueField = "BuyerOrderCuttingId";
                                drpprpno.DataBind();
                                drpprpno.Items.Insert(0, "Select PRP.No");

                                DataSet dsProcessLedger1 = objBs.GetApprovedProcessLedger();
                                if (dsProcessLedger1.Tables[0].Rows.Count > 0)
                                {
                                    ddlProcessLedger.DataSource = dsProcessLedger1.Tables[0];
                                    ddlProcessLedger.DataTextField = "LedgerName";
                                    ddlProcessLedger.DataValueField = "LedgerID";
                                    ddlProcessLedger.DataBind();
                                    ddlProcessLedger.Items.Insert(0, "Select Jobwork Ledger");
                                }
                                else
                                {
                                    ddlProcessLedger.Items.Insert(0, "Select Jobwork Ledger");
                                }

                            }
                        }
                        else if (drpissuetype.SelectedValue == "2")
                        {
                            ddlProcessLedger.Items.Clear();
                            lblissuefor.Text = "";

                            DataSet ds = objBs.GetBuyerOrderproductionMaster(Convert.ToInt32(ddlExcNo.SelectedValue));
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                drpprpno.Enabled = true;
                                drpprpno.DataSource = ds.Tables[0];
                                drpprpno.DataTextField = "FullEntryNo";
                                drpprpno.DataValueField = "ProcessEntryId";
                                drpprpno.DataBind();
                                drpprpno.Items.Insert(0, "Select PRP.No");

                                //ddlProcessLedger.SelectedValue = ds.Tables[0].Rows[0]["CuttingLedger"].ToString();
                                //ddlProcessLedger.Enabled = false;
                                //lblissuefor.Text = "Cutting Process";
                            }
                            DataSet dsProcessLedger1 = objBs.GetApprovedProcessLedger();
                            if (dsProcessLedger1.Tables[0].Rows.Count > 0)
                            {
                                ddlProcessLedger.DataSource = dsProcessLedger1.Tables[0];
                                ddlProcessLedger.DataTextField = "LedgerName";
                                ddlProcessLedger.DataValueField = "LedgerID";
                                ddlProcessLedger.DataBind();
                                ddlProcessLedger.Items.Insert(0, "Select Jobwork Ledger");
                            }
                            else
                            {
                                ddlProcessLedger.Items.Insert(0, "Select Jobwork Ledger");
                            }
                        }



                        drpprpno.SelectedValue = dsmaterialissue.Tables[0].Rows[0]["PRPId"].ToString();
                        drpprpno.Enabled = false;
                        ddlProcessLedger.SelectedValue = dsmaterialissue.Tables[0].Rows[0]["LedgerID"].ToString();
                        ddlProcessLedger.Enabled = false;
                        lblissuefor.Text = dsmaterialissue.Tables[0].Rows[0]["IssueFor"].ToString();

                        #endregion
                    }

                    DataSet dsBuyerOrderCuttingFabric = objBs.GetTransMaterialIssue(Convert.ToInt32(MaterialissueId));
                    if (dsBuyerOrderCuttingFabric.Tables[0].Rows.Count > 0)
                    {
                        DataSet dstd2 = new DataSet();
                        DataTable dtddd2 = new DataTable();
                        DataRow drNew2;
                        DataColumn dct2;
                        DataTable dttt2 = new DataTable();

                        #region

                        dct2 = new DataColumn("Item");
                        dttt2.Columns.Add(dct2);
                        dct2 = new DataColumn("ItemId");
                        dttt2.Columns.Add(dct2);
                        dct2 = new DataColumn("Color");
                        dttt2.Columns.Add(dct2);
                        dct2 = new DataColumn("ColorId");
                        dttt2.Columns.Add(dct2);
                        dct2 = new DataColumn("AvlStock");
                        dttt2.Columns.Add(dct2);
                        dct2 = new DataColumn("RequiredStock");
                        dttt2.Columns.Add(dct2);
                        dct2 = new DataColumn("IssueQty");
                        dttt2.Columns.Add(dct2);

                        dct2 = new DataColumn("IssueStock");
                        dttt2.Columns.Add(dct2);
                     //   dct2 = new DataColumn("UsedQty");
                     //   dttt2.Columns.Add(dct2);

                        dstd2.Tables.Add(dttt2);

                        foreach (DataRow Dr2 in dsBuyerOrderCuttingFabric.Tables[0].Rows)
                        {
                            drNew2 = dttt2.NewRow();

                            drNew2["Item"] = Dr2["Item"];
                            drNew2["ItemId"] = Dr2["ItemId"];
                            drNew2["Color"] = Dr2["Color"];
                            drNew2["ColorId"] = Dr2["ColorId"];
                            drNew2["AvlStock"] =Convert.ToDouble(Dr2["AvlStock"]).ToString("f2");
                            drNew2["RequiredStock"] = Convert.ToDouble(Dr2["RequiredStock"]).ToString("f2");
                            drNew2["IssueQty"] = Convert.ToDouble(Dr2["IssueStock"]).ToString("f2");

                            drNew2["IssueStock"] = Convert.ToDouble(Dr2["IssueStock"]).ToString("f2");
                          //  drNew2["UsedQty"] = Convert.ToDouble(Dr2["UsedQty"]).ToString("f2");

                            dstd2.Tables[0].Rows.Add(drNew2);
                            dtddd2 = dstd2.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable3"] = dtddd2;

                        GVFabricDetails.DataSource = dtddd2;
                        GVFabricDetails.DataBind();

                       // GVFabricDetails.Columns[7].Visible = false;

                        if (GVFabricDetails.Rows.Count > 0)
                        {
                            for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                            {
                                HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                                HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                                Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");

                                //DataSet dsstock = objBs.GetAvlStock(hdItemId.Value, hdColorId.Value, dsBuyerOrderCutting.Tables[0].Rows[0]["CompanyId"].ToString());
                                DataSet dsstock = objBs.GetAvlStock(hdItemId.Value, hdColorId.Value, "2");
                                if (dsstock.Tables[0].Rows.Count > 0)
                                {
                                    lblAvlStock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                                }
                                else
                                {
                                    lblAvlStock.Text = "0";
                                }
                            }
                        }
                    }

                }
            }
        }

        protected void issuetype_change(object sender, EventArgs e)
        {
            ddlProcessLedger.Items.Clear();
            drpprpno.Items.Clear();
            lblissuefor.Text = "";
            if (drpissuetype.SelectedValue == "1")
            {

                DataSet dsExcNo = objBs.GetAllPreCutting();
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderCuttingId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }
            }
            else if (drpissuetype.SelectedValue == "2")
            {

                DataSet dsExcNo = objBs.GetExcNo_ItemDetails();
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderMasterCuttingId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }
            }
        }


        protected void ddlExcNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpissuetype.SelectedValue == "1")
            {
                DataSet ds = objBs.GetBuyerOrderCutting(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {

                    drpprpno.DataSource = ds.Tables[0];
                    drpprpno.DataTextField = "FullCuttingNo";
                    drpprpno.DataValueField = "BuyerOrderCuttingId";
                    drpprpno.DataBind();
                    drpprpno.Items.Insert(0, "Select PRP.No");

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


                    drpprpno.SelectedValue = ds.Tables[0].Rows[0]["BuyerOrderCuttingId"].ToString();
                    drpprpno.Enabled = false;
                    ddlProcessLedger.SelectedValue = ds.Tables[0].Rows[0]["CuttingLedger"].ToString();
                    ddlProcessLedger.Enabled = false;
                    lblissuefor.Text = "Cutting Process";
                }
            }
            else if (drpissuetype.SelectedValue == "2")
            {
                ddlProcessLedger.Items.Clear();
                lblissuefor.Text = "";

                DataSet ds = objBs.GetBuyerOrderproductionMaster(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpprpno.Enabled = true;
                    drpprpno.DataSource = ds.Tables[0];
                    drpprpno.DataTextField = "FullEntryNo";
                    drpprpno.DataValueField = "ProcessEntryId";
                    drpprpno.DataBind();
                    drpprpno.Items.Insert(0, "Select PRP.No");

                    //ddlProcessLedger.SelectedValue = ds.Tables[0].Rows[0]["CuttingLedger"].ToString();
                    //ddlProcessLedger.Enabled = false;
                    //lblissuefor.Text = "Cutting Process";
                }
            }

            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "0" && ddlExcNo.SelectedValue != "Select ExcNo")
            {
                DataSet ds = objBs.GetBuyerOrderCutting(Convert.ToInt32(ddlExcNo.SelectedValue));

                DataSet dsFab = objBs.GetAdditionalFabforPreCut(Convert.ToInt32(Convert.ToInt32(ddlExcNo.SelectedValue)));
                if (dsFab.Tables[0].Rows.Count > 0)
                {
                    GVFabricDetails.DataSource = dsFab;
                    GVFabricDetails.DataBind();
                }
                else
                {
                    GVFabricDetails.DataSource = null;
                    GVFabricDetails.DataBind();
                }

                if (GVFabricDetails.Rows.Count > 0)
                {
                    for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                    {
                        HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                        Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");

                        DataSet dsstock = objBs.GetAvlStock(hdItemId.Value, hdColorId.Value, ddlCompanyName.SelectedValue);
                        if (dsstock.Tables[0].Rows.Count > 0)
                        {
                            lblAvlStock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                        }
                        else
                        {
                            lblAvlStock.Text = "0";
                        }
                    }
                }
            }
            else
            {
                GVFabricDetails.DataSource = null;
                GVFabricDetails.DataBind();
            }
        }

        protected void ddlCompanyName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            #region GET AVALIABLE STOCK FabricDetails

            if (GVFabricDetails.Rows.Count > 0)
            {
                for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                {
                    HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                    Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");

                    DataSet dsstock = objBs.GetAvlStock(hdItemId.Value, hdColorId.Value, ddlCompanyName.SelectedValue);
                    if (dsstock.Tables[0].Rows.Count > 0)
                    {
                        lblAvlStock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                    }
                    else
                    {
                        lblAvlStock.Text = "0";
                    }
                }
            }

            #endregion
        }

        protected void prp_chnaged(object sender, EventArgs e)
        {

            if (drpissuetype.SelectedValue == "2")
            {

                if (drpprpno.SelectedValue != "Select PRP.No")
                {

                    DataSet ds = objBs.CuttingProcessEntryPrint(Convert.ToInt32(drpprpno.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {

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

                        ddlProcessLedger.SelectedValue = ds.Tables[0].Rows[0]["ProcessLedger"].ToString();
                        ddlProcessLedger.Enabled = false;
                        lblissuefor.Text = ds.Tables[0].Rows[0]["Process"].ToString();
                    }
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (ddlProcessLedger.SelectedValue == "Select Jobwork Ledger")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select LEdger.Thank You!!!')", true);
                ddlProcessLedger.Focus();
                return;
            }

            DateTime MaterialIssueDate = DateTime.ParseExact(txtmaterialdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "0" && ddlExcNo.SelectedValue != "Select ExcNo")
            {
                DataSet ds = objBs.GetBuyerOrderCutting(Convert.ToInt32(ddlExcNo.SelectedValue));
                string CompanyId = ds.Tables[0].Rows[0]["CompanyId"].ToString();

                for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                {
                    HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdRequiredStock = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdRequiredStock");
                    HiddenField hdIssueStock = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdIssueStock");

                    Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");
                    TextBox txtIssueQty = (TextBox)GVFabricDetails.Rows[vLoop].FindControl("txtIssueQty");

                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";

                    if (Convert.ToDouble(txtIssueQty.Text) > 0)
                    {
                        if (Convert.ToDouble(lblAvlStock.Text) < Convert.ToDouble(txtIssueQty.Text))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Avl.Stock  in Row " + (Convert.ToInt32(vLoop) + 1) + ".')", true);
                            txtIssueQty.Focus();
                            return;
                        }
                    }
                }

                DataSet dsCuttingNo = objBs.GetMAterialIssueNo(YearCode);
                string Matno = dsCuttingNo.Tables[0].Rows[0]["MaterialIssueNo"].ToString().PadLeft(4, '0');
                txtmaterialissno.Text = "STR - " + Matno + " / " + YearCode;
                
                
                //  Insert MAin TAble

                int MaterialIssueID = objBs.InsertMain_Material(Matno, txtmaterialissno.Text, MaterialIssueDate, drpissuetype.SelectedValue, drpissuetype.SelectedItem.Text, ddlExcNo.SelectedValue, ddlExcNo.SelectedItem.Text, drpprpno.SelectedValue, drpprpno.SelectedItem.Text, lblissuefor.Text, ddlProcessLedger.SelectedValue, YearCode,ddlCompanyName.SelectedValue);

                for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                {
                    HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdRequiredStock = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdRequiredStock");

                    Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");
                    TextBox txtIssueQty = (TextBox)GVFabricDetails.Rows[vLoop].FindControl("txtIssueQty");

                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";

                    int CuttingFabricId = objBs.InsertAdditionalFabforPreCut(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(lblAvlStock.Text), Convert.ToDouble(hdRequiredStock.Value), Convert.ToDouble(txtIssueQty.Text), Convert.ToInt32(CompanyId), ddlExcNo.SelectedItem.Text, drpprpno.SelectedValue, ddlProcessLedger.SelectedValue, drpissuetype.SelectedItem.Text, lblissuefor.Text, MaterialIssueID.ToString(),ddlCompanyName.SelectedValue);

                }
                Response.Redirect("MaterialsIssueGrid.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select EXC.No.')", true);
                ddlExcNo.Focus();
                return;
            }

        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MaterialsIssueGrid.aspx");
        }

    }
}

