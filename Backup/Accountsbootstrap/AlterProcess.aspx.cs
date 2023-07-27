using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Text;
using System.Data;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class AlterProcess : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string empid = "";

        double Q30F = 0; double Q32F = 0; double Q34F = 0; double Q36F = 0;
        double QXSF = 0; double QSF = 0; double QMF = 0; double QLF = 0;
        double QXLF = 0; double QXXLF = 0; double Q3XLF = 0; double Q4XLF = 0;
         
        double Q30H = 0; double Q32H = 0; double Q34H = 0; double Q36H = 0;
        double QXSH = 0; double QSH = 0; double QMH = 0; double QLH = 0;
        double QXLH = 0; double QXXLH = 0; double Q3XLH = 0; double Q4XLH = 0;
        double QttlFH = 0; double GVtotalshirt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string Stitchingid = string.Empty;
            string Requests = string.Empty;
            Stitchingid = Request.QueryString.Get("Stitchingid");


            Requests = Request.QueryString.Get("name");
            empid = Session["Empid"].ToString();

            if (!IsPostBack)
            {

                string super = Session["IsSuperAdmin"].ToString();
                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objbs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "Select Branch");
                    }
                }
                else
                {

                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objbs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }
                DataSet drpEmpp = objbs.Alljobworkmasterpayment();
                drpMultiemployee.DataSource = drpEmpp;
                drpMultiemployee.DataTextField = "ledgerName";
                drpMultiemployee.DataValueField = "ledgerId";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "Select Name");
                divWork.Visible = false;

                if (Stitchingid != null)
                {
                    #region

                    if (Requests == "Edit")
                    {

                        DataSet dgetcheck = objbs.getJpStichingLot(Stitchingid);
                        if (dgetcheck.Tables[0].Rows.Count > 0)
                        {
                            drpbranch.SelectedValue = dgetcheck.Tables[0].Rows[0]["Companyid"].ToString();
                            drpbranch.Enabled = false;
                            DataSet dsLot = objbs.Select_Lot("Stc", drpbranch.SelectedValue);
                            if (dsLot.Tables[0].Rows.Count > 0)
                            {
                                ddlLotNo.DataSource = dsLot.Tables[0];
                                ddlLotNo.DataTextField = "CompanyLotNo";
                                ddlLotNo.DataValueField = "LotDetailsID";
                                ddlLotNo.DataBind();
                                ddlLotNo.Items.Insert(0, "Select Lot No");
                            } DataSet dlotprocess = new DataSet();
                            dlotprocess = objbs.Get_LotDetails(Convert.ToString(dgetcheck.Tables[0].Rows[0]["LotDetailID"]), "3", "Stc");
                            txttotalqty.Text = dlotprocess.Tables[0].Rows[0]["TotalQuantity"].ToString();

                            if (dlotprocess.Tables[0].Rows[0]["Jobwork"].ToString() == "Out")
                            {
                                DataSet drpEmpp1 = objbs.Selectname("10");
                                drpMultiemployee.DataSource = drpEmpp1;
                                drpMultiemployee.DataTextField = "LedgerName";
                                drpMultiemployee.DataValueField = "LedgerID";
                                drpMultiemployee.DataBind();
                                drpMultiemployee.Items.Insert(0, "Select Name");
                                //emp.Visible = false;
                                //job.Visible = true;
                            }
                            else if (dlotprocess.Tables[0].Rows[0]["Jobwork"].ToString() == "In")
                            {
                                DataSet drpEmpp1 = objbs.Selectname("9");
                                drpMultiemployee.DataSource = drpEmpp1;
                                drpMultiemployee.DataTextField = "LedgerName";
                                drpMultiemployee.DataValueField = "LedgerID";
                                drpMultiemployee.DataBind();
                                drpMultiemployee.Items.Insert(0, "Select Name");
                                //emp.Visible = true;
                                //job.Visible = false;
                            }
                            //////  btnadd.Visible = true;
                            ////// btnadd.Text = "Update";
                            txtid.Text = dgetcheck.Tables[0].Rows[0]["Stichingid"].ToString();
                            txtmultiid.Text = dgetcheck.Tables[0].Rows[0]["Stichingid"].ToString();
                            txtmultiplecode.Text = dgetcheck.Tables[0].Rows[0]["Stichingid"].ToString();
                            txtmultiplecode.Enabled = false;
                            ddlLotNo.SelectedValue = dgetcheck.Tables[0].Rows[0]["LotDetailID"].ToString();
                            ddlLotNo.Enabled = false;
                            lbllotno.Text = dgetcheck.Tables[0].Rows[0]["LotNo"].ToString();
                            drpMultiemployee.SelectedValue = dgetcheck.Tables[0].Rows[0]["WORKERID"].ToString();
                            drpMultiemployee.Enabled = false;
                            txtmultidate.Text = Convert.ToDateTime(dgetcheck.Tables[0].Rows[0]["date"]).ToString("dd/MM/yyyy");
                            txtmultidate.Enabled = false;
                            txttotalqty.Text = dgetcheck.Tables[0].Rows[0]["Totalqty"].ToString();
                            txttotalqty.Enabled = false;
                            txtAmount.Text = dgetcheck.Tables[0].Rows[0]["TotalAmount"].ToString();
                            txtAmount.Enabled = false;
                            DataSet ds = objbs.Get_JpStiching(Convert.ToString(Stitchingid), "3", "Stc");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                #region

                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                                dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                                dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"].ToString()) != 0)
                                    {
                                        DataRow dr = dtt.NewRow();

                                        dr["OrderNo"] = "0";
                                        dr["fitid"] = ds.Tables[0].Rows[i]["fitid"].ToString();
                                        dr["fit"] = ds.Tables[0].Rows[i]["fit"].ToString();
                                        dr["ProcessTypeID"] = ds.Tables[0].Rows[i]["ProcessID"].ToString();
                                        dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                                        dr["TotalQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["totalQty"]);
                                        dr["RemainQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"]);
                                        dr["senddate"] = ds.Tables[0].Rows[i]["senddate"].ToString();
                                        dr["RecQty"] = "0";
                                        dr["Recdate"] = "0";
                                        dr["Damageqty"] = "0";
                                        dr["patternid"] = ds.Tables[0].Rows[i]["patternid"].ToString();
                                        dr["pattern"] = ds.Tables[0].Rows[i]["patternname"].ToString();
                                        dr["itemname"] = ds.Tables[0].Rows[i]["itemname"].ToString();
                                        temp.Tables[0].Rows.Add(dr);
                                    }
                                }

                                ViewState["CurrentTable1"] = dtt;

                                gvcustomerorder.DataSource = temp;
                                gvcustomerorder.DataBind();

                                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");

                                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                {
                                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                                    Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                                    Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                                    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                                    Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                                    Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                                    Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                                    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                                    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                                    TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                                    string recdate = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;
                                    string date11 = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;



                                    //Recdate.Enabled = false;
                                    txtrecFQty.Enabled = true;
                                    //drpLotno.Enabled = false;
                                    drpProcess.Enabled = false;
                                    txtsendFQty.Enabled = false;
                                    txtdamageqty.Enabled = true;
                                    Recdate.Enabled = false;
                                    date1.Enabled = false;

                                    txtsendFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                                    txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                                    lblfit.Text = temp.Tables[0].Rows[i]["fit"].ToString();
                                    lblitemname.Text = temp.Tables[0].Rows[i]["itemname"].ToString();
                                    lblfitid.Text = temp.Tables[0].Rows[i]["fitid"].ToString();
                                    txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                                    drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    lblPattern.Text = temp.Tables[0].Rows[i]["Pattern"].ToString();
                                    lblPatternid.Text = temp.Tables[0].Rows[i]["Patternid"].ToString();
                                    txtdamageqty.Text = temp.Tables[0].Rows[i]["damageqty"].ToString();
                                    txtrecFQty.Text = temp.Tables[0].Rows[i]["recqty"].ToString();
                                    date1.Text = temp.Tables[0].Rows[i]["senddate"].ToString();
                                    Recdate.Text = recdate.ToString();
                                }

                                #endregion


                            }
                            else
                            {
                                FirstGridViewRow();

                            }


                            ddlLotNo.Enabled = false;

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Check Lot No.Or Contact Administrator.')", true);
                        return;
                    }

                    #endregion
                }
                else
                {


                }

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        }

        private void SetRowData()
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

                        //DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendFQty");
                        TextBox txtremainQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRemainQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRate");
                        Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                        Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                        Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");

                        Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                        Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                        TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("Recdate");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["RecQuantity"] = txtrecFQty.Text;
                        dtCurrentTable.Rows[i - 1]["SendQty"] = txtsendFQty.Text;
                        dtCurrentTable.Rows[i - 1]["RemainQty"] = txtremainQty.Text;
                        dtCurrentTable.Rows[i - 1]["date"] = date.Text;
                        dtCurrentTable.Rows[i - 1]["Recdate"] = date.Text;
                        //dtCurrentTable.Rows[i - 1]["LotNo"] = drpLotno.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["damageqty"] = txtdamageqty.Text;
                        dtCurrentTable.Rows[i - 1]["fit"] = lblfit.Text;
                        dtCurrentTable.Rows[i - 1]["itemname"] = lblitemname.Text;
                        dtCurrentTable.Rows[i - 1]["Pattern"] = lblPattern.Text;
                        dtCurrentTable.Rows[i - 1]["Patternid"] = lblPatternid.Text;
                        dtCurrentTable.Rows[i - 1]["fitid"] = lblfitid.Text;
                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }
        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Process", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
            dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
            dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
            dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
            dtt.Columns.Add(new DataColumn("date", typeof(string)));
            dtt.Columns.Add(new DataColumn("Recdate", typeof(string)));
            dtt.Columns.Add(new DataColumn("Bundle", typeof(string)));

            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["LotNo"] = string.Empty;
            dr["Process"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["SendQty"] = string.Empty;
            dr["TotalQty"] = string.Empty;
            dr["RemainQty"] = string.Empty;
            dr["RecQuantity"] = string.Empty;
            dr["date"] = string.Empty;
            dr["Recdate"] = string.Empty;
            dr["Bundle"] = string.Empty;

            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("LotNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Process");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("TotalQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SendQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("RemainQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("RecQuantity");
            dttt.Columns.Add(dct);

            dct = new DataColumn("date");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Recdate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Bundle");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["OrderNo"] = "";
            drNew["LotNo"] = "";
            drNew["Process"] = "";
            drNew["Rate"] = "";
            drNew["SendQty"] = "";
            drNew["TotalQty"] = "";
            drNew["RemainQty"] = "";
            drNew["Bundle"] = "";
            drNew["RecQuantity"] = "";
            drNew["date"] = DateTime.Now.ToString("dd/MM/yyyy");
            drNew["Recdate"] = DateTime.Now.ToString("dd/MM/yyyy");

            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }
        private void AddNewRow()
        {
            DateTime Recdate1 = DateTime.Now;
            DateTime date1 = DateTime.Now;
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                TextBox txtremainQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRemainQty");
                TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                //   DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpEmp");
                TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");

                // DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                if (drpProcess.SelectedValue == "Select Process Type")
                {

                }
            }

            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {

                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendFQty");
                        TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRemainQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRate");
                        TextBox txtBundle = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtBundle");
                        //   DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpEmp");
                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        if (date.Text == "")
                        {
                            System.Globalization.CultureInfo cultureinfo =
                            new System.Globalization.CultureInfo("nl-NL");
                            date1 = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), cultureinfo);

                        }
                        else
                        {
                            System.Globalization.CultureInfo cultureinfo =
                           new System.Globalization.CultureInfo("nl-NL");
                            date1 = DateTime.Parse(Convert.ToDateTime(date.Text).ToString("dd/MM/yyyy HH:mm:ss"), cultureinfo);
                        }
                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("Recdate");
                        if (Recdate.Text == "")
                        {
                            System.Globalization.CultureInfo cultureinfo =
                            new System.Globalization.CultureInfo("nl-NL");
                            Recdate1 = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), cultureinfo);

                        }
                        else
                        {
                            System.Globalization.CultureInfo cultureinfo =
                          new System.Globalization.CultureInfo("nl-NL");
                            Recdate1 = DateTime.Parse(Convert.ToDateTime(Recdate.Text).ToString("dd/MM/yyyy HH:mm:ss"), cultureinfo);
                        }

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["RecQuantity"] = txtrecFQty.Text;
                        dtCurrentTable.Rows[i - 1]["SendQty"] = txtsendFQty.Text;
                        dtCurrentTable.Rows[i - 1]["RemainQty"] = txtsendFQty.Text;
                        dtCurrentTable.Rows[i - 1]["date"] = date1.ToString();
                        dtCurrentTable.Rows[i - 1]["Recdate"] = Recdate1;
                        dtCurrentTable.Rows[i - 1]["LotNo"] = drpLotno.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Bundle"] = txtBundle.Text;
                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();

            //for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //{
            //    DropDownList drpProcess =
            // (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

            //    drpProcess.Focus();
            //}
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("date");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("date");
                    if (oldtxttk.Text == "")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            }
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        //DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendFQty");
                        TextBox txtremainQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRemainQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRate");
                        Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                        Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                        Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                        TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("Recdate");

                        Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                        Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");

                        drpProcess.SelectedValue = dt.Rows[i]["Process"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtrecFQty.Text = dt.Rows[i]["RecQuantity"].ToString();
                        //drpLotno.SelectedValue = dt.Rows[i]["LotNo"].ToString();
                        date.Text = dt.Rows[i]["date"].ToString();
                        Recdate.Text = dt.Rows[i]["Recdate"].ToString();
                        txtdamageqty.Text = dt.Rows[i]["Damageqty"].ToString();
                        txtsendFQty.Text = dt.Rows[i]["SendQty"].ToString();
                        txtremainQty.Text = dt.Rows[i]["RemainQty"].ToString();
                        lblPattern.Text = dt.Rows[i]["Pattern"].ToString();
                        lblPatternid.Text = dt.Rows[i]["Patternid"].ToString();
                        lblitemname.Text = dt.Rows[i]["itemname"].ToString();
                        lblfit.Text = dt.Rows[i]["fit"].ToString();
                        lblfitid.Text = dt.Rows[i]["fitid"].ToString();
                        rowIndex++;
                        drpProcess.Focus();
                    }
                }
            }
        }

        protected void chkprocess_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            #region
            string Processid = "";
            DataSet ds2 = new DataSet();
            #region
            if (chkprocess.SelectedValue == "2")
            {

                ds2 = objbs.getalterprocess("tblJpStiching", "tblTransJpStiching", "StichingId");
                Processid = "StichingId";
                //lblheadingname.Text = "Stitching Issued WorkOrder -";
                //TableName = "tblJpStiching";
                //lbllContrasts.Text = "Contrast :";
            }
            else if (chkprocess.SelectedValue == "3")
            {
                ds2 = objbs.getalterprocess("tblJpEmbroiding", "tblTransJpEmbroiding", "EmbroidingId");
                Processid = "EmbroidingId";
                //lblheadingname.Text = "Embroiding Issued  WorkOrder -";
                //TableName = "tblJpEmbroiding";
                //lbllContrasts.Text = "Narration :";
            }
            else if (chkprocess.SelectedValue == "1")
            {
                ds2 = objbs.getalterprocess("tblJpKajaButton", "tblTransJpKajaButton", "KajaButtonId");
                Processid = "KajaButtonId";
                //lblheadingname.Text = "KajaButton Issued WorkOrder -";
                //TableName = "tblJpKajaButton";
                //lbllContrasts.Text = "Narration :";
            }
            else if (chkprocess.SelectedValue == "7")
            {
                ds2 = objbs.getalterprocess("tblJpPrinting", "tblTransJpPrinting", "PrintingId");
                Processid = "PrintingId";
                //lblheadingname.Text = "Printing Issued WorkOrder -";
                //TableName = "tblJpPrinting";
                //lbllContrasts.Text = "Narration :";
            }
            else if (chkprocess.SelectedValue == "4")
            {
                ds2 = objbs.getalterprocess("tblJpWashing", "tblTransJpWashing", "WashingId");
                Processid = "WashingId";
                //lblheadingname.Text = "Washing Issued WorkOrder -";
                //TableName = "tblJpWashing";
                //lbllContrasts.Text = "Narration :";
            }
            else if (chkprocess.SelectedValue == "8")
            {
                ds2 = objbs.getalterprocess("tblJpBarTag", "tblTransJpBarTag", "BarTagId");
                Processid = "BarTagId";
                //lblheadingname.Text = "BarTag Issued WorkOrder -";
                //TableName = "tblJpBarTag";
                //lbllContrasts.Text = "Narration :";
            }
            else if (chkprocess.SelectedValue == "9")
            {
                ds2 = objbs.getalterprocess("tblJpTrimming", "tblTransJpTrimming", "TrimmingId");
                Processid = "TrimmingId";
                //lblheadingname.Text = "Trimming Issued WorkOrder -";
                //TableName = "tblJpTrimming";
                //lbllContrasts.Text = "Narration :";
            }
            else if (chkprocess.SelectedValue == "10")
            {
                ds2 = objbs.getalterprocess("tblJpConsai", "tblTransJpConsai", "ConsaiId");
                Processid = "ConsaiId";
                //lblheadingname.Text = "Consai Issued WorkOrder -";
                //TableName = "tblJpConsai";
                //lbllContrasts.Text = "Narration :";
            }
            else if (chkprocess.SelectedValue == "5")
            {
                ds2 = objbs.getalterprocess("tblJpIroning", "tblTransJpIroning", "IroningId");
                Processid = "IroningId";
                //lblheadingname.Text = "Consai Issued WorkOrder -";
                //TableName = "tblJpConsai";
                //lbllContrasts.Text = "Narration :";
            }



            #endregion
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlLotNo.DataSource = ds2.Tables[0];
                ddlLotNo.DataTextField = "CompanyLotNo";
                ddlLotNo.DataValueField = Processid;
                ddlLotNo.DataBind();
                ddlLotNo.Items.Insert(0, "Select LotNo");

            }
            else
            {
                ddlLotNo.Items.Insert(0, "Select LotNo");
            }
            #endregion
        }

        protected void ddlLotNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");

            if (ddlLotNo.SelectedValue != "Select LotNo")
            {
                string Processid = "";
                DataSet ds2 = new DataSet();
                #region
                if (chkprocess.SelectedValue == "2")
                {
                    ds2 = objbs.getalterprocess("tblJpStiching", "tblTransJpStiching", "StichingId");
                    Processid = "StichingId";
                }
                else if (chkprocess.SelectedValue == "3")
                {
                    ds2 = objbs.getalterprocess("tblJpEmbroiding", "tblTransJpEmbroiding", "EmbroidingId");
                    Processid = "EmbroidingId";
                }
                else if (chkprocess.SelectedValue == "1")
                {
                    ds2 = objbs.getalterprocess("tblJpKajaButton", "tblTransJpKajaButton", "KajaButtonId");
                    Processid = "KajaButtonId";
                }
                else if (chkprocess.SelectedValue == "7")
                {
                    ds2 = objbs.getalterprocess("tblJpPrinting", "tblTransJpPrinting", "PrintingId");
                    Processid = "PrintingId";
                }
                else if (chkprocess.SelectedValue == "4")
                {
                    ds2 = objbs.getalterprocess("tblJpWashing", "tblTransJpWashing", "WashingId");
                    Processid = "WashingId";
                }
                else if (chkprocess.SelectedValue == "8")
                {
                    ds2 = objbs.getalterprocess("tblJpBarTag", "tblTransJpBarTag", "BarTagId");
                    Processid = "BarTagId";
                }
                else if (chkprocess.SelectedValue == "9")
                {
                    ds2 = objbs.getalterprocess("tblJpTrimming", "tblTransJpTrimming", "TrimmingId");
                    Processid = "TrimmingId";
                }
                else if (chkprocess.SelectedValue == "10")
                {
                    ds2 = objbs.getalterprocess("tblJpConsai", "tblTransJpConsai", "ConsaiId");
                    Processid = "ConsaiId";
                }
                else if (chkprocess.SelectedValue == "5")
                {
                    ds2 = objbs.getalterprocess("tblJpIroning", "tblTransJpIroning", "IroningId");
                    Processid = "IroningId";
                }



                #endregion

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region
                    double stitchrate = 0;

                    //DataSet rateforprecost = objbs.getrateforprecost(Convert.ToString(ddlLotNo.SelectedItem.Text), "STITCHING");
                    //if (rateforprecost.Tables[0].Rows.Count > 0)
                    //{
                    //    stitchrate = Convert.ToDouble(rateforprecost.Tables[0].Rows[0]["Cost"].ToString());
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please to Entry Cost in Master Cutting for this " + ddlLotNo.SelectedItem.Text + ". Thank you');", true);
                    //    return;

                    //}

                    DataSet dlotprocess = new DataSet();
                   // if (dlotprocess.Tables[0].Rows.Count > 0)
                    {
                        #region

                        //DataSet temp = new DataSet();
                        //DataTable dtt = new DataTable();

                        //dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                        ////dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                        //temp.Tables.Add(dtt);


                        //double tot = 0;
                        //double rate = 0;
                        //double TotalQty = 0;
                        //double TotalQty1 = 0;
                        //for (int i = 0; i < dlotprocess.Tables[0].Rows.Count; i++)
                        //{

                        //    DataRow dr = dtt.NewRow();

                        //    dr["fit"] = dlotprocess.Tables[0].Rows[i]["fit"].ToString();
                        //    dr["itemname"] = dlotprocess.Tables[0].Rows[i]["itemname"].ToString();
                        //    dr["TotalQty"] = dlotprocess.Tables[0].Rows[i]["SendTotalQty"].ToString();
                        //    dr["SendQty"] = dlotprocess.Tables[0].Rows[i]["SendTotalQty"].ToString();
                        //    dr["RemainQty"] = dlotprocess.Tables[0].Rows[i]["SendTotalQty"].ToString();
                        //    dr["ProcessTypeID"] = dlotprocess.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                        //    dr["rate"] = dlotprocess.Tables[0].Rows[i]["rate"].ToString();
                        //    dr["damageqty"] = "0";
                        //    dr["RecQty"] = "0";
                        //    TotalQty = Convert.ToDouble(dlotprocess.Tables[0].Rows[i]["TotalQty"]);
                        //    TotalQty1 = TotalQty1 + TotalQty;
                        //    rate = Convert.ToDouble(dlotprocess.Tables[0].Rows[i]["rate"]);
                        //    tot = tot + (TotalQty * rate);
                        //    dr["Fitid"] = dlotprocess.Tables[0].Rows[i]["Fitid"].ToString();
                        //    dr["Pattern"] = dlotprocess.Tables[0].Rows[i]["PatternName"].ToString();
                        //    dr["Patternid"] = dlotprocess.Tables[0].Rows[i]["Patternid"].ToString();

                        //    temp.Tables[0].Rows.Add(dr);

                        //}

                        //txttotalqty.Text = TotalQty1.ToString("0.00");

                        //txtAmount.Text = tot.ToString();

                        //ViewState["CurrentTable1"] = dtt;

                        //gvcustomerorder.DataSource = temp;
                        //gvcustomerorder.DataBind();

                        //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        //{
                        //    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                        //    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                        //    Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                        //    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                        //    TextBox txtTotalFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTotalFQty");
                        //    Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                        //    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                        //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                        //    Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                        //    Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                        //    Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                        //    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                        //    TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                        //    TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                        //    string recdate = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;
                        //    string date11 = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;

                        //    txtsendFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                        //    txtTotalFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                        //    txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                        //    lblfit.Text = temp.Tables[0].Rows[i]["fit"].ToString();
                        //    lblitemname.Text = temp.Tables[0].Rows[i]["itemname"].ToString();
                        //    lblfitid.Text = temp.Tables[0].Rows[i]["fitid"].ToString();
                        //    //txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                        //    txtrate.Text = stitchrate.ToString();
                        //    drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                        //    lblPattern.Text = temp.Tables[0].Rows[i]["Pattern"].ToString();
                        //    lblPatternid.Text = temp.Tables[0].Rows[i]["Patternid"].ToString();
                        //    txtdamageqty.Text = temp.Tables[0].Rows[i]["damageqty"].ToString();
                        //    date1.Text = date11.ToString();
                        //    Recdate.Text = recdate.ToString();
                        //    txtrecFQty.Text = temp.Tables[0].Rows[i]["recQty"].ToString();
                        //    txtdamageqty.Text = "0";
                        //    txtrecFQty.Text = "0";
                        //}
                        //gvcustomerorder.Columns[11].Visible = false;
                        //gvcustomerorder.Columns[12].Visible = false;
                      

                        #endregion
                    }

                    #endregion

                    btnadd.Enabled = true;

                    #region


                    DataSet dssstichinglot = new DataSet();
                    DataTable dtstch = new DataTable();

                    DataSet dsstichinglot = new DataSet();

                    #region
                    if (chkprocess.SelectedValue == "2")
                    {
                        Processid = "StichingId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpStiching", "StichingId",Convert.ToInt32(ddlLotNo.SelectedValue));
                    }
                    else if (chkprocess.SelectedValue == "3")
                    {
                        Processid = "EmbroidingId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpEmbroiding", "EmbroidingId", Convert.ToInt32(ddlLotNo.SelectedValue));
                    }
                    else if (chkprocess.SelectedValue == "1")
                    {
                        Processid = "KajaButtonId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpKajaButton", "KajaButtonId", Convert.ToInt32(ddlLotNo.SelectedValue));
                    }
                    else if (chkprocess.SelectedValue == "7")
                    {
                        Processid = "PrintingId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpPrinting", "PrintingId", Convert.ToInt32(ddlLotNo.SelectedValue));
                    }
                    else if (chkprocess.SelectedValue == "4")
                    {
                        Processid = "WashingId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpWashing", "WashingId", Convert.ToInt32(ddlLotNo.SelectedValue));
                    }
                    else if (chkprocess.SelectedValue == "8")
                    {
                        Processid = "BarTagId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpBarTag", "BarTagId", Convert.ToInt32(ddlLotNo.SelectedValue));
                    }
                    else if (chkprocess.SelectedValue == "9")
                    {
                        Processid = "TrimmingId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpTrimming", "TrimmingId", Convert.ToInt32(ddlLotNo.SelectedValue));
                    }
                    else if (chkprocess.SelectedValue == "10")
                    {
                        Processid = "ConsaiId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpConsai", "ConsaiId", Convert.ToInt32(ddlLotNo.SelectedValue));
                    }
                    else if (chkprocess.SelectedValue == "5")
                    {
                        Processid = "IroningId";
                        dsstichinglot = objbs.getallprodamage("tblTransJpIroning", "IroningId", Convert.ToInt32(ddlLotNo.SelectedValue));
                    }



                    #endregion

                  

                    dtstch.Columns.Add(new DataColumn("TransId", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("Masterid", typeof(string)));
                   // dtstch.Columns.Add(new DataColumn("Transfabid", typeof(string)));

                    dtstch.Columns.Add(new DataColumn("Design", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("30fs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("32fs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("34fs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("36fs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("xsfs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("sfs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("mfs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("lfs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("xlfs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("xxlfs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("3xlfs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("4xlfs", typeof(string)));

                    dtstch.Columns.Add(new DataColumn("30hs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("32hs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("34hs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("36hs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("xshs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("shs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("mhs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("lhs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("xlhs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("xxlhs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("3xlhs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("4xlhs", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("TtlQty", typeof(string)));

                    dssstichinglot.Tables.Add(dtstch);

                    for (int i = 0; i < dsstichinglot.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dtstch.NewRow();
                        dr["Transid"] = dsstichinglot.Tables[0].Rows[i]["transid"].ToString();
                        dr["Masterid"] = dsstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                       // dr["Transfabid"] = dsstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                        dr["Design"] = dsstichinglot.Tables[0].Rows[i]["ItemName"].ToString();
                        dr["30fs"] = dsstichinglot.Tables[0].Rows[i]["D30fs"].ToString();
                        dr["32fs"] = dsstichinglot.Tables[0].Rows[i]["D32fs"].ToString();
                        dr["34fs"] = dsstichinglot.Tables[0].Rows[i]["D34fs"].ToString();
                        dr["36fs"] = dsstichinglot.Tables[0].Rows[i]["D36fs"].ToString();
                        dr["xsfs"] = dsstichinglot.Tables[0].Rows[i]["Dxsfs"].ToString();
                        dr["sfs"] = dsstichinglot.Tables[0].Rows[i]["Dsfs"].ToString();
                        dr["mfs"] = dsstichinglot.Tables[0].Rows[i]["Dmfs"].ToString();
                        dr["lfs"] = dsstichinglot.Tables[0].Rows[i]["Dlfs"].ToString();
                        dr["xlfs"] = dsstichinglot.Tables[0].Rows[i]["Dxlfs"].ToString();
                        dr["xxlfs"] = dsstichinglot.Tables[0].Rows[i]["Dxxlfs"].ToString();
                        dr["3xlfs"] = dsstichinglot.Tables[0].Rows[i]["D3xlfs"].ToString();
                        dr["4xlfs"] = dsstichinglot.Tables[0].Rows[i]["D4xlfs"].ToString();

                        dr["30hs"] = dsstichinglot.Tables[0].Rows[i]["D30hs"].ToString();
                        dr["32hs"] = dsstichinglot.Tables[0].Rows[i]["D32hs"].ToString();
                        dr["34hs"] = dsstichinglot.Tables[0].Rows[i]["D34hs"].ToString();
                        dr["36hs"] = dsstichinglot.Tables[0].Rows[i]["D36hs"].ToString();
                        dr["xshs"] = dsstichinglot.Tables[0].Rows[i]["Dxshs"].ToString();
                        dr["shs"] = dsstichinglot.Tables[0].Rows[i]["Dshs"].ToString();
                        dr["mhs"] = dsstichinglot.Tables[0].Rows[i]["Dmhs"].ToString();
                        dr["lhs"] = dsstichinglot.Tables[0].Rows[i]["Dlhs"].ToString();
                        dr["xlhs"] = dsstichinglot.Tables[0].Rows[i]["Dxlhs"].ToString();
                        dr["xxlhs"] = dsstichinglot.Tables[0].Rows[i]["Dxxlhs"].ToString();
                        dr["3xlhs"] = dsstichinglot.Tables[0].Rows[i]["D3xlhs"].ToString();
                        dr["4xlhs"] = dsstichinglot.Tables[0].Rows[i]["D4xlhs"].ToString();
                        dr["TtlQty"] = dsstichinglot.Tables[0].Rows[i]["TotalQty"].ToString();


                        dssstichinglot.Tables[0].Rows.Add(dr);

                    }
                    gvnewstiching.DataSource = dssstichinglot;
                    gvnewstiching.DataBind();

                    for (int i = 0; i < gvnewstiching.Rows.Count; i++)
                    {
                        Label lblTransid = (Label)gvnewstiching.Rows[i].FindControl("lblTransid");
                        Label lblMasterid = (Label)gvnewstiching.Rows[i].FindControl("lblMasterid");
                       // Label lblTransfabid = (Label)gvnewstiching.Rows[i].FindControl("lblTransfabid");


                        Label lbldesignno = (Label)gvnewstiching.Rows[i].FindControl("lbldesignno");
                        TextBox txts30fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fs");
                        TextBox txts32fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fs");
                        TextBox txts34fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fs");
                        TextBox txts36fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fs");
                        TextBox txtsxsfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfs");
                        TextBox txtssfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfs");
                        TextBox txtsmfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfs");
                        TextBox txtslfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfs");
                        TextBox txtsxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfs");
                        TextBox txtsxxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfs");
                        TextBox txts3xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfs");
                        TextBox txts4xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfs");

                        TextBox txts30hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hs");
                        TextBox txts32hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hs");
                        TextBox txts34hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hs");
                        TextBox txts36hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hs");
                        TextBox txtsxshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshs");
                        TextBox txtsshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshs");
                        TextBox txtsmhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhs");
                        TextBox txtslhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhs");
                        TextBox txtsxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhs");
                        TextBox txtsxxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhs");
                        TextBox txts3xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhs");
                        TextBox txts4xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhs");

                        TextBox txtsendFQty = (TextBox)gvnewstiching.Rows[i].FindControl("txtsendFQty");

                        lblTransid.Text = dssstichinglot.Tables[0].Rows[i]["Transid"].ToString();
                        lblMasterid.Text = dssstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                       // lblTransfabid.Text = dssstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                        lbldesignno.Text = dssstichinglot.Tables[0].Rows[i]["Design"].ToString();
                        txts30fs.Text = dssstichinglot.Tables[0].Rows[i]["30fs"].ToString();
                        txts32fs.Text = dssstichinglot.Tables[0].Rows[i]["32fs"].ToString();
                        txts34fs.Text = dssstichinglot.Tables[0].Rows[i]["34fs"].ToString();
                        txts36fs.Text = dssstichinglot.Tables[0].Rows[i]["36fs"].ToString();
                        txtsxsfs.Text = dssstichinglot.Tables[0].Rows[i]["xsfs"].ToString();
                        txtssfs.Text = dssstichinglot.Tables[0].Rows[i]["sfs"].ToString();
                        txtsmfs.Text = dssstichinglot.Tables[0].Rows[i]["mfs"].ToString();
                        txtslfs.Text = dssstichinglot.Tables[0].Rows[i]["lfs"].ToString();
                        txtsxlfs.Text = dssstichinglot.Tables[0].Rows[i]["xlfs"].ToString();
                        txtsxxlfs.Text = dssstichinglot.Tables[0].Rows[i]["xxlfs"].ToString();
                        txts3xlfs.Text = dssstichinglot.Tables[0].Rows[i]["3xlfs"].ToString();
                        txts4xlfs.Text = dssstichinglot.Tables[0].Rows[i]["4xlfs"].ToString();

                        txts30hs.Text = dssstichinglot.Tables[0].Rows[i]["30hs"].ToString();
                        txts32hs.Text = dssstichinglot.Tables[0].Rows[i]["32hs"].ToString();
                        txts34hs.Text = dssstichinglot.Tables[0].Rows[i]["34hs"].ToString();
                        txts36hs.Text = dssstichinglot.Tables[0].Rows[i]["36hs"].ToString();
                        txtsxshs.Text = dssstichinglot.Tables[0].Rows[i]["xshs"].ToString();
                        txtsshs.Text = dssstichinglot.Tables[0].Rows[i]["shs"].ToString();
                        txtsmhs.Text = dssstichinglot.Tables[0].Rows[i]["mhs"].ToString();
                        txtslhs.Text = dssstichinglot.Tables[0].Rows[i]["lhs"].ToString();
                        txtsxlhs.Text = dssstichinglot.Tables[0].Rows[i]["xlhs"].ToString();
                        txtsxxlhs.Text = dssstichinglot.Tables[0].Rows[i]["xxlhs"].ToString();
                        txts3xlhs.Text = dssstichinglot.Tables[0].Rows[i]["3xlhs"].ToString();
                        txts4xlhs.Text = dssstichinglot.Tables[0].Rows[i]["4xlhs"].ToString();

                        txtsendFQty.Text = dssstichinglot.Tables[0].Rows[i]["TtlQty"].ToString();


                        TextBox txts30fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fsac");
                        TextBox txts32fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fsac");
                        TextBox txts34fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fsac");
                        TextBox txts36fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fsac");
                        TextBox txtsxsfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfsac");
                        TextBox txtssfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfsac");
                        TextBox txtsmfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfsac");
                        TextBox txtslfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfsac");
                        TextBox txtsxlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfsac");
                        TextBox txtsxxlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfsac");
                        TextBox txts3xlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfsac");
                        TextBox txts4xlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfsac");

                        TextBox txts30hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hsac");
                        TextBox txts32hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hsac");
                        TextBox txts34hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hsac");
                        TextBox txts36hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hsac");
                        TextBox txtsxshsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshsac");
                        TextBox txtsshsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshsac");
                        TextBox txtsmhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhsac");
                        TextBox txtslhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhsac");
                        TextBox txtsxlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhsac");
                        TextBox txtsxxlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhsac");
                        TextBox txts3xlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhsac");
                        TextBox txts4xlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhsac");

                        TextBox txtsendFQtyac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsendFQtyac");

                        txts30fsac.Text = dssstichinglot.Tables[0].Rows[i]["30fs"].ToString();
                        txts32fsac.Text = dssstichinglot.Tables[0].Rows[i]["32fs"].ToString();
                        txts34fsac.Text = dssstichinglot.Tables[0].Rows[i]["34fs"].ToString();
                        txts36fsac.Text = dssstichinglot.Tables[0].Rows[i]["36fs"].ToString();
                        txtsxsfsac.Text = dssstichinglot.Tables[0].Rows[i]["xsfs"].ToString();
                        txtssfsac.Text = dssstichinglot.Tables[0].Rows[i]["sfs"].ToString();
                        txtsmfsac.Text = dssstichinglot.Tables[0].Rows[i]["mfs"].ToString();
                        txtslfsac.Text = dssstichinglot.Tables[0].Rows[i]["lfs"].ToString();
                        txtsxlfsac.Text = dssstichinglot.Tables[0].Rows[i]["xlfs"].ToString();
                        txtsxxlfsac.Text = dssstichinglot.Tables[0].Rows[i]["xxlfs"].ToString();
                        txts3xlfsac.Text = dssstichinglot.Tables[0].Rows[i]["3xlfs"].ToString();
                        txts4xlfsac.Text = dssstichinglot.Tables[0].Rows[i]["4xlfs"].ToString();

                        txts30hsac.Text = dssstichinglot.Tables[0].Rows[i]["30hs"].ToString();
                        txts32hsac.Text = dssstichinglot.Tables[0].Rows[i]["32hs"].ToString();
                        txts34hsac.Text = dssstichinglot.Tables[0].Rows[i]["34hs"].ToString();
                        txts36hsac.Text = dssstichinglot.Tables[0].Rows[i]["36hs"].ToString();
                        txtsxshsac.Text = dssstichinglot.Tables[0].Rows[i]["xshs"].ToString();
                        txtsshsac.Text = dssstichinglot.Tables[0].Rows[i]["shs"].ToString();
                        txtsmhsac.Text = dssstichinglot.Tables[0].Rows[i]["mhs"].ToString();
                        txtslhsac.Text = dssstichinglot.Tables[0].Rows[i]["lhs"].ToString();
                        txtsxlhsac.Text = dssstichinglot.Tables[0].Rows[i]["xlhs"].ToString();
                        txtsxxlhsac.Text = dssstichinglot.Tables[0].Rows[i]["xxlhs"].ToString();
                        txts3xlhsac.Text = dssstichinglot.Tables[0].Rows[i]["3xlhs"].ToString();
                        txts4xlhsac.Text = dssstichinglot.Tables[0].Rows[i]["4xlhs"].ToString();

                        txtsendFQtyac.Text = dssstichinglot.Tables[0].Rows[i]["TtlQty"].ToString();



                    }
                    #endregion

                }
                else
                {
                    gvcustomerorder.DataSource = null;
                    gvcustomerorder.DataBind();
                    btnadd.Enabled = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Ready To Stiching Process.Thank You!!!')", true);
                    return;
                }

            }

        }
        protected void btncalc_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gvnewstiching.Rows.Count; i++)
            {
                int Row = i + 1;

                #region Qty Check


                TextBox txts30fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fs");
                TextBox txts30fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fsac");
                if (Convert.ToInt32(txts30fsac.Text) < Convert.ToInt32(txts30fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 30FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts32fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fs");
                TextBox txts32fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fsac");
                if (Convert.ToInt32(txts32fsac.Text) < Convert.ToInt32(txts32fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 32FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts34fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fs");
                TextBox txts34fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fsac");
                if (Convert.ToInt32(txts34fsac.Text) < Convert.ToInt32(txts34fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 34FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts36fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fs");
                TextBox txts36fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fsac");
                if (Convert.ToInt32(txts36fsac.Text) < Convert.ToInt32(txts36fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 36FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxsfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfs");
                TextBox txtsxsfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfsac");
                if (Convert.ToInt32(txtsxsfsac.Text) < Convert.ToInt32(txtsxsfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XSFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtssfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfs");
                TextBox txtssfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfsac");
                if (Convert.ToInt32(txtssfsac.Text) < Convert.ToInt32(txtssfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " SFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsmfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfs");
                TextBox txtsmfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfsac");
                if (Convert.ToInt32(txtsmfsac.Text) < Convert.ToInt32(txtsmfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " MFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtslfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfs");
                TextBox txtslfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfsac");
                if (Convert.ToInt32(txtslfsac.Text) < Convert.ToInt32(txtslfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " LFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfsac");
                if (Convert.ToInt32(txtsxlfsac.Text) < Convert.ToInt32(txtsxlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfs");
                TextBox txtsxxlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfsac");
                if (Convert.ToInt32(txtsxxlfsac.Text) < Convert.ToInt32(txtsxxlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XXLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts3xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfs");
                TextBox txts3xlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfsac");
                if (Convert.ToInt32(txts3xlfsac.Text) < Convert.ToInt32(txts3xlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 3XLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts4xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfs");
                TextBox txts4xlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfsac");
                if (Convert.ToInt32(txts4xlfsac.Text) < Convert.ToInt32(txts4xlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 4XLFS.Thank You!!!.')", true);
                    return;
                }


                TextBox txts30hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hs");
                TextBox txts30hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hsac");
                if (Convert.ToInt32(txts30hsac.Text) < Convert.ToInt32(txts30hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 30HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts32hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hs");
                TextBox txts32hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hsac");
                if (Convert.ToInt32(txts32hsac.Text) < Convert.ToInt32(txts32hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 32HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts34hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hs");
                TextBox txts34hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hsac");
                if (Convert.ToInt32(txts34hsac.Text) < Convert.ToInt32(txts34hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 34HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts36hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hs");
                TextBox txts36hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hsac");
                if (Convert.ToInt32(txts36hsac.Text) < Convert.ToInt32(txts36hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 36HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshs");
                TextBox txtsxshsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshsac");
                if (Convert.ToInt32(txtsxshsac.Text) < Convert.ToInt32(txtsxshs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XSHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshs");
                TextBox txtsshsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshsac");
                if (Convert.ToInt32(txtsshsac.Text) < Convert.ToInt32(txtsshs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " SHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsmhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhs");
                TextBox txtsmhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhsac");
                if (Convert.ToInt32(txtsmhsac.Text) < Convert.ToInt32(txtsmhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " MHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtslhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhs");
                TextBox txtslhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhsac");
                if (Convert.ToInt32(txtslhsac.Text) < Convert.ToInt32(txtslhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " LHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhsac");
                if (Convert.ToInt32(txtsxlhsac.Text) < Convert.ToInt32(txtsxlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhs");
                TextBox txtsxxlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhsac");
                if (Convert.ToInt32(txtsxxlhsac.Text) < Convert.ToInt32(txtsxxlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XXLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts3xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhs");
                TextBox txts3xlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhsac");
                if (Convert.ToInt32(txts3xlhsac.Text) < Convert.ToInt32(txts3xlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 3XLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts4xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhs");
                TextBox txts4xlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhsac");
                if (Convert.ToInt32(txts4xlhsac.Text) < Convert.ToInt32(txts4xlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 4XLHS.Thank You!!!.')", true);
                    return;
                }
                #endregion
            }

            double f30 = 0; double f32 = 0; double f34 = 0; double f36 = 0; double fxs = 0; double fs = 0;
            double fm = 0; double fl = 0; double fxl = 0; double fxxl = 0; double f3xl = 0; double f4xl = 0;

            double h30 = 0; double h32 = 0; double h34 = 0; double h36 = 0; double hxs = 0; double hs = 0;
            double hm = 0; double hl = 0; double hxl = 0; double hxxl = 0; double h3xl = 0; double h4xl = 0;

            double total = 0;
            for (int i = 0; i < gvnewstiching.Rows.Count; i++)
            {
                #region

                TextBox txtsendFQty = (TextBox)gvnewstiching.Rows[i].FindControl("txtsendFQty");

                TextBox txts30fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fs");
                f30 += Convert.ToDouble(txts30fs.Text);
                TextBox txts32fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fs");
                f32 += Convert.ToDouble(txts32fs.Text);
                TextBox txts34fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fs");
                f34 += Convert.ToDouble(txts34fs.Text);
                TextBox txts36fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fs");
                f36 += Convert.ToDouble(txts36fs.Text);
                TextBox txtsxsfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfs");
                fxs += Convert.ToDouble(txtsxsfs.Text);
                TextBox txtssfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfs");
                fs += Convert.ToDouble(txtssfs.Text);
                TextBox txtsmfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfs");
                fm += Convert.ToDouble(txtsmfs.Text);
                TextBox txtslfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfs");
                fl += Convert.ToDouble(txtslfs.Text);
                TextBox txtsxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfs");
                fxl += Convert.ToDouble(txtsxlfs.Text);
                TextBox txtsxxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfs");
                fxxl += Convert.ToDouble(txtsxxlfs.Text);
                TextBox txts3xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfs");
                f3xl += Convert.ToDouble(txts3xlfs.Text);
                TextBox txts4xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfs");
                f4xl += Convert.ToDouble(txts4xlfs.Text);

                TextBox txts30hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hs");
                h30 += Convert.ToDouble(txts30hs.Text);
                TextBox txts32hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hs");
                h32 += Convert.ToDouble(txts32hs.Text);
                TextBox txts34hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hs");
                h34 += Convert.ToDouble(txts34hs.Text);
                TextBox txts36hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hs");
                h36 += Convert.ToDouble(txts36hs.Text);
                TextBox txtsxshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshs");
                hxs += Convert.ToDouble(txtsxshs.Text);
                TextBox txtsshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshs");
                hs += Convert.ToDouble(txtsshs.Text);
                TextBox txtsmhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhs");
                hm += Convert.ToDouble(txtsmhs.Text);
                TextBox txtslhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhs");
                hl += Convert.ToDouble(txtslhs.Text);
                TextBox txtsxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhs");
                hxl += Convert.ToDouble(txtsxlhs.Text);
                TextBox txtsxxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhs");
                hxxl += Convert.ToDouble(txtsxxlhs.Text);
                TextBox txts3xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhs");
                h3xl += Convert.ToDouble(txts3xlhs.Text);
                TextBox txts4xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhs");
                h4xl += Convert.ToDouble(txts4xlhs.Text);

                txtsendFQty.Text = (Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text)).ToString();

                total += (Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text));

                #endregion
            }

            if (btnadd.Text == "Save")
            {
                TextBox sendFQty = (TextBox)gvcustomerorder.Rows[0].FindControl("txtsendFQty");
                sendFQty.Text = total.ToString();
            }
            else if (btnadd.Text == "Received")
            {
                if (ddltype.SelectedValue == "Damage")
                {
                    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[0].FindControl("txtrecFQty");
                    txtrecFQty.Text = 0.ToString();

                    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[0].FindControl("txtdamageqty");
                    txtdamageqty.Text = total.ToString();
                }
                else
                {
                    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[0].FindControl("txtdamageqty");
                    txtdamageqty.Text = 0.ToString();

                    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[0].FindControl("txtrecFQty");
                    txtrecFQty.Text = total.ToString();
                }
            }

            double grandtotalamount = 0;
            #region
            for (int ii = 0; ii < gvcustomerorder.Rows.Count; ii++)
            {
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[ii].FindControl("txtRate");
                TextBox txtsendFQtyn = (TextBox)gvcustomerorder.Rows[ii].FindControl("txtsendFQty");

                if (txtRate.Text == "" || txtRate.Text == "0" || txtRate.Text == "0.0000")
                {
                    if (txtsendFQtyn.Text == "0")
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate in  " + Convert.ToInt32(ii + 1) + " Row . Thank you!!');", true);
                        return;
                    }
                }



                if (btnadd.Text == "Received")
                {
                    if (ddltype.SelectedValue == "Damage")
                    {
                        TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[ii].FindControl("txtdamageqty");
                        grandtotalamount = grandtotalamount + (Convert.ToDouble(txtdamageqty.Text) * Convert.ToDouble(txtRate.Text));
                    }
                    else
                    {
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[ii].FindControl("txtrecFQty");
                        grandtotalamount = grandtotalamount + (Convert.ToDouble(txtrecFQty.Text) * Convert.ToDouble(txtRate.Text));
                    }
                }
                else
                {
                    TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[ii].FindControl("txtsendFQty");
                    grandtotalamount = grandtotalamount + (Convert.ToDouble(txtsendFQty1.Text) * Convert.ToDouble(txtRate.Text));
                }




            }
            #endregion
            txtAmount.Text = grandtotalamount.ToString("f2");
          //  sendqty_chnaged(sender, e);

            #region New BoundData

            DataSet dssstichinglot = new DataSet();
            DataTable dtstch = new DataTable();

            //int KAJABUTTON = 1;
            //int STICHING = 2;
            //int EMBROIDERING = 3;
            //int WASHING = 4;
            //int IRON = 5;
            //int CHECK = 6;
            //int PRINTING = 7;
            //int BarTagProcess = 8;
            //int TrimmingProcess = 9;
            //int ConsaiProcess = 10;

            //DataSet dsstichinglot = new DataSet();
            //dsstichinglot = objbs.Get_dsstichinglot(Convert.ToInt32(ddlLotNo.SelectedValue), STICHING);

            dtstch.Columns.Add(new DataColumn("Transid", typeof(string)));
            dtstch.Columns.Add(new DataColumn("Masterid", typeof(string)));
           // dtstch.Columns.Add(new DataColumn("Transfabid", typeof(string)));

            dtstch.Columns.Add(new DataColumn("Design", typeof(string)));
            dtstch.Columns.Add(new DataColumn("30fs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("32fs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("34fs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("36fs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xsfs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("sfs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("mfs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("lfs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xlfs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xxlfs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("3xlfs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("4xlfs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("30hs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("32hs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("34hs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("36hs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xshs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("shs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("mhs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("lhs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xlhs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xxlhs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("3xlhs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("4xlhs", typeof(string)));
            dtstch.Columns.Add(new DataColumn("TtlQty", typeof(string)));

            dtstch.Columns.Add(new DataColumn("30fsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("32fsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("34fsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("36fsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xsfsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("sfsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("mfsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("lfsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xlfsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xxlfsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("3xlfsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("4xlfsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("30hsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("32hsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("34hsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("36hsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xshsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("shsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("mhsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("lhsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xlhsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("xxlhsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("3xlhsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("4xlhsac", typeof(string)));
            dtstch.Columns.Add(new DataColumn("TtlQtyac", typeof(string)));

            dssstichinglot.Tables.Add(dtstch);

            for (int i = 0; i < gvnewstiching.Rows.Count; i++)
            {
                Label lblTransid = (Label)gvnewstiching.Rows[i].FindControl("lblTransid");
                Label lblMasterid = (Label)gvnewstiching.Rows[i].FindControl("lblMasterid");
               // Label lblTransfabid = (Label)gvnewstiching.Rows[i].FindControl("lblTransfabid");

                Label lbldesignno = (Label)gvnewstiching.Rows[i].FindControl("lbldesignno");
                TextBox txts30fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fs");
                TextBox txts32fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fs");
                TextBox txts34fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fs");
                TextBox txts36fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fs");
                TextBox txtsxsfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfs");
                TextBox txtssfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfs");
                TextBox txtsmfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfs");
                TextBox txtslfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfs");
                TextBox txtsxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfs");
                TextBox txts3xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfs");
                TextBox txts4xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfs");
                TextBox txts30hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hs");
                TextBox txts32hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hs");
                TextBox txts34hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hs");
                TextBox txts36hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hs");
                TextBox txtsxshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshs");
                TextBox txtsshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshs");
                TextBox txtsmhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhs");
                TextBox txtslhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhs");
                TextBox txtsxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhs");
                TextBox txts3xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhs");
                TextBox txts4xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhs");
                TextBox txtsendFQty = (TextBox)gvnewstiching.Rows[i].FindControl("txtsendFQty");

                TextBox txts30fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fsac");
                TextBox txts32fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fsac");
                TextBox txts34fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fsac");
                TextBox txts36fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fsac");
                TextBox txtsxsfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfsac");
                TextBox txtssfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfsac");
                TextBox txtsmfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfsac");
                TextBox txtslfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfsac");
                TextBox txtsxlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfsac");
                TextBox txtsxxlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfsac");
                TextBox txts3xlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfsac");
                TextBox txts4xlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfsac");
                TextBox txts30hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hsac");
                TextBox txts32hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hsac");
                TextBox txts34hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hsac");
                TextBox txts36hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hsac");
                TextBox txtsxshsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshsac");
                TextBox txtsshsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshsac");
                TextBox txtsmhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhsac");
                TextBox txtslhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhsac");
                TextBox txtsxlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhsac");
                TextBox txtsxxlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhsac");
                TextBox txts3xlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhsac");
                TextBox txts4xlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhsac");
                TextBox txtsendFQtyac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsendFQtyac");

                DataRow dr = dtstch.NewRow();
                dr["Transid"] = lblTransid.Text;
                dr["Masterid"] = lblMasterid.Text;
                //dr["Transfabid"] = lblTransfabid.Text;

                dr["Design"] = lbldesignno.Text;
                dr["30fs"] = txts30fs.Text;
                dr["32fs"] = txts32fs.Text;
                dr["34fs"] = txts34fs.Text;
                dr["36fs"] = txts36fs.Text;
                dr["xsfs"] = txtsxsfs.Text;
                dr["sfs"] = txtssfs.Text;
                dr["mfs"] = txtsmfs.Text;
                dr["lfs"] = txtslfs.Text;
                dr["xlfs"] = txtsxlfs.Text;
                dr["xxlfs"] = txtsxxlfs.Text;
                dr["3xlfs"] = txts3xlfs.Text;
                dr["4xlfs"] = txts4xlfs.Text;
                dr["30hs"] = txts30hs.Text;
                dr["32hs"] = txts32hs.Text;
                dr["34hs"] = txts34hs.Text;
                dr["36hs"] = txts36hs.Text;
                dr["xshs"] = txtsxshs.Text;
                dr["shs"] = txtsshs.Text;
                dr["mhs"] = txtsmhs.Text;
                dr["lhs"] = txtslhs.Text;
                dr["xlhs"] = txtsxlhs.Text;
                dr["xxlhs"] = txtsxxlhs.Text;
                dr["3xlhs"] = txts3xlhs.Text;
                dr["4xlhs"] = txts4xlhs.Text;
                dr["TtlQty"] = txtsendFQty.Text;


                dr["30fsac"] = txts30fsac.Text;
                dr["32fsac"] = txts32fsac.Text;
                dr["34fsac"] = txts34fsac.Text;
                dr["36fsac"] = txts36fsac.Text;
                dr["xsfsac"] = txtsxsfsac.Text;
                dr["sfsac"] = txtssfsac.Text;
                dr["mfsac"] = txtsmfsac.Text;
                dr["lfsac"] = txtslfsac.Text;
                dr["xlfsac"] = txtsxlfsac.Text;
                dr["xxlfsac"] = txtsxxlfsac.Text;
                dr["3xlfsac"] = txts3xlfsac.Text;
                dr["4xlfsac"] = txts4xlfsac.Text;
                dr["30hsac"] = txts30hsac.Text;
                dr["32hsac"] = txts32hsac.Text;
                dr["34hsac"] = txts34hsac.Text;
                dr["36hsac"] = txts36hsac.Text;
                dr["xshsac"] = txtsxshsac.Text;
                dr["shsac"] = txtsshsac.Text;
                dr["mhsac"] = txtsmhsac.Text;
                dr["lhsac"] = txtslhsac.Text;
                dr["xlhsac"] = txtsxlhsac.Text;
                dr["xxlhsac"] = txtsxxlhsac.Text;
                dr["3xlhsac"] = txts3xlhsac.Text;
                dr["4xlhsac"] = txts4xlhsac.Text;
                dr["TtlQtyac"] = txtsendFQtyac.Text;

                dssstichinglot.Tables[0].Rows.Add(dr);

            }
            gvnewstiching.DataSource = dssstichinglot;
            gvnewstiching.DataBind();

            for (int i = 0; i < gvnewstiching.Rows.Count; i++)
            {
                Label lblTransid = (Label)gvnewstiching.Rows[i].FindControl("lblTransid");
                Label lblMasterid = (Label)gvnewstiching.Rows[i].FindControl("lblMasterid");
                //Label lblTransfabid = (Label)gvnewstiching.Rows[i].FindControl("lblTransfabid");

                Label lbldesignno = (Label)gvnewstiching.Rows[i].FindControl("lbldesignno");
                TextBox txts30fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fs");
                TextBox txts32fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fs");
                TextBox txts34fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fs");
                TextBox txts36fs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fs");
                TextBox txtsxsfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfs");
                TextBox txtssfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfs");
                TextBox txtsmfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfs");
                TextBox txtslfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfs");
                TextBox txtsxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxxlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfs");
                TextBox txts3xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfs");
                TextBox txts4xlfs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfs");
                TextBox txts30hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hs");
                TextBox txts32hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hs");
                TextBox txts34hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hs");
                TextBox txts36hs = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hs");
                TextBox txtsxshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshs");
                TextBox txtsshs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshs");
                TextBox txtsmhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhs");
                TextBox txtslhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhs");
                TextBox txtsxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxxlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhs");
                TextBox txts3xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhs");
                TextBox txts4xlhs = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhs");
                TextBox txtsendFQty = (TextBox)gvnewstiching.Rows[i].FindControl("txtsendFQty");

                lblTransid.Text = dssstichinglot.Tables[0].Rows[i]["Transid"].ToString();
                lblMasterid.Text = dssstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                //lblTransfabid.Text = dssstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                lbldesignno.Text = dssstichinglot.Tables[0].Rows[i]["Design"].ToString();
                txts30fs.Text = dssstichinglot.Tables[0].Rows[i]["30fs"].ToString();
                txts32fs.Text = dssstichinglot.Tables[0].Rows[i]["32fs"].ToString();
                txts34fs.Text = dssstichinglot.Tables[0].Rows[i]["34fs"].ToString();
                txts36fs.Text = dssstichinglot.Tables[0].Rows[i]["36fs"].ToString();
                txtsxsfs.Text = dssstichinglot.Tables[0].Rows[i]["xsfs"].ToString();
                txtssfs.Text = dssstichinglot.Tables[0].Rows[i]["sfs"].ToString();
                txtsmfs.Text = dssstichinglot.Tables[0].Rows[i]["mfs"].ToString();
                txtslfs.Text = dssstichinglot.Tables[0].Rows[i]["lfs"].ToString();
                txtsxlfs.Text = dssstichinglot.Tables[0].Rows[i]["xlfs"].ToString();
                txtsxxlfs.Text = dssstichinglot.Tables[0].Rows[i]["xxlfs"].ToString();
                txts3xlfs.Text = dssstichinglot.Tables[0].Rows[i]["3xlfs"].ToString();
                txts4xlfs.Text = dssstichinglot.Tables[0].Rows[i]["4xlfs"].ToString();
                txts30hs.Text = dssstichinglot.Tables[0].Rows[i]["30hs"].ToString();
                txts32hs.Text = dssstichinglot.Tables[0].Rows[i]["32hs"].ToString();
                txts34hs.Text = dssstichinglot.Tables[0].Rows[i]["34hs"].ToString();
                txts36hs.Text = dssstichinglot.Tables[0].Rows[i]["36hs"].ToString();
                txtsxshs.Text = dssstichinglot.Tables[0].Rows[i]["xshs"].ToString();
                txtsshs.Text = dssstichinglot.Tables[0].Rows[i]["shs"].ToString();
                txtsmhs.Text = dssstichinglot.Tables[0].Rows[i]["mhs"].ToString();
                txtslhs.Text = dssstichinglot.Tables[0].Rows[i]["lhs"].ToString();
                txtsxlhs.Text = dssstichinglot.Tables[0].Rows[i]["xlhs"].ToString();
                txtsxxlhs.Text = dssstichinglot.Tables[0].Rows[i]["xxlhs"].ToString();
                txts3xlhs.Text = dssstichinglot.Tables[0].Rows[i]["3xlhs"].ToString();
                txts4xlhs.Text = dssstichinglot.Tables[0].Rows[i]["4xlhs"].ToString();
                txtsendFQty.Text = dssstichinglot.Tables[0].Rows[i]["TtlQty"].ToString();



                TextBox txts30fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts30fsac");
                TextBox txts32fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts32fsac");
                TextBox txts34fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts34fsac");
                TextBox txts36fsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts36fsac");
                TextBox txtsxsfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxsfsac");
                TextBox txtssfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtssfsac");
                TextBox txtsmfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmfsac");
                TextBox txtslfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtslfsac");
                TextBox txtsxlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlfsac");
                TextBox txtsxxlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlfsac");
                TextBox txts3xlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlfsac");
                TextBox txts4xlfsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlfsac");
                TextBox txts30hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts30hsac");
                TextBox txts32hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts32hsac");
                TextBox txts34hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts34hsac");
                TextBox txts36hsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts36hsac");
                TextBox txtsxshsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxshsac");
                TextBox txtsshsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsshsac");
                TextBox txtsmhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsmhsac");
                TextBox txtslhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtslhsac");
                TextBox txtsxlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxlhsac");
                TextBox txtsxxlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsxxlhsac");
                TextBox txts3xlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts3xlhsac");
                TextBox txts4xlhsac = (TextBox)gvnewstiching.Rows[i].FindControl("txts4xlhsac");
                TextBox txtsendFQtyac = (TextBox)gvnewstiching.Rows[i].FindControl("txtsendFQtyac");

                txts30fsac.Text = dssstichinglot.Tables[0].Rows[i]["30fsac"].ToString();
                txts32fsac.Text = dssstichinglot.Tables[0].Rows[i]["32fsac"].ToString();
                txts34fsac.Text = dssstichinglot.Tables[0].Rows[i]["34fsac"].ToString();
                txts36fsac.Text = dssstichinglot.Tables[0].Rows[i]["36fsac"].ToString();
                txtsxsfsac.Text = dssstichinglot.Tables[0].Rows[i]["xsfsac"].ToString();
                txtssfsac.Text = dssstichinglot.Tables[0].Rows[i]["sfsac"].ToString();
                txtsmfsac.Text = dssstichinglot.Tables[0].Rows[i]["mfsac"].ToString();
                txtslfsac.Text = dssstichinglot.Tables[0].Rows[i]["lfsac"].ToString();
                txtsxlfsac.Text = dssstichinglot.Tables[0].Rows[i]["xlfsac"].ToString();
                txtsxxlfsac.Text = dssstichinglot.Tables[0].Rows[i]["xxlfsac"].ToString();
                txts3xlfsac.Text = dssstichinglot.Tables[0].Rows[i]["3xlfsac"].ToString();
                txts4xlfsac.Text = dssstichinglot.Tables[0].Rows[i]["4xlfsac"].ToString();
                txts30hsac.Text = dssstichinglot.Tables[0].Rows[i]["30hsac"].ToString();
                txts32hsac.Text = dssstichinglot.Tables[0].Rows[i]["32hsac"].ToString();
                txts34hsac.Text = dssstichinglot.Tables[0].Rows[i]["34hsac"].ToString();
                txts36hsac.Text = dssstichinglot.Tables[0].Rows[i]["36hsac"].ToString();
                txtsxshsac.Text = dssstichinglot.Tables[0].Rows[i]["xshsac"].ToString();
                txtsshsac.Text = dssstichinglot.Tables[0].Rows[i]["shsac"].ToString();
                txtsmhsac.Text = dssstichinglot.Tables[0].Rows[i]["mhsac"].ToString();
                txtslhsac.Text = dssstichinglot.Tables[0].Rows[i]["lhsac"].ToString();
                txtsxlhsac.Text = dssstichinglot.Tables[0].Rows[i]["xlhsac"].ToString();
                txtsxxlhsac.Text = dssstichinglot.Tables[0].Rows[i]["xxlhsac"].ToString();
                txts3xlhsac.Text = dssstichinglot.Tables[0].Rows[i]["3xlhsac"].ToString();
                txts4xlhsac.Text = dssstichinglot.Tables[0].Rows[i]["4xlhsac"].ToString();
                txtsendFQtyac.Text = dssstichinglot.Tables[0].Rows[i]["TtlQtyac"].ToString();

            }

            #endregion

        }

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {

        }
       
       
    }
}