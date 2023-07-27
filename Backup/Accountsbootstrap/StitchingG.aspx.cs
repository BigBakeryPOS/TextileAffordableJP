using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;


namespace Billing.Accountsbootstrap
{
    public partial class StitchingG : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string CmpyId = "";
        double Q30F = 0; double Q32F = 0; double Q34F = 0; double Q36F = 0;
        double QXSF = 0; double QSF = 0; double QMF = 0; double QLF = 0;
        double QXLF = 0; double QXXLF = 0; double Q3XLF = 0; double Q4XLF = 0;

        double Q30H = 0; double Q32H = 0; double Q34H = 0; double Q36H = 0;
        double QXSH = 0; double QSH = 0; double QMH = 0; double QLH = 0;
        double QXLH = 0; double QXXLH = 0; double Q3XLH = 0; double Q4XLH = 0;
        double QttlFH = 0; double GVtotalshirt = 0;


        double DQ30F = 0; double DQ32F = 0; double DQ34F = 0; double DQ36F = 0;
        double DQXSF = 0; double DQSF = 0; double DQMF = 0; double DQLF = 0;
        double DQXLF = 0; double DQXXLF = 0; double DQ3XLF = 0; double DQ4XLF = 0;

        double DQ30H = 0; double DQ32H = 0; double DQ34H = 0; double DQ36H = 0;
        double DQXSH = 0; double DQSH = 0; double DQMH = 0; double DQLH = 0;
        double DQXLH = 0; double DQXXLH = 0; double DQ3XLH = 0; double DQ4XLH = 0;
        double DQttlFH = 0; double DGVtotalshirt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");



            string Stitchingid = Request.QueryString.Get("PrintIdForAll");
            string Name = Request.QueryString.Get("ScreenName");

            sTableName = Session["User"].ToString();
            DataSet ds23 = new DataSet();
            if (Stitchingid != null)
            {
                tabledummy.Visible = true;
                DataSet ds2 = new DataSet();
                string TableName = "";

                #region
                if (Name == "Stitch")
                {
                    gridrawmaterial.Visible = true;
                    ds2 = objBs.JpStichingforall("tblJpStiching", "StichingId", Stitchingid);
                    lblheadingname.Text = "DELIVERY NOTE - ISSUE ";
                    lblheadingname1.Text = "DELIVERY NOTE - RECEIVE ";
                    TableName = "tblJpStiching";
                    lbllContrasts.Text = "Contrast :";
                    lblprocessname.Text = "STITCHING";
                    lblprocessname1.Text = "STITCHING";
                    lbljwhead.Text = "STITCHING BY";
                }
                else if (Name == "Emb")
                {
                    ds2 = objBs.JpStichingforall("tblJpEmbroiding", "EmbroidingId", Stitchingid);
                    lblheadingname.Text = "DELIVERY NOTE - ISSUE ";
                    lblheadingname1.Text = "DELIVERY NOTE - RECEIVE  ";
                    TableName = "tblJpEmbroiding";
                    lbllContrasts.Text = "Narration :";
                    lblprocessname.Text = "EMBROIDING";
                    lblprocessname1.Text = "EMBROIDING";
                    lbljwhead.Text = "EMBROIDING BY";
                }
                else if (Name == "Kaja")
                {
                    ds2 = objBs.JpStichingforall("tblJpKajaButton", "KajaButtonId", Stitchingid);
                    lblheadingname.Text = "DELIVERY NOTE - ISSUE ";
                    lblheadingname1.Text = "DELIVERY NOTE - RECEIVE  ";
                    TableName = "tblJpKajaButton";
                    lbllContrasts.Text = "Narration :";
                    lblprocessname.Text = "KAJA";
                    lblprocessname1.Text = "KAJA";
                }
                else if (Name == "Print")
                {
                    ds2 = objBs.JpStichingforall("tblJpPrinting", "PrintingId", Stitchingid);
                    lblheadingname.Text = "DELIVERY NOTE - ISSUE ";
                    lblheadingname1.Text = "DELIVERY NOTE - RECEIVE  ";
                    TableName = "tblJpPrinting";
                    lbllContrasts.Text = "Narration :";
                    lblprocessname.Text = "PRINTING";
                    lblprocessname1.Text = "PRINTING";
                    lbljwhead.Text = "PRINTING BY";
                }
                else if (Name == "Wash")
                {
                    ds2 = objBs.JpStichingforall("tblJpWashing", "WashingId", Stitchingid);
                    lblheadingname.Text = "DELIVERY NOTE - ISSUE ";
                    lblheadingname1.Text = "DELIVERY NOTE - RECEIVE  ";
                    TableName = "tblJpWashing";
                    lbllContrasts.Text = "Narration :";
                    lblprocessname.Text = "WASHING";
                    lblprocessname1.Text = "WASHING";
                }
                else if (Name == "Bartag")
                {
                    ds2 = objBs.JpStichingforall("tblJpBarTag", "BarTagId", Stitchingid);
                    lblheadingname.Text = "DELIVERY NOTE - ISSUE ";
                    lblheadingname1.Text = "DELIVERY NOTE - RECEIVE ";
                    TableName = "tblJpBarTag";
                    lbllContrasts.Text = "Narration :";
                }
                else if (Name == "Trim")
                {
                    ds2 = objBs.JpStichingforall("tblJpTrimming", "TrimmingId", Stitchingid);
                    lblheadingname.Text = "DELIVERY NOTE ISSUE -";
                    lblheadingname1.Text = "DELIVERY NOTE - RECEIVE  ";
                    TableName = "tblJpTrimming";
                    lbllContrasts.Text = "Narration :";
                }
                else if (Name == "Consai")
                {
                    ds2 = objBs.JpStichingforall("tblJpConsai", "ConsaiId", Stitchingid);
                    lblheadingname.Text = "DELIVERY NOTE ISSUE -";
                    lblheadingname1.Text = "DELIVERY NOTE ISSUE -";
                    TableName = "tblJpConsai";
                    lbllContrasts.Text = "Narration :";
                }



                #endregion


                #region
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblitemnarrations.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();
                    lblitemnarrations1.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();

                    lblLot.Text = ds2.Tables[0].Rows[0]["Companylotno"].ToString();
                    lblworkorder.Text = ds2.Tables[0].Rows[0]["idd"].ToString();
                    lblwrkorderrec.Text = ds2.Tables[0].Rows[0]["idd"].ToString();
                    DataSet getitemdetails = objBs.getitemidinprecutforitem(lblLot.Text);
                    if (getitemdetails.Tables[0].Rows.Count > 0)
                    {
                        lblsleeve.Text = getitemdetails.Tables[0].Rows[0]["sleevetypec"].ToString();
                        lbllabel.Text = getitemdetails.Tables[0].Rows[0]["labeltypec"].ToString();

                        lblsleeve1.Text = getitemdetails.Tables[0].Rows[0]["sleevetypec"].ToString();
                        lbllabel1.Text = getitemdetails.Tables[0].Rows[0]["labeltypec"].ToString();

                        lblbitemname.Text = getitemdetails.Tables[0].Rows[0]["Itemname"].ToString() + '(' + getitemdetails.Tables[0].Rows[0]["Itemcode"].ToString()+ ')';
                        lblbitemname1.Text = getitemdetails.Tables[0].Rows[0]["Itemname"].ToString() + '(' + getitemdetails.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                    }

                    lblLot1.Text = ds2.Tables[0].Rows[0]["Companylotno"].ToString();
                    //DataSet getitemname = objBs.getitenameforprocess(lblLot.Text, "", "");
                    //if (getitemname.Tables[0].Rows.Count > 0)
                    //{
                    //    lblitemname.Text = getitemname.Tables[0].Rows[0]["itemname"].ToString();
                    //}
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    lblDeldate1.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    lblLedgerName.Text = ds2.Tables[0].Rows[0]["name"].ToString();
                    lbljwname.Text = ds2.Tables[0].Rows[0]["name"].ToString();
                    lblLedgerName1.Text = ds2.Tables[0].Rows[0]["name"].ToString();

                    lblgastin.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString(); //"33MMMKPR48YFHZK";
                    lblgastin1.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString(); //"33MMMKPR48YFHZK";
                    lblPaidAmount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["PaidAmount"]).ToString("f2");
                    lblTotalAmount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");
                    lblbrand.Text = ds2.Tables[0].Rows[0]["BrandName"].ToString();

