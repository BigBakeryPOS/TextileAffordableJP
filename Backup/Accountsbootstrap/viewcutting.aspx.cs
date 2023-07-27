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
    public partial class viewcutting : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string superadmin = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            superadmin = Session["IsSuperAdmin"].ToString();
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dst = objBs.Alljobworkmaster();
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddljobworker.DataSource = dst.Tables[0];
                        ddljobworker.DataTextField = "LedgerName";
                        ddljobworker.DataValueField = "LedgerID";
                        ddljobworker.DataBind();
                        ddljobworker.Items.Insert(0, "ALL");
                    }
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
                        drpbranch.Items.Insert(0, "Select Branch");
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




            }
        }






        protected void Date_OnTextChanged(object sender, EventArgs e)
        {
            DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = objBs.Selectprecutting(drpbranch.SelectedValue, ddljobworker.SelectedValue, FromDate, ToDate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvcust.DataSource = ds;
                gvcust.DataBind();
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }
        protected void ddljobworker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Date_OnTextChanged(sender, e);
        }
        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {
            DataSet ds = objBs.selectCutprocess(drpbranch.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.DataBind();
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

        protected void Add_Click(object sender, EventArgs e)
        {

            //Response.Redirect("../Accountsbootstrap/Cuttingprocess.aspx");
            Response.Redirect("../Accountsbootstrap/NewPrecutprocess.aspx");

        }

        protected void Add1_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/NewPrecutprocess.aspx");

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectCheque();
            }
            else
            {
                ds = objBs.searchfilterCheque(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.PageIndex = e.NewPageIndex;
                    gvcust.DataBind();
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

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchviewprocess(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.DataBind();
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
        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("RatiowisePreCut.aspx?CuttingID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.deleteprecut(Convert.ToInt32(e.CommandArgument.ToString()));
                Response.Redirect("viewcutting.aspx");
            }
            else if (e.CommandName == "print")
            {
                Response.Redirect("PrintCutting.aspx?iCutID=" + e.CommandArgument.ToString());
            }

            else if (e.CommandName == "custprint")
            {
                Response.Redirect("PrintCuttingnew.aspx?iCutID=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "labelprint")
            {
                Response.Redirect("Customerlabelprint.aspx?iCutID=" + e.CommandArgument.ToString());
            }

            else if (e.CommandName == "AddProcess")
            {
                #region

                btnClear_OnClick(sender, e);


                lblcutid.Text = "";
                DataSet dsprocess = objBs.Getallprocess(e.CommandArgument.ToString());
                if (dsprocess.Tables[0].Rows.Count > 0)
                {
                    lblcutid.Text = e.CommandArgument.ToString();

                    #region
                    for (int i = 0; i < dsprocess.Tables[0].Rows.Count; i++)
                    {
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Stc")
                        {
                            Nchkstch.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchkstch.Enabled = false;
                            lblNchkstch.Text = "1";
                        }
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Kaja")
                        {
                            Nchkkbut.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchkkbut.Enabled = false;
                            lblNchkkbut.Text = "1";
                        }
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Emb")
                        {
                            Nchkemb.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchkemb.Enabled = false;
                            lblNchkemb.Text = "1";
                        }
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Wash")
                        {
                            Nchkwash.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchkwash.Enabled = false;
                            lblNchkwash.Text = "1";
                        }
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Print")
                        {
                            Nchkprint.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchkprint.Enabled = false;
                            lblNchkprint.Text = "1";
                        }
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Iron")
                        {
                            Nchkiron.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchkiron.Enabled = false;
                            lblNchkiron.Text = "1";
                        }
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Btag")
                        {
                            Nchkbartag.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchkbartag.Enabled = false;
                            lblNchkbartag.Text = "1";
                        }
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Trm")
                        {
                            Nchktrimming.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchktrimming.Enabled = false;
                            lblNchktrimming.Text = "1";
                        }
                        if (dsprocess.Tables[0].Rows[i]["Screen"].ToString() == "Cni")
                        {
                            Nchkconsai.SelectedValue = dsprocess.Tables[0].Rows[i]["Jobwork"].ToString();
                            Nchkconsai.Enabled = false;
                            lblNchkconsai.Text = "1";
                        }


                    }

                    #endregion
                }
                //DataSet dsLotCombination = objBs.Getstichingsnocontra(Convert.ToInt32(e.CommandArgument.ToString()));
                //if (dsLotCombination.Tables[0].Rows.Count > 0)
                //{
                //    txtLotCombination.Text = dsLotCombination.Tables[0].Rows[0]["LotCombination"].ToString();
                //}
                mpe.Show();
                #endregion

            }
            else if (e.CommandName == "EditC")
            {


                DataSet getcuttingdetails = objBs.getprecuttingDetailsTab1(Convert.ToInt32(e.CommandArgument));
                if (getcuttingdetails.Tables[0].Rows.Count > 0)
                {
                    lblcutid.Text = e.CommandArgument.ToString();

                    lblcutidnew.Text = getcuttingdetails.Tables[0].Rows[0]["cutid"].ToString();
                    lbljobtype.Text = getcuttingdetails.Tables[0].Rows[0]["worktypeid"].ToString();

                    #region GET CUTTING DETAILS
                    if (lbljobtype.Text == "1")
                    {
                        DataSet dst = objBs.Getcuttmastrr();
                        if (dst != null)
                        {
                            if (dst.Tables[0].Rows.Count > 0)
                            {
                                drpcutting.DataSource = dst.Tables[0];
                                drpcutting.DataTextField = "LedgerName";
                                drpcutting.DataValueField = "LedgerID";
                                drpcutting.DataBind();
                                drpcutting.Items.Insert(0, "Select Cutting Name");
                            }
                        }
                    }
                    else
                    {
                        DataSet dst = objBs.Getjobworkmastrr();
                        if (dst != null)
                        {
                            if (dst.Tables[0].Rows.Count > 0)
                            {
                                drpcutting.DataSource = dst.Tables[0];
                                drpcutting.DataTextField = "LedgerName";
                                drpcutting.DataValueField = "LedgerID";
                                drpcutting.DataBind();
                                drpcutting.Items.Insert(0, "Select Job Name");
                            }
                        }
                    }

                    DataSet dbitem = objBs.griditem();
                    if (dbitem.Tables[0].Rows.Count > 0)
                    {
                        drpitemtype.DataSource = dbitem.Tables[0];
                        drpitemtype.DataTextField = "ItemName";
                        drpitemtype.DataValueField = "itemid";
                        drpitemtype.DataBind();
                        drpitemtype.Items.Insert(0, "Select Item");
                    }

                    DataSet brandName = objBs.getBrandName();
                    if (brandName.Tables[0].Rows.Count > 0)
                    {
                        ddlbrand.DataSource = brandName.Tables[0];
                        ddlbrand.DataTextField = "name";
                        ddlbrand.DataValueField = "BrandID";
                        ddlbrand.DataBind();
                        ddlbrand.Items.Insert(0, "Select Brand Name");
                    }
                    DataSet dfit = objBs.Fit();
                    if (dfit.Tables[0].Rows.Count > 0)
                    {
                        drpNchkfit.DataSource = dfit.Tables[0];
                        drpNchkfit.DataTextField = "Fit";
                        drpNchkfit.DataValueField = "Fitid";
                        drpNchkfit.DataBind();
                        drpNchkfit.Items.Insert(0, "Select Fit");
                    }

                    drpcutting.SelectedValue = getcuttingdetails.Tables[0].Rows[0]["cuttingmaster"].ToString();

                    drpitemtype.SelectedValue = getcuttingdetails.Tables[0].Rows[0]["itemid"].ToString();
                    lblitemid.Text = getcuttingdetails.Tables[0].Rows[0]["itemid"].ToString();
                    lblitemlotcode.Text = getcuttingdetails.Tables[0].Rows[0]["itemcode"].ToString();
                    lblitemcode.Text = getcuttingdetails.Tables[0].Rows[0]["itemcode"].ToString();
                    txtitemlotno.Text = getcuttingdetails.Tables[0].Rows[0]["itemlotno"].ToString();
                    lblitemlotno.Text = getcuttingdetails.Tables[0].Rows[0]["itemlotno"].ToString();

                    txtdate.Text = Convert.ToDateTime(getcuttingdetails.Tables[0].Rows[0]["deliverydate"]).ToString("dd/MM/yyyy");
                    txtdeliverydate.Text = Convert.ToDateTime(getcuttingdetails.Tables[0].Rows[0]["deldate"]).ToString("dd/MM/yyyy");

                    ddlbrand.SelectedValue = getcuttingdetails.Tables[0].Rows[0]["brandid"].ToString();
                    drpNchkfit.SelectedValue = getcuttingdetails.Tables[0].Rows[0]["fit"].ToString();
                    drpnewsleevetype.SelectedValue = getcuttingdetails.Tables[0].Rows[0]["sleevetype"].ToString();
                    drpnewlabeltype.SelectedValue = getcuttingdetails.Tables[0].Rows[0]["labeltype"].ToString();
                    #endregion



                    mpecut.Show();
                }
            }
            else if (e.CommandName == "EditF")
            {

                DataSet ds2345 = objBs.getfabricusedforedit((e.CommandArgument.ToString()));
                if (ds2345.Tables[0].Rows.Count > 0)
                {
                    lblcutid.Text = e.CommandArgument.ToString();

                    GridView2.DataSource = ds2345;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }

                mpefab.Show();
            }
        }

        protected void Itemlotnumber_chnaged(object sender, EventArgs e)
        {

            if (drpitemtype.SelectedValue == "Select Item")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Item. Thank you!!');", true);
                return;
            }
            else
            {

                if (lblitemid.Text == drpitemtype.SelectedValue)
                {
                    lblitemlotcode.Text = lblitemcode.Text;
                    txtitemlotno.Text = lblitemlotno.Text;
                }
                else
                {

                    DataSet getmaxitemlot = objBs.getmaxlotno(drpitemtype.SelectedValue);
                    if (getmaxitemlot.Tables[0].Rows.Count > 0)
                    {
                        DataSet getitemcode = objBs.getitemvalue(drpitemtype.SelectedValue);
                        if (getitemcode.Tables[0].Rows.Count > 0)
                        {
                            lblitemlotcode.Text = getitemcode.Tables[0].Rows[0]["Itemcode"].ToString();
                            //  txtavgmeter.Text = Convert.ToDouble(getitemcode.Tables[0].Rows[0]["AvgGms"]).ToString("0.000");
                            string itemlotno = getmaxitemlot.Tables[0].Rows[0]["itemlotno"].ToString();

                            txtitemlotno.Text = String.Format("{0:0000}", Convert.ToInt32(itemlotno)); ;
                            txtitemlotno.Focus();
                        }
                    }
                }

            }
            //  updpanel.Update();
            mpecut.Show();

        }

        protected void brandindexchnaged(object sender, EventArgs e)
        {

            DataSet branchd = objBs.getbrandnameforcuttprocessnewww(ddlbrand.SelectedValue, "1");

            if (branchd.Tables[0].Rows.Count > 0)
            {

                drpNchkfit.SelectedValue = branchd.Tables[0].Rows[0]["fitid"].ToString();

            }
            // updpanel.Update();
            mpecut.Show();
        }


        protected void btneditfab_OnClick(object sender, EventArgs e)
        {


            for (int vLoop1 = 0; vLoop1 < GridView2.Rows.Count; vLoop1++)
            {


                Label lblcuttingid = (Label)GridView2.Rows[vLoop1].FindControl("lblcuttingid");
                Label lbltransfabid = (Label)GridView2.Rows[vLoop1].FindControl("lbltransfabid");

                Label lblavlkg = (Label)GridView2.Rows[vLoop1].FindControl("lblavlkg");
                Label lblreqkg = (Label)GridView2.Rows[vLoop1].FindControl("lblreqkg");

                TextBox txtchnagekg = (TextBox)GridView2.Rows[vLoop1].FindControl("txtchnagekg");

                if (txtchnagekg.Text == "")
                    txtchnagekg.Text = "0";

                if (txtchnagekg.Text == "0")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check All Fabric.It May Not be Zero Or Empty.Thank you!!!');", true);
                    //txtchnagekg.Focus();
                    //mpefab.Show();
                    //return;
                }

                double alterkg = Convert.ToDouble(txtchnagekg.Text);
                double avlkg = Convert.ToDouble(lblavlkg.Text);
                double reqkg = Convert.ToDouble(lblreqkg.Text);

                double totkg = avlkg + reqkg;

                if (alterkg > totkg)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Alter Fabric is Greater Than Avl.Fabric.Thank you!!!');", true);
                    txtchnagekg.Focus();
                    mpefab.Show();
                    return;
                }
                else
                {

                }
            }

            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                Label lblcuttingid = (Label)GridView2.Rows[vLoop].FindControl("lblcuttingid");
                Label lbltransfabid = (Label)GridView2.Rows[vLoop].FindControl("lbltransfabid");
                Label lblshirttype = (Label)GridView2.Rows[vLoop].FindControl("lblshirttype");
                Label lblavlkg = (Label)GridView2.Rows[vLoop].FindControl("lblavlkg");
                Label lblreqkg = (Label)GridView2.Rows[vLoop].FindControl("lblreqkg");

                TextBox txtchnagekg = (TextBox)GridView2.Rows[vLoop].FindControl("txtchnagekg");

                if (txtchnagekg.Text == "")
                    txtchnagekg.Text = "0";

                if (txtchnagekg.Text == "0")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check All Fabric.It May Not be Zero Or Empty.Thank you!!!');", true);
                    //txtchnagekg.Focus();
                    //mpefab.Show();
                    //return;
                }
                else
                {

                    int iss = objBs.updatefabric(lblcuttingid.Text, lbltransfabid.Text, Convert.ToDouble(lblavlkg.Text), Convert.ToDouble(lblreqkg.Text), Convert.ToDouble(txtchnagekg.Text), lblshirttype.Text);
                }
            }

        }
        protected void btneditcut_OnClick(object sender, EventArgs e)
        {

            DateTime cuttingdate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime deliverydate = DateTime.ParseExact(txtdeliverydate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);



            int iscuess = objBs.Updatecuttingdetails(lblcutid.Text, drpcutting.SelectedValue, drpitemtype.SelectedValue, lblitemlotcode.Text, txtitemlotno.Text, cuttingdate, deliverydate, ddlbrand.SelectedValue, drpNchkfit.SelectedValue, drpnewsleevetype.SelectedValue, drpnewlabeltype.SelectedValue);

            Response.Redirect("viewcutting.aspx");

        }
        protected void btnAddprocess_OnClick(object sender, EventArgs e)
        {
            if (rdbmode.SelectedValue == "1")
            {
                DataSet dscurrenttbl = objBs.getcurrenttbl(Convert.ToInt32(lblcutid.Text));
                if (dscurrenttbl.Tables[0].Rows.Count > 0)
                {
                    int TotalQty = 0; int FitId = 0; string ItemName = ""; string Pattern = "";

                    DataSet dstransLotDetails = objBs.gettransLotDetails(Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString()));
                    if (dstransLotDetails.Tables[0].Rows.Count > 0)
                    {
                        #region
                        TotalQty = Convert.ToInt32(dstransLotDetails.Tables[0].Rows[0]["TotalQty"].ToString());
                        FitId = Convert.ToInt32(dstransLotDetails.Tables[0].Rows[0]["FitId"].ToString());
                        ItemName = dstransLotDetails.Tables[0].Rows[0]["ItemName"].ToString();
                        Pattern = dstransLotDetails.Tables[0].Rows[0]["Pattern"].ToString();
                        #endregion
                    }

                    #region
                    DataSet dsProcessStockRatio = objBs.getProcessStockRatio(Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString()));
                    if (Nchkstch.SelectedValue != "" && lblNchkstch.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();

                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Stc", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "IsStiching", TotalQty, FitId, ItemName, Pattern, 2);

                        #endregion

                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqty(2, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "STICHING", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }
                    if (Nchkkbut.SelectedValue != "" && lblNchkkbut.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Kaja", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "Iskaja", TotalQty, FitId, ItemName, Pattern, 1);

                        #endregion
                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqty(1, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "KAJA BUTTON", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }
                    if (Nchkemb.SelectedValue != "" && lblNchkemb.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Emb", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "IsEmb", TotalQty, FitId, ItemName, Pattern, 3);

                        #endregion
                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqty(3, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "EMBROIDERING", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }
                    if (Nchkwash.SelectedValue != "" && lblNchkwash.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Wash", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "Iswash", TotalQty, FitId, ItemName, Pattern, 4);

                        #endregion
                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqty(4, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "WASHING", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }
                    if (Nchkprint.SelectedValue != "" && lblNchkprint.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Print", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "IsPrint", TotalQty, FitId, ItemName, Pattern, 7);

                        #endregion
                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqty(7, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "PRINTING", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }
                    if (Nchkiron.SelectedValue != "" && lblNchkiron.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Iron", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "IsIron", TotalQty, FitId, ItemName, Pattern, 5);

                        #endregion
                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqtyonlyiron(5, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "IRON", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }
                    if (Nchkbartag.SelectedValue != "" && lblNchkbartag.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Btag", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "Isbartag", TotalQty, FitId, ItemName, Pattern, 8);

                        #endregion
                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqty(8, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "BarTag Process", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }
                    if (Nchktrimming.SelectedValue != "" && lblNchktrimming.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Trm", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "IsTrim", TotalQty, FitId, ItemName, Pattern, 9);

                        #endregion
                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqty(9, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "Trimming Process", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }
                    if (Nchkconsai.SelectedValue != "" && lblNchkconsai.Text != "1")
                    {
                        #region

                        int Cutid = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["Cutid"].ToString());
                        int LotNo = Convert.ToInt32(dscurrenttbl.Tables[0].Rows[0]["LotNo"].ToString());
                        string Counts = dscurrenttbl.Tables[0].Rows[0]["Counts"].ToString();
                        string Jobwork = dscurrenttbl.Tables[0].Rows[0]["Jobwork"].ToString();
                        string IsComplete = dscurrenttbl.Tables[0].Rows[0]["IsComplete"].ToString();
                        string LotDetailsId = dscurrenttbl.Tables[0].Rows[0]["LotDetailsId"].ToString();
                        string CompanyLotNo = dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                        int patinsertid = objBs.insertcurrentpro(Cutid, LotNo, "Cni", Counts, Nchkstch.SelectedValue, LotDetailsId, CompanyLotNo, "IsConsai", TotalQty, FitId, ItemName, Pattern, 10);

                        #endregion
                        if (dsProcessStockRatio.Tables[0].Rows.Count > 0)
                        {
                            #region

                            for (int r = 0; r < dsProcessStockRatio.Tables[0].Rows.Count; r++)
                            {
                                int Masterid = 0; int Cutid_Pro = 0; int Transfabid = 0; string DesignCode = "";
                                int BrandId = 0; int Fit = 0; string Itemname = ""; int Companyid = 0;
                                string CompanyLotNo_Pro = ""; int Totalshirt = 0; int Damageshirt = 0;

                                int s30FS = 0; int s32FS = 0; int s34FS = 0; int s36FS = 0;
                                int XSFS = 0; int SFS = 0; int MFS = 0; int LFS = 0;
                                int XLFS = 0; int XXLFS = 0; int s3XLFS = 0; int s4XLFS = 0;

                                int s30HS = 0; int s32HS = 0; int s34HS = 0; int s36HS = 0;
                                int XSHS = 0; int SHS = 0; int MHS = 0; int LHS = 0;
                                int XLHS = 0; int XXLHS = 0; int s3XLHS = 0; int s4XLHS = 0;

                                Masterid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Masterid"].ToString());
                                Cutid_Pro = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Cutid"].ToString());
                                Transfabid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Transfabid"].ToString());
                                DesignCode = dsProcessStockRatio.Tables[0].Rows[r]["DesignCode"].ToString();
                                BrandId = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["BrandId"].ToString());
                                Fit = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Fit"].ToString());
                                Itemname = dsProcessStockRatio.Tables[0].Rows[r]["Itemname"].ToString();
                                Companyid = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Companyid"].ToString());
                                CompanyLotNo_Pro = dsProcessStockRatio.Tables[0].Rows[r]["CompanyLotNo"].ToString();
                                Totalshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Totalshirt"].ToString());
                                Damageshirt = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["Damageshirt"].ToString());

                                s30FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30FS"].ToString());
                                s32FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32FS"].ToString());
                                s34FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34FS"].ToString());
                                s36FS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36FS"].ToString());
                                XSFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSFS"].ToString());
                                SFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SFS"].ToString());
                                MFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MFS"].ToString());
                                LFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LFS"].ToString());
                                XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLFS"].ToString());
                                XXLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLFS"].ToString());
                                s3XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLFS"].ToString());
                                s4XLFS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLFS"].ToString());

                                s30HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["30HS"].ToString());
                                s32HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["32HS"].ToString());
                                s34HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["34HS"].ToString());
                                s36HS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["36HS"].ToString());
                                XSHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XSHS"].ToString());
                                SHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["SHS"].ToString());
                                MHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["MHS"].ToString());
                                LHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["LHS"].ToString());
                                XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XLHS"].ToString());
                                XXLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["XXLHS"].ToString());
                                s3XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["3XLHS"].ToString());
                                s4XLHS = Convert.ToInt32(dsProcessStockRatio.Tables[0].Rows[r]["4XLHS"].ToString());

                                int istockid = objBs.insetprocessratioqty(10, Masterid, Cutid_Pro, Transfabid, DesignCode, BrandId, Fit, Itemname, Companyid, CompanyLotNo_Pro, Totalshirt, Damageshirt, s30FS, s32FS, s34FS, s36FS, XSFS, SFS, MFS, LFS, XLFS, XXLFS, s3XLFS, s4XLFS, s30HS, s32HS, s34HS, s36HS, XSHS, SHS, MHS, LHS, XLHS, XXLHS, s3XLHS, s4XLHS);
                            }
                            #endregion

                            #region
                            DataSet dsPreCosting = objBs.getPreCosting(dscurrenttbl.Tables[0].Rows[0]["CompanyLotNo"].ToString());
                            if (dsPreCosting.Tables[0].Rows.Count > 0)
                            {
                                int MasterIdcost = Convert.ToInt32(dsPreCosting.Tables[0].Rows[0]["MasterId"].ToString());
                                string LotNocost = dsPreCosting.Tables[0].Rows[0]["LotNo"].ToString();

                                int costsave = objBs.savecost(Convert.ToInt32(MasterIdcost), "Consai Process", Convert.ToDouble(0.00), LotNocost);
                            }
                            #endregion
                        }
                    }

                    #endregion
                }
            }
            //if (rdbmode.SelectedValue == "2")
            //{
            //    int insertLotCombination = objBs.addLotCombination(txtLotCombination.Text, lblcutid.Text);
            //}
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            lblcutid.Text = "";

            //if (lblNchkstch.Text != "1")
            {
                Nchkstch.ClearSelection();
            }
            //  if (lblNchkemb.Text != "1")
            {
                Nchkemb.ClearSelection();
            }
            // if (lblNchkkbut.Text != "1")
            {
                Nchkkbut.ClearSelection();
            }
            //   if (lblNchkwash.Text != "1")
            {
                Nchkwash.ClearSelection();
            }
            //  if (lblNchkprint.Text != "1")
            {
                Nchkprint.ClearSelection();
            }
            //   if (lblNchkiron.Text != "1")
            {
                Nchkiron.ClearSelection();
            }
            //  if (lblNchkbartag.Text != "1")
            {
                Nchkbartag.ClearSelection();
            }
            // if (lblNchktrimming.Text != "1")
            {
                Nchktrimming.ClearSelection();
            }
            //  if (lblNchkconsai.Text != "1")
            {
                Nchkconsai.ClearSelection();
            }

        }
        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gv = e.Row.FindControl("gvfabfetails") as GridView;



                string cutid = e.Row.Cells[4].Text;
                string[] tokens = cutid.Split('@');
                cutid = tokens[0];

                DataSet ds = objBs.getfabdetailsforcutting(cutid);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }




                #region
                if (superadmin == "1")
                {
                    if (objBs.chkmasterforcut(cutid))
                    {

                        ((Image)e.Row.FindControl("img")).Visible = false;
                        ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;


                        ((Image)e.Row.FindControl("dlt")).Visible = false;
                        ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = true;

                    }
                    else
                    {

                        ((Image)e.Row.FindControl("img")).Visible = true;
                        ((ImageButton)e.Row.FindControl("imgdisable")).Visible = false;


                        ((Image)e.Row.FindControl("dlt")).Visible = true;
                        ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = false;
                    }
                }
                #endregion

            }
        }
    }
}