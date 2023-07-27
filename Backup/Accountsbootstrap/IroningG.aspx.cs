using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;


namespace Billing.Accountsbootstrap
{
    public partial class IroningG : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double totalfs = 0;

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


        double aQ30F = 0; double aQ32F = 0; double aQ34F = 0; double aQ36F = 0;
        double aQXSF = 0; double aQSF = 0; double aQMF = 0; double aQLF = 0;
        double aQXLF = 0; double aQXXLF = 0; double aQ3XLF = 0; double aQ4XLF = 0;

        double aQ30H = 0; double aQ32H = 0; double aQ34H = 0; double aQ36H = 0;
        double aQXSH = 0; double aQSH = 0; double aQMH = 0; double aQLH = 0;
        double aQXLH = 0; double aQXXLH = 0; double aQ3XLH = 0; double aQ4XLH = 0;
        double aQttlFH = 0; double aGVtotalshirt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string Ironingid = Request.QueryString.Get("Ironingid");
            string Type = Request.QueryString.Get("Type");

            sTableName = Session["User"].ToString();
            if (Ironingid != null)
            {
                #region
                DataSet ds2 = objBs.JpIroningfor(Ironingid);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    String Mode = "";

                    DataSet stichingsno = objBs.Getironcutid(Convert.ToInt32(ds2.Tables[0].Rows[0]["LotNo"].ToString()));
                    if (stichingsno.Tables[0].Rows.Count > 0)
                    {
                        lbllscutno.Text = stichingsno.Tables[0].Rows[0]["Cutid"].ToString();
                        lbllscutno1.Text = stichingsno.Tables[0].Rows[0]["Cutid"].ToString();
                        lblheadingname.Text = "DELIVERY NOTE ISSUE ";
                        lblheadingname1.Text = "DELIVERY NOTE RECEIVE ";
                        lblprocessname.Text = "IRONING/PACKING";
                        lblprocessname1.Text = "IRONING/PACKING";
                        Mode = "Packed";
                        
                    }
                    else
                    {
                        lblheadingname.Text = "DELIVERY NOTE ISSUE ";
                        lblheadingname1.Text = "DELIVERY NOTE RECEIVE ";
                        lblprocessname.Text = "IRONING/PACKING";
                        lblprocessname1.Text = "IRONING/PACKING";
                        Mode = "UnPacked";
                    }


                    lblLot.Text = ds2.Tables[0].Rows[0]["Companylotno"].ToString();
                    lblworkorder.Text = ds2.Tables[0].Rows[0]["ironingid"].ToString();
                    lblwrkorderrec.Text = ds2.Tables[0].Rows[0]["ironingid"].ToString();
                    DataSet getitemdetails = objBs.getitemidinprecutforitem(lblLot.Text);
                    if (getitemdetails.Tables[0].Rows.Count > 0)
                    {
                        lblitemnarration.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();
                        lblitemnarrations.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();

                        lblsleeve.Text = getitemdetails.Tables[0].Rows[0]["sleevetypec"].ToString();
                        lbllabel.Text = getitemdetails.Tables[0].Rows[0]["labeltypec"].ToString();

                        lblsleeve1.Text = getitemdetails.Tables[0].Rows[0]["sleevetypec"].ToString();
                        lbllabel1.Text = getitemdetails.Tables[0].Rows[0]["labeltypec"].ToString();

                        lblbitemname.Text = getitemdetails.Tables[0].Rows[0]["Itemname"].ToString() + '(' + getitemdetails.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                        lblbitemname1.Text = getitemdetails.Tables[0].Rows[0]["Itemname"].ToString() + '(' + getitemdetails.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                    }

                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    lblLedgerName.Text = ds2.Tables[0].Rows[0]["name"].ToString();
                    lbljwname.Text = ds2.Tables[0].Rows[0]["name"].ToString();

                    lblgastin.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString();


                    lblLot1.Text = ds2.Tables[0].Rows[0]["Companylotno"].ToString();
                    lblDeldate1.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    lblLedgerName1.Text = ds2.Tables[0].Rows[0]["name"].ToString();

                    lblgastin1.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString();

                    lblPaidAmount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["PaidAmount"]).ToString("f2");
                    lblTotalAmount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");
                    lbllnarration.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();

