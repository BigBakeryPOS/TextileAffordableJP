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
    public partial class DespatchStock : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();

        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        string Empid = "";
        string CmpyId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();

            sTableName = Session["User"].ToString();
            Empid = Session["Empid"].ToString();

            string DespatchId = string.Empty;
            DespatchId = Request.QueryString.Get("DespatchId");

            CmpyId = Session["CmpyId"].ToString();
            if (!IsPostBack)
            {

                //GridView1.DataSource = DataSet;
                //GridView1.DataBind();

                #region

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
                        drpbranch.Items.Insert(0, "ALL");
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
                        drpbranch_OnSelectedIndexChanged(sender, e);
                       
                    }
                }

                DataSet dss = objBs.getdcemployee();
                if (dss.Tables[0].Rows.Count > 0)
                {
                    ddlpacker.DataSource = dss.Tables[0];
                    ddlpacker.DataTextField = "LedgerName";
                    ddlpacker.DataValueField = "LedgerID";
                    ddlpacker.DataBind();
                    ddlpacker.Items.Insert(0, "Select Despatcher");
                }

                DataSet dss1 = objBs.getcustledger();
                if (dss1.Tables[0].Rows.Count > 0)
                {
                    ddlcustomer.DataSource = dss1.Tables[0];
                    ddlcustomer.DataTextField = "LedgerName";
                    ddlcustomer.DataValueField = "LedgerID";
                    ddlcustomer.DataBind();
                    ddlcustomer.Items.Insert(0, "Select Customer");
                }




                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


                DataSet dsrefno = objBs.getStockforDespatchlotno(drpbranch.SelectedValue);
                if (dsrefno != null)
                {
                    if (dsrefno.Tables[0].Rows.Count > 0)
                    {

                        GVLotqty.DataSource = dsrefno;
                        GVLotqty.DataBind();

                        //chkinvno.DataSource = dsrefno.Tables[0];
                        //chkinvno.DataTextField = "CompanyLotNo";
                        //chkinvno.DataValueField = "CompanyLotNo";
                        ////chkinvno.DataTextField = "LotNo";
                        ////chkinvno.DataValueField = "FinishedStockRatioId";
                        //chkinvno.DataBind();
                    }
                }

                #endregion

                if (DespatchId != null)
                {
                    #region

                    DataSet DsDespatchStock = objBs.geteditforDespatchStock(Convert.ToInt32(DespatchId));
                    if (DsDespatchStock.Tables[0].Rows.Count > 0)
                    {

                        txtdcno.Enabled = false;
                        txtDate.Enabled = false;
                        ddlpacker.Enabled = false;
                        ddlcustomer.Enabled = false;
                       ////// txtnarration.Enabled = false;
                        drpbranch.Enabled = false;

                        GVLotqty.Enabled = false;
                        btnSave.Text = "Update";

                        txtdcno.Text = DsDespatchStock.Tables[0].Rows[0]["DcNo"].ToString();
                        txtDate.Text = Convert.ToDateTime(DsDespatchStock.Tables[0].Rows[0]["DcDate"]).ToString("dd/MM/yyyy");

                        drpbranch.SelectedValue = DsDespatchStock.Tables[0].Rows[0]["Companyid"].ToString();

                        ddlpacker.SelectedValue = DsDespatchStock.Tables[0].Rows[0]["Despacher"].ToString();
                        ddlcustomer.SelectedValue = DsDespatchStock.Tables[0].Rows[0]["Customer"].ToString();

                        txtnarration.Text = DsDespatchStock.Tables[0].Rows[0]["Narration"].ToString();
                        lblalltotalqty.Text = DsDespatchStock.Tables[0].Rows[0]["TotalQty"].ToString();

                        if (DsDespatchStock.Tables[0].Rows.Count > 0)
                        {
                            gvdeliverstock.DataSource = DsDespatchStock;
                            gvdeliverstock.DataBind();

                            #region

                            gvdeliverstock.Columns[0].Visible = false;
                            gvdeliverstock.Columns[1].Visible = false;
                            gvdeliverstock.Columns[2].Visible = true;
                            gvdeliverstock.Columns[3].Visible = true;
                            gvdeliverstock.Columns[4].Visible = true;

                            gvdeliverstock.Columns[5].Visible = false;
                            gvdeliverstock.Columns[6].Visible = false;
                            gvdeliverstock.Columns[7].Visible = false;
                            gvdeliverstock.Columns[8].Visible = false;
                            gvdeliverstock.Columns[9].Visible = false;
                            gvdeliverstock.Columns[10].Visible = false;
                            gvdeliverstock.Columns[11].Visible = false;
                            gvdeliverstock.Columns[12].Visible = false;
                            gvdeliverstock.Columns[13].Visible = false;
                            gvdeliverstock.Columns[14].Visible = false;
                            gvdeliverstock.Columns[15].Visible = false;
                            gvdeliverstock.Columns[16].Visible = false;

                            gvdeliverstock.Columns[17].Visible = false;
                            gvdeliverstock.Columns[18].Visible = false;
                            gvdeliverstock.Columns[19].Visible = false;
                            gvdeliverstock.Columns[20].Visible = false;
                            gvdeliverstock.Columns[21].Visible = false;
                            gvdeliverstock.Columns[22].Visible = false;
                            gvdeliverstock.Columns[23].Visible = false;
                            gvdeliverstock.Columns[24].Visible = false;
                            gvdeliverstock.Columns[25].Visible = false;
                            gvdeliverstock.Columns[26].Visible = false;
                            gvdeliverstock.Columns[27].Visible = false;
                            gvdeliverstock.Columns[28].Visible = false;

                            for (int j = 0; j < DsDespatchStock.Tables[0].Rows.Count; j++)
                            {

                                #region
                                string S30 = DsDespatchStock.Tables[0].Rows[j]["30FS"].ToString();
                                string S32 = DsDespatchStock.Tables[0].Rows[j]["32FS"].ToString();
                                string S34 = DsDespatchStock.Tables[0].Rows[j]["34FS"].ToString();
                                string S36 = DsDespatchStock.Tables[0].Rows[j]["36FS"].ToString();
                                string SXS = DsDespatchStock.Tables[0].Rows[j]["xsFS"].ToString();
                                string SS = DsDespatchStock.Tables[0].Rows[j]["sFS"].ToString();
                                string SM = DsDespatchStock.Tables[0].Rows[j]["mFS"].ToString();
                                string SL = DsDespatchStock.Tables[0].Rows[j]["lFS"].ToString();
                                string SXL = DsDespatchStock.Tables[0].Rows[j]["xlFS"].ToString();
                                string SXXL = DsDespatchStock.Tables[0].Rows[j]["xxlFS"].ToString();
                                string S3XL = DsDespatchStock.Tables[0].Rows[j]["3xlFS"].ToString();
                                string S4XL = DsDespatchStock.Tables[0].Rows[j]["4xlFS"].ToString();


                                string HS30 = DsDespatchStock.Tables[0].Rows[j]["30HS"].ToString();
                                string HS32 = DsDespatchStock.Tables[0].Rows[j]["32HS"].ToString();
                                string HS34 = DsDespatchStock.Tables[0].Rows[j]["34HS"].ToString();
                                string HS36 = DsDespatchStock.Tables[0].Rows[j]["36HS"].ToString();
                                string HSXS = DsDespatchStock.Tables[0].Rows[j]["xsHS"].ToString();
                                string HSS = DsDespatchStock.Tables[0].Rows[j]["sHS"].ToString();
                                string HSM = DsDespatchStock.Tables[0].Rows[j]["mHS"].ToString();
                                string HSL = DsDespatchStock.Tables[0].Rows[j]["lHS"].ToString();
                                string HSXL = DsDespatchStock.Tables[0].Rows[j]["xlHS"].ToString();
                                string HSXXL = DsDespatchStock.Tables[0].Rows[j]["xxlHS"].ToString();
                                string HS3XL = DsDespatchStock.Tables[0].Rows[j]["3xlHS"].ToString();
                                string HS4XL = DsDespatchStock.Tables[0].Rows[j]["4xlHS"].ToString();


                                if (S30 != "0")
                                {

                                    gvdeliverstock.Columns[5].Visible = true;
                                }
                                if (S32 != "0")
                                {

                                    gvdeliverstock.Columns[6].Visible = true;
                                }

                                if (S34 != "0")
                                {

                                    gvdeliverstock.Columns[7].Visible = true;
                                }

                                if (S36 != "0")
                                {

                                    gvdeliverstock.Columns[8].Visible = true;
                                }

                                if (SXS != "0")
                                {

                                    gvdeliverstock.Columns[9].Visible = true;
                                }

                                if (SS != "0")
                                {

                                    gvdeliverstock.Columns[10].Visible = true;
                                }

                                if (SM != "0")
                                {

                                    gvdeliverstock.Columns[11].Visible = true;
                                }

                                if (SL != "0")
                                {

                                    gvdeliverstock.Columns[12].Visible = true;
                                }

                                if (SXL != "0")
                                {

                                    gvdeliverstock.Columns[13].Visible = true;
                                }

                                if (SXXL != "0")
                                {

                                    gvdeliverstock.Columns[14].Visible = true;
                                }

                                if (S3XL != "0")
                                {

                                    gvdeliverstock.Columns[15].Visible = true;
                                }

                                if (S4XL != "0")
                                {

                                    gvdeliverstock.Columns[16].Visible = true;
                                }


                                if (HS30 != "0")
                                {

                                    gvdeliverstock.Columns[17].Visible = true;
                                }
                                if (HS32 != "0")
                                {

                                    gvdeliverstock.Columns[18].Visible = true;
                                }

                                if (HS34 != "0")
                                {

                                    gvdeliverstock.Columns[19].Visible = true;
                                }

                                if (HS36 != "0")
                                {

                                    gvdeliverstock.Columns[20].Visible = true;
                                }

                                if (HSXS != "0")
                                {

                                    gvdeliverstock.Columns[21].Visible = true;
                                }

                                if (HSS != "0")
                                {

                                    gvdeliverstock.Columns[22].Visible = true;
                                }

                                if (HSM != "0")
                                {

                                    gvdeliverstock.Columns[23].Visible = true;
                                }

                                if (HSL != "0")
                                {

                                    gvdeliverstock.Columns[24].Visible = true;
                                }

                                if (HSXL != "0")
                                {

                                    gvdeliverstock.Columns[25].Visible = true;
                                }

                                if (HSXXL != "0")
                                {

                                    gvdeliverstock.Columns[26].Visible = true;
                                }

                                if (HS3XL != "0")
                                {

                                    gvdeliverstock.Columns[27].Visible = true;
                                }

                                if (HS4XL != "0")
                                {

                                    gvdeliverstock.Columns[28].Visible = true;
                                }
                                #endregion

                            }
                            #endregion

                        }

                        double allttldespatchqty = 0;
                        double allttldespatchamt = 0;

                        for (int vLoop = 0; vLoop < gvdeliverstock.Rows.Count; vLoop++)
                        {
                            double ttldespatchqty = 0;
                            double initialttldespatchqty = 0;
                            double ttldespatchamt = 0;



                            #region

                            Label lblFinishedStockRatioId = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblFinishedStockRatioId");
                            Label lblCutid = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCutid");
                            Label lblDesignCode = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblDesignCode");
                            Label lblItemname = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblItemname");
                            Label lblCompanyLotNo = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCompanyLotNo");




                            Label lbl30FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl30FS");
                            TextBox txtR30FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30FS");

                            Label lbl32FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl32FS");
                            TextBox txtR32FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32FS");

                            Label lbl34FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl34FS");
                            TextBox txtR34FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34FS");

                            Label lbl36FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl36FS");
                            TextBox txtR36FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36FS");

                            Label lblXSFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXSFS");
                            TextBox txtRXSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSFS");

                            Label lblSFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblSFS");
                            TextBox txtRSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSFS");

                            Label lblMFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblMFS");
                            TextBox txtRMFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMFS");

                            Label lblLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblLFS");
                            TextBox txtRLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLFS");

                            Label lblXLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXLFS");
                            TextBox txtRXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLFS");

                            Label lblXXLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXXLFS");
                            TextBox txtRXXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLFS");

                            Label lbl3XLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl3XLFS");
                            TextBox txtR3XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLFS");

                            Label lbl4XLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl4XLFS");
                            TextBox txtR4XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLFS");

                            Label lbl30HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl30HS");
                            TextBox txtR30HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30HS");

                            Label lbl32HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl32HS");
                            TextBox txtR32HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32HS");

                            Label lbl34HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl34HS");
                            TextBox txtR34HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34HS");

                            Label lbl36HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl36HS");
                            TextBox txtR36HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36HS");

                            Label lblXSHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXSHS");
                            TextBox txtRXSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSHS");

                            Label lblSHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblSHS");
                            TextBox txtRSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSHS");

                            Label lblMHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblMHS");
                            TextBox txtRMHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMHS");

                            Label lblLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblLHS");
                            TextBox txtRLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLHS");

                            Label lblXLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXLHS");
                            TextBox txtRXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLHS");

                            Label lblXXLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXXLHS");
                            TextBox txtRXXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLHS");

                            Label lbl3XLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl3XLHS");
                            TextBox txtR3XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLHS");

                            Label lbl4XLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl4XLHS");
                            TextBox txtR4XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLHS");

                            Label lbllotqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbllotqty");
                            Label lbldespatchqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbldespatchqty");

                            lbl30FS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["30FS"].ToString();
                            txtR30FS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["30FS"].ToString();

                            lbl32FS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["32FS"].ToString();
                            txtR32FS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["32FS"].ToString();

                            lbl34FS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["34FS"].ToString();
                            txtR34FS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["34FS"].ToString();

                            lbl36FS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["36FS"].ToString();
                            txtR36FS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["36FS"].ToString();

                            lblXSFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XSFS"].ToString();
                            txtRXSFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XSFS"].ToString();

                            lblSFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["SFS"].ToString();
                            txtRSFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["SFS"].ToString();

                            lblMFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["MFS"].ToString();
                            txtRMFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["MFS"].ToString();

                            lblLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["LFS"].ToString();
                            txtRLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["LFS"].ToString();

                            lblXLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XLFS"].ToString();
                            txtRXLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XLFS"].ToString();

                            lblXXLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XXLFS"].ToString();
                            txtRXXLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XXLFS"].ToString();

                            lbl3XLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["3XLFS"].ToString();
                            txtR3XLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["3XLFS"].ToString();

                            lbl4XLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["4XLFS"].ToString();
                            txtR4XLFS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["4XLFS"].ToString();

                            lbl30HS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["30HS"].ToString();
                            txtR30HS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["30HS"].ToString();

                            lbl32HS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["32HS"].ToString();
                            txtR32HS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["32HS"].ToString();

                            lbl34HS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["34HS"].ToString();
                            txtR34HS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["34HS"].ToString();

                            lbl36HS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["36HS"].ToString();
                            txtR36HS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["36HS"].ToString();

                            lblXSHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XSHS"].ToString();
                            txtRXSHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XSHS"].ToString();

                            lblSHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["SHS"].ToString();
                            txtRSHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["SHS"].ToString();

                            lblMHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["MHS"].ToString();
                            txtRMHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["MHS"].ToString();

                            lblLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["LHS"].ToString();
                            txtRLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["LHS"].ToString();

                            lblXLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XLHS"].ToString();
                            txtRXLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XLHS"].ToString();

                            lblXXLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XXLHS"].ToString();
                            txtRXXLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["XXLHS"].ToString();

                            lbl3XLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["3XLHS"].ToString();
                            txtR3XLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["3XLHS"].ToString();

                            lbl4XLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["4XLHS"].ToString();
                            txtR4XLHS.Text = DsDespatchStock.Tables[0].Rows[vLoop]["4XLHS"].ToString();

                            #region Rate
                            Label lblr30fb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30f");
                            Label lblr32fb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32f");
                            Label lblr34fb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34f");
                            Label lblr36fb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36f");
                            Label lblrXSfb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSf");
                            Label lblrSfb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSf");
                            Label lblrMfb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMf");
                            Label lblrLfb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLf");
                            Label lblrXLfb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLf");
                            Label lblrxxLfb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrxxLf");
                            Label lblr3XLfb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLf");
                            Label lblr4XLfb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLf");
                            Label lblr30hb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30h");
                            Label lblr32hb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32h");
                            Label lblr34hb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34h");
                            Label lblr36hb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36h");
                            Label lblrXShb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSh");
                            Label lblrShb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSh");
                            Label lblrMhb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMh");
                            Label lblrLhb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLh");
                            Label lblrXLhb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLh");
                            Label lblrXXLhb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXXLh");
                            Label lblr3XLhb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLh");
                            Label lblr4XLhb = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLh");


                            lblr30fb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R30F"]).ToString("f2");
                            lblr32fb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R32F"]).ToString("f2");
                            lblr34fb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R34F"]).ToString("f2");
                            lblr36fb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R36F"]).ToString("f2");
                            lblrXSfb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["RxsF"]).ToString("f2");
                            lblrSfb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["RsF"]).ToString("f2");
                            lblrMfb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["RmF"]).ToString("f2");
                            lblrLfb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["RlF"]).ToString("f2");
                            lblrXLfb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["RxlF"]).ToString("f2");
                            lblrxxLfb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["RxxlF"]).ToString("f2");
                            lblr3XLfb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R3xlF"]).ToString("f2");
                            lblr4XLfb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R4xlF"]).ToString("f2");

                            lblr30hb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R30h"]).ToString("f2");
                            lblr32hb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R32h"]).ToString("f2");
                            lblr34hb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R34h"]).ToString("f2");
                            lblr36hb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R36h"]).ToString("f2");
                            lblrXShb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["Rxsh"]).ToString("f2");
                            lblrShb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["Rsh"]).ToString("f2");
                            lblrMhb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["Rmh"]).ToString("f2");
                            lblrLhb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["Rlh"]).ToString("f2");
                            lblrXLhb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["Rxlh"]).ToString("f2");
                            lblrXXLhb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["Rxxlh"]).ToString("f2");
                            lblr3XLhb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R3xlh"]).ToString("f2");
                            lblr4XLhb.Text = Convert.ToDouble(DsDespatchStock.Tables[0].Rows[vLoop]["R4xlh"]).ToString("f2");

                            #endregion

                            #region Qty Amt Calculation

                            Label lblr30f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30f");
                            Label lblr32f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32f");
                            Label lblr34f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34f");
                            Label lblr36f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36f");
                            Label lblrXSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSf");
                            Label lblrSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSf");
                            Label lblrMf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMf");
                            Label lblrLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLf");
                            Label lblrXLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLf");
                            Label lblrxxLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrxxLf");
                            Label lblr3XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLf");
                            Label lblr4XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLf");
                            Label lblr30h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30h");
                            Label lblr32h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32h");
                            Label lblr34h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34h");
                            Label lblr36h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36h");
                            Label lblrXSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSh");
                            Label lblrSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSh");
                            Label lblrMh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMh");
                            Label lblrLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLh");
                            Label lblrXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLh");
                            Label lblrXXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXXLh");
                            Label lblr3XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLh");
                            Label lblr4XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLh");

                            Label lblamtf30 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf30");
                            Label lblamtf32 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf32");
                            Label lblamtf34 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf34");
                            Label lblamtf36 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf36");
                            Label lblamtfxs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxs");
                            Label lblamtfs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfs");
                            Label lblamtfm = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfm");
                            Label lblamtfl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfl");
                            Label lblamtfxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxl");
                            Label lblamtfxxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxxl");
                            Label lblamtf3xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf3xl");
                            Label lblamtf4xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf4xl");
                            Label lblamth30 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth30");
                            Label lblamth32 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth32");
                            Label lblamth34 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth34");
                            Label lblamth36 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth36");
                            Label lblamthxs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxs");
                            Label lblamths = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamths");
                            Label lblamthm = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthm");
                            Label lblamthl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthl");
                            Label lblamthxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxl");
                            Label lblamthxxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxxl");
                            Label lblamth3xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth3xl");
                            Label lblamth4xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth4xl");

                            lblamtf30.Text = (Convert.ToInt32(txtR30FS.Text) * Convert.ToDouble(lblr30f.Text)).ToString("f2");
                            lblamtf32.Text = (Convert.ToInt32(txtR32FS.Text) * Convert.ToDouble(lblr32f.Text)).ToString("f2");
                            lblamtf34.Text = (Convert.ToInt32(txtR34FS.Text) * Convert.ToDouble(lblr34f.Text)).ToString("f2");
                            lblamtf36.Text = (Convert.ToInt32(txtR36FS.Text) * Convert.ToDouble(lblr36f.Text)).ToString("f2");
                            lblamtfxs.Text = (Convert.ToInt32(txtRXSFS.Text) * Convert.ToDouble(lblrXSf.Text)).ToString("f2");
                            lblamtfs.Text = (Convert.ToInt32(txtRSFS.Text) * Convert.ToDouble(lblrSf.Text)).ToString("f2");
                            lblamtfm.Text = (Convert.ToInt32(txtRMFS.Text) * Convert.ToDouble(lblrMf.Text)).ToString("f2");
                            lblamtfl.Text = (Convert.ToInt32(txtRLFS.Text) * Convert.ToDouble(lblrLf.Text)).ToString("f2");
                            lblamtfxl.Text = (Convert.ToInt32(txtRXLFS.Text) * Convert.ToDouble(lblrXLf.Text)).ToString("f2");
                            lblamtfxxl.Text = (Convert.ToInt32(txtRXXLFS.Text) * Convert.ToDouble(lblrxxLf.Text)).ToString("f2");
                            lblamtf3xl.Text = (Convert.ToInt32(txtR3XLFS.Text) * Convert.ToDouble(lblr3XLf.Text)).ToString("f2");
                            lblamtf4xl.Text = (Convert.ToInt32(txtR4XLFS.Text) * Convert.ToDouble(lblr4XLf.Text)).ToString("f2");

                            lblamth30.Text = (Convert.ToInt32(txtR30HS.Text) * Convert.ToDouble(lblr30h.Text)).ToString("f2");
                            lblamth32.Text = (Convert.ToInt32(txtR32HS.Text) * Convert.ToDouble(lblr32h.Text)).ToString("f2");
                            lblamth34.Text = (Convert.ToInt32(txtR34HS.Text) * Convert.ToDouble(lblr34h.Text)).ToString("f2");
                            lblamth36.Text = (Convert.ToInt32(txtR36HS.Text) * Convert.ToDouble(lblr36h.Text)).ToString("f2");
                            lblamthxs.Text = (Convert.ToInt32(txtRXSHS.Text) * Convert.ToDouble(lblrXSh.Text)).ToString("f2");
                            lblamths.Text = (Convert.ToInt32(txtRSHS.Text) * Convert.ToDouble(lblrSh.Text)).ToString("f2");
                            lblamthm.Text = (Convert.ToInt32(txtRMHS.Text) * Convert.ToDouble(lblrMh.Text)).ToString("f2");
                            lblamthl.Text = (Convert.ToInt32(txtRLHS.Text) * Convert.ToDouble(lblrLh.Text)).ToString("f2");
                            lblamthxl.Text = (Convert.ToInt32(txtRXLHS.Text) * Convert.ToDouble(lblrXLh.Text)).ToString("f2");
                            lblamthxxl.Text = (Convert.ToInt32(txtRXXLHS.Text) * Convert.ToDouble(lblrXXLh.Text)).ToString("f2");
                            lblamth3xl.Text = (Convert.ToInt32(txtR3XLHS.Text) * Convert.ToDouble(lblr3XLh.Text)).ToString("f2");
                            lblamth4xl.Text = (Convert.ToInt32(txtR4XLHS.Text) * Convert.ToDouble(lblr4XLh.Text)).ToString("f2");

                            ttldespatchamt = Convert.ToDouble(lblamtf30.Text) + Convert.ToDouble(lblamtf32.Text) + Convert.ToDouble(lblamtf34.Text) + Convert.ToDouble(lblamtf36.Text) +
                                           Convert.ToDouble(lblamtfxs.Text) + Convert.ToDouble(lblamtfs.Text) + Convert.ToDouble(lblamtfm.Text) + Convert.ToDouble(lblamtfl.Text) +
                                           Convert.ToDouble(lblamtfxl.Text) + Convert.ToDouble(lblamtfxxl.Text) + Convert.ToDouble(lblamtf3xl.Text) + Convert.ToDouble(lblamtf4xl.Text) +
                                           Convert.ToDouble(lblamth30.Text) + Convert.ToDouble(lblamth32.Text) + Convert.ToDouble(lblamth34.Text) + Convert.ToDouble(lblamth36.Text) +
                                           Convert.ToDouble(lblamthxs.Text) + Convert.ToDouble(lblamths.Text) + Convert.ToDouble(lblamthm.Text) + Convert.ToDouble(lblamthl.Text) +
                                           Convert.ToDouble(lblamthxl.Text) + Convert.ToDouble(lblamthxxl.Text) + Convert.ToDouble(lblamth3xl.Text) + Convert.ToDouble(lblamth4xl.Text);

                            Label lblttlamt = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblttlamt");
                            lblttlamt.Text = ttldespatchamt.ToString("f2");
                            allttldespatchamt = allttldespatchamt + ttldespatchamt;

                            #endregion


                            lblFinishedStockRatioId.Text = DsDespatchStock.Tables[0].Rows[vLoop]["FinishedStockRatioId"].ToString();
                            lblCutid.Text = "0";// DsDespatchStock.Tables[0].Rows[vLoop]["Cutid"].ToString();
                            lblItemname.Text = DsDespatchStock.Tables[0].Rows[vLoop]["Itemname"].ToString();
                            lblCompanyLotNo.Text = DsDespatchStock.Tables[0].Rows[vLoop]["CompanyLotNo"].ToString();
                            lblDesignCode.Text = DsDespatchStock.Tables[0].Rows[vLoop]["DesignCode"].ToString();

                            ttldespatchqty = Convert.ToDouble(txtR30FS.Text) + Convert.ToDouble(txtR32FS.Text) + Convert.ToDouble(txtR34FS.Text) + Convert.ToDouble(txtR36FS.Text) +
                                      Convert.ToDouble(txtRXSFS.Text) + Convert.ToDouble(txtRSFS.Text) + Convert.ToDouble(txtRMFS.Text) + Convert.ToDouble(txtRLFS.Text) +
                                      Convert.ToDouble(txtRXLFS.Text) + Convert.ToDouble(txtRXXLFS.Text) + Convert.ToDouble(txtR3XLFS.Text) + Convert.ToDouble(txtR4XLFS.Text) +
                                      Convert.ToDouble(txtR30HS.Text) + Convert.ToDouble(txtR32HS.Text) + Convert.ToDouble(txtR34HS.Text) + Convert.ToDouble(txtR36HS.Text) +
                                      Convert.ToDouble(txtRXSHS.Text) + Convert.ToDouble(txtRSHS.Text) + Convert.ToDouble(txtRMHS.Text) + Convert.ToDouble(txtRLHS.Text) +
                                      Convert.ToDouble(txtRXLHS.Text) + Convert.ToDouble(txtRXXLHS.Text) + Convert.ToDouble(txtR3XLHS.Text) + Convert.ToDouble(txtR4XLHS.Text);
                            allttldespatchqty = allttldespatchqty + ttldespatchqty;

                            initialttldespatchqty = Convert.ToDouble(lbl30FS.Text) + Convert.ToDouble(lbl32FS.Text) + Convert.ToDouble(lbl34FS.Text) + Convert.ToDouble(lbl36FS.Text) +
                                                         Convert.ToDouble(lblXSFS.Text) + Convert.ToDouble(lblSFS.Text) + Convert.ToDouble(lblMFS.Text) + Convert.ToDouble(lblLFS.Text) +
                                                         Convert.ToDouble(lblXLFS.Text) + Convert.ToDouble(lblXXLFS.Text) + Convert.ToDouble(lbl3XLFS.Text) + Convert.ToDouble(lbl4XLFS.Text) +
                                                         Convert.ToDouble(lbl30HS.Text) + Convert.ToDouble(lbl32HS.Text) + Convert.ToDouble(lbl34HS.Text) + Convert.ToDouble(lbl36HS.Text) +
                                                         Convert.ToDouble(lblXSHS.Text) + Convert.ToDouble(lblSHS.Text) + Convert.ToDouble(lblMHS.Text) + Convert.ToDouble(lblLHS.Text) +
                                                         Convert.ToDouble(lblXLHS.Text) + Convert.ToDouble(lblXXLHS.Text) + Convert.ToDouble(lbl3XLHS.Text) + Convert.ToDouble(lbl4XLHS.Text);


                            lbllotqty.Text = initialttldespatchqty.ToString();
                            lbldespatchqty.Text = initialttldespatchqty.ToString();

                            #endregion

                        }
                        lblalltotalqty.Text = allttldespatchqty.ToString();
                        lblalltotalamt.Text = allttldespatchamt.ToString("f2");




                    }
                    #endregion
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }
        protected void drpbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet dsdcno = objBs.getdespatchno(drpbranch.SelectedValue);
            txtdcno.Text = dsdcno.Tables[0].Rows[0]["DcNo"].ToString();


            gvdeliverstock.DataSource = null;
            gvdeliverstock.DataBind();

            DataSet dsrefno = objBs.getStockforDespatchlotno(drpbranch.SelectedValue);
            if (dsrefno != null)
            {
                if (dsrefno.Tables[0].Rows.Count > 0)
                {

                    GVLotqty.DataSource = dsrefno;
                    GVLotqty.DataBind();

                   
                }
            }
        }

        protected void txtdcno_OnTextChanged(object sender, EventArgs e)
        {
            DataSet dsdcno = objBs.getdespatchnouser(txtdcno.Text, drpbranch.SelectedValue);
            if (dsdcno.Tables[0].Rows.Count > 0)
            {
                btnSave.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This DcNo Alread Inserted Use Any Other. Thank you !!!');", true);
                return;
            }
            else
            {
                btnSave.Enabled = true;
            }

        }
        protected void chkinvnochanged(object sender, EventArgs e)
        {
            gvdeliverstock.DataSource = null;
            gvdeliverstock.DataBind();
            double allttldespatchqty = 0;
            DataSet dsmerge = new DataSet();
            // if (chkinvno.SelectedIndex >= 0)
            {
                // foreach (ListItem item in chkinvno.Items)
                for (int vLoop = 0; vLoop < GVLotqty.Rows.Count; vLoop++)
                {
                    CheckBox item = (CheckBox)GVLotqty.Rows[vLoop].FindControl("chkitemchecked");

                    if (item.Checked == true)
                    {
                        DataSet ds = new DataSet();

                        ds = objBs.getselectstkforlot(item.Text, drpbranch.SelectedValue);


                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dsmerge.Merge(ds);
                            }
                        }
                    }
                }
                gvdeliverstock.DataSource = dsmerge;
                gvdeliverstock.DataBind();


                #region

                //DataSet dstt1 = new DataSet();
                //DataTable dttt1 = new DataTable();

                //DataColumn dc1 = new DataColumn("DesignCode");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("Itemname");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("CompanyLotNo");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R30F");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R32F");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R34F");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R36F");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RXSF");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RSF");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RMF");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RLF");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RXLF");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RXXLF");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R3XLF");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R4XLF");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R30H");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R32H");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R34H");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R36H");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RXSH");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RSH");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RMH");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RLH");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RXLH");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("RXXLH");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R3XLH");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("R4XLH");
                //dttt1.Columns.Add(dc1);
                //dc1 = new DataColumn("TTl");
                //dttt1.Columns.Add(dc1);
                //dstt1.Tables.Add(dttt1);

                //DataRow drd1 = dstt1.Tables[0].NewRow();
                //drd1["DesignCode"] = "DesignCode";
                //drd1["Itemname"] ="Itemname";
                //drd1["CompanyLotNo"] = "LotNo";

                //drd1["R30F"] = 1;
                //drd1["R32F"] = 1;
                //drd1["R34F"] = 1;
                //drd1["R36F"] = 1;
                //drd1["RXSF"] = 1;
                //drd1["RSF"] = 1;
                //drd1["RMF"] = 1;
                //drd1["RLF"] = 1;
                //drd1["RXLF"] = 1;
                //drd1["RXXLF"] = 1;
                //drd1["R3XLF"] = 1;
                //drd1["R4XLF"] = 1;

                //drd1["R30H"] = 1;
                //drd1["R32H"] = 1;
                //drd1["R34H"] = 1;
                //drd1["R36H"] = 1;
                //drd1["RXSH"] = 1;
                //drd1["RSH"] = 1;
                //drd1["RMH"] = 1;
                //drd1["RLH"] = 1;
                //drd1["RXLH"] = 1;
                //drd1["RXXLH"] = 1;
                //drd1["R3XLH"] = 1;
                //drd1["R4XLH"] = 1;

                //dstt1.Tables[0].Rows.Add(drd1);

                //GridView1.DataSource = dstt1;
                //GridView1.DataBind();


                #endregion


                //DataSet Dset = new DataSet();
                //Dset.Tables[0].Rows.Add(Dset.Tables[0].NewRow());
                //GridView1.DataSource = Dset;
                //GridView1.DataBind();

                if (dsmerge.Tables[0].Rows.Count > 0)
                {
                    gvdeliverstock.DataSource = dsmerge;
                    gvdeliverstock.DataBind();

                    gvdeliverstock.Columns[0].Visible = false;
                    gvdeliverstock.Columns[1].Visible = false;
                    gvdeliverstock.Columns[2].Visible = true;
                    gvdeliverstock.Columns[3].Visible = true;
                    gvdeliverstock.Columns[4].Visible = true;

                    gvdeliverstock.Columns[5].Visible = false;
                    gvdeliverstock.Columns[6].Visible = false;
                    gvdeliverstock.Columns[7].Visible = false;
                    gvdeliverstock.Columns[8].Visible = false;
                    gvdeliverstock.Columns[9].Visible = false;
                    gvdeliverstock.Columns[10].Visible = false;
                    gvdeliverstock.Columns[11].Visible = false;
                    gvdeliverstock.Columns[12].Visible = false;
                    gvdeliverstock.Columns[13].Visible = false;
                    gvdeliverstock.Columns[14].Visible = false;
                    gvdeliverstock.Columns[15].Visible = false;
                    gvdeliverstock.Columns[16].Visible = false;

                    gvdeliverstock.Columns[17].Visible = false;
                    gvdeliverstock.Columns[18].Visible = false;
                    gvdeliverstock.Columns[19].Visible = false;
                    gvdeliverstock.Columns[20].Visible = false;
                    gvdeliverstock.Columns[21].Visible = false;
                    gvdeliverstock.Columns[22].Visible = false;
                    gvdeliverstock.Columns[23].Visible = false;
                    gvdeliverstock.Columns[24].Visible = false;
                    gvdeliverstock.Columns[25].Visible = false;
                    gvdeliverstock.Columns[26].Visible = false;
                    gvdeliverstock.Columns[27].Visible = false;
                    gvdeliverstock.Columns[28].Visible = false;

                    for (int j = 0; j < dsmerge.Tables[0].Rows.Count; j++)
                    {

                        #region
                        string S30 = dsmerge.Tables[0].Rows[j]["30FS"].ToString();
                        string S32 = dsmerge.Tables[0].Rows[j]["32FS"].ToString();
                        string S34 = dsmerge.Tables[0].Rows[j]["34FS"].ToString();
                        string S36 = dsmerge.Tables[0].Rows[j]["36FS"].ToString();
                        string SXS = dsmerge.Tables[0].Rows[j]["xsFS"].ToString();
                        string SS = dsmerge.Tables[0].Rows[j]["sFS"].ToString();
                        string SM = dsmerge.Tables[0].Rows[j]["mFS"].ToString();
                        string SL = dsmerge.Tables[0].Rows[j]["lFS"].ToString();
                        string SXL = dsmerge.Tables[0].Rows[j]["xlFS"].ToString();
                        string SXXL = dsmerge.Tables[0].Rows[j]["xxlFS"].ToString();
                        string S3XL = dsmerge.Tables[0].Rows[j]["3xlFS"].ToString();
                        string S4XL = dsmerge.Tables[0].Rows[j]["4xlFS"].ToString();


                        string HS30 = dsmerge.Tables[0].Rows[j]["30HS"].ToString();
                        string HS32 = dsmerge.Tables[0].Rows[j]["32HS"].ToString();
                        string HS34 = dsmerge.Tables[0].Rows[j]["34HS"].ToString();
                        string HS36 = dsmerge.Tables[0].Rows[j]["36HS"].ToString();
                        string HSXS = dsmerge.Tables[0].Rows[j]["xsHS"].ToString();
                        string HSS = dsmerge.Tables[0].Rows[j]["sHS"].ToString();
                        string HSM = dsmerge.Tables[0].Rows[j]["mHS"].ToString();
                        string HSL = dsmerge.Tables[0].Rows[j]["lHS"].ToString();
                        string HSXL = dsmerge.Tables[0].Rows[j]["xlHS"].ToString();
                        string HSXXL = dsmerge.Tables[0].Rows[j]["xxlHS"].ToString();
                        string HS3XL = dsmerge.Tables[0].Rows[j]["3xlHS"].ToString();
                        string HS4XL = dsmerge.Tables[0].Rows[j]["4xlHS"].ToString();

                        //   int tot = Convert.ToInt32(dsmerge.Tables[0].Rows[j]["Total"]);

                        //////grndtot = grndtot + tot;
                        //////lblstockgrandtot.Text = grndtot.ToString();

                        if (S30 != "0")
                        {
                            //  idf30.Visible = true;
                            // GridView1.Columns[3].Visible = true;
                            gvdeliverstock.Columns[5].Visible = true;
                        }
                        if (S32 != "0")
                        {
                            //   idf32.Visible = true;
                            // GridView1.Columns[4].Visible = true;
                            gvdeliverstock.Columns[6].Visible = true;
                        }

                        if (S34 != "0")
                        {
                            // idf34.Visible = true;
                            // GridView1.Columns[5].Visible = true;
                            gvdeliverstock.Columns[7].Visible = true;
                        }

                        if (S36 != "0")
                        {
                            // idf36.Visible = true;
                            //GridView1.Columns[6].Visible = true;
                            gvdeliverstock.Columns[8].Visible = true;
                        }

                        if (SXS != "0")
                        {
                            // idfxs.Visible = true;
                            // GridView1.Columns[7].Visible = true;
                            gvdeliverstock.Columns[9].Visible = true;
                        }

                        if (SS != "0")
                        {
                            // idfs.Visible = true;
                            // GridView1.Columns[8].Visible = true;
                            gvdeliverstock.Columns[10].Visible = true;
                        }

                        if (SM != "0")
                        {
                            // idfm.Visible = true;
                            // GridView1.Columns[9].Visible = true;
                            gvdeliverstock.Columns[11].Visible = true;
                        }

                        if (SL != "0")
                        {
                            // idfl.Visible = true;
                            //  GridView1.Columns[10].Visible = true;
                            gvdeliverstock.Columns[12].Visible = true;
                        }

                        if (SXL != "0")
                        {
                            // idfxl.Visible = true;
                            //   GridView1.Columns[11].Visible = true;
                            gvdeliverstock.Columns[13].Visible = true;
                        }

                        if (SXXL != "0")
                        {
                            //  idfxxl.Visible = true;
                            //  GridView1.Columns[12].Visible = true;
                            gvdeliverstock.Columns[14].Visible = true;
                        }

                        if (S3XL != "0")
                        {
                            //   idf3xl.Visible = true;
                            //  GridView1.Columns[13].Visible = true;
                            gvdeliverstock.Columns[15].Visible = true;
                        }

                        if (S4XL != "0")
                        {
                            // idf4xl.Visible = true;
                            //  GridView1.Columns[14].Visible = true;
                            gvdeliverstock.Columns[16].Visible = true;
                        }


                        if (HS30 != "0")
                        {
                            //   idh30.Visible = true;
                            //  GridView1.Columns[15].Visible = true;
                            gvdeliverstock.Columns[17].Visible = true;
                        }
                        if (HS32 != "0")
                        {
                            // idh32.Visible = true;
                            //  GridView1.Columns[16].Visible = true;
                            gvdeliverstock.Columns[18].Visible = true;
                        }

                        if (HS34 != "0")
                        {
                            //  idh34.Visible = true;
                            //  GridView1.Columns[17].Visible = true;
                            gvdeliverstock.Columns[19].Visible = true;
                        }

                        if (HS36 != "0")
                        {
                            // idh36.Visible = true;
                            //  GridView1.Columns[18].Visible = true;
                            gvdeliverstock.Columns[20].Visible = true;
                        }

                        if (HSXS != "0")
                        {
                            // idhxs.Visible = true;
                            //  GridView1.Columns[19].Visible = true;
                            gvdeliverstock.Columns[21].Visible = true;
                        }

                        if (HSS != "0")
                        {
                            //  idhs.Visible = true;
                            //    GridView1.Columns[20].Visible = true;
                            gvdeliverstock.Columns[22].Visible = true;
                        }

                        if (HSM != "0")
                        {
                            //  idhm.Visible = true;
                            //    GridView1.Columns[21].Visible = true;
                            gvdeliverstock.Columns[23].Visible = true;
                        }

                        if (HSL != "0")
                        {
                            // idhl.Visible = true;
                            //  GridView1.Columns[22].Visible = true;
                            gvdeliverstock.Columns[24].Visible = true;
                        }

                        if (HSXL != "0")
                        {
                            //  idhxl.Visible = true;
                            //   GridView1.Columns[23].Visible = true;
                            gvdeliverstock.Columns[25].Visible = true;
                        }

                        if (HSXXL != "0")
                        {
                            //   idhxxl.Visible = true;
                            //   GridView1.Columns[24].Visible = true;
                            gvdeliverstock.Columns[26].Visible = true;
                        }

                        if (HS3XL != "0")
                        {
                            //   idh3xl.Visible = true;
                            //   GridView1.Columns[25].Visible = true;
                            gvdeliverstock.Columns[27].Visible = true;
                        }

                        if (HS4XL != "0")
                        {
                            // idh4xl.Visible = true;
                            //  GridView1.Columns[26].Visible = true;
                            gvdeliverstock.Columns[28].Visible = true;
                        }
                        #endregion

                    }


                }


                for (int vLoop = 0; vLoop < gvdeliverstock.Rows.Count; vLoop++)
                {
                    double ttldespatchqty = 0;
                    double initialttldespatchqty = 0;

                    #region

                    Label lblFinishedStockRatioId = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblFinishedStockRatioId");
                    Label lblCutid = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCutid");
                    Label lblDesignCode = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblDesignCode");
                    Label lblItemname = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblItemname");
                    Label lblCompanyLotNo = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCompanyLotNo");




                    Label lbl30FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl30FS");
                    TextBox txtR30FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30FS");

                    Label lbl32FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl32FS");
                    TextBox txtR32FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32FS");

                    Label lbl34FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl34FS");
                    TextBox txtR34FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34FS");

                    Label lbl36FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl36FS");
                    TextBox txtR36FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36FS");

                    Label lblXSFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXSFS");
                    TextBox txtRXSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSFS");

                    Label lblSFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblSFS");
                    TextBox txtRSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSFS");

                    Label lblMFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblMFS");
                    TextBox txtRMFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMFS");

                    Label lblLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblLFS");
                    TextBox txtRLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLFS");

                    Label lblXLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXLFS");
                    TextBox txtRXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLFS");

                    Label lblXXLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXXLFS");
                    TextBox txtRXXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLFS");

                    Label lbl3XLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl3XLFS");
                    TextBox txtR3XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLFS");

                    Label lbl4XLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl4XLFS");
                    TextBox txtR4XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLFS");

                    Label lbl30HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl30HS");
                    TextBox txtR30HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30HS");

                    Label lbl32HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl32HS");
                    TextBox txtR32HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32HS");

                    Label lbl34HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl34HS");
                    TextBox txtR34HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34HS");

                    Label lbl36HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl36HS");
                    TextBox txtR36HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36HS");

                    Label lblXSHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXSHS");
                    TextBox txtRXSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSHS");

                    Label lblSHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblSHS");
                    TextBox txtRSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSHS");

                    Label lblMHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblMHS");
                    TextBox txtRMHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMHS");

                    Label lblLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblLHS");
                    TextBox txtRLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLHS");

                    Label lblXLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXLHS");
                    TextBox txtRXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLHS");

                    Label lblXXLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXXLHS");
                    TextBox txtRXXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLHS");

                    Label lbl3XLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl3XLHS");
                    TextBox txtR3XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLHS");

                    Label lbl4XLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl4XLHS");
                    TextBox txtR4XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLHS");

                    Label lbllotqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbllotqty");
                    Label lbldespatchqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbldespatchqty");


                    #region Rate
                    Label lblr30f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30f");
                    Label lblr32f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32f");
                    Label lblr34f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34f");
                    Label lblr36f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36f");
                    Label lblrXSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSf");
                    Label lblrSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSf");
                    Label lblrMf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMf");
                    Label lblrLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLf");
                    Label lblrXLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLf");
                    Label lblrxxLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrxxLf");
                    Label lblr3XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLf");
                    Label lblr4XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLf");
                    Label lblr30h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30h");
                    Label lblr32h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32h");
                    Label lblr34h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34h");
                    Label lblr36h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36h");
                    Label lblrXSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSh");
                    Label lblrSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSh");
                    Label lblrMh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMh");
                    Label lblrLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLh");
                    Label lblrXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLh");
                    Label lblrXXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXXLh");
                    Label lblr3XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLh");
                    Label lblr4XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLh");


                    lblr30f.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R30F"]).ToString("f2");
                    lblr32f.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R32F"]).ToString("f2");
                    lblr34f.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R34F"]).ToString("f2");
                    lblr36f.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R36F"]).ToString("f2");
                    lblrXSf.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["RxsF"]).ToString("f2");
                    lblrSf.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["RsF"]).ToString("f2");
                    lblrMf.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["RmF"]).ToString("f2");
                    lblrLf.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["RlF"]).ToString("f2");
                    lblrXLf.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["RxlF"]).ToString("f2");
                    lblrxxLf.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["RxxlF"]).ToString("f2");
                    lblr3XLf.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R3xlF"]).ToString("f2");
                    lblr4XLf.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R4xlF"]).ToString("f2");

                    lblr30h.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R30h"]).ToString("f2");
                    lblr32h.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R32h"]).ToString("f2");
                    lblr34h.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R34h"]).ToString("f2");
                    lblr36h.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R36h"]).ToString("f2");
                    lblrXSh.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["Rxsh"]).ToString("f2");
                    lblrSh.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["Rsh"]).ToString("f2");
                    lblrMh.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["Rmh"]).ToString("f2");
                    lblrLh.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["Rlh"]).ToString("f2");
                    lblrXLh.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["Rxlh"]).ToString("f2");
                    lblrXXLh.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["Rxxlh"]).ToString("f2");
                    lblr3XLh.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R3xlh"]).ToString("f2");
                    lblr4XLh.Text = Convert.ToDouble(dsmerge.Tables[0].Rows[vLoop]["R4xlh"]).ToString("f2");

                    #endregion

                    lbl30FS.Text = dsmerge.Tables[0].Rows[vLoop]["R30FS"].ToString();
                    lbl32FS.Text = dsmerge.Tables[0].Rows[vLoop]["R32FS"].ToString();
                    lbl34FS.Text = dsmerge.Tables[0].Rows[vLoop]["R34FS"].ToString();
                    lbl36FS.Text = dsmerge.Tables[0].Rows[vLoop]["R36FS"].ToString();
                    lblXSFS.Text = dsmerge.Tables[0].Rows[vLoop]["RXSFS"].ToString();
                    lblSFS.Text = dsmerge.Tables[0].Rows[vLoop]["RSFS"].ToString();
                    lblMFS.Text = dsmerge.Tables[0].Rows[vLoop]["RMFS"].ToString();
                    lblLFS.Text = dsmerge.Tables[0].Rows[vLoop]["RLFS"].ToString();
                    lblXLFS.Text = dsmerge.Tables[0].Rows[vLoop]["RXLFS"].ToString();
                    lblXXLFS.Text = dsmerge.Tables[0].Rows[vLoop]["RXXLFS"].ToString();
                    lbl3XLFS.Text = dsmerge.Tables[0].Rows[vLoop]["R3XLFS"].ToString();
                    lbl4XLFS.Text = dsmerge.Tables[0].Rows[vLoop]["R4XLFS"].ToString();
                    lbl30HS.Text = dsmerge.Tables[0].Rows[vLoop]["R30HS"].ToString();
                    lbl32HS.Text = dsmerge.Tables[0].Rows[vLoop]["R32HS"].ToString();
                    lbl34HS.Text = dsmerge.Tables[0].Rows[vLoop]["R34HS"].ToString();
                    lbl36HS.Text = dsmerge.Tables[0].Rows[vLoop]["R36HS"].ToString();
                    lblXSHS.Text = dsmerge.Tables[0].Rows[vLoop]["RXSHS"].ToString();
                    lblSHS.Text = dsmerge.Tables[0].Rows[vLoop]["RSHS"].ToString();
                    lblMHS.Text = dsmerge.Tables[0].Rows[vLoop]["RMHS"].ToString();
                    lblLHS.Text = dsmerge.Tables[0].Rows[vLoop]["RLHS"].ToString();
                    lblXLHS.Text = dsmerge.Tables[0].Rows[vLoop]["RXLHS"].ToString();
                    lblXXLHS.Text = dsmerge.Tables[0].Rows[vLoop]["RXXLHS"].ToString();
                    lbl3XLHS.Text = dsmerge.Tables[0].Rows[vLoop]["R3XLHS"].ToString();
                    lbl4XLHS.Text = dsmerge.Tables[0].Rows[vLoop]["R4XLHS"].ToString();

                    if (CmpyId == "3")
                    {
                        txtR30FS.Text = dsmerge.Tables[0].Rows[vLoop]["R30FS"].ToString();
                        txtR32FS.Text = dsmerge.Tables[0].Rows[vLoop]["R32FS"].ToString();
                        txtR34FS.Text = dsmerge.Tables[0].Rows[vLoop]["R34FS"].ToString();
                        txtR36FS.Text = dsmerge.Tables[0].Rows[vLoop]["R36FS"].ToString();
                        txtRXSFS.Text = dsmerge.Tables[0].Rows[vLoop]["RXSFS"].ToString();
                        txtRSFS.Text = dsmerge.Tables[0].Rows[vLoop]["RSFS"].ToString();
                        txtRMFS.Text = dsmerge.Tables[0].Rows[vLoop]["RMFS"].ToString();
                        txtRLFS.Text = dsmerge.Tables[0].Rows[vLoop]["RLFS"].ToString();
                        txtRXLFS.Text = dsmerge.Tables[0].Rows[vLoop]["RXLFS"].ToString();
                        txtRXXLFS.Text = dsmerge.Tables[0].Rows[vLoop]["RXXLFS"].ToString();
                        txtR3XLFS.Text = dsmerge.Tables[0].Rows[vLoop]["R3XLFS"].ToString();
                        txtR4XLFS.Text = dsmerge.Tables[0].Rows[vLoop]["R4XLFS"].ToString();
                        txtR30HS.Text = dsmerge.Tables[0].Rows[vLoop]["R30HS"].ToString();
                        txtR32HS.Text = dsmerge.Tables[0].Rows[vLoop]["R32HS"].ToString();
                        txtR34HS.Text = dsmerge.Tables[0].Rows[vLoop]["R34HS"].ToString();
                        txtR36HS.Text = dsmerge.Tables[0].Rows[vLoop]["R36HS"].ToString();
                        txtRXSHS.Text = dsmerge.Tables[0].Rows[vLoop]["RXSHS"].ToString();
                        txtRSHS.Text = dsmerge.Tables[0].Rows[vLoop]["RSHS"].ToString();
                        txtRMHS.Text = dsmerge.Tables[0].Rows[vLoop]["RMHS"].ToString();
                        txtRLHS.Text = dsmerge.Tables[0].Rows[vLoop]["RLHS"].ToString();
                        txtRXLHS.Text = dsmerge.Tables[0].Rows[vLoop]["RXLHS"].ToString();
                        txtRXXLHS.Text = dsmerge.Tables[0].Rows[vLoop]["RXXLHS"].ToString();
                        txtR3XLHS.Text = dsmerge.Tables[0].Rows[vLoop]["R3XLHS"].ToString();
                        txtR4XLHS.Text = dsmerge.Tables[0].Rows[vLoop]["R4XLHS"].ToString();

                    }
                    else
                    {
                        txtR30FS.Text = "0";
                        txtR32FS.Text = "0";
                        txtR34FS.Text = "0";
                        txtR36FS.Text = "0";
                        txtRXSFS.Text = "0";
                        txtRSFS.Text = "0";
                        txtRMFS.Text = "0";
                        txtRLFS.Text = "0";
                        txtRXLFS.Text = "0";
                        txtRXXLFS.Text = "0";
                        txtR3XLFS.Text = "0";
                        txtR4XLFS.Text = "0";
                        txtR30HS.Text = "0";
                        txtR32HS.Text = "0";
                        txtR34HS.Text = "0";
                        txtR36HS.Text = "0";
                        txtRXSHS.Text = "0";
                        txtRSHS.Text = "0";
                        txtRMHS.Text = "0";
                        txtRLHS.Text = "0";
                        txtRXLHS.Text = "0";
                        txtRXXLHS.Text = "0";
                        txtR3XLHS.Text = "0";
                        txtR4XLHS.Text = "0";
                    }

                    lblFinishedStockRatioId.Text = dsmerge.Tables[0].Rows[vLoop]["FinishedStockRatioId"].ToString();
                    lblCutid.Text = dsmerge.Tables[0].Rows[vLoop]["Cutid"].ToString();
                    lblItemname.Text = dsmerge.Tables[0].Rows[vLoop]["Itemname"].ToString();
                    lblCompanyLotNo.Text = dsmerge.Tables[0].Rows[vLoop]["CompanyLotNo"].ToString();
                    lblDesignCode.Text = dsmerge.Tables[0].Rows[vLoop]["DesignCode"].ToString();

                    ttldespatchqty = Convert.ToDouble(txtR30FS.Text) + Convert.ToDouble(txtR32FS.Text) + Convert.ToDouble(txtR34FS.Text) + Convert.ToDouble(txtR36FS.Text) +
                              Convert.ToDouble(txtRXSFS.Text) + Convert.ToDouble(txtRSFS.Text) + Convert.ToDouble(txtRMFS.Text) + Convert.ToDouble(txtRLFS.Text) +
                              Convert.ToDouble(txtRXLFS.Text) + Convert.ToDouble(txtRXXLFS.Text) + Convert.ToDouble(txtR3XLFS.Text) + Convert.ToDouble(txtR4XLFS.Text) +
                              Convert.ToDouble(txtR30HS.Text) + Convert.ToDouble(txtR32HS.Text) + Convert.ToDouble(txtR34HS.Text) + Convert.ToDouble(txtR36HS.Text) +
                              Convert.ToDouble(txtRXSHS.Text) + Convert.ToDouble(txtRSHS.Text) + Convert.ToDouble(txtRMHS.Text) + Convert.ToDouble(txtRLHS.Text) +
                              Convert.ToDouble(txtRXLHS.Text) + Convert.ToDouble(txtRXXLHS.Text) + Convert.ToDouble(txtR3XLHS.Text) + Convert.ToDouble(txtR4XLHS.Text);
                    allttldespatchqty = allttldespatchqty + ttldespatchqty;

                    initialttldespatchqty = Convert.ToDouble(lbl30FS.Text) + Convert.ToDouble(lbl32FS.Text) + Convert.ToDouble(lbl34FS.Text) + Convert.ToDouble(lbl36FS.Text) +
                                                 Convert.ToDouble(lblXSFS.Text) + Convert.ToDouble(lblSFS.Text) + Convert.ToDouble(lblMFS.Text) + Convert.ToDouble(lblLFS.Text) +
                                                 Convert.ToDouble(lblXLFS.Text) + Convert.ToDouble(lblXXLFS.Text) + Convert.ToDouble(lbl3XLFS.Text) + Convert.ToDouble(lbl4XLFS.Text) +
                                                 Convert.ToDouble(lbl30HS.Text) + Convert.ToDouble(lbl32HS.Text) + Convert.ToDouble(lbl34HS.Text) + Convert.ToDouble(lbl36HS.Text) +
                                                 Convert.ToDouble(lblXSHS.Text) + Convert.ToDouble(lblSHS.Text) + Convert.ToDouble(lblMHS.Text) + Convert.ToDouble(lblLHS.Text) +
                                                 Convert.ToDouble(lblXLHS.Text) + Convert.ToDouble(lblXXLHS.Text) + Convert.ToDouble(lbl3XLHS.Text) + Convert.ToDouble(lbl4XLHS.Text);


                    lbllotqty.Text = initialttldespatchqty.ToString();
                    if (CmpyId == "3")
                    {
                        lbldespatchqty.Text = initialttldespatchqty.ToString();
                    }
                    else
                    {
                        lbldespatchqty.Text = "0";
                    }
                    #endregion

                }


            }
            lblalltotalqty.Text = allttldespatchqty.ToString();
        }


        protected void btncal_OnClick(object sender, EventArgs e)
        {
            // chkinvnochanged(sender, e);

            double allttldespatchqty = 0;
            double allttldespatchamt = 0;

            int ttl = 0;
            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();
            dct = new DataColumn("LotNo");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Total");
            dttt.Columns.Add(dct);
            dstd.Tables.Add(dttt);

            for (int vLoop = 0; vLoop < gvdeliverstock.Rows.Count; vLoop++)
            {

                double ttldespatchqty = 0;
                double ttldespatchamt = 0;

                Label lblCompanyLotNo = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCompanyLotNo");

                #region

                Label lbl30FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl30FS");
                TextBox txtR30FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30FS");
                if (Convert.ToDouble(lbl30FS.Text) < Convert.ToDouble(txtR30FS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 30FS ');", true);
                    return;
                }
                Label txt39fs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl32FS");
                TextBox txtR32FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32FS");
                if (Convert.ToDouble(txt39fs.Text) < Convert.ToDouble(txtR32FS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 32FS ');", true);
                    return;
                }
                Label lbl34FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl34FS");
                TextBox txtR34FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34FS");
                if (Convert.ToDouble(lbl34FS.Text) < Convert.ToDouble(txtR34FS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 34FS ');", true);
                    return;
                }
                Label lbl36FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl36FS");
                TextBox txtR36FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36FS");
                if (Convert.ToDouble(lbl36FS.Text) < Convert.ToDouble(txtR36FS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 36FS ');", true);
                    return;
                }
                Label lblXSFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXSFS");
                TextBox txtRXSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSFS");
                if (Convert.ToDouble(lblXSFS.Text) < Convert.ToDouble(txtRXSFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XSFS ');", true);
                    return;
                }
                Label lblSFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblSFS");
                TextBox txtRSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSFS");
                if (Convert.ToDouble(lblSFS.Text) < Convert.ToDouble(txtRSFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of SFS ');", true);
                    return;

                }
                Label lblMFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblMFS");
                TextBox txtRMFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMFS");
                if (Convert.ToDouble(lblMFS.Text) < Convert.ToDouble(txtRMFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of MFS ');", true);
                    return;
                }
                Label lblLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblLFS");
                TextBox txtRLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLFS");
                if (Convert.ToDouble(lblLFS.Text) < Convert.ToDouble(txtRLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of LFS ');", true);
                    return;
                }
                Label lblXLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXLFS");
                TextBox txtRXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLFS");
                if (Convert.ToDouble(lblXLFS.Text) < Convert.ToDouble(txtRXLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XLFS ');", true);
                    return;
                }
                Label lblXXLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXXLFS");
                TextBox txtRXXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLFS");
                if (Convert.ToDouble(lblXXLFS.Text) < Convert.ToDouble(txtRXXLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XXLFS ');", true);
                    return;
                }
                Label lbl3XLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl3XLFS");
                TextBox txtR3XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLFS");
                if (Convert.ToDouble(lbl3XLFS.Text) < Convert.ToDouble(txtR3XLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 3XLFS ');", true);
                    return;
                }
                Label lbl4XLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl4XLFS");
                TextBox txtR4XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLFS");
                if (Convert.ToDouble(lbl4XLFS.Text) < Convert.ToDouble(txtR4XLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 4XLFS ');", true);
                    return;
                }
                Label lbl30HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl30HS");
                TextBox txtR30HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30HS");
                if (Convert.ToDouble(lbl30HS.Text) < Convert.ToDouble(txtR30HS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 30HS ');", true);
                    return;
                }
                Label lbl32HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl32HS");
                TextBox txtR32HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32HS");
                if (Convert.ToDouble(lbl32HS.Text) < Convert.ToDouble(txtR32HS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 32HS ');", true);
                    return;
                }
                Label lbl34HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl34HS");
                TextBox txtR34HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34HS");
                if (Convert.ToDouble(lbl34HS.Text) < Convert.ToDouble(txtR34HS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 34HS ');", true);
                    return;
                }
                Label lbl36HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl36HS");
                TextBox txtR36HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36HS");
                if (Convert.ToDouble(lbl36HS.Text) < Convert.ToDouble(txtR36HS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 36HS ');", true);
                    return;
                }
                Label lblXSHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXSHS");
                TextBox txtRXSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSHS");
                if (Convert.ToDouble(lblXSHS.Text) < Convert.ToDouble(txtRXSHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XSHS ');", true);
                    return;
                }
                Label lblSHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblSHS");
                TextBox txtRSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSHS");
                if (Convert.ToDouble(lblSHS.Text) < Convert.ToDouble(txtRSHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of SHS ');", true);
                    return;
                }
                Label lblMHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblMHS");
                TextBox txtRMHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMHS");
                if (Convert.ToDouble(lblMHS.Text) < Convert.ToDouble(txtRMHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of MHS ');", true);
                    return;
                }
                Label lblLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblLHS");
                TextBox txtRLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLHS");
                if (Convert.ToDouble(lblLHS.Text) < Convert.ToDouble(txtRLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of LHS ');", true);
                    return;
                }
                Label lblXLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXLHS");
                TextBox txtRXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLHS");
                if (Convert.ToDouble(lblXLHS.Text) < Convert.ToDouble(txtRXLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XLHS ');", true);
                    return;
                }
                Label lblXXLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXXLHS");
                TextBox txtRXXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLHS");
                if (Convert.ToDouble(lblXXLHS.Text) < Convert.ToDouble(txtRXXLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XXLHS ');", true);
                    return;
                }
                Label lbl3XLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl3XLHS");
                TextBox txtR3XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLHS");
                if (Convert.ToDouble(lbl3XLHS.Text) < Convert.ToDouble(txtR3XLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 3XLHS ');", true);
                    return;
                }
                Label lbl4XLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl4XLHS");
                TextBox txtR4XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLHS");
                if (Convert.ToDouble(lbl4XLHS.Text) < Convert.ToDouble(txtR4XLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 4XLHS ');", true);
                    return;
                }

                ttldespatchqty = Convert.ToDouble(txtR30FS.Text) + Convert.ToDouble(txtR32FS.Text) + Convert.ToDouble(txtR34FS.Text) + Convert.ToDouble(txtR36FS.Text) +
                               Convert.ToDouble(txtRXSFS.Text) + Convert.ToDouble(txtRSFS.Text) + Convert.ToDouble(txtRMFS.Text) + Convert.ToDouble(txtRLFS.Text) +
                               Convert.ToDouble(txtRXLFS.Text) + Convert.ToDouble(txtRXXLFS.Text) + Convert.ToDouble(txtR3XLFS.Text) + Convert.ToDouble(txtR4XLFS.Text) +
                               Convert.ToDouble(txtR30HS.Text) + Convert.ToDouble(txtR32HS.Text) + Convert.ToDouble(txtR34HS.Text) + Convert.ToDouble(txtR36HS.Text) +
                               Convert.ToDouble(txtRXSHS.Text) + Convert.ToDouble(txtRSHS.Text) + Convert.ToDouble(txtRMHS.Text) + Convert.ToDouble(txtRLHS.Text) +
                               Convert.ToDouble(txtRXLHS.Text) + Convert.ToDouble(txtRXXLHS.Text) + Convert.ToDouble(txtR3XLHS.Text) + Convert.ToDouble(txtR4XLHS.Text);

                Label lbldespatchqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbldespatchqty");

                lbldespatchqty.Text = ttldespatchqty.ToString();
                allttldespatchqty = allttldespatchqty + ttldespatchqty;

                #endregion


                #region Qty Amt Calculation

                Label lblr30f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30f");
                Label lblr32f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32f");
                Label lblr34f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34f");
                Label lblr36f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36f");
                Label lblrXSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSf");
                Label lblrSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSf");
                Label lblrMf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMf");
                Label lblrLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLf");
                Label lblrXLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLf");
                Label lblrxxLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrxxLf");
                Label lblr3XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLf");
                Label lblr4XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLf");
                Label lblr30h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30h");
                Label lblr32h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32h");
                Label lblr34h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34h");
                Label lblr36h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36h");
                Label lblrXSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSh");
                Label lblrSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSh");
                Label lblrMh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMh");
                Label lblrLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLh");
                Label lblrXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLh");
                Label lblrXXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXXLh");
                Label lblr3XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLh");
                Label lblr4XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLh");

                Label lblamtf30 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf30");
                Label lblamtf32 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf32");
                Label lblamtf34 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf34");
                Label lblamtf36 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf36");
                Label lblamtfxs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxs");
                Label lblamtfs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfs");
                Label lblamtfm = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfm");
                Label lblamtfl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfl");
                Label lblamtfxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxl");
                Label lblamtfxxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxxl");
                Label lblamtf3xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf3xl");
                Label lblamtf4xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf4xl");
                Label lblamth30 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth30");
                Label lblamth32 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth32");
                Label lblamth34 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth34");
                Label lblamth36 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth36");
                Label lblamthxs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxs");
                Label lblamths = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamths");
                Label lblamthm = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthm");
                Label lblamthl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthl");
                Label lblamthxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxl");
                Label lblamthxxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxxl");
                Label lblamth3xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth3xl");
                Label lblamth4xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth4xl");

                lblamtf30.Text = (Convert.ToInt32(txtR30FS.Text) * Convert.ToDouble(lblr30f.Text)).ToString("f2");
                lblamtf32.Text = (Convert.ToInt32(txtR32FS.Text) * Convert.ToDouble(lblr32f.Text)).ToString("f2");
                lblamtf34.Text = (Convert.ToInt32(txtR34FS.Text) * Convert.ToDouble(lblr34f.Text)).ToString("f2");
                lblamtf36.Text = (Convert.ToInt32(txtR36FS.Text) * Convert.ToDouble(lblr36f.Text)).ToString("f2");
                lblamtfxs.Text = (Convert.ToInt32(txtRXSFS.Text) * Convert.ToDouble(lblrXSf.Text)).ToString("f2");
                lblamtfs.Text = (Convert.ToInt32(txtRSFS.Text) * Convert.ToDouble(lblrSf.Text)).ToString("f2");
                lblamtfm.Text = (Convert.ToInt32(txtRMFS.Text) * Convert.ToDouble(lblrMf.Text)).ToString("f2");
                lblamtfl.Text = (Convert.ToInt32(txtRLFS.Text) * Convert.ToDouble(lblrLf.Text)).ToString("f2");
                lblamtfxl.Text = (Convert.ToInt32(txtRXLFS.Text) * Convert.ToDouble(lblrXLf.Text)).ToString("f2");
                lblamtfxxl.Text = (Convert.ToInt32(txtRXXLFS.Text) * Convert.ToDouble(lblrxxLf.Text)).ToString("f2");
                lblamtf3xl.Text = (Convert.ToInt32(txtR3XLFS.Text) * Convert.ToDouble(lblr3XLf.Text)).ToString("f2");
                lblamtf4xl.Text = (Convert.ToInt32(txtR4XLFS.Text) * Convert.ToDouble(lblr4XLf.Text)).ToString("f2");

                lblamth30.Text = (Convert.ToInt32(txtR30HS.Text) * Convert.ToDouble(lblr30h.Text)).ToString("f2");
                lblamth32.Text = (Convert.ToInt32(txtR32HS.Text) * Convert.ToDouble(lblr32h.Text)).ToString("f2");
                lblamth34.Text = (Convert.ToInt32(txtR34HS.Text) * Convert.ToDouble(lblr34h.Text)).ToString("f2");
                lblamth36.Text = (Convert.ToInt32(txtR36HS.Text) * Convert.ToDouble(lblr36h.Text)).ToString("f2");
                lblamthxs.Text = (Convert.ToInt32(txtRXSHS.Text) * Convert.ToDouble(lblrXSh.Text)).ToString("f2");
                lblamths.Text = (Convert.ToInt32(txtRSHS.Text) * Convert.ToDouble(lblrSh.Text)).ToString("f2");
                lblamthm.Text = (Convert.ToInt32(txtRMHS.Text) * Convert.ToDouble(lblrMh.Text)).ToString("f2");
                lblamthl.Text = (Convert.ToInt32(txtRLHS.Text) * Convert.ToDouble(lblrLh.Text)).ToString("f2");
                lblamthxl.Text = (Convert.ToInt32(txtRXLHS.Text) * Convert.ToDouble(lblrXLh.Text)).ToString("f2");
                lblamthxxl.Text = (Convert.ToInt32(txtRXXLHS.Text) * Convert.ToDouble(lblrXXLh.Text)).ToString("f2");
                lblamth3xl.Text = (Convert.ToInt32(txtR3XLHS.Text) * Convert.ToDouble(lblr3XLh.Text)).ToString("f2");
                lblamth4xl.Text = (Convert.ToInt32(txtR4XLHS.Text) * Convert.ToDouble(lblr4XLh.Text)).ToString("f2");

                ttldespatchamt = Convert.ToDouble(lblamtf30.Text) + Convert.ToDouble(lblamtf32.Text) + Convert.ToDouble(lblamtf34.Text) + Convert.ToDouble(lblamtf36.Text) +
                               Convert.ToDouble(lblamtfxs.Text) + Convert.ToDouble(lblamtfs.Text) + Convert.ToDouble(lblamtfm.Text) + Convert.ToDouble(lblamtfl.Text) +
                               Convert.ToDouble(lblamtfxl.Text) + Convert.ToDouble(lblamtfxxl.Text) + Convert.ToDouble(lblamtf3xl.Text) + Convert.ToDouble(lblamtf4xl.Text) +
                               Convert.ToDouble(lblamth30.Text) + Convert.ToDouble(lblamth32.Text) + Convert.ToDouble(lblamth34.Text) + Convert.ToDouble(lblamth36.Text) +
                               Convert.ToDouble(lblamthxs.Text) + Convert.ToDouble(lblamths.Text) + Convert.ToDouble(lblamthm.Text) + Convert.ToDouble(lblamthl.Text) +
                               Convert.ToDouble(lblamthxl.Text) + Convert.ToDouble(lblamthxxl.Text) + Convert.ToDouble(lblamth3xl.Text) + Convert.ToDouble(lblamth4xl.Text);

                Label lblttlamt = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblttlamt");
                lblttlamt.Text = ttldespatchamt.ToString("f2");
                allttldespatchamt = allttldespatchamt + ttldespatchamt;

                #endregion



                #region NewCount



                Label CompanyLotNo_f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCompanyLotNo");
                Label despatchqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbldespatchqty");


                Label CompanyLotNo_b = new Label();
                if (vLoop + 1 == gvdeliverstock.Rows.Count)
                {
                    CompanyLotNo_b = (Label)gvdeliverstock.Rows[0].FindControl("lblCompanyLotNo");
                    ttl = ttl + Convert.ToInt32(despatchqty.Text);
                }
                else
                {
                    CompanyLotNo_b = (Label)gvdeliverstock.Rows[vLoop + 1].FindControl("lblCompanyLotNo");
                    ttl = ttl + Convert.ToInt32(despatchqty.Text);

                }


                if (CompanyLotNo_f.Text == CompanyLotNo_b.Text)
                {



                }
                else
                {

                    drNew = dttt.NewRow();
                    drNew["LotNo"] = CompanyLotNo_f.Text;
                    drNew["Total"] = ttl;
                    dstd.Tables[0].Rows.Add(drNew);
                    ttl = 0;
                }





                #endregion
            }
            gridcatqty.DataSource = dstd;
            gridcatqty.DataBind();
            lblalltotalqty.Text = allttldespatchqty.ToString();
            lblalltotalamt.Text = allttldespatchamt.ToString("f2");




        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
           
            System.Threading.Thread.Sleep(3000);

            #region Calculation
            double allttldespatchqty = 0;
            double allttldespatchamt = 0;

            int ttl = 0;
            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();
            dct = new DataColumn("LotNo");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Total");
            dttt.Columns.Add(dct);
            dstd.Tables.Add(dttt);


            for (int vLoop = 0; vLoop < gvdeliverstock.Rows.Count; vLoop++)
            {

                double ttldespatchqty = 0;
                double ttldespatchamt = 0;

                Label lblCompanyLotNo = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCompanyLotNo");

                #region


                Label lbl30FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl30FS");
                TextBox txtR30FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30FS");
                if (Convert.ToDouble(lbl30FS.Text) < Convert.ToDouble(txtR30FS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 30FS ');", true);
                    return;
                }
                Label txt39fs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl32FS");
                TextBox txtR32FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32FS");
                if (Convert.ToDouble(txt39fs.Text) < Convert.ToDouble(txtR32FS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 32FS ');", true);
                    return;
                }
                Label lbl34FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl34FS");
                TextBox txtR34FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34FS");
                if (Convert.ToDouble(lbl34FS.Text) < Convert.ToDouble(txtR34FS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 34FS ');", true);
                    return;
                }
                Label lbl36FS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl36FS");
                TextBox txtR36FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36FS");
                if (Convert.ToDouble(lbl36FS.Text) < Convert.ToDouble(txtR36FS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 36FS ');", true);
                    return;
                }
                Label lblXSFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXSFS");
                TextBox txtRXSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSFS");
                if (Convert.ToDouble(lblXSFS.Text) < Convert.ToDouble(txtRXSFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XSFS ');", true);
                    return;
                }
                Label lblSFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblSFS");
                TextBox txtRSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSFS");
                if (Convert.ToDouble(lblSFS.Text) < Convert.ToDouble(txtRSFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of SFS ');", true);
                    return;

                }
                Label lblMFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblMFS");
                TextBox txtRMFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMFS");
                if (Convert.ToDouble(lblMFS.Text) < Convert.ToDouble(txtRMFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of MFS ');", true);
                    return;
                }
                Label lblLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblLFS");
                TextBox txtRLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLFS");
                if (Convert.ToDouble(lblLFS.Text) < Convert.ToDouble(txtRLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of LFS ');", true);
                    return;
                }
                Label lblXLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXLFS");
                TextBox txtRXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLFS");
                if (Convert.ToDouble(lblXLFS.Text) < Convert.ToDouble(txtRXLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XLFS ');", true);
                    return;
                }
                Label lblXXLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXXLFS");
                TextBox txtRXXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLFS");
                if (Convert.ToDouble(lblXXLFS.Text) < Convert.ToDouble(txtRXXLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XXLFS ');", true);
                    return;
                }
                Label lbl3XLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl3XLFS");
                TextBox txtR3XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLFS");
                if (Convert.ToDouble(lbl3XLFS.Text) < Convert.ToDouble(txtR3XLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 3XLFS ');", true);
                    return;
                }
                Label lbl4XLFS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl4XLFS");
                TextBox txtR4XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLFS");
                if (Convert.ToDouble(lbl4XLFS.Text) < Convert.ToDouble(txtR4XLFS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 4XLFS ');", true);
                    return;
                }
                Label lbl30HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl30HS");
                TextBox txtR30HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30HS");
                if (Convert.ToDouble(lbl30HS.Text) < Convert.ToDouble(txtR30HS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 30HS ');", true);
                    return;
                }
                Label lbl32HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl32HS");
                TextBox txtR32HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32HS");
                if (Convert.ToDouble(lbl32HS.Text) < Convert.ToDouble(txtR32HS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 32HS ');", true);
                    return;
                }
                Label lbl34HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl34HS");
                TextBox txtR34HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34HS");
                if (Convert.ToDouble(lbl34HS.Text) < Convert.ToDouble(txtR34HS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 34HS ');", true);
                    return;
                }
                Label lbl36HS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl36HS");
                TextBox txtR36HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36HS");
                if (Convert.ToDouble(lbl36HS.Text) < Convert.ToDouble(txtR36HS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 36HS ');", true);
                    return;
                }
                Label lblXSHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXSHS");
                TextBox txtRXSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSHS");
                if (Convert.ToDouble(lblXSHS.Text) < Convert.ToDouble(txtRXSHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XSHS ');", true);
                    return;
                }
                Label lblSHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblSHS");
                TextBox txtRSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSHS");
                if (Convert.ToDouble(lblSHS.Text) < Convert.ToDouble(txtRSHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of SHS ');", true);
                    return;
                }
                Label lblMHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblMHS");
                TextBox txtRMHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMHS");
                if (Convert.ToDouble(lblMHS.Text) < Convert.ToDouble(txtRMHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of MHS ');", true);
                    return;
                }
                Label lblLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblLHS");
                TextBox txtRLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLHS");
                if (Convert.ToDouble(lblLHS.Text) < Convert.ToDouble(txtRLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of LHS ');", true);
                    return;
                }
                Label lblXLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXLHS");
                TextBox txtRXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLHS");
                if (Convert.ToDouble(lblXLHS.Text) < Convert.ToDouble(txtRXLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XLHS ');", true);
                    return;
                }
                Label lblXXLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblXXLHS");
                TextBox txtRXXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLHS");
                if (Convert.ToDouble(lblXXLHS.Text) < Convert.ToDouble(txtRXXLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of XXLHS ');", true);
                    return;
                }
                Label lbl3XLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl3XLHS");
                TextBox txtR3XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLHS");
                if (Convert.ToDouble(lbl3XLHS.Text) < Convert.ToDouble(txtR3XLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 3XLHS ');", true);
                    return;
                }
                Label lbl4XLHS = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbl4XLHS");
                TextBox txtR4XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLHS");
                if (Convert.ToDouble(lbl4XLHS.Text) < Convert.ToDouble(txtR4XLHS.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Quantity is greater in LotNo " + lblCompanyLotNo.Text + " and the Size of 4XLHS ');", true);
                    return;
                }
                #endregion



                #region Qty Amt Calculation

                Label lblr30f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30f");
                Label lblr32f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32f");
                Label lblr34f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34f");
                Label lblr36f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36f");
                Label lblrXSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSf");
                Label lblrSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSf");
                Label lblrMf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMf");
                Label lblrLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLf");
                Label lblrXLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLf");
                Label lblrxxLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrxxLf");
                Label lblr3XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLf");
                Label lblr4XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLf");
                Label lblr30h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30h");
                Label lblr32h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32h");
                Label lblr34h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34h");
                Label lblr36h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36h");
                Label lblrXSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSh");
                Label lblrSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSh");
                Label lblrMh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMh");
                Label lblrLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLh");
                Label lblrXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLh");
                Label lblrXXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXXLh");
                Label lblr3XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLh");
                Label lblr4XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLh");

                Label lblamtf30 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf30");
                Label lblamtf32 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf32");
                Label lblamtf34 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf34");
                Label lblamtf36 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf36");
                Label lblamtfxs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxs");
                Label lblamtfs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfs");
                Label lblamtfm = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfm");
                Label lblamtfl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfl");
                Label lblamtfxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxl");
                Label lblamtfxxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtfxxl");
                Label lblamtf3xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf3xl");
                Label lblamtf4xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamtf4xl");
                Label lblamth30 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth30");
                Label lblamth32 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth32");
                Label lblamth34 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth34");
                Label lblamth36 = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth36");
                Label lblamthxs = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxs");
                Label lblamths = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamths");
                Label lblamthm = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthm");
                Label lblamthl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthl");
                Label lblamthxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxl");
                Label lblamthxxl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamthxxl");
                Label lblamth3xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth3xl");
                Label lblamth4xl = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblamth4xl");

                lblamtf30.Text = (Convert.ToInt32(txtR30FS.Text) * Convert.ToDouble(lblr30f.Text)).ToString("f2");
                lblamtf32.Text = (Convert.ToInt32(txtR32FS.Text) * Convert.ToDouble(lblr32f.Text)).ToString("f2");
                lblamtf34.Text = (Convert.ToInt32(txtR34FS.Text) * Convert.ToDouble(lblr34f.Text)).ToString("f2");
                lblamtf36.Text = (Convert.ToInt32(txtR36FS.Text) * Convert.ToDouble(lblr36f.Text)).ToString("f2");
                lblamtfxs.Text = (Convert.ToInt32(txtRXSFS.Text) * Convert.ToDouble(lblrXSf.Text)).ToString("f2");
                lblamtfs.Text = (Convert.ToInt32(txtRSFS.Text) * Convert.ToDouble(lblrSf.Text)).ToString("f2");
                lblamtfm.Text = (Convert.ToInt32(txtRMFS.Text) * Convert.ToDouble(lblrMf.Text)).ToString("f2");
                lblamtfl.Text = (Convert.ToInt32(txtRLFS.Text) * Convert.ToDouble(lblrLf.Text)).ToString("f2");
                lblamtfxl.Text = (Convert.ToInt32(txtRXLFS.Text) * Convert.ToDouble(lblrXLf.Text)).ToString("f2");
                lblamtfxxl.Text = (Convert.ToInt32(txtRXXLFS.Text) * Convert.ToDouble(lblrxxLf.Text)).ToString("f2");
                lblamtf3xl.Text = (Convert.ToInt32(txtR3XLFS.Text) * Convert.ToDouble(lblr3XLf.Text)).ToString("f2");
                lblamtf4xl.Text = (Convert.ToInt32(txtR4XLFS.Text) * Convert.ToDouble(lblr4XLf.Text)).ToString("f2");

                lblamth30.Text = (Convert.ToInt32(txtR30HS.Text) * Convert.ToDouble(lblr30h.Text)).ToString("f2");
                lblamth32.Text = (Convert.ToInt32(txtR32HS.Text) * Convert.ToDouble(lblr32h.Text)).ToString("f2");
                lblamth34.Text = (Convert.ToInt32(txtR34HS.Text) * Convert.ToDouble(lblr34h.Text)).ToString("f2");
                lblamth36.Text = (Convert.ToInt32(txtR36HS.Text) * Convert.ToDouble(lblr36h.Text)).ToString("f2");
                lblamthxs.Text = (Convert.ToInt32(txtRXSHS.Text) * Convert.ToDouble(lblrXSh.Text)).ToString("f2");
                lblamths.Text = (Convert.ToInt32(txtRSHS.Text) * Convert.ToDouble(lblrSh.Text)).ToString("f2");
                lblamthm.Text = (Convert.ToInt32(txtRMHS.Text) * Convert.ToDouble(lblrMh.Text)).ToString("f2");
                lblamthl.Text = (Convert.ToInt32(txtRLHS.Text) * Convert.ToDouble(lblrLh.Text)).ToString("f2");
                lblamthxl.Text = (Convert.ToInt32(txtRXLHS.Text) * Convert.ToDouble(lblrXLh.Text)).ToString("f2");
                lblamthxxl.Text = (Convert.ToInt32(txtRXXLHS.Text) * Convert.ToDouble(lblrXXLh.Text)).ToString("f2");
                lblamth3xl.Text = (Convert.ToInt32(txtR3XLHS.Text) * Convert.ToDouble(lblr3XLh.Text)).ToString("f2");
                lblamth4xl.Text = (Convert.ToInt32(txtR4XLHS.Text) * Convert.ToDouble(lblr4XLh.Text)).ToString("f2");

                ttldespatchamt = Convert.ToDouble(lblamtf30.Text) + Convert.ToDouble(lblamtf32.Text) + Convert.ToDouble(lblamtf34.Text) + Convert.ToDouble(lblamtf36.Text) +
                               Convert.ToDouble(lblamtfxs.Text) + Convert.ToDouble(lblamtfs.Text) + Convert.ToDouble(lblamtfm.Text) + Convert.ToDouble(lblamtfl.Text) +
                               Convert.ToDouble(lblamtfxl.Text) + Convert.ToDouble(lblamtfxxl.Text) + Convert.ToDouble(lblamtf3xl.Text) + Convert.ToDouble(lblamtf4xl.Text) +
                               Convert.ToDouble(lblamth30.Text) + Convert.ToDouble(lblamth32.Text) + Convert.ToDouble(lblamth34.Text) + Convert.ToDouble(lblamth36.Text) +
                               Convert.ToDouble(lblamthxs.Text) + Convert.ToDouble(lblamths.Text) + Convert.ToDouble(lblamthm.Text) + Convert.ToDouble(lblamthl.Text) +
                               Convert.ToDouble(lblamthxl.Text) + Convert.ToDouble(lblamthxxl.Text) + Convert.ToDouble(lblamth3xl.Text) + Convert.ToDouble(lblamth4xl.Text);

                Label lblttlamt = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblttlamt");
                lblttlamt.Text = ttldespatchamt.ToString("f2");
                allttldespatchamt = allttldespatchamt + ttldespatchamt;

                #endregion


                ttldespatchqty = Convert.ToDouble(txtR30FS.Text) + Convert.ToDouble(txtR32FS.Text) + Convert.ToDouble(txtR34FS.Text) + Convert.ToDouble(txtR36FS.Text) +
                               Convert.ToDouble(txtRXSFS.Text) + Convert.ToDouble(txtRSFS.Text) + Convert.ToDouble(txtRMFS.Text) + Convert.ToDouble(txtRLFS.Text) +
                               Convert.ToDouble(txtRXLFS.Text) + Convert.ToDouble(txtRXXLFS.Text) + Convert.ToDouble(txtR3XLFS.Text) + Convert.ToDouble(txtR4XLFS.Text) +
                               Convert.ToDouble(txtR30HS.Text) + Convert.ToDouble(txtR32HS.Text) + Convert.ToDouble(txtR34HS.Text) + Convert.ToDouble(txtR36HS.Text) +
                               Convert.ToDouble(txtRXSHS.Text) + Convert.ToDouble(txtRSHS.Text) + Convert.ToDouble(txtRMHS.Text) + Convert.ToDouble(txtRLHS.Text) +
                               Convert.ToDouble(txtRXLHS.Text) + Convert.ToDouble(txtRXXLHS.Text) + Convert.ToDouble(txtR3XLHS.Text) + Convert.ToDouble(txtR4XLHS.Text);

                Label lbldespatchqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbldespatchqty");

                lbldespatchqty.Text = ttldespatchqty.ToString();
                allttldespatchqty = allttldespatchqty + ttldespatchqty;



                #region NewCount



                Label CompanyLotNo_f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCompanyLotNo");
                Label despatchqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbldespatchqty");


                Label CompanyLotNo_b = new Label();
                if (vLoop + 1 == gvdeliverstock.Rows.Count)
                {
                    CompanyLotNo_b = (Label)gvdeliverstock.Rows[0].FindControl("lblCompanyLotNo");
                    ttl = ttl + Convert.ToInt32(despatchqty.Text);
                }
                else
                {
                    CompanyLotNo_b = (Label)gvdeliverstock.Rows[vLoop + 1].FindControl("lblCompanyLotNo");
                    ttl = ttl + Convert.ToInt32(despatchqty.Text);

                }


                if (CompanyLotNo_f.Text == CompanyLotNo_b.Text)
                {



                }
                else
                {

                    drNew = dttt.NewRow();
                    drNew["LotNo"] = CompanyLotNo_f.Text;
                    drNew["Total"] = ttl;
                    dstd.Tables[0].Rows.Add(drNew);
                    ttl = 0;
                }





                #endregion
            }
            gridcatqty.DataSource = dstd;
            gridcatqty.DataBind();
            lblalltotalqty.Text = allttldespatchqty.ToString();
            lblalltotalamt.Text = allttldespatchamt.ToString("f2");

            #endregion

            #region
            if (drpbranch.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you !!!');", true);
                return;
            }

            if (ddlpacker.SelectedValue == "Select Despatcher")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Despatcher. Thank you !!!');", true);
                return;
            }

            if (ddlcustomer.SelectedValue == "Select Customer")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Customer. Thank you !!!');", true);
                return;
            }
            if (txtdcno.Text == "" || txtdcno.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter DcNo. Thank you !!!');", true);
                return;
            }
            if (lblalltotalqty.Text == "" || lblalltotalqty.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select LotNo and Quantity. Thank you !!!');", true);
                return;
            }
            #endregion

            btnSave.Enabled = false;

            if (btnSave.Text == "Save")
            {
                #region

                DataSet dsdcno = objBs.getdespatchnouser(txtdcno.Text, drpbranch.SelectedValue);
                if (dsdcno.Tables[0].Rows.Count > 0)
                {
                    btnSave.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This DcNo Alread Inserted Use Any Other. Thank you !!!');", true);
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                }

                DateTime Date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                int savedespatch = objBs.savedespatchdetails(txtdcno.Text, Date, Convert.ToInt32(ddlpacker.SelectedValue), txtnarration.Text, Convert.ToInt32(lblalltotalqty.Text), Convert.ToInt32(ddlcustomer.SelectedValue), Convert.ToInt32(drpbranch.SelectedValue), Convert.ToInt32(Empid), Convert.ToDouble(lblalltotalamt.Text));

                for (int vLoop = 0; vLoop < gvdeliverstock.Rows.Count; vLoop++)
                {

                    TextBox txtR30FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30FS");
                    TextBox txtR32FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32FS");
                    TextBox txtR34FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34FS");
                    TextBox txtR36FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36FS");
                    TextBox txtRXSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSFS");
                    TextBox txtRSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSFS");
                    TextBox txtRMFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMFS");
                    TextBox txtRLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLFS");
                    TextBox txtRXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLFS");
                    TextBox txtRXXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLFS");
                    TextBox txtR3XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLFS");
                    TextBox txtR4XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLFS");
                    TextBox txtR30HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30HS");
                    TextBox txtR32HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32HS");
                    TextBox txtR34HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34HS");
                    TextBox txtR36HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36HS");
                    TextBox txtRXSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSHS");
                    TextBox txtRSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSHS");
                    TextBox txtRMHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMHS");
                    TextBox txtRLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLHS");
                    TextBox txtRXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLHS");
                    TextBox txtRXXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLHS");
                    TextBox txtR3XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLHS");
                    TextBox txtR4XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLHS");

                    Label lblFinishedStockRatioId = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblFinishedStockRatioId");
                    Label lblCutid = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCutid");
                    Label lblItemname = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblItemname");
                    Label lblCompanyLotNo = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCompanyLotNo");
                    Label lblDesignCode = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblDesignCode");



                    Label lblr30f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30f");
                    Label lblr32f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32f");
                    Label lblr34f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34f");
                    Label lblr36f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36f");
                    Label lblrXSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSf");
                    Label lblrSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSf");
                    Label lblrMf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMf");
                    Label lblrLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLf");
                    Label lblrXLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLf");
                    Label lblrxxLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrxxLf");
                    Label lblr3XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLf");
                    Label lblr4XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLf");
                    Label lblr30h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30h");
                    Label lblr32h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32h");
                    Label lblr34h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34h");
                    Label lblr36h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36h");
                    Label lblrXSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSh");
                    Label lblrSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSh");
                    Label lblrMh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMh");
                    Label lblrLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLh");
                    Label lblrXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLh");
                    Label lblrXXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXXLh");
                    Label lblr3XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLh");
                    Label lblr4XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLh");

                    Label lblttlamt = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblttlamt");

                    Label lbldespatchqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbldespatchqty");
                    if (lbldespatchqty.Text != "0")
                    {
                        int transsavedespatch = objBs.savetransdespatchdetails(savedespatch, Convert.ToInt32(lblFinishedStockRatioId.Text), Convert.ToInt32(lblCutid.Text), lblItemname.Text, lblCompanyLotNo.Text, Convert.ToInt32(txtR30FS.Text), Convert.ToInt32(txtR32FS.Text), Convert.ToInt32(txtR34FS.Text), Convert.ToInt32(txtR36FS.Text), Convert.ToInt32(txtRXSFS.Text), Convert.ToInt32(txtRSFS.Text), Convert.ToInt32(txtRMFS.Text), Convert.ToInt32(txtRLFS.Text), Convert.ToInt32(txtRXLFS.Text), Convert.ToInt32(txtRXXLFS.Text), Convert.ToInt32(txtR3XLFS.Text), Convert.ToInt32(txtR4XLFS.Text), Convert.ToInt32(txtR30HS.Text), Convert.ToInt32(txtR32HS.Text), Convert.ToInt32(txtR34HS.Text), Convert.ToInt32(txtR36HS.Text), Convert.ToInt32(txtRXSHS.Text), Convert.ToInt32(txtRSHS.Text), Convert.ToInt32(txtRMHS.Text), Convert.ToInt32(txtRLHS.Text), Convert.ToInt32(txtRXLHS.Text), Convert.ToInt32(txtRXXLHS.Text), Convert.ToInt32(txtR3XLHS.Text), Convert.ToInt32(txtR4XLHS.Text), Convert.ToInt32(lbldespatchqty.Text), lblDesignCode.Text, Convert.ToDouble(lblr30f.Text), Convert.ToDouble(lblr32f.Text), Convert.ToDouble(lblr34f.Text), Convert.ToDouble(lblr36f.Text), Convert.ToDouble(lblrXSf.Text), Convert.ToDouble(lblrSf.Text), Convert.ToDouble(lblrMf.Text), Convert.ToDouble(lblrLf.Text), Convert.ToDouble(lblrXLf.Text), Convert.ToDouble(lblrxxLf.Text), Convert.ToDouble(lblr3XLf.Text), Convert.ToDouble(lblr4XLf.Text), Convert.ToDouble(lblr30h.Text), Convert.ToDouble(lblr32h.Text), Convert.ToDouble(lblr34h.Text), Convert.ToDouble(lblr36h.Text), Convert.ToDouble(lblrXSh.Text), Convert.ToDouble(lblrSh.Text), Convert.ToDouble(lblrMh.Text), Convert.ToDouble(lblrLh.Text), Convert.ToDouble(lblrXLh.Text), Convert.ToDouble(lblrXXLh.Text), Convert.ToDouble(lblr3XLh.Text), Convert.ToDouble(lblr4XLh.Text), Convert.ToDouble(lblttlamt.Text));
                    }

                }
                #endregion

                Response.Redirect("DespatchGrid.aspx");
            }
            else
            {
                string DespatchId = Request.QueryString.Get("DespatchId");
                #region

                //DataSet dsdcno = objBs.getdespatchnouser(txtdcno.Text);
                //if (dsdcno.Tables[0].Rows.Count > 0)
                //{
                //    btnSave.Enabled = false;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This DcNo Alread Inserted Use Any Other. Thank you !!!');", true);
                //    return;
                //}
                //else
                //{
                //    btnSave.Enabled = true;
                //}

                //DateTime Date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //int savedespatch = objBs.savedespatchdetails(txtdcno.Text, Date, Convert.ToInt32(ddlpacker.SelectedValue), txtnarration.Text, Convert.ToInt32(lblalltotalqty.Text), Convert.ToInt32(ddlcustomer.SelectedValue), Convert.ToInt32(drpbranch.SelectedValue), Convert.ToInt32(Empid));



                DataSet DsDespatchStock = objBs.geteditforDespatchStock(Convert.ToInt32(DespatchId));//Update Stock
                for (int j = 0; j < DsDespatchStock.Tables[0].Rows.Count; j++)
                {
                    #region

                    string S30FS = DsDespatchStock.Tables[0].Rows[j]["30FS"].ToString();
                    string S32FS = DsDespatchStock.Tables[0].Rows[j]["32FS"].ToString();
                    string S34FS = DsDespatchStock.Tables[0].Rows[j]["34FS"].ToString();
                    string S36FS = DsDespatchStock.Tables[0].Rows[j]["36FS"].ToString();
                    string SXSFS = DsDespatchStock.Tables[0].Rows[j]["xsFS"].ToString();
                    string SSFS = DsDespatchStock.Tables[0].Rows[j]["sFS"].ToString();
                    string SMFS = DsDespatchStock.Tables[0].Rows[j]["mFS"].ToString();
                    string SLFS = DsDespatchStock.Tables[0].Rows[j]["lFS"].ToString();
                    string SXLFS = DsDespatchStock.Tables[0].Rows[j]["xlFS"].ToString();
                    string SXXLFS = DsDespatchStock.Tables[0].Rows[j]["xxlFS"].ToString();
                    string S3XLFS = DsDespatchStock.Tables[0].Rows[j]["3xlFS"].ToString();
                    string S4XLFS = DsDespatchStock.Tables[0].Rows[j]["4xlFS"].ToString();


                    string S30HS = DsDespatchStock.Tables[0].Rows[j]["30HS"].ToString();
                    string S32HS = DsDespatchStock.Tables[0].Rows[j]["32HS"].ToString();
                    string S34HS = DsDespatchStock.Tables[0].Rows[j]["34HS"].ToString();
                    string S36HS = DsDespatchStock.Tables[0].Rows[j]["36HS"].ToString();
                    string SXSHS = DsDespatchStock.Tables[0].Rows[j]["xsHS"].ToString();
                    string SSHS = DsDespatchStock.Tables[0].Rows[j]["sHS"].ToString();
                    string SMHS = DsDespatchStock.Tables[0].Rows[j]["mHS"].ToString();
                    string SLHS = DsDespatchStock.Tables[0].Rows[j]["lHS"].ToString();
                    string SXLHS = DsDespatchStock.Tables[0].Rows[j]["xlHS"].ToString();
                    string SXXLHS = DsDespatchStock.Tables[0].Rows[j]["xxlHS"].ToString();
                    string S3XLHS = DsDespatchStock.Tables[0].Rows[j]["3xlHS"].ToString();
                    string S4XLHS = DsDespatchStock.Tables[0].Rows[j]["4xlHS"].ToString();




                    string R30FS = DsDespatchStock.Tables[0].Rows[j]["R30F"].ToString();
                    string R32FS = DsDespatchStock.Tables[0].Rows[j]["R32F"].ToString();
                    string R34FS = DsDespatchStock.Tables[0].Rows[j]["R34F"].ToString();
                    string R36FS = DsDespatchStock.Tables[0].Rows[j]["R36F"].ToString();
                    string RXSFS = DsDespatchStock.Tables[0].Rows[j]["RxsF"].ToString();
                    string RSFS = DsDespatchStock.Tables[0].Rows[j]["RsF"].ToString();
                    string RMFS = DsDespatchStock.Tables[0].Rows[j]["RmF"].ToString();
                    string RLFS = DsDespatchStock.Tables[0].Rows[j]["RlF"].ToString();
                    string RXLFS = DsDespatchStock.Tables[0].Rows[j]["RxlF"].ToString();
                    string RXXLFS = DsDespatchStock.Tables[0].Rows[j]["RxxlF"].ToString();
                    string R3XLFS = DsDespatchStock.Tables[0].Rows[j]["R3xlF"].ToString();
                    string R4XLFS = DsDespatchStock.Tables[0].Rows[j]["R4xlF"].ToString();


                    string R30HS = DsDespatchStock.Tables[0].Rows[j]["R30H"].ToString();
                    string R32HS = DsDespatchStock.Tables[0].Rows[j]["R32H"].ToString();
                    string R34HS = DsDespatchStock.Tables[0].Rows[j]["R34H"].ToString();
                    string R36HS = DsDespatchStock.Tables[0].Rows[j]["R36H"].ToString();
                    string RXSHS = DsDespatchStock.Tables[0].Rows[j]["RxsH"].ToString();
                    string RSHS = DsDespatchStock.Tables[0].Rows[j]["RsH"].ToString();
                    string RMHS = DsDespatchStock.Tables[0].Rows[j]["RmH"].ToString();
                    string RLHS = DsDespatchStock.Tables[0].Rows[j]["RlH"].ToString();
                    string RXLHS = DsDespatchStock.Tables[0].Rows[j]["RxlH"].ToString();
                    string RXXLHS = DsDespatchStock.Tables[0].Rows[j]["RxxlH"].ToString();
                    string R3XLHS = DsDespatchStock.Tables[0].Rows[j]["R3xlH"].ToString();
                    string R4XLHS = DsDespatchStock.Tables[0].Rows[j]["R4xlH"].ToString();

                    string TotalDespatchAmt = DsDespatchStock.Tables[0].Rows[j]["TotalDespatchAmt"].ToString();

                    string TotalDespatchqty = DsDespatchStock.Tables[0].Rows[j]["TotalDespatchqty"].ToString();

                    int Version = (Convert.ToInt32(DsDespatchStock.Tables[0].Rows[j]["Version"].ToString()) + 1);
                    string FinishedStockRatioId = DsDespatchStock.Tables[0].Rows[j]["FinishedStockRatioId"].ToString();

                    string Itemname = DsDespatchStock.Tables[0].Rows[j]["Itemname"].ToString();
                    string CompanyLotNo = DsDespatchStock.Tables[0].Rows[j]["CompanyLotNo"].ToString();
                    string DesignCode = DsDespatchStock.Tables[0].Rows[j]["DesignCode"].ToString();

                    #endregion

                    int transsavedespatch = objBs.updatetransdespatchdetails(Convert.ToInt32(DespatchId), Convert.ToInt32(FinishedStockRatioId), Convert.ToInt32(S30FS), Convert.ToInt32(S32FS), Convert.ToInt32(S34FS), Convert.ToInt32(S36FS), Convert.ToInt32(SXSFS), Convert.ToInt32(SSFS), Convert.ToInt32(SMFS), Convert.ToInt32(SLFS), Convert.ToInt32(SXLFS), Convert.ToInt32(SXXLFS), Convert.ToInt32(S3XLFS), Convert.ToInt32(S4XLFS), Convert.ToInt32(S30HS), Convert.ToInt32(S32HS), Convert.ToInt32(S34HS), Convert.ToInt32(S36HS), Convert.ToInt32(SXSHS), Convert.ToInt32(SSHS), Convert.ToInt32(SMHS), Convert.ToInt32(SLHS), Convert.ToInt32(SXLHS), Convert.ToInt32(SXXLHS), Convert.ToInt32(S3XLHS), Convert.ToInt32(S4XLHS), Convert.ToInt32(TotalDespatchqty), Itemname, CompanyLotNo, DesignCode, Version, Convert.ToInt32(Empid), Convert.ToDouble(R30FS), Convert.ToDouble(R32FS), Convert.ToDouble(R34FS), Convert.ToDouble(R36FS), Convert.ToDouble(RXSFS), Convert.ToDouble(RSFS), Convert.ToDouble(RMFS), Convert.ToDouble(RLFS), Convert.ToDouble(RXLFS), Convert.ToDouble(RXXLFS), Convert.ToDouble(R3XLFS), Convert.ToDouble(R4XLFS), Convert.ToDouble(R30HS), Convert.ToDouble(R32HS), Convert.ToDouble(R34HS), Convert.ToDouble(R36HS), Convert.ToDouble(RXSHS), Convert.ToDouble(RSHS), Convert.ToDouble(RMHS), Convert.ToDouble(RLHS), Convert.ToDouble(RXLHS), Convert.ToDouble(RXXLHS), Convert.ToDouble(R3XLHS), Convert.ToDouble(R4XLHS), Convert.ToDouble(TotalDespatchAmt));
                }

                int updespatch = objBs.newupdespatch(Convert.ToInt32(DespatchId), Convert.ToInt32(lblalltotalqty.Text), Convert.ToDouble(lblalltotalamt.Text),txtnarration.Text);
                int deletetransdespatch = objBs.deletetransdespatchdetails(Convert.ToInt32(DespatchId));
                for (int vLoop = 0; vLoop < gvdeliverstock.Rows.Count; vLoop++)
                {
                    #region

                    TextBox txtR30FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30FS");
                    TextBox txtR32FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32FS");
                    TextBox txtR34FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34FS");
                    TextBox txtR36FS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36FS");
                    TextBox txtRXSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSFS");
                    TextBox txtRSFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSFS");
                    TextBox txtRMFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMFS");
                    TextBox txtRLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLFS");
                    TextBox txtRXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLFS");
                    TextBox txtRXXLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLFS");
                    TextBox txtR3XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLFS");
                    TextBox txtR4XLFS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLFS");
                    TextBox txtR30HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR30HS");
                    TextBox txtR32HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR32HS");
                    TextBox txtR34HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR34HS");
                    TextBox txtR36HS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR36HS");
                    TextBox txtRXSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXSHS");
                    TextBox txtRSHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRSHS");
                    TextBox txtRMHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRMHS");
                    TextBox txtRLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRLHS");
                    TextBox txtRXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXLHS");
                    TextBox txtRXXLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtRXXLHS");
                    TextBox txtR3XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR3XLHS");
                    TextBox txtR4XLHS = (TextBox)gvdeliverstock.Rows[vLoop].FindControl("txtR4XLHS");

                    Label lblFinishedStockRatioId = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblFinishedStockRatioId");
                    Label lblCutid = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCutid");
                    Label lblItemname = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblItemname");
                    Label lblCompanyLotNo = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblCompanyLotNo");
                    Label lblDesignCode = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblDesignCode");

                    Label lblr30f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30f");
                    Label lblr32f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32f");
                    Label lblr34f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34f");
                    Label lblr36f = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36f");
                    Label lblrXSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSf");
                    Label lblrSf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSf");
                    Label lblrMf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMf");
                    Label lblrLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLf");
                    Label lblrXLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLf");
                    Label lblrxxLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrxxLf");
                    Label lblr3XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLf");
                    Label lblr4XLf = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLf");
                    Label lblr30h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr30h");
                    Label lblr32h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr32h");
                    Label lblr34h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr34h");
                    Label lblr36h = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr36h");
                    Label lblrXSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXSh");
                    Label lblrSh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrSh");
                    Label lblrMh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrMh");
                    Label lblrLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrLh");
                    Label lblrXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXLh");
                    Label lblrXXLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblrXXLh");
                    Label lblr3XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr3XLh");
                    Label lblr4XLh = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblr4XLh");

                    Label lblttlamt = (Label)gvdeliverstock.Rows[vLoop].FindControl("lblttlamt");


                    Label lbldespatchqty = (Label)gvdeliverstock.Rows[vLoop].FindControl("lbldespatchqty");
                    if (lbldespatchqty.Text != "0")
                    {
                        int transsavedespatch = objBs.savetransdespatchdetails(Convert.ToInt32(DespatchId), Convert.ToInt32(lblFinishedStockRatioId.Text), Convert.ToInt32(lblCutid.Text), lblItemname.Text, lblCompanyLotNo.Text, Convert.ToInt32(txtR30FS.Text), Convert.ToInt32(txtR32FS.Text), Convert.ToInt32(txtR34FS.Text), Convert.ToInt32(txtR36FS.Text), Convert.ToInt32(txtRXSFS.Text), Convert.ToInt32(txtRSFS.Text), Convert.ToInt32(txtRMFS.Text), Convert.ToInt32(txtRLFS.Text), Convert.ToInt32(txtRXLFS.Text), Convert.ToInt32(txtRXXLFS.Text), Convert.ToInt32(txtR3XLFS.Text), Convert.ToInt32(txtR4XLFS.Text), Convert.ToInt32(txtR30HS.Text), Convert.ToInt32(txtR32HS.Text), Convert.ToInt32(txtR34HS.Text), Convert.ToInt32(txtR36HS.Text), Convert.ToInt32(txtRXSHS.Text), Convert.ToInt32(txtRSHS.Text), Convert.ToInt32(txtRMHS.Text), Convert.ToInt32(txtRLHS.Text), Convert.ToInt32(txtRXLHS.Text), Convert.ToInt32(txtRXXLHS.Text), Convert.ToInt32(txtR3XLHS.Text), Convert.ToInt32(txtR4XLHS.Text), Convert.ToInt32(lbldespatchqty.Text), lblDesignCode.Text, Convert.ToDouble(lblr30f.Text), Convert.ToDouble(lblr32f.Text), Convert.ToDouble(lblr34f.Text), Convert.ToDouble(lblr36f.Text), Convert.ToDouble(lblrXSf.Text), Convert.ToDouble(lblrSf.Text), Convert.ToDouble(lblrMf.Text), Convert.ToDouble(lblrLf.Text), Convert.ToDouble(lblrXLf.Text), Convert.ToDouble(lblrxxLf.Text), Convert.ToDouble(lblr3XLf.Text), Convert.ToDouble(lblr4XLf.Text), Convert.ToDouble(lblr30h.Text), Convert.ToDouble(lblr32h.Text), Convert.ToDouble(lblr34h.Text), Convert.ToDouble(lblr36h.Text), Convert.ToDouble(lblrXSh.Text), Convert.ToDouble(lblrSh.Text), Convert.ToDouble(lblrMh.Text), Convert.ToDouble(lblrLh.Text), Convert.ToDouble(lblrXLh.Text), Convert.ToDouble(lblrXXLh.Text), Convert.ToDouble(lblr3XLh.Text), Convert.ToDouble(lblr4XLh.Text), Convert.ToDouble(lblttlamt.Text));
                    }

                    #endregion
                }


                #endregion

                Response.Redirect("DespatchGrid.aspx");
            }
        }
        protected void btnexit_OnClick(object sender, EventArgs e)
        {

        }
    }
}