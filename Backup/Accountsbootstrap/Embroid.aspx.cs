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
    public partial class Embroid : System.Web.UI.Page
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
            string Embroidingid = string.Empty;
            string Requests = string.Empty;
            Embroidingid = Request.QueryString.Get("Embroidingid");
            empid = Session["Empid"].ToString();
            Requests = Request.QueryString.Get("name");


            if (!IsPostBack)
            {

                //DataSet dsUnitName = objbs.Select_UnitFirst();//tblUnit
                //if (dsUnitName.Tables[0].Rows.Count > 0)
                //{
                //    drpmultiunit.DataSource = dsUnitName.Tables[0];
                //    drpmultiunit.DataTextField = "UnitName";
                //    drpmultiunit.DataValueField = "UnitID";
                //    drpmultiunit.DataBind();
                //    drpmultiunit.Items.Insert(0, "Select Unit");
                //}

                //DataSet dsLotNo = objbs.Select_LotDetails("Emb");
                //if (dsLotNo.Tables[0].Rows.Count > 0)
                //{
                //    ddlLotNo.DataSource = dsLotNo.Tables[0];
                //    ddlLotNo.DataTextField = "LotNo";
                //    ddlLotNo.DataValueField = "LotDetailsID";
                //    ddlLotNo.DataBind();
                //    ddlLotNo.Items.Insert(0, "Select Lot No");
                //}
                DataSet dsize = objbs.Getsizetype();
                if (dsize != null)
                {
                    if (dsize.Tables[0].Rows.Count > 0)
                    {
                        chkSizes.DataSource = dsize.Tables[0];
                        chkSizes.DataTextField = "Size";
                        chkSizes.DataValueField = "Sizeid";
                        chkSizes.DataBind();
                    }
                }
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
                        company_SelectedIndexChnaged(sender, e);
                    }
                }



                DataSet dsjob = objbs.getjobcardlist();
                DropDownList1.DataSource = dsjob;
                DropDownList1.DataTextField = "ledgerName";
                DropDownList1.DataValueField = "ledgerId";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Select jobwork Name");

                //MultiUnit_SelectedIndex(sender, e);
                DataSet drpEmpp = objbs.Selectname("9");
                drpMultiemployee.DataSource = drpEmpp;
                drpMultiemployee.DataTextField = "ledgerName";
                drpMultiemployee.DataValueField = "ledgerId";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "Select Name");
                divWork.Visible = false;
                if (Embroidingid != null)
                {


                    if (Requests == "Receive")
                    {
                        idType.Visible = true;

                        DataSet dgetcheck = objbs.getJpEmbroidingLot(Embroidingid);
                        if (dgetcheck.Tables[0].Rows.Count > 0)
                        {
                            txtitemnarration.Text = dgetcheck.Tables[0].Rows[0]["ItemNarrations"].ToString();
                            drpbranch.SelectedValue = dgetcheck.Tables[0].Rows[0]["Companyid"].ToString();
                            drpbranch.Enabled = false;

                            DataSet dsLot = objbs.Select_Lot("Emb", drpbranch.SelectedValue);
                            if (dsLot.Tables[0].Rows.Count > 0)
                            {
                                ddlLotNo.DataSource = dsLot.Tables[0];
                                ddlLotNo.DataTextField = "CompanyLotNo";
                                ddlLotNo.DataValueField = "LotDetailsID";
                                ddlLotNo.DataBind();
                                ddlLotNo.Items.Insert(0, "Select Lot No");
                            }

                            DataSet dlotprocess = new DataSet();
                            dlotprocess = objbs.Get_LotDetails(Convert.ToString(dgetcheck.Tables[0].Rows[0]["LotDetailID"]), "4", "Emb");
                            if (dlotprocess.Tables[0].Rows.Count > 0)
                            {
                                txttotalqty.Text = dlotprocess.Tables[0].Rows[0]["TotalQuantity"].ToString();
                                lblbrandnid.Text = dlotprocess.Tables[0].Rows[0]["brandid"].ToString();
                                lblfitid.Text = dlotprocess.Tables[0].Rows[0]["fitid"].ToString();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Something Went Wrong Please Contact Administrator.While fetching Lot details. Thank you!!');", true);
                                return;
                            }


                            DataSet dsizee = objbs.Getfitseize(lblfitid.Text, lblbrandnid.Text);
                            if ((dsizee.Tables[0].Rows.Count > 0))
                            {

                                // for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                                for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                                {
                                    //You need to change this as per your DB Design
                                    string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();
                                    {
                                        //Find the checkbox list items using FindByValue and select it.
                                        chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                                    }

                                }
                            }

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


                            btnadd.Visible = true;
                            btnadd.Text = "Received";
                            txtid.Text = dgetcheck.Tables[0].Rows[0]["Embroidingid"].ToString();
                            txtmultiid.Text = dgetcheck.Tables[0].Rows[0]["Embroidingid"].ToString();
                            txtmultiplecode.Text = dgetcheck.Tables[0].Rows[0]["Embroidingid"].ToString();
                            txtmultiplecode.Enabled = false;
                            ddlLotNo.SelectedValue = dgetcheck.Tables[0].Rows[0]["LotDetailID"].ToString();
                            DataSet getitem = objbs.getitenameforprocess(ddlLotNo.SelectedItem.Text, "tbljpstiching", "R");
                            if (getitem.Tables[0].Rows.Count > 0)
                            {
                                lblitemname.Text = getitem.Tables[0].Rows[0]["ItemName"].ToString() + '(' + getitem.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                            }
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
                            DataSet ds = objbs.Get_JpEmbroiding(Convert.ToString(Embroidingid), "4", "Emb");
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                #region

                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("transid", typeof(string)));
                                dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                                dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                                dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                                temp.Tables.Add(dtt);



                                // for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                for (int i = 0; i < 1; i++)
                                {
                                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"].ToString()) != 0)
                                    {
                                        DataRow dr = dtt.NewRow();

                                        dr["OrderNo"] = "0";
                                        dr["transid"] = ds.Tables[0].Rows[i]["transid"].ToString();
                                        dr["fitid"] = ds.Tables[0].Rows[i]["fitid"].ToString();
                                        dr["fit"] = ds.Tables[0].Rows[i]["fit"].ToString();
                                        dr["ProcessTypeID"] = ds.Tables[0].Rows[i]["ProcessID"].ToString();
                                        dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                                        dr["TotalQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["totalQty"]);
                                        dr["SendQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["SendTotQty"]);
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
                                    Label lbltransid = (Label)gvcustomerorder.Rows[i].FindControl("lbltransid");
                                    Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                                    TextBox txtTotalFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTotalFQty");
                                    Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                                    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                                    Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                                    Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                                    Label lbllfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                                    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                                    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                                    TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                                    string recdate = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;
                                    string date11 = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;



                                    //Recdate.Enabled = false;
                                    // txtrecFQty.Enabled = true;
                                    //drpLotno.Enabled = false;
                                    drpProcess.Enabled = false;
                                    txtsendFQty.Enabled = false;
                                    //  txtdamageqty.Enabled = true;
                                    // Recdate.Enabled = false;
                                    date1.Enabled = false;

                                    txtsendFQty.Text = temp.Tables[0].Rows[i]["SendQty"].ToString();
                                    txtTotalFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                                    txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                                    lblfit.Text = temp.Tables[0].Rows[i]["fit"].ToString();
                                    lblitemname.Text = temp.Tables[0].Rows[i]["itemname"].ToString();
                                    lbllfitid.Text = temp.Tables[0].Rows[i]["fitid"].ToString();
                                    txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                                    drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    lblPattern.Text = temp.Tables[0].Rows[i]["Pattern"].ToString();
                                    lblPatternid.Text = temp.Tables[0].Rows[i]["Patternid"].ToString();
                                    txtdamageqty.Text = temp.Tables[0].Rows[i]["damageqty"].ToString();
                                    txtrecFQty.Text = temp.Tables[0].Rows[i]["recqty"].ToString();
                                    date1.Text = Convert.ToDateTime(temp.Tables[0].Rows[i]["senddate"]).ToString("dd/MM/yyyy");
                                    Recdate.Text = recdate.ToString();
                                    lbltransid.Text = temp.Tables[0].Rows[i]["transid"].ToString();
                                }

                                #endregion

                                #region New Updates Color Wise


                                DataSet dssstichinglot = new DataSet();
                                DataTable dtstch = new DataTable();

                                DataSet dsstichinglot = new DataSet();
                                dsstichinglot = objbs.Get_dsemplotcolor(Convert.ToInt32(Embroidingid));

                                dtstch.Columns.Add(new DataColumn("StockRatioId", typeof(string)));
                                dtstch.Columns.Add(new DataColumn("Masterid", typeof(string)));
                                dtstch.Columns.Add(new DataColumn("Transfabid", typeof(string)));
                                dtstch.Columns.Add(new DataColumn("Color", typeof(string)));

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
                                    dr["StockRatioId"] = dsstichinglot.Tables[0].Rows[i]["Transid"].ToString();
                                    dr["Masterid"] = dsstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                                    dr["Transfabid"] = dsstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                                    dr["Design"] = dsstichinglot.Tables[0].Rows[i]["Itemname"].ToString();
                                    dr["Color"] = dsstichinglot.Tables[0].Rows[i]["Onlycolor"].ToString();
                                    dr["30fs"] = dsstichinglot.Tables[0].Rows[i]["30fs"].ToString();
                                    dr["32fs"] = dsstichinglot.Tables[0].Rows[i]["32fs"].ToString();
                                    dr["34fs"] = dsstichinglot.Tables[0].Rows[i]["34fs"].ToString();
                                    dr["36fs"] = dsstichinglot.Tables[0].Rows[i]["36fs"].ToString();
                                    dr["xsfs"] = dsstichinglot.Tables[0].Rows[i]["xsfs"].ToString();
                                    dr["sfs"] = dsstichinglot.Tables[0].Rows[i]["sfs"].ToString();
                                    dr["mfs"] = dsstichinglot.Tables[0].Rows[i]["mfs"].ToString();
                                    dr["lfs"] = dsstichinglot.Tables[0].Rows[i]["lfs"].ToString();
                                    dr["xlfs"] = dsstichinglot.Tables[0].Rows[i]["xlfs"].ToString();
                                    dr["xxlfs"] = dsstichinglot.Tables[0].Rows[i]["xxlfs"].ToString();
                                    dr["3xlfs"] = dsstichinglot.Tables[0].Rows[i]["3xlfs"].ToString();
                                    dr["4xlfs"] = dsstichinglot.Tables[0].Rows[i]["4xlfs"].ToString();

                                    dr["30hs"] = dsstichinglot.Tables[0].Rows[i]["30hs"].ToString();
                                    dr["32hs"] = dsstichinglot.Tables[0].Rows[i]["32hs"].ToString();
                                    dr["34hs"] = dsstichinglot.Tables[0].Rows[i]["34hs"].ToString();
                                    dr["36hs"] = dsstichinglot.Tables[0].Rows[i]["36hs"].ToString();
                                    dr["xshs"] = dsstichinglot.Tables[0].Rows[i]["xshs"].ToString();
                                    dr["shs"] = dsstichinglot.Tables[0].Rows[i]["shs"].ToString();
                                    dr["mhs"] = dsstichinglot.Tables[0].Rows[i]["mhs"].ToString();
                                    dr["lhs"] = dsstichinglot.Tables[0].Rows[i]["lhs"].ToString();
                                    dr["xlhs"] = dsstichinglot.Tables[0].Rows[i]["xlhs"].ToString();
                                    dr["xxlhs"] = dsstichinglot.Tables[0].Rows[i]["xxlhs"].ToString();
                                    dr["3xlhs"] = dsstichinglot.Tables[0].Rows[i]["3xlhs"].ToString();
                                    dr["4xlhs"] = dsstichinglot.Tables[0].Rows[i]["4xlhs"].ToString();
                                    dr["TtlQty"] = dsstichinglot.Tables[0].Rows[i]["TtlQty"].ToString();
                                    //  dr["StockRatioId"] = dssstichinglot.Tables[0].Rows[i]["StockRatioId"].ToString();

                                    dssstichinglot.Tables[0].Rows.Add(dr);

                                }
                                gvnewemb.DataSource = dssstichinglot;
                                gvnewemb.DataBind();


                                if (chkSizes.SelectedIndex >= 0)
                                {
                                    gvnewemb.Columns[2].Visible = false; //30FS
                                    gvnewemb.Columns[3].Visible = false; //32FS

                                    gvnewemb.Columns[4].Visible = false;//34Fs
                                    gvnewemb.Columns[5].Visible = false;//36Fs

                                    gvnewemb.Columns[6].Visible = false; //XSFS
                                    gvnewemb.Columns[7].Visible = false; //SFS

                                    gvnewemb.Columns[8].Visible = false; //MFS
                                    gvnewemb.Columns[9].Visible = false; //LFS

                                    gvnewemb.Columns[10].Visible = false; //XLFS
                                    gvnewemb.Columns[11].Visible = false; //xxlFS

                                    gvnewemb.Columns[12].Visible = false; //3xlHS
                                    gvnewemb.Columns[13].Visible = false; //4xlHS

                                    gvnewemb.Columns[14].Visible = false; //30HS

                                    gvnewemb.Columns[15].Visible = false; //32HS

                                    gvnewemb.Columns[16].Visible = false; //34HS
                                    gvnewemb.Columns[17].Visible = false; //36HS

                                    gvnewemb.Columns[18].Visible = false; //XSHS
                                    gvnewemb.Columns[19].Visible = false; //SHS

                                    gvnewemb.Columns[20].Visible = false; //MHS
                                    gvnewemb.Columns[21].Visible = false; //LHS

                                    gvnewemb.Columns[22].Visible = false; //XLHS
                                    gvnewemb.Columns[23].Visible = false; //XXLHS

                                    gvnewemb.Columns[24].Visible = false; //3XLHS
                                    gvnewemb.Columns[25].Visible = false; //4XLHS




                                    int lop = 0;
                                    //Loop through each item of checkboxlist
                                    foreach (ListItem item in chkSizes.Items)
                                    {
                                        //check if item selected

                                        if (item.Selected)
                                        {

                                            {
                                                if (item.Text == "30FS")
                                                {
                                                    gvnewemb.Columns[2].Visible = true;
                                                }
                                                if (item.Text == "32FS")
                                                {
                                                    gvnewemb.Columns[3].Visible = true;
                                                }
                                                if (item.Text == "34FS")
                                                {
                                                    gvnewemb.Columns[4].Visible = true;
                                                }
                                                if (item.Text == "36FS")
                                                {
                                                    gvnewemb.Columns[5].Visible = true;
                                                }
                                                if (item.Text == "XSFS")
                                                {
                                                    gvnewemb.Columns[6].Visible = true;
                                                }
                                                if (item.Text == "SFS")
                                                {
                                                    gvnewemb.Columns[7].Visible = true;
                                                }
                                                if (item.Text == "MFS")
                                                {
                                                    gvnewemb.Columns[8].Visible = true;
                                                }
                                                if (item.Text == "LFS")
                                                {
                                                    gvnewemb.Columns[9].Visible = true;
                                                }
                                                if (item.Text == "XLFS")
                                                {
                                                    gvnewemb.Columns[10].Visible = true;
                                                }
                                                if (item.Text == "XXLFS")
                                                {
                                                    gvnewemb.Columns[11].Visible = true;
                                                }
                                                if (item.Text == "3XLFS")
                                                {
                                                    gvnewemb.Columns[12].Visible = true;
                                                }
                                                if (item.Text == "4XLFS")
                                                {
                                                    gvnewemb.Columns[13].Visible = true;
                                                }


                                                // FOR HS

                                                if (item.Text == "30HS")
                                                {
                                                    gvnewemb.Columns[14].Visible = true;
                                                }

                                                if (item.Text == "32HS")
                                                {
                                                    gvnewemb.Columns[15].Visible = true;
                                                }

                                                if (item.Text == "34HS")
                                                {
                                                    gvnewemb.Columns[16].Visible = true;
                                                }

                                                if (item.Text == "36HS")
                                                {
                                                    gvnewemb.Columns[17].Visible = true;

                                                }

                                                if (item.Text == "XSHS")
                                                {
                                                    gvnewemb.Columns[18].Visible = true;
                                                }

                                                if (item.Text == "SHS")
                                                {
                                                    gvnewemb.Columns[19].Visible = true;
                                                }

                                                if (item.Text == "MHS")
                                                {
                                                    gvnewemb.Columns[20].Visible = true;
                                                }

                                                if (item.Text == "LHS")
                                                {
                                                    gvnewemb.Columns[21].Visible = true;
                                                }

                                                if (item.Text == "XLHS")
                                                {
                                                    gvnewemb.Columns[22].Visible = true;
                                                }

                                                if (item.Text == "XXLHS")
                                                {
                                                    gvnewemb.Columns[23].Visible = true;
                                                }

                                                if (item.Text == "3XLHS")
                                                {
                                                    gvnewemb.Columns[24].Visible = true;
                                                }

                                                if (item.Text == "4XLHS")
                                                {
                                                    gvnewemb.Columns[25].Visible = true;
                                                }





                                                lop++;

                                            }
                                        }
                                    }
                                    //gvcustomerorder.DataSource = dssmer;
                                    //gvcustomerorder.DataBind();
                                }
                                else
                                {
                                    gvnewemb.Columns[2].Visible = false; //30FS
                                    gvnewemb.Columns[3].Visible = false; //32FS

                                    gvnewemb.Columns[4].Visible = false;//34Fs
                                    gvnewemb.Columns[5].Visible = false;//36Fs

                                    gvnewemb.Columns[6].Visible = false; //XSFS
                                    gvnewemb.Columns[7].Visible = false; //SFS

                                    gvnewemb.Columns[8].Visible = false; //MFS
                                    gvnewemb.Columns[9].Visible = false; //LFS

                                    gvnewemb.Columns[10].Visible = false; //XLFS
                                    gvnewemb.Columns[11].Visible = false; //xxlFS

                                    gvnewemb.Columns[12].Visible = false; //3xlHS
                                    gvnewemb.Columns[13].Visible = false; //4xlHS

                                    gvnewemb.Columns[14].Visible = false; //30HS

                                    gvnewemb.Columns[15].Visible = false; //32HS

                                    gvnewemb.Columns[16].Visible = false; //34HS
                                    gvnewemb.Columns[17].Visible = false; //36HS

                                    gvnewemb.Columns[18].Visible = false; //XSHS
                                    gvnewemb.Columns[19].Visible = false; //SHS

                                    gvnewemb.Columns[20].Visible = false; //MHS
                                    gvnewemb.Columns[21].Visible = false; //LHS

                                    gvnewemb.Columns[22].Visible = false; //XLHS
                                    gvnewemb.Columns[23].Visible = false; //XXLHS

                                    gvnewemb.Columns[24].Visible = false; //3XLHS
                                    gvnewemb.Columns[25].Visible = false; //4XLHS

                                }

                                for (int i = 0; i < gvnewemb.Rows.Count; i++)
                                {
                                    Label lblStockRatioId = (Label)gvnewemb.Rows[i].FindControl("lblStockRatioId");
                                    Label lblMasterid = (Label)gvnewemb.Rows[i].FindControl("lblMasterid");
                                    Label lblTransfabid = (Label)gvnewemb.Rows[i].FindControl("lblTransfabid");

                                    Label lblonlycolor = (Label)gvnewemb.Rows[i].FindControl("lblonlycolor");

                                    Label lbldesignno = (Label)gvnewemb.Rows[i].FindControl("lbldesignno");
                                    TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                                    TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                                    TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                                    TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                                    TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                                    TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                                    TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                                    TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                                    TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                                    TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                                    TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                                    TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");

                                    TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                                    TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                                    TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                                    TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                                    TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                                    TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                                    TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                                    TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                                    TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                                    TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                                    TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                                    TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");

                                    TextBox txtsendFQty = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQty");

                                    lblStockRatioId.Text = dssstichinglot.Tables[0].Rows[i]["StockRatioId"].ToString();
                                    lblMasterid.Text = dssstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                                    lblTransfabid.Text = dssstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                                    lblonlycolor.Text = dssstichinglot.Tables[0].Rows[i]["color"].ToString();
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


                                    TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                                    TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                                    TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                                    TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                                    TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                                    TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                                    TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                                    TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                                    TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                                    TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                                    TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                                    TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");

                                    TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                                    TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                                    TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                                    TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                                    TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                                    TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                                    TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                                    TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                                    TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                                    TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                                    TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                                    TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");

                                    TextBox txtsendFQtyac = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQtyac");

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
                }
                else
                {

                    //  FirstGridViewRow();
                }
                // Detail_checked(sender, e);
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        }

        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {

                //DataSet dsLotNo = objbs.Select_LotNo_For(drpbranch.SelectedValue);    // 7-Checking 
                //if (dsLotNo.Tables[0].Rows.Count > 0)
                //{
                //    ddlLotNo.DataSource = dsLotNo.Tables[0];
                //    ddlLotNo.DataTextField = "CompanyLotNo";
                //    ddlLotNo.DataValueField = "lotdetailid";
                //    ddlLotNo.DataBind();
                //    ddlLotNo.Items.Insert(0, "Select Lot No");
                //}

                DataSet dsLotNo = objbs.Select_LotDetails("Emb", drpbranch.SelectedValue);
                if (dsLotNo.Tables[0].Rows.Count > 0)
                {
                    DataSet dsLotNoval = objbs.Select_LotDetailsnew("Emb", drpbranch.SelectedValue, 3);
                    if (dsLotNoval.Tables[0].Rows.Count > 0)
                    {
                        ddlLotNo.DataSource = dsLotNoval.Tables[0];
                        ddlLotNo.DataTextField = "CompanyLotNo";
                        ddlLotNo.DataValueField = "LotDetailID";
                        ddlLotNo.DataBind();
                        ddlLotNo.Items.Insert(0, "Select Lot No");
                        lbllotno.Text = dsLotNo.Tables[0].Rows[0]["LotNo"].ToString();
                    }
                }



            }
        }


        protected void MultiUnit_SelectedIndex(object sender, EventArgs e)
        {

            if (drpmultiunit.SelectedValue == "Select Unit")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Unit Name.Thank You!!!.')", true);
                //drpmultiunit.Focus();
                //return;
            }
            else
            {

                DataSet dmaxbill = objbs.getmaxbillformulti(drpmultiunit.SelectedValue);
                if (dmaxbill.Tables[0].Rows.Count > 0)
                {
                    string bill = dmaxbill.Tables[0].Rows[0]["bill"].ToString();

                    char F = drpmultiunit.SelectedItem.Text.First();

                    char L = drpmultiunit.SelectedItem.Text.Last();


                    txtmultiplecode.Text = F.ToString() + L.ToString() + '/' + bill;
                    txtmultiid.Text = bill.ToString();

                }


                DataSet drpEmp = objbs.SelectEmpName(drpmultiunit.SelectedValue);
                drpMultiemployee.DataSource = drpEmp;
                drpMultiemployee.DataTextField = "Name";
                drpMultiemployee.DataValueField = "Employee_Id";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "Select Employee Name");


                FirstGridViewRow();
                ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "closeNav();", true);
            }
            //  gvcustomerorder.DataBind();

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    DropDownList drpLprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
            //    DataSet dsLotNo = objbs.Select_Lotnewstich(drpmultiunit.SelectedValue);
            //    if (dsLotNo.Tables[0].Rows.Count > 0)
            //    {
            //        drpLprocess.DataSource = dsLotNo.Tables[0];
            //        drpLprocess.DataTextField = "LotNo";
            //        drpLprocess.DataValueField = "cutid";
            //        drpLprocess.DataBind();
            //        drpLprocess.Items.Insert(0, "Select Lot No");
            //    }
            //    else
            //    {
            //        drpLprocess.Items.Clear();
            //        drpLprocess.Items.Insert(0, "Select Lot No");
            //    }
            //}

            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;

            //DropDownList drplotprocess = (DropDownList)row.FindControl("drpLotno");

            //DropDownList drpProcess = (DropDownList)row.FindControl("drpProcess");



            //if (drplotprocess.SelectedItem.Text != "Select Lot No")
            //{

            //    DataSet dsLotNo = objbs.Select_Lotnewstich(drpmultiunit.SelectedValue);
            //    if (dsLotNo.Tables[0].Rows.Count > 0)
            //    {
            //        drplotprocess.DataSource = dsLotNo.Tables[0];
            //        drplotprocess.DataTextField = "LotNo";
            //        drplotprocess.DataValueField = "cutid";
            //        drplotprocess.DataBind();
            //        drplotprocess.Items.Insert(0, "Select Lot No");
            //    }
            //    else
            //    {
            //        drplotprocess.Items.Clear();
            //        drplotprocess.Items.Insert(0, "Select Lot No");
            //    }
            //}
            //else
            //{
            //}

        }
        protected void txtrecqtychnaged_text(object sender, EventArgs e)
        {
            if (btnadd.Text == "Save")
            {
                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    string temp = drpprocess.SelectedValue;
                    double qty = 0;
                    for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                    {

                        DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                        curent = process.SelectedValue;
                        namee = process.SelectedItem.Text;
                        TextBox RecQuantity = (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");

                        if (process.SelectedValue == temp)
                        {
                            qty = qty + Convert.ToDouble(RecQuantity.Text);
                        }
                    }
                    //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                    //{
                    //}
                    if (objbs.CheckIfrecqtyinstiching(ddlLotNo.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee + " has Exists received Quantity.')", true);
                        txtRecQuantity.Focus();
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    date.Focus();


                }
            }
            else
            {

                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    string temp = drpprocess.SelectedValue;
                    double qty = 0;
                    for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                    {

                        DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                        curent = process.SelectedValue;
                        namee = process.SelectedItem.Text;
                        TextBox RecQuantity = (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");

                        if (process.SelectedValue == temp)
                        {
                            qty = qty + Convert.ToDouble(RecQuantity.Text);
                        }
                    }
                    //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                    //{
                    //}
                    if (objbs.CheckIfrecqtyinstichingupdate(ddlLotNo.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee.Trim() + " has Exists received Quantity.')", true);
                        txtRecQuantity.Focus();
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    date.Focus();
                }
            }
            //  AddNewRow();
        }

        protected void drpLot_selected(object sender, EventArgs e)
        {
            drpmultiunit.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "closeNav();", true);

            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            DropDownList drplotprocess = (DropDownList)row.FindControl("drpLotno");

            DropDownList drpProcess = (DropDownList)row.FindControl("drpProcess");



            if (drplotprocess.SelectedItem.Text != "Select Lot No")
            {

                DataSet drpProcesss = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(drplotprocess.SelectedValue));
                if (drpProcesss.Tables[0].Rows.Count > 0)
                {
                    drpProcess.Items.Clear();
                    drpProcess.DataSource = drpProcesss;
                    drpProcess.DataTextField = "ProcessType";
                    drpProcess.DataValueField = "ProcessMasterID";
                    drpProcess.DataBind();
                    drpProcess.Items.Insert(0, "Select Process Type");

                }
                else
                {
                    drpProcess.Items.Clear();
                    drpProcess.Items.Insert(0, "Select Process Type");
                }
            }
            else
            {
            }

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                DropDownList drpProce = (DropDownList)row.FindControl("drpProcess");
                drpProce.Focus();
            }

        }
        protected void txtsendfqtychnaged_text(object sender, EventArgs e)
        {
            double sndqty = 0;
            int iq = 1;
            int iii = 1;
            int iq1 = 1;
            int iii1 = 1;
            string itemc = string.Empty;
            string itemd = string.Empty;
            string iteme = string.Empty;
            string itemcd = string.Empty;

            if (btnadd.Text == "Save")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                    if (drpprocess.SelectedValue != "Select Process Type")
                    {

                        TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");

                        TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                        if (date.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                            return;
                        }

                        //   DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //   DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                        itemc = drpprocess.SelectedValue;
                        itemd = txtBundle.Text;
                        iteme = drplotno.SelectedValue;


                        if ((itemc == null) || (itemc == ""))
                        {
                        }
                        else
                        {
                            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                            {
                                DropDownList drplotno1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpLotno");
                                DropDownList drpprocesss = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpProcess");
                                TextBox txtBundle1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtBundle");
                                if (drpprocesss.SelectedValue != "Select Process Type")
                                {
                                    // DropDownList drpEmpp = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpEmp");
                                    TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");
                                    // DateTime recDatee = DateTime.ParseExact(datee.Text, "dd/MM/yyyy HH:MM:ss", CultureInfo.InvariantCulture);

                                    {

                                        if (iii == iq)
                                        {
                                        }
                                        else
                                        {
                                            if (itemc == drpprocesss.Text && iteme == drplotno1.SelectedValue && itemd == txtBundle1.Text)
                                            {
                                                itemcd = drpprocess.SelectedItem.Text;
                                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "," + drpEmpp.SelectedItem.Text + ",'" + recDatee.ToString("dd/MM/yyyy") + "'  already exists in the Grid.');", true);
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd.Trim() + "," + drplotno1.SelectedItem.Text.Trim() + "already exists in the Grid.');", true);
                                                drpprocess.Focus();
                                                // drpEmpp.Focus();
                                                return;

                                            }
                                        }
                                        iii = iii + 1;
                                    }
                                }
                            }
                        }
                        iq = iq + 1;
                        iii = 1;
                    }
                }
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                    if (drpprocess.SelectedValue != "Select Process Type")
                    {

                        TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");

                        TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                        if (date.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                            return;
                        }

                        //   DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //   DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                        itemc = drpprocess.SelectedValue;
                        itemd = txtBundle.Text;
                        iteme = drplotno.SelectedValue;


                        if ((itemc == null) || (itemc == ""))
                        {
                        }
                        else
                        {
                            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                            {
                                DropDownList drplotno1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpLotno");
                                DropDownList drpprocesss = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpProcess");
                                TextBox txtBundle1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtBundle");
                                if (drpprocesss.SelectedValue != "Select Process Type")
                                {
                                    // DropDownList drpEmpp = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpEmp");
                                    TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");
                                    // DateTime recDatee = DateTime.ParseExact(datee.Text, "dd/MM/yyyy HH:MM:ss", CultureInfo.InvariantCulture);

                                    {

                                        if (iii1 == iq1)
                                        {
                                        }
                                        else
                                        {
                                            if (itemc == drpprocesss.Text && iteme == drplotno1.SelectedValue && itemd == txtBundle1.Text)
                                            {
                                                itemcd = drpprocess.SelectedItem.Text;
                                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "," + drpEmpp.SelectedItem.Text + ",'" + recDatee.ToString("dd/MM/yyyy") + "'  already exists in the Grid.');", true);
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd.Trim() + "," + drplotno1.SelectedItem.Text.Trim() + "already exists in the Grid.');", true);
                                                drpprocess.Focus();
                                                // drpEmpp.Focus();
                                                return;

                                            }
                                        }
                                        iii1 = iii1 + 1;
                                    }
                                }
                            }
                        }
                        iq1 = iq1 + 1;
                        iii1 = 1;
                    }
                }

                string curent = string.Empty;
                string namee = string.Empty;
                string bun = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                    if (drpLotprocess.SelectedValue == "Select Lot No")
                    {
                    }
                    else
                    {
                        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                        TextBox txtbundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                        txtrecFQty.Text = "0";
                        txtrecFQty.Enabled = false;

                        sndqty = sndqty + Convert.ToDouble(txtsendFQty.Text);
                        //  TextBox txtsendHQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendHQty");

                        //TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                        string temp = drpprocess.SelectedValue;
                        string tempbun = txtbundle.Text;
                        double qty = 0;
                        for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                        {
                            DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                            DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                            TextBox bundle = (TextBox)gvcustomerorder.Rows[j].FindControl("txtBundle");
                            curent = process.SelectedValue;
                            namee = process.SelectedItem.Text;
                            bun = bundle.Text;
                            TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");
                            //   TextBox txtsendHQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendHQty");

                            if (process.SelectedValue == temp && bun == tempbun)
                            {
                                qty = qty + Convert.ToDouble(txtsendFQty1.Text);
                            }
                        }
                        //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                        //{
                        //}

                        if (drpLotprocess.SelectedValue == "Select Lot No")
                        {
                        }
                        else
                        {
                            if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + drpprocess.SelectedItem.Text + " has Exists received Quantity.')", true);
                                // txtRecQuantity.Focus();
                                txtsendFQty.Focus();
                                return;
                                // lblerror.Text = "These Category has already Exists. please enter a new one";

                            }

                        }
                        // date.Focus();
                        //  txtsendHQty.Focus();
                        //txtbundle.Focus();

                    }

                    txttotalqty.Text = sndqty.ToString();
                }
            }
            else
            {
                if (btnadd.Text == "Update")
                {
                    string curent = string.Empty;
                    string namee = string.Empty;
                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                        if (drpLotprocess.SelectedValue == "Select Lot No")
                        {
                        }
                        else
                        {
                            DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                            TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                            TextBox txtbundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                            TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                            txtrecFQty.Text = "0";
                            txtrecFQty.Enabled = false;

                            sndqty = sndqty + Convert.ToDouble(txtsendFQty.Text);
                            //  TextBox txtsendHQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendHQty");

                            //TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                            //string temp = drpprocess.SelectedValue;
                            //double qty = 0;
                            //for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                            //{
                            //    DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                            //    DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                            //    curent = process.SelectedValue;
                            //    namee = process.SelectedItem.Text;
                            //    TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");
                            //    //   TextBox txtsendHQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendHQty");

                            //    if (process.SelectedValue == temp)
                            //    {
                            //        qty = qty + Convert.ToDouble(txtsendFQty1.Text);
                            //    }
                            //}
                            //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                            //{
                            //}

                            //if (drpLotprocess.SelectedValue == "Select Lot No")
                            //{
                            //}
                            //else
                            //{
                            //    if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                            //    {
                            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + drpprocess.SelectedItem.Text + " has Exists received Quantity.')", true);

                            //        txtsendFQty.Focus();
                            //        return;

                            //    }
                            //}

                            txtbundle.Focus();

                        }

                        txttotalqty.Text = sndqty.ToString();
                    }
                }
            }



            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtBundle");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtBundle");
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


            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    int cnt = gvcustomerorder.Rows.Count;
            //    //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
            //    if (vLoop >= 1)
            //    {
            //        //TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtBundle");

            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");

            //        //    oldtxttk.Text = ".00";
            //        oldtxttk.Focus();
            //    }
            //    int tot = cnt - vLoop;
            //    if (tot == 1)
            //    {
            //        //TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtBundle");

            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
            //        if (oldtxttk.Text == "")
            //        {
            //            oldtxttk.Text = "";
            //            oldtxttk.Focus();
            //        }
            //        else
            //        {
            //            oldtxttk.Focus();
            //        }
            //    }
            //    //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            //}

            // AddNewRow();

        }
        protected void txtsendhqtychnaged_text(object sender, EventArgs e)
        {
            if (btnadd.Text == "Save")
            {
                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    TextBox txtsendHQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendHQty");

                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    string temp = drpprocess.SelectedValue;
                    double qty = 0;
                    for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                    {
                        DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                        DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                        curent = process.SelectedValue;
                        namee = process.SelectedItem.Text;
                        TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");
                        TextBox txtsendHQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendHQty");

                        if (process.SelectedValue == temp)
                        {
                            qty = qty + Convert.ToDouble(txtsendFQty1.Text) + Convert.ToDouble(txtsendHQty1.Text); ;
                        }
                    }
                    //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                    //{
                    //}
                    if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee + " has Exists received Quantity.')", true);
                        // txtRecQuantity.Focus();
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    // date.Focus();
                    date.Focus();

                }
            }
            else
            {

                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    TextBox txtsendHQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendHQty");

                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    string temp = drpprocess.SelectedValue;
                    double qty = 0;
                    for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                    {
                        DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                        DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                        curent = process.SelectedValue;
                        namee = process.SelectedItem.Text;
                        TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");
                        TextBox txtsendHQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendHQty");

                        if (process.SelectedValue == temp)
                        {
                            qty = qty + Convert.ToDouble(txtsendHQty1.Text) + Convert.ToDouble(txtsendFQty1.Text);
                        }
                    }
                    //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                    //{
                    //}
                    if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee + " has Exists received Quantity.')", true);
                        // txtRecQuantity.Focus();
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    date.Focus();
                    // txtsendHQty.Focus();

                }
            }
            // AddNewRow();
        }
        protected void Add1_Click(object sender, EventArgs e)
        {
            double tot = 0;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                Label lblfit = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblfit");
                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                Label lblitemname = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblitemname");
                TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRemainQty");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrate");
                Label lblPattern = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblPattern");
                Label lblPatternid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblPatternid");
                Label lblfitid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblfitid");
                TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdamageqty");
                TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                TextBox date1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");
                //DateTime recdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime date11 = DateTime.ParseExact(date1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                tot = tot + (Convert.ToDouble(txtrate.Text) * Convert.ToDouble(txtsendFQty.Text));
            }
            txtAmount.Text = tot.ToString();
        }
        protected void txtrecfqtychnaged_text(object sender, EventArgs e)
        {
            //double recqty = 0;
            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
            //    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
            //    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
            //    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");

            //    recqty = recqty + Convert.ToDouble(txtrecQty.Text);

            //    if (Convert.ToDouble(txtsendFQty.Text) < Convert.ToDouble(txtrecQty.Text))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Received Qty is not Greater than That Remain Qty!!!')", true);
            //        txtrecQty.Focus();
            //        return;
            //    }

            //}

            //txtreceivedQty.Text = recqty.ToString();

        }
        protected void txtrechqtychnaged_text(object sender, EventArgs e)
        {

        }

        protected void drpprocess_selected(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
            DropDownList drplotprocess = (DropDownList)row.FindControl("drpLotno");
            TextBox txtrate = (TextBox)row.FindControl("txtRate");


            DataSet ds = new DataSet();
            if (ddlprocess.SelectedValue != "Select Process Type")
            {
                ds = objbs.getrateforstiching(ddlprocess.SelectedValue, drplotprocess.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtrate.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"]).ToString();
                }
                DataSet dlotprocess = new DataSet();
                dlotprocess = objbs.getprocessdetailsforsticparticular(drplotprocess.SelectedValue, ddlprocess.SelectedValue);
                if (dlotprocess.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = dlotprocess;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }

                DataSet drpRateProcess = objbs.SelectProcessTypeLotProcessforpartiuclarr(Convert.ToInt32(drplotprocess.SelectedValue), ddlprocess.SelectedValue);
                if (drpRateProcess.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = drpRateProcess;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }



            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {

                TextBox txtsendfFqty = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtsendFQty");
                txtsendfFqty.Focus();

            }

            //ButtonAdd1_Click(sender, e);
            ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "openNav();", true);

            ButtonAdd1_Click(sender, e);



            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtsendFQty");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtsendFQty");
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
        protected void Detail_checked(object sender, EventArgs e)
        {

        }

        protected void RateDetail_checked(object sender, EventArgs e)
        {
            if (ddlLotNo.SelectedValue != "Select Lot No")
            {
                DataSet drpProcess = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(ddlLotNo.SelectedValue));
                if (drpProcess.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = drpProcess;
                    GridView2.DataBind();
                }
                GridView1.Visible = false;
                GridView2.Visible = true;
                processs.Visible = false;
                ratee.Visible = true;
                //  mpe1.Show();
                Ratedetail.Checked = true;
                DetailView.Checked = false;
            }
        }


        protected void StitchingInfo_Loadforupdate(object sender, EventArgs e)
        {
            //DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            //if (dataSet.Tables[0].Rows.Count > 0)
            //{
            //    txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
            //    txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

            //    txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
            //    txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

            //    txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
            //    txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
            //    txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();
            //    txtHalf.Text = dataSet.Tables[0].Rows[0]["HalfQty"].ToString();
            //    txtfull.Text = dataSet.Tables[0].Rows[0]["FullQty"].ToString();
            //    txtdesignno.Text = dataSet.Tables[0].Rows[0]["Designno"].ToString();
            //    string processDate = dataSet.Tables[0].Rows[0]["ProcessDate"].ToString();
            //    if (processDate == "")
            //    {
            //        DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        txtProcessDate.Text = date.ToString("dd/MM/yyyy");
            //    }
            //    else
            //    {
            //        DateTime date = DateTime.ParseExact(Convert.ToDateTime(processDate).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        txtProcessDate.Text = date.ToString("dd/MM/yyyy");
            //        //        CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            //    }
            //    //txtProcessDate.Text = DateTime.ParseExact(processDate, "dd/MM/yyyy hh:mm:ss tt",
            //    //            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            //    //DateTime processDate1 = DateTime.ParseExact(processDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //txtProcessDate.Text = processDate1.ToString();
            //    string lotno = "0";
            //    if (ddlLotNo.SelectedValue == "Select Lot No")
            //    {
            //        lotno = "0";
            //    }
            //    else
            //    {
            //        lotno = ddlLotNo.SelectedValue;
            //    }

            //    DataSet drpProcess = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(lotno));
            //    DataSet drpEmp = objbs.SelectStitchingEmpName(Convert.ToInt32(txtUnitID.Text));

            //    //DropDownList dbrand = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpProcess");
            //    //DropDownList dEmp = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpEmp");
            //    //if (drpProcess.Tables[0].Rows.Count > 0)
            //    //{
            //    //    dbrand.Items.Clear();
            //    //    dbrand.ClearSelection();
            //    //    dbrand.DataSource = drpProcess.Tables[0];
            //    //    dbrand.DataTextField = "ProcessType";
            //    //    dbrand.DataValueField = "ProcessMasterID";
            //    //    dbrand.DataBind();
            //    //    dbrand.Items.Insert(0, "Select Process Type");

            //    //    dEmp.DataSource = drpEmp;
            //    //    dEmp.DataTextField = "Name";
            //    //    dEmp.DataValueField = "Employee_Id";
            //    //    dEmp.DataBind();
            //    //    dEmp.Items.Insert(0, "Select Employee Name");
            //    //}
            //    //   GridView2.DataSource = drpProcess;
            //    //    GridView2.DataBind();

            //    DataSet dlotprocess = new DataSet();
            //    dlotprocess = objbs.getprocessdetailsforstic(lotno);
            //    if (dlotprocess.Tables[0].Rows.Count > 0)
            //    {
            //        GridView1.DataSource = dlotprocess;
            //        GridView1.DataBind();
            //    }

            //    DataSet workProcessManual = objbs.SelectWorkProcessTypeManual(Convert.ToInt32(ddlLotNo.SelectedValue));
            //    if (workProcessManual.Tables[0].Rows[0]["IsManual"].ToString() == "True")
            //    {
            //        divWork.Visible = true;
            //        GridView3.DataSource = workProcessManual;
            //        GridView3.DataBind();
            //    }
            //    else
            //    {
            //        divWork.Visible = false;
            //    }

            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //        drpprocess.Focus();

            //    }
            //}
            //else
            //{
            //    gvcustomerorder.DataSource = null;
            //    gvcustomerorder.DataBind();
            //}

            //GridView1.Visible = true;
            //GridView2.Visible = true;
            //processs.Visible = true;
            //ratee.Visible = true;
        }

        protected void EmployeeRate_chnaged(object sender, EventArgs e)
        {
            if (ddlLotNo.SelectedValue != "Select Lot No")
            {
                double stitchrate = 0;

                if (drpMultiemployee.SelectedValue == "Select Name")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Jobworker/Employee Name For this " + ddlLotNo.SelectedItem.Text + ". Thank you');", true);
                    return;
                }
                else
                {
                    DataSet getrate = objbs.getrateforjobworker(drpMultiemployee.SelectedValue, lblitemid.Text, "Emb");
                    if (getrate.Tables[0].Rows.Count > 0)
                    {
                        stitchrate = Convert.ToDouble(getrate.Tables[0].Rows[0]["Rate"]);



                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Try Again Later.Rate Not confirmed.Something Went Wrong to this " + ddlLotNo.SelectedItem.Text + ". Thank you');", true);
                        return;
                    }

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {

                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                        //txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                        txtrate.Text = stitchrate.ToString();

                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Try Again Later.Rate Not confirmed.Something Went Wrong to this " + ddlLotNo.SelectedItem.Text + ". Thank you');", true);
                return;
            }


        }

        protected void StitchingInfo_Load(object sender, EventArgs e)
        {

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");

            if (ddlLotNo.SelectedValue != "Select Lot No")
            {
                int dcheck = objbs.checkcurrent(ddlLotNo.SelectedValue, "Emb");
                if (dcheck == 1)
                {
                    #region

                    DataSet getiteminprecut = objbs.getitemidinprecut(Convert.ToString(ddlLotNo.SelectedItem.Text));
                    if (getiteminprecut.Tables[0].Rows.Count > 0)
                    {
                        lblitemid.Text = getiteminprecut.Tables[0].Rows[0]["itemid"].ToString();
                        lblcuttingdate.Text = Convert.ToDateTime(getiteminprecut.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");

                    }

                    double embrate = 0;

                    // TEMP COMMENT

                    //DataSet rateforprecost = objbs.getrateforprecost(Convert.ToString(ddlLotNo.SelectedItem.Text), "EMBROIDERING");
                    //if (rateforprecost.Tables[0].Rows.Count > 0)
                    //{
                    //    embrate = Convert.ToDouble(rateforprecost.Tables[0].Rows[0]["Cost"].ToString());
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please to Entry Cost in Master Cutting for this " + ddlLotNo.SelectedItem.Text + ". Thank you');", true);
                    //    return;
                    //}

                    DataSet getitem = objbs.getitenameforprocess(ddlLotNo.SelectedItem.Text, "tbljpstiching", "R");
                    if (getitem.Tables[0].Rows.Count > 0)
                    {
                        lblitemname.Text = getitem.Tables[0].Rows[0]["ItemName"].ToString() + '(' + getitem.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                    }

                    DataSet dlotprocess = new DataSet();
                    dlotprocess = objbs.Get_LotDetails(Convert.ToString(ddlLotNo.SelectedValue), "4", "Emb");
                    if (dlotprocess.Tables[0].Rows.Count > 0)
                    {
                        txttotalqty.Text = dlotprocess.Tables[0].Rows[0]["TotalQuantity"].ToString();

                        lblbrandnid.Text = dlotprocess.Tables[0].Rows[0]["Brandid"].ToString();
                        lblfitid.Text = dlotprocess.Tables[0].Rows[0]["Fitid"].ToString();
                        txtitemnarration.Text = dlotprocess.Tables[0].Rows[0]["ItemNarrations"].ToString();

                        DataSet dsizee = objbs.Getfitseize(lblfitid.Text, lblbrandnid.Text);
                        if ((dsizee.Tables[0].Rows.Count > 0))
                        {

                            // for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                            for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                            {
                                //You need to change this as per your DB Design
                                string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();
                                {
                                    //Find the checkbox list items using FindByValue and select it.
                                    chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                                }

                            }
                        }

                        if (dlotprocess.Tables[0].Rows[0]["Jobwork"].ToString() == "Out")
                        {
                            DataSet drpEmpp = objbs.Selectname("10");
                            drpMultiemployee.DataSource = drpEmpp;
                            drpMultiemployee.DataTextField = "LedgerName";
                            drpMultiemployee.DataValueField = "LedgerID";
                            drpMultiemployee.DataBind();
                            drpMultiemployee.Items.Insert(0, "Select Name");
                            //emp.Visible = false;
                            //job.Visible = true;
                        }
                        else if (dlotprocess.Tables[0].Rows[0]["Jobwork"].ToString() == "In")
                        {
                            DataSet drpEmpp = objbs.Selectname("9");
                            drpMultiemployee.DataSource = drpEmpp;
                            drpMultiemployee.DataTextField = "LedgerName";
                            drpMultiemployee.DataValueField = "LedgerID";
                            drpMultiemployee.DataBind();
                            drpMultiemployee.Items.Insert(0, "Select Name");
                            //emp.Visible = true;
                            //job.Visible = false;
                        }
                    }

                    if (dlotprocess.Tables[0].Rows.Count > 0)
                    {
                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                        dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                        dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                        temp.Tables.Add(dtt);


                        double tot = 0;
                        double rate = 0;
                        double TotalQty = 0;
                        double TotalQty1 = 0;
                        for (int i = 0; i < dlotprocess.Tables[0].Rows.Count; i++)
                        {

                            DataRow dr = dtt.NewRow();

                            dr["fit"] = dlotprocess.Tables[0].Rows[i]["fit"].ToString();
                            dr["itemname"] = dlotprocess.Tables[0].Rows[i]["itemname"].ToString();
                            dr["TotalQty"] = dlotprocess.Tables[0].Rows[i]["SendTotalQty"].ToString();
                            dr["SendQty"] = dlotprocess.Tables[0].Rows[i]["SendTotalQty"].ToString();
                            dr["RemainQty"] = dlotprocess.Tables[0].Rows[i]["SendTotalQty"].ToString();
                            dr["ProcessTypeID"] = dlotprocess.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                            dr["rate"] = dlotprocess.Tables[0].Rows[i]["rate"].ToString();
                            dr["damageqty"] = "0";
                            dr["RecQty"] = "0";
                            TotalQty = Convert.ToDouble(dlotprocess.Tables[0].Rows[i]["TotalQty"]);
                            TotalQty1 = TotalQty1 + TotalQty;
                            rate = Convert.ToDouble(dlotprocess.Tables[0].Rows[i]["rate"]);
                            tot = tot + (TotalQty * rate);
                            dr["Fitid"] = dlotprocess.Tables[0].Rows[i]["Fitid"].ToString();
                            dr["Pattern"] = dlotprocess.Tables[0].Rows[i]["PatternName"].ToString();
                            dr["Patternid"] = dlotprocess.Tables[0].Rows[i]["Patternid"].ToString();

                            temp.Tables[0].Rows.Add(dr);

                        }
                        txttotalqty.Text = TotalQty1.ToString("0.00");
                        txtAmount.Text = tot.ToString();

                        ViewState["CurrentTable1"] = dtt;

                        gvcustomerorder.DataSource = temp;
                        gvcustomerorder.DataBind();

                        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        {
                            TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                            DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                            Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                            TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                            TextBox txtTotalFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTotalFQty");
                            Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                            TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                            TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                            Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                            Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                            Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                            TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                            TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                            TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                            string recdate = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;
                            string date11 = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;

                            txtsendFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                            txtTotalFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                            txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                            lblfit.Text = temp.Tables[0].Rows[i]["fit"].ToString();
                            lblitemname.Text = temp.Tables[0].Rows[i]["itemname"].ToString();
                            lblfitid.Text = temp.Tables[0].Rows[i]["fitid"].ToString();
                            // txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                            txtrate.Text = embrate.ToString();
                            drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                            lblPattern.Text = temp.Tables[0].Rows[i]["Pattern"].ToString();
                            lblPatternid.Text = temp.Tables[0].Rows[i]["Patternid"].ToString();
                            txtdamageqty.Text = temp.Tables[0].Rows[i]["damageqty"].ToString();
                            date1.Text = date11.ToString();
                            Recdate.Text = recdate.ToString();
                            txtrecFQty.Text = temp.Tables[0].Rows[i]["recQty"].ToString();
                            txtdamageqty.Text = "0";
                            txtrecFQty.Text = "0";
                        }
                        gvcustomerorder.Columns[11].Visible = false;
                        gvcustomerorder.Columns[12].Visible = false;
                        // gvcustomerorder.Columns[14].Visible = false;

                    }

                    #endregion

                    btnadd.Enabled = true;

                    #region


                    DataSet dssstichinglot = new DataSet();
                    DataTable dtstch = new DataTable();

                    DataSet dsstichinglot = new DataSet();


                    int EMBROIDERING = 3;


                    dsstichinglot = objbs.Get_dsEmblot(Convert.ToInt32(ddlLotNo.SelectedValue), EMBROIDERING);

                    dtstch.Columns.Add(new DataColumn("StockRatioId", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("Masterid", typeof(string)));
                    dtstch.Columns.Add(new DataColumn("Transfabid", typeof(string)));

                    dtstch.Columns.Add(new DataColumn("Color", typeof(string)));
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
                        dr["StockRatioId"] = dsstichinglot.Tables[0].Rows[i]["StockRatioId"].ToString();
                        dr["Masterid"] = dsstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                        dr["Transfabid"] = dsstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                        dr["Color"] = dsstichinglot.Tables[0].Rows[i]["Onlycolor"].ToString();

                        dr["Design"] = dsstichinglot.Tables[0].Rows[i]["DesignCode"].ToString();
                        dr["30fs"] = dsstichinglot.Tables[0].Rows[i]["R30fs"].ToString();
                        dr["32fs"] = dsstichinglot.Tables[0].Rows[i]["R32fs"].ToString();
                        dr["34fs"] = dsstichinglot.Tables[0].Rows[i]["R34fs"].ToString();
                        dr["36fs"] = dsstichinglot.Tables[0].Rows[i]["R36fs"].ToString();
                        dr["xsfs"] = dsstichinglot.Tables[0].Rows[i]["Rxsfs"].ToString();
                        dr["sfs"] = dsstichinglot.Tables[0].Rows[i]["Rsfs"].ToString();
                        dr["mfs"] = dsstichinglot.Tables[0].Rows[i]["Rmfs"].ToString();
                        dr["lfs"] = dsstichinglot.Tables[0].Rows[i]["Rlfs"].ToString();
                        dr["xlfs"] = dsstichinglot.Tables[0].Rows[i]["Rxlfs"].ToString();
                        dr["xxlfs"] = dsstichinglot.Tables[0].Rows[i]["Rxxlfs"].ToString();
                        dr["3xlfs"] = dsstichinglot.Tables[0].Rows[i]["R3xlfs"].ToString();
                        dr["4xlfs"] = dsstichinglot.Tables[0].Rows[i]["R4xlfs"].ToString();

                        dr["30hs"] = dsstichinglot.Tables[0].Rows[i]["R30hs"].ToString();
                        dr["32hs"] = dsstichinglot.Tables[0].Rows[i]["R32hs"].ToString();
                        dr["34hs"] = dsstichinglot.Tables[0].Rows[i]["R34hs"].ToString();
                        dr["36hs"] = dsstichinglot.Tables[0].Rows[i]["R36hs"].ToString();
                        dr["xshs"] = dsstichinglot.Tables[0].Rows[i]["Rxshs"].ToString();
                        dr["shs"] = dsstichinglot.Tables[0].Rows[i]["Rshs"].ToString();
                        dr["mhs"] = dsstichinglot.Tables[0].Rows[i]["Rmhs"].ToString();
                        dr["lhs"] = dsstichinglot.Tables[0].Rows[i]["Rlhs"].ToString();
                        dr["xlhs"] = dsstichinglot.Tables[0].Rows[i]["Rxlhs"].ToString();
                        dr["xxlhs"] = dsstichinglot.Tables[0].Rows[i]["Rxxlhs"].ToString();
                        dr["3xlhs"] = dsstichinglot.Tables[0].Rows[i]["R3xlhs"].ToString();
                        dr["4xlhs"] = dsstichinglot.Tables[0].Rows[i]["R4xlhs"].ToString();
                        dr["TtlQty"] = dsstichinglot.Tables[0].Rows[i]["TtlQty"].ToString();


                        dssstichinglot.Tables[0].Rows.Add(dr);

                    }
                    gvnewemb.DataSource = dssstichinglot;
                    gvnewemb.DataBind();




                    if (chkSizes.SelectedIndex >= 0)
                    {
                        gvnewemb.Columns[2].Visible = false; //30FS
                        gvnewemb.Columns[3].Visible = false; //32FS

                        gvnewemb.Columns[4].Visible = false;//34Fs
                        gvnewemb.Columns[5].Visible = false;//36Fs

                        gvnewemb.Columns[6].Visible = false; //XSFS
                        gvnewemb.Columns[7].Visible = false; //SFS

                        gvnewemb.Columns[8].Visible = false; //MFS
                        gvnewemb.Columns[9].Visible = false; //LFS

                        gvnewemb.Columns[10].Visible = false; //XLFS
                        gvnewemb.Columns[11].Visible = false; //xxlFS

                        gvnewemb.Columns[12].Visible = false; //3xlHS
                        gvnewemb.Columns[13].Visible = false; //4xlHS

                        gvnewemb.Columns[14].Visible = false; //30HS

                        gvnewemb.Columns[15].Visible = false; //32HS

                        gvnewemb.Columns[16].Visible = false; //34HS
                        gvnewemb.Columns[17].Visible = false; //36HS

                        gvnewemb.Columns[18].Visible = false; //XSHS
                        gvnewemb.Columns[19].Visible = false; //SHS

                        gvnewemb.Columns[20].Visible = false; //MHS
                        gvnewemb.Columns[21].Visible = false; //LHS

                        gvnewemb.Columns[22].Visible = false; //XLHS
                        gvnewemb.Columns[23].Visible = false; //XXLHS

                        gvnewemb.Columns[24].Visible = false; //3XLHS
                        gvnewemb.Columns[25].Visible = false; //4XLHS




                        int lop = 0;
                        //Loop through each item of checkboxlist
                        foreach (ListItem item in chkSizes.Items)
                        {
                            //check if item selected

                            if (item.Selected)
                            {

                                {
                                    if (item.Text == "30FS")
                                    {
                                        gvnewemb.Columns[2].Visible = true;
                                    }
                                    if (item.Text == "32FS")
                                    {
                                        gvnewemb.Columns[3].Visible = true;
                                    }
                                    if (item.Text == "34FS")
                                    {
                                        gvnewemb.Columns[4].Visible = true;
                                    }
                                    if (item.Text == "36FS")
                                    {
                                        gvnewemb.Columns[5].Visible = true;
                                    }
                                    if (item.Text == "XSFS")
                                    {
                                        gvnewemb.Columns[6].Visible = true;
                                    }
                                    if (item.Text == "SFS")
                                    {
                                        gvnewemb.Columns[7].Visible = true;
                                    }
                                    if (item.Text == "MFS")
                                    {
                                        gvnewemb.Columns[8].Visible = true;
                                    }
                                    if (item.Text == "LFS")
                                    {
                                        gvnewemb.Columns[9].Visible = true;
                                    }
                                    if (item.Text == "XLFS")
                                    {
                                        gvnewemb.Columns[10].Visible = true;
                                    }
                                    if (item.Text == "XXLFS")
                                    {
                                        gvnewemb.Columns[11].Visible = true;
                                    }
                                    if (item.Text == "3XLFS")
                                    {
                                        gvnewemb.Columns[12].Visible = true;
                                    }
                                    if (item.Text == "4XLFS")
                                    {
                                        gvnewemb.Columns[13].Visible = true;
                                    }


                                    // FOR HS

                                    if (item.Text == "30HS")
                                    {
                                        gvnewemb.Columns[14].Visible = true;
                                    }

                                    if (item.Text == "32HS")
                                    {
                                        gvnewemb.Columns[15].Visible = true;
                                    }

                                    if (item.Text == "34HS")
                                    {
                                        gvnewemb.Columns[16].Visible = true;
                                    }

                                    if (item.Text == "36HS")
                                    {
                                        gvnewemb.Columns[17].Visible = true;

                                    }

                                    if (item.Text == "XSHS")
                                    {
                                        gvnewemb.Columns[18].Visible = true;
                                    }

                                    if (item.Text == "SHS")
                                    {
                                        gvnewemb.Columns[19].Visible = true;
                                    }

                                    if (item.Text == "MHS")
                                    {
                                        gvnewemb.Columns[20].Visible = true;
                                    }

                                    if (item.Text == "LHS")
                                    {
                                        gvnewemb.Columns[21].Visible = true;
                                    }

                                    if (item.Text == "XLHS")
                                    {
                                        gvnewemb.Columns[22].Visible = true;
                                    }

                                    if (item.Text == "XXLHS")
                                    {
                                        gvnewemb.Columns[23].Visible = true;
                                    }

                                    if (item.Text == "3XLHS")
                                    {
                                        gvnewemb.Columns[24].Visible = true;
                                    }

                                    if (item.Text == "4XLHS")
                                    {
                                        gvnewemb.Columns[25].Visible = true;
                                    }





                                    lop++;

                                }
                            }
                        }
                        //gvcustomerorder.DataSource = dssmer;
                        //gvcustomerorder.DataBind();
                    }
                    else
                    {
                        gvnewemb.Columns[2].Visible = false; //30FS
                        gvnewemb.Columns[3].Visible = false; //32FS

                        gvnewemb.Columns[4].Visible = false;//34Fs
                        gvnewemb.Columns[5].Visible = false;//36Fs

                        gvnewemb.Columns[6].Visible = false; //XSFS
                        gvnewemb.Columns[7].Visible = false; //SFS

                        gvnewemb.Columns[8].Visible = false; //MFS
                        gvnewemb.Columns[9].Visible = false; //LFS

                        gvnewemb.Columns[10].Visible = false; //XLFS
                        gvnewemb.Columns[11].Visible = false; //xxlFS

                        gvnewemb.Columns[12].Visible = false; //3xlHS
                        gvnewemb.Columns[13].Visible = false; //4xlHS

                        gvnewemb.Columns[14].Visible = false; //30HS

                        gvnewemb.Columns[15].Visible = false; //32HS

                        gvnewemb.Columns[16].Visible = false; //34HS
                        gvnewemb.Columns[17].Visible = false; //36HS

                        gvnewemb.Columns[18].Visible = false; //XSHS
                        gvnewemb.Columns[19].Visible = false; //SHS

                        gvnewemb.Columns[20].Visible = false; //MHS
                        gvnewemb.Columns[21].Visible = false; //LHS

                        gvnewemb.Columns[22].Visible = false; //XLHS
                        gvnewemb.Columns[23].Visible = false; //XXLHS

                        gvnewemb.Columns[24].Visible = false; //3XLHS
                        gvnewemb.Columns[25].Visible = false; //4XLHS

                    }




                    for (int i = 0; i < gvnewemb.Rows.Count; i++)
                    {
                        Label lblStockRatioId = (Label)gvnewemb.Rows[i].FindControl("lblStockRatioId");
                        Label lblMasterid = (Label)gvnewemb.Rows[i].FindControl("lblMasterid");
                        Label lblTransfabid = (Label)gvnewemb.Rows[i].FindControl("lblTransfabid");

                        Label lblonlycolor = (Label)gvnewemb.Rows[i].FindControl("lblonlycolor");

                        Label lbldesignno = (Label)gvnewemb.Rows[i].FindControl("lbldesignno");
                        TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                        TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                        TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                        TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                        TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                        TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                        TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                        TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                        TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                        TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                        TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                        TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");

                        TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                        TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                        TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                        TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                        TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                        TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                        TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                        TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                        TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                        TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                        TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                        TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");

                        TextBox txtsendFQty = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQty");

                        lblStockRatioId.Text = dssstichinglot.Tables[0].Rows[i]["StockRatioId"].ToString();
                        lblMasterid.Text = dssstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                        lblTransfabid.Text = dssstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                        lblonlycolor.Text = dssstichinglot.Tables[0].Rows[i]["Color"].ToString();

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


                        TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                        TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                        TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                        TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                        TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                        TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                        TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                        TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                        TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                        TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                        TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                        TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");

                        TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                        TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                        TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                        TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                        TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                        TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                        TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                        TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                        TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                        TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                        TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                        TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");

                        TextBox txtsendFQtyac = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQtyac");

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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Ready To Embroiding Process.Thank You!!!')", true);
                    return;
                }
            }
            //DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            //if (dataSet.Tables[0].Rows.Count > 0)
            //{
            //    txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
            //    txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

            //    txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
            //    txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

            //    txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
            //    txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
            //    txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();
            //    txtHalf.Text = dataSet.Tables[0].Rows[0]["HalfQty"].ToString();
            //    txtfull.Text = dataSet.Tables[0].Rows[0]["FullQty"].ToString();
            //    txtdesignno.Text = dataSet.Tables[0].Rows[0]["DesignNo"].ToString();
            //    string processDate = dataSet.Tables[0].Rows[0]["ProcessDate"].ToString();
            //    if (processDate == "")
            //    {
            //        DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        txtProcessDate.Text = date.ToString("dd/MM/yyyy");
            //    }
            //    else
            //    {
            //        txtProcessDate.Text = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["ProcessDate"]).ToString("dd/MM/yyyy");
            //    }
            //    //txtProcessDate.Text = DateTime.ParseExact(processDate, "dd/MM/yyyy hh:mm:ss tt",
            //    //            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            //    //DateTime processDate1 = DateTime.ParseExact(processDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //txtProcessDate.Text = processDate1.ToString();
            //    string lotno = "0";
            //    if (ddlLotNo.SelectedValue == "Select Lot No")
            //    {
            //        lotno = "0";
            //    }
            //    else
            //    {
            //        lotno = ddlLotNo.SelectedValue;
            //    }


            //    DropDownList dbrand = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpProcess");

            //    if (drpProcess.Tables[0].Rows.Count > 0)
            //    {
            //        dbrand.Items.Clear();
            //        dbrand.ClearSelection();
            //        dbrand.DataSource = drpProcess.Tables[0];
            //        dbrand.DataTextField = "ProcessType";
            //        dbrand.DataValueField = "ProcessMasterID";
            //        dbrand.DataBind();
            //        dbrand.Items.Insert(0, "Select Process Type");
            //    }

            //    DataSet drpEmpName = new DataSet();
            //    if (txtUnitID.Text == "")
            //    {
            //        drpEmpName = objbs.SelectEmpName();
            //    }
            //    else
            //    {
            //        drpEmpName = objbs.SelectStitchingEmpName(Convert.ToInt32(txtUnitID.Text));
            //    }
            //    DropDownList dEmp = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpEmp");

            //    if (drpEmpName.Tables[0].Rows.Count > 0)
            //    {
            //        dEmp.Items.Clear();
            //        dEmp.DataSource = drpEmpName.Tables[0];
            //        dEmp.DataTextField = "Name";
            //        dEmp.DataValueField = "Employee_Id";
            //        dEmp.DataBind();
            //        dEmp.Items.Insert(0, "Select Employee Name");
            //    }

            //    GridView2.DataSource = drpProcess;
            //    GridView2.DataBind();



            //    DataSet workProcessManual = objbs.SelectWorkProcessTypeManual(Convert.ToInt32(ddlLotNo.SelectedValue));
            //    if (workProcessManual.Tables[0].Rows[0]["IsManual"].ToString() == "True")
            //    {
            //        divWork.Visible = true;
            //        GridView3.DataSource = workProcessManual;
            //        GridView3.DataBind();
            //    }
            //    else
            //    {
            //        divWork.Visible = false;
            //    }

            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //        drpprocess.Focus();

            //    }
            //}
            //else
            //{
            //    gvcustomerorder.DataSource = null;
            //    gvcustomerorder.DataBind();
            //}
        }

        protected void sendqty_chnaged(object sender, EventArgs e)
        {
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRemainQty");

                txtRemainQty.Text = txtsendFQty.Text;
            }

        }

        protected void GridViewRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rate = e.Row.Cells[2].Text;
                decimal rateTotal = Convert.ToDecimal(rate);

                foreach (TableCell gr in e.Row.Cells)
                {
                    if (1 <= rateTotal && rateTotal <= 3)
                    {
                        gr.BackColor = System.Drawing.Color.Red;
                    }

                    if (4 <= rateTotal && rateTotal <= 6)
                    {
                        gr.BackColor = System.Drawing.Color.GreenYellow;
                    }
                    if (7 <= rateTotal && rateTotal <= 10)
                    {
                        gr.BackColor = System.Drawing.Color.Gold;
                    }
                    if (11 <= rateTotal && rateTotal <= 15)
                    {
                        gr.BackColor = System.Drawing.Color.BlueViolet;
                    }
                    if (15 <= rateTotal && rateTotal <= 20)
                    {
                        gr.BackColor = System.Drawing.Color.RosyBrown;
                    }
                }
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (drpmultiunit.SelectedValue == "Select Unit")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot Number.Thank You!!!')", true);
                //return;
            }

            DataSet drpProcess = new DataSet();

            DataSet drpEmp = new DataSet();
            DataSet dsLotNo = new DataSet();
            //if (btnadd.Text == "Save")
            //{
            //    //if (drpmultiunit.SelectedValue == "All")
            //    //{
            //    //    dsLotNo = objbs.Select_Lotnewstich();//tblCut
            //    //}
            //    //else
            //    {
            //        dsLotNo = objbs.Select_Lotnewstich(drpmultiunit.SelectedValue);//tblCut
            //    }
            //}

            //if (btnadd.Text == "Update")
            //{
            //    dsLotNo = objbs.Select_Lotnewstichupdate(drpmultiunit.SelectedValue);

            //    //if (e.Row.RowType == DataControlRowType.DataRow)
            //    //{
            //    //    if (Convert.ToString(e.Row.Cells[4].Text) != "")
            //    //    {
            //    //        if (Convert.ToDouble(e.Row.Cells[4].Text) == 0)

            //    //            e.Row.Visible = false;
            //    //    }
            //    //}
            //}
            //if (btnadd.Text == "Received")
            //{
            //    dsLotNo = objbs.Select_Lotnewstichupdate(drpmultiunit.SelectedValue);
            //}



            drpProcess = objbs.SelectAllProcessTypeLotProcess("4");



            //if (drpmultiunit.SelectedValue == "All")
            //{
            //    drpEmp = objbs.SelectEmpName();
            //}
            //else
            //{
            //    drpEmp = objbs.SelectStitchingEmpName(Convert.ToInt32(txtUnitID.Text));
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // DropDownList drplot = (DropDownList)e.Row.FindControl("drpLotno");
                //  DropDownList drpEmp1 = (DropDownList)e.Row.FindControl("drpEmp");

                var ddProcess = (DropDownList)e.Row.FindControl("drpProcess");
                ddProcess.DataSource = drpProcess;
                ddProcess.DataTextField = "ProcessType";
                ddProcess.DataValueField = "ProcessMasterID";
                ddProcess.DataBind();
                ddProcess.Items.Insert(0, "Select Process Type");

                //var drplot = (DropDownList)e.Row.FindControl("drpLotno");
                //if (dsLotNo.Tables[0].Rows.Count > 0)
                //{
                //    drplot.DataSource = dsLotNo.Tables[0];
                //    drplot.DataTextField = "LotNo";
                //    drplot.DataValueField = "cutid";
                //    drplot.DataBind();
                //    drplot.Items.Insert(0, "Select Lot No");
                //}


                //var ddEmp = (DropDownList)e.Row.FindControl("drpEmp");
                //ddEmp.DataSource = drpEmp;
                //ddEmp.DataTextField = "Name";
                //ddEmp.DataValueField = "Employee_Id";
                //ddEmp.DataBind();
                //ddEmp.Items.Insert(0, "Select Employee Name");
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (btnadd.Text == "Received")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Allow To Delete This Entry.Thank You')", true);
                return;
            }
            else
            {

                SetRowData();

                if (ViewState["CurrentTable1"] != null)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = (DataTable)ViewState["CurrentTable1"];
                    DataRow drCurrentRow = null;
                    int rowIndex = Convert.ToInt32(e.RowIndex);
                    if (dt.Rows.Count > 1)
                    {

                        ds.Merge(dt);


                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        drCurrentRow = dt.NewRow();

                        ViewState["CurrentTable1"] = dt;
                        gvcustomerorder.DataSource = dt;
                        gvcustomerorder.DataBind();

                        SetPreviousData();

                        //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        //{
                        //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                        //    txtno.Text = Convert.ToString(i + 1);
                        //}
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        drCurrentRow = dt.NewRow();
                        ViewState["CurrentTable1"] = dt;
                        gvcustomerorder.DataSource = dt;
                        gvcustomerorder.DataBind();

                        SetPreviousData();
                        FirstGridViewRow();
                        drpmultiunit.Enabled = true;
                    }
                }
            }

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

            dct = new DataColumn("SendQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("TotalQty");
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
            drNew["TotalQty"] = "";
            drNew["SendQty"] = "";
            drNew["RemainQty"] = "";
            drNew["Bundle"] = "";
            drNew["RecQuantity"] = "";
            drNew["date"] = DateTime.Now.ToString("dd/MM/yyyy");
            drNew["Recdate"] = DateTime.Now.ToString("dd/MM/yyyy");

            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }

        //protected void txtRange_Change(object sender, EventArgs e)
        //{
        //    ButtonAdd1_Click(sender, e);

        //    int test = 0;


        //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
        //    {
        //        int total = 0;
        //        DropDownList drpProcess =
        //         (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
        //        if (drpProcess.SelectedValue != "Select Process Type")
        //        {
        //            ds = objbs.CheckQuantityOverLoad(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpProcess.SelectedValue));
        //            string ProcessType = "";
        //            test = ds.Tables[0].Rows[0]["ProcessType"].ToString();

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {

        //                ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
        //                for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
        //                {

        //                    DropDownList drpProcessCheck =
        //                     (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
        //                    if (drpProcessCheck.SelectedValue != "Select Process Type")
        //                    {
        //                        if (ProcessType == drpProcessCheck.SelectedItem.Text)
        //                        {
        //                            TextBox txtRecQuantity =
        //                                    (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");
        //                            total = total + Convert.ToInt32(txtRecQuantity.Text);
        //                        }
        //                    }


        //                }
        //                if (total > test)
        //                {
        //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + ProcessType + " has enetered Over Quantity!!!.')", true);
        //                    return;
        //                }
        //            }
        //        }

        //    }

        //}

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {

            if (btnadd.Text == "Received")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Allow To Add New Row.Thank You!!!')", true);
                return;
            }
            else
            {

                int No = 0;
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");


                    if (drpProcess.SelectedItem.Text == "Select Process Type" && drpLotno.SelectedItem.Text == "Select Lot No")
                    {
                        No = 0;
                        break;
                    }
                    else
                    {
                        No = 1;
                    }
                }

                if (No == 1)
                {

                    AddNewRow();
                }
                else
                {

                }
            }
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

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {


            #region Calculation
            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                int Row = i + 1;

                #region Qty Check


                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                if (Convert.ToInt32(txts30fsac.Text) < Convert.ToInt32(txts30fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 30FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                if (Convert.ToInt32(txts32fsac.Text) < Convert.ToInt32(txts32fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 32FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                if (Convert.ToInt32(txts34fsac.Text) < Convert.ToInt32(txts34fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 34FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                if (Convert.ToInt32(txts36fsac.Text) < Convert.ToInt32(txts36fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 36FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                if (Convert.ToInt32(txtsxsfsac.Text) < Convert.ToInt32(txtsxsfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XSFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                if (Convert.ToInt32(txtssfsac.Text) < Convert.ToInt32(txtssfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " SFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                if (Convert.ToInt32(txtsmfsac.Text) < Convert.ToInt32(txtsmfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " MFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                if (Convert.ToInt32(txtslfsac.Text) < Convert.ToInt32(txtslfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " LFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                if (Convert.ToInt32(txtsxlfsac.Text) < Convert.ToInt32(txtsxlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                if (Convert.ToInt32(txtsxxlfsac.Text) < Convert.ToInt32(txtsxxlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XXLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                if (Convert.ToInt32(txts3xlfsac.Text) < Convert.ToInt32(txts3xlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 3XLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");
                if (Convert.ToInt32(txts4xlfsac.Text) < Convert.ToInt32(txts4xlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 4XLFS.Thank You!!!.')", true);
                    return;
                }


                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                if (Convert.ToInt32(txts30hsac.Text) < Convert.ToInt32(txts30hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 30HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                if (Convert.ToInt32(txts32hsac.Text) < Convert.ToInt32(txts32hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 32HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                if (Convert.ToInt32(txts34hsac.Text) < Convert.ToInt32(txts34hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 34HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                if (Convert.ToInt32(txts36hsac.Text) < Convert.ToInt32(txts36hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 36HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                if (Convert.ToInt32(txtsxshsac.Text) < Convert.ToInt32(txtsxshs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XSHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                if (Convert.ToInt32(txtsshsac.Text) < Convert.ToInt32(txtsshs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " SHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                if (Convert.ToInt32(txtsmhsac.Text) < Convert.ToInt32(txtsmhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " MHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                if (Convert.ToInt32(txtslhsac.Text) < Convert.ToInt32(txtslhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " LHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                if (Convert.ToInt32(txtsxlhsac.Text) < Convert.ToInt32(txtsxlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                if (Convert.ToInt32(txtsxxlhsac.Text) < Convert.ToInt32(txtsxxlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XXLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                if (Convert.ToInt32(txts3xlhsac.Text) < Convert.ToInt32(txts3xlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 3XLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
                TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");
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
            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                #region

                TextBox txtsendFQty = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQty");

                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                f30 += Convert.ToDouble(txts30fs.Text);
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                f32 += Convert.ToDouble(txts32fs.Text);
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                f34 += Convert.ToDouble(txts34fs.Text);
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                f36 += Convert.ToDouble(txts36fs.Text);
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                fxs += Convert.ToDouble(txtsxsfs.Text);
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                fs += Convert.ToDouble(txtssfs.Text);
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                fm += Convert.ToDouble(txtsmfs.Text);
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                fl += Convert.ToDouble(txtslfs.Text);
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                fxl += Convert.ToDouble(txtsxlfs.Text);
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                fxxl += Convert.ToDouble(txtsxxlfs.Text);
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                f3xl += Convert.ToDouble(txts3xlfs.Text);
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                f4xl += Convert.ToDouble(txts4xlfs.Text);

                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                h30 += Convert.ToDouble(txts30hs.Text);
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                h32 += Convert.ToDouble(txts32hs.Text);
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                h34 += Convert.ToDouble(txts34hs.Text);
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                h36 += Convert.ToDouble(txts36hs.Text);
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                hxs += Convert.ToDouble(txtsxshs.Text);
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                hs += Convert.ToDouble(txtsshs.Text);
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                hm += Convert.ToDouble(txtsmhs.Text);
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                hl += Convert.ToDouble(txtslhs.Text);
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                hxl += Convert.ToDouble(txtsxlhs.Text);
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                hxxl += Convert.ToDouble(txtsxxlhs.Text);
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                h3xl += Convert.ToDouble(txts3xlhs.Text);
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
                h4xl += Convert.ToDouble(txts4xlhs.Text);

                txtsendFQty.Text = (Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text)).ToString();

                total += (Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text));

                #endregion
            }

            if (btnadd.Text == "Save")
            {

                DateTime MultiDate = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime cutdate = DateTime.ParseExact(lblcuttingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (Convert.ToDateTime(cutdate) > Convert.ToDateTime(MultiDate))
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Cutting Date And Embroidery Issue date MisMatch.Thank you!!!');", true);
                    return;

                }
                else
                {

                }

                TextBox sendFQty = (TextBox)gvcustomerorder.Rows[0].FindControl("txtsendFQty");
                sendFQty.Text = total.ToString();
            }
            else if (btnadd.Text == "Received")
            {

                DateTime MultiDate = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                TextBox Recdate = (TextBox)gvcustomerorder.Rows[0].FindControl("Recdate");
                DateTime Rdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (Convert.ToDateTime(MultiDate) > Convert.ToDateTime(Rdate))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Embroidery Issue Date And Embroidery Receive date MisMatch.Thank you!!!');", true);
                    return;

                }
                else
                {

                }


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
            sendqty_chnaged(sender, e);

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

            dtstch.Columns.Add(new DataColumn("StockRatioId", typeof(string)));
            dtstch.Columns.Add(new DataColumn("Masterid", typeof(string)));
            dtstch.Columns.Add(new DataColumn("Transfabid", typeof(string)));
            dtstch.Columns.Add(new DataColumn("Color", typeof(string)));

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

            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                Label lblStockRatioId = (Label)gvnewemb.Rows[i].FindControl("lblStockRatioId");
                Label lblMasterid = (Label)gvnewemb.Rows[i].FindControl("lblMasterid");
                Label lblTransfabid = (Label)gvnewemb.Rows[i].FindControl("lblTransfabid");
                Label lblonlycolor = (Label)gvnewemb.Rows[i].FindControl("lblonlycolor");

                Label lbldesignno = (Label)gvnewemb.Rows[i].FindControl("lbldesignno");
                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
                TextBox txtsendFQty = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQty");

                TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");
                TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");
                TextBox txtsendFQtyac = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQtyac");

                DataRow dr = dtstch.NewRow();
                dr["StockRatioId"] = lblStockRatioId.Text;
                dr["Masterid"] = lblMasterid.Text;
                dr["Transfabid"] = lblTransfabid.Text;

                dr["Color"] = lblonlycolor.Text;

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
            gvnewemb.DataSource = dssstichinglot;
            gvnewemb.DataBind();

            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                Label lblStockRatioId = (Label)gvnewemb.Rows[i].FindControl("lblStockRatioId");
                Label lblMasterid = (Label)gvnewemb.Rows[i].FindControl("lblMasterid");
                Label lblTransfabid = (Label)gvnewemb.Rows[i].FindControl("lblTransfabid");

                Label lblonlycolor = (Label)gvnewemb.Rows[i].FindControl("lblonlycolor");

                Label lbldesignno = (Label)gvnewemb.Rows[i].FindControl("lbldesignno");
                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
                TextBox txtsendFQty = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQty");

                lblStockRatioId.Text = dssstichinglot.Tables[0].Rows[i]["StockRatioId"].ToString();
                lblMasterid.Text = dssstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                lblTransfabid.Text = dssstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                lblonlycolor.Text = dssstichinglot.Tables[0].Rows[i]["color"].ToString();
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



                TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");
                TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");
                TextBox txtsendFQtyac = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQtyac");

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

            #endregion

            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                int Row = i + 1;

                #region Qty Check


                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                if (Convert.ToInt32(txts30fsac.Text) < Convert.ToInt32(txts30fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 30FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                if (Convert.ToInt32(txts32fsac.Text) < Convert.ToInt32(txts32fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 32FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                if (Convert.ToInt32(txts34fsac.Text) < Convert.ToInt32(txts34fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 34FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                if (Convert.ToInt32(txts36fsac.Text) < Convert.ToInt32(txts36fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 36FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                if (Convert.ToInt32(txtsxsfsac.Text) < Convert.ToInt32(txtsxsfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XSFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                if (Convert.ToInt32(txtssfsac.Text) < Convert.ToInt32(txtssfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " SFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                if (Convert.ToInt32(txtsmfsac.Text) < Convert.ToInt32(txtsmfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " MFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                if (Convert.ToInt32(txtslfsac.Text) < Convert.ToInt32(txtslfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " LFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                if (Convert.ToInt32(txtsxlfsac.Text) < Convert.ToInt32(txtsxlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                if (Convert.ToInt32(txtsxxlfsac.Text) < Convert.ToInt32(txtsxxlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XXLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                if (Convert.ToInt32(txts3xlfsac.Text) < Convert.ToInt32(txts3xlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 3XLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");
                if (Convert.ToInt32(txts4xlfsac.Text) < Convert.ToInt32(txts4xlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 4XLFS.Thank You!!!.')", true);
                    return;
                }


                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                if (Convert.ToInt32(txts30hsac.Text) < Convert.ToInt32(txts30hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 30HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                if (Convert.ToInt32(txts32hsac.Text) < Convert.ToInt32(txts32hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 32HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                if (Convert.ToInt32(txts34hsac.Text) < Convert.ToInt32(txts34hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 34HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                if (Convert.ToInt32(txts36hsac.Text) < Convert.ToInt32(txts36hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 36HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                if (Convert.ToInt32(txtsxshsac.Text) < Convert.ToInt32(txtsxshs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XSHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                if (Convert.ToInt32(txtsshsac.Text) < Convert.ToInt32(txtsshs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " SHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                if (Convert.ToInt32(txtsmhsac.Text) < Convert.ToInt32(txtsmhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " MHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                if (Convert.ToInt32(txtslhsac.Text) < Convert.ToInt32(txtslhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " LHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                if (Convert.ToInt32(txtsxlhsac.Text) < Convert.ToInt32(txtsxlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                if (Convert.ToInt32(txtsxxlhsac.Text) < Convert.ToInt32(txtsxxlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XXLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                if (Convert.ToInt32(txts3xlhsac.Text) < Convert.ToInt32(txts3xlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 3XLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
                TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");
                if (Convert.ToInt32(txts4xlhsac.Text) < Convert.ToInt32(txts4xlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 4XLHS.Thank You!!!.')", true);
                    return;
                }
                #endregion
            }

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                #region

                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                if (drpprocess.SelectedValue != "Select Process Type")
                {

                    TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    if (txtsendQty.Text == "")
                    {
                        txtsendQty.Text = "0";
                    }
                    if (txtsendQty.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Quantity.Thank You!!!.')", true);
                        txtsendQty.Focus();
                        return;
                    }
                    //TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    //if (date.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                    //    return;
                    //}
                }

                #endregion
            }

            if (btnadd.Text == "Received")
            {
                if (ddltype.SelectedValue == "Type")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Type.Thank You!!!.')", true);
                    ddltype.Focus();
                    return;
                }

                #region

                int iq = 1;
                int iii = 1;
                string itemc = string.Empty;
                string itemd = string.Empty;
                string iteme = string.Empty;
                string itemcd = string.Empty;
                if (ViewState["CurrentTable1"] != null)
                {
                    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];


                    string curent = string.Empty;
                    string namee = string.Empty;
                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {


                        TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");

                        TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");


                        double qty = 0;
                        double qty1 = 0;
                        qty1 = Convert.ToDouble(txtRemainQty.Text);
                        qty = Convert.ToDouble(txtdamageqty.Text) + Convert.ToDouble(txtrecQty.Text);

                        if (qty1 < qty)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Received Qty is not Greater than That Remain Qty.Thank You!!!.')", true);
                            txtrecQty.Focus();
                            return;
                        }

                    }

                }

                #endregion
            }




            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                if (drpprocess.SelectedValue != "Select Process Type")
                {

                    TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    if (txtsendQty.Text == "")
                    {
                        txtsendQty.Text = "0";
                    }
                    if (txtsendQty.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Quantity.Thank You!!!.')", true);
                        txtsendQty.Focus();
                        return;
                    }
                    //TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    //if (date.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                    //    return;
                    //}
                }
            }


            if (btnadd.Text == "Save")
            {
                #region

                DateTime MultiDate = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                TextBox txtsendFQtymain = (TextBox)gvcustomerorder.Rows[0].FindControl("txtsendFQty");

                TextBox RDate = (TextBox)gvcustomerorder.Rows[0].FindControl("Recdate");
                DateTime ReceiveDate = DateTime.ParseExact(RDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int istatus3 = objbs.insertJpEmbroiding(Convert.ToDouble(txtAmount.Text), Convert.ToInt32(ddlLotNo.SelectedValue), lbllotno.Text, MultiDate, Convert.ToInt32(drpMultiemployee.SelectedValue), Convert.ToDouble(txttotalqty.Text), empid, drpbranch.SelectedValue, ddlLotNo.SelectedItem.Text, Convert.ToInt32(txtsendFQtymain.Text), txtnarration.Text, ReceiveDate, txtitemnarration.Text);



                for (int vLoop = 0; vLoop < gvnewemb.Rows.Count; vLoop++)
                {

                    int vLoopp = 0;


                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoopp].FindControl("drpProcess");
                    Label lblfit = (Label)gvcustomerorder.Rows[vLoopp].FindControl("lblfit");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoopp].FindControl("txtsendFQty");
                    TextBox txtTotalFQty = (TextBox)gvcustomerorder.Rows[vLoopp].FindControl("txtTotalFQty");
                    //////       Label lblitemname = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblitemname");
                    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[vLoopp].FindControl("txtRemainQty");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoopp].FindControl("txtrate");
                    Label lblPattern = (Label)gvcustomerorder.Rows[vLoopp].FindControl("lblPattern");
                    Label lblPatternid = (Label)gvcustomerorder.Rows[vLoopp].FindControl("lblPatternid");
                    Label lblfitid = (Label)gvcustomerorder.Rows[vLoopp].FindControl("lblfitid");
                    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[vLoopp].FindControl("txtdamageqty");
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[vLoopp].FindControl("txtrecFQty");
                    //TextBox date1 = (TextBox)gvcustomerorder.Rows[vLoopp].FindControl("date");
                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoopp].FindControl("Recdate");
                    DateTime recdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime date11 = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                    #region
                    Label lblitemname = (Label)gvnewemb.Rows[vLoop].FindControl("lbldesignno");

                    TextBox txts30fs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts30fs");
                    TextBox txts32fs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts32fs");
                    TextBox txts34fs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts34fs");
                    TextBox txts36fs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts36fs");
                    TextBox txtsxsfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxsfs");
                    TextBox txtssfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtssfs");
                    TextBox txtsmfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsmfs");
                    TextBox txtslfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtslfs");
                    TextBox txtsxlfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxlfs");
                    TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxxlfs");
                    TextBox txts3xlfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts3xlfs");
                    TextBox txts4xlfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts4xlfs");

                    TextBox txts30hs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts30hs");
                    TextBox txts32hs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts32hs");
                    TextBox txts34hs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts34hs");
                    TextBox txts36hs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts36hs");
                    TextBox txtsxshs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxshs");
                    TextBox txtsshs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsshs");
                    TextBox txtsmhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsmhs");
                    TextBox txtslhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtslhs");
                    TextBox txtsxlhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxlhs");
                    TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxxlhs");
                    TextBox txts3xlhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts3xlhs");
                    TextBox txts4xlhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts4xlhs");

                    Label lblStockRatioId = (Label)gvnewemb.Rows[vLoop].FindControl("lblStockRatioId");
                    Label lblMasterid = (Label)gvnewemb.Rows[vLoop].FindControl("lblMasterid");
                    Label lblTransfabid = (Label)gvnewemb.Rows[vLoop].FindControl("lblTransfabid");

                    TextBox txtsendFQtycolor = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsendFQty");

                    #endregion

                    if (drpProcess.SelectedValue == "Select Process Type")
                    {

                    }
                    else
                    {
                        if (txtsendFQtycolor.Text != "0")
                        {
                            int istasHistory = objbs.inserttransjpEmbroidingHistory1(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(txtrecQty.Text), date11,
                                      recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtTotalFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), "0", empid, Convert.ToInt32(txtsendFQty.Text), Convert.ToInt32(txts30fs.Text), Convert.ToInt32(txts32fs.Text), Convert.ToInt32(txts34fs.Text), Convert.ToInt32(txts36fs.Text), Convert.ToInt32(txtsxsfs.Text), Convert.ToInt32(txtssfs.Text), Convert.ToInt32(txtsmfs.Text), Convert.ToInt32(txtslfs.Text), Convert.ToInt32(txtsxlfs.Text), Convert.ToInt32(txtsxxlfs.Text), Convert.ToInt32(txts3xlfs.Text), Convert.ToInt32(txts4xlfs.Text), Convert.ToInt32(txts30hs.Text), Convert.ToInt32(txts32hs.Text), Convert.ToInt32(txts34hs.Text), Convert.ToInt32(txts36hs.Text), Convert.ToInt32(txtsxshs.Text), Convert.ToInt32(txtsshs.Text), Convert.ToInt32(txtsmhs.Text), Convert.ToInt32(txtslhs.Text), Convert.ToInt32(txtsxlhs.Text), Convert.ToInt32(txtsxxlhs.Text), Convert.ToInt32(txts3xlhs.Text), Convert.ToInt32(txts4xlhs.Text));

                            int istas = objbs.inserttransJpEmbroiding(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(txtrecQty.Text), date11,
                                      recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtTotalFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Convert.ToInt32(ddlLotNo.SelectedValue), empid, Convert.ToInt32(txtsendFQty.Text), Convert.ToInt32(txts30fs.Text), Convert.ToInt32(txts32fs.Text), Convert.ToInt32(txts34fs.Text), Convert.ToInt32(txts36fs.Text), Convert.ToInt32(txtsxsfs.Text), Convert.ToInt32(txtssfs.Text), Convert.ToInt32(txtsmfs.Text), Convert.ToInt32(txtslfs.Text), Convert.ToInt32(txtsxlfs.Text), Convert.ToInt32(txtsxxlfs.Text), Convert.ToInt32(txts3xlfs.Text), Convert.ToInt32(txts4xlfs.Text), Convert.ToInt32(txts30hs.Text), Convert.ToInt32(txts32hs.Text), Convert.ToInt32(txts34hs.Text), Convert.ToInt32(txts36hs.Text), Convert.ToInt32(txtsxshs.Text), Convert.ToInt32(txtsshs.Text), Convert.ToInt32(txtsmhs.Text), Convert.ToInt32(txtslhs.Text), Convert.ToInt32(txtsxlhs.Text), Convert.ToInt32(txtsxxlhs.Text), Convert.ToInt32(txts3xlhs.Text), Convert.ToInt32(txts4xlhs.Text), Convert.ToInt32(lblStockRatioId.Text), Convert.ToInt32(lblMasterid.Text), Convert.ToInt32(lblTransfabid.Text));

                            if (vLoop == 0)
                            {
                                int istas1 = objbs.JpEmbroiding(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(txtrecQty.Text), date11,
                                          recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Convert.ToInt32(ddlLotNo.SelectedValue), empid, "0");
                            }
                        }
                    }
                }

                int istatus3l = objbs.statusJpEmbroiding(Convert.ToInt32(ddlLotNo.SelectedValue));

                #endregion
            }
            else if (btnadd.Text == "Received")
            {
                if (ddltype.SelectedValue == "Type")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Type.Thank You!!!.')", true);
                    ddltype.Focus();
                    return;
                }

                #region

                string Embroidingid = Request.QueryString.Get("Embroidingid");
                TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[0].FindControl("txtrecFQty");
                TextBox txtdamageqtymain = (TextBox)gvcustomerorder.Rows[0].FindControl("txtdamageqty");

                if (ddltype.SelectedValue == "Damage")
                {
                    int istasHistory = objbs.processmaintableemb(Convert.ToInt32(Embroidingid), Convert.ToInt32(txtdamageqtymain.Text), ddltype.SelectedValue, Convert.ToDouble(txtAmount.Text));
                }
                else
                {
                    int istasHistory = objbs.processmaintableemb(Convert.ToInt32(Embroidingid), Convert.ToInt32(txtrecFQty.Text), ddltype.SelectedValue, Convert.ToDouble(txtAmount.Text));
                }
                for (int vLoop = 0; vLoop < gvnewemb.Rows.Count; vLoop++)
                {

                    int vLooppp = 0;

                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLooppp].FindControl("drpProcess");
                    Label lblfit = (Label)gvcustomerorder.Rows[vLooppp].FindControl("lblfit");
                    Label lbltransid = (Label)gvcustomerorder.Rows[vLooppp].FindControl("lbltransid");
                    TextBox txtTotalFQty = (TextBox)gvcustomerorder.Rows[vLooppp].FindControl("txtTotalFQty");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLooppp].FindControl("txtsendFQty");
                    //////    Label lblitemname = (Label)gvcustomerorder.vLooppp[vLoop].FindControl("lblitemname");
                    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[vLooppp].FindControl("txtRemainQty");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLooppp].FindControl("txtrate");
                    Label lblPattern = (Label)gvcustomerorder.Rows[vLooppp].FindControl("lblPattern");
                    Label lblPatternid = (Label)gvcustomerorder.Rows[vLooppp].FindControl("lblPatternid");
                    Label lblfitid = (Label)gvcustomerorder.Rows[vLooppp].FindControl("lblfitid");
                    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[vLooppp].FindControl("txtdamageqty");
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[vLooppp].FindControl("txtrecFQty");
                    //TextBox date1 = (TextBox)gvcustomerorder.Rows[vLooppp].FindControl("date");
                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLooppp].FindControl("Recdate");
                    DateTime recdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime date11 = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                    #region

                    Label lblitemname = (Label)gvnewemb.Rows[vLoop].FindControl("lbldesignno");

                    TextBox txts30fs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts30fs");
                    TextBox txts32fs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts32fs");
                    TextBox txts34fs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts34fs");
                    TextBox txts36fs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts36fs");
                    TextBox txtsxsfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxsfs");
                    TextBox txtssfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtssfs");
                    TextBox txtsmfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsmfs");
                    TextBox txtslfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtslfs");
                    TextBox txtsxlfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxlfs");
                    TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxxlfs");
                    TextBox txts3xlfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts3xlfs");
                    TextBox txts4xlfs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts4xlfs");

                    TextBox txts30hs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts30hs");
                    TextBox txts32hs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts32hs");
                    TextBox txts34hs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts34hs");
                    TextBox txts36hs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts36hs");
                    TextBox txtsxshs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxshs");
                    TextBox txtsshs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsshs");
                    TextBox txtsmhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsmhs");
                    TextBox txtslhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtslhs");
                    TextBox txtsxlhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxlhs");
                    TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsxxlhs");
                    TextBox txts3xlhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts3xlhs");
                    TextBox txts4xlhs = (TextBox)gvnewemb.Rows[vLoop].FindControl("txts4xlhs");

                    TextBox txtsendFQtycolor = (TextBox)gvnewemb.Rows[vLoop].FindControl("txtsendFQty");

                    Label lblStockRatioId = (Label)gvnewemb.Rows[vLoop].FindControl("lblStockRatioId");
                    Label lblMasterid = (Label)gvnewemb.Rows[vLoop].FindControl("lblMasterid");
                    Label lblTransfabid = (Label)gvnewemb.Rows[vLoop].FindControl("lblTransfabid");

                    #endregion

                    if (drpProcess.SelectedValue == "Select Process Type")
                    {

                    }
                    else
                    {
                        if (txtsendFQtycolor.Text != "0")
                        {
                            int istasHistory = objbs.inserttransjpembHistory1new(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(txtrecQty.Text), recdate,
                                      recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtTotalFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Embroidingid, empid, Convert.ToInt32(txtsendFQty.Text), Convert.ToInt32(txts30fs.Text), Convert.ToInt32(txts32fs.Text), Convert.ToInt32(txts34fs.Text), Convert.ToInt32(txts36fs.Text), Convert.ToInt32(txtsxsfs.Text), Convert.ToInt32(txtssfs.Text), Convert.ToInt32(txtsmfs.Text), Convert.ToInt32(txtslfs.Text), Convert.ToInt32(txtsxlfs.Text), Convert.ToInt32(txtsxxlfs.Text), Convert.ToInt32(txts3xlfs.Text), Convert.ToInt32(txts4xlfs.Text), Convert.ToInt32(txts30hs.Text), Convert.ToInt32(txts32hs.Text), Convert.ToInt32(txts34hs.Text), Convert.ToInt32(txts36hs.Text), Convert.ToInt32(txtsxshs.Text), Convert.ToInt32(txtsshs.Text), Convert.ToInt32(txtsmhs.Text), Convert.ToInt32(txtslhs.Text), Convert.ToInt32(txtsxlhs.Text), Convert.ToInt32(txtsxxlhs.Text), Convert.ToInt32(txts3xlhs.Text), Convert.ToInt32(txts4xlhs.Text), ddltype.SelectedItem.Text);

                            if (vLoop == 0)
                            {
                                //int istasHistory = objbs.inserttransjpEmbroidingHistory1(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(txtrecQty.Text), date11,
                                //          recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Embroidingid, empid, Convert.ToInt32(txtsendFQty.Text));



                                int istas = objbs.JpEmbroidingREC(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(txtrecQty.Text), date11,
                                          recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Convert.ToInt32(ddlLotNo.SelectedValue), empid, lbltransid.Text);

                            }



                            int EMBROIDERING = 3;

                            if (ddltype.SelectedValue == "Damage")
                            {
                                int rec = objbs.DamageProcessStockRatioemb(Convert.ToInt32(txts30fs.Text), Convert.ToInt32(txts32fs.Text), Convert.ToInt32(txts34fs.Text), Convert.ToInt32(txts36fs.Text), Convert.ToInt32(txtsxsfs.Text), Convert.ToInt32(txtssfs.Text), Convert.ToInt32(txtsmfs.Text), Convert.ToInt32(txtslfs.Text), Convert.ToInt32(txtsxlfs.Text), Convert.ToInt32(txtsxxlfs.Text), Convert.ToInt32(txts3xlfs.Text), Convert.ToInt32(txts4xlfs.Text), Convert.ToInt32(txts30hs.Text), Convert.ToInt32(txts32hs.Text), Convert.ToInt32(txts34hs.Text), Convert.ToInt32(txts36hs.Text), Convert.ToInt32(txtsxshs.Text), Convert.ToInt32(txtsshs.Text), Convert.ToInt32(txtsmhs.Text), Convert.ToInt32(txtslhs.Text), Convert.ToInt32(txtsxlhs.Text), Convert.ToInt32(txtsxxlhs.Text), Convert.ToInt32(txts3xlhs.Text), Convert.ToInt32(txts4xlhs.Text), Convert.ToInt32(lblStockRatioId.Text), Convert.ToInt32(lblMasterid.Text), Convert.ToInt32(lblTransfabid.Text), EMBROIDERING);
                            }
                            else
                            {
                                int rec = objbs.updateProcessStockRatioemb(Convert.ToInt32(txts30fs.Text), Convert.ToInt32(txts32fs.Text), Convert.ToInt32(txts34fs.Text), Convert.ToInt32(txts36fs.Text), Convert.ToInt32(txtsxsfs.Text), Convert.ToInt32(txtssfs.Text), Convert.ToInt32(txtsmfs.Text), Convert.ToInt32(txtslfs.Text), Convert.ToInt32(txtsxlfs.Text), Convert.ToInt32(txtsxxlfs.Text), Convert.ToInt32(txts3xlfs.Text), Convert.ToInt32(txts4xlfs.Text), Convert.ToInt32(txts30hs.Text), Convert.ToInt32(txts32hs.Text), Convert.ToInt32(txts34hs.Text), Convert.ToInt32(txts36hs.Text), Convert.ToInt32(txtsxshs.Text), Convert.ToInt32(txtsshs.Text), Convert.ToInt32(txtsmhs.Text), Convert.ToInt32(txtslhs.Text), Convert.ToInt32(txtsxlhs.Text), Convert.ToInt32(txtsxxlhs.Text), Convert.ToInt32(txts3xlhs.Text), Convert.ToInt32(txts4xlhs.Text), Convert.ToInt32(lblStockRatioId.Text), Convert.ToInt32(lblMasterid.Text), Convert.ToInt32(lblTransfabid.Text));

                            }
                        }
                    }
                }

                int istatus3l = objbs.statusJpEmbroiding(Convert.ToInt32(ddlLotNo.SelectedValue));



                #endregion
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Administrator.Because Some process Received.')", true);
                return;
            }



            System.Threading.Thread.Sleep(3000);

            Response.Redirect("EmbroidingGrid1.aspx");


        }

        //protected void drpprocess_selected(object sender, EventArgs e)
        //{
        //    DropDownList ddl = (DropDownList)sender;
        //    GridViewRow row = (GridViewRow)ddl.NamingContainer;
        //    DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
        //    TextBox txtrate = (TextBox)row.FindControl("txtRate");


        //    DataSet ds = new DataSet();
        //    if (ddlprocess.SelectedValue != "Select Process Type")
        //    {
        //        ds = objbs.getrateforstiching(ddlprocess.SelectedValue, ddlLotNo.SelectedValue);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            txtrate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
        //        }
        //    }

        //}

        protected void GridViewWork_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell gr in e.Row.Cells)
                {
                    if (gr.Text == "YES")
                    {
                        gr.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        gr.BackColor = System.Drawing.Color.Red;
                    }

                    if (gr.Text == "True")
                    {
                        gr.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        gr.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void gvnewemb_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // string  Q30Fjj = (DataBinder.Eval(e.Row.DataItem, "Design").ToString());

                Q30F = Q30F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30fs"));
                Q32F = Q32F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32fs"));
                Q34F = Q34F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34fs"));
                Q36F = Q36F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36fs"));
                QXSF = QXSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xsfs"));
                QSF = QSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "sfs"));
                QMF = QMF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "mfs"));
                QLF = QLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "lfs"));
                QXLF = QXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xlfs"));
                QXXLF = QXXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xxlfs"));
                Q3XLF = Q3XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3xlfs"));
                Q4XLF = Q4XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4xlfs"));

                Q30H = Q30H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30hs"));
                Q32H = Q32H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32hs"));
                Q34H = Q34H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34hs"));
                Q36H = Q36H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36hs"));
                QXSH = QXSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xshs"));
                QSH = QSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "shs"));
                QMH = QMH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "mhs"));
                QLH = QLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "lhs"));
                QXLH = QXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xlhs"));
                QXXLH = QXXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xxlhs"));
                Q3XLH = Q3XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3xlhs"));
                Q4XLH = Q4XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4xlhs"));

                GVtotalshirt = GVtotalshirt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TtlQty"));
                // QttlFH = QttlFH + Q30F + Q32F + Q34F + Q36F + QXSF + QSF + QMF + QLF + QXLF + QXXLF + Q3XLF + Q4XLF + Q30H + Q32H + Q34H + Q36H + QXSH + QSH + QMH + QLH + QXLH + QXXLH + Q3XLH + Q4XLH;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // e.Row.Cells[6].Text = GVtotalshirt.ToString();

                e.Row.Cells[2].Text = Q30F.ToString();
                e.Row.Cells[3].Text = Q32F.ToString();
                e.Row.Cells[4].Text = Q34F.ToString();
                e.Row.Cells[5].Text = Q36F.ToString();
                e.Row.Cells[6].Text = QXSF.ToString();
                e.Row.Cells[7].Text = QSF.ToString();
                e.Row.Cells[8].Text = QMF.ToString();
                e.Row.Cells[9].Text = QLF.ToString();
                e.Row.Cells[10].Text = QXLF.ToString();
                e.Row.Cells[11].Text = QXXLF.ToString();
                e.Row.Cells[12].Text = Q3XLF.ToString();
                e.Row.Cells[13].Text = Q4XLF.ToString();

                e.Row.Cells[14].Text = Q30H.ToString();
                e.Row.Cells[15].Text = Q32H.ToString();
                e.Row.Cells[16].Text = Q34H.ToString();
                e.Row.Cells[17].Text = Q36H.ToString();
                e.Row.Cells[18].Text = QXSH.ToString();
                e.Row.Cells[19].Text = QSH.ToString();
                e.Row.Cells[20].Text = QMH.ToString();
                e.Row.Cells[21].Text = QLH.ToString();
                e.Row.Cells[22].Text = QXLH.ToString();
                e.Row.Cells[23].Text = QXXLH.ToString();
                e.Row.Cells[24].Text = Q3XLH.ToString();
                e.Row.Cells[25].Text = Q4XLH.ToString();

                e.Row.Cells[26].Text = GVtotalshirt.ToString();
                // e.Row.Cells[33].Text = QttlFH.ToString();
                // e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        protected void btncalc_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                int Row = i + 1;

                #region Qty Check


                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                if (Convert.ToInt32(txts30fsac.Text) < Convert.ToInt32(txts30fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 30FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                if (Convert.ToInt32(txts32fsac.Text) < Convert.ToInt32(txts32fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 32FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                if (Convert.ToInt32(txts34fsac.Text) < Convert.ToInt32(txts34fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 34FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                if (Convert.ToInt32(txts36fsac.Text) < Convert.ToInt32(txts36fs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 36FS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                if (Convert.ToInt32(txtsxsfsac.Text) < Convert.ToInt32(txtsxsfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XSFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                if (Convert.ToInt32(txtssfsac.Text) < Convert.ToInt32(txtssfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " SFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                if (Convert.ToInt32(txtsmfsac.Text) < Convert.ToInt32(txtsmfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " MFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                if (Convert.ToInt32(txtslfsac.Text) < Convert.ToInt32(txtslfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " LFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                if (Convert.ToInt32(txtsxlfsac.Text) < Convert.ToInt32(txtsxlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                if (Convert.ToInt32(txtsxxlfsac.Text) < Convert.ToInt32(txtsxxlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XXLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                if (Convert.ToInt32(txts3xlfsac.Text) < Convert.ToInt32(txts3xlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 3XLFS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");
                if (Convert.ToInt32(txts4xlfsac.Text) < Convert.ToInt32(txts4xlfs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 4XLFS.Thank You!!!.')", true);
                    return;
                }


                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                if (Convert.ToInt32(txts30hsac.Text) < Convert.ToInt32(txts30hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 30HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                if (Convert.ToInt32(txts32hsac.Text) < Convert.ToInt32(txts32hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 32HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                if (Convert.ToInt32(txts34hsac.Text) < Convert.ToInt32(txts34hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 34HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                if (Convert.ToInt32(txts36hsac.Text) < Convert.ToInt32(txts36hs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 36HS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                if (Convert.ToInt32(txtsxshsac.Text) < Convert.ToInt32(txtsxshs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XSHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                if (Convert.ToInt32(txtsshsac.Text) < Convert.ToInt32(txtsshs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " SHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                if (Convert.ToInt32(txtsmhsac.Text) < Convert.ToInt32(txtsmhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " MHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                if (Convert.ToInt32(txtslhsac.Text) < Convert.ToInt32(txtslhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " LHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                if (Convert.ToInt32(txtsxlhsac.Text) < Convert.ToInt32(txtsxlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                if (Convert.ToInt32(txtsxxlhsac.Text) < Convert.ToInt32(txtsxxlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " XXLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                if (Convert.ToInt32(txts3xlhsac.Text) < Convert.ToInt32(txts3xlhs.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Qty in Row " + Row + " 3XLHS.Thank You!!!.')", true);
                    return;
                }
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
                TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");
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
            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                #region

                TextBox txtsendFQty = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQty");

                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                f30 += Convert.ToDouble(txts30fs.Text);
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                f32 += Convert.ToDouble(txts32fs.Text);
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                f34 += Convert.ToDouble(txts34fs.Text);
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                f36 += Convert.ToDouble(txts36fs.Text);
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                fxs += Convert.ToDouble(txtsxsfs.Text);
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                fs += Convert.ToDouble(txtssfs.Text);
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                fm += Convert.ToDouble(txtsmfs.Text);
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                fl += Convert.ToDouble(txtslfs.Text);
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                fxl += Convert.ToDouble(txtsxlfs.Text);
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                fxxl += Convert.ToDouble(txtsxxlfs.Text);
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                f3xl += Convert.ToDouble(txts3xlfs.Text);
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                f4xl += Convert.ToDouble(txts4xlfs.Text);

                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                h30 += Convert.ToDouble(txts30hs.Text);
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                h32 += Convert.ToDouble(txts32hs.Text);
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                h34 += Convert.ToDouble(txts34hs.Text);
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                h36 += Convert.ToDouble(txts36hs.Text);
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                hxs += Convert.ToDouble(txtsxshs.Text);
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                hs += Convert.ToDouble(txtsshs.Text);
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                hm += Convert.ToDouble(txtsmhs.Text);
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                hl += Convert.ToDouble(txtslhs.Text);
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                hxl += Convert.ToDouble(txtsxlhs.Text);
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                hxxl += Convert.ToDouble(txtsxxlhs.Text);
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                h3xl += Convert.ToDouble(txts3xlhs.Text);
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Assign Rate in  " + Convert.ToInt32(ii + 1) + " Row .Please Contact Administrator. Thank you!!');", true);
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
            sendqty_chnaged(sender, e);

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

            dtstch.Columns.Add(new DataColumn("StockRatioId", typeof(string)));
            dtstch.Columns.Add(new DataColumn("Masterid", typeof(string)));
            dtstch.Columns.Add(new DataColumn("Transfabid", typeof(string)));

            dtstch.Columns.Add(new DataColumn("color", typeof(string)));

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

            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                Label lblStockRatioId = (Label)gvnewemb.Rows[i].FindControl("lblStockRatioId");
                Label lblMasterid = (Label)gvnewemb.Rows[i].FindControl("lblMasterid");
                Label lblTransfabid = (Label)gvnewemb.Rows[i].FindControl("lblTransfabid");

                Label lblonlycolor = (Label)gvnewemb.Rows[i].FindControl("lblonlycolor");

                Label lbldesignno = (Label)gvnewemb.Rows[i].FindControl("lbldesignno");
                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
                TextBox txtsendFQty = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQty");

                TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");
                TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");
                TextBox txtsendFQtyac = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQtyac");

                DataRow dr = dtstch.NewRow();
                dr["StockRatioId"] = lblStockRatioId.Text;
                dr["Masterid"] = lblMasterid.Text;
                dr["Transfabid"] = lblTransfabid.Text;

                dr["Color"] = lblonlycolor.Text;

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
            gvnewemb.DataSource = dssstichinglot;
            gvnewemb.DataBind();

            for (int i = 0; i < gvnewemb.Rows.Count; i++)
            {
                Label lblStockRatioId = (Label)gvnewemb.Rows[i].FindControl("lblStockRatioId");
                Label lblMasterid = (Label)gvnewemb.Rows[i].FindControl("lblMasterid");
                Label lblTransfabid = (Label)gvnewemb.Rows[i].FindControl("lblTransfabid");
                Label lblonlycolor = (Label)gvnewemb.Rows[i].FindControl("lblonlycolor");

                Label lbldesignno = (Label)gvnewemb.Rows[i].FindControl("lbldesignno");
                TextBox txts30fs = (TextBox)gvnewemb.Rows[i].FindControl("txts30fs");
                TextBox txts32fs = (TextBox)gvnewemb.Rows[i].FindControl("txts32fs");
                TextBox txts34fs = (TextBox)gvnewemb.Rows[i].FindControl("txts34fs");
                TextBox txts36fs = (TextBox)gvnewemb.Rows[i].FindControl("txts36fs");
                TextBox txtsxsfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfs");
                TextBox txtssfs = (TextBox)gvnewemb.Rows[i].FindControl("txtssfs");
                TextBox txtsmfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfs");
                TextBox txtslfs = (TextBox)gvnewemb.Rows[i].FindControl("txtslfs");
                TextBox txtsxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxxlfs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfs");
                TextBox txts3xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfs");
                TextBox txts4xlfs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfs");
                TextBox txts30hs = (TextBox)gvnewemb.Rows[i].FindControl("txts30hs");
                TextBox txts32hs = (TextBox)gvnewemb.Rows[i].FindControl("txts32hs");
                TextBox txts34hs = (TextBox)gvnewemb.Rows[i].FindControl("txts34hs");
                TextBox txts36hs = (TextBox)gvnewemb.Rows[i].FindControl("txts36hs");
                TextBox txtsxshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshs");
                TextBox txtsshs = (TextBox)gvnewemb.Rows[i].FindControl("txtsshs");
                TextBox txtsmhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhs");
                TextBox txtslhs = (TextBox)gvnewemb.Rows[i].FindControl("txtslhs");
                TextBox txtsxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxxlhs = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhs");
                TextBox txts3xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhs");
                TextBox txts4xlhs = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhs");
                TextBox txtsendFQty = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQty");

                lblStockRatioId.Text = dssstichinglot.Tables[0].Rows[i]["StockRatioId"].ToString();
                lblMasterid.Text = dssstichinglot.Tables[0].Rows[i]["Masterid"].ToString();
                lblTransfabid.Text = dssstichinglot.Tables[0].Rows[i]["Transfabid"].ToString();

                lblonlycolor.Text = dssstichinglot.Tables[0].Rows[i]["Color"].ToString();

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



                TextBox txts30fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30fsac");
                TextBox txts32fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32fsac");
                TextBox txts34fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34fsac");
                TextBox txts36fsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36fsac");
                TextBox txtsxsfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxsfsac");
                TextBox txtssfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtssfsac");
                TextBox txtsmfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmfsac");
                TextBox txtslfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslfsac");
                TextBox txtsxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlfsac");
                TextBox txtsxxlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlfsac");
                TextBox txts3xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlfsac");
                TextBox txts4xlfsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlfsac");
                TextBox txts30hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts30hsac");
                TextBox txts32hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts32hsac");
                TextBox txts34hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts34hsac");
                TextBox txts36hsac = (TextBox)gvnewemb.Rows[i].FindControl("txts36hsac");
                TextBox txtsxshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxshsac");
                TextBox txtsshsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsshsac");
                TextBox txtsmhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsmhsac");
                TextBox txtslhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtslhsac");
                TextBox txtsxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxlhsac");
                TextBox txtsxxlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txtsxxlhsac");
                TextBox txts3xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts3xlhsac");
                TextBox txts4xlhsac = (TextBox)gvnewemb.Rows[i].FindControl("txts4xlhsac");
                TextBox txtsendFQtyac = (TextBox)gvnewemb.Rows[i].FindControl("txtsendFQtyac");

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
    }
}