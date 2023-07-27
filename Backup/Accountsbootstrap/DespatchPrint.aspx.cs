using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;
using System.IO;
using System.Text;


namespace Billing.Accountsbootstrap
{
    public partial class DespatchPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string CmpyId = "";
        double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string DSPid = Request.QueryString.Get("DSPid");
            string DSPRetid = Request.QueryString.Get("DSPRetid");

            sTableName = Session["User"].ToString();
            CmpyId = Session["CmpyId"].ToString();


            if (DSPid != null)
            {
                #region


                DataSet ds2 = objBs.GETPRINTOFDESPATCH(Convert.ToInt32(DSPid));
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    #region

                    //   imgchkmark.Src = "~/images/jplogo 1.png";


                    lbllcustname.Text = ds2.Tables[0].Rows[0]["Customername"].ToString();
                    lblgastin.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString();
                    lblladdress.Text = ds2.Tables[0].Rows[0]["Address"].ToString();
                    lbllmobile.Text = ds2.Tables[0].Rows[0]["MobileNo"].ToString();
                    lbllphone.Text = ds2.Tables[0].Rows[0]["PhoneNo"].ToString();
                    lbllemail.Text = ds2.Tables[0].Rows[0]["Email"].ToString();

                    lblldcno.Text = ds2.Tables[0].Rows[0]["DcNo"].ToString();
                    lblldcdate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["DCDate"]).ToString("dd/MM/yyyy");
                    lblltotalqty.Text = ds2.Tables[0].Rows[0]["TotalQty"].ToString();
                    lbllnarration.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                    lblldespatch.Text = ds2.Tables[0].Rows[0]["ledgername"].ToString();

                    CmpyId = ds2.Tables[0].Rows[0]["Companyid"].ToString();

                    //////lblLot.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    //////lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    //////lblLedgerName.Text = ds2.Tables[0].Rows[0]["name"].ToString();
                    //////lblPaidAmount.Text = ds2.Tables[0].Rows[0]["PaidAmount"].ToString();
                    //////lblTotalAmount.Text = ds2.Tables[0].Rows[0]["TotalAmount"].ToString();
                    //////lblTotalQty.Text = ds2.Tables[0].Rows[0]["Totalqty"].ToString();


                    if (CmpyId == "3")
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            #region

                            GridView1.DataSource = ds2;
                            GridView1.DataBind();

                            GridView1.Columns[0].Visible = true;
                            GridView1.Columns[1].Visible = true;
                            GridView1.Columns[2].Visible = false;
                            GridView1.Columns[3].Visible = true;
                            GridView1.Columns[4].Visible = true;

                            GridView1.Columns[5].Visible = false;

                            GridView1.Columns[6].Visible = false;
                            GridView1.Columns[7].Visible = false;
                            GridView1.Columns[8].Visible = false;
                            GridView1.Columns[9].Visible = false;
                            GridView1.Columns[10].Visible = false;
                            GridView1.Columns[11].Visible = false;
                            GridView1.Columns[12].Visible = false;
                            GridView1.Columns[13].Visible = false;
                            GridView1.Columns[14].Visible = false;
                            GridView1.Columns[15].Visible = false;

                            GridView1.Columns[16].Visible = false;

                            GridView1.Columns[17].Visible = false;
                            GridView1.Columns[18].Visible = false;

                            GridView1.Columns[19].Visible = false;

                            GridView1.Columns[20].Visible = false;
                            GridView1.Columns[21].Visible = false;
                            GridView1.Columns[22].Visible = false;
                            GridView1.Columns[23].Visible = false;
                            GridView1.Columns[24].Visible = false;
                            GridView1.Columns[25].Visible = false;
                            GridView1.Columns[26].Visible = false;
                            GridView1.Columns[27].Visible = false;
                            GridView1.Columns[28].Visible = false;
                            GridView1.Columns[29].Visible = false;

                            GridView1.Columns[30].Visible = false;

