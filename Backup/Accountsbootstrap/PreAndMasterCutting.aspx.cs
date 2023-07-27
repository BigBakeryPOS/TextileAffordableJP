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
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class PreAndMasterCutting : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double ttlissuemtr = 0; double ttlActualMeter = 0;
        double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;
        DataRow[] rows;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dst = objBs.Getjobworker();
                if (dst.Tables[0].Rows.Count > 0)
                {
                    ddlsupplier.DataSource = dst.Tables[0];
                    ddlsupplier.DataTextField = "LedgerName";
                    ddlsupplier.DataValueField = "LedgerID";
                    ddlsupplier.DataBind();
                    ddlsupplier.Items.Insert(0, "All");
                }
                DataSet dsItem = objBs.Getitem();
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlitem.DataSource = dsItem.Tables[0];
                    ddlitem.DataTextField = "ItemCode";
                    ddlitem.DataValueField = "Itemid";
                    ddlitem.DataBind();
                    ddlitem.Items.Insert(0, "All");
                }

                string super = Session["IsSuperAdmin"].ToString();
                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "All");
                    }
                }
                else
                {

                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }

            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {

            #region

            gvcust.Columns[12].Visible = true;
            gvcust.Columns[13].Visible = true;
            gvcust.Columns[14].Visible = true;
            gvcust.Columns[15].Visible = true;
            gvcust.Columns[16].Visible = true;
            gvcust.Columns[17].Visible = true;
            gvcust.Columns[18].Visible = true;
            gvcust.Columns[19].Visible = true;
            gvcust.Columns[20].Visible = true;
            gvcust.Columns[21].Visible = true;
            gvcust.Columns[22].Visible = true;
            gvcust.Columns[23].Visible = true;
            gvcust.Columns[24].Visible = true;
            gvcust.Columns[25].Visible = true;
            gvcust.Columns[26].Visible = true;
            gvcust.Columns[27].Visible = true;
            gvcust.Columns[28].Visible = true;
            gvcust.Columns[29].Visible = true;
            gvcust.Columns[30].Visible = true;
            gvcust.Columns[31].Visible = true;
            gvcust.Columns[32].Visible = true;
            gvcust.Columns[33].Visible = true;
            gvcust.Columns[34].Visible = true;
            gvcust.Columns[35].Visible = true;
            gvcust.Columns[36].Visible = true;
            gvcust.Columns[37].Visible = true;
            gvcust.Columns[38].Visible = true;
            gvcust.Columns[39].Visible = true;
            gvcust.Columns[40].Visible = true;
            gvcust.Columns[41].Visible = true;
            gvcust.Columns[42].Visible = true;
            gvcust.Columns[43].Visible = true;
            gvcust.Columns[44].Visible = true;
            gvcust.Columns[45].Visible = true;
            gvcust.Columns[46].Visible = true;
            gvcust.Columns[47].Visible = true;
            gvcust.Columns[48].Visible = true;
            gvcust.Columns[49].Visible = true;
            gvcust.Columns[50].Visible = true;
            gvcust.Columns[51].Visible = true;
            gvcust.Columns[52].Visible = true;
            gvcust.Columns[53].Visible = true;
            gvcust.Columns[54].Visible = true;
            gvcust.Columns[55].Visible = true;
            gvcust.Columns[56].Visible = true;
            gvcust.Columns[57].Visible = true;
            gvcust.Columns[58].Visible = true;
            gvcust.Columns[59].Visible = true;
            gvcust.Columns[60].Visible = true;
            gvcust.Columns[61].Visible = true;
            gvcust.Columns[62].Visible = true;
            gvcust.Columns[63].Visible = true;
            gvcust.Columns[64].Visible = true;
            gvcust.Columns[65].Visible = true;
            gvcust.Columns[66].Visible = true;
            gvcust.Columns[67].Visible = true;
            gvcust.Columns[68].Visible = true;
            gvcust.Columns[69].Visible = true;
            gvcust.Columns[70].Visible = true;
            gvcust.Columns[71].Visible = true;
            gvcust.Columns[72].Visible = true;
            gvcust.Columns[73].Visible = true;
            gvcust.Columns[74].Visible = true;
            gvcust.Columns[75].Visible = true;
            gvcust.Columns[76].Visible = true;
            gvcust.Columns[77].Visible = true;
            gvcust.Columns[78].Visible = true;
            gvcust.Columns[79].Visible = true;
            gvcust.Columns[80].Visible = true;
            gvcust.Columns[81].Visible = true;
            gvcust.Columns[82].Visible = true;
            gvcust.Columns[83].Visible = true;

            gvcust.Columns[84].Visible = true;
            gvcust.Columns[85].Visible = true;
            gvcust.Columns[86].Visible = true;

            gvcust.Columns[87].Visible = true;
            gvcust.Columns[88].Visible = true;
            gvcust.Columns[89].Visible = true;

            gvcust.Columns[90].Visible = true;
            gvcust.Columns[91].Visible = true;
            gvcust.Columns[92].Visible = true;

            #endregion

            gvcust.DataSource = null;
            gvcust.DataBind();

            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dsCut = new DataSet();
            DataSet dsMaster = new DataSet();

            if (ddltype.SelectedValue == "1")
            {
                dsCut = objBs.precuttingComparesummary(drpbranch.SelectedValue, ddltype.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue, ddlitem.SelectedValue);
                dsMaster = objBs.preandmastercuttingComparesummary(drpbranch.SelectedValue, ddltype.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue, ddlitem.SelectedValue);

            }
            else
            {
                dsCut = objBs.precuttingComparedetailed(drpbranch.SelectedValue, ddltype.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue, ddlitem.SelectedValue);
                dsMaster = objBs.preandmastercuttingComparedetailed(drpbranch.SelectedValue, ddltype.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue, ddlitem.SelectedValue);

            }

            DataTable dtstock = new DataTable();
            DataSet dsstock = new DataSet();
            DataRow drstock;

            DataSet dstd = new DataSet();

            if (dsCut.Tables[0].Rows.Count > 0)
            {
                #region NewColumn Table


                DataTable dttt;
                DataRow drNew;
                DataColumn dct;
                dttt = new DataTable();

                #region

                dct = new DataColumn("Cutid");
                dttt.Columns.Add(dct);
                dct = new DataColumn("LotNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CutDate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Master");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MasterDate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ItemCode");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ItemName");
                dttt.Columns.Add(dct);

                dct = new DataColumn("BrandName");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Fit");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Width");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Designno");
                dttt.Columns.Add(dct);

                #endregion

                #region

                dct = new DataColumn("CF30");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CF32");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CF34");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CF36");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CFXS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CFS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CFM");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CFL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CFXL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CFXXl");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CF3XL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CF4XL");
                dttt.Columns.Add(dct);


                dct = new DataColumn("CH30");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CH32");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CH34");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CH36");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CHXS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CHS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CHM");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CHL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CHXL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CHXXl");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CH3XL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CH4XL");
                dttt.Columns.Add(dct);

                #endregion

                #region
                dct = new DataColumn("MF30");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MF32");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MF34");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MF36");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MFXS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MFS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MFM");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MFL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MFXL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MFXXl");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MF3XL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MF4XL");
                dttt.Columns.Add(dct);

                dct = new DataColumn("MH30");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MH32");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MH34");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MH36");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MHXS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MHS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MHM");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MHL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MHXL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MHXXl");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MH3XL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MH4XL");
                dttt.Columns.Add(dct);
                #endregion


                #region

                dct = new DataColumn("DF30");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DF32");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DF34");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DF36");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DFXS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DFS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DFM");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DFL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DFXL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DFXXl");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DF3XL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DF4XL");
                dttt.Columns.Add(dct);


                dct = new DataColumn("DH30");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DH32");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DH34");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DH36");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DHXS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DHS");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DHM");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DHL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DHXL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DHXXl");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DH3XL");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DH4XL");
                dttt.Columns.Add(dct);

                #endregion

                dct = new DataColumn("CFT");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CHT");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MFT");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MHT");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DFT");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DHT");
                dttt.Columns.Add(dct);

                dct = new DataColumn("CFHT");
                dttt.Columns.Add(dct);
                dct = new DataColumn("MFHT");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DFHT");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Changes");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                foreach (DataRow Dr in dsCut.Tables[0].Rows)
                {
                    drNew = dttt.NewRow();

                    int rowsLength = 0;

                    if (dsMaster.Tables[0].Rows.Count > 0)
                    {
                        rows = dsMaster.Tables[0].Select("CutId='" + Dr["CutId"] + "' and DesignNo='" + Dr["DesignNo"] + "'");
                        rowsLength = rows.Length;

                        if (rows.Length > 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.');", true);
                            return;
                        }
                    }

                    if (rowsLength > 0)
                    {
                        #region

                        drNew["Cutid"] = Dr["cutid"];
                        drNew["LotNo"] = Dr["CompanyFullLotNo"];
                        drNew["CutDate"] = Convert.ToDateTime(Dr["Deliverydate"]).ToString("dd/MM/yyyy");
                        drNew["Master"] = Dr["LedgerName"];
                        drNew["ItemCode"] = Dr["ItemCode"];
                        drNew["ItemName"] = Dr["ItemName"];
                        drNew["BrandName"] = Dr["BrandName"];
                        drNew["Fit"] = Dr["Fit"];
                        drNew["Width"] = Dr["Width"];
                        drNew["Designno"] = Dr["Designno"];

                        drNew["MasterDate"] = Convert.ToDateTime(rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");

                        #region
                        drNew["CF30"] = Dr["F30"];
                        drNew["CF32"] = Dr["F32"];
                        drNew["CF34"] = Dr["F34"];
                        drNew["CF36"] = Dr["F36"];
                        drNew["CFXS"] = Dr["FXS"];
                        drNew["CFS"] = Dr["FS"];
                        drNew["CFM"] = Dr["FM"];
                        drNew["CFL"] = Dr["FL"];
                        drNew["CFXL"] = Dr["FXL"];
                        drNew["CFXXl"] = Dr["FXXl"];
                        drNew["CF3XL"] = Dr["F3XL"];
                        drNew["CF4XL"] = Dr["F4XL"];

                        drNew["CH30"] = Dr["H30"];
                        drNew["CH32"] = Dr["H32"];
                        drNew["CH34"] = Dr["H34"];
                        drNew["CH36"] = Dr["H36"];
                        drNew["CHXS"] = Dr["HXS"];
                        drNew["CHS"] = Dr["HS"];
                        drNew["CHM"] = Dr["HM"];
                        drNew["CHL"] = Dr["HL"];
                        drNew["CHXL"] = Dr["HXL"];
                        drNew["CHXXl"] = Dr["HXXl"];
                        drNew["CH3XL"] = Dr["H3XL"];
                        drNew["CH4XL"] = Dr["H4XL"];

                        #endregion

                        #region
                        drNew["MF30"] = rows[0]["F30"];
                        drNew["MF32"] = rows[0]["F32"];
                        drNew["MF34"] = rows[0]["F34"];
                        drNew["MF36"] = rows[0]["F36"];
                        drNew["MFXS"] = rows[0]["FXS"];
                        drNew["MFS"] = rows[0]["FS"];
                        drNew["MFM"] = rows[0]["FM"];
                        drNew["MFL"] = rows[0]["FL"];
                        drNew["MFXL"] = rows[0]["FXL"];
                        drNew["MFXXL"] = rows[0]["FXXL"];
                        drNew["MF3XL"] = rows[0]["F3XL"];
                        drNew["MF4XL"] = rows[0]["F4XL"];

                        drNew["MH30"] = rows[0]["H30"];
                        drNew["MH32"] = rows[0]["H32"];
                        drNew["MH34"] = rows[0]["H34"];
                        drNew["MH36"] = rows[0]["H36"];
                        drNew["MHXS"] = rows[0]["HXS"];
                        drNew["MHS"] = rows[0]["HS"];
                        drNew["MHM"] = rows[0]["HM"];
                        drNew["MHL"] = rows[0]["HL"];
                        drNew["MHXL"] = rows[0]["HXL"];
                        drNew["MHXXl"] = rows[0]["HXXL"];
                        drNew["MH3XL"] = rows[0]["H3XL"];
                        drNew["MH4XL"] = rows[0]["H4XL"];

                        #endregion

                        #region
                        drNew["DF30"] = Convert.ToInt32(Dr["F30"]) - Convert.ToInt32(rows[0]["F30"]);
                        drNew["DF32"] = Convert.ToInt32(Dr["F32"]) - Convert.ToInt32(rows[0]["F32"]);
                        drNew["DF34"] = Convert.ToInt32(Dr["F34"]) - Convert.ToInt32(rows[0]["F34"]);
                        drNew["DF36"] = Convert.ToInt32(Dr["F36"]) - Convert.ToInt32(rows[0]["F36"]);
                        drNew["DFXS"] = Convert.ToInt32(Dr["FXS"]) - Convert.ToInt32(rows[0]["FXS"]);
                        drNew["DFS"] = Convert.ToInt32(Dr["FS"]) - Convert.ToInt32(rows[0]["FS"]);
                        drNew["DFM"] = Convert.ToInt32(Dr["FM"]) - Convert.ToInt32(rows[0]["FM"]);
                        drNew["DFL"] = Convert.ToInt32(Dr["FL"]) - Convert.ToInt32(rows[0]["FL"]);
                        drNew["DFXL"] = Convert.ToInt32(Dr["FXL"]) - Convert.ToInt32(rows[0]["FXL"]);
                        drNew["DFXXL"] = Convert.ToInt32(Dr["FXXL"]) - Convert.ToInt32(rows[0]["FXXL"]);
                        drNew["DF3XL"] = Convert.ToInt32(Dr["F3XL"]) - Convert.ToInt32(rows[0]["F3XL"]);
                        drNew["DF4XL"] = Convert.ToInt32(Dr["F4XL"]) - Convert.ToInt32(rows[0]["F4XL"]);

                        drNew["DH30"] = Convert.ToInt32(Dr["H30"]) - Convert.ToInt32(rows[0]["H30"]);
                        drNew["DH32"] = Convert.ToInt32(Dr["H32"]) - Convert.ToInt32(rows[0]["H32"]);
                        drNew["DH34"] = Convert.ToInt32(Dr["H34"]) - Convert.ToInt32(rows[0]["H34"]);
                        drNew["DH36"] = Convert.ToInt32(Dr["H36"]) - Convert.ToInt32(rows[0]["H36"]);
                        drNew["DHXS"] = Convert.ToInt32(Dr["HXS"]) - Convert.ToInt32(rows[0]["HXS"]);
                        drNew["DHS"] = Convert.ToInt32(Dr["HS"]) - Convert.ToInt32(rows[0]["HS"]);
                        drNew["DHM"] = Convert.ToInt32(Dr["HM"]) - Convert.ToInt32(rows[0]["HM"]);
                        drNew["DHL"] = Convert.ToInt32(Dr["HL"]) - Convert.ToInt32(rows[0]["HL"]);
                        drNew["DHXL"] = Convert.ToInt32(Dr["HXL"]) - Convert.ToInt32(rows[0]["HXL"]);
                        drNew["DHXXl"] = Convert.ToInt32(Dr["HXXL"]) - Convert.ToInt32(rows[0]["HXXL"]);
                        drNew["DH3XL"] = Convert.ToInt32(Dr["H3XL"]) - Convert.ToInt32(rows[0]["H3XL"]);
                        drNew["DH4XL"] = Convert.ToInt32(Dr["H4XL"]) - Convert.ToInt32(rows[0]["H4XL"]);

                        #endregion

                        drNew["CFT"] = Convert.ToInt32(Dr["F30"]) + Convert.ToInt32(Dr["F32"]) + Convert.ToInt32(Dr["F34"]) + Convert.ToInt32(Dr["F36"]) + Convert.ToInt32(Dr["FXS"]) + Convert.ToInt32(Dr["FS"]) + Convert.ToInt32(Dr["FM"]) + Convert.ToInt32(Dr["FL"]) + Convert.ToInt32(Dr["FXL"]) + Convert.ToInt32(Dr["FXXL"]) + Convert.ToInt32(Dr["F3XL"]) + Convert.ToInt32(Dr["F4XL"]);
                        drNew["CHT"] = Convert.ToInt32(Dr["H30"]) + Convert.ToInt32(Dr["H32"]) + Convert.ToInt32(Dr["H34"]) + Convert.ToInt32(Dr["H36"]) + Convert.ToInt32(Dr["HXS"]) + Convert.ToInt32(Dr["HS"]) + Convert.ToInt32(Dr["HM"]) + Convert.ToInt32(Dr["HL"]) + Convert.ToInt32(Dr["HXL"]) + Convert.ToInt32(Dr["HXXL"]) + Convert.ToInt32(Dr["H3XL"]) + Convert.ToInt32(Dr["H4XL"]);

                        drNew["MFT"] = Convert.ToInt32(rows[0]["F30"]) + Convert.ToInt32(rows[0]["F32"]) + Convert.ToInt32(rows[0]["F34"]) + Convert.ToInt32(rows[0]["F36"]) + Convert.ToInt32(rows[0]["FXS"]) + Convert.ToInt32(rows[0]["FS"]) + Convert.ToInt32(rows[0]["FM"]) + Convert.ToInt32(rows[0]["FL"]) + Convert.ToInt32(rows[0]["FXL"]) + Convert.ToInt32(rows[0]["FXXL"]) + Convert.ToInt32(rows[0]["F3XL"]) + Convert.ToInt32(rows[0]["F4XL"]);
                        drNew["MHT"] = Convert.ToInt32(rows[0]["H30"]) + Convert.ToInt32(rows[0]["H32"]) + Convert.ToInt32(rows[0]["H34"]) + Convert.ToInt32(rows[0]["H36"]) + Convert.ToInt32(rows[0]["HXS"]) + Convert.ToInt32(rows[0]["HS"]) + Convert.ToInt32(rows[0]["HM"]) + Convert.ToInt32(rows[0]["HL"]) + Convert.ToInt32(rows[0]["HXL"]) + Convert.ToInt32(rows[0]["HXXL"]) + Convert.ToInt32(rows[0]["H3XL"]) + Convert.ToInt32(rows[0]["H4XL"]);

                        drNew["DFT"] = (Convert.ToInt32(Dr["F30"]) + Convert.ToInt32(Dr["F32"]) + Convert.ToInt32(Dr["F34"]) + Convert.ToInt32(Dr["F36"]) + Convert.ToInt32(Dr["FXS"]) + Convert.ToInt32(Dr["FS"]) + Convert.ToInt32(Dr["FM"]) + Convert.ToInt32(Dr["FL"]) + Convert.ToInt32(Dr["FXL"]) + Convert.ToInt32(Dr["FXXL"]) + Convert.ToInt32(Dr["F3XL"]) + Convert.ToInt32(Dr["F4XL"])) - (Convert.ToInt32(rows[0]["F30"]) + Convert.ToInt32(rows[0]["F32"]) + Convert.ToInt32(rows[0]["F34"]) + Convert.ToInt32(rows[0]["F36"]) + Convert.ToInt32(rows[0]["FXS"]) + Convert.ToInt32(rows[0]["FS"]) + Convert.ToInt32(rows[0]["FM"]) + Convert.ToInt32(rows[0]["FL"]) + Convert.ToInt32(rows[0]["FXL"]) + Convert.ToInt32(rows[0]["FXXL"]) + Convert.ToInt32(rows[0]["F3XL"]) + Convert.ToInt32(rows[0]["F4XL"]));
                        drNew["DHT"] = (Convert.ToInt32(Dr["H30"]) + Convert.ToInt32(Dr["H32"]) + Convert.ToInt32(Dr["H34"]) + Convert.ToInt32(Dr["H36"]) + Convert.ToInt32(Dr["HXS"]) + Convert.ToInt32(Dr["HS"]) + Convert.ToInt32(Dr["HM"]) + Convert.ToInt32(Dr["HL"]) + Convert.ToInt32(Dr["HXL"]) + Convert.ToInt32(Dr["HXXL"]) + Convert.ToInt32(Dr["H3XL"]) + Convert.ToInt32(Dr["H4XL"])) - (Convert.ToInt32(rows[0]["H30"]) + Convert.ToInt32(rows[0]["H32"]) + Convert.ToInt32(rows[0]["H34"]) + Convert.ToInt32(rows[0]["H36"]) + Convert.ToInt32(rows[0]["HXS"]) + Convert.ToInt32(rows[0]["HS"]) + Convert.ToInt32(rows[0]["HM"]) + Convert.ToInt32(rows[0]["HL"]) + Convert.ToInt32(rows[0]["HXL"]) + Convert.ToInt32(rows[0]["HXXL"]) + Convert.ToInt32(rows[0]["H3XL"]) + Convert.ToInt32(rows[0]["H4XL"]));

                        drNew["CFHT"] = Convert.ToInt32(Dr["F30"]) + Convert.ToInt32(Dr["F32"]) + Convert.ToInt32(Dr["F34"]) + Convert.ToInt32(Dr["F36"]) + Convert.ToInt32(Dr["FXS"]) + Convert.ToInt32(Dr["FS"]) + Convert.ToInt32(Dr["FM"]) + Convert.ToInt32(Dr["FL"]) + Convert.ToInt32(Dr["FXL"]) + Convert.ToInt32(Dr["FXXL"]) + Convert.ToInt32(Dr["F3XL"]) + Convert.ToInt32(Dr["F4XL"]) + Convert.ToInt32(Dr["H30"]) + Convert.ToInt32(Dr["H32"]) + Convert.ToInt32(Dr["H34"]) + Convert.ToInt32(Dr["H36"]) + Convert.ToInt32(Dr["HXS"]) + Convert.ToInt32(Dr["HS"]) + Convert.ToInt32(Dr["HM"]) + Convert.ToInt32(Dr["HL"]) + Convert.ToInt32(Dr["HXL"]) + Convert.ToInt32(Dr["HXXL"]) + Convert.ToInt32(Dr["H3XL"]) + Convert.ToInt32(Dr["H4XL"]);
                        drNew["MFHT"] = Convert.ToInt32(rows[0]["F30"]) + Convert.ToInt32(rows[0]["F32"]) + Convert.ToInt32(rows[0]["F34"]) + Convert.ToInt32(rows[0]["F36"]) + Convert.ToInt32(rows[0]["FXS"]) + Convert.ToInt32(rows[0]["FS"]) + Convert.ToInt32(rows[0]["FM"]) + Convert.ToInt32(rows[0]["FL"]) + Convert.ToInt32(rows[0]["FXL"]) + Convert.ToInt32(rows[0]["FXXL"]) + Convert.ToInt32(rows[0]["F3XL"]) + Convert.ToInt32(rows[0]["F4XL"]) + Convert.ToInt32(rows[0]["H30"]) + Convert.ToInt32(rows[0]["H32"]) + Convert.ToInt32(rows[0]["H34"]) + Convert.ToInt32(rows[0]["H36"]) + Convert.ToInt32(rows[0]["HXS"]) + Convert.ToInt32(rows[0]["HS"]) + Convert.ToInt32(rows[0]["HM"]) + Convert.ToInt32(rows[0]["HL"]) + Convert.ToInt32(rows[0]["HXL"]) + Convert.ToInt32(rows[0]["HXXL"]) + Convert.ToInt32(rows[0]["H3XL"]) + Convert.ToInt32(rows[0]["H4XL"]);

                        int CFHT = Convert.ToInt32(Dr["F30"]) + Convert.ToInt32(Dr["F32"]) + Convert.ToInt32(Dr["F34"]) + Convert.ToInt32(Dr["F36"]) + Convert.ToInt32(Dr["FXS"]) + Convert.ToInt32(Dr["FS"]) + Convert.ToInt32(Dr["FM"]) + Convert.ToInt32(Dr["FL"]) + Convert.ToInt32(Dr["FXL"]) + Convert.ToInt32(Dr["FXXL"]) + Convert.ToInt32(Dr["F3XL"]) + Convert.ToInt32(Dr["F4XL"]) + Convert.ToInt32(Dr["H30"]) + Convert.ToInt32(Dr["H32"]) + Convert.ToInt32(Dr["H34"]) + Convert.ToInt32(Dr["H36"]) + Convert.ToInt32(Dr["HXS"]) + Convert.ToInt32(Dr["HS"]) + Convert.ToInt32(Dr["HM"]) + Convert.ToInt32(Dr["HL"]) + Convert.ToInt32(Dr["HXL"]) + Convert.ToInt32(Dr["HXXL"]) + Convert.ToInt32(Dr["H3XL"]) + Convert.ToInt32(Dr["H4XL"]);
                        int MFHT = Convert.ToInt32(rows[0]["F30"]) + Convert.ToInt32(rows[0]["F32"]) + Convert.ToInt32(rows[0]["F34"]) + Convert.ToInt32(rows[0]["F36"]) + Convert.ToInt32(rows[0]["FXS"]) + Convert.ToInt32(rows[0]["FS"]) + Convert.ToInt32(rows[0]["FM"]) + Convert.ToInt32(rows[0]["FL"]) + Convert.ToInt32(rows[0]["FXL"]) + Convert.ToInt32(rows[0]["FXXL"]) + Convert.ToInt32(rows[0]["F3XL"]) + Convert.ToInt32(rows[0]["F4XL"]) + Convert.ToInt32(rows[0]["H30"]) + Convert.ToInt32(rows[0]["H32"]) + Convert.ToInt32(rows[0]["H34"]) + Convert.ToInt32(rows[0]["H36"]) + Convert.ToInt32(rows[0]["HXS"]) + Convert.ToInt32(rows[0]["HS"]) + Convert.ToInt32(rows[0]["HM"]) + Convert.ToInt32(rows[0]["HL"]) + Convert.ToInt32(rows[0]["HXL"]) + Convert.ToInt32(rows[0]["HXXL"]) + Convert.ToInt32(rows[0]["H3XL"]) + Convert.ToInt32(rows[0]["H4XL"]);

                        drNew["DFHT"] = CFHT - MFHT;

                        if (CFHT == MFHT)
                        {
                            drNew["Changes"] = "UnChanged";
                        }
                        else
                        {
                            drNew["Changes"] = "Changed";
                        }

                        #endregion
                    }
                    else
                    {
                        #region

                        drNew["Cutid"] = Dr["cutid"];
                        drNew["LotNo"] = Dr["CompanyFullLotNo"];
                        drNew["CutDate"] =Convert.ToDateTime(Dr["Deliverydate"]).ToString("dd/MM/yyyy");
                        drNew["Master"] = Dr["LedgerName"];
                        drNew["ItemCode"] = Dr["ItemCode"];
                        drNew["ItemName"] = Dr["ItemName"];
                        drNew["BrandName"] = Dr["BrandName"];
                        drNew["Fit"] = Dr["Fit"];
                        drNew["Width"] = Dr["Width"];
                        drNew["Designno"] = Dr["Designno"];

                        drNew["MasterDate"] = "-";

                        #region
                        drNew["CF30"] = Dr["F30"];
                        drNew["CF32"] = Dr["F32"];
                        drNew["CF34"] = Dr["F34"];
                        drNew["CF36"] = Dr["F36"];
                        drNew["CFXS"] = Dr["FXS"];
                        drNew["CFS"] = Dr["FS"];
                        drNew["CFM"] = Dr["FM"];
                        drNew["CFL"] = Dr["FL"];
                        drNew["CFXL"] = Dr["FXL"];
                        drNew["CFXXl"] = Dr["FXXl"];
                        drNew["CF3XL"] = Dr["F3XL"];
                        drNew["CF4XL"] = Dr["F4XL"];

                        drNew["CH30"] = Dr["H30"];
                        drNew["CH32"] = Dr["H32"];
                        drNew["CH34"] = Dr["H34"];
                        drNew["CH36"] = Dr["H36"];
                        drNew["CHXS"] = Dr["HXS"];
                        drNew["CHS"] = Dr["HS"];
                        drNew["CHM"] = Dr["HM"];
                        drNew["CHL"] = Dr["HL"];
                        drNew["CHXL"] = Dr["HXL"];
                        drNew["CHXXl"] = Dr["HXXl"];
                        drNew["CH3XL"] = Dr["H3XL"];
                        drNew["CH4XL"] = Dr["H4XL"];

                        #endregion

                        #region
                        drNew["MF30"] = "0";
                        drNew["MF32"] = "0";
                        drNew["MF34"] = "0";
                        drNew["MF36"] = "0";
                        drNew["MFXS"] = "0";
                        drNew["MFS"] = "0";
                        drNew["MFM"] = "0";
                        drNew["MFL"] = "0";
                        drNew["MFXL"] = "0";
                        drNew["MFXXL"] = "0";
                        drNew["MF3XL"] = "0";
                        drNew["MF4XL"] = "0";

                        drNew["MH30"] = "0";
                        drNew["MH32"] = "0";
                        drNew["MH34"] = "0";
                        drNew["MH36"] = "0";
                        drNew["MHXS"] = "0";
                        drNew["MHS"] = "0";
                        drNew["MHM"] = "0";
                        drNew["MHL"] = "0";
                        drNew["MHXL"] = "0";
                        drNew["MHXXl"] = "0";
                        drNew["MH3XL"] = "0";
                        drNew["MH4XL"] = "0";

                        #endregion

                        #region
                        drNew["DF30"] = "0";
                        drNew["DF32"] = "0";
                        drNew["DF34"] = "0";
                        drNew["DF36"] = "0";
                        drNew["DFXS"] = "0";
                        drNew["DFS"] = "0";
                        drNew["DFM"] = "0";
                        drNew["DFL"] = "0";
                        drNew["DFXL"] = "0";
                        drNew["DFXXL"] = "0";
                        drNew["DF3XL"] = "0";
                        drNew["DF4XL"] = "0";
                               
                        drNew["DH30"] = "0";
                        drNew["DH32"] = "0";
                        drNew["DH34"] = "0";
                        drNew["DH36"] = "0";
                        drNew["DHXS"] = "0";
                        drNew["DHS"] = "0";
                        drNew["DHM"] = "0";
                        drNew["DHL"] = "0";
                        drNew["DHXL"] = "0";
                        drNew["DHXXl"] = "0";
                        drNew["DH3XL"] = "0";
                        drNew["DH4XL"] = "0";

                        #endregion
                        drNew["CFT"] = Convert.ToInt32(Dr["F30"]) + Convert.ToInt32(Dr["F32"]) + Convert.ToInt32(Dr["F34"]) + Convert.ToInt32(Dr["F36"]) + Convert.ToInt32(Dr["FXS"]) + Convert.ToInt32(Dr["FS"]) + Convert.ToInt32(Dr["FM"]) + Convert.ToInt32(Dr["FL"]) + Convert.ToInt32(Dr["FXL"]) + Convert.ToInt32(Dr["FXXL"]) + Convert.ToInt32(Dr["F3XL"]) + Convert.ToInt32(Dr["F4XL"]);
                        drNew["CHT"] = Convert.ToInt32(Dr["H30"]) + Convert.ToInt32(Dr["H32"]) + Convert.ToInt32(Dr["H34"]) + Convert.ToInt32(Dr["H36"]) + Convert.ToInt32(Dr["HXS"]) + Convert.ToInt32(Dr["HS"]) + Convert.ToInt32(Dr["HM"]) + Convert.ToInt32(Dr["HL"]) + Convert.ToInt32(Dr["HXL"]) + Convert.ToInt32(Dr["HXXL"]) + Convert.ToInt32(Dr["H3XL"]) + Convert.ToInt32(Dr["H4XL"]);
                        drNew["MFT"] = "0";
                        drNew["MHT"] = "0";

                        drNew["CFHT"] = Convert.ToInt32(Dr["F30"]) + Convert.ToInt32(Dr["F32"]) + Convert.ToInt32(Dr["F34"]) + Convert.ToInt32(Dr["F36"]) + Convert.ToInt32(Dr["FXS"]) + Convert.ToInt32(Dr["FS"]) + Convert.ToInt32(Dr["FM"]) + Convert.ToInt32(Dr["FL"]) + Convert.ToInt32(Dr["FXL"]) + Convert.ToInt32(Dr["FXXL"]) + Convert.ToInt32(Dr["F3XL"]) + Convert.ToInt32(Dr["F4XL"]) + Convert.ToInt32(Dr["H30"]) + Convert.ToInt32(Dr["H32"]) + Convert.ToInt32(Dr["H34"]) + Convert.ToInt32(Dr["H36"]) + Convert.ToInt32(Dr["HXS"]) + Convert.ToInt32(Dr["HS"]) + Convert.ToInt32(Dr["HM"]) + Convert.ToInt32(Dr["HL"]) + Convert.ToInt32(Dr["HXL"]) + Convert.ToInt32(Dr["HXXL"]) + Convert.ToInt32(Dr["H3XL"]) + Convert.ToInt32(Dr["H4XL"]);
                        drNew["MFHT"] = "0";

                        drNew["Changes"] = "-----";

                        drNew["DFT"] = "0";
                        drNew["DHT"] = "0";

                        drNew["DFHT"] = "0";

                        #endregion
                    }

                    dstd.Tables[0].Rows.Add(drNew);
                }



                #endregion
            }

            if (dstd.Tables.Count > 0)
            {
                if (dstd.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = dstd;
                    gvcust.DataBind();

                    #region

                    gvcust.Columns[12].Visible = false;
                    gvcust.Columns[13].Visible = false;
                    gvcust.Columns[14].Visible = false;
                    gvcust.Columns[15].Visible = false;
                    gvcust.Columns[16].Visible = false;
                    gvcust.Columns[17].Visible = false;
                    gvcust.Columns[18].Visible = false;
                    gvcust.Columns[19].Visible = false;
                    gvcust.Columns[20].Visible = false;
                    gvcust.Columns[21].Visible = false;
                    gvcust.Columns[22].Visible = false;
                    gvcust.Columns[23].Visible = false;
                    gvcust.Columns[24].Visible = false;
                    gvcust.Columns[25].Visible = false;
                    gvcust.Columns[26].Visible = false;
                    gvcust.Columns[27].Visible = false;
                    gvcust.Columns[28].Visible = false;
                    gvcust.Columns[29].Visible = false;
                    gvcust.Columns[30].Visible = false;
                    gvcust.Columns[31].Visible = false;
                    gvcust.Columns[32].Visible = false;
                    gvcust.Columns[33].Visible = false;
                    gvcust.Columns[34].Visible = false;
                    gvcust.Columns[35].Visible = false;
                    gvcust.Columns[36].Visible = false;
                    gvcust.Columns[37].Visible = false;
                    gvcust.Columns[38].Visible = false;
                    gvcust.Columns[39].Visible = false;
                    gvcust.Columns[40].Visible = false;
                    gvcust.Columns[41].Visible = false;
                    gvcust.Columns[42].Visible = false;
                    gvcust.Columns[43].Visible = false;
                    gvcust.Columns[44].Visible = false;
                    gvcust.Columns[45].Visible = false;
                    gvcust.Columns[46].Visible = false;
                    gvcust.Columns[47].Visible = false;
                    gvcust.Columns[48].Visible = false;
                    gvcust.Columns[49].Visible = false;
                    gvcust.Columns[50].Visible = false;
                    gvcust.Columns[51].Visible = false;
                    gvcust.Columns[52].Visible = false;
                    gvcust.Columns[53].Visible = false;
                    gvcust.Columns[54].Visible = false;
                    gvcust.Columns[55].Visible = false;
                    gvcust.Columns[56].Visible = false;
                    gvcust.Columns[57].Visible = false;
                    gvcust.Columns[58].Visible = false;
                    gvcust.Columns[59].Visible = false;
                    gvcust.Columns[60].Visible = false;
                    gvcust.Columns[61].Visible = false;
                    gvcust.Columns[62].Visible = false;
                    gvcust.Columns[63].Visible = false;
                    gvcust.Columns[64].Visible = false;
                    gvcust.Columns[65].Visible = false;
                    gvcust.Columns[66].Visible = false;
                    gvcust.Columns[67].Visible = false;
                    gvcust.Columns[68].Visible = false;
                    gvcust.Columns[69].Visible = false;
                    gvcust.Columns[70].Visible = false;
                    gvcust.Columns[71].Visible = false;
                    gvcust.Columns[72].Visible = false;
                    gvcust.Columns[73].Visible = false;
                    gvcust.Columns[74].Visible = false;
                    gvcust.Columns[75].Visible = false;
                    gvcust.Columns[76].Visible = false;
                    gvcust.Columns[77].Visible = false;
                    gvcust.Columns[78].Visible = false;
                    gvcust.Columns[79].Visible = false;
                    gvcust.Columns[80].Visible = false;
                    gvcust.Columns[81].Visible = false;
                    gvcust.Columns[82].Visible = false;
                    gvcust.Columns[83].Visible = false;

                    gvcust.Columns[84].Visible = false;
                    gvcust.Columns[85].Visible = false;
                    gvcust.Columns[86].Visible = false;

                    gvcust.Columns[87].Visible = false;
                    gvcust.Columns[88].Visible = false;
                    gvcust.Columns[89].Visible = false;

                    gvcust.Columns[90].Visible = false;
                    gvcust.Columns[91].Visible = false;
                    gvcust.Columns[92].Visible = false;

                    #endregion


                    for (int j = 0; j < dstd.Tables[0].Rows.Count; j++)
                    {
                        #region
                        string CFS30 = dstd.Tables[0].Rows[j]["CF30"].ToString();
                        string CFS32 = dstd.Tables[0].Rows[j]["CF32"].ToString();
                        string CFS34 = dstd.Tables[0].Rows[j]["CF34"].ToString();
                        string CFS36 = dstd.Tables[0].Rows[j]["CF36"].ToString();
                        string CFSXS = dstd.Tables[0].Rows[j]["CFxs"].ToString();
                        string CFSS = dstd.Tables[0].Rows[j]["CFs"].ToString();
                        string CFSM = dstd.Tables[0].Rows[j]["CFm"].ToString();
                        string CFSL = dstd.Tables[0].Rows[j]["CFl"].ToString();
                        string CFSXL = dstd.Tables[0].Rows[j]["CFxl"].ToString();
                        string CFSXXL = dstd.Tables[0].Rows[j]["CFxxl"].ToString();
                        string CFS3XL = dstd.Tables[0].Rows[j]["CF3xl"].ToString();
                        string CFS4XL = dstd.Tables[0].Rows[j]["CF4xl"].ToString();
                        string CHS30 = dstd.Tables[0].Rows[j]["CH30"].ToString();
                        string CHS32 = dstd.Tables[0].Rows[j]["CH32"].ToString();
                        string CHS34 = dstd.Tables[0].Rows[j]["CH34"].ToString();
                        string CHS36 = dstd.Tables[0].Rows[j]["CH36"].ToString();
                        string CHSXS = dstd.Tables[0].Rows[j]["CHxs"].ToString();
                        string CHSS = dstd.Tables[0].Rows[j]["CHs"].ToString();
                        string CHSM = dstd.Tables[0].Rows[j]["CHm"].ToString();
                        string CHSL = dstd.Tables[0].Rows[j]["CHl"].ToString();
                        string CHSXL = dstd.Tables[0].Rows[j]["CHxl"].ToString();
                        string CHSXXL = dstd.Tables[0].Rows[j]["CHxxl"].ToString();
                        string CHS3XL = dstd.Tables[0].Rows[j]["CH3xl"].ToString();
                        string CHS4XL = dstd.Tables[0].Rows[j]["CH4xl"].ToString();

                        string MFS30 = dstd.Tables[0].Rows[j]["MF30"].ToString();
                        string MFS32 = dstd.Tables[0].Rows[j]["MF32"].ToString();
                        string MFS34 = dstd.Tables[0].Rows[j]["MF34"].ToString();
                        string MFS36 = dstd.Tables[0].Rows[j]["MF36"].ToString();
                        string MFSXS = dstd.Tables[0].Rows[j]["MFxs"].ToString();
                        string MFSS = dstd.Tables[0].Rows[j]["MFs"].ToString();
                        string MFSM = dstd.Tables[0].Rows[j]["MFm"].ToString();
                        string MFSL = dstd.Tables[0].Rows[j]["MFl"].ToString();
                        string MFSXL = dstd.Tables[0].Rows[j]["MFxl"].ToString();
                        string MFSXXL = dstd.Tables[0].Rows[j]["MFxxl"].ToString();
                        string MFS3XL = dstd.Tables[0].Rows[j]["MF3xl"].ToString();
                        string MFS4XL = dstd.Tables[0].Rows[j]["MF4xl"].ToString();
                        string MHS30 = dstd.Tables[0].Rows[j]["MH30"].ToString();
                        string MHS32 = dstd.Tables[0].Rows[j]["MH32"].ToString();
                        string MHS34 = dstd.Tables[0].Rows[j]["MH34"].ToString();
                        string MHS36 = dstd.Tables[0].Rows[j]["MH36"].ToString();
                        string MHSXS = dstd.Tables[0].Rows[j]["MHxs"].ToString();
                        string MHSS = dstd.Tables[0].Rows[j]["MHs"].ToString();
                        string MHSM = dstd.Tables[0].Rows[j]["MHm"].ToString();
                        string MHSL = dstd.Tables[0].Rows[j]["MHl"].ToString();
                        string MHSXL = dstd.Tables[0].Rows[j]["MHxl"].ToString();
                        string MHSXXL = dstd.Tables[0].Rows[j]["MHxxl"].ToString();
                        string MHS3XL = dstd.Tables[0].Rows[j]["MH3xl"].ToString();
                        string MHS4XL = dstd.Tables[0].Rows[j]["MH4xl"].ToString();

                        string CFT = dstd.Tables[0].Rows[j]["CFT"].ToString();
                        string MFT = dstd.Tables[0].Rows[j]["MFT"].ToString();

                        string CHT = dstd.Tables[0].Rows[j]["CHT"].ToString();
                        string MHT = dstd.Tables[0].Rows[j]["MHT"].ToString();

                        string CFHT = dstd.Tables[0].Rows[j]["CFHT"].ToString();
                        string MFHT = dstd.Tables[0].Rows[j]["MFHT"].ToString();

                        #endregion

                        #region

                        if (CFS30 != "0")
                        {
                            gvcust.Columns[12].Visible = true;
                        }
                        if (MFS30 != "0")
                        {
                            gvcust.Columns[13].Visible = true;
                            gvcust.Columns[14].Visible = true;
                        }
                        if (CFS32 != "0")
                        {
                            gvcust.Columns[15].Visible = true;
                        }
                        if (MFS32 != "0")
                        {
                            gvcust.Columns[16].Visible = true;
                            gvcust.Columns[17].Visible = true;
                        }
                        if (CFS34 != "0")
                        {
                            gvcust.Columns[18].Visible = true;
                        }
                        if (MFS34 != "0")
                        {
                            gvcust.Columns[19].Visible = true;
                            gvcust.Columns[20].Visible = true;
                        }
                        if (CFS36 != "0")
                        {
                            gvcust.Columns[21].Visible = true;
                        }
                        if (MFS36 != "0")
                        {
                            gvcust.Columns[22].Visible = true;
                            gvcust.Columns[23].Visible = true;
                        }
                        if (CFSXS != "0")
                        {
                            gvcust.Columns[24].Visible = true;
                        }
                        if (MFSXS != "0")
                        {
                            gvcust.Columns[25].Visible = true;
                            gvcust.Columns[26].Visible = true;
                        }
                        if (CFSS != "0")
                        {
                            gvcust.Columns[27].Visible = true;
                        }
                        if (MFSS != "0")
                        {
                            gvcust.Columns[28].Visible = true;
                            gvcust.Columns[29].Visible = true;
                        }
                        if (CFSM != "0")
                        {
                            gvcust.Columns[30].Visible = true;
                        }
                        if (MFSM != "0")
                        {
                            gvcust.Columns[31].Visible = true;
                            gvcust.Columns[32].Visible = true;
                        }
                        if (CFSL != "0")
                        {
                            gvcust.Columns[33].Visible = true;
                        }
                        if (MFSL != "0")
                        {
                            gvcust.Columns[34].Visible = true;
                            gvcust.Columns[35].Visible = true;
                        }
                        if (CFSXL != "0")
                        {
                            gvcust.Columns[36].Visible = true;
                        }
                        if (MFSXL != "0")
                        {
                            gvcust.Columns[37].Visible = true;
                            gvcust.Columns[38].Visible = true;
                        }
                        if (CFSXXL != "0")
                        {
                            gvcust.Columns[39].Visible = true;
                        }
                        if (MFSXXL != "0")
                        {
                            gvcust.Columns[40].Visible = true;
                            gvcust.Columns[41].Visible = true;
                        }
                        if (CFS3XL != "0")
                        {
                            gvcust.Columns[42].Visible = true;
                        }
                        if (MFS3XL != "0")
                        {
                            gvcust.Columns[43].Visible = true;
                            gvcust.Columns[44].Visible = true;
                        }
                        if (CFS4XL != "0")
                        {
                            gvcust.Columns[45].Visible = true;
                        }
                        if (MFS4XL != "0")
                        {
                            gvcust.Columns[46].Visible = true;
                            gvcust.Columns[47].Visible = true;
                        }
                        if (CHS30 != "0")
                        {
                            gvcust.Columns[48].Visible = true;
                        }
                        if (MHS30 != "0")
                        {
                            gvcust.Columns[49].Visible = true;
                            gvcust.Columns[50].Visible = true;
                        }
                        if (CHS32 != "0")
                        {
                            gvcust.Columns[51].Visible = true;
                        }
                        if (MHS32 != "0")
                        {
                            gvcust.Columns[52].Visible = true;
                            gvcust.Columns[53].Visible = true;
                        }
                        if (CHS34 != "0")
                        {
                            gvcust.Columns[54].Visible = true;
                        }
                        if (MHS34 != "0")
                        {
                            gvcust.Columns[55].Visible = true;
                            gvcust.Columns[56].Visible = true;
                        }
                        if (CHS36 != "0")
                        {
                            gvcust.Columns[57].Visible = true;
                        }
                        if (MHS36 != "0")
                        {
                            gvcust.Columns[58].Visible = true;
                            gvcust.Columns[59].Visible = true;
                        }
                        if (CHSXS != "0")
                        {
                            gvcust.Columns[60].Visible = true;
                        }
                        if (MHSXS != "0")
                        {
                            gvcust.Columns[61].Visible = true;
                            gvcust.Columns[62].Visible = true;
                        }
                        if (CHSS != "0")
                        {
                            gvcust.Columns[63].Visible = true;
                        }
                        if (MHSS != "0")
                        {
                            gvcust.Columns[64].Visible = true;
                            gvcust.Columns[65].Visible = true;
                        }
                        if (CHSM != "0")
                        {
                            gvcust.Columns[66].Visible = true;
                        }
                        if (MHSM != "0")
                        {
                            gvcust.Columns[67].Visible = true;
                            gvcust.Columns[68].Visible = true;
                        }
                        if (CHSL != "0")
                        {
                            gvcust.Columns[69].Visible = true;
                        }
                        if (MHSL != "0")
                        {
                            gvcust.Columns[70].Visible = true;
                            gvcust.Columns[71].Visible = true;
                        }
                        if (CHSXL != "0")
                        {
                            gvcust.Columns[72].Visible = true;
                        }
                        if (MHSXL != "0")
                        {
                            gvcust.Columns[73].Visible = true;
                            gvcust.Columns[74].Visible = true;
                        }
                        if (CHSXXL != "0")
                        {
                            gvcust.Columns[75].Visible = true;
                        }
                        if (MHSXXL != "0")
                        {
                            gvcust.Columns[76].Visible = true;
                            gvcust.Columns[77].Visible = true;
                        }
                        if (CHS3XL != "0")
                        {
                            gvcust.Columns[78].Visible = true;
                        }
                        if (MHS3XL != "0")
                        {
                            gvcust.Columns[79].Visible = true;
                            gvcust.Columns[80].Visible = true;
                        }
                        if (CHS4XL != "0")
                        {
                            gvcust.Columns[81].Visible = true;
                        }
                        if (MHS4XL != "0")
                        {
                            gvcust.Columns[82].Visible = true;
                            gvcust.Columns[83].Visible = true;
                        }
                        if (CFT != "0")
                        {
                            gvcust.Columns[84].Visible = true;
                        }
                        if (MFT != "0")
                        {
                            gvcust.Columns[85].Visible = true;
                            gvcust.Columns[86].Visible = true;
                        }
                        if (CHT != "0")
                        {
                            gvcust.Columns[87].Visible = true;

                        }
                        if (MHT != "0")
                        {
                            gvcust.Columns[88].Visible = true;
                            gvcust.Columns[89].Visible = true;
                        }
                        if (CFHT != "0")
                        {
                            gvcust.Columns[90].Visible = true;

                        }
                        if (MFHT != "0")
                        {
                            gvcust.Columns[91].Visible = true;
                            gvcust.Columns[92].Visible = true;
                        }

                        #endregion

                    }
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region


                if (e.Row.Cells[7].Text != "-" && e.Row.Cells[7].Text != "")
                {
                    F30 = F30 + Convert.ToDouble(e.Row.Cells[7].Text);
                }
                if (e.Row.Cells[8].Text != "-" && e.Row.Cells[8].Text != "")
                {
                    F32 = F32 + Convert.ToDouble(e.Row.Cells[8].Text);
                }
                if (e.Row.Cells[9].Text != "-" && e.Row.Cells[9].Text != "")
                {
                    F34 = F34 + Convert.ToDouble(e.Row.Cells[9].Text);
                }
                if (e.Row.Cells[10].Text != "-" && e.Row.Cells[10].Text != "")
                {
                    F36 = F36 + Convert.ToDouble(e.Row.Cells[10].Text);
                }
                if (e.Row.Cells[11].Text != "-" && e.Row.Cells[11].Text != "")
                {
                    FXS = FXS + Convert.ToDouble(e.Row.Cells[11].Text);
                }
                if (e.Row.Cells[12].Text != "-" && e.Row.Cells[12].Text != "")
                {
                    FS = FS + Convert.ToDouble(e.Row.Cells[12].Text);
                }
                if (e.Row.Cells[13].Text != "-" && e.Row.Cells[13].Text != "")
                {
                    FM = FM + Convert.ToDouble(e.Row.Cells[13].Text);
                }
                if (e.Row.Cells[14].Text != "-" && e.Row.Cells[14].Text != "")
                {
                    FL = FL + Convert.ToDouble(e.Row.Cells[14].Text);
                }
                if (e.Row.Cells[15].Text != "-" && e.Row.Cells[15].Text != "")
                {
                    FXL = FXL + Convert.ToDouble(e.Row.Cells[15].Text);
                }
                if (e.Row.Cells[16].Text != "-" && e.Row.Cells[16].Text != "")
                {
                    FXXL = FXXL + Convert.ToDouble(e.Row.Cells[16].Text);
                }
                if (e.Row.Cells[17].Text != "-" && e.Row.Cells[17].Text != "")
                {

                    F3XL = F3XL + Convert.ToDouble(e.Row.Cells[17].Text);
                }
                if (e.Row.Cells[18].Text != "-" && e.Row.Cells[18].Text != "")
                {
                    F4XL = F4XL + Convert.ToDouble(e.Row.Cells[18].Text);
                }

                if (e.Row.Cells[19].Text != "-" && e.Row.Cells[19].Text != "")
                {
                    H30 = H30 + Convert.ToDouble(e.Row.Cells[19].Text);
                }
                if (e.Row.Cells[20].Text != "-" && e.Row.Cells[20].Text != "")
                {
                    H32 = H32 + Convert.ToDouble(e.Row.Cells[20].Text);
                }
                if (e.Row.Cells[21].Text != "-" && e.Row.Cells[21].Text != "")
                {
                    H34 = H34 + Convert.ToDouble(e.Row.Cells[21].Text);
                }
                if (e.Row.Cells[22].Text != "-" && e.Row.Cells[22].Text != "")
                {
                    H36 = H36 + Convert.ToDouble(e.Row.Cells[22].Text);
                }
                if (e.Row.Cells[23].Text != "-" && e.Row.Cells[23].Text != "")
                {
                    HXS = HXS + Convert.ToDouble(e.Row.Cells[23].Text);
                }
                if (e.Row.Cells[24].Text != "-" && e.Row.Cells[24].Text != "")
                {
                    HS = HS + Convert.ToDouble(e.Row.Cells[24].Text);
                }
                if (e.Row.Cells[25].Text != "-" && e.Row.Cells[25].Text != "")
                {
                    HM = HM + Convert.ToDouble(e.Row.Cells[25].Text);
                }
                if (e.Row.Cells[26].Text != "-" && e.Row.Cells[26].Text != "")
                {
                    HL = HL + Convert.ToDouble(e.Row.Cells[26].Text);
                }
                if (e.Row.Cells[27].Text != "-" && e.Row.Cells[27].Text != "")
                {
                    HXL = HXL + Convert.ToDouble(e.Row.Cells[27].Text);
                }

                if (e.Row.Cells[28].Text != "-" && e.Row.Cells[28].Text != "")
                {
                    HXXL = HXXL + Convert.ToDouble(e.Row.Cells[28].Text);
                }
                if (e.Row.Cells[29].Text != "-" && e.Row.Cells[29].Text != "")
                {
                    H3XL = H3XL + Convert.ToDouble(e.Row.Cells[29].Text);
                }
                if (e.Row.Cells[30].Text != "-" && e.Row.Cells[30].Text != "")
                {
                    H4XL = H4XL + Convert.ToDouble(e.Row.Cells[30].Text);
                }
                if (e.Row.Cells[31].Text != "-" && e.Row.Cells[31].Text != "")
                {
                    TOTAL = TOTAL + Convert.ToDouble(e.Row.Cells[31].Text);
                }
                #endregion

                #region

                if (e.Row.Cells[7].Text == "0")
                {
                    e.Row.Cells[7].Text = "-";
                }
                if (e.Row.Cells[8].Text == "0")
                {
                    e.Row.Cells[8].Text = "-";
                }
                if (e.Row.Cells[9].Text == "0")
                {
                    e.Row.Cells[9].Text = "-";
                }
                if (e.Row.Cells[10].Text == "0")
                {
                    e.Row.Cells[10].Text = "-";
                }
                if (e.Row.Cells[11].Text == "0")
                {
                    e.Row.Cells[11].Text = "-";
                }
                if (e.Row.Cells[12].Text == "0")
                {
                    e.Row.Cells[12].Text = "-";
                }
                if (e.Row.Cells[13].Text == "0")
                {
                    e.Row.Cells[13].Text = "-";
                }
                if (e.Row.Cells[14].Text == "0")
                {
                    e.Row.Cells[14].Text = "-";
                }

                if (e.Row.Cells[15].Text == "0")
                {
                    e.Row.Cells[15].Text = "-";
                }
                if (e.Row.Cells[16].Text == "0")
                {
                    e.Row.Cells[16].Text = "-";
                }
                if (e.Row.Cells[17].Text == "0")
                {
                    e.Row.Cells[17].Text = "-";
                }
                if (e.Row.Cells[18].Text == "0")
                {
                    e.Row.Cells[18].Text = "-";
                }
                if (e.Row.Cells[19].Text == "0")
                {
                    e.Row.Cells[19].Text = "-";
                }
                if (e.Row.Cells[20].Text == "0")
                {
                    e.Row.Cells[20].Text = "-";
                }
                if (e.Row.Cells[21].Text == "0")
                {
                    e.Row.Cells[21].Text = "-";
                }
                if (e.Row.Cells[22].Text == "0")
                {
                    e.Row.Cells[22].Text = "-";
                }
                if (e.Row.Cells[23].Text == "0")
                {
                    e.Row.Cells[23].Text = "-";
                }
                if (e.Row.Cells[24].Text == "0")
                {
                    e.Row.Cells[24].Text = "-";
                }
                if (e.Row.Cells[25].Text == "0")
                {
                    e.Row.Cells[25].Text = "-";
                }
                if (e.Row.Cells[26].Text == "0")
                {
                    e.Row.Cells[26].Text = "-";
                }
                if (e.Row.Cells[27].Text == "0")
                {
                    e.Row.Cells[27].Text = "-";
                }
                if (e.Row.Cells[28].Text == "0")
                {
                    e.Row.Cells[28].Text = "-";
                }

                if (e.Row.Cells[29].Text == "0")
                {
                    e.Row.Cells[29].Text = "-";
                }
                if (e.Row.Cells[30].Text == "0")
                {
                    e.Row.Cells[30].Text = "-";
                }
                #endregion

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region
                e.Row.Cells[7].Text = F30.ToString();
                e.Row.Cells[8].Text = F32.ToString();
                e.Row.Cells[9].Text = F34.ToString();
                e.Row.Cells[10].Text = F36.ToString();
                e.Row.Cells[11].Text = FXS.ToString();
                e.Row.Cells[12].Text = FS.ToString();
                e.Row.Cells[13].Text = FM.ToString();
                e.Row.Cells[14].Text = FL.ToString();
                e.Row.Cells[15].Text = FXL.ToString();
                e.Row.Cells[16].Text = FXXL.ToString();
                e.Row.Cells[17].Text = F3XL.ToString();
                e.Row.Cells[18].Text = F4XL.ToString();

                e.Row.Cells[19].Text = H30.ToString();
                e.Row.Cells[20].Text = H32.ToString();
                e.Row.Cells[21].Text = H34.ToString();
                e.Row.Cells[22].Text = H36.ToString();
                e.Row.Cells[23].Text = HXS.ToString();
                e.Row.Cells[24].Text = HS.ToString();
                e.Row.Cells[25].Text = HM.ToString();
                e.Row.Cells[26].Text = HL.ToString();
                e.Row.Cells[27].Text = HXL.ToString();
                e.Row.Cells[28].Text = HXXL.ToString();
                e.Row.Cells[29].Text = H3XL.ToString();
                e.Row.Cells[30].Text = H4XL.ToString();

                e.Row.Cells[31].Text = TOTAL.ToString();
                //e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;

                //e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[23].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[24].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[25].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[26].HorizontalAlign = HorizontalAlign.Right;


                //e.Row.Cells[3].Font.Bold = true;
                //e.Row.Cells[4].Font.Bold = true;
                //e.Row.Cells[5].Font.Bold = true;
                //e.Row.Cells[6].Font.Bold = true;
                //e.Row.Cells[7].Font.Bold = true;
                //e.Row.Cells[8].Font.Bold = true;
                //e.Row.Cells[9].Font.Bold = true;
                //e.Row.Cells[10].Font.Bold = true;
                //e.Row.Cells[11].Font.Bold = true;
                //e.Row.Cells[12].Font.Bold = true;
                //e.Row.Cells[13].Font.Bold = true;
                //e.Row.Cells[14].Font.Bold = true;

                //e.Row.Cells[15].Font.Bold = true;
                //e.Row.Cells[16].Font.Bold = true;
                //e.Row.Cells[17].Font.Bold = true;
                //e.Row.Cells[18].Font.Bold = true;
                //e.Row.Cells[19].Font.Bold = true;
                //e.Row.Cells[20].Font.Bold = true;
                //e.Row.Cells[21].Font.Bold = true;
                //e.Row.Cells[22].Font.Bold = true;
                //e.Row.Cells[23].Font.Bold = true;
                //e.Row.Cells[24].Font.Bold = true;
                //e.Row.Cells[25].Font.Bold = true;
                //e.Row.Cells[26].Font.Bold = true;

                //e.Row.Cells[4].Text = "TOTAL :";
                //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[4].Font.Bold = true;

                //e.Row.Cells[33].Text = TOTAL.ToString();
                //e.Row.Cells[33].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[33].Font.Bold = true;


                #endregion
            }
        }

        protected void btnexcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= FabricCuttingDetails.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div2.RenderControl(htmlWrite);
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