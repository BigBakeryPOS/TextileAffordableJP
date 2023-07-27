using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusinessLayer;
using CommonLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;
using System.Drawing;

namespace Billing.Accountsbootstrap
{
    public partial class FinishedStockReport : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();
        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


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

                        company_SelectedIndexChnaged(sender, e);
                    }


                }


                //DataSet dss = objBs.finishedstocks();
                //if (dss.Tables[0].Rows.Count > 0)
                //{
                //    gridcatqty.Caption = "Finished Stock Report ";
                //    gridcatqty.DataSource = dss;
                //    gridcatqty.DataBind();
                //}

                //DataSet dsStock = objBs.FinalStockBC(Convert.ToInt32(drpbranch.SelectedValue));
                //if (dsStock.Tables[0].Rows.Count > 0)
                //{
                //    GVBottiCelliStock.Caption = "BottiCelli Stock Report ";
                //    GVBottiCelliStock.DataSource = dsStock;
                //    GVBottiCelliStock.DataBind();
                //}

            }
        }
        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {
            ddldespatchversion.Enabled = false;
            if (ddlstocktype.SelectedValue == "1")
            {
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
            }
            else
            {
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
            }
            DataSet dsStock = objBs.getfinalstock(drpbranch.SelectedValue);
            if (dsStock.Tables[0].Rows.Count > 0)
            {
                GVFinalStock.Caption = "Stock Report ";
                GVFinalStock.DataSource = dsStock;
                GVFinalStock.DataBind();

                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    #region

                    GVFinalStock.Columns[0].Visible = false;
                    GVFinalStock.Columns[1].Visible = true;
                    GVFinalStock.Columns[2].Visible = false;
                    GVFinalStock.Columns[3].Visible = true;
                    GVFinalStock.Columns[4].Visible = true;

                    GVFinalStock.Columns[5].Visible = false;
                    GVFinalStock.Columns[6].Visible = false;
                    GVFinalStock.Columns[7].Visible = false;
                    GVFinalStock.Columns[8].Visible = false;
                    GVFinalStock.Columns[9].Visible = false;
                    GVFinalStock.Columns[10].Visible = false;
                    GVFinalStock.Columns[11].Visible = false;
                    GVFinalStock.Columns[12].Visible = false;
                    GVFinalStock.Columns[13].Visible = false;
                    GVFinalStock.Columns[14].Visible = false;
                    GVFinalStock.Columns[15].Visible = false;
                    GVFinalStock.Columns[16].Visible = false;

                    GVFinalStock.Columns[17].Visible = false;
                    GVFinalStock.Columns[18].Visible = false;
                    GVFinalStock.Columns[19].Visible = false;
                    GVFinalStock.Columns[20].Visible = false;
                    GVFinalStock.Columns[21].Visible = false;
                    GVFinalStock.Columns[22].Visible = false;
                    GVFinalStock.Columns[23].Visible = false;
                    GVFinalStock.Columns[24].Visible = false;
                    GVFinalStock.Columns[25].Visible = false;
                    GVFinalStock.Columns[26].Visible = false;
                    GVFinalStock.Columns[27].Visible = false;
                    GVFinalStock.Columns[28].Visible = false;



                    for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                    {

                        #region
                        string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                        string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                        string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                        string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                        string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                        string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                        string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                        string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                        string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                        string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                        string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                        string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                        string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                        string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                        string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                        string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                        string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                        string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                        string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                        string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                        string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                        string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                        string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                        string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                        //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                        //////grndtot = grndtot + tot;
                        //////lblstockgrandtot.Text = grndtot.ToString();

                        if (S30 != "0")
                        {

                            GVFinalStock.Columns[5].Visible = true;
                        }
                        if (S32 != "0")
                        {

                            GVFinalStock.Columns[6].Visible = true;
                        }

                        if (S34 != "0")
                        {

                            GVFinalStock.Columns[7].Visible = true;
                        }

                        if (S36 != "0")
                        {

                            GVFinalStock.Columns[8].Visible = true;
                        }

                        if (SXS != "0")
                        {

                            GVFinalStock.Columns[9].Visible = true;
                        }

                        if (SS != "0")
                        {

                            GVFinalStock.Columns[10].Visible = true;
                        }

                        if (SM != "0")
                        {

                            GVFinalStock.Columns[11].Visible = true;
                        }

                        if (SL != "0")
                        {

                            GVFinalStock.Columns[12].Visible = true;
                        }

                        if (SXL != "0")
                        {

                            GVFinalStock.Columns[13].Visible = true;
                        }

                        if (SXXL != "0")
                        {

                            GVFinalStock.Columns[14].Visible = true;
                        }

                        if (S3XL != "0")
                        {

                            GVFinalStock.Columns[15].Visible = true;
                        }

                        if (S4XL != "0")
                        {

                            GVFinalStock.Columns[16].Visible = true;
                        }


                        if (HS30 != "0")
                        {

                            GVFinalStock.Columns[17].Visible = true;
                        }
                        if (HS32 != "0")
                        {

                            GVFinalStock.Columns[18].Visible = true;
                        }

                        if (HS34 != "0")
                        {

                            GVFinalStock.Columns[19].Visible = true;
                        }

                        if (HS36 != "0")
                        {

                            GVFinalStock.Columns[20].Visible = true;
                        }

                        if (HSXS != "0")
                        {

                            GVFinalStock.Columns[21].Visible = true;
                        }

                        if (HSS != "0")
                        {

                            GVFinalStock.Columns[22].Visible = true;
                        }

                        if (HSM != "0")
                        {

                            GVFinalStock.Columns[23].Visible = true;
                        }

                        if (HSL != "0")
                        {

                            GVFinalStock.Columns[24].Visible = true;
                        }

                        if (HSXL != "0")
                        {

                            GVFinalStock.Columns[25].Visible = true;
                        }

                        if (HSXXL != "0")
                        {

                            GVFinalStock.Columns[26].Visible = true;
                        }

                        if (HS3XL != "0")
                        {

                            GVFinalStock.Columns[27].Visible = true;
                        }

                        if (HS4XL != "0")
                        {

                            GVFinalStock.Columns[28].Visible = true;
                        }
                        #endregion

                    }

                    #endregion
                }
            }
            else
            {
                GVFinalStock.DataSource = null;
                GVFinalStock.DataBind();
            }
        }

        protected void ddlstocktype_SelectedIndexChnaged(object sender, EventArgs e)
        {
            GVFinalStock.DataSource = null;
            GVFinalStock.DataBind();

            if (ddlstocktype.SelectedValue == "1" || ddlstocktype.SelectedValue == "5")
            {
                ddldespatchversion.Enabled = false;
                #region

                if (ddlstocktype.SelectedValue == "1" )
                {
                    txtFromDate.Enabled = false;
                    txtToDate.Enabled = false;
                }
                else
                {
                    txtFromDate.Enabled = true;
                    txtToDate.Enabled = true;
                }
                DataSet dsStock = objBs.getfinalstock(drpbranch.SelectedValue);
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Godown Stock Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;


                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }
            else if (ddlstocktype.SelectedValue == "2")
            {
                ddldespatchversion.Enabled = false;
                #region
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                DataSet dsStock = objBs.getdeloverynotestock(drpbranch.SelectedValue);
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Despatch Stock Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;


                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }
            else if (ddlstocktype.SelectedValue == "3")
            {
                ddldespatchversion.Enabled = false;
                #region
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                DataSet dsStock = objBs.getdeloverynotestockret(drpbranch.SelectedValue);
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Return Stock Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;


                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }
            else if (ddlstocktype.SelectedValue == "4")
            {
                ddldespatchversion.Enabled = true;

                DataSet dsversion = objBs.getdespatchversion();
                if (dsversion.Tables[0].Rows.Count > 0)
                {
                    ddldespatchversion.DataSource = dsversion.Tables[0];
                    ddldespatchversion.DataTextField = "DcVersion";
                    ddldespatchversion.DataValueField = "DespatchId";
                    ddldespatchversion.DataBind();
                    ddldespatchversion.Items.Insert(0, "ALL");
                }

                #region
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                DataSet dsStock = objBs.getdeloverynotestockversion(drpbranch.SelectedValue);
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Despatch Version Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;

                        GVFinalStock.Columns[30].Visible = true;
                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }


        }

        protected void ddldespatchversion_SelectedIndexChnaged(object sender, EventArgs e)
        {

            #region
            GVFinalStock.Columns[0].Visible = false;
            GVFinalStock.Columns[1].Visible = true;
            GVFinalStock.Columns[2].Visible = false;
            GVFinalStock.Columns[3].Visible = true;
            GVFinalStock.Columns[4].Visible = true;

            GVFinalStock.Columns[5].Visible = true;
            GVFinalStock.Columns[6].Visible = true;
            GVFinalStock.Columns[7].Visible = true;
            GVFinalStock.Columns[8].Visible = true;
            GVFinalStock.Columns[9].Visible = true;
            GVFinalStock.Columns[10].Visible = true;
            GVFinalStock.Columns[11].Visible = true;
            GVFinalStock.Columns[12].Visible = true;
            GVFinalStock.Columns[13].Visible = true;
            GVFinalStock.Columns[14].Visible = true;
            GVFinalStock.Columns[15].Visible = true;
            GVFinalStock.Columns[16].Visible = true;

            GVFinalStock.Columns[17].Visible = true;
            GVFinalStock.Columns[18].Visible = true;
            GVFinalStock.Columns[19].Visible = true;
            GVFinalStock.Columns[20].Visible = true;
            GVFinalStock.Columns[21].Visible = true;
            GVFinalStock.Columns[22].Visible = true;
            GVFinalStock.Columns[23].Visible = true;
            GVFinalStock.Columns[24].Visible = true;
            GVFinalStock.Columns[25].Visible = true;
            GVFinalStock.Columns[26].Visible = true;
            GVFinalStock.Columns[27].Visible = true;
            GVFinalStock.Columns[28].Visible = true;

            #endregion

            GVFinalStock.DataSource = null;
            GVFinalStock.DataBind();

            if (ddlstocktype.SelectedValue == "4")
            {
                #region
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                DataSet dsStock = objBs.getdeloverynotestockversionsingle(drpbranch.SelectedValue, Convert.ToInt32(ddldespatchversion.SelectedValue));
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Despatch Version Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;


                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }

        }
        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region

                if (e.Row.Cells[5].Text != "-" && e.Row.Cells[5].Text != "")
                {
                    F30 = F30 + Convert.ToDouble(e.Row.Cells[5].Text);
                }
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
                if (e.Row.Cells[16].Text != "-" && e.Row.Cells[16].Text != "")
                {
                    F4XL = F4XL + Convert.ToDouble(e.Row.Cells[16].Text);
                }
                if (e.Row.Cells[17].Text != "-" && e.Row.Cells[17].Text != "")
                {

                    H30 = H30 + Convert.ToDouble(e.Row.Cells[17].Text);
                }
                if (e.Row.Cells[18].Text != "-" && e.Row.Cells[18].Text != "")
                {
                    H32 = H32 + Convert.ToDouble(e.Row.Cells[18].Text);
                }
                if (e.Row.Cells[19].Text != "-" && e.Row.Cells[19].Text != "")
                {
                    H34 = H34 + Convert.ToDouble(e.Row.Cells[19].Text);
                }
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

                #endregion

                TOTAL = TOTAL + Convert.ToDouble(e.Row.Cells[29].Text);

                #region




                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "-";
                }
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
                #endregion

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region
                e.Row.Cells[5].Text = F30.ToString();
                e.Row.Cells[6].Text = F32.ToString();
                e.Row.Cells[7].Text = F34.ToString();
                e.Row.Cells[8].Text = F36.ToString();
                e.Row.Cells[9].Text = FXS.ToString();
                e.Row.Cells[10].Text = FS.ToString();
                e.Row.Cells[11].Text = FM.ToString();
                e.Row.Cells[12].Text = FL.ToString();
                e.Row.Cells[13].Text = FXL.ToString();
                e.Row.Cells[14].Text = FXXL.ToString();
                e.Row.Cells[15].Text = F3XL.ToString();
                e.Row.Cells[16].Text = F4XL.ToString();

                e.Row.Cells[17].Text = H30.ToString();
                e.Row.Cells[18].Text = H32.ToString();
                e.Row.Cells[19].Text = H34.ToString();
                e.Row.Cells[20].Text = H36.ToString();
                e.Row.Cells[21].Text = HXS.ToString();
                e.Row.Cells[22].Text = HS.ToString();
                e.Row.Cells[23].Text = HM.ToString();
                e.Row.Cells[24].Text = HL.ToString();
                e.Row.Cells[25].Text = HXL.ToString();
                e.Row.Cells[26].Text = HXXL.ToString();
                e.Row.Cells[27].Text = H3XL.ToString();
                e.Row.Cells[28].Text = H3XL.ToString();


                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[23].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[24].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[25].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[26].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[27].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[28].HorizontalAlign = HorizontalAlign.Right;


                e.Row.Cells[5].Font.Bold = true;
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

                e.Row.Cells[4].Text = "TOTAL ";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Font.Bold = true;

                e.Row.Cells[29].Text = TOTAL.ToString();
                e.Row.Cells[29].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[29].Font.Bold = true;


                #endregion
            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {

            #region
            GVFinalStock.Columns[0].Visible = false;
            GVFinalStock.Columns[1].Visible = true;
            GVFinalStock.Columns[2].Visible = false;
            GVFinalStock.Columns[3].Visible = true;
            GVFinalStock.Columns[4].Visible = true;

            GVFinalStock.Columns[5].Visible = true;
            GVFinalStock.Columns[6].Visible = true;
            GVFinalStock.Columns[7].Visible = true;
            GVFinalStock.Columns[8].Visible = true;
            GVFinalStock.Columns[9].Visible = true;
            GVFinalStock.Columns[10].Visible = true;
            GVFinalStock.Columns[11].Visible = true;
            GVFinalStock.Columns[12].Visible = true;
            GVFinalStock.Columns[13].Visible = true;
            GVFinalStock.Columns[14].Visible = true;
            GVFinalStock.Columns[15].Visible = true;
            GVFinalStock.Columns[16].Visible = true;

            GVFinalStock.Columns[17].Visible = true;
            GVFinalStock.Columns[18].Visible = true;
            GVFinalStock.Columns[19].Visible = true;
            GVFinalStock.Columns[20].Visible = true;
            GVFinalStock.Columns[21].Visible = true;
            GVFinalStock.Columns[22].Visible = true;
            GVFinalStock.Columns[23].Visible = true;
            GVFinalStock.Columns[24].Visible = true;
            GVFinalStock.Columns[25].Visible = true;
            GVFinalStock.Columns[26].Visible = true;
            GVFinalStock.Columns[27].Visible = true;
            GVFinalStock.Columns[28].Visible = true;
            #endregion


            GVFinalStock.DataSource = null;
            GVFinalStock.DataBind();
            if (ddlstocktype.SelectedValue == "1")
            {
                #region
                DataSet dsStock = new DataSet();
                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsStock = objBs.getfinalstocksearch(drpbranch.SelectedValue, txtsearch.Text);
                    }
                    else
                    {
                        dsStock = objBs.getfinalstocksearchfab(drpbranch.SelectedValue, txtsearch.Text);
                    }
                }
                else
                {
                    dsStock = objBs.getfinalstock(drpbranch.SelectedValue);
                }

                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Stock Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;


                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                #endregion
            }
            else if (ddlstocktype.SelectedValue == "2")
            {
                #region
                DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet dsStock = new DataSet();
                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdate(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                    else
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdatefab(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                }
                else
                {
                    dsStock = objBs.getdeloverynotestockdate(drpbranch.SelectedValue, FromDate, ToDate);
                }

                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Stock Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;


                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }
            else if (ddlstocktype.SelectedValue == "3")
            {
                #region
                DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet dsStock = new DataSet();
                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdateret(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                    else
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdateretfab(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                }
                else
                {
                    dsStock = objBs.getdeloverynotestockdateret(drpbranch.SelectedValue, FromDate, ToDate);
                }

                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Stock Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;


                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }
            else if (ddlstocktype.SelectedValue == "4")
            {
                #region
                DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DataSet dsStock = new DataSet();
                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdateversion(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                    else
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdatefabversion(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                }
                else
                {
                    dsStock = objBs.getdeloverynotestockdateversion(drpbranch.SelectedValue, FromDate, ToDate);
                }

                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    GVFinalStock.Caption = "Despatch Version Report ";
                    GVFinalStock.DataSource = dsStock;
                    GVFinalStock.DataBind();

                    if (dsStock.Tables[0].Rows.Count > 0)
                    {
                        #region

                        GVFinalStock.Columns[0].Visible = false;
                        GVFinalStock.Columns[1].Visible = true;
                        GVFinalStock.Columns[2].Visible = false;
                        GVFinalStock.Columns[3].Visible = true;
                        GVFinalStock.Columns[4].Visible = true;

                        GVFinalStock.Columns[5].Visible = false;
                        GVFinalStock.Columns[6].Visible = false;
                        GVFinalStock.Columns[7].Visible = false;
                        GVFinalStock.Columns[8].Visible = false;
                        GVFinalStock.Columns[9].Visible = false;
                        GVFinalStock.Columns[10].Visible = false;
                        GVFinalStock.Columns[11].Visible = false;
                        GVFinalStock.Columns[12].Visible = false;
                        GVFinalStock.Columns[13].Visible = false;
                        GVFinalStock.Columns[14].Visible = false;
                        GVFinalStock.Columns[15].Visible = false;
                        GVFinalStock.Columns[16].Visible = false;

                        GVFinalStock.Columns[17].Visible = false;
                        GVFinalStock.Columns[18].Visible = false;
                        GVFinalStock.Columns[19].Visible = false;
                        GVFinalStock.Columns[20].Visible = false;
                        GVFinalStock.Columns[21].Visible = false;
                        GVFinalStock.Columns[22].Visible = false;
                        GVFinalStock.Columns[23].Visible = false;
                        GVFinalStock.Columns[24].Visible = false;
                        GVFinalStock.Columns[25].Visible = false;
                        GVFinalStock.Columns[26].Visible = false;
                        GVFinalStock.Columns[27].Visible = false;
                        GVFinalStock.Columns[28].Visible = false;


                        for (int j = 0; j < dsStock.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = dsStock.Tables[0].Rows[j]["R30FS"].ToString();
                            string S32 = dsStock.Tables[0].Rows[j]["R32FS"].ToString();
                            string S34 = dsStock.Tables[0].Rows[j]["R34FS"].ToString();
                            string S36 = dsStock.Tables[0].Rows[j]["R36FS"].ToString();
                            string SXS = dsStock.Tables[0].Rows[j]["RxsFS"].ToString();
                            string SS = dsStock.Tables[0].Rows[j]["RsFS"].ToString();
                            string SM = dsStock.Tables[0].Rows[j]["RmFS"].ToString();
                            string SL = dsStock.Tables[0].Rows[j]["RlFS"].ToString();
                            string SXL = dsStock.Tables[0].Rows[j]["RxlFS"].ToString();
                            string SXXL = dsStock.Tables[0].Rows[j]["RxxlFS"].ToString();
                            string S3XL = dsStock.Tables[0].Rows[j]["R3xlFS"].ToString();
                            string S4XL = dsStock.Tables[0].Rows[j]["R4xlFS"].ToString();


                            string HS30 = dsStock.Tables[0].Rows[j]["R30HS"].ToString();
                            string HS32 = dsStock.Tables[0].Rows[j]["R32HS"].ToString();
                            string HS34 = dsStock.Tables[0].Rows[j]["R34HS"].ToString();
                            string HS36 = dsStock.Tables[0].Rows[j]["R36HS"].ToString();
                            string HSXS = dsStock.Tables[0].Rows[j]["RxsHS"].ToString();
                            string HSS = dsStock.Tables[0].Rows[j]["RsHS"].ToString();
                            string HSM = dsStock.Tables[0].Rows[j]["RmHS"].ToString();
                            string HSL = dsStock.Tables[0].Rows[j]["RlHS"].ToString();
                            string HSXL = dsStock.Tables[0].Rows[j]["RxlHS"].ToString();
                            string HSXXL = dsStock.Tables[0].Rows[j]["RxxlHS"].ToString();
                            string HS3XL = dsStock.Tables[0].Rows[j]["R3xlHS"].ToString();
                            string HS4XL = dsStock.Tables[0].Rows[j]["R4xlHS"].ToString();

                            //  int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);

                            //////grndtot = grndtot + tot;
                            //////lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GVFinalStock.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                GVFinalStock.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GVFinalStock.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GVFinalStock.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GVFinalStock.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GVFinalStock.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GVFinalStock.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GVFinalStock.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GVFinalStock.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GVFinalStock.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GVFinalStock.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GVFinalStock.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                GVFinalStock.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                GVFinalStock.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                GVFinalStock.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                GVFinalStock.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                GVFinalStock.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                GVFinalStock.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                GVFinalStock.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                GVFinalStock.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                GVFinalStock.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                GVFinalStock.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                GVFinalStock.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                GVFinalStock.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }

            else if (ddlstocktype.SelectedValue == "5")
            {
                #region
                DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DataSet dsalllot = new DataSet();

                DataSet dsall = new DataSet();
                

                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsall = objBs.getdespatchdatewise1(drpbranch.SelectedValue, FromDate, ToDate, txtsearch.Text);
                     
                    }
                    else
                    {
                        dsall = objBs.getdespatchdatewise2(drpbranch.SelectedValue, FromDate, ToDate, txtsearch.Text);
                       
                    }
                }
                else
                {
                    dsall = objBs.getdespatchdatewise(drpbranch.SelectedValue, FromDate, ToDate);
                   
                }

                if (dsall.Tables[0].Rows.Count > 0)
                {
                   

                    for (int i = 0; i < dsall.Tables[0].Rows.Count; i++)
                    {

                        string CompanyLotNo = dsall.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                        DataSet dsStock = new DataSet();
                        if (txtsearch.Text != "" && txtsearch.Text != "0")
                        {
                            if (ddltype.SelectedValue == "1")
                            {
                                dsStock = objBs.getfinalstocksearchdate(CompanyLotNo);
                            }
                            else
                            {
                                dsStock = objBs.getfinalstocksearchdate1(CompanyLotNo, txtsearch.Text);
                            }
                        }
                        else
                        {
                            dsStock = objBs.getfinalstocksearchdate(CompanyLotNo);
                        }
                      
                        dsalllot.Merge(dsStock);
                    }


                    GVFinalStock.DataSource = dsalllot;
                    GVFinalStock.DataBind();
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            GVFinalStock.DataSource = null;
            GVFinalStock.DataBind();
            DataSet dsStock = new DataSet();

            string filename = "";
            if (ddlstocktype.SelectedValue == "1")
            {

                filename = "Godown Stock Report-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xls";

                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsStock = objBs.getfinalstocksearch(drpbranch.SelectedValue, txtsearch.Text);
                    }
                    else
                    {
                        dsStock = objBs.getfinalstocksearchfab(drpbranch.SelectedValue, txtsearch.Text);
                    }
                }
                else
                {
                    dsStock = objBs.getfinalstock(drpbranch.SelectedValue);
                }

            }
            else if (ddlstocktype.SelectedValue == "2")
            {


                filename = "Despatch Stock Report-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xls";

                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdate(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                    else
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdatefab(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                }
                else
                {
                    dsStock = objBs.getdeloverynotestockdate(drpbranch.SelectedValue, FromDate, ToDate);
                }
            }
            else if (ddlstocktype.SelectedValue == "3")
            {


                filename = "Return Stock Report-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xls";


                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdateret(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                    else
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdateretfab(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }

                }
                else
                {
                    dsStock = objBs.getdeloverynotestockdateret(drpbranch.SelectedValue, FromDate, ToDate);
                }
            }
            else if (ddlstocktype.SelectedValue == "4")
            {


                filename = "Despatch Version Report-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xls";

                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdateversion(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                    else
                    {
                        dsStock = objBs.getdeloverynotsstocksearchdatefabversion(drpbranch.SelectedValue, txtsearch.Text, FromDate, ToDate);
                    }
                }
                else
                {
                    dsStock = objBs.getdeloverynotestockdateversion(drpbranch.SelectedValue, FromDate, ToDate);
                }
            }
            else if (ddlstocktype.SelectedValue == "5")
            {
                #region

                filename = "Despatch Based on Pending Report-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xls";

                DataSet dsStock1 = new DataSet();
                DataSet dsall = new DataSet();


                if (txtsearch.Text != "" && txtsearch.Text != "0")
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dsall = objBs.getdespatchdatewise1(drpbranch.SelectedValue, FromDate, ToDate, txtsearch.Text);

                    }
                    else
                    {
                        dsall = objBs.getdespatchdatewise2(drpbranch.SelectedValue, FromDate, ToDate, txtsearch.Text);

                    }
                }
                else
                {
                    dsall = objBs.getdespatchdatewise(drpbranch.SelectedValue, FromDate, ToDate);

                }

                if (dsall.Tables[0].Rows.Count > 0)
                {


                    for (int i = 0; i < dsall.Tables[0].Rows.Count; i++)
                    {

                        string CompanyLotNo = dsall.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                        if (txtsearch.Text != "" && txtsearch.Text != "0")
                        {
                            if (ddltype.SelectedValue == "1")
                            {
                                dsStock1 = objBs.getfinalstocksearchdate(CompanyLotNo);
                            }
                            else
                            {
                                dsStock1 = objBs.getfinalstocksearchdate1(CompanyLotNo, txtsearch.Text);
                            }
                        }
                        else
                        {
                            dsStock1 = objBs.getfinalstocksearchdate(CompanyLotNo);
                        }
                       
                         dsStock.Merge(dsStock1);
                    }
                }
                else
                {
                    GVFinalStock.DataSource = null;
                    GVFinalStock.DataBind();
                }
                #endregion
            }



            if (dsStock.Tables[0].Rows.Count > 0)
            {

                #region
                DataSet ndstt = new DataSet();
                DataTable ndttt = new DataTable();
                DataColumn ndc = new DataColumn("Fit");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("Design");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("LotNo");
                ndttt.Columns.Add(ndc);

                ndc = new DataColumn("30/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("32/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("34/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("36/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("XS/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("S/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("M/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("L/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("XL/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("XXL/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("3XL/F");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("4XL/F");
                ndttt.Columns.Add(ndc);

                ndc = new DataColumn("30/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("32/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("34/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("36/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("XS/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("S/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("M/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("L/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("XL/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("XXL/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("3XL/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("4XL/H");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("TotalQty");
                ndttt.Columns.Add(ndc);
                ndc = new DataColumn("Version");
                ndttt.Columns.Add(ndc);
                ndstt.Tables.Add(ndttt);
                #endregion

                double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;
                for (int i = 0; i < dsStock.Tables[0].Rows.Count; i++)
                {
                    double RowTotal = 0;

                    #region

                    DataRow ndrd = ndstt.Tables[0].NewRow();
                    ndrd["Fit"] = dsStock.Tables[0].Rows[i]["Fit"].ToString();
                    ndrd["Design"] = dsStock.Tables[0].Rows[i]["DesignCode"].ToString();
                    ndrd["LotNo"] = dsStock.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                    ndrd["30/F"] = dsStock.Tables[0].Rows[i]["R30FS"].ToString();
                    F30 += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R30FS"].ToString());
                    // RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R30FS"].ToString());

                    ndrd["32/F"] = dsStock.Tables[0].Rows[i]["R32FS"].ToString();
                    F32 += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R32FS"].ToString());
                    // RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R32FS"].ToString());

                    ndrd["34/F"] = dsStock.Tables[0].Rows[i]["R34FS"].ToString();
                    F34 += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R34FS"].ToString());
                    // RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R34FS"].ToString());

                    ndrd["36/F"] = dsStock.Tables[0].Rows[i]["R36FS"].ToString();
                    F36 += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R36FS"].ToString());
                    //  RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R36FS"].ToString());

                    ndrd["XS/F"] = dsStock.Tables[0].Rows[i]["RXSFS"].ToString();
                    FXS += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RXSFS"].ToString());
                    //  RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RXSFS"].ToString());

                    ndrd["S/F"] = dsStock.Tables[0].Rows[i]["RSFS"].ToString();
                    FS += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RSFS"].ToString());
                    //   RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RSFS"].ToString());

                    ndrd["M/F"] = dsStock.Tables[0].Rows[i]["RMFS"].ToString();
                    FM += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RMFS"].ToString());
                    //   RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RMFS"].ToString());

                    ndrd["L/F"] = dsStock.Tables[0].Rows[i]["RLFS"].ToString();
                    FL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RLFS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RLFS"].ToString());

                    ndrd["XL/F"] = dsStock.Tables[0].Rows[i]["RXLFS"].ToString();
                    FXL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RXLFS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RXLFS"].ToString());

                    ndrd["XXL/F"] = dsStock.Tables[0].Rows[i]["RXXLFS"].ToString();
                    FXXL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RXXLFS"].ToString());
                    //     RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RXXLFS"].ToString());

                    ndrd["3XL/F"] = dsStock.Tables[0].Rows[i]["R3XLFS"].ToString();
                    F3XL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R3XLFS"].ToString());
                    //   RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R3XLFS"].ToString());

                    ndrd["4XL/F"] = dsStock.Tables[0].Rows[i]["R4XLFS"].ToString();
                    F4XL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R4XLFS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R4XLFS"].ToString());


                    ndrd["30/H"] = dsStock.Tables[0].Rows[i]["R30HS"].ToString();
                    H30 += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R30HS"].ToString());
                    //   RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R30HS"].ToString());

                    ndrd["32/H"] = dsStock.Tables[0].Rows[i]["R32HS"].ToString();
                    H32 += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R32HS"].ToString());
                    //   RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R32HS"].ToString());

                    ndrd["34/H"] = dsStock.Tables[0].Rows[i]["R34HS"].ToString();
                    H34 += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R34HS"].ToString());
                    //   RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R34HS"].ToString());

                    ndrd["36/H"] = dsStock.Tables[0].Rows[i]["R36HS"].ToString();
                    H36 += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R36HS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R36HS"].ToString());

                    ndrd["XS/H"] = dsStock.Tables[0].Rows[i]["RXSHS"].ToString();
                    HXS += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RXSHS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RXSHS"].ToString());

                    ndrd["S/H"] = dsStock.Tables[0].Rows[i]["RSHS"].ToString();
                    HS += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RSHS"].ToString());
                    //     RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RSHS"].ToString());

                    ndrd["M/H"] = dsStock.Tables[0].Rows[i]["RMHS"].ToString();
                    HM += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RMHS"].ToString());
                    //       RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RMHS"].ToString());

                    ndrd["L/H"] = dsStock.Tables[0].Rows[i]["RLHS"].ToString();
                    HL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RLHS"].ToString());
                    //     RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RLHS"].ToString());

                    ndrd["XL/H"] = dsStock.Tables[0].Rows[i]["RXLHS"].ToString();
                    HXL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RXLHS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RXLHS"].ToString());

                    ndrd["XXL/H"] = dsStock.Tables[0].Rows[i]["RXXLHS"].ToString();
                    HXXL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["RXXLHS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["RXXLHS"].ToString());

                    ndrd["3XL/H"] = dsStock.Tables[0].Rows[i]["R3XLHS"].ToString();
                    H3XL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R3XLHS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R3XLHS"].ToString());

                    ndrd["4XL/H"] = dsStock.Tables[0].Rows[i]["R4XLHS"].ToString();
                    H4XL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["R4XLHS"].ToString());
                    //    RowTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["R4XLHS"].ToString());

                    ndrd["TotalQty"] = Convert.ToInt32(dsStock.Tables[0].Rows[i]["TotalQty"].ToString());
                    TOTAL += Convert.ToInt32(dsStock.Tables[0].Rows[i]["TotalQty"].ToString());

                    ndrd["Version"] = dsStock.Tables[0].Rows[i]["Version"].ToString();
                    ndstt.Tables[0].Rows.Add(ndrd);

                    #endregion
                }
                {
                    #region

                    DataRow ndrd1 = ndstt.Tables[0].NewRow();
                    ndrd1["Fit"] = "";
                    ndrd1["Design"] = "";
                    ndrd1["LotNo"] = "Total:-";

                    ndrd1["30/F"] = F30;
                    ndrd1["32/F"] = F32;
                    ndrd1["34/F"] = F34;
                    ndrd1["36/F"] = F36;
                    ndrd1["XS/F"] = FXS;
                    ndrd1["S/F"] = FS;
                    ndrd1["M/F"] = FM;
                    ndrd1["L/F"] = FL;
                    ndrd1["XL/F"] = FXL;
                    ndrd1["XXL/F"] = FXXL;
                    ndrd1["3XL/F"] = F3XL;
                    ndrd1["4XL/F"] = F4XL;

                    ndrd1["30/H"] = H30;
                    ndrd1["32/H"] = H32;
                    ndrd1["34/H"] = H34;
                    ndrd1["36/H"] = H36;
                    ndrd1["XS/H"] = HXS;
                    ndrd1["S/H"] = HS;
                    ndrd1["M/H"] = HM;
                    ndrd1["L/H"] = HL;
                    ndrd1["XL/H"] = HXL;
                    ndrd1["XXL/H"] = HXXL;
                    ndrd1["3XL/H"] = H3XL;
                    ndrd1["4XL/H"] = H4XL;
                    ndrd1["TotalQty"] = TOTAL;
                    ndrd1["Version"] = "";
                    ndstt.Tables[0].Rows.Add(ndrd1);

                    #endregion
                }

                //////   ExportToExcel(ndstt.Tables[0]);

                #region

                #region Caption
                String Caption = "";

                if (ddlstocktype.SelectedValue == "1")
                {
                    if (txtsearch.Text != "" && txtsearch.Text != "0")
                    {
                        Caption = "Report For :-" + drpbranch.SelectedItem.Text + " " + ddlstocktype.SelectedItem.Text + " " + txtsearch.Text;

                    }
                    else
                    {
                        Caption = "Report For :-" + drpbranch.SelectedItem.Text + " " + ddlstocktype.SelectedItem.Text;
                    }

                }
                else
                {
                    if (txtsearch.Text != "" && txtsearch.Text != "0")
                    {
                        Caption = "Report For :- " + drpbranch.SelectedItem.Text + " " + ddlstocktype.SelectedItem.Text + " " + "From :" + txtFromDate.Text + " " + "To :" + txtToDate.Text + " " + txtsearch.Text;
                    }
                    else
                    {
                        Caption = "Report For :-" + drpbranch.SelectedItem.Text + " " + ddlstocktype.SelectedItem.Text + " " + "From :" + txtFromDate.Text + " " + "To :" + txtToDate.Text;

                    }
                }
                #endregion
                ////// string filename = "StockReport-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.Caption = Caption;
                dgGrid.DataSource = ndstt;
                dgGrid.DataBind();
                dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.HeaderStyle.Font.Bold = true;

                dgGrid.FooterStyle.ForeColor = System.Drawing.Color.Black;
                dgGrid.FooterStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                dgGrid.FooterStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.FooterStyle.Font.Bold = true;
                //dgGrid.GridLines = GridLines.Both;
                //dgGrid.GridLines = GridLines.Horizontal;
                //dgGrid.GridLines = GridLines.None;
                //dgGrid.GridLines = GridLines.Vertical;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
                #endregion
            }
            else
            {
                GVFinalStock.DataSource = null;
                GVFinalStock.DataBind();
            }

        }
        public void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                //string filename = "Sales Report.xls";
                string filename = "NewProduct _" + DateTime.Now.ToString() + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                //dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                //dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.HeaderStyle.Font.Bold = true;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }
    }
}