                            GridView1.Columns[31].Visible = false;
                            GridView1.Columns[32].Visible = false;

                            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                            {

                                #region
                                string S30 = ds2.Tables[0].Rows[j]["s30"].ToString();
                                string S32 = ds2.Tables[0].Rows[j]["s32"].ToString();
                                string S34 = ds2.Tables[0].Rows[j]["s34"].ToString();
                                string S36 = ds2.Tables[0].Rows[j]["s36"].ToString();
                                string SXS = ds2.Tables[0].Rows[j]["xs"].ToString();
                                string SS = ds2.Tables[0].Rows[j]["s"].ToString();
                                string SM = ds2.Tables[0].Rows[j]["m"].ToString();
                                string SL = ds2.Tables[0].Rows[j]["l"].ToString();
                                string SXL = ds2.Tables[0].Rows[j]["xl"].ToString();
                                string SXXL = ds2.Tables[0].Rows[j]["xxl"].ToString();
                                string S3XL = ds2.Tables[0].Rows[j]["s3xl"].ToString();
                                string S4XL = ds2.Tables[0].Rows[j]["s4xl"].ToString();


                                string HS30 = ds2.Tables[0].Rows[j]["Hs30"].ToString();
                                string HS32 = ds2.Tables[0].Rows[j]["Hs32"].ToString();
                                string HS34 = ds2.Tables[0].Rows[j]["Hs34"].ToString();
                                string HS36 = ds2.Tables[0].Rows[j]["Hs36"].ToString();
                                string HSXS = ds2.Tables[0].Rows[j]["Hxs"].ToString();
                                string HSS = ds2.Tables[0].Rows[j]["Hs"].ToString();
                                string HSM = ds2.Tables[0].Rows[j]["Hm"].ToString();
                                string HSL = ds2.Tables[0].Rows[j]["Hl"].ToString();
                                string HSXL = ds2.Tables[0].Rows[j]["Hxl"].ToString();
                                string HSXXL = ds2.Tables[0].Rows[j]["Hxxl"].ToString();
                                string HS3XL = ds2.Tables[0].Rows[j]["Hs3xl"].ToString();
                                string HS4XL = ds2.Tables[0].Rows[j]["Hs4xl"].ToString();

                                int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                                //////grndtot = grndtot + tot;
                                //////lblstockgrandtot.Text = grndtot.ToString();

                                if (S30 != "0")
                                {

                                    GridView1.Columns[6].Visible = true;
                                }
                                if (S32 != "0")
                                {

                                    GridView1.Columns[7].Visible = true;
                                }

                                if (S34 != "0")
                                {

                                    GridView1.Columns[8].Visible = true;
                                }

                                if (S36 != "0")
                                {

                                    GridView1.Columns[9].Visible = true;
                                }

                                if (SXS != "0")
                                {

                                    GridView1.Columns[10].Visible = true;
                                }

                                if (SS != "0")
                                {

                                    GridView1.Columns[11].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SM != "0")
                                {

                                    GridView1.Columns[12].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SL != "0")
                                {

                                    GridView1.Columns[13].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SXL != "0")
                                {

                                    GridView1.Columns[14].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SXXL != "0")
                                {

                                    GridView1.Columns[15].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (S3XL != "0")
                                {

                                    GridView1.Columns[17].Visible = true;
                                    GridView1.Columns[16].Visible = true;
                                }

                                if (S4XL != "0")
                                {

                                    GridView1.Columns[18].Visible = true;
                                    GridView1.Columns[16].Visible = true;
                                }


                                if (HS30 != "0")
                                {

                                    GridView1.Columns[20].Visible = true;
                                }
                                if (HS32 != "0")
                                {

                                    GridView1.Columns[21].Visible = true;
                                }

                                if (HS34 != "0")
                                {

                                    GridView1.Columns[22].Visible = true;
                                }

                                if (HS36 != "0")
                                {

                                    GridView1.Columns[23].Visible = true;
                                }

                                if (HSXS != "0")
                                {

                                    GridView1.Columns[24].Visible = true;
                                }

                                if (HSS != "0")
                                {

                                    GridView1.Columns[25].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSM != "0")
                                {

                                    GridView1.Columns[26].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSL != "0")
                                {

                                    GridView1.Columns[27].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSXL != "0")
                                {

                                    GridView1.Columns[28].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSXXL != "0")
                                {

                                    GridView1.Columns[29].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HS3XL != "0")
                                {

                                    GridView1.Columns[31].Visible = true;
                                    GridView1.Columns[30].Visible = true;
                                }

                                if (HS4XL != "0")
                                {

                                    GridView1.Columns[32].Visible = true;
                                    GridView1.Columns[30].Visible = true;
                                }
                                #endregion

                            }

                            #endregion
                            GridView1.Columns[2].Visible = false;

                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[16].Visible = false;
                            GridView1.Columns[19].Visible = false;
                            GridView1.Columns[30].Visible = false;
                        }
                    }
                    else
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {

                            #region
                            GridView1.DataSource = ds2;
                            GridView1.DataBind();

                            GridView1.Columns[0].Visible = true;
                            GridView1.Columns[1].Visible = true;
                            GridView1.Columns[2].Visible = false;
                            GridView1.Columns[3].Visible = true;
                            GridView1.Columns[4].Visible = true;

                            GridView1.Columns[5].Visible = false;

                            GridView1.Columns[6].Visible = false;
                            GridView1.Columns[7].Visible = false;
                            GridView1.Columns[8].Visible = false;
                            GridView1.Columns[9].Visible = false;
                            GridView1.Columns[10].Visible = false;
                            GridView1.Columns[11].Visible = false;
                            GridView1.Columns[12].Visible = false;
                            GridView1.Columns[13].Visible = false;
                            GridView1.Columns[14].Visible = false;
                            GridView1.Columns[15].Visible = false;

                            GridView1.Columns[16].Visible = false;

                            GridView1.Columns[17].Visible = false;
                            GridView1.Columns[18].Visible = false;

                            GridView1.Columns[19].Visible = false;

                            GridView1.Columns[20].Visible = false;
                            GridView1.Columns[21].Visible = false;
                            GridView1.Columns[22].Visible = false;
                            GridView1.Columns[23].Visible = false;
                            GridView1.Columns[24].Visible = false;
                            GridView1.Columns[25].Visible = false;
                            GridView1.Columns[26].Visible = false;
                            GridView1.Columns[27].Visible = false;
                            GridView1.Columns[28].Visible = false;
                            GridView1.Columns[29].Visible = false;

                            GridView1.Columns[30].Visible = false;

                            GridView1.Columns[31].Visible = false;
                            GridView1.Columns[32].Visible = false;

                            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                            {

                                #region
                                string S30 = ds2.Tables[0].Rows[j]["s30"].ToString();
                                string S32 = ds2.Tables[0].Rows[j]["s32"].ToString();
                                string S34 = ds2.Tables[0].Rows[j]["s34"].ToString();
                                string S36 = ds2.Tables[0].Rows[j]["s36"].ToString();
                                string SXS = ds2.Tables[0].Rows[j]["xs"].ToString();
                                string SS = ds2.Tables[0].Rows[j]["s"].ToString();
                                string SM = ds2.Tables[0].Rows[j]["m"].ToString();
                                string SL = ds2.Tables[0].Rows[j]["l"].ToString();
                                string SXL = ds2.Tables[0].Rows[j]["xl"].ToString();
                                string SXXL = ds2.Tables[0].Rows[j]["xxl"].ToString();
                                string S3XL = ds2.Tables[0].Rows[j]["s3xl"].ToString();
                                string S4XL = ds2.Tables[0].Rows[j]["s4xl"].ToString();


                                string HS30 = ds2.Tables[0].Rows[j]["Hs30"].ToString();
                                string HS32 = ds2.Tables[0].Rows[j]["Hs32"].ToString();
                                string HS34 = ds2.Tables[0].Rows[j]["Hs34"].ToString();
                                string HS36 = ds2.Tables[0].Rows[j]["Hs36"].ToString();
                                string HSXS = ds2.Tables[0].Rows[j]["Hxs"].ToString();
                                string HSS = ds2.Tables[0].Rows[j]["Hs"].ToString();
                                string HSM = ds2.Tables[0].Rows[j]["Hm"].ToString();
                                string HSL = ds2.Tables[0].Rows[j]["Hl"].ToString();
                                string HSXL = ds2.Tables[0].Rows[j]["Hxl"].ToString();
                                string HSXXL = ds2.Tables[0].Rows[j]["Hxxl"].ToString();
                                string HS3XL = ds2.Tables[0].Rows[j]["Hs3xl"].ToString();
                                string HS4XL = ds2.Tables[0].Rows[j]["Hs4xl"].ToString();

                                int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                                //////grndtot = grndtot + tot;
                                //////lblstockgrandtot.Text = grndtot.ToString();

                                if (S30 != "0")
                                {

                                    GridView1.Columns[6].Visible = true;
                                }
                                if (S32 != "0")
                                {

                                    GridView1.Columns[7].Visible = true;
                                }

                                if (S34 != "0")
                                {

                                    GridView1.Columns[8].Visible = true;
                                }

                                if (S36 != "0")
                                {

                                    GridView1.Columns[9].Visible = true;
                                }

                                if (SXS != "0")
                                {

                                    GridView1.Columns[10].Visible = true;
                                }

                                if (SS != "0")
                                {

                                    GridView1.Columns[11].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SM != "0")
                                {

                                    GridView1.Columns[12].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SL != "0")
                                {

                                    GridView1.Columns[13].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SXL != "0")
                                {

                                    GridView1.Columns[14].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SXXL != "0")
                                {

                                    GridView1.Columns[15].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (S3XL != "0")
                                {

                                    GridView1.Columns[17].Visible = true;
                                    GridView1.Columns[16].Visible = true;
                                }

                                if (S4XL != "0")
                                {

                                    GridView1.Columns[18].Visible = true;
                                    GridView1.Columns[16].Visible = true;
                                }


                                if (HS30 != "0")
                                {

                                    GridView1.Columns[20].Visible = true;
                                }
                                if (HS32 != "0")
                                {

                                    GridView1.Columns[21].Visible = true;
                                }

                                if (HS34 != "0")
                                {

                                    GridView1.Columns[22].Visible = true;
                                }

                                if (HS36 != "0")
                                {

                                    GridView1.Columns[23].Visible = true;
                                }

                                if (HSXS != "0")
                                {

                                    GridView1.Columns[24].Visible = true;
                                }

                                if (HSS != "0")
                                {

                                    GridView1.Columns[25].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSM != "0")
                                {

                                    GridView1.Columns[26].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSL != "0")
                                {

                                    GridView1.Columns[27].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSXL != "0")
                                {

                                    GridView1.Columns[28].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSXXL != "0")
                                {

                                    GridView1.Columns[29].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HS3XL != "0")
                                {

                                    GridView1.Columns[31].Visible = true;
                                    GridView1.Columns[30].Visible = true;
                                }

                                if (HS4XL != "0")
                                {

                                    GridView1.Columns[32].Visible = true;
                                    GridView1.Columns[30].Visible = true;
                                }
                                #endregion

                            }

                            #endregion
                        }
                    }

                    #endregion

                }


                string CompanyLotNo = "";
                string CountCompanyLotNo = "";
                DataSet ds = objBs.geDespatchlotdetails(Convert.ToInt32(DSPid));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        CompanyLotNo = ds.Tables[0].Rows[k]["CompanyLotNo"].ToString();

                        if (k == 0)
                        {
                            CountCompanyLotNo = "'" + CompanyLotNo + "'";
                        }
                        else
                        {
                            CountCompanyLotNo = CountCompanyLotNo + "," + "'" + CompanyLotNo + "'";
                        }
                    }

                    #region
                    DataSet dss1 = new DataSet();

                    DataSet ds1 = objBs.getDespatchStockQtydetails(CountCompanyLotNo);

                    ////DataSet Merge1 = objBs.getMasterStockRatioQtydetails(CountCompanyLotNo);
                    ////DataSet Merge2 = objBs.getMasterStockRatioQtydetailsstich(CountCompanyLotNo);

                    ////if (Merge1.Tables.Count > 0)
                    ////{
                    ////    if (Merge1.Tables[0].Rows.Count > 0)
                    ////    {
                    ////        dss1.Merge(Merge1);
                    ////    }
                    ////}
                    ////if (Merge2.Tables.Count > 0)
                    ////{
                    ////    if (Merge2.Tables[0].Rows.Count > 0)
                    ////    {
                    ////        dss1.Merge(Merge2);
                    ////    }
                    ////}

                    if (CmpyId == "3")
                    {
                        #region

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            DataTable dttt;
                            DataRow drNew;
                            DataColumn dct;
                            DataSet dstd = new DataSet();
                            dttt = new DataTable();

                            dct = new DataColumn("LotNo");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("DespatchQty");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("GodownQty");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("BalanceQty");
                            dttt.Columns.Add(dct);
                            dstd.Tables.Add(dttt);
                            for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                            {
                                int valBalanceQty = 0;
                                drNew = dttt.NewRow();
                                drNew["LotNo"] = ds1.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                drNew["DespatchQty"] = ds1.Tables[0].Rows[r]["TotalDespatchqty"].ToString();

                                DataSet dsopenstk = objBs.Getopenfinishedstock(ds1.Tables[0].Rows[r]["CompanyLotNo"].ToString());
                                if (dsopenstk.Tables[0].Rows.Count > 0)
                                {
                                    valBalanceQty = Convert.ToInt32(dsopenstk.Tables[0].Rows[0]["Totalshirt"].ToString());

                                    if (valBalanceQty > 0)
                                    {
                                        drNew["GodownQty"] = valBalanceQty;
                                    }
                                    else
                                    {
                                        drNew["GodownQty"] = 0;
                                    }
                                }


                                int valmasterQty = 0; int valironQty = 0;

                                DataSet dscut = objBs.Getopenfinishedstockforcut(ds1.Tables[0].Rows[r]["CompanyLotNo"].ToString());

                                if (dscut.Tables[0].Rows.Count > 0)
                                {
                                    if (dscut.Tables[0].Rows[0]["CompleteStitching"].ToString() == "Yes")
                                    {
                                        DataSet dsmasterstk = objBs.Getopenfinishedstockforprocess(ds1.Tables[0].Rows[r]["CompanyLotNo"].ToString());
                                        DataSet dsironstk = objBs.Getopenfinishedstockforstitch(ds1.Tables[0].Rows[r]["CompanyLotNo"].ToString());

                                       
                                            valmasterQty = Convert.ToInt32(dsmasterstk.Tables[0].Rows[0]["Totalshirt"].ToString());
                                            valironQty = Convert.ToInt32(dsironstk.Tables[0].Rows[0]["Totalshirt"].ToString());

                                            if ((valmasterQty + valironQty) > 0)
                                            {
                                                drNew["BalanceQty"] = (valmasterQty + valironQty);
                                            }
                                            else
                                            {
                                                drNew["BalanceQty"] = 0;
                                            }
                                       
                                    }
                                    else
                                    {
                                        DataSet dsmasterstk = objBs.Getopenfinishedstockformaster(ds1.Tables[0].Rows[r]["CompanyLotNo"].ToString());
                                        DataSet dsironstk = objBs.Getopenfinishedstockforiron(ds1.Tables[0].Rows[r]["CompanyLotNo"].ToString());

                                       
                                            valmasterQty = Convert.ToInt32(dsmasterstk.Tables[0].Rows[0]["Totalshirt"].ToString());
                                            valironQty = Convert.ToInt32(dsironstk.Tables[0].Rows[0]["Totalshirt"].ToString());

                                            if ((valmasterQty + valironQty) > 0)
                                            {
                                                drNew["BalanceQty"] = (valmasterQty + valironQty);
                                            }
                                            else
                                            {
                                                drNew["BalanceQty"] = 0;
                                            }
                                        

                                    }

                                }
                                else
                                {
                                    drNew["BalanceQty"] = 0;
                                }



                                //////1 drNew["BalanceQty"] = (Convert.ToInt32(dss1.Tables[0].Rows[r]["Totalshirt"].ToString()) - Convert.ToInt32(ds1.Tables[0].Rows[r]["TotalDespatchqty"].ToString()));

                                //////2 if (dss1.Tables.Count > 0)
                                //////{
                                //////    if (dss1.Tables[0].Rows.Count > 0)
                                //////    {
                                //////        valBalanceQty = (Convert.ToInt32(dss1.Tables[0].Rows[r]["Totalshirt"].ToString()) - Convert.ToInt32(ds1.Tables[0].Rows[r]["TotalDespatchqty"].ToString()));

                                //////        if (valBalanceQty > 0)
                                //////        {
                                //////            drNew["BalanceQty"] = valBalanceQty;
                                //////        }
                                //////        else
                                //////        {
                                //////            drNew["BalanceQty"] = 0;
                                //////        }
                                //////    }
                                //////}





                                dstd.Tables[0].Rows.Add(drNew);
                            }
                            gvlotqtyDetails.DataSource = dstd;
                            gvlotqtyDetails.DataBind();

                        }
                        #endregion
                    }
                    #endregion

                }



               

                #endregion

                if (CmpyId == "3")
                {
                    lblDeliveryprint.InnerText = "Delivery Note";

                    lblmblrpll.Visible = true;
                    lblmblbc.Visible = false;
                }
                else
                {
                    lblDeliveryprint.InnerText = "Order Form Note";

                    lblmblrpll.Visible = false;
                    lblmblbc.Visible = true;
                }
            }
            if (DSPRetid != null)
            {
                #region

                DataSet ds2 = objBs.GETPRINTOFDESPATCHret(Convert.ToInt32(DSPRetid));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lbllcustname.Text = ds2.Tables[0].Rows[0]["Customername"].ToString();
                    lblgastin.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString();
                    lblladdress.Text = ds2.Tables[0].Rows[0]["Address"].ToString();
                    lbllmobile.Text = ds2.Tables[0].Rows[0]["MobileNo"].ToString();
                    lbllphone.Text = ds2.Tables[0].Rows[0]["PhoneNo"].ToString();
                    lbllemail.Text = ds2.Tables[0].Rows[0]["Email"].ToString();

                    lblldcno.Text = ds2.Tables[0].Rows[0]["DcNo"].ToString();
                    lblldcdate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["DCDate"]).ToString("dd/MM/yyyy");
                    lblltotalqty.Text = ds2.Tables[0].Rows[0]["TotalQty"].ToString();
                    lbllnarration.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                    lblldespatch.Text = ds2.Tables[0].Rows[0]["ledgername"].ToString();

                    CmpyId = ds2.Tables[0].Rows[0]["Companyid"].ToString();

                    //////lblLot.Text = ds2.Tables[0].Rows[0]["lotno"].ToString();
                    //////lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    //////lblLedgerName.Text = ds2.Tables[0].Rows[0]["name"].ToString();
                    //////lblPaidAmount.Text = ds2.Tables[0].Rows[0]["PaidAmount"].ToString();
                    //////lblTotalAmount.Text = ds2.Tables[0].Rows[0]["TotalAmount"].ToString();
                    //////lblTotalQty.Text = ds2.Tables[0].Rows[0]["Totalqty"].ToString();


                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        #region

                        //GridView1.Columns[0].Visible = true;
                        //GridView1.Columns[1].Visible = true;
                        //GridView1.Columns[2].Visible = true;
                        //GridView1.Columns[3].Visible = true;
                        //GridView1.Columns[4].Visible = true;

                        //GridView1.Columns[5].Visible = false;
                        //GridView1.Columns[6].Visible = false;
                        //GridView1.Columns[7].Visible = false;
                        //GridView1.Columns[8].Visible = false;
                        //GridView1.Columns[9].Visible = false;
                        //GridView1.Columns[10].Visible = false;
                        //GridView1.Columns[11].Visible = false;
                        //GridView1.Columns[12].Visible = false;
                        //GridView1.Columns[13].Visible = false;
                        //GridView1.Columns[14].Visible = false;
                        //GridView1.Columns[15].Visible = false;
                        //GridView1.Columns[16].Visible = false;

                        //GridView1.Columns[17].Visible = false;
                        //GridView1.Columns[18].Visible = false;
                        //GridView1.Columns[19].Visible = false;
                        //GridView1.Columns[20].Visible = false;
                        //GridView1.Columns[21].Visible = false;
                        //GridView1.Columns[22].Visible = false;
                        //GridView1.Columns[23].Visible = false;
                        //GridView1.Columns[24].Visible = false;
                        //GridView1.Columns[25].Visible = false;
                        //GridView1.Columns[26].Visible = false;
                        //GridView1.Columns[27].Visible = false;
                        //GridView1.Columns[28].Visible = false;

                        //for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                        //{

                        //    #region
                        //    string S30 = ds2.Tables[0].Rows[j]["s30"].ToString();
                        //    string S32 = ds2.Tables[0].Rows[j]["s32"].ToString();
                        //    string S34 = ds2.Tables[0].Rows[j]["s34"].ToString();
                        //    string S36 = ds2.Tables[0].Rows[j]["s36"].ToString();
                        //    string SXS = ds2.Tables[0].Rows[j]["xs"].ToString();
                        //    string SS = ds2.Tables[0].Rows[j]["s"].ToString();
                        //    string SM = ds2.Tables[0].Rows[j]["m"].ToString();
                        //    string SL = ds2.Tables[0].Rows[j]["l"].ToString();
                        //    string SXL = ds2.Tables[0].Rows[j]["xl"].ToString();
                        //    string SXXL = ds2.Tables[0].Rows[j]["xxl"].ToString();
                        //    string S3XL = ds2.Tables[0].Rows[j]["s3xl"].ToString();
                        //    string S4XL = ds2.Tables[0].Rows[j]["s4xl"].ToString();


                        //    string HS30 = ds2.Tables[0].Rows[j]["Hs30"].ToString();
                        //    string HS32 = ds2.Tables[0].Rows[j]["Hs32"].ToString();
                        //    string HS34 = ds2.Tables[0].Rows[j]["Hs34"].ToString();
                        //    string HS36 = ds2.Tables[0].Rows[j]["Hs36"].ToString();
                        //    string HSXS = ds2.Tables[0].Rows[j]["Hxs"].ToString();
                        //    string HSS = ds2.Tables[0].Rows[j]["Hs"].ToString();
                        //    string HSM = ds2.Tables[0].Rows[j]["Hm"].ToString();
                        //    string HSL = ds2.Tables[0].Rows[j]["Hl"].ToString();
                        //    string HSXL = ds2.Tables[0].Rows[j]["Hxl"].ToString();
                        //    string HSXXL = ds2.Tables[0].Rows[j]["Hxxl"].ToString();
                        //    string HS3XL = ds2.Tables[0].Rows[j]["Hs3xl"].ToString();
                        //    string HS4XL = ds2.Tables[0].Rows[j]["Hs4xl"].ToString();

                        //    int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                        //    //////grndtot = grndtot + tot;
                        //    //////lblstockgrandtot.Text = grndtot.ToString();

                        //    if (S30 != "0")
                        //    {

                        //        GridView1.Columns[5].Visible = true;
                        //    }
                        //    if (S32 != "0")
                        //    {

                        //        GridView1.Columns[6].Visible = true;
                        //    }

                        //    if (S34 != "0")
                        //    {

                        //        GridView1.Columns[7].Visible = true;
                        //    }

                        //    if (S36 != "0")
                        //    {

                        //        GridView1.Columns[8].Visible = true;
                        //    }

                        //    if (SXS != "0")
                        //    {

                        //        GridView1.Columns[9].Visible = true;
                        //    }

                        //    if (SS != "0")
                        //    {

                        //        GridView1.Columns[10].Visible = true;
                        //    }

                        //    if (SM != "0")
                        //    {

                        //        GridView1.Columns[11].Visible = true;
                        //    }

                        //    if (SL != "0")
                        //    {

                        //        GridView1.Columns[12].Visible = true;
                        //    }

                        //    if (SXL != "0")
                        //    {

                        //        GridView1.Columns[13].Visible = true;
                        //    }

                        //    if (SXXL != "0")
                        //    {

                        //        GridView1.Columns[14].Visible = true;
                        //    }

                        //    if (S3XL != "0")
                        //    {

                        //        GridView1.Columns[15].Visible = true;
                        //    }

                        //    if (S4XL != "0")
                        //    {

                        //        GridView1.Columns[16].Visible = true;
                        //    }


                        //    if (HS30 != "0")
                        //    {

                        //        GridView1.Columns[17].Visible = true;
                        //    }
                        //    if (HS32 != "0")
                        //    {

                        //        GridView1.Columns[18].Visible = true;
                        //    }

                        //    if (HS34 != "0")
                        //    {

                        //        GridView1.Columns[19].Visible = true;
                        //    }

                        //    if (HS36 != "0")
                        //    {

                        //        GridView1.Columns[20].Visible = true;
                        //    }

                        //    if (HSXS != "0")
                        //    {

                        //        GridView1.Columns[21].Visible = true;
                        //    }

                        //    if (HSS != "0")
                        //    {

                        //        GridView1.Columns[22].Visible = true;
                        //    }

                        //    if (HSM != "0")
                        //    {

                        //        GridView1.Columns[23].Visible = true;
                        //    }

                        //    if (HSL != "0")
                        //    {

                        //        GridView1.Columns[24].Visible = true;
                        //    }

                        //    if (HSXL != "0")
                        //    {

                        //        GridView1.Columns[25].Visible = true;
                        //    }

                        //    if (HSXXL != "0")
                        //    {

                        //        GridView1.Columns[26].Visible = true;
                        //    }

                        //    if (HS3XL != "0")
                        //    {

                        //        GridView1.Columns[27].Visible = true;
                        //    }

                        //    if (HS4XL != "0")
                        //    {

                        //        GridView1.Columns[28].Visible = true;
                        //    }
                        //    #endregion

                        //}

                        #endregion
                    }
                    if (CmpyId == "3")
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            #region

                            GridView1.DataSource = ds2;
                            GridView1.DataBind();

                            GridView1.Columns[0].Visible = true;
                            GridView1.Columns[1].Visible = true;
                            GridView1.Columns[2].Visible = false;
                            GridView1.Columns[3].Visible = true;
                            GridView1.Columns[4].Visible = true;

                            GridView1.Columns[5].Visible = false;

                            GridView1.Columns[6].Visible = false;
                            GridView1.Columns[7].Visible = false;
                            GridView1.Columns[8].Visible = false;
                            GridView1.Columns[9].Visible = false;
                            GridView1.Columns[10].Visible = false;
                            GridView1.Columns[11].Visible = false;
                            GridView1.Columns[12].Visible = false;
                            GridView1.Columns[13].Visible = false;
                            GridView1.Columns[14].Visible = false;
                            GridView1.Columns[15].Visible = false;

                            GridView1.Columns[16].Visible = false;

                            GridView1.Columns[17].Visible = false;
                            GridView1.Columns[18].Visible = false;

                            GridView1.Columns[19].Visible = false;

                            GridView1.Columns[20].Visible = false;
                            GridView1.Columns[21].Visible = false;
                            GridView1.Columns[22].Visible = false;
                            GridView1.Columns[23].Visible = false;
                            GridView1.Columns[24].Visible = false;
                            GridView1.Columns[25].Visible = false;
                            GridView1.Columns[26].Visible = false;
                            GridView1.Columns[27].Visible = false;
                            GridView1.Columns[28].Visible = false;
                            GridView1.Columns[29].Visible = false;

                            GridView1.Columns[30].Visible = false;

                            GridView1.Columns[31].Visible = false;
                            GridView1.Columns[32].Visible = false;

                            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                            {

                                #region
                                string S30 = ds2.Tables[0].Rows[j]["s30"].ToString();
                                string S32 = ds2.Tables[0].Rows[j]["s32"].ToString();
                                string S34 = ds2.Tables[0].Rows[j]["s34"].ToString();
                                string S36 = ds2.Tables[0].Rows[j]["s36"].ToString();
                                string SXS = ds2.Tables[0].Rows[j]["xs"].ToString();
                                string SS = ds2.Tables[0].Rows[j]["s"].ToString();
                                string SM = ds2.Tables[0].Rows[j]["m"].ToString();
                                string SL = ds2.Tables[0].Rows[j]["l"].ToString();
                                string SXL = ds2.Tables[0].Rows[j]["xl"].ToString();
                                string SXXL = ds2.Tables[0].Rows[j]["xxl"].ToString();
                                string S3XL = ds2.Tables[0].Rows[j]["s3xl"].ToString();
                                string S4XL = ds2.Tables[0].Rows[j]["s4xl"].ToString();


                                string HS30 = ds2.Tables[0].Rows[j]["Hs30"].ToString();
                                string HS32 = ds2.Tables[0].Rows[j]["Hs32"].ToString();
                                string HS34 = ds2.Tables[0].Rows[j]["Hs34"].ToString();
                                string HS36 = ds2.Tables[0].Rows[j]["Hs36"].ToString();
                                string HSXS = ds2.Tables[0].Rows[j]["Hxs"].ToString();
                                string HSS = ds2.Tables[0].Rows[j]["Hs"].ToString();
                                string HSM = ds2.Tables[0].Rows[j]["Hm"].ToString();
                                string HSL = ds2.Tables[0].Rows[j]["Hl"].ToString();
                                string HSXL = ds2.Tables[0].Rows[j]["Hxl"].ToString();
                                string HSXXL = ds2.Tables[0].Rows[j]["Hxxl"].ToString();
                                string HS3XL = ds2.Tables[0].Rows[j]["Hs3xl"].ToString();
                                string HS4XL = ds2.Tables[0].Rows[j]["Hs4xl"].ToString();

                                int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                                //////grndtot = grndtot + tot;
                                //////lblstockgrandtot.Text = grndtot.ToString();

                                if (S30 != "0")
                                {

                                    GridView1.Columns[6].Visible = true;
                                }
                                if (S32 != "0")
                                {

                                    GridView1.Columns[7].Visible = true;
                                }

                                if (S34 != "0")
                                {

                                    GridView1.Columns[8].Visible = true;
                                }

                                if (S36 != "0")
                                {

                                    GridView1.Columns[9].Visible = true;
                                }

                                if (SXS != "0")
                                {

                                    GridView1.Columns[10].Visible = true;
                                }

                                if (SS != "0")
                                {

                                    GridView1.Columns[11].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SM != "0")
                                {

                                    GridView1.Columns[12].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SL != "0")
                                {

                                    GridView1.Columns[13].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SXL != "0")
                                {

                                    GridView1.Columns[14].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SXXL != "0")
                                {

                                    GridView1.Columns[15].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (S3XL != "0")
                                {

                                    GridView1.Columns[17].Visible = true;
                                    GridView1.Columns[16].Visible = true;
                                }

                                if (S4XL != "0")
                                {

                                    GridView1.Columns[18].Visible = true;
                                    GridView1.Columns[16].Visible = true;
                                }


                                if (HS30 != "0")
                                {

                                    GridView1.Columns[20].Visible = true;
                                }
                                if (HS32 != "0")
                                {

                                    GridView1.Columns[21].Visible = true;
                                }

                                if (HS34 != "0")
                                {

                                    GridView1.Columns[22].Visible = true;
                                }

                                if (HS36 != "0")
                                {

                                    GridView1.Columns[23].Visible = true;
                                }

                                if (HSXS != "0")
                                {

                                    GridView1.Columns[24].Visible = true;
                                }

                                if (HSS != "0")
                                {

                                    GridView1.Columns[25].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSM != "0")
                                {

                                    GridView1.Columns[26].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSL != "0")
                                {

                                    GridView1.Columns[27].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSXL != "0")
                                {

                                    GridView1.Columns[28].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSXXL != "0")
                                {

                                    GridView1.Columns[29].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HS3XL != "0")
                                {

                                    GridView1.Columns[31].Visible = true;
                                    GridView1.Columns[30].Visible = true;
                                }

                                if (HS4XL != "0")
                                {

                                    GridView1.Columns[32].Visible = true;
                                    GridView1.Columns[30].Visible = true;
                                }
                                #endregion

                            }

                            #endregion

                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[16].Visible = false;
                            GridView1.Columns[19].Visible = false;
                            GridView1.Columns[30].Visible = false;
                        }
                    }
                    else
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            #region

                            GridView1.DataSource = ds2;
                            GridView1.DataBind();

                            GridView1.Columns[0].Visible = true;
                            GridView1.Columns[1].Visible = true;
                            GridView1.Columns[2].Visible = false;
                            GridView1.Columns[3].Visible = true;
                            GridView1.Columns[4].Visible = true;

                            GridView1.Columns[5].Visible = false;

                            GridView1.Columns[6].Visible = false;
                            GridView1.Columns[7].Visible = false;
                            GridView1.Columns[8].Visible = false;
                            GridView1.Columns[9].Visible = false;
                            GridView1.Columns[10].Visible = false;
                            GridView1.Columns[11].Visible = false;
                            GridView1.Columns[12].Visible = false;
                            GridView1.Columns[13].Visible = false;
                            GridView1.Columns[14].Visible = false;
                            GridView1.Columns[15].Visible = false;

                            GridView1.Columns[16].Visible = false;

                            GridView1.Columns[17].Visible = false;
                            GridView1.Columns[18].Visible = false;

                            GridView1.Columns[19].Visible = false;

                            GridView1.Columns[20].Visible = false;
                            GridView1.Columns[21].Visible = false;
                            GridView1.Columns[22].Visible = false;
                            GridView1.Columns[23].Visible = false;
                            GridView1.Columns[24].Visible = false;
                            GridView1.Columns[25].Visible = false;
                            GridView1.Columns[26].Visible = false;
                            GridView1.Columns[27].Visible = false;
                            GridView1.Columns[28].Visible = false;
                            GridView1.Columns[29].Visible = false;

                            GridView1.Columns[30].Visible = false;

                            GridView1.Columns[31].Visible = false;
                            GridView1.Columns[32].Visible = false;

                            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                            {

                                #region
                                string S30 = ds2.Tables[0].Rows[j]["s30"].ToString();
                                string S32 = ds2.Tables[0].Rows[j]["s32"].ToString();
                                string S34 = ds2.Tables[0].Rows[j]["s34"].ToString();
                                string S36 = ds2.Tables[0].Rows[j]["s36"].ToString();
                                string SXS = ds2.Tables[0].Rows[j]["xs"].ToString();
                                string SS = ds2.Tables[0].Rows[j]["s"].ToString();
                                string SM = ds2.Tables[0].Rows[j]["m"].ToString();
                                string SL = ds2.Tables[0].Rows[j]["l"].ToString();
                                string SXL = ds2.Tables[0].Rows[j]["xl"].ToString();
                                string SXXL = ds2.Tables[0].Rows[j]["xxl"].ToString();
                                string S3XL = ds2.Tables[0].Rows[j]["s3xl"].ToString();
                                string S4XL = ds2.Tables[0].Rows[j]["s4xl"].ToString();


                                string HS30 = ds2.Tables[0].Rows[j]["Hs30"].ToString();
                                string HS32 = ds2.Tables[0].Rows[j]["Hs32"].ToString();
                                string HS34 = ds2.Tables[0].Rows[j]["Hs34"].ToString();
                                string HS36 = ds2.Tables[0].Rows[j]["Hs36"].ToString();
                                string HSXS = ds2.Tables[0].Rows[j]["Hxs"].ToString();
                                string HSS = ds2.Tables[0].Rows[j]["Hs"].ToString();
                                string HSM = ds2.Tables[0].Rows[j]["Hm"].ToString();
                                string HSL = ds2.Tables[0].Rows[j]["Hl"].ToString();
                                string HSXL = ds2.Tables[0].Rows[j]["Hxl"].ToString();
                                string HSXXL = ds2.Tables[0].Rows[j]["Hxxl"].ToString();
                                string HS3XL = ds2.Tables[0].Rows[j]["Hs3xl"].ToString();
                                string HS4XL = ds2.Tables[0].Rows[j]["Hs4xl"].ToString();

                                int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                                //////grndtot = grndtot + tot;
                                //////lblstockgrandtot.Text = grndtot.ToString();

                                if (S30 != "0")
                                {

                                    GridView1.Columns[6].Visible = true;
                                }
                                if (S32 != "0")
                                {

                                    GridView1.Columns[7].Visible = true;
                                }

                                if (S34 != "0")
                                {

                                    GridView1.Columns[8].Visible = true;
                                }

                                if (S36 != "0")
                                {

                                    GridView1.Columns[9].Visible = true;
                                }

                                if (SXS != "0")
                                {

                                    GridView1.Columns[10].Visible = true;
                                }

                                if (SS != "0")
                                {

                                    GridView1.Columns[11].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SM != "0")
                                {

                                    GridView1.Columns[12].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SL != "0")
                                {

                                    GridView1.Columns[13].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SXL != "0")
                                {

                                    GridView1.Columns[14].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (SXXL != "0")
                                {

                                    GridView1.Columns[15].Visible = true;
                                    GridView1.Columns[5].Visible = true;
                                }

                                if (S3XL != "0")
                                {

                                    GridView1.Columns[17].Visible = true;
                                    GridView1.Columns[16].Visible = true;
                                }

                                if (S4XL != "0")
                                {

                                    GridView1.Columns[18].Visible = true;
                                    GridView1.Columns[16].Visible = true;
                                }


                                if (HS30 != "0")
                                {

                                    GridView1.Columns[20].Visible = true;
                                }
                                if (HS32 != "0")
                                {

                                    GridView1.Columns[21].Visible = true;
                                }

                                if (HS34 != "0")
                                {

                                    GridView1.Columns[22].Visible = true;
                                }

                                if (HS36 != "0")
                                {

                                    GridView1.Columns[23].Visible = true;
                                }

                                if (HSXS != "0")
                                {

                                    GridView1.Columns[24].Visible = true;
                                }

                                if (HSS != "0")
                                {

                                    GridView1.Columns[25].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSM != "0")
                                {

                                    GridView1.Columns[26].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSL != "0")
                                {

                                    GridView1.Columns[27].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSXL != "0")
                                {

                                    GridView1.Columns[28].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HSXXL != "0")
                                {

                                    GridView1.Columns[29].Visible = true;
                                    GridView1.Columns[19].Visible = true;
                                }

                                if (HS3XL != "0")
                                {

                                    GridView1.Columns[31].Visible = true;
                                    GridView1.Columns[30].Visible = true;
                                }

                                if (HS4XL != "0")
                                {

                                    GridView1.Columns[32].Visible = true;
                                    GridView1.Columns[30].Visible = true;
                                }
                                #endregion

                            }

                            #endregion
                        }
                    }
                }


                string CompanyLotNo = "";
                string CountCompanyLotNo = "";
                DataSet ds = objBs.geDespatchlotdetailsRet(Convert.ToInt32(DSPRetid));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        CompanyLotNo = ds.Tables[0].Rows[k]["CompanyLotNo"].ToString();

                        if (k == 0)
                        {
                            CountCompanyLotNo = "'" + CompanyLotNo + "'";
                        }
                        else
                        {
                            CountCompanyLotNo = CountCompanyLotNo + "," + "'" + CompanyLotNo + "'";
                        }
                    }

                    #region
                    //////DataSet dss1 = new DataSet();

                    //////DataSet ds1 = objBs.getDespatchStockQtydetails(CountCompanyLotNo);
                    //////DataSet Merge1 = objBs.getMasterStockRatioQtydetails(CountCompanyLotNo);
                    //////DataSet Merge2 = objBs.getMasterStockRatioQtydetailsstich(CountCompanyLotNo);
                    //////if (Merge1.Tables.Count > 0)
                    //////{
                    //////    if (Merge1.Tables[0].Rows.Count > 0)
                    //////    {
                    //////        dss1.Merge(Merge1);
                    //////    }
                    //////}
                    //////if (Merge2.Tables.Count > 0)
                    //////{
                    //////    if (Merge2.Tables[0].Rows.Count > 0)
                    //////    {
                    //////        dss1.Merge(Merge2);
                    //////    }
                    //////}

                    //////if (ds1.Tables[0].Rows.Count > 0)
                    //////{
                    //////    DataTable dttt;
                    //////    DataRow drNew;
                    //////    DataColumn dct;
                    //////    DataSet dstd = new DataSet();
                    //////    dttt = new DataTable();

                    //////    dct = new DataColumn("LotNo");
                    //////    dttt.Columns.Add(dct);

                    //////    dct = new DataColumn("DespatchQty");
                    //////    dttt.Columns.Add(dct);

                    //////    dct = new DataColumn("BalanceQty");
                    //////    dttt.Columns.Add(dct);
                    //////    dstd.Tables.Add(dttt);
                    //////    for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                    //////    {
                    //////        int valBalanceQty = 0;
                    //////        drNew = dttt.NewRow();
                    //////        drNew["LotNo"] = ds1.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                    //////        drNew["DespatchQty"] = ds1.Tables[0].Rows[r]["TotalDespatchqty"].ToString();
                    //////        ////// drNew["BalanceQty"] = (Convert.ToInt32(dss1.Tables[0].Rows[r]["Totalshirt"].ToString()) - Convert.ToInt32(ds1.Tables[0].Rows[r]["TotalDespatchqty"].ToString()));
                    //////        valBalanceQty = (Convert.ToInt32(dss1.Tables[0].Rows[r]["Totalshirt"].ToString()) - Convert.ToInt32(ds1.Tables[0].Rows[r]["TotalDespatchqty"].ToString()));
                    //////        if (valBalanceQty > 0)
                    //////        {
                    //////            drNew["BalanceQty"] = valBalanceQty;
                    //////        }
                    //////        else
                    //////        {
                    //////            drNew["BalanceQty"] = 0;
                    //////        }


                    //////        dstd.Tables[0].Rows.Add(drNew);
                    //////    }
                    //////    gvlotqtyDetails.DataSource = dstd;
                    //////    gvlotqtyDetails.DataBind();

                    //////}
                    #endregion

                }



                

                #endregion

                if (CmpyId == "3")
                {
                    lblDeliveryprint.InnerText = "Delivery Return Note";
                    lblmblrpll.Visible = true;
                    lblmblbc.Visible = false;
                }
                else
                {
                    lblDeliveryprint.InnerText = "Order Form Return Note";
                    lblmblrpll.Visible = false;
                    lblmblbc.Visible = true;
                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("DespatchGrid.aspx");
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region

                //if (e.Row.Cells[5].Text != "-" && e.Row.Cells[5].Text != "")
                //{
                //    F30 = F30 + Convert.ToDouble(e.Row.Cells[5].Text);
                //}
                if (e.Row.Cells[6].Text != "-" && e.Row.Cells[6].Text != "")
                {
                    F32 = F32 + Convert.ToDouble(e.Row.Cells[6].Text);
                }
                if (e.Row.Cells[7].Text != "-" && e.Row.Cells[7].Text != "")
                {
                    F34 = F34 + Convert.ToDouble(e.Row.Cells[7].Text);
                }
                if (e.Row.Cells[8].Text != "-" && e.Row.Cells[8].Text != "")
                {
                    F36 = F36 + Convert.ToDouble(e.Row.Cells[8].Text);
                }
                if (e.Row.Cells[9].Text != "-" && e.Row.Cells[9].Text != "")
                {
                    FXS = FXS + Convert.ToDouble(e.Row.Cells[9].Text);
                }
                if (e.Row.Cells[10].Text != "-" && e.Row.Cells[10].Text != "")
                {
                    FS = FS + Convert.ToDouble(e.Row.Cells[10].Text);
                }
                if (e.Row.Cells[11].Text != "-" && e.Row.Cells[11].Text != "")
                {
                    FM = FM + Convert.ToDouble(e.Row.Cells[11].Text);
                }
                if (e.Row.Cells[12].Text != "-" && e.Row.Cells[12].Text != "")
                {
                    FL = FL + Convert.ToDouble(e.Row.Cells[12].Text);
                }
                if (e.Row.Cells[13].Text != "-" && e.Row.Cells[13].Text != "")
                {
                    FXL = FXL + Convert.ToDouble(e.Row.Cells[13].Text);
                }
                if (e.Row.Cells[14].Text != "-" && e.Row.Cells[14].Text != "")
                {
                    FXXL = FXXL + Convert.ToDouble(e.Row.Cells[14].Text);
                }
                if (e.Row.Cells[15].Text != "-" && e.Row.Cells[15].Text != "")
                {
                    F3XL = F3XL + Convert.ToDouble(e.Row.Cells[15].Text);
                }
                //if (e.Row.Cells[16].Text != "-" && e.Row.Cells[16].Text != "")
                //{
                //    F4XL = F4XL + Convert.ToDouble(e.Row.Cells[16].Text);
                //}
                if (e.Row.Cells[17].Text != "-" && e.Row.Cells[17].Text != "")
                {

                    H30 = H30 + Convert.ToDouble(e.Row.Cells[17].Text);
                }
                if (e.Row.Cells[18].Text != "-" && e.Row.Cells[18].Text != "")
                {
                    H32 = H32 + Convert.ToDouble(e.Row.Cells[18].Text);
                }
                //if (e.Row.Cells[19].Text != "-" && e.Row.Cells[19].Text != "")
                //{
                //    H34 = H34 + Convert.ToDouble(e.Row.Cells[19].Text);
                //}
                if (e.Row.Cells[20].Text != "-" && e.Row.Cells[20].Text != "")
                {
                    H36 = H36 + Convert.ToDouble(e.Row.Cells[20].Text);
                }
                if (e.Row.Cells[21].Text != "-" && e.Row.Cells[21].Text != "")
                {
                    HXS = HXS + Convert.ToDouble(e.Row.Cells[21].Text);
                }
                if (e.Row.Cells[22].Text != "-" && e.Row.Cells[22].Text != "")
                {
                    HS = HS + Convert.ToDouble(e.Row.Cells[22].Text);
                }
                if (e.Row.Cells[23].Text != "-" && e.Row.Cells[23].Text != "")
                {
                    HM = HM + Convert.ToDouble(e.Row.Cells[23].Text);
                }
                if (e.Row.Cells[24].Text != "-" && e.Row.Cells[24].Text != "")
                {
                    HL = HL + Convert.ToDouble(e.Row.Cells[24].Text);
                }
                if (e.Row.Cells[25].Text != "-" && e.Row.Cells[25].Text != "")
                {
                    HXL = HXL + Convert.ToDouble(e.Row.Cells[25].Text);
                }
                if (e.Row.Cells[26].Text != "-" && e.Row.Cells[26].Text != "")
                {
                    HXXL = HXXL + Convert.ToDouble(e.Row.Cells[26].Text);
                }
                if (e.Row.Cells[27].Text != "-" && e.Row.Cells[27].Text != "")
                {
                    H3XL = H3XL + Convert.ToDouble(e.Row.Cells[27].Text);
                }
                if (e.Row.Cells[28].Text != "-" && e.Row.Cells[28].Text != "")
                {
                    H4XL = H4XL + Convert.ToDouble(e.Row.Cells[28].Text);
                }

                if (e.Row.Cells[29].Text != "-" && e.Row.Cells[29].Text != "")
                {
                    H4XL = H4XL + Convert.ToDouble(e.Row.Cells[29].Text);
                }
                //if (e.Row.Cells[30].Text != "-" && e.Row.Cells[30].Text != "")
                //{
                //    H4XL = H4XL + Convert.ToDouble(e.Row.Cells[30].Text);
                //}
                if (e.Row.Cells[31].Text != "-" && e.Row.Cells[31].Text != "")
                {
                    H4XL = H4XL + Convert.ToDouble(e.Row.Cells[31].Text);
                }
                if (e.Row.Cells[32].Text != "-" && e.Row.Cells[32].Text != "")
                {
                    H4XL = H4XL + Convert.ToDouble(e.Row.Cells[32].Text);
                }
                #endregion

                TOTAL = TOTAL + Convert.ToDouble(e.Row.Cells[33].Text);

                #region




                //if (e.Row.Cells[5].Text == "0")
                //{
                //    e.Row.Cells[5].Text = "-";
                //}
                if (e.Row.Cells[6].Text == "0")
                {
                    e.Row.Cells[6].Text = "-";
                }
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
                //if (e.Row.Cells[16].Text == "0")
                //{
                //    e.Row.Cells[16].Text = "-";
                //}
                if (e.Row.Cells[17].Text == "0")
                {
                    e.Row.Cells[17].Text = "-";
                }
                if (e.Row.Cells[18].Text == "0")
                {
                    e.Row.Cells[18].Text = "-";
                }
                //if (e.Row.Cells[19].Text == "0")
                //{
                //    e.Row.Cells[19].Text = "-";
                //}
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
                //if (e.Row.Cells[30].Text == "0")
                //{
                //    e.Row.Cells[30].Text = "-";
                //}
                if (e.Row.Cells[31].Text == "0")
                {
                    e.Row.Cells[31].Text = "-";
                }
                if (e.Row.Cells[32].Text == "0")
                {
                    e.Row.Cells[32].Text = "-";
                }
                #endregion

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region
                //e.Row.Cells[3].Text = F30.ToString();
                //e.Row.Cells[4].Text = F32.ToString();
                //e.Row.Cells[5].Text = F34.ToString();
                //e.Row.Cells[6].Text = F36.ToString();
                //e.Row.Cells[7].Text = FXS.ToString();
                //e.Row.Cells[8].Text = FS.ToString();
                //e.Row.Cells[9].Text = FM.ToString();
                //e.Row.Cells[10].Text = FL.ToString();
                //e.Row.Cells[11].Text = FXL.ToString();
                //e.Row.Cells[12].Text = FXXL.ToString();
                //e.Row.Cells[13].Text = F3XL.ToString();
                //e.Row.Cells[14].Text = F4XL.ToString();

                //e.Row.Cells[15].Text = H30.ToString();
                //e.Row.Cells[16].Text = H32.ToString();
                //e.Row.Cells[17].Text = H34.ToString();
                //e.Row.Cells[18].Text = H36.ToString();
                //e.Row.Cells[19].Text = HXS.ToString();
                //e.Row.Cells[20].Text = HS.ToString();
                //e.Row.Cells[21].Text = HM.ToString();
                //e.Row.Cells[22].Text = HL.ToString();
                //e.Row.Cells[23].Text = HXL.ToString();
                //e.Row.Cells[24].Text = HXXL.ToString();
                //e.Row.Cells[25].Text = H3XL.ToString();
                //e.Row.Cells[26].Text = H3XL.ToString();

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

                e.Row.Cells[4].Text = "TOTAL :";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Font.Bold = true;

                e.Row.Cells[33].Text = TOTAL.ToString();
                e.Row.Cells[33].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[33].Font.Bold = true;


                #endregion
            }
        }

        protected void gridnewprint_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }

        protected void gridnewprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }

        }

        protected void Print(object sender, EventArgs e)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
            GridView1.Attributes["style"] = "border-collapse:separate";
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
                {
                    row.Attributes["style"] = "page-break-after:always;";
                }
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
            sb.Append(style + gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();");
            sb.Append("};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

            // ScriptManager.RegisterStartupScript(this, typeof(Page), "GridPrint", "myFunction();", true);

            GridView1.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }
    }
}