                    DataSet dsbrand = objBs.JpStichingforall("tblJpIroning", "IroningId", Ironingid);
                    if (dsbrand.Tables[0].Rows.Count > 0)
                    {
                        lblbrand.Text = dsbrand.Tables[0].Rows[0]["BrandName"].ToString();
                        lblbrand1.Text = dsbrand.Tables[0].Rows[0]["BrandName"].ToString();

                    }


                    DataSet ds23 = new DataSet();
                    if (Mode == "Packed")
                    {
                        ds23 = objBs.Get_JpIroningprint(Convert.ToString(Ironingid), "6", "Iron", Type);
                    }
                    else
                    {
                        ds23 = objBs.Get_JpIroningprintunpack(Convert.ToString(Ironingid), "6", "Iron", Type);
                    }

                    if (ds23.Tables[0].Rows.Count > 0)
                    {


                        GridView1.DataSource = ds23;
                        GridView1.DataBind();

                        griddummy.DataSource = ds23;
                        griddummy.DataBind();


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
                            string S30 = ds23.Tables[0].Rows[j]["Rec30FS"].ToString();
                            string S32 = ds23.Tables[0].Rows[j]["Rec32FS"].ToString();
                            string S34 = ds23.Tables[0].Rows[j]["Rec34FS"].ToString();
                            string S36 = ds23.Tables[0].Rows[j]["Rec36FS"].ToString();
                            string SXS = ds23.Tables[0].Rows[j]["RecxsFS"].ToString();
                            string SS = ds23.Tables[0].Rows[j]["RecsFS"].ToString();
                            string SM = ds23.Tables[0].Rows[j]["RecmFS"].ToString();
                            string SL = ds23.Tables[0].Rows[j]["ReclFS"].ToString();
                            string SXL = ds23.Tables[0].Rows[j]["RecxlFS"].ToString();
                            string SXXL = ds23.Tables[0].Rows[j]["RecxxlFS"].ToString();
                            string S3XL = ds23.Tables[0].Rows[j]["Rec3xlFS"].ToString();
                            string S4XL = ds23.Tables[0].Rows[j]["Rec4xlFS"].ToString();


                            string HS30 = ds23.Tables[0].Rows[j]["Rec30HS"].ToString();
                            string HS32 = ds23.Tables[0].Rows[j]["Rec32HS"].ToString();
                            string HS34 = ds23.Tables[0].Rows[j]["Rec34HS"].ToString();
                            string HS36 = ds23.Tables[0].Rows[j]["Rec36HS"].ToString();
                            string HSXS = ds23.Tables[0].Rows[j]["RecxsHS"].ToString();
                            string HSS = ds23.Tables[0].Rows[j]["RecsHS"].ToString();
                            string HSM = ds23.Tables[0].Rows[j]["RecmHS"].ToString();
                            string HSL = ds23.Tables[0].Rows[j]["ReclHS"].ToString();
                            string HSXL = ds23.Tables[0].Rows[j]["RecxlHS"].ToString();
                            string HSXXL = ds23.Tables[0].Rows[j]["RecxxlHS"].ToString();
                            string HS3XL = ds23.Tables[0].Rows[j]["Rec3xlHS"].ToString();
                            string HS4XL = ds23.Tables[0].Rows[j]["Rec4xlHS"].ToString();

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
                                griddummy.Columns[7].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GridView1.Columns[8].Visible = true;
                                griddummy.Columns[8].Visible = true;
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

                }
                #endregion
            }


            string IroningidRec = Request.QueryString.Get("IroningidRec");
            //// string Type = Request.QueryString.Get("Type");

            sTableName = Session["User"].ToString();
            if (IroningidRec != null)
            {
                DataSet dsbrand = objBs.JpStichingforall("tblJpIroning", "IroningId", IroningidRec);
                if (dsbrand.Tables[0].Rows.Count > 0)
                {
                    lblbrand.Text = dsbrand.Tables[0].Rows[0]["BrandName"].ToString();
                    lblbrand1.Text = dsbrand.Tables[0].Rows[0]["BrandName"].ToString();

                }

                #region

                DataSet ds2 = objBs.JpIroningfor(IroningidRec);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblitemnarrations.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();
                    lblitemnarration.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();


                    tabledummy.Visible = false;
                    String Mode = "";
                    DataSet stichingsno = objBs.Getironcutid(Convert.ToInt32(ds2.Tables[0].Rows[0]["LotNo"].ToString()));
                    if (stichingsno.Tables[0].Rows.Count > 0)
                    {
                        lbllscutno.Text = stichingsno.Tables[0].Rows[0]["Cutid"].ToString();
                        lblheadingname.Text = "DELIVERY NOTE RECEIVE";
                        
                        lblprocessname.Text = "IRONING/PACKING";
                        
                        Mode = "Packed";
                    }
                    else
                    {
                        lblheadingname.Text = "DELIVERY NOTE RECEIVE";
                        lblprocessname.Text = "IRONING/PACKING";
                        Mode = "UnPacked";
                    }


                    lblLot.Text = ds2.Tables[0].Rows[0]["Companylotno"].ToString();
                    lblworkorder.Text = ds2.Tables[0].Rows[0]["ironingid"].ToString();
                    DataSet getitemdetails = objBs.getitemidinprecutforitem(lblLot.Text);
                    if (getitemdetails.Tables[0].Rows.Count > 0)
                    {
                        lblsleeve.Text = getitemdetails.Tables[0].Rows[0]["sleevetypec"].ToString();
                        lbllabel.Text = getitemdetails.Tables[0].Rows[0]["labeltypec"].ToString();
                        lblsleeve1.Text = getitemdetails.Tables[0].Rows[0]["sleevetypec"].ToString();
                        lbllabel1.Text = getitemdetails.Tables[0].Rows[0]["labeltypec"].ToString();
                        lblbitemname.Text = getitemdetails.Tables[0].Rows[0]["Itemname"].ToString() + '(' + getitemdetails.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                        lblbitemname1.Text = getitemdetails.Tables[0].Rows[0]["Itemname"].ToString() + '(' + getitemdetails.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                    }
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
                    lblLedgerName.Text = ds2.Tables[0].Rows[0]["name"].ToString();
                    lblgastin.Text = ds2.Tables[0].Rows[0]["GSTIN"].ToString();
                    lblPaidAmount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["PaidAmount"]).ToString("f2");
                    lblTotalAmount.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");

                    lbllnarration.Text = ds2.Tables[0].Rows[0]["Narration"].ToString();

                    DataSet ds23 = new DataSet();
                    if (Mode == "Packed")
                    {
                        ds23 = objBs.Get_JpIroningprint(Convert.ToString(IroningidRec), "6", "Iron", Type);
                    }
                    else
                    {
                        ds23 = objBs.Get_JpIroningprintunpack(Convert.ToString(IroningidRec), "6", "Iron", Type);
                    }
                    if (ds23.Tables[0].Rows.Count > 0)
                    {


                        GridView1.DataSource = ds23;
                        GridView1.DataBind();


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
                            string S30 = ds23.Tables[0].Rows[j]["Rec30FS"].ToString();
                            string S32 = ds23.Tables[0].Rows[j]["Rec32FS"].ToString();
                            string S34 = ds23.Tables[0].Rows[j]["Rec34FS"].ToString();
                            string S36 = ds23.Tables[0].Rows[j]["Rec36FS"].ToString();
                            string SXS = ds23.Tables[0].Rows[j]["RecxsFS"].ToString();
                            string SS = ds23.Tables[0].Rows[j]["RecsFS"].ToString();
                            string SM = ds23.Tables[0].Rows[j]["RecmFS"].ToString();
                            string SL = ds23.Tables[0].Rows[j]["ReclFS"].ToString();
                            string SXL = ds23.Tables[0].Rows[j]["RecxlFS"].ToString();
                            string SXXL = ds23.Tables[0].Rows[j]["RecxxlFS"].ToString();
                            string S3XL = ds23.Tables[0].Rows[j]["Rec3xlFS"].ToString();
                            string S4XL = ds23.Tables[0].Rows[j]["Rec4xlFS"].ToString();


                            string HS30 = ds23.Tables[0].Rows[j]["Rec30HS"].ToString();
                            string HS32 = ds23.Tables[0].Rows[j]["Rec32HS"].ToString();
                            string HS34 = ds23.Tables[0].Rows[j]["Rec34HS"].ToString();
                            string HS36 = ds23.Tables[0].Rows[j]["Rec36HS"].ToString();
                            string HSXS = ds23.Tables[0].Rows[j]["RecxsHS"].ToString();
                            string HSS = ds23.Tables[0].Rows[j]["RecsHS"].ToString();
                            string HSM = ds23.Tables[0].Rows[j]["RecmHS"].ToString();
                            string HSL = ds23.Tables[0].Rows[j]["ReclHS"].ToString();
                            string HSXL = ds23.Tables[0].Rows[j]["RecxlHS"].ToString();
                            string HSXXL = ds23.Tables[0].Rows[j]["RecxxlHS"].ToString();
                            string HS3XL = ds23.Tables[0].Rows[j]["Rec3xlHS"].ToString();
                            string HS4XL = ds23.Tables[0].Rows[j]["Rec4xlHS"].ToString();

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
                                griddummy.Columns[7].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GridView1.Columns[8].Visible = true;
                                griddummy.Columns[8].Visible = true;
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


                    DataSet dsacessories = objBs.getironmatforupdate(Convert.ToInt32(IroningidRec), 5);
                    if (dsacessories.Tables[0].Rows.Count > 0)
                    {
                        gvacessories.DataSource = dsacessories;
                        gvacessories.DataBind();
                    }

                    #region Damage
                    DataSet dsdamage = new DataSet();
                    if (Mode == "Packed")
                    {
                        dsdamage = objBs.Get_JpIroningprint(Convert.ToString(IroningidRec), "6", "Iron", "Damage");

                    }
                    else
                    {
                        dsdamage = objBs.Get_JpIroningprintunpack(Convert.ToString(IroningidRec), "6", "Iron", "Damage");
                    }
                    if (dsdamage.Tables[0].Rows.Count > 0)
                    {
                        GVDamage.DataSource = dsdamage;
                        GVDamage.DataBind();
                    }
                    if (dsdamage.Tables[0].Rows.Count > 0)
                    {
                        #region
                        GVDamage.Columns[6].Visible = false;
                        GVDamage.Columns[7].Visible = false;
                        GVDamage.Columns[8].Visible = false;
                        GVDamage.Columns[9].Visible = false;
                        GVDamage.Columns[10].Visible = false;
                        GVDamage.Columns[11].Visible = false;
                        GVDamage.Columns[12].Visible = false;
                        GVDamage.Columns[13].Visible = false;
                        GVDamage.Columns[14].Visible = false;
                        GVDamage.Columns[15].Visible = false;
                        GVDamage.Columns[16].Visible = false;
                        GVDamage.Columns[17].Visible = false;

                        GVDamage.Columns[18].Visible = false;
                        GVDamage.Columns[19].Visible = false;
                        GVDamage.Columns[20].Visible = false;
                        GVDamage.Columns[21].Visible = false;
                        GVDamage.Columns[22].Visible = false;
                        GVDamage.Columns[23].Visible = false;
                        GVDamage.Columns[24].Visible = false;
                        GVDamage.Columns[25].Visible = false;
                        GVDamage.Columns[26].Visible = false;
                        GVDamage.Columns[27].Visible = false;
                        GVDamage.Columns[28].Visible = false;
                        GVDamage.Columns[29].Visible = false;

                        #endregion

                        for (int j = 0; j < dsdamage.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsdamage.Tables[0].Rows[j]["Rec30FS"].ToString();
                            string S32 = dsdamage.Tables[0].Rows[j]["Rec32FS"].ToString();
                            string S34 = dsdamage.Tables[0].Rows[j]["Rec34FS"].ToString();
                            string S36 = dsdamage.Tables[0].Rows[j]["Rec36FS"].ToString();
                            string SXS = dsdamage.Tables[0].Rows[j]["RecxsFS"].ToString();
                            string SS = dsdamage.Tables[0].Rows[j]["RecsFS"].ToString();
                            string SM = dsdamage.Tables[0].Rows[j]["RecmFS"].ToString();
                            string SL = dsdamage.Tables[0].Rows[j]["ReclFS"].ToString();
                            string SXL = dsdamage.Tables[0].Rows[j]["RecxlFS"].ToString();
                            string SXXL = dsdamage.Tables[0].Rows[j]["RecxxlFS"].ToString();
                            string S3XL = dsdamage.Tables[0].Rows[j]["Rec3xlFS"].ToString();
                            string S4XL = dsdamage.Tables[0].Rows[j]["Rec4xlFS"].ToString();


                            string HS30 = dsdamage.Tables[0].Rows[j]["Rec30HS"].ToString();
                            string HS32 = dsdamage.Tables[0].Rows[j]["Rec32HS"].ToString();
                            string HS34 = dsdamage.Tables[0].Rows[j]["Rec34HS"].ToString();
                            string HS36 = dsdamage.Tables[0].Rows[j]["Rec36HS"].ToString();
                            string HSXS = dsdamage.Tables[0].Rows[j]["RecxsHS"].ToString();
                            string HSS = dsdamage.Tables[0].Rows[j]["RecsHS"].ToString();
                            string HSM = dsdamage.Tables[0].Rows[j]["RecmHS"].ToString();
                            string HSL = dsdamage.Tables[0].Rows[j]["ReclHS"].ToString();
                            string HSXL = dsdamage.Tables[0].Rows[j]["RecxlHS"].ToString();
                            string HSXXL = dsdamage.Tables[0].Rows[j]["RecxxlHS"].ToString();
                            string HS3XL = dsdamage.Tables[0].Rows[j]["Rec3xlHS"].ToString();
                            string HS4XL = dsdamage.Tables[0].Rows[j]["Rec4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVDamage.Columns[6].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVDamage.Columns[7].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVDamage.Columns[8].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVDamage.Columns[9].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVDamage.Columns[10].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVDamage.Columns[11].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVDamage.Columns[12].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVDamage.Columns[13].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVDamage.Columns[14].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVDamage.Columns[15].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVDamage.Columns[16].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVDamage.Columns[17].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVDamage.Columns[18].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVDamage.Columns[19].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVDamage.Columns[20].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVDamage.Columns[21].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVDamage.Columns[22].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVDamage.Columns[23].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVDamage.Columns[24].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVDamage.Columns[25].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVDamage.Columns[26].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVDamage.Columns[27].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVDamage.Columns[28].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVDamage.Columns[29].Visible = true;
                            }
                            #endregion

                        }


                    }
                    #endregion

                    #region Alter
                    DataSet dsAlter = new DataSet();
                    if (Mode == "Packed")
                    {
                        dsAlter = objBs.Get_JpIroningprint(Convert.ToString(IroningidRec), "6", "Iron", "Alter");

                    }
                    else
                    {
                        dsAlter = objBs.Get_JpIroningprintunpack(Convert.ToString(IroningidRec), "6", "Iron", "Alter");
                    }
                    if (dsAlter.Tables[0].Rows.Count > 0)
                    {
                        GVAlter.DataSource = dsAlter;
                        GVAlter.DataBind();
                    }
                    if (dsAlter.Tables[0].Rows.Count > 0)
                    {
                        #region
                        GVAlter.Columns[6].Visible = false;
                        GVAlter.Columns[7].Visible = false;
                        GVAlter.Columns[8].Visible = false;
                        GVAlter.Columns[9].Visible = false;
                        GVAlter.Columns[10].Visible = false;
                        GVAlter.Columns[11].Visible = false;
                        GVAlter.Columns[12].Visible = false;
                        GVAlter.Columns[13].Visible = false;
                        GVAlter.Columns[14].Visible = false;
                        GVAlter.Columns[15].Visible = false;
                        GVAlter.Columns[16].Visible = false;
                        GVAlter.Columns[17].Visible = false;

                        GVAlter.Columns[18].Visible = false;
                        GVAlter.Columns[19].Visible = false;
                        GVAlter.Columns[20].Visible = false;
                        GVAlter.Columns[21].Visible = false;
                        GVAlter.Columns[22].Visible = false;
                        GVAlter.Columns[23].Visible = false;
                        GVAlter.Columns[24].Visible = false;
                        GVAlter.Columns[25].Visible = false;
                        GVAlter.Columns[26].Visible = false;
                        GVAlter.Columns[27].Visible = false;
                        GVAlter.Columns[28].Visible = false;
                        GVAlter.Columns[29].Visible = false;

                        #endregion

                        for (int j = 0; j < dsAlter.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsAlter.Tables[0].Rows[j]["Rec30FS"].ToString();
                            string S32 = dsAlter.Tables[0].Rows[j]["Rec32FS"].ToString();
                            string S34 = dsAlter.Tables[0].Rows[j]["Rec34FS"].ToString();
                            string S36 = dsAlter.Tables[0].Rows[j]["Rec36FS"].ToString();
                            string SXS = dsAlter.Tables[0].Rows[j]["RecxsFS"].ToString();
                            string SS = dsAlter.Tables[0].Rows[j]["RecsFS"].ToString();
                            string SM = dsAlter.Tables[0].Rows[j]["RecmFS"].ToString();
                            string SL = dsAlter.Tables[0].Rows[j]["ReclFS"].ToString();
                            string SXL = dsAlter.Tables[0].Rows[j]["RecxlFS"].ToString();
                            string SXXL = dsAlter.Tables[0].Rows[j]["RecxxlFS"].ToString();
                            string S3XL = dsAlter.Tables[0].Rows[j]["Rec3xlFS"].ToString();
                            string S4XL = dsAlter.Tables[0].Rows[j]["Rec4xlFS"].ToString();


                            string HS30 = dsAlter.Tables[0].Rows[j]["Rec30HS"].ToString();
                            string HS32 = dsAlter.Tables[0].Rows[j]["Rec32HS"].ToString();
                            string HS34 = dsAlter.Tables[0].Rows[j]["Rec34HS"].ToString();
                            string HS36 = dsAlter.Tables[0].Rows[j]["Rec36HS"].ToString();
                            string HSXS = dsAlter.Tables[0].Rows[j]["RecxsHS"].ToString();
                            string HSS = dsAlter.Tables[0].Rows[j]["RecsHS"].ToString();
                            string HSM = dsAlter.Tables[0].Rows[j]["RecmHS"].ToString();
                            string HSL = dsAlter.Tables[0].Rows[j]["ReclHS"].ToString();
                            string HSXL = dsAlter.Tables[0].Rows[j]["RecxlHS"].ToString();
                            string HSXXL = dsAlter.Tables[0].Rows[j]["RecxxlHS"].ToString();
                            string HS3XL = dsAlter.Tables[0].Rows[j]["Rec3xlHS"].ToString();
                            string HS4XL = dsAlter.Tables[0].Rows[j]["Rec4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVAlter.Columns[6].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVAlter.Columns[7].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVAlter.Columns[8].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVAlter.Columns[9].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVAlter.Columns[10].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVAlter.Columns[11].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVAlter.Columns[12].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVAlter.Columns[13].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVAlter.Columns[14].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVAlter.Columns[15].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVAlter.Columns[16].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVAlter.Columns[17].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVAlter.Columns[18].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVAlter.Columns[19].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVAlter.Columns[20].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVAlter.Columns[21].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVAlter.Columns[22].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVAlter.Columns[23].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVAlter.Columns[24].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVAlter.Columns[25].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVAlter.Columns[26].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVAlter.Columns[27].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVAlter.Columns[28].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVAlter.Columns[29].Visible = true;
                            }
                            #endregion

                        }


                    }
                    #endregion
                }

                #endregion
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("IroningGrid.aspx");
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalfs = totalfs + Convert.ToDouble(e.Row.Cells[4].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //  e.Row.Cells[6].Text = "Total:";
                e.Row.Cells[3].Text = "Total";
                e.Row.Cells[4].Text = totalfs.ToString();
                // totalfs = totalfs + Convert.ToDouble(e.Row.Cells[7].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
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
        protected void gvnewstiching_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (DataBinder.Eval(e.Row.DataItem, "Rec30fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec30fs") != "")
                {
                    Q30F = Q30F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec30fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec32fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec32fs") != "")
                {
                    Q32F = Q32F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec32fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec34fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec34fs") != "")
                {
                    Q34F = Q34F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec34fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec36fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec36fs") != "")
                {
                    Q36F = Q36F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec36fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxsfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxsfs") != "")
                {
                    QXSF = QXSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxsfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recsfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recsfs") != "")
                {
                    QSF = QSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recsfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recmfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recmfs") != "")
                {
                    QMF = QMF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recmfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Reclfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Reclfs") != "")
                {
                    QLF = QLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reclfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxlfs") != "")
                {
                    QXLF = QXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxxlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxxlfs") != "")
                {
                    QXXLF = QXXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxxlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec3xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec3xlfs") != "")
                {
                    Q3XLF = Q3XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec3xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec4xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec4xlfs") != "")
                {
                    Q4XLF = Q4XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec4xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec30hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec30hs") != "")
                {

                    Q30H = Q30H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec30hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec32hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec32hs") != "")
                {
                    Q32H = Q32H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec32hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec34hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec34hs") != "")
                {
                    Q34H = Q34H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec34hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec36hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec36hs") != "")
                {
                    Q36H = Q36H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec36hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxshs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxshs") != "")
                {
                    QXSH = QXSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxshs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recshs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recshs") != "")
                {
                    QSH = QSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recshs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recmhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recmhs") != "")
                {
                    QMH = QMH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recmhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Reclhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Reclhs") != "")
                {
                    QLH = QLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reclhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxlhs") != "")
                {
                    QXLH = QXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxxlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxxlhs") != "")
                {
                    QXXLH = QXXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxxlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec3xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec3xlhs") != "")
                {
                    Q3XLH = Q3XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec3xlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec4xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec4xlhs") != "")
                {
                    Q4XLH = Q4XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec4xlhs"));
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
                // e.Row.Cells[33].Text = QttlFH.ToString();
                // e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
                // lblTotalQty.Text = GVtotalshirt.ToString();
                lblTotalQty.Text = (DGVtotalshirt + GVtotalshirt + aGVtotalshirt).ToString();
            }
        }

        protected void GVDamage_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (DataBinder.Eval(e.Row.DataItem, "Rec30fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec30fs") != "")
                {
                    DQ30F = DQ30F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec30fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec32fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec32fs") != "")
                {
                    DQ32F = DQ32F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec32fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec34fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec34fs") != "")
                {
                    DQ34F = DQ34F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec34fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec36fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec36fs") != "")
                {
                    DQ36F = DQ36F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec36fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxsfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxsfs") != "")
                {
                    DQXSF = DQXSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxsfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recsfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recsfs") != "")
                {
                    DQSF = DQSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recsfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recmfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recmfs") != "")
                {
                    DQMF = DQMF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recmfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Reclfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Reclfs") != "")
                {
                    DQLF = DQLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reclfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxlfs") != "")
                {
                    DQXLF = DQXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxxlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxxlfs") != "")
                {
                    DQXXLF = DQXXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxxlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec3xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec3xlfs") != "")
                {
                    DQ3XLF = DQ3XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec3xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec4xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec4xlfs") != "")
                {
                    DQ4XLF = DQ4XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec4xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec30hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec30hs") != "")
                {

                    DQ30H = DQ30H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec30hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec32hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec32hs") != "")
                {
                    DQ32H = DQ32H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec32hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec34hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec34hs") != "")
                {
                    DQ34H = DQ34H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec34hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec36hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec36hs") != "")
                {
                    DQ36H = DQ36H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec36hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxshs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxshs") != "")
                {
                    DQXSH = DQXSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxshs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recshs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recshs") != "")
                {
                    DQSH = DQSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recshs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recmhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recmhs") != "")
                {
                    DQMH = DQMH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recmhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Reclhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Reclhs") != "")
                {
                    DQLH = DQLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reclhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxlhs") != "")
                {
                    DQXLH = DQXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxxlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxxlhs") != "")
                {
                    DQXXLH = DQXXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxxlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec3xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec3xlhs") != "")
                {
                    DQ3XLH = DQ3XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec3xlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec4xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec4xlhs") != "")
                {
                    DQ4XLH = DQ4XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec4xlhs"));
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
                lblTotalQty.Text = (DGVtotalshirt + GVtotalshirt + aGVtotalshirt).ToString();
            }
        }

        protected void GVAlter_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (DataBinder.Eval(e.Row.DataItem, "Rec30fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec30fs") != "")
                {
                    aQ30F = aQ30F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec30fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec32fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec32fs") != "")
                {
                    aQ32F = aQ32F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec32fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec34fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec34fs") != "")
                {
                    aQ34F = aQ34F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec34fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec36fs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec36fs") != "")
                {
                    aQ36F = aQ36F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec36fs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxsfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxsfs") != "")
                {
                    aQXSF = aQXSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxsfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recsfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recsfs") != "")
                {
                    aQSF = aQSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recsfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recmfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recmfs") != "")
                {
                    aQMF = aQMF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recmfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Reclfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Reclfs") != "")
                {
                    aQLF = aQLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reclfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxlfs") != "")
                {
                    aQXLF = aQXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxxlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxxlfs") != "")
                {
                    aQXXLF = aQXXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxxlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec3xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec3xlfs") != "")
                {
                    aQ3XLF = aQ3XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec3xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec4xlfs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec4xlfs") != "")
                {
                    aQ4XLF = aQ4XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec4xlfs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec30hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec30hs") != "")
                {

                    aQ30H = aQ30H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec30hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec32hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec32hs") != "")
                {
                    aQ32H = aQ32H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec32hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec34hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec34hs") != "")
                {
                    aQ34H = aQ34H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec34hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec36hs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec36hs") != "")
                {
                    aQ36H = aQ36H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec36hs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxshs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxshs") != "")
                {
                    aQXSH = aQXSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxshs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recshs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recshs") != "")
                {
                    aQSH = aQSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recshs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recmhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recmhs") != "")
                {
                    aQMH = aQMH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recmhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Reclhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Reclhs") != "")
                {
                    aQLH = aQLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reclhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxlhs") != "")
                {
                    aQXLH = aQXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Recxxlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Recxxlhs") != "")
                {
                    aQXXLH = aQXXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Recxxlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec3xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec3xlhs") != "")
                {
                    aQ3XLH = aQ3XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec3xlhs"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Rec4xlhs") != "-" && DataBinder.Eval(e.Row.DataItem, "Rec4xlhs") != "")
                {
                    aQ4XLH = aQ4XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rec4xlhs"));
                }


                aGVtotalshirt = aGVtotalshirt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "GrandTtl"));
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

                e.Row.Cells[6].Text = aQ30F.ToString();
                e.Row.Cells[7].Text = aQ32F.ToString();
                e.Row.Cells[8].Text = aQ34F.ToString();
                e.Row.Cells[9].Text = aQ36F.ToString();
                e.Row.Cells[10].Text = aQXSF.ToString();
                e.Row.Cells[11].Text = aQSF.ToString();
                e.Row.Cells[12].Text = aQMF.ToString();
                e.Row.Cells[13].Text = aQLF.ToString();
                e.Row.Cells[14].Text = aQXLF.ToString();
                e.Row.Cells[15].Text = aQXXLF.ToString();
                e.Row.Cells[16].Text = aQ3XLF.ToString();
                e.Row.Cells[17].Text = aQ4XLF.ToString();

                e.Row.Cells[18].Text = aQ30H.ToString();
                e.Row.Cells[19].Text = aQ32H.ToString();
                e.Row.Cells[20].Text = aQ34H.ToString();
                e.Row.Cells[21].Text = aQ36H.ToString();
                e.Row.Cells[22].Text = aQXSH.ToString();
                e.Row.Cells[23].Text = aQSH.ToString();
                e.Row.Cells[24].Text = aQMH.ToString();
                e.Row.Cells[25].Text = aQLH.ToString();
                e.Row.Cells[26].Text = aQXLH.ToString();
                e.Row.Cells[27].Text = aQXXLH.ToString();
                e.Row.Cells[28].Text = aQ3XLH.ToString();
                e.Row.Cells[29].Text = aQ4XLH.ToString();

                e.Row.Cells[30].Text = aGVtotalshirt.ToString();

                lblTotalQty.Text = (DGVtotalshirt + GVtotalshirt + aGVtotalshirt).ToString();
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


            string Ironingid = Request.QueryString.Get("Ironingid");

            int Isprint = 0;
            #region


            Isprint = objBs.IssueIsprint("tbljpIroning", "IroningId", Ironingid);

            #endregion


            ScriptManager.RegisterStartupScript(this, typeof(Page), "myFunction", "myFunction();", true);

        }
    }
}