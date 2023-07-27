using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using CommonLayer;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class CuttingPreprocess : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iid;
        DataSet dmerge1 = new DataSet();
        DataSet dsnneeww = new DataSet();

        DataTable dCrt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            iid = Request.QueryString.Get("CuttingID");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {


                //sTableName = Session["User"].ToString();
                DataTable dt = new DataTable();
                divcode.Visible = false;

                dt.Columns.Add("Transid");
                dt.Columns.Add("Design");
                dt.Columns.Add("Rate");
                dt.Columns.Add("meter");
                dt.Columns.Add("Shirt");
                dt.Columns.Add("Reqmeter");
                dt.Columns.Add("Actmeter");
                dt.Columns.Add("Reqshirt");
                dt.Columns.Add("brandid");
                dt.Columns.Add("brand");
                dt.Columns.Add("ledgerid");
                dt.Columns.Add("party");
                dt.Columns.Add("Fitid");
                dt.Columns.Add("Fit");
                dt.Columns.Add("TSFS");
                dt.Columns.Add("TSHS");
                dt.Columns.Add("TEFS");
                dt.Columns.Add("TEHS");
                dt.Columns.Add("TNFS");
                dt.Columns.Add("TNHS");
                dt.Columns.Add("FZFS");
                dt.Columns.Add("FZHS");
                dt.Columns.Add("FTFS");
                dt.Columns.Add("FTHS");
                dt.Columns.Add("FFFS");
                dt.Columns.Add("FFHS");
                dt.Columns.Add("WSP");
                dt.Columns.Add("avgsize");
                dt.Columns.Add("actsize");
                dt.Columns.Add("Extra");
                dt.Columns.Add("LLedger");
                dt.Columns.Add("Mainlab");
                dt.Columns.Add("FItLab");
                dt.Columns.Add("Washlab");
                dt.Columns.Add("Logolab");
                dt.Columns.Add("Total");


                ViewState["Data"] = dt;
                DataSet brandName = objBs.getBrandName();
                if (brandName.Tables[0].Rows.Count > 0)
                {
                    ddlBrand.DataSource = brandName.Tables[0];
                    ddlBrand.DataTextField = "BrandName";
                    ddlBrand.DataValueField = "BrandID";
                    ddlBrand.DataBind();
                    ddlBrand.Items.Insert(0, "Select Brand Name");
                }

                DataSet dst = objBs.Getcuttmastrr();
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        drpjobwork.DataSource = dst.Tables[0];
                        drpjobwork.DataTextField = "LedgerName";
                        drpjobwork.DataValueField = "LedgerID";
                        drpjobwork.DataBind();
                        drpjobwork.Items.Insert(0, "Select Cutting");
                    }
                }

                DataSet dsFit = objBs.GetFit();
                if (dsFit != null)
                {
                    if (dsFit.Tables[0].Rows.Count > 0)
                    {
                        ddlFit.DataSource = dsFit.Tables[0];
                        ddlFit.DataTextField = "Fit";
                        ddlFit.DataValueField = "FitID";
                        ddlFit.DataBind();
                        ddlFit.Items.Insert(0, "Select Fit");


                    }
                }

                DataSet dswidth = objBs.GetWidth();
                if (dswidth != null)
                {
                    if (dswidth.Tables[0].Rows.Count > 0)
                    {
                        drpwidth.DataSource = dswidth.Tables[0];
                        drpwidth.DataTextField = "Width";
                        drpwidth.DataValueField = "WidthID";
                        drpwidth.DataBind();
                        // drpwidth.Items.Insert(0, "Select Width");
                    }
                }


                DataSet dsrefno = objBs.getnewsupplierforcut(drpwidth.SelectedValue);
                if (dsrefno != null)
                {
                    if (dsrefno.Tables[0].Rows.Count > 0)
                    {
                        chkinvno.DataSource = dsrefno.Tables[0];
                        chkinvno.DataTextField = "fabno";
                        chkinvno.DataValueField = "fabid";
                        chkinvno.DataBind();
                        //  drpwidth.Items.Insert(0, "Select Width");
                    }
                }

                DataSet dcheckwidth = objBs.getwidthnewprocess(drpwidth.SelectedItem.Text);
                if (dcheckwidth.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dcheckwidth.Tables[0].Rows.Count; i++)
                    {
                        string size = dcheckwidth.Tables[0].Rows[i]["Sizeid"].ToString();
                        string value = dcheckwidth.Tables[0].Rows[i]["w"].ToString();
                        if (size == "3")
                        {
                            txtformal.Text = value;
                        }
                        else if (size == "4")
                        {
                            txtboys.Text = value;
                        }
                        else if (size == "5")
                        {
                            txtcasual.Text = value;
                        }

                    }
                }
                string date = DateTime.Now.ToString("dd/MM/yyyy");

                // txtdate.Text = date;

                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();


                if (iid != null)
                {
                    DataSet ds1 = objBs.getCuttingProcess(Convert.ToInt32(iid));
                    if (ds1 != null)
                    {

                    }
                }
                else
                {
                    txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    DataSet dss = objBs.getmaaxBillnoforcut("P");
                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        txtLotNo.Text = dss.Tables[0].Rows[0]["billId"].ToString();
                    }
                    btnadd.Text = "Save";
                    btnadd.Enabled = false;
                    btnprocess.Enabled = true;
                    radchecked(sender, e);
                }
            }
        }

        protected void Change_LotNo(object sender, EventArgs e)
        {
            txtLotNo.Text = "";
            if (drpjobwork.SelectedValue != "")
            {
                DataSet LotNO = objBs.getLotNo(Convert.ToInt32(drpjobwork.SelectedValue));
                txtLotNo.Text = LotNO.Tables[0].Rows[0]["LotNo"].ToString();
            }
            //else if (drpjobwork.SelectedValue == "386")
            //{
            //    DataSet LotNO2 = objBs.getLotNo2(Convert.ToInt32(drpjobwork.SelectedValue));
            //    txtLotNo.Text = LotNO2.Tables[0].Rows[0]["LotNo2"].ToString();
            //}
            //else
            //{
            //    DataSet LotNO3 = objBs.getLotNo3(Convert.ToInt32(drpjobwork.SelectedValue));
            //    txtLotNo.Text = LotNO3.Tables[0].Rows[0]["LotNo3"].ToString();
            //}

        }

        protected void ckhsize_index(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            if (radbtn.SelectedValue == "1")
            {
                if (Sddrrpfit.SelectedValue == "3")
                {
                    if (chkSizes.SelectedIndex >= 0)
                    {
                        tsfs.Visible = false; tshs.Visible = false;

                        tefs.Visible = false; tehs.Visible = false;

                        tnfs.Visible = false; tnhs.Visible = false;

                        fzfs.Visible = false; fzhs.Visible = false;

                        ftfs.Visible = false; fths.Visible = false;

                        fffs.Visible = false; ffhs.Visible = false;

                        int lop = 0;
                        //Loop through each item of checkboxlist
                        foreach (ListItem item in chkSizes.Items)
                        {
                            //check if item selected

                            if (item.Selected)
                            {

                                {
                                    if (item.Value == "1")
                                    {
                                        tsfs.Visible = true;
                                    }
                                    if (item.Value == "2")
                                    {
                                        //gridsize.Columns[8].Visible = true;
                                        tshs.Visible = true;
                                    }
                                    if (item.Value == "3")
                                    {
                                        //    gridsize.Columns[3].Visible = true;
                                        tefs.Visible = true;
                                    }
                                    if (item.Value == "4")
                                    {
                                        //gridsize.Columns[9].Visible = true;
                                        tehs.Visible = true;
                                    }
                                    if (item.Value == "5")
                                    {
                                        // gridsize.Columns[4].Visible = true;
                                        tnfs.Visible = true;
                                    }
                                    if (item.Value == "6")
                                    {
                                        // gridsize.Columns[10].Visible = true;
                                        tnhs.Visible = true;
                                    }
                                    if (item.Value == "7")
                                    {
                                        //gridsize.Columns[5].Visible = true;
                                        fzfs.Visible = true;
                                    }
                                    if (item.Value == "8")
                                    {
                                        // gridsize.Columns[11].Visible = true;
                                        fzhs.Visible = true;
                                    }
                                    if (item.Value == "9")
                                    {
                                        //  gridsize.Columns[6].Visible = true;
                                        ftfs.Visible = true;
                                    }
                                    if (item.Value == "10")
                                    {
                                        // gridsize.Columns[12].Visible = true;
                                        fths.Visible = true;
                                    }
                                    if (item.Value == "11")
                                    {
                                        // gridsize.Columns[7].Visible = true;
                                        fffs.Visible = true;
                                    }
                                    if (item.Value == "12")
                                    {
                                        // gridsize.Columns[13].Visible = true;

                                        ffhs.Visible = true;
                                    }


                                    lop++;

                                }
                            }
                        }

                    }
                    else
                    {
                        tsfs.Visible = false; tshs.Visible = false;

                        tefs.Visible = false; tehs.Visible = false;

                        tnfs.Visible = false; tnhs.Visible = false;

                        fzfs.Visible = false; fzhs.Visible = false;

                        ftfs.Visible = false; fths.Visible = false;

                        fffs.Visible = false; ffhs.Visible = false;
                    }
                }
                else if (Sddrrpfit.SelectedValue == "4")
                {
                    if (chkSizes.SelectedIndex >= 0)
                    {
                        b28fs.Visible = false; b28hs.Visible = false;

                        b30fs.Visible = false; b30hs.Visible = false;

                        b32fs.Visible = false; b32hs.Visible = false;

                        b34fs.Visible = false; b34hs.Visible = false;

                        b36fs.Visible = false; b36hs.Visible = false;

                        b38fs.Visible = false; b38hs.Visible = false;

                        int lop = 0;
                        //Loop through each item of checkboxlist
                        foreach (ListItem item in chkSizes.Items)
                        {
                            //check if item selected

                            if (item.Selected)
                            {

                                {
                                    if (item.Value == "23")
                                    {
                                        b28fs.Visible = true;
                                    }
                                    if (item.Value == "24")
                                    {
                                        //gridsize.Columns[8].Visible = true;
                                        b28hs.Visible = true;
                                    }
                                    if (item.Value == "25")
                                    {
                                        //    gridsize.Columns[3].Visible = true;
                                        b30fs.Visible = true;
                                    }
                                    if (item.Value == "26")
                                    {
                                        //gridsize.Columns[9].Visible = true;
                                        b30hs.Visible = true;
                                    }
                                    if (item.Value == "27")
                                    {
                                        // gridsize.Columns[4].Visible = true;
                                        b32fs.Visible = true;
                                    }
                                    if (item.Value == "28")
                                    {
                                        // gridsize.Columns[10].Visible = true;
                                        b32hs.Visible = true;
                                    }
                                    if (item.Value == "29")
                                    {
                                        //gridsize.Columns[5].Visible = true;
                                        b34fs.Visible = true;
                                    }
                                    if (item.Value == "30")
                                    {
                                        // gridsize.Columns[11].Visible = true;
                                        b34hs.Visible = true;
                                    }
                                    if (item.Value == "31")
                                    {
                                        //  gridsize.Columns[6].Visible = true;
                                        b36fs.Visible = true;
                                    }
                                    if (item.Value == "32")
                                    {
                                        // gridsize.Columns[12].Visible = true;
                                        b36hs.Visible = true;
                                    }
                                    if (item.Value == "33")
                                    {
                                        // gridsize.Columns[7].Visible = true;
                                        b38fs.Visible = true;
                                    }
                                    if (item.Value == "34")
                                    {
                                        // gridsize.Columns[13].Visible = true;

                                        b38hs.Visible = true;
                                    }


                                    lop++;

                                }
                            }
                        }

                    }
                    else
                    {
                        b28fs.Visible = false; b28hs.Visible = false;

                        b30fs.Visible = false; b30hs.Visible = false;

                        b32fs.Visible = false; b32hs.Visible = false;

                        b34fs.Visible = false; b34hs.Visible = false;

                        b36fs.Visible = false; b36hs.Visible = false;

                        b38fs.Visible = false; b38hs.Visible = false;
                    }
                }
                else if (Sddrrpfit.SelectedValue == "5")
                {
                    if (chkSizes.SelectedIndex >= 0)
                    {
                        sfs.Visible = false; shs.Visible = false;

                        mfs.Visible = false; mhs.Visible = false;

                        lfs.Visible = false; lhs.Visible = false;

                        xlfs.Visible = false; xlhs.Visible = false;

                        xxlfs.Visible = false; xxlhs.Visible = false;


                        int lop = 0;
                        //Loop through each item of checkboxlist
                        foreach (ListItem item in chkSizes.Items)
                        {
                            //check if item selected

                            if (item.Selected)
                            {

                                {
                                    if (item.Value == "13")
                                    {
                                        sfs.Visible = true;
                                    }
                                    if (item.Value == "14")
                                    {
                                        //gridsize.Columns[8].Visible = true;
                                        shs.Visible = true;
                                    }
                                    if (item.Value == "15")
                                    {
                                        //    gridsize.Columns[3].Visible = true;
                                        mfs.Visible = true;
                                    }
                                    if (item.Value == "16")
                                    {
                                        //gridsize.Columns[9].Visible = true;
                                        mhs.Visible = true;
                                    }
                                    if (item.Value == "17")
                                    {
                                        // gridsize.Columns[4].Visible = true;
                                        lfs.Visible = true;
                                    }
                                    if (item.Value == "18")
                                    {
                                        // gridsize.Columns[10].Visible = true;
                                        lhs.Visible = true;
                                    }
                                    if (item.Value == "19")
                                    {
                                        //gridsize.Columns[5].Visible = true;
                                        xlfs.Visible = true;
                                    }
                                    if (item.Value == "20")
                                    {
                                        // gridsize.Columns[11].Visible = true;
                                        xlhs.Visible = true;
                                    }
                                    if (item.Value == "21")
                                    {
                                        //  gridsize.Columns[6].Visible = true;
                                        xxlfs.Visible = true;
                                    }
                                    if (item.Value == "22")
                                    {
                                        // gridsize.Columns[12].Visible = true;
                                        xxlhs.Visible = true;
                                    }
                                    lop++;

                                }
                            }
                        }

                    }
                    else
                    {
                        sfs.Visible = false; shs.Visible = false;

                        mfs.Visible = false; mhs.Visible = false;

                        lfs.Visible = false; lhs.Visible = false;

                        xlfs.Visible = false; xlhs.Visible = false;

                        xxlfs.Visible = false; xxlhs.Visible = false;
                    }
                }
            }

            else
            {

                if (chkSizes.SelectedIndex >= 0)
                {
                    gridsize.Columns[2].Visible = false; //36FS
                    gridsize.Columns[3].Visible = false; //38FS

                    gridsize.Columns[4].Visible = false;//39Fs
                    gridsize.Columns[5].Visible = false;//40Fs

                    gridsize.Columns[6].Visible = false; //42FS
                    gridsize.Columns[7].Visible = false; //44FS

                    gridsize.Columns[8].Visible = false; //36HS
                    gridsize.Columns[9].Visible = false; //38HS

                    gridsize.Columns[10].Visible = false; //39HS
                    gridsize.Columns[11].Visible = false; //40HS

                    gridsize.Columns[12].Visible = false; //42HS
                    gridsize.Columns[13].Visible = false; //44HS



                    int lop = 0;
                    //Loop through each item of checkboxlist
                    foreach (ListItem item in chkSizes.Items)
                    {
                        //check if item selected

                        if (item.Selected)
                        {

                            {
                                if (item.Value == "1")
                                {
                                    gridsize.Columns[2].Visible = true;
                                }
                                if (item.Value == "2")
                                {
                                    gridsize.Columns[8].Visible = true;
                                }
                                if (item.Value == "3")
                                {
                                    gridsize.Columns[3].Visible = true;
                                }
                                if (item.Value == "4")
                                {
                                    gridsize.Columns[9].Visible = true;
                                }
                                if (item.Value == "5")
                                {
                                    gridsize.Columns[4].Visible = true;
                                }
                                if (item.Value == "6")
                                {
                                    gridsize.Columns[10].Visible = true;
                                }
                                if (item.Value == "7")
                                {
                                    gridsize.Columns[5].Visible = true;
                                }
                                if (item.Value == "8")
                                {
                                    gridsize.Columns[11].Visible = true;
                                }
                                if (item.Value == "9")
                                {
                                    gridsize.Columns[6].Visible = true;
                                }
                                if (item.Value == "10")
                                {
                                    gridsize.Columns[12].Visible = true;
                                }
                                if (item.Value == "11")
                                {
                                    gridsize.Columns[7].Visible = true;
                                }
                                if (item.Value == "12")
                                {
                                    gridsize.Columns[13].Visible = true;
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
                    gridsize.Columns[2].Visible = false;
                    gridsize.Columns[3].Visible = false;

                    gridsize.Columns[4].Visible = false;
                    gridsize.Columns[5].Visible = false;

                    gridsize.Columns[6].Visible = false; gridsize.Columns[7].Visible = false;

                    gridsize.Columns[8].Visible = false; gridsize.Columns[9].Visible = false;

                    gridsize.Columns[10].Visible = false; gridsize.Columns[11].Visible = false;

                    gridsize.Columns[12].Visible = false; gridsize.Columns[13].Visible = false;


                }
            }
        }

        protected void ckhsize_indexforcasula(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            if (radbtn.SelectedValue == "1")
            {
                if (chkSizes.SelectedIndex >= 0)
                {
                    sfs.Visible = false; shs.Visible = false;

                    mfs.Visible = false; mhs.Visible = false;

                    lfs.Visible = false; lhs.Visible = false;

                    xlfs.Visible = false; xlhs.Visible = false;

                    xxlfs.Visible = false; xxlhs.Visible = false;


                    int lop = 0;
                    //Loop through each item of checkboxlist
                    foreach (ListItem item in chkSizes.Items)
                    {
                        //check if item selected

                        if (item.Selected)
                        {

                            {
                                if (item.Value == "13")
                                {
                                    sfs.Visible = true;
                                }
                                if (item.Value == "14")
                                {
                                    //gridsize.Columns[8].Visible = true;
                                    shs.Visible = true;
                                }
                                if (item.Value == "15")
                                {
                                    //    gridsize.Columns[3].Visible = true;
                                    mfs.Visible = true;
                                }
                                if (item.Value == "16")
                                {
                                    //gridsize.Columns[9].Visible = true;
                                    mhs.Visible = true;
                                }
                                if (item.Value == "17")
                                {
                                    // gridsize.Columns[4].Visible = true;
                                    lfs.Visible = true;
                                }
                                if (item.Value == "18")
                                {
                                    // gridsize.Columns[10].Visible = true;
                                    lhs.Visible = true;
                                }
                                if (item.Value == "19")
                                {
                                    //gridsize.Columns[5].Visible = true;
                                    xlfs.Visible = true;
                                }
                                if (item.Value == "20")
                                {
                                    // gridsize.Columns[11].Visible = true;
                                    xlhs.Visible = true;
                                }
                                if (item.Value == "21")
                                {
                                    //  gridsize.Columns[6].Visible = true;
                                    xxlfs.Visible = true;
                                }
                                if (item.Value == "22")
                                {
                                    // gridsize.Columns[12].Visible = true;
                                    xxlhs.Visible = true;
                                }
                                lop++;

                            }
                        }
                    }

                }
                else
                {
                    sfs.Visible = false; shs.Visible = false;

                    mfs.Visible = false; mhs.Visible = false;

                    lfs.Visible = false; lhs.Visible = false;

                    xlfs.Visible = false; xlhs.Visible = false;

                    xxlfs.Visible = false; xxlhs.Visible = false;
                }
            }
        }

        protected void ckhsize_indexforboys(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            if (radbtn.SelectedValue == "1")
            {
                if (chkSizes.SelectedIndex >= 0)
                {
                    b28fs.Visible = false; b28hs.Visible = false;

                    b30fs.Visible = false; b30hs.Visible = false;

                    b32fs.Visible = false; b32hs.Visible = false;

                    b34fs.Visible = false; b34hs.Visible = false;

                    b36fs.Visible = false; b36hs.Visible = false;

                    b38fs.Visible = false; b38hs.Visible = false;

                    int lop = 0;
                    //Loop through each item of checkboxlist
                    foreach (ListItem item in chkSizes.Items)
                    {
                        //check if item selected

                        if (item.Selected)
                        {

                            {
                                if (item.Value == "23")
                                {
                                    b28fs.Visible = true;
                                }
                                if (item.Value == "24")
                                {
                                    //gridsize.Columns[8].Visible = true;
                                    b28hs.Visible = true;
                                }
                                if (item.Value == "25")
                                {
                                    //    gridsize.Columns[3].Visible = true;
                                    b30fs.Visible = true;
                                }
                                if (item.Value == "26")
                                {
                                    //gridsize.Columns[9].Visible = true;
                                    b30hs.Visible = true;
                                }
                                if (item.Value == "27")
                                {
                                    // gridsize.Columns[4].Visible = true;
                                    b32fs.Visible = true;
                                }
                                if (item.Value == "28")
                                {
                                    // gridsize.Columns[10].Visible = true;
                                    b32hs.Visible = true;
                                }
                                if (item.Value == "29")
                                {
                                    //gridsize.Columns[5].Visible = true;
                                    b34fs.Visible = true;
                                }
                                if (item.Value == "30")
                                {
                                    // gridsize.Columns[11].Visible = true;
                                    b34hs.Visible = true;
                                }
                                if (item.Value == "31")
                                {
                                    //  gridsize.Columns[6].Visible = true;
                                    b36fs.Visible = true;
                                }
                                if (item.Value == "32")
                                {
                                    // gridsize.Columns[12].Visible = true;
                                    b36hs.Visible = true;
                                }
                                if (item.Value == "33")
                                {
                                    // gridsize.Columns[7].Visible = true;
                                    b38fs.Visible = true;
                                }
                                if (item.Value == "34")
                                {
                                    // gridsize.Columns[13].Visible = true;

                                    b38hs.Visible = true;
                                }


                                lop++;

                            }
                        }
                    }

                }
                else
                {
                    b28fs.Visible = false; b28hs.Visible = false;

                    b30fs.Visible = false; b30hs.Visible = false;

                    b32fs.Visible = false; b32hs.Visible = false;

                    b34fs.Visible = false; b34hs.Visible = false;

                    b36fs.Visible = false; b36hs.Visible = false;

                    b38fs.Visible = false; b38hs.Visible = false;
                }
            }


        }

        protected void call_Click(object sender, EventArgs e)
        {
            DataSet dcalculate = new DataSet();

            //btnadd.Enabled = false;
            //string width = string.Empty;
            //if (btnadd.Text == "Save")
            //{
            //    if (ddlFit.SelectedValue == "Select fit")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit. Thank you');", true);
            //        return;
            //    }
            //    if (CheckBoxList2.SelectedIndex >= 0)
            //    {
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Design Number. Thank you');", true);
            //        return;

            //    }

            //    if (chkinvno.SelectedIndex >= 0)
            //    {
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Invoice Number. Thank you');", true);
            //        return;

            //    }




            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        double totgnd = 0;
            //        TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //        Label lblid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblid");
            //        TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpparty");
            //        TextBox meter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");
            //        TextBox reqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");




            //        TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");

            //        if (drpwidth.SelectedValue == "1")
            //        {
            //            width = "36";
            //        }
            //        else if (drpwidth.SelectedValue == "2")
            //        {
            //            width = "48";
            //        }
            //        else
            //        {
            //            width = "54";
            //        }



            //        dcalculate = objBs.getsizeforcutt(ddlFit.SelectedValue, width);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt36fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt36fs.Text);
            //            }
            //        }


            //        TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");

            //        //  dcalculate = objBs.getsizeforworkorder("36HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt36hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt36hs.Text);
            //            }
            //        }

            //        TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
            //        //  dcalculate = objBs.getsizeforworkorder("38FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt38fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt38fs.Text);
            //            }
            //        }

            //        TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
            //        //   dcalculate = objBs.getsizeforworkorder("38HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt38hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt38hs.Text);
            //            }
            //        }

            //        TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
            //        //  dcalculate = objBs.getsizeforworkorder("39FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt39fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt39fs.Text);
            //            }
            //        }

            //        TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
            //        //   dcalculate = objBs.getsizeforworkorder("39HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt39hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt39hs.Text);
            //            }
            //        }

            //        TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
            //        // dcalculate = objBs.getsizeforworkorder("40FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt40fs.Text) * wid;
            //                //   grandfs = grandfs + Convert.ToDouble(txt40fs.Text);
            //            }
            //        }

            //        TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
            //        //   dcalculate = objBs.getsizeforworkorder("40HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt40hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt40hs.Text);
            //            }
            //        }

            //        TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
            //        //  dcalculate = objBs.getsizeforworkorder("42FS", str);

            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt42fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt42fs.Text);
            //            }
            //        }

            //        TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
            //        //  dcalculate = objBs.getsizeforworkorder("42HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt42hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt42hs.Text);
            //            }
            //        }

            //        TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
            //        //  dcalculate = objBs.getsizeforworkorder("44FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt44fs.Text) * wid;
            //                // grandfs = grandfs + Convert.ToDouble(txt44fs.Text);
            //            }
            //        }

            //        TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");
            //        //   dcalculate = objBs.getsizeforworkorder("44HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt44hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt44hs.Text);
            //            }
            //        }

            //        reqmeter.Text = totgnd.ToString();





            //        int col = vLoop + 1;

            //        double meter1 = Convert.ToDouble(meter.Text);
            //        double reqmeter1 = Convert.ToDouble(totgnd);


            //        if (drpparty.SelectedValue == "Select Party Name")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name in Row " + col + ". Thank you');", true);
            //            btnadd.Enabled = false;
            //            return;
            //        }
            //        else
            //        {
            //            btnadd.Enabled = true;
            //        }



            //        double number = meter1 - reqmeter1;
            //        if (number < 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter in Row " + col + ". Thank you');", true);
            //            btnadd.Enabled = false;
            //            return;
            //        }
            //        else
            //        {
            //            btnadd.Enabled = true;
            //        }





            //    }
            //}
        }

        protected void ddrpartyselected_changed(object sender, EventArgs e)
        {
            string partyname = string.Empty;
            for (int vLoop1 = 0; vLoop1 < gridsize.Rows.Count; vLoop1++)
            {
                DropDownList drpparty1 = (DropDownList)gridsize.Rows[vLoop1].FindControl("ddrparty");
                DropDownList drpfit = (DropDownList)gridsize.Rows[vLoop1].FindControl("ddrpfit");
                partyname = drpparty1.SelectedItem.Text;
                for (int vLoop = 0; vLoop < grdcust.Rows.Count; vLoop++)
                {
                    TextBox txtno = (TextBox)grdcust.Rows[vLoop].FindControl("txtcust");

                    if (partyname == txtno.Text)
                    {
                        //   ledgerr = drpparty.SelectedValue;
                        DropDownList drpparty = (DropDownList)grdcust.Rows[vLoop].FindControl("drrplab");
                        DropDownList ddrpfit = (DropDownList)grdcust.Rows[vLoop].FindControl("ddrrpfit");
                        drpfit.SelectedValue = ddrpfit.SelectedValue;

                    }
                }
            }
            ddpfitindexchanged(sender, e);


        }

        protected void ddrbrandselected_changed(object sender, EventArgs e)
        {
            string brandname = string.Empty;
            for (int vLoop1 = 0; vLoop1 < gridsize.Rows.Count; vLoop1++)
            {
                DropDownList drpbrand1 = (DropDownList)gridsize.Rows[vLoop1].FindControl("ddrbrand");
                DropDownList drpfit = (DropDownList)gridsize.Rows[vLoop1].FindControl("ddrpfit");
                brandname = drpbrand1.SelectedItem.Text;
                for (int vLoop = 0; vLoop < grdcust.Rows.Count; vLoop++)
                {
                    TextBox txtno = (TextBox)grdcust.Rows[vLoop].FindControl("txtbrand");

                    if (brandname == txtno.Text)
                    {
                        //   ledgerr = drpparty.SelectedValue;
                        DropDownList drpbrand = (DropDownList)grdcust.Rows[vLoop].FindControl("drrpbrand");
                        DropDownList ddrpfit = (DropDownList)grdcust.Rows[vLoop].FindControl("ddrrpfit");
                        drpfit.SelectedValue = ddrpfit.SelectedValue;

                    }
                }
            }
            ddpfitindexchanged(sender, e);


        }

        protected void Sddrpartyselected_changed(object sender, EventArgs e)
        {
            //    txt36FS.Focus();
            //    if (txt36FS.Text == "0")
            //    {
            //        txt36FS.Text = "";
            //    }
            //    else
            //    {

            //    }


        }

        protected void Add_Click(object sender, EventArgs e)
        {

            string ledgerr = string.Empty;
            string mainlab = string.Empty;
            string party = string.Empty;
            string maar = string.Empty;
            string mmrrpp = string.Empty;
            string ddffiitt = string.Empty;

            bool fitlab = false;
            bool washlab = false;
            bool logolab = false;
            string cond1 = string.Empty;
            string con = string.Empty;
            string partyname = string.Empty;
            //string Mode = Request.QueryString.Get("Mode");
            //DataSet dcalculate = new DataSet();

            //btnadd.Enabled = false;
            //string width = string.Empty;

            DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Save")
            {
                if (radcuttype.SelectedValue == "1")
                {
                    if (drpjobwork.SelectedValue == "Select Cutting")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Job Work. Thank you!!');", true);
                        return;
                    }
                    else
                    {

                    }

                    int istas = objBs.updatesizesettingg(drpwidth.SelectedItem.Text, txtformal.Text, txtboys.Text, txtcasual.Text);


                    dCrt = (DataTable)ViewState["Data"];
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dCrt);

                    int iStatus = 0;

                    iStatus = objBs.insertcutnewone(txtLotNo.Text, deliverydate, drpwidth.SelectedValue, drpjobwork.SelectedValue, txtprod.Text, "0", "0", radbtn.SelectedValue, txtadjmeter.Text, lblmin.Text, lblmax.Text, radcuttype.SelectedValue, "P", txtcuttingrate.Text,Sddrrpfit.SelectedValue);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            string trainid = ds.Tables[0].Rows[i]["Transid"].ToString();
                            string design = ds.Tables[0].Rows[i]["Design"].ToString();
                            party = ds.Tables[0].Rows[i]["ledgerid"].ToString();
                            partyname = ds.Tables[0].Rows[i]["Party"].ToString();
                            string totmeter = ds.Tables[0].Rows[i]["meter"].ToString();
                            string shirt = ds.Tables[0].Rows[i]["Shirt"].ToString();
                            string reqmeter = ds.Tables[0].Rows[i]["reqmeter"].ToString();
                            string reqshirt = ds.Tables[0].Rows[i]["reqshirt"].ToString();
                            string fitid = ds.Tables[0].Rows[i]["Fitid"].ToString();
                            string rate = ds.Tables[0].Rows[i]["Rate"].ToString();

                            string brandiid = ds.Tables[0].Rows[i]["brandid"].ToString();
                            string brandname = ds.Tables[0].Rows[i]["brand"].ToString();


                            string tsfs = ds.Tables[0].Rows[i]["TSFS"].ToString();
                            string tshs = ds.Tables[0].Rows[i]["TSHS"].ToString();


                            string tefs = ds.Tables[0].Rows[i]["TEFS"].ToString();
                            string tehs = ds.Tables[0].Rows[i]["TEHS"].ToString();

                            string tnfs = ds.Tables[0].Rows[i]["TNFS"].ToString();
                            string tnhs = ds.Tables[0].Rows[i]["TNHS"].ToString();

                            string fzfs = ds.Tables[0].Rows[i]["FZFS"].ToString();
                            string fzhs = ds.Tables[0].Rows[i]["FZHS"].ToString();

                            string ftfs = ds.Tables[0].Rows[i]["FTFS"].ToString();
                            string fths = ds.Tables[0].Rows[i]["FTHS"].ToString();

                            string fffs = ds.Tables[0].Rows[i]["FFFS"].ToString();
                            string ffhs = ds.Tables[0].Rows[i]["FFHS"].ToString();

                            string wsp = ds.Tables[0].Rows[i]["WSP"].ToString();

                            string avgsize = ds.Tables[0].Rows[i]["avgsize"].ToString();
                            string extra = ds.Tables[0].Rows[i]["Extra"].ToString();
                            string actmeter = ds.Tables[0].Rows[i]["actmeter"].ToString();
                            string actsize = ds.Tables[0].Rows[i]["actsize"].ToString();

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drpFit.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                                maar = Stxtmargin.Text;
                                mmrrpp = Stxtwsp.Text;
                                ddffiitt = drpFit.SelectedValue;
                            }


                            foreach (ListItem listItem in chkinvno.Items)
                            {
                                if (listItem.Text != "All")
                                {
                                    if (listItem.Selected)
                                    {
                                        cond1 += " Fabid='" + listItem.Value + "' ,";
                                        con += listItem.Value + ",";
                                    }
                                }
                            }
                            cond1 = cond1.TrimEnd(',');
                            cond1 = cond1.Replace(",", "or");

                            con = con.TrimEnd(',');

                            //int iStatus2 = objBs.insertTranscutnewone(txtLotNo.Text, "0", trainid, design, party, totmeter, reqmeter, rate, tsfs, tshs, tefs, tehs, tnfs, tnhs, fzfs, fzhs, ftfs, fths, fffs, ffhs, shirt, reqshirt, fitid, ledgerr, mainlab, fitlab, washlab, logolab, maar, mmrrpp, extra, ddffiitt, avgsize, radcuttype.SelectedValue, cond1, con, brandiid, actmeter,actsize);

                        }
                    }
                }
                //  else if (radcuttype.SelectedValue == "2")
                //{

                //    if (drpjobwork.SelectedValue == "Select Job Work")
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Job Work. Thank you!!');", true);
                //        return;
                //    }
                //    else
                //    {

                //    }

                //    int istas = objBs.updatesizesettingg(drpwidth.SelectedItem.Text, txtformal.Text, txtboys.Text,txtcasual.Text);


                //    dCrt = (DataTable)ViewState["Data"];
                //    DataSet ds = new DataSet();
                //    ds.Tables.Add(dCrt);

                //    int iStatus = 0;

                //    iStatus = objBs.insertcutnewone(txtLotNo.Text, deliverydate, drpwidth.SelectedValue, drpjobwork.SelectedValue, txtprod.Text, "0", "0", radbtn.SelectedValue, txtadjmeter.Text, lblmin.Text, lblmax.Text, radcuttype.SelectedValue,"P");

                //    if (ds.Tables[0].Rows.Count > 0)
                //    {

                //        //dr["Transid"] = dddldesign.SelectedValue;dr["Design"] = dddldesign.SelectedItem.Text; dr["Rate"] = txtDesignRate.Text;

                //        //dr["meter"] = txtAvailableMtr.Text;dr["Shirt"] = txtNoofShirts.Text;dr["reqmeter"] = txtavamet2.Text; dr["reqshirt"] = txttotshirt2.Text;
                //        //dr["ledgerid"] = drpCustomer2.SelectedValue; dr["party"] = drpCustomer2.SelectedItem.Text; dr["Fitid"] = drpFit2.SelectedValue;
                //        //dr["Fit"] = drpFit2.SelectedItem.Text;dr["TSFS"] = txt36FS2.Text;dr["TSHS"] = txt36HS2.Text; dr["TEFS"] = txt38FS2.Text;
                //        //dr["TEHS"] = txt38HS2.Text;dr["FZFS"] = txt40FS2.Text; dr["FZHS"] = txt38HS2.Text; dr["FTFS"] = txt42FS2.Text;
                //        //dr["FTHS"] = txt38HS2.Text; dr["LLedger"] = ledgerr; dr["Mainlab"] = mainlab; dr["FItLab"] = fitlab;dr["Washlab"] = washlab; dr["Logolab"] = logolab;
                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {

                //            string trainid = ds.Tables[0].Rows[i]["Transid"].ToString();
                //            string design = ds.Tables[0].Rows[i]["Design"].ToString();
                //            party = ds.Tables[0].Rows[i]["ledgerid"].ToString();
                //            partyname = ds.Tables[0].Rows[i]["Party"].ToString();
                //            string totmeter = ds.Tables[0].Rows[i]["meter"].ToString();
                //            string shirt = ds.Tables[0].Rows[i]["Shirt"].ToString();
                //            string reqmeter = ds.Tables[0].Rows[i]["reqmeter"].ToString();
                //            string reqshirt = ds.Tables[0].Rows[i]["reqshirt"].ToString();
                //            string fitid = ds.Tables[0].Rows[i]["Fitid"].ToString();
                //            string rate = ds.Tables[0].Rows[i]["Rate"].ToString();

                //            string tsfs = ds.Tables[0].Rows[i]["TSFS"].ToString();
                //            string tshs = ds.Tables[0].Rows[i]["TSHS"].ToString();


                //            string tefs = ds.Tables[0].Rows[i]["TEFS"].ToString();
                //            string tehs = ds.Tables[0].Rows[i]["TEHS"].ToString();

                //            string tnfs = ds.Tables[0].Rows[i]["TNFS"].ToString();
                //            string tnhs = ds.Tables[0].Rows[i]["TNHS"].ToString();

                //            string fzfs = ds.Tables[0].Rows[i]["FZFS"].ToString();
                //            string fzhs = ds.Tables[0].Rows[i]["FZHS"].ToString();

                //            string ftfs = ds.Tables[0].Rows[i]["FTFS"].ToString();
                //            string fths = ds.Tables[0].Rows[i]["FTHS"].ToString();

                //            string fffs = ds.Tables[0].Rows[i]["FFFS"].ToString();
                //            string ffhs = ds.Tables[0].Rows[i]["FFHS"].ToString();

                //            string wsp = ds.Tables[0].Rows[i]["WSP"].ToString();

                //            string avgsize = ds.Tables[0].Rows[i]["avgsize"].ToString();
                //            string extra = ds.Tables[0].Rows[i]["Extra"].ToString();

                //            if (radbtn.SelectedValue == "1")
                //            {
                //                ledgerr = ddlSupplier.SelectedValue;
                //                mainlab = drplab.SelectedValue;
                //                fitlab = chkfit.Checked;
                //                washlab = Chkwash.Checked;
                //                logolab = Chllogo.Checked;
                //                maar = Stxtmargin.Text;
                //                mmrrpp = Stxtwsp.Text;
                //                ddffiitt = drpFit.SelectedValue;
                //                //if (Stxtmargin.Text == "0" || Stxtmargin.Text == "")
                //                //{
                //                //    mmrrpp = "0";
                //                //    maar = "10";

                //                //}
                //                //else
                //                //{

                //                //    mmrrpp = Stxtmargin.Text;
                //                //    maar = "0";
                //                //}
                //            }
                //            else
                //            {
                //                for (int vLoop = 0; vLoop < grdcust.Rows.Count; vLoop++)
                //                {


                //                    TextBox txtno = (TextBox)grdcust.Rows[vLoop].FindControl("txtcust");

                //                    if (partyname == txtno.Text)
                //                    {
                //                        //   ledgerr = drpparty.SelectedValue;
                //                        DropDownList drpparty = (DropDownList)grdcust.Rows[vLoop].FindControl("drrplab");
                //                        CheckBox fitll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchkfit");
                //                        CheckBox wasll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchkwash");
                //                        CheckBox logll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchklogo");
                //                        TextBox txtmargins = (TextBox)grdcust.Rows[vLoop].FindControl("txtmargin");
                //                        DropDownList ddrpfit = (DropDownList)grdcust.Rows[vLoop].FindControl("ddrrpfit");
                //                        //if (txtmmrrp.Text == "0" || txtmmrrp.Text == "")
                //                        //{
                //                        //    mmrrpp = "0";
                //                        //    maar = "10";
                //                        //}
                //                        //else
                //                        //{

                //                        //    mmrrpp = txtmmrrp.Text;
                //                        //    maar = "0";
                //                        //}


                //                        ledgerr = party;
                //                        mainlab = drpparty.SelectedValue;
                //                        fitlab = fitll.Checked;
                //                        washlab = wasll.Checked;
                //                        logolab = logll.Checked;
                //                        maar = txtmargins.Text;
                //                        mmrrpp = wsp;
                //                        ddffiitt = ddrpfit.SelectedValue;

                //                    }


                //                }
                //            }

                //            foreach (ListItem listItem in chkinvno.Items)
                //            {
                //                if (listItem.Text != "All")
                //                {
                //                    if (listItem.Selected)
                //                    {
                //                        cond1 += " Fabid='" + listItem.Value + "' ,";
                //                        con += listItem.Value + ",";
                //                    }
                //                }
                //            }
                //            cond1 = cond1.TrimEnd(',');
                //            cond1 = cond1.Replace(",", "or");

                //            int iStatus2 = objBs.insertTranscutnewone(txtLotNo.Text, "0", trainid, design, party, totmeter, reqmeter, rate, tsfs, tshs, tefs, tehs, tnfs, tnhs, fzfs, fzhs, ftfs, fths, fffs, ffhs, shirt, reqshirt, fitid, ledgerr, mainlab, fitlab, washlab, logolab, maar, mmrrpp, extra, ddffiitt, avgsize, radcuttype.SelectedValue, cond1, con);

                //        }
                //    }

                //}




            }
            //    if (ddlFit.SelectedValue == "Select fit")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit. Thank you');", true);
            //        return;
            //    }
            //    if (CheckBoxList2.SelectedIndex >= 0)
            //    {
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Design Number. Thank you');", true);
            //        return;

            //    }

            //    if (chkinvno.SelectedIndex >= 0)
            //    {
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Invoice Number. Thank you');", true);
            //        return;

            //    }

            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        double totgnd = 0;

            //        TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //        Label lblid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblid");
            //        TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpparty");
            //        TextBox meter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");
            //        TextBox reqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

            //        TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");

            //        if (drpwidth.SelectedValue == "1")
            //        {
            //            width = "36";
            //        }
            //        else if (drpwidth.SelectedValue == "2")
            //        {
            //            width = "48";
            //        }
            //        else
            //        {
            //            width = "54";
            //        }



            //        dcalculate = objBs.getsizeforcutt(ddlFit.SelectedValue, width);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt36fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt36fs.Text);
            //            }
            //        }


            //        TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");

            //        //  dcalculate = objBs.getsizeforworkorder("36HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt36hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt36hs.Text);
            //            }
            //        }

            //        TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
            //        //  dcalculate = objBs.getsizeforworkorder("38FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt38fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt38fs.Text);
            //            }
            //        }

            //        TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
            //        //   dcalculate = objBs.getsizeforworkorder("38HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt38hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt38hs.Text);
            //            }
            //        }

            //        TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
            //        //  dcalculate = objBs.getsizeforworkorder("39FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt39fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt39fs.Text);
            //            }
            //        }

            //        TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
            //        //   dcalculate = objBs.getsizeforworkorder("39HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt39hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt39hs.Text);
            //            }
            //        }

            //        TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
            //        // dcalculate = objBs.getsizeforworkorder("40FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt40fs.Text) * wid;
            //                //   grandfs = grandfs + Convert.ToDouble(txt40fs.Text);
            //            }
            //        }

            //        TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
            //        //   dcalculate = objBs.getsizeforworkorder("40HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt40hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt40hs.Text);
            //            }
            //        }

            //        TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
            //        //  dcalculate = objBs.getsizeforworkorder("42FS", str);

            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt42fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt42fs.Text);
            //            }
            //        }

            //        TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
            //        //  dcalculate = objBs.getsizeforworkorder("42HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt42hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt42hs.Text);
            //            }
            //        }

            //        TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
            //        //  dcalculate = objBs.getsizeforworkorder("44FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt44fs.Text) * wid;
            //                // grandfs = grandfs + Convert.ToDouble(txt44fs.Text);
            //            }
            //        }

            //        TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");
            //        //   dcalculate = objBs.getsizeforworkorder("44HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgnd = totgnd + Convert.ToDouble(txt44hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt44hs.Text);
            //            }
            //        }

            //        reqmeter.Text = totgnd.ToString();





            //        int col = vLoop + 1;

            //        double meter1 = Convert.ToDouble(meter.Text);
            //        double reqmeter1 = Convert.ToDouble(totgnd);

            //        if (drpparty.SelectedValue == "Select Party Name")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name in Row " + col + ". Thank you');", true);
            //            btnadd.Enabled = false;
            //            return;
            //        }
            //        else
            //        {
            //            btnadd.Enabled = true;
            //        }

            //        double number = meter1 - reqmeter1;
            //        if (number < 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter in Row " + col + ". Thank you');", true);
            //            btnadd.Enabled = false;
            //            return;
            //        }
            //        else
            //        {
            //            btnadd.Enabled = true;
            //        }

            //    }


            //    //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    //{
            //    //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    //    Label lblid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblid");
            //    //    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
            //    //    DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpparty");
            //    //    TextBox meter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");
            //    //    TextBox reqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
            //    //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

            //    //    int col = vLoop + 1;

            //    //    double meter1 = Convert.ToDouble(meter.Text);
            //    //    double reqmeter1 = Convert.ToDouble(reqmeter.Text);

            //    //    double number = meter1 - reqmeter1;
            //    //    if (number < 0)
            //    //    {
            //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter in Row "+ col +". Thank you');", true);
            //    //        return;
            //    //    }

            //    //}

            //    int iStatus = 0;

            //    iStatus = objBs.insertcut(txtLotNo.Text, deliverydate, drpwidth.SelectedValue, ddlFit.SelectedValue);

            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        double totgndfin = 0;
            //        TextBox orderno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //        Label lblid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblid");
            //        TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
            //        DropDownList dparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpparty");
            //        TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");
            //        TextBox txtreq = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

            //        TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");

            //        if (drpwidth.SelectedValue == "1")
            //        {
            //            width = "36";
            //        }
            //        else if (drpwidth.SelectedValue == "2")
            //        {
            //            width = "48";
            //        }
            //        else
            //        {
            //            width = "54";
            //        }



            //        dcalculate = objBs.getsizeforcutt(ddlFit.SelectedValue, width);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt36fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt36fs.Text);
            //            }
            //        }


            //        TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");

            //        //  dcalculate = objBs.getsizeforworkorder("36HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt36hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt36hs.Text);
            //            }
            //        }

            //        TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
            //        //  dcalculate = objBs.getsizeforworkorder("38FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt38fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt38fs.Text);
            //            }
            //        }

            //        TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
            //        //   dcalculate = objBs.getsizeforworkorder("38HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt38hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt38hs.Text);
            //            }
            //        }

            //        TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
            //        //  dcalculate = objBs.getsizeforworkorder("39FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt39fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt39fs.Text);
            //            }
            //        }

            //        TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
            //        //   dcalculate = objBs.getsizeforworkorder("39HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt39hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt39hs.Text);
            //            }
            //        }

            //        TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
            //        // dcalculate = objBs.getsizeforworkorder("40FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt40fs.Text) * wid;
            //                //   grandfs = grandfs + Convert.ToDouble(txt40fs.Text);
            //            }
            //        }

            //        TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
            //        //   dcalculate = objBs.getsizeforworkorder("40HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt40hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt40hs.Text);
            //            }
            //        }

            //        TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
            //        //  dcalculate = objBs.getsizeforworkorder("42FS", str);

            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt42fs.Text) * wid;
            //                //  grandfs = grandfs + Convert.ToDouble(txt42fs.Text);
            //            }
            //        }

            //        TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
            //        //  dcalculate = objBs.getsizeforworkorder("42HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt42hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt42hs.Text);
            //            }
            //        }

            //        TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
            //        //  dcalculate = objBs.getsizeforworkorder("44FS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt44fs.Text) * wid;
            //                // grandfs = grandfs + Convert.ToDouble(txt44fs.Text);
            //            }
            //        }

            //        TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");
            //        //   dcalculate = objBs.getsizeforworkorder("44HS", str);
            //        if (dcalculate != null)
            //        {
            //            if (dcalculate.Tables[0].Rows.Count > 0)
            //            {
            //                double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
            //                totgndfin = totgndfin + Convert.ToDouble(txt44hs.Text) * wid;
            //                //  grandhs = grandhs + Convert.ToDouble(txt44hs.Text);
            //            }
            //        }

            //        txtreq.Text = totgndfin.ToString();





            //        int col = vLoop + 1;

            //        double meter1 = Convert.ToDouble(txtmeter.Text);
            //        double reqmeter1 = Convert.ToDouble(totgndfin);

            //        if (dparty.SelectedValue == "Select Party Name")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name in Row " + col + ". Thank you');", true);
            //            btnadd.Enabled = false;
            //            return;
            //        }
            //        else
            //        {
            //            btnadd.Enabled = true;
            //        }

            //        double number = meter1 - reqmeter1;
            //        if (number < 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter in Row " + col + ". Thank you');", true);
            //            btnadd.Enabled = false;
            //            return;
            //        }
            //        else
            //        {
            //            btnadd.Enabled = true;
            //        }
            //   int iStatus2 = objBs.insertTranscut(txtLotNo.Text, orderno.Text, lblid.Text, txtdesign.Text, dparty.SelectedValue, txtmeter.Text, txtreq.Text, txtrate.Text, txt36fs.Text, txt36hs.Text, txt38fs.Text, txt38hs.Text, txt39fs.Text, txt39hs.Text, txt40fs.Text, txt40hs.Text, txt42fs.Text, txt42hs.Text, txt44fs.Text, txt44hs.Text);

            //    }

            //    //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    //{

            //    //    TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

            //    //    Label lblid = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("lblid");

            //    //    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesigno");


            //    //    DropDownList dparty = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpparty");


            //    //    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtmet");


            //    //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

            //    //    TextBox txtreq = (TextBox)gvcustomerorder.Rows[0].FindControl("txtrmeter");






            //    //    int iStatus2 = objBs.insertTranscut(txtLotNo.Text,orderno.Text,lblid.Text,txtdesign.Text,dparty.SelectedValue,txtmeter.Text,txtreq.Text,txtrate.Text);

            //    //}





            //    //    string condno = getCond();
            //    //   string condname = getCondname();

            //    //  return;


            //    ///  iStatus = objBs.insertCuttingprocess(Convert.ToInt32(txtLotNo.Text), ddlSupplier.SelectedValue, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), txtreq_meter.Text, ddlSupplier.SelectedItem.Text, radbtn.SelectedValue, deliverydate);
            //    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");

            //}
            //else
            //{
            //    //int iStatus = 0;
            //    //if (radbtn.SelectedValue == "1")
            //    //{
            //    //    double meter = Convert.ToDouble(txtMeter.Text);
            //    //    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

            //    //    double number = meter - reqmeter;
            //    //    if (number < 0)
            //    //    {
            //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
            //    //        return;
            //    //    }


            //    //    iStatus = objBs.UpdateCuttingprocess(Convert.ToInt32(txtLotNo.Text), ddlSupplier.SelectedValue, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), Convert.ToInt32(iid), txtreq_meter.Text, ddlSupplier.SelectedItem.Text, radbtn.SelectedValue, deliverydate);
            //    //    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
            //    //}
            //    //else
            //    //{
            //    //    double meter = Convert.ToDouble(txtMeter.Text);
            //    //    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

            //    //    double number = meter - reqmeter;
            //    //    if (number < 0)
            //    //    {
            //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
            //    //        return;
            //    //    }
            //    //    string condno = getCond();
            //    //    string condname = getCondname();

            //    //    iStatus = objBs.UpdateCuttingprocess(Convert.ToInt32(txtLotNo.Text), condno, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), Convert.ToInt32(iid), txtreq_meter.Text, condname, radbtn.SelectedValue, deliverydate);
            //    //    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
            //    //}
            //}
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
        }

        protected string getCond()
        {
            string cond = "";

            //foreach (ListItem listItem in chkSupplier.Items)
            //{
            //    if (listItem.Text != "All")
            //    {
            //        if (listItem.Selected)
            //        {
            //            cond += listItem.Value + ",";
            //        }
            //    }
            //}
            //cond = cond.TrimEnd(',');
            ////   cond = cond.Replace(",", ",");
            return cond;
        }

        protected string getCondname()
        {
            string cond = "";

            //foreach (ListItem listItem in chkSupplier.Items)
            //{
            //    if (listItem.Text != "All")
            //    {
            //        if (listItem.Selected)
            //        {
            //            cond += listItem.Text + ",";
            //        }
            //    }
            //}
            //// cond = cond.TrimEnd(',');
            ////   cond = cond.Replace(",", ",");
            //cond = cond.TrimEnd(',');
            return cond;
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
        }

        protected void drpwidthChange(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();

            DataSet dsrefno = objBs.getnewsupplierforcut(drpwidth.SelectedValue);
            if (dsrefno != null)
            {
                if (dsrefno.Tables[0].Rows.Count > 0)
                {
                    chkinvno.DataSource = dsrefno.Tables[0];
                    chkinvno.DataTextField = "fabno";
                    chkinvno.DataValueField = "fabid";
                    chkinvno.DataBind();
                    //  drpwidth.Items.Insert(0, "Select Width");
                }
                else
                {
                    chkinvno.DataSource = null;
                    chkinvno.DataTextField = "fabno";
                    chkinvno.DataValueField = "fabid";
                    chkinvno.DataBind();
                    chkinvno.ClearSelection();
                    chkinvno.Items.Clear();
                }
            }
            DataSet dcheckwidth = objBs.getwidthnewprocess(drpwidth.SelectedItem.Text);
            if (dcheckwidth.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dcheckwidth.Tables[0].Rows.Count; i++)
                {
                    string size = dcheckwidth.Tables[0].Rows[i]["Sizeid"].ToString();
                    string value = dcheckwidth.Tables[0].Rows[i]["w"].ToString();
                    if (size == "3")
                    {
                        txtformal.Text = value;
                    }
                    else if (size == "4")
                    {
                        txtboys.Text = value;
                    }
                    else if (size == "5")
                    {
                        txtcasual.Text = value;
                    }

                }
            }

            //if (chkinvno.SelectedIndex >= 0)
            //{
            //    // CheckBoxList2.Enabled = true;
            //    //Loop through each item of checkboxlist
            //    foreach (ListItem item in chkinvno.Items)
            //    {
            //        //check if item selected
            //        if (item.Selected)
            //        {
            //            // Add participant to the selected list if not alreay added
            //            if (!IsParticipantExists(item.Value))
            //            {

            //            }
            //            else
            //            {
            //                dteo = objBs.getcutlistdesign(item.Value, drpwidth.SelectedValue);
            //                if (dteo != null)
            //                {
            //                    if (dteo.Tables[0].Rows.Count > 0)
            //                    {
            //                        dssmer.Merge(dteo);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    if (dssmer != null)
            //    {
            //        if (dssmer.Tables[0].Rows.Count > 0)
            //        {
            //            CheckBoxList2.DataSource = dssmer;
            //            CheckBoxList2.DataTextField = "Design";
            //            CheckBoxList2.DataValueField = "id";
            //            CheckBoxList2.DataBind();
            //        }
            //    }
            //    //Uncheck all selected items
            //    //  cbParticipants.ClearSelection();
            //}
            //else
            //{

            //    CheckBoxList2.Items.Clear();
            //    // chkinvno.Enabled = false;

            //}


            //DataSet dssmer1 = new DataSet();
            //DataSet dteo1 = new DataSet();
            //if (CheckBoxList2.SelectedIndex >= 0)
            //{

            //    int lop = 0;
            //    //Loop through each item of checkboxlist
            //    foreach (ListItem item in CheckBoxList2.Items)
            //    {
            //        //check if item selected

            //        if (item.Selected)
            //        {
            //            // Add participant to the selected list if not alreay added
            //            //if (!IsParticipantExists(item.Value))
            //            //{

            //            //}
            //            //if (lop == 1)
            //            //{
            //            //    ButtonAdd1_Click(sender, e);

            //            //}
            //            // else
            //            {
            //                dteo1 = objBs.getcutlistdesignfortrans(item.Value);
            //                if (dteo1 != null)
            //                {
            //                    if (dteo1.Tables[0].Rows.Count > 0)
            //                    {
            //                        dssmer1.Merge(dteo1);
            //                    }
            //                    lop++;
            //                }
            //            }
            //        }
            //    }
            //    gvcustomerorder.DataSource = dssmer1;
            //    gvcustomerorder.DataBind();
            //}
            //else
            //{
            //    //CheckBoxList2.Enabled = true;
            //    //chkinvno.Enabled = true;

            //    gvcustomerorder.DataSource = null;
            //    gvcustomerorder.DataBind();
            //}



        }

        protected void dddldesignchanged(object sender, EventArgs e)
        {

            double r = 0.00;
            double rr = 0.00;
            double rb = 0.00;
            double rr1 = 0.00;
            double rb1 = 0.00;
            string width = string.Empty;
            //DataSet dsFit = objBs.GetFit();
            //if (dsFit != null)
            //{
            //    if (dsFit.Tables[0].Rows.Count > 0)
            //    {

            //        drpFit.DataSource = dsFit.Tables[0];
            //        drpFit.DataTextField = "Fit";
            //        drpFit.DataValueField = "FitID";
            //        drpFit.DataBind();

            //    }
            //}
            if (radcuttype.SelectedValue == "1")
            {
                DataSet dteo = objBs.getcutlistdesignfortrans(dddldesign.SelectedValue);
                if (dteo.Tables[0].Rows.Count > 0)
                {
                    if (drpFit.SelectedValue == "0")
                    {
                        dddldesign.SelectedValue = "Select Design";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Brand Name. Thank you');", true);
                        ddlBrand.Focus();
                        return;
                    }

                    txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
                    txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                    txtReqMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                    if (radbtn.SelectedValue == "1")
                    {
                        if (Sddrrpfit.SelectedValue == "3")
                        {

                            txtavamet1.Text = txtAvailableMtr.Text;
                        }
                        else if (Sddrrpfit.SelectedValue == "4")
                        {

                            txtavamet3.Text = txtAvailableMtr.Text;
                        }
                        else if (Sddrrpfit.SelectedValue == "5")
                        {

                            txtavamet2.Text = txtAvailableMtr.Text;
                        }
                    }

                    //if (drpwidth.SelectedValue == "1")
                    //{
                    //    width = "36";
                    //}
                    //else if (drpwidth.SelectedValue == "2")
                    //{
                    //    width = "44";
                    //}
                    //else
                    //{
                    //    width = "58";
                    //}

                    //DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                    //if (dcalculate.Tables[0].Rows.Count > 0)
                    //{

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                    double wid = 0;

                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                    if (roundoff > 0.5)
                    {
                        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        r = Math.Floor(Convert.ToDouble(roundoff));
                    }

                    //  }
                    txtNoofShirts.Text = r.ToString();
                    txtReqNoShirts.Text = r.ToString();


                }
                rr = ((r * 15) / 100);
                if (rr > 0.5)
                {
                    rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb = Math.Floor(Convert.ToDouble(rr));
                }
                txtextrashirt.Text = rb.ToString();

                rr1 = ((r * 2) / 100);
                if (rr1 > 0.5)
                {
                    rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb1 = Math.Floor(Convert.ToDouble(rr1));
                }
                txtminshirt.Text = rb1.ToString();
            }
            else if (radcuttype.SelectedValue == "2")
            {
                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }


                txtNoofShirts.Text = r.ToString();
                txtReqNoShirts.Text = r.ToString();



                rr = ((r * 15) / 100);
                if (rr > 0.5)
                {
                    rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb = Math.Floor(Convert.ToDouble(rr));
                }
                txtextrashirt.Text = rb.ToString();

                rr1 = ((r * 2) / 100);
                if (rr1 > 0.5)
                {
                    rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb1 = Math.Floor(Convert.ToDouble(rr1));
                }
                txtminshirt.Text = rb1.ToString();


            }
        }
        protected void reqchanged(object sender, EventArgs e)
        {
            double r = 0.00;
            double rr = 0.00;
            double rb = 0.00;
            double rr1 = 0.00;
            double rb1 = 0.00;
            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                // double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(txtReqMtr.Text) / wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

            }
            //  txtNoofShirts.Text = r.ToString();
            txtReqNoShirts.Text = r.ToString();

            rr = ((r * 15) / 100);
            if (rr > 0.5)
            {
                rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
            }
            else
            {
                rb = Math.Floor(Convert.ToDouble(rr));
            }
            txtextrashirt.Text = rb.ToString();

            rr1 = ((r * 2) / 100);
            if (rr1 > 0.5)
            {
                rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
            }
            else
            {
                rb1 = Math.Floor(Convert.ToDouble(rr1));
            }
            txtminshirt.Text = rb1.ToString();


            if (radbtn.SelectedValue == "1")
            {
                getzeroforemptysize();

                if (Sddrrpfit.SelectedValue == "3")
                {
                    txtavamet1.Text = txtReqMtr.Text;
                    // Sddrpartyselected_changed(sender, e);

                    if (tsfs.Visible == true)
                    {
                        txt36FS.Focus();
                        if (txt36FS.Text == "0")
                        {
                            txt36FS.Text = "";
                        }
                    }
                    else if (tefs.Visible == true)
                    {
                        txt38FS.Focus();
                        if (txt38FS.Text == "0")
                        {
                            txt38FS.Text = "";
                        }
                    }
                    else if (tnfs.Visible == true)
                    {
                        txt39FS.Focus();
                        if (txt39FS.Text == "0")
                        {
                            txt39FS.Text = "";
                        }
                    }
                    else if (fzfs.Visible == true)
                    {
                        txt40FS.Focus();
                        if (txt40FS.Text == "0")
                        {
                            txt40FS.Text = "";
                        }
                    }
                    else if (ftfs.Visible == true)
                    {
                        txt42FS.Focus();
                        if (txt42FS.Text == "0")
                        {
                            txt42FS.Text = "";
                        }
                    }
                    else if (fffs.Visible == true)
                    {
                        txt44FS.Focus();
                        if (txt44FS.Text == "0")
                        {
                            txt44FS.Text = "";
                        }
                    }
                    else if (tshs.Visible == true)
                    {
                        txt36HS.Focus();
                        if (txt36HS.Text == "0")
                        {
                            txt36HS.Text = "";
                        }
                    }

                    else if (tehs.Visible == true)
                    {
                        txt38HS.Focus();
                        if (txt38HS.Text == "0")
                        {
                            txt38HS.Text = "";
                        }
                    }
                    else if (tnhs.Visible == true)
                    {
                        txt39HS.Focus();
                        if (txt39HS.Text == "0")
                        {
                            txt39HS.Text = "";
                        }
                    }
                    else if (fzhs.Visible == true)
                    {
                        txt40HS.Focus();
                        if (txt40HS.Text == "0")
                        {
                            txt40HS.Text = "";
                        }
                    }
                    else if (fths.Visible == true)
                    {
                        txt42HS.Focus();
                        if (txt42HS.Text == "0")
                        {
                            txt42HS.Text = "";
                        }
                    }
                    else if (ffhs.Visible == true)
                    {
                        txt44HS.Focus();
                        if (txt44HS.Text == "0")
                        {
                            txt44HS.Text = "";
                        }
                    }
                }
                else if (Sddrrpfit.SelectedValue == "4")
                {
                    txtavamet3.Text = txtReqMtr.Text;
                    // Sddrpartyselected_changed(sender, e);
                    if (b28fs.Visible == true)
                    {
                        txt28FS3.Focus();
                        if (txt28FS3.Text == "0")
                        {
                            txt28FS3.Text = "";
                        }
                    }
                    else if (b30fs.Visible == true)
                    {
                        txt30FS3.Focus();
                        if (txt30FS3.Text == "0")
                        {
                            txt30FS3.Text = "";
                        }
                    }
                    else if (b32fs.Visible == true)
                    {
                        txt32FS3.Focus();
                        if (txt32FS3.Text == "0")
                        {
                            txt32FS3.Text = "";
                        }
                    }
                    else if (b34fs.Visible == true)
                    {
                        txt34FS3.Focus();
                        if (txt34FS3.Text == "0")
                        {
                            txt34FS3.Text = "";
                        }
                    }
                    else if (b36fs.Visible == true)
                    {
                        txt36FS3.Focus();
                        if (txt36FS3.Text == "0")
                        {
                            txt36FS3.Text = "";
                        }
                    }
                    else if (b38fs.Visible == true)
                    {
                        txt38FS3.Focus();
                        if (txt38FS3.Text == "0")
                        {
                            txt38FS3.Text = "";
                        }
                    }
                    else if (b28hs.Visible == true)
                    {
                        txt28HS3.Focus();
                        if (txt28HS3.Text == "0")
                        {
                            txt28HS3.Text = "";
                        }
                    }
                    else if (b30hs.Visible == true)
                    {
                        txt30HS3.Focus();
                        if (txt30HS3.Text == "0")
                        {
                            txt30HS3.Text = "";
                        }
                    }

                    else if (b32hs.Visible == true)
                    {
                        txt32HS3.Focus();
                        if (txt32HS3.Text == "0")
                        {
                            txt32HS3.Text = "";
                        }
                    }
                    else if (b34hs.Visible == true)
                    {
                        txt34HS3.Focus();
                        if (txt34HS3.Text == "0")
                        {
                            txt34HS3.Text = "";
                        }
                    }
                    else if (b36hs.Visible == true)
                    {
                        txt36HS3.Focus();
                        if (txt36HS3.Text == "0")
                        {
                            txt36HS3.Text = "";
                        }
                    }
                    else if (b38hs.Visible == true)
                    {
                        txt38HS3.Focus();
                        if (txt38HS3.Text == "0")
                        {
                            txt38HS3.Text = "";
                        }
                    }

                    
                }
                else if (Sddrrpfit.SelectedValue == "5")
                {
                    txtavamet2.Text = txtReqMtr.Text;
                    // Sddrpartyselected_changed(sender, e);
                    if (sfs.Visible == true)
                    {
                        txtSFS2.Focus();
                        if (txtSFS2.Text == "0")
                        {
                            txtSFS2.Text = "";
                        }
                    }
                    else if (mfs.Visible == true)
                    {
                        txtMFS2.Focus();
                        if (txtMFS2.Text == "0")
                        {
                            txtMFS2.Text = "";
                        }
                    }
                    else if (lfs.Visible == true)
                    {
                        txtLFS2.Focus();
                        if (txtLFS2.Text == "0")
                        {
                            txtLFS2.Text = "";
                        }
                    }
                    else if (xlfs.Visible == true)
                    {
                        txtXLFS2.Focus();
                        if (txtXLFS2.Text == "0")
                        {
                            txtXLFS2.Text = "";
                        }
                    }
                    else if (xxlfs.Visible == true)
                    {
                        txtXXLFS2.Focus();
                        if (txtXXLFS2.Text == "0")
                        {
                            txtXXLFS2.Text = "";
                        }
                    }
                
                    else if (shs.Visible == true)
                    {
                        txtSHS2.Focus();
                        if (txtSHS2.Text == "0")
                        {
                            txtSHS2.Text = "";
                        }
                    }

                    else if (mhs.Visible == true)
                    {
                        txtMHS2.Focus();
                        if (txtMHS2.Text == "0")
                        {
                            txtMHS2.Text = "";
                        }
                    }
                    else if (lhs.Visible == true)
                    {
                        txtLHS2.Focus();
                        if (txtLHS2.Text == "0")
                        {
                            txtLHS2.Text = "";
                        }
                    }
                    else if (xlhs.Visible == true)
                    {
                        txtXLHS2.Focus();
                        if (txtXLHS2.Text == "0")
                        {
                            txtXLHS2.Text = "";
                        }
                    }
                    else if (xxlhs.Visible == true)
                    {
                        txtXXLHS2.Focus();
                        if (txtXXLHS2.Text == "0")
                        {
                            txtXXLHS2.Text = "";
                        }
                    }
                }
            }

        }

        protected void drpfitchanged(object sender, EventArgs e)
        {
            double r = 0.00;
            double r1 = 0.00;

            double rr = 0.00;
            double rb = 0.00;
            DataSet dteo = objBs.getcutlistdesignfortrans(dddldesign.SelectedValue);
            if (dteo.Tables[0].Rows.Count > 0)
            {
                txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
                txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                if (txtReqMtr.Text == "")
                {
                    txtReqMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                }
                else
                {

                }

                DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                    double roundoff1 = Convert.ToDouble(txtReqMtr.Text) / wid;
                    if (roundoff > 0.5)
                    {
                        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        r = Math.Floor(Convert.ToDouble(roundoff));
                    }

                    if (roundoff1 > 0.5)
                    {
                        r1 = Math.Round(Convert.ToDouble(roundoff1), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        r1 = Math.Floor(Convert.ToDouble(roundoff1));
                    }

                }
                txtNoofShirts.Text = r.ToString();
                txtReqNoShirts.Text = r1.ToString();
            }
            rr = ((r * 15) / 100);
            if (rr > 0.5)
            {
                rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
            }
            else
            {
                rb = Math.Floor(Convert.ToDouble(rr));
            }
            txtextrashirt.Text = rb.ToString();
        }
        protected void radcuttype_selectedindex(object sender, EventArgs e)
        {
            if (radcuttype.SelectedValue == "1")
            {
                dddldesign.Enabled = true;
                txtDesignRate.Enabled = true;
                txtReqMtr.Enabled = true;
                txtNoofShirts.Enabled = true;
                radchecked(sender, e);
            }
            else if (radcuttype.SelectedValue == "2")
            {
                dddldesign.Enabled = false;
                txtDesignRate.Enabled = false;
                txtReqMtr.Enabled = false;
                txtNoofShirts.Enabled = false;
                radchecked(sender, e);

            }
        }
        protected void chkinvnochanged(object sender, EventArgs e)
        {

            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            string cond = "";
            string cond1 = "";
            //  dteo = objBs.getjobcardlistdesign(CheckBoxList1.SelectedValue);



            if (chkinvno.SelectedIndex >= 0)
            {
                if (radcuttype.SelectedValue == "1")
                {
                    // CheckBoxList2.Enabled = true;
                    //Loop through each item of checkboxlist
                    foreach (ListItem item in chkinvno.Items)
                    {
                        //check if item selected
                        if (item.Selected)
                        {
                            // Add participant to the selected list if not alreay added
                            if (!IsParticipantExists(item.Value))
                            {

                            }
                            else
                            {
                                dteo = objBs.getcutlistdesign(item.Value, drpwidth.SelectedValue);
                                if (dteo != null)
                                {
                                    if (dteo.Tables[0].Rows.Count > 0)
                                    {
                                        dssmer.Merge(dteo);
                                    }
                                }
                            }
                        }
                    }
                    if (dssmer != null)
                    {
                        if (dssmer.Tables[0].Rows.Count > 0)
                        {
                            //CheckBoxList2.DataSource = dssmer;
                            //CheckBoxList2.DataTextField = "Design";
                            //CheckBoxList2.DataValueField = "id";
                            //CheckBoxList2.DataBind();

                            dddldesign.DataSource = dssmer;
                            dddldesign.DataTextField = "Design";
                            dddldesign.DataValueField = "id";
                            dddldesign.DataBind();
                            dddldesign.Items.Insert(0, "Select Design");
                            ViewState["MyDataSet"] = dssmer;
                        }
                        else
                        {
                            dddldesign.DataSource = null;
                            dddldesign.DataBind();
                            dddldesign.ClearSelection();
                            // dddldesign.Items.Insert(0, "Select Design");

                        }

                    }
                    else
                    {
                        dddldesign.DataSource = null;
                        dddldesign.DataBind();
                        dddldesign.ClearSelection();

                    }
                }
                else if (radcuttype.SelectedValue == "2")
                {

                    foreach (ListItem listItem in chkinvno.Items)
                    {
                        if (listItem.Text != "All")
                        {
                            if (listItem.Selected)
                            {
                                cond1 += " Fabid='" + listItem.Value + "' ,";
                            }
                        }
                    }
                    cond1 = cond1.TrimEnd(',');
                    cond1 = cond1.Replace(",", "or");

                    if (cond1 != "")
                    {
                        DataSet dminmax1 = objBs.getcutlistdesignforminandmaxaddition(cond1, drpwidth.SelectedValue);
                        if (dminmax1.Tables[0].Rows.Count > 0)
                        {
                            txtAvailableMtr.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                            txtReqMtr.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                            txtavamet1.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                        }
                    }
                    else
                    {
                        txtAvailableMtr.Text = "0";
                        txtReqMtr.Text = "0";
                        txtavamet1.Text = "0";
                    }

                }
                //Uncheck all selected items
                //  cbParticipants.ClearSelection();
            }
            else
            {

                dddldesign.DataSource = null;
                dddldesign.DataBind();
                dddldesign.ClearSelection();
                dddldesign.Items.Clear();
                // chkinvno.Enabled = false;

            }


            foreach (ListItem listItem in chkinvno.Items)
            {
                if (listItem.Text != "All")
                {
                    if (listItem.Selected)
                    {
                        cond += " Fabid='" + listItem.Value + "' ,";
                    }
                }
            }
            cond = cond.TrimEnd(',');
            cond = cond.Replace(",", "or");

            if (cond != "")
            {


                DataSet dminmax = objBs.getcutlistdesignforminandmax(cond, drpwidth.SelectedValue);
                if (dminmax.Tables[0].Rows.Count > 0)
                {
                    lblmin.Text = Convert.ToDouble(dminmax.Tables[0].Rows[0]["mini"]).ToString("N");
                    lblmax.Text = Convert.ToDouble(dminmax.Tables[0].Rows[0]["maxx"]).ToString("N");
                }
            }
            else
            {
                lblmin.Text = "0.00";
                lblmax.Text = "0.00";
            }
        }

        private bool IsParticipantExists(string val)
        {
            bool exists = false;
            //Loop through each item in selected participant checkboxlist
            foreach (ListItem item in chkinvno.Items)
            {
                //Check if item selected already exists in the selected participant checboxlist or not
                if (item.Value == val)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        protected void chkgridview(object sender, EventArgs e)
        {

            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            if (chckbrand.SelectedIndex >= 0)
            {

                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (ListItem item in chckbrand.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        //if (!IsParticipantExists(item.Value))
                        //{

                        //}
                        //if (lop == 1)
                        //{
                        //    ButtonAdd1_Click(sender, e);

                        //}
                        // else
                        {
                            dteo = objBs.getbrandnameforcuttprocess(item.Value);
                            if (dteo != null)
                            {
                                if (dteo.Tables[0].Rows.Count > 0)
                                {
                                    dssmer.Merge(dteo);
                                }
                                lop++;
                            }
                        }
                    }
                }

                for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                {
                    DropDownList dbrand = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrbrand");
                    if (dssmer.Tables[0].Rows.Count > 0)
                    {
                        dbrand.DataSource = dssmer.Tables[0];
                        dbrand.DataTextField = "BrandName";
                        dbrand.DataValueField = "BrandID";
                        dbrand.DataBind();
                        dbrand.Items.Insert(0, "Select Brand Name");
                    }
                }


                grdcust.DataSource = dssmer;
                grdcust.DataBind();
            }
            else
            {
                //CheckBoxList2.Enabled = true;
                //chkinvno.Enabled = true;

                grdcust.DataSource = null;
                grdcust.DataBind();
            }
        }


        protected void check2_changed(object sender, EventArgs e)
        {

            //gvcustomerorder.Columns[7].Visible = false;
            //gvcustomerorder.Columns[8].Visible = false;

            //gvcustomerorder.Columns[9].Visible = false;
            //gvcustomerorder.Columns[10].Visible = false;

            //gvcustomerorder.Columns[11].Visible = false; gvcustomerorder.Columns[12].Visible = false;

            //gvcustomerorder.Columns[13].Visible = false; gvcustomerorder.Columns[14].Visible = false;

            //gvcustomerorder.Columns[15].Visible = false; gvcustomerorder.Columns[16].Visible = false;

            //gvcustomerorder.Columns[17].Visible = false; gvcustomerorder.Columns[18].Visible = false;


            //gvcustomerorder.Columns[19].Visible = false;
            //gvcustomerorder.Columns[20].Visible = false;

            //gvcustomerorder.Columns[21].Visible = false;
            //gvcustomerorder.Columns[22].Visible = false;

            //gvcustomerorder.Columns[23].Visible = false; gvcustomerorder.Columns[24].Visible = false;

            //gvcustomerorder.Columns[25].Visible = false; gvcustomerorder.Columns[26].Visible = false;

            //gvcustomerorder.Columns[27].Visible = false; gvcustomerorder.Columns[28].Visible = false;

            //gvcustomerorder.Columns[29].Visible = false; gvcustomerorder.Columns[30].Visible = false;




            //DataSet dssmer = new DataSet();
            //DataSet dteo = new DataSet();
            //if (CheckBoxList2.SelectedIndex >= 0)
            //{

            //    int lop = 0;
            //    //Loop through each item of checkboxlist
            //    foreach (ListItem item in CheckBoxList2.Items)
            //    {
            //        //check if item selected

            //        if (item.Selected)
            //        {
            //            // Add participant to the selected list if not alreay added
            //            //if (!IsParticipantExists(item.Value))
            //            //{

            //            //}
            //            //if (lop == 1)
            //            //{
            //            //    ButtonAdd1_Click(sender, e);

            //            //}
            //            // else
            //            {
            //                dteo = objBs.getcutlistdesignfortrans(item.Value);
            //                if (dteo != null)
            //                {
            //                    if (dteo.Tables[0].Rows.Count > 0)
            //                    {
            //                        dssmer.Merge(dteo);
            //                    }
            //                    lop++;
            //                }
            //            }
            //        }
            //    }
            //    gvcustomerorder.DataSource = dssmer;
            //    gvcustomerorder.DataBind();
            //}
            //else
            //{
            //    //CheckBoxList2.Enabled = true;
            //    //chkinvno.Enabled = true;

            //    gvcustomerorder.DataSource = null;
            //    gvcustomerorder.DataBind();
            //}
        }

        protected void ddlDNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet ds_Cutting = objBs.SelectDesign_CuttingProcess(ddlDNo.SelectedValue);
            //int Width_Id = Convert.ToInt32(ds_Cutting.Tables[0].Rows[0]["WidthID"].ToString());

            //DataSet ds_Width = objBs.editwidth(Width_Id);
            //txtWidth.Text = ds_Width.Tables[0].Rows[0]["Width"].ToString();
            //txtRate.Text = ds_Cutting.Tables[0].Rows[0]["Rate"].ToString();
            //txtMeter.Text = ds_Cutting.Tables[0].Rows[0]["AvaliableMeter"].ToString();

            //txtreq_meter.Focus();
        }

        protected void Sfitchaged(object sender, EventArgs e)
        {
            if (radcuttype.SelectedValue == "1")
            {
                if (radbtn.SelectedValue == "1")
                {
                    if (Sddrrpfit.SelectedValue == "Select Fit")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit Label');", true);
                        return;
                    }
                    else
                    {

                    }

                    DataSet dsFit = objBs.GetFitforsingle(Sddrrpfit.SelectedValue);
                    if (dsFit != null)
                    {
                        if (dsFit.Tables[0].Rows.Count > 0)
                        {

                            drpFit.DataSource = dsFit.Tables[0];
                            drpFit.DataTextField = "Fit";
                            drpFit.DataValueField = "FitID";
                            drpFit.DataBind();

                        }
                    }

                }
            }
            else if (radcuttype.SelectedValue == "2")
            {
                double r = 0.00;
                double rr = 0.00;
                double rb = 0.00;
                double rr1 = 0.00;
                double rb1 = 0.00;
                string width = string.Empty;
                double wid = 0;
                DataSet dsFit = objBs.GetFitforsingle(Sddrrpfit.SelectedValue);
                if (dsFit != null)
                {
                    if (dsFit.Tables[0].Rows.Count > 0)
                    {

                        drpFit.DataSource = dsFit.Tables[0];
                        drpFit.DataTextField = "Fit";
                        drpFit.DataValueField = "FitID";
                        drpFit.DataBind();

                    }
                }
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }


                txtNoofShirts.Text = r.ToString();
                txtReqNoShirts.Text = r.ToString();



                rr = ((r * 15) / 100);
                if (rr > 0.5)
                {
                    rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb = Math.Floor(Convert.ToDouble(rr));
                }
                txtextrashirt.Text = rb.ToString();

                rr1 = ((r * 2) / 100);
                if (rr1 > 0.5)
                {
                    rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb1 = Math.Floor(Convert.ToDouble(rr1));
                }
                txtminshirt.Text = rb1.ToString();




            }

        }

        protected void supplierfill(object sender, EventArgs e)
        {
            DataSet dsFit = objBs.GetFit();
            if (dsFit != null)
            {
                if (dsFit.Tables[0].Rows.Count > 0)
                {

                    drpFit.DataSource = dsFit.Tables[0];
                    drpFit.DataTextField = "Fit";
                    drpFit.DataValueField = "FitID";
                    drpFit.DataBind();

                }
            }


            if (radbtn.SelectedValue == "1")
            {
                if (ddlSupplier.SelectedValue == "Select Party Name")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                    return;
                }
                else
                {

                }

                DataSet dledgercheck = objBs.getledgernameforcuttprocess(ddlSupplier.SelectedValue);
                if (dledgercheck.Tables[0].Rows.Count > 0)
                {
                    drpCustomer.DataSource = dledgercheck.Tables[0];
                    drpCustomer.DataTextField = "LedgerName";
                    drpCustomer.DataValueField = "LedgerID";
                    drpCustomer.DataBind();
                    //  drpCustomer.Items.Insert(0, "Select Party Name");
                }



            }
        }

        protected void brandfill(object sender, EventArgs e)
        {
            DataSet dsFit = objBs.GetFit();
            if (dsFit != null)
            {
                if (dsFit.Tables[0].Rows.Count > 0)
                {

                    drpFit.DataSource = dsFit.Tables[0];
                    drpFit.DataTextField = "Fit";
                    drpFit.DataValueField = "FitID";
                    drpFit.DataBind();
                    drpFit.Items.Insert(0, "Select Fit");

                    Sddrrpfit.DataSource = dsFit.Tables[0];
                    Sddrrpfit.DataTextField = "Fit";
                    Sddrrpfit.DataValueField = "FitID";
                    Sddrrpfit.DataBind();
                    Sddrrpfit.Items.Insert(0, "Select Fit");

                }
            }




            if (radbtn.SelectedValue == "1")
            {
                if (ddlBrand.SelectedValue == "Select Brand Name")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Brand Name');", true);
                    return;
                }
                else
                {

                }

                DataSet dbrandcheck = objBs.getbrandnameforcuttprocessnew(ddlBrand.SelectedValue);
                if (dbrandcheck.Tables[0].Rows.Count > 0)
                {
                    drpBrand.DataSource = dbrandcheck.Tables[0];
                    drpBrand.DataTextField = "BrandName";
                    drpBrand.DataValueField = "BrandID";
                    drpBrand.DataBind();
                    // drpCustomer.Items.Insert(0, "Select Party Name");
                }
                if (dbrandcheck.Tables[0].Rows.Count > 0)
                {

                    string fidit = dbrandcheck.Tables[0].Rows[0]["fitid"].ToString();
                    Sddrrpfit.SelectedValue = dbrandcheck.Tables[0].Rows[0]["fitid"].ToString();
                    drpFit.SelectedValue = dbrandcheck.Tables[0].Rows[0]["fitid"].ToString();
                    drpBrand.SelectedValue = dbrandcheck.Tables[0].Rows[0]["BrandID"].ToString();
                    DataSet dsize = objBs.Getsizetypenew(fidit);
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







                    if ((dsize.Tables[0].Rows.Count > 0))
                    {
                        //Select the checkboxlist items those values are true in database
                        //Loop through the DataTable
                        for (int i = 0; i <= dbrandcheck.Tables[0].Rows.Count - 1; i++)
                        {
                            //You need to change this as per your DB Design
                            string size = dbrandcheck.Tables[0].Rows[i]["Sizeid2"].ToString();

                            {
                                //Find the checkbox list items using FindByValue and select it.
                                chkSizes.Items.FindByValue(dbrandcheck.Tables[0].Rows[i]["Sizeid2"].ToString()).Selected = true;
                            }

                        }

                    }
                    //Formal
                    if (Sddrrpfit.SelectedValue == "3")
                    {
                        tr1.Visible = true;
                        ckhsize_index(sender, e);
                        Tr2.Visible = false;
                        Tr3.Visible = false;
                    }
                    else if (Sddrrpfit.SelectedValue == "4")
                    {
                        Tr3.Visible = true;
                        tr1.Visible = false;
                        Tr2.Visible = false;
                        ckhsize_indexforboys(sender, e);

                    }
                    else if (Sddrrpfit.SelectedValue == "5")
                    {
                        tr1.Visible = false;
                        Tr3.Visible = false;
                        Tr2.Visible = true;
                        ckhsize_indexforcasula(sender, e);

                    }
                }
                else
                {
                    ddlBrand.SelectedValue = "Select Brand Name";
                    Sddrrpfit.SelectedValue = "Select Fit";
                    drpFit.SelectedValue = "Select Fit";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Size for that Particular Brand.Thank You!!!.');", true);
                    ddlBrand.Focus();
                    return;

                }



            }
        }

        protected void radchecked(object sender, EventArgs e)
        {
            if (radcuttype.SelectedValue == "1")
            {
                btngohead.Visible = false;
                btnprocess.Visible = true;
                btnprocessall.Visible = false;

                if (radbtn.SelectedValue == "1")
                {
                    btnprocessall.Visible = false;
                    tr1.Visible = true;
                    //  tr3.Visible = false;
                    //   tr2.Visible = false;
                    tr4.Visible = false;
                    FirstGridViewRow();
                    sing.Visible = true;
                    mul.Visible = false;
                    addsingle.Visible = false;

                    //  singormulit.Visible = false;
                    DataSet dst = objBs.GetLedgersUser(1);
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            ddlSupplier.DataSource = dst.Tables[0];
                            ddlSupplier.DataTextField = "LedgerName";
                            ddlSupplier.DataValueField = "LedgerID";
                            ddlSupplier.DataBind();
                            ddlSupplier.Items.Insert(0, "Select Party Name");


                        }
                    }
                    DataSet dsDNo = objBs.getmainlabel();
                    if (dsDNo != null)
                    {
                        if (dsDNo.Tables[0].Rows.Count > 0)
                        {
                            drplab.DataSource = dsDNo.Tables[0];
                            drplab.DataTextField = "MainLabel";
                            drplab.DataValueField = "LabelID";
                            drplab.DataBind();
                            drplab.Items.Insert(0, "Select Label");


                        }
                    }

                    DataSet dsFit = objBs.GetFit();
                    if (dsFit != null)
                    {
                        if (dsFit.Tables[0].Rows.Count > 0)
                        {

                            Sddrrpfit.DataSource = dsFit.Tables[0];
                            Sddrrpfit.DataTextField = "Fit";
                            Sddrrpfit.DataValueField = "FitID";
                            Sddrrpfit.DataBind();
                            Sddrrpfit.Items.Insert(0, "Select Fit");
                        }
                    }

                }
                else
                {
                    btnprocessall.Visible = false;
                    FirstGridViewRow();
                    tr1.Visible = false;
                    //tr3.Visible = false;
                    //   tr2.Visible = false;
                    btnprocess.Enabled = true;
                    tr4.Visible = true;
                    mul.Visible = true;
                    // mulBrand.Visible = true;
                    sing.Visible = false;
                    rdSingle.Visible = false;
                    // singormulit.Visible = true;
                    DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {


                            chkcust.DataSource = dst.Tables[0];
                            chkcust.DataTextField = "LedgerName";
                            chkcust.DataValueField = "LedgerID";
                            chkcust.DataBind();

                        }
                    }

                    DataSet dstbrand = objBs.GetBrand();//GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                    if (dstbrand != null)
                    {
                        if (dstbrand.Tables[0].Rows.Count > 0)
                        {


                            chckbrand.DataSource = dstbrand.Tables[0];
                            chckbrand.DataTextField = "BrandName";
                            chckbrand.DataValueField = "BrandID";
                            chckbrand.DataBind();

                        }
                    }

                }
            }
            else if (radcuttype.SelectedValue == "2")
            {
                btngohead.Visible = true;
                btnprocess.Visible = false;
                btnprocessall.Visible = false;

                if (radbtn.SelectedValue == "1")
                {
                    // btnprocessall.Visible = true;
                    tr1.Visible = true;
                    //  tr3.Visible = false;
                    //   tr2.Visible = false;
                    tr4.Visible = false;
                    FirstGridViewRow();
                    sing.Visible = true;
                    mul.Visible = false;
                    //mulBrand.Visible = false;
                    addsingle.Visible = false;

                    //  singormulit.Visible = false;
                    DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            ddlSupplier.DataSource = dst.Tables[0];
                            ddlSupplier.DataTextField = "LedgerName";
                            ddlSupplier.DataValueField = "LedgerID";
                            ddlSupplier.DataBind();
                            ddlSupplier.Items.Insert(0, "Select Party Name");


                        }
                    }
                    DataSet dsDNo = objBs.getmainlabel();
                    if (dsDNo != null)
                    {
                        if (dsDNo.Tables[0].Rows.Count > 0)
                        {
                            drplab.DataSource = dsDNo.Tables[0];
                            drplab.DataTextField = "MainLabel";
                            drplab.DataValueField = "LabelID";
                            drplab.DataBind();
                            drplab.Items.Insert(0, "Select Label");


                        }
                    }

                    DataSet dsFit = objBs.GetFit();
                    if (dsFit != null)
                    {
                        if (dsFit.Tables[0].Rows.Count > 0)
                        {

                            Sddrrpfit.DataSource = dsFit.Tables[0];
                            Sddrrpfit.DataTextField = "Fit";
                            Sddrrpfit.DataValueField = "FitID";
                            Sddrrpfit.DataBind();
                            Sddrrpfit.Items.Insert(0, "Select Fit");
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Bulk Cutting For Multiple Party is in Process.Thank You!!!.');", true);
                    return;
                    //btnprocessall.Visible = false;
                    //FirstGridViewRow();
                    //tr1.Visible = false;
                    ////tr3.Visible = false;
                    ////   tr2.Visible = false;
                    //btnprocess.Enabled = false;
                    //tr4.Visible = true;
                    //mul.Visible = true;
                    //sing.Visible = false;
                    //rdSingle.Visible = false;
                    //// singormulit.Visible = true;
                    //DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                    //if (dst != null)
                    //{
                    //    if (dst.Tables[0].Rows.Count > 0)
                    //    {


                    //        chkcust.DataSource = dst.Tables[0];
                    //        chkcust.DataTextField = "LedgerName";
                    //        chkcust.DataValueField = "LedgerID";
                    //        chkcust.DataBind();

                    //    }
                    //}

                }
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("Partyname", typeof(string)));
            dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
            dtt.Columns.Add(new DataColumn("36FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("38FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("39FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("40FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("42FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("44FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("36HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("38HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("39HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("40HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("42HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("44HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("WSP", typeof(string)));
            dtt.Columns.Add(new DataColumn("avgsize", typeof(string)));
            dtt.Columns.Add(new DataColumn("Reqmeter", typeof(string)));
            dtt.Columns.Add(new DataColumn("Shirt", typeof(string)));


            dr = dtt.NewRow();
            dr["Partyname"] = string.Empty;
            dr["Fit"] = string.Empty;
            dr["36FS"] = string.Empty;
            dr["38FS"] = string.Empty;
            dr["39FS"] = string.Empty;
            dr["40FS"] = string.Empty;
            dr["42FS"] = string.Empty;
            dr["44FS"] = string.Empty;
            dr["36HS"] = string.Empty;
            dr["38HS"] = string.Empty;
            dr["39HS"] = string.Empty;
            dr["40HS"] = string.Empty;
            dr["42HS"] = string.Empty;
            dr["44HS"] = string.Empty;
            dr["WSP"] = string.Empty;
            dr["avgsize"] = string.Empty;
            dr["Reqmeter"] = string.Empty;
            dr["Shirt"] = string.Empty;


            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gridsize.DataSource = dtt;
            gridsize.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("Partyname");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Fit");
            dttt.Columns.Add(dct);

            dct = new DataColumn("36FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("38FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("39FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("40FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("42FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("44FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("36HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("38HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("39HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("40HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("42HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("44HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("WSP");
            dttt.Columns.Add(dct);

            dct = new DataColumn("avgsize");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Reqmeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Shirt");
            dttt.Columns.Add(dct);





            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();

            drNew["Partyname"] = "";
            drNew["Fit"] = "";
            drNew["36FS"] = 0;
            drNew["38FS"] = 0;
            drNew["39FS"] = 0;
            drNew["40FS"] = 0;
            drNew["42FS"] = 0;
            drNew["44FS"] = 0;
            drNew["36HS"] = 0;
            drNew["38HS"] = 0;
            drNew["39HS"] = 0;
            drNew["40HS"] = 0;
            drNew["42HS"] = 0;
            drNew["44HS"] = 0;
            drNew["WSP"] = 0;
            drNew["avgsize"] = 0;
            drNew["Reqmeter"] = 0;
            drNew["Shirt"] = 0;



            dstd.Tables[0].Rows.Add(drNew);

            gridsize.DataSource = dstd;
            gridsize.DataBind();


            //TextBox txn = (TextBox)grvStudentDetails.Rows[0].Cells[1].FindControl("txtName");
            //txn.Focus();
            //Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
            //Page.Form.DefaultFocus = btnAdd.ClientID;

        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gridsize_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void Grdcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsDNo = objBs.getmainlabel();

            DataSet dsFit = objBs.GetFit();



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox txtno = (TextBox)e.Row.FindControl("txtno");

                //TextBox txtttk = (TextBox)e.Row.FindControl("txtqty");
                //TextBox txttk = (TextBox)e.Row.FindControl("txtRate");
                //TextBox txtkt = (TextBox)e.Row.FindControl("txtTax");
                //TextBox txtkttt = (TextBox)e.Row.FindControl("txtAmount");
                //TextBox txtktt = (TextBox)e.Row.FindControl("txtStock");
                //TextBox txtktttt = (TextBox)e.Row.FindControl("txtDiscount");

                //txtno.Text = "1";
                //txtttk.Text = "0";
                //txttk.Text = "0";
                //txtkt.Text = "0";
                //txtkttt.Text = "0";
                //txtktt.Text = "0";
                //txtktttt.Text = "0";
                // txtno.Text = "1";


                var ddl = (DropDownList)e.Row.FindControl("drrplab");
                ddl.DataSource = dsDNo;
                ddl.DataTextField = "Mainlabel";
                ddl.DataValueField = "Labelid";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Label");


                var ddll = (DropDownList)e.Row.FindControl("ddrrpfit");
                ddll.DataSource = dsFit;
                ddll.DataTextField = "Fit";
                ddll.DataValueField = "Fitid";
                ddll.DataBind();
                ddll.Items.Insert(0, "Select Fit");



                //var ddlt = (DropDownList)e.Row.FindControl("drpfit");
                //ddlt.DataSource = dsFit;
                //ddlt.DataTextField = "Fit";
                //ddlt.DataValueField = "fitid";
                //ddlt.DataBind();
                //ddlt.Items.Insert(0, "Select Fit");




            }

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsDNo = objBs.getnewpartyforcut();

            //  DataSet dsFit = objBs.GetFit();



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox txtno = (TextBox)e.Row.FindControl("txtno");

                //TextBox txtttk = (TextBox)e.Row.FindControl("txtqty");
                //TextBox txttk = (TextBox)e.Row.FindControl("txtRate");
                //TextBox txtkt = (TextBox)e.Row.FindControl("txtTax");
                //TextBox txtkttt = (TextBox)e.Row.FindControl("txtAmount");
                //TextBox txtktt = (TextBox)e.Row.FindControl("txtStock");
                //TextBox txtktttt = (TextBox)e.Row.FindControl("txtDiscount");

                //txtno.Text = "1";
                //txtttk.Text = "0";
                //txttk.Text = "0";
                //txtkt.Text = "0";
                //txtkttt.Text = "0";
                //txtktt.Text = "0";
                //txtktttt.Text = "0";
                // txtno.Text = "1";


                //var ddl = (DropDownList)e.Row.FindControl("drpparty");
                //ddl.DataSource = dsDNo;
                //ddl.DataTextField = "LedgerName";
                //ddl.DataValueField = "Ledgerid";
                //ddl.DataBind();
                //ddl.Items.Insert(0, "Select Party Name");



                //var ddlt = (DropDownList)e.Row.FindControl("drpfit");
                //ddlt.DataSource = dsFit;
                //ddlt.DataTextField = "Fit";
                //ddlt.DataValueField = "fitid";
                //ddlt.DataBind();
                //ddlt.Items.Insert(0, "Select Fit");




            }

        }

        protected void gridsize_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dst = new DataSet();
            //  dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
            if (radbtn.SelectedValue == "1")
            {
                if (ddlSupplier.SelectedValue == "Select Party Name")
                {
                    dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                }
                else
                {

                    //    // dst = objBs.getledgernameforcuttprocess(ddlSupplier.SelectedValue);
                    dst = objBs.getledgernameforcuttprocess(ddlSupplier.SelectedValue);
                }

            }
            else
            {

                DataSet dssmer = new DataSet();
                DataSet dteo = new DataSet();
                if (chkcust.SelectedIndex >= 0)
                {

                    int lop = 0;

                    foreach (ListItem item in chkcust.Items)
                    {
                        if (item.Selected)
                        {

                            {
                                dteo = objBs.getledgernameforcuttprocess(item.Value);
                                if (dteo != null)
                                {
                                    if (dteo.Tables[0].Rows.Count > 0)
                                    {
                                        dst.Merge(dteo);
                                    }
                                    lop++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                    //return;

                }

            }
            //else
            //{

            //}
            DataSet dsFit = objBs.GetFit();

            //  DataSet dsFit = objBs.GetFit();



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt36fs = (TextBox)e.Row.FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)e.Row.FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)e.Row.FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)e.Row.FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)e.Row.FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)e.Row.FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)e.Row.FindControl("dtxttsfs");
                TextBox txt38hs = (TextBox)e.Row.FindControl("dtxttefs");
                TextBox txt39hs = (TextBox)e.Row.FindControl("dtxttnfs");
                TextBox txt40hs = (TextBox)e.Row.FindControl("dtxtfzfs");
                TextBox txt42hs = (TextBox)e.Row.FindControl("dtxtftfs");
                TextBox txt44hs = (TextBox)e.Row.FindControl("dtxtfffs");

                TextBox txtwsp = (TextBox)e.Row.FindControl("dtxtwsp");

                TextBox txtreqmeter = (TextBox)e.Row.FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)e.Row.FindControl("dtxtshirt");

                txt36fs.Text = "0";
                txt38fs.Text = "0";
                txt39fs.Text = "0";
                txt40fs.Text = "0";
                txt42fs.Text = "0";
                txt44fs.Text = "0";

                txt36hs.Text = "0";
                txt38hs.Text = "0";
                txt39hs.Text = "0";
                txt40hs.Text = "0";
                txt42hs.Text = "0";
                txt44hs.Text = "0";

                txtwsp.Text = "0";
                txtreqmeter.Text = "0";
                txtshirt.Text = "0";



                var ddl = (DropDownList)e.Row.FindControl("ddrparty");
                //   ddl.Items.Add(new ListItem("Select Party Name", "0"));
                ddl.DataSource = dst;
                ddl.DataTextField = "LedgerName";
                ddl.DataValueField = "Ledgerid";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Party Name");



                var ddlt = (DropDownList)e.Row.FindControl("ddrpfit");
                ddlt.DataSource = dsFit;
                ddlt.DataTextField = "Fit";
                ddlt.DataValueField = "fitid";
                ddlt.DataBind();
                ddlt.Items.Insert(0, "Select Fit");




            }

        }

        protected void gvcustomerorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsCategory = new DataSet();

            DataSet dscat = new DataSet();

            string OrderNo = Request.QueryString.Get("OrderNo");
            //if (OrderNo != "")
            //{
            //    /// dsCategory = objBs.GetCAT_OrderForm();
            //}
            //else
            //    dsCategory = objBs.selectcategorybrandcat(sTableName);

            DataSet dsDNo = objBs.GetDNo();

            DataSet dsFit = objBs.GetFit();



            //else
            //    dsCategory = objBs.selectcategorymaster_Dealer("tblStock_" + sTableName);


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlCategory1 = (DropDownList)(e.Row.FindControl("drpItem") as DropDownList);
                ddlCategory1.Focus();
                ddlCategory1.Enabled = true;
                ddlCategory1.DataSource = dsDNo.Tables[0];
                ddlCategory1.DataTextField = "Dno";
                ddlCategory1.DataValueField = "ProcessID";
                ddlCategory1.DataBind();
                ddlCategory1.Items.Insert(0, "Select Design");

                //DropDownList ddlDef1 = (DropDownList)(e.Row.FindControl("drpfit") as DropDownList);
                //ddlDef1.Focus();
                //ddlDef1.Enabled = true;
                //ddlDef1.DataSource = dsFit.Tables[0];
                //ddlDef1.DataTextField = "Fit";
                //ddlDef1.DataValueField = "fitid";
                //ddlDef1.DataBind();
                //ddlDef1.Items.Insert(0, "Select Fit");



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

                        DropDownList ddrpparty =
                         (DropDownList)gridsize.Rows[rowIndex].Cells[2].FindControl("ddrparty");

                        DropDownList ddrpfit =
                        (DropDownList)gridsize.Rows[rowIndex].Cells[2].FindControl("ddrpfit");

                        TextBox txt36fs =
                      (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttsfs");

                        TextBox txt38fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttefs");

                        TextBox txt39fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttnfs");

                        TextBox txt40fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfzfs");

                        TextBox txt42fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtftfs");

                        TextBox txt44fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfffs");


                        TextBox txt36hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttshs");

                        TextBox txt38hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttehs");

                        TextBox txt39hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttnhs");

                        TextBox txt40hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfzhs");

                        TextBox txt42hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfths");

                        TextBox txt44hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtffhs");


                        TextBox txtwsp =
                           (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtwsp");

                        TextBox txtreqmeter =
                         (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtreqmeter");

                        TextBox txtshirt =
                          (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtshirt");






                        ddrpparty.Items.Clear();
                        DataSet dst = new DataSet();

                        if (radbtn.SelectedValue == "1")
                        {
                            if (ddlSupplier.SelectedValue == "Select Party Name")
                            {
                                dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                            }
                            else
                            {

                                //    // dst = objBs.getledgernameforcuttprocess(ddlSupplier.SelectedValue);
                                dst = objBs.getledgernameforcuttprocess(ddlSupplier.SelectedValue);
                            }
                            ddrpparty.DataSource = dst;
                            ddrpparty.DataBind();
                            ddrpparty.DataTextField = "Ledgername";
                            ddrpparty.DataValueField = "Ledgerid";
                            ddrpparty.Items.Insert(0, "Select Party Name");

                        }
                        else
                        {
                            if (chkcust.SelectedIndex >= 0)
                            {
                            }
                            else
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                                return;

                            }
                            DataSet dssmer = new DataSet();
                            DataSet dteo = new DataSet();
                            if (chkcust.SelectedIndex >= 0)
                            {

                                int lop = 0;

                                foreach (ListItem item in chkcust.Items)
                                {
                                    if (item.Selected)
                                    {

                                        {
                                            dteo = objBs.getledgernameforcuttprocess(item.Value);
                                            if (dteo != null)
                                            {
                                                if (dteo.Tables[0].Rows.Count > 0)
                                                {
                                                    dst.Merge(dteo);
                                                }
                                                lop++;
                                            }
                                        }
                                    }
                                }
                                ddrpparty.DataSource = dst;
                                ddrpparty.DataBind();
                                ddrpparty.DataTextField = "Ledgername";
                                ddrpparty.DataValueField = "Ledgerid";
                                ddrpparty.Items.Insert(0, "Select Party Name");
                            }
                            else
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                                return;

                            }

                        }

                        //DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                        //// ddrpparty.Items.Add(new ListItem("Select Party Name", "0"));
                        //ddrpparty.DataSource = dst;
                        //ddrpparty.DataBind();
                        //ddrpparty.DataTextField = "Ledgername";
                        //ddrpparty.DataValueField = "Ledgerid";
                        //ddrpparty.Items.Insert(0, "Select Party Name");

                        ddrpfit.Items.Clear();

                        DataSet dstt = objBs.GetFit();
                        // ddrpfit.Items.Add(new ListItem("Select fit", "0"));
                        ddrpfit.DataSource = dstt;
                        ddrpfit.DataBind();
                        ddrpfit.DataTextField = "Fit";
                        ddrpfit.DataValueField = "Fitid";
                        ddrpfit.Items.Insert(0, "Select Fit");

                        ddrpparty.SelectedValue = dt.Rows[i]["Partyname"].ToString();
                        ddrpfit.SelectedValue = dt.Rows[i]["Fit"].ToString();

                        txt36fs.Text = dt.Rows[i]["36FS"].ToString();
                        txt38fs.Text = dt.Rows[i]["38FS"].ToString();
                        txt39fs.Text = dt.Rows[i]["39FS"].ToString();
                        txt40fs.Text = dt.Rows[i]["40FS"].ToString();
                        txt42fs.Text = dt.Rows[i]["42FS"].ToString();
                        txt44fs.Text = dt.Rows[i]["44FS"].ToString();

                        txt36hs.Text = dt.Rows[i]["36HS"].ToString();
                        txt38hs.Text = dt.Rows[i]["38HS"].ToString();
                        txt39hs.Text = dt.Rows[i]["39HS"].ToString();
                        txt40hs.Text = dt.Rows[i]["40HS"].ToString();
                        txt42hs.Text = dt.Rows[i]["42HS"].ToString();
                        txt44hs.Text = dt.Rows[i]["44HS"].ToString();

                        txtwsp.Text = dt.Rows[i]["WSP"].ToString();

                        txtreqmeter.Text = dt.Rows[i]["Reqmeter"].ToString();
                        txtshirt.Text = dt.Rows[i]["Shirt"].ToString();


                        rowIndex++;

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


                        DropDownList ddrpparty =
                           (DropDownList)gridsize.Rows[rowIndex].Cells[2].FindControl("ddrparty");

                        DropDownList ddrpfit =
                        (DropDownList)gridsize.Rows[rowIndex].Cells[2].FindControl("ddrpfit");

                        TextBox txt36fs =
                      (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttsfs");

                        TextBox txt38fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttefs");

                        TextBox txt39fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttnfs");

                        TextBox txt40fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfzfs");

                        TextBox txt42fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtftfs");

                        TextBox txt44fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfffs");


                        TextBox txt36hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttshs");

                        TextBox txt38hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttehs");

                        TextBox txt39hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttnhs");

                        TextBox txt40hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfzhs");

                        TextBox txt42hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfths");

                        TextBox txt44hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtffhs");




                        TextBox txtwsp =
                           (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtwsp");

                        TextBox txtreqmeter =
                       (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtreqmeter");

                        TextBox txtshirt =
                          (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtshirt");



                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Partyname"] = ddrpparty.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Fit"] = ddrpfit.Text;
                        dtCurrentTable.Rows[i - 1]["36FS"] = txt36fs.Text;
                        dtCurrentTable.Rows[i - 1]["38FS"] = txt38fs.Text;
                        dtCurrentTable.Rows[i - 1]["39FS"] = txt39fs.Text;
                        dtCurrentTable.Rows[i - 1]["40FS"] = txt40fs.Text;
                        dtCurrentTable.Rows[i - 1]["42FS"] = txt42fs.Text;
                        dtCurrentTable.Rows[i - 1]["44FS"] = txt44fs.Text;
                        dtCurrentTable.Rows[i - 1]["36HS"] = txt36hs.Text;
                        dtCurrentTable.Rows[i - 1]["38HS"] = txt38hs.Text;
                        dtCurrentTable.Rows[i - 1]["39HS"] = txt39hs.Text;
                        dtCurrentTable.Rows[i - 1]["40HS"] = txt40hs.Text;
                        dtCurrentTable.Rows[i - 1]["42HS"] = txt42hs.Text;
                        dtCurrentTable.Rows[i - 1]["44HS"] = txt44hs.Text;
                        dtCurrentTable.Rows[i - 1]["WSP"] = txtwsp.Text;
                        dtCurrentTable.Rows[i - 1]["Reqmeter"] = txtreqmeter.Text;
                        dtCurrentTable.Rows[i - 1]["Shirt"] = txtshirt.Text;



                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    gridsize.DataSource = dtCurrentTable;
                    gridsize.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //DropDownList drpCategory = (DropDownList)row.FindControl("drpItem");


            ////DropDownList Defitem = (DropDownList)row.FindControl("drpItem");

            ////DropDownList procode = (DropDownList)row.FindControl("ProductCode");


            //DataSet ds_Cutting = objBs.SelectDesign_CuttingProcess(drpCategory.SelectedValue);
            //int Width_Id = Convert.ToInt32(ds_Cutting.Tables[0].Rows[0]["WidthID"].ToString());

            //DataSet ds_Width = objBs.editwidth(Width_Id);




            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

            //        TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
            //        TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
            //        TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
            //        TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");


            //        txtwidth.Text = ds_Width.Tables[0].Rows[0]["Width"].ToString();
            //        txtrate.Text = ds_Cutting.Tables[0].Rows[0]["Rate"].ToString();
            //        txtameter.Text = ds_Cutting.Tables[0].Rows[0]["AvaliableMeter"].ToString();


            //    }
            //}
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");


                if (dparty.SelectedItem.Text == "Select Party Name")
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
                //txt36fs.Text = "0";
                //txt38fs.Text = "0";
                //txt39fs.Text = "0";
                //txt40fs.Text = "0";
                //txt42fs.Text = "0";
                //txt44fs.Text = "0";

                //txt36hs.Text = "0";
                //txt38hs.Text = "0";
                //txt39hs.Text = "0";
                //txt40hs.Text = "0";
                //txt42hs.Text = "0";
                //txt44hs.Text = "0";
                AddNewRow();

                for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                {
                    DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                    DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                    TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                    TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                    TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                    TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                    TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                    TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                    TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                    TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                    TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                    TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                    TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                    TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                    TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                    TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                    TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                    string dpar = dparty.SelectedValue;
                    string fitt = dfit.SelectedValue;
                    if (dpar == "Select Party Name")
                    {
                        txt36fs.Text = "0";
                        txt38fs.Text = "0";
                        txt39fs.Text = "0";
                        txt40fs.Text = "0";
                        txt42fs.Text = "0";
                        txt44fs.Text = "0";

                        txt36hs.Text = "0";
                        txt38hs.Text = "0";
                        txt39hs.Text = "0";
                        txt40hs.Text = "0";
                        txt42hs.Text = "0";
                        txt44hs.Text = "0";
                        txtwsp.Text = "0";
                    }
                    else
                    {

                    }
                }
            }
            else
            {

            }
            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");
                //  TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                dparty.Focus();
            }



        }

        private void AddNewRow()
        {
            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");


                int col = vLoop + 1;
                //txt36fs.Text = "0";
                //txt38fs.Text = "0";
                //txt39fs.Text = "0";
                //txt40fs.Text = "0";
                //txt42fs.Text = "0";
                //txt44fs.Text = "0";

                //txt36hs.Text = "0";
                //txt38hs.Text = "0";
                //txt39hs.Text = "0";
                //txt40hs.Text = "0";
                //txt42hs.Text = "0";
                //txt44hs.Text = "0";


                dparty.Focus();
            }

            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        DropDownList ddrpparty =
                          (DropDownList)gridsize.Rows[rowIndex].Cells[2].FindControl("ddrparty");

                        DropDownList ddrpfit =
                        (DropDownList)gridsize.Rows[rowIndex].Cells[2].FindControl("ddrpfit");

                        TextBox txt36fs =
                      (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttsfs");

                        TextBox txt38fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttefs");

                        TextBox txt39fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttnfs");

                        TextBox txt40fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfzfs");

                        TextBox txt42fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtftfs");

                        TextBox txt44fs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfffs");


                        TextBox txt36hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttshs");

                        TextBox txt38hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttehs");

                        TextBox txt39hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxttnhs");

                        TextBox txt40hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfzhs");

                        TextBox txt42hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtfths");

                        TextBox txt44hs =
                     (TextBox)gridsize.Rows[rowIndex].Cells[3].FindControl("dtxtffhs");


                        TextBox txtwsp =
                           (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtwsp");

                        TextBox txtreqmeter =
                          (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtreqmeter");

                        TextBox txtshirt =
                          (TextBox)gridsize.Rows[rowIndex].Cells[4].FindControl("dtxtshirt");


                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Partyname"] = ddrpparty.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Fit"] = ddrpfit.Text;
                        dtCurrentTable.Rows[i - 1]["36FS"] = txt36fs.Text;
                        dtCurrentTable.Rows[i - 1]["38FS"] = txt38fs.Text;
                        dtCurrentTable.Rows[i - 1]["39FS"] = txt39fs.Text;
                        dtCurrentTable.Rows[i - 1]["40FS"] = txt40fs.Text;
                        dtCurrentTable.Rows[i - 1]["42FS"] = txt42fs.Text;
                        dtCurrentTable.Rows[i - 1]["44FS"] = txt44fs.Text;
                        dtCurrentTable.Rows[i - 1]["36HS"] = txt36hs.Text;
                        dtCurrentTable.Rows[i - 1]["38HS"] = txt38hs.Text;
                        dtCurrentTable.Rows[i - 1]["39HS"] = txt39hs.Text;
                        dtCurrentTable.Rows[i - 1]["40HS"] = txt40hs.Text;
                        dtCurrentTable.Rows[i - 1]["42HS"] = txt42hs.Text;
                        dtCurrentTable.Rows[i - 1]["44HS"] = txt44hs.Text;
                        dtCurrentTable.Rows[i - 1]["WSP"] = txtwsp.Text;
                        dtCurrentTable.Rows[i - 1]["reqmeter"] = txtreqmeter.Text;
                        dtCurrentTable.Rows[i - 1]["Shirt"] = txtshirt.Text;



                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    gridsize.DataSource = dtCurrentTable;
                    gridsize.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void reqmeter(object sender, EventArgs e)
        {
            //ButtonAdd1_Click(sender, e);

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    int cnt = gvcustomerorder.Rows.Count;
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    if (vLoop >= 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            //        //    oldtxttk.Text = ".00";
            //        oldtxttk.Focus();
            //    }
            //    int tot = cnt - vLoop;
            //    if (tot == 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            //        if (oldtxttk.Text == "0.00")
            //        {
            //            oldtxttk.Text = ".00";
            //            oldtxttk.Focus();
            //        }
            //        else
            //        {
            //            oldtxttk.Focus();
            //        }
            //    }
            //    //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            //}
        }

        protected void rdSingle_CheckedChanged(object sender, EventArgs e)
        {


            if (radcuttype.SelectedValue == "1")
            {
                btngohead.Visible = false;
                btnprocess.Visible = true;
                btnprocessall.Visible = false;
                tr1.Visible = false;
                //tr2.Visible = false;
                //  tr3.Visible = false;
                //  addsingle.Visible = false;

                DataSet dsFit = objBs.GetFit();
                if (dsFit != null)
                {
                    if (dsFit.Tables[0].Rows.Count > 0)
                    {

                        drpFit.DataSource = dsFit.Tables[0];
                        drpFit.DataTextField = "Fit";
                        drpFit.DataValueField = "FitID";
                        drpFit.DataBind();

                    }
                }


                if (radbtn.SelectedValue == "1")
                {
                    if (ddlSupplier.SelectedValue == "Select Party Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                        return;
                    }
                    else
                    {

                    }

                    DataSet dledgercheck = objBs.getledgernameforcuttprocess(ddlSupplier.SelectedValue);
                    //if (dledgercheck.Tables[0].Rows.Count > 0)
                    //{
                    //    drpCustomer.DataSource = dledgercheck.Tables[0];
                    //    drpCustomer.DataTextField = "LedgerName";
                    //    drpCustomer.DataValueField = "LedgerID";
                    //    drpCustomer.DataBind();
                    //    drpCustomer.Items.Insert(0, "Select Party Name");
                    //}

                    for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                    {
                        DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");
                        if (dledgercheck.Tables[0].Rows.Count > 0)
                        {
                            dparty.DataSource = dledgercheck.Tables[0];
                            dparty.DataTextField = "LedgerName";
                            dparty.DataValueField = "LedgerID";
                            dparty.DataBind();
                            dparty.Items.Insert(0, "Select Party Name");
                        }
                    }

                }
                else
                {
                    if (chkcust.SelectedIndex >= 0)
                    {
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                        return;

                    }
                    DataSet dssmer = new DataSet();
                    DataSet dteo = new DataSet();
                    if (chkcust.SelectedIndex >= 0)
                    {

                        int lop = 0;
                        //Loop through each item of checkboxlist
                        foreach (ListItem item in chkcust.Items)
                        {
                            //check if item selected

                            if (item.Selected)
                            {
                                // Add participant to the selected list if not alreay added
                                //if (!IsParticipantExists(item.Value))
                                //{

                                //}
                                //if (lop == 1)
                                //{
                                //    ButtonAdd1_Click(sender, e);

                                //}
                                // else
                                {
                                    dteo = objBs.getledgernameforcuttprocess(item.Value);
                                    if (dteo != null)
                                    {
                                        if (dteo.Tables[0].Rows.Count > 0)
                                        {
                                            dssmer.Merge(dteo);
                                        }
                                        lop++;
                                    }
                                }
                            }
                        }
                        if (dssmer.Tables[0].Rows.Count > 0)
                        {
                            drpCustomer.DataSource = dssmer.Tables[0];
                            drpCustomer.DataTextField = "LedgerName";
                            drpCustomer.DataValueField = "LedgerID";
                            drpCustomer.DataBind();
                            drpCustomer.Items.Insert(0, "Select Party Name");
                        }
                    }
                    else
                    {



                    }


                }
            }
            else if (radcuttype.SelectedValue == "2")
            {

                btngohead.Visible = true;
                btnprocess.Visible = false;
                btnprocessall.Visible = false;
            }
        }

        protected void rdMultiple_CheckedChanged(object sender, EventArgs e)
        {

            if (radcuttype.SelectedValue == "1")
            {
                tr1.Visible = false;
                //  tr2.Visible = false;
                //  tr3.Visible = false;
                // addsingle.Visible = true;





                DataSet dsFit = objBs.GetFit();
                if (dsFit != null)
                {
                    if (dsFit.Tables[0].Rows.Count > 0)
                    {

                        drpFit.DataSource = dsFit.Tables[0];
                        drpFit.DataTextField = "Fit";
                        drpFit.DataValueField = "FitID";
                        drpFit.DataBind();


                        drpFit2.DataSource = dsFit.Tables[0];
                        drpFit2.DataTextField = "Fit";
                        drpFit2.DataValueField = "FitID";
                        drpFit2.DataBind();


                        drpFit3.DataSource = dsFit.Tables[0];
                        drpFit3.DataTextField = "Fit";
                        drpFit3.DataValueField = "FitID";
                        drpFit3.DataBind();

                    }
                }


                if (radbtn.SelectedValue == "1")
                {
                    if (ddlSupplier.SelectedValue == "Select Party Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                        return;
                    }
                    else
                    {

                    }

                    DataSet dledgercheck = objBs.getledgernameforcuttprocess(ddlSupplier.SelectedValue);
                    if (dledgercheck.Tables[0].Rows.Count > 0)
                    {
                        drpCustomer.DataSource = dledgercheck.Tables[0];
                        drpCustomer.DataTextField = "LedgerName";
                        drpCustomer.DataValueField = "LedgerID";
                        drpCustomer.DataBind();
                        drpCustomer.Items.Insert(0, "Select Party Name");
                    }
                }
                else
                {
                    if (chkcust.SelectedIndex >= 0)
                    {
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                        return;

                    }
                    DataSet dssmer = new DataSet();
                    DataSet dteo = new DataSet();
                    if (chkcust.SelectedIndex >= 0)
                    {

                        int lop = 0;
                        //Loop through each item of checkboxlist
                        foreach (ListItem item in chkcust.Items)
                        {
                            //check if item selected

                            if (item.Selected)
                            {
                                // Add participant to the selected list if not alreay added
                                //if (!IsParticipantExists(item.Value))
                                //{

                                //}
                                //if (lop == 1)
                                //{
                                //    ButtonAdd1_Click(sender, e);

                                //}
                                // else
                                {
                                    dteo = objBs.getledgernameforcuttprocess(item.Value);
                                    if (dteo != null)
                                    {
                                        if (dteo.Tables[0].Rows.Count > 0)
                                        {
                                            dssmer.Merge(dteo);
                                        }
                                        lop++;
                                    }
                                }
                            }
                        }
                        if (dssmer.Tables[0].Rows.Count > 0)
                        {
                            drpCustomer.DataSource = dssmer.Tables[0];
                            drpCustomer.DataTextField = "LedgerName";
                            drpCustomer.DataValueField = "LedgerID";
                            drpCustomer.DataBind();
                            drpCustomer.Items.Insert(0, "Select Party Name");

                            drpCustomer2.DataSource = dssmer.Tables[0];
                            drpCustomer2.DataTextField = "LedgerName";
                            drpCustomer2.DataValueField = "LedgerID";
                            drpCustomer2.DataBind();
                            drpCustomer2.Items.Insert(0, "Select Party Name");

                            drpCustomer3.DataSource = dssmer.Tables[0];
                            drpCustomer3.DataTextField = "LedgerName";
                            drpCustomer3.DataValueField = "LedgerID";
                            drpCustomer3.DataBind();
                            drpCustomer3.Items.Insert(0, "Select Party Name");
                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Name');", true);
                        return;

                    }

                }
            }
            else if (radcuttype.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Sorry Bulk Process For Multiple Party is in Process.');", true);
                return;
            }
        }
        protected void Recalclick(object sender, EventArgs e)
        {
            //double tot = 0.00;
            //double tot2 = 0.00;
            //double tot3 = 0.00;
            //double r = 0.00;
            //double tooo = 0.00;

            //string ledgerr = string.Empty;
            //string mainlab = string.Empty;

            //bool fitlab = false;
            //bool washlab = false;
            //bool logolab = false;


            //if (rdSingle.Checked == true)
            //{
            //    tot = tot + Convert.ToDouble(txt36FS.Text);
            //    tot = tot + Convert.ToDouble(txt36HS.Text);

            //    tot = tot + Convert.ToDouble(txt38FS.Text);
            //    tot = tot + Convert.ToDouble(txt38HS.Text);

            //    tot = tot + Convert.ToDouble(txt40FS.Text);
            //    tot = tot + Convert.ToDouble(txt40HS.Text);

            //    tot = tot + Convert.ToDouble(txt42FS.Text);
            //    tot = tot + Convert.ToDouble(txt42HS.Text);

            //    DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //    if (dcalculate.Tables[0].Rows.Count > 0)
            //    {

            //        double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //        double roundoff = Convert.ToDouble(tot) * wid;
            //        //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
            //        if (roundoff > 0.5)
            //        {
            //            r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //        }
            //        else
            //        {
            //            r = Math.Floor(Convert.ToDouble(roundoff));
            //        }

            //        txtavamet1.Text = r.ToString();
            //        txttotshirt1.Text = tot.ToString();

            //        //if (roundoff1 > 0.5)
            //        //{
            //        //    r1 = Math.Round(Convert.ToDouble(roundoff1), MidpointRounding.AwayFromZero);
            //        //}
            //        //else
            //        //{
            //        //    r1 = Math.Floor(Convert.ToDouble(roundoff1));
            //        //}

            //    }
            //    tooo = tot;

            //}
            //else
            //{
            //    if (drpCustomer.SelectedValue != "Select Party Name")
            //    {
            //        tot = tot + Convert.ToDouble(txt36FS.Text);
            //        tot = tot + Convert.ToDouble(txt36HS.Text);

            //        tot = tot + Convert.ToDouble(txt38FS.Text);
            //        tot = tot + Convert.ToDouble(txt38HS.Text);

            //        tot = tot + Convert.ToDouble(txt40FS.Text);
            //        tot = tot + Convert.ToDouble(txt40HS.Text);

            //        tot = tot + Convert.ToDouble(txt42FS.Text);
            //        tot = tot + Convert.ToDouble(txt42HS.Text);

            //        DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //        if (dcalculate.Tables[0].Rows.Count > 0)
            //        {

            //            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //            double roundoff = Convert.ToDouble(tot) * wid;
            //            //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
            //            if (roundoff > 0.5)
            //            {
            //                r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //            }
            //            else
            //            {
            //                r = Math.Floor(Convert.ToDouble(roundoff));
            //            }

            //            txtavamet1.Text = r.ToString();
            //            txttotshirt1.Text = tot.ToString();
            //        }


            //    }
            //    if (drpCustomer2.SelectedValue != "Select Party Name")
            //    {

            //        tot2 = tot2 + Convert.ToDouble(txt36FS2.Text);
            //        tot2 = tot2 + Convert.ToDouble(txt36HS2.Text);

            //        tot2 = tot2 + Convert.ToDouble(txt38FS2.Text);
            //        tot2 = tot2 + Convert.ToDouble(txt38HS2.Text);

            //        tot2 = tot2 + Convert.ToDouble(txt40FS2.Text);
            //        tot2 = tot2 + Convert.ToDouble(txt40HS2.Text);

            //        tot2 = tot2 + Convert.ToDouble(txt42FS2.Text);
            //        tot2 = tot2 + Convert.ToDouble(txt42HS2.Text);

            //        DataSet dcalculate = objBs.getsizeforcutt(drpFit2.SelectedValue, drpwidth.SelectedItem.Text);
            //        if (dcalculate.Tables[0].Rows.Count > 0)
            //        {

            //            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //            double roundoff = Convert.ToDouble(tot2) * wid;
            //            //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
            //            if (roundoff > 0.5)
            //            {
            //                r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //            }
            //            else
            //            {
            //                r = Math.Floor(Convert.ToDouble(roundoff));
            //            }

            //            txtavamet2.Text = r.ToString();
            //            txttotshirt2.Text = tot2.ToString();

            //        }



            //    }
            //    if (drpCustomer3.SelectedValue != "Select Party Name")
            //    {

            //        tot3 = tot3 + Convert.ToDouble(txt36FS3.Text);
            //        tot3 = tot3 + Convert.ToDouble(txt36HS3.Text);

            //        tot3 = tot3 + Convert.ToDouble(txt38FS3.Text);
            //        tot3 = tot3 + Convert.ToDouble(txt38HS3.Text);

            //        tot3 = tot3 + Convert.ToDouble(txt40FS3.Text);
            //        tot3 = tot3 + Convert.ToDouble(txt40HS3.Text);

            //        tot3 = tot3 + Convert.ToDouble(txt42FS3.Text);
            //        tot3 = tot3 + Convert.ToDouble(txt42HS3.Text);
            //        DataSet dcalculate = objBs.getsizeforcutt(drpFit3.SelectedValue, drpwidth.SelectedItem.Text);
            //        if (dcalculate.Tables[0].Rows.Count > 0)
            //        {

            //            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //            double roundoff = Convert.ToDouble(tot3) * wid;
            //            //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
            //            if (roundoff > 0.5)
            //            {
            //                r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //            }
            //            else
            //            {
            //                r = Math.Floor(Convert.ToDouble(roundoff));
            //            }

            //            txtavamet3.Text = r.ToString();
            //            txttotshirt3.Text = tot3.ToString();



            //        }
            //    }

            //    tooo = tot + tot2 + tot3;


            //}
            //if (tooo > Convert.ToDouble(txtReqNoShirts.Text))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    // btnadd.Enabled = true;
            //    btnprocess.Enabled = true;
            //    // return;
            //}
        }
        protected void callcclick(object sender, EventArgs e)
        {

            //remaingmeter * givenshirts / No.of.shirts
            btnprocess.Enabled = false;
            //int cou = gridsize.Rows.Count;
            //double etrmtr = Convert.ToDouble(txtremameter.Text) / Convert.ToDouble(cou);

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");


                txtreqmeter.Text = (Convert.ToDouble(txtreqmeter.Text) + (Convert.ToDouble(txtremameter.Text) * Convert.ToDouble(txtshirt.Text)) / Convert.ToDouble(txtReqNoShirts.Text)).ToString("0.00");

                txtavggsize.Text = (Convert.ToDouble(txtreqmeter.Text) / Convert.ToDouble(txtshirt.Text)).ToString("0.00");


                btnprocess.Enabled = true;
            }

            txtremameter.Text = "0";

        }
        protected void processclickall(object sender, EventArgs e)
        {

            if (txtadjmeter.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //  btnadd.Enabled = false;
                btnprocessall.Enabled = false;
                return;
            }
            else
            {
                // btnadd.Enabled = false;
                btnprocessall.Enabled = true;
            }

            DataSet dddesgin = new DataSet();
            double tot = 0.00;
            double originalreqq = 0.00;
            double tot2 = 0.00;
            double tot3 = 0.00;
            double r = 0.00;
            double tooo = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            string ledgerr = string.Empty;
            string mainlab = string.Empty;

            bool fitlab = false;
            bool washlab = false;
            bool logolab = false;

            //double r1 = 0.00;
            //double rr = 0.00;
            //double rb = 0.00;
            //double rr1 = 0.00;
            //double rb1 = 0.00;
            //string width = string.Empty;
            //   double reqq = 0.00;

            if (radbtn.SelectedValue == "1")
            {
                dddesgin = (DataSet)ViewState["MyDataSet"];
                if (dddesgin.Tables[0].Rows.Count > 0)
                {
                    originalreqq = Convert.ToDouble(txtReqMtr.Text);
                    for (int i = 0; i < dddesgin.Tables[0].Rows.Count; i++)
                    {
                        double reqq = 0.00;
                        double reqq1 = 0.000;
                        dddldesign.SelectedValue = dddesgin.Tables[0].Rows[i]["id"].ToString();

                        reqq = originalreqq;
                        reqq1 = originalreqq + Convert.ToDouble(txtadjmeter.Text);
                        //   reqq = reqq + 3;

                        //Get Desgin Number
                        double r1 = 0.00;
                        double rr = 0.00;
                        double rb = 0.00;
                        double rr1 = 0.00;
                        double rb1 = 0.00;
                        string width = string.Empty;


                        DataSet dteo = objBs.getcutlistdesignfortrans(dddldesign.SelectedValue);
                        if (dteo.Tables[0].Rows.Count > 0)
                        {
                            txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
                            txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                            if (reqq == Convert.ToDouble(txtAvailableMtr.Text))
                            {

                            }
                            else if ((Convert.ToDouble(txtAvailableMtr.Text)) >= reqq && (Convert.ToDouble(txtAvailableMtr.Text)) <= reqq1)
                            {
                                reqq = (Convert.ToDouble(txtAvailableMtr.Text));
                            }
                            else if (reqq >= (Convert.ToDouble(txtAvailableMtr.Text)))
                            {
                                reqq = (Convert.ToDouble(txtAvailableMtr.Text));
                            }
                            else
                            {
                                reqq = originalreqq;
                            }
                            //if (reqq >= Convert.ToDouble(txtAvailableMtr.Text) && reqq <= Convert.ToDouble(txtAvailableMtr.Text))
                            //{
                            //    return;
                            //}


                            //  return;
                            txtReqMtr.Text = reqq.ToString("N");
                            if (radbtn.SelectedValue == "1")
                            {
                                txtavamet1.Text = reqq.ToString();
                            }

                            //     DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                            //     if (dcalculate.Tables[0].Rows.Count > 0)
                            {

                                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                                double wid = 0;
                                if (drpFit.SelectedValue == "3")
                                {
                                    wid = Convert.ToDouble(txtformal.Text);
                                }
                                else if (drpFit.SelectedValue == "4")
                                {
                                    wid = Convert.ToDouble(txtboys.Text);
                                }
                                else if (drpFit.SelectedValue == "5")
                                {
                                    wid = Convert.ToDouble(txtcasual.Text);
                                }

                                double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                                if (roundoff > 0.5)
                                {
                                    r1 = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    r1 = Math.Floor(Convert.ToDouble(roundoff));
                                }

                            }
                            txtNoofShirts.Text = r1.ToString();
                            txtReqNoShirts.Text = r1.ToString();


                        }
                        rr = ((r1 * 15) / 100);
                        if (rr > 0.5)
                        {
                            rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            rb = Math.Floor(Convert.ToDouble(rr));
                        }
                        txtextrashirt.Text = rb.ToString();

                        rr1 = ((r1 * 2) / 100);
                        if (rr1 > 0.5)
                        {
                            rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            rb1 = Math.Floor(Convert.ToDouble(rr1));
                        }
                        txtminshirt.Text = rb1.ToString();


                        if (txt36FS.Text == "")
                        {
                            txt36FS.Text = "0";
                        }
                        if (txt36HS.Text == "")
                        {
                            txt36HS.Text = "0";
                        }

                        if (txt38FS.Text == "")
                        {
                            txt38FS.Text = "0";
                        }
                        if (txt38HS.Text == "")
                        {
                            txt38HS.Text = "0";
                        }
                        if (txt39FS.Text == "")
                        {
                            txt39FS.Text = "0";
                        }
                        if (txt39HS.Text == "")
                        {
                            txt39HS.Text = "0";
                        }
                        if (txt40FS.Text == "")
                        {
                            txt40FS.Text = "0";
                        }
                        if (txt40HS.Text == "")
                        {
                            txt40HS.Text = "0";
                        }
                        if (txt42FS.Text == "")
                        {
                            txt42FS.Text = "0";
                        }
                        if (txt42HS.Text == "")
                        {
                            txt42HS.Text = "0";
                        }
                        if (txt44FS.Text == "")
                        {
                            txt44FS.Text = "0";
                        }
                        if (txt44HS.Text == "")
                        {
                            txt44HS.Text = "0";
                        }



                        if (radbtn.SelectedValue == "1")
                        {
                            if (Sddrrpfit.SelectedValue == "3")
                            {




                                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                                Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);

                                txtavvgmeter.Text = Convert.ToDouble(reqq / tot).ToString("N");
                            }
                            else if (Sddrrpfit.SelectedValue == "4")
                            {
                                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) +
                             Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text);
                                txtavvgmeter.Text = Convert.ToDouble(reqq / tot).ToString("N");
                            }
                            else if (Sddrrpfit.SelectedValue == "5")
                            {
                                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) +
                             Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text);
                                txtavvgmeter.Text = Convert.ToDouble(reqq / tot).ToString("N");
                            }

                            if (txtavvgmeter.Text == "Infinity")
                            {
                                txtavvgmeter.Text = "0";
                            }

                            gndtot = gndtot + tot;



                            //     DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                            //    if (dcalculate.Tables[0].Rows.Count > 0)
                            {

                                double wid = 0;
                                if (drpFit.SelectedValue == "3")
                                {
                                    wid = Convert.ToDouble(txtformal.Text);
                                }
                                else if (drpFit.SelectedValue == "4")
                                {
                                    wid = Convert.ToDouble(txtboys.Text);
                                }
                                else if (drpFit.SelectedValue == "5")
                                {
                                    wid = Convert.ToDouble(txtcasual.Text);
                                }

                                double roundoff = Convert.ToDouble(tot) * wid;

                                r = roundoff;

                                // txtavamet1.Text = r.ToString();
                                txttotshirt1.Text = tot.ToString();
                            }

                            txt38FS.Focus();
                            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
                            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");
                        }
                        decimal dAmt = 0; decimal dTotal = 0;
                        dCrt = (DataTable)ViewState["Data"];
                        //if (originalreqq > (Convert.ToDouble(txtAvailableMtr.Text)))
                        //{
                        //}
                        //else
                        {
                            if (dCrt.Rows.Count == 0)
                            {
                                if (tr1.Visible == true)
                                {
                                    if (drpCustomer.SelectedValue == "Select Party Name")
                                    {
                                    }
                                    else
                                    {
                                        DataRow dr = dCrt.NewRow();

                                        dr["Transid"] = dddldesign.SelectedValue;
                                        dr["Design"] = dddldesign.SelectedItem.Text;
                                        dr["Rate"] = txtDesignRate.Text;

                                        dr["meter"] = txtAvailableMtr.Text;
                                        // dr["Shirt"] = txtNoofShirts.Text;
                                        dr["Shirt"] = txtNoofShirts.Text;
                                        dr["reqmeter"] = txtavamet1.Text;

                                        dr["reqshirt"] = txttotshirt1.Text;

                                        dr["brandid"] = drpBrand.SelectedValue;
                                        dr["brand"] = drpBrand.SelectedItem.Text;

                                        dr["ledgerid"] = drpCustomer.SelectedValue;
                                        dr["party"] = drpCustomer.SelectedItem.Text;
                                        dr["Fitid"] = drpFit.SelectedValue;
                                        dr["Fit"] = drpFit.SelectedItem.Text;

                                        if (Sddrrpfit.SelectedValue == "3")
                                        {

                                            dr["TSFS"] = txt36FS.Text;
                                            dr["TSHS"] = txt36HS.Text;

                                            dr["TEFS"] = txt38FS.Text;
                                            dr["TEHS"] = txt38HS.Text;

                                            dr["TNFS"] = txt39FS.Text;
                                            dr["TNHS"] = txt39HS.Text;

                                            dr["FZFS"] = txt40FS.Text;
                                            dr["FZHS"] = txt40HS.Text;

                                            dr["FTFS"] = txt42FS.Text;
                                            dr["FTHS"] = txt42HS.Text;

                                            dr["FFFS"] = txt44FS.Text;
                                            dr["FFHS"] = txt44HS.Text;

                                        }
                                        else if (Sddrrpfit.SelectedValue == "4")
                                        {

                                            dr["TSFS"] = txt28FS3.Text;
                                            dr["TSHS"] = txt28HS3.Text;

                                            dr["TEFS"] = txt30FS3.Text;
                                            dr["TEHS"] = txt30HS3.Text;

                                            dr["TNFS"] = txt32FS3.Text;
                                            dr["TNHS"] = txt32HS3.Text;

                                            dr["FZFS"] = txt34FS3.Text;
                                            dr["FZHS"] = txt34HS3.Text;

                                            dr["FTFS"] = txt36FS3.Text;
                                            dr["FTHS"] = txt36HS3.Text;

                                            dr["FFFS"] = "0";
                                            dr["FFHS"] = "0";

                                        }
                                        else if (Sddrrpfit.SelectedValue == "5")
                                        {

                                            dr["TSFS"] = txtSFS2.Text;
                                            dr["TSHS"] = txtSHS2.Text;

                                            dr["TEFS"] = txtMFS2.Text;
                                            dr["TEHS"] = txtMHS2.Text;

                                            dr["TNFS"] = "0";
                                            dr["TNHS"] = "0";

                                            dr["FZFS"] = txtLFS2.Text;
                                            dr["FZHS"] = txtLHS2.Text;

                                            dr["FTFS"] = txtXLFS2.Text;
                                            dr["FTHS"] = txtXLHS2.Text;

                                            dr["FFFS"] = txtXXLFS2.Text;
                                            dr["FFHS"] = txtXXLHS2.Text;

                                        }
                                        dr["avgsize"] = txtavvgmeter.Text;

                                        dr["WSP"] = Stxtwsp.Text;
                                        dr["Extra"] = txtextrashirt.Text;

                                        if (radbtn.SelectedValue == "1")
                                        {
                                            ledgerr = ddlSupplier.SelectedValue;
                                            mainlab = drpFit.SelectedValue;
                                            fitlab = chkfit.Checked;
                                            washlab = Chkwash.Checked;
                                            logolab = Chllogo.Checked;
                                        }

                                        dr["LLedger"] = ledgerr;
                                        dr["Mainlab"] = mainlab;
                                        dr["FItLab"] = fitlab;
                                        dr["Washlab"] = washlab;
                                        dr["Logolab"] = logolab;



                                        dCrt.Rows.Add(dr);
                                    }
                                }
                            }
                            else
                            {
                                // if (tr1.Visible == true)
                                // {
                                if (drpCustomer.SelectedValue == "Select Party Name")
                                {
                                }
                                else
                                {
                                    DataRow dr = dCrt.NewRow();

                                    dr["Transid"] = dddldesign.SelectedValue;
                                    dr["Design"] = dddldesign.SelectedItem.Text;
                                    dr["Rate"] = txtDesignRate.Text;

                                    dr["meter"] = txtAvailableMtr.Text;
                                    dr["Shirt"] = txtNoofShirts.Text;
                                    dr["reqmeter"] = txtavamet1.Text;

                                    dr["reqshirt"] = txttotshirt1.Text;

                                    dr["brandid"] = drpBrand.SelectedValue;
                                    dr["brand"] = drpBrand.SelectedItem.Text;

                                    dr["ledgerid"] = drpCustomer.SelectedValue;
                                    dr["party"] = drpCustomer.SelectedItem.Text;
                                    dr["Fitid"] = drpFit.SelectedValue;
                                    dr["Fit"] = drpFit.SelectedItem.Text;

                                    if (Sddrrpfit.SelectedValue == "3")
                                    {

                                        dr["TSFS"] = txt36FS.Text;
                                        dr["TSHS"] = txt36HS.Text;

                                        dr["TEFS"] = txt38FS.Text;
                                        dr["TEHS"] = txt38HS.Text;

                                        dr["TNFS"] = txt39FS.Text;
                                        dr["TNHS"] = txt39HS.Text;

                                        dr["FZFS"] = txt40FS.Text;
                                        dr["FZHS"] = txt40HS.Text;

                                        dr["FTFS"] = txt42FS.Text;
                                        dr["FTHS"] = txt42HS.Text;

                                        dr["FFFS"] = txt44FS.Text;
                                        dr["FFHS"] = txt44HS.Text;

                                    }
                                    else if (Sddrrpfit.SelectedValue == "4")
                                    {

                                        dr["TSFS"] = txt28FS3.Text;
                                        dr["TSHS"] = txt28HS3.Text;

                                        dr["TEFS"] = txt30FS3.Text;
                                        dr["TEHS"] = txt30HS3.Text;

                                        dr["TNFS"] = txt32FS3.Text;
                                        dr["TNHS"] = txt32HS3.Text;

                                        dr["FZFS"] = txt34FS3.Text;
                                        dr["FZHS"] = txt34HS3.Text;

                                        dr["FTFS"] = txt36FS3.Text;
                                        dr["FTHS"] = txt36HS3.Text;

                                        dr["FFFS"] = "0";
                                        dr["FFHS"] = "0";

                                    }
                                    else if (Sddrrpfit.SelectedValue == "5")
                                    {

                                        dr["TSFS"] = txtSFS2.Text;
                                        dr["TSHS"] = txtSHS2.Text;

                                        dr["TEFS"] = txtMFS2.Text;
                                        dr["TEHS"] = txtMHS2.Text;

                                        dr["TNFS"] = "0";
                                        dr["TNHS"] = "0";

                                        dr["FZFS"] = txtLFS2.Text;
                                        dr["FZHS"] = txtLHS2.Text;

                                        dr["FTFS"] = txtXLFS2.Text;
                                        dr["FTHS"] = txtXLHS2.Text;

                                        dr["FFFS"] = txtXXLFS2.Text;
                                        dr["FFHS"] = txtXXLHS2.Text;

                                    }

                                    dr["WSP"] = Stxtwsp.Text;
                                    dr["avgsize"] = txtavvgmeter.Text;
                                    dr["Extra"] = txtextrashirt.Text;

                                    if (radbtn.SelectedValue == "1")
                                    {
                                        ledgerr = ddlSupplier.SelectedValue;
                                        mainlab = drpFit.SelectedValue;
                                        fitlab = chkfit.Checked;
                                        washlab = Chkwash.Checked;
                                        logolab = Chllogo.Checked;
                                    }
                                    else
                                    {

                                    }
                                    dr["LLedger"] = ledgerr;
                                    dr["Mainlab"] = mainlab;
                                    dr["FItLab"] = fitlab;
                                    dr["Washlab"] = washlab;
                                    dr["Logolab"] = logolab;



                                    dCrt.Rows.Add(dr);
                                }
                                //  }
                            }
                            if (Sddrrpfit.SelectedValue == "3")
                            {
                                gvcustomerorder.DataSource = dCrt;
                                gvcustomerorder.DataBind();
                                panelformal.Visible = true;

                            }
                            else if (Sddrrpfit.SelectedValue == "4")
                            {
                                GridView2.DataSource = dCrt;
                                GridView2.DataBind();
                                panelboys.Visible = true;
                            }
                            else if (Sddrrpfit.SelectedValue == "5")
                            {
                                GridView1.DataSource = dCrt;
                                GridView1.DataBind();
                                panelcasual.Visible = true;
                            }
                            //   string idd = myDS.Tables[0].Rows[j]["id"].ToString();


                            dddldesign.Items.Remove(dddldesign.Items.FindByValue(dddldesign.SelectedValue));
                            // dddldesign.Items.Remove(dddldesign.Items[i]);




                            dddldesign.ClearSelection();
                            //txtDesignRate.Text = "";
                            //txtAvailableMtr.Text = "";
                            //txtNoofShirts.Text = "";
                            //txtReqMtr.Text = "";
                            //txtReqNoShirts.Text = "";
                            //txtextrashirt.Text = "";

                            //drpCustomer.ClearSelection();
                            //drpFit.ClearSelection();
                            //txt36FS.Text = "0";
                            //txt36HS.Text = "0";

                            //txt38FS.Text = "0";
                            //txt38HS.Text = "0";

                            //txt40FS.Text = "0";
                            //txt40HS.Text = "0";

                            //txt42FS.Text = "0";
                            //txt42HS.Text = "0";

                            //txt44FS.Text = "0";
                            //txt44HS.Text = "0";

                            //txt39FS.Text = "0";
                            //txt39HS.Text = "0";

                            //txtavamet1.Text = "0";
                            //txttotshirt1.Text = "0";
                            //txtavvgmeter.Text = "0";

                            // FirstGridViewRow();
                            // removedropdownlist();
                            dddldesign.Focus();
                            btnadd.Enabled = true;
                        }

                    }
                }
            }
            System.Threading.Thread.Sleep(3000);
        }


        protected void processclickallnew(object sender, EventArgs e)
        {

            if (txtadjmeter.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                btnprocessall.Enabled = false;
                return;
            }
            else
            {
                btnprocessall.Enabled = false;
            }

            DataSet dddesgin = new DataSet();
            double tot = 0.00;
            double originalreqq = 0.00;
            double tot2 = 0.00;
            double tot3 = 0.00;
            double r = 0.00;
            double tooo = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            string ledgerr = string.Empty;
            string mainlab = string.Empty;

            bool fitlab = false;
            bool washlab = false;
            bool logolab = false;
            if (radbtn.SelectedValue == "1")
            {
                dddesgin = (DataSet)ViewState["MyDataSet"];
                if (dddesgin.Tables[0].Rows.Count > 0)
                {
                    originalreqq = Convert.ToDouble(txtReqMtr.Text);
                    for (int i = 0; i < dddesgin.Tables[0].Rows.Count; i++)
                    {
                        double reqq = 0.00;
                        double reqq1 = 0.000;
                        dddldesign.SelectedValue = dddesgin.Tables[0].Rows[i]["id"].ToString();

                        reqq = originalreqq;
                        reqq1 = originalreqq + Convert.ToDouble(txtadjmeter.Text);

                        double r1 = 0.00;
                        double rr = 0.00;
                        double rb = 0.00;
                        double rr1 = 0.00;
                        double rb1 = 0.00;
                        string width = string.Empty;


                        DataSet dteo = objBs.getcutlistdesignfortrans(dddldesign.SelectedValue);
                        if (dteo.Tables[0].Rows.Count > 0)
                        {
                            txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
                            txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                            if (reqq == Convert.ToDouble(txtAvailableMtr.Text))
                            {

                            }
                            else if ((Convert.ToDouble(txtAvailableMtr.Text)) >= reqq && (Convert.ToDouble(txtAvailableMtr.Text)) <= reqq1)
                            {
                                reqq = (Convert.ToDouble(txtAvailableMtr.Text));
                            }
                            else if (reqq >= (Convert.ToDouble(txtAvailableMtr.Text)))
                            {
                                reqq = (Convert.ToDouble(txtAvailableMtr.Text));
                            }
                            else
                            {
                                reqq = originalreqq;
                            }

                            txtReqMtr.Text = reqq.ToString("N");
                            if (radbtn.SelectedValue == "1")
                            {
                                txtavamet1.Text = reqq.ToString();
                            }

                            //DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                            //if (dcalculate.Tables[0].Rows.Count > 0)
                            //{

                            //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                            double wid = 0;
                            if (drpFit.SelectedValue == "3")
                            {
                                wid = Convert.ToDouble(txtformal.Text);
                            }
                            else if (drpFit.SelectedValue == "4")
                            {
                                wid = Convert.ToDouble(txtboys.Text);
                            }
                            else if (drpFit.SelectedValue == "5")
                            {
                                wid = Convert.ToDouble(txtcasual.Text);
                            }

                            double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                            if (roundoff > 0.5)
                            {
                                r1 = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                r1 = Math.Floor(Convert.ToDouble(roundoff));
                            }

                            //   }
                            txtNoofShirts.Text = r1.ToString();
                            txtReqNoShirts.Text = r1.ToString();


                        }
                        rr = ((r1 * 15) / 100);
                        if (rr > 0.5)
                        {
                            rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            rb = Math.Floor(Convert.ToDouble(rr));
                        }
                        txtextrashirt.Text = rb.ToString();

                        rr1 = ((r1 * 2) / 100);
                        if (rr1 > 0.5)
                        {
                            rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            rb1 = Math.Floor(Convert.ToDouble(rr1));
                        }
                        txtminshirt.Text = rb1.ToString();

                        if (radbtn.SelectedValue == "1")
                        {
                            tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                            Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);

                            txtavvgmeter.Text = Convert.ToDouble(reqq / tot).ToString("N");

                            gndtot = gndtot + tot;



                            //DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                            //if (dcalculate.Tables[0].Rows.Count > 0)
                            //{

                            double wid = 0;
                            if (drpFit.SelectedValue == "3")
                            {
                                wid = Convert.ToDouble(txtformal.Text);
                            }
                            else if (drpFit.SelectedValue == "4")
                            {
                                wid = Convert.ToDouble(txtboys.Text);
                            }
                            else if (drpFit.SelectedValue == "5")
                            {
                                wid = Convert.ToDouble(txtcasual.Text);
                            }

                            double roundoff = Convert.ToDouble(tot) * wid;

                            r = roundoff;


                            txttotshirt1.Text = tot.ToString();
                            // }

                            txt38FS.Focus();
                            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
                            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");
                        }
                        decimal dAmt = 0; decimal dTotal = 0;
                        dCrt = (DataTable)ViewState["Data"];
                        {
                            if (dCrt.Rows.Count == 0)
                            {
                                if (tr1.Visible == true)
                                {
                                    if (drpCustomer.SelectedValue == "Select Party Name")
                                    {
                                    }
                                    else
                                    {
                                        DataRow dr = dCrt.NewRow();

                                        dr["Transid"] = dddldesign.SelectedValue;
                                        dr["Design"] = dddldesign.SelectedItem.Text;
                                        dr["Rate"] = txtDesignRate.Text;

                                        dr["meter"] = txtAvailableMtr.Text;
                                        dr["Shirt"] = txtNoofShirts.Text;
                                        dr["reqmeter"] = txtAvailableMtr.Text;

                                        dr["reqshirt"] = txttotshirt1.Text;

                                        dr["brandid"] = drpBrand.SelectedValue;
                                        dr["brand"] = drpBrand.SelectedItem.Text;

                                        dr["ledgerid"] = drpCustomer.SelectedValue;
                                        dr["party"] = drpCustomer.SelectedItem.Text;
                                        dr["Fitid"] = drpFit.SelectedValue;
                                        dr["Fit"] = drpFit.SelectedItem.Text;

                                        dr["TSFS"] = txt36FS.Text;
                                        dr["TSHS"] = txt36HS.Text;

                                        dr["TEFS"] = txt38FS.Text;
                                        dr["TEHS"] = txt38HS.Text;

                                        dr["TNFS"] = txt39FS.Text;
                                        dr["TNHS"] = txt39HS.Text;

                                        dr["FZFS"] = txt40FS.Text;
                                        dr["FZHS"] = txt40HS.Text;

                                        dr["FTFS"] = txt42FS.Text;
                                        dr["FTHS"] = txt42HS.Text;

                                        dr["FFFS"] = txt44FS.Text;
                                        dr["FFHS"] = txt44HS.Text;

                                        dr["avgsize"] = txtavvgmeter.Text;

                                        dr["WSP"] = Stxtwsp.Text;
                                        dr["Extra"] = txtextrashirt.Text;

                                        if (radbtn.SelectedValue == "1")
                                        {
                                            ledgerr = ddlSupplier.SelectedValue;
                                            mainlab = drplab.SelectedValue;
                                            fitlab = chkfit.Checked;
                                            washlab = Chkwash.Checked;
                                            logolab = Chllogo.Checked;
                                        }

                                        dr["LLedger"] = ledgerr;
                                        dr["Mainlab"] = mainlab;
                                        dr["FItLab"] = fitlab;
                                        dr["Washlab"] = washlab;
                                        dr["Logolab"] = logolab;



                                        dCrt.Rows.Add(dr);
                                    }
                                }
                            }
                            else
                            {
                                if (tr1.Visible == true)
                                {
                                    if (drpCustomer.SelectedValue == "Select Party Name")
                                    {
                                    }
                                    else
                                    {
                                        DataRow dr = dCrt.NewRow();

                                        dr["Transid"] = dddldesign.SelectedValue;
                                        dr["Design"] = dddldesign.SelectedItem.Text;
                                        dr["Rate"] = txtDesignRate.Text;

                                        dr["meter"] = txtAvailableMtr.Text;
                                        dr["Shirt"] = txtNoofShirts.Text;
                                        dr["reqmeter"] = txtAvailableMtr.Text;

                                        dr["reqshirt"] = txttotshirt1.Text;

                                        dr["brandid"] = drpBrand.SelectedValue;
                                        dr["brand"] = drpBrand.SelectedItem.Text;

                                        dr["ledgerid"] = drpCustomer.SelectedValue;
                                        dr["party"] = drpCustomer.SelectedItem.Text;
                                        dr["Fitid"] = drpFit.SelectedValue;
                                        dr["Fit"] = drpFit.SelectedItem.Text;

                                        dr["TSFS"] = txt36FS.Text;
                                        dr["TSHS"] = txt36HS.Text;

                                        dr["TEFS"] = txt38FS.Text;
                                        dr["TEHS"] = txt38HS.Text;

                                        dr["TNFS"] = txt39FS.Text;
                                        dr["TNHS"] = txt39HS.Text;

                                        dr["FZFS"] = txt40FS.Text;
                                        dr["FZHS"] = txt40HS.Text;

                                        dr["FTFS"] = txt42FS.Text;
                                        dr["FTHS"] = txt42HS.Text;

                                        dr["FFFS"] = txt44FS.Text;
                                        dr["FFHS"] = txt44HS.Text;

                                        dr["WSP"] = Stxtwsp.Text;
                                        dr["avgsize"] = txtavvgmeter.Text;
                                        dr["Extra"] = txtextrashirt.Text;

                                        if (radbtn.SelectedValue == "1")
                                        {
                                            ledgerr = ddlSupplier.SelectedValue;
                                            mainlab = drplab.SelectedValue;
                                            fitlab = chkfit.Checked;
                                            washlab = Chkwash.Checked;
                                            logolab = Chllogo.Checked;
                                        }
                                        else
                                        {

                                        }
                                        dr["LLedger"] = ledgerr;
                                        dr["Mainlab"] = mainlab;
                                        dr["FItLab"] = fitlab;
                                        dr["Washlab"] = washlab;
                                        dr["Logolab"] = logolab;



                                        dCrt.Rows.Add(dr);
                                    }
                                }
                            }
                            gvcustomerorder.DataSource = dCrt;
                            gvcustomerorder.DataBind();
                            dddldesign.Items.Remove(dddldesign.Items.FindByValue(dddldesign.SelectedValue));
                            dddldesign.ClearSelection();
                            FirstGridViewRow();
                            dddldesign.Focus();
                            btnadd.Enabled = true;
                        }

                    }
                }
            }
            System.Threading.Thread.Sleep(3000);
        }





        //protected void processclick(object sender, EventArgs e)
        //{
        //    double tot = 0.00;
        //    double tot2 = 0.00;
        //    double tot3 = 0.00;
        //    double r = 0.00;
        //    double tooo = 0.00;
        //    double gndtot = 0.00;
        //    double gndmet = 0.00;

        //    string ledgerr = string.Empty;
        //    string mainlab = string.Empty;

        //    bool fitlab = false;
        //    bool washlab = false;
        //    bool logolab = false;


        //    if (radbtn.SelectedValue == "1")
        //    {
        //        // double tot = 0.00;

        //        //  double r = 0.00;

        //        tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
        //        Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



        //        gndtot = gndtot + tot;



        //        DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
        //        if (dcalculate.Tables[0].Rows.Count > 0)
        //        {

        //            //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

        //            double wid = 0;
        //            if (drpFit.SelectedValue == "3")
        //            {
        //                wid = Convert.ToDouble(txtformal.Text);
        //            }
        //            else if (drpFit.SelectedValue == "4")
        //            {
        //                wid = Convert.ToDouble(txtboys.Text);
        //            }
        //            else if (drpFit.SelectedValue == "5")
        //            {
        //                wid = Convert.ToDouble(txtcasual.Text);
        //            }

        //            double roundoff = Convert.ToDouble(tot) * wid;
        //            //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
        //            //if (roundoff > 0.5)
        //            //{
        //            //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
        //            //}
        //            //else
        //            //{
        //            //    r = Math.Floor(Convert.ToDouble(roundoff));
        //            //}
        //            r = roundoff;

        //            //  txtavamet1.Text = r.ToString();
        //            txttotshirt1.Text = tot.ToString();
        //            //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

        //        }

        //        txt38FS.Focus();
        //        txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
        //        txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

        //        btnprocess.Enabled = true;
        //        btnadd.Enabled = true;

        //        //if (gndtot < (Convert.ToDouble(txtReqNoShirts.Text) - Convert.ToDouble(txtminshirt.Text)))
        //        //{
        //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in Lesser Than that Required Shirt.Thank you!!!');", true);
        //        //    btnadd.Enabled = false;
        //        //    btnprocess.Enabled = false;
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //    btnprocess.Enabled = true;
        //        //    btnadd.Enabled = true;
        //        //}

        //        //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
        //        //{
        //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
        //        //    btnadd.Enabled = false;
        //        //    btnprocess.Enabled = false;
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //    btnprocess.Enabled = true;
        //        //    btnadd.Enabled = true;
        //        //}



        //    }
        //    else
        //    {
        //        btnprocess.Enabled = true;
        //        btnadd.Enabled = true;

        //        for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
        //        {
        //            //DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

        //            //DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

        //            //TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
        //            //TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
        //            //TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
        //            //TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
        //            //TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
        //            //TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

        //            //TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
        //            //TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
        //            //TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
        //            //TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
        //            //TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
        //            //TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

        //            //TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
        //            TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
        //            TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
        //            if (txtavggsize.Text == "0" || txtavggsize.Text == "")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please click Calc Button.Thank You!!!');", true);
        //                btnadd.Enabled = false;
        //                btnprocess.Enabled = false;
        //                return;
        //            }
        //            if (txtreqmeter.Text == "0" || txtreqmeter.Text == "")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please click Calc Button.Thank you!!!');", true);
        //                btnadd.Enabled = false;
        //                btnprocess.Enabled = false;
        //                return;

        //            }
        //            //TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

        //            //tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
        //            //    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

        //            //gndtot = gndtot + tot;

        //            //int col = vLoop + 1;
        //            //if (dfit.SelectedValue != "Select Fit")
        //            //{

        //            //    DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
        //            //    if (dcalculate.Tables[0].Rows.Count > 0)
        //            //    {

        //            //        double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

        //            //        double roundoff = Convert.ToDouble(tot) * wid;
        //            //        //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
        //            //        //if (roundoff > 0.5)
        //            //        //{
        //            //        //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
        //            //        //}
        //            //        //else
        //            //        //{
        //            //        //    r = Math.Floor(Convert.ToDouble(roundoff));
        //            //        //}
        //            //        r = roundoff;

        //            //        txtreqmeter.Text = r.ToString();
        //            //        txtshirt.Text = tot.ToString();
        //            //        gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

        //            //    }

        //            //    txt38fs.Focus();

        //            //    // dparty.Focus();
        //            //}


        //            //txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
        //            //txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");


        //        }
        //        //if (gndtot < (Convert.ToDouble(txtReqNoShirts.Text) - Convert.ToDouble(txtminshirt.Text)))
        //        //{
        //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in Lesser Than that Required Shirt.Thank you!!!');", true);
        //        //    btnadd.Enabled = false;
        //        //    btnprocess.Enabled = false;
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //    btnprocess.Enabled = true;
        //        //    btnadd.Enabled = true;
        //        //}

        //        //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
        //        //{
        //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
        //        //    btnadd.Enabled = false;
        //        //    btnprocess.Enabled = false;
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //    btnprocess.Enabled = true;
        //        //    btnadd.Enabled = true;
        //        //}
        //    }
        //    //else
        //    //{
        //    //    if (drpCustomer.SelectedValue != "Select Party Name")
        //    //    {
        //    //        tot = tot + Convert.ToDouble(txt36FS.Text);
        //    //        tot = tot + Convert.ToDouble(txt36HS.Text);

        //    //        tot = tot + Convert.ToDouble(txt38FS.Text);
        //    //        tot = tot + Convert.ToDouble(txt38HS.Text);

        //    //        tot = tot + Convert.ToDouble(txt40FS.Text);
        //    //        tot = tot + Convert.ToDouble(txt40HS.Text);

        //    //        tot = tot + Convert.ToDouble(txt42FS.Text);
        //    //        tot = tot + Convert.ToDouble(txt42HS.Text);

        //    //        DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
        //    //        if (dcalculate.Tables[0].Rows.Count > 0)
        //    //        {

        //    //            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

        //    //            double roundoff = Convert.ToDouble(tot) * wid;
        //    //            //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
        //    //            if (roundoff > 0.5)
        //    //            {
        //    //                r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
        //    //            }
        //    //            else
        //    //            {
        //    //                r = Math.Floor(Convert.ToDouble(roundoff));
        //    //            }

        //    //            txtavamet1.Text = r.ToString();
        //    //            txttotshirt1.Text = tot.ToString();
        //    //        }


        //    //    }
        //    //    if (drpCustomer2.SelectedValue != "Select Party Name")
        //    //    {

        //    //        tot2 = tot2 + Convert.ToDouble(txt36FS2.Text);
        //    //        tot2 = tot2 + Convert.ToDouble(txt36HS2.Text);

        //    //        tot2 = tot2 + Convert.ToDouble(txt38FS2.Text);
        //    //        tot2 = tot2 + Convert.ToDouble(txt38HS2.Text);

        //    //        tot2 = tot2 + Convert.ToDouble(txt40FS2.Text);
        //    //        tot2 = tot2 + Convert.ToDouble(txt40HS2.Text);

        //    //        tot2 = tot2 + Convert.ToDouble(txt42FS2.Text);
        //    //        tot2 = tot2 + Convert.ToDouble(txt42HS2.Text);

        //    //        DataSet dcalculate = objBs.getsizeforcutt(drpFit2.SelectedValue, drpwidth.SelectedItem.Text);
        //    //        if (dcalculate.Tables[0].Rows.Count > 0)
        //    //        {

        //    //            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

        //    //            double roundoff = Convert.ToDouble(tot2) * wid;
        //    //            //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
        //    //            if (roundoff > 0.5)
        //    //            {
        //    //                r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
        //    //            }
        //    //            else
        //    //            {
        //    //                r = Math.Floor(Convert.ToDouble(roundoff));
        //    //            }

        //    //            txtavamet2.Text = r.ToString();
        //    //            txttotshirt2.Text = tot2.ToString();

        //    //        }



        //    //    }
        //    //    if (drpCustomer3.SelectedValue != "Select Party Name")
        //    //    {

        //    //        tot3 = tot3 + Convert.ToDouble(txt36FS3.Text);
        //    //        tot3 = tot3 + Convert.ToDouble(txt36HS3.Text);

        //    //        tot3 = tot3 + Convert.ToDouble(txt38FS3.Text);
        //    //        tot3 = tot3 + Convert.ToDouble(txt38HS3.Text);

        //    //        tot3 = tot3 + Convert.ToDouble(txt40FS3.Text);
        //    //        tot3 = tot3 + Convert.ToDouble(txt40HS3.Text);

        //    //        tot3 = tot3 + Convert.ToDouble(txt42FS3.Text);
        //    //        tot3 = tot3 + Convert.ToDouble(txt42HS3.Text);
        //    //        DataSet dcalculate = objBs.getsizeforcutt(drpFit3.SelectedValue, drpwidth.SelectedItem.Text);
        //    //        if (dcalculate.Tables[0].Rows.Count > 0)
        //    //        {

        //    //            double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

        //    //            double roundoff = Convert.ToDouble(tot3) * wid;
        //    //            //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
        //    //            if (roundoff > 0.5)
        //    //            {
        //    //                r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
        //    //            }
        //    //            else
        //    //            {
        //    //                r = Math.Floor(Convert.ToDouble(roundoff));
        //    //            }

        //    //            txtavamet3.Text = r.ToString();
        //    //            txttotshirt3.Text = tot3.ToString();



        //    //        }
        //    //    }

        //    //    tooo = tot + tot2 + tot3;


        //    //}
        //    //if (tooo > Convert.ToDouble(txtReqNoShirts.Text))
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
        //    //    btnadd.Enabled = false;
        //    //    return;
        //    //}
        //    //else
        //    //{
        //    //    btnadd.Enabled = true;
        //    //    // return;
        //    //}

        //    decimal dAmt = 0; decimal dTotal = 0;
        //    dCrt = (DataTable)ViewState["Data"];
        //    if (dCrt.Rows.Count == 0)
        //    {
        //        if (tr1.Visible == true)
        //        {
        //            if (drpCustomer.SelectedValue == "Select Party Name")
        //            {
        //            }
        //            else
        //            {
        //                DataRow dr = dCrt.NewRow();

        //                dr["Transid"] = dddldesign.SelectedValue;
        //                dr["Design"] = dddldesign.SelectedItem.Text;
        //                dr["Rate"] = txtDesignRate.Text;

        //                dr["meter"] = txtAvailableMtr.Text;
        //                dr["Shirt"] = txtNoofShirts.Text;
        //                dr["reqmeter"] = txtavamet1.Text;

        //                dr["reqshirt"] = txttotshirt1.Text;

        //                dr["brandid"] = drpCustomer.SelectedValue;
        //                dr["brand"] = drpCustomer.SelectedItem.Text;

        //                dr["ledgerid"] = drpCustomer.SelectedValue;
        //                dr["party"] = drpCustomer.SelectedItem.Text;
        //                dr["Fitid"] = drpFit.SelectedValue;
        //                dr["Fit"] = drpFit.SelectedItem.Text;

        //                dr["TSFS"] = txt36FS.Text;
        //                dr["TSHS"] = txt36HS.Text;

        //                dr["TEFS"] = txt38FS.Text;
        //                dr["TEHS"] = txt38HS.Text;

        //                dr["TNFS"] = txt39FS.Text;
        //                dr["TNHS"] = txt39HS.Text;

        //                dr["FZFS"] = txt40FS.Text;
        //                dr["FZHS"] = txt40HS.Text;

        //                dr["FTFS"] = txt42FS.Text;
        //                dr["FTHS"] = txt42HS.Text;

        //                dr["FFFS"] = txt44FS.Text;
        //                dr["FFHS"] = txt44HS.Text;

        //                dr["avgsize"] = txtavvgmeter.Text;

        //                dr["WSP"] = Stxtwsp.Text;
        //                dr["Extra"] = txtextrashirt.Text;

        //                if (radbtn.SelectedValue == "1")
        //                {
        //                    ledgerr = ddlSupplier.SelectedValue;
        //                    mainlab = drplab.SelectedValue;
        //                    fitlab = chkfit.Checked;
        //                    washlab = Chkwash.Checked;
        //                    logolab = Chllogo.Checked;
        //                }

        //                dr["LLedger"] = ledgerr;
        //                dr["Mainlab"] = mainlab;
        //                dr["FItLab"] = fitlab;
        //                dr["Washlab"] = washlab;
        //                dr["Logolab"] = logolab;



        //                dCrt.Rows.Add(dr);
        //            }
        //        }
        //        else
        //        {
        //            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
        //            {
        //                DropDownList dbrand = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrbrand");
        //                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

        //                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

        //                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
        //                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
        //                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
        //                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
        //                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
        //                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

        //                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
        //                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
        //                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
        //                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
        //                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
        //                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

        //                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
        //                TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
        //                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
        //                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

        //                if (dfit.SelectedValue == "Select Fit")
        //                {
        //                }
        //                else
        //                {

        //                    DataRow dr = dCrt.NewRow();

        //                    dr["Transid"] = dddldesign.SelectedValue;
        //                    dr["Design"] = dddldesign.SelectedItem.Text;
        //                    dr["Rate"] = txtDesignRate.Text;

        //                    dr["meter"] = txtAvailableMtr.Text;
        //                    dr["Shirt"] = txtNoofShirts.Text;
        //                    dr["reqmeter"] = txtreqmeter.Text;

        //                    dr["reqshirt"] = txtshirt.Text;

        //                    dr["brandid"] = dbrand.SelectedValue;
        //                    dr["brand"] = dbrand.SelectedItem.Text;

        //                    dr["ledgerid"] = dparty.SelectedValue;
        //                    dr["party"] = dparty.SelectedItem.Text;
        //                    dr["Fitid"] = dfit.SelectedValue;
        //                    dr["Fit"] = dfit.SelectedItem.Text;

        //                    dr["TSFS"] = txt36fs.Text;
        //                    dr["TSHS"] = txt36hs.Text;

        //                    dr["TEFS"] = txt38fs.Text;
        //                    dr["TEHS"] = txt38hs.Text;
        //                    dr["TNFS"] = txt39fs.Text;
        //                    dr["TNHS"] = txt39hs.Text;
        //                    //dr["TNFS"] = txt39FS.Text;
        //                    //dr["TNHS"] = txt39HS.Text;

        //                    dr["FZFS"] = txt40fs.Text;
        //                    dr["FZHS"] = txt40hs.Text;

        //                    dr["FTFS"] = txt42fs.Text;
        //                    dr["FTHS"] = txt42hs.Text;

        //                    dr["FFFS"] = txt44fs.Text;
        //                    dr["FFHS"] = txt44hs.Text;

        //                    dr["WSP"] = txtwsp.Text;
        //                    dr["avgsize"] = txtavggsize.Text;
        //                    dr["Extra"] = txtextrashirt.Text;
        //                    if (radbtn.SelectedValue == "1")
        //                    {
        //                        ledgerr = ddlSupplier.SelectedValue;
        //                        mainlab = drplab.SelectedValue;
        //                        fitlab = chkfit.Checked;
        //                        washlab = Chkwash.Checked;
        //                        logolab = Chllogo.Checked;
        //                    }
        //                    dr["LLedger"] = ledgerr;
        //                    dr["Mainlab"] = mainlab;
        //                    dr["FItLab"] = fitlab;
        //                    dr["Washlab"] = washlab;
        //                    dr["Logolab"] = logolab;



        //                    dCrt.Rows.Add(dr);

        //                }
        //            }

        //        }

        //        //if (tr2.Visible == true)
        //        //{
        //        //    if (drpCustomer2.SelectedValue == "Select Party Name")
        //        //    {

        //        //    }
        //        //    else
        //        //    {
        //        //        DataRow dr = dCrt.NewRow();

        //        //        dr["Transid"] = dddldesign.SelectedValue;
        //        //        dr["Design"] = dddldesign.SelectedItem.Text;
        //        //        dr["Rate"] = txtDesignRate.Text;

        //        //        dr["meter"] = txtAvailableMtr.Text;
        //        //        dr["Shirt"] = txtNoofShirts.Text;
        //        //        dr["reqmeter"] = txtavamet2.Text;

        //        //        dr["reqshirt"] = txttotshirt2.Text;
        //        //        dr["ledgerid"] = drpCustomer2.SelectedValue;
        //        //        dr["party"] = drpCustomer2.SelectedItem.Text;
        //        //        dr["Fitid"] = drpFit2.SelectedValue;
        //        //        dr["Fit"] = drpFit2.SelectedItem.Text;

        //        //        dr["TSFS"] = txt36FS2.Text;
        //        //        dr["TSHS"] = txt36HS2.Text;

        //        //        dr["TEFS"] = txt38FS2.Text;
        //        //        dr["TEHS"] = txt38HS2.Text;

        //        //        dr["FZFS"] = txt40FS2.Text;
        //        //        dr["FZHS"] = txt40HS2.Text;

        //        //        dr["FTFS"] = txt42FS2.Text;
        //        //        dr["FTHS"] = txt42HS2.Text;

        //        //        if (radbtn.SelectedValue == "1")
        //        //        {
        //        //            ledgerr = ddlSupplier.SelectedValue;
        //        //            mainlab = drplab.SelectedValue;
        //        //            fitlab = chkfit.Checked;
        //        //            washlab = Chkwash.Checked;
        //        //            logolab = Chllogo.Checked;
        //        //        }
        //        //        else
        //        //        {
        //        //            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
        //        //            //{


        //        //            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

        //        //            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
        //        //            //    {
        //        //            //        //   ledgerr = drpparty.SelectedValue;
        //        //            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
        //        //            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
        //        //            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
        //        //            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



        //        //            //        ledgerr = drpCustomer.SelectedValue;
        //        //            //        mainlab = drpparty.SelectedValue;
        //        //            //        fitlab = fitll.Checked;
        //        //            //        washlab = wasll.Checked;
        //        //            //        logolab = logll.Checked;
        //        //            //    }


        //        //            //}
        //        //        }
        //        //        dr["LLedger"] = ledgerr;
        //        //        dr["Mainlab"] = mainlab;
        //        //        dr["FItLab"] = fitlab;
        //        //        dr["Washlab"] = washlab;
        //        //        dr["Logolab"] = logolab;



        //        //        dCrt.Rows.Add(dr);
        //        //    }

        //        //}
        //        //if (tr3.Visible == true)
        //        //{
        //        //    if (drpCustomer3.SelectedValue == "Select Party Name")
        //        //    {

        //        //    }
        //        //    else
        //        //    {
        //        //        DataRow dr = dCrt.NewRow();

        //        //        dr["Transid"] = dddldesign.SelectedValue;
        //        //        dr["Design"] = dddldesign.SelectedItem.Text;
        //        //        dr["Rate"] = txtDesignRate.Text;

        //        //        dr["meter"] = txtAvailableMtr.Text;
        //        //        dr["Shirt"] = txtNoofShirts.Text;
        //        //        dr["reqmeter"] = txtavamet3.Text;

        //        //        dr["reqshirt"] = txttotshirt3.Text;
        //        //        dr["ledgerid"] = drpCustomer.SelectedValue;
        //        //        dr["party"] = drpCustomer3.SelectedItem.Text;
        //        //        dr["Fitid"] = drpFit3.SelectedValue;
        //        //        dr["Fit"] = drpFit3.SelectedItem.Text;

        //        //        dr["TSFS"] = txt36FS3.Text;
        //        //        dr["TSHS"] = txt36HS3.Text;

        //        //        dr["TEFS"] = txt38FS3.Text;
        //        //        dr["TEHS"] = txt38HS3.Text;

        //        //        dr["FZFS"] = txt40FS3.Text;
        //        //        dr["FZHS"] = txt40HS3.Text;

        //        //        dr["FTFS"] = txt42FS3.Text;
        //        //        dr["FTHS"] = txt42HS3.Text;

        //        //        if (radbtn.SelectedValue == "1")
        //        //        {
        //        //            ledgerr = ddlSupplier.SelectedValue;
        //        //            mainlab = drplab.SelectedValue;
        //        //            fitlab = chkfit.Checked;
        //        //            washlab = Chkwash.Checked;
        //        //            logolab = Chllogo.Checked;
        //        //        }
        //        //        else
        //        //        {
        //        //            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
        //        //            //{


        //        //            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

        //        //            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
        //        //            //    {
        //        //            //        //   ledgerr = drpparty.SelectedValue;
        //        //            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
        //        //            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
        //        //            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
        //        //            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



        //        //            //        ledgerr = drpCustomer.SelectedValue;
        //        //            //        mainlab = drpparty.SelectedValue;
        //        //            //        fitlab = fitll.Checked;
        //        //            //        washlab = wasll.Checked;
        //        //            //        logolab = logll.Checked;
        //        //            //    }


        //        //            //}
        //        //        }
        //        //        dr["LLedger"] = ledgerr;
        //        //        dr["Mainlab"] = mainlab;
        //        //        dr["FItLab"] = fitlab;
        //        //        dr["Washlab"] = washlab;
        //        //        dr["Logolab"] = logolab;



        //        //        dCrt.Rows.Add(dr);
        //        //    }
        //        //}

        //    }
        //    else
        //    {
        //        if (tr1.Visible == true)
        //        {
        //            if (drpCustomer.SelectedValue == "Select Party Name")
        //            {
        //            }
        //            else
        //            {
        //                DataRow dr = dCrt.NewRow();

        //                dr["Transid"] = dddldesign.SelectedValue;
        //                dr["Design"] = dddldesign.SelectedItem.Text;
        //                dr["Rate"] = txtDesignRate.Text;

        //                dr["meter"] = txtAvailableMtr.Text;
        //                dr["Shirt"] = txtNoofShirts.Text;
        //                dr["reqmeter"] = txtavamet1.Text;

        //                dr["reqshirt"] = txttotshirt1.Text;
        //                dr["ledgerid"] = drpCustomer.SelectedValue;
        //                dr["party"] = drpCustomer.SelectedItem.Text;
        //                dr["Fitid"] = drpFit.SelectedValue;
        //                dr["Fit"] = drpFit.SelectedItem.Text;

        //                dr["TSFS"] = txt36FS.Text;
        //                dr["TSHS"] = txt36HS.Text;

        //                dr["TEFS"] = txt38FS.Text;
        //                dr["TEHS"] = txt38HS.Text;

        //                dr["TNFS"] = txt39FS.Text;
        //                dr["TNHS"] = txt39HS.Text;

        //                dr["FZFS"] = txt40FS.Text;
        //                dr["FZHS"] = txt40HS.Text;

        //                dr["FTFS"] = txt42FS.Text;
        //                dr["FTHS"] = txt42HS.Text;

        //                dr["FFFS"] = txt44FS.Text;
        //                dr["FFHS"] = txt44HS.Text;

        //                dr["WSP"] = Stxtwsp.Text;
        //                dr["avgsize"] = txtavvgmeter.Text;
        //                dr["Extra"] = txtextrashirt.Text;

        //                if (radbtn.SelectedValue == "1")
        //                {
        //                    ledgerr = ddlSupplier.SelectedValue;
        //                    mainlab = drplab.SelectedValue;
        //                    fitlab = chkfit.Checked;
        //                    washlab = Chkwash.Checked;
        //                    logolab = Chllogo.Checked;
        //                }
        //                else
        //                {
        //                    //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
        //                    //{


        //                    //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

        //                    //    if (drpCustomer.SelectedItem.Text == txtno.Text)
        //                    //    {
        //                    //        //   ledgerr = drpparty.SelectedValue;
        //                    //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
        //                    //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
        //                    //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
        //                    //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



        //                    //        ledgerr = drpCustomer.SelectedValue;
        //                    //        mainlab = drpparty.SelectedValue;
        //                    //        fitlab = fitll.Checked;
        //                    //        washlab = wasll.Checked;
        //                    //        logolab = logll.Checked;
        //                    //    }


        //                    //}
        //                }
        //                dr["LLedger"] = ledgerr;
        //                dr["Mainlab"] = mainlab;
        //                dr["FItLab"] = fitlab;
        //                dr["Washlab"] = washlab;
        //                dr["Logolab"] = logolab;



        //                dCrt.Rows.Add(dr);
        //            }
        //        }
        //        else
        //        {
        //            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
        //            {
        //                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

        //                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

        //                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
        //                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
        //                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
        //                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
        //                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
        //                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

        //                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
        //                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
        //                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
        //                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
        //                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
        //                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

        //                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
        //                TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
        //                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
        //                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");


        //                DataRow dr = dCrt.NewRow();

        //                dr["Transid"] = dddldesign.SelectedValue;
        //                dr["Design"] = dddldesign.SelectedItem.Text;
        //                dr["Rate"] = txtDesignRate.Text;

        //                dr["meter"] = txtAvailableMtr.Text;
        //                dr["Shirt"] = txtNoofShirts.Text;
        //                dr["reqmeter"] = txtreqmeter.Text;

        //                dr["reqshirt"] = txtshirt.Text;
        //                dr["ledgerid"] = dparty.SelectedValue;
        //                dr["party"] = dparty.SelectedItem.Text;
        //                dr["Fitid"] = dfit.SelectedValue;
        //                dr["Fit"] = dfit.SelectedItem.Text;

        //                dr["TSFS"] = txt36fs.Text;
        //                dr["TSHS"] = txt36hs.Text;

        //                dr["TEFS"] = txt38fs.Text;
        //                dr["TEHS"] = txt38hs.Text;

        //                dr["TNFS"] = txt39fs.Text;
        //                dr["TNHS"] = txt39hs.Text;

        //                dr["FZFS"] = txt40fs.Text;
        //                dr["FZHS"] = txt40hs.Text;

        //                dr["FTFS"] = txt42fs.Text;
        //                dr["FTHS"] = txt42hs.Text;

        //                dr["FFFS"] = txt44fs.Text;
        //                dr["FFHS"] = txt44hs.Text;

        //                dr["WSP"] = txtwsp.Text;
        //                dr["avgsize"] = txtavggsize.Text;
        //                dr["Extra"] = txtextrashirt.Text;

        //                if (radbtn.SelectedValue == "1")
        //                {
        //                    ledgerr = ddlSupplier.SelectedValue;
        //                    mainlab = drplab.SelectedValue;
        //                    fitlab = chkfit.Checked;
        //                    washlab = Chkwash.Checked;
        //                    logolab = Chllogo.Checked;
        //                }
        //                dr["LLedger"] = ledgerr;
        //                dr["Mainlab"] = mainlab;
        //                dr["FItLab"] = fitlab;
        //                dr["Washlab"] = washlab;
        //                dr["Logolab"] = logolab;



        //                dCrt.Rows.Add(dr);

        //            }


        //        }
        //        //if (tr2.Visible == true)
        //        //{
        //        //    if (drpCustomer2.SelectedValue == "Select Party Name")
        //        //    {

        //        //    }
        //        //    else
        //        //    {
        //        //        DataRow dr = dCrt.NewRow();

        //        //        dr["Transid"] = dddldesign.SelectedValue;
        //        //        dr["Design"] = dddldesign.SelectedItem.Text;
        //        //        dr["Rate"] = txtDesignRate.Text;

        //        //        dr["meter"] = txtAvailableMtr.Text;
        //        //        dr["Shirt"] = txtNoofShirts.Text;
        //        //        dr["reqmeter"] = txtavamet2.Text;

        //        //        dr["reqshirt"] = txttotshirt2.Text;
        //        //        dr["ledgerid"] = drpCustomer2.SelectedValue;
        //        //        dr["party"] = drpCustomer2.SelectedItem.Text;
        //        //        dr["Fitid"] = drpFit2.SelectedValue;
        //        //        dr["Fit"] = drpFit2.SelectedItem.Text;

        //        //        dr["TSFS"] = txt36FS2.Text;
        //        //        dr["TSHS"] = txt36HS2.Text;

        //        //        dr["TEFS"] = txt38FS2.Text;
        //        //        dr["TEHS"] = txt38HS2.Text;

        //        //        dr["FZFS"] = txt40FS2.Text;
        //        //        dr["FZHS"] = txt40HS2.Text;

        //        //        dr["FTFS"] = txt42FS2.Text;
        //        //        dr["FTHS"] = txt42HS2.Text;

        //        //        if (radbtn.SelectedValue == "1")
        //        //        {
        //        //            ledgerr = ddlSupplier.SelectedValue;
        //        //            mainlab = drplab.SelectedValue;
        //        //            fitlab = chkfit.Checked;
        //        //            washlab = Chkwash.Checked;
        //        //            logolab = Chllogo.Checked;
        //        //        }
        //        //        else
        //        //        {
        //        //            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
        //        //            //{


        //        //            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

        //        //            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
        //        //            //    {
        //        //            //        //   ledgerr = drpparty.SelectedValue;
        //        //            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
        //        //            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
        //        //            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
        //        //            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



        //        //            //        ledgerr = drpCustomer.SelectedValue;
        //        //            //        mainlab = drpparty.SelectedValue;
        //        //            //        fitlab = fitll.Checked;
        //        //            //        washlab = wasll.Checked;
        //        //            //        logolab = logll.Checked;
        //        //            //    }


        //        //            //}
        //        //        }
        //        //        dr["LLedger"] = ledgerr;
        //        //        dr["Mainlab"] = mainlab;
        //        //        dr["FItLab"] = fitlab;
        //        //        dr["Washlab"] = washlab;
        //        //        dr["Logolab"] = logolab;



        //        //        dCrt.Rows.Add(dr);
        //        //    }

        //        //}
        //        //if (tr3.Visible == true)
        //        //{
        //        //    if (drpCustomer3.SelectedValue == "Select Party Name")
        //        //    {

        //        //    }
        //        //    else
        //        //    {
        //        //        DataRow dr = dCrt.NewRow();

        //        //        dr["Transid"] = dddldesign.SelectedValue;
        //        //        dr["Design"] = dddldesign.SelectedItem.Text;
        //        //        dr["Rate"] = txtDesignRate.Text;

        //        //        dr["meter"] = txtAvailableMtr.Text;
        //        //        dr["Shirt"] = txtNoofShirts.Text;
        //        //        dr["reqmeter"] = txtavamet3.Text;

        //        //        dr["reqshirt"] = txttotshirt3.Text;
        //        //        dr["ledgerid"] = drpCustomer.SelectedValue;
        //        //        dr["party"] = drpCustomer3.SelectedItem.Text;
        //        //        dr["Fitid"] = drpFit3.SelectedValue;
        //        //        dr["Fit"] = drpFit3.SelectedItem.Text;

        //        //        dr["TSFS"] = txt36FS3.Text;
        //        //        dr["TSHS"] = txt36HS3.Text;

        //        //        dr["TEFS"] = txt38FS3.Text;
        //        //        dr["TEHS"] = txt38HS3.Text;

        //        //        dr["FZFS"] = txt40FS3.Text;
        //        //        dr["FZHS"] = txt40HS3.Text;

        //        //        dr["FTFS"] = txt42FS3.Text;
        //        //        dr["FTHS"] = txt42HS3.Text;

        //        //        if (radbtn.SelectedValue == "1")
        //        //        {
        //        //            ledgerr = ddlSupplier.SelectedValue;
        //        //            mainlab = drplab.SelectedValue;
        //        //            fitlab = chkfit.Checked;
        //        //            washlab = Chkwash.Checked;
        //        //            logolab = Chllogo.Checked;
        //        //        }
        //        //        else
        //        //        {
        //        //            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
        //        //            //{


        //        //            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

        //        //            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
        //        //            //    {
        //        //            //        //   ledgerr = drpparty.SelectedValue;
        //        //            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
        //        //            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
        //        //            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
        //        //            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



        //        //            //        ledgerr = drpCustomer.SelectedValue;
        //        //            //        mainlab = drpparty.SelectedValue;
        //        //            //        fitlab = fitll.Checked;
        //        //            //        washlab = wasll.Checked;
        //        //            //        logolab = logll.Checked;
        //        //            //    }


        //        //            //}
        //        //        }
        //        //        dr["LLedger"] = ledgerr;
        //        //        dr["Mainlab"] = mainlab;
        //        //        dr["FItLab"] = fitlab;
        //        //        dr["Washlab"] = washlab;
        //        //        dr["Logolab"] = logolab;



        //        //        dCrt.Rows.Add(dr);
        //        //    }
        //        //}

        //        //DataRow dr = dCrt.NewRow();


        //        //dr["CatID"] = ddlCategory.SelectedValue;
        //        //dr["SubCatID"] = lblSubcatid.Text;

        //        //dr["Group"] = ddlCategory.SelectedItem.Text;
        //        //dr["item"] = ddlitem.SelectedItem.Text;
        //        //dr["ExistQty"] = txtAvalQty.Text;

        //        //dr["Qty"] = txtretQty.Text;
        //        //dr["Rate"] = txtRate.Text;
        //        //dr["Amount"] = txtAmount.Text;
        //        //dr["stockid"] = ddlitem.SelectedValue;
        //        //dCrt.Rows.Add(dr);

        //    }

        //    gvcustomerorder.DataSource = dCrt;
        //    gvcustomerorder.DataBind();


        //    dddldesign.ClearSelection();
        //    txtDesignRate.Text = "";
        //    txtAvailableMtr.Text = "";
        //    txtNoofShirts.Text = "";
        //    txtReqMtr.Text = "";
        //    txtReqNoShirts.Text = "";
        //    txtextrashirt.Text = "";
        //    //   rdSingle.Checked = false;
        //    //   rdMultiple.Checked = false;

        //    drpCustomer.ClearSelection();
        //    drpFit.ClearSelection();
        //    txt36FS.Text = "0";
        //    txt36HS.Text = "0";

        //    txt38FS.Text = "0";
        //    txt38HS.Text = "0";

        //    txt40FS.Text = "0";
        //    txt40HS.Text = "0";

        //    txt42FS.Text = "0";
        //    txt42HS.Text = "0";

        //    txt44FS.Text = "0";
        //    txt44HS.Text = "0";

        //    txt39FS.Text = "0";
        //    txt39HS.Text = "0";

        //    txtavamet1.Text = "0";
        //    txttotshirt1.Text = "0";
        //    txtavvgmeter.Text = "0";



        //    //gridsize.DataSource = null;
        //    //gridsize.DataBind();
        //    FirstGridViewRow();
        //    removedropdownlist();


        //    //drpCustomer2.ClearSelection();
        //    //drpFit2.ClearSelection();
        //    //txt36FS2.Text = "";
        //    //txt36HS2.Text = "";

        //    //txt38FS2.Text = "";
        //    //txt38HS2.Text = "";

        //    //txt40FS2.Text = "";
        //    //txt40HS2.Text = "";

        //    //txt42FS2.Text = "";
        //    //txt42HS2.Text = "";

        //    //txtavamet2.Text = "";
        //    //txttotshirt2.Text = "";

        //    //drpCustomer3.ClearSelection();
        //    //drpFit3.ClearSelection();
        //    //txt36FS3.Text = "";
        //    //txt36HS3.Text = "";

        //    //txt38FS3.Text = "";
        //    //txt38HS3.Text = "";

        //    //txt40FS3.Text = "";
        //    //txt40HS3.Text = "";

        //    //txt42FS3.Text = "";
        //    //txt42HS3.Text = "";

        //    //txtavamet3.Text = "";
        //    //txttotshirt3.Text = "";


        //    dddldesign.Focus();

        //    System.Threading.Thread.Sleep(3000);

        //}


        protected void processclick(object sender, EventArgs e)
        {

            if (txtadjmeter.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //  btnadd.Enabled = false;
                btnprocessall.Enabled = false;
                return;
            }
            else
            {
                // btnadd.Enabled = false;
                btnprocessall.Enabled = false;
            }

            DataSet dddesgin = new DataSet();
            double tot = 0.00;
            double originalreqq = 0.00;
            double tot2 = 0.00;
            double tot3 = 0.00;
            double r = 0.00;
             double wid = 0;
             double roundoff=0;
            double tooo = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            string ledgerr = string.Empty;
            string mainlab = string.Empty;

            bool fitlab = false;
            bool washlab = false;
            bool logolab = false;

            //double r1 = 0.00;
            //double rr = 0.00;
            //double rb = 0.00;
            //double rr1 = 0.00;
            //double rb1 = 0.00;
            //string width = string.Empty;
            //   double reqq = 0.00;

            if (radbtn.SelectedValue == "1")
            {
                

                originalreqq = Convert.ToDouble(txtReqMtr.Text);
                //   for (int i = 0; i < dddesgin.Tables[0].Rows.Count; i++)
                //   {
                double reqq = 0.00;
                double reqq1 = 0.000;
                //  dddldesign.SelectedValue = dddesgin.Tables[0].Rows[i]["id"].ToString();

                reqq = originalreqq;
                reqq1 = originalreqq + Convert.ToDouble(txtadjmeter.Text);
                //   reqq = reqq + 3;

                //Get Desgin Number
                double r1 = 0.00;
                double rr = 0.00;
                double rb = 0.00;
                double rr1 = 0.00;
                double rb1 = 0.00;
                string width = string.Empty;


                DataSet dteo = objBs.getcutlistdesignfortrans(dddldesign.SelectedValue);
                if (dteo.Tables[0].Rows.Count > 0)
                {
                    txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
                    txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                    //if (reqq == Convert.ToDouble(txtAvailableMtr.Text))
                    //{

                    //}
                    //else if ((Convert.ToDouble(txtAvailableMtr.Text)) >= reqq && (Convert.ToDouble(txtAvailableMtr.Text)) <= reqq1)
                    //{
                    //    reqq = (Convert.ToDouble(txtAvailableMtr.Text));
                    //}
                    //else if (reqq >= (Convert.ToDouble(txtAvailableMtr.Text)))
                    //{
                    //    reqq = (Convert.ToDouble(txtAvailableMtr.Text));
                    //}
                    //else
                    //{
                    //    reqq = originalreqq;
                    //}
                    //if (reqq >= Convert.ToDouble(txtAvailableMtr.Text) && reqq <= Convert.ToDouble(txtAvailableMtr.Text))
                    //{
                    //    return;
                    //}


                    //  return;
                    txtReqMtr.Text = reqq.ToString("N");
                    if (radbtn.SelectedValue == "1")
                    {
                        txtavamet1.Text = reqq.ToString();
                    }

                    //     DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                    //     if (dcalculate.Tables[0].Rows.Count > 0)
                    {

                        //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                         wid = 0;
                        if (drpFit.SelectedValue == "3")
                        {
                            wid = Convert.ToDouble(txtformal.Text);
                        }
                        else if (drpFit.SelectedValue == "4")
                        {
                            wid = Convert.ToDouble(txtboys.Text);
                        }
                        else if (drpFit.SelectedValue == "5")
                        {
                            wid = Convert.ToDouble(txtcasual.Text);
                        }

                         roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                        if (roundoff > 0.5)
                        {
                            r1 = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            r1 = Math.Floor(Convert.ToDouble(roundoff));
                        }

                    }
                    txtNoofShirts.Text = r1.ToString();
                    txtReqNoShirts.Text = r1.ToString();


                }
                rr = ((r1 * 15) / 100);
                if (rr > 0.5)
                {
                    rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb = Math.Floor(Convert.ToDouble(rr));
                }
                txtextrashirt.Text = rb.ToString();

                rr1 = ((r1 * 2) / 100);
                if (rr1 > 0.5)
                {
                    rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb1 = Math.Floor(Convert.ToDouble(rr1));
                }
                txtminshirt.Text = rb1.ToString();


                if (txt36FS.Text == "")
                {
                    txt36FS.Text = "0";
                }
                if (txt36HS.Text == "")
                {
                    txt36HS.Text = "0";
                }

                if (txt38FS.Text == "")
                {
                    txt38FS.Text = "0";
                }
                if (txt38HS.Text == "")
                {
                    txt38HS.Text = "0";
                }
                if (txt39FS.Text == "")
                {
                    txt39FS.Text = "0";
                }
                if (txt39HS.Text == "")
                {
                    txt39HS.Text = "0";
                }
                if (txt40FS.Text == "")
                {
                    txt40FS.Text = "0";
                }
                if (txt40HS.Text == "")
                {
                    txt40HS.Text = "0";
                }
                if (txt42FS.Text == "")
                {
                    txt42FS.Text = "0";
                }
                if (txt42HS.Text == "")
                {
                    txt42HS.Text = "0";
                }
                if (txt44FS.Text == "")
                {
                    txt44FS.Text = "0";
                }
                if (txt44HS.Text == "")
                {
                    txt44HS.Text = "0";
                }



                if (radbtn.SelectedValue == "1")
                {
                    if (Sddrrpfit.SelectedValue == "3")
                    {




                        tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                        Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);

                        txtavvgmeter.Text = Convert.ToDouble(reqq / tot).ToString("N");
                    }
                    else if (Sddrrpfit.SelectedValue == "4")
                    {
                        tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text);
                        txtavvgmeter.Text = Convert.ToDouble(reqq / tot).ToString("N");
                    }
                    else if (Sddrrpfit.SelectedValue == "5")
                    {
                        tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text);
                        txtavvgmeter.Text = Convert.ToDouble(reqq / tot).ToString("N");
                    }

                    if (txtavvgmeter.Text == "Infinity")
                    {
                        txtavvgmeter.Text = "0";
                    }

                    gndtot = gndtot + tot;



                    //     DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                    //    if (dcalculate.Tables[0].Rows.Count > 0)
                    {

                       
                        if (drpFit.SelectedValue == "3")
                        {
                            wid = Convert.ToDouble(txtformal.Text);
                        }
                        else if (drpFit.SelectedValue == "4")
                        {
                            wid = Convert.ToDouble(txtboys.Text);
                        }
                        else if (drpFit.SelectedValue == "5")
                        {
                            wid = Convert.ToDouble(txtcasual.Text);
                        }

                         roundoff = Convert.ToDouble(tot) * wid;

                        r = roundoff;

                        // txtavamet1.Text = r.ToString();
                        txttotshirt1.Text = tot.ToString();
                    }

                    txt38FS.Focus();
                    txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
                    txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");
                }
                decimal dAmt = 0; decimal dTotal = 0;
                dCrt = (DataTable)ViewState["Data"];
                //if (originalreqq > (Convert.ToDouble(txtAvailableMtr.Text)))
                //{
                //}
                //else
                {
                    if (dCrt.Rows.Count == 0)
                    {
                        //if (tr1.Visible == true)
                      //  {
                            if (drpCustomer.SelectedValue == "Select Brand Name")
                            {
                            }
                            else
                            {
                                DataRow dr = dCrt.NewRow();

                                dr["Transid"] = dddldesign.SelectedValue;
                                dr["Design"] = dddldesign.SelectedItem.Text;
                                dr["Rate"] = txtDesignRate.Text;

                                dr["meter"] = txtAvailableMtr.Text;
                                // dr["Shirt"] = txtNoofShirts.Text;
                                dr["Shirt"] = txtNoofShirts.Text;
                                dr["reqmeter"] = txtavamet1.Text;
                                dr["actmeter"] = roundoff.ToString("0.00");

                                dr["reqshirt"] = txttotshirt1.Text;

                                dr["brandid"] = drpBrand.SelectedValue;
                                dr["brand"] = drpBrand.SelectedItem.Text;

                                dr["ledgerid"] = drpCustomer.SelectedValue;
                                dr["party"] = drpCustomer.SelectedItem.Text;
                                dr["Fitid"] = drpFit.SelectedValue;
                                dr["Fit"] = drpFit.SelectedItem.Text;

                                if (Sddrrpfit.SelectedValue == "3")
                                {

                                    dr["TSFS"] = txt36FS.Text;
                                    dr["TSHS"] = txt36HS.Text;

                                    dr["TEFS"] = txt38FS.Text;
                                    dr["TEHS"] = txt38HS.Text;

                                    dr["TNFS"] = txt39FS.Text;
                                    dr["TNHS"] = txt39HS.Text;

                                    dr["FZFS"] = txt40FS.Text;
                                    dr["FZHS"] = txt40HS.Text;

                                    dr["FTFS"] = txt42FS.Text;
                                    dr["FTHS"] = txt42HS.Text;

                                    dr["FFFS"] = txt44FS.Text;
                                    dr["FFHS"] = txt44HS.Text;

                                }
                                else if (Sddrrpfit.SelectedValue == "4")
                                {

                                    dr["TSFS"] = txt28FS3.Text;
                                    dr["TSHS"] = txt28HS3.Text;

                                    dr["TEFS"] = txt30FS3.Text;
                                    dr["TEHS"] = txt30HS3.Text;

                                    dr["TNFS"] = txt32FS3.Text;
                                    dr["TNHS"] = txt32HS3.Text;

                                    dr["FZFS"] = txt34FS3.Text;
                                    dr["FZHS"] = txt34HS3.Text;

                                    dr["FTFS"] = txt36FS3.Text;
                                    dr["FTHS"] = txt36HS3.Text;

                                    dr["FFFS"] = "0";
                                    dr["FFHS"] = "0";

                                }
                                else if (Sddrrpfit.SelectedValue == "5")
                                {

                                    dr["TSFS"] = txtSFS2.Text;
                                    dr["TSHS"] = txtSHS2.Text;

                                    dr["TEFS"] = txtMFS2.Text;
                                    dr["TEHS"] = txtMHS2.Text;

                                    dr["TNFS"] = "0";
                                    dr["TNHS"] = "0";

                                    dr["FZFS"] = txtLFS2.Text;
                                    dr["FZHS"] = txtLHS2.Text;

                                    dr["FTFS"] = txtXLFS2.Text;
                                    dr["FTHS"] = txtXLHS2.Text;

                                    dr["FFFS"] = txtXXLFS2.Text;
                                    dr["FFHS"] = txtXXLHS2.Text;

                                }
                                dr["avgsize"] = txtavvgmeter.Text;
                                dr["actsize"] = wid.ToString("0.00");

                                dr["WSP"] = Stxtwsp.Text;
                                dr["Extra"] = txtextrashirt.Text;

                                if (radbtn.SelectedValue == "1")
                                {
                                    ledgerr = ddlSupplier.SelectedValue;
                                    mainlab = drpFit.SelectedValue;
                                    fitlab = chkfit.Checked;
                                    washlab = Chkwash.Checked;
                                    logolab = Chllogo.Checked;
                                }

                                dr["LLedger"] = ledgerr;
                                dr["Mainlab"] = mainlab;
                                dr["FItLab"] = fitlab;
                                dr["Washlab"] = washlab;
                                dr["Logolab"] = logolab;



                                dCrt.Rows.Add(dr);
                            }
                      //  }
                    }
                    else
                    {
                        // if (tr1.Visible == true)
                        // {
                        if (drpCustomer.SelectedValue == "Select Party Name")
                        {
                        }
                        else
                        {
                            DataRow dr = dCrt.NewRow();

                            dr["Transid"] = dddldesign.SelectedValue;
                            dr["Design"] = dddldesign.SelectedItem.Text;
                            dr["Rate"] = txtDesignRate.Text;

                            dr["meter"] = txtAvailableMtr.Text;
                            dr["Shirt"] = txtNoofShirts.Text;
                            dr["reqmeter"] = txtavamet1.Text;
                            dr["actmeter"] = roundoff.ToString("0.00");

                            dr["reqshirt"] = txttotshirt1.Text;

                            dr["brandid"] = drpBrand.SelectedValue;
                            dr["brand"] = drpBrand.SelectedItem.Text;

                            dr["ledgerid"] = drpCustomer.SelectedValue;
                            dr["party"] = drpCustomer.SelectedItem.Text;
                            dr["Fitid"] = drpFit.SelectedValue;
                            dr["Fit"] = drpFit.SelectedItem.Text;

                            if (Sddrrpfit.SelectedValue == "3")
                            {

                                dr["TSFS"] = txt36FS.Text;
                                dr["TSHS"] = txt36HS.Text;

                                dr["TEFS"] = txt38FS.Text;
                                dr["TEHS"] = txt38HS.Text;

                                dr["TNFS"] = txt39FS.Text;
                                dr["TNHS"] = txt39HS.Text;

                                dr["FZFS"] = txt40FS.Text;
                                dr["FZHS"] = txt40HS.Text;

                                dr["FTFS"] = txt42FS.Text;
                                dr["FTHS"] = txt42HS.Text;

                                dr["FFFS"] = txt44FS.Text;
                                dr["FFHS"] = txt44HS.Text;

                            }
                            else if (Sddrrpfit.SelectedValue == "4")
                            {

                                dr["TSFS"] = txt28FS3.Text;
                                dr["TSHS"] = txt28HS3.Text;

                                dr["TEFS"] = txt30FS3.Text;
                                dr["TEHS"] = txt30HS3.Text;

                                dr["TNFS"] = txt32FS3.Text;
                                dr["TNHS"] = txt32HS3.Text;

                                dr["FZFS"] = txt34FS3.Text;
                                dr["FZHS"] = txt34HS3.Text;

                                dr["FTFS"] = txt36FS3.Text;
                                dr["FTHS"] = txt36HS3.Text;

                                dr["FFFS"] = "0";
                                dr["FFHS"] = "0";

                            }
                            else if (Sddrrpfit.SelectedValue == "5")
                            {

                                dr["TSFS"] = txtSFS2.Text;
                                dr["TSHS"] = txtSHS2.Text;

                                dr["TEFS"] = txtMFS2.Text;
                                dr["TEHS"] = txtMHS2.Text;

                                dr["TNFS"] = "0";
                                dr["TNHS"] = "0";

                                dr["FZFS"] = txtLFS2.Text;
                                dr["FZHS"] = txtLHS2.Text;

                                dr["FTFS"] = txtXLFS2.Text;
                                dr["FTHS"] = txtXLHS2.Text;

                                dr["FFFS"] = txtXXLFS2.Text;
                                dr["FFHS"] = txtXXLHS2.Text;

                            }

                            dr["WSP"] = Stxtwsp.Text;
                            dr["avgsize"] = txtavvgmeter.Text;
                            dr["actsize"] = wid.ToString("0.00") ;
                            dr["Extra"] = txtextrashirt.Text;

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drpFit.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                            }
                            else
                            {

                            }
                            dr["LLedger"] = ledgerr;
                            dr["Mainlab"] = mainlab;
                            dr["FItLab"] = fitlab;
                            dr["Washlab"] = washlab;
                            dr["Logolab"] = logolab;



                            dCrt.Rows.Add(dr);
                        }
                        //  }
                    }
                    if (Sddrrpfit.SelectedValue == "3")
                    {
                        gvcustomerorder.DataSource = dCrt;
                        gvcustomerorder.DataBind();
                        panelformal.Visible = true;

                    }
                    else if (Sddrrpfit.SelectedValue == "4")
                    {
                        GridView2.DataSource = dCrt;
                        GridView2.DataBind();
                        panelboys.Visible = true;
                    }
                    else if (Sddrrpfit.SelectedValue == "5")
                    {
                        GridView1.DataSource = dCrt;
                        GridView1.DataBind();
                        panelcasual.Visible = true;
                    }
                    //   string idd = myDS.Tables[0].Rows[j]["id"].ToString();


                    dddldesign.Items.Remove(dddldesign.Items.FindByValue(dddldesign.SelectedValue));
                    // dddldesign.Items.Remove(dddldesign.Items[i]);




                    //  dddldesign.ClearSelection();
                    //txtDesignRate.Text = "";
                    //txtAvailableMtr.Text = "";
                    //txtNoofShirts.Text = "";
                    //txtReqMtr.Text = "";
                    //txtReqNoShirts.Text = "";
                    //txtextrashirt.Text = "";

                    //drpCustomer.ClearSelection();
                    //drpFit.ClearSelection();
                    txt36FS.Text = "0";
                    txt36HS.Text = "0";

                    txt38FS.Text = "0";
                    txt38HS.Text = "0";

                    txt40FS.Text = "0";
                    txt40HS.Text = "0";

                    txt42FS.Text = "0";
                    txt42HS.Text = "0";

                    txt44FS.Text = "0";
                    txt44HS.Text = "0";

                    txt39FS.Text = "0";
                    txt39HS.Text = "0";

                    txtavamet1.Text = "0";
                    txttotshirt1.Text = "0";
                    txtavvgmeter.Text = "0";


                    txt28FS3.Text = "0";
                    txt28HS3.Text = "0";

                    txt30FS3.Text = "0";
                    txt30HS3.Text = "0";

                    txt32FS3.Text = "0";
                    txt32HS3.Text = "0";

                    txt34FS3.Text = "0";
                    txt34HS3.Text = "0";

                    txt36FS3.Text = "0";
                    txt36HS3.Text = "0";

                    txtSFS2.Text = "0";
                    txtSHS2.Text = "0";

                    txtMFS2.Text = "0";
                    txtMHS2.Text = "0";


                    txtLFS2.Text = "0";
                    txtLHS2.Text = "0";

                    txtXLFS2.Text = "0";
                    txtXLHS2.Text = "0";

                    txtXXLFS2.Text = "0";
                    txtXXLHS2.Text = "0";

                    // FirstGridViewRow();
                    // removedropdownlist();
                    dddldesign.Focus();
                    btnadd.Enabled = true;
                }



            }
            System.Threading.Thread.Sleep(3000);
        }

        protected void GOheadprocessclick(object sender, EventArgs e)
        {
            double tot = 0.00;
            double tot2 = 0.00;
            double tot3 = 0.00;
            double r = 0.00;
            double tooo = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            string ledgerr = string.Empty;
            string mainlab = string.Empty;

            bool fitlab = false;
            bool washlab = false;
            bool logolab = false;

            if (radcuttype.SelectedValue == "1")
            {

            }
            else
            {

                if (radbtn.SelectedValue == "1")
                {
                    // double tot = 0.00;

                    //  double r = 0.00;

                    tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                    Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                    gndtot = gndtot + tot;



                    DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                    if (dcalculate.Tables[0].Rows.Count > 0)
                    {

                        //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                        double wid = 0;
                        if (drpFit.SelectedValue == "3")
                        {
                            wid = Convert.ToDouble(txtformal.Text);
                        }
                        else if (drpFit.SelectedValue == "4")
                        {
                            wid = Convert.ToDouble(txtboys.Text);
                        }
                        else if (drpFit.SelectedValue == "5")
                        {
                            wid = Convert.ToDouble(txtcasual.Text);
                        }

                        double roundoff = Convert.ToDouble(tot) * wid;

                        r = roundoff;


                        txttotshirt1.Text = tot.ToString();


                    }

                    txt38FS.Focus();
                    txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
                    txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

                    btnprocess.Enabled = true;
                    btnadd.Enabled = true;





                }
                else
                {
                    btnprocess.Enabled = true;
                    btnadd.Enabled = true;

                    for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                    {

                        TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
                        TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                        if (txtavggsize.Text == "0" || txtavggsize.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please click Calc Button.Thank You!!!');", true);
                            btnadd.Enabled = false;
                            btnprocess.Enabled = false;
                            return;
                        }
                        if (txtreqmeter.Text == "0" || txtreqmeter.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please click Calc Button.Thank you!!!');", true);
                            btnadd.Enabled = false;
                            btnprocess.Enabled = false;
                            return;

                        }



                    }

                }
            }

            decimal dAmt = 0; decimal dTotal = 0;
            if (radcuttype.SelectedValue == "2")
            {
                dCrt = (DataTable)ViewState["Data"];
                if (dCrt.Rows.Count == 0)
                {
                    if (tr1.Visible == true)
                    {
                        if (drpCustomer.SelectedValue == "Select Party Name")
                        {
                        }
                        else
                        {
                            DataRow dr = dCrt.NewRow();

                            dr["Transid"] = "";
                            dr["Design"] = "Single Bulk Cutting";
                            dr["Rate"] = "0";

                            dr["meter"] = txtAvailableMtr.Text;
                            dr["Shirt"] = txtNoofShirts.Text;
                            dr["reqmeter"] = txtavamet1.Text;

                            dr["reqshirt"] = txttotshirt1.Text;
                            dr["ledgerid"] = drpCustomer.SelectedValue;
                            dr["party"] = drpCustomer.SelectedItem.Text;
                            dr["Fitid"] = drpFit.SelectedValue;
                            dr["Fit"] = drpFit.SelectedItem.Text;

                            dr["TSFS"] = txt36FS.Text;
                            dr["TSHS"] = txt36HS.Text;

                            dr["TEFS"] = txt38FS.Text;
                            dr["TEHS"] = txt38HS.Text;

                            dr["TNFS"] = txt39FS.Text;
                            dr["TNHS"] = txt39HS.Text;

                            dr["FZFS"] = txt40FS.Text;
                            dr["FZHS"] = txt40HS.Text;

                            dr["FTFS"] = txt42FS.Text;
                            dr["FTHS"] = txt42HS.Text;

                            dr["FFFS"] = txt44FS.Text;
                            dr["FFHS"] = txt44HS.Text;

                            dr["avgsize"] = txtavvgmeter.Text;

                            dr["WSP"] = Stxtwsp.Text;
                            dr["Extra"] = txtextrashirt.Text;

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drplab.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                            }

                            dr["LLedger"] = ledgerr;
                            dr["Mainlab"] = mainlab;
                            dr["FItLab"] = fitlab;
                            dr["Washlab"] = washlab;
                            dr["Logolab"] = logolab;



                            dCrt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        //for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                        //{
                        //    DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                        //    DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                        //    TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                        //    TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                        //    TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                        //    TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                        //    TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                        //    TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                        //    TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                        //    TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                        //    TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                        //    TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                        //    TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                        //    TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                        //    TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                        //    TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
                        //    TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                        //    TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                        //    if (dfit.SelectedValue == "Select Fit")
                        //    {
                        //    }
                        //    else
                        //    {

                        //        DataRow dr = dCrt.NewRow();

                        //        dr["Transid"] = dddldesign.SelectedValue;
                        //        dr["Design"] = dddldesign.SelectedItem.Text;
                        //        dr["Rate"] = txtDesignRate.Text;

                        //        dr["meter"] = txtAvailableMtr.Text;
                        //        dr["Shirt"] = txtNoofShirts.Text;
                        //        dr["reqmeter"] = txtreqmeter.Text;

                        //        dr["reqshirt"] = txtshirt.Text;
                        //        dr["ledgerid"] = dparty.SelectedValue;
                        //        dr["party"] = dparty.SelectedItem.Text;
                        //        dr["Fitid"] = dfit.SelectedValue;
                        //        dr["Fit"] = dfit.SelectedItem.Text;

                        //        dr["TSFS"] = txt36fs.Text;
                        //        dr["TSHS"] = txt36hs.Text;

                        //        dr["TEFS"] = txt38fs.Text;
                        //        dr["TEHS"] = txt38hs.Text;
                        //        dr["TNFS"] = txt39fs.Text;
                        //        dr["TNHS"] = txt39hs.Text;
                        //        //dr["TNFS"] = txt39FS.Text;
                        //        //dr["TNHS"] = txt39HS.Text;

                        //        dr["FZFS"] = txt40fs.Text;
                        //        dr["FZHS"] = txt40hs.Text;

                        //        dr["FTFS"] = txt42fs.Text;
                        //        dr["FTHS"] = txt42hs.Text;

                        //        dr["FFFS"] = txt44fs.Text;
                        //        dr["FFHS"] = txt44hs.Text;

                        //        dr["WSP"] = txtwsp.Text;
                        //        dr["avgsize"] = txtavggsize.Text;
                        //        dr["Extra"] = txtextrashirt.Text;
                        //        if (radbtn.SelectedValue == "1")
                        //        {
                        //            ledgerr = ddlSupplier.SelectedValue;
                        //            mainlab = drplab.SelectedValue;
                        //            fitlab = chkfit.Checked;
                        //            washlab = Chkwash.Checked;
                        //            logolab = Chllogo.Checked;
                        //        }
                        //        dr["LLedger"] = ledgerr;
                        //        dr["Mainlab"] = mainlab;
                        //        dr["FItLab"] = fitlab;
                        //        dr["Washlab"] = washlab;
                        //        dr["Logolab"] = logolab;



                        //        dCrt.Rows.Add(dr);

                        //    }
                        //}

                    }



                }
                else
                {
                    if (tr1.Visible == true)
                    {
                        if (drpCustomer.SelectedValue == "Select Party Name")
                        {
                        }
                        else
                        {
                            DataRow dr = dCrt.NewRow();

                            dr["Transid"] = dddldesign.SelectedValue;
                            dr["Design"] = dddldesign.SelectedItem.Text;
                            dr["Rate"] = txtDesignRate.Text;

                            dr["meter"] = txtAvailableMtr.Text;
                            dr["Shirt"] = txtNoofShirts.Text;
                            dr["reqmeter"] = txtavamet1.Text;

                            dr["reqshirt"] = txttotshirt1.Text;
                            dr["ledgerid"] = drpCustomer.SelectedValue;
                            dr["party"] = drpCustomer.SelectedItem.Text;
                            dr["Fitid"] = drpFit.SelectedValue;
                            dr["Fit"] = drpFit.SelectedItem.Text;

                            dr["TSFS"] = txt36FS.Text;
                            dr["TSHS"] = txt36HS.Text;

                            dr["TEFS"] = txt38FS.Text;
                            dr["TEHS"] = txt38HS.Text;

                            dr["TNFS"] = txt39FS.Text;
                            dr["TNHS"] = txt39HS.Text;

                            dr["FZFS"] = txt40FS.Text;
                            dr["FZHS"] = txt40HS.Text;

                            dr["FTFS"] = txt42FS.Text;
                            dr["FTHS"] = txt42HS.Text;

                            dr["FFFS"] = txt44FS.Text;
                            dr["FFHS"] = txt44HS.Text;

                            dr["WSP"] = Stxtwsp.Text;
                            dr["avgsize"] = txtavvgmeter.Text;
                            dr["Extra"] = txtextrashirt.Text;

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drplab.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                            }
                            else
                            {

                            }
                            dr["LLedger"] = ledgerr;
                            dr["Mainlab"] = mainlab;
                            dr["FItLab"] = fitlab;
                            dr["Washlab"] = washlab;
                            dr["Logolab"] = logolab;



                            dCrt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        //for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                        //{
                        //    DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                        //    DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                        //    TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                        //    TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                        //    TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                        //    TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                        //    TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                        //    TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                        //    TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                        //    TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                        //    TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                        //    TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                        //    TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                        //    TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                        //    TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                        //    TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
                        //    TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                        //    TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");


                        //    DataRow dr = dCrt.NewRow();

                        //    dr["Transid"] = dddldesign.SelectedValue;
                        //    dr["Design"] = dddldesign.SelectedItem.Text;
                        //    dr["Rate"] = txtDesignRate.Text;

                        //    dr["meter"] = txtAvailableMtr.Text;
                        //    dr["Shirt"] = txtNoofShirts.Text;
                        //    dr["reqmeter"] = txtreqmeter.Text;

                        //    dr["reqshirt"] = txtshirt.Text;
                        //    dr["ledgerid"] = dparty.SelectedValue;
                        //    dr["party"] = dparty.SelectedItem.Text;
                        //    dr["Fitid"] = dfit.SelectedValue;
                        //    dr["Fit"] = dfit.SelectedItem.Text;

                        //    dr["TSFS"] = txt36fs.Text;
                        //    dr["TSHS"] = txt36hs.Text;

                        //    dr["TEFS"] = txt38fs.Text;
                        //    dr["TEHS"] = txt38hs.Text;

                        //    dr["TNFS"] = txt39fs.Text;
                        //    dr["TNHS"] = txt39hs.Text;

                        //    dr["FZFS"] = txt40fs.Text;
                        //    dr["FZHS"] = txt40hs.Text;

                        //    dr["FTFS"] = txt42fs.Text;
                        //    dr["FTHS"] = txt42hs.Text;

                        //    dr["FFFS"] = txt44fs.Text;
                        //    dr["FFHS"] = txt44hs.Text;

                        //    dr["WSP"] = txtwsp.Text;
                        //    dr["avgsize"] = txtavggsize.Text;
                        //    dr["Extra"] = txtextrashirt.Text;

                        //    if (radbtn.SelectedValue == "1")
                        //    {
                        //        ledgerr = ddlSupplier.SelectedValue;
                        //        mainlab = drplab.SelectedValue;
                        //        fitlab = chkfit.Checked;
                        //        washlab = Chkwash.Checked;
                        //        logolab = Chllogo.Checked;
                        //    }
                        //    dr["LLedger"] = ledgerr;
                        //    dr["Mainlab"] = mainlab;
                        //    dr["FItLab"] = fitlab;
                        //    dr["Washlab"] = washlab;
                        //    dr["Logolab"] = logolab;
                        //    dCrt.Rows.Add(dr);

                        //}
                    }
                }

            }
            else if (radcuttype.SelectedValue == "1")
            {
                dCrt = (DataTable)ViewState["Data"];
                if (dCrt.Rows.Count == 0)
                {
                    if (tr1.Visible == true)
                    {
                        if (drpCustomer.SelectedValue == "Select Party Name")
                        {
                        }
                        else
                        {
                            DataRow dr = dCrt.NewRow();

                            dr["Transid"] = dddldesign.SelectedValue;
                            dr["Design"] = dddldesign.SelectedItem.Text;
                            dr["Rate"] = txtDesignRate.Text;

                            dr["meter"] = txtAvailableMtr.Text;
                            dr["Shirt"] = txtNoofShirts.Text;
                            dr["reqmeter"] = txtavamet1.Text;

                            dr["reqshirt"] = txttotshirt1.Text;
                            dr["ledgerid"] = drpCustomer.SelectedValue;
                            dr["party"] = drpCustomer.SelectedItem.Text;
                            dr["Fitid"] = drpFit.SelectedValue;
                            dr["Fit"] = drpFit.SelectedItem.Text;

                            dr["TSFS"] = txt36FS.Text;
                            dr["TSHS"] = txt36HS.Text;

                            dr["TEFS"] = txt38FS.Text;
                            dr["TEHS"] = txt38HS.Text;

                            dr["TNFS"] = txt39FS.Text;
                            dr["TNHS"] = txt39HS.Text;

                            dr["FZFS"] = txt40FS.Text;
                            dr["FZHS"] = txt40HS.Text;

                            dr["FTFS"] = txt42FS.Text;
                            dr["FTHS"] = txt42HS.Text;

                            dr["FFFS"] = txt44FS.Text;
                            dr["FFHS"] = txt44HS.Text;

                            dr["avgsize"] = txtavvgmeter.Text;

                            dr["WSP"] = Stxtwsp.Text;
                            dr["Extra"] = txtextrashirt.Text;

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drplab.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                            }

                            dr["LLedger"] = ledgerr;
                            dr["Mainlab"] = mainlab;
                            dr["FItLab"] = fitlab;
                            dr["Washlab"] = washlab;
                            dr["Logolab"] = logolab;



                            dCrt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                        {
                            DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                            DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                            TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                            TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                            TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                            TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                            TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                            TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                            TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                            TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                            TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                            TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                            TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                            TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                            TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                            TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
                            TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                            TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                            if (dfit.SelectedValue == "Select Fit")
                            {
                            }
                            else
                            {

                                DataRow dr = dCrt.NewRow();

                                dr["Transid"] = dddldesign.SelectedValue;
                                dr["Design"] = dddldesign.SelectedItem.Text;
                                dr["Rate"] = txtDesignRate.Text;

                                dr["meter"] = txtAvailableMtr.Text;
                                dr["Shirt"] = txtNoofShirts.Text;
                                dr["reqmeter"] = txtreqmeter.Text;

                                dr["reqshirt"] = txtshirt.Text;
                                dr["ledgerid"] = dparty.SelectedValue;
                                dr["party"] = dparty.SelectedItem.Text;
                                dr["Fitid"] = dfit.SelectedValue;
                                dr["Fit"] = dfit.SelectedItem.Text;

                                dr["TSFS"] = txt36fs.Text;
                                dr["TSHS"] = txt36hs.Text;

                                dr["TEFS"] = txt38fs.Text;
                                dr["TEHS"] = txt38hs.Text;
                                dr["TNFS"] = txt39fs.Text;
                                dr["TNHS"] = txt39hs.Text;
                                //dr["TNFS"] = txt39FS.Text;
                                //dr["TNHS"] = txt39HS.Text;

                                dr["FZFS"] = txt40fs.Text;
                                dr["FZHS"] = txt40hs.Text;

                                dr["FTFS"] = txt42fs.Text;
                                dr["FTHS"] = txt42hs.Text;

                                dr["FFFS"] = txt44fs.Text;
                                dr["FFHS"] = txt44hs.Text;

                                dr["WSP"] = txtwsp.Text;
                                dr["avgsize"] = txtavggsize.Text;
                                dr["Extra"] = txtextrashirt.Text;
                                if (radbtn.SelectedValue == "1")
                                {
                                    ledgerr = ddlSupplier.SelectedValue;
                                    mainlab = drplab.SelectedValue;
                                    fitlab = chkfit.Checked;
                                    washlab = Chkwash.Checked;
                                    logolab = Chllogo.Checked;
                                }
                                dr["LLedger"] = ledgerr;
                                dr["Mainlab"] = mainlab;
                                dr["FItLab"] = fitlab;
                                dr["Washlab"] = washlab;
                                dr["Logolab"] = logolab;



                                dCrt.Rows.Add(dr);

                            }
                        }

                    }



                }
                else
                {
                    if (tr1.Visible == true)
                    {
                        if (drpCustomer.SelectedValue == "Select Party Name")
                        {
                        }
                        else
                        {
                            DataRow dr = dCrt.NewRow();

                            dr["Transid"] = dddldesign.SelectedValue;
                            dr["Design"] = dddldesign.SelectedItem.Text;
                            dr["Rate"] = txtDesignRate.Text;

                            dr["meter"] = txtAvailableMtr.Text;
                            dr["Shirt"] = txtNoofShirts.Text;
                            dr["reqmeter"] = txtavamet1.Text;

                            dr["reqshirt"] = txttotshirt1.Text;
                            dr["ledgerid"] = drpCustomer.SelectedValue;
                            dr["party"] = drpCustomer.SelectedItem.Text;
                            dr["Fitid"] = drpFit.SelectedValue;
                            dr["Fit"] = drpFit.SelectedItem.Text;

                            dr["TSFS"] = txt36FS.Text;
                            dr["TSHS"] = txt36HS.Text;

                            dr["TEFS"] = txt38FS.Text;
                            dr["TEHS"] = txt38HS.Text;

                            dr["TNFS"] = txt39FS.Text;
                            dr["TNHS"] = txt39HS.Text;

                            dr["FZFS"] = txt40FS.Text;
                            dr["FZHS"] = txt40HS.Text;

                            dr["FTFS"] = txt42FS.Text;
                            dr["FTHS"] = txt42HS.Text;

                            dr["FFFS"] = txt44FS.Text;
                            dr["FFHS"] = txt44HS.Text;

                            dr["WSP"] = Stxtwsp.Text;
                            dr["avgsize"] = txtavvgmeter.Text;
                            dr["Extra"] = txtextrashirt.Text;

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drplab.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                            }
                            else
                            {

                            }
                            dr["LLedger"] = ledgerr;
                            dr["Mainlab"] = mainlab;
                            dr["FItLab"] = fitlab;
                            dr["Washlab"] = washlab;
                            dr["Logolab"] = logolab;



                            dCrt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                        {
                            DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                            DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                            TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                            TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                            TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                            TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                            TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                            TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                            TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                            TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                            TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                            TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                            TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                            TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                            TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                            TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
                            TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                            TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");


                            DataRow dr = dCrt.NewRow();

                            dr["Transid"] = dddldesign.SelectedValue;
                            dr["Design"] = dddldesign.SelectedItem.Text;
                            dr["Rate"] = txtDesignRate.Text;

                            dr["meter"] = txtAvailableMtr.Text;
                            dr["Shirt"] = txtNoofShirts.Text;
                            dr["reqmeter"] = txtreqmeter.Text;

                            dr["reqshirt"] = txtshirt.Text;
                            dr["ledgerid"] = dparty.SelectedValue;
                            dr["party"] = dparty.SelectedItem.Text;
                            dr["Fitid"] = dfit.SelectedValue;
                            dr["Fit"] = dfit.SelectedItem.Text;

                            dr["TSFS"] = txt36fs.Text;
                            dr["TSHS"] = txt36hs.Text;

                            dr["TEFS"] = txt38fs.Text;
                            dr["TEHS"] = txt38hs.Text;

                            dr["TNFS"] = txt39fs.Text;
                            dr["TNHS"] = txt39hs.Text;

                            dr["FZFS"] = txt40fs.Text;
                            dr["FZHS"] = txt40hs.Text;

                            dr["FTFS"] = txt42fs.Text;
                            dr["FTHS"] = txt42hs.Text;

                            dr["FFFS"] = txt44fs.Text;
                            dr["FFHS"] = txt44hs.Text;

                            dr["WSP"] = txtwsp.Text;
                            dr["avgsize"] = txtavggsize.Text;
                            dr["Extra"] = txtextrashirt.Text;

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drplab.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                            }
                            dr["LLedger"] = ledgerr;
                            dr["Mainlab"] = mainlab;
                            dr["FItLab"] = fitlab;
                            dr["Washlab"] = washlab;
                            dr["Logolab"] = logolab;
                            dCrt.Rows.Add(dr);

                        }
                    }
                }
            }

            gvcustomerorder.DataSource = dCrt;
            gvcustomerorder.DataBind();


            dddldesign.ClearSelection();
            txtDesignRate.Text = "";
            txtAvailableMtr.Text = "";
            txtNoofShirts.Text = "";
            txtReqMtr.Text = "";
            txtReqNoShirts.Text = "";
            txtextrashirt.Text = "";
            drpCustomer.ClearSelection();
            drpFit.ClearSelection();
            txt36FS.Text = "0";
            txt36HS.Text = "0";
            txt38FS.Text = "0";
            txt38HS.Text = "0";
            txt40FS.Text = "0";
            txt40HS.Text = "0";
            txt42FS.Text = "0";
            txt42HS.Text = "0";
            txt44FS.Text = "0";
            txt44HS.Text = "0";
            txt39FS.Text = "0";
            txt39HS.Text = "0";
            txtavamet1.Text = "0";
            txttotshirt1.Text = "0";
            txtavvgmeter.Text = "0";
            FirstGridViewRow();
            // removedropdownlist();
            dddldesign.Focus();
            System.Threading.Thread.Sleep(3000);
        }

        public void removedropdownlist()
        {
            DataSet myDS = (DataSet)ViewState["MyDataSet"];

            dCrt = (DataTable)ViewState["Data"];

            dsnneeww.Tables.Add(dCrt);


            if (dsnneeww.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < dsnneeww.Tables[0].Rows.Count; i++)
                {
                    string trainid = dsnneeww.Tables[0].Rows[i]["Transid"].ToString();
                    if (myDS.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < myDS.Tables[0].Rows.Count; j++)
                        {

                            string idd = myDS.Tables[0].Rows[j]["id"].ToString();
                            if (idd == trainid)
                            {
                                dddldesign.Items.Remove(dddldesign.Items.FindByValue(idd));
                                // dddldesign.Items.Remove(dddldesign.Items[i]);

                            }
                        }

                    }
                }
            }






            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox lbltransid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
            //    if (myDS.Tables[0].Rows.Count > 0)
            //    {
            //        for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
            //        {

            //            string idd = myDS.Tables[0].Rows[i]["design"].ToString();
            //            if (idd == lbltransid.Text)
            //            {
            //                //dddldesign.Items.Remove(dddldesign.Items.FindByValue(idd));
            //                dddldesign.Items.Remove(dddldesign.Items[i]);

            //            }
            //        }

            //    }

            //}
        }


        protected void Addfirst(object sender, EventArgs e)
        {
            //tr2.Visible = true;
            // tr3.Visible = false;
        }

        protected void Addsecond(object sender, EventArgs e)
        {

            // tr3.Visible = true;
        }
        protected void ddpfitindexchanged(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;


            double re = 0.00;
            double r1 = 0.00;

            double rr = 0.00;
            double rb = 0.00;


            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dbrand = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrbrand");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;

                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    if (roundoff > 0.5)
                    {
                        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        r = Math.Floor(Convert.ToDouble(roundoff));
                    }

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //   txt36fs.Focus();
                if (gridsize.Columns[2].Visible == true) //38FS
                {
                    if (txt36fs.Text == "0" || txt36fs.Text == "")
                    {
                        txt36fs.Text = "";
                    }
                    txt36fs.Focus();
                }
                else if (gridsize.Columns[3].Visible == true) //38FS
                {
                    if (txt38fs.Text == "0" || txt38fs.Text == "")
                    {
                        txt38fs.Text = "";
                    }
                    txt38fs.Focus();
                }
                else if (gridsize.Columns[4].Visible == true)//39Fs
                {
                    if (txt39fs.Text == "0" || txt39fs.Text == "")
                    {
                        txt39fs.Text = "";
                    }

                    txt39fs.Focus();
                }
                else if (gridsize.Columns[5].Visible == true)//40Fs
                {
                    if (txt40fs.Text == "0" || txt40fs.Text == "")
                    {
                        txt40fs.Text = "";
                    }

                    txt40fs.Focus();
                }

                else if (gridsize.Columns[6].Visible == true) //42FS
                {
                    if (txt42fs.Text == "0" || txt42fs.Text == "")
                    {
                        txt42fs.Text = "";
                    }

                    txt42fs.Focus();
                }
                else if (gridsize.Columns[7].Visible == true) //44FS
                {
                    if (txt44fs.Text == "0" || txt44fs.Text == "")
                    {
                        txt44fs.Text = "";
                    }

                    txt44fs.Focus();
                }

                else if (gridsize.Columns[8].Visible == true) //36HS
                {
                    if (txt36hs.Text == "0" || txt36hs.Text == "")
                    {
                        txt36hs.Text = "";
                    }

                    txt36hs.Focus();
                }
                else if (gridsize.Columns[9].Visible == true) //38HS
                {
                    if (txt38hs.Text == "0" || txt38hs.Text == "")
                    {
                        txt38hs.Text = "";
                    }

                    txt38hs.Focus();
                }

                else if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }


                DataSet dteo = objBs.getcutlistdesignfortrans(dddldesign.SelectedValue);
                if (dteo.Tables[0].Rows.Count > 0)
                {
                    txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
                    txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                    if (txtReqMtr.Text == "")
                    {
                        txtReqMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                    }
                    else
                    {

                    }

                    DataSet dcalculate1 = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                    if (dcalculate1.Tables[0].Rows.Count > 0)
                    {

                        double wid = Convert.ToDouble(dcalculate1.Tables[0].Rows[0]["width"]);

                        double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                        double roundoff1 = Convert.ToDouble(txtReqMtr.Text) / wid;
                        if (roundoff > 0.5)
                        {
                            re = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            re = Math.Floor(Convert.ToDouble(roundoff));
                        }

                        if (roundoff1 > 0.5)
                        {
                            r1 = Math.Round(Convert.ToDouble(roundoff1), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            r1 = Math.Floor(Convert.ToDouble(roundoff1));
                        }

                    }
                    txtNoofShirts.Text = re.ToString();
                    txtReqNoShirts.Text = r1.ToString();
                }
                rr = ((re * 2) / 100);
                if (rr > 0.5)
                {
                    rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
                }
                else
                {
                    rb = Math.Floor(Convert.ToDouble(rr));
                }
                txtextrashirt.Text = rb.ToString();

                // dparty.Focus();
            }


            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            // if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                btnadd.Enabled = false;
                //   btnprocess.Enabled = false;
                return;
            }

        }

        public void getzeroforemptysize()
        {
            if (Sddrrpfit.SelectedValue == "3")
            {

                if (txt36FS.Text == "")
                {
                    txt36FS.Text = "0";
                }
                if (txt38FS.Text == "")
                {
                    txt38FS.Text = "0";
                }
                if (txt39FS.Text == "")
                {
                    txt39FS.Text = "0";
                }
                if (txt40FS.Text == "")
                {
                    txt40FS.Text = "0";
                }
                if (txt42FS.Text == "")
                {
                    txt42FS.Text = "0";
                }
                if (txt44FS.Text == "")
                {
                    txt44FS.Text = "0";
                }
                if (txt36HS.Text == "")
                {
                    txt36HS.Text = "0";
                }
                if (txt38HS.Text == "")
                {
                    txt38HS.Text = "0";
                }
                if (txt39HS.Text == "")
                {
                    txt39HS.Text = "0";
                }
                if (txt40HS.Text == "")
                {
                    txt40HS.Text = "0";
                }
                if (txt42HS.Text == "")
                {
                    txt42HS.Text = "0";
                }
                if (txt44HS.Text == "")
                {
                    txt44HS.Text = "0";
                }
            }
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt28FS3.Text == "")
                {
                    txt28FS3.Text = "0";
                }
                if (txt30FS3.Text == "")
                {
                    txt30FS3.Text = "0";
                }
                if (txt32FS3.Text == "")
                {
                    txt32FS3.Text = "0";
                }
                if (txt34FS3.Text == "")
                {
                    txt34FS3.Text = "0";
                }
                if (txt36FS3.Text == "")
                {
                    txt36FS3.Text = "0";
                }
                if (txt38FS3.Text == "")
                {
                    txt38FS3.Text = "0";
                }

                if (txt28HS3.Text == "")
                {
                    txt28HS3.Text = "0";
                }
                if (txt30HS3.Text == "")
                {
                    txt30HS3.Text = "0";
                }
                if (txt32HS3.Text == "")
                {
                    txt32HS3.Text = "0";
                }
                if (txt34HS3.Text == "")
                {
                    txt34HS3.Text = "0";
                }
                if (txt36HS3.Text == "")
                {
                    txt36HS3.Text = "0";
                }
                if (txt38HS3.Text == "")
                {
                    txt38HS3.Text = "0";
                }
            }
            else if (Sddrrpfit.SelectedValue == "5")
            {
                if (txtSFS2.Text == "")
                {
                    txtSFS2.Text = "0";
                }
                if (txtMFS2.Text == "")
                {
                    txtMFS2.Text = "0";
                }
                if (txtLFS2.Text == "")
                {
                    txtLFS2.Text = "0";
                }
                if (txtXLFS2.Text == "")
                {
                    txtXLFS2.Text = "0";
                }
                if (txtXXLFS2.Text == "")
                {
                    txtXXLFS2.Text = "0";
                }
                //if (txt44FS.Text == "")
                //{
                //    txt44FS.Text = "0";
                //}
                if (txtSHS2.Text == "")
                {
                    txtSHS2.Text = "0";
                }
                if (txtMHS2.Text == "")
                {
                    txtMHS2.Text = "0";
                }
                if (txtLHS2.Text == "")
                {
                    txtLHS2.Text = "0";
                }
                if (txtXLHS2.Text == "")
                {
                    txtXLHS2.Text = "0";
                }
                if (txtXXLHS2.Text == "")
                {
                    txtXXLHS2.Text = "0";
                }
                //if (txt44HS.Text == "")
                //{
                //    txt44HS.Text = "0";
                //}
            }
        }
        //Single Process Formal
        


        protected void Schange36fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt36FS.Text == "0" || txt36FS.Text == "")
                {
                    txt36FS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                if (tefs.Visible == true)
                {
                    txt38FS.Focus();
                    if (txt38FS.Text == "0")
                    {
                        txt38FS.Text = "";
                    }
                }
                else if (tnfs.Visible == true)
                {
                    txt39FS.Focus();
                    if (txt39FS.Text == "0")
                    {
                        txt39FS.Text = "";
                    }
                }
                else if (fzfs.Visible == true)
                {
                    txt40FS.Focus();
                    if (txt40FS.Text == "0")
                    {
                        txt40FS.Text = "";
                    }
                }
                else if (ftfs.Visible == true)
                {
                    txt42FS.Focus();
                    if (txt42FS.Text == "0")
                    {
                        txt42FS.Text = "";
                    }
                }
                else if (fffs.Visible == true)
                {
                    txt44FS.Focus();
                    if (txt44FS.Text == "0")
                    {
                        txt44FS.Text = "";
                    }
                }
                else if (tshs.Visible == true)
                {
                    txt36HS.Focus();
                    if (txt36HS.Text == "0")
                    {
                        txt36HS.Text = "";
                    }
                }

                else if (tehs.Visible == true)
                {
                    txt38HS.Focus();
                    if (txt38HS.Text == "0")
                    {
                        txt38HS.Text = "";
                    }
                }
                else if (tnhs.Visible == true)
                {
                    txt39HS.Focus();
                    if (txt39HS.Text == "0")
                    {
                        txt39HS.Text = "";
                    }
                }
                else if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
                //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt28FS3.Text == "0" || txt28FS3.Text == "")
                {
                    txt28FS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                if (b30fs.Visible == true)
                {
                    txt30FS3.Focus();
                    if (txt30FS3.Text == "0")
                    {
                        txt30FS3.Text = "";
                    }
                }
                else if (b32fs.Visible == true)
                {
                    txt32FS3.Focus();
                    if (txt32FS3.Text == "0")
                    {
                        txt32FS3.Text = "";
                    }
                }
                else if (b34fs.Visible == true)
                {
                    txt34FS3.Focus();
                    if (txt34FS3.Text == "0")
                    {
                        txt34FS3.Text = "";
                    }
                }
                else if (b36fs.Visible == true)
                {
                    txt36FS3.Focus();
                    if (txt36FS3.Text == "0")
                    {
                        txt36FS3.Text = "";
                    }
                }
                else if (b38fs.Visible == true)
                {
                    txt38FS3.Focus();
                    if (txt38FS3.Text == "0")
                    {
                        txt38FS3.Text = "";
                    }
                }
                else if (b28hs.Visible == true)
                {
                    txt28HS3.Focus();
                    if (txt28HS3.Text == "0")
                    {
                        txt28HS3.Text = "";
                    }
                }
                else if (b30hs.Visible == true)
                {
                    txt30HS3.Focus();
                    if (txt30HS3.Text == "0")
                    {
                        txt30HS3.Text = "";
                    }
                }

                else if (b32hs.Visible == true)
                {
                    txt32HS3.Focus();
                    if (txt32HS3.Text == "0")
                    {
                        txt32HS3.Text = "";
                    }
                }
                else if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }
                
            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtSFS2.Text == "0" || txtSFS2.Text == "")
                {
                    txtSFS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

                if (mfs.Visible == true)
                {
                    txtMFS2.Focus();
                    if (txtMFS2.Text == "0")
                    {
                        txtMFS2.Text = "";
                    }
                }
                else if (lfs.Visible == true)
                {
                    txtLFS2.Focus();
                    if (txtLFS2.Text == "0")
                    {
                        txtLFS2.Text = "";
                    }
                }
                else if (xlfs.Visible == true)
                {
                    txtXLFS2.Focus();
                    if (txtXLFS2.Text == "0")
                    {
                        txtXLFS2.Text = "";
                    }
                }
                else if (xxlfs.Visible == true)
                {
                    txtXXLFS2.Focus();
                    if (txtXXLFS2.Text == "0")
                    {
                        txtXXLFS2.Text = "";
                    }
                }
                //else if (fffs.Visible == true)
                //{
                //    txt44FS.Focus();
                //    if (txt44FS.Text == "0")
                //    {
                //        txt44FS.Text = "";
                //    }
                //}
                else if (shs.Visible == true)
                {
                    txtSHS2.Focus();
                    if (txtSHS2.Text == "0")
                    {
                        txtSHS2.Text = "";
                    }
                }

                else if (mhs.Visible == true)
                {
                    txtMHS2.Focus();
                    if (txtMHS2.Text == "0")
                    {
                        txtMHS2.Text = "";
                    }
                }
                else if (lhs.Visible == true)
                {
                    txtLHS2.Focus();
                    if (txtLHS2.Text == "0")
                    {
                        txtLHS2.Text = "";
                    }
                }
                else if (xlhs.Visible == true)
                {
                    txtXLHS2.Focus();
                    if (txtXLHS2.Text == "0")
                    {
                        txtXLHS2.Text = "";
                    }
                }
                else if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }

          
            System.Threading.Thread.Sleep(30);
        }

        protected void Schange38fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt38FS.Text == "0" || txt38FS.Text == "")
                {
                    txt38FS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                //if (tsfs.Visible == true)
                //{
                //    txt36FS.Focus();
                //    if (txt36FS.Text == "0")
                //    {
                //        txt36FS.Text = "";
                //    }
                //}
                 if (tnfs.Visible == true)
                {
                    txt39FS.Focus();
                    if (txt39FS.Text == "0")
                    {
                        txt39FS.Text = "";
                    }
                }
                else if (fzfs.Visible == true)
                {
                    txt40FS.Focus();
                    if (txt40FS.Text == "0")
                    {
                        txt40FS.Text = "";
                    }
                }
                else if (ftfs.Visible == true)
                {
                    txt42FS.Focus();
                    if (txt42FS.Text == "0")
                    {
                        txt42FS.Text = "";
                    }
                }
                else if (fffs.Visible == true)
                {
                    txt44FS.Focus();
                    if (txt44FS.Text == "0")
                    {
                        txt44FS.Text = "";
                    }
                }
                else if (tshs.Visible == true)
                {
                    txt36HS.Focus();
                    if (txt36HS.Text == "0")
                    {
                        txt36HS.Text = "";
                    }
                }

                else if (tehs.Visible == true)
                {
                    txt38HS.Focus();
                    if (txt38HS.Text == "0")
                    {
                        txt38HS.Text = "";
                    }
                }
                else if (tnhs.Visible == true)
                {
                    txt39HS.Focus();
                    if (txt39HS.Text == "0")
                    {
                        txt39HS.Text = "";
                    }
                }
                else if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt30FS3.Text == "0" || txt30FS3.Text == "")
                {
                    txt30FS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                //if (b28fs.Visible == true)
                //{
                //    txt28FS3.Focus();
                //    if (txt28FS3.Text == "0")
                //    {
                //        txt28FS3.Text = "";
                //    }
                //}
                 if (b32fs.Visible == true)
                {
                    txt32FS3.Focus();
                    if (txt32FS3.Text == "0")
                    {
                        txt32FS3.Text = "";
                    }
                }
                else if (b34fs.Visible == true)
                {
                    txt34FS3.Focus();
                    if (txt34FS3.Text == "0")
                    {
                        txt34FS3.Text = "";
                    }
                }
                else if (b36fs.Visible == true)
                {
                    txt36FS3.Focus();
                    if (txt36FS3.Text == "0")
                    {
                        txt36FS3.Text = "";
                    }
                }
                else if (b38fs.Visible == true)
                {
                    txt38FS3.Focus();
                    if (txt38FS3.Text == "0")
                    {
                        txt38FS3.Text = "";
                    }
                }
                else if (b28hs.Visible == true)
                {
                    txt28HS3.Focus();
                    if (txt28HS3.Text == "0")
                    {
                        txt28HS3.Text = "";
                    }
                }
                else if (b30hs.Visible == true)
                {
                    txt30HS3.Focus();
                    if (txt30HS3.Text == "0")
                    {
                        txt30HS3.Text = "";
                    }
                }

                else if (b32hs.Visible == true)
                {
                    txt32HS3.Focus();
                    if (txt32HS3.Text == "0")
                    {
                        txt32HS3.Text = "";
                    }
                }
                else if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtMFS2.Text == "0" || txtMFS2.Text == "")
                {
                    txtMFS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

                //if (sfs.Visible == true)
                //{
                //    txtSFS2.Focus();
                //    if (txtSFS2.Text == "0")
                //    {
                //        txtSFS2.Text = "";
                //    }
                //}
                 if (lfs.Visible == true)
                {
                    txtLFS2.Focus();
                    if (txtLFS2.Text == "0")
                    {
                        txtLFS2.Text = "";
                    }
                }
                else if (xlfs.Visible == true)
                {
                    txtXLFS2.Focus();
                    if (txtXLFS2.Text == "0")
                    {
                        txtXLFS2.Text = "";
                    }
                }
                else if (xxlfs.Visible == true)
                {
                    txtXXLFS2.Focus();
                    if (txtXXLFS2.Text == "0")
                    {
                        txtXXLFS2.Text = "";
                    }
                }
                //else if (fffs.Visible == true)
                //{
                //    txt44FS.Focus();
                //    if (txt44FS.Text == "0")
                //    {
                //        txt44FS.Text = "";
                //    }
                //}
                else if (shs.Visible == true)
                {
                    txtSHS2.Focus();
                    if (txtSHS2.Text == "0")
                    {
                        txtSHS2.Text = "";
                    }
                }

                else if (mhs.Visible == true)
                {
                    txtMHS2.Focus();
                    if (txtMHS2.Text == "0")
                    {
                        txtMHS2.Text = "";
                    }
                }
                else if (lhs.Visible == true)
                {
                    txtLHS2.Focus();
                    if (txtLHS2.Text == "0")
                    {
                        txtLHS2.Text = "";
                    }
                }
                else if (xlhs.Visible == true)
                {
                    txtXLHS2.Focus();
                    if (txtXLHS2.Text == "0")
                    {
                        txtXLHS2.Text = "";
                    }
                }
                else if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }


            System.Threading.Thread.Sleep(30);
        }

        protected void Schange39fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt39FS.Text == "0" || txt39FS.Text == "")
                {
                    txt39FS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                //if (tsfs.Visible == true)
                //{
                //    txt36FS.Focus();
                //    if (txt36FS.Text == "0")
                //    {
                //        txt36FS.Text = "";
                //    }
                //}
                //else if (tefs.Visible == true)
                //{
                //    txt38FS.Focus();
                //    if (txt38FS.Text == "0")
                //    {
                //        txt38FS.Text = "";
                //    }
                //}
                 if (fzfs.Visible == true)
                {
                    txt40FS.Focus();
                    if (txt40FS.Text == "0")
                    {
                        txt40FS.Text = "";
                    }
                }
                else if (ftfs.Visible == true)
                {
                    txt42FS.Focus();
                    if (txt42FS.Text == "0")
                    {
                        txt42FS.Text = "";
                    }
                }
                else if (fffs.Visible == true)
                {
                    txt44FS.Focus();
                    if (txt44FS.Text == "0")
                    {
                        txt44FS.Text = "";
                    }
                }
                else if (tshs.Visible == true)
                {
                    txt36HS.Focus();
                    if (txt36HS.Text == "0")
                    {
                        txt36HS.Text = "";
                    }
                }

                else if (tehs.Visible == true)
                {
                    txt38HS.Focus();
                    if (txt38HS.Text == "0")
                    {
                        txt38HS.Text = "";
                    }
                }
                else if (tnhs.Visible == true)
                {
                    txt39HS.Focus();
                    if (txt39HS.Text == "0")
                    {
                        txt39HS.Text = "";
                    }
                }
                else if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt32FS3.Text == "0" || txt32FS3.Text == "")
                {
                    txt32FS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                //if (b28fs.Visible == true)
                //{
                //    txt28FS3.Focus();
                //    if (txt28FS3.Text == "0")
                //    {
                //        txt28FS3.Text = "";
                //    }
                //}
                //else if (b30fs.Visible == true)
                //{
                //    txt30FS3.Focus();
                //    if (txt30FS3.Text == "0")
                //    {
                //        txt30FS3.Text = "";
                //    }
                //}
                if (b34fs.Visible == true)
                {
                    txt34FS3.Focus();
                    if (txt34FS3.Text == "0")
                    {
                        txt34FS3.Text = "";
                    }
                }
                else if (b36fs.Visible == true)
                {
                    txt36FS3.Focus();
                    if (txt36FS3.Text == "0")
                    {
                        txt36FS3.Text = "";
                    }
                }
                else if (b38fs.Visible == true)
                {
                    txt38FS3.Focus();
                    if (txt38FS3.Text == "0")
                    {
                        txt38FS3.Text = "";
                    }
                }
                else if (b28hs.Visible == true)
                {
                    txt28HS3.Focus();
                    if (txt28HS3.Text == "0")
                    {
                        txt28HS3.Text = "";
                    }
                }
                else if (b30hs.Visible == true)
                {
                    txt30HS3.Focus();
                    if (txt30HS3.Text == "0")
                    {
                        txt30HS3.Text = "";
                    }
                }

                else if (b32hs.Visible == true)
                {
                    txt32HS3.Focus();
                    if (txt32HS3.Text == "0")
                    {
                        txt32HS3.Text = "";
                    }
                }
                else if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtLFS2.Text == "0" || txtLFS2.Text == "")
                {
                    txtLFS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

                //if (sfs.Visible == true)
                //{
                //    txtSFS2.Focus();
                //    if (txtSFS2.Text == "0")
                //    {
                //        txtSFS2.Text = "";
                //    }
                //}
                //else if (mfs.Visible == true)
                //{
                //    txtMFS2.Focus();
                //    if (txtMFS2.Text == "0")
                //    {
                //        txtMFS2.Text = "";
                //    }
                //}
                 if (xlfs.Visible == true)
                {
                    txtXLFS2.Focus();
                    if (txtXLFS2.Text == "0")
                    {
                        txtXLFS2.Text = "";
                    }
                }
                else if (xxlfs.Visible == true)
                {
                    txtXXLFS2.Focus();
                    if (txtXXLFS2.Text == "0")
                    {
                        txtXXLFS2.Text = "";
                    }
                }
                //else if (fffs.Visible == true)
                //{
                //    txt44FS.Focus();
                //    if (txt44FS.Text == "0")
                //    {
                //        txt44FS.Text = "";
                //    }
                //}
                else if (shs.Visible == true)
                {
                    txtSHS2.Focus();
                    if (txtSHS2.Text == "0")
                    {
                        txtSHS2.Text = "";
                    }
                }

                else if (mhs.Visible == true)
                {
                    txtMHS2.Focus();
                    if (txtMHS2.Text == "0")
                    {
                        txtMHS2.Text = "";
                    }
                }
                else if (lhs.Visible == true)
                {
                    txtLHS2.Focus();
                    if (txtLHS2.Text == "0")
                    {
                        txtLHS2.Text = "";
                    }
                }
                else if (xlhs.Visible == true)
                {
                    txtXLHS2.Focus();
                    if (txtXLHS2.Text == "0")
                    {
                        txtXLHS2.Text = "";
                    }
                }
                else if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }


            System.Threading.Thread.Sleep(30);
        }

        protected void Schange40fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt40FS.Text == "0" || txt40FS.Text == "")
                {
                    txt40FS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                //if (tsfs.Visible == true)
                //{
                //    txt36FS.Focus();
                //    if (txt36FS.Text == "0")
                //    {
                //        txt36FS.Text = "";
                //    }
                //}
                //else if (tefs.Visible == true)
                //{
                //    txt38FS.Focus();
                //    if (txt38FS.Text == "0")
                //    {
                //        txt38FS.Text = "";
                //    }
                //}
                //else if (tnfs.Visible == true)
                //{
                //    txt39FS.Focus();
                //    if (txt39FS.Text == "0")
                //    {
                //        txt39FS.Text = "";
                //    }
                //}
                 if (ftfs.Visible == true)
                {
                    txt42FS.Focus();
                    if (txt42FS.Text == "0")
                    {
                        txt42FS.Text = "";
                    }
                }
                else if (fffs.Visible == true)
                {
                    txt44FS.Focus();
                    if (txt44FS.Text == "0")
                    {
                        txt44FS.Text = "";
                    }
                }
                else if (tshs.Visible == true)
                {
                    txt36HS.Focus();
                    if (txt36HS.Text == "0")
                    {
                        txt36HS.Text = "";
                    }
                }

                else if (tehs.Visible == true)
                {
                    txt38HS.Focus();
                    if (txt38HS.Text == "0")
                    {
                        txt38HS.Text = "";
                    }
                }
                else if (tnhs.Visible == true)
                {
                    txt39HS.Focus();
                    if (txt39HS.Text == "0")
                    {
                        txt39HS.Text = "";
                    }
                }
                else if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt34FS3.Text == "0" || txt34FS3.Text == "")
                {
                    txt34FS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                //if (b28fs.Visible == true)
                //{
                //    txt28FS3.Focus();
                //    if (txt28FS3.Text == "0")
                //    {
                //        txt28FS3.Text = "";
                //    }
                //}
                //else if (b30fs.Visible == true)
                //{
                //    txt30FS3.Focus();
                //    if (txt30FS3.Text == "0")
                //    {
                //        txt30FS3.Text = "";
                //    }
                //}
                //else if (b32fs.Visible == true)
                //{
                //    txt32FS3.Focus();
                //    if (txt32FS3.Text == "0")
                //    {
                //        txt32FS3.Text = "";
                //    }
                //}
                 if (b36fs.Visible == true)
                {
                    txt36FS3.Focus();
                    if (txt36FS3.Text == "0")
                    {
                        txt36FS3.Text = "";
                    }
                }
                else if (b38fs.Visible == true)
                {
                    txt38FS3.Focus();
                    if (txt38FS3.Text == "0")
                    {
                        txt38FS3.Text = "";
                    }
                }
                else if (b28hs.Visible == true)
                {
                    txt28HS3.Focus();
                    if (txt28HS3.Text == "0")
                    {
                        txt28HS3.Text = "";
                    }
                }
                else if (b30hs.Visible == true)
                {
                    txt30HS3.Focus();
                    if (txt30HS3.Text == "0")
                    {
                        txt30HS3.Text = "";
                    }
                }

                else if (b32hs.Visible == true)
                {
                    txt32HS3.Focus();
                    if (txt32HS3.Text == "0")
                    {
                        txt32HS3.Text = "";
                    }
                }
                else if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtXLFS2.Text == "0" || txtXLFS2.Text == "")
                {
                    txtXLFS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

                //if (sfs.Visible == true)
                //{
                //    txtSFS2.Focus();
                //    if (txtSFS2.Text == "0")
                //    {
                //        txtSFS2.Text = "";
                //    }
                //}
                //else if (mfs.Visible == true)
                //{
                //    txtMFS2.Focus();
                //    if (txtMFS2.Text == "0")
                //    {
                //        txtMFS2.Text = "";
                //    }
                //}
                //else if (lfs.Visible == true)
                //{
                //    txtLFS2.Focus();
                //    if (txtLFS2.Text == "0")
                //    {
                //        txtLFS2.Text = "";
                //    }
                //}
                 if (xxlfs.Visible == true)
                {
                    txtXXLFS2.Focus();
                    if (txtXXLFS2.Text == "0")
                    {
                        txtXXLFS2.Text = "";
                    }
                }
                //else if (fffs.Visible == true)
                //{
                //    txt44FS.Focus();
                //    if (txt44FS.Text == "0")
                //    {
                //        txt44FS.Text = "";
                //    }
                //}
                else if (shs.Visible == true)
                {
                    txtSHS2.Focus();
                    if (txtSHS2.Text == "0")
                    {
                        txtSHS2.Text = "";
                    }
                }

                else if (mhs.Visible == true)
                {
                    txtMHS2.Focus();
                    if (txtMHS2.Text == "0")
                    {
                        txtMHS2.Text = "";
                    }
                }
                else if (lhs.Visible == true)
                {
                    txtLHS2.Focus();
                    if (txtLHS2.Text == "0")
                    {
                        txtLHS2.Text = "";
                    }
                }
                else if (xlhs.Visible == true)
                {
                    txtXLHS2.Focus();
                    if (txtXLHS2.Text == "0")
                    {
                        txtXLHS2.Text = "";
                    }
                }
                else if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }


            System.Threading.Thread.Sleep(30);
        }

        protected void Schange42fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt42FS.Text == "0" || txt42FS.Text == "")
                {
                    txt42FS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                //if (tsfs.Visible == true)
                //{
                //    txt36FS.Focus();
                //    if (txt36FS.Text == "0")
                //    {
                //        txt36FS.Text = "";
                //    }
                //}
                //else if (tefs.Visible == true)
                //{
                //    txt38FS.Focus();
                //    if (txt38FS.Text == "0")
                //    {
                //        txt38FS.Text = "";
                //    }
                //}
                //else if (tnfs.Visible == true)
                //{
                //    txt39FS.Focus();
                //    if (txt39FS.Text == "0")
                //    {
                //        txt39FS.Text = "";
                //    }
                //}
                //else if (fzfs.Visible == true)
                //{
                //    txt40FS.Focus();
                //    if (txt40FS.Text == "0")
                //    {
                //        txt40FS.Text = "";
                //    }
                //}
                 if (fffs.Visible == true)
                {
                    txt44FS.Focus();
                    if (txt44FS.Text == "0")
                    {
                        txt44FS.Text = "";
                    }
                }
                else if (tshs.Visible == true)
                {
                    txt36HS.Focus();
                    if (txt36HS.Text == "0")
                    {
                        txt36HS.Text = "";
                    }
                }

                else if (tehs.Visible == true)
                {
                    txt38HS.Focus();
                    if (txt38HS.Text == "0")
                    {
                        txt38HS.Text = "";
                    }
                }
                else if (tnhs.Visible == true)
                {
                    txt39HS.Focus();
                    if (txt39HS.Text == "0")
                    {
                        txt39HS.Text = "";
                    }
                }
                else if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt36FS3.Text == "0" || txt36FS3.Text == "")
                {
                    txt36FS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                //if (b28fs.Visible == true)
                //{
                //    txt28FS3.Focus();
                //    if (txt28FS3.Text == "0")
                //    {
                //        txt28FS3.Text = "";
                //    }
                //}
                //else if (b30fs.Visible == true)
                //{
                //    txt30FS3.Focus();
                //    if (txt30FS3.Text == "0")
                //    {
                //        txt30FS3.Text = "";
                //    }
                //}
                //else if (b32fs.Visible == true)
                //{
                //    txt32FS3.Focus();
                //    if (txt32FS3.Text == "0")
                //    {
                //        txt32FS3.Text = "";
                //    }
                //}
                //else if (b34fs.Visible == true)
                //{
                //    txt34FS3.Focus();
                //    if (txt34FS3.Text == "0")
                //    {
                //        txt34FS3.Text = "";
                //    }
                //}
                 if (b38fs.Visible == true)
                {
                    txt38FS3.Focus();
                    if (txt38FS3.Text == "0")
                    {
                        txt38FS3.Text = "";
                    }
                }
                else if (b28hs.Visible == true)
                {
                    txt28HS3.Focus();
                    if (txt28HS3.Text == "0")
                    {
                        txt28HS3.Text = "";
                    }
                }
                else if (b30hs.Visible == true)
                {
                    txt30HS3.Focus();
                    if (txt30HS3.Text == "0")
                    {
                        txt30HS3.Text = "";
                    }
                }

                else if (b32hs.Visible == true)
                {
                    txt32HS3.Focus();
                    if (txt32HS3.Text == "0")
                    {
                        txt32HS3.Text = "";
                    }
                }
                else if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtXXLFS2.Text == "0" || txtXXLFS2.Text == "")
                {
                    txtXXLFS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

                //if (sfs.Visible == true)
                //{
                //    txtSFS2.Focus();
                //    if (txtSFS2.Text == "0")
                //    {
                //        txtSFS2.Text = "";
                //    }
                //}
                //else if (mfs.Visible == true)
                //{
                //    txtMFS2.Focus();
                //    if (txtMFS2.Text == "0")
                //    {
                //        txtMFS2.Text = "";
                //    }
                //}
                //else if (lfs.Visible == true)
                //{
                //    txtLFS2.Focus();
                //    if (txtLFS2.Text == "0")
                //    {
                //        txtLFS2.Text = "";
                //    }
                //}
                //else if (xlfs.Visible == true)
                //{
                //    txtXLFS2.Focus();
                //    if (txtXLFS2.Text == "0")
                //    {
                //        txtXXLFS2.Text = "";
                //    }
                //}
                //else if (fffs.Visible == true)
                //{
                //    txt44FS.Focus();
                //    if (txt44FS.Text == "0")
                //    {
                //        txt44FS.Text = "";
                //    }
                //}
                if (shs.Visible == true)
                {
                    txtSHS2.Focus();
                    if (txtSHS2.Text == "0")
                    {
                        txtSHS2.Text = "";
                    }
                }

                else if (mhs.Visible == true)
                {
                    txtMHS2.Focus();
                    if (txtMHS2.Text == "0")
                    {
                        txtMHS2.Text = "";
                    }
                }
                else if (lhs.Visible == true)
                {
                    txtLHS2.Focus();
                    if (txtLHS2.Text == "0")
                    {
                        txtLHS2.Text = "";
                    }
                }
                else if (xlhs.Visible == true)
                {
                    txtXLHS2.Focus();
                    if (txtXLHS2.Text == "0")
                    {
                        txtXLHS2.Text = "";
                    }
                }
                else if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }


            System.Threading.Thread.Sleep(30);
        }

        protected void Schange44fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt44FS.Text == "0" || txt44FS.Text == "")
                {
                    txt44FS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                //if (tsfs.Visible == true)
                //{
                //    txt36FS.Focus();
                //    if (txt36FS.Text == "0")
                //    {
                //        txt36FS.Text = "";
                //    }
                //}
                //else if (tefs.Visible == true)
                //{
                //    txt38FS.Focus();
                //    if (txt38FS.Text == "0")
                //    {
                //        txt38FS.Text = "";
                //    }
                //}
                //else if (tnfs.Visible == true)
                //{
                //    txt39FS.Focus();
                //    if (txt39FS.Text == "0")
                //    {
                //        txt39FS.Text = "";
                //    }
                //}
                //else if (fzfs.Visible == true)
                //{
                //    txt40FS.Focus();
                //    if (txt40FS.Text == "0")
                //    {
                //        txt40FS.Text = "";
                //    }
                //}
                //else if (ftfs.Visible == true)
                //{
                //    txt42FS.Focus();
                //    if (txt42FS.Text == "0")
                //    {
                //        txt42FS.Text = "";
                //    }
                //}
                if (tshs.Visible == true)
                {
                    txt36HS.Focus();
                    if (txt36HS.Text == "0")
                    {
                        txt36HS.Text = "";
                    }
                }

                else if (tehs.Visible == true)
                {
                    txt38HS.Focus();
                    if (txt38HS.Text == "0")
                    {
                        txt38HS.Text = "";
                    }
                }
                else if (tnhs.Visible == true)
                {
                    txt39HS.Focus();
                    if (txt39HS.Text == "0")
                    {
                        txt39HS.Text = "";
                    }
                }
                else if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt38FS3.Text == "0" || txt38FS3.Text == "")
                {
                    txt38FS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                //if (b28fs.Visible == true)
                //{
                //    txt28FS3.Focus();
                //    if (txt28FS3.Text == "0")
                //    {
                //        txt28FS3.Text = "";
                //    }
                //}
                //else if (b30fs.Visible == true)
                //{
                //    txt30FS3.Focus();
                //    if (txt30FS3.Text == "0")
                //    {
                //        txt30FS3.Text = "";
                //    }
                //}
                //else if (b32fs.Visible == true)
                //{
                //    txt32FS3.Focus();
                //    if (txt32FS3.Text == "0")
                //    {
                //        txt32FS3.Text = "";
                //    }
                //}
                //else if (b34fs.Visible == true)
                //{
                //    txt34FS3.Focus();
                //    if (txt34FS3.Text == "0")
                //    {
                //        txt34FS3.Text = "";
                //    }
                //}
                //else if (b36fs.Visible == true)
                //{
                //    txt36FS3.Focus();
                //    if (txt36FS3.Text == "0")
                //    {
                //        txt36FS3.Text = "";
                //    }
                //}
                 if (b28hs.Visible == true)
                {
                    txt28HS3.Focus();
                    if (txt28HS3.Text == "0")
                    {
                        txt28HS3.Text = "";
                    }
                }
                else if (b30hs.Visible == true)
                {
                    txt30HS3.Focus();
                    if (txt30HS3.Text == "0")
                    {
                        txt30HS3.Text = "";
                    }
                }

                else if (b32hs.Visible == true)
                {
                    txt32HS3.Focus();
                    if (txt32HS3.Text == "0")
                    {
                        txt32HS3.Text = "";
                    }
                }
                else if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            //else if (Sddrrpfit.SelectedValue == "5")
            //{

            //    if (txtXXLFS2.Text == "0" || txtXXLFS2.Text == "")
            //    {
            //        txtXXLFS2.Text = "0";
            //    }
            //    else
            //    {

            //    }

            //    tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
            //         Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



            //    gndtot = gndtot + tot;

            //    double wid = 0;
            //    if (drpFit.SelectedValue == "3")
            //    {
            //        wid = Convert.ToDouble(txtformal.Text);
            //    }
            //    else if (drpFit.SelectedValue == "4")
            //    {
            //        wid = Convert.ToDouble(txtboys.Text);
            //    }
            //    else if (drpFit.SelectedValue == "5")
            //    {
            //        wid = Convert.ToDouble(txtcasual.Text);
            //    }

            //    double roundoff = Convert.ToDouble(tot) * wid;
            //    r = roundoff;

            //    txttotshirt1.Text = tot.ToString();
            //    avgmeter();

            //    if (sfs.Visible == true)
            //    {
            //        txtSFS2.Focus();
            //        if (txtSFS2.Text == "0")
            //        {
            //            txtSFS2.Text = "";
            //        }
            //    }
            //    else if (mfs.Visible == true)
            //    {
            //        txtMFS2.Focus();
            //        if (txtMFS2.Text == "0")
            //        {
            //            txtMFS2.Text = "";
            //        }
            //    }
            //    else if (lfs.Visible == true)
            //    {
            //        txtLFS2.Focus();
            //        if (txtLFS2.Text == "0")
            //        {
            //            txtLFS2.Text = "";
            //        }
            //    }
            //    else if (xlfs.Visible == true)
            //    {
            //        txtXLFS2.Focus();
            //        if (txtXLFS2.Text == "0")
            //        {
            //            txtXXLFS2.Text = "";
            //        }
            //    }
            //    //else if (fffs.Visible == true)
            //    //{
            //    //    txt44FS.Focus();
            //    //    if (txt44FS.Text == "0")
            //    //    {
            //    //        txt44FS.Text = "";
            //    //    }
            //    //}
            //    else if (shs.Visible == true)
            //    {
            //        txtSHS2.Focus();
            //        if (txtSHS2.Text == "0")
            //        {
            //            txtSHS2.Text = "";
            //        }
            //    }

            //    else if (mhs.Visible == true)
            //    {
            //        txtMHS2.Focus();
            //        if (txtMHS2.Text == "0")
            //        {
            //            txtMHS2.Text = "";
            //        }
            //    }
            //    else if (lhs.Visible == true)
            //    {
            //        txtLHS2.Focus();
            //        if (txtLHS2.Text == "0")
            //        {
            //            txtLHS2.Text = "";
            //        }
            //    }
            //    else if (xlhs.Visible == true)
            //    {
            //        txtXLHS2.Focus();
            //        if (txtXLHS2.Text == "0")
            //        {
            //            txtXLHS2.Text = "";
            //        }
            //    }
            //    else if (xxlhs.Visible == true)
            //    {
            //        txtXXLHS2.Focus();
            //        if (txtXXLHS2.Text == "0")
            //        {
            //            txtXXLHS2.Text = "";
            //        }
            //    }
            //    //else if (ffhs.Visible == true)
            //    //{
            //    //    txt44HS.Focus();
            //    //    if (txt44HS.Text == "0")
            //    //    {
            //    //        txt44HS.Text = "";
            //    //    }
            //    //}
            //}


            System.Threading.Thread.Sleep(30);
        }


        protected void Schange36hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt36HS.Text == "0" || txt36HS.Text == "")
                {
                    txt36HS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

               
                 if (tehs.Visible == true)
                {
                    txt38HS.Focus();
                    if (txt38HS.Text == "0")
                    {
                        txt38HS.Text = "";
                    }
                }
                else if (tnhs.Visible == true)
                {
                    txt39HS.Focus();
                    if (txt39HS.Text == "0")
                    {
                        txt39HS.Text = "";
                    }
                }
                else if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt28HS3.Text == "0" || txt28HS3.Text == "")
                {
                    txt28HS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

               
                 if (b30hs.Visible == true)
                {
                    txt30HS3.Focus();
                    if (txt30HS3.Text == "0")
                    {
                        txt30HS3.Text = "";
                    }
                }

                else if (b32hs.Visible == true)
                {
                    txt32HS3.Focus();
                    if (txt32HS3.Text == "0")
                    {
                        txt32HS3.Text = "";
                    }
                }
                else if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtSHS2.Text == "0" || txtSHS2.Text == "")
                {
                    txtSHS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

               
                //else if (fffs.Visible == true)
                //{
                //    txt44FS.Focus();
                //    if (txt44FS.Text == "0")
                //    {
                //        txt44FS.Text = "";
                //    }
                //}
               if (mhs.Visible == true)
                {
                    txtMHS2.Focus();
                    if (txtMHS2.Text == "0")
                    {
                        txtMHS2.Text = "";
                    }
                }
                else if (lhs.Visible == true)
                {
                    txtLHS2.Focus();
                    if (txtLHS2.Text == "0")
                    {
                        txtLHS2.Text = "";
                    }
                }
                else if (xlhs.Visible == true)
                {
                    txtXLHS2.Focus();
                    if (txtXLHS2.Text == "0")
                    {
                        txtXLHS2.Text = "";
                    }
                }
                else if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }


            System.Threading.Thread.Sleep(30);
        }

        protected void Schange38hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt38HS.Text == "0" || txt38HS.Text == "")
                {
                    txt38HS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                if (tnhs.Visible == true)
                {
                    txt39HS.Focus();
                    if (txt39HS.Text == "0")
                    {
                        txt39HS.Text = "";
                    }
                }
                else if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt30HS3.Text == "0" || txt30HS3.Text == "")
                {
                    txt30HS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                 if (b32hs.Visible == true)
                {
                    txt32HS3.Focus();
                    if (txt32HS3.Text == "0")
                    {
                        txt32HS3.Text = "";
                    }
                }
                else if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtMHS2.Text == "0" || txtMHS2.Text == "")
                {
                    txtMHS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

                if (lhs.Visible == true)
                {
                    txtLHS2.Focus();
                    if (txtLHS2.Text == "0")
                    {
                        txtLHS2.Text = "";
                    }
                }
                else if (xlhs.Visible == true)
                {
                    txtXLHS2.Focus();
                    if (txtXLHS2.Text == "0")
                    {
                        txtXLHS2.Text = "";
                    }
                }
                else if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }


            System.Threading.Thread.Sleep(30);
        }


        protected void Schange39hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt39HS.Text == "0" || txt39HS.Text == "")
                {
                    txt39HS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                if (fzhs.Visible == true)
                {
                    txt40HS.Focus();
                    if (txt40HS.Text == "0")
                    {
                        txt40HS.Text = "";
                    }
                }
                else if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt32HS3.Text == "0" || txt32HS3.Text == "")
                {
                    txt32HS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                if (b34hs.Visible == true)
                {
                    txt34HS3.Focus();
                    if (txt34HS3.Text == "0")
                    {
                        txt34HS3.Text = "";
                    }
                }
                else if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtLHS2.Text == "0" || txtLHS2.Text == "")
                {
                    txtLHS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

               if (xlhs.Visible == true)
                {
                    txtXLHS2.Focus();
                    if (txtXLHS2.Text == "0")
                    {
                        txtXLHS2.Text = "";
                    }
                }
                else if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }


            System.Threading.Thread.Sleep(30);
        }

        protected void Schange40hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt40HS.Text == "0" || txt40HS.Text == "")
                {
                    txt40HS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

               if (fths.Visible == true)
                {
                    txt42HS.Focus();
                    if (txt42HS.Text == "0")
                    {
                        txt42HS.Text = "";
                    }
                }
                else if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt34HS3.Text == "0" || txt34HS3.Text == "")
                {
                    txt34HS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                if (b36hs.Visible == true)
                {
                    txt36HS3.Focus();
                    if (txt36HS3.Text == "0")
                    {
                        txt36HS3.Text = "";
                    }
                }
                else if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtXLHS2.Text == "0" || txtXLHS2.Text == "")
                {
                    txtXLHS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

               if (xxlhs.Visible == true)
                {
                    txtXXLHS2.Focus();
                    if (txtXXLHS2.Text == "0")
                    {
                        txtXXLHS2.Text = "";
                    }
                }
                //else if (ffhs.Visible == true)
                //{
                //    txt44HS.Focus();
                //    if (txt44HS.Text == "0")
                //    {
                //        txt44HS.Text = "";
                //    }
                //}
            }


            System.Threading.Thread.Sleep(30);
        }

        protected void Schange42hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt42HS.Text == "0" || txt42HS.Text == "")
                {
                    txt42HS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                  
            
                
                
                
                
                 if (ffhs.Visible == true)
                {
                    txt44HS.Focus();
                    if (txt44HS.Text == "0")
                    {
                        txt44HS.Text = "";
                    }
                }
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt36HS3.Text == "0" || txt36HS3.Text == "")
                {
                    txt36HS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                if (b38hs.Visible == true)
                {
                    txt38HS3.Focus();
                    if (txt38HS3.Text == "0")
                    {
                        txt38HS3.Text = "";
                    }
                }

            }
            //Casual Fit
            else if (Sddrrpfit.SelectedValue == "5")
            {

                if (txtXXLHS2.Text == "0" || txtXXLHS2.Text == "")
                {
                    txtXXLHS2.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
                     Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt2.Text = tot.ToString();
                avgmeter();

             
            }


            System.Threading.Thread.Sleep(30);
        }

        protected void Schange44hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //Formal Fit
            if (Sddrrpfit.SelectedValue == "3")
            {
                if (txt44HS.Text == "0" || txt44HS.Text == "")
                {
                    txt44HS.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt1.Text = tot.ToString();
                avgmeter();

                //if (tsfs.Visible == true)
                //{
                //    txt36FS.Focus();
                //    if (txt36FS.Text == "0")
                //    {
                //        txt36FS.Text = "";
                //    }
                //}
                //else if (tefs.Visible == true)
                //{
                //    txt38FS.Focus();
                //    if (txt38FS.Text == "0")
                //    {
                //        txt38FS.Text = "";
                //    }
                //}
                //else if (tnfs.Visible == true)
                //{
                //    txt39FS.Focus();
                //    if (txt39FS.Text == "0")
                //    {
                //        txt39FS.Text = "";
                //    }
                //}
                //else if (fzfs.Visible == true)
                //{
                //    txt40FS.Focus();
                //    if (txt40FS.Text == "0")
                //    {
                //        txt40FS.Text = "";
                //    }
                //}
                //else if (ftfs.Visible == true)
                //{
                //    txt42FS.Focus();
                //    if (txt42FS.Text == "0")
                //    {
                //        txt42FS.Text = "";
                //    }
                //}
                //else if (fffs.Visible == true)
                //{
                //    txt44FS.Focus();
                //    if (txt44FS.Text == "0")
                //    {
                //        txt44FS.Text = "";
                //    }
                //}

                //else if (tshs.Visible == true)
                //{
                //    txt36HS.Focus();
                //    if (txt36HS.Text == "0")
                //    {
                //        txt36HS.Text = "";
                //    }
                //}
                //else if (tehs.Visible == true)
                //{
                //    txt38HS.Focus();
                //    if (txt38HS.Text == "0")
                //    {
                //        txt38HS.Text = "";
                //    }
                //}
                //else if (tnhs.Visible == true)
                //{
                //    txt39HS.Focus();
                //    if (txt39HS.Text == "0")
                //    {
                //        txt39HS.Text = "";
                //    }
                //}
                //else if (fzhs.Visible == true)
                //{
                //    txt40HS.Focus();
                //    if (txt40HS.Text == "0")
                //    {
                //        txt40HS.Text = "";
                //    }
                //}
                //else if (fths.Visible == true)
                //{
                //    txt42HS.Focus();
                //    if (txt42HS.Text == "0")
                //    {
                //        txt42HS.Text = "";
                //    }
                //}
            }
            //Boys Fit
            else if (Sddrrpfit.SelectedValue == "4")
            {
                if (txt38HS3.Text == "0" || txt38HS3.Text == "")
                {
                    txt38HS3.Text = "0";
                }
                else
                {

                }

                tot = Convert.ToDouble(txt28FS3.Text) + Convert.ToDouble(txt30FS3.Text) + Convert.ToDouble(txt32FS3.Text) + Convert.ToDouble(txt34FS3.Text) + Convert.ToDouble(txt36FS3.Text) + Convert.ToDouble(txt38FS3.Text) +
                     Convert.ToDouble(txt28HS3.Text) + Convert.ToDouble(txt30HS3.Text) + Convert.ToDouble(txt32HS3.Text) + Convert.ToDouble(txt34HS3.Text) + Convert.ToDouble(txt36HS3.Text) + Convert.ToDouble(txt38HS3.Text);



                gndtot = gndtot + tot;

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtformal.Text);
                }
                else if (drpFit.SelectedValue == "4")
                {
                    wid = Convert.ToDouble(txtboys.Text);
                }
                else if (drpFit.SelectedValue == "5")
                {
                    wid = Convert.ToDouble(txtcasual.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;

                txttotshirt3.Text = tot.ToString();
                avgmeter();

                //if (b28fs.Visible == true)
                //{
                //    txt28FS3.Focus();
                //    if (txt28FS3.Text == "0")
                //    {
                //        txt28FS3.Text = "";
                //    }
                //}
                //else if (b30fs.Visible == true)
                //{
                //    txt30FS3.Focus();
                //    if (txt30FS3.Text == "0")
                //    {
                //        txt30FS3.Text = "";
                //    }
                //}
                //else if (b32fs.Visible == true)
                //{
                //    txt32FS3.Focus();
                //    if (txt32FS3.Text == "0")
                //    {
                //        txt32FS3.Text = "";
                //    }
                //}
                //else if (b34fs.Visible == true)
                //{
                //    txt34FS3.Focus();
                //    if (txt34FS3.Text == "0")
                //    {
                //        txt34FS3.Text = "";
                //    }
                //}
                //else if (b36fs.Visible == true)
                //{
                //    txt36FS3.Focus();
                //    if (txt36FS3.Text == "0")
                //    {
                //        txt36FS3.Text = "";
                //    }
                //}
                //else if (b38fs.Visible == true)
                //{
                //    txt38FS3.Focus();
                //    if (txt38FS3.Text == "0")
                //    {
                //        txt38FS3.Text = "";
                //    }
                //}
                //else if (b28hs.Visible == true)
                //{
                //    txt28HS3.Focus();
                //    if (txt28HS3.Text == "0")
                //    {
                //        txt28HS3.Text = "";
                //    }
                //}

                //else if (b30hs.Visible == true)
                //{
                //    txt30HS3.Focus();
                //    if (txt30HS3.Text == "0")
                //    {
                //        txt30HS3.Text = "";
                //    }
                //}
                //else if (b32hs.Visible == true)
                //{
                //    txt32HS3.Focus();
                //    if (txt32HS3.Text == "0")
                //    {
                //        txt32HS3.Text = "";
                //    }
                //}
                //else if (b34hs.Visible == true)
                //{
                //    txt34HS3.Focus();
                //    if (txt34HS3.Text == "0")
                //    {
                //        txt34HS3.Text = "";
                //    }
                //}
                //else if (b36hs.Visible == true)
                //{
                //    txt36HS3.Focus();
                //    if (txt36HS3.Text == "0")
                //    {
                //        txt36HS3.Text = "";
                //    }
                //}

            }
            //Casual Fit
            //else if (Sddrrpfit.SelectedValue == "5")
            //{

            //    if (txtXXLHS2.Text == "0" || txtXXLHS2.Text == "")
            //    {
            //        txtXXLHS2.Text = "0";
            //    }
            //    else
            //    {

            //    }

            //    tot = Convert.ToDouble(txtSFS2.Text) + Convert.ToDouble(txtMFS2.Text) + Convert.ToDouble(txtLFS2.Text) + Convert.ToDouble(txtXLFS2.Text) + Convert.ToDouble(txtXXLFS2.Text) + Convert.ToDouble(0) +
            //         Convert.ToDouble(txtSHS2.Text) + Convert.ToDouble(txtMHS2.Text) + Convert.ToDouble(txtLHS2.Text) + Convert.ToDouble(txtXLHS2.Text) + Convert.ToDouble(txtXXLHS2.Text) + Convert.ToDouble(0);



            //    gndtot = gndtot + tot;

            //    double wid = 0;
            //    if (drpFit.SelectedValue == "3")
            //    {
            //        wid = Convert.ToDouble(txtformal.Text);
            //    }
            //    else if (drpFit.SelectedValue == "4")
            //    {
            //        wid = Convert.ToDouble(txtboys.Text);
            //    }
            //    else if (drpFit.SelectedValue == "5")
            //    {
            //        wid = Convert.ToDouble(txtcasual.Text);
            //    }

            //    double roundoff = Convert.ToDouble(tot) * wid;
            //    r = roundoff;

            //    txttotshirt1.Text = tot.ToString();
            //    avgmeter();

            //    if (sfs.Visible == true)
            //    {
            //        txtSFS2.Focus();
            //        if (txtSFS2.Text == "0")
            //        {
            //            txtSFS2.Text = "";
            //        }
            //    }
            //    else if (mfs.Visible == true)
            //    {
            //        txtMFS2.Focus();
            //        if (txtMFS2.Text == "0")
            //        {
            //            txtMFS2.Text = "";
            //        }
            //    }
            //    else if (lfs.Visible == true)
            //    {
            //        txtLFS2.Focus();
            //        if (txtLFS2.Text == "0")
            //        {
            //            txtLFS2.Text = "";
            //        }
            //    }
            //    else if (xlfs.Visible == true)
            //    {
            //        txtXLFS2.Focus();
            //        if (txtXLFS2.Text == "0")
            //        {
            //            txtXXLFS2.Text = "";
            //        }
            //    }
            //    //else if (fffs.Visible == true)
            //    //{
            //    //    txt44FS.Focus();
            //    //    if (txt44FS.Text == "0")
            //    //    {
            //    //        txt44FS.Text = "";
            //    //    }
            //    //}
            //    else if (xxlfs.Visible == true)
            //    {
            //        txtXXLFS2.Focus();
            //        if (txtXXLFS2.Text == "0")
            //        {
            //            txtXXLFS2.Text = "";
            //        }
            //    }

            //    else if (shs.Visible == true)
            //    {
            //        txtSHS2.Focus();
            //        if (txtSHS2.Text == "0")
            //        {
            //            txtSHS2.Text = "";
            //        }
            //    }
            //    else if (mhs.Visible == true)
            //    {
            //        txtMHS2.Focus();
            //        if (txtMHS2.Text == "0")
            //        {
            //            txtMHS2.Text = "";
            //        }
            //    }
            //    else if (lhs.Visible == true)
            //    {
            //        txtLHS2.Focus();
            //        if (txtLHS2.Text == "0")
            //        {
            //            txtLHS2.Text = "";
            //        }
            //    }
            //    else if (xlhs.Visible == true)
            //    {
            //        txtXLHS2.Focus();
            //        if (txtXLHS2.Text == "0")
            //        {
            //            txtXLHS2.Text = "";
            //        }
            //    }
            //    //else if (ffhs.Visible == true)
            //    //{
            //    //    txt44HS.Focus();
            //    //    if (txt44HS.Text == "0")
            //    //    {
            //    //        txt44HS.Text = "";
            //    //    }
            //    //}
            //}


            System.Threading.Thread.Sleep(30);
        }





        public void avgmeter()
        {
            if (Sddrrpfit.SelectedValue == "3")
            {
                txtavvgmeter.Text = (Convert.ToDouble(txtavamet1.Text) / Convert.ToDouble(txttotshirt1.Text)).ToString("N");
            }
            else if (Sddrrpfit.SelectedValue == "4")
            {
                txtavvgmeter3.Text = (Convert.ToDouble(txtavamet3.Text) / Convert.ToDouble(txttotshirt3.Text)).ToString("N");
            }
            else if (Sddrrpfit.SelectedValue == "5")
            {
                txtavvgmeter2.Text = (Convert.ToDouble(txtavamet2.Text) / Convert.ToDouble(txttotshirt2.Text)).ToString("N");
            }
        }




        //Multiple Process
        protected void change36fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;
            //   int tot = 0;
            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;

                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);


                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;
                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                // txt38fs.Focus();

                if (gridsize.Columns[3].Visible == true) //38FS
                {
                    if (txt38fs.Text == "0" || txt38fs.Text == "")
                    {
                        txt38fs.Text = "";
                    }
                    txt38fs.Focus();
                }
                else if (gridsize.Columns[4].Visible == true)//39Fs
                {
                    if (txt39fs.Text == "0" || txt39fs.Text == "")
                    {
                        txt39fs.Text = "";
                    }

                    txt39fs.Focus();
                }
                else if (gridsize.Columns[5].Visible == true)//40Fs
                {
                    if (txt40fs.Text == "0" || txt40fs.Text == "")
                    {
                        txt40fs.Text = "";
                    }

                    txt40fs.Focus();
                }

                else if (gridsize.Columns[6].Visible == true) //42FS
                {
                    if (txt42fs.Text == "0" || txt42fs.Text == "")
                    {
                        txt42fs.Text = "";
                    }

                    txt42fs.Focus();
                }
                else if (gridsize.Columns[7].Visible == true) //44FS
                {
                    if (txt44fs.Text == "0" || txt44fs.Text == "")
                    {
                        txt44fs.Text = "";
                    }

                    txt44fs.Focus();
                }

                else if (gridsize.Columns[8].Visible == true) //36HS
                {
                    if (txt36hs.Text == "0" || txt36hs.Text == "")
                    {
                        txt36hs.Text = "";
                    }

                    txt36hs.Focus();
                }
                else if (gridsize.Columns[9].Visible == true) //38HS
                {
                    if (txt38hs.Text == "0" || txt38hs.Text == "")
                    {
                        txt38hs.Text = "";
                    }

                    txt38hs.Focus();
                }

                else if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt38fs.Text == "0" || txt38fs.Text == "")
                //{
                //    txt38fs.Text = "";
                //}



                // dparty.Focus();
            }

            //for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            //{
            //    int cnt = gridsize.Rows.Count;
            //    TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
            //    if (vLoop >= 1)
            //    {
            //        TextBox oldtxt38fs = (TextBox)gridsize.Rows[vLoop-1].FindControl("dtxttefs");
            //        //    oldtxttk.Text = ".00";
            //        oldtxt38fs.Focus();
            //    }
            //    int tot2 = cnt - vLoop;
            //    if (tot2 == 1)
            //    {
            //        TextBox oldtxt38fs = (TextBox)gridsize.Rows[vLoop - 1].FindControl("dtxttefs");
            //        if (oldtxt38fs.Text == "0")
            //        {
            //            oldtxt38fs.Text = "";
            //            oldtxt38fs.Focus();
            //        }
            //        else
            //        {
            //            oldtxt38fs.Focus();
            //        }
            //    }


            //}


            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}

        }
        protected void change38fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();
            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;

                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                // txt39fs.Focus();
                //if (gridsize.Columns[3].Visible == true) //38FS
                //{
                //    if (txt38fs.Text == "0" || txt38fs.Text == "")
                //    {
                //        txt38fs.Text = "0";
                //    }
                //    txt38fs.Focus();
                //}
                if (gridsize.Columns[4].Visible == true)//39Fs
                {
                    if (txt39fs.Text == "0" || txt39fs.Text == "")
                    {
                        txt39fs.Text = "";
                    }

                    txt39fs.Focus();
                }
                else if (gridsize.Columns[5].Visible == true)//40Fs
                {
                    if (txt40fs.Text == "0" || txt40fs.Text == "")
                    {
                        txt40fs.Text = "";
                    }

                    txt40fs.Focus();
                }

                else if (gridsize.Columns[6].Visible == true) //42FS
                {
                    if (txt42fs.Text == "0" || txt42fs.Text == "")
                    {
                        txt42fs.Text = "";
                    }

                    txt42fs.Focus();
                }
                else if (gridsize.Columns[7].Visible == true) //44FS
                {
                    if (txt44fs.Text == "0" || txt44fs.Text == "")
                    {
                        txt44fs.Text = "";
                    }

                    txt44fs.Focus();
                }

                else if (gridsize.Columns[8].Visible == true) //36HS
                {
                    if (txt36hs.Text == "0" || txt36hs.Text == "")
                    {
                        txt36hs.Text = "";
                    }

                    txt36hs.Focus();
                }
                else if (gridsize.Columns[9].Visible == true) //38HS
                {
                    if (txt38hs.Text == "0" || txt38hs.Text == "")
                    {
                        txt38hs.Text = "";
                    }

                    txt38hs.Focus();
                }

                else if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt39fs.Text == "0" || txt39fs.Text == "")
                //{
                //    txt39fs.Text = "";
                //}
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //  if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //      btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change39fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //   txt40fs.Focus();
                if (gridsize.Columns[5].Visible == true)//40Fs
                {
                    if (txt40fs.Text == "0" || txt40fs.Text == "")
                    {
                        txt40fs.Text = "";
                    }

                    txt40fs.Focus();
                }

                else if (gridsize.Columns[6].Visible == true) //42FS
                {
                    if (txt42fs.Text == "0" || txt42fs.Text == "")
                    {
                        txt42fs.Text = "";
                    }

                    txt42fs.Focus();
                }
                else if (gridsize.Columns[7].Visible == true) //44FS
                {
                    if (txt44fs.Text == "0" || txt44fs.Text == "")
                    {
                        txt44fs.Text = "";
                    }

                    txt44fs.Focus();
                }

                else if (gridsize.Columns[8].Visible == true) //36HS
                {
                    if (txt36hs.Text == "0" || txt36hs.Text == "")
                    {
                        txt36hs.Text = "";
                    }

                    txt36hs.Focus();
                }
                else if (gridsize.Columns[9].Visible == true) //38HS
                {
                    if (txt38hs.Text == "0" || txt38hs.Text == "")
                    {
                        txt38hs.Text = "";
                    }

                    txt38hs.Focus();
                }

                else if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt40fs.Text == "0" || txt40fs.Text == "")
                //{
                //    txt40fs.Text = "";
                //}
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //  if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //     btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change40fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);


                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //     txt42fs.Focus();
                if (gridsize.Columns[6].Visible == true) //42FS
                {
                    if (txt42fs.Text == "0" || txt42fs.Text == "")
                    {
                        txt42fs.Text = "";
                    }

                    txt42fs.Focus();
                }
                else if (gridsize.Columns[7].Visible == true) //44FS
                {
                    if (txt44fs.Text == "0" || txt44fs.Text == "")
                    {
                        txt44fs.Text = "";
                    }

                    txt44fs.Focus();
                }

                else if (gridsize.Columns[8].Visible == true) //36HS
                {
                    if (txt36hs.Text == "0" || txt36hs.Text == "")
                    {
                        txt36hs.Text = "";
                    }

                    txt36hs.Focus();
                }
                else if (gridsize.Columns[9].Visible == true) //38HS
                {
                    if (txt38hs.Text == "0" || txt38hs.Text == "")
                    {
                        txt38hs.Text = "";
                    }

                    txt38hs.Focus();
                }

                else if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt42fs.Text == "0" || txt42fs.Text == "")
                //{
                //    txt42fs.Text = "";
                //}
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //   if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //      btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change42fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //  txt44fs.Focus();
                if (gridsize.Columns[7].Visible == true) //44FS
                {
                    if (txt44fs.Text == "0" || txt44fs.Text == "")
                    {
                        txt44fs.Text = "";
                    }

                    txt44fs.Focus();
                }

                else if (gridsize.Columns[8].Visible == true) //36HS
                {
                    if (txt36hs.Text == "0" || txt36hs.Text == "")
                    {
                        txt36hs.Text = "";
                    }

                    txt36hs.Focus();
                }
                else if (gridsize.Columns[9].Visible == true) //38HS
                {
                    if (txt38hs.Text == "0" || txt38hs.Text == "")
                    {
                        txt38hs.Text = "";
                    }

                    txt38hs.Focus();
                }

                else if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt44fs.Text == "0" || txt44fs.Text == "")
                //{
                //    txt44fs.Text = "";
                //}
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            // if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //      btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change44fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                // txt36hs.Focus();
                if (gridsize.Columns[8].Visible == true) //36HS
                {
                    if (txt36hs.Text == "0" || txt36hs.Text == "")
                    {
                        txt36hs.Text = "";
                    }

                    txt36hs.Focus();
                }
                else if (gridsize.Columns[9].Visible == true) //38HS
                {
                    if (txt38hs.Text == "0" || txt38hs.Text == "")
                    {
                        txt38hs.Text = "";
                    }

                    txt38hs.Focus();
                }

                else if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt36hs.Text == "0" || txt36hs.Text == "")
                //{
                //    txt36hs.Text = "";
                //}
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            // if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //      btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }

        protected void change36hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //  txt38hs.Focus();
                if (gridsize.Columns[9].Visible == true) //38HS
                {
                    if (txt38hs.Text == "0" || txt38hs.Text == "")
                    {
                        txt38hs.Text = "";
                    }

                    txt38hs.Focus();
                }

                else if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt38hs.Text == "0" || txt38hs.Text == "")
                //{
                //    txt38hs.Text = "";
                //}
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //  if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change38hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                // txt39hs.Focus();
                if (gridsize.Columns[10].Visible == true) //39HS
                {
                    if (txt39hs.Text == "0" || txt39hs.Text == "")
                    {
                        txt39hs.Text = "";
                    }

                    txt39hs.Focus();
                }
                else if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt39hs.Text == "0" || txt39hs.Text == "")
                //{
                //    txt39hs.Text = "";
                //}
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //  if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change39hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //  txt40hs.Focus();
                if (gridsize.Columns[11].Visible == true) //40HS
                {
                    if (txt40hs.Text == "0" || txt40hs.Text == "")
                    {
                        txt40hs.Text = "";
                    }

                    txt40hs.Focus();
                }

                else if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt40hs.Text == "0" || txt40hs.Text == "")
                //{
                //    txt40hs.Text = "";
                //}
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //   if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change40hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);


                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;
                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //  txt42hs.Focus();
                if (gridsize.Columns[12].Visible == true) //42HS
                {
                    if (txt42hs.Text == "0" || txt42hs.Text == "")
                    {
                        txt42hs.Text = "";
                    }

                    txt42hs.Focus();
                }
                else if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt42hs.Text == "0" || txt42hs.Text == "")
                //{
                //    txt42hs.Text = "";
                //}
                //// dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //     if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change42hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //       gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //  txt44hs.Focus();
                if (gridsize.Columns[13].Visible == true) //44HS
                {
                    if (txt44hs.Text == "0" || txt44hs.Text == "")
                    {
                        txt44hs.Text = "";
                    }

                    txt44hs.Focus();
                }
                //if (txt44hs.Text == "0" || txt44hs.Text == "")
                //{
                //    txt44hs.Text = "";
                //}


                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //      if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
        protected void change44hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();
                    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                //txt44hs.Focus();
                //if (txt44hs.Text == "0" || txt44hs.Text == "")
                //{
                //    txt44hs.Text = "";
                //}
                txtwsp.Focus();
                // dparty.Focus();
            }
            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //      if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }

        public void getmultiplesizesetting()
        {
            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                if (txt36fs.Text == "")
                {
                    txt36fs.Text = "0";
                }
                if (txt38fs.Text == "")
                {
                    txt38fs.Text = "0";
                }
                if (txt39fs.Text == "")
                {
                    txt39fs.Text = "0";
                }
                if (txt40fs.Text == "")
                {
                    txt40fs.Text = "0";
                }
                if (txt42fs.Text == "")
                {
                    txt42fs.Text = "0";
                }
                if (txt44fs.Text == "")
                {
                    txt44fs.Text = "0";
                }

                if (txt36hs.Text == "")
                {
                    txt36hs.Text = "0";
                }
                if (txt38hs.Text == "")
                {
                    txt38hs.Text = "0";
                }
                if (txt39hs.Text == "")
                {
                    txt39hs.Text = "0";
                }
                if (txt40hs.Text == "")
                {
                    txt40hs.Text = "0";
                }
                if (txt42hs.Text == "")
                {
                    txt42hs.Text = "0";
                }
                if (txt44hs.Text == "")
                {
                    txt44hs.Text = "0";
                }
            }

        }


        protected void granddiscount(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;


            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
                TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                gndtot = gndtot + tot;
                gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);
                int col = vLoop + 1;

                DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtformal.Text);
                    }
                    else if (drpFit.SelectedValue == "4")
                    {
                        wid = Convert.ToDouble(txtboys.Text);
                    }
                    else if (drpFit.SelectedValue == "5")
                    {
                        wid = Convert.ToDouble(txtcasual.Text);
                    }

                    double roundoff = Convert.ToDouble(tot) * wid;
                    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //if (roundoff > 0.5)
                    //{
                    //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //}
                    //else
                    //{
                    //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //}
                    r = roundoff;

                    txtreqmeter.Text = r.ToString();
                    txtshirt.Text = tot.ToString();

                }

                txt38fs.Focus();

                // dparty.Focus();
            }


            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //    if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //}
        }
    }
}