                    CmpyId = ds2.Tables[0].Rows[0]["Companyid"].ToString();

                    if (lbllContrasts.Text == "Narration :")
                    {
                        lblContrasts.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                    }


                    DataSet dsuniquecutid = objBs.Getuniquecutid(Convert.ToInt32(ds2.Tables[0].Rows[0]["LotDetailId"].ToString()), TableName);
                    if (dsuniquecutid.Tables[0].Rows.Count > 0)
                    {
                        lbllscutno.Text = dsuniquecutid.Tables[0].Rows[0]["Cutid"].ToString();
                        lbllscutno1.Text = dsuniquecutid.Tables[0].Rows[0]["Cutid"].ToString();
                        lblcutmaster.Text = dsuniquecutid.Tables[0].Rows[0]["LedgerName"].ToString();
                        lblmodel.Text = dsuniquecutid.Tables[0].Rows[0]["Narration"].ToString();

                        lblCompination.Text = dsuniquecutid.Tables[0].Rows[0]["LotCombination"].ToString();
                    }

                    DataSet stichingsnoEmb = objBs.Getstichingsnoemb(Convert.ToInt32(ds2.Tables[0].Rows[0]["LotDetailId"].ToString()), TableName);
                    if (stichingsnoEmb.Tables[0].Rows.Count > 0)
                    {
                        DataSet dscontras = objBs.Getstichingsnocontra(Convert.ToInt32(stichingsnoEmb.Tables[0].Rows[0]["Cutid"].ToString()));
                        lblsample.Text = dscontras.Tables[0].Rows[0]["Sample"].ToString();
                        if (lbllContrasts.Text == "Narration :")
                        {
                            // lblContrasts.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                        }
                        else
                        {
                            lblContrasts.Text = dscontras.Tables[0].Rows[0]["Contrasts"].ToString();

                        }

                        lblEmbroidery.Text = "Yes";

                    }
                    else
                    {
                        lblEmbroidery.Text = "No";
                    }

                    DataSet stichingsnoEmbWash = objBs.Getstichingsnowash(Convert.ToInt32(ds2.Tables[0].Rows[0]["LotDetailId"].ToString()), TableName);
                    if (stichingsnoEmbWash.Tables[0].Rows.Count > 0)
                    {
                        DataSet dscontras = objBs.Getstichingsnocontra(Convert.ToInt32(stichingsnoEmbWash.Tables[0].Rows[0]["Cutid"].ToString()));
                        lblsample.Text = dscontras.Tables[0].Rows[0]["Sample"].ToString();
                        if (lbllContrasts.Text == "Narration :")
                        {
                            // lblContrasts.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                        }
                        else
                        {
                            lblContrasts.Text = dscontras.Tables[0].Rows[0]["Contrasts"].ToString();

                        }

                        lblWash.Text = "Yes";

                    }
                    else
                    {
                        lblWash.Text = "No";
                    }

                    DataSet drawmater = objBs.getusedrawmaterials(ds2.Tables[0].Rows[0]["LotDetailId"].ToString());
                    if (drawmater.Tables[0].Rows.Count > 0)
                    {
                        gridrawmaterial.DataSource = drawmater;
                        gridrawmaterial.DataBind();
                    }
                    else
                    {
                        gridrawmaterial.DataSource = null;
                        gridrawmaterial.DataBind();
                    }



                    // ds23 = objBs.Get_JpStichingneww(Convert.ToString(Stitchingid), "3", "Stc");
                    #region
                    if (Name == "Stitch")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpStiching", "tbltransjpstichinghistory", "StichingId", "Issue", Stitchingid, "3", "Stc");
                    }
                    else if (Name == "Emb")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpEmbroiding", "tbltransjpEmbroidinghistory", "EmbroidingId", "Issue", Stitchingid, "4", "Emb");
                    }
                    else if (Name == "Kaja")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpKajaButton", "tbltransjpKajaButtonhistory", "KajaButtonId", "Issue", Stitchingid, "2", "Kaja");
                    }
                    else if (Name == "Print")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpPrinting", "tbltransjpPrintinghistory", "PrintingId", "Issue", Stitchingid, "8", "Print");
                    }
                    else if (Name == "Wash")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpWashing", "tbltransjpWashinghistory", "WashingId", "Issue", Stitchingid, "5", "Wash");
                    }
                    else if (Name == "Bartag")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpBarTag", "tbltransjpBarTaghistory", "BarTagId", "Issue", Stitchingid, "9", "Btag");
                    }
                    else if (Name == "Trim")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpTrimming", "tbltransjpTrimminghistory", "TrimmingId", "Issue", Stitchingid, "10", "Trm");
                    }
                    else if (Name == "Consai")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpConsai", "tbltransjpConsaihistory", "ConsaiId", "Issue", Stitchingid, "11", "Cni");
                    }



                    #endregion

                    if (ds23.Tables[0].Rows.Count > 0)
                    {
                        lbllfit.Text = ds23.Tables[0].Rows[0]["fit"].ToString();
                        lbllrate.Text = Convert.ToDouble(ds23.Tables[0].Rows[0]["rate"]).ToString("f2");

                        GridView1.DataSource = ds23;
                        GridView1.DataBind();

                        griddummy.DataSource = ds23;
                        griddummy.DataBind();
                    }



                }

                #endregion

            }

            DataSet Damage = new DataSet();
            string Stitchingidrec = Request.QueryString.Get("PrintIdForAllrec");
            string Namerec = Request.QueryString.Get("ScreenName");
            if (Stitchingidrec != null)
            {

                tabledummy.Visible = false;
                DataSet ds2 = new DataSet();
                string TableName = "";
                #region
                if (Name == "Stitch")
                {
                    ds2 = objBs.JpStichingforall("tblJpStiching", "StichingId", Stitchingidrec);
                    lblheadingname.Text = "DELIVERY NOTE RECEIVE";
                    lblprocessname.Text = "STITCHING";
                    TableName = "tblJpStiching";
                    lbllContrasts.Text = "Contrast :";
                }
                else if (Name == "Emb")
                {
                    ds2 = objBs.JpStichingforall("tblJpEmbroiding", "EmbroidingId", Stitchingidrec);
                    lblheadingname.Text = "DELIVERY NOTE RECEIVE";
                    TableName = "tblJpEmbroiding";
                    lblprocessname.Text = "EMBROIDERY";
                    lbllContrasts.Text = "Narration :";
                }
                else if (Name == "Kaja")
                {
                    ds2 = objBs.JpStichingforall("tblJpKajaButton", "KajaButtonId", Stitchingidrec);
                    lblheadingname.Text = "KajaButton Received WorkOrder -";
                    TableName = "tblJpKajaButton";
                    lblprocessname.Text = "KAJA";
                    lbllContrasts.Text = "Narration :";
                }
                else if (Name == "Print")
                {
                    ds2 = objBs.JpStichingforall("tblJpPrinting", "PrintingId", Stitchingidrec);
                    lblheadingname.Text = "DELIVERY NOTE RECEIVE";
                    TableName = "tblJpPrinting";
                    lblprocessname.Text = "PRINTING";
                    lbllContrasts.Text = "Narration :";
                }
                else if (Name == "Wash")
                {
                    ds2 = objBs.JpStichingforall("tblJpWashing", "WashingId", Stitchingidrec);
                    lblheadingname.Text = "Washing Received WorkOrder -";
                    TableName = "tblJpWashing";
                    lblprocessname.Text = "WASHING";
                    lbllContrasts.Text = "Narration :";
                }
                else if (Name == "Bartag")
                {
                    ds2 = objBs.JpStichingforall("tblJpBarTag", "BarTagId", Stitchingidrec);
                    lblheadingname.Text = "BarTag Received WorkOrder -";
                    TableName = "tblJpBarTag";
                    lbllContrasts.Text = "Narration :";
                }
                else if (Name == "Trim")
                {
                    ds2 = objBs.JpStichingforall("tblJpTrimming", "TrimmingId", Stitchingidrec);
                    lblheadingname.Text = "Trimming Received WorkOrder -";
                    TableName = "tblJpTrimming";
                    lbllContrasts.Text = "Narration :";
                }
                else if (Name == "Consai")
                {
                    ds2 = objBs.JpStichingforall("tblJpConsai", "ConsaiId", Stitchingidrec);
                    lblheadingname.Text = "Consai Received WorkOrder -";
                    TableName = "tblJpConsai";
                    lbllContrasts.Text = "Narration :";
                }



                #endregion


                #region

                if (ds2.Tables[0].Rows.Count > 0)
                {

                    lblitemnarrations.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();
                    lblitemnarrations1.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();

                    lblLot.Text = ds2.Tables[0].Rows[0]["Companylotno"].ToString();
                    lblworkorder.Text = ds2.Tables[0].Rows[0]["idd"].ToString();
                    DataSet getitemdetails = objBs.getitemidinprecutforitem(lblLot.Text);
                    if (getitemdetails.Tables[0].Rows.Count > 0)
                    {
                        lblsleeve.Text = getitemdetails.Tables[0].Rows[0]["sleevetypec"].ToString();
                        lbllabel.Text = getitemdetails.Tables[0].Rows[0]["labeltypec"].ToString();
                        lblbitemname.Text = getitemdetails.Tables[0].Rows[0]["Itemname"].ToString() + '(' + getitemdetails.Tables[0].Rows[0]["Itemcode"].ToString() + ')';


                        lblsleeve1.Text = getitemdetails.Tables[0].Rows[0]["sleevetypec"].ToString();
                        lbllabel1.Text = getitemdetails.Tables[0].Rows[0]["labeltypec"].ToString();
                        lblbitemname1.Text = getitemdetails.Tables[0].Rows[0]["Itemname"].ToString() + '(' + getitemdetails.Tables[0].Rows[0]["Itemcode"].ToString() + ')';

                    }
                    lblLot1.Text = ds2.Tables[0].Rows[0]["Companylotno"].ToString();
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    lblDeldate1.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    lblLedgerName.Text = ds2.Tables[0].Rows[0]["name"].ToString();
                    lblLedgerName1.Text = ds2.Tables[0].Rows[0]["name"].ToString();

                    lblgastin.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString(); //"33MMMKPR48YFHZK";
                    lblgastin1.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString(); //"33MMMKPR48YFHZK";

                    lblPaidAmount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["PaidAmount"]).ToString("f2");
                    lblTotalAmount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");
                    lblbrand.Text = ds2.Tables[0].Rows[0]["BrandName"].ToString();

                    CmpyId = ds2.Tables[0].Rows[0]["Companyid"].ToString();

                    if (lbllContrasts.Text == "Narration :")
                    {
                        lblContrasts.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                    }


                    DataSet dsuniquecutid = objBs.Getuniquecutid(Convert.ToInt32(ds2.Tables[0].Rows[0]["LotDetailId"].ToString()), TableName);
                    if (dsuniquecutid.Tables[0].Rows.Count > 0)
                    {
                        lbllscutno.Text = dsuniquecutid.Tables[0].Rows[0]["Cutid"].ToString();
                        lbllscutno1.Text = dsuniquecutid.Tables[0].Rows[0]["Cutid"].ToString();
                        lblcutmaster.Text = dsuniquecutid.Tables[0].Rows[0]["LedgerName"].ToString();
                        lblmodel.Text = dsuniquecutid.Tables[0].Rows[0]["Narration"].ToString();
                        lblCompination.Text = dsuniquecutid.Tables[0].Rows[0]["LotCombination"].ToString();
                    }

                    DataSet stichingsnoEmb = objBs.Getstichingsnoemb(Convert.ToInt32(ds2.Tables[0].Rows[0]["LotDetailId"].ToString()), TableName);
                    if (stichingsnoEmb.Tables[0].Rows.Count > 0)
                    {
                        DataSet dscontras = objBs.Getstichingsnocontra(Convert.ToInt32(stichingsnoEmb.Tables[0].Rows[0]["Cutid"].ToString()));
                        lblsample.Text = dscontras.Tables[0].Rows[0]["Sample"].ToString();
                        if (lbllContrasts.Text == "Narration :")
                        {
                            // lblContrasts.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                        }
                        else
                        {
                            lblContrasts.Text = dscontras.Tables[0].Rows[0]["Contrasts"].ToString();

                        }

                        lblEmbroidery.Text = "Yes";

                    }
                    else
                    {
                        lblEmbroidery.Text = "No";
                    }

                    DataSet stichingsnoEmbWash = objBs.Getstichingsnowash(Convert.ToInt32(ds2.Tables[0].Rows[0]["LotDetailId"].ToString()), TableName);
                    if (stichingsnoEmbWash.Tables[0].Rows.Count > 0)
                    {
                        DataSet dscontras = objBs.Getstichingsnocontra(Convert.ToInt32(stichingsnoEmbWash.Tables[0].Rows[0]["Cutid"].ToString()));
                        lblsample.Text = dscontras.Tables[0].Rows[0]["Sample"].ToString();
                        if (lbllContrasts.Text == "Narration :")
                        {
                            // lblContrasts.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();
                        }
                        else
                        {
                            lblContrasts.Text = dscontras.Tables[0].Rows[0]["Contrasts"].ToString();

                        }

                        lblWash.Text = "Yes";
                    }
                    else
                    {
                        lblWash.Text = "No";
                    }



                    // ds23 = objBs.Get_JpStichingnewwReceive(Convert.ToString(Stitchingidrec), "3", "Stc");
                    #region
                    if (Namerec == "Stitch")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpStiching", "tbltransjpstichinghistory", "StichingId", "Receive", Stitchingidrec, "3", "Stc");
                    }
                    else if (Namerec == "Emb")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpEmbroiding", "tbltransjpEmbroidinghistory", "EmbroidingId", "Receive", Stitchingidrec, "4", "Emb");
                    }
                    else if (Namerec == "Kaja")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpKajaButton", "tbltransjpKajaButtonhistory", "KajaButtonId", "Receive", Stitchingidrec, "2", "Kaja");
                    }
                    else if (Namerec == "Print")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpPrinting", "tbltransjpPrintinghistory", "PrintingId", "Receive", Stitchingidrec, "8", "Print");
                    }
                    else if (Namerec == "Wash")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpWashing", "tbltransjpWashinghistory", "WashingId", "Receive", Stitchingidrec, "5", "Wash");
                    }
                    else if (Namerec == "Bartag")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpBarTag", "tbltransjpBarTaghistory", "BarTagId", "Receive", Stitchingidrec, "9", "Btag");
                    }
                    else if (Namerec == "Trim")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpTrimming", "tbltransjpTrimminghistory", "TrimmingId", "Receive", Stitchingidrec, "10", "Trm");
                    }
                    else if (Namerec == "Consai")
                    {
                        ds23 = objBs.Get_JpStichingnewwall("tblJpConsai", "tbltransjpConsaihistory", "ConsaiId", "Receive", Stitchingidrec, "11", "Cni");
                    }



                    #endregion
                    if (ds23.Tables[0].Rows.Count > 0)
                    {
                        lbllfit.Text = ds23.Tables[0].Rows[0]["fit"].ToString();
                        lbllrate.Text = Convert.ToDouble(ds23.Tables[0].Rows[0]["rate"]).ToString("f2");

                        GridView1.DataSource = ds23;
                        GridView1.DataBind();

                        griddummy.DataSource = ds23;
                        griddummy.DataBind();
                    }

                    // Damage = objBs.Get_JpStichingnewwDamage(Convert.ToString(Stitchingidrec), "3", "Stc");
                    #region
                    if (Namerec == "Stitch")
                    {
                        Damage = objBs.Get_JpStichingnewwall("tblJpStiching", "tbltransjpstichinghistory", "StichingId", "Damage", Stitchingidrec, "3", "Stc");
                    }
                    else if (Namerec == "Emb")
                    {
                        Damage = objBs.Get_JpStichingnewwall("tblJpEmbroiding", "tbltransjpEmbroidinghistory", "EmbroidingId", "Damage", Stitchingidrec, "4", "Emb");
                    }
                    else if (Namerec == "Print")
                    {
                        Damage = objBs.Get_JpStichingnewwall("tblJpKajaButton", "tbltransjpKajaButtonhistory", "KajaButtonId", "Damage", Stitchingidrec, "2", "Kaja");
                    }
                    else if (Namerec == "Kaja")
                    {
                        Damage = objBs.Get_JpStichingnewwall("tblJpPrinting", "tbltransjpPrintinghistory", "PrintingId", "Damage", Stitchingidrec, "8", "Print");
                    }
                    else if (Namerec == "Wash")
                    {
                        Damage = objBs.Get_JpStichingnewwall("tblJpWashing", "tbltransjpWashinghistory", "WashingId", "Damage", Stitchingidrec, "5", "Wash");
                    }
                    else if (Namerec == "BarTag")
                    {
                        Damage = objBs.Get_JpStichingnewwall("tblJpBarTag", "tbltransjpBarTaghistory", "BarTagId", "Damage", Stitchingidrec, "9", "Btag");
                    }
                    else if (Namerec == "Trim")
                    {
                        Damage = objBs.Get_JpStichingnewwall("tblJpTrimming", "tbltransjpTrimminghistory", "TrimmingId", "Damage", Stitchingidrec, "10", "Trm");
                    }
                    else if (Namerec == "Consai")
                    {
                        Damage = objBs.Get_JpStichingnewwall("tblJpConsai", "tbltransjpConsaihistory", "ConsaiId", "Damage", Stitchingidrec, "11", "Cni");
                    }



                    #endregion
                    if (Damage.Tables.Count > 0)
                    {
                        if (Damage.Tables[0].Rows.Count > 0)
                        {
                            lbllfit.Text = Damage.Tables[0].Rows[0]["fit"].ToString();
                            lbllrate.Text = Convert.ToDouble(Damage.Tables[0].Rows[0]["rate"]).ToString("f2");

                            gvdamage.DataSource = Damage;
                            gvdamage.DataBind();
                        }
                    }

                }

                #endregion

            }

            if (ds23.Tables[0].Rows.Count > 0)
            {
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



                griddummy.Columns[6].Visible = false;
                griddummy.Columns[7].Visible = false;
                griddummy.Columns[8].Visible = false;
                griddummy.Columns[9].Visible = false;
                griddummy.Columns[10].Visible = false;
                griddummy.Columns[11].Visible = false;
                griddummy.Columns[12].Visible = false;
                griddummy.Columns[13].Visible = false;
                griddummy.Columns[14].Visible = false;
                griddummy.Columns[15].Visible = false;
                griddummy.Columns[16].Visible = false;
                griddummy.Columns[17].Visible = false;

                griddummy.Columns[18].Visible = false;
                griddummy.Columns[19].Visible = false;
                griddummy.Columns[20].Visible = false;
                griddummy.Columns[21].Visible = false;
                griddummy.Columns[22].Visible = false;
                griddummy.Columns[23].Visible = false;
                griddummy.Columns[24].Visible = false;
                griddummy.Columns[25].Visible = false;
                griddummy.Columns[26].Visible = false;
                griddummy.Columns[27].Visible = false;
                griddummy.Columns[28].Visible = false;
                griddummy.Columns[29].Visible = false;


                for (int j = 0; j < ds23.Tables[0].Rows.Count; j++)
                {

                    #region
                    string S30 = ds23.Tables[0].Rows[j]["30FS"].ToString();
                    string S32 = ds23.Tables[0].Rows[j]["32FS"].ToString();
                    string S34 = ds23.Tables[0].Rows[j]["34FS"].ToString();
                    string S36 = ds23.Tables[0].Rows[j]["36FS"].ToString();
                    string SXS = ds23.Tables[0].Rows[j]["xsFS"].ToString();
                    string SS = ds23.Tables[0].Rows[j]["sFS"].ToString();
                    string SM = ds23.Tables[0].Rows[j]["mFS"].ToString();
                    string SL = ds23.Tables[0].Rows[j]["lFS"].ToString();
                    string SXL = ds23.Tables[0].Rows[j]["xlFS"].ToString();
                    string SXXL = ds23.Tables[0].Rows[j]["xxlFS"].ToString();
                    string S3XL = ds23.Tables[0].Rows[j]["3xlFS"].ToString();
                    string S4XL = ds23.Tables[0].Rows[j]["4xlFS"].ToString();


                    string HS30 = ds23.Tables[0].Rows[j]["30HS"].ToString();
                    string HS32 = ds23.Tables[0].Rows[j]["32HS"].ToString();
                    string HS34 = ds23.Tables[0].Rows[j]["34HS"].ToString();
                    string HS36 = ds23.Tables[0].Rows[j]["36HS"].ToString();
                    string HSXS = ds23.Tables[0].Rows[j]["xsHS"].ToString();
                    string HSS = ds23.Tables[0].Rows[j]["sHS"].ToString();
                    string HSM = ds23.Tables[0].Rows[j]["mHS"].ToString();
                    string HSL = ds23.Tables[0].Rows[j]["lHS"].ToString();
                    string HSXL = ds23.Tables[0].Rows[j]["xlHS"].ToString();
                    string HSXXL = ds23.Tables[0].Rows[j]["xxlHS"].ToString();
                    string HS3XL = ds23.Tables[0].Rows[j]["3xlHS"].ToString();
                    string HS4XL = ds23.Tables[0].Rows[j]["4xlHS"].ToString();

                    //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                    //////grndtot = grndtot + tot;
                    //////lblstockgrandtot.Text = grndtot.ToString();

                    if (S30 != "0")
                    {

                        GridView1.Columns[6].Visible = true;
                        griddummy.Columns[6].Visible = true;
                    }
                    if (S32 != "0")
                    {

                        GridView1.Columns[7].Visible = true;
                        griddummy.Columns[6].Visible = true;
                    }

                    if (S34 != "0")
                    {

                        GridView1.Columns[8].Visible = true;
                        griddummy.Columns[6].Visible = true;
                    }

                    if (S36 != "0")
                    {

                        GridView1.Columns[9].Visible = true;
                        griddummy.Columns[9].Visible = true;
                    }

                    if (SXS != "0")
                    {

                        GridView1.Columns[10].Visible = true;
                        griddummy.Columns[10].Visible = true;
                    }

                    if (SS != "0")
                    {

                        GridView1.Columns[11].Visible = true;
                        griddummy.Columns[11].Visible = true;
                    }

                    if (SM != "0")
                    {

                        GridView1.Columns[12].Visible = true;
                        griddummy.Columns[12].Visible = true;
                    }

                    if (SL != "0")
                    {

                        GridView1.Columns[13].Visible = true;
                        griddummy.Columns[13].Visible = true;
                    }

                    if (SXL != "0")
                    {

                        GridView1.Columns[14].Visible = true;
                        griddummy.Columns[14].Visible = true;
                    }

                    if (SXXL != "0")
                    {

                        GridView1.Columns[15].Visible = true;
                        griddummy.Columns[15].Visible = true;
                    }

                    if (S3XL != "0")
                    {

                        GridView1.Columns[16].Visible = true;
                        griddummy.Columns[16].Visible = true;
                    }

                    if (S4XL != "0")
                    {

                        GridView1.Columns[17].Visible = true;
                        griddummy.Columns[17].Visible = true;
                    }


                    if (HS30 != "0")
                    {

                        GridView1.Columns[18].Visible = true;
                        griddummy.Columns[18].Visible = true;
                    }
                    if (HS32 != "0")
                    {

                        GridView1.Columns[19].Visible = true;
                        griddummy.Columns[19].Visible = true;
                    }

                    if (HS34 != "0")
                    {

                        GridView1.Columns[20].Visible = true;
                        griddummy.Columns[20].Visible = true;
                    }

                    if (HS36 != "0")
                    {

                        GridView1.Columns[21].Visible = true;
                        griddummy.Columns[21].Visible = true;
                    }

                    if (HSXS != "0")
                    {

                        GridView1.Columns[22].Visible = true;
                        griddummy.Columns[22].Visible = true;
                    }

                    if (HSS != "0")
                    {

                        GridView1.Columns[23].Visible = true;
                        griddummy.Columns[23].Visible = true;
                    }

                    if (HSM != "0")
                    {

                        GridView1.Columns[24].Visible = true;
                        griddummy.Columns[24].Visible = true;
                    }

                    if (HSL != "0")
                    {

                        GridView1.Columns[25].Visible = true;
                        griddummy.Columns[25].Visible = true;
                    }

                    if (HSXL != "0")
                    {

                        GridView1.Columns[26].Visible = true;
                        griddummy.Columns[26].Visible = true;
                    }

                    if (HSXXL != "0")
                    {

                        GridView1.Columns[27].Visible = true;
                        griddummy.Columns[27].Visible = true;
                    }

                    if (HS3XL != "0")
                    {

                        GridView1.Columns[28].Visible = true;
                        griddummy.Columns[28].Visible = true;
                    }

                    if (HS4XL != "0")
                    {

                        GridView1.Columns[29].Visible = true;
                        griddummy.Columns[29].Visible = true;
                    }
                    #endregion

                }


            }


            if (Damage.Tables.Count > 0)
            {
                if (Damage.Tables[0].Rows.Count > 0)
                {

                    gvdamage.Columns[6].Visible = false;
                    gvdamage.Columns[7].Visible = false;
                    gvdamage.Columns[8].Visible = false;
                    gvdamage.Columns[9].Visible = false;
                    gvdamage.Columns[10].Visible = false;
                    gvdamage.Columns[11].Visible = false;
                    gvdamage.Columns[12].Visible = false;
                    gvdamage.Columns[13].Visible = false;
                    gvdamage.Columns[14].Visible = false;
                    gvdamage.Columns[15].Visible = false;
                    gvdamage.Columns[16].Visible = false;
                    gvdamage.Columns[17].Visible = false;

                    gvdamage.Columns[18].Visible = false;
                    gvdamage.Columns[19].Visible = false;
                    gvdamage.Columns[20].Visible = false;
                    gvdamage.Columns[21].Visible = false;
                    gvdamage.Columns[22].Visible = false;
                    gvdamage.Columns[23].Visible = false;
                    gvdamage.Columns[24].Visible = false;
                    gvdamage.Columns[25].Visible = false;
                    gvdamage.Columns[26].Visible = false;
                    gvdamage.Columns[27].Visible = false;
                    gvdamage.Columns[28].Visible = false;
                    gvdamage.Columns[29].Visible = false;


                    for (int j = 0; j < Damage.Tables[0].Rows.Count; j++)
                    {

                        #region
                        string S30 = Damage.Tables[0].Rows[j]["30FS"].ToString();
                        string S32 = Damage.Tables[0].Rows[j]["32FS"].ToString();
                        string S34 = Damage.Tables[0].Rows[j]["34FS"].ToString();
                        string S36 = Damage.Tables[0].Rows[j]["36FS"].ToString();
                        string SXS = Damage.Tables[0].Rows[j]["xsFS"].ToString();
                        string SS = Damage.Tables[0].Rows[j]["sFS"].ToString();
                        string SM = Damage.Tables[0].Rows[j]["mFS"].ToString();
                        string SL = Damage.Tables[0].Rows[j]["lFS"].ToString();
                        string SXL = Damage.Tables[0].Rows[j]["xlFS"].ToString();
                        string SXXL = Damage.Tables[0].Rows[j]["xxlFS"].ToString();
                        string S3XL = Damage.Tables[0].Rows[j]["3xlFS"].ToString();
                        string S4XL = Damage.Tables[0].Rows[j]["4xlFS"].ToString();


                        string HS30 = Damage.Tables[0].Rows[j]["30HS"].ToString();
                        string HS32 = Damage.Tables[0].Rows[j]["32HS"].ToString();
                        string HS34 = Damage.Tables[0].Rows[j]["34HS"].ToString();
                        string HS36 = Damage.Tables[0].Rows[j]["36HS"].ToString();
                        string HSXS = Damage.Tables[0].Rows[j]["xsHS"].ToString();
                        string HSS = Damage.Tables[0].Rows[j]["sHS"].ToString();
                        string HSM = Damage.Tables[0].Rows[j]["mHS"].ToString();
                        string HSL = Damage.Tables[0].Rows[j]["lHS"].ToString();
                        string HSXL = Damage.Tables[0].Rows[j]["xlHS"].ToString();
                        string HSXXL = Damage.Tables[0].Rows[j]["xxlHS"].ToString();
                        string HS3XL = Damage.Tables[0].Rows[j]["3xlHS"].ToString();
                        string HS4XL = Damage.Tables[0].Rows[j]["4xlHS"].ToString();

                        //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                        //////grndtot = grndtot + tot;
                        //////lblstockgrandtot.Text = grndtot.ToString();

                        if (S30 != "0")
                        {

                            gvdamage.Columns[6].Visible = true;
                        }
                        if (S32 != "0")
                        {

                            gvdamage.Columns[7].Visible = true;
                        }

                        if (S34 != "0")
                        {

                            gvdamage.Columns[8].Visible = true;
                        }

                        if (S36 != "0")
                        {

                            gvdamage.Columns[9].Visible = true;
                        }

                        if (SXS != "0")
                        {

                            gvdamage.Columns[10].Visible = true;
                        }

                        if (SS != "0")
                        {

                            gvdamage.Columns[11].Visible = true;
                        }

                        if (SM != "0")
                        {

                            gvdamage.Columns[12].Visible = true;
                        }

                        if (SL != "0")
                        {

                            gvdamage.Columns[13].Visible = true;
                        }

                        if (SXL != "0")
                        {

                            gvdamage.Columns[14].Visible = true;
                        }

                        if (SXXL != "0")
                        {

                            gvdamage.Columns[15].Visible = true;
                        }

                        if (S3XL != "0")
                        {

                            gvdamage.Columns[16].Visible = true;
                        }

                        if (S4XL != "0")
                        {

                            gvdamage.Columns[17].Visible = true;
                        }


                        if (HS30 != "0")
                        {

                            gvdamage.Columns[18].Visible = true;
                        }
                        if (HS32 != "0")
                        {

                            gvdamage.Columns[19].Visible = true;
                        }

                        if (HS34 != "0")
                        {

                            gvdamage.Columns[20].Visible = true;
                        }

                        if (HS36 != "0")
                        {

                            gvdamage.Columns[21].Visible = true;
                        }

                        if (HSXS != "0")
                        {

                            gvdamage.Columns[22].Visible = true;
                        }

                        if (HSS != "0")
                        {

                            gvdamage.Columns[23].Visible = true;
                        }

                        if (HSM != "0")
                        {

                            gvdamage.Columns[24].Visible = true;
                        }

                        if (HSL != "0")
                        {

                            gvdamage.Columns[25].Visible = true;
                        }

                        if (HSXL != "0")
                        {

                            gvdamage.Columns[26].Visible = true;
                        }

                        if (HSXXL != "0")
                        {

                            gvdamage.Columns[27].Visible = true;
                        }

                        if (HS3XL != "0")
                        {

                            gvdamage.Columns[28].Visible = true;
                        }

                        if (HS4XL != "0")
                        {

                            gvdamage.Columns[29].Visible = true;
                        }
                        #endregion

                    }

                }
            }


            if (CmpyId == "3")
            {
                lblmblrpll.Visible = true;
                lblmblbc.Visible = false;
            }
            else
            {
                lblmblrpll.Visible = false;
                lblmblbc.Visible = true;
            }
        }




        protected void btnexit_Click(object sender, EventArgs e)
        {

            string Name = Request.QueryString.Get("ScreenName");

            #region
            if (Name == "Stitch")
            {
                Response.Redirect("StitchingGrid.aspx");
            }
            else if (Name == "Emb")
            {
                Response.Redirect("EmbroidingGrid1.aspx");
            }
            else if (Name == "Kaja")
            {
                Response.Redirect("KajaButtonGrid.aspx");
            }
            else if (Name == "Print")
            {
                Response.Redirect("PrintingGrid1.aspx");
            }
            else if (Name == "Wash")
            {
                Response.Redirect("WashingGrid.aspx");
            }
            else if (Name == "Bartag")
            {
                Response.Redirect("BarTagGrid.aspx");
            }
            else if (Name == "Trim")
            {
                Response.Redirect("TrimmingGrid.aspx");
            }
            else if (Name == "Consai")
            {
                Response.Redirect("ConsaiGrid.aspx");
            }



            #endregion
        }

        protected void gvnewstiching_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (DataBinder.Eval(e.Row.DataItem, "30fs") != "-" && DataBinder.Eval(e.Row.DataItem, "30fs") != "")
                {
                    Q30F = Q30F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "32fs") != "-" && DataBinder.Eval(e.Row.DataItem, "32fs") != "")
                {
                    Q32F = Q32F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "34fs") != "-" && DataBinder.Eval(e.Row.DataItem, "34fs") != "")
                {
                    Q34F = Q34F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "36fs") != "-" && DataBinder.Eval(e.Row.DataItem, "36fs") != "")
                {
                    Q36F = Q36F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xsfs") != "-" && DataBinder.Eval(e.Row.DataItem, "xsfs") != "")
                {
                    QXSF = QXSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xsfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "sfs") != "-" && DataBinder.Eval(e.Row.DataItem, "sfs") != "")
                {
                    QSF = QSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "sfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "mfs") != "-" && DataBinder.Eval(e.Row.DataItem, "mfs") != "")
                {
                    QMF = QMF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "mfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "lfs") != "-" && DataBinder.Eval(e.Row.DataItem, "lfs") != "")
                {
                    QLF = QLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "lfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "xlfs") != "")
                {
                    QXLF = QXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xxlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "xxlfs") != "")
                {
                    QXXLF = QXXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xxlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "3xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "3xlfs") != "")
                {
                    Q3XLF = Q3XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "4xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "4xlfs") != "")
                {
                    Q4XLF = Q4XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "30hs") != "-" && DataBinder.Eval(e.Row.DataItem, "30hs") != "")
                {

                    Q30H = Q30H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "32hs") != "-" && DataBinder.Eval(e.Row.DataItem, "32hs") != "")
                {
                    Q32H = Q32H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "34hs") != "-" && DataBinder.Eval(e.Row.DataItem, "34hs") != "")
                {
                    Q34H = Q34H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "36hs") != "-" && DataBinder.Eval(e.Row.DataItem, "36hs") != "")
                {
                    Q36H = Q36H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xshs") != "-" && DataBinder.Eval(e.Row.DataItem, "xshs") != "")
                {
                    QXSH = QXSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xshs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "shs") != "-" && DataBinder.Eval(e.Row.DataItem, "shs") != "")
                {
                    QSH = QSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "shs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "mhs") != "-" && DataBinder.Eval(e.Row.DataItem, "mhs") != "")
                {
                    QMH = QMH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "mhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "lhs") != "-" && DataBinder.Eval(e.Row.DataItem, "lhs") != "")
                {
                    QLH = QLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "lhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "xlhs") != "")
                {
                    QXLH = QXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xxlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "xxlhs") != "")
                {
                    QXXLH = QXXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xxlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "3xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "3xlhs") != "")
                {
                    Q3XLH = Q3XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3xlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "4xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "4xlhs") != "")
                {
                    Q4XLH = Q4XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4xlhs"));
                }


                GVtotalshirt = GVtotalshirt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "GrandTtl"));
                // QttlFH = QttlFH + Q30F + Q32F + Q34F + Q36F + QXSF + QSF + QMF + QLF + QXLF + QXXLF + Q3XLF + Q4XLF + Q30H + Q32H + Q34H + Q36H + QXSH + QSH + QMH + QLH + QXLH + QXXLH + Q3XLH + Q4XLH;



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
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // e.Row.Cells[6].Text = GVtotalshirt.ToString();
                e.Row.Cells[1].Text = "Total:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = Q30F.ToString();
                e.Row.Cells[7].Text = Q32F.ToString();
                e.Row.Cells[8].Text = Q34F.ToString();
                e.Row.Cells[9].Text = Q36F.ToString();
                e.Row.Cells[10].Text = QXSF.ToString();
                e.Row.Cells[11].Text = QSF.ToString();
                e.Row.Cells[12].Text = QMF.ToString();
                e.Row.Cells[13].Text = QLF.ToString();
                e.Row.Cells[14].Text = QXLF.ToString();
                e.Row.Cells[15].Text = QXXLF.ToString();
                e.Row.Cells[16].Text = Q3XLF.ToString();
                e.Row.Cells[17].Text = Q4XLF.ToString();

                e.Row.Cells[18].Text = Q30H.ToString();
                e.Row.Cells[19].Text = Q32H.ToString();
                e.Row.Cells[20].Text = Q34H.ToString();
                e.Row.Cells[21].Text = Q36H.ToString();
                e.Row.Cells[22].Text = QXSH.ToString();
                e.Row.Cells[23].Text = QSH.ToString();
                e.Row.Cells[24].Text = QMH.ToString();
                e.Row.Cells[25].Text = QLH.ToString();
                e.Row.Cells[26].Text = QXLH.ToString();
                e.Row.Cells[27].Text = QXXLH.ToString();
                e.Row.Cells[28].Text = Q3XLH.ToString();
                e.Row.Cells[29].Text = Q4XLH.ToString();

                e.Row.Cells[30].Text = GVtotalshirt.ToString();

                #region
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;
                e.Row.Cells[9].Font.Bold = true;
                e.Row.Cells[10].Font.Bold = true;
                e.Row.Cells[11].Font.Bold = true;
                e.Row.Cells[12].Font.Bold = true;
                e.Row.Cells[13].Font.Bold = true;
                e.Row.Cells[14].Font.Bold = true;
                e.Row.Cells[15].Font.Bold = true;
                e.Row.Cells[16].Font.Bold = true;
                e.Row.Cells[17].Font.Bold = true;

                e.Row.Cells[18].Font.Bold = true;
                e.Row.Cells[19].Font.Bold = true;
                e.Row.Cells[20].Font.Bold = true;
                e.Row.Cells[21].Font.Bold = true;
                e.Row.Cells[22].Font.Bold = true;
                e.Row.Cells[23].Font.Bold = true;
                e.Row.Cells[24].Font.Bold = true;
                e.Row.Cells[25].Font.Bold = true;
                e.Row.Cells[26].Font.Bold = true;
                e.Row.Cells[27].Font.Bold = true;
                e.Row.Cells[28].Font.Bold = true;
                e.Row.Cells[29].Font.Bold = true;

                e.Row.Cells[30].Font.Bold = true;

                e.Row.Cells[1].Font.Size = 13;
                e.Row.Cells[6].Font.Size = 13;
                e.Row.Cells[7].Font.Size = 13;
                e.Row.Cells[8].Font.Size = 13;
                e.Row.Cells[9].Font.Size = 13;
                e.Row.Cells[10].Font.Size = 13;
                e.Row.Cells[11].Font.Size = 13;
                e.Row.Cells[12].Font.Size = 13;
                e.Row.Cells[13].Font.Size = 13;
                e.Row.Cells[14].Font.Size = 13;
                e.Row.Cells[15].Font.Size = 13;
                e.Row.Cells[16].Font.Size = 13;
                e.Row.Cells[17].Font.Size = 13;

                e.Row.Cells[18].Font.Size = 13;
                e.Row.Cells[19].Font.Size = 13;
                e.Row.Cells[20].Font.Size = 13;
                e.Row.Cells[21].Font.Size = 13;
                e.Row.Cells[22].Font.Size = 13;
                e.Row.Cells[23].Font.Size = 13;
                e.Row.Cells[24].Font.Size = 13;
                e.Row.Cells[25].Font.Size = 13;
                e.Row.Cells[26].Font.Size = 13;
                e.Row.Cells[27].Font.Size = 13;
                e.Row.Cells[28].Font.Size = 13;
                e.Row.Cells[29].Font.Size = 13;

                e.Row.Cells[30].Font.Size = 13;

                #endregion



                lblTotalQty.Text = GVtotalshirt.ToString();



            }
        }


        protected void griddummy_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                

               // if (e.Row.Cells[6].Text == "0")
                {
                    e.Row.Cells[6].Text = "";
                }
                //if (e.Row.Cells[7].Text == "0")
                {
                    e.Row.Cells[7].Text = "";
                }
                //if (e.Row.Cells[8].Text == "0")
                {
                    e.Row.Cells[8].Text = "";
                }
                //if (e.Row.Cells[9].Text == "0")
                {
                    e.Row.Cells[9].Text = "";
                }
                //if (e.Row.Cells[10].Text == "0")
                {
                    e.Row.Cells[10].Text = "";
                }
                //if (e.Row.Cells[11].Text == "0")
                {
                    e.Row.Cells[11].Text = "";
                }
                //if (e.Row.Cells[12].Text == "0")
                {
                    e.Row.Cells[12].Text = "";
                }
                //if (e.Row.Cells[13].Text == "0")
                {
                    e.Row.Cells[13].Text = "";
                }
                //if (e.Row.Cells[14].Text == "0")
                {
                    e.Row.Cells[14].Text = "";
                }

                //if (e.Row.Cells[15].Text == "0")
                {
                    e.Row.Cells[15].Text = "";
                }
                //if (e.Row.Cells[16].Text == "0")
                {
                    e.Row.Cells[16].Text = "";
                }
                //if (e.Row.Cells[17].Text == "0")
                {
                    e.Row.Cells[17].Text = "";
                }
                //if (e.Row.Cells[18].Text == "0")
                {
                    e.Row.Cells[18].Text = "";
                }
                //if (e.Row.Cells[19].Text == "0")
                {
                    e.Row.Cells[19].Text = "";
                }
                //if (e.Row.Cells[20].Text == "0")
                {
                    e.Row.Cells[20].Text = "";
                }
                //if (e.Row.Cells[21].Text == "0")
                {
                    e.Row.Cells[21].Text = "";
                }
                //if (e.Row.Cells[22].Text == "0")
                {
                    e.Row.Cells[22].Text = "";
                }
                //if (e.Row.Cells[23].Text == "0")
                {
                    e.Row.Cells[23].Text = "";
                }
                //if (e.Row.Cells[24].Text == "0")
                {
                    e.Row.Cells[24].Text = "";
                }
                //if (e.Row.Cells[25].Text == "0")
                {
                    e.Row.Cells[25].Text = "";
                }
                //if (e.Row.Cells[26].Text == "0")
                {
                    e.Row.Cells[26].Text = "";
                }
                //if (e.Row.Cells[27].Text == "0")
                {
                    e.Row.Cells[27].Text = "";
                }
                //if (e.Row.Cells[28].Text == "0")
                {
                    e.Row.Cells[28].Text = "";
                }
                //if (e.Row.Cells[29].Text == "0")
                {
                    e.Row.Cells[29].Text = "";
                }

                {
                    e.Row.Cells[30].Text = "";
                }
            }
           
            
        }

        protected void gvdamage_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (DataBinder.Eval(e.Row.DataItem, "30fs") != "-" && DataBinder.Eval(e.Row.DataItem, "30fs") != "")
                {
                    DQ30F = DQ30F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "32fs") != "-" && DataBinder.Eval(e.Row.DataItem, "32fs") != "")
                {
                    DQ32F = DQ32F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "34fs") != "-" && DataBinder.Eval(e.Row.DataItem, "34fs") != "")
                {
                    DQ34F = DQ34F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "36fs") != "-" && DataBinder.Eval(e.Row.DataItem, "36fs") != "")
                {
                    DQ36F = DQ36F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xsfs") != "-" && DataBinder.Eval(e.Row.DataItem, "xsfs") != "")
                {
                    DQXSF = DQXSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xsfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "sfs") != "-" && DataBinder.Eval(e.Row.DataItem, "sfs") != "")
                {
                    DQSF = DQSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "sfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "mfs") != "-" && DataBinder.Eval(e.Row.DataItem, "mfs") != "")
                {
                    DQMF = DQMF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "mfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "lfs") != "-" && DataBinder.Eval(e.Row.DataItem, "lfs") != "")
                {
                    DQLF = DQLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "lfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "xlfs") != "")
                {
                    DQXLF = DQXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xxlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "xxlfs") != "")
                {
                    DQXXLF = DQXXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xxlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "3xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "3xlfs") != "")
                {
                    DQ3XLF = DQ3XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "4xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "4xlfs") != "")
                {
                    DQ4XLF = DQ4XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "30hs") != "-" && DataBinder.Eval(e.Row.DataItem, "30hs") != "")
                {

                    DQ30H = DQ30H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "32hs") != "-" && DataBinder.Eval(e.Row.DataItem, "32hs") != "")
                {
                    DQ32H = DQ32H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "34hs") != "-" && DataBinder.Eval(e.Row.DataItem, "34hs") != "")
                {
                    DQ34H = DQ34H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "36hs") != "-" && DataBinder.Eval(e.Row.DataItem, "36hs") != "")
                {
                    DQ36H = DQ36H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xshs") != "-" && DataBinder.Eval(e.Row.DataItem, "xshs") != "")
                {
                    DQXSH = DQXSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xshs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "shs") != "-" && DataBinder.Eval(e.Row.DataItem, "shs") != "")
                {
                    DQSH = DQSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "shs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "mhs") != "-" && DataBinder.Eval(e.Row.DataItem, "mhs") != "")
                {
                    DQMH = DQMH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "mhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "lhs") != "-" && DataBinder.Eval(e.Row.DataItem, "lhs") != "")
                {
                    DQLH = DQLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "lhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "xlhs") != "")
                {
                    DQXLH = DQXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "xxlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "xxlhs") != "")
                {
                    DQXXLH = DQXXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "xxlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "3xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "3xlhs") != "")
                {
                    DQ3XLH = DQ3XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3xlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "4xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "4xlhs") != "")
                {
                    DQ4XLH = DQ4XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4xlhs"));
                }

                DGVtotalshirt = DGVtotalshirt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "GrandTtl"));
                // QttlFH = QttlFH + Q30F + Q32F + Q34F + Q36F + QXSF + QSF + QMF + QLF + QXLF + QXXLF + Q3XLF + Q4XLF + Q30H + Q32H + Q34H + Q36H + QXSH + QSH + QMH + QLH + QXLH + QXXLH + Q3XLH + Q4XLH;

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
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // e.Row.Cells[6].Text = GVtotalshirt.ToString();

                e.Row.Cells[6].Text = DQ30F.ToString();
                e.Row.Cells[7].Text = DQ32F.ToString();
                e.Row.Cells[8].Text = DQ34F.ToString();
                e.Row.Cells[9].Text = DQ36F.ToString();
                e.Row.Cells[10].Text = DQXSF.ToString();
                e.Row.Cells[11].Text = DQSF.ToString();
                e.Row.Cells[12].Text = DQMF.ToString();
                e.Row.Cells[13].Text = DQLF.ToString();
                e.Row.Cells[14].Text = DQXLF.ToString();
                e.Row.Cells[15].Text = DQXXLF.ToString();
                e.Row.Cells[16].Text = DQ3XLF.ToString();
                e.Row.Cells[17].Text = DQ4XLF.ToString();

                e.Row.Cells[18].Text = DQ30H.ToString();
                e.Row.Cells[19].Text = DQ32H.ToString();
                e.Row.Cells[20].Text = DQ34H.ToString();
                e.Row.Cells[21].Text = DQ36H.ToString();
                e.Row.Cells[22].Text = DQXSH.ToString();
                e.Row.Cells[23].Text = DQSH.ToString();
                e.Row.Cells[24].Text = DQMH.ToString();
                e.Row.Cells[25].Text = DQLH.ToString();
                e.Row.Cells[26].Text = DQXLH.ToString();
                e.Row.Cells[27].Text = DQXXLH.ToString();
                e.Row.Cells[28].Text = DQ3XLH.ToString();
                e.Row.Cells[29].Text = DQ4XLH.ToString();

                e.Row.Cells[30].Text = DGVtotalshirt.ToString();
                // e.Row.Cells[33].Text = QttlFH.ToString();
                // e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
                lblTotalQty.Text = GVtotalshirt.ToString();

                #region
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;
                e.Row.Cells[9].Font.Bold = true;
                e.Row.Cells[10].Font.Bold = true;
                e.Row.Cells[11].Font.Bold = true;
                e.Row.Cells[12].Font.Bold = true;
                e.Row.Cells[13].Font.Bold = true;
                e.Row.Cells[14].Font.Bold = true;
                e.Row.Cells[15].Font.Bold = true;
                e.Row.Cells[16].Font.Bold = true;
                e.Row.Cells[17].Font.Bold = true;

                e.Row.Cells[18].Font.Bold = true;
                e.Row.Cells[19].Font.Bold = true;
                e.Row.Cells[20].Font.Bold = true;
                e.Row.Cells[21].Font.Bold = true;
                e.Row.Cells[22].Font.Bold = true;
                e.Row.Cells[23].Font.Bold = true;
                e.Row.Cells[24].Font.Bold = true;
                e.Row.Cells[25].Font.Bold = true;
                e.Row.Cells[26].Font.Bold = true;
                e.Row.Cells[27].Font.Bold = true;
                e.Row.Cells[28].Font.Bold = true;
                e.Row.Cells[29].Font.Bold = true;

                e.Row.Cells[30].Font.Bold = true;


                e.Row.Cells[1].Font.Size = 13;
                e.Row.Cells[6].Font.Size = 13;
                e.Row.Cells[7].Font.Size = 13;
                e.Row.Cells[8].Font.Size = 13;
                e.Row.Cells[9].Font.Size = 13;
                e.Row.Cells[10].Font.Size = 13;
                e.Row.Cells[11].Font.Size = 13;
                e.Row.Cells[12].Font.Size = 13;
                e.Row.Cells[13].Font.Size = 13;
                e.Row.Cells[14].Font.Size = 13;
                e.Row.Cells[15].Font.Size = 13;
                e.Row.Cells[16].Font.Size = 13;
                e.Row.Cells[17].Font.Size = 13;

                e.Row.Cells[18].Font.Size = 13;
                e.Row.Cells[19].Font.Size = 13;
                e.Row.Cells[20].Font.Size = 13;
                e.Row.Cells[21].Font.Size = 13;
                e.Row.Cells[22].Font.Size = 13;
                e.Row.Cells[23].Font.Size = 13;
                e.Row.Cells[24].Font.Size = 13;
                e.Row.Cells[25].Font.Size = 13;
                e.Row.Cells[26].Font.Size = 13;
                e.Row.Cells[27].Font.Size = 13;
                e.Row.Cells[28].Font.Size = 13;
                e.Row.Cells[29].Font.Size = 13;

                e.Row.Cells[30].Font.Size = 13;

                #endregion
            }
        }

        protected void gridnewprint_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //#region 1

            ////----------start----------//
            //bTotal = false;
            //bool IsSubTotalRowNeedToAdd = false;

            //bool IsGrandTotalRowNeedtoAdd = false;
            //if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") != null))
            //{
            //    if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "design").ToString())
            //    {

            //        IsSubTotalRowNeedToAdd = true;
            //        iCntDesign = intSubTotalIndex;
            //    }

            //}

            //if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") == null))
            //{
            //    IsSubTotalRowNeedToAdd = true;
            //    iCntDesign = intSubTotalIndex;
            //    IsGrandTotalRowNeedtoAdd = true;
            //    intSubTotalIndex = 0;
            //    // iCntDesign = 0;
            //}
            //#region Inserting first Row and populating fist Group Header details
            //if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "design") != null))
            //{
            //    GridView gridPurchase = (GridView)sender;
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    TableCell cell = new TableCell();
            //    cell.Text = "Design Name : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
            //    designcount = DataBinder.Eval(e.Row.DataItem, "design").ToString();
            //    cell.ColumnSpan = 10;
            //    cell.CssClass = "GroupHeaderStyle";
            //    row.Cells.Add(cell);
            //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //    intSubTotalIndex++;
            //    iCntDesign++;
            //}
            //#endregion
            //if (IsSubTotalRowNeedToAdd)
            //{
            //    string iSalesID1 = Request.QueryString.Get("iCutID");
            //    int ddesgin = objBs.getcountforgroup(designcount, Convert.ToInt32(iSalesID1));

            //    #region Adding Sub Total Row
            //    GridView gridPurchase = (GridView)sender;
            //    // Creating a Row          
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    //Adding Total Cell          
            //    TableCell cell = new TableCell();

            //    cell.Text = ">>>>";
            //    cell.HorizontalAlign = HorizontalAlign.Left;
            //    cell.ColumnSpan = 22;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Quantity Column            
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity / ddesgin);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);
            //    //Adding Unit Price Column          
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice / ddesgin);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);
            //    //Adding Discount Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount / ddesgin);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Discount Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalRAte / ddesgin);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding the Row at the RowIndex position in the Grid      
            //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //    iCntDesign = 0;
            //    intSubTotalIndex++;
            //    iCntDesign++;
            //    #endregion
            //    #region Adding Next Group Header Details
            //    if (DataBinder.Eval(e.Row.DataItem, "design") != null)
            //    {
            //        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //        cell = new TableCell();
            //        cell.Text = "Design : " + DataBinder.Eval(e.Row.DataItem, "designno").ToString();
            //        designcount = DataBinder.Eval(e.Row.DataItem, "design").ToString();
            //        cell.ColumnSpan = 9;
            //        cell.CssClass = "GroupHeaderStyle";
            //        row.Cells.Add(cell);
            //        gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //        intSubTotalIndex++;
            //        iCntDesign++;
            //    }
            //    #endregion
            //    #region Reseting the Sub Total Variables
            //    dblSubTotalUnitPrice = 0;
            //    dblSubTotalQuantity = 0;
            //    dblSubTotalDiscount = 0;
            //    dblSubTotalRAte = 0;

            //    #endregion
            //}
            //if (IsGrandTotalRowNeedtoAdd)
            //{
            //    #region Grand Total Row
            //    GridView gridPurchase = (GridView)sender;
            //    // Creating a Row      
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    //Adding Total Cell           
            //    TableCell cell = new TableCell();
            //    //cell.Text = "Grand Total";
            //    //cell.HorizontalAlign = HorizontalAlign.Left;
            //    //cell.ColumnSpan = 6;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);

            //    ////Adding Quantity Column           
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0}", dblGrandTotalQuantity);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);
            //    ////Adding Unit Price Column          
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0.00}", dblGrandTotalUnitPrice);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0.00}", dblGrandTotalDiscount);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);

            //    //Adding the Row at the RowIndex position in the Grid     
            //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
            //    #endregion
            //}

            //#endregion

        }

        protected void gridnewprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "design").ToString();

                //double dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvgMtr").ToString());
                //double dblUnitPrice = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AvgRate").ToString());
                //double dblDiscount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MarginRAte").ToString());
                //double dblrate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MRPRat").ToString());
                //dblSubTotalUnitPrice += dblUnitPrice;
                //dblSubTotalQuantity += dblQuantity;
                //dblSubTotalDiscount += dblDiscount;
                //dblSubTotalRAte += dblrate;
                //dblGrandTotalUnitPrice += dblUnitPrice;
                //dblGrandTotalQuantity += dblQuantity;
                //dblGrandTotalDiscount += dblDiscount;

            }

        }

        protected void btnprint_Click(object sender, EventArgs e)
        {


            string Stitchingid = Request.QueryString.Get("PrintIdForAll");
            string Name = Request.QueryString.Get("ScreenName");
            int Isprint = 0;
            #region

            if (Name == "Stitch")
            {

                Isprint = objBs.IssueIsprint("tblJpStiching", "StichingId", Stitchingid);

            }
            else if (Name == "Emb")
            {
                Isprint = objBs.IssueIsprint("tblJpEmbroiding", "EmbroidingId", Stitchingid);

            }
            else if (Name == "Kaja")
            {
                Isprint = objBs.IssueIsprint("tblJpKajaButton", "KajaButtonId", Stitchingid);

            }
            else if (Name == "Print")
            {
                Isprint = objBs.IssueIsprint("tblJpPrinting", "PrintingId", Stitchingid);

            }
            else if (Name == "Wash")
            {
                Isprint = objBs.IssueIsprint("tblJpWashing", "WashingId", Stitchingid);

            }
            else if (Name == "Bartag")
            {
                Isprint = objBs.IssueIsprint("tblJpBarTag", "BarTagId", Stitchingid);

            }
            else if (Name == "Trim")
            {
                Isprint = objBs.IssueIsprint("tblJpTrimming", "TrimmingId", Stitchingid);

            }
            else if (Name == "Consai")
            {
                Isprint = objBs.IssueIsprint("tblJpConsai", "ConsaiId", Stitchingid);

            }
            #endregion


            ScriptManager.RegisterStartupScript(this, typeof(Page), "myFunction", "myFunction();", true);

        }
    